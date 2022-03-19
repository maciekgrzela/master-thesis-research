using Persistence.Context;

namespace Persistence.Repositories
{
    public abstract class BaseRepository
    {
        protected readonly DataContext context;
        
        public BaseRepository(DataContext context)
        {
            this.context = context;
        }

    }
}