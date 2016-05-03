using SportLoto.DbModels;
using System.Linq;
using System.Threading.Tasks;

namespace SportLoto.Repositories
{
    public partial class SqlRepository
    {
        /// <summary>
        /// 
        /// </summary>
        public IQueryable<Transaction> Transactions => db.Transactions.AsQueryable();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public async Task<bool> CreateTransactionAsync(Transaction instance)
        {
            db.Transactions.Add(instance);
            return await db.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Transaction> GetTransactionByIdAsync(int id) =>
            await db.Transactions.FindAsync(id);

    }
}