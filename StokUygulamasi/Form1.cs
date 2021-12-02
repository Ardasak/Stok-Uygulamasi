using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StokUygulamasi
{
    public partial class Form1 : Form
    {
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-95T7H1G; Initial Catalog=Stok; Integrated Security=True");
        public Form1()
        {
            InitializeComponent();
        }

        private void ekle_Click(object sender, EventArgs e)
        {
            try {
                String t1 = textBox1.Text;
                String t2 = textBox2.Text;
                String t3 = textBox3.Text;
                String t4 = textBox4.Text;
                String t5 = textBox5.Text;
                String t6 = textBox6.Text;
                if (!(String.IsNullOrEmpty(t1) || String.IsNullOrEmpty(t2) || String.IsNullOrEmpty(t3) || String.IsNullOrEmpty(t4) || String.IsNullOrEmpty(t5) || String.IsNullOrEmpty(t6))) {
                    baglanti.Open();
                    SqlCommand komut = new SqlCommand("INSERT INTO Malzemeler (MalzemeKodu, MalzemeAdi, YillikSatis, BirimFiyat, MinStok, TSuresi) VALUES (@data1,@data2,@data3,@data4,@data5,@data6)", baglanti);
                    komut.Prepare();
                    komut.Parameters.Add("@data1", SqlDbType.VarChar).Value = t1;
                    komut.Parameters.Add("@data2", SqlDbType.VarChar).Value = t2;
                    komut.Parameters.Add("@data3", SqlDbType.VarChar).Value = t3;
                    komut.Parameters.Add("@data4", SqlDbType.VarChar).Value = t4;
                    komut.Parameters.Add("@data5", SqlDbType.VarChar).Value = t5;
                    komut.Parameters.Add("@data6", SqlDbType.VarChar).Value = t6;
                    komut.ExecuteNonQuery();
                    baglanti.Close();
                    listele();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Veri tabanına veri eklerken bir hata oluştu.", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            }

        private void sil_Click(object sender, EventArgs e)
        {
            try
            {
                String malzeme_kod = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                baglanti.Open();
                SqlCommand sqlCommand = new SqlCommand("DELETE FROM Malzemeler WHERE MalzemeKodu=@data1", baglanti);
                sqlCommand.Prepare();
                sqlCommand.Parameters.Add("@data1", SqlDbType.VarChar).Value = malzeme_kod;
                sqlCommand.ExecuteNonQuery();
                baglanti.Close();
                listele();
            }
            catch (Exception)
            {
                MessageBox.Show("Veri tabanından silerken bir hata oluştu.","Hata!",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void guncelle_Click(object sender, EventArgs e)
        {
            try
            {

                String eski_malzeme_kod = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                String t1 = textBox1.Text;
                String t2 = textBox2.Text;
                String t3 = textBox3.Text;
                String t4 = textBox4.Text;
                String t5 = textBox5.Text;
                String t6 = textBox6.Text;
                if (!(String.IsNullOrEmpty(t1) || String.IsNullOrEmpty(t2) || String.IsNullOrEmpty(t3) || String.IsNullOrEmpty(t4) || String.IsNullOrEmpty(t5) || String.IsNullOrEmpty(t6)))
                    {
                        baglanti.Open();
                        SqlCommand sqlCommand = new SqlCommand("UPDATE Malzemeler SET MalzemeKodu=@data1 , MalzemeAdi=@data2 , YillikSatis=@data3 , BirimFiyat=@data4 , MinStok=@data5 , TSuresi=@data6 WHERE MalzemeKodu=@data7", baglanti);
                        sqlCommand.Prepare();
                        sqlCommand.Parameters.Add("@data1", SqlDbType.VarChar).Value = t1;
                        sqlCommand.Parameters.Add("@data2", SqlDbType.VarChar).Value = t2;
                        sqlCommand.Parameters.Add("@data3", SqlDbType.VarChar).Value = t3;
                        sqlCommand.Parameters.Add("@data4", SqlDbType.VarChar).Value = t4;
                        sqlCommand.Parameters.Add("@data5", SqlDbType.VarChar).Value = t5;
                        sqlCommand.Parameters.Add("@data6", SqlDbType.VarChar).Value = t6;
                        sqlCommand.Parameters.Add("@data7", SqlDbType.VarChar).Value = eski_malzeme_kod;
                        sqlCommand.ExecuteNonQuery();
                        baglanti.Close();
                        listele();
                    }
            }
            catch (Exception)
            {
                MessageBox.Show("Veri tabanını güncellerken bir hata oluştu.", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void listele()
        {
            try
            {
                baglanti.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT * FROM Malzemeler", baglanti);
                DataTable table = new DataTable();
                sqlDataAdapter.Fill(table);
                dataGridView1.DataSource = table;
                baglanti.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Veri tabanından veri çekerken bir hata oluştu.", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            textBox6.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
        }
    }
}
