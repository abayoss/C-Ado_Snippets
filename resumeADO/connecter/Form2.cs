using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace resumeADO
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        ADO ADO = new ADO();
        private void Form2_Load(object sender, EventArgs e)
        {
            ADO.CONNECTER();
            remplirGrid();
            ADO.dr.Close();
            remplierComboBox();
        }
        // ----------------------------------------------------------LES METHODES----------------------------------------------------- :
        #region
        public void VIDER(Control f)
        {
            foreach (Control ct in f.Controls)
            {
                if (ct.GetType() == typeof(TextBox))    { ct.Text = ""; }
                if (ct.Controls.Count != 0)     { VIDER(ct); }
            }
        }
        public void remplirGrid()
        {
            if (ADO.dt.Rows != null) ADO.dt.Clear();
            ADO.requeteRead("select * from STAGIAIRE");
            ADO.dt.Load(ADO.dr);
            dataGridView1.DataSource = ADO.dt;
        }
        public void remplierComboBox()
        {
            comboBox1.Items.Clear();
            ADO.requeteRead("select Mat from STAGIAIRE");
            while (ADO.dr.Read())
            { comboBox1.Items.Add(ADO.dr[0]); }
            ADO.dr.Close();
        }
        public int nombre()
        {
            int cpt;
            ADO.cmd = new SqlCommand(" select count(Mat) from STAGIAIRE where Mat ='" + txtmatricule.Text + "'",ADO.con);
            cpt = (int)ADO.cmd.ExecuteScalar();
            return cpt;
        }
        public bool ajouter()
        {
            if (nombre() == 0)
            {
                ADO.requeteCUD("insert into STAGIAIRE values ('" + txtmatricule.Text + "','" + txtnom.Text + "','" + txtprenom.Text + "','" + txtmoyenne.Text + "','" + txtage.Text + "')");
                return true;
            }
            else
                return false;
        }
        public bool modifier()
        {
            if (nombre() != 0)
            {
                ADO.requeteCUD("update STAGIAIRE set Nom = '" + txtnom.Text + "',Prenom ='" + txtprenom.Text + "',Moyenne='" + txtmoyenne.Text + "',Age ='" + txtage.Text + "'where Mat ='" + txtmatricule.Text + "'");
                return true;
            }
            return false;
        }
        public bool Supprimer()
        {
            if (nombre() != 0)
            {
                ADO.requeteCUD("delete from STAGIAIRE where Mat ='" + txtmatricule.Text + "'");
                return true;
            }
            return false;
        }
        #endregion
        // ----------------------------------------------------------LES BouTons----------------------------------------------------- :
        #region
        //button vider
        private void vider_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Voulez vous vider les champs ?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                VIDER(this);
            }
        }
        //button fermer :
        private void fermer_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Voulez vous quitter ?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                ADO.DECONNECTER();
                Close();
            }
        }
        //button ajouter 
        private void ajouter_Click(object sender, EventArgs e)
        {
            if (txtage.Text == "" || txtmatricule.Text == "" || txtmoyenne.Text == "" || txtnom.Text == "" || txtprenom.Text == "")
            {
                MessageBox.Show("ajouter tt les champs");
                return;
            }
            if (ajouter() == true)
            {
                MessageBox.Show("ajouter vc succes");
                remplirGrid();
            }
            else
            {
                MessageBox.Show("stagiaire exist deja");
            }
        }
        //button supprimer
        private void supprimer_Click(object sender, EventArgs e)
        {
            if (txtmatricule.Text == "")
            {
                MessageBox.Show("ajouter le champ matrucule");
                return;
            }
            if (Supprimer() == true)
            {
                MessageBox.Show("supprimer vc succes");
                remplirGrid();
            }
            else
            {
                MessageBox.Show("stagiaire n'exist pas");
            }
        }
        
        //button modifier
        private void modifier_Click(object sender, EventArgs e)
        {

            if (txtage.Text == "" || txtmatricule.Text == "" || txtmoyenne.Text == "" || txtnom.Text == "" || txtprenom.Text == "")
            {
                MessageBox.Show("ajouter tt les champs");
                return;
            }
            if (modifier() == true)
            {
                MessageBox.Show("modifier vc succes");
                remplirGrid();
            }
            else
            {
                MessageBox.Show("stagiaire n'exist pas! ");
            }
        }
        #endregion
        // boutton to open navigation form :
        private void navigation_Click(object sender, EventArgs e)
        {
            navigation n = new navigation();
            n.Show();
        }
        // boutton to open LOGIN form :
        private void LOGIN_Click(object sender, EventArgs e)
        {
            Form1 L = new Form1();
            L.Show();
        }
        private void button7_Click(object sender, EventArgs e)
        {
            Deconnecter.deco d = new Deconnecter.deco();
            d.Show();
        }
    }
}
