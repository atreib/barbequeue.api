using System.Collections.Generic;
using System.Threading.Tasks;
using barbequeue.api.Domain.Models;
using barbequeue.api.Domain.UseCases;
using barbequeue.api.Data.Protocols;

namespace barbequeue.api.Data.UseCases
{
    public class BarbequeParticipantService : IBarbequeParticipantService
    {
        private readonly IBarbequeParticipantRepository _barbequeParticipantsRepository;

        public BarbequeParticipantService (IBarbequeParticipantRepository bbqParticipantsRepository) {
            _barbequeParticipantsRepository = bbqParticipantsRepository;
        }

        public async Task<IEnumerable<BarbequeParticipant>> ListByBarbequeAsync (int barbequeId)
        {
            return await _barbequeParticipantsRepository.ListByBarbequeAsync(barbequeId);
        }

        public async Task<BarbequeParticipant> FindByIdAsync (int id)
        {
            return await _barbequeParticipantsRepository.FindByIdAsync(id);
        }

        public async Task<BarbequeParticipant> AddAsync (AddBarbequeParticipantModel participantData)
        {
            BarbequeParticipant participant = new BarbequeParticipant(
                participantData.BarbequeId,
                participantData.Name, 
                participantData.Contribution,
                participantData.Paid
            );
            return await _barbequeParticipantsRepository.AddAsync(participant);
        }

        public async Task<BarbequeParticipant> UpdateAsync (int id, AddBarbequeParticipantModel participantData)
        {
            BarbequeParticipant participant = new BarbequeParticipant(
                participantData.BarbequeId, 
                participantData.Name,
                participantData.Contribution,
                participantData.Paid
            );
            participant.Id = id;
            return await _barbequeParticipantsRepository.Update(participant);
        }

        public async Task<bool> DeleteAsync (int id)
        {
            BarbequeParticipant bbq = await _barbequeParticipantsRepository.FindByIdAsync(id);
            return await _barbequeParticipantsRepository.Remove(bbq);
        }
    }
}