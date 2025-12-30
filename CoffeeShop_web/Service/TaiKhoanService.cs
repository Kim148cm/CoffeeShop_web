using CoffeeShop_web.DAO;
using CoffeeShop_web.Models;
using System.Collections.Generic;
using System.Linq;

namespace CoffeeShop_web.Services
{
    public class TaiKhoanService
    {
        TaiKhoanDAO dao = new TaiKhoanDAO();

        // Lấy danh sách tài khoản
        public List<TAIKHOAN> LayDanhSach()
        {
            return dao.LayDanhSach().ToList();
        }

        // Thêm nhân viên
        public string ThemNhanVien(string tenDangNhap, string matKhau)
        {
            if (string.IsNullOrWhiteSpace(tenDangNhap) ||
                string.IsNullOrWhiteSpace(matKhau))
            {
                return "Không được để trống!";
            }

            if (dao.KiemTraTonTai(tenDangNhap))
            {
                return "Tên đăng nhập đã tồn tại!";
            }

            dao.ThemNhanVien(tenDangNhap, matKhau);
            return null; // null = thành công
        }

        // Xoá nhân viên
        public void XoaNhanVien(int id)
        {
            dao.Xoa(id);
        }
    }
}
