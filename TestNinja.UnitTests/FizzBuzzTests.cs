using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    class FizzBuzzTests
    {
        [Test]
        public void GetOutput_NumberIsOnlyDivisibleByThree_ReturnsFizz()
        {
            string result = FizzBuzz.GetOutput(3);

            Assert.That(result, Is.EqualTo("Fizz"));
        }

        [Test]
        public void GetOutput_NumberIsOnlyDivisibleByFive_ReturnsBuzz()
        {
            string result = FizzBuzz.GetOutput(10);

            Assert.That(result, Is.EqualTo("Buzz"));
        }

        [Test]
        public void GetOutput_NumberIsDivisibleByThreeAndFive_ReturnsFizzBuzz()
        {
            string result = FizzBuzz.GetOutput(15);

            Assert.That(result, Is.EqualTo("FizzBuzz"));
        }

        [Test]
        public void GetOutput_NumberIsNotDivisibleByThreeAndFive_ReturnsSameNumber()
        {
            string result = FizzBuzz.GetOutput(14);

            Assert.That(result, Is.EqualTo("14"));
        }
    }
}
