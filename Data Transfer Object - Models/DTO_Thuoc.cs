using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Transfer_Object___Models
{
    public class DTO_Thuoc
    {

        public string MaThuoc { get; set; }
        public string TenThuoc { get; set; }
        public string TenDonViTinh { get; set; }
        public string TenDangBaoChe { get; set; }
        public string TenLoai { get; set; }
        public string TenNhaCungCap { get; set; }
        public decimal DonGia { get; set; }
        public DateTime NgayHetHan { get; set; }
        public string MaNhaCungCap { get; set; }
        public int SoLuongTon { get; set; }

        public string MaLoai { get; set; }
        public string MaDonViTinh { get; set; }
        public string MaDangBaoChe { get; set; }
        
    }
}
