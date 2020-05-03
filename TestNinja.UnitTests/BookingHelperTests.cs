using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Mocking;

namespace TestNinja.UnitTests
{
    [TestFixture]
    class BookingHelper_OverlappingBookingExistsTests
    {
        private Mock<IBookingRepository> _bookingRepository;

        [SetUp]
        public void Setup()
        {
            _bookingRepository = new Mock<IBookingRepository>();
            
        }

        [Test]
        public void BookingStartsAndEndsBeforeExistingBooking_ReturnEmptyString()
        {
            _bookingRepository.Setup(br => br.GetActiveBookings(1)).Returns(new List<Booking>
            {
                new Booking
                {
                    Id =2,
                    ArrivalDate = new DateTime(20,1,14,14,0,0),
                    DepartureDate = new DateTime(20, 1, 20, 10,0,0),
                    Reference = "a"                    
                }
            }.AsQueryable<Booking>);

            string result = BookingHelper.OverlappingBookingsExist(
                new Booking
                {
                    Id = 1,
                    ArrivalDate = new DateTime(20,1, 10, 14,0,0),
                    DepartureDate = new DateTime(20,1,13, 10,0,0 )
                }, _bookingRepository.Object);

            Assert.That(result, Is.Empty);
        }
    }
}
