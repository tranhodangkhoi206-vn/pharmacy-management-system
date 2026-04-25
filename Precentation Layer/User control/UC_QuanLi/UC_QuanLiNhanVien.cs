using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using Business_Logic_Layer;
using Data_Transfer_Object___Models;

namespace Dự_án_Quản_lí_thuốc.NET.User_control.UC_QuanLi
{
    public partial class UC_QuanLiNhanVien : UserControl
    {
        List<DTO_NguoiDung> bangNguoiDung;
        List<DTO_ChucVu> bangChuVu;
        BLL_QuanLiNguoiDung bll_quanlinguoidung = new BLL_QuanLiNguoiDung();
        public UC_QuanLiNhanVien()
        {
            InitializeComponent();
            LoadDataGridView();
            DonDep();
        }

        public bool KiemTraNhapLieu()
        {
            string maNguoiDung = txtMaNV.Text.Trim().ToUpper();
            string tenNguoiDung = txtTenNV.Text.Trim();
            string tuoi = txtTuoi.Text.Trim();
            string soDienThoai = txtSDT.Text.Trim();
            string email = txtEmail.Text.Trim();
            string gioiTinh = cbGioiTinh.Text.ToString();
            string maChucVu = cbChucVu.Text.ToString();
            if (!string.IsNullOrWhiteSpace(maNguoiDung)
                && !string.IsNullOrWhiteSpace(tenNguoiDung)
                && !string.IsNullOrEmpty(tuoi)
                && !string.IsNullOrWhiteSpace(soDienThoai)
                && !string.IsNullOrWhiteSpace(email)
                && !string.IsNullOrWhiteSpace(gioiTinh)
                && !string.IsNullOrEmpty(maChucVu))
            {
                return false;
            }
            return true;
        }

        private void DonDep()
        {
            txtMaNV.Text = string.Empty;
            txtTenNV.Text = string.Empty;
            txtTuoi.Text = string.Empty;
            txtSDT.Text = string.Empty;
            txtEmail.Text = string.Empty;

            cbGioiTinh.SelectedIndex = -1;
            cbGioiTinh.Text = string.Empty;

            cbChucVu.SelectedIndex = -1;
            cbChucVu.Text = string.Empty;

            dtpNgaySinh.Value = DateTime.Today;

            txtMaNV.Focus();
        }

        private void LoadDataGridView()
        {
            bangNguoiDung = bll_quanlinguoidung.LoadDanhSachNguoiDung();
            dgvDSKhachHang.DataSource = null;
            dgvDSKhachHang.DataSource = bangNguoiDung;
            lbTongNhanVien.Text = bll_quanlinguoidung.TongNhanVien().ToString();
        }

        private void TimKiem(string manguoidung)
        {
            bangNguoiDung = bll_quanlinguoidung.LoadDanhSachNguoiDung();
            dgvDSKhachHang.DataSource = null;
            dgvDSKhachHang.DataSource = bangNguoiDung.Where(t => t.MaNguoiDung != null && t.MaNguoiDung.StartsWith(manguoidung)).ToList();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (KiemTraNhapLieu())
                {
                    throw new Exception("Bạn không được bỏ trống bất kì ô nào!");
                }
                DTO_NguoiDung nguoiDung = new DTO_NguoiDung();
                nguoiDung.MaNguoiDung = txtMaNV.Text.Trim().ToUpper();
                nguoiDung.TenNguoiDung = txtTenNV.Text.Trim();
                nguoiDung.Tuoi = txtTuoi.Text.Trim();
                nguoiDung.SoDienThoai = txtSDT.Text.Trim();
                nguoiDung.Email = txtEmail.Text.Trim();
                nguoiDung.GioiTinh = cbGioiTinh.Text.ToString();
                nguoiDung.MaChucVu = cbChucVu.Text.Trim();
                nguoiDung.NgaySinh = DateTime.Parse(dtpNgaySinh.Value.ToString("yyyy-MM-dd"));
                bll_quanlinguoidung.ThemNguoiDungBLL(nguoiDung);
                LoadDataGridView();
                MessageBox.Show("Thêm người dùng thành công", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Question);
                DonDep();

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
                if (MessageBox.Show("Bạn có chắc chắn xóa người dùng này không", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string maNguoiDung = txtMaNV.Text.Trim().ToUpper();
                    bangNguoiDung = bll_quanlinguoidung.LoadDanhSachNguoiDung();
                    DTO_NguoiDung nguoiDung = bangNguoiDung.Find(t => maNguoiDung == t.MaNguoiDung);
                    if (nguoiDung == null)
                    {
                        throw new Exception("Nhà người dùng không tồn tại không hệ thống!");
                    }
                    bll_quanlinguoidung.XoaNguoiDung(maNguoiDung);
                    LoadDataGridView();
                    MessageBox.Show("Xóa người dùng  thành công", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    DonDep();
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
                if (MessageBox.Show("Bạn có chắc chắn sửa người dùng này không", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (KiemTraNhapLieu())
                    {
                        throw new Exception("Bạn không được bỏ trống bất kì ô nào!");
                    }
                    DTO_NguoiDung nguoiDung = new DTO_NguoiDung();
                    nguoiDung.MaNguoiDung = txtMaNV.Text.Trim().ToUpper();
                    nguoiDung.TenNguoiDung = txtTenNV.Text.Trim();
                    nguoiDung.Tuoi = txtTuoi.Text.Trim();
                    nguoiDung.SoDienThoai = txtSDT.Text.Trim();
                    nguoiDung.Email = txtEmail.Text.Trim();
                    nguoiDung.GioiTinh = cbGioiTinh.Text.ToString();
                    nguoiDung.MaChucVu = cbChucVu.Text.Trim();
                    nguoiDung.NgaySinh = DateTime.Parse(dtpNgaySinh.Value.ToString("yyyy-MM-dd"));
                    bll_quanlinguoidung.SuaNguoiDungBLL(nguoiDung);
                    LoadDataGridView();
                    MessageBox.Show("Sửa người dùng thành công", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    DonDep();
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

        private void txtMaNV_TextChanged(object sender, EventArgs e)
        {
            string maNguoiDung = txtMaNV.Text.ToUpper().Trim();
            TimKiem(maNguoiDung);
        }

        private void btnNhap_Click(object sender, EventArgs e)
        {
            try
            {
                string maNguoiDung = txtMaNV.Text.Trim().ToUpper();
                DTO_NguoiDung nguoiDung = bll_quanlinguoidung.NhapDuLieu(maNguoiDung);
                if (nguoiDung == null)
                {
                    throw new Exception("Người dùng không tồn tại không hệ thống!");
                }
                txtTenNV.Text = nguoiDung.TenNguoiDung;
                txtTuoi.Text = nguoiDung.Tuoi;
                txtSDT.Text = nguoiDung.SoDienThoai;
                txtEmail.Text = nguoiDung.Email;
                cbChucVu.Text = nguoiDung.MaChucVu;
                cbGioiTinh.Text = nguoiDung.GioiTinh;
                dtpNgaySinh.Value = nguoiDung.NgaySinh;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
