using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuryoyoBibliotek.Model
{
    internal class User
    {
        public int Id { get; set; }

        [MaxLength(30)]
        public string FirstName { get; set; }

        [MaxLength(30)]
        public string LastName { get; set; }
        public bool LibraryCard { get; set; }
        public int LibraryCardPin { get; set; } = new Random().Next(1000, 9999);


        public User()
        {
            
        }
    }
}
