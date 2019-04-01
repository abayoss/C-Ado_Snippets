using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace resumeADO
{
    public partial class Form1 : Form
    {
        ADO ADO = new ADO();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ADO.CONNECTER();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {


            ADO.cmd = new SqlCommand("select count(mat) from STAGIAIRE where nom='" + txtnom.Text + "' and mat='" + txtmatricule.Text + "'", ADO.con);
            int tr = (int)ADO.cmd.ExecuteScalar();

            if (tr != 0) {
                this.Hide();
                Form2 f = new Form2();
                f.Show();
            }
            
            //bool tr = false;
            //ADO.requeteRead("select mat,nom from STAGIAIRE");
            //while (ADO.dr.Read())
            //{
            //    if (txtnom.Text.Equals(ADO.dr[1].ToString()) && txtmatricule.Text.Equals(ADO.dr[0].ToString()))
            //    {
            //        tr = true;
            //        Hide();
            //        Form2 f = new Form2();
            //        f.Show();
            //        break;
            //    }
            //}
            //if (!tr)
            //{
            //    MessageBox.Show("eeeeeeeeey boy!");
            //}
            //dr should be closed :
            //ADO.dr.Close();
        }

        
    }
}
