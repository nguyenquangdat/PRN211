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

       Dictionary<Product, int> cart = null;
       
        public ActionResult addToCart(int productID)
        {
            // lấy sản phẩm từ web
            var detail = sugasContext.Products.SingleOrDefault(n => n.ProductID == productID);
            if (detail == null)
            {
               Response.StatusCode = 404;
                return null;
               //ViewBag.error = "aaaa";
            }
            else
            {
                cart = (Dictionary<Product, int>)Session["Cart"];
                if (cart == null)
                {
                    cart = new Dictionary<Product, int>();
                    cart.Add(detail, 1);
                }
                else
                { //nếu sản phẩm có trong kho thì add to cart
                    for (int index = 0; index < cart.Count; index++)
                    {
                        var item =  cart.ElementAt(index);
                        var itemKey = item.Key;
                        var itemValue = item.Value;
                        if(itemKey.ProductID == productID)
                        {
                            cart[itemKey]++;
                            return RedirectToAction("Index");
                        }
                    }
                     
                    cart.Add(detail, 1);
                }

            }
            Session.Add("Cart", cart);
            return View();
          //  return RedirectToAction("Index");
            //
        }
   

        public ActionResult removeFromCart(int productID)
        {
            // lấy sản phẩm từ web
            var detail = sugasContext.Products.SingleOrDefault(n => n.ProductID == productID);
            if (detail == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            else
            {
                cart = (Dictionary<Product, int>)Session["Cart"];
                if (cart != null)
                {
                    for (int index = 0; index < cart.Count; index++)
                    {
                        var item = cart.ElementAt(index);
                        var itemKey = item.Key;
                        var itemValue = item.Value;
                        if (itemKey.ProductID == productID)
                        {
                            cart.Remove(item.Key);                            
                        }
                    }                    
                }               
            }
            Session.Add("Cart", cart);
            return RedirectToAction("Index");
            //
        }


        public ActionResult checkOut()
        {
            User u = (User)Session["use"];
            if(u != null )
            {
                var cart = (Dictionary<Product, int>)Session["Cart"];
                if(cart != null)
                {

                
                for (int index = 0; index < cart.Count; index++)
                {
                    var item = cart.ElementAt(index);
                    var itemKey = item.Key;
                    var itemValue = item.Value;
                    Order o = new Order();
                    o.pid = itemKey.ProductID;
                    o.price = itemKey.ProductPrice;
                    o.quantity = itemValue;
                    o.time = DateTime.Now;
                    o.uid = u.UserID;
                    o.total =  itemValue * (int)itemKey.ProductPrice;
                    o.status = "Pending";
                    sugasContext.Orders.Add(o);
                    sugasContext.SaveChanges();
                    }
                    
                    Session.Remove("Cart");
                return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return RedirectToAction("Login", "User");
            }             
        }
        public ActionResult GetLastestProduct1(int id)
        {
          
            return View("addToCart");
        }


    }
}