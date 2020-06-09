using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using barbequeue.api.Domain.Models;
using barbequeue.api.Data.Protocols;
using barbequeue.api.Infra.Db.SQLServer.Contexts;
using System.Linq;

namespace barbequeue.api.Infra.Db.SQLServer.Repositories
{
    public class BarbequeParticipantRepository : BaseRepository, IBarbequeParticipantRepository
    {
        public BarbequeParticipantRepository (AppDbContext context) : base (context) { }

        public async Task<IEnumerable<BarbequeParticipant>> ListByBarbequeAsync (int barbequeId)
        {
            return await _context.BarbequeParticipants
                .Where(x => x.BarbequeId == barbequeId)
                .OrderBy(x => x.Name)
                .ToListAsync();
        }
        
        public async Task<BarbequeParticipant> AddAsync (BarbequeParticipant participant) 
        {
            await _context.BarbequeParticipants.AddAsync(participant);
            await _context.SaveChangesAsync(true);
            return participant;
        }
        
        public async Task<BarbequeParticipant> FindByIdAsync (int id)
        {
            return await _context.BarbequeParticipants
                .FirstOrDefaultAsync(r => r.Id == id);
        }
        
        public async Task<BarbequeParticipant> Update (BarbequeParticipant participant)
        {
            _context.BarbequeParticipants.Update(participant);
            await _context.SaveChangesAsync(true);
            return participant;
        }

        public async Task<bool> Remove (BarbequeParticipant participant)
        {
            _context.BarbequeParticipants.Remove(participant);
            await _context.SaveChangesAsync(true);
            return true;
        }
    }
}