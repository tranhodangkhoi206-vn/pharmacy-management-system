using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Access_Layer;
using Data_Transfer_Object___Models;

namespace Business_Logic_Layer
{
    public class BLL_DuLieuForm
    {
        private List<DTO_NguoiDung> danhSachNguoiDungCSDL;
        private List<DTO_TaiKhoan> danhSachTaiKhoanCSDL;
        private DAL_NguoiDung dal_nguoidung = new DAL_NguoiDung();
        private DAL_TaiKhoan dal_taikhoan = new DAL_TaiKhoan();
        private BLL_DangNhap bll_dangnhap = new BLL_DangNhap();
        public string ThongTinForm()
        {
            danhSachNguoiDungCSDL = dal_nguoidung.DocTenNguoiDungDAL();
            danhSachTaiKhoanCSDL = dal_taikhoan.DocTaiKhoanDAL();
            if (danhSachNguoiDungCSDL == null || danhSachTaiKhoanCSDL == null) return null;
            string maNguoiDung = bll_dangnhap.LayMaNguoiDung();
            foreach (DTO_NguoiDung nguoiDung in danhSachNguoiDungCSDL)
            {
                if (maNguoiDung == nguoiDung.MaNguoiDung)
                {
                    return nguoiDung.MaNguoiDung + "," + nguoiDung.TenNguoiDung;
                }
            }
            return null;
        }
    }
}
