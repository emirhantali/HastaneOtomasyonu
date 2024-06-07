using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HastaneOtomasyonu
{
    public partial class admin4 : Form
    {
        private string connectionString = "Data Source=.\\SQLEXPRESS,55222;Initial Catalog=hastane;Integrated Security=True;TrustServerCertificate=True";
        public admin4()
        {
            InitializeComponent();
        }

        private void admin4_Load(object sender, EventArgs e)
        {
            RandevularıGetir();
        }
        private void RandevularıGetir()
        {
            // Veritabanından BölümID'si 1 olan randevuları çek
            string query = @"SELECT r.id, r.Gün, r.Saat, d.Ad + ' ' + d.Soyad AS Doktor, b.BölümAdı AS Bölüm,
                            k.Ad AS HastaAdı, k.Soyad AS HastaSoyadı
                     FROM Randevu r 
                     INNER JOIN Doktorlar d ON r.Doktorid = d.id 
                     INNER JOIN Bolumler b ON r.Bolumid = b.id
                     INNER JOIN Kullanıcı k ON r.KullaniciId = k.id
                     WHERE r.Bolumid = 4";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();

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

        private void button2_Click(object sender, EventArgs e)
        {
            admin2 admin = new admin2();

            this.Visible = false;

            admin.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            admin3 admin = new admin3();

            this.Visible = false;

            admin.Visible = true;
        }



        private void button4_Click(object sender, EventArgs e)
        {
            admin5 admin = new admin5();

            this.Visible = false;

            admin.Visible = true;
        }



        private void btn_ekle_Click(object sender, EventArgs e)
        {
            admin admin = new admin();

            this.Visible = false;

            admin.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
           
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Uygulamayı kapatmak istediğinize emin misiniz?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
