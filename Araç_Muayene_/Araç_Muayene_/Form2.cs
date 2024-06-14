using System;
using System.Data.OleDb;
using System.Windows.Forms;


namespace Araç_Muayene_
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

      

        private void UpdateRecord(string plaka)
        {
            string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\frtgn\OneDrive\Masaüstü\Araç_Muayene_\Araç_Muayene_\bin\x64\Debug\araclar.accdb";

            try
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    connection.Open();

                    string query = "UPDATE araclar SET fren = @Fren, direksiyon = @Direksiyon, gorus = @Gorus, " +
                                   "aydinlatma = @Aydinlatma, suspansiyon = @Suspansiyon, lastik = @Lastik, " +
                                   "zorunlu = @Zorunlu, gurultu = @Gurultu, yag = @Yag, su = @Su, " +
                                   "kemer = @Kemer, sonuc = @Sonuc WHERE plaka = @Plaka";

                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Fren", textBox1.Text);
                        command.Parameters.AddWithValue("@Direksiyon", textBox2.Text);
                        command.Parameters.AddWithValue("@Gorus", textBox3.Text);
                        command.Parameters.AddWithValue("@Aydinlatma", textBox4.Text);
                        command.Parameters.AddWithValue("@Suspansiyon", textBox5.Text);
                        command.Parameters.AddWithValue("@Lastik", textBox6.Text);
                        command.Parameters.AddWithValue("@Zorunlu", textBox7.Text);
                        command.Parameters.AddWithValue("@Gurultu", textBox8.Text);
                        command.Parameters.AddWithValue("@Yag", textBox9.Text);
                        command.Parameters.AddWithValue("@Su", textBox10.Text);
                        command.Parameters.AddWithValue("@Kemer", textBox11.Text);
                        command.Parameters.AddWithValue("@Sonuc", textBox12.Text);
                        command.Parameters.AddWithValue("@Plaka", plaka);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Kayıt başarıyla güncellendi.");
                        }
                        else
                        {
                            MessageBox.Show("Kayıt güncellenirken bir hata oluştu.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        public void LoadData(string plaka)
        {
            if (string.IsNullOrEmpty(plaka))
            {
                MessageBox.Show("Plaka alanı boş bırakılamaz.");
                return;
            }

            string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\frtgn\OneDrive\Masaüstü\Araç_Muayene_\Araç_Muayene_\bin\x64\Debug\araclar.accdb";
            string query = "SELECT * FROM araclar WHERE plaka = @Plaka";

            try
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    connection.Open();

                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Plaka", plaka);

                        using (OleDbDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Veritabanından okunan verileri Form2'deki TextBox'lara yüklüyoruz
                                textBox1.Text = reader["fren"].ToString();
                                textBox2.Text = reader["direksiyon"].ToString();
                                textBox3.Text = reader["gorus"].ToString();
                                textBox4.Text = reader["aydinlatma"].ToString();
                                textBox5.Text = reader["suspansiyon"].ToString();
                                textBox6.Text = reader["lastik"].ToString();
                                textBox7.Text = reader["zorunlu"].ToString();
                                textBox8.Text = reader["gurultu"].ToString();
                                textBox9.Text = reader["yag"].ToString();
                                textBox10.Text = reader["su"].ToString();
                                textBox11.Text = reader["kemer"].ToString();
                                textBox12.Text = reader["sonuc"].ToString();
                                textBox13.Text = reader["plaka"].ToString();
                                // Diğer TextBox'ları da bu şekilde doldurabilirsiniz
                            }
                            else
                            {
                                MessageBox.Show("Belirtilen plakaya sahip araç bulunamadı.");
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

        private void button1_Click(object sender, EventArgs e)
        {
          
                string plaka = textBox13.Text.Trim(); // Plaka değerini alırken trim() kullanarak baştaki ve sondaki boşlukları temizliyoruz

                if (string.IsNullOrEmpty(plaka))
                {
                    MessageBox.Show("Plaka alanı boş bırakılamaz.");
                    return;
                }

                UpdateRecord(plaka); // Güncelleme işlemi için UpdateRecord metodunu çağırıyoruz

                // Form1'e geri dönmek için yeni bir Form1 örneği oluşturup, onu gösteriyoruz
                Form1 form1 = new Form1();
                form1.Show();

                this.Close(); // Şu anki Form2'yi kapatıyoruz
            }

        public void AddRowToDataGridView(string[] values)
        {
            DataGridViewRow row = new DataGridViewRow();
            row.CreateCells(dataGridView1);

            // Verilen değerleri DataGridView'e ekliyoruz
            for (int i = 0; i < values.Length; i++)
            {
                row.Cells[i].Value = values[i];
            }

            // DataGridView'e yeni satırı ekliyoruz
            dataGridView1.Rows.Add(row);
        }

    }
}

