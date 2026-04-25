using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Transfer_Object___Models;

namespace Data_Access_Layer
{
    public class DAL_ChucVu
    {
        SqlCommand command;
        SqlDataReader reader;
        DAL_KetNoiCSDL ketNoiCSDL = new DAL_KetNoiCSDL();
        public List<DTO_ChucVu> DocChucVuDAL()
        {
            List<DTO_ChucVu> bangChucVu = new List<DTO_ChucVu>();
            using (SqlConnection conn = ketNoiCSDL.Connection())
            {
                string layChucVu = "SELECT * FROM CHUCVU";
                command = new SqlCommand(layChucVu, conn);
                using (reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DTO_ChucVu chucVu = new DTO_ChucVu();
                        chucVu.MaChucVu = reader["MACHUCVU"].ToString();
                        chucVu.TenChucVu = reader["TENCHUCVU"].ToString();
                        bangChucVu.Add(chucVu);
                    }
                }
            }
            return bangChucVu;
        }
    }
}
