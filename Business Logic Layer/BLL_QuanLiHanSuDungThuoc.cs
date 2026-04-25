using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Access_Layer;
using Data_Transfer_Object___Models;

namespace Business_Logic_Layer
{
    public class BLL_QuanLiHanSuDungThuoc
    {
        private List<DTO_Thuoc> danhSachThuocHienThiCSDL;
        DAL_Thuoc dal_thuoc = new DAL_Thuoc();
        public List<DTO_Thuoc> LoadDanhSachThuocHienThi()
        {
            danhSachThuocHienThiCSDL = dal_thuoc.DocThuocHienThiDAL();
            return danhSachThuocHienThiCSDL;
        }
        public List<int> CapNhatHanSuDungThuoc()
        {
            DateTime ngayHienTai = DateTime.Now.Date;
            int tongHetHan = 0;
            int tongSapHetHan = 0;
            int tongConHan = danhSachThuocHienThiCSDL.Count();
            foreach (DTO_Thuoc thuoc in danhSachThuocHienThiCSDL)
            {
                int soNgayConLai = (thuoc.NgayHetHan - ngayHienTai).Days;
                if (soNgayConLai <= 0)
                {
                    tongHetHan += 1;
                }
                else if (soNgayConLai <= 10)
                {
                    tongSapHetHan += 1;
                }
            }
            tongConHan -= (tongSapHetHan + tongHetHan);
            List<int> ketQuaTong = new List<int>();
            ketQuaTong.Add(tongConHan);
            ketQuaTong.Add(tongSapHetHan);
            ketQuaTong.Add(tongHetHan);
            return ketQuaTong;
        }
    }
}