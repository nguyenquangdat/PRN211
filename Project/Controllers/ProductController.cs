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
            int pageSize = 4;

            // 4.1 Toán tử ?? trong C# mô tả nếu page khác null thì lấy giá trị page, còn
            // nếu page = null thì lấy giá trị 1 cho biến pageNumber.
            int pageNumber = (page ?? 1);

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

       Dictionary<Product, int> cart = null;
        public ActionResult addToCart(int productID)
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
            return RedirectToAction("Index");
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


         
    }
}