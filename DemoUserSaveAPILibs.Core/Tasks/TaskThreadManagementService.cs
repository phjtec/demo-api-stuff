using System;
using System.Threading.Tasks;

namespace DemoUserSaveAPILibs.Core.Tasks
{
    public class TaskThreadManagementService : ITaskThreadManagementService
    {
        public async Task<T> AnyThread<T>(Func<Task<T>> action)
        {
            return await action().ConfigureAwait(false);
        }

        public async Task<T> CallingThread<T>(Func<Task<T>> action)
        {
            return await action().ConfigureAwait(true);
        }
    }
}
