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
        public IQueryable<Drawing> Drawings => db.Drawings.AsQueryable();

        public async Task<bool> CreateDrawingAsync(Drawing instance)
        {
            db.Drawings.Add(instance);
            return await db.SaveChangesAsync() > 0;
        }

    }
}