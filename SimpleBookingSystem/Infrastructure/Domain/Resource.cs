using System.Collections.Generic;

namespace SimpleBookingSystemService.Infrastructure.Domain
{
    public class Resource
    {
        public Resource()
        {
            Bookings = new HashSet<Booking>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
    }
}
