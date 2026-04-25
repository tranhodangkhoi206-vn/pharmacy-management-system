using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Access_Layer;
using Data_Transfer_Object___Models;

namespace Business_Logic_Layer
{
    public class BLL_QuanLiNhaCungCap
    {
        private List<DTO_NhaCungCap> danhSachNhaCungCapCSDL;
        DAL_NhaCungCap dal_nhacungcap = new DAL_NhaCungCap();
        DAL_Thuoc dal_thuoc = new DAL_Thuoc();
        public List<DTO_NhaCungCap> LoadDanhSachNhaCungCap()
        {
            danhSachNhaCungCapCSDL = dal_nhacungcap.DocNhaCungCapDAL();
            return danhSachNhaCungCapCSDL;
        }

        public bool KiemTraNhapLieu(DTO_NhaCungCap nhacungcap)
        {
            if (string.IsNullOrEmpty(nhacungcap.MaNhaCungCap) ||
                string.IsNullOrEmpty(nhacungcap.TenNhaCungCap) ||
                string.IsNullOrEmpty(nhacungcap.TrangThai))
            {
                return true;
            }
            return false;
        }
        public DTO_NhaCungCap TimKiemNhaCungCapTonTai(string manhacungcap)
        {
            DTO_NhaCungCap nhaCungCap = danhSachNhaCungCapCSDL.Find(t => manhacungcap == t.MaNhaCungCap);
            return nhaCungCap;
        }

        public List<DTO_NhaCungCap> TimKiem(string manhacungcap)
        {
            List<DTO_NhaCungCap> danhSachNhaCungCapLoc = danhSachNhaCungCapCSDL.Where(t => t.MaNhaCungCap != null && t.MaNhaCungCap.StartsWith(manhacungcap)).ToList();
            return danhSachNhaCungCapLoc;
        }

        public void ThemNhaCungCap(DTO_NhaCungCap nhacungcap)
        {
            dal_nhacungcap.ThemNhaCungCapDAL(nhacungcap);
        }
        public void SuaNhaCungCap(DTO_NhaCungCap nhacungcap)
        {
            dal_nhacungcap.SuaNhaCungCapDAL(nhacungcap);
        }
        public void XoaNhaCungCap(string manhacungcap)
        {
            List<DTO_Thuoc> danhSachThuoc = dal_thuoc.DocThuocDAL();
            DTO_Thuoc thuoc = danhSachThuoc.Find(t => manhacungcap == t.MaNhaCungCap);
            if (thuoc == null)
                dal_nhacungcap.XoaNhaCungCap(manhacungcap);
            else
            {
                throw new Exception("Không thể xóa nhà cung cấp này vì đang có các loại thuốc thuộc về họ. Hãy xóa thuốc trước!");
            }
        }
        public string TinhTongNhaCungCap()
        {
            return danhSachNhaCungCapCSDL.Count().ToString();
        }
    }
}
