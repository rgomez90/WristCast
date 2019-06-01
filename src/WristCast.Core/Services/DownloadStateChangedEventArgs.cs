namespace WristCast.Core.Services
{
    public class DownloadStateChangedEventArgs
    {
        public DownloadStateChangedEventArgs(DownloadState oldState, DownloadState newState)
        {
            OldState = oldState;
            NewState = newState;
        }

        public DownloadState OldState { get; set; }
        public DownloadState NewState { get; set; }
    }
}