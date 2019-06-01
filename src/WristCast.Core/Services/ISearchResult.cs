namespace WristCast.Core.Services
{
    public interface ISearchResult
    {
        string Rss { get; set; }
        string DescriptionHighlighted { get; set; }

        /// <summary>Plain text of this episode's description</summary>
        string DescriptionOriginal { get; set; }

        /// <summary>Plain text of this episode' title</summary>
        string TitleOriginal { get; set; }

        string PublisherOriginal { get; set; }
        string Image { get; set; }
        int ItunesId { get; set; }
        string Id { get; set; }
        bool ExplicitContent { get; set; }
    }
}