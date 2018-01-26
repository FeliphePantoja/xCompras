using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using SQLite;
using Xcompras.Controller;
using Xcompras.Model;
using Xcompras.Views.adpter;

namespace Xcompras.Views.activity
{
	[Activity( Label = "Lista de Compras" )]
	public class lista_ComprasActivity : Activity
	{
		private RecyclerView mRecyclerView;
		private RecyclerView.LayoutManager mLayoutManager;
		private recycleAdpter mAdpter;
		private List<cadProduto> produto = new List<cadProduto>();

		protected override void OnCreate( Bundle savedInstanceState )
		{
			base.OnCreate( savedInstanceState );
			SetContentView( Resource.Layout.listaCompras );

			mRecyclerView = (RecyclerView)FindViewById( Resource.Id.recyclerView );

			dadosBD();

			// Criando o laayout manager
			mLayoutManager = new LinearLayoutManager( this );
			mRecyclerView.SetLayoutManager( mLayoutManager );
			mAdpter = new recycleAdpter( produto );
			mAdpter.ItemClick += OnItemClick;
			mRecyclerView.SetAdapter( mAdpter );
			mRecyclerView.AddItemDecoration( new DividerItemDecoration( this, DividerItemDecoration.Vertical ) );

		}

		public void dadosBD()
		{
			using ( SQLiteConnection bd = new DataContext().GetDataBase() )
			{
				produto = bd.Table<cadProduto>().ToList();
				//bd.DeleteAll<cadProduto>();
				//bd.Execute("drop table cadProduto");
			}
		}

		public void OnItemClick( object sender, int position )
		{
			int itemNum = position + 1;
			//int tipo = position.GetType();
			//Toast.MakeText( this, "O Click foi: " + itemNum, ToastLength.Short ).Show();

			//dadosProduto produto = new dadosProduto();
			//produto.id = position + 1;
			GlobalData.id = itemNum;
			Intent intent = new Intent( this, typeof( infor_ProdutoActivity ) );
			//intent.PutExtra("dadosProduto", JsonConvert.SerializeObject( produto ) );
			StartActivity( intent );
		}
	}
}