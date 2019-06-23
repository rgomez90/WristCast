using System;
using System.IO;
using Autofac;
using WristCast.Core.Data;
using WristCast.Core.IoC;
using WristCast.Core.Services;

namespace WristCast.Core.Model
{
    public class PodcastEpisodeMetadata : Entity<string>
    {
        public PodcastEpisodeMetadata()
        {

        }

        public PodcastEpisodeMetadata(string id, string title, DateTime date, string description) : base(id)
        {
            Title = title;
            Date = date;
            Description = description;
            using (var con = IocContainer.Instance.BeginLifetimeScope())
            {
                FilePath = Path.Combine(con.Resolve<IStorageProvider>().MediaFolderPath, Id);
                FilePath = Path.Combine(con.Resolve<IStorageProvider>().MediaFolderPath, Id);
            }
        }

        public string FilePath { get; }
        public string Title { get; }
        public DateTime Date { get; }
        public string Description { get; }
    }
}