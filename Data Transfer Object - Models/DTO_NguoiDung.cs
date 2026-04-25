using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Transfer_Object___Models
{
    public class DTO_NguoiDung
    {
        public string MaNguoiDung { get; set; }
        public string TenNguoiDung { get; set; }
        public string Tuoi { get; set; }
        public string SoDienThoai { get; set; }
        public string Email { get; set; }
        public string GioiTinh { get; set; }
        public DateTime NgaySinh { get; set; }
        public string MaChucVu { get; set; }
    }
}
