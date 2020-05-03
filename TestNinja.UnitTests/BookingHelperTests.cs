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
        private Booking _existingBooking;

        [SetUp]
        public void Setup()
        {
            _bookingRepository = new Mock<IBookingRepository>();

            _existingBooking = new Booking
            {
                Id = 2,
                ArrivalDate = ArriveOn(20, 1, 14),
                DepartureDate = DepartOn(20, 1, 20),
                Reference = "a"
            };

            _bookingRepository.Setup(br => br.GetActiveBookings(1)).Returns(new List<Booking>
            {
               _existingBooking
            }.AsQueryable<Booking>);
        }

        [Test]
        public void BookingStartsAndEndsBeforeExistingBooking_ReturnEmptyString()
        {          
            string result = BookingHelper.OverlappingBookingsExist(
                new Booking
                {
                    Id = 1,
                    ArrivalDate = Before(_existingBooking.ArrivalDate, days: 5),
                    DepartureDate = Before(_existingBooking.ArrivalDate)
                }, _bookingRepository.Object);

            Assert.That(result, Is.Empty);
        }


        [Test]
        public void BookingStartsBeforeExistingBookingAndFinisheshInTheMiddle_ReturnsExistingBooking()
        {
            string result = BookingHelper.OverlappingBookingsExist(
                new Booking
                {
                    Id = 1,
                    ArrivalDate = Before(_existingBooking.ArrivalDate, days: 5),
                    DepartureDate = Before(_existingBooking.DepartureDate)
                }, _bookingRepository.Object);

            Assert.That(result, Is.EqualTo(_existingBooking.Reference));
        }

        [Test]
        public void BookingStartsBeforeExistingBookingAndFinisheshAfter_ReturnsExistingBooking()
        {
            string result = BookingHelper.OverlappingBookingsExist(
                new Booking
                {
                    Id = 1,
                    ArrivalDate = Before(_existingBooking.ArrivalDate, days: 5),
                    DepartureDate = After(_existingBooking.DepartureDate)
                }, _bookingRepository.Object);

            Assert.That(result, Is.EqualTo(_existingBooking.Reference));
        }


        private DateTime Before(DateTime dateTime, int days = 1)
        {
            return dateTime.AddDays(-days);
        }

        private DateTime After(DateTime dateTime)
        {
            return dateTime.AddDays(1);
        }

        private DateTime ArriveOn(int year, int month, int day)
        {
            return new DateTime(year, month, day, 14, 0, 0);
        }

        private DateTime DepartOn(int year, int month, int day)
        {
            return new DateTime(year, month, day, 10, 0, 0);
        }
    }
}
