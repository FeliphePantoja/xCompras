using System.Collections.Generic;
using System.Linq;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.Widget;
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
		private cadProduto prod = new cadProduto();

		protected override void OnCreate( Bundle savedInstanceState )
		{
			base.OnCreate( savedInstanceState );
			SetContentView( Resource.Layout.listaCompras );

			mRecyclerView = (RecyclerView)FindViewById( Resource.Id.recyclerView );

			// A linha abaixo cria a lista pela primeira vez
			myLista();
		}

		// A linha abaixo atualiza a lista quando os dados são alterados ou excluidos
		protected override void OnStart()
		{
			base.OnStart();
			myLista();
		}

		public void myLista()
		{
			// Recuperando as informações do banco e colocando na lista
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
				//bd.Execute("SELECT * FROM cadProduto");
			}
		}

		public void OnItemClick( object sender, int position )
		{
			
			GlobalData.id = position;
			Intent intent = new Intent( this, typeof( infor_ProdutoActivity ) );
			StartActivity( intent );
			//intent.PutExtra("dadosProduto", JsonConvert.SerializeObject( produto ) );

		}
	}
}