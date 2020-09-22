using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Цветочный_магазин.Models;
using PagedList;


namespace Цветочный_магазин.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View(db.Flowers.OrderByDescending(fl=>fl.id).Take(12).ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        // GET: Home/Details/5
        [Authorize]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Flower flower = await db.Flowers.FindAsync(id);
            if (flower == null)
            {
                return HttpNotFound();
            }
            return View(flower);
        }


        public ActionResult AllFlowers(int? page)
        {
            int pageSize = 9;
            int pageNumber = (page ?? 1);
            return View(db.Flowers.ToList().ToPagedList(pageNumber, pageSize));
        }
    }
}