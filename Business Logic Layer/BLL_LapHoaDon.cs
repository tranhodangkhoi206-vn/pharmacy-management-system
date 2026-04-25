using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Channels;
using System.Security.AccessControl;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Data_Access_Layer;
using Data_Transfer_Object___Models;

namespace Business_Logic_Layer
{
    public class BLL_LapHoaDon
    {
        private List<DTO_GioHangHienThi> danhSachThuocTrongGio = new List<DTO_GioHangHienThi>();
        private List<DTO_GioHangHienThi> danhSachThuocGioHangCSDL;
        private List<DTO_KhachHang> danhSachMaKhachHangCSDL;
        private List<DTO_HoaDon> danhSachMaHoaDonCSDL;

        DAL_Thuoc dal_thuoc = new DAL_Thuoc();
        DAL_HoaDon dal_hoadon = new DAL_HoaDon();
        DAL_ChiTietHoaDon dal_chitiethoadon = new DAL_ChiTietHoaDon();
        DAL_HoaDonHienThi dal_hoadonhienthi = new DAL_HoaDonHienThi();

        DAL_KhachHang dal_khachhang = new DAL_KhachHang();
        BLL_DangNhap bll_dangnhap = new BLL_DangNhap();


        private decimal tongTien = 0m;
        private string maHoaDon = "";

        // <--------------- Load dữ liệu khi ứng dụng chạy --------------->
        public List<DTO_GioHangHienThi> LoadDuLieuGioHang()
        {
            return danhSachThuocTrongGio;
        }

        public void LoadDuLieuCSDL()
        {
            danhSachMaKhachHangCSDL = dal_khachhang.DocMaKhachHang();
            danhSachThuocGioHangCSDL = dal_thuoc.DocThuocGioHangDAL();
            danhSachMaHoaDonCSDL = dal_hoadon.DocMaHoaDon();
        }

        // <--------------- Xử lý Lỗi giỏ hàng --------------->
        public void LoiGioHang(string mathuoc)
        {
            if (string.IsNullOrEmpty(mathuoc))
            {
                throw new Exception("Bạn không được bỏ trống mã thuốc!");
            }
        }
        public void LoiThanhToan()
        {
            if (danhSachThuocTrongGio.Count == 0)
            {
                throw new Exception("Ban phải thêm thuốc trước khi thanh toán");
            }
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
                return false;
            }
            return true;
        }
        // <--------------- Xử lý logic giỏ hàng --------------->
        public void ThemGioHang(string mathuoc, int soluong)
        {

            DTO_GioHangHienThi gioHang = new DTO_GioHangHienThi();
            DTO_GioHangHienThi thuoc = danhSachThuocGioHangCSDL.Find(t => mathuoc == t.MaThuoc);
            if (thuoc == null)
            {
                throw new Exception("Mã thuốc không tồn tại trong hệ thống");
            }
            if (XuLiSoLuong(thuoc, soluong))
            {
                gioHang.SoLuong = soluong;
                gioHang.MaThuoc = thuoc.MaThuoc;
                gioHang.TenThuoc = thuoc.TenThuoc;
                gioHang.TenLoai = thuoc.TenLoai;
                gioHang.TenDonViTinh = thuoc.TenDonViTinh;
                gioHang.TenDangBaoChe = thuoc.TenDangBaoChe;
                gioHang.DonGia = thuoc.DonGia;
                gioHang.NgayHetHan = thuoc.NgayHetHan;
                danhSachThuocTrongGio.Add(gioHang);
            }
        }

        public void XoaGioHang(string mathuoc)
        {
            DTO_GioHangHienThi thuocCanXoa = danhSachThuocTrongGio.Find(t => mathuoc == t.MaThuoc);
            if (thuocCanXoa == null)
            {
                throw new Exception("Mã thuốc không tồn tại trong giỏ hàng");
            }
            danhSachThuocTrongGio.RemoveAll(t => t.MaThuoc == mathuoc);
        }
        public bool XuLiSoLuong(DTO_GioHangHienThi thuoc, int soluong)
        {
            if (danhSachThuocTrongGio != null)
            {
                DTO_GioHangHienThi thuocTrung = danhSachThuocTrongGio.Find(t => thuoc.MaThuoc == t.MaThuoc);

                if (thuocTrung != null)
                {
                    thuocTrung.SoLuong += soluong;
                    return false;
                }
            }
            return true;
        }

