using System;

namespace Tailwinds.OData.Models
{
    public class Album
    {
        #region Mobile Context
        public string Id { get; set; }
        public DateTimeOffset LastUpdated { get; set; }
        public bool Deleted { get; set; }
        #endregion

        public string Title { get; set; }
    }
}
