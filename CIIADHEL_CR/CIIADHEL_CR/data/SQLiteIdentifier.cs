using CIIADHEL_CR.models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CIIADHEL_CR.data
{
    public class SQLiteIdentifier
    {
        SQLiteAsyncConnection db;
        public SQLiteIdentifier(string dbPath)
        {
            // dbPath contains a valid file path for the database file to be stored

            db = new SQLiteAsyncConnection(dbPath);
            db.CreateTableAsync<Identifier>().Wait();

        }

        // Insert Identifier 
        public Task<int> SaveIdentifierAsync(Identifier identifier)
        {
            try
            {
                return db.InsertAsync(identifier);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> existIdentifier()
        {
            if (await db.Table<Identifier>().CountAsync() > 0)
            {
                return true;
            }

            return false;
        }

        public Task<List<Identifier>> getIdentifier()
        {
            return db.Table<Identifier>().ToListAsync();
        }

    }
}
