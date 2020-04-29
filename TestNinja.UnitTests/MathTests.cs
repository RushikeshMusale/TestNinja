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
        [Ignore("Because I wanted to!")]
        public void Add_WhenCalled_ReturnsSumOfTwoNumbers()
        {            

            var result = _math.Add(1, 3);

            Assert.That(result, Is.EqualTo(4));
        }   
        
        [Test]
        [TestCase(2,1,2)]
        [TestCase(1,2,2)]
        [TestCase(1,1,1)]
        public void Max_WhenCalled_ReturnsTheGreaterArgument(int a, int b, int expectedResult)
        {
            
            var result = _math.Max(a, b);

            Assert.That(result, Is.EqualTo(expectedResult));
        }

      
    }
}
