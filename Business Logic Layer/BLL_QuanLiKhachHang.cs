using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using Data_Access_Layer;
using Data_Transfer_Object___Models;

namespace Business_Logic_Layer
{
    public class BLL_QuanLiKhachHang
    {
        private List<DTO_KhachHang> danhSachKhachHangCSDL;
        DAL_KhachHang dal_khachhang = new DAL_KhachHang();
        public List<DTO_KhachHang> LoadDanhSachKhachHang()
        {
            danhSachKhachHangCSDL = dal_khachhang.DocKhachHangDAL();
            return danhSachKhachHangCSDL;
        }
        public List<DTO_KhachHang> TimKiem(string hovaten)
        {
            if (!string.IsNullOrEmpty(hovaten))
            {
                List<DTO_KhachHang> danhSachKhachHangTimKiem = danhSachKhachHangCSDL.Where(t => t.HoVaTen != null && t.HoVaTen.ToLower().Trim().StartsWith(hovaten)).ToList();
                return danhSachKhachHangTimKiem;
            }
            return danhSachKhachHangCSDL;
        }
        public DTO_KhachHang TimKiemKhachHangTonTai(string makhachhang)
        {
            DTO_KhachHang khachHang = danhSachKhachHangCSDL.Find(t => makhachhang == t.MaKhachHang);
            return khachHang;
        }
        private string ThemMaKhachHang()
        {
            string maKhachHang = "KH";
            if (danhSachKhachHangCSDL.Count() != 0)
            {
                string maKhachHangCuoi = danhSachKhachHangCSDL[danhSachKhachHangCSDL.Count() - 1].MaKhachHang.Substring(2);
                int soHienTai = int.Parse(maKhachHangCuoi);
                int soMoi = soHienTai + 1;
                return maKhachHang + soMoi.ToString("D3");
            }
            return "KH001";
        }
        public void ThemKhachHang(DTO_KhachHang khachhang)
        {
            khachhang.MaKhachHang = ThemMaKhachHang();
            danhSachKhachHangCSDL.Add(khachhang);
            dal_khachhang.ThemKhachHangDAL(khachhang);
        }

        public void XoaKhachHang(string makhachhang)
        {
            dal_khachhang.XoaKhachHangDAL(makhachhang);
        }

        public void SuaKhachHang(DTO_KhachHang khachhang)
        {
            dal_khachhang.SuaKhachHangDAL(khachhang);
        }

        public bool KiemTraNhapLieu(DTO_KhachHang khachhang)
        {
            if (string.IsNullOrEmpty(khachhang.HoVaTen) ||
                string.IsNullOrEmpty(khachhang.Tuoi) ||
                string.IsNullOrEmpty(khachhang.GioiTinh) ||
                string.IsNullOrEmpty(khachhang.SoTheBHYT) ||
                string.IsNullOrEmpty(khachhang.SoCMND) ||
                string.IsNullOrEmpty(khachhang.DiaChi))
            {
                return true;
            }
            return false;
        }

        public string CapNhatTongKhachHang()
        {
            return danhSachKhachHangCSDL.Count().ToString();
        }
    }
}
