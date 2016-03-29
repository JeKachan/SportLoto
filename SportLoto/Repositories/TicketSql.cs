using SportLoto.DbModels;
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

        public async Task<List<Ticket>> GetNotPayedTicketsAsync(string userId) =>
            await db.Tickets.Where(x => x.ApplicationUserId == userId && x.DrawingId == null).ToListAsync();


        public async Task<int> SetTicketsTransactionId(IEnumerable<int> ticketsId, int transactionId)
        {
            var tickets = await db.Tickets.Where(x => ticketsId.Contains(x.Id)).ToListAsync();
            foreach(var ticket in tickets)
            {
                ticket.TransactionId = transactionId;
            }
            return await db.SaveChangesAsync();
        }
    }
}