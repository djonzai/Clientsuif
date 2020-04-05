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
using System.Globalization;

namespace Clientsuif
{
    public partial class LESCONSO : Form
    {
        Connexion cnx = new Connexion();
        DataSet dts;
        DataTable dtmodel;
        DataTable dtgv;
        DataTable dtconso;
        DataTable dtENGINE;
        DataTable dtBAC;
        DataTable dtRECH;
        MaskedTextBox dynamicMaskedTextBox = new MaskedTextBox();

        string id , test;
        int position;
        public string getclient;
       
        string date;
        public static object DataGridView1 { get; private set; }
        public object Ttbidconso { get; private set; }
        public Form1 F1 = new Form1();
        private object value;
        public Form1 f;
        string cont;
        string sql;

        public LESCONSO(String DGV)

        {

            val = DGV;
            //val2 = getcient;
            InitializeComponent();




        }

        public LESCONSO(object value)
        {
            this.value = value;
        }
        //RECUPERATION DE LA VALEUR DATAGRIV LA CONLONE 0
        public string val = "";


        //CHARGE
    //===================== forme load=========================================
        private void LESCONSO_Load(object sender, EventArgs e)
        {
            cont = TBC_DATE.Text;
           
            panel2.Enabled = false;
            groupBox4.Enabled = false;
            groupBox5.Enabled = false;


            tbidmodel.Visible = false;
            tbidconso.Visible = false;
            tbidclient.Visible = false;
            label34.Visible = false;
            tbiDbac.Visible = false;
            tbtotalanterieur.Visible = false;
            dataGridView3.Visible = false;
            button11.Visible = false;
            button12.Visible = false;


            label36.Text = getclient.ToString();
            AFFICHERmodel();
           
            ConnexionAEngine();
            this.Width = 1365;
            this.Height = 860;
            CONNEXION();

            //SELECTIONNER LA DERNIERE LIGNE DU DATAGRIDVIEW1
            if (dataGridView1.SelectedRows.Count > 0) // make sure user select at least 1 row 
            {
                dataGridView1.CurrentCell = dataGridView1[0, dataGridView1.RowCount - 1];

            }
        }
        //===================== fin forme load=========================================
        
        //=======================================CALCULE DES DONNEES ENTREE DANS LA ZONE COMPTEUR BAC==================================================
        private void tbbac1_KeyDown(object sender, KeyEventArgs e)
        {


            if (e.KeyData == Keys.Enter)
            {
                if (e.KeyData == Keys.Enter)
                {
                    button6.Focus();

                }
            }
        }
        private void CALCULtotal()
        {

            double a, b, c;
            a = double.Parse(tbbac1.Text);
            b = double.Parse(tbbacmf.Text);
            c = a + b;
            tbtotal.Text = c.ToString();
            //}

        }

        private void CALCULdifference()
        {
           
            try
            {
               int  id =0;

                double E, F, G, h;
                if (tbtotalanterieur.Text=="")
                {
                    //tbtotalanterieur.Text = 


                    //E = double.Parse(tbtotal.Text);
                    //F = double.Parse(tbtotalanterieur.Text);
                    //F = 0;
                    //G = E - F;
                    tbdiff.Text = "0";

                }
                else
                {
                    E = double.Parse(tbtotal.Text);
                    F = double.Parse(tbtotalanterieur.Text);
                    G =E-F;

                    tbdiff.Text = G.ToString();
                }

              
                //}



            }
            catch (Exception E)
            {

                MessageBox.Show(E.Message);
            }


        }
        //======================================= fin CALCULE DES DONNEES ENTREE DANS LA ZONE COMPTEUR BAC==================================================
        private void CONNEXION()
        {
            cnx.cnx = new SQLiteConnection();
            cnx.cnx.ConnectionString = cnx.str;
        }


