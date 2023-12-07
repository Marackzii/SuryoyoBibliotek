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



        private bool _borrowed; //_loaned

        public bool Loaned //Borrowed
        {
            get => LoanCardId.HasValue;
            set
            {
                if (value && !_loanDate.HasValue)
                {
                    _loanDate = DateTime.Now;
                    ReturnDate = _loanDate?.AddDays(10);
                }
                else if (!value)
                {
                    _loanDate = null;
                    ReturnDate = null;
                }
            }
        }


        private DateTime? _loanDate;

        public DateTime? LoanDate
        {
            get => _loanDate;
            set
            {
                _loanDate = value;

                if (Loaned && _loanDate == null)
                {
                    // Book is still loaned, update _loanDate and ReturnDate
                    _loanDate = DateTime.Now;
                    ReturnDate = _loanDate?.AddDays(10);
                }
                else if (!Loaned)
                {
                    // Book is not loaned, reset _loanDate and ReturnDate
                    _loanDate = null;
                    ReturnDate = null;
                }
            }
        }


        private int? _loanCardId; //rent_CardId;
        public int? LoanCardId //RentCardId 
        {
            get => _loanCardId;
            set
            {
                _loanCardId = value;

                if (value == null)
                {
                    _loanDate = null;
                    ReturnDate = null;
                }
                else if (Loaned)
                {
                    _loanDate = DateTime.Now;
                    ReturnDate = _loanDate?.AddDays(10);
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
