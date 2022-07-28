using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RushAg.Infrastructure.Data
{
    public class Repository<T> : RepositoryBase<T>, IRepositoryBase<T> where T : class
    {
        public Repository(RushAgDbContext dbContext) : base(dbContext)
        { }
    }
}