        //================================= debut gestion de la table compteur ===================================================================================
        //================1
        public void decalage()
        {
            if (label34.Text == tbtotal.Text)
            {
                dts = new DataSet();
                dtBAC = new DataTable();
                cnx.connectionopen();
                double id = double.Parse(tbidconso.Text);
                id -= 1;
                cnx.dta = new SQLiteDataAdapter("select *from COMPTEUR where IDCONSO = '" + id + "' and IDMODEL='" + tbidmodel.Text + "'", cnx.str);
                cnx.dta.Fill(dtBAC);
                if (dtBAC.Rows.Count > 0)
                {
                    //position = 0;
                    //tbibac.Text = dtBAC.Rows[position][0].ToString();
                    //tbbacmf.Text = dtBAC.Rows[position][1].ToString();
                    //tbbac1.Text = dtBAC.Rows[position][2].ToString();
                    //tbtotal.Text = dtBAC.Rows[position][3].ToString();
                    //tbdiff.Text = dtBAC.Rows[position][4].ToString();
                    label34.Text = dtBAC.Rows[position][3].ToString();
                }
                }
            }
        public void SelectionCELLBAC ()
        {
            tbtotalanterieur.Text = "";
            dts = new DataSet();
            dtBAC = new DataTable();
            String SQL = "SELECT idcomp, BacMF, Bac1, Totalcompt, diffpage, idconso, IDMODEL FROM     Compteur WHERE  (idconso = idconso) AND (IDMODEL = '" + tbidmodel.Text + "') AND (Totalcompt <'" + tbtotal.Text + "' )";
            cnx.dta = new SQLiteDataAdapter(SQL, cnx.str);
            dtBAC.Clear();
            cnx.dta.Fill(dtBAC);
            //tbtotalanterieur.Text = dtBAC.Rows[0][3].ToString();
            dataGridView3.DataSource = dtBAC;
           
            if (dataGridView3.Rows.Count > 0)
            {
                dataGridView3.CurrentCell = dataGridView3[0, dataGridView3.RowCount - 1];
                tbtotalanterieur.Text = dataGridView3.SelectedRows[0].Cells[3].Value.ToString();
                CALCULdifference();
            }
        }
        private void button12_Click(object sender, EventArgs e)
        {

            tbtotalanterieur.Text = "";
          dts = new DataSet();
            dtBAC = new DataTable();
            String SQL = "SELECT * FROM     Compteur WHERE  (idconso = idconso) AND (IDMODEL ='"+ tbidmodel .Text+ "') AND (Totalcompt <'" + tbtotal.Text + "' )";
            cnx.dta = new SQLiteDataAdapter(SQL, cnx.str);
            dtBAC.Clear();
            cnx.dta.Fill(dtBAC);
            //tbtotalanterieur.Text = dtBAC.Rows[0][3].ToString();
            dataGridView3.DataSource = dtBAC;

            if (dataGridView3.Rows.Count > 0)
            {
                dataGridView3.CurrentCell = dataGridView3[0, dataGridView3.RowCount - 1];
                tbtotalanterieur.Text = dataGridView3.SelectedRows[0].Cells[3].Value.ToString();
             
            }
        }
        //cnx.connectionopen();
        //double id = double.Parse(tbiDbac.Text);



        //cnx.dta = new SQLiteDataAdapter("select *from COMPTEUR where idcomp ='" + id + "' and idmodel='" + tbidmodel.Text + "' ", cnx.str);
        //    dtBAC.Clear();
        //    cnx.dta.Fill(dtBAC);
        //dataGridView3.DataSource = dtBAC;

        //if (label34.Text == tbtotal.Text)
        //{
        //id -= 1;

        //foreach (DataRow row in dtBAC.Rows)
        //{

        //                SELECT idcomp, BacMF, Bac1, Totalcompt, diffpage, idconso, IDMODEL
        //FROM Compteur
        //WHERE(idconso = idconso) AND(IDMODEL = 2) AND(Totalcompt < @Param1)
        //}




        //}
        //else
        //{
        //MessageBox.Show("rien");

        //}

        //    foreach (DataRow row in dtBAC.Rows)
        //{

        //   dtBAC.Rows[position][0].GetType();
        //        dataGridView3.DataSource = dtBAC;
        //        //.Text = row["ImagePath"].ToString();
        //    }
        //cnx.dta.Fill(dtconso);
        //tbconso();

        //}
        //}
        //foreach (var item in dtBAC)
        //{

        //}

        //{

        //    //position = 0;
        //    //tbibac.Text = dtBAC.Rows[position][0].ToString();
        //    //tbbacmf.Text = dtBAC.Rows[position][1].ToString();
        //    //tbbac1.Text = dtBAC.Rows[position][2].ToString();
        //    //tbtotal.Text = dtBAC.Rows[position][3].ToString();
        //    //tbdiff.Text = dtBAC.Rows[position][4].ToString();
        //    label34.Text = dtBAC.Rows[position][3].ToString();
        //    CALCULtotal();
        //    CALCULdifference();
        //    AJOUTERBAC();

        //}


        //}
        //else
        //{
        //    CALCULtotal();
        //    CALCULdifference();
        //    AJOUTERBAC();
        //}

        //chargerBac();
        //if (position>=dtBAC.Rows.Count)
        //{
        //position += 1;

        //}
        //else
        //{ position += 1;
        //chargerBac();
        //label34.Text = dtBAC.Rows[position][3].ToString();
        //position += 1;
        //chargerBac();
        //}

        //If rownum = dtt.Rows.Count - 1 Or rownum > dtt.Rows.Count - 1 Then


        //}
        //else
        //{
        //    //label34.Text = test;
        //}
        //cnx.cmd = new SQLiteCommand();
        //cnx.cnx.Open();
        //cnx.cmd.Connection = cnx.cnx;
        //CALCULdifference();


        //cnx.cmd.CommandText = "update Compteur set diffpage='" + tbdiff.Text + "' where IDCOMP='" + tbibac.Text + "'";

        //cnx.cmd.ExecuteNonQuery();
        //cnx.cnx.Close();

        //cnx.cmd = new SQLiteCommand();

        //cnx.cnx.Open();
        //cnx.cmd.Connection = cnx.cnx;





        //}

        private void tbidconso_TextChanged(object sender, EventArgs e)
        {
            connexionbac();
            ConnexionAEngine();
            string test;
            test = tbtotal.Text;
        }

        public void connexionbac()
        {
            dts = new DataSet();
            dtBAC = new DataTable();
            cnx.connectionopen();
            
            cnx.dta = new SQLiteDataAdapter("select *from COMPTEUR where IDCONSO = '" + tbidconso.Text + "'", cnx.str);
            cnx.dta.Fill(dtBAC);
            chargerBac();
            //cnx.dta = new SQLiteDataAdapter("select *from model where idmodel=idclient", cnx.str);


        }

