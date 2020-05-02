using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    class InstallerHelperTests
    {
        [Test]
        public void DownloadInstaller_FileDownloadSuccessfully_ReturnsTrue()
        {
            var fileDownloader = new Mock<IFileDownloader>();
            var service = new InstallerHelper(fileDownloader.Object);

            var result = service.DownloadInstaller("", "");

            Assert.That(result, Is.True);


        }

        [Test]
        public void DownloadInstaller_FileDownloadFailed_ReturnsFalse()
        {
            var fileDownloader = new Mock<IFileDownloader>();
            var service = new InstallerHelper(fileDownloader.Object);

            // It only throws exception if the arguments matches
            //fileDownloader.Setup(x => x.DownloadFile(string.Format("http://example.com/{0}/{1}",
            //            "",
            //            ""), null)).Throws<WebException>();

            // Better way is It.class
            fileDownloader
                .Setup(x => x.DownloadFile(
                    It.IsAny<string>(), It.IsAny<string>())
                    ).Throws<WebException>();

            var result = service.DownloadInstaller("", "");

            Assert.That(result, Is.False);

        }
    }
}
