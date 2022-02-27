using System;

namespace SimpleBookingSystemService.Models
{
    public class CreateBookingRequestModel
    {
        public int ResourceId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int Quantity { get; set; }
    }
}
