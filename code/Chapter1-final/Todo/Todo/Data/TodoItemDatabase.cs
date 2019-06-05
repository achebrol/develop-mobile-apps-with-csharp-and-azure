using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using SQLite;

namespace Todo.Data
{
    public class TodoItemDatabase
    {
        private static readonly string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "todoitems2.db3");
        private readonly SQLiteAsyncConnection database;

        #region Singleton
        private static TodoItemDatabase instance;

        public static TodoItemDatabase Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new TodoItemDatabase(dbPath);
                }
                return instance;
            }
        }
        #endregion

        private TodoItemDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<TodoItem>().Wait(); // turns the async operation into a synchronous one
        }

        public Task<List<TodoItem>> GetAllItemsAsync()
        {
            return database.Table<TodoItem>().ToListAsync();
        }

        public Task<TodoItem> GetItemAsync(string id)
        {
            return database.Table<TodoItem>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public async Task<TodoItem> SaveItemAsync(TodoItem item)
        {
            if (item.internalID != 0) {
                await database.UpdateAsync(item);
            }
            else
            {
                item.ID = Guid.NewGuid().ToString();
                int internalID = await database.InsertAsync(item);
                item.internalID = internalID;
            }
            return item;
        }

        public async Task DeleteItemAsync(TodoItem item)
        {
            await database.DeleteAsync(item);
        }
    }
}