        public void chargerBac()
        {
            //textBox3.Text = "";
            if (dtBAC.Rows.Count > 0)
            {
                //position = 0;
                tbiDbac.Text = dtBAC.Rows[position][0].ToString();
                tbbacmf.Text = dtBAC.Rows[position][1].ToString();
                tbbac1.Text = dtBAC.Rows[position][2].ToString();
                tbtotal.Text = dtBAC.Rows[position][3].ToString();
                tbdiff.Text = dtBAC.Rows[position][4].ToString();
                label34.Text = dtBAC.Rows[position][3].ToString();

            }

            else
            {
                viderBac();

            }

        }
        //=================================2
        //AJOUTER COMPTEUR DANS LA BASE DE DONNEE

        private void button6_Click(object sender, EventArgs e)
        {
            if (button6.Text == "AJOUTER")
            {
                viderBac();
                button6.Text = "VALIDER";
            }
            else
            {
                if (button6.Text == "VALIDER" && tbbac1.Text == "")
                {
                    MessageBox.Show("vide ");
                }
                else
                {
                    CALCULtotal();
                    SelectionCELLBAC();

                    AJOUTERBAC();

                    conxionconso();

                    //MessageBox.Show("AJOUTER AVEC SUCCES");
                    button6.Text = "AJOUTER";

                    if (dataGridView1.SelectedRows.Count > 0) // make sure user select at least 1 row 
                    {
                        dataGridView1.CurrentCell = dataGridView1[0, dataGridView1.RowCount - 1];

                    }
                }

            }
            //cviderBac()

        }
        
