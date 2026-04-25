using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Transfer_Object___Models
{
    public class DTO_NhapHang
    {
        public string MaLoHang { get; set; }
        public string MaNhaCungCap { get; set; }
        public string MaThuoc { get; set; }
        public string MaNguoiDung { get; set; }
        public int SoLuongNhap { get; set; }
        public DateTime NgayNhap { get; set; }
    }
}
