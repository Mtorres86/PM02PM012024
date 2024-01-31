using System;
using SQLite;
namespace PM02PM012024.Models
{
/// <summary>
/// aqui creamos un elemento de sqlite como seria una tabla personas
/// </summary>
	[Table("Personas")]
	public class Personas
	{
		[PrimaryKey, AutoIncrement]
        public int Id { get; set; }

		[MaxLength(100)]
		public string Nombres { get; set; }

        [MaxLength(100)]
        public string Apellidos { get; set; }

        public DateTime FechaNac { get; set; }

		[Unique]
        public string Telefono { get; set; }


        public Personas()
		{
		}
	}
}

