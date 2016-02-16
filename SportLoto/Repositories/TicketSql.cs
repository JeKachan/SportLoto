using SportLoto.DbModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace SportLoto.Repositories
{
    public partial class SqlRepository 
    {
        public IQueryable<Ticket> Tickets => db.Tickets.AsQueryable();

        public async Task<bool> CreateTicketAsync(Ticket instance)
        {
            db.Tickets.Add(instance);
            return await db.SaveChangesAsync() > 0;
        }
        
    }
}