using DemoUserSaveAPILibs.Core.Configuration;
using System.Collections.Generic;

namespace DemoUserSaveAPI.Configuration
{
    public class ApiConfigurationService : IConfigurationService
    {
        private readonly Dictionary<string, object> _configData;

        public ApiConfigurationService(Dictionary<string, object> configData)
        {
            _configData = configData;
        }
        public string Get(string key)
        {
            return Get<string>(key);
        }

        public T Get<T>(string key)
        {
            return (T)_configData[key];
        }
    }
}
