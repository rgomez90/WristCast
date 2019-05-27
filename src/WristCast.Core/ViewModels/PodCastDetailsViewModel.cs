using WristCast.Core.Model;

namespace WristCast.Core.ViewModels
{
    public class PodcastDetailsViewModel:ViewModel
    {
        public Podcast Podcast { get; }

        public PodcastDetailsViewModel(Podcast podcast)
        {
            Podcast = podcast;
        }



    }
}
