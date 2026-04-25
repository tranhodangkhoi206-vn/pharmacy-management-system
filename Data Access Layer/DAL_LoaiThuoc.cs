using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Transfer_Object___Models;

namespace Data_Access_Layer
{
    public class DAL_LoaiThuoc
    {
        SqlDataReader reader;
        SqlCommand command;
        DAL_KetNoiCSDL ketNoiCSDL = new DAL_KetNoiCSDL();

        public List<DTO_LoaiThuoc> DocLoaiThuocDAL()
        {
            List<DTO_LoaiThuoc> bangLoaiThuoc = new List<DTO_LoaiThuoc>();
            using (SqlConnection conn = ketNoiCSDL.Connection())
            {
                const string layLoaiThuoc = "SELECT * FROM LOAITHUOC";
                command = new SqlCommand(layLoaiThuoc, conn);
                using (reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DTO_LoaiThuoc loaiThuoc = new DTO_LoaiThuoc();
                        loaiThuoc.MaLoai = reader["MALOAI"].ToString().Trim();
                        loaiThuoc.TenLoai = reader["TENLOAI"].ToString();

                        bangLoaiThuoc.Add(loaiThuoc);
                    }
                }
            }
            return bangLoaiThuoc;
        }
    }
}
