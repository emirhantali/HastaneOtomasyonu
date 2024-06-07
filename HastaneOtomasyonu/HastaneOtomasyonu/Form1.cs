using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace HastaneOtomasyonu
{
    public partial class Form1 : Form
    {
        private string connectionString = "Data Source=.\\SQLEXPRESS,55222;Initial Catalog=hastane;Integrated Security=True;TrustServerCertificate=True";
      

        public Form1()
        {
            InitializeComponent();
        }





        private void grs_btn_Click(object sender, EventArgs e)
        {
            string tc = tc_txt.Text;
            string sifre = sfr_txt.Text;

         
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

          
                string query = "SELECT * FROM Kullanıcı WHERE Tc = @Tc AND Sifre = @Sifre";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                
                    command.Parameters.AddWithValue("@Tc", tc);
                    command.Parameters.AddWithValue("@Sifre", sifre);

                    
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                     
                        if (reader.Read()) // Okuma işlemi gerçekleştir
                        {
                            MessageBox.Show("Giriş başarılı!");
                          
                            int kullaniciId = reader.GetInt32(reader.GetOrdinal("id"));
                            Randevu form2 = new Randevu(this, kullaniciId);
                            this.Visible = false;
                            form2.Visible = true;
                        }
                        else
                        {
                            MessageBox.Show("Kullanıcı adı veya şifre hatalı. Lütfen tekrar deneyin.");
                        }
                    }
                }
            }
        }

        private void kyt_btn_Click(object sender, EventArgs e)
        {
            Kayit k1 = new Kayit();
            this.Visible = false;
            k1.Visible = true;
        }

        private void admn_btn_Click(object sender, EventArgs e)
        {
           
            string ad = tc_txt.Text;
            string sifre = sfr_txt.Text;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM admin WHERE ad = @Tc AND sifre = @Sifre";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                  
                    command.Parameters.AddWithValue("@Tc", ad);
                    command.Parameters.AddWithValue("@Sifre", sifre);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                    
                        if (reader.Read()) // Okuma işlemi gerçekleştir
                        {
                            MessageBox.Show("Giriş başarılı!");
                           
                            int kullaniciId = reader.GetInt32(reader.GetOrdinal("id"));
                            admin form2 = new admin();
                            this.Visible = false;
                            form2.Visible = true;
                        }
                        else
                        {
                            MessageBox.Show("Kullanıcı adı veya şifre hatalı. Lütfen tekrar deneyin.");
                        }
                    }
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
