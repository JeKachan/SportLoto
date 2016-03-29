using SportLoto.DbModels;
using System.Linq;
using System.Threading.Tasks;

namespace SportLoto.Repositories
{
    public partial class SqlRepository
    {
        public IQueryable<Transaction> Transactions => db.Transactions.AsQueryable();

        public async Task<bool> CreateTransactionAsync(Transaction instance)
        {
            db.Transactions.Add(instance);
            return await db.SaveChangesAsync() > 0;
        }

    }
}