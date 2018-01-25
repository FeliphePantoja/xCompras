using SQLite;

namespace Xcompras.Model
{
	[Table( "cadUsuario" )]
	public class cadUsuario
	{
		[PrimaryKey, AutoIncrement, Column("_id")]
		public int id { get; set; }
		[MaxLength( 3 )]

		public string nome { get; set; }
		public string usuario { get; set; }
		public string senha { get; set; }

	}
}