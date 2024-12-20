using ledReport.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ledReport.Class
{
    class CSQL
    {
        private siixsem_led_rpt_dbEntities m_db;
        SqlConnection sqlCon;
        String SqlconString;
        public CSQL()
        {
            m_db = new siixsem_led_rpt_dbEntities();
            sqlCon = null;
            SqlconString = ConfigurationManager.ConnectionStrings["led_report"].ConnectionString;
        }
        public bool getMonthlyDetail(ref DataTable detail)
        {
            bool result = false;

            using (sqlCon = new SqlConnection(SqlconString))
            {
                sqlCon.Open();
                SqlCommand sql_cmnd = new SqlCommand("getDetail", sqlCon);
                sql_cmnd.CommandType = CommandType.StoredProcedure;
                SqlDataReader r = sql_cmnd.ExecuteReader();

                if (r.HasRows)
                {
                    detail.Clear();
                    detail.Load(r);
                    result = true;
                }
                else result = false;
                sqlCon.Close();
            }

            return result;
        }

        public bool getYesterdayDetail(ref DataTable yesterday)
        {
            bool result = false;

            using (sqlCon = new SqlConnection(SqlconString))
            {
                sqlCon.Open();
                SqlCommand sql_cmnd = new SqlCommand("getLastDay", sqlCon);
                sql_cmnd.CommandType = CommandType.StoredProcedure;
                SqlDataReader r = sql_cmnd.ExecuteReader();

                if (r.HasRows)
                {
                    yesterday.Clear();
                    yesterday.Load(r);
                    result = true;
                }
                else result = false;
                sqlCon.Close();
            }

            return result;
        }
    }
}
