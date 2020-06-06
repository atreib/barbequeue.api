using System.Collections.Generic;
using System.Threading.Tasks;
using barbequeue.api.Domain.Models;

namespace barbequeue.api.Data.Protocols
{
    public interface IBarbequeRepository
    {
         Task<IEnumerable<Barbeque>> ListAsync ();
         Task<Barbeque> AddAsync (Barbeque barbeque);
         Task<Barbeque> FindByIdAsync (int id);
         Task<Barbeque> Update (Barbeque barbeque);
         Task<bool> Remove (Barbeque barbeque);
    }
}