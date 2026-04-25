using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Data_Transfer_Object___Models;

namespace Data_Access_Layer
{
    public class DAL_NhaCungCap
    {

        SqlCommand command;
        SqlDataReader reader;
        DAL_KetNoiCSDL ketNoiCSDL = new DAL_KetNoiCSDL();
        DTO_NhaCungCap nhaCungCap;
        public List<DTO_NhaCungCap> DocNhaCungCapDAL()
        {
            List<DTO_NhaCungCap> bangNhaCungCap = new List<DTO_NhaCungCap>();
            using (SqlConnection conn = ketNoiCSDL.Connection())
            {
                const string layNhaCungCap = "SELECT * FROM NHACUNGCAP";
                command = new SqlCommand(layNhaCungCap, conn);
                using (reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DTO_NhaCungCap nhaCungCap = new DTO_NhaCungCap();
                        nhaCungCap.MaNhaCungCap = reader["MANHACUNGCAP"].ToString().Trim();
                        nhaCungCap.TenNhaCungCap = reader["TENNHACUNGCAP"].ToString();
                        nhaCungCap.TrangThai = reader["TRANGTHAI"].ToString();
                        nhaCungCap.NgayHopTac = Convert.ToDateTime(reader["NGAYHOPTAC"]);
                        bangNhaCungCap.Add(nhaCungCap);
                    }

                }
            }
            return bangNhaCungCap;
        }
        public void ThemNhaCungCapDAL(DTO_NhaCungCap nhacungcap)
        {
            using (SqlConnection conn = ketNoiCSDL.Connection())
            {
                string themNhaCungCap = @"INSERT INTO NHACUNGCAP(MANHACUNGCAP, TENNHACUNGCAP,TRANGTHAI,NGAYHOPTAC)
                    VALUES 
                    (@MaNhaCungCap, @TenNhaCungCap, @TrangThai, @NgayHopTac)";
                command = new SqlCommand(themNhaCungCap, conn);
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@MaNhaCungCap", nhacungcap.MaNhaCungCap);
                command.Parameters.AddWithValue("@TenNhaCungCap", nhacungcap.TenNhaCungCap);
                command.Parameters.AddWithValue("@TrangThai", nhacungcap.TrangThai);
                command.Parameters.AddWithValue("@NgayHopTac", nhacungcap.NgayHopTac);
                command.ExecuteNonQuery();
            }
        }
        public void SuaNhaCungCapDAL(DTO_NhaCungCap nhacungcap)
        {
            using (SqlConnection conn = ketNoiCSDL.Connection())
            {
                string suaNhaCungCap = @"UPDATE NHACUNGCAP
                    SET TENNHACUNGCAP = @TenNhaCungCap, TRANGTHAI = @TrangThai, NGAYHOPTAC = @NgayHopTac
                    WHERE MANHACUNGCAP = @MaNhaCungCap";
                command = new SqlCommand(suaNhaCungCap, conn);
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@MaNhaCungCap", nhacungcap.MaNhaCungCap);
                command.Parameters.AddWithValue("@TenNhaCungCap", nhacungcap.TenNhaCungCap);
                command.Parameters.AddWithValue("@TrangThai", nhacungcap.TrangThai);
                command.Parameters.AddWithValue("@NgayHopTac", nhacungcap.NgayHopTac);
                command.ExecuteNonQuery();
            }
        }
        public void XoaNhaCungCap(string manhacungcap)
        {
            using (SqlConnection conn = ketNoiCSDL.Connection())
            {
                string xoaNhaCungCap = @"DELETE NHACUNGCAP
                    WHERE MANHACUNGCAP = @MaNhaCungCap";
                command = new SqlCommand(xoaNhaCungCap, conn);
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@MaNhaCungCap", manhacungcap);
                command.ExecuteNonQuery();
            }
        }
        public List<DTO_NhaCungCapCoBan> DocNhaCungCaoCoBanDAL()
        {
            List<DTO_NhaCungCapCoBan> danhSachNhaCungCapCoBan = new List<DTO_NhaCungCapCoBan>();
            using (SqlConnection conn = ketNoiCSDL.Connection())
            {
                const string query = "SELECT MANHACUNGCAP, TENNHACUNGCAP FROM NHACUNGCAP";
                command = new SqlCommand(query, conn);
                using (reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DTO_NhaCungCapCoBan nhaCungCapCoBan = new DTO_NhaCungCapCoBan();
                        nhaCungCapCoBan.MaNhaCungCap = reader["MANHACUNGCAP"].ToString().Trim();
                        nhaCungCapCoBan.TenNhaCungCap = reader["TENNHACUNGCAP"].ToString().Trim();
                        danhSachNhaCungCapCoBan.Add(nhaCungCapCoBan);
                    }
                }
            }
            return danhSachNhaCungCapCoBan;
        }
    }
}
