using WristCast.Core.Shared;

namespace WristCast.Core.Model
{
    public class MediaType : SmartEnum<MediaType, int>
    {
        public static MediaType Podcast = new MediaType(nameof(Podcast), 1);
        public static MediaType Episode = new MediaType(nameof(Episode), 2);

        public MediaType(string name, int value) : base(name, value)
        {
        }
    }
}