using System;

namespace Todo.Backend
{
    public class TodoItem
    {
        public string ID { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public string Notes { get; set; }
        public bool Done { get; set; }
    }
}
