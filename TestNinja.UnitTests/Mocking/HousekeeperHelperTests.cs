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
    class HousekeeperHelperTests
    {
        [Test]
        public void SendStatementEmails_WhenCalled_GeneratesStatementReport()
        {
            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(uow => uow.Query<Housekeeper>()).Returns(new List<Housekeeper>
            {
                new Housekeeper { Oid=1, Email="a", FullName="b", StatementEmailBody="c" }
            }.AsQueryable());

            var statementGenerator = new Mock<IStatementGenerator>();
            var emailSender = new Mock<IEmailSender>();
            var messageBox = new Mock<IXtraMessageBox>();

            var service = new HousekeeperHelper(unitOfWork.Object, statementGenerator.Object, emailSender.Object, messageBox.Object);

            service.SendStatementEmails(new DateTime(2020, 5, 3));

            statementGenerator.Verify(sg => sg.SaveStatement(1, "b", new DateTime(2020, 5, 3)));

        }
    }
}
