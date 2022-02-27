using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleBookingSystemService.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace SimpleBookingSystem.UnitTest.Booking
{
    public class CreateBookingTest
    {
        public static IEnumerable<object[]> CreateBookingRequest_ValidModel()
        {
            yield return new object[] { new CreateBookingRequestModel()
            {
                ResourceId = 1,
                Quantity = 50,
                FromDate = DateTime.Parse("24/02/2022"),
                ToDate = DateTime.Parse("25/02/2022"),
           }};
        }


        [Theory]
        [MemberData(nameof(CreateBookingRequest_ValidModel))]
        public async Task CreateBooking_ValidModel_ReturnsBookingId(CreateBookingRequestModel createBookingRequestModel)
        {
            //Arrange
            var bookingService = TestFactory.BookingServiceTestFactory();

            //Act
            var booking = await bookingService.CreateBookingAsync(createBookingRequestModel);

            //Assert
            Assert.IsTrue(booking.Id > 0);
        }
    }
}
