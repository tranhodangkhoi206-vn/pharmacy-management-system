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
using Dự_án_Quản_lí_thuốc.NET.User_control.UC_NhanVienKho;

namespace Dự_án_Quản_lí_thuốc.NET.Forms
{
    public partial class frmNhanVienKho : Form
    {
        private BLL_DuLieuForm bll_dulieuform = new BLL_DuLieuForm();
        public frmNhanVienKho()
        {
            InitializeComponent();
        }

        private void frmNhanVienKho_Load(object sender, EventArgs e)
        {
            string[] duLieu = bll_dulieuform.ThongTinForm().Split(',');
            lbMaNV.Text = duLieu[0];
            lbTenNV.Text = duLieu[1];
        }
        private void btnNhapKho_Click(object sender, EventArgs e)
        {
            btnNhapKho.BackColor = ColorTranslator.FromHtml("#ffba08");
            btnNhapKho.ForeColor = Color.White;

            pScreen.Controls.Clear();
            UC_NhapKho ucNhapKho = new UC_NhapKho();
            ucNhapKho.Dock = DockStyle.Fill;
            pScreen.Controls.Add(ucNhapKho);
            ucNhapKho.BringToFront();
        }
        private void btnNhapKho_Leave(object sender, EventArgs e)
        {
            btnNhapKho.BackColor = Color.OrangeRed;
            btnNhapKho.ForeColor = Color.White;
        }

        private void btnQuanLiNhaCungCap_Click(object sender, EventArgs e)
        {
            btnQuanLiNhaCungCap.BackColor = ColorTranslator.FromHtml("#ffba08");
            btnQuanLiNhaCungCap.ForeColor = Color.White;

            pScreen.Controls.Clear();
            UC_QuanLiNhaCungCap ucQuanLiNhaCungCap = new UC_QuanLiNhaCungCap();
            ucQuanLiNhaCungCap.Dock = DockStyle.Fill;
            pScreen.Controls.Add(ucQuanLiNhaCungCap);
            ucQuanLiNhaCungCap.BringToFront();
        }
        private void btnQuanLiNhaCungCap_Leave(object sender, EventArgs e)
        {
            btnQuanLiNhaCungCap.BackColor = Color.OrangeRed;
            btnQuanLiNhaCungCap.ForeColor = Color.White;
        }

        private void btnQuanLiHanSuDungThuoc_Click(object sender, EventArgs e)
        {
            btnQuanLiHanSuDungThuoc.BackColor = ColorTranslator.FromHtml("#ffba08");
            btnQuanLiHanSuDungThuoc.ForeColor = Color.White;

            pScreen.Controls.Clear();
            UC_QuanLiHanSuDungThuoc ucQuanLiHanSuDungThuoc = new UC_QuanLiHanSuDungThuoc();
            ucQuanLiHanSuDungThuoc.Dock = DockStyle.Fill;
            pScreen.Controls.Add(ucQuanLiHanSuDungThuoc);
            ucQuanLiHanSuDungThuoc.BringToFront();
        }

        private void btnQuanLiHanSuDungThuoc_Leave(object sender, EventArgs e)
        {
            btnQuanLiHanSuDungThuoc.BackColor = Color.OrangeRed;
            btnQuanLiHanSuDungThuoc.ForeColor = Color.White;
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmDangNhap frmDangNhap = new frmDangNhap();
            frmDangNhap.Show();
        }

        private void frmNhanVienKho_FormClosing(object sender, FormClosingEventArgs e)
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
