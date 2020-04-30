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
    class DemeritPointsCalculatorTests
    {
        private DemeritPointsCalculator _calculator;

        [SetUp]
        public void Setup()
        {
            _calculator = new DemeritPointsCalculator();
        }

        // Specific test cases

        [Test]
        public void CalculateDemeritPoints_SpeedIsWithinLimit_ReturnsZero()
        {           
            var result = _calculator.CalculateDemeritPoints(60);

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void CalculateDemeritPoints_ValidSpeedGreaterThanLimit_ReturnsPoint()
        {
            var result = _calculator.CalculateDemeritPoints(70);

            Assert.That(result, Is.EqualTo(1));
        }

        // Generalized test cases

        [Test]
        [TestCase(0,0)]
        [TestCase(64,0)]
        [TestCase(65,0)]
        [TestCase(68, 0)]
        [TestCase(70, 1)]
        [TestCase(75, 2)]
        public void CalculateDemeritPoints_WhenCalled_ReturnsDemeritPoint(int speed, int expectedPoints)
        {
            var result = _calculator.CalculateDemeritPoints(speed);

            Assert.That(result, Is.EqualTo(expectedPoints));
        }


        [Test]
        [TestCase(-12)]
        [TestCase(301)]
        public void CalculateDemeritPoints_SpeedIsOutOfRange_ThrowsArgumentOutOfRangeException(int speed)
        {
            Assert.That(() => _calculator.CalculateDemeritPoints(speed), 
                        Throws.Exception.TypeOf<ArgumentOutOfRangeException>());
        }
    }
}
