using DemoUserSaveAPILibs.Core.Data.Entity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoUserSaveAPILibs.Core.Data.IO.Providers
{
    public interface IDataAccessProvider
    {
        Task<IEnumerable<IDataEntityObject>> Select(string from);
        Task<Guid> Insert(string into, IDataEntityObject item);
        Task<Guid> Update(string into, IDataEntityObject item);
    }
}
