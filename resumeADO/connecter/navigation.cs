using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace resumeADO
{
    //navigation et supprimer/exporter () => par selection
    public partial class navigation : Form
    {
        public navigation()
        {
            InitializeComponent();
        }
        ADO ADO = new ADO();
        public static int cpt;
        //form load
        private void navigation_Load(object sender, EventArgs e)
        {
            ADO.CONNECTER();
            remplirGrid();
            ajouterColone();
            remplierComboBox();
        }
        //Les methodes : 
        #region
        //remplir les textBox vc la dt
        public void remplirTextBox()
        {
            if (ADO.dt.Rows != null) ADO.dt.Clear();
            ADO.requeteRead(" select * from STAGIAIRE");
            ADO.dt.Load(ADO.dr);
            txtmatricule.Text = ADO.dt.Rows[cpt][0].ToString();
            txtnom.Text = ADO.dt.Rows[cpt][1].ToString();
            txtprenom.Text = ADO.dt.Rows[cpt][2].ToString();
            txtmoyenne.Text = ADO.dt.Rows[cpt][3].ToString();
            txtage.Text = ADO.dt.Rows[cpt][4].ToString();
        }
        public void remplirGrid()
        {
            //to refresh dataGrid :
            if (ADO.dt.Rows != null) ADO.dt.Clear();
            ADO.requeteRead(" select * from STAGIAIRE");
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
        // ajouter une checkbox Column => pour selectioner 
        public void ajouterColone()
        {
            DataGridViewCheckBoxColumn check = new DataGridViewCheckBoxColumn();
            check.Name = "Selection";
            dataGridView1.Columns.Add(check);
        }
        //supprimer => par selection d'une ligne ou plusieurs ligne dans un datagridview 
        public void supprimer()
        {
            foreach (DataGridViewRow r in dataGridView1.Rows)
            {
                if (Convert.ToBoolean(r.Cells["Selection"].Value) == true)
                {
                    ADO.requeteCUD("delete from STAGIAIRE WHERE Mat='" + r.Cells["Mat"].Value.ToString() +"'");
                }
            }
            MessageBox.Show("supprimer vc succes");
            remplirGrid();
        }
        //exporter => par selection d'une ligne ou plusieurs ligne dans un datagridview 
        public void EXPORTER()
        {
            //string chemin = "";
            //saveFileDialog1.Filter = "TEXT FILES |.*txt";
            //if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            //{
            //    chemin = saveFileDialog1.FileName;
            //}
            //StreamWriter st = new StreamWriter(chemin);
            //foreach (DataGridViewRow item in dataGridView1.Rows)
            //{
            //    if (Convert.ToBoolean(item.Cells["Selection"].Value) == true)
            //    {
            //        st.WriteLine(item.Cells[0].Value.ToString() + " " + item.Cells[1].Value.ToString());
            //    }
            //}
            //st.Close();
            //MessageBox.Show("enregistrer");
        }

        public void exporXML()
        {
            ADO.requeteCUDdec("select * from PRODUIT", "produit");
            string chemin = "";
            saveFileDialog1.Filter = "XML FILES |.*xml";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                chemin = saveFileDialog1.FileName;
            }
            ADO.ds.WriteXml(chemin);
            MessageBox.Show("enregistrer");
        }
        #endregion
        //-----------------------------------------------------------------bouttons de navigation Conn-------------------------------------------------------:
        #region
        private void premier_Click(object sender, EventArgs e)
        {
            cpt = 0;
            remplirTextBox();
        }
        private void end_Click(object sender, EventArgs e)
        {
            cpt = ADO.dt.Rows.Count - 1;
            remplirTextBox();
        }
        private void Suivant_Click(object sender, EventArgs e)
        {
            try
            {
                cpt++;
                remplirTextBox();
            }
            catch
            {
                MessageBox.Show("you'r at the end boy!");
                cpt--;
            }
        }
        private void precedant_Click(object sender, EventArgs e)
        {
            try
            {
                cpt--;
                remplirTextBox();
            }
            catch
            {
                MessageBox.Show("you'r at the end boy!");
                cpt++;
            }
        }
        #endregion
        //button supprimer , exporter fishier :
        #region
        //btn suprimer ce qui est selectioner :
        private void btnSupp_Click(object sender, EventArgs e)
        {
            supprimer();
            remplierComboBox();
        }
        private void btnExporter_Click(object sender, EventArgs e)
        { EXPORTER(); }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            exporXML();
        }
    }
}
