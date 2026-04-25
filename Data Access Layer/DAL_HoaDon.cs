using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Data_Transfer_Object___Models;

namespace Data_Access_Layer
{
    public class DAL_HoaDon
    {
        SqlCommand command;
        SqlDataReader reader;
        DAL_KetNoiCSDL ketNoiCSDL = new DAL_KetNoiCSDL();
        public void ThemHoaDonDAL(DTO_HoaDon hoadon)
        {
            using (SqlConnection conn = ketNoiCSDL.Connection())
            {
                string query = @"
                    INSERT INTO HOADON
                    VALUES
                    (@MaHoaDon, @MaNguoiDung, @NgayTao, @TongTien);";
                command = new SqlCommand(query, conn);
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@MaHoaDon", hoadon.MaHoaDon);
                command.Parameters.AddWithValue("@MaNguoiDung", hoadon.MaNguoiDung);
                command.Parameters.AddWithValue("@NgayTao", hoadon.NgayTao);
                command.Parameters.AddWithValue("@TongTien", hoadon.TongTien);
                command.ExecuteNonQuery();
            }
        }

        public List<DTO_HoaDon> DocHoaDonDAL()
        {
            List<DTO_HoaDon> bangHoaDon = new List<DTO_HoaDon>();
            using (SqlConnection conn = ketNoiCSDL.Connection())
            {
                string layKhachHang = "SELECT * FROM HOADON";
                command = new SqlCommand(layKhachHang, conn);
                using (reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DTO_HoaDon hoaDon = new DTO_HoaDon();
                        hoaDon.MaHoaDon = reader["MAHOADON"].ToString().Trim();
                        hoaDon.MaNguoiDung = reader["MaNGUOIDUNG"].ToString().Trim();
                        hoaDon.NgayTao = Convert.ToDateTime(reader["NGAYTAO"]);
                        hoaDon.TongTien = Convert.ToDecimal(reader["TONGTIEN"]);
                        bangHoaDon.Add(hoaDon);
                    }
                }
            }
            return bangHoaDon;
        }
        public List<DTO_HoaDon> DocMaHoaDon()
        {
            List<DTO_HoaDon> danhSachMaHoaDon = new List<DTO_HoaDon>();
            using (SqlConnection conn = ketNoiCSDL.Connection())
            {
                string query = "SELECT MAHOADON FROM HOADON";
                command = new SqlCommand(query, conn);
                using (reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DTO_HoaDon maHoaDon = new DTO_HoaDon();
                        maHoaDon.MaHoaDon = reader["MAHOADON"].ToString().Trim();
                        danhSachMaHoaDon.Add(maHoaDon);
                    }
                }
            }
            return danhSachMaHoaDon;
        }
    }
}
