using SportLoto.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportLoto.Repositories
{
    public interface ISqlRepository : IDisposable
    {

        #region Ticket

        IQueryable<Ticket> Tickets { get; }
        Task<bool> CreateTicketAsync(Ticket instance);
        Task<List<Ticket>> GetNotPayedTicketsAsync(string userId);

        #endregion

        #region Drawing

        IQueryable<Drawing> Drawings { get; }
        Task<bool> CreateDrawingAsync(Drawing instance);
        Task<Drawing> GetLastDrawingAsync();
        Drawing GetLastDrawing();

        #endregion

        #region User

        IQueryable<ApplicationUser> Users { get; }
        Task<ApplicationUser> FindUserAsync(string id);

        #endregion

        #region Transaction
        IQueryable<Transaction> Transactions { get; }
        Task<bool> CreateTransactionAsync(Transaction instance);
        #endregion
    }
}
