using System.Collections.Generic;
using System.Threading.Tasks;
using WristCast.Core.Model;

namespace WristCast.Core.Services
{
    public interface ISearchService
    {
        Task<IEnumerable<PodcastSearchResult>> SearchPodcastsAsync(string name, int? offset = null, int? count = null);
        Task<Podcast> SearchPodcastAsync(string id);
        Task<IEnumerable<EpisodeSearchResult>> SearchEpisodesAsync(string name, int? offset = null, int? count = null);
        Task<IEnumerable<ListenNotesSearch.NET.Models.ISearchResult>> Search<T>(string name, int? offset = null, int? count = null) where T:ListenNotesSearch.NET.Models.ISearchResult;
        Task<PodcastEpisode> SearchEpisodeAsync(string resId);
        Task<IEnumerable<ISearchResult>> SearchAsync(string searchString, MediaType mediaType, int? offset = null, int? count = null);
    }
}
