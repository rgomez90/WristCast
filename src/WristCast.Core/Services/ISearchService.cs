using System.Collections.Generic;
using System.Threading.Tasks;
using ListenNotesSearch.NET;
using ListenNotesSearch.NET.Models;
using WristCast.Core.Model;

namespace WristCast.Core.Services
{
    public interface ISearchService
    {
        Task<IEnumerable<Podcast>> SearchPodcastAsync(string name);

        Task<IEnumerable<PodcastEpisode>> SearchEpisodesAsync(string name);
    }

    public class SearchService : ISearchService
    {
        private readonly ListenNodeSearchClient _client;

        public SearchService(ListenNodeSearchClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<Podcast>> SearchPodcastAsync(string name)
        {
            var response = await _client.SearchAsync(name,1,Type.Podcast);
        }

        public Task<IEnumerable<PodcastEpisode>> SearchEpisodesAsync(string name)
        {
            throw new System.NotImplementedException();
        }
    }
}
