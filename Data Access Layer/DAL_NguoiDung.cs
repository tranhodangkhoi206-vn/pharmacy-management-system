using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using Data_Transfer_Object___Models;

namespace Data_Access_Layer
{
    public class DAL_NguoiDung
    {
        private SqlCommand command;
        private SqlDataReader reader;
        private DAL_KetNoiCSDL ketNoiCSDL = new DAL_KetNoiCSDL();
        public List<DTO_NguoiDung> DocNguoiDungDAL()
        {
            List<DTO_NguoiDung> bangnguoidung = new List<DTO_NguoiDung>();
            using (SqlConnection conn = ketNoiCSDL.Connection())
            {
                const string layThongTinNhanVien = "SELECT * FROM NGUOIDUNG";
                command = new SqlCommand(layThongTinNhanVien, conn);
                using (reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DTO_NguoiDung nguoidung = new DTO_NguoiDung();
                        nguoidung.MaNguoiDung = reader["MANGUOIDUNG"].ToString().Trim();
                        nguoidung.TenNguoiDung = reader["HOVATEN"].ToString();
                        nguoidung.Tuoi = reader["TUOI"].ToString().Trim();
                        nguoidung.SoDienThoai = reader["SDT"].ToString().Trim();
                        nguoidung.Email = reader["EMAIL"].ToString().Trim();
                        nguoidung.GioiTinh = reader["GIOITINH"].ToString().Trim();
                        nguoidung.NgaySinh = Convert.ToDateTime(reader["NGAYSINH"]);
                        nguoidung.MaChucVu = reader["MACHUCVU"].ToString().Trim();

                        bangnguoidung.Add(nguoidung);
                    }
                }
            }
            return bangnguoidung;
        }
        public List<DTO_NguoiDung> DocTenNguoiDungDAL()
        {
            List<DTO_NguoiDung> bangntenguoidung = new List<DTO_NguoiDung>();
            using (SqlConnection conn = ketNoiCSDL.Connection())
            {
                const string query = "SELECT MANGUOIDUNG, HOVATEN, MACHUCVU FROM NGUOIDUNG";
                command = new SqlCommand(query, conn);
                using (reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DTO_NguoiDung nguoidung = new DTO_NguoiDung();
                        nguoidung.MaNguoiDung = reader["MANGUOIDUNG"].ToString().Trim();
                        nguoidung.TenNguoiDung = reader["HOVATEN"].ToString().Trim();
                        nguoidung.MaChucVu = reader["MACHUCVU"].ToString().Trim();
                        bangntenguoidung.Add(nguoidung);
                    }
                }
            }
            return bangntenguoidung;
        }
        public void ThemNguoiDungDAL(DTO_NguoiDung nguoidung)
        {
            using (SqlConnection conn = ketNoiCSDL.Connection())
            {
                string query = @"INSERT INTO NGUOIDUNG
                    VALUES
                    (@MaNguoiDung, @HoVaTen, @Tuoi, @SDT, @Email, @GioiTinh, @NgaySinh, @MaChucVu)";
                command = new SqlCommand(query, conn);
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@MaNguoiDung", nguoidung.MaNguoiDung);
                command.Parameters.AddWithValue("@HoVaTen", nguoidung.TenNguoiDung);
                command.Parameters.AddWithValue("@Tuoi", nguoidung.Tuoi);
                command.Parameters.AddWithValue("@SDT", nguoidung.SoDienThoai);
                command.Parameters.AddWithValue("@NgaySinh", nguoidung.NgaySinh);
                command.Parameters.AddWithValue("@Email", nguoidung.Email);
                command.Parameters.AddWithValue("@GioiTinh", nguoidung.GioiTinh);
                command.Parameters.AddWithValue("@MaChucVu", nguoidung.MaChucVu);
                command.ExecuteNonQuery();
            }
        }
        public void SuaNguoiDungDAL(DTO_NguoiDung nguoidung)
        {
            using (SqlConnection conn = ketNoiCSDL.Connection())
            {
                string query = @"UPDATE NGUOIDUNG
                    SET HOVATEN = @HoVaTen, TUOI = @Tuoi,SDT= @SDT,EMAIL= @Email,GIOITINH = @GioiTinh,  NGAYSINH = @NgaySinh, MACHUCVU = @MaChucVu
                        WHERE MANGUOIDUNG = @MaNguoiDung";
                command = new SqlCommand(query, conn);
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@MaNguoiDung", nguoidung.MaNguoiDung);
                command.Parameters.AddWithValue("@HoVaTen", nguoidung.TenNguoiDung);
                command.Parameters.AddWithValue("@SDT", nguoidung.SoDienThoai);
                command.Parameters.AddWithValue("@Email", nguoidung.Email);
                command.Parameters.AddWithValue("@GioiTinh", nguoidung.GioiTinh);
                command.Parameters.AddWithValue("@Tuoi", nguoidung.Tuoi);
                command.Parameters.AddWithValue("@NgaySinh", nguoidung.NgaySinh);
                command.Parameters.AddWithValue("@MaChucVu", nguoidung.MaChucVu);
                command.ExecuteNonQuery();
            }
        }
        public void XoaNguoiDungDAL(string manguoidung)
        {
            using (SqlConnection conn = ketNoiCSDL.Connection())
            {
                string query = @"DELETE NGUOIDUNG 
                    WHERE MANGUOIDUNG = @MaNguoiDung";
                command = new SqlCommand(query, conn);
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@MaNguoiDung", manguoidung);
                command.ExecuteNonQuery();
            }
        }
        public void SuaChucVuDAl(string manguoidung, string machucvu)
        {
            using (SqlConnection conn = ketNoiCSDL.Connection())
            {
                string query = @"UPDATE NGUOIDUNG
                    SET MACHUCVU = @MaChucVu
                        WHERE MANGUOIDUNG = @MaNguoiDung";
                command = new SqlCommand(query, conn);
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@MaNguoiDung", manguoidung);
                command.Parameters.AddWithValue("@MaChucVu", machucvu);
                command.ExecuteNonQuery();
            }
        }
    }
}
