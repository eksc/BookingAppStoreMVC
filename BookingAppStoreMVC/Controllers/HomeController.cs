using BookingAppStoreMVC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BookingAppStoreMVC.Controllers
{
    public class HomeController : Controller
    {
        BookContext db = new BookContext();

        public string Index()
        {
            string result = "Вы не авторизованы";
            if (User.Identity.IsAuthenticated)
            {
                result = $"Ваш логин: {User.Identity.Name}";
            }
            return result;
        }

        //public ActionResult Index()
        //{
        //    var books = db.Books;
        //    ViewBag.Message = "Частичное представление";
        //    //ViewBag.Books = new List<Book>();

        //    SelectList authors = new SelectList(db.Books, "Author", "Name");
        //    ViewBag.Authors = authors;

        //    return View(books);
        //}
        
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Book book)
        {
            db.Books.Add(book);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        //public ActionResult Delete (int id)
        //{
        //    //Book book = db.Books.Find(id);
        //    //if (book != null)
        //    //{
        //    //    db.Books.Remove(b);
        //    //    db.SaveChanges();
        //    //}

        //    //Book book = new Book { Id = id };
        //    //db.Entry(book).State = EntityState.Deleted;
        //    //db.SaveChanges();
            
        //    //<img src="адрес сайта/Home/Delete/1" />

        //    return RedirectToAction("Index");
        //}

        [HttpGet]
        public ActionResult Delete(int id)
        {
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            db.Books.Remove(book);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        [HttpPost]
        public ActionResult Edit(Book book)
        {
            db.Entry(book).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult GetList()
        {
            string[] states = new string[] { "Russia", "USA", "Sweden" };
            //ViewBag.Message = "Частичное представление";
            return PartialView(states);
        }

        public ActionResult BookIndex()
        {
            var books = db.Books;

            //ViewBag.Books = new List<Book>();

            return View(books);
        }

        public ActionResult GetBook(int id)
        {
            Book book = db.Books.Find(id);
            if (book == null)
                return HttpNotFound();
            return View(book);
        }

        public async Task<ActionResult> BookList()
        {
            //var books = await Task.Run(() => 
            //{
            //    Task.Delay(5000);
            //    return db.Books;
            //});

            // up and down is equals

            var books = await db.Books.ToListAsync();
            //ViewBag.Books = books;
            return View("Index", books);
        }

        [HttpGet]
        public ActionResult Buy(int id)
        {
            ViewBag.BookId = id;
            Purchase purchase = new Purchase() { BookId = id, Person = "Неизвестно" };
            return View(purchase);
        }

        [HttpPost]
        public string Buy(Purchase purchase)
        {
            purchase.Date = DateTime.Now;
            db.Purchases.Add(purchase);
            db.SaveChanges();
            return $"Спасибо, {purchase.Person}, за покупку!";
        }

        [HttpPost]
        public string GetForm(string author)
        {
            return author;
        }

        [Authorize(Roles ="admin")]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [Authorize(Roles ="user")]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}