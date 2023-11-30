using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SuryoyoBibliotek.Data
{
    internal class Context : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost; Database=NewtonLibrary_Josef; " +
                "Trusted_Connection=True; Trust Server Certificate =Yes; " +
                "User Id=NewtonLibrary password=NewtonLibrary");
        }
    }
}
