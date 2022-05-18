using CIIADHEL_CR.models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CIIADHEL_CR.data
{
    public class SQLiteAirportsPrincipal
    {
        SQLiteAsyncConnection db;
        public SQLiteAirportsPrincipal(string dbPath)
        {
            // dbPath contains a valid file path for the database file to be stored
            db = new SQLiteAsyncConnection(dbPath);
            db.CreateTableAsync<Airport_Principal>().Wait();
        }
        //*************************************************************************
        #region Guardar y actualizar aeropuertos -> Insert and update
        public Task<int> SaveAirportAsync(Airport_Principal airport)
        {
            try
            {
                return db.InsertAsync(airport); //return airport
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex); //show error on console
                throw ex;
            }
        }
        //****
        public async Task<int> SaveAllAirports(List<Airport_Principal> airports, bool isNewData = false)
        {
            try
            {
                int resulDB;
                if (!isNewData)
                {
                    resulDB = await db.DeleteAllAsync<Airport_Principal>();
                    if (resulDB == 0)
                    {
                        return 0;
                    }
                }

                resulDB = await db.InsertAllAsync(airports);
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
        //***********************************************************************
        #region Borrado de aeropuerto ->DeleteAllAirportsAsync()
        public Task<int> DeleteAllAirportsAsync()
        {
            return db.DeleteAllAsync<Airport_Principal>();
        }
        public Task<int> UpdateAirportAsync(Airport_Principal airport)
        {
            return db.UpdateAsync(airport);
        }
        #endregion
        //***********************************************************************
        #region Lista de Aeropuertos -->GetAllAirportAsync()
        public Task<List<Airport_Principal>> GetAllAirportAsync()
        {
            return db.Table<Airport_Principal>().ToListAsync();
        }

        public Task<int> GetLengthAirportsAsync()
        {
            return db.Table<Airport_Principal>().CountAsync();
        }
        #endregion
        // **********************************************************************
        #region Lista de Aeropuertos por id -->GetAirportByIdAsync(int ID_Aeropuerto)
        public Task<Airport_Principal> GetAirportByIdAsync(int ID_Aeropuerto)
        {
            return db.Table<Airport_Principal>().Where(a => a.ID_Aeropuerto == ID_Aeropuerto).FirstOrDefaultAsync();
        }
        #endregion
        //***********************************************************************
        #region Buscar con sqllite ->GetSearchAsync(string Search)
        public Task<List<Airport_Principal>> GetSearchAsync(string Search)
        {
            #region Busqueda de sqlite old
            // return  table, find the airport by Airport name or ICAO name or OACI name 
            //return db.Table<Airport_Principal>().Where(a => a.Nombre.StartsWith(Search) || a.NombreICAO.StartsWith(Search)
            //|| a.Nombre_OACI.StartsWith(Search) || a.Nombre.ToLower().StartsWith(Search) || a.NombreICAO.ToLower().StartsWith(Search)
            //|| a.Nombre_OACI.ToLower().StartsWith(Search) || a.Nombre_OACI.ToUpper().StartsWith(Search)
            //|| a.NombreICAO.ToUpper().StartsWith(Search) || a.Nombre.ToUpper().StartsWith(Search)

            //|| a.Nombre.EndsWith(Search) || a.NombreICAO.EndsWith(Search)
            //|| a.Nombre_OACI.EndsWith(Search) || a.Nombre.ToLower().EndsWith(Search) || a.NombreICAO.ToLower().EndsWith(Search)
            //|| a.Nombre_OACI.ToLower().EndsWith(Search) || a.Nombre_OACI.ToUpper().EndsWith(Search)
            //|| a.NombreICAO.ToUpper().EndsWith(Search) || a.Nombre.ToUpper().EndsWith(Search)

            //|| a.Nombre.Contains(Search) || a.NombreICAO.Contains(Search) || a.Nombre_OACI.Contains(Search)
            //).ToListAsync();
            #endregion
            return db.Table<Airport_Principal>().Where(a => a.Nombre.ToUpper().Contains(Search.ToUpper()) 
                || a.NombreICAO.ToUpper().Contains(Search.ToUpper()) 
                || a.Nombre_OACI.ToUpper().Contains(Search.ToUpper())
            ).ToListAsync();

        }
        #endregion
    }
}
