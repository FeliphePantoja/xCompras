using Android.App;
using Android.Widget;
using Android.OS;

namespace Xcompras
{
	[Activity( Label = "Xcompras")]
	public class cad_ComprasActivity : Activity
	{
		private EditText produto;
		private EditText valor;
		private EditText quantidade;
		private Button salvar;

		protected override void OnCreate( Bundle savedInstanceState )
		{
			base.OnCreate( savedInstanceState );

			// Set our view from the "main" layout resource
			SetContentView( Resource.Layout.cadastroCompras );

			produto = (EditText)FindViewById( Resource.Id.etProduto );
			valor = (EditText)FindViewById( Resource.Id.etValor );
			quantidade = (EditText)FindViewById( Resource.Id.etQuantidade );
			salvar = (Button)FindViewById( Resource.Id.btSalvar );

		}
	}
}

