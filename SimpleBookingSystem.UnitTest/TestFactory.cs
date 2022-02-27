using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;

namespace SimpleBookingSystem.UnitTest
{
    public static class TestFactory
    {
        public static SimpleBookingSystemService.Infrastructure.Services.ResourceService ResourceServiceTestFactory()
        {
            var context = SimpleBookingSystemContextMock.GetSimpleBookingSystemContext();
            var mapperMock = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(System.Reflection.Assembly.GetEntryAssembly(), typeof(Startup).Assembly);
            });
            var mapper = mapperMock.CreateMapper();
            return new SimpleBookingSystemService.Infrastructure.Services.ResourceService(context, mapper);
        }

        public static SimpleBookingSystemService.Infrastructure.Services.BookingService BookingServiceTestFactory()
        {
            var context = SimpleBookingSystemContextMock.GetSimpleBookingSystemContext();
            var loggerMock = new Mock<ILogger<SimpleBookingSystemService.Infrastructure.Services.BookingService>>();
            var mapperMock = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(System.Reflection.Assembly.GetEntryAssembly(), typeof(Startup).Assembly);
            });
            var mapper = mapperMock.CreateMapper();
            return new SimpleBookingSystemService.Infrastructure.Services.BookingService(context, mapper, loggerMock.Object, ValidationServiceTestFactory());
        }

        public static SimpleBookingSystemService.Infrastructure.Services.ValidationService ValidationServiceTestFactory()
        {
            var context = SimpleBookingSystemContextMock.GetSimpleBookingSystemContext();
            var loggerMock = new Mock<ILogger<SimpleBookingSystemService.Infrastructure.Services.ValidationService>>();
            var mapperMock = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(System.Reflection.Assembly.GetEntryAssembly(), typeof(Startup).Assembly);
            });
            var mapper = mapperMock.CreateMapper();
            return new SimpleBookingSystemService.Infrastructure.Services.ValidationService(context, loggerMock.Object, mapper);
        }
    }
}
