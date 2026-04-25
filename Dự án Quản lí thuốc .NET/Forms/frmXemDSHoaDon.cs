using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Business_Logic_Layer;

namespace Dự_án_Quản_lí_thuốc.NET.Forms
{
    public partial class frmXemDSHoaDon : Form
    {
        private BLL_LapHoaDon bll_laphoadon = new BLL_LapHoaDon();
        public frmXemDSHoaDon()
        {
            InitializeComponent();
            dgvDSHoaDon.DataSource = bll_laphoadon.LoadDanhSachHoaDon();
        }
    }
}
