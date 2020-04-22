using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anda.Fluid.Domain.Data
{
    public class MySQLDBHelp
    {
        //private string connStr = ConfigurationManager.ConnectionStrings["strCon"].ConnectionString; 
        private string connStr = "server=localhost;port=3306;user id=root;password=anda;database=mydb";

        public MySqlConnection getMySqlCon()
        {
            string M_str_sqlcon = connStr;
            MySqlConnection myCon = new MySqlConnection(M_str_sqlcon);
            return myCon;
        }
        public bool MySqlOpen()
        {
            try
            {
                string M_str_sqlcon = connStr;
                MySqlConnection myCon = new MySqlConnection(M_str_sqlcon);
                myCon.Open();
                return true;
            }
            catch
            {
                return false;
            }
            
        }
        public int getMySqlCom(string M_str_sqlstr, params MySqlParameter[] parameters)
        {
            MySqlConnection mysqlcon = this.getMySqlCon();
            mysqlcon.Open();
            MySqlCommand mysqlcom = new MySqlCommand(M_str_sqlstr, mysqlcon);
            mysqlcom.Parameters.AddRange(parameters);
            int count = mysqlcom.ExecuteNonQuery();
            mysqlcom.Dispose();
            mysqlcon.Close();
            mysqlcon.Dispose();
            return count;
        }
        public DataTable getMySqlRead(string M_str_sqlstr, params MySqlParameter[] parameters)
        {
            MySqlConnection mysqlcon = this.getMySqlCon();
            mysqlcon.Open();
            MySqlCommand mysqlcom = new MySqlCommand(M_str_sqlstr, mysqlcon);
            mysqlcom.Parameters.AddRange(parameters);
            MySqlDataAdapter mda = new MySqlDataAdapter(mysqlcom);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mysqlcon.Close();
            return dt;
        }

        public DataTable GetTable(string sqlQuery, string tableName)
        {
            DataSet ds=null;
            DataTable dt=null;
            try
            {
                MySqlConnection mysqlcon = this.getMySqlCon();
                mysqlcon.Open();
                MySqlDataAdapter mda = new MySqlDataAdapter(sqlQuery, connStr);
                ds = new DataSet();
                mda.Fill(ds, tableName);
                dt = new DataTable();
                dt = ds.Tables[tableName];
                ds.Dispose();
                dt.Dispose();
                mda.Dispose();
                mysqlcon.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return dt;

        }


    }

}
