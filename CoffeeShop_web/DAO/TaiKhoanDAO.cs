using CoffeeShop_web.Models;
using System.Configuration;
using System.Linq;

namespace CoffeeShop_web.DAO
{
    public class TaiKhoanDAO
    {
        DataDataContext db;

        public TaiKhoanDAO()
        {
            db = new DataDataContext(
                ConfigurationManager
                    .ConnectionStrings["CoffeeShopDBConnectionString"]
                    .ConnectionString
            );
        }

        public IQueryable<TAIKHOAN> LayDanhSach()
        {
            return db.TAIKHOANs;
        }

        public bool KiemTraTonTai(string tenDangNhap)
        {
            return db.TAIKHOANs.Any(x => x.TenDangNhap == tenDangNhap);
        }

        public void ThemNhanVien(string tenDangNhap, string matKhau)
        {
            TAIKHOAN tk = new TAIKHOAN
            {
                TenDangNhap = tenDangNhap,
                MatKhau = matKhau,
                Quyen = "NhanVien"
            };

            db.TAIKHOANs.InsertOnSubmit(tk);
            db.SubmitChanges();
        }

        public void Xoa(int id)
        {
            var tk = db.TAIKHOANs.FirstOrDefault(x => x.MaTaiKhoan == id);
            if (tk != null)
            {
                db.TAIKHOANs.DeleteOnSubmit(tk);
                db.SubmitChanges();
            }
        }
    }
}
