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

            if (page == null) page = 1;

            var products = sugasContext.Products.OrderBy(x => x.ProductID);

            int pageSize = 8;

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
        public ActionResult GetLastestProductByCategory(int? category, int? page)
        {
            if (page == null) page = 1;

            var products = sugasContext.Products.OrderBy(x => x.ProductID);

            int pageSize = 4;

            int pageNumber = (page ?? 1);

            if (category != null)
            {

                var product = sugasContext.Products.OrderByDescending(x => x.ProductDate).Where(x => x.CategoryID == category).ToPagedList(pageNumber, pageSize);
                return PartialView(product);
            }
            else
            {
                var productlist = sugasContext.Products.OrderByDescending(x => x.ProductDate).ToPagedList(pageNumber, pageSize);
                return PartialView(productlist);
            }
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
        public ActionResult ProductByCategory(int? category, int? page)
        {
            if (page == null) page = 1;

            var products = sugasContext.Products.OrderBy(x => x.ProductID);

            int pageSize = 4;

            int pageNumber = (page ?? 1);

            if (category != null)
            {

                var product = sugasContext.Products.OrderByDescending(x => x.ProductID).Where(x => x.CategoryID == category).ToPagedList(pageNumber, pageSize);
                return View(product);
            }
            else
            {
                var productlist = sugasContext.Products.OrderByDescending(x => x.ProductID).ToPagedList(pageNumber, pageSize);
                return View(productlist);
            }


        }
    }
}