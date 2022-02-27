using System;

namespace SimpleBookingSystemService.Infrastructure.Domain
{
    public class Booking
    {
        public int Id { get; set; }
        public int ResourceId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int BookedQuantity { get; set; }
        public virtual Resource Resource { get; set; }
    }
}
