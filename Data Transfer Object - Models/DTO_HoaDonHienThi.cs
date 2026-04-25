using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Transfer_Object___Models
{
    public class DTO_HoaDonHienThi
    {
        public string MaHoaDon { get; set; }
        public string MaNguoiDung { get; set; }
        public string MaThuoc { get; set; }
        public decimal DonGia { get; set; }
        public decimal TongTien { get; set; }
        public DateTime NgayTao { get; set; }
        public int SoLuong { get; set; }
    }
}
