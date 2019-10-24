using System;
using System.Collections.Generic;

namespace DemoUserSaveAPILibs.Core.Data.Entity
{
    public class DataEntityObject : IDataEntityObject
    {
        Dictionary<string, object> _data;
        public DataEntityObject()
        {
            _data = new Dictionary<string, object>();
        }
        public Guid Id { get; set; }

        public T Get<T>(string key)
        {
            return (T)_data[key];
        }

        public IEnumerable<string> Keys()
        {
            return _data.Keys;
        }

        public void Set<T>(string key, T value)
        {
            if(_data.ContainsKey(key))
            {
                _data[key] = value;
                return;
            }
                
            _data.Add(key, value);
        }
    }
}
