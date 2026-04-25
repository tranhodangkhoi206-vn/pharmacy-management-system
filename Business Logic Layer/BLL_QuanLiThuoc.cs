using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Instrumentation;
using System.Net.Http.Headers;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Remoting.Proxies;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Data_Access_Layer;
using Data_Transfer_Object___Models;
using static System.Net.Mime.MediaTypeNames;

namespace Business_Logic_Layer
{
    public class BLL_QuanLiThuoc
    {
        private List<DTO_Thuoc> danhSachThuocCSDL = new List<DTO_Thuoc>();
        DAL_Thuoc dal_thuoc = new DAL_Thuoc();
        DAL_LoaiThuoc dal_loaithuoc = new DAL_LoaiThuoc();
        DAL_DonViTinh dal_donvitinh = new DAL_DonViTinh();
        DAL_DangBaoChe dal_dangbaoche = new DAL_DangBaoChe();
        DAL_NhaCungCap dal_nhacungcap = new DAL_NhaCungCap();

        public List<DTO_Thuoc> LoadDanhSachThuocHienThi()
        {
            danhSachThuocCSDL = dal_thuoc.DocThuocHienThiDAL();
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
        public List<DTO_NhaCungCap> ComboboxNhaCungCap()
        {
            return dal_nhacungcap.DocNhaCungCapDAL();
        }
        public int CapNhatTongThuoc()
        {
            return danhSachThuocCSDL.Count();
        }
        public int CapNhatTongSoLuongThuoc()
        {
            int tong = 0;
            foreach (DTO_Thuoc thuoc in danhSachThuocCSDL)
            {
                tong += thuoc.SoLuongTon;
            }
            return tong;
        }

        public bool KiemTraNhapLieu(string tenthuoc, string donvitinh, string dangbaoche, string loai, string nhacungcap)
        {
            if (string.IsNullOrEmpty(tenthuoc) ||
                string.IsNullOrEmpty(donvitinh) ||
                string.IsNullOrEmpty(dangbaoche) ||
                string.IsNullOrEmpty(loai) ||
                string.IsNullOrEmpty(nhacungcap))
            {
                return false;
            }
            return true;
        }

        public DTO_Thuoc TimKiemThuocTonTai(string mathuoc)
        {
            DTO_Thuoc thuoc = danhSachThuocCSDL.Find(t => mathuoc == t.MaThuoc);
            return thuoc;
        }

        public List<DTO_Thuoc> TimKiem(string tenthuoc)
        {
            if (!string.IsNullOrEmpty(tenthuoc))
            {
                List<DTO_Thuoc> danhSachThuocTimKiem = danhSachThuocCSDL.Where(t => t.TenThuoc != null && t.TenThuoc.ToLower().Trim().StartsWith(tenthuoc)).ToList();
                return danhSachThuocTimKiem;
            }
            return danhSachThuocCSDL;
        }

        private string ThemMaThuoc()
        {
            string maThuoc = "T";
            if (danhSachThuocCSDL.Count() != 0)
            {
                string maThuocCuoi = danhSachThuocCSDL[danhSachThuocCSDL.Count() - 1].MaThuoc.Substring(2);
                int soHienTai = int.Parse(maThuocCuoi);
                int soMoi = soHienTai + 1;
                return maThuoc + soMoi.ToString("D4");
            }
            return "T0001";
        }
        public void ThemThuocBLL(DTO_Thuoc thuoc)
        {
            thuoc.MaThuoc = ThemMaThuoc();
            danhSachThuocCSDL.Add(thuoc);
            dal_thuoc.ThemThuocDAL(thuoc);
        }

        public void SuaThuoc(DTO_Thuoc thuoc)
        {
            dal_thuoc.SuaThuocDAL(thuoc);
        }
        public void XoaThuoc(string mathuoc)
        {
            dal_thuoc.XoaThuocDAL(mathuoc);
        }
    }
}
