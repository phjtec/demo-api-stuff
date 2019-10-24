using DemoUserSaveAPILibs.Core.Data.Entity;
using DemoUserSaveAPILibs.Core.Data.IO.Providers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoUserSaveAPILibs.Domain.UserProfile.Repositories
{
    public class UserProfileRepository : IUserProfileRepository
    {
        private readonly string CollectionName = "UserProfile";

        private readonly IDataAccessProvider _dataAccessProvider;

        public UserProfileRepository(IDataAccessProvider dataAccessProvider)
        {
            _dataAccessProvider = dataAccessProvider;
        }

        public async Task<IEnumerable<IDataEntityObject>> GetAll()
        {
            return await _dataAccessProvider.Select(CollectionName).ConfigureAwait(false);
        }

        public async Task<Guid> Insert(IDataEntityObject item)
        {
            return await _dataAccessProvider.Insert(CollectionName, item).ConfigureAwait(false);
        }
    }
}
