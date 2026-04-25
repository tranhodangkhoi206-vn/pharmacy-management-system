using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dự_án_Quản_lí_thuốc.NET.Precentation_Layer.User_control;

using System.Data.SqlClient;
using System.Runtime.InteropServices;
using Dự_án_Quản_lí_thuốc.NET.User_control;
using Business_Logic_Layer;

namespace Dự_án_Quản_lí_thuốc.NET.Precentation_Layer
{
    public partial class frmDuocSi : Form
    {
        private string maNguoiDung;
        private BLL_DuLieuForm bll_dulieuform = new BLL_DuLieuForm();
        public frmDuocSi()
        {
            InitializeComponent();
        }

        public string LayMaNguoiDung()
        {
            return maNguoiDung;
        }

        private void frmDuocSi_Load(object sender, EventArgs e)
        {
            string[] duLieu = bll_dulieuform.ThongTinForm().Split(',');
            lbMaNV.Text = duLieu[0];
            lbTenNV.Text = duLieu[1];

        }
        // Các chức năng trên giao diện
        private void btnTraCuuThuoc_Click(object sender, EventArgs e)
        {
            btnTraCuuThuoc.BackColor = ColorTranslator.FromHtml("#70e000");
            btnTraCuuThuoc.ForeColor = Color.White;

            pScreen.Controls.Clear();
            UC_TraCuuThuoc ucTraCuuThuoc = new UC_TraCuuThuoc();
            ucTraCuuThuoc.Dock = DockStyle.Fill;
            pScreen.Controls.Add(ucTraCuuThuoc);
            ucTraCuuThuoc.BringToFront();
        }

        private void btnTraCuuThuoc_Leave(object sender, EventArgs e)
        {
            btnTraCuuThuoc.BackColor = SystemColors.HotTrack;
            btnTraCuuThuoc.ForeColor = Color.White;
        }

        private void btnLapHoaDon_Click(object sender, EventArgs e)
        {
            btnLapHoaDon.BackColor = ColorTranslator.FromHtml("#70e000");
            btnLapHoaDon.ForeColor = Color.White;

            pScreen.Controls.Clear();
            UC_LapHoaDon ucLapHoaDon = new UC_LapHoaDon();
            ucLapHoaDon.Dock = DockStyle.Fill;
            pScreen.Controls.Add(ucLapHoaDon);
            ucLapHoaDon.BringToFront();
        }

        private void btnLapHoaDon_Leave(object sender, EventArgs e)
        {
            btnLapHoaDon.BackColor = SystemColors.HotTrack;
            btnLapHoaDon.ForeColor = Color.White;
        }

        private void btnQuanLiKhachHang_Click(object sender, EventArgs e)
        {
            btnQuanLiKhachHang.BackColor = ColorTranslator.FromHtml("#70e000");
            btnQuanLiKhachHang.ForeColor = Color.White;

            pScreen.Controls.Clear();
            UC_QuanLiKhachHang ucQuanLiKhachHang = new UC_QuanLiKhachHang();
            ucQuanLiKhachHang.Dock = DockStyle.Fill;
            pScreen.Controls.Add(ucQuanLiKhachHang);
            ucQuanLiKhachHang.BringToFront();
        }
        private void btnQuanLiKhachHang_Leave(object sender, EventArgs e)
        {
            btnQuanLiKhachHang.BackColor = SystemColors.HotTrack;
            btnQuanLiKhachHang.ForeColor = Color.White;
        }
        private void btnQuanLiThuoc_Click(object sender, EventArgs e)
        {
            btnQuanLiThuoc.BackColor = ColorTranslator.FromHtml("#70e000");
            btnQuanLiThuoc.ForeColor = Color.White;

            pScreen.Controls.Clear();
            UC_QuanLiThuoc ucQuanLiThuoc = new UC_QuanLiThuoc();
            ucQuanLiThuoc.Dock = DockStyle.Fill;
            pScreen.Controls.Add(ucQuanLiThuoc);
            ucQuanLiThuoc.BringToFront();
        }

        private void btnQuanLiThuoc_Leave(object sender, EventArgs e)
        {
            btnQuanLiThuoc.BackColor = SystemColors.HotTrack;
            btnQuanLiThuoc.ForeColor = Color.White;
        }


        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmDangNhap FrmDangNhap = new frmDangNhap();
            FrmDangNhap.Show();
        }

        private void frmDuocSi_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn chắc chắn muốn thoát không?", "Xác nhân", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Environment.Exit(0);
            }
            else
            {
                e.Cancel = true;
            }
        }
    }
}
