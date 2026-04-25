using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Data_Access_Layer
{
    public class DAL_KetNoiCSDL
    {
        //internal DataSet dataSet = new DataSet();
        public SqlConnection Connection()
        {
            const string connectionString = "Data Source=DESKTOP-GKAL02I\\SQLEXPRESS;Initial Catalog=DUAN_QLTHUOC_DOTNET;Integrated Security=True;";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            return conn;
        }
    }
}