        //========
        //=========================3 MODIFIER
        private void button10_Click(object sender, EventArgs e)
        {

            try
            {
                //int id = 0;
                //tbtotalanterieur.Text = id.ToString();
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    SelectionCELLBAC();
                    CALCULtotal();
                    CALCULdifference();
                 
                    cnx.cmd = new SQLiteCommand();

                    cnx.cnx.Open();
                    cnx.cmd.Connection = cnx.cnx;
                    cnx.cmd.CommandText = "update Compteur set BacMF='" + tbbacmf.Text + "',Bac1='" + tbbac1.Text + "',diffpage='" + tbdiff.Text + "',Totalcompt='" + tbtotal.Text + "' where IDCOMP='" + tbiDbac.Text + "'";
                    CALCULtotal(); 
                    cnx.cmd.ExecuteNonQuery();
                    cnx.dta = new SQLiteDataAdapter("select *from COMPTEUR where IDCONSO = '" + tbidconso.Text + "'", cnx.str);

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
        //=================================ajouter
        public void AJOUTERBAC()
        {
            try
            {



                cnx.cmd = new SQLiteCommand();

                cnx.cnx.Open();
                cnx.cmd.Connection = cnx.cnx;

                cnx.cmd.CommandText = "insert into COMPTEUR (BacMF,Bac1,Totalcompt,diffpage,idconso,idmodel ) values ('" + tbbacmf.Text + "','" + tbbac1.Text + "','" + tbtotal.Text + "','" + tbdiff.Text + "','" + tbidconso.Text + "','" + tbidmodel.Text + "')";
                cnx.cmd.ExecuteNonQuery();
//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++     
                cnx.cnx.Close();
                dataGridView1.CurrentCell = dataGridView1[0, dataGridView1.RowCount - 1];
          
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        //==========================test

      



        private void button11_Click(object sender, EventArgs e)
        {

            //cnx.connectionopen();
            //dts = new DataSet();
            //cnx.dta = new SQLiteDataAdapter("select *from conso where IDMODEL='" + tbidclient.Text + "'", cnx.str);
            //dtconso = new DataTable();
            //dtconso.Clear();
            //cnx.dta.Fill(dtconso);
            //tbconso();
            //dataGridView1.DataSource = dtconso;
            ////AFFICHER CONNEXION BAC
            ////connexionbac();
            //if (position == dtconso.Rows.Count - 1 || position > dtconso.Rows.Count - 1)
            //{
            //    MessageBox.Show("derniere ligne");
            //}
            //else
            //{
            //    //position -= 1;

            //    //label34.Text= dtconso.Rows[position][0].ToString();
                

            //}

            //label34.Text = tbtotal.Text.ToString();
            



        }

        //=====================4
        public void viderBac()
        {
            tbbac1.Text = "";
           tbbacmf.Text = "";
            tbtotal.Text = "";
            tbdiff.Text = "";
        }

        public void DESTEBCOMPTEUR()
        {
            tbbac1.Enabled = false;
           tbbacmf.Enabled = false;
        }
        public void ACTEBCOMPTEUR()
        {
            tbbac1.Enabled = true;
            tbbacmf.Enabled = true;
        }
       //===================================5
       
        //================================================6
        private void button9_Click_2(object sender, EventArgs e)

        {
            connexionbac();
            //FORMCOMPBAC.tbco
            FORMCOMPBAC F2 = new FORMCOMPBAC();
            F2.tbcon = tbidconso.Text.ToString();


            F2.Show();
        }


        //================================= fin debut gestion de la table compteur ===================================================================================




        private void dataGridView1_SelectionChanged_1(object sender, EventArgs e)
        {


            if (dataGridView1.SelectedRows.Count > 0)
            {
                groupBox5.Enabled = true;
                groupBox4.Enabled = true;
                //position = 0;
             TBC_DATE.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
             tb_ECYAN.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                tb_EM.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                tb_EY.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
                tb_EB.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
                //""""""""""
                tb_DC.Text = dataGridView1.SelectedRows[0].Cells[6].Value.ToString();
                tb_DM.Text = dataGridView1.SelectedRows[0].Cells[7].Value.ToString();
                tb_DY.Text = dataGridView1.SelectedRows[0].Cells[8].Value.ToString();
                tb_DK.Text = dataGridView1.SelectedRows[0].Cells[9].Value.ToString();
                tb_PB.Text = dataGridView1.SelectedRows[0].Cells[10].Value.ToString();
              tb_BELT.Text = dataGridView1.SelectedRows[0].Cells[11].Value.ToString();
              tb_FOUR.Text = dataGridView1.SelectedRows[0].Cells[12].Value.ToString();
                tbidconso.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
              

            }
        }
      
        public void ConnexionAEngine()
        {
            cnx.connectionopen();
            dts = new DataSet();
            cnx.dta = new SQLiteDataAdapter("select *from ENGINE  where idconso='" + tbidconso.Text + "'", cnx.str);
            //cnx.dta = new SQLiteDataAdapter("select *from model where idmodel=idclient", cnx.str);
            dtENGINE = new DataTable();
            cnx.dta.Fill(dtENGINE);
            texboxEng();


        }
        public void texboxEng()
        {
            if (dtENGINE.Rows.Count > 0) // make sure user select at least 1 row 
            {
                //VIDETBENGINE();
                TBE_cpK.Text = dtENGINE.Rows[0][1].ToString();
                TBE_cmpC.Text = dtENGINE.Rows[0][2].ToString();
                TBE_TC.Text = dtENGINE.Rows[0][3].ToString();
                TBE_TM.Text = dtENGINE.Rows[0][4].ToString();
                TBE_TY.Text = dtENGINE.Rows[0][5].ToString();
                TBE_TK.Text = dtENGINE.Rows[0][6].ToString();
                TBE_DC.Text = dtENGINE.Rows[0][7].ToString();
                TBE_DM.Text = dtENGINE.Rows[0][8].ToString();
                TBE_DY.Text = dtENGINE.Rows[0][9].ToString();
                TBE_DK.Text = dtENGINE.Rows[0][10].ToString();
                TBE_PB.Text = dtENGINE.Rows[0][11].ToString();
                TBE_BELT.Text = dtENGINE.Rows[0][12].ToString();
                TBE_FOUR.Text = dtENGINE.Rows[0][13].ToString();
            }
            else
            {
                VIDETBENGINE();
            }
        }
        public void VIDETBENGINE()
        {
            TBE_cpK.Text = "";
            TBE_cmpC.Text = "";
            TBE_TC.Text = "";
            TBE_TM.Text = "";
            TBE_TY.Text = "";
            TBE_TK.Text = "";
            TBE_DC.Text = "";
            TBE_DM.Text = "";
            TBE_DY.Text = "";
            TBE_DK.Text = "";
            TBE_PB.Text = "";
            TBE_BELT.Text = "";
            TBE_FOUR.Text = "";
        }

        
        //Connexion a datatable model
        public void AFFICHERmodel()
        {

            cnx.connectionopen();
            dts = new DataSet();
            cnx.dta = new SQLiteDataAdapter("select *from model M where IDCLIENT='" + val + "'", cnx.str);
            //cnx.dta = new SQLiteDataAdapter("select *from model where idmodel=idclient", cnx.str);
            dtmodel = new DataTable();
            cnx.dta.Fill(dtmodel);
            dataGridView2.DataSource = dtmodel;
            chargermodel();
            //en fonction du model on charge 
            conxionconso();
        }
        //CHARGER TEXTBOX MODEL
        public void chargermodel()
        {
            if (dtmodel.Rows.Count > 0) // make sure user select at least 1 row 
            {
                //textBox1.Text = dtmodel.Rows[0][0].ToString();
                tbdate.Text = dtmodel.Rows[0][1].ToString();
                tbmodel.Text = dtmodel.Rows[0][2].ToString();
                tbserial.Text = dtmodel.Rows[0][3].ToString();
                //textBox2.Text = dtmodel.Rows[0][4].ToString();

            }

            cnx.connectionopen();
        }
        //Connexion a une table conso
        public void conxionconso()
        {

            cnx.connectionopen();
            dts = new DataSet();
            cnx.dta = new SQLiteDataAdapter("select *from conso where IDMODEL='" + tbidclient.Text + "'", cnx.str);
            dtconso = new DataTable();
            dtconso.Clear();
            cnx.dta.Fill(dtconso);
            tbconso();
            dataGridView1.DataSource = dtconso;
            //AFFICHER CONNEXION BAC
            connexionbac();

        }
        //&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //remplier les textbox conso
        private void tbconso()
        {

            if (dtconso.Rows.Count > 0) // make sure user select at least 1 row 
            {

     TBC_DATE.SelectedText = dtconso.Rows[0][1].ToString();
             tb_ECYAN.Text = dtconso.Rows[0][2].ToString();
                tb_EM.Text = dtconso.Rows[0][3].ToString();
                tb_EY.Text = dtconso.Rows[0][4].ToString();
                tb_EB.Text = dtconso.Rows[0][5].ToString();
                tb_DC.Text = dtconso.Rows[0][6].ToString();
                tb_DM.Text = dtconso.Rows[0][7].ToString();
                tb_DY.Text = dtconso.Rows[0][8].ToString();
                tb_DK.Text = dtconso.Rows[0][9].ToString();
                tb_PB.Text = dtconso.Rows[0][10].ToString();
              tb_BELT.Text = dtconso.Rows[0][11].ToString();
              tb_FOUR.Text = dtconso.Rows[0][12].ToString();
             tbidconso.Text = dtconso.Rows[0][0].ToString();
            }
            else
            {
                vidertbconso();
            }
        }
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public void vidertbconso()
        {
            tb_ECYAN.Text = "";
            tb_EM.Text = "";
            tb_EY.Text = "";
            tb_EB.Text = "";
            //""""""""""
            tb_DC.Text = "";
            tb_DM.Text = "";
            tb_DY.Text = "";
            tb_DK.Text = "";
            tb_PB.Text = "";
            tb_BELT.Text = "";
            tb_FOUR.Text = "";
            tbidconso.Text = "";
            TBC_DATE.Text = "";
        }
        public void Desac_tbconso()
        {
            tb_ECYAN.Enabled = false;
            tb_EM.Enabled = false;
            tb_EY.Enabled = false;
            tb_EB.Enabled = false;
            //""""""""""
            tb_DC.Enabled = false;
            tb_DM.Enabled = false;
            tb_DY.Enabled = false;
            tb_DK.Enabled = false;
            tb_PB.Enabled = false;
            tb_BELT.Enabled = false;
            tb_FOUR.Enabled = false;
            tbidconso.Enabled = false;
            TBC_DATE.Enabled = false;
        }
        public void Ac_tbconso()
        {
            tb_ECYAN.Enabled = true;
            tb_EM.Enabled = true;
            tb_EY.Enabled = true;
            tb_EB.Enabled = true;
            //""""""""""    true;
            tb_DC.Enabled = true;
            tb_DM.Enabled = true;
            tb_DY.Enabled = true;
            tb_DK.Enabled = true;
            tb_PB.Enabled = true;
            tb_BELT.Enabled = true;
            tb_FOUR.Enabled = true;
            tbidconso.Enabled = true;
            TBC_DATE.Enabled = true;
        }

        //affihcer les donnees datagv dans les texbox lies
        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count > 0) // make sure user select at least 1 row 
            {
                //panel2.Enabled = true;
                dataGridView2.Refresh();
                tbdate.Text = dataGridView2.SelectedRows[0].Cells[1].Value.ToString();
                tbmodel.Text = dataGridView2.SelectedRows[0].Cells[2].Value.ToString();
                tbserial.Text = dataGridView2.SelectedRows[0].Cells[3].Value.ToString();
                tbidclient.Text = dataGridView2.SelectedRows[0].Cells[0].Value.ToString();
                tbidmodel.Text = dataGridView2.SelectedRows[0].Cells[4].Value.ToString();
            }

        }
        //charger tbconso en fonction de l'index
       
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            conxionconso();


        }






       

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }
        //LESCONSO REQUETE SQLITE
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //AJOUTER UNE NOUVELLE MACHINE

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                cnx.cmd = new SQLiteCommand();

