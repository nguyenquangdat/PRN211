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

        //public ActionResult _CategoriesPartial()
        //{
           
        //    return PartialView(sugasContext.Categories.ToList());
        //}
    }
}