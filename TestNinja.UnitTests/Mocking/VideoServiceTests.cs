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
    class VideoServiceTests
    {
        private Mock<IFileReader> _fileReader;
        private Mock<IVideoRepository> _videoRepository; 
        private VideoService _service;
        
        [SetUp]
        public void Setup()
        {
            _fileReader = new Mock<IFileReader>();
            _videoRepository = new Mock<IVideoRepository>();

            _service = new VideoService(_fileReader.Object,_videoRepository.Object);
        }
        
        [Test]
        public void ReadVideoTitle_EmptyFile_ReturnError()
        {
            
            _fileReader.Setup(fr => fr.Read("video.txt")).Returns("");                   

            var result = _service.ReadVideoTitle();

            Assert.That(result, Does.Contain("error").IgnoreCase);
        }

        

        [Test]
        public void GetUnprocessedVideosAsCsv_AllVideosAreProcessed_ReturnsEmptyString()
        {
            
            _videoRepository.Setup(v => v.GetUnprocessedVideos()).Returns(new List<Video>());
            var result = _service.GetUnprocessedVideosAsCsv();
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void GetUnprocessedVideosAsCsv_SomeVideosAreUnProcessed_ReturnsAStringWithUnprocessedIds()
        {
            List<Video> list = new List<Video>()
            {
                new Video {Id=1 },
                new Video {Id=2 },
                new Video {Id=3 },
            };
            _videoRepository.Setup(v => v.GetUnprocessedVideos()).Returns(list);

            var result = _service.GetUnprocessedVideosAsCsv();

            Assert.That(result, Is.EqualTo("1,2,3"));
        }

    }
}
