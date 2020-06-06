using System.Collections.Generic;
using System.Threading.Tasks;
using barbequeue.api.Domain.Models;

namespace barbequeue.api.Domain.UseCases
{
    public interface IBarbequeParticipantService
    {
        Task<IEnumerable<BarbequeParticipant>> ListByBarbequeAsync (int barbequeId);
        Task<BarbequeParticipant> FindByIdAsync (int id);
        Task<BarbequeParticipant> AddAsync (AddBarbequeParticipantModel participantData);
        Task<BarbequeParticipant> UpdateAsync (int id, AddBarbequeParticipantModel participantData);
        Task<bool> DeleteAsync (int id);
    }
}