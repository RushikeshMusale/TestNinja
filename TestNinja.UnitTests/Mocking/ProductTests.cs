using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    class ProductTests
    {
        [Test]
        public void GetPrice_GoldCustomers_Apply30PercentDicsount()
        {
            var product = new Product() { ListPrice = 100 };
            var result = product.GetPrice(new Customer { IsGold = true });

            Assert.That(result, Is.EqualTo(70));
        }

        [Test]
        public void GetPrice_GoldCustomers_Apply30PercentDiscount2()
        {
            var product = new Product { ListPrice = 100 };

            var customer = new Mock<ICustomer>();
            customer.Setup(x => x.IsGold).Returns(true);

            var result = product.GetPrice(customer.Object);

            Assert.That(result, Is.EqualTo(70));
        }
    }
}
