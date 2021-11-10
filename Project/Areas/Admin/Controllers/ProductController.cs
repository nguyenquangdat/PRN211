using Project.DBcontext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Project.Models;
using OfficeOpenXml;
using System.IO;

namespace Project.Areas.Admin.Controllers
{
    //[Authorize(Roles = "ADMIN")]
    public class ProductController : Controller
    {
        SugasContext sugasContext = new SugasContext();

        // GET: Admin/Home
        public ActionResult Index(int? page, string searchBy, string search)
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
            if (searchBy == "NameProduct")
            {
                return View(sugasContext.Products.Where(s => s.ProductName.StartsWith(search)).OrderByDescending(s => s.ProductID).ToPagedList(pageNumber, pageSize));
            }
            // 5. Trả về các sản phẩm được phân trang theo kích thước và số trang.
            return View(products.ToPagedList(pageNumber, pageSize));
        }

        // Xem chi tiết người dùng GET: Admin/Home/Details/5 
        public ActionResult Details(int id)
        {
            return View(sugasContext.Products.Where(s => s.ProductID == id).FirstOrDefault());
        }

        // Tạo sản phẩm mới phương thức GET: Admin/Home/Create
        public ActionResult Create()
        {
            Product product = new Product();
            var categorySelected = new SelectList(sugasContext.Categories, "CategoryID", "CategoryName");
            ViewBag.CategoryID = categorySelected;
            var tagSelected = new SelectList(sugasContext.Tags, "TagID", "TagName");
            ViewBag.TagID = tagSelected;
            return View(product);
        }

        // Tạo sản phẩm mới phương thức POST: Admin/Home/Create
        [HttpPost]
        public ActionResult Create(Product product)
        {
            try
            {
                if (product.ImageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(product.ImageUpload.FileName);
                    string extension = Path.GetExtension(product.ImageUpload.FileName);
                    fileName = fileName + extension;
                    product.Image = fileName;
                    product.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/ProductImage/"), fileName));
                }
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
            var categorySelected = new SelectList(sugasContext.Categories, "CategoryID", "CategoryName", product.CategoryID);
            ViewBag.CategoryID = categorySelected;
            var tagSelected = new SelectList(sugasContext.Tags, "TagID", "TagName", product.TagID);
            ViewBag.TagID = tagSelected;
            return View(product);

        }

        // POST: Admin/Home/Edit/5
        [HttpPost]
        public ActionResult Edit(Product product)
        {
            try
            {
                if (product.ImageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(product.ImageUpload.FileName);
                    string extension = Path.GetExtension(product.ImageUpload.FileName);
                    fileName = fileName + extension;
                    product.Image = fileName;
                    product.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/ProductImage/"), fileName));
                }
                // lưu lại
                sugasContext.Entry(product).State = System.Data.Entity.EntityState.Modified;
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

        public void ExportToExcel()
        {
            var products = sugasContext.Products.ToList();

            ExcelPackage.LicenseContext = LicenseContext.Commercial;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            ExcelPackage excelPackage = new ExcelPackage();
            //add new ExcelSheet
            ExcelWorksheet ws = excelPackage.Workbook.Worksheets.Add("Report");
            // process our database dât in excelSheet
            ws.Cells["A1"].Value = "Report";
            ws.Cells["B1"].Value = "Report1";

            ws.Cells["A2"].Value = "Date";
            ws.Cells["B2"].Value = string.Format("{0:dd MMM yyy} at {0:H: mm tt}", DateTimeOffset.Now);

            ws.Cells["A5"].Value = "ProductID";
            ws.Cells["B5"].Value = "ProductDescription";
            ws.Cells["C5"].Value = "ProductPrice";
            ws.Cells["D5"].Value = "ProductName";
            ws.Cells["E5"].Value = "Stock";
            ws.Cells["F5"].Value = "IsNewProduct";
            ws.Cells["G5"].Value = "ProductDate";
            ws.Cells["H5"].Value = "Image";
            ws.Cells["I5"].Value = "TagID";

            int rows = 6;
            foreach(var item in products)
            {
                ws.Cells[String.Format("A{0}", rows)].Value = item.ProductID;
                ws.Cells[String.Format("B{0}", rows)].Value = item.ProductDescription;
                ws.Cells[String.Format("C{0}", rows)].Value = item.ProductPrice;
                ws.Cells[String.Format("D{0}", rows)].Value = item.ProductName;
                ws.Cells[String.Format("E{0}", rows)].Value = item.Stock;
                ws.Cells[String.Format("F{0}", rows)].Value = item.IsNewProduct;
                ws.Cells[String.Format("G{0}", rows)].Value = item.ProductDate;
                ws.Cells[String.Format("H{0}", rows)].Value = item.Image;
                ws.Cells[String.Format("I{0}", rows)].Value = item.TagID;
                rows++;

            }

            ws.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposittion", "attachment: filename=" + "ExcelReport.xlsx");
            Response.BinaryWrite(excelPackage.GetAsByteArray());
            Response.End();
        }
    }
}