using DemoUserSaveAPILibs.Core.Data.Entity;
using DemoUserSaveAPILibs.Core.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoUserSaveAPILibs.Domain.UserProfile.Repositories
{
    public interface IUserProfileRepository : IDataRepository
    {
        //TODO: Make IDataRepository<T> and return some kind of userprofile entity over the DataEntity
        Task<IEnumerable<IDataEntityObject>> GetAll();
        Task<Guid> Insert(IDataEntityObject item);
    }
}
