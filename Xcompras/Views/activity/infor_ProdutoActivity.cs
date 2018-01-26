using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using SQLite;
using Xcompras.Controller;
using Xcompras.Model;

namespace Xcompras.Views.activity
{
	[Activity( Label = "infor_ProdutoActivity" )]
	public class infor_ProdutoActivity : Activity
	{
		private EditText produto;
		private EditText valor;
		private EditText localCompra;
		dadosProduto prod = new dadosProduto();

		protected override void OnCreate( Bundle savedInstanceState )
		{
			base.OnCreate( savedInstanceState );
			SetContentView( Resource.Layout.inforProduto );
			Title = "";
			dadosProduto( GlobalData.id );

			produto = (EditText)FindViewById( Resource.Id.etProduto );
			valor = (EditText)FindViewById( Resource.Id.etValor );
			localCompra = (EditText)FindViewById( Resource.Id.etLocalCompra );

			setaCapos();

			//prod = JsonConvert.DeserializeObject<dadosProduto>( Intent.GetStringExtra( "dadosProduto" ) );
		}

		private void dadosProduto( int codigo )
		{
			using ( SQLiteConnection Bd = new DataContext().GetDatabase() )
			{
				cadProduto query = ( from produto in Bd.Table<cadProduto>() where produto.codigo == codigo select produto ).SingleOrDefault();

				prod.produto = query.produto.ToString();
				prod.valor = query.valor.ToString();
				prod.localCompra = query.local.ToString();

				//Toast.MakeText( this, "Usuario"+query.codigo, ToastLength.Short ).Show();
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
					Toast.MakeText( this, "EM DESENVOLVIMENTO", ToastLength.Short ).Show();
					break;

				case Resource.Id.menu_excluir:
					Toast.MakeText( this, "EM DESENVOLVIMENTO", ToastLength.Short ).Show();
					break;
			}

			return base.OnOptionsItemSelected( item );
		}
	}
}