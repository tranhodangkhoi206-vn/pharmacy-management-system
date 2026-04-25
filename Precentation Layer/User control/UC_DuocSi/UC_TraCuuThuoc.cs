using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Business_Logic_Layer;
using Data_Transfer_Object___Models;

namespace Dự_án_Quản_lí_thuốc.NET.Precentation_Layer.User_control
{
    public partial class UC_TraCuuThuoc : UserControl
    {
        BLL_TraCuuThuoc bll_tracuuthuoc = new BLL_TraCuuThuoc();
        private bool flag = false;
        public UC_TraCuuThuoc()
        {
            InitializeComponent();
            LoadDanhSachThuoc();
            LoadComboboxLoaiThuoc();
            LoadComboboxDangBaoChe();
            LoadComboboxDonViTinh();
            LoadDataGridViewDanhSachThuoc();
        }
        private void DonDep()
        {
            cbDangBaoChe.SelectedIndex = -1;
            cbDonViTinh.SelectedIndex = -1;
            cbTenLoai.SelectedIndex = -1;
        }

        public void LoadDanhSachThuoc()
        {
            bll_tracuuthuoc.LoadDanhSachThuocCSDL();
        }
        private void LoadComboboxLoaiThuoc()
        {
            List<DTO_LoaiThuoc> danhSachLoaiThuocCSDL = bll_tracuuthuoc.ComboboxLoaiThuoc();
            danhSachLoaiThuocCSDL.Insert(0, new DTO_LoaiThuoc { TenLoai = "Tất cả(Loại thuốc)", MaLoai = "" });
            cbTenLoai.DataSource = danhSachLoaiThuocCSDL;
            cbTenLoai.DisplayMember = "TENLOAI";
            cbTenLoai.ValueMember = "MALOAI";
        }
        private void LoadComboboxDonViTinh()
        {
            List<DTO_DonViTinh> danhSachDonViTinhCSDL = bll_tracuuthuoc.ComboboxDonViTinh();
            danhSachDonViTinhCSDL.Insert(0, new DTO_DonViTinh { TenDonViTinh = "Tất cả (Đơn vị tính)", MaDonViTinh = "" });
            cbDonViTinh.DataSource = danhSachDonViTinhCSDL;
            cbDonViTinh.DisplayMember = "TENDONVITINH";
            cbDonViTinh.ValueMember = "MADONVITINH";
        }
        private void LoadComboboxDangBaoChe()
        {
            List<DTO_DangBaoChe> danhSachDangBaoChe = bll_tracuuthuoc.ComboboxDangBaoChe();
            danhSachDangBaoChe.Insert(0, new DTO_DangBaoChe { TenDangBaoChe = "Tất cả (Dạng bào chế)", MaDangBaoChe = "" });
            cbDangBaoChe.DataSource = danhSachDangBaoChe;
            cbDangBaoChe.DisplayMember = "TENDANGBAOCHE";
            cbDangBaoChe.ValueMember = "MADANGBAOCHE";
            flag = true;
        }
        private void LoadDataGridViewDanhSachThuoc()
        {
            dgvDanhSachThuoc.DataSource = null;
            dgvDanhSachThuoc.DataSource = bll_tracuuthuoc.LoadDataGridView();
            dgvDanhSachThuoc.Columns["MALOAI"].Visible = false;
            dgvDanhSachThuoc.Columns["MADONVITINH"].Visible = false;
            dgvDanhSachThuoc.Columns["MADANGBAOCHE"].Visible = false;
            dgvDanhSachThuoc.Columns["MANHACUNGCAP"].Visible = false;
        }
        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            //if (!flag) return;
            //dgvDanhSachThuoc.DataSource = null;
            dgvDanhSachThuoc.DataSource = bll_tracuuthuoc.LocThuoc(txtTimKiem.Text.Trim().ToLower(), cbTenLoai.SelectedValue.ToString(), cbDangBaoChe.SelectedValue.ToString(), cbDonViTinh.SelectedValue.ToString());
        }

        private void cbTenLoai_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!flag) return;
            //dgvDanhSachThuoc.DataSource = null;
            dgvDanhSachThuoc.DataSource = bll_tracuuthuoc.LocThuoc(txtTimKiem.Text.Trim().ToLower(), cbTenLoai.SelectedValue.ToString(), cbDangBaoChe.SelectedValue.ToString(), cbDonViTinh.SelectedValue.ToString());
        }

        private void cbDangBaoChe_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!flag) return;
            //dgvDanhSachThuoc.DataSource = null;
            dgvDanhSachThuoc.DataSource = bll_tracuuthuoc.LocThuoc(txtTimKiem.Text.Trim().ToLower(), cbTenLoai.SelectedValue.ToString(), cbDangBaoChe.SelectedValue.ToString(), cbDonViTinh.SelectedValue.ToString());
        }

        private void cbDonViTinh_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!flag) return;
            //dgvDanhSachThuoc.DataSource = null;
            dgvDanhSachThuoc.DataSource = bll_tracuuthuoc.LocThuoc(txtTimKiem.Text.Trim().ToLower(), cbTenLoai.SelectedValue.ToString(), cbDangBaoChe.SelectedValue.ToString(), cbDonViTinh.SelectedValue.ToString());
        }
    }
}
