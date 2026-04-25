using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Business_Logic_Layer;
using Data_Transfer_Object___Models;
using Microsoft.SqlServer.Server;

namespace Dự_án_Quản_lí_thuốc.NET.User_control
{
    public partial class UC_CapTaiKhoan : UserControl
    {
        BLL_CapTaiKhoan bll_captaikhoan = new BLL_CapTaiKhoan();
        private bool flag = false;
        public UC_CapTaiKhoan()
        {
            InitializeComponent();
            LoadDataGridView();
            LoadCombobox();
            DonDep(true);
            flag = true;
        }
        private void LoadDataGridView()
        {
            dgvTaiKhoan.DataSource = bll_captaikhoan.LoadDanhSachTaiKhoan();
            dgvTaiKhoan.Columns["MACHUCVU"].Visible = false;
        }
        private void LoadCombobox()
        {
            cbChuVu.DataSource = bll_captaikhoan.LoadChucVu();
            cbChuVu.DisplayMember = "TENCHUCVU";
            cbChuVu.ValueMember = "MACHUCVU";
            cbTenNhanVien.DataSource = bll_captaikhoan.LoadTenNguoiDung();
            cbTenNhanVien.DisplayMember = "TENNGUOIDUNG";
            cbTenNhanVien.ValueMember = "MANGUOIDUNG";
        }


        private void DonDep(bool dk)
        {
            txtUsername.Text = string.Empty;
            txtMatKhau.Text = string.Empty;

            cbChuVu.SelectedIndex = -1;
            if (dk)
            {
                cbTenNhanVien.SelectedIndex = -1;
                cbTenNhanVien.Text = string.Empty;
            }
        }

        private void btnCapTaiKhoan_Click(object sender, EventArgs e)
        {
            try
            {
                bll_captaikhoan.TimKiemTaiKhoanTonTai(txtUsername.Text.Trim(), cbTenNhanVien.SelectedValue.ToString());
                DTO_TaiKhoan taiKhoan = new DTO_TaiKhoan();
                taiKhoan.MaNguoiDung = cbTenNhanVien.SelectedValue.ToString();
                taiKhoan.MaChucVu = cbChuVu.SelectedValue.ToString();
                taiKhoan.Username = txtUsername.Text.Trim();
                taiKhoan.MatKhau = txtMatKhau.Text.Trim();
                bll_captaikhoan.ThemTaiKhoan(taiKhoan);
                if (bll_captaikhoan.KiemTraNhapLieu(taiKhoan))
                {
                    throw new Exception("Bạn không được bỏ trống bất kì ô nào");

                }
                LoadDataGridView();
                MessageBox.Show("Thêm tài khoản thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DonDep(true);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                DTO_TaiKhoan taiKhoan = new DTO_TaiKhoan();
                taiKhoan.MaNguoiDung = cbTenNhanVien.SelectedValue.ToString();
                taiKhoan.MaChucVu = cbChuVu.SelectedValue.ToString();
                taiKhoan.Username = txtUsername.Text.Trim();
                taiKhoan.MatKhau = txtMatKhau.Text.Trim();
                //bll_captaikhoan.ThemTaiKhoan(taiKhoan);
                if (bll_captaikhoan.KiemTraNhapLieu(taiKhoan))
                {
                    throw new Exception("Bạn không được bỏ trống bất kì ô nào");
                }
                bll_captaikhoan.SuaTaiKhoan(taiKhoan);
                LoadDataGridView();
                MessageBox.Show("Sửa tài khoản thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DonDep(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                string maNguoiDung = cbTenNhanVien.SelectedValue.ToString();
                bll_captaikhoan.XoaTaiKhoan(maNguoiDung);
                LoadDataGridView();
                MessageBox.Show("Xoa tài khoản thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DonDep(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDonDep_Click(object sender, EventArgs e)
        {
            DonDep(true);
        }

        private void cbTenNhanVien_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (flag)
            {
                DonDep(false);
                string maNguoiDung = cbTenNhanVien.SelectedValue?.ToString();
                DTO_TaiKhoan taiKhoan = bll_captaikhoan.NhapThongTin(maNguoiDung);

                if (taiKhoan == null || string.IsNullOrEmpty(taiKhoan.Username))
                {
                    cbChuVu.SelectedIndex = -1;
                    txtUsername.Text = string.Empty;
                    txtMatKhau.Text = string.Empty;
                }
                else
                {
                    cbChuVu.Text = taiKhoan.TenChucVu;
                    txtUsername.Text = taiKhoan.Username;
                    txtMatKhau.Text = taiKhoan.MatKhau;
                }
            }
        }
    }
}
