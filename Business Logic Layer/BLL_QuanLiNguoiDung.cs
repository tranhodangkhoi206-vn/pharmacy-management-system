using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Access_Layer;
using Data_Transfer_Object___Models;

namespace Business_Logic_Layer
{
    public class BLL_QuanLiNguoiDung
    {
        List<DTO_NguoiDung> bangNguoiDung;
        List<DTO_ChucVu> bangChucVu;
        DAL_NguoiDung dal_nguoidung = new DAL_NguoiDung();
        DAL_ChucVu dal_chucvu = new DAL_ChucVu();
        DAL_HoaDon dal_hoadon = new DAL_HoaDon();
        public List<DTO_NguoiDung> LoadDanhSachNguoiDung()
        {
            bangNguoiDung = dal_nguoidung.DocNguoiDungDAL();
            return bangNguoiDung;
        }
        public List<DTO_ChucVu> LoadDanhSachChucVu()
        {
            bangChucVu = dal_chucvu.DocChucVuDAL();
            return bangChucVu;
        }

        public void ThemNguoiDungBLL(DTO_NguoiDung nguoidung)
        {
            
            dal_nguoidung.ThemNguoiDungDAL(nguoidung);
        }
        public void SuaNguoiDungBLL(DTO_NguoiDung nguoidung)
        {
            dal_nguoidung.SuaNguoiDungDAL(nguoidung);
        }
        public void XoaNguoiDung(string manguoidung)
        {
            List<DTO_HoaDon> danhSachHoaDon = new List<DTO_HoaDon>();
            DTO_HoaDon hoaDon = danhSachHoaDon.Find(t => manguoidung == t.MaNguoiDung);
            if (hoaDon != null)
            {
                throw new Exception("Dữ liệu này đang được sử dụng trong các hóa đơn hoặc chứng từ liên quan.");
            }
            dal_nguoidung.XoaNguoiDungDAL(manguoidung);
        }
        public int TongNhanVien()
        {
            return bangNguoiDung.Count();
        }
        public DTO_NguoiDung NhapDuLieu(string manguoidung)
        {
            DTO_NguoiDung nguoiDung = bangNguoiDung.Find(t => manguoidung == t.MaNguoiDung);
            return nguoiDung;
        }
    }
}
