using System.Threading.Tasks;
using Persistence.Context;
using Persistence.Repositories.Interfaces;

namespace Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataReadContext context;

        public UnitOfWork(DataReadContext context)
        {
            this.context = context;
        }

        public async Task CommitTransactionAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}