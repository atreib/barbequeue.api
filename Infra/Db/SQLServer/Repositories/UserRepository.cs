using System.Threading.Tasks;
using barbequeue.api.Domain.Models;
using barbequeue.api.Data.Protocols;
using barbequeue.api.Infra.Db.SQLServer.Contexts;
using Microsoft.EntityFrameworkCore;

namespace barbequeue.api.Infra.Db.SQLServer.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository (AppDbContext context) : base (context) { }

        public async Task<User> GetLogin (string login) 
        {
            return await _context.Users.FirstOrDefaultAsync(x => 
                x.Username == login
            );
        }
    }
}