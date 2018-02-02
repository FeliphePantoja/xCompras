using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using SQLite;
using Xcompras.Controller;
using Xcompras.Model;
using Xcompras.Views.activity;

namespace Xcompras
{
	[Activity( Label = "Xcompras")]
	public class cad_ComprasActivity : Activity
	{
		private EditText produto;
		private EditText valor;
		private EditText local;
		private TextView data;
		private Button salvar;
		private string Data;
		private TextView valorTotalGasto;
		private Toolbar toolbar;

		protected override void OnCreate( Bundle savedInstanceState )
		{
			base.OnCreate( savedInstanceState );
			SetContentView( Resource.Layout.cadastroCompras );

			// Recuperando os objetos do layout
			produto = (EditText)FindViewById( Resource.Id.etProduto );
			valor = (EditText)FindViewById( Resource.Id.etValor );
			local = (EditText)FindViewById( Resource.Id.etLocalCompra );
			salvar = (Button)FindViewById( Resource.Id.btSalvar );
			data = (TextView)FindViewById( Resource.Id.tvData );
			valorTotalGasto = (TextView)FindViewById( Resource.Id.tvTotalGasto );
			toolbar = (Toolbar)FindViewById(Resource.Id.toolbar_cadCompras );

			SetActionBar(toolbar);

			// Para a data
			DateTime date = DateTime.Now;
			string formato = "dd/MM/yyyy";
			data.Text = date.ToString( formato );
			Data = date.ToString( formato );

			salvar.Click +=this.Salvar_Click;
			valorTotalGasto.Click +=this.ValorTotalGasto_Click;
			valorTotal();
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
					cadastroProd.data = Data;

					using ( SQLiteConnection db = new DadosContext().GetDatabase() )
					{
						db.Insert( cadastroProd );
						Toast.MakeText( this, "Cadastro realizado com sucesso", ToastLength.Short ).Show();

						this.produto.Text = "";
						this.valor.Text = "";
						this.local.Text = "";

						db.Close();
					}
					// Para atualizar o valor da venda
					valorTotal();
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
					Intent intent = new Intent( this, typeof( lista_ComprasActivity ) );
					StartActivity( intent );
					break;

				case Resource.Id.menu_sair:
					Finish();
					break;

				case Resource.Id.menu_configuracao:
					Intent config = new Intent( this, typeof( Configuracao_Activity ) );
					StartActivity( config );
					break;
			}

			return base.OnOptionsItemSelected( item );
		}

		private void valorTotal()
		{
			// Essa variavel é criada para que os produtos seja somado
			decimal guardaTotal = 0;

			using ( SQLiteConnection db = new DadosContext().GetDataBase() )
			{

				List<cadProduto> c = db.Table<cadProduto>().ToList();
				//List<cadProduto> c =  db.Query<cadProduto>( "SELECT * FROM cadProduto WHERE valor" );

				foreach ( var prod in c )
				{
					guardaTotal += Convert.ToDecimal( prod.valor );
				}
				db.Close();
				// Para exibir todos os valores 
				valorTotalGasto.Text ="R$ "+ Convert.ToString( guardaTotal );
			}
		}

		private void ValorTotalGasto_Click( object sender, EventArgs e )
		{
			Intent intent = new Intent( this, typeof( lista_ComprasActivity ) );
			StartActivity( intent );
		}

		protected override void OnStart()
		{
			base.OnStart();
			valorTotal();
		}

	}
}

