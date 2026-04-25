using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Data_Access_Layer;
using Data_Transfer_Object___Models;
namespace Business_Logic_Layer
{
    public class BLL_DangNhap
    {
        private static string maNguoiDung;
        private List<DTO_TaiKhoan> bangTaiKhoan;
        private DAL_TaiKhoan dal_taikhoan = new DAL_TaiKhoan();

        public string KiemTraDangNhap(string username, string matkhau)
        {
            bangTaiKhoan = dal_taikhoan.DocTaiKhoanDAL();
            if (bangTaiKhoan == null) return null;
            foreach (DTO_TaiKhoan taikhoan in bangTaiKhoan)
            {
                if (username == taikhoan.Username && matkhau == taikhoan.MatKhau)
                {
                    maNguoiDung = taikhoan.MaNguoiDung;
                    return taikhoan.MaChucVu;
                }
            }
            return null;
        }
        public string LayMaNguoiDung()
        {
            return maNguoiDung;
        }
    }
}