using SURL.Models;
using SURL.Utility;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace SURL.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Exclude="Id")] UrlModel model)
        {
            if(ModelState.IsValid)
            {
                using(var context = new UrlModelDbContext())
                {
                    var link = context.Urls.FirstOrDefault(x => x.Url == model.Url);
                    if(link == null)
                    {
                        context.Urls.Add(model);
                        context.SaveChanges();
                        link = model;
                    }

                    var converter = new Base36();
                    string key = converter.To(link.Id);
                    TempData["newLink"] = string.Format("{0}://{1}/{2}", Request.Url.Scheme, Request.Url.Authority, key);
                }
            }
            return View(model);
        }

        public ActionResult Go(string id)
        {
            var converter = new Base36();
            int key = converter.From(id);
            using(var context = new UrlModelDbContext())
            {
                var link = context.Urls.FirstOrDefault(x => x.Id == key);
                if(link == null)
                {
                    throw new Exception("Link does not exist.");
                }
                return Redirect(link.Url);
            }
        }
    }
}