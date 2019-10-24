using System;
using System.Collections.Generic;
using System.Text;

namespace DemoUserSaveAPILibs.Core.Configuration
{
    public interface IConfigurationService
    {
        string Get(string key);
        T Get<T>(string key);
    }
}
