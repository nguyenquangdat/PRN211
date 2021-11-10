using Project.DBcontext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project.Models;
using PagedList;

namespace Project.Controllers
{
    
    public class ProductController : Controller
    {
        SugasContext sugasContext = new SugasContext();
        // GET: Product
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
            int pageSize = 4;

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
        public ActionResult GetLastestProduct()
        {
            var product = sugasContext.Products.Where(p => p.IsNewProduct == true)
                .OrderByDescending(p => p.ProductDate).Take(8).ToList();
            return PartialView(product);
        }
        public ActionResult Details(int productID = 0)
        {
            var detail = sugasContext.Products.SingleOrDefault(n => n.ProductID == productID);
            if (detail == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(detail);
        }
    }
}