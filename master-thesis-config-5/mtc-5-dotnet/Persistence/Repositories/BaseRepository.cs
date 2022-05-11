using Persistence.Context;

namespace Persistence.Repositories
{
    public abstract class BaseRepository
    {
        protected readonly DataReadContext context;
        
        public BaseRepository(DataReadContext context)
        {
            this.context = context;
        }

    }
}