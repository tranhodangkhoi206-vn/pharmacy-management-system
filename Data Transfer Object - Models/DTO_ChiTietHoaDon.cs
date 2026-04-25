using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Transfer_Object___Models
{
    public class DTO_ChiTietHoaDon
    {
        public string MaHoaDon { get; set; }
        public string MaThuoc { get; set; }
        public decimal DonGia { get; set; }
        public int SoLuong { get; set; }
    }
}
