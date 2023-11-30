using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SuryoyoBibliotek.Model;

namespace SuryoyoBibliotek.Data
{
    internal class Context : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost; Database=NewtonLibrary_Josef; " +
                "Trusted_Connection=True; Trust Server Certificate =Yes; " +
                "User Id=NewtonLibrary password=NewtonLibrary");
        }
    }
}
