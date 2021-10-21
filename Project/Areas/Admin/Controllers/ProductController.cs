using Project.DBcontext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Project.Models;

namespace Project.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        SugasContext sugasContext = new SugasContext();

        // GET: Admin/Home
        public ActionResult Index(int? page)
        {
            // 1. Tham số int? dùng để thể hiện null và kiểu int(số nguyên)
            // page có thể có giá trị là null ( rỗng) và kiểu int.

            // 2. Nếu page = null thì đặt lại là 1.
            if (page == null) page = 1;

            // 3. Tạo querry sql, lưu ý phải sắp xếp theo trường nào đó, ví dụ OrderBy
            // theo ProductID mới có thể phân trang.
            var products = sugasContext.Products.OrderBy(x => x.ProductID);

            // 4. Tạo kích thước trang (pageSize) hay là số sản phẩm hiển thị trên 1 trang
            int pageSize = 5;

            // 4.1 Toán tử ?? trong C# mô tả nếu page khác null thì lấy giá trị page, còn
            // nếu page = null thì lấy giá trị 1 cho biến pageNumber.
            int pageNumber = (page ?? 1);

            // 5. Trả về các sản phẩm được phân trang theo kích thước và số trang.
            return View(products.ToPagedList(pageNumber, pageSize));
        }

        // Xem chi tiết người dùng GET: Admin/Home/Details/5 
        public ActionResult Details(int id)
        {
            var productDetails = sugasContext.Products.Find(id);
            return View(productDetails);
        }

        // Tạo sản phẩm mới phương thức GET: Admin/Home/Create
        public ActionResult Create()
        {
            //Để tạo dropdownList bên view
            var categoryselected = new SelectList(sugasContext.Categories, "CategoryID", "CategoryName");
            ViewBag.CategoryID = categoryselected;
            var tagselected = new SelectList(sugasContext.Tags, "TagID", "TagName");
            ViewBag.TagID = tagselected;
            return View();
        }

        // Tạo sản phẩm mới phương thức POST: Admin/Home/Create
        [HttpPost]
        public ActionResult Create(Product product)
        {
            try
            {
                product.ProductDate = DateTime.Now;
                //Thêm  sản phẩm mới
                sugasContext.Products.Add(product);
                // Lưu lại
                sugasContext.SaveChanges();
                // Thành công chuyển đến trang index
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // Sửa sản phẩm GET lấy ra ID sản phẩm: Admin/Home/Edit/5
        public ActionResult Edit(int id)
        {
            // Hiển thị dropdownlist
            var product = sugasContext.Products.Find(id);
            var categoryselected = new SelectList(sugasContext.Categories, "CategoryID", "CategoryName", product.CategoryID);
            ViewBag.Mahang = categoryselected;
            var taghselected = new SelectList(sugasContext.Tags, "TagID", "TagName", product.TagID);
            ViewBag.Mahdh = taghselected;
            // 
            return View(product);

        }

        // POST: Admin/Home/Edit/5
        [HttpPost]
        public ActionResult Edit(Product product)
        {
            try
            {
                // Sửa sản phẩm theo mã sản phẩm
                var oldItem = sugasContext.Products.Find(product.ProductID);
                oldItem.ProductName = product.ProductName;
                oldItem.ProductPrice = product.ProductPrice;
                oldItem.Stock = product.Stock;
                oldItem.ProductDescription = product.ProductDescription;
                oldItem.Image = product.Image;
                oldItem.ProductDate = product.ProductDate;
                oldItem.CategoryID = product.CategoryID;
                oldItem.TagID = product.TagID;
                // lưu lại
                sugasContext.SaveChanges();
                // xong chuyển qua index
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }

        }


        // Xoá sản phẩm phương thức GET: Admin/Home/Delete/5
        public ActionResult Delete(int id)
        {
            var product = sugasContext.Products.Find(id);
            return View(product);
        }

        // Xoá sản phẩm phương thức POST: Admin/Home/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                //Lấy được thông tin sản phẩm theo ID(mã sản phẩm)
                var product = sugasContext.Products.Find(id);
                // Xoá
                sugasContext.Products.Remove(product);
                // Lưu lại
                sugasContext.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}