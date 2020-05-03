using System;
using System.Linq;

namespace TestNinja.Mocking
{
    public class BookingRepository : IBookingRepository
    {
        private UnitOfWork _unitOfWork;

        public BookingRepository()
        {
            _unitOfWork = new UnitOfWork();
        }

        public IQueryable<Booking> GetActiveBookings(int? excludedBookingId = null)
        {
            var bookings =
               _unitOfWork.Query<Booking>()
                   .Where(
                       b => b.Status != "Cancelled");

            if (excludedBookingId.HasValue)
                bookings = bookings.Where(b => b.Id == excludedBookingId.Value);

            return bookings;
        }
    }
}