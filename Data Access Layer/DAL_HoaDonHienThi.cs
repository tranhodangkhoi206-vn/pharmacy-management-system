using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Transfer_Object___Models;

namespace Data_Access_Layer
{
    public class DAL_HoaDonHienThi
    {
        SqlCommand command;
        SqlDataReader reader;
        DAL_KetNoiCSDL ketNoiCSDL = new DAL_KetNoiCSDL();
        public List<DTO_HoaDonHienThi> DocHoaDonHienThi()
        {
            List<DTO_HoaDonHienThi> danhSachHoaDonHienThi = new List<DTO_HoaDonHienThi>();
            using (SqlConnection conn = ketNoiCSDL.Connection())
            {
                string query = "SELECT * FROM HOADONVIEW";
                command = new SqlCommand(query, conn);
                using (reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DTO_HoaDonHienThi hoaDonHienThi = new DTO_HoaDonHienThi();
                        hoaDonHienThi.MaHoaDon = reader["MAHOADON"].ToString().Trim();
                        hoaDonHienThi.MaNguoiDung = reader["MANGUOIDUNG"].ToString().Trim();
                        hoaDonHienThi.MaThuoc = reader["MATHUOC"].ToString().Trim();
                        hoaDonHienThi.DonGia = Convert.ToDecimal(reader["DONGIA"]);
                        hoaDonHienThi.SoLuong = Convert.ToInt32(reader["SOLUONG"]);
                        hoaDonHienThi.NgayTao = Convert.ToDateTime(reader["NGAYTAO"]);
                        hoaDonHienThi.TongTien = Convert.ToDecimal(reader["TONGTIEN"]);
                        danhSachHoaDonHienThi.Add(hoaDonHienThi);
                    }
                }
            }
            return danhSachHoaDonHienThi;
        }
    }
}
