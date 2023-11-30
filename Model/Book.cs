using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuryoyoBibliotek.Model
{
    internal class Book
    {
        public Guid BookID { get; set; }

        [MaxLength(30)]
        public string BookTitle { get; set; }
        public bool BorrowedBook { get; set; }
        public int RentalYear { get; set; }
        public DateTime? RentalDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        private int restrictedGrades;

        public int BookGrade
        {
            get => restrictedGrades;
            set
            {
                if (value < 1 || value > 5) throw new ArgumentOutOfRangeException(nameof(value));
                restrictedGrades = value;
            }
        }


        public Book()
        {
            
        }

        public int UserID { get; set; }
        public User? Users { get; set; }

        public ICollection<Author> Authors {  get; set; }
    }
}
