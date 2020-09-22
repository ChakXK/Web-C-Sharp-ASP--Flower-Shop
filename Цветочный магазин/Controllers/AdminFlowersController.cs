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

namespace Цветочный_магазин.Controllers
{
    public class AdminFlowersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AdminFlowers
        [Authorize]
        public async Task<ActionResult> Index()
        {
            return View(await db.Flowers.ToListAsync());
        }

        // GET: AdminFlowers/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminFlowers/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> Create([Bind(Include = "id,type,name,description,price,availability")] Flower flower,
            HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                string imagename = await UploadImage(upload);
                flower.image = imagename;
                db.Flowers.Add(flower);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(flower);
        }



        // GET: AdminFlowers/Edit/5
        [Authorize]
        public async Task<ActionResult> Edit(int? id)
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

        // POST: AdminFlowers/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> Edit([Bind(Include = "id,type,name,description,price,availability,image")] Flower flower,
            HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                if (upload != null) { 
                    await DeleteImage(flower);
                    string imagename = await UploadImage(upload);
                    flower.image =imagename;
                }
              
                db.Entry(flower).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(flower);
        }

        // GET: AdminFlowers/Delete/5
        [Authorize]
        public async Task<ActionResult> Delete(int? id)
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

        // POST: AdminFlowers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Flower flower = await db.Flowers.FindAsync(id);
            db.Flowers.Remove(flower);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private async Task<string> UploadImage(HttpPostedFileBase upload)
        {
            string fileName=null;
            if (upload != null)
            {
                 fileName = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds
                    + System.IO.Path.GetFileName(upload.FileName);
                upload.SaveAs(Server.MapPath("~/Content/userfiles/" + fileName));
            }
            return fileName;
        }

        private async Task<Flower> DeleteImage(Flower flower)
        {
            if (flower.image == null)
            {
                return null;
            }
            System.IO.File.Delete(Server.MapPath("~/Content/userfiles/" + flower.image));
            flower.image = null;
            return flower;
        }

    }
}
