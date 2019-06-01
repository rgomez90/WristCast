using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ListenNotesSearch.NET;
using WristCast.Core.Model;
using WristCast.Core.ViewModels;
using ApiSearchResults = ListenNotesSearch.NET.Models;

namespace WristCast.Core.Services
{
    public class SearchService : ISearchService
    {
        private readonly ILog _logger;
        private readonly IFileService _fileService;
        private readonly ListenNodeSearchClient _client;

        public SearchService(string apiKey, ILog logger, IFileService fileService)
        {
            _logger = logger;
            _fileService = fileService;
            _client = new ListenNodeSearchClient(apiKey);
            _client.NewResponse += OnNewResponse;
            _logger.Debug($"Created client with key: {apiKey}");
        }

        private void OnNewResponse(object sender, string e)
        {
            _logger.Info($"New Response: {e}");
        }

        public async Task<Podcast> SearchPodcastAsync(string id)
        {
            var podcast = await _client.GetPodcastByIdAsync(id).ConfigureAwait(false);
            var p = new Podcast(podcast);
            var files = (await _fileService.GetAllMediaFiles()).Select(Path.GetFileNameWithoutExtension).ToArray();
            foreach (var podcastEpisode in p.Episodes)
            {
                if (files.Contains(podcastEpisode.Id))
                {
                    podcastEpisode.IsDownloaded = true;
                }
            }
            return p;
        }

        public async Task<IEnumerable<PodcastSearchResult>> SearchPodcastsAsync(string name)
        {
            var response = await Search<ApiSearchResults.PodcastSearchResult>(name);
            return response.Cast<ApiSearchResults.PodcastSearchResult>().Select(x => x.ToResult());
        }

        public async Task<IEnumerable<EpisodeSearchResult>> SearchEpisodesAsync(string name)
        {
            var response = await Search<ApiSearchResults.EpisodeSearchResult>(name);
            var episodes = response.Cast<ApiSearchResults.EpisodeSearchResult>().Select(x => x.ToResult()).ToArray();
            foreach (var episodeSearchResult in episodes)
            {
                if ((await _fileService.GetAllMediaFiles()).Select(Path.GetFileNameWithoutExtension)
                    .Contains(episodeSearchResult.Id))
                {
                    episodeSearchResult.IsDownloaded = true;
                }
            }
            return episodes;
        }

        public async Task<IEnumerable<ListenNotesSearch.NET.Models.ISearchResult>> Search<T>(string name) where T : ListenNotesSearch.NET.Models.ISearchResult
        {
            _logger.Debug($"Searching containing '{name}'");
            try
            {
                var response = await _client.SearchAsync<T>(name).ConfigureAwait(false);
                _logger.Debug($"Response:{response.Count}");
                return (IEnumerable<ApiSearchResults.ISearchResult>)response.Results;
            }
            catch (Exception ex)
            {
                _logger.Error("Error searching podcasts", ex);
                throw;
            }
        }

        public async Task<PodcastEpisode> SearchEpisodeAsync(string resId)
        {
            var res = await _client.GetEpisodeByIdAsync(resId);
            var episode = new PodcastEpisode(res) { IsDownloaded = await _fileService.CheckIfMediaFileExists(resId + ".mp3") };
            episode.Podcast = new Podcast(res.Podcast, new[] { episode });
            return episode;
        }

        public async Task<IEnumerable<ISearchResult>> SearchAsync(string searchString, MediaType mediaType)
        {
            switch (mediaType.Name)
            {
                case nameof(MediaType.Podcast):
                    return await SearchPodcastsAsync(searchString);
                case nameof(MediaType.Episode):
                    return await SearchEpisodesAsync(searchString);
                default:
                    throw new InvalidEnumArgumentException(nameof(mediaType));
            }

        }
    }
}