using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using CoffeeShop_web.Models;

namespace CoffeeShop_web.Controllers
{
    public class AdminController : BaseController
    {
        DataDataContext db = new DataDataContext(
            ConfigurationManager
                .ConnectionStrings["CoffeeShopDBConnectionString"]
                .ConnectionString
        );

        // 🔒 CHỈ ADMIN
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Session["Quyen"] == null || Session["Quyen"].ToString() != "Admin")
            {
                filterContext.Result = RedirectToAction("Index", "SanPham");
                return;
            }
            base.OnActionExecuting(filterContext);
        }

        // ===== DANH SÁCH NHÂN VIÊN =====
        public ActionResult NhanVien()
        {
            var ds = db.TAIKHOANs.ToList();
            return View(ds);
        }

        // ===== THÊM NHÂN VIÊN =====
        [HttpGet]
        public ActionResult ThemNhanVien()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ThemNhanVien(string tenDangNhap, string matKhau)
        {
            if (string.IsNullOrWhiteSpace(tenDangNhap) ||
                string.IsNullOrWhiteSpace(matKhau))
            {
                ViewBag.Error = "Không được để trống!";
                return View();
            }

            var tonTai = db.TAIKHOANs.Any(x => x.TenDangNhap == tenDangNhap);
            if (tonTai)
            {
                ViewBag.Error = "Tên đăng nhập đã tồn tại!";
                return View();
            }

            TAIKHOAN tk = new TAIKHOAN
            {
                TenDangNhap = tenDangNhap,
                MatKhau = matKhau,
                Quyen = "NhanVien"
            };

            db.TAIKHOANs.InsertOnSubmit(tk);
            db.SubmitChanges();

            return RedirectToAction("NhanVien");
        }

        // ===== XOÁ =====
        public ActionResult XoaNhanVien(int id)
        {
            var nv = db.TAIKHOANs.FirstOrDefault(x => x.MaTaiKhoan == id);
            if (nv != null)
            {
                db.TAIKHOANs.DeleteOnSubmit(nv);
                db.SubmitChanges();
            }
            return RedirectToAction("NhanVien");
        }
    }
}
