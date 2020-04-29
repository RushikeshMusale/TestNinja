using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    class MathTests
    {
        private Math _math;

        [SetUp]
        public void Setup()
        {
            _math = new Math();
        }


        [Test]
        public void Add_WhenCalled_ReturnsSumOfTwoNumbers()
        {            

            var result = _math.Add(1, 3);

            Assert.That(result, Is.EqualTo(4));
        }   
        
        [Test]
        public void Max_FirstArgumentIsGreater_ReturnsFirstArgument()
        {
            
            var result = _math.Max(3, 2);

            Assert.That(result, Is.EqualTo(3));
        }

        [Test]
        public void Max_SecondArgumentIsGreater_ReturnsSecondArgument()
        {
            
            var result = _math.Max(2, 3);

            Assert.That(result, Is.EqualTo(3));
        }

        [Test]
        public void Max_ArgumentsAreEqual_ReturnsSameArgument()
        {
            
            var result = _math.Max(3, 3);

            Assert.That(result, Is.EqualTo(3));
        }
    }
}
