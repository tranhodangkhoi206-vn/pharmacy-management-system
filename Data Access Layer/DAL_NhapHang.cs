using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using Data_Transfer_Object___Models;

namespace Data_Access_Layer
{
    public class DAL_NhapHang
    {
        SqlCommand command;
        SqlDataReader reader;
        DAL_KetNoiCSDL ketNoiCSDL = new DAL_KetNoiCSDL();
        public List<DTO_NhapHang> DocNhapHangDAL()
        {
            List<DTO_NhapHang> bangNhapHang = new List<DTO_NhapHang>();
            using (SqlConnection conn = ketNoiCSDL.Connection())
            {
                string layNhaphang = "SELECT * FROM NHAPHANG";
                command = new SqlCommand(layNhaphang, conn);
                using (reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DTO_NhapHang nhapHang = new DTO_NhapHang();
                        nhapHang.MaLoHang = reader["MALOHANG"].ToString().Trim();
                        nhapHang.MaNhaCungCap = reader["MANHACUNGCAP"].ToString().Trim();
                        nhapHang.MaThuoc = reader["MATHUOC"].ToString().Trim();
                        nhapHang.MaNguoiDung = reader["MANGUOIDUNG"].ToString().Trim();
                        nhapHang.SoLuongNhap = (int)reader["SLNHAP"];
                        nhapHang.NgayNhap = Convert.ToDateTime(reader["NGAYNHAP"]);
                        bangNhapHang.Add(nhapHang);
                    }
                }
            }
            return bangNhapHang;
        }


        public void ThemNhapHangDAL(DTO_NhapHang nhaphang)
        {
            using (SqlConnection conn = ketNoiCSDL.Connection())
            {
                string layNhapHang = @"INSERT INTO NHAPHANG 
                        VALUES (@MaLoHang, @MaNCC, @MaThuoc, @MaND, @SoLuong, @NgayNhap)";
                command = new SqlCommand(layNhapHang, conn);
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@MaLoHang", nhaphang.MaLoHang);
                command.Parameters.AddWithValue("@MaNCC", nhaphang.MaNhaCungCap);
                command.Parameters.AddWithValue("@MaThuoc", nhaphang.MaThuoc);
                command.Parameters.AddWithValue("@MaND", nhaphang.MaNguoiDung);
                command.Parameters.AddWithValue("@SoLuong", nhaphang.SoLuongNhap);
                command.Parameters.AddWithValue("@NgayNhap", nhaphang.NgayNhap);
                command.ExecuteNonQuery();
            }
        }

    }
}
