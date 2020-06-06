using System.Collections.Generic;
using System.Threading.Tasks;
using barbequeue.api.Domain.Models;

namespace barbequeue.api.Domain.UseCases
{
    public interface IBarbequeService
    {
         Task<IEnumerable<Barbeque>> ListAsync ();
         Task<Barbeque> FindByIdAsync (int id);
         Task<Barbeque> AddAsync (AddBarbequeModel barbeque);
         Task<Barbeque> UpdateAsync (int id, AddBarbequeModel barbequeData);
         Task<bool> DeleteAsync (int id);
    }
}