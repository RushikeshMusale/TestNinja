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
    class ErrorLoggerTests
    {
        [Test]
        public void Log_WhenCalled_SetTheLastErrorProperty()
        {
            var logger = new ErrorLogger();

            logger.Log("a");

            Assert.That(logger.LastError, Is.EqualTo("a"));
        }

        [Test]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public void Log_InvalidInput_ThrowsArgumentNullException(string error)
        {
            var logger = new ErrorLogger();

            // if we directly call method here then it will throw exception & test case will fail
            // So we use lamda expression

            Assert.That(() => logger.Log(error), Throws.ArgumentNullException);
            // Another way is use generic excpetion for custom exception

            Assert.That(() => logger.Log(error), Throws.Exception.TypeOf<ArgumentNullException>());           
        }

    }
}
