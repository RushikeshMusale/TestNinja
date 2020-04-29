using NUnit.Framework;
using System.Linq;
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

      
        [Test]
        public void GetOddNumbers_LimitIsGreaterThanZero_ReturnsOddNumbersUptoLimit()
        {
            var result = _math.GetOddNumbers(5);

            //Assert.That(result, Is.Not.Empty);

            //Assert.That(result.Count(), Is.EqualTo(3));

            //Assert.That(result, Does.Contain(1));
            //Assert.That(result, Does.Contain(3));
            //Assert.That(result, Does.Contain(5));

            Assert.That(result, Is.EquivalentTo(new[] { 1, 3, 5 }));

            //Assert.That(result, Is.Ordered);
            //Assert.That(result, Is.Unique);
        }


        [Test]
        public void GetOddNumbers_LimitIsLessThanZero_ReturnsEmpty()
        {
            var result = _math.GetOddNumbers(-3);

            Assert.That(result, Is.Empty);            
        }
    }
}
