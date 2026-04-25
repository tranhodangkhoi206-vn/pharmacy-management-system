using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Access_Layer;
using Data_Transfer_Object___Models;
using Microsoft.SqlServer.Server;

namespace Business_Logic_Layer
{
    public class BLL_CapTaiKhoan
    {
        private List<DTO_TaiKhoan> danhSachTaiKhoanCSDL;
        private List<DTO_NguoiDung> danhSachNguoiDungCSDL;
        DAL_TaiKhoan dal_taikhoan = new DAL_TaiKhoan();
        DAL_ChucVu dal_chucvu = new DAL_ChucVu();
        DAL_NguoiDung dal_nguoidung = new DAL_NguoiDung();
        public List<DTO_TaiKhoan> LoadDanhSachTaiKhoan()
        {
            danhSachTaiKhoanCSDL = dal_taikhoan.DocTaiKhoanHienThi();
            return danhSachTaiKhoanCSDL;
        }
        public List<DTO_ChucVu> LoadChucVu()
        {
            return dal_chucvu.DocChucVuDAL();
        }
        public List<DTO_NguoiDung> LoadTenNguoiDung()
        {
            danhSachNguoiDungCSDL = dal_nguoidung.DocTenNguoiDungDAL();
            return danhSachNguoiDungCSDL;
        }


        public bool KiemTraNhapLieu(DTO_TaiKhoan taikhoan)
        {
            if (string.IsNullOrEmpty(taikhoan.Username) ||
                string.IsNullOrEmpty(taikhoan.MatKhau) ||
                string.IsNullOrEmpty(taikhoan.MaNguoiDung) ||
                string.IsNullOrEmpty(taikhoan.MaChucVu))
            {
                return true;
            }
            return false;
        }

        public void TimKiemTaiKhoanTonTai(string username, string manguoidung)
        {
            DTO_TaiKhoan userName = danhSachTaiKhoanCSDL.Find(t => username == t.Username);
            if (userName != null)
            {
                throw new Exception("Username đã tồn tại!");
            }
            DTO_TaiKhoan maNguoiDung = danhSachTaiKhoanCSDL.Find(t => manguoidung == t.MaNguoiDung);

            if (maNguoiDung != null)
            {
                throw new Exception("Người dùng này đã có tài khoản rồi!");
            }
        }



        public void ThemTaiKhoan(DTO_TaiKhoan taikhoan)
        {
            dal_taikhoan.ThemTaiKhoanDAL(taikhoan);
            dal_nguoidung.SuaChucVuDAl(taikhoan.MaNguoiDung, taikhoan.MaChucVu);
        }
        public void SuaTaiKhoan(DTO_TaiKhoan taikhoan)
        {
            dal_taikhoan.SuaTaiKhoanDAL(taikhoan);
            dal_nguoidung.SuaChucVuDAl(taikhoan.MaNguoiDung, taikhoan.MaChucVu);
        }
        public void XoaTaiKhoan(string manguoidung)
        {
            List<DTO_TaiKhoan> chucVuTrung = danhSachTaiKhoanCSDL.FindAll(t => t.MaChucVu == "QL");
            DTO_TaiKhoan khongCoTaiKhoan = danhSachTaiKhoanCSDL.Find(t => manguoidung == t.MaNguoiDung);
            if (chucVuTrung.Count() == 1 && chucVuTrung[0].MaNguoiDung == manguoidung)
            {
                throw new Exception("Bạn không thể xóa vì chỉ có 1 quản lí!");
            }
            if (khongCoTaiKhoan == null)
            {
                throw new Exception("Bạn không thể xóa vì người dùng không có tài khoản!");
            }
            dal_taikhoan.XoaTaiKhoanDAL(manguoidung);
        }
        public DTO_TaiKhoan NhapThongTin(string manguoidung)
        {
            DTO_TaiKhoan taiKhoanChon = danhSachTaiKhoanCSDL.Find(t => manguoidung == t.MaNguoiDung);
            return taiKhoanChon;
        }
    }
}
