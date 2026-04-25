using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Data_Transfer_Object___Models;
using System.Security.Permissions;
namespace Data_Access_Layer
{
    public class DAL_TaiKhoan
    {
        SqlDataReader reader;
        SqlCommand command;
        DAL_KetNoiCSDL ketNoiCSDL = new DAL_KetNoiCSDL();

        public List<DTO_TaiKhoan> DocTaiKhoanDAL()
        {
            List<DTO_TaiKhoan> bangTaiKhoan = new List<DTO_TaiKhoan>();
            using (SqlConnection conn = ketNoiCSDL.Connection())
            {
                const string layTaiKhoan = "SELECT * FROM TAIKHOAN";
                command = new SqlCommand(layTaiKhoan, conn);
                using (reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DTO_TaiKhoan taiKhoan = new DTO_TaiKhoan();
                        taiKhoan.MaNguoiDung = reader["MANGUOIDUNG"].ToString().Trim();
                        taiKhoan.MaChucVu = reader["MACHUCVU"].ToString().Trim();
                        taiKhoan.Username = reader["USERNAME"].ToString().Trim();
                        taiKhoan.MatKhau = reader["MATKHAU"].ToString().Trim();
                        bangTaiKhoan.Add(taiKhoan);
                    }
                }
            }
            return bangTaiKhoan;
        }

        public List<DTO_TaiKhoan> DocTaiKhoanHienThi()
        {
            List<DTO_TaiKhoan> danhSachTaiKhoanHienThi = new List<DTO_TaiKhoan>();
            using (SqlConnection conn = ketNoiCSDL.Connection())
            {
                const string query =
                @"SELECT USERNAME, MATKHAU, MANGUOIDUNG, HOVATEN, TENCHUCVU
                    FROM TAIKHOANVIEW";

                command = new SqlCommand(query, conn);
                using (reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DTO_TaiKhoan taiKhoanHienThi = new DTO_TaiKhoan();
                        taiKhoanHienThi.Username = reader["USERNAME"].ToString().Trim();
                        taiKhoanHienThi.MatKhau = reader["MATKHAU"].ToString().Trim();
                        taiKhoanHienThi.MaNguoiDung = reader["MANGUOIDUNG"].ToString().Trim();
                        taiKhoanHienThi.HoVaTen = reader["HOVATEN"].ToString().Trim();
                        taiKhoanHienThi.TenChucVu = reader["TENCHUCVU"].ToString().Trim();
                        danhSachTaiKhoanHienThi.Add(taiKhoanHienThi);
                    }
                }
            }
            return danhSachTaiKhoanHienThi;
        }
        public void ThemTaiKhoanDAL(DTO_TaiKhoan taiKhoan)
        {
            using (SqlConnection conn = ketNoiCSDL.Connection())
            {
                string themTaiKhoan = @"INSERT INTO TAIKHOAN
                    VALUES
                    (@MaNguoiDung, @MaChucVu, @Username, @MatKhau)";
                command = new SqlCommand(themTaiKhoan, conn);
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@MaNguoiDung", taiKhoan.MaNguoiDung);
                command.Parameters.AddWithValue("@MaChucVu", taiKhoan.MaChucVu);
                command.Parameters.AddWithValue("@Username", taiKhoan.Username);
                command.Parameters.AddWithValue("@MatKhau", taiKhoan.MatKhau);
                command.ExecuteNonQuery();
            }
        }
        public void SuaTaiKhoanDAL(DTO_TaiKhoan taikhoan)
        {
            using (SqlConnection conn = ketNoiCSDL.Connection())
            {
                string suaTaiKhoan = @"UPDATE TAIKHOAN
                    SET MACHUCVU = @MaChucVu, Username = @Username, MATKHAU = @MatKhau
                    WHERE MANGUOIDUNG = @MaNguoiDung";
                command = new SqlCommand(suaTaiKhoan, conn);
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@MaNguoiDung", taikhoan.MaNguoiDung);
                command.Parameters.AddWithValue("@MaChucVu", taikhoan.MaChucVu);
                command.Parameters.AddWithValue("@Username", taikhoan.Username);
                command.Parameters.AddWithValue("@MatKhau", taikhoan.MatKhau);
                command.ExecuteNonQuery();
            }
        }
        public void XoaTaiKhoanDAL(string manguoidung)
        {
            using (SqlConnection conn = ketNoiCSDL.Connection())
            {
                string query = @"DELETE TAIKHOAN WHERE MANGUOIDUNG = @MaNguoiDung";
                command = new SqlCommand(query, conn);
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@MaNguoiDung", manguoidung);
                command.ExecuteNonQuery();
            }
        }
    }
}
