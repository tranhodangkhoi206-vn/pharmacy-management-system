using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.IO.Pipes;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Business_Logic_Layer;
using Data_Transfer_Object___Models;
using Dự_án_Quản_lí_thuốc.NET.Precentation_Layer.User_control;
using Dự_án_Quản_lí_thuốc.NET.User_control.UC_DuocSi;
using Microsoft.VisualBasic;

namespace Dự_án_Quản_lí_thuốc.NET.User_control
{
    public partial class UC_QuanLiKhachHang : UserControl
    {
        BLL_QuanLiKhachHang bll_quanlikhachhang = new BLL_QuanLiKhachHang();
        UC_ThemKhachHang ucThemKhachHang = new UC_ThemKhachHang();
        UC_ChinhSuaKhachHang ucChinhSuaKhachHang = new UC_ChinhSuaKhachHang();
        public UC_QuanLiKhachHang()
        {
            InitializeComponent();
            pThayDoi.Controls.Add(ucChinhSuaKhachHang);
            pThayDoi.Controls.Add(ucThemKhachHang);

            ucChinhSuaKhachHang.Dock = DockStyle.Fill;
            ucThemKhachHang.Dock = DockStyle.Fill;

            ucThemKhachHang.BringToFront();
            LoadDataGridViewDanhSachKhachHang();
            CapNhatTongKhachHang();
        }
        private void LoadDataGridViewDanhSachKhachHang()
        {
            dgvDSKhachHang.DataSource = bll_quanlikhachhang.LoadDanhSachKhachHang();
        }
        private void CapNhatTongKhachHang()
        {
            ucThemKhachHang.TongKhachHang(bll_quanlikhachhang.CapNhatTongKhachHang());
        }

        private void DonDep()
        {
            txtHoTen.Text = string.Empty;
            txtTuoi.Text = string.Empty;
            txtBHYT.Text = string.Empty;
            txtCMND.Text = string.Empty;

            ucChinhSuaKhachHang.maKhachHang = string.Empty;

            cbDiaChi.Text = string.Empty;
            cbGioiTinh.Text = string.Empty;

            cbDiaChi.SelectedIndex = -1;
            cbGioiTinh.SelectedIndex = -1;
            dtpNgaySinh.Value = DateTime.Now;

            txtHoTen.Focus();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                DTO_KhachHang khachHang = new DTO_KhachHang();
                khachHang.HoVaTen = txtHoTen.Text.Trim();
                khachHang.Tuoi = txtTuoi.Text.Trim();
                khachHang.GioiTinh = cbGioiTinh.Text;
                khachHang.SoTheBHYT = txtBHYT.Text.Trim();
                khachHang.SoCMND = txtCMND.Text.Trim();
                khachHang.DiaChi = cbDiaChi.Text;
                khachHang.NgaySinh = DateTime.Parse(dtpNgaySinh.Value.ToString("yyyy-MM-dd"));
                if (bll_quanlikhachhang.KiemTraNhapLieu(khachHang))
                {
                    throw new Exception("Bạn không được bỏ trống bất kì ô nào!");
                }
                bll_quanlikhachhang.ThemKhachHang(khachHang);
                LoadDataGridViewDanhSachKhachHang();
                CapNhatTongKhachHang();
                MessageBox.Show("Thêm khách hàng thành công", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Question);
                DonDep();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void txtHoTen_TextChanged(object sender, EventArgs e)
        {
            string hoVaTen = txtHoTen.Text.ToLower().Trim();
            dgvDSKhachHang.DataSource = null;
            dgvDSKhachHang.DataSource = bll_quanlikhachhang.TimKiem(hoVaTen);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Bạn xác nhận xóa khách hàng này chứ?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string maKhachHang = ucChinhSuaKhachHang.maKhachHang;
                    DTO_KhachHang khachHang = bll_quanlikhachhang.TimKiemKhachHangTonTai(maKhachHang);
                    if (khachHang != null)
                    {
                        bll_quanlikhachhang.XoaKhachHang(maKhachHang);
                        LoadDataGridViewDanhSachKhachHang();
                        CapNhatTongKhachHang();
                        cbxChinhSua.Checked = false;
                        MessageBox.Show("Xóa khách hàng thành công", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Question);
                        DonDep();
                    }
                    else
                    {
                        throw new Exception("Khách hàng không tồn tại!");
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbxChinhSua_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxChinhSua.Checked)
            {
                ucChinhSuaKhachHang.BringToFront();
                btnThem.Enabled = false;
                btnDonDep.Enabled = false;
                btnXoa.Enabled = true;
                btnNhapForm.Enabled = true;
            }
            else
            {
                ucThemKhachHang.BringToFront();
                btnThem.Enabled = true;
                btnDonDep.Enabled = true;
                btnXoa.Enabled = false;
                btnNhapForm.Enabled = false;
            }
        }

        private void btnNhapForm_Click(object sender, EventArgs e)
        {
            try
            {
                btnSua.Enabled = true;
                string maKhachHang = ucChinhSuaKhachHang.maKhachHang;
                DTO_KhachHang khachHang = bll_quanlikhachhang.TimKiemKhachHangTonTai(maKhachHang);
                if (khachHang == null)
                {
                    throw new Exception("Khách hàng không tồn tại trong hệ thống!");
                }
                else
                {
                    txtHoTen.Text = khachHang.HoVaTen;
                    txtTuoi.Text = khachHang.Tuoi;
                    txtBHYT.Text = khachHang.SoTheBHYT;
                    txtCMND.Text = khachHang.SoCMND;
                    dtpNgaySinh.Value = khachHang.NgaySinh;
                    cbGioiTinh.Text = khachHang.GioiTinh;
                    cbDiaChi.Text = khachHang.DiaChi;
                    btnSua.Enabled = true;
                }
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
                if (MessageBox.Show("Bạn xác nhận xóa khách hàng này chứ?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    DTO_KhachHang khachHang = new DTO_KhachHang();
                    khachHang.MaKhachHang = ucChinhSuaKhachHang.maKhachHang;
                    khachHang.HoVaTen = txtHoTen.Text.Trim();
                    khachHang.Tuoi = txtTuoi.Text.Trim();
                    khachHang.GioiTinh = cbGioiTinh.Text;
                    khachHang.SoTheBHYT = txtBHYT.Text.Trim();
                    khachHang.SoCMND = txtCMND.Text.Trim();
                    khachHang.DiaChi = cbDiaChi.SelectedText;
                    khachHang.NgaySinh = DateTime.Parse(dtpNgaySinh.Value.ToString("yyyy-MM-dd"));
                    if (bll_quanlikhachhang.KiemTraNhapLieu(khachHang))
                    {
                        bll_quanlikhachhang.SuaKhachHang(khachHang);
                        LoadDataGridViewDanhSachKhachHang();
                        CapNhatTongKhachHang();
                        MessageBox.Show("Sửa khách hàng thành công", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Question);
                        DonDep();
                    }
                    else
                    {
                        throw new Exception("Bạn không được bỏ trống bất kì ô nào!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDonDep_Click(object sender, EventArgs e)
        {
            DonDep();
        }
    }
}
