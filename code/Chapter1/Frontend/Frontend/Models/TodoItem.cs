using Frontend.Services;

namespace Frontend.Models
{
    public class TodoItem : TableData
    {
        public string Title { get; set; }
        public bool IsComplete { get; set; }
    }
}
