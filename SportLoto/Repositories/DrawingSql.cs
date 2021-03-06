﻿using SportLoto.DbModels;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace SportLoto.Repositories
{
    public partial class SqlRepository
    {
        public IQueryable<Drawing> Drawings => db.Drawings.AsQueryable();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public async Task<bool> CreateDrawingAsync(Drawing instance)
        {
            db.Drawings.Add(instance);
            return await db.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<Drawing> GetLastDrawingAsync()
        {
            return await db.Drawings.
                OrderByDescending(x => x.CreateDate).
                FirstOrDefaultAsync(x => !x.IsCompleted);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Drawing GetLastDrawing()
        {
            return db.Drawings.OrderByDescending(x => x.CreateDate).FirstOrDefault(x => !x.IsCompleted);
            
        }
    }
}