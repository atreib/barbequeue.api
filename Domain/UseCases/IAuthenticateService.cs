using barbequeue.api.Domain.Models;
using System.Threading.Tasks;

namespace barbequeue.api.Domain.UseCases
{
    public interface IAuthenticateService
    {
         Task<User> Authenticate (LoginModel loginData);
    }
}