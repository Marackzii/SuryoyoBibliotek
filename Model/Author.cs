using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuryoyoBibliotek.Model
{
    internal class Author
    {
        public int AuthorId { get; set; }
        
        [MaxLength(30)]
        public string Name { get; set; }


        public Author()
        {
            
        }

    }
}
