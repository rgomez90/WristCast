namespace WristCast.Core.Model
{
    public class PodcastInfo
    {
        public string Id { get; set; }

        public string Publisher { get; set; }

        public bool ExplicitContent { get; set; }

        public string Website { get; set; }

        public string Image { get; set; }

        public string Description { get; set; }

        public string Country { get; set; }

        public string Name { get; }

        public string RssFeed { get; }
    }
}