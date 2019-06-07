using Microsoft.EntityFrameworkCore;

namespace Todo.Backend.Data
{
    public class TodoItemsContext: DbContext
    {
        public TodoItemsContext(DbContextOptions<TodoItemsContext> options) : base(options)
        {

        }

        public DbSet<TodoItem> TodoItems { get; set; }
    }
}
