using System;
using System.Collections.Generic;
using System.Text;

namespace Frontend.Services
{
    public class AzureCloudServiceClient : ICloudServiceClient
    {
        protected Uri baseUri = new Uri("https://localhost:44398");

        public AzureCloudServiceClient()
        {

        }

        public IDataTable<T> GetTable<T>() where T : TableData
        {
            throw new NotImplementedException();
        }
    }
}
