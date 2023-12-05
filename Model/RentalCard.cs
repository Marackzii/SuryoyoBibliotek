using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuryoyoBibliotek.Model
{
    internal class RentalCard
    {
        public int RentalCardId { get; set; }
        public int RentalCardPin { get; set; } = new Random().Next(1000, 9999);

        public ICollection<Book>? Books { get; set; }
    }
}
