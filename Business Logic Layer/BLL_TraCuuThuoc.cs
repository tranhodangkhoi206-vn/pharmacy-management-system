using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Access_Layer;
using Data_Transfer_Object___Models;

namespace Business_Logic_Layer
{
    public class BLL_TraCuuThuoc
    {
        private List<DTO_Thuoc> danhSachThuocCSDL;
        DAL_LoaiThuoc dal_loaithuoc = new DAL_LoaiThuoc();
        DAL_DonViTinh dal_donvitinh = new DAL_DonViTinh();
        DAL_DangBaoChe dal_dangbaoche = new DAL_DangBaoChe();
        DAL_Thuoc dal_thuoc = new DAL_Thuoc();
        public void LoadDanhSachThuocCSDL()
        {
            danhSachThuocCSDL = dal_thuoc.DocThuocHienThiDAL();
            //danhSachThuocLoc = danhSachThuocCSDL;
        }
        public List<DTO_Thuoc> LoadDataGridView()
        {
            return danhSachThuocCSDL;
        }

        public List<DTO_LoaiThuoc> ComboboxLoaiThuoc()
        {
            return dal_loaithuoc.DocLoaiThuocDAL();
        }
        public List<DTO_DonViTinh> ComboboxDonViTinh()
        {
            return dal_donvitinh.DocDonViTinhDAL();
        }
        public List<DTO_DangBaoChe> ComboboxDangBaoChe()
        {
            return dal_dangbaoche.DocDangBaoCheDAl();
        }
        public List<DTO_Thuoc> LocThuoc(string tukhoa, string tenloai, string dangbaoche, string donvitinh)
        {
            List<DTO_Thuoc> danhSachThuocLoc = danhSachThuocCSDL;
            if (!string.IsNullOrEmpty(tukhoa))
            {
                danhSachThuocLoc = danhSachThuocLoc.Where(t => t.TenThuoc != null && t.TenThuoc.ToLower().StartsWith(tukhoa)).ToList();
            }

            if (!string.IsNullOrEmpty(tenloai))
            {
                danhSachThuocLoc = danhSachThuocLoc.Where(t => t.MaLoai != null && t.MaLoai.Equals(tenloai)).ToList();
            }
            if (!string.IsNullOrEmpty(dangbaoche))
            {
                danhSachThuocLoc = danhSachThuocLoc.Where(t => t.MaDangBaoChe != null && t.MaDangBaoChe.Equals(dangbaoche)).ToList();
            }
            if (!string.IsNullOrEmpty(donvitinh))
            {
                danhSachThuocLoc = danhSachThuocLoc.Where(t => t.MaDonViTinh != null && t.MaDonViTinh.Equals(donvitinh)).ToList();
            }
            return danhSachThuocLoc;
        }
    }
}