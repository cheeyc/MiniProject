using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ContactsController : Controller
    {
        private DbModel db = new DbModel();

        // GET: Contacts
        public ActionResult Index()
        {
            return View();
        }

        // GET: Contacts/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Contacts/Create
        public ActionResult Create()
        {
            ViewBag.Message = string.Empty;
            return View();
        }

        // POST: Contacts/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "Id,Message")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                contact.Id = Guid.NewGuid();
                contact.TimeStamp = DateTime.Now;
                contact.UserId = "Admin";

                var user = db.Contacts.Where(t => t.UserId.Equals(contact.UserId) && contact.Message.Equals(t.Message.Trim(), StringComparison.OrdinalIgnoreCase))
                                        .OrderByDescending(t => t.TimeStamp).ToList().FirstOrDefault();
                if (user != null)
                {
                    TimeSpan diff = DateTime.Now - user.TimeStamp.Value;
                    if (diff.Hours <= 24 )
                    {
                        ModelState.AddModelError("", "Not allowed to submit the same message 2 times in one day. Please try again next day.");
                        return View(contact);
                    }
                }
                if (Request.Files.Count > 0)
                {
                    var file = Request.Files[0];

                    if (file != null && file.ContentLength > 0)
                    {
                        contact.FileName = Path.GetFileName(file.FileName);
                        var path = Path.Combine(Server.MapPath("~/Uploads/"), contact.Id.ToString());
                        file.SaveAs(path);
                    }
                }
                db.Contacts.Add(contact);
                db.SaveChanges();

                ViewBag.Message = "Saved Message :" + contact.Message;
            }

            return View(contact);
        }
    }
}
