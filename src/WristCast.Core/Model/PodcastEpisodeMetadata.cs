using System;
using System.IO;
using SQLite;
using WristCast.Core.Data;

namespace WristCast.Core.Model
{
    public class PodcastEpisodeMetadata : IEntity<string>
    {
        public PodcastEpisodeMetadata()
        {

        }

        public PodcastEpisodeMetadata(string id, string title, DateTime date, string description)
        {
            Id = id;
            Title = title;
            Date = date;
            Description = description;
            FilePath = Path.Combine(StorageProvider.Current.MediaFolderPath, Id);
        }

        public string FilePath { get; set; }

        public string Title { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; }

        [PrimaryKey]
        public string Id { get; set; }
    }
}