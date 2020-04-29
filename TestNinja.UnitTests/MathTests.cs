using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    class MathTests
    {
        [Test]
        public void Add_WhenCalled_ReturnsSumOfTwoNumbers()
        {
            Math math = new Math();

            var result = math.Add(1, 3);

            Assert.That(result, Is.EqualTo(4));
        }   
        
        [Test]
        public void Max_FirstArgumentIsGreater_ReturnsFirstArgument()
        {
            Math math = new Math();
            var result = math.Max(3, 2);

            Assert.That(result, Is.EqualTo(3));
        }

        [Test]
        public void Max_SecondArgumentIsGreater_ReturnsSecondArgument()
        {
            Math math = new Math();
            var result = math.Max(2, 3);

            Assert.That(result, Is.EqualTo(3));
        }

        [Test]
        public void Max_ArgumentsAreEqual_ReturnsSameArgument()
        {
            Math math = new Math();
            var result = math.Max(3, 3);

            Assert.That(result, Is.EqualTo(3));
        }
    }
}
