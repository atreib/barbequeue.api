using System.Collections.Generic;
using System.Threading.Tasks;
using barbequeue.api.Domain.Models;

namespace barbequeue.api.Data.Protocols
{
    public interface IBarbequeParticipantRepository
    {
         Task<IEnumerable<BarbequeParticipant>> ListByBarbequeAsync (int barbequeId);
         Task<BarbequeParticipant> AddAsync (BarbequeParticipant participant);
         Task<BarbequeParticipant> FindByIdAsync (int id);
         Task<BarbequeParticipant> Update (BarbequeParticipant participant);
         Task<bool> Remove (BarbequeParticipant participant);
    }
}