                cnx.cnx.Open();
                cnx.cmd.Connection = cnx.cnx;

                cnx.cmd.CommandText = "insert into MODEL(DATEMODEL,MODEL,SERIAL,idclient)values ('" + tbdate.Text + "','" + tbmodel.Text + "','" + tbserial.Text + "','" + val + "' )";
                cnx.dta = new SQLiteDataAdapter("select *from model M where IDCLIENT='" + val + "'", cnx.str);
                cnx.cmd.ExecuteNonQuery();
                cnx.cnx.Close();
                dtmodel = new DataTable();
                dtmodel.Clear();
                cnx.dta.Fill(dtmodel);
                dataGridView2.DataSource = dtmodel;
                chargermodel();

                //MessageBox.Show("AJOUTER AVEC SUCCES");
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }
        //modifier le model
        private void button7_Click(object sender, EventArgs e)
        {
            cnx.cmd = new SQLiteCommand();

            cnx.cnx.Open();
            cnx.cmd.Connection = cnx.cnx;
            //"update client set Intitule='" + tbNOM.Text + "',Contact='" + tbAdress.Text + "' where idclient=" + dataGridView1.SelectedRows[0].Cells[0].Value.ToString() + "";

            cnx.cmd.CommandText = "update MODEL set DATEMODEL='" + tbdate.Text + "',MODEL='" + tbmodel.Text + "',SERIAL='" + tbserial.Text + "' where IDMODEL='" + tbidclient.Text + "'";
            cnx.dta = new SQLiteDataAdapter("select *from model M where IDCLIENT='" + val + "'", cnx.str);
            cnx.cmd.ExecuteNonQuery();
            cnx.cnx.Close();
            dtmodel = new DataTable();
            dtmodel.Clear();
            cnx.dta.Fill(dtmodel);
            dataGridView2.DataSource = dtmodel;
            chargermodel();
            //MessageBox.Show("modifie AVEC SUCCES");
        }
        //public void verif( bool verif)
        //   {


        //   }

