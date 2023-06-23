using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeServiceCenter.Data.Model
{
    // Model for items
    public class Item
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime LastTakenOut { get; set; }

    }
}
