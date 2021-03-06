﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace TestNinja.Mocking
{
    public static class BookingHelper
    {
        // There are two ways to do this, since static constructor can not have parameters,
        // either we have to use property injection, or paremeter injection

        // Option 1: Property injection

        //public static IBookingRepository _bookingRepository { get; set; }

        //static BookingHelper()
        //{
        //    _bookingRepository = new BookingRepository();
        //}        
        

        // Option 2 : Parameter injection
        public static string OverlappingBookingsExist(Booking booking, IBookingRepository bookingRepository)
        {
            if (booking.Status == "Cancelled")
                return string.Empty;


            IQueryable<Booking> bookings = bookingRepository.GetActiveBookings(booking.Id);

            var overlappingBooking =
              bookings.FirstOrDefault(
                  b =>
                      booking.ArrivalDate < b.DepartureDate
                      && b.ArrivalDate <  booking.DepartureDate
                     );

            return overlappingBooking == null ? string.Empty : overlappingBooking.Reference;
        }
    }

    public class UnitOfWork : IUnitOfWork
    {
        public IQueryable<T> Query<T>()
        {
            return new List<T>().AsQueryable();
        }
    }

    public class Booking
    {
        public string Status { get; set; }
        public int Id { get; set; }
        public DateTime ArrivalDate { get; set; }
        public DateTime DepartureDate { get; set; }
        public string Reference { get; set; }
    }
}