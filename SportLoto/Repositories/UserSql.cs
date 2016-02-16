using SportLoto.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Web;

namespace SportLoto.Repositories
{
    public partial class SqlRepository
    {
        public IQueryable<ApplicationUser> Users => db.Users.AsQueryable();

        public async Task<ApplicationUser> FindUserAsync(string id) =>
            await db.Users.FirstAsync(x => x.Id == id);

    }
}