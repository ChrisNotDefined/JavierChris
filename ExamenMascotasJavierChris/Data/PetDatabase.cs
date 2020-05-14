using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using ExamenMascotasJavierChris.Models;
using ExamenMascotasJavierChris.Extensions;

namespace ExamenMascotasJavierChris.Data
{
    public class PetDatabase
    {
        // Lazy nos ayuda para que al conectar nuestra base de datos con SQLite no se bloquee la app
        static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() => {
            return new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        });

        // Nos regresa la conexion de la base de datos de SQLite
        static SQLiteAsyncConnection Database => lazyInitializer.Value;

        static bool initialized = false;

        public PetDatabase()
        {
            InitializeAsync().SafeFireAndForget(false);
        }

        async Task InitializeAsync()
        {
            if (!initialized)
            {
                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(PetModel).Name))
                {
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(PetModel)).ConfigureAwait(false);
                    initialized= true;
                }
            }
        }

        // Obtiene todas las mascotas guardadas en SQLite
        public Task<List<PetModel>> GetAllPetsAsync()
        {
            return Database.Table<PetModel>().ToListAsync();
        }

        public Task<int> SavePetAsync(PetModel item)
        {
            if (item.ID != 0)
            {
                return Database.UpdateAsync(item);
            }
            else
            {
                return Database.InsertAsync(item);
            }
        }

        public Task<int> DeletePetAsync(PetModel item)
        {
            return Database.DeleteAsync(item);
        }

        public Task<List<PetModel>> GetPetByID(int IDPet)
        {
            return Database.QueryAsync<PetModel>($"SELECT * FROM [{typeof(PetModel).Name}] WHERE ID = ?", IDPet);
        }
    }
}
