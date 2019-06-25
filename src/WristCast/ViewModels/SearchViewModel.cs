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

        private bool _isError;

        public bool IsError
        {
            get => _isError;
            set => SetProperty(ref _isError,value);
        }

        public bool IsSearchTextValid => SearchText.Length > 0;

        public Command<string> SearchCommand { get; }

        private string _searchText;

        public string SearchText
        {
            get => _searchText;
            set => SetProperty(ref _searchText,value,nameof(SearchText),nameof(IsSearchTextValid));
        }

        private MediaType _selectedMediaType;

        public MediaType SelectedMediaType
        {
            get => _selectedMediaType;
            set => SetProperty(ref _selectedMediaType,value);
        }

        private async Task SearchAsync()
        {
            var popUp = CreateProgressPopup($"Searching for {SearchText}...");
            IEnumerable<ISearchResult> searchResults = null;
            SearchText = SearchText.Trim();
            try
            {
                popUp.Show();
                _log.Info("Selected media type: " + SelectedMediaType.Name);
                searchResults = await _searchService.SearchAsync(SearchText, SelectedMediaType);
            }
            catch (Exception e)
            {
                popUp.Dismiss();
                popUp = CreateTextPopup("Error searching...");
                popUp.Show();
                IsError = true;
                _log.Error($"Error searching for {SearchText}", e);
            }
            finally
            {
                popUp.Dismiss();
            }
            await _navigation.PushAsync<SearchResultViewModel, IEnumerable<ISearchResult>>(searchResults);
        }
    }
}
