namespace SimpleBookingSystemService.Models
{
    public class CreateBookingResponseModel
    {
        public int BookingId { get; }
        public CreateBookingResponseModel(int bookingId) 
        { 
            BookingId = bookingId;
        }
    }
}
