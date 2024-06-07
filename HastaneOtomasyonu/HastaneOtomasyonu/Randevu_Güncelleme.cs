using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace HastaneOtomasyonu
{
    public partial class Randevu_Güncelleme : Form
    {
        private string connectionString = "Data Source=.\\SQLEXPRESS,55222;Initial Catalog=hastane;Integrated Security=True;TrustServerCertificate=True";
        private int kullaniciId;
        private int selectedRandevuId;

        public Randevu_Güncelleme(int kullaniciId)
        {
            InitializeComponent();
            this.kullaniciId = kullaniciId; 
        }

        private void Randevu_Güncelleme_Load(object sender, EventArgs e)
        {
            BolumComboBoxDoldur();
            RandevularıGetir();
        }

        private void BolumComboBoxDoldur()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT id, BölümAdı FROM Bolumler";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(reader);
                        blm_cmb.DisplayMember = "BölümAdı";
                        blm_cmb.ValueMember = "id";
                        blm_cmb.DataSource = dt;
                    }
                }
            }
        }

        private void DoktorComboBoxDoldur(int bolumId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = @"SELECT d.id, d.Ad, d.Soyad 
                         FROM Doktorlar d 
                         INNER JOIN Bolumler b ON d.Bolumid = b.id 
                         WHERE b.id = @Bolumid";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Bolumid", bolumId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(reader);

                        List<string> doktorList = new List<string>();
                        foreach (DataRow row in dt.Rows)
                        {
                            string doktorAdSoyad = row["Ad"].ToString() + " " + row["Soyad"].ToString();
                            doktorList.Add(doktorAdSoyad);
                        }

                        dktr_cmb.DataSource = doktorList;
                    }
                }
            }
        }

        private void RandevuyuGuncelle(int randevuId, int bolumId, int doktorId, string gun, string saat)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = @"UPDATE Randevu 
                         SET Bolumid = @Bolumid, Doktorid = @Doktorid, Gün = @Gün, Saat = @Saat
                         WHERE id = @RandevuId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Bolumid", bolumId);
                    command.Parameters.AddWithValue("@Doktorid", doktorId);
                    command.Parameters.AddWithValue("@Gün", gun);
                    command.Parameters.AddWithValue("@Saat", saat);
                    command.Parameters.AddWithValue("@RandevuId", randevuId);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Randevu başarıyla güncellendi!");
                    }
                    else
                    {
                        MessageBox.Show("Randevu güncellenirken bir hata oluştu!");
                    }
                }
            }
        }



        private bool Mevcut_Randevu_Kontrol(int bolumId, int doktorId, string gun, string saat)
        {
            bool mevcutRandevu = false;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = @"SELECT COUNT(*) FROM Randevu WHERE Bolumid = @Bolumid AND Doktorid = @Doktorid AND Gün = @Gün AND Saat = @Saat";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Bolumid", bolumId);
                    command.Parameters.AddWithValue("@Doktorid", doktorId);
                    command.Parameters.AddWithValue("@Gün", gun);
                    command.Parameters.AddWithValue("@Saat", saat);

                    int count = Convert.ToInt32(command.ExecuteScalar());
                    if (count > 0)
                    {
                        mevcutRandevu = true;
                    }
                }
            }

            return mevcutRandevu;
        }


        private int Secilen_DoktorIdAl(string doktorAd, string doktorSoyad)
        {
            int doktorId = -1;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = @"SELECT id FROM Doktorlar WHERE Ad = @Ad AND Soyad = @Soyad";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Ad", doktorAd);
                    command.Parameters.AddWithValue("@Soyad", doktorSoyad);

                    object result = command.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        doktorId = Convert.ToInt32(result);
                    }
                }
            }

            return doktorId;
        }




        private void randevu_btn_Click(object sender, EventArgs e)
        {
           
            string gun = gün_cmb.SelectedItem.ToString();
            string saat = saat_cmb.SelectedItem.ToString();
            string doktor = dktr_cmb.SelectedItem.ToString();
            string bolum = blm_cmb.SelectedItem.ToString();

          
            string[] doktorAdSoyad = doktor.Split(' ');
            int doktorId = Secilen_DoktorIdAl(doktorAdSoyad[0], doktorAdSoyad[1]);

            int bolumId = Convert.ToInt32(blm_cmb.SelectedValue);

       
            if (Mevcut_Randevu_Kontrol(bolumId, doktorId, gun, saat))
            {
                MessageBox.Show("Seçilen saatte zaten bir randevu var. Lütfen farklı bir saat seçin.");
            }
            else
            {
              
                RandevuyuGuncelle(selectedRandevuId, bolumId, doktorId, gun, saat);
                RandevularıGetir();
            }
        }

        private void blm_cmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (blm_cmb.SelectedIndex != -1)
            {
                int selectedBolumId = Convert.ToInt32(blm_cmb.SelectedValue);
                DoktorComboBoxDoldur(selectedBolumId);
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

        private void btn_iptal_Click(object sender, EventArgs e)
        {
            Randevu_İptal r1 = new Randevu_İptal(kullaniciId);
            this.Visible = false;
            r1.Visible = true;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                // DataGridView'da seçilen satırdaki randevunun ID'sini alıyoruz
                selectedRandevuId = Convert.ToInt32(row.Cells["id"].Value);

                // ComboBox'lara randevu bilgilerini doldurduk
                string gun = row.Cells["Gün"].Value.ToString();
                string saat = row.Cells["Saat"].Value.ToString();
                string doktor = row.Cells["Doktor"].Value.ToString();
                string bolum = row.Cells["Bölüm"].Value.ToString();

                gün_cmb.SelectedItem = gun;
                saat_cmb.SelectedItem = saat;
                dktr_cmb.SelectedItem = doktor;
                blm_cmb.SelectedItem = bolum;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Uygulamayı kapatmak istediğinize emin misiniz?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
              
                Application.Exit();
            }
        }

        private void btn_iptal_Click_1(object sender, EventArgs e)
        {
            Randevu_İptal randevuIptalForm = new Randevu_İptal(kullaniciId); 
            randevuIptalForm.Show(); 
            this.Hide(); 
        }

        private void güncelle_btn_Click(object sender, EventArgs e)
        {
            this.Hide();

            Randevu randevuForm = new Randevu((Form1)this.Owner, kullaniciId);
            randevuForm.Show();
        }
    }
}
