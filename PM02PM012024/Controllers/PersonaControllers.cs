using System;
using SQLite;
using PM02PM012024.Models;
namespace PM02PM012024.Controllers
{
	//permite controlar nuestra base de datos. 
	public class PersonaControllers
	{
        SQLiteAsyncConnection _connection;

		//constructor vacio
        public PersonaControllers(){ }

        //conexion a la base de datos

        public async Task Init()
			{
				if (_connection is not null)
				{
					return;
				}

			SQLite.SQLiteOpenFlags extensiones =SQLite.SQLiteOpenFlags.ReadWrite |
                                                SQLite.SQLiteOpenFlags.Create |
                                                SQLite.SQLiteOpenFlags.SharedCache;
			_connection = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, "DBPersonas.db3"),extensiones);

			var creacion = await _connection.CreateTableAsync<Models.Personas>();


			}

		//crear los metodos CRUD para la clase persona

		//Create
		public async Task <int> StorePerson(Personas personas)
		{
			Console.WriteLine(personas);
			
			await Init();

			if (personas.Id == 0)
			{
				return await _connection.InsertAsync(personas);

			}
			else
			{
                await Init();
                return await _connection.UpdateAsync(personas);
			}
		}

		//READ, podemos retornar un solo valor o todos los elementos de manera asincrona.

		public async Task<List<Models.Personas>> GetListPersons()
		{
			await Init();
			return await _connection.Table<Personas>().ToListAsync();
		}

		// Read Element
        public async Task<Models.Personas> GetPersons(int pid)
        {
            await Init();
            return await _connection.Table<Personas>().Where(i => i.Id == pid).FirstOrDefaultAsync();
        }

		//Delete element

		public async Task<int> DeletePerson(Personas personas)
		{
			await Init();
			return await _connection.DeleteAsync(personas);
		}


    }
}

