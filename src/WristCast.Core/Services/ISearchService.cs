using System.Collections.Generic;
using System.Threading.Tasks;
using WristCast.Core.Model;

namespace WristCast.Core.Services
{
    public interface ISearchService
    {
        Task<IEnumerable<PodcastSearchResult>> SearchPodcastsAsync(string name);
        Task<Podcast> SearchPodcastAsync(string id);
        Task<IEnumerable<EpisodeSearchResult>> SearchEpisodesAsync(string name);
        Task<IEnumerable<ListenNotesSearch.NET.Models.ISearchResult>> Search<T>(string name) where T:ListenNotesSearch.NET.Models.ISearchResult;
        Task<PodcastEpisode> SearchEpisodeAsync(string resId);
        Task<IEnumerable<ISearchResult>> SearchAsync(string searchString, MediaType mediaType);
    }
}
