using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WristCast.Core;
using WristCast.Core.Model;
using WristCast.Core.Services;
using Xamarin.Forms;

namespace WristCast.ViewModels
{
    public class SearchViewModel : ViewModel
    {
        private readonly ILog _log;
        private readonly INavigationService _navigation;
        private readonly ISearchService _searchService;

        public SearchViewModel(INavigationService navigation, ILog log, ISearchService searchService)
        {
            _navigation = navigation;
            _log = log;
            _searchService = searchService;
            SelectedMediaType = MediaType.Podcast;
            SearchText = string.Empty;
            SearchCommand = new Command<string>(async searchText => await SearchAsync());
        }

        public bool IsError { get; set; }

        public bool IsSearchTextValid => SearchText.Length > 0;

        public Command<string> SearchCommand { get; }

        public string SearchText { get; set; }

        public MediaType SelectedMediaType { get; set; }

        private async Task SearchAsync()
        {
            var popUp = CreateProgressPopup($"Searching for {SearchText}...");
            SearchText = SearchText.Trim();
            try
            {
                popUp.Show();
                _log.Info("Selected media type: " + SelectedMediaType.Name);
                var podcasts = await _searchService.SearchAsync(SearchText, SelectedMediaType);
                popUp.Dismiss();
                await _navigation.PushAsync<SearchResultViewModel, IEnumerable<ISearchResult>>(podcasts);
            }
            catch (Exception e)
            {
                IsError = true;
                _log.Error($"Error searching for {SearchText}",e);
            }
        }
    }
}
