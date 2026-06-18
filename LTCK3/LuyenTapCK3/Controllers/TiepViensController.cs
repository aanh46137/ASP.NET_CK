using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.DynamicData;
using System.Web.Management;
using System.Web.Mvc;
using LuyenTapCK3.Models;

namespace LuyenTapCK3.Controllers
{
    public class TiepViensController : Controller
    {
        private TiepVienContext db = new TiepVienContext();

        [ChildActionOnly]
        public PartialViewResult BoPhanMenu()
        {
            var list = db.BoPhan.ToList();
            return PartialView(list);
        }

        [Route("emps/empbydept/{MaBoPhan}")]
        public ActionResult ListEmployeeByMaBoPhan(int MaBoPhan)
        {
            var list = db.TiepVien.Where( e => e.MaBoPhan == MaBoPhan ).ToList();
            return View(list);
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login( string hoten, string matkhau)
        {
            var user = db.TiepVien.Where( u => u.HoTen == hoten && u.MatKhau == matkhau).FirstOrDefault();
            if (user == null)
            {
                ViewBag.errMsg = "Sai ho ten hoac mat khau";
                return View("Login");
            }else
            {
                Session["hoten"] = hoten;
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Logout()
        {
            Session["hoten"] = null;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult LuongMenu()
        {
            var ds = db.TiepVien.Where( x => x.Luong >= 1600 ).ToList();
            ViewBag.TongLuong = ds.Sum( tv => tv.Luong );
            return View(ds);
        }

        // GET: TiepViens
        public ActionResult Index()
        {
            var tiepVien = db.TiepVien.Include(t => t.BoPhan);
            return View(tiepVien.ToList());
        }

        // GET: TiepViens/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TiepVien tiepVien = db.TiepVien.Find(id);
            if (tiepVien == null)
            {
                return HttpNotFound();
            }
            return View(tiepVien);
        }

        // GET: TiepViens/Create
        public ActionResult Create()
        {
            ViewBag.MaBoPhan = new SelectList(db.BoPhan, "MaBoPhan", "TenBoPhan");
            return View();
        }

        // POST: TiepViens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(TiepVien emp)
        {
            try
            {
                
                db.TiepVien.Add(emp);
                db.SaveChanges();
                return Json(new { result = true, JsonRequestBehavior.AllowGet });
            }
            catch(Exception er)
            {
                return Json(new { result = false, error = er.Message });
            }

        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "MaTV,MatKhau,HoTen,NgaySinh,Luong,SoLuongChuyenBay,MaBoPhan")] TiepVien tiepVien)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.TiepVien.Add(tiepVien);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.MaBoPhan = new SelectList(db.BoPhan, "MaBoPhan", "TenBoPhan", tiepVien.MaBoPhan);
        //    return View(tiepVien);
        //}

        // GET: TiepViens/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TiepVien tiepVien = db.TiepVien.Find(id);
            if (tiepVien == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaBoPhan = new SelectList(db.BoPhan, "MaBoPhan", "TenBoPhan", tiepVien.MaBoPhan);
            return View(tiepVien);
        }

        // POST: TiepViens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit(TiepVien emp)
        {
            //db.Entry(tiepVien).State = EntityState.Modified;
            //db.SaveChanges();
            //return RedirectToAction("Index");
            try
            {
                db.Entry(emp).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { result = true, JsonRequestBehavior.AllowGet });
            }
            catch (Exception er)
            {
                return Json(new { result = false, error = er.Message });
            }
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "MaTV,MatKhau,HoTen,NgaySinh,Luong,SoLuongChuyenBay,MaBoPhan")] TiepVien tiepVien)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(tiepVien).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.MaBoPhan = new SelectList(db.BoPhan, "MaBoPhan", "TenBoPhan", tiepVien.MaBoPhan);
        //    return View(tiepVien);
        //}

        // GET: TiepViens/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TiepVien tiepVien = db.TiepVien.Find(id);
            if (tiepVien == null)
            {
                return HttpNotFound();
            }
            return View(tiepVien);
        }

        // POST: TiepViens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TiepVien tiepVien = db.TiepVien.Find(id);
            db.TiepVien.Remove(tiepVien);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
