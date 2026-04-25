using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dự_án_Quản_lí_thuốc.NET.User_control.UC_DuocSi
{
    public partial class UC_ThemKhachHang : UserControl
    {
        public UC_ThemKhachHang()
        {
            InitializeComponent();
        }
        public void TongKhachHang(string tongkhachhang)
        {
            lbTongKhachHang.Text = tongkhachhang;
        }
    }
}
