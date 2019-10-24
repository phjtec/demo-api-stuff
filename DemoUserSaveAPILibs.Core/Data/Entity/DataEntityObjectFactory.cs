namespace DemoUserSaveAPILibs.Core.Data.Entity
{
    public class DataEntityObjectFactory : IDataEntityObjectFactory
    {
        public IDataEntityObject Create()
        {
            return new DataEntityObject();
        }
    }
}
