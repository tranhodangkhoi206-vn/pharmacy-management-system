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
using Dự_án_Quản_lí_thuốc.NET.Precentation_Layer.User_control;
using Dự_án_Quản_lí_thuốc.NET.User_control;
using Dự_án_Quản_lí_thuốc.NET.User_control.UC_QuanLi;

namespace Dự_án_Quản_lí_thuốc.NET.Forms
{
    public partial class frmQuanLi : Form
    {
        private BLL_DuLieuForm bll_dulieuform = new BLL_DuLieuForm();
        public frmQuanLi()
        {
            InitializeComponent();
        }

        private void frmQuanLi_Load(object sender, EventArgs e)
        {
            string[] duLieu = bll_dulieuform.ThongTinForm().Split(',');
            lbMaNV.Text = duLieu[0];
            lbTenNV.Text = duLieu[1];
        }
        private void btnCapTaiKhoan_Click(object sender, EventArgs e)
        {
            btnCapTaiKhoan.BackColor = ColorTranslator.FromHtml("#22223b");
            btnCapTaiKhoan.ForeColor = Color.White;

            pScreen.Controls.Clear();
            UC_CapTaiKhoan ucCapTK = new UC_CapTaiKhoan();
            ucCapTK.Dock = DockStyle.Fill;
            pScreen.Controls.Add(ucCapTK);
            ucCapTK.BringToFront();
        }

        private void btnCapTaiKhoan_Leave(object sender, EventArgs e)
        {
            btnCapTaiKhoan.BackColor = Color.DimGray;
            btnCapTaiKhoan.ForeColor = Color.White;
        }

        private void btnQuanLiNhanVien_Click(object sender, EventArgs e)
        {
            btnQuanLiNhanVien.BackColor = ColorTranslator.FromHtml("#22223b");
            btnQuanLiNhanVien.ForeColor = Color.White;

            pScreen.Controls.Clear();
            UC_QuanLiNhanVien ucQuanLiNhanVien = new UC_QuanLiNhanVien();
            ucQuanLiNhanVien.Dock = DockStyle.Fill;
            pScreen.Controls.Add(ucQuanLiNhanVien);
            ucQuanLiNhanVien.BringToFront();
        }

        private void btnQuanLiNhanVien_Leave(object sender, EventArgs e)
        {
            btnQuanLiNhanVien.BackColor = Color.DimGray;
            btnQuanLiNhanVien.ForeColor = Color.White;
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmDangNhap frmDangNhap = new frmDangNhap();
            frmDangNhap.Show();
        }
        private void frmQuanLi_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn chắc chắn muốn thoát không?", "Xác nhân", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true;
            }
        }
    }
}
