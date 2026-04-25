using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Business_Logic_Layer;
using Data_Transfer_Object___Models;
using Dự_án_Quản_lí_thuốc.NET.User_control.UC_DuocSi;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace Dự_án_Quản_lí_thuốc.NET.User_control
{
    public partial class UC_QuanLiThuoc : UserControl
    {
        private BLL_QuanLiThuoc bll_quanlithuoc = new BLL_QuanLiThuoc();
        private UC_ChinhSuaThuoc ucChinhSuaThuoc = new UC_ChinhSuaThuoc();
        private UC_ThemThuoc ucThemThuoc = new UC_ThemThuoc();
        public UC_QuanLiThuoc()
        {
            InitializeComponent();
            pNhap.Controls.Add(ucChinhSuaThuoc);
            pNhap.Controls.Add(ucThemThuoc);

            ucChinhSuaThuoc.Dock = DockStyle.Fill;
            ucThemThuoc.Dock = DockStyle.Fill;

            ucThemThuoc.BringToFront();
            LoadDataGridViewDanhSachThuoc();
            LoadComboboxDangBaoChe();
            LoadComboboxDonViTinh();
            LoadComboboxLoaiThuoc();
            LoadComboboxNhaCungCap();
            CapNhatTongThuoc();
            CapNhatTongSoLuongThuoc();
            DonDep();
        }
        private void LoadDataGridViewDanhSachThuoc()
        {
            //dgvDSThuoc.DataSource = null;
            dgvDSThuoc.DataSource = bll_quanlithuoc.LoadDanhSachThuocHienThi();
            dgvDSThuoc.Columns["MALOAI"].Visible = false;
            dgvDSThuoc.Columns["MADONVITINH"].Visible = false;
            dgvDSThuoc.Columns["MADANGBAOCHE"].Visible = false;
            dgvDSThuoc.Columns["MANHACUNGCAP"].Visible = false;

        }
        private void LoadComboboxLoaiThuoc()
        {
            cbLoai.DataSource = bll_quanlithuoc.ComboboxLoaiThuoc();
            cbLoai.DisplayMember = "TENLOAI";
            cbLoai.ValueMember = "MALOAI";

        }
        private void LoadComboboxDonViTinh()
        {
            cbDVT.DataSource = bll_quanlithuoc.ComboboxDonViTinh();
            cbDVT.DisplayMember = "TENDONVITINH";
            cbDVT.ValueMember = "MADONVITINH";

        }
        private void LoadComboboxDangBaoChe()
        {
            cbDBC.DataSource = bll_quanlithuoc.ComboboxDangBaoChe();
            cbDBC.DisplayMember = "TENDANGBAOCHE";
            cbDBC.ValueMember = "MADANGBAOCHE";

        }
        private void LoadComboboxNhaCungCap()
        {
            cbNhaCungCap.DataSource = bll_quanlithuoc.ComboboxNhaCungCap();
            cbNhaCungCap.DisplayMember = "TENNHACUNGCAP";
            cbNhaCungCap.ValueMember = "MANHACUNGCAP";
            cbNhaCungCap.SelectedIndex = -1;
        }
        private void DonDep()
        {
            txtTenThuoc.Text = string.Empty;
            txtDonGia.Text = string.Empty;

            ucChinhSuaThuoc.maThuoc = string.Empty;

            cbDBC.Text = string.Empty;
            cbDVT.Text = string.Empty;
            cbLoai.Text = string.Empty;
            cbNhaCungCap.Text = string.Empty;

            cbDBC.SelectedIndex = -1;
            cbDVT.SelectedIndex = -1;
            cbNhaCungCap.SelectedIndex = -1;
            cbLoai.SelectedIndex = -1;
            dtpHanDung.Value = DateTime.Now;
            nudSoLuongTon.Value = 1;

            txtTenThuoc.Focus();
        }

        private void CapNhatTongThuoc()
        {
            string tong = bll_quanlithuoc.CapNhatTongThuoc().ToString();
            ucThemThuoc.TongThuoc(tong);
        }
        private void CapNhatTongSoLuongThuoc()
        {
            string tong = bll_quanlithuoc.CapNhatTongSoLuongThuoc().ToString();
            ucThemThuoc.TongSoLuongThuoc(tong);
        }

        private void txtTenThuoc_TextChanged(object sender, EventArgs e)
        {
            string tenThuoc = txtTenThuoc.Text.Trim().ToLower();
            //dgvDSThuoc.DataSource = null;
            dgvDSThuoc.DataSource = bll_quanlithuoc.TimKiem(tenThuoc);
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (bll_quanlithuoc.KiemTraNhapLieu(txtTenThuoc.Text, cbDVT.SelectedValue?.ToString(), cbDBC.SelectedValue?.ToString(), cbLoai.SelectedValue?.ToString(), cbNhaCungCap.SelectedValue?.ToString()))
                {
                    DTO_Thuoc thuoc = new DTO_Thuoc();
                    thuoc.TenThuoc = txtTenThuoc.Text.Trim();
                    thuoc.MaDonViTinh = cbDVT.SelectedValue.ToString();
                    thuoc.MaDangBaoChe = cbDBC.SelectedValue
                        .ToString();
                    thuoc.MaLoai = cbLoai.SelectedValue.ToString();
                    thuoc.DonGia = decimal.Parse(txtDonGia.Text.Trim());
                    thuoc.NgayHetHan = DateTime.Parse(dtpHanDung.Value.ToString("yyyy-MM-dd"));
                    thuoc.MaNhaCungCap = cbNhaCungCap.SelectedValue.ToString();
                    thuoc.SoLuongTon = (int)nudSoLuongTon.Value;
                    bll_quanlithuoc.ThemThuocBLL(thuoc);
                    LoadDataGridViewDanhSachThuoc();
                    CapNhatTongThuoc();
                    CapNhatTongSoLuongThuoc();
                    MessageBox.Show("Thêm thuốc thành công", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    DonDep();
                }
                else
                {
                    throw new Exception("Bạn không được bỏ trống bất kì ô nào!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNhapDuLieuForm_Click(object sender, EventArgs e)
        {
            try
            {
                btnSua.Enabled = true;
                string maThuoc = ucChinhSuaThuoc.maThuoc;
                DTO_Thuoc thuoc = bll_quanlithuoc.TimKiemThuocTonTai(maThuoc);
                if (thuoc != null)
                {
                    txtTenThuoc.Text = thuoc.TenThuoc;
                    txtDonGia.Text = thuoc.DonGia.ToString();
                    cbDBC.SelectedValue = thuoc.MaDangBaoChe;
                    cbDVT.SelectedValue = thuoc.MaDonViTinh;
                    cbLoai.SelectedValue = thuoc.MaLoai;
                    cbNhaCungCap.SelectedValue = thuoc.MaNhaCungCap;
                    dtpHanDung.Value = thuoc.NgayHetHan;
                    nudSoLuongTon.Value = thuoc.SoLuongTon;
                    btnSua.Enabled = true;
                }
                else
                {
                    throw new Exception("Thuốc không tồn tại trong hệ thống!");
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
                if (MessageBox.Show("Bạn xác nhận sửa thông tin thuốc này chứ?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (bll_quanlithuoc.KiemTraNhapLieu(txtTenThuoc.Text, cbDVT.SelectedValue?.ToString(), cbDBC.SelectedValue?.ToString(), cbLoai.SelectedValue?.ToString(), cbNhaCungCap.SelectedValue?.ToString()))
                    {
                        DTO_Thuoc thuoc = new DTO_Thuoc();
                        thuoc.MaThuoc = ucChinhSuaThuoc.maThuoc;
                        thuoc.TenThuoc = txtTenThuoc.Text.Trim();
                        thuoc.MaDonViTinh = cbDVT.SelectedValue.ToString();
                        thuoc.MaDangBaoChe = cbDBC.SelectedValue.ToString();
                        thuoc.MaLoai = cbLoai.SelectedValue.ToString();
                        thuoc.MaNhaCungCap = cbNhaCungCap.SelectedValue.ToString();
                        thuoc.DonGia = decimal.Parse(txtDonGia.Text.Trim());
                        thuoc.SoLuongTon = (int)nudSoLuongTon.Value;
                        thuoc.NgayHetHan = DateTime.Parse(dtpHanDung.Value.ToString("yyyy-MM-dd"));
                        bll_quanlithuoc.SuaThuoc(thuoc);
                        LoadDataGridViewDanhSachThuoc();
                        CapNhatTongThuoc();
                        CapNhatTongSoLuongThuoc();
                        MessageBox.Show("Sửa thông tin thuốc thành công", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Question);
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

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Bạn xác nhận xóa thuốc này chứ?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string maThuoc = ucChinhSuaThuoc.maThuoc.ToUpper();
                    DTO_Thuoc thuoc = bll_quanlithuoc.TimKiemThuocTonTai(maThuoc);
                    if (thuoc != null)
                    {
                        bll_quanlithuoc.XoaThuoc(maThuoc);
                        LoadDataGridViewDanhSachThuoc();
                        CapNhatTongThuoc();
                        CapNhatTongSoLuongThuoc();
                        cbxChinhSua.Checked = false;
                        MessageBox.Show("Xóa thuốc thành công", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Question);
                        DonDep();
                    }
                    else
                    {
                        throw new Exception("Thuốc không tồn tại trong hệ thống!");
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
                ucChinhSuaThuoc.BringToFront();
                btnThem.Enabled = false;
                btnDonDep.Enabled = false;
                btnXoa.Enabled = true;
                btnNhapDuLieuForm.Enabled = true;
            }
            else
            {
                ucThemThuoc.BringToFront();
                btnThem.Enabled = true;
                btnDonDep.Enabled = true;
                btnXoa.Enabled = false;
                btnNhapDuLieuForm.Enabled = false;
            }
        }

        private void btnDonDep_Click(object sender, EventArgs e)
        {
            DonDep();
        }
    }
}
