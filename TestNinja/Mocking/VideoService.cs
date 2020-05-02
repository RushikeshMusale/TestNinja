using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using TestNinja.Mocking;

namespace TestNinja.Mocking
{
    public class VideoService
    {
        private IFileReader _fileReader { get; set; }
        private IVideoRepository _videoRepository;

        //// This can be used to inject FakeFileReader in UnitTests
        //public VideoService(IFileReader fileReader)
        //{
        //    _fileReader = fileReader;
        //}

        //// Let the default constructor be there to minimise the breaking of code

        //public VideoService()
        //{
        //    _fileReader = new FileReader();
        //}


        // It can be safely combined using one ctor
        public VideoService(IFileReader fileReader = null, IVideoRepository repository= null)
        {
            _fileReader = fileReader ?? new FileReader();
            _videoRepository = repository ?? new VideoRepository( );
        }


        //when we are using dependency injection
        //public VideoService(IFileReader fileReader)
        //{
        //    _fileReader = fileReader;
        //}


        public string ReadVideoTitle()
        {
            var str = _fileReader.Read("video.txt");
            var video = JsonConvert.DeserializeObject<Video>(str);
            if (video == null)
                return "Error parsing the video.";
            return video.Title;
        }

        public string GetUnprocessedVideosAsCsv()
        {
            var videoIds = new List<int>();

            // Move the quering logic to separate class, as it it depends on database

            var videos = _videoRepository.GetUnprocessedVideos();

            foreach (var v in videos)
                videoIds.Add(v.Id);

            return String.Join(",", videoIds);
        }
    }

    public class Video
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsProcessed { get; set; }
    }

    public class VideoContext : DbContext
    {
        public DbSet<Video> Videos { get; set; }
    }
}