using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestNinja.UnitTests.Mocking.TestForMoqLib
{
    [TestFixture]
    class MoqTests
    {
        [Test]
        public void Moq_AbstractClass()
        {
            var abstractClass = new Mock<AbstractClass>();
            abstractClass.Setup(ac => ac.ReturnHello()).Returns("hello");
            var res = abstractClass.Object.ReturnHello();
            Assert.That(res, Does.Contain("hello").IgnoreCase);
        }

        [Test]
        public void Moq_SimpleClass()
        {
            var simpleClass = new Mock<SimpleClass>();
            simpleClass.Setup(sc => sc.ReturnsHello()).Returns("hello1");

            var res = simpleClass.Object.ReturnsHello();
            Assert.That(res, Does.Contain("hello1"));
        }
    }
}
