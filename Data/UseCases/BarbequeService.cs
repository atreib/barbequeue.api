using System.Collections.Generic;
using System.Threading.Tasks;
using barbequeue.api.Domain.Models;
using barbequeue.api.Domain.UseCases;
using barbequeue.api.Data.Protocols;

namespace barbequeue.api.Data.UseCases
{
    public class BarbequeService : IBarbequeService
    {
        private readonly IBarbequeRepository _barbequeRepository;

        public BarbequeService (IBarbequeRepository barbequeRepository) {
            _barbequeRepository = barbequeRepository;
        }

        public async Task<IEnumerable<Barbeque>> ListAsync ()
        {
            return await _barbequeRepository.ListAsync();
        }

        public async Task<Barbeque> FindByIdAsync (int id)
        {
            return await _barbequeRepository.FindByIdAsync(id);
        }

        public async Task<Barbeque> AddAsync (AddBarbequeModel insertedBbq)
        {
            Barbeque bbq = new Barbeque(insertedBbq.Description, insertedBbq.EventDateTime);
            return await _barbequeRepository.AddAsync(bbq);
        }

        public async Task<Barbeque> UpdateAsync (int id, AddBarbequeModel updatedBbq)
        {
            Barbeque bbq = new Barbeque(updatedBbq.Description, updatedBbq.EventDateTime);
            bbq.Id = id;
            return await _barbequeRepository.Update(bbq);
        }

        public async Task<bool> DeleteAsync (int id)
        {
            Barbeque bbq = await _barbequeRepository.FindByIdAsync(id);
            return await _barbequeRepository.Remove(bbq);
        }
    }
}