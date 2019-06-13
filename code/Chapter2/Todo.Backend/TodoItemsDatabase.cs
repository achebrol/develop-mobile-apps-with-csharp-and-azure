using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Todo.Backend
{
    public class TodoItemsDatabase
    {
        #region Singleton
        private static TodoItemsDatabase instance;

        public static TodoItemsDatabase Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new TodoItemsDatabase();
                }
                return instance;
            }
        }
        #endregion

        #region Implementation
        private IDictionary<string,TodoItem> items;

        private TodoItemsDatabase()
        {
            items = new Dictionary<string, TodoItem>();
        }

        public async Task<List<TodoItem>> GetAllItemsAsync()
        {
            return new List<TodoItem>(items.Values);
        }

        public async Task<TodoItem> GetItemAsync(string id)
        {
            return items[id];
        }

        public async Task<TodoItem> SaveItemAsync(TodoItem item)
        {
            if (item.ID == null)
            {
                item.ID = Guid.NewGuid().ToString();
            }
            if (items.ContainsKey(item.ID))
            {
                items[item.ID] = item;
            } else
            {
                items.Add(item.ID, item);
            }
            return items[item.ID];
        }

        public async Task DeleteItemAsync(string id)
        {
            items.Remove(id);
        }
        #endregion
    }
}

