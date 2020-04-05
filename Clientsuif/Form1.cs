using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace Clientsuif
{
    public partial class Form1 : Form
    {
        Connexion cnx = new Connexion();
        DataTable dtclient;
   
        LESCONSO CL;
        int position;
        public Form1()
        {
            InitializeComponent();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
            client();
            CONNEXION();
            //tbNOM.Text = dtclient.Rows[0][0].ToString();
            ////tbAdress.Text = dtclient.Rows[0][1].ToString();

        }
        private void CONNEXION()
        {
            cnx.cnx = new SQLiteConnection();
            cnx.cnx.ConnectionString = cnx.str;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ////cnx.connectionopen();
            //MessageBox.Show("ihuih");
        }
        public void client()
        {
            cnx.cmd = new SQLiteCommand();
            cnx.cnx = new SQLiteConnection();
            cnx.cmd = cnx.cnx.CreateCommand();
            string sql = "SELECT * FROM client";
            cnx.dta = new SQLiteDataAdapter(sql, cnx.str);
            using (dtclient = new DataTable()) 
            {
                cnx.dta.Fill(dtclient);

                dataGridView1.DataSource = dtclient;


            }
        }
        //VERIFICATION DE LA TOUCHE ENTREZ
        private void tbNOM_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter && tbNOM.Text!="")
            {
                tbAdress.Focus();
            }
        }

        //VERIFICATION DE LA TOUCHE ENTRE ET VALIDATION
        private void tbAdress_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter && tbAdress.Text!="")
            {
                BTAJOUTER.Text = "ANNULER";
                BTAJOUTER.Text = "AJOUTER";

                cnx.cmd = new SQLiteCommand();
                cnx.cnx.Open();
                cnx.cmd.Connection = cnx.cnx;

                cnx.cmd.CommandText = "insert into Client(Intitule,Contact) values ('" + tbNOM.Text + "','" + tbAdress.Text + "')";
                cnx.cmd.ExecuteNonQuery();
                cnx.cnx.Close();
                dtclient.Clear();
                cnx.dta.Fill(dtclient);
                dataGridView1.Refresh();
                dataGridView1.DataSource = dtclient;
                tbNOM.Text = "";
                tbAdress.Text = "";
            }
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            if (BTAJOUTER.Text == "AJOUTER"  )
            {
                tbNOM.Text = "";
                tbAdress.Text = "";
                BTAJOUTER.Text = "ANNULER";
                tbNOM.Focus();
            


            }
            else
            {
                if (BTAJOUTER.Text == "ANNULER" )
                {

                    if (dataGridView1.SelectedRows.Count > 0) // make sure user select at least 1 row 
                    {

                        tbNOM.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                        tbAdress.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                        BTAJOUTER.Text = "AJOUTER";
                    }

                }
                


            }

            }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            searchdata(textBox1.Text);
        }
        public void searchdata(string trouver)
        {
            //cnx.cnx.Open();
            string searchrequet = "select *from client where Intitule like '%" + trouver+ "%' ";
            cnx.dta = new SQLiteDataAdapter(searchrequet, cnx.str);
            dtclient = new DataTable();
            cnx.dta.Fill(dtclient);
            dataGridView1.DataSource = dtclient;


        }
            

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0) // make sure user select at least 1 row 
            {

                tbNOM.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                tbAdress.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            }
        }


        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
           


            LESCONSO CL = new LESCONSO(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
            CL.getclient = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            if (CL.ShowDialog()==DialogResult.OK)
            {
                CL.Show();
            }
         
            
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //if (this.WindowState == FormWindowState.Maximized)
            //{
            //    this.WindowState = FormWindowState.Minimized;
            //}
            //else
            //{
            //    this.WindowState = FormWindowState.Maximized;
            //}
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click_3(object sender, EventArgs e)
        {
            
        }

        private void button1_Click_4(object sender, EventArgs e)
        {
            cnx.cmd = new SQLiteCommand();
            cnx.cnx.Open();
            cnx.cmd.Connection = cnx.cnx;
            cnx.cmd.CommandText = "update client set Intitule='" + tbNOM.Text + "',Contact='" + tbAdress.Text + "' where idclient=" + dataGridView1.SelectedRows[0].Cells[0].Value.ToString() + "";
            cnx.cmd.ExecuteNonQuery();
            cnx.cnx.Close();
            //MessageBox.Show("AJOUTER AVEC SUCCES");
            dtclient.Clear();
            cnx.dta.Fill(dtclient);
            dataGridView1.Refresh();
            dataGridView1.DataSource = dtclient;
            //MessageBox.Show("AJOUTER AVEC SUCCES");
        }

        private void button5_Click(object sender, EventArgs e)
        {
           

        
            

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }
    }
    }

