using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using SQLite;
using Xcompras.Controller;
using Xcompras.Model;

namespace Xcompras.Views.activity
{
	[Activity( Label = "infor_ProdutoActivity", ParentActivity = typeof( lista_ComprasActivity ) )]
	[MetaData( NavUtils.ParentActivity, Value = ".lista_ComprasActivity" )]
	public class infor_ProdutoActivity : Activity
	{
		private EditText produto;
		private EditText valor;
		private EditText localCompra;
		private dadosProduto prod = new dadosProduto();
		private cadProduto produtoUpdate = new cadProduto();
		private Toolbar toolbar;

		protected override void OnCreate( Bundle savedInstanceState )
		{
			base.OnCreate( savedInstanceState );
			SetContentView( Resource.Layout.inforProduto );
			Title = "";
			// Pegando a informação que ta guardada na variavel local
			dadosProduto( GlobalData.id );

			// Recuperando os objetos do layout
			produto = (EditText)FindViewById( Resource.Id.etProduto );
			valor = (EditText)FindViewById( Resource.Id.etValor );
			localCompra = (EditText)FindViewById( Resource.Id.etLocalCompra );
			toolbar = (Toolbar)FindViewById( Resource.Id.toolbar );
			toolbar.SetNavigationIcon( Resource.Mipmap.ic_fechar );
			
			SetActionBar( toolbar );

			setaCapos();

			//prod = JsonConvert.DeserializeObject<dadosProduto>( Intent.GetStringExtra( "dadosProduto" ) );
		}

		private void dadosProduto( int codigo )
		{
			try
			{
				using ( SQLiteConnection Bd = new DadosContext().GetDatabase() )
				{
					cadProduto query = ( from produto in Bd.Table<cadProduto>() where produto.codigo == codigo select produto ).SingleOrDefault();

					prod.produto = query.produto.ToString();
					prod.valor = query.valor.ToString();
					prod.localCompra = query.local.ToString();
					prod.data = query.data.ToString();

					//Toast.MakeText( this, "Usuario"+query.codigo, ToastLength.Short ).Show();
				}
			}
			catch ( System.Exception ex )
			{
				System.Diagnostics.Debug.WriteLine( "ERRO!", ex.Message );
			}


		}

		public void setaCapos()
		{
			produto.Text = prod.produto;
			valor.Text = prod.valor;
			localCompra.Text = prod.localCompra;
		}


		public override bool OnCreateOptionsMenu( IMenu menu )
		{
			MenuInflater.Inflate( Resource.Menu.menu_infor_prod, menu );
			return base.OnCreateOptionsMenu( menu );
		}

		public override bool OnOptionsItemSelected( IMenuItem item )
		{

			switch ( item.ItemId )
			{
				case Resource.Id.menu_salvar:
					salvarInforProd();
					Finish();
					Toast.MakeText( this, "O Produto Foi Atualizado", ToastLength.Short ).Show();
					break;

				case Resource.Id.menu_excluir:
					excluirProd();
					Finish();
					Toast.MakeText( this, " O Produto Foi Excluido", ToastLength.Short ).Show();
					break;
			}

			return base.OnOptionsItemSelected( item );
		}

		private void salvarInforProd()
		{

			try
			{
				using ( SQLiteConnection bd = new DadosContext().GetDataBase() )
				{
					// A linha abaixo pega as informações dos campos para salvar no banco de dados
					produtoUpdate.codigo = GlobalData.id;
					produtoUpdate.produto = produto.Text;
					produtoUpdate.valor = valor.Text;
					produtoUpdate.local = localCompra.Text;
					produtoUpdate.data = prod.data;

					bd.Update( produtoUpdate );
				}
			}
			catch ( System.Exception ex )
			{
				System.Diagnostics.Debug.WriteLine( "ERRO!", ex.Message );
			}
		}

		private void excluirProd()
		{

			try
			{
				using ( SQLiteConnection bd = new DadosContext().GetDataBase() )
				{
					produtoUpdate.codigo = GlobalData.id;
					bd.Delete( produtoUpdate );
				}
			}
			catch ( System.Exception ex )
			{
				System.Diagnostics.Debug.WriteLine( "ERRO!", ex.Message );
			}

		}

	}
}