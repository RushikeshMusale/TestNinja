using System;
using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
   // [TestClass] MS Test
    public class ReservationTests
    {
        [Test]
       // [TestMethod] MS Test
        public void CanBeCancelledBy_UserIsAdmin_ReturnsTrue()
        {
            //arrange
            var reservation = new Reservation();

            //act
            var result = reservation.CanBeCancelledBy(new User { IsAdmin = true });

            //assert
            //Assert.IsTrue(result); // MS Test default
            Assert.That(result, Is.True);

        }


        [Test]
        public void CanBeCancelledBy_SameUserCancellingReservation_ReturnsTrue()
        {
            //arrange
            var reservation = new Reservation();

            var user = new User();
            reservation.MadeBy = user;            

            //act
            var result = reservation.CanBeCancelledBy(user);

            //assert
            Assert.IsTrue(result);
        }


        [Test]
        public void CanBeCancelledBy_AnotherUserCancellingReservation_ReturnsTrue()
        {
            //arrange
            var reservation = new Reservation() { MadeBy = new User() };
                     

            //act
            var result = reservation.CanBeCancelledBy(new User());

            //assert
            Assert.IsFalse(result);
        }
    }
}
