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
using Data_Transfer_Object___Models;

namespace Dự_án_Quản_lí_thuốc.NET.User_control.UC_NhanVienKho
{
    public partial class UC_QuanLiNhaCungCap : UserControl
    {
        BLL_QuanLiNhaCungCap bll_quanlinhacungcap = new BLL_QuanLiNhaCungCap();
        public UC_QuanLiNhaCungCap()
        {
            InitializeComponent();
            LoadDataGridView();
            DonDep();
        }
        private void DonDep()
        {
            txtMaNCC.Text = string.Empty;
            txtTenNCC.Text = string.Empty;
            cbTrangThai.SelectedIndex = -1;
            dtpNgayHopTac.Value = DateTime.Now;
        }

        private void LoadDataGridView()
        {
            dgvDSNhaCungCap.DataSource = null;
            dgvDSNhaCungCap.DataSource = bll_quanlinhacungcap.LoadDanhSachNhaCungCap();
            lbTongNhaCungCap.Text = bll_quanlinhacungcap.TinhTongNhaCungCap();
        }


        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                DTO_NhaCungCap nhaCungCap = new DTO_NhaCungCap();
                nhaCungCap.MaNhaCungCap = txtMaNCC.Text.Trim().ToUpper();
                nhaCungCap.TenNhaCungCap = txtTenNCC.Text.Trim();
                nhaCungCap.TrangThai = cbTrangThai.Text;
                nhaCungCap.NgayHopTac = DateTime.Parse(dtpNgayHopTac.Value.ToString("yyyy-MM-dd"));
                if (bll_quanlinhacungcap.KiemTraNhapLieu(nhaCungCap))
                {
                    throw new Exception("Bạn không được bỏ trống bất kì ô nào!");
                }
                bll_quanlinhacungcap.ThemNhaCungCap(nhaCungCap);
                LoadDataGridView();
                MessageBox.Show("Thêm nhà cung cấp thành công", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Question);
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
                if (MessageBox.Show("Bạn có chắc chắn xóa nhà cung cấp này không", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string maNhaCungCap = txtMaNCC.Text.Trim().ToUpper();

                    if (bll_quanlinhacungcap.TimKiemNhaCungCapTonTai(maNhaCungCap) == null)
                    {
                        throw new Exception("Nhà cung cấp không tồn tại không hệ thống!");
                    }
                    bll_quanlinhacungcap.XoaNhaCungCap(maNhaCungCap);
                    LoadDataGridView();
                    MessageBox.Show("Xóa nhà cung cấp thành công", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Question);
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
                if (MessageBox.Show("Bạn có chắc chắn sửa nhà cung cấp này không", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    DTO_NhaCungCap nhaCungCap = new DTO_NhaCungCap();
                    nhaCungCap.MaNhaCungCap = txtMaNCC.Text.Trim().ToUpper();
                    nhaCungCap.TenNhaCungCap = txtTenNCC.Text.Trim();
                    nhaCungCap.TrangThai = cbTrangThai.Text;
                    nhaCungCap.NgayHopTac = DateTime.Parse(dtpNgayHopTac.Value.ToString("yyyy-MM-dd"));
                    if (bll_quanlinhacungcap.KiemTraNhapLieu(nhaCungCap))
                    {
                        throw new Exception("Bạn không được bỏ trống bất kì ô nào!");
                    }
                    bll_quanlinhacungcap.SuaNhaCungCap(nhaCungCap);
                    LoadDataGridView();
                    MessageBox.Show("Sửa nhà cung cấp thành công", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    DonDep();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNhapDuLieu_Click(object sender, EventArgs e)
        {
            try
            {
                string maNhaCungCap = txtMaNCC.Text.Trim().ToUpper();
                DTO_NhaCungCap nhaCungCap = bll_quanlinhacungcap.TimKiemNhaCungCapTonTai(maNhaCungCap);
                if (nhaCungCap == null)
                {
                    throw new Exception("Nhà cung cấp không tồn tại không hệ thống!");
                }
                txtMaNCC.Text = nhaCungCap.MaNhaCungCap;
                txtTenNCC.Text = nhaCungCap.TenNhaCungCap;
                cbTrangThai.Text = nhaCungCap.TrangThai;
                dtpNgayHopTac.Value = nhaCungCap.NgayHopTac;
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

        private void txtMaNCC_TextChanged(object sender, EventArgs e)
        {
            string maNhaCungCap = txtMaNCC.Text.Trim().ToUpper();
            dgvDSNhaCungCap.DataSource = null;
            dgvDSNhaCungCap.DataSource = bll_quanlinhacungcap.TimKiem(maNhaCungCap);
        }
    }
}
