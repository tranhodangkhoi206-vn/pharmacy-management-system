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
using Dự_án_Quản_lí_thuốc.NET.Forms;
using Dự_án_Quản_lí_thuốc.NET.Precentation_Layer;
using Dự_án_Quản_lí_thuốc.NET.Precentation_Layer.User_control;

namespace Dự_án_Quản_lí_thuốc.NET
{
    public partial class frmDangNhap : Form
    {
        private BLL_DangNhap bll_dangnhap = new BLL_DangNhap();

        public frmDangNhap()
        {
            InitializeComponent();
        }

        private void frmDangNhap_Load(object sender, EventArgs e)
        {
            txtMK.PasswordChar = '*';
        }
        private bool KiemTraNhap()
        {
            string username = txtUsername.Text.Trim();
            string matkhau = txtMK.Text.Trim();
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(matkhau))
                return false;
            return true;
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            try
            {
                if (!KiemTraNhap()) throw new Exception("Bạn không được để trống bất kì phần nào!");
                string username = txtUsername.Text.Trim();
                string matkhau = txtMK.Text.Trim();
                string chucvu = bll_dangnhap.KiemTraDangNhap(username, matkhau);
                if (chucvu == "DS")
                {
                    this.Hide();
                    frmDuocSi frmDuocSi = new frmDuocSi();
                    frmDuocSi.Show();
                }
                else if (chucvu == "NVK")
                {
                    this.Hide();
                    frmNhanVienKho frmNhanVienKho = new frmNhanVienKho();
                    frmNhanVienKho.Show();
                }
                else if (chucvu == "QL")
                {
                    this.Hide();
                    frmQuanLi frmQuanLi = new frmQuanLi();
                    frmQuanLi.Show();
                }
                else
                {
                    throw new Exception("Username hoặc mật khẩu chưa chính xác");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmDangNhap_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (MessageBox.Show("Bạn chắc chắn muốn thoát không?", "Xác nhân", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void cbHienThiMatKhau_CheckedChanged(object sender, EventArgs e)
        {
            if (cbHienThiMatKhau.Checked)
                txtMK.PasswordChar = '\0';
            else
                txtMK.PasswordChar = '*';
        }

        private void frmDangNhap_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn chắc chắn muốn thoát không?", "Xác nhân", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                Environment.Exit(0);
            }
        }
    }
}
