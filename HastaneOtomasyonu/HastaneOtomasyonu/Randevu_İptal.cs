using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace HastaneOtomasyonu
{
    public partial class Randevu_İptal : Form
    {
        private string connectionString = "Data Source=.\\SQLEXPRESS,55222;Initial Catalog=hastane;Integrated Security=True;TrustServerCertificate=True";
        private int kullaniciId; 

        public Randevu_İptal(int kullaniciId)
        {
            InitializeComponent();
            this.kullaniciId = kullaniciId;
        }

        private void Randevu_İptal_Load(object sender, EventArgs e)
        {
            RandevularıGetir();
        }

        private void randevusil_btn_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedRandevuId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["id"].Value);
                RandevuyuSil(selectedRandevuId);
            }
            else
            {
                MessageBox.Show("Lütfen bir randevu seçin!");
            }
        }

        private void RandevularıGetir()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = @"SELECT r.id, r.Gün, r.Saat, d.Ad + ' ' + d.Soyad AS Doktor, b.BölümAdı AS Bölüm 
                FROM Randevu r 
                INNER JOIN Doktorlar d ON r.Doktorid = d.id 
                INNER JOIN Bolumler b ON r.Bolumid = b.id
                WHERE r.KullaniciId = @KullaniciId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@KullaniciId", kullaniciId);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dataGridView1.DataSource = dt;
                    }
                }
            }
        }

        private void RandevuyuSil(int randevuId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "DELETE FROM Randevu WHERE id = @RandevuId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@RandevuId", randevuId);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Randevu başarıyla iptal edildi!");
                        RandevularıGetir(); 
                    }
                    else
                    {
                        MessageBox.Show("Randevu iptal edilirken bir hata oluştu!");
                    }
                }
            }
        }

        private void btn_ekle_Click(object sender, EventArgs e)
        {
            this.Hide();

          
            Randevu randevuForm = new Randevu((Form1)this.Owner, kullaniciId);
            randevuForm.Show();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Uygulamayı kapatmak istediğinize emin misiniz?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
              
                Application.Exit();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
          
            Randevu_Güncelleme randevuGuncellemeForm = new Randevu_Güncelleme(kullaniciId);

         
            this.Visible = false;

            randevuGuncellemeForm.Visible = true;
        }
    }
}
