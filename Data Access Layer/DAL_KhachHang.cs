using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.Security;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Data_Transfer_Object___Models;
using Microsoft.SqlServer.Server;

namespace Data_Access_Layer
{
    public class DAL_KhachHang
    {
        private SqlDataReader reader;
        private SqlCommand command;
        private DAL_KetNoiCSDL ketNoiCSDL = new DAL_KetNoiCSDL();
        private DTO_KhachHang khachHang;

        // -- Lấy khách hàng từ CSDL -- //
        public List<DTO_KhachHang> DocKhachHangDAL()
        {
            List<DTO_KhachHang> bangKhachHang = new List<DTO_KhachHang>();
            using (SqlConnection conn = ketNoiCSDL.Connection())
            {
                string layKhachHang = "SELECT * FROM KHACHHANG";
                command = new SqlCommand(layKhachHang, conn);
                using (reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DTO_KhachHang khachHang = new DTO_KhachHang();
                        khachHang.MaKhachHang = reader["MAKHACHHANG"].ToString().Trim();
                        khachHang.HoVaTen = reader["HOVATEN"].ToString().Trim();
                        khachHang.Tuoi = reader["TUOI"].ToString().Trim();
                        khachHang.GioiTinh = reader["GIOITINH"].ToString().Trim();
                        khachHang.SoTheBHYT = reader["SOTHEBHYT"].ToString().Trim();
                        khachHang.SoCMND = reader["SOCMND"].ToString().Trim();
                        khachHang.DiaChi = reader["DIACHI"].ToString().Trim();
                        khachHang.NgaySinh = Convert.ToDateTime(reader["NGAYSINH"]);
                        bangKhachHang.Add(khachHang);
                    }
                }
            }
            return bangKhachHang;
        }

        public void ThemKhachHangDAL(DTO_KhachHang khachhang)
        {
            using (SqlConnection conn = ketNoiCSDL.Connection())
            {
                string themKhachHang = @"
                    INSERT INTO KHACHHANG 
                    VALUES
                    (@MaKhachHang, @HoVaTen, @Tuoi, @GioiTinh, @SoTheBHYT, @SoCMND, @DiaChi, @NgaySinh)";
                command = new SqlCommand(themKhachHang, conn);
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@MaKhachHang", khachhang.MaKhachHang);
                command.Parameters.AddWithValue("@HoVaTen", khachhang.HoVaTen);
                command.Parameters.AddWithValue("@Tuoi", khachhang.Tuoi);
                command.Parameters.AddWithValue("@GioiTinh", khachhang.GioiTinh);
                command.Parameters.AddWithValue("@SoTheBHYT", khachhang.SoTheBHYT);
                command.Parameters.AddWithValue("@SoCMND", khachhang.SoCMND);
                command.Parameters.AddWithValue("@DiaChi", khachhang.DiaChi);
                command.Parameters.AddWithValue("@NgaySinh", khachhang.NgaySinh);
                command.ExecuteNonQuery();
            }
        }
        public void XoaKhachHangDAL(string makhachhang)
        {
            using (SqlConnection conn = ketNoiCSDL.Connection())
            {
                string xoaKhachHang = @"
                    DELETE KHACHHANG
                    WHERE MAKHACHHANG = @MaKhachHang";
                command = new SqlCommand(xoaKhachHang, conn);
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@MaKhachHang", makhachhang);
                command.ExecuteNonQuery();
            }
        }
        public void SuaKhachHangDAL(DTO_KhachHang khachhang)
        {
            using (SqlConnection conn = ketNoiCSDL.Connection())
            {
                string suaKhachHang = @"
                    UPDATE KHACHHANG
                    SET MAKHACHHANG = @MaKhachHang,
                    HOVATEN = @HoVaTen, TUOI = @Tuoi, GIOITINH = @GioiTinh, SOTHEBHYT = @SoTheBHYT, SOCMND = @SoCMND, DIACHI = @DiaChi, NGAYSINH = @NgaySinh
                    WHERE MAKHACHHANG = @MaKhachHang";
                command.Parameters.Clear();
                command = new SqlCommand(suaKhachHang, conn);
                command.Parameters.AddWithValue("@MaKhachHang", khachhang.MaKhachHang);
                command.Parameters.AddWithValue("@HoVaTen", khachhang.HoVaTen);
                command.Parameters.AddWithValue("@Tuoi", khachhang.Tuoi);
                command.Parameters.AddWithValue("@GioiTinh", khachhang.GioiTinh);
                command.Parameters.AddWithValue("@SoTheBHYT", khachhang.SoTheBHYT);
                command.Parameters.AddWithValue("@SoCMND", khachhang.SoCMND);
                command.Parameters.AddWithValue("@DiaChi", khachhang.DiaChi);
                command.Parameters.AddWithValue("@NgaySinh", khachhang.NgaySinh);
                command.ExecuteNonQuery();
            }
        }
        public List<DTO_KhachHang> DocMaKhachHang()
        {
            List<DTO_KhachHang> danhSachMaKHachHang = new List<DTO_KhachHang>();
            using (SqlConnection conn = ketNoiCSDL.Connection())
            {
                string query = "SELECT MAKHACHHANG FROM KHACHHANG";
                command = new SqlCommand(query, conn);
                using (reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DTO_KhachHang maKhachHang = new DTO_KhachHang();
                        maKhachHang.MaKhachHang = reader["MAKHACHHANG"].ToString().Trim();
                        danhSachMaKHachHang.Add(maKhachHang);
                    }
                }
            }
            return danhSachMaKHachHang;
        }
    }
}
