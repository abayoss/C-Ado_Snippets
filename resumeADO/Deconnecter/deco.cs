using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace resumeADO.Deconnecter
{
    public partial class deco : Form
    {
        public deco()
        {
            InitializeComponent();
        }
        ADO ADO = new ADO();
        public static int cpt;
        private void deco_Load(object sender, EventArgs e)
        {   ADO.CONNECTER();//<<-------connect first 
            //update_name(5, "java");
            RemplirCombo();
            RemplirGrid();
        }
        //-----------------------------------------------------------------les methodes-----------------------------------------------------------------:
        #region
        // procedure de navigation
        public void NAVIGATION()
        {
            ADO.requeteCUDdec("select * from PRODUIT", "produit");
            txtref.Text = ADO.ds.Tables["produit"].Rows[cpt][0].ToString();
            txtdes.Text = ADO.ds.Tables["produit"].Rows[cpt][1].ToString();
            txtqte.Text = ADO.ds.Tables["produit"].Rows[cpt][2].ToString();
        }//etape 1 - Remplissage du GridView / combo
        public void clearTable()
        { if (ADO.ds.Tables["produit"] != null) ADO.ds.Tables["produit"].Clear(); }
        public void RemplirGrid()
        {   clearTable();
            ADO.requeteCUDdec("select * from PRODUIT", "produit");
            dataGridView1.DataSource = ADO.ds.Tables["produit"];
        }// Remplissage de combobox
        public void RemplirCombo()
        {   comboBox1.Items.Clear();
            ADO.requeteCUDdec("select * from CATEGORIE", "cat");
            comboBox1.DataSource = ADO.ds.Tables["cat"];
            comboBox1.DisplayMember = "Nom";
            comboBox1.ValueMember = "IDCAT";
        }//stored Procedure
        public void update_name(int param1, string param2)
        {   SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@IDCAT", SqlDbType.Int);
            param[0].Value = param1;
            param[1] = new SqlParameter("@NomCAT", SqlDbType.VarChar, 50);
            param[1].Value = param2;
            ADO.ExecuteProcedure("stored",param);
        }// METHODE VIDER
        public void VIDER(Control f)
        {   foreach (Control ct in f.Controls)
            {   if (ct.GetType() == typeof(TextBox)) ct.Text = "";
                if (ct.GetType() == typeof(ComboBox)) ct.ResetText(); 
                if (ct.Controls.Count != 0) VIDER(ct); 
            }
        }
        #endregion
        //-----------------------------------------------------------------les boutons------------------------------------------------------------------:
        #region
        private void fermer_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Voulez vous quitter?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            { ADO.DECONNECTER(); this.Close(); }
        }

        private void vider_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Voulez vous vider les champs?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            { VIDER(this); }
        }
        //button ajouter :
        private void ajouter_Click(object sender, EventArgs e)
        {
            //if (txtref.Text == "" || txtdes.Text == "" || txtqte.Text == ""){return;}
            ADO.ligne = ADO.ds.Tables["produit"].NewRow();
            ADO.ligne[0] = txtref.Text;
            ADO.ligne[1] = txtdes.Text;
            ADO.ligne[2] = txtqte.Text;
            ADO.ligne[3] = comboBox1.SelectedValue;
            //for (int i = 0; i < ADO.ds.Tables["produit"].Rows.Count; i++)
            //{
            //    if (txtref.Text == ADO.ds.Tables["produit"].Rows[i][0].ToString())
            //    {   MessageBox.Show("Produit existe déja");
            //        return;
            //    }
            //}
            ADO.ds.Tables["produit"].Rows.Add(ADO.ligne);
            dataGridView1.DataSource = ADO.ds.Tables["produit"];
            MessageBox.Show("Produit ajouter avec succes");
        }
        //boutton supprimer :
        private void supprimer_Click(object sender, EventArgs e)
        {   bool tr = false;
            for (int i = 0; i < ADO.ds.Tables["produit"].Rows.Count; i++)
            {
                if (txtref.Text == ADO.ds.Tables["produit"].Rows[i][0].ToString())
                {   tr = true;
                    ADO.ds.Tables["produit"].Rows[i].Delete();
                    dataGridView1.DataSource = ADO.ds.Tables["produit"];
                    //MessageBox.Show("Produit supprimer");
                    break;
                }
            }
            if (tr == false)
            { MessageBox.Show("Produit n'existe pas"); }
        }
        //button modifier
        private void modifier_Click(object sender, EventArgs e)
        {   bool tr = false;
            for (int i = 0; i < ADO.ds.Tables["produit"].Rows.Count; i++)
            {
                if (txtref.Text == ADO.ds.Tables["produit"].Rows[i][0].ToString())
                {   tr = true;
                    ADO.ds.Tables["produit"].Rows[i][1] = txtdes.Text;
                    ADO.ds.Tables["produit"].Rows[i][2] = txtqte.Text;
                    ADO.ds.Tables["produit"].Rows[i][3] = comboBox1.SelectedValue;
                    dataGridView1.DataSource = ADO.ds.Tables["produit"];
                    //MessageBox.Show("Produit modifier !");
                    break;
                }
            }
            if (tr == false)
            { MessageBox.Show("Produit n'existe pas"); }
        }//button enregistrer
        private void enregistrer_Click(object sender, EventArgs e)
        {
            ADO.bc = new SqlCommandBuilder(ADO.da);
            ADO.da.Update(ADO.ds, "produit");
            //MessageBox.Show("enregistrer vc succes!");
        }//button rechercher :
        private void rechercher(object sender, EventArgs e)
        {   clearTable();
            ADO.requeteCUDdec("select * from PRODUIT WHERE IDCAT='" + comboBox1.SelectedValue + "'","produit");
            dataGridView1.DataSource = ADO.ds.Tables["produit"];
        }
        #endregion
        //-----------------------------------------------------------------bouttons de navigation DEC-------------------------------------------------------:
        #region
        private void premier_Click(object sender, EventArgs e)
        { cpt = 0; NAVIGATION(); }
        private void dernier_Click(object sender, EventArgs e)
        { cpt = ADO.ds.Tables["produit"].Rows.Count - 1; NAVIGATION(); }
        private void suivant_Click(object sender, EventArgs e)
        {   try
            { cpt++; NAVIGATION(); }
            catch
            {cpt--; }
        }
        private void precedant_Click(object sender, EventArgs e)
        {   try
            { cpt--; NAVIGATION(); }
            catch
            { cpt++; }
        }
        #endregion
    }
}
