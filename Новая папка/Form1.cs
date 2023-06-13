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
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Testing
{

    public partial class Form1 : Form
    {
        
        String id;
         SqlConnection sqlConnection =
            new SqlConnection(@"Data Source=WIN-685GVMRFB5H\SQLEXPRESS;Initial Catalog=Testing_System_2023;Integrated Security=True;");

        SqlCommand cmd;
        public Form1()
        {

            InitializeComponent();
            id = Guid.NewGuid().ToString();
            textBox3.Text = id;
            textBox3.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            

            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text))
            {
                // строка содержит только пробельные символы, или является null
                MessageBox.Show("Строка пуста или содержит только пробельные символы!");
            }
            else
            {
                button1.Enabled = false;
                string text = textBox1.Text;
                text = text.Replace("'", "");
                text = text.Replace("\"", "");
                textBox1.Text = text;



                sqlConnection.Open();
                cmd = new SqlCommand
                    ($"INSERT INTO Authentification(ID, FullName, UserGroup) " +
                    $"VALUES('{textBox3.Text}', '{textBox1.Text}', '{textBox2.Text}')", sqlConnection);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Test started!");
                sqlConnection.Close();

                Form2 forma2 = new Form2();
                forma2.SqlConnection = sqlConnection;
                forma2._cmd=cmd;
                forma2.guid = id;
                
                forma2.Show();
            }
            
            
        }
    }
}
