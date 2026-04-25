using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace Data_Transfer_Object___Models
{
    public class DTO_KhachHang
    {
        public string MaKhachHang { get; set; }
        public string HoVaTen { get; set; }
        public string Tuoi { get; set; }
        public string GioiTinh { get; set; }
        public string SoTheBHYT { get; set; }
        public string SoCMND { get; set; }
        public string DiaChi { get; set; }
        public DateTime NgaySinh { get; set; }
    }
}
