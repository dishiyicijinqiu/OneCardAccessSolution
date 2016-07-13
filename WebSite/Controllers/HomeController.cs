using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FengSharp.OneCardAccess.WebSite.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
            //return View(GetCompanyInfos());
        }

        //public ActionResult HtmlEditorPartialView()
        //{
        //    return PartialView("HtmlEditorPartialView");
        //    //return PartialView("HtmlEditorPartialView",
        //    //   GetCompanyInfos());
        //}

        public IEnumerable<ComapnyInfo> GetCompanyInfos()
        {
            for (int i = 0; i < 20; i++)
            {
                yield return new ComapnyInfo() { CustomerID = Guid.NewGuid().ToString(), City = string.Format("a{0}", i), CompanyName = "b", ContactName = "c", ContactTitle = "d", Phone = "e" };
            }
        }
    }
    public class ComapnyInfo
    {
        public string CustomerID { get; set; }
        public string ContactName { get; set; }
        public string CompanyName { get; set; }
        public string ContactTitle { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
    }
}