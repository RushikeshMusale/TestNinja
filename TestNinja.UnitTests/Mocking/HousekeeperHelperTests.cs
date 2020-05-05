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
        private Mock<IUnitOfWork> _unitOfWork;
        private Mock<IStatementGenerator> _statementGenerator;
        private Mock<IEmailSender> _emailSender;
        private Mock<IXtraMessageBox> _messageBox;
        private Housekeeper _houseKeeper;
        private HousekeeperHelper _service;
        private DateTime _statementDate = new DateTime(2020, 5, 3);
        private string _statementFileName;

        [SetUp]
        public void Setup()
        {
            
            _unitOfWork = new Mock<IUnitOfWork>();
            _houseKeeper = new Housekeeper { Oid = 1, Email = "a", FullName = "b", StatementEmailBody = "c" };

            _unitOfWork.Setup(uow => uow.Query<Housekeeper>()).Returns(new List<Housekeeper>
            {
                _houseKeeper    
            }.AsQueryable());

            _statementFileName = "filename";
            _statementGenerator = new Mock<IStatementGenerator>();                                   
            _statementGenerator
                .Setup(sg => sg.SaveStatement(_houseKeeper.Oid, _houseKeeper.FullName, _statementDate))
                .Returns(()=>_statementFileName); 
            // NOTE: Lamda expression allows lazy loading. it means latest value of _statementFileName 
            // is checked during test case 
            // If we do not use lamda expression, then it will by default return 'fileName' value inititialized in previous step

            _emailSender = new Mock<IEmailSender>();
            _messageBox = new Mock<IXtraMessageBox>();

            _service = new HousekeeperHelper(_unitOfWork.Object, _statementGenerator.Object, _emailSender.Object, _messageBox.Object);
        }

        [Test]
        public void SendStatementEmails_WhenCalled_GeneratesStatementReport()
        {            

            _service.SendStatementEmails(_statementDate);

            _statementGenerator.Verify(sg => sg.SaveStatement(_houseKeeper.Oid, _houseKeeper.FullName, _statementDate));

        }

        [Test]
        public void SendStatementEmails_HousekeepersEmailIsNull_ShouldNotGeneratesStatementReport()
        {
            _houseKeeper.Email = null;

            _service.SendStatementEmails(_statementDate);

            // Test thate save statement is not called
            _statementGenerator.Verify(sg => sg.SaveStatement(_houseKeeper.Oid, _houseKeeper.FullName, _statementDate), Times.Never);

        }

        [Test]
        public void SendStatementEmails_HousekeepersEmailIsWhiteSpace_ShouldNotGeneratesStatementReport()
        {
            _houseKeeper.Email = " ";

            _service.SendStatementEmails(_statementDate);

            // Test thate save statement is not called
            _statementGenerator.Verify(sg => sg.SaveStatement(_houseKeeper.Oid, _houseKeeper.FullName, _statementDate), Times.Never);

        }

        [Test]
        public void SendStatementEmails_HousekeepersEmailIsEmpty_ShouldNotGeneratesStatementReport()
        {
            _houseKeeper.Email = "";

            _service.SendStatementEmails(_statementDate);

            // Test thate save statement is not called
            _statementGenerator.Verify(sg => sg.SaveStatement(_houseKeeper.Oid, _houseKeeper.FullName, _statementDate), Times.Never);

        }


        [Test]
        public void SendStatementEmails_WhenCalled_SendEmailStatment()
        {

            _service.SendStatementEmails(_statementDate);

            VerifyEmailSent();
        }

        private void VerifyEmailSent()
        {
            _emailSender.Verify(es => es.EmailFile(
                            _houseKeeper.Email,
                            _houseKeeper.StatementEmailBody,
                            _statementFileName,
                            It.IsAny<string>()));
        }

        [Test]
        public void SendStatementEmails_StatementFileNameIsNull_ShouldNotSendEmailStatment()
        {

            //_statementGenerator
            //    .Setup(sg => sg.SaveStatement(_houseKeeper.Oid, _houseKeeper.FullName, _statementDate))
            //    .Returns<string>(null);

            _statementFileName = null;

            _service.SendStatementEmails(_statementDate);

            VerifyEmailNotSent();
        }

        [Test]
        public void SendStatementEmails_StatementFileNameIsEmpty_ShouldNotSendEmailStatment()
        {
            // Another way to return null object is to pass lamda expression () => null
            // Or we can use generic method .Returns<string>(null)
            //_statementGenerator
            //    .Setup(sg => sg.SaveStatement(_houseKeeper.Oid, _houseKeeper.FullName, _statementDate))
            //    .Returns("");

            _statementFileName = "";

            //_statementGenerator
            //    .Setup(sg => sg.SaveStatement(_houseKeeper.Oid, _houseKeeper.FullName, _statementDate))
            //    .Returns<string>(null);

            _service.SendStatementEmails(_statementDate);

            VerifyEmailNotSent();
        }

        [Test]
        public void SendStatementEmails_StatementFileNameIsWhiteSpace_ShouldNotSendEmailStatment()
        {
            _statementFileName = " ";

            _service.SendStatementEmails(_statementDate);
            VerifyEmailNotSent();
        }       

        private void VerifyEmailNotSent()
        {
            _emailSender.Verify(es => es.EmailFile(
                            It.IsAny<string>(),
                            It.IsAny<string>(),
                            It.IsAny<string>(),
                            It.IsAny<string>()), Times.Never);
        }
    }
}
