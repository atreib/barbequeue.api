using System.Collections.Generic;
using System.Threading.Tasks;
using barbequeue.api.Domain.Models;

namespace barbequeue.api.Data.Protocols
{
    public interface IUserRepository
    {
         Task<User> GetLogin (string login);
    }
}