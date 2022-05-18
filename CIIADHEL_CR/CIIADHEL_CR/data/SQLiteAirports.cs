using CIIADHEL_CR.models;
using SQLite;
using System;
using System.Threading.Tasks;

namespace CIIADHEL_CR.data
{
    public class SQLiteAirports
    {
        SQLiteAsyncConnection db;
        #region Conexion sqlLite
        public SQLiteAirports(string dbPath)
        {
            // dbPath contains a valid file path for the database file to be stored
            db = new SQLiteAsyncConnection(dbPath);
            db.CreateTableAsync<AirportLite>().Wait();
        }
        #endregion
        //***********************************************************************************************
        #region Guardar Aeropuerto Sqllite
        public async Task<int> SaveAirportAsync(AirportLite airport, bool isNewData = false)
        {
            try
            {
                int resulDB;
                if (!isNewData)
                {
                    resulDB = await db.DeleteAsync<AirportLite>(airport.ID_Aeropuerto);
                    if (resulDB == 0)
                    {
                        return 0;
                    }
                }
                resulDB = await db.InsertAsync(airport);
                if (resulDB == 0)
                {
                    return 0;
                }
                return resulDB;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        //***********************************************************************************************
        #region obtiene aeropuerto por IDAirport ->GetAnAirportByIDAsync(int id)
        public Task<AirportLite> GetAnAirportByIDAsync(int id)
        {
            return db.GetAsync<AirportLite>(id);
        }
        #endregion
        //***********************************************************************************************
        #region Existe aeropuerto ->GetExistsAirportsAsync(int id)
        public Task<int> GetExistsAirportsAsync(int id)
        {
            return db.Table<AirportLite>().Where(air => air.ID_Aeropuerto == id).CountAsync();
        }
        #endregion
        //***********************************************************************************************
        #region Obtiene ultima Actualizacion del aeropuerto ->GetLastDateAirportsAsync(int id)
        public string GetLastDateAirportsAsync(int id)
        {
            return db.GetAsync<AirportLite>(id).Result.Ultima_Actualizacion_Aeropuerto;
        }
        #endregion
        //***********************************************************************************************
    }
}
