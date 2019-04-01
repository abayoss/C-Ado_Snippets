using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace resumeADO
{
    class ADO
    {
        public SqlConnection con = new SqlConnection();

        public SqlCommand cmd = new SqlCommand();// <---------------uncomment pour le mode Connecter :
        public DataTable dt = new DataTable();
        public SqlDataReader dr;
        public SqlDataAdapter da = new SqlDataAdapter();// <----------uncomment pour le mode deconnecter :
        public DataSet ds = new DataSet();
        public DataRow ligne;
        public SqlCommandBuilder bc;
        public void CONNECTER()
        {
            if (con.State == ConnectionState.Closed || con.State == ConnectionState.Broken)
            {
                con = new SqlConnection("Data Source=.;Initial Catalog=TDI;Integrated Security=True");
                con.Open();
            }
        }
        public void DECONNECTER()
        {
            if (con.State == ConnectionState.Open)
            { con.Close(); }
        }
        // ------------ Connecter
        public void requeteRead(string requetR)
        {
            cmd = new SqlCommand(requetR, con);
            dr = cmd.ExecuteReader();
            //dt.Load(dr);
        }
        public void requeteCUD(string requetCUD)
        {
            cmd = new SqlCommand(requetCUD, con);
            cmd.ExecuteNonQuery();
        }
        // ----------- Deconnecter
        public void requeteCUDdec(string requete, string table)
        {
            da = new SqlDataAdapter(requete, con);
            da.Fill(ds, table);
        }
        public void ExecuteProcedure(string stored_procedure, SqlParameter[] param)
        {
            SqlCommand sqlcmd = new SqlCommand(stored_procedure, con);
            sqlcmd.CommandType = CommandType.StoredProcedure;
            if (param != null)
            { sqlcmd.Parameters.AddRange(param); }
            sqlcmd.ExecuteNonQuery();
            sqlcmd.CommandType = CommandType.Text;
        }
    }
}
