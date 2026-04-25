using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;
using Data_Access_Layer;
using Data_Transfer_Object___Models;

namespace Business_Logic_Layer
{
    public class BLL_NhapKho
    {
        private List<DTO_NhapHang> danhSachNhapHangCSDL;

        DAL_NhapHang dal_nhaphang = new DAL_NhapHang();
        BLL_DangNhap bll_dangnhap = new BLL_DangNhap();
        DAL_Thuoc dal_thuoc = new DAL_Thuoc();
        DAL_NhaCungCap dal_nhacungcap = new DAL_NhaCungCap();

        public List<DTO_NhapHang> LoadDanhSachNhapHang()
        {
            danhSachNhapHangCSDL = dal_nhaphang.DocNhapHangDAL();
            return danhSachNhapHangCSDL;
        }

        public List<DTO_NhaCungCapCoBan> LoadNhaCungCap()
        {
            return dal_nhacungcap.DocNhaCungCaoCoBanDAL();
        }
        public List<DTO_ThuocCoBan> LoadThuoc()
        {
            return dal_thuoc.DocThuocCoBanDAL();
        }

        public DTO_NhapHang TimKiemLoHangTonTai(string malohang)
        {
            DTO_NhapHang loHang = danhSachNhapHangCSDL.Find(t => malohang == t.MaLoHang);
            return loHang;
        }

        public void ThemLoHang(DTO_NhapHang nhaphang)
        {
            List<DTO_Thuoc> danhSachThuocCSDL = dal_thuoc.DocThuocDAL();
            DTO_Thuoc thuoc = danhSachThuocCSDL.Find(t => nhaphang.MaThuoc == t.MaThuoc);
            nhaphang.MaNguoiDung = bll_dangnhap.LayMaNguoiDung();
            dal_nhaphang.ThemNhapHangDAL(nhaphang);
            dal_thuoc.CapNhatSoLuongThuocDAL(nhaphang.MaThuoc, nhaphang.SoLuongNhap + thuoc.SoLuongTon);
        }

        public bool KiemTraNhapLieu(DTO_NhapHang lohang)
        {
            if (string.IsNullOrEmpty(lohang.MaLoHang) ||
                string.IsNullOrEmpty(lohang.MaNhaCungCap) ||
                string.IsNullOrEmpty(lohang.MaThuoc))
            {
                return false;
            }
            return true;
        }
    }
}
