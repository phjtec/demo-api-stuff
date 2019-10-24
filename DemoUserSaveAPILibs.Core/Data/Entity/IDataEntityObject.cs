using System;
using System.Collections.Generic;
using System.Text;

namespace DemoUserSaveAPILibs.Core.Data.Entity
{
    public interface IDataEntityObject
    {
        Guid Id { get; set; }
        T Get<T>(string key);
        void Set<T>(string key, T value);
        IEnumerable<string> Keys();
    }
}
