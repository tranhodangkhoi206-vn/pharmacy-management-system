using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Transfer_Object___Models;

namespace Data_Access_Layer
{
    public class DAL_DonViTinh
    {
        SqlDataReader reader;
        SqlCommand command;
        DAL_KetNoiCSDL ketNoiCSDL = new DAL_KetNoiCSDL();
        public List<DTO_DonViTinh> DocDonViTinhDAL()
        {
            List<DTO_DonViTinh> bangDonViTinh = new List<DTO_DonViTinh>();
            using (SqlConnection conn = ketNoiCSDL.Connection())
            {
                const string layDonViTinh = "SELECT * FROM DVT";
                command = new SqlCommand(layDonViTinh, conn);
                using (reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DTO_DonViTinh donViTinh = new DTO_DonViTinh();
                        donViTinh.MaDonViTinh = reader["MADVT"].ToString().Trim();
                        donViTinh.TenDonViTinh = reader["TENDVT"].ToString();

                        bangDonViTinh.Add(donViTinh);
                    }
                }
            }
            return bangDonViTinh;
        }
    }
}
