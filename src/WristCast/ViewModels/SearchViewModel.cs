using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ListenNotesSearch.NET.Models;
using WristCast.Core;
using WristCast.Core.Model;
using WristCast.Core.Services;
using Xamarin.Forms;
using EpisodeSearchResult = ListenNotesSearch.NET.Models.EpisodeSearchResult;
using ISearchResult = WristCast.Core.Services.ISearchResult;
using PodcastSearchResult = WristCast.Core.Services.PodcastSearchResult;

namespace WristCast.ViewModels
{
    public class SearchViewModel : ViewModel
    {
        private readonly INavigationService _navigation;
        private readonly ILog _log;
        private readonly ISearchService _searchService;

        public SearchViewModel(INavigationService navigation, ILog log, ISearchService searchService) : base()
        {
            _navigation = navigation;
            _log = log;
            _searchService = searchService;
            SelectedMediaType = MediaType.Podcast;
            SearchText = string.Empty;
            Search = new Command<string>(async (s) => await SearchAsync());
        }

        public MediaType SelectedMediaType { get; set; }

        private async Task SearchAsync()
        {
            IsBusy = true;
            try
            {
                Error = string.Empty;
                _log.Info("Selected media type: " + SelectedMediaType.Name);
                Podcasts = await _searchService.SearchAsync(SearchText, SelectedMediaType);
            }
            catch (SwaggerException e)
            {
                Error = "SC: " + e.StatusCode;
            }
            finally
            {
                IsBusy = false;
            }
        }

        public string Error { get; set; }

        public bool IsError => !string.IsNullOrEmpty(Error);

        public bool IsValid => !IsError;

        public bool IsBusy { get; set; }

        public string SearchText { get; set; }

        public bool IsSearchTextValid => SearchText.Length > 0;

        public IEnumerable<ISearchResult> Podcasts { get; set; }

        public Command<string> Search { get; }

        public async Task ShowDetails(ISearchResult searchResult)
        {
            try
            {
                Error = null;
                switch (searchResult)
                {
                    case PodcastSearchResult res:
                        IsBusy = true;
                        var podcast = await _searchService.SearchPodcastAsync(res.Id);
                        IsBusy = false;
                        await _navigation.PushModalAsync<PodcastDetailsViewModel, Podcast>(podcast);
                        break;
                    case EpisodeSearchResult res:
                        var episode = await _searchService.SearchEpisodeAsync(res.Id);
                        await _navigation.PushModalAsync<EpisodeDetailsViewModel, PodcastEpisode>(episode);
                        break;
                }
            }
            catch (Exception e)
            {
                _log.Error($"Error searching for {searchResult.Id}",e);
                Error = "Error retrieving info";
            }
            finally
            {
                IsBusy = false;
            }
        }

    }
}
