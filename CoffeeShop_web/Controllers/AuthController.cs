using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using CoffeeShop_web.Models;

namespace CoffeeShop_web.Controllers
{
    public class AuthController : Controller
    {
        DataDataContext db = new DataDataContext(
            ConfigurationManager
                .ConnectionStrings["CoffeeShopDBConnectionString"]
                .ConnectionString
        );

        // ================= LOGIN =================
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string tenDangNhap, string matKhau)
        {
            if (string.IsNullOrWhiteSpace(tenDangNhap) ||
                string.IsNullOrWhiteSpace(matKhau))
            {
                ViewBag.Error = "Vui lòng nhập đầy đủ!";
                return View();
            }

            var tk = db.TAIKHOANs.FirstOrDefault(x =>
                x.TenDangNhap == tenDangNhap &&
                x.MatKhau == matKhau
            );

            if (tk == null)
            {
                ViewBag.Error = "Sai tài khoản hoặc mật khẩu!";
                return View();
            }

            Session["Username"] = tk.TenDangNhap;
            Session["Quyen"] = tk.Quyen;
            Session["User"] = tk;

            return RedirectToAction("Index", "SanPham");
        }

        // ================= LOGOUT =================
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
