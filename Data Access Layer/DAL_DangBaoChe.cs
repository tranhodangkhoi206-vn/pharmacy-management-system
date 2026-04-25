using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Transfer_Object___Models;

namespace Data_Access_Layer
{
    public class DAL_DangBaoChe
    {
        SqlDataReader reader;
        SqlCommand command;
        DAL_KetNoiCSDL ketNoiCSDL = new DAL_KetNoiCSDL();
        public List<DTO_DangBaoChe> DocDangBaoCheDAl()
        {
            List<DTO_DangBaoChe> bangDangBaoChe = new List<DTO_DangBaoChe>();
            using (SqlConnection conn = ketNoiCSDL.Connection())
            {
                const string layDangBaoChe = "SELECT * FROM DBC";
                command = new SqlCommand(layDangBaoChe, conn);
                using (reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DTO_DangBaoChe dangBaoChe = new DTO_DangBaoChe();
                        dangBaoChe.MaDangBaoChe = reader["MADBC"].ToString().Trim();
                        dangBaoChe.TenDangBaoChe = reader["TENDBC"].ToString();
                        bangDangBaoChe.Add(dangBaoChe);
                    }
                }
            }
            return bangDangBaoChe;
        }
    }
}
