using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Business_Logic_Layer;
using Data_Transfer_Object___Models;
using Dự_án_Quản_lí_thuốc.NET.Forms;

namespace Dự_án_Quản_lí_thuốc.NET.Precentation_Layer.User_control
{
    public partial class UC_LapHoaDon : UserControl
    {
        private BLL_LapHoaDon bll_laphoadon = new BLL_LapHoaDon();
        public UC_LapHoaDon()
        {
            InitializeComponent();
            TatNhapKhachHang();
            LoadDataGridViewGioHang();
            LoadDuLieuCSDL();
        }

        private void TatNhapKhachHang()
        {
            txtHoTen.Enabled = false;
            txtTuoi.Enabled = false;
            txtBHYT.Enabled = false;
            txtCMND.Enabled = false;
            cbDiaChi.Enabled = false;
            dtpNgaySinh.Enabled = false;
            cbGioiTinh.Enabled = false;
        }

        private void DonDepThongTin()
        {
            txtMaThuoc.Text = string.Empty;
            nudSoLuong.Value = 1;

            txtHoTen.Text = string.Empty;
            txtTuoi.Text = string.Empty;
            txtBHYT.Text = string.Empty;
            txtCMND.Text = string.Empty;

            cbDiaChi.Text = string.Empty;
            cbGioiTinh.Text = string.Empty;

            cbDiaChi.SelectedIndex = -1;
            cbGioiTinh.SelectedIndex = -1;

            dtpNgaySinh.Value = DateTime.Now;
            dgvGioHang.DataSource = null;

            txtHoTen.Focus();
        }

        private void LoadDataGridViewGioHang()
        {
            List<DTO_GioHangHienThi> danhSachThuocTrongGio = bll_laphoadon.LoadDuLieuGioHang();
            dgvGioHang.DataSource = null;
            dgvGioHang.DataSource = danhSachThuocTrongGio;
        }

        private void LoadDuLieuCSDL()
        {
            bll_laphoadon.LoadDuLieuCSDL();
        }

        private void btnXemDSHoaDon_Click(object sender, EventArgs e)
        {
            frmXemDSHoaDon frmXemDSHoaDon = new frmXemDSHoaDon();
            frmXemDSHoaDon.ShowDialog();
        }

        private void btnThemGio_Click(object sender, EventArgs e)
        {
            try
            {
                string maThuoc = txtMaThuoc.Text.Trim().ToUpper();
                int soLuong = (int)nudSoLuong.Value;
                bll_laphoadon.LoiGioHang(maThuoc);
                bll_laphoadon.ThemGioHang(maThuoc, soLuong);
                lbTongTien.Text = bll_laphoadon.CongTien();
                LoadDataGridViewGioHang();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoaGio_Click(object sender, EventArgs e)
        {
            try
            {
                string maThuoc = txtMaThuoc.Text.Trim().ToUpper();
                lbTongTien.Text = bll_laphoadon.TruTien(maThuoc);
                bll_laphoadon.XoaGioHang(maThuoc);
                LoadDataGridViewGioHang();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            try
            {
                bll_laphoadon.LoiThanhToan();

                if (cbNhapKhachHang.Checked)
                {
                    DTO_KhachHang khachHang = new DTO_KhachHang();
                    khachHang.HoVaTen = txtHoTen.Text.Trim();
                    khachHang.Tuoi = txtTuoi.Text.Trim();
                    khachHang.GioiTinh = cbGioiTinh.Text;
                    khachHang.SoTheBHYT = txtBHYT.Text.Trim();
                    khachHang.SoCMND = txtCMND.Text.Trim();
                    khachHang.DiaChi = cbDiaChi.Text;
                    khachHang.NgaySinh = DateTime.Parse(dtpNgaySinh.Value.ToString("yyyy-MM-dd"));
                    if (bll_laphoadon.KiemTraNhapLieu(khachHang))
                    {
                        bll_laphoadon.ThemKhachHang(khachHang);
                    }
                    else
                    {
                        throw new Exception("Bạn không được bỏ trống bất kì ô nào!");
                    }
                }

                bll_laphoadon.ThemHoaDon();
                bll_laphoadon.ThemChiTietHoaDon();
                LoadDuLieuCSDL();
                MessageBox.Show("Thanh toán hóa đơn thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Question);
                DonDepThongTin();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbNhapKhachHang_CheckedChanged(object sender, EventArgs e)
        {
            if (cbNhapKhachHang.Checked)
            {
                txtHoTen.Enabled = true;
                txtTuoi.Enabled = true;
                txtBHYT.Enabled = true;
                txtCMND.Enabled = true;
                cbDiaChi.Enabled = true;
                dtpNgaySinh.Enabled = true;
                cbGioiTinh.Enabled = true;
            }
            else
            {
                txtHoTen.Enabled = false;
                txtTuoi.Enabled = false;
                txtBHYT.Enabled = false;
                txtCMND.Enabled = false;
                cbDiaChi.Enabled = false;
                dtpNgaySinh.Enabled = false;
                cbGioiTinh.Enabled = false;
            }
        }
    }
}