        //AJOUTER CONSO DANS LA TABLE
        private void button5_Click(object sender, EventArgs e)
        {
            try
            {

                if (button5.Text == "AJOUTER")
                {
                   
                    button5.Text = "VALIDER";
                    button8.Text = "ANNULER";
                    panel2.Enabled = true;
                    dataGridView1.CurrentCell = dataGridView1[0, dataGridView1.RowCount - 1];
                    vidertbconso();
                    TBC_DATE.Focus();
                }
                else
                {
                    if (button5.Text == "VALIDER" && tb_ECYAN.Text == "" && tb_EM.Text == "" && tb_EY.Text == "" && tb_EB.Text == ""
                        && tb_DC.Text == "" && tb_DM.Text == "" && tb_DY.Text == "" && tb_DK.Text == "" && tb_PB.Text == "" && tb_BELT.Text == "" && tb_FOUR.Text == "" && tbidclient.Text != "")
                    {
                        MessageBox.Show("veuillez remplir toutes les cases vides");
                    }


                    else
                    {
                        button5.Text = "AJOUTER";
                        cnx.cmd = new SQLiteCommand();

                        cnx.cnx.Open();
                        cnx.cmd.Connection = cnx.cnx;

                        cnx.cmd.CommandText = "insert into conso(DATE,tonerCyan,tonerMAgent,tonerJaune,tonerBlack,DrumCyan,DrumMag,DrumJaune,DrumBlack,Poubelle,Belt,Four,IDMODEL)" +
                            "values ('" + TBC_DATE.Text + "','" + tb_ECYAN.Text + "','" + tb_EM.Text + "','" + tb_EY.Text + "','" + tb_EB.Text + "', '" + tb_DC.Text + "','" + tb_DM.Text + "','" + tb_DY.Text + "','" + tb_DK.Text + "','" + tb_PB.Text + "','" + tb_BELT.Text + "','" + tb_FOUR.Text + "','" + tbidclient.Text + "')";
                        cnx.dta = new SQLiteDataAdapter("select *from conso where IDMODEL='" + tbidclient.Text + "'", cnx.str);
                        //IDMODEL = '" + textBox1.Text + "'", cnx.str);
                        cnx.cmd.ExecuteNonQuery();
                        cnx.cnx.Close();
                        dtconso = new DataTable();
                        dtconso.Clear();
                        cnx.dta.Fill(dtconso);
                        tbconso();
                        dataGridView1.DataSource = dtconso;
                        button6.Text = "VALIDER";
                        tbbacmf.Focus();
                        //connexionbac();
                        //FORMCOMPBAC F2 = new FORMCOMPBAC();
                        //F2.tbcon = tbidconso.Text.ToString();


                        //F2.Show();

                    }

                    if (dataGridView1.SelectedRows.Count > 0) // make sure user select at least 1 row 
                    {
                        dataGridView1.CurrentCell = dataGridView1[0, dataGridView1.RowCount - 1];
                       
                        //dataGridView1.Enabled = false;

                    }
                }



                //MessageBox.Show("AJOUTER AVEC SUCCES");
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }

        //modifier conso  DANS LA BASE DE DONNEE
        private void button8_Click(object sender, EventArgs e)
        {
            //try
            //{
                if (button8.Text == "ANNULER")
                {
                    button5.Text = "AJOUTER";
                button8.Text = "MODIFIER";

                }
                else
            {
                panel2.Enabled =true;
                if (button8.Text == "MODIFIER" || dataGridView1.SelectedRows.Count > 0 || panel2.Enabled == true)
                    {
                   

                    cnx.cmd = new SQLiteCommand();

                    cnx.cnx.Open();
                    cnx.cmd.Connection = cnx.cnx;
                    //cnx.cmd.CommandText = "update MODEL set DATEMODEL='" + tbdate.Text + "',MODEL='" + tbmodel.Text + "',SERIAL='" + tbserial.Text + "' where IDMODEL='" + textBox1.Text + "'";
                    cnx.cmd.CommandText = "update conso set DATE='" + TBC_DATE.Text + "', tonerCyan='" + tb_ECYAN.Text + "',tonerMAgent='" + tb_EM.Text + "',tonerJaune='" + tb_EY.Text + "',tonerBlack='" + tb_EB.Text + "',DrumCyan='" + tb_DC.Text + "',DrumMag='" + tb_DM.Text + "',DrumJaune='" + tb_DY.Text + "', DrumBlack= '" +
                    tb_DK.Text + "', Poubelle = '" + tb_PB.Text + "',Belt = '" + tb_BELT.Text + "',Four = '" + tb_FOUR.Text + "'where idconso = '" + tbidconso.Text + "'";
                    cnx.dta = new SQLiteDataAdapter("select *from conso where IDMODEL='" + tbidclient.Text + "'", cnx.str);
                    //IDMODEL = '" + textBox1.Text + "'", cnx.str);
                    cnx.cmd.ExecuteNonQuery();
                    cnx.cnx.Close();
                    dtconso = new DataTable();
                    dtconso.Clear();
                    cnx.dta.Fill(dtconso);
                    tbconso();
                    dataGridView1.DataSource = dtconso;
                }
            
                else
                {

                MessageBox.Show("AUCUNE LIGNE SELECTIONNEE");
            }

        }


    }
                //if (dataGridView1.SelectedRows.Count > 0)
                //    {
                //        

                //   
                //    //MessageBox.Show("AJOUTER AVEC SUCCES");
                //}
                //catch (Exception ex)
                //{

                //    MessageBox.Show(ex.Message);
                //}

                //}


                //ajouter engine
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                cnx.cmd = new SQLiteCommand();

                cnx.cnx.Open();
                cnx.cmd.Connection = cnx.cnx;

                cnx.cmd.CommandText = "insert into ENGINE (comptK,COMPTC,TON_Change_C,TON_Change_M,TON_Change_J,TON_Change_K,Tam_Chand_C,Tam_Chand_M,Tam_Chand_J,Tam_Chand_K,Poubelle,Belt,Four,IDconso )" +
               "values ('" + TBE_cpK.Text + "','" + TBE_cmpC.Text + "','" + TBE_TC.Text + "','" + TBE_TM.Text + "','" + TBE_TY.Text + "','" + TBE_TK.Text + "','" + TBE_DC.Text + "','" + TBE_DM.Text + "','" + TBE_DY.Text + "','" + TBE_DK.Text + "','" + TBE_PB.Text + "','" + TBE_BELT.Text + "','" + TBE_FOUR.Text + "','" + tbidconso.Text + "' )";
                cnx.cmd.ExecuteNonQuery();
                cnx.cnx.Close();
                cnx.dta = new SQLiteDataAdapter("select *from ENGINE  where idconso='" + tbidconso.Text + "'", cnx.str);
                dtENGINE = new DataTable();
                cnx.dta.Fill(dtENGINE);
                texboxEng();

                MessageBox.Show("AJOUTER AVEC SUCCES");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //MODIFIER ENGINE
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {

                cnx.cmd = new SQLiteCommand();

                cnx.cnx.Open();
                cnx.cmd.Connection = cnx.cnx;

                cnx.cmd.CommandText = "update ENGINE set comptK='" + TBE_cpK.Text + "',COMPTC='" + TBE_cmpC.Text + "' " +
                ",TON_Change_C='" + TBE_TC.Text + "',TON_Change_M='" + TBE_TM.Text + "',TON_Change_J='" + TBE_TY.Text + "'" +
                ",TON_Change_K='" + TBE_TY.Text + "',Tam_Chand_C='" + TBE_DC.Text + "',Tam_Chand_M='" + TBE_DM.Text + "'" +
                ",Tam_Chand_J='" + TBE_DY.Text + "',Tam_Chand_K='" + TBE_DK.Text + "',Poubelle='" + TBE_PB.Text + "'" +
                ",Belt='" + TBE_BELT.Text + "',Four='" + TBE_FOUR.Text + "' where IDeng ='" + dtENGINE.Rows[0][0].ToString() + "'";
                cnx.cmd.ExecuteNonQuery();
                cnx.cnx.Close();
                cnx.dta = new SQLiteDataAdapter("select *from ENGINE  where idconso='" + tbidconso.Text + "'", cnx.str);
                dtENGINE = new DataTable();
                cnx.dta.Fill(dtENGINE);
                texboxEng();

                MessageBox.Show("modifie AVEC SUCCES");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


      

       
       
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        private void textBox14_TextChanged(object sender, EventArgs e)
        {

        }
        public void calcul(string tb1, string tb2)
        {
            //string RESULTA = int.Parse(tb1+tb2).ToString();
        }








        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }



        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tbtotal_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbbac1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label34_Click(object sender, EventArgs e)
        {



        }

        private void tbbacmf_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            position = position - 1;
            chargerBac();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button9_Click_1(object sender, EventArgs e)
        {

        }




        private void tb_ECYAN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                tb_EM.Focus();
            }
        }

        private void tb_EM_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                tb_EY.Focus();
            }
        }

        private void tb_EY_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                tb_EB.Focus();
            }
        }

        private void tb_EB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                tb_DC.Focus();
            }
        }

        //private void tb_DC_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyData == Keys.Enter)
        //    {
        //        tb_DM.Focus();
        //    }
        //}

        private void tb_DC_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                tb_DM.Focus();
            }
        }

        private void tb_DM_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                tb_DY.Focus();
            }
        }

        private void tb_DY_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                tb_DK.Focus();
            }
        }

        private void tb_DK_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                tb_PB.Focus();
            }
        }

        private void tb_PB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                tb_BELT.Focus();
            }
        }

        private void tb_BELT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                tb_FOUR.Focus();
               
            }
        }

        public void AjouterConso()
        {

            cnx.cmd = new SQLiteCommand();

            cnx.cnx.Open();
            cnx.cmd.Connection = cnx.cnx;

            cnx.cmd.CommandText = "insert into conso(DATE,tonerCyan,tonerMAgent,tonerJaune,tonerBlack,DrumCyan,DrumMag,DrumJaune,DrumBlack,Poubelle,Belt,Four,IDMODEL)" +
                "values ('" + TBC_DATE.Text + "','" + tb_ECYAN.Text + "','" + tb_EM.Text + "','" + tb_EY.Text + "','" + tb_EB.Text + "', '" + tb_DC.Text + "','" + tb_DM.Text + "','" + tb_DY.Text + "','" + tb_DK.Text + "','" + tb_PB.Text + "','" + tb_BELT.Text + "','" + tb_FOUR.Text + "','" + tbidclient.Text + "')";
            cnx.dta = new SQLiteDataAdapter("select *from conso where IDMODEL='" + tbidclient.Text + "'", cnx.str);
            //IDMODEL = '" + textBox1.Text + "'", cnx.str);
            cnx.cmd.ExecuteNonQuery();
            cnx.cnx.Close();
            dtconso = new DataTable();
            dtconso.Clear();
            cnx.dta.Fill(dtconso);
            tbconso();
            dataGridView1.DataSource = dtconso;
        }
        private void tb_FOUR_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                button5.Focus();

            }
            //    try
            //    {

            //            if (button5.Text == "VALIDER" && tb_ECYAN.Text == "" && tb_EM.Text == "" && tb_EY.Text == "" && tb_EB.Text == ""
            //                && tb_DC.Text == "" && tb_DM.Text == "" && tb_DY.Text == "" && tb_DK.Text == "" && tb_PB.Text == "" && tb_BELT.Text == "" && tb_FOUR.Text == "" && textBox1.Text != "")
            //            {
            //                MessageBox.Show("veuillez remplir toutes les cases vides");
            //            }


            //            else
            //            {
            //                button5.Text = "AJOUTER";
            //                cnx.cmd = new SQLiteCommand();

            //cnx.cnx.Open();
            //cnx.cmd.Connection = cnx.cnx;

            //cnx.cmd.CommandText = "insert into conso(DATE,tonerCyan,tonerMAgent,tonerJaune,tonerBlack,DrumCyan,DrumMag,DrumJaune,DrumBlack,Poubelle,Belt,Four,IDMODEL)" +
            //    "values ('" + TBC_DATE.Text + "','" + tb_ECYAN.Text + "','" + tb_EM.Text + "','" + tb_EY.Text + "','" + tb_EB.Text + "', '" + tb_DC.Text + "','" + tb_DM.Text + "','" + tb_DY.Text + "','" + tb_DK.Text + "','" + tb_PB.Text + "','" + tb_BELT.Text + "','" + tb_FOUR.Text + "','" + textBox1.Text + "')";
            //cnx.dta = new SQLiteDataAdapter("select *from conso where IDMODEL='" + textBox1.Text + "'", cnx.str);
            ////IDMODEL = '" + textBox1.Text + "'", cnx.str);
            //cnx.cmd.ExecuteNonQuery();
            //cnx.cnx.Close();
            //dtconso = new DataTable();
            //dtconso.Clear();
            //cnx.dta.Fill(dtconso);
            //tbconso();
            //dataGridView1.DataSource = dtconso;


            //            }

            //            if (dataGridView1.SelectedRows.Count > 0) // make sure user select at least 1 row 
            //            {
            //                dataGridView1.CurrentCell = dataGridView1[0, dataGridView1.RowCount - 1];

            //            }




            //        //MessageBox.Show("AJOUTER AVEC SUCCES");
            //    }
            //    catch (Exception ex)
            //    {

            //        MessageBox.Show(ex.Message);
            //    }
        }










        private void button10_Click_1(object sender, EventArgs e)
        {
            //position = -1;

            //cnx.cmd  = new SQLiteCommand();
            //    SQLiteDataReader resulta;
            //    bool lecture;

            
            //sql =" SELECT Client.Intitule, MODEL.DATEMODEL, MODEL.MODEL, Compteur.BacMF, Compteur.Bac1, Compteur.Totalcompt, Conso.[DATE] FROM Client INNER JOIN MODEL ON Client.IdClient = MODEL.IDclient INNER JOIN Conso ON MODEL.IDMODEL = Conso.IDMODEL INNER JOIN  Compteur ON Conso.idConso = Compteur.idconso WHERE Compteur.Totalcompt < '"+ textBox4 .Text+ "' ";
            //cnx.cnx.Open();
            //    cnx.cmd.Connection = cnx.cnx;
            //cnx.cmd.CommandText = sql;
            //try
            //{
            //    if (dataGridView1.SelectedRows.Count > 0)
            //    {

            //        resulta = cnx.cmd.ExecuteReader();

            //        dtRECH = new DataTable();
            //        dtRECH.Clear();
            //        dtRECH.Load(resulta);
            //        dataGridView1.DataSource = dtRECH;
            //        cnx.cnx.Close();
            //    }

            //    }
            //catch (Exception E)
            //{

            //    MessageBox.Show(E.Message);

             
            //}


        }
        

        private void label36_Click(object sender, EventArgs e)
        {
           
        }

        private void TBC_DATE_Leave(object sender, EventArgs e)
        {

        }

        private void TBC_DATE_KeyDown_1(object sender, KeyEventArgs e)
        {
            try
            {
                date = TBC_DATE.Text;

                //date = dynamicMaskedTextBox.Mask;
                if (e.KeyData == Keys.Enter)
                {

                    DateTime dtm = DateTime.Parse(date);

                    TBC_DATE.Text = dtm.ToString("dd/MM/yyyy");
                    tb_ECYAN.Focus();
                }
            }
            catch (Exception E)
            {

                MessageBox.Show(E.Message);
            }

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView3_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView3.Rows.Count > 0)
            {
                //tbtotalanterieur.Text = dataGridView3.Rows[0].Cells[3].Value.ToString();
            }
        }

        private void tbbacmf_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                tbbac1.Focus();

            }
        }

        private void tbbac1_ImeModeChanged(object sender, EventArgs e)
        {
            
        }

       
    }
    }

        
    

