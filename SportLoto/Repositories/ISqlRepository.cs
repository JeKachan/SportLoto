using SportLoto.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportLoto.Repositories
{
    interface ISqlRepository : IDisposable
    {

        #region Ticket

        IQueryable<Ticket> Tickets { get; }
        Task<bool> CreateTicketAsync(Ticket instance);

        #endregion

        #region Drawing

        IQueryable<Drawing> Drawings { get; }
        Task<bool> CreateDrawingAsync(Drawing instance);

        #endregion

        #region User

        IQueryable<ApplicationUser> Users { get; }
        Task<ApplicationUser> FindUserAsync(string id);

        #endregion
    }
}
