using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clientsuif
{
    public partial class Form2 : Form
    {

        Connexion cnx = new Connexion();
        DataTable dtclient;
        int position;
        public Form2()
        {
            InitializeComponent();
        }
        
        public class tbclient
        {
            public string nom;
            public string adress;
            public tbclient(string tb1, string tb2)
            {
                nom = tb1;

                adress = tb2;
            }
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            position = 0;
            cnx.cmd = new SQLiteCommand();
            cnx.cnx = new SQLiteConnection();
            cnx.cmd = cnx.cnx.CreateCommand();
            string sql = "SELECT * FROM client";
            cnx.dta = new SQLiteDataAdapter(sql, cnx.str);
            using (dtclient = new DataTable())
            {
                cnx.dta.Fill(dtclient);
                textBox1.Text = dtclient.Rows[position][1].ToString();
                textBox2.Text = dtclient.Rows[position][2].ToString();
            }
          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (position >= dtclient.Rows.Count - 1)
            {
            }
            else
            {
                position += 1;
                textBox1.Text = dtclient.Rows[position][1].ToString();
                textBox2.Text = dtclient.Rows[position][2].ToString();
            }  
            
            
        }
    }
}
