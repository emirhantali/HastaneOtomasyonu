using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace HastaneOtomasyonu
{
    public partial class Kayit : Form
    {
        private string connectionString = "Data Source=.\\SQLEXPRESS,55222;Initial Catalog=hastane;Integrated Security=True;TrustServerCertificate=True";

        public Kayit()
        {
            InitializeComponent();
        }

        private void kyt_btn_Click(object sender, EventArgs e)
        {
            string tc = tc_txt.Text;
            string sifre = sfr_txt.Text;
            string ad = ad_txt.Text;
            string soyad = soyad_txt.Text;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                
                if (int.TryParse(tc, out int tcValue))
                  {
                    // SQL sorgusu
                    string query = "INSERT INTO kullanıcı (Tc, Ad, Soyad, Sifre) VALUES (@tc, @ad, @soyad, @sifre)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        
                        command.Parameters.Add("@tc", SqlDbType.BigInt).Value = tcValue;
                        command.Parameters.AddWithValue("@ad", ad);
                        command.Parameters.AddWithValue("@soyad", soyad);
                        command.Parameters.AddWithValue("@sifre", sifre);

                        int affectedRows = command.ExecuteNonQuery();

                        if (affectedRows > 0)
                        {
                            MessageBox.Show("Kayıt başarıyla eklendi!");

                            Form1 form1 = new Form1();
                            form1.Show();
                            this.Hide(); 
                        }
                        else
                        {
                            MessageBox.Show("Kayıt eklenirken bir hata oluştu. Lütfen tekrar deneyin.");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Geçersiz Tc formatı. Lütfen doğru bir Tc girin.");
                }
            }
        }

        private void Kayit_Load(object sender, EventArgs e)
        {

        }
    }
}
