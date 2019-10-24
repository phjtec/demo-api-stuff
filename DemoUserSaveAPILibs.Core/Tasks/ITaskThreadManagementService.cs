using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace DemoUserSaveAPILibs.Core.Tasks
{
    public interface ITaskThreadManagementService
    {
        Task<T> AnyThread<T>(Func<Task<T>> action);
        Task<T> CallingThread<T>(Func<Task<T>> action);
    }
}
