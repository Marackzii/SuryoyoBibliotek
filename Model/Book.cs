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
        public int BookID { get; set; }

        [MaxLength(30)]
        public string? BookTitle { get; set; }
        public int? RentalYear { get; set; }
        public int Grade { get; set; } = new Random().Next(1, 5);
        public Guid Isbn { get; set; } = Guid.NewGuid();
        public DateTime? ReturnDate { get; set; }



        public bool Borrowed
        {
            get => RentCardId.HasValue;
            set
            {
                if (value && !hire_Date.HasValue)
                {
                    hire_Date = DateTime.Now;
                    ReturnDate = hire_Date?.AddDays(10);
                }
                else if (!value)
                {
                    hire_Date = null;
                    ReturnDate = null;
                }
            }
        }


        private DateTime? hire_Date;

        public DateTime? HireDate
        {
            get => hire_Date;
            set
            {
                hire_Date = value;

                if (Borrowed && hire_Date == null)
                {
                    hire_Date = DateTime.Now;
                    ReturnDate = hire_Date?.AddDays(10);
                }
                else if (!Borrowed)
                {
                    hire_Date = null;
                    ReturnDate = null;
                }
            }
        }


        private int? rent_CardId;
        public int? RentCardId 
        {
            get => rent_CardId;
            set
            {
                rent_CardId = value;

                if (value == null)
                {
                    hire_Date = null;
                    ReturnDate = null;
                }
                else if (Borrowed)
                {
                    hire_Date = DateTime.Now;
                    ReturnDate = hire_Date?.AddDays(10);
                }
            }
        }


        public Book()
        {
            
        }

        public RentalCard? RentalCards { get; set; }

        public ICollection<Author>? Authors {  get; set; }
    }
}
