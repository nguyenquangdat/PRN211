using Project.DBcontext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Controllers
{

    public class CategoryController : Controller
    {
        SugasContext sugasContext = new SugasContext();
        // GET: Category

        public ActionResult Index()
        {

            return View();

        }
        public PartialViewResult GetCategory()
        {
            var category = sugasContext.Categories.OrderBy(x => x.CategoryName).ToList();


            return PartialView(category);
        }
        public PartialViewResult GetLatestCategory()
        {
            var category = sugasContext.Categories.OrderBy(x => x.CategoryName).ToList();


            return PartialView(category);
        }
    }
}