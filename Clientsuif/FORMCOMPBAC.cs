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
    public partial class FORMCOMPBAC : Form
    {
           
        Connexion cnx = new Connexion();
        public string tbcon;
        DataTable dtBAC;
        DataSet dts;
        int position;

        public FORMCOMPBAC()
        {
            InitializeComponent();
        }

        private void FORMCOMPBAC_Load(object sender, EventArgs e)
        {
            label1.Text = tbcon.ToString();


            connexionbac();
        }
        public void connexionbac()
        {
            
            cnx.connectionopen();
            dts = new DataSet();
            cnx.dta = new SQLiteDataAdapter("select *from COMPTEUR where IDCONSO = '" + tbcon + "'", cnx.str);

            //cnx.dta = new SQLiteDataAdapter("select *from model where idmodel=idclient", cnx.str);
            dtBAC = new DataTable();
            cnx.dta.Fill(dtBAC);
            chargerBac();
        }

        public void chargerBac()
        {
            //textBox3.Text = "";
            if (dtBAC.Rows.Count > 0)
            {
                position = 0;
                tbbacmf.Text = dtBAC.Rows[position][1].ToString();
                tbbac1.Text = dtBAC.Rows[position][2].ToString();
                tbtotal.Text = dtBAC.Rows[position][3].ToString();
                tbdiff.Text = dtBAC.Rows[position][4].ToString();
                label34.Text = tbtotal.Text.ToString();

            }

            else { cviderBac(); }

        }

        public void cviderBac()
        {
            tbbac1.Text = "";
            tbbacmf.Text = "";
            tbtotal.Text = "";
            tbdiff.Text = "";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //conxionconso();
            //cnx.cmd = new SQLiteCommand();

            //cnx.cnx.Open();
            //cnx.cmd.Connection = cnx.cnx;

            //cnx.cmd.CommandText = "insert into COMPTEUR (BacMF,Bac1,Totalcompt,diffpage,idconso )values ('" + tbbacmf.Text + "','" + tbbac1.Text + "','" + tbtotal.Text + "','" + tbdiff.Text + "','" + tbcon + "' )";
            //cnx.dta = new SQLiteDataAdapter("select *from COMPTEUR  where IDCONSO='" + tbcon + "'", cnx.str);
            ////cnx.dta = new SQLiteDataAdapter("select *from model where idmodel=idclient", cnx.str);
            //cnx.cmd.ExecuteNonQuery();
            //cnx.cnx.Close();
            //dtBAC = new DataTable();
            //cnx.dta.Fill(dtBAC);
            //chargerBac();

            //MessageBox.Show("AJOUTER AVEC SUCCES");
        }

        //private void conxionconso()
        //{
        //    throw new NotImplementedException();
        //}

        private void BTCPTMODIF_Click(object sender, EventArgs e)
        {
            try
            {
                {   cnx.cmd = new SQLiteCommand();

                    cnx.cnx.Open();
                    cnx.cmd.Connection = cnx.cnx;
                    //"update client set Intitule='" + tbNOM.Text + "',Contact='" + tbAdress.Text + "' where idclient=" + dataGridView1.SelectedRows[0].Cells[0].Value.ToString() + "";

                    cnx.cmd.CommandText = "update Compteur set BacMF='" + tbbacmf.Text + "',Bac1='" + tbbac1.Text + "' where IDCOMP='" + dtBAC.Rows[position][0].ToString() + "'";
                    cnx.dta = new SQLiteDataAdapter("select *from COMPTEUR where IDCONSO = '" + tbcon + "'", cnx.str);
                    cnx.cmd.ExecuteNonQuery();
                    cnx.cnx.Close();
                    dtBAC = new DataTable();
                    cnx.dta.Fill(dtBAC);
                    chargerBac();
                }
            }
            catch (Exception E)
            {

                MessageBox.Show("AUCUNE SELECTIONNEE");
            }
        }
    }
}
