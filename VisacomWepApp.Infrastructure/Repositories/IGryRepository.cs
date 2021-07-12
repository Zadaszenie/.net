using System.Collections.Generic;
using System.Threading.Tasks;

namespace VisacomWepApp.Infrastructure.Repositories
{
    public interface IGryRepository
    {
        Task<IEnumerable<GamePresentation>> GetGamesAsync();
    }
}
