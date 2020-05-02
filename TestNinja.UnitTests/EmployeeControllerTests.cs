using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Mocking;

namespace TestNinja.UnitTests
{
    [TestFixture]
    class EmployeeControllerTests
    {
        private Mock<IEmployeeStorage> _storage;
        private EmployeeController _controller;

        [SetUp]
        public void Setup()
        {
            _storage = new Mock<IEmployeeStorage>();
            _controller = new EmployeeController(_storage.Object);
        }
        [Test]
        public void DeleteEmployee_WhenCalled_ReturnsRedirectResult()
        {
            
            var result = _controller.DeleteEmployee(1);

            Assert.That(result, Is.TypeOf<RedirectResult>());
        }

        [Test]
        public void DeleteEmployee_WhenCalled_DeletesEmployee()
        {
            var result = _controller.DeleteEmployee(1);

            _storage.Verify(s => s.Remove(1));
        }
    }
}
