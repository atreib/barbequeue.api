using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using barbequeue.api.Domain.Models;
using barbequeue.api.Data.Protocols;
using barbequeue.api.Infra.Db.SQLServer.Contexts;
using System.Linq;
using System;

namespace barbequeue.api.Infra.Db.SQLServer.Repositories
{
    public class BarbequeRepository : BaseRepository, IBarbequeRepository
    {
        public BarbequeRepository (AppDbContext context) : base (context) { }

        public async Task<IEnumerable<Barbeque>> ListAsync ()
        {
            return await _context.Barbeques
                .Include(p => p.Participants)
                .Where(p => p.EventDateTime >= DateTime.Now)
                .OrderBy(x => x.EventDateTime)
                .ToListAsync();
        }

        public async Task<Barbeque> AddAsync (Barbeque barbeque)
        {
            await _context.Barbeques.AddAsync(barbeque);
            await _context.SaveChangesAsync(true);
            return barbeque;
        }

        public async Task<Barbeque> FindByIdAsync (int id)
        {
            return await _context.Barbeques.Include(p => p.Participants)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<bool> Remove (Barbeque barbeque)
        {
            _context.Barbeques.Remove(barbeque);
            await _context.SaveChangesAsync(true);
            return true;
        }
        
        public async Task<Barbeque> Update (Barbeque barbeque)
        {
            _context.Barbeques.Update(barbeque);
            await _context.SaveChangesAsync(true);
            return barbeque;
        }
    }
}