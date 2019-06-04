using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using SQLite;

namespace Todo.Data
{
    public class TodoItemDatabase
    {
        private static readonly string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "todoitems.db3");
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

        public Task<TodoItem> GetItemAsync(int id)
        {
            return database.Table<TodoItem>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(TodoItem item)
        {
            return (item.ID != 0) ? database.UpdateAsync(item) : database.InsertAsync(item);
        }

        public Task<int> DeleteItemAsync(TodoItem item)
        {
            return database.DeleteAsync(item);
        }
    }
}
