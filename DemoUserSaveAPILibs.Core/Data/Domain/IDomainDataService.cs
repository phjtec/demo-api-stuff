using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoUserSaveAPILibs.Core.Data.Domain
{
    public interface IDomainDataService<T> where T : IDomainModel
    {
        Task<Guid> Save(T model);
        Task<IEnumerable<T>> Get(Func<bool,T> filter = null);
        Task<T> Get(Guid id);
        Task<Guid> Delete(Guid id);
    }
}
