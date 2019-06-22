using WristCast.Core.Model;

namespace WristCast.Core.Data.Repositories
{
    public class PodcastEpisodeMetadataRepository : GenericRepository<PodcastEpisodeMetadata, string>, IPodcastEpisodeMetadataRepository
    {
        public PodcastEpisodeMetadataRepository(WristCastContext context) : base(context)
        {
        }
    }
}