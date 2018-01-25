using Android.App;
using Android.Widget;
using Android.OS;
using Xcompras.Model;
using SQLite;
using Xcompras.Controller;
using Android.Views;
using Android.Content;
using Xcompras.Views.activity;

namespace Xcompras
{
	[Activity( Label = "Xcompras" )]
	public class cad_ComprasActivity : Activity
	{
		private EditText produto;
		private EditText valor;
		private EditText local;
		private Button salvar;

		protected override void OnCreate( Bundle savedInstanceState )
		{
			base.OnCreate( savedInstanceState );

			// Set our view from the "main" layout resource
			SetContentView( Resource.Layout.cadastroCompras );

			produto = (EditText)FindViewById( Resource.Id.etProduto );
			valor = (EditText)FindViewById( Resource.Id.etValor );
			local = (EditText)FindViewById( Resource.Id.etLocalCompra );
			salvar = (Button)FindViewById( Resource.Id.btSalvar );

			salvar.Click +=this.Salvar_Click;
		}

		private void Salvar_Click( object sender, System.EventArgs e )
		{
			salvarProduto();
		}

		private void salvarProduto()
		{

			try
			{
				cadProduto cadastroProd = new cadProduto();

				if ( string.IsNullOrEmpty( this.produto.Text ).Equals( false ) && string.IsNullOrEmpty( this.valor.Text ).Equals( false ) && string.IsNullOrEmpty( this.local.Text ).Equals( false ) )
				{
					cadastroProd.produto = produto.Text;
					cadastroProd.valor = valor.Text;
					cadastroProd.local = local.Text;

					using ( SQLiteConnection db = new DataContext().GetDatabase() )
					{
						db.Insert( cadastroProd );
						Toast.MakeText( this, "Cadastro realizado com sucesso", ToastLength.Short ).Show();

						this.produto.Text = "";
						this.valor.Text = "";
						this.local.Text = "";
					}
				}
				else
				{
					Toast.MakeText( this, "Existe Campo Vazios", ToastLength.Long ).Show();
				}
			}
			catch ( System.Exception ex )
			{
				Toast.MakeText( this, "Cadastro não realizado", ToastLength.Short ).Show();
				System.Diagnostics.Debug.WriteLine( "ERRO!", ex.Message );
			}
		}

		public override bool OnCreateOptionsMenu( IMenu menu )
		{
			MenuInflater.Inflate( Resource.Menu.menu, menu );
			return base.OnCreateOptionsMenu( menu );
		}

		public override bool OnOptionsItemSelected( IMenuItem item )
		{

			switch ( item.ItemId )
			{
				case Resource.Id.menu_lista:
					Intent intent = new Intent(this, typeof(lista_ComprasActivity));
					StartActivity(intent);
					break;
			}

			return base.OnOptionsItemSelected( item );
		}

	}
}