        // <--------------- Xử lý logic tổng tiền --------------->
        public string CongTien()
        {
            if (danhSachThuocTrongGio != null)
            {
                tongTien = 0m;
                foreach (DTO_GioHangHienThi gioHang in danhSachThuocTrongGio)
                {
                    decimal tongTienSanPham = 0m;
                    tongTienSanPham = gioHang.DonGia;
                    tongTienSanPham *= gioHang.SoLuong;
                    tongTien += tongTienSanPham;
                }
            }
            string chuoiDinhDang = tongTien.ToString("N0", new CultureInfo("vi-VN"));
            return chuoiDinhDang;
        }
        public string TruTien(string mathuoc)
        {
            DTO_GioHangHienThi thuocBiXoa = danhSachThuocTrongGio.Find(t => mathuoc == t.MaThuoc);

            if (thuocBiXoa != null && danhSachThuocTrongGio != null) tongTien -= (thuocBiXoa.DonGia * thuocBiXoa.SoLuong);

            string chuoiDinhDang = tongTien.ToString("N0", new CultureInfo("vi-VN"));
            return chuoiDinhDang;
        }
        // <--------------- Xử lý thông tin khách hàng --------------->
        private string ThemMaKhachHang()
        {
            string maKhachHang = "KH";
            if (danhSachMaKhachHangCSDL.Count() != 0)
            {
                string maKhachHangCuoi = danhSachMaKhachHangCSDL[danhSachMaKhachHangCSDL.Count() - 1].MaKhachHang.Substring(2);
                int soHienTai = int.Parse(maKhachHangCuoi);
                int soMoi = soHienTai + 1;
                return maKhachHang + soMoi.ToString("D3");
            }
            return "KH001";
        }
        public void ThemKhachHang(DTO_KhachHang khachhang)
        {
            khachhang.MaKhachHang = ThemMaKhachHang();
            danhSachMaKhachHangCSDL.Add(khachhang);
            dal_khachhang.ThemKhachHangDAL(khachhang);
        }
        // <--------------- Xử lý thông tin hóa đơn --------------->
        private string ThemMaHoaDon()
        {
            string maHoaDon = "HD";
            if (danhSachMaHoaDonCSDL.Count() != 0)
            {
                string maHoaDonCuoi = danhSachMaHoaDonCSDL[danhSachMaHoaDonCSDL.Count() - 1].MaHoaDon.Substring(2);
                int soHienTai = int.Parse(maHoaDonCuoi);
                int soMoi = soHienTai + 1;
                return maHoaDon + soMoi.ToString("D3");

            }
            return "HD001";
        }
        public void ThemHoaDon()
        {
            maHoaDon = ThemMaHoaDon();
            DateTime homNay = DateTime.Now;
            DTO_HoaDon hoaDon = new DTO_HoaDon();
            hoaDon.MaHoaDon = maHoaDon;
            hoaDon.MaNguoiDung = bll_dangnhap.LayMaNguoiDung();
            hoaDon.NgayTao = DateTime.Parse(homNay.ToString("yyyy-MM-dd HH:mm:ss"));
            hoaDon.TongTien = tongTien;
            dal_hoadon.ThemHoaDonDAL(hoaDon);
        }
        public void ThemChiTietHoaDon()
        {
            List<DTO_ChiTietHoaDon> danhSachChiTietHoaDon = new List<DTO_ChiTietHoaDon>();
            foreach (DTO_GioHangHienThi gioHang in danhSachThuocTrongGio)
            {
                DTO_ChiTietHoaDon chiTietHoaDon = new DTO_ChiTietHoaDon();
                chiTietHoaDon.MaHoaDon = maHoaDon;
                chiTietHoaDon.MaThuoc = gioHang.MaThuoc;
                chiTietHoaDon.DonGia = gioHang.DonGia;
                chiTietHoaDon.SoLuong = gioHang.SoLuong;
                danhSachChiTietHoaDon.Add(chiTietHoaDon);
            }
            dal_chitiethoadon.ThemChiTietHoaDon(danhSachChiTietHoaDon);
            danhSachThuocTrongGio.Clear();
        }
        // <--------------- Xử lý logic xem danh sách hóa đơn --------------->
        public List<DTO_HoaDonHienThi> LoadDanhSachHoaDon()
        {
            List<DTO_HoaDonHienThi> danhSachHoaDonHienThi = dal_hoadonhienthi.DocHoaDonHienThi();
            return danhSachHoaDonHienThi;
        }
    }
}
