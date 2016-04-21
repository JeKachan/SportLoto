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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public async Task<bool> CreateTicketAsync(Ticket instance)
        {
            db.Tickets.Add(instance);
            return await db.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<Ticket>> GetNotPayedTicketsAsync(string userId) =>
            await db.Tickets
                .Where(x => x.ApplicationUserId == userId && x.DrawingId == null && x.TransactionId == null)
                .ToListAsync();


        /// <summary>
        /// 
        /// </summary>
        /// <param name="ticketsId"></param>
        /// <param name="transactionId"></param>
        /// <returns></returns>
        public async Task<int> SetTicketsTransactionId(IEnumerable<int> ticketsId, int transactionId)
        {
            var tickets = await db.Tickets.Where(x => ticketsId.Contains(x.Id)).ToListAsync();
            foreach(var ticket in tickets)
            {
                ticket.TransactionId = transactionId;
            }
            return await db.SaveChangesAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<List<Ticket>> GetTicketsByIdsAsync(IEnumerable<int> ids) =>
            await db.Tickets.Where(x => ids.Contains(x.Id)).ToListAsync();

        public async Task<List<Ticket>> GetTicketsByUserDrawingIdsAsync(string userId, int drawingId) =>
            await db.Tickets.Where(x => x.ApplicationUserId == userId && x.DrawingId == drawingId).ToListAsync();
                
    }
}