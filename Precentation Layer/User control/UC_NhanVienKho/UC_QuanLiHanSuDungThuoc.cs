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
    public partial class UC_QuanLiHanSuDungThuoc : UserControl
    {
        List<DTO_Thuoc> danhSachThuocCSDL;
        BLL_QuanLiHanSuDungThuoc bll_quanlihansuhangthuoc = new BLL_QuanLiHanSuDungThuoc();
        public UC_QuanLiHanSuDungThuoc()
        {
            InitializeComponent();
            LoadDataGridView();
            LoadTongThuoc();
        }
        private void LoadDataGridView()
        {
            danhSachThuocCSDL = bll_quanlihansuhangthuoc.LoadDanhSachThuocHienThi();
            dgvDSThuoc.DataSource = danhSachThuocCSDL;
            dgvDSThuoc.Columns["MALOAI"].Visible = false;
            dgvDSThuoc.Columns["MADONVITINH"].Visible = false;
            dgvDSThuoc.Columns["MADANGBAOCHE"].Visible = false;
            dgvDSThuoc.Columns["MANHACUNGCAP"].Visible = false;
        }
        private void LoadTongThuoc()
        {
            List<int> tongSo = bll_quanlihansuhangthuoc.CapNhatHanSuDungThuoc();
            lbConHan.Text = tongSo[0].ToString();
            lbSapHet.Text = tongSo[1].ToString();
            lbHet.Text = tongSo[2].ToString();
        }
    }
}
