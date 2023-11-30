using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuryoyoBibliotek.Model
{
    internal class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool LibraryCard { get; set; }
        public int LibraryCardPin { get; set; } = new Random().Next(1000, 9999);


        public User()
        {
            
        }
    }
}
