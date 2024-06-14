using System;
using System.Data.OleDb;
using System.Windows.Forms;

namespace Araç_Muayene_
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void btnKaydet_Click(object sender, EventArgs e)
        {
            string plaka = textBox1.Text;

            string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\frtgn\OneDrive\Masaüstü\Araç_Muayene_\Araç_Muayene_\bin\x64\Debug\araclar.accdb";

            try
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    connection.Open();

                    if (PlakaVarMi(plaka, connection))
                    {
                        ShowExistingRecord(plaka);
                    }
                    else
                    {
                        string query = "INSERT INTO araclar (plaka, marka, model, renk, km, motor, yil, myil) " +
                                       "VALUES (@Plaka, @Marka, @Model, @Renk, @Km, @Motor, @Yil, @Myil)";

                        using (OleDbCommand command = new OleDbCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@Plaka", textBox1.Text);
                            command.Parameters.AddWithValue("@Marka", textBox2.Text);
                            command.Parameters.AddWithValue("@Model", textBox3.Text);
                            command.Parameters.AddWithValue("@Renk", textBox4.Text);
                            command.Parameters.AddWithValue("@Km", textBox5.Text);
                            command.Parameters.AddWithValue("@Motor", textBox6.Text);
                            command.Parameters.AddWithValue("@Yil", dateTimePicker1.Text);
                            command.Parameters.AddWithValue("@Myil", dateTimePicker2.Text);

                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Veri başarıyla eklendi.");
                                ShowExistingRecord(plaka);
                            }
                            else
                            {
                                MessageBox.Show("Veri eklenirken bir hata oluştu.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        private bool PlakaVarMi(string plaka, OleDbConnection connection)
        {
            string query = "SELECT COUNT(*) FROM araclar WHERE plaka = @Plaka";

            try
            {
                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Plaka", plaka);
                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
                return false;
            }
        }

        private void ShowExistingRecord(string plaka)
        {
            try
            {
                Form2 form2 = new Form2();
                form2.LoadData(plaka); // Form2'de plakaya göre veri yüklemek için bir metod kullandığınızı varsayalım

             
               

                    // Form1'den gelen verileri alıyoruz
                    string[] values = new string[]
                    {
            textBox1.Text,
            textBox2.Text, // marka
            textBox3.Text, // model
            textBox4.Text, // renk
            textBox5.Text, // km
            textBox6.Text, // motor
            dateTimePicker1.Value.ToShortDateString() // yıl
                    };

                form2.AddRowToDataGridView(values);
                form2.Show();
                    this.Hide(); // Form1'i gizliyoruz, isteğe bağlı olarak formun kapatılması için this.Close() kullanabilirsiniz.
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
    }
    }


