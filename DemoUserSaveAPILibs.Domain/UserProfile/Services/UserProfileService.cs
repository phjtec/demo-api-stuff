using DemoUserSaveAPILibs.Core.Data.Entity;
using DemoUserSaveAPILibs.Core.Tasks;
using DemoUserSaveAPILibs.Domain.UserProfile.Models;
using DemoUserSaveAPILibs.Domain.UserProfile.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoUserSaveAPILibs.Domain.UserProfile.Services
{
    public class UserProfileService : IUserProfileService
    {
        private readonly ITaskThreadManagementService _taskThreadManagementService;
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly IDataEntityObjectFactory _dataEntityObjectFactory;

        private Task<T> AnyThread<T>(Func<Task<T>> func) => _taskThreadManagementService.AnyThread(func);

        public UserProfileService(ITaskThreadManagementService taskThreadManagementService,
            IUserProfileRepository userProfileRepository,
            IDataEntityObjectFactory dataEntityObjectFactory)
        {
            _taskThreadManagementService = taskThreadManagementService;
            _userProfileRepository = userProfileRepository;
            _dataEntityObjectFactory = dataEntityObjectFactory;
        }

        public Task<Guid> Delete(Guid id)
        {
            throw new NotImplementedException();
        }



        public async Task<IEnumerable<UserProfileModel>> Get(Func<bool, UserProfileModel> filter = null)
        {
            var users = new List<UserProfileModel>();

            var entities = await AnyThread(() => _userProfileRepository.GetAll());

            foreach (var entity in entities)
            {
                users.Add(new UserProfileModel
                {
                    Id = entity.Keys().Any(k => k == "id") ? Guid.Parse(entity.Get<string>("id")) : Guid.Empty,
                    Username = entity.Get<string>("name")
                });
            }

            return users;
        }

        public Task<UserProfileModel> Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<Guid> Save(UserProfileModel userProfileModel)
        {

            if (userProfileModel.Id == Guid.Empty || userProfileModel.Id == null)
            {
                userProfileModel.Id = Guid.NewGuid();
            }

            var entity = _dataEntityObjectFactory.Create();
            entity.Id = userProfileModel.Id;

            entity.Set("id", userProfileModel.Id.ToString());
            entity.Set("name", userProfileModel.Username);

            await AnyThread(() => _userProfileRepository.Insert(entity));

            return userProfileModel.Id;
        }
    }
}
