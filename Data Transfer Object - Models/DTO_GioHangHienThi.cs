using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Transfer_Object___Models
{
    public class DTO_GioHangHienThi
    {
        public int SoLuong { get; set; }
        public string MaThuoc { get; set; }
        public string TenThuoc { get; set; }
        public string TenLoai { get; set; }
        public string TenDonViTinh { get; set; }
        public string TenDangBaoChe { get; set; }
        public decimal DonGia { get; set; }
        public DateTime NgayHetHan { get; set; }
    }
}
