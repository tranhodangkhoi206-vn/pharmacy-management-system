using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Business_Logic_Layer;
using Data_Transfer_Object___Models;
using Dự_án_Quản_lí_thuốc.NET.User_control.UC_DuocSi;

namespace Dự_án_Quản_lí_thuốc.NET.User_control
{
    public partial class UC_NhapKho : UserControl
    {
        BLL_NhapKho bll_nhapkho = new BLL_NhapKho();
        public UC_NhapKho()
        {
            InitializeComponent();
            LoadDataGridViewDanhSachNhapKho();
            LoadComboboxThuoc();
            LoadComboboxNhaCungCap();
            DonDep();
        }
        private void LoadDataGridViewDanhSachNhapKho()
        {
            List<DTO_NhapHang> danhSachNhapHangCSDL = bll_nhapkho.LoadDanhSachNhapHang();
            //dgvLSNhapHang.DataSource = null;
            dgvLSNhapHang.DataSource = danhSachNhapHangCSDL;
        }

        private void LoadComboboxThuoc()
        {
            cbThuoc.DataSource = bll_nhapkho.LoadThuoc();
            cbThuoc.DisplayMember = "MATHUOC";
            cbThuoc.ValueMember = "MATHUOC";
        }
        private void LoadComboboxNhaCungCap()
        {
            cbNhaCungCap.DataSource = bll_nhapkho.LoadNhaCungCap();
            cbNhaCungCap.DisplayMember = "MANHACUNGCAP";
            cbNhaCungCap.ValueMember = "MANHACUNGCAP";
        }
        private void DonDep()
        {
            txtMaLoHang.Text = string.Empty;

            cbThuoc.SelectedIndex = -1;
            cbNhaCungCap.SelectedIndex = -1;

            nudSoLuong.Value = 1;
        }

        private void btnNhapKho_Click(object sender, EventArgs e)
        {
            try
            {
                string maLohang = txtMaLoHang.Text.ToUpper().Trim();
                DTO_NhapHang lohang = bll_nhapkho.TimKiemLoHangTonTai(maLohang);
                if (lohang != null)
                {
                    throw new Exception("Mã lô hàng đã tồn tại!");
                }
                DateTime homNay = DateTime.Now;
                DTO_NhapHang loHang = new DTO_NhapHang();
                loHang.MaLoHang = txtMaLoHang.Text.ToUpper().Trim();
                loHang.MaNhaCungCap = cbNhaCungCap.SelectedValue.ToString();
                loHang.MaThuoc = cbThuoc.SelectedValue.ToString();
                loHang.SoLuongNhap = (int)nudSoLuong.Value;
                loHang.NgayNhap = DateTime.Parse(homNay.ToString("yyyy-MM-dd HH:mm:ss"));
                if (bll_nhapkho.KiemTraNhapLieu(loHang))
                {
                    bll_nhapkho.ThemLoHang(loHang);
                    LoadDataGridViewDanhSachNhapKho();
                    MessageBox.Show("Nhập kho thành công", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Question);
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
        private void btnDonDep_Click(object sender, EventArgs e)
        {
            DonDep();
        }
    }
}
