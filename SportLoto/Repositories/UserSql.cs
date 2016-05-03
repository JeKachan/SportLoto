using SportLoto.DbModels;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;

namespace SportLoto.Repositories
{
    public partial class SqlRepository
    {
        public IQueryable<ApplicationUser> Users => db.Users.AsQueryable();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ApplicationUser> FindUserAsync(string id) =>
            await db.Users.FirstAsync(x => x.Id == id);

    }
}