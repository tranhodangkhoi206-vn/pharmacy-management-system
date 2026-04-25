using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Management.Instrumentation;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using Data_Transfer_Object___Models;

namespace Data_Access_Layer
{
    public class DAL_Thuoc
    {
        SqlDataReader reader;
        SqlCommand command;
        DAL_KetNoiCSDL ketNoiCSDL = new DAL_KetNoiCSDL();

        public void ThemThuocDAL(DTO_Thuoc thuoc)
        {
            using (SqlConnection conn = ketNoiCSDL.Connection())
            {
                string query = @"
                    INSERT INTO THUOC
                    VALUES
                    (@MaThuoc, @TenThuoc, @MaDVT, @MaDBC, @DonGia, @NgayHetHan, @MaLoai, @MaNhaCungCap, @SoLuongTon)
                    ";
                command = new SqlCommand(query, conn);
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@MaThuoc", thuoc.MaThuoc);
                command.Parameters.AddWithValue("@TenThuoc", thuoc.TenThuoc);
                command.Parameters.AddWithValue("@MaDVT", thuoc.MaDonViTinh);
                command.Parameters.AddWithValue("@MaDBC", thuoc.MaDangBaoChe);
                command.Parameters.AddWithValue("@DonGia", thuoc.DonGia);
                command.Parameters.AddWithValue("@NgayHetHan", thuoc.NgayHetHan);
                command.Parameters.AddWithValue("@MaLoai", thuoc.MaLoai);
                command.Parameters.AddWithValue("@MaNhaCungCap", thuoc.MaNhaCungCap);
                command.Parameters.AddWithValue("@SoLuongTon", thuoc.SoLuongTon);
                command.ExecuteNonQuery();
            }
        }

        public void SuaThuocDAL(DTO_Thuoc thuoc)
        {
            using (SqlConnection conn = ketNoiCSDL.Connection())
            {
                string suaThuoc = @"
                    UPDATE THUOC
                    SET TENTHUOC = @TenThuoc, 
                        MADVT = @MaDVT, 
                        MADBC = @MaDBC,
                        DONGIA = @DonGia,
                        NGAYHETHAN = @NgayHetHan, 
                        MALOAI = @MaLoai, 
                        MANHACUNGCAP = @MaNhaCungCap,
                        SOLUONGTON  = @SoLuongTon
                    WHERE MATHUOC = @MaThuoc
                    ";
                command = new SqlCommand(suaThuoc, conn);
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@MaThuoc", thuoc.MaThuoc);
                command.Parameters.AddWithValue("@TenThuoc", thuoc.TenThuoc);
                command.Parameters.AddWithValue("@MaDVT", thuoc.MaDonViTinh);
                command.Parameters.AddWithValue("@MaDBC", thuoc.MaDangBaoChe);
                command.Parameters.AddWithValue("@DonGia", thuoc.DonGia);
                command.Parameters.AddWithValue("@NgayHetHan", thuoc.NgayHetHan);
                command.Parameters.AddWithValue("@MaLoai", thuoc.MaLoai);
                command.Parameters.AddWithValue("@MaNhaCungCap", thuoc.MaNhaCungCap);
                command.Parameters.AddWithValue("@SoLuongTon", thuoc.SoLuongTon);
                command.ExecuteNonQuery();

            }
        }

