using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeServiceCenter.Data.Model
{
    // Model for inventory log
    public class ItemRecord
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string ItemName { get; set; }
        public Guid ItemId { get; set; }
        public int Quantity { get; set; }
        public string ApprovedBy { get; set; }
        public string TakenBy { get; set; }
        public DateTime TakenOutAt { get; set; }
        public bool IsApproved { get; set; }
        public bool IsDeclined { get; set; } 

    }
}
