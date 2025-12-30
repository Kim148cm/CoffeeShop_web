using CoffeeShop_web.Models;
using System;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;

namespace CoffeeShop_web.Controllers
{
    public class SanPhamController : BaseController
    {
        DataDataContext db = new DataDataContext(
            ConfigurationManager
                .ConnectionStrings["CoffeeShopDBConnectionString"]
                .ConnectionString
        );

        // ================== TRANG BÁN HÀNG ==================
        // ADMIN + NHÂN VIÊN ĐỀU VÀO ĐƯỢC
        public ActionResult Index()
        {
            return View(db.SANPHAMs.ToList());
        }

        // ================== THÊM MÓN (CHỈ ADMIN) ==================
        public ActionResult Create()
        {
            if (Session["Quyen"].ToString() != "Admin")
                return RedirectToAction("Index");

            return View();
        }

        [HttpPost]
        public ActionResult Create(SANPHAM sp)
        {
            if (Session["Quyen"].ToString() != "Admin")
                return RedirectToAction("Index");

            db.SANPHAMs.InsertOnSubmit(sp);
            db.SubmitChanges();

            return RedirectToAction("Index");
        }

        // ================== SỬA MÓN (CHỈ ADMIN) ==================
        public ActionResult Edit(int id)
        {
            if (Session["Quyen"].ToString() != "Admin")
                return RedirectToAction("Index");

            var sp = db.SANPHAMs.FirstOrDefault(x => x.MaSanPham == id);
            return View(sp);
        }

        [HttpPost]
        public ActionResult Edit(SANPHAM sp)
        {
            if (Session["Quyen"].ToString() != "Admin")
                return RedirectToAction("Index");

            var s = db.SANPHAMs.First(x => x.MaSanPham == sp.MaSanPham);
            s.TenSanPham = sp.TenSanPham;
            s.Gia = sp.Gia;

            db.SubmitChanges();
            return RedirectToAction("Index");
        }


        // ================== XOÁ MÓN (CHỈ ADMIN) ==================
        public ActionResult Delete(int id)
        {
            if (Session["Quyen"].ToString() != "Admin")
                return RedirectToAction("Index");

            var sp = db.SANPHAMs.FirstOrDefault(x => x.MaSanPham == id);
            db.SANPHAMs.DeleteOnSubmit(sp);
            db.SubmitChanges();

            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult ThanhToan(decimal tongTien)
        {
            HOADON hd = new HOADON
            {
                NgayLap = DateTime.Now,
                TongTien = tongTien
            };

            db.HOADONs.InsertOnSubmit(hd);
            db.SubmitChanges();

            // hd.MaHoaDon lúc này đã có (1,2,3,...)
            ViewBag.MaHoaDon = hd.MaHoaDon;

            return RedirectToAction("Index");
        }

    }
}
