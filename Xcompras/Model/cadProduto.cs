using SQLite;

namespace Xcompras.Model
{
	[Table( "cadProduto" )]
	public class cadProduto
	{
		[PrimaryKey, AutoIncrement, Column( "_codigo" )]
		public int codigo { get; set; }
		[MaxLength( 3 )]

		public string produto { get; set; }
		public string valor  { get; set; }
		public string local { get; set; }

	}
}