using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BookingAppStoreMVC.Models
{
    public class BookContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
    }

    public class BookDbInitializer : DropCreateDatabaseAlways<BookContext>
    {
        protected override void Seed(BookContext context)
        {
            context.Books.Add(new Book() { Name = "Война и Мир", Author = "Л. Толстой", Price = 220 });
            context.Books.Add(new Book() { Name = "Отцы и Дети", Author = "И. Тургенев", Price = 180 });
            context.Books.Add(new Book() { Name = "Чайка", Author = "А. Чехов", Price = 150 });

            base.Seed(context);
        }
    }
}