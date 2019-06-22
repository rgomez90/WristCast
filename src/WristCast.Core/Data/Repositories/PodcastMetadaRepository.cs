using WristCast.Core.Model;

namespace WristCast.Core.Data.Repositories
{
    public class PodcastMetadaRepository : GenericRepository<PodcastMetadata,string>, IPodcastMetadaRepository
    {
        public PodcastMetadaRepository(WristCastContext context) : base(context)
        {
        }
    }
}