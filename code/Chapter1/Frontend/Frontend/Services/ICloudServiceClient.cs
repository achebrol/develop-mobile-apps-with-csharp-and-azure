namespace Frontend.Services
{
    public interface ICloudServiceClient
    {
        IDataTable<T> GetTable<T>() where T : TableData;
    }
}