        public void XoaThuocDAL(string mathuoc)
        {
            using (SqlConnection conn = ketNoiCSDL.Connection())
            {
                string query = @"
                    DELETE THUOC
                    WHERE MATHUOC = @MaThuoc";
                command = new SqlCommand(query, conn);
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@MaThuoc", mathuoc);
                command.ExecuteNonQuery();
            }
        }
        public List<DTO_Thuoc> DocThuocDAL()
        {
            List<DTO_Thuoc> bangThuoc = new List<DTO_Thuoc>();
            using (SqlConnection conn = ketNoiCSDL.Connection())
            {
                const string layThuoc = "SELECT * FROM THUOC";
                command = new SqlCommand(layThuoc, conn);
                using (reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DTO_Thuoc thuoc = new DTO_Thuoc();
                        thuoc.MaThuoc = reader["MATHUOC"].ToString().Trim();
                        thuoc.TenThuoc = reader["TENTHUOC"].ToString();
                        thuoc.MaDonViTinh = reader["MADVT"].ToString().Trim();
                        thuoc.MaDangBaoChe = reader["MADBC"].ToString().Trim();
                        thuoc.DonGia = Convert.ToDecimal(reader["DONGIA"]);
                        thuoc.NgayHetHan = Convert.ToDateTime(reader["NGAYHETHAN"]);
                        thuoc.MaLoai = reader["MALOAI"].ToString().Trim();
                        thuoc.MaNhaCungCap = reader["MANHACUNGCAP"].ToString().Trim();
                        thuoc.SoLuongTon = (int)reader["SOLUONGTON"];
                        //thuoc.TenThuoc = reader["TENTHUOC"].ToString().Trim();
                        //thuoc.TenDonViTinh = reader["TENDVT"].ToString().Trim();
                        //thuoc.TenDangBaoChe = reader["TENDBC"].ToString().Trim();
                        //thuoc.TenLoai = reader["TENLOAI"].ToString().Trim();
                        //thuoc.TenNhaCungCap = reader["TENNHACUNGCAP"].ToString().Trim();
                        bangThuoc.Add(thuoc);
                    }
                }
            }
            return bangThuoc;
        }
        public List<DTO_NgayHetHanThuoc> DocNgayHetHanThuocDAL()
        {
            List<DTO_NgayHetHanThuoc> bangNgayHetHan = new List<DTO_NgayHetHanThuoc>();
            using (SqlConnection conn = ketNoiCSDL.Connection())
            {
                string layNgayHetHan = "SELECT MATHUOC, NGAYHETHAN FROM THUOC";
                command = new SqlCommand(layNgayHetHan, conn);
                using (reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DTO_NgayHetHanThuoc ngayHetHan = new DTO_NgayHetHanThuoc();
                        ngayHetHan.MaThuoc = reader["MATHUOC"].ToString().Trim();
                        ngayHetHan.NgayHetHan = reader["NGAYHETHAN"].ToString().Trim();
                        bangNgayHetHan.Add(ngayHetHan);
                    }
                }
            }
            return bangNgayHetHan;
        }

        public List<DTO_ThuocCoBan> DocThuocCoBanDAL()
        {
            List<DTO_ThuocCoBan> danhSachThuocCoBan = new List<DTO_ThuocCoBan>();
            using (SqlConnection conn = ketNoiCSDL.Connection())
            {
                string query = "SELECT MATHUOC, TENTHUOC FROM THUOC";
                command = new SqlCommand(query, conn);
                using (reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DTO_ThuocCoBan thuocCoBan = new DTO_ThuocCoBan();
                        thuocCoBan.MaThuoc = reader["MATHUOC"].ToString().Trim();
                        thuocCoBan.TenThuoc = reader["TENTHUOC"].ToString().Trim();
                        danhSachThuocCoBan.Add(thuocCoBan);
                    }
                }
            }
            return danhSachThuocCoBan;
        }
        public List<DTO_GioHangHienThi> DocThuocGioHangDAL()
        {
            List<DTO_GioHangHienThi> danhSachThuocGioHang = new List<DTO_GioHangHienThi>();
            using (SqlConnection conn = ketNoiCSDL.Connection())
            {
                string query = "SELECT * FROM THUOCVIEW";
                command = new SqlCommand(query, conn);
                using (reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DTO_GioHangHienThi thuocGiohang = new DTO_GioHangHienThi();
                        thuocGiohang.MaThuoc = reader["MATHUOC"].ToString().Trim();
                        thuocGiohang.TenThuoc = reader["TENTHUOC"].ToString().Trim();
                        thuocGiohang.TenDonViTinh = reader["TENDVT"].ToString().Trim();
                        thuocGiohang.TenDangBaoChe = reader["TENDBC"].ToString().Trim();
                        thuocGiohang.TenLoai = reader["TENLOAI"].ToString().Trim();
                        thuocGiohang.DonGia = Convert.ToDecimal(reader["DONGIA"]);
                        thuocGiohang.NgayHetHan = Convert.ToDateTime(reader["NGAYHETHAN"]);
                        danhSachThuocGioHang.Add(thuocGiohang);
                    }
                }
            }
            return danhSachThuocGioHang;
        }
        public List<DTO_Thuoc> DocThuocHienThiDAL()
        {
            List<DTO_Thuoc> bangThuoc = new List<DTO_Thuoc>();
            using (SqlConnection conn = ketNoiCSDL.Connection())
            {
                const string query = "SELECT * FROM THUOCVIEW";
                command = new SqlCommand(query, conn);
                using (reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DTO_Thuoc thuoc = new DTO_Thuoc();
                        thuoc.MaThuoc = reader["MATHUOC"].ToString().Trim();
                        thuoc.TenThuoc = reader["TENTHUOC"].ToString();
                        thuoc.MaDonViTinh = reader["MADVT"].ToString().Trim();
                        thuoc.MaDangBaoChe = reader["MADBC"].ToString().Trim();
                        thuoc.DonGia = Convert.ToDecimal(reader["DONGIA"]);
                        thuoc.NgayHetHan = Convert.ToDateTime(reader["NGAYHETHAN"]);
                        thuoc.MaLoai = reader["MALOAI"].ToString().Trim();
                        thuoc.MaNhaCungCap = reader["MANHACUNGCAP"].ToString().Trim();
                        thuoc.SoLuongTon = (int)reader["SOLUONGTON"];
                        thuoc.TenThuoc = reader["TENTHUOC"].ToString().Trim();
                        thuoc.TenDonViTinh = reader["TENDVT"].ToString().Trim();
                        thuoc.TenDangBaoChe = reader["TENDBC"].ToString().Trim();
                        thuoc.TenLoai = reader["TENLOAI"].ToString().Trim();
                        thuoc.TenNhaCungCap = reader["TENNHACUNGCAP"].ToString().Trim();
                        bangThuoc.Add(thuoc);
                    }
                }
            }
            return bangThuoc;
        }
        public void CapNhatSoLuongThuocDAL(string mathuoc, int soluong)
        {
            using (SqlConnection conn = ketNoiCSDL.Connection())
            {
                string capNhatSoLuong = @"UPDATE THUOC
                            SET SOLUONGTON = @SoLuongTon
                            WHERE MATHUOC = @MaThuoc";
                command = new SqlCommand(capNhatSoLuong, conn);
                command.Parameters.Clear();

                command.Parameters.AddWithValue("@MaThuoc", mathuoc);
                command.Parameters.AddWithValue("@SoLuongTon", soluong);
                command.ExecuteNonQuery();
            }
        }
        public void ThemThuocMoi(DTO_Thuoc thuoc)
        {
            using (SqlConnection conn = ketNoiCSDL.Connection())
            {
                string query = @"
                    INSERT INTO THUOC
                    VALUES
                    (@MaThuoc, @TenThuoc, @MaDVT, @MaDBC, @DonGia, @NgayHetHan, @MaLoai, @MaNhaCungCap, @SoLuongTon)
                    ";
                command = new SqlCommand(query, conn);
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@MaThuoc", thuoc.MaThuoc);
                command.Parameters.AddWithValue("@TenThuoc", thuoc.TenThuoc);
                command.Parameters.AddWithValue("@MaDVT", thuoc.MaDonViTinh);
                command.Parameters.AddWithValue("@MaDBC", thuoc.MaDangBaoChe);
                command.Parameters.AddWithValue("@DonGia", thuoc.DonGia);
                command.Parameters.AddWithValue("@NgayHetHan", thuoc.NgayHetHan);
                command.Parameters.AddWithValue("@MaLoai", thuoc.MaLoai);
                command.Parameters.AddWithValue("@MaNhaCungCap", thuoc.MaNhaCungCap);
                command.Parameters.AddWithValue("@SoLuongTon", thuoc.SoLuongTon);
                command.ExecuteNonQuery();
            }
        }
    }
}
