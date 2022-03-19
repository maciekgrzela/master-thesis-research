using System.Threading.Tasks;

namespace Persistence.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
         Task CommitTransactionAsync();
    }
}