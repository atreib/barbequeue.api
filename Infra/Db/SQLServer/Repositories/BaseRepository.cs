using barbequeue.api.Infra.Db.SQLServer.Contexts;

namespace barbequeue.api.Infra.Db.SQLServer.Repositories
{
    public class BaseRepository
    {
        protected readonly AppDbContext _context;

        public BaseRepository (AppDbContext context) 
        {
            _context = context;
        }
    }
}