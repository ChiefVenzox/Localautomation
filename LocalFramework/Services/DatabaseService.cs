using SQLite;
using LocalFramework.Models;

namespace LocalFramework.Services
{
    public class DatabaseService
    {
        private SQLiteAsyncConnection _database;

        public DatabaseService()
        {
            InitializeAsync().Wait();
        }

        private async Task InitializeAsync()
        {
            if (_database is not null)
                return;

            _database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            var result = await _database.CreateTableAsync<QrCodeRecord>();
        }

        public async Task<List<QrCodeRecord>> GetQrCodeRecordsAsync()
        {
            return await _database.Table<QrCodeRecord>().ToListAsync();
        }

        public async Task<int> SaveQrCodeRecordAsync(QrCodeRecord record)
        {
            if (record.Id != 0)
            {
                return await _database.UpdateAsync(record);
            }
            else
            {
                return await _database.InsertAsync(record);
            }
        }

        public async Task<int> DeleteQrCodeRecordAsync(QrCodeRecord record)
        {
            return await _database.DeleteAsync(record);
        }
    }
}
