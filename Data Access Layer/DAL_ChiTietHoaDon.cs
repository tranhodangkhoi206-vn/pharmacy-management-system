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
    public class DAL_ChiTietHoaDon
    {
        SqlCommand command;
        SqlDataReader reader;
        DAL_KetNoiCSDL ketNoiCSDL = new DAL_KetNoiCSDL();
        public void ThemChiTietHoaDon(List<DTO_ChiTietHoaDon> bangchitiethoadon)
        {
            using (SqlConnection conn = ketNoiCSDL.Connection())
            {
                foreach (DTO_ChiTietHoaDon chiTietHoaDon in bangchitiethoadon)
                {
                    string query = @"
                    INSERT INTO CHITIETHOADON
                    VALUES
                    (@MaHoaDon,  @DonGia, @SoLuong, @MaThuoc)";
                    command = new SqlCommand(query, conn);
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@MaHoaDon", chiTietHoaDon.MaHoaDon);
                    command.Parameters.AddWithValue("@DonGia", chiTietHoaDon.DonGia);
                    command.Parameters.AddWithValue("@SoLuong", chiTietHoaDon.SoLuong);
                    command.Parameters.AddWithValue("@MaThuoc", chiTietHoaDon.MaThuoc);
                    command.ExecuteNonQuery();
                }
            }
        }
        private List<DTO_ChiTietHoaDon> LayChiTietHoaDon()
        {
            List<DTO_ChiTietHoaDon> bangChiTieHoaDon = new List<DTO_ChiTietHoaDon>();
            using (SqlConnection conn = ketNoiCSDL.Connection())
            {
                string query = "SELECT * FROM CHITIETHOADON";
                command = new SqlCommand(query, conn);
                using (reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DTO_ChiTietHoaDon chiTietHoaDon = new DTO_ChiTietHoaDon();
                        chiTietHoaDon.MaHoaDon = reader["MAHOADON"].ToString().Trim();
                        chiTietHoaDon.DonGia = Convert.ToDecimal(reader["DONGIA"]);
                        chiTietHoaDon.SoLuong = Convert.ToInt32(reader["SOLUONG"]);
                        chiTietHoaDon.MaThuoc = reader["MATHUOC"].ToString().Trim();
                        bangChiTieHoaDon.Add(chiTietHoaDon);
                    }
                }
            }
            return bangChiTieHoaDon;
        }
        public List<DTO_ChiTietHoaDon> DuLieuChiTietHoaDon()
        {
            return LayChiTietHoaDon();
        } 
    }
}
