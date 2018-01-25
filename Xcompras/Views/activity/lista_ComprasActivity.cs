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
using SQLite;
using Xcompras.Controller;
using Xcompras.Model;
using Xcompras.Views.adpter;

namespace Xcompras.Views.activity
{
	[Activity( Label = "lista_ComprasActivity" )]
	public class lista_ComprasActivity : Activity
	{
		private RecyclerView mRecyclerView;
		private RecyclerView.LayoutManager mLayoutManager;
		private RecyclerView.Adapter mAdpter;
		private List<cadProduto> produto = new List<cadProduto>();
		public event EventHandler<int> ItemClick;

		protected override void OnCreate( Bundle savedInstanceState )
		{
			base.OnCreate( savedInstanceState );
			SetContentView( Resource.Layout.listaCompras );

			mRecyclerView = (RecyclerView)FindViewById(Resource.Id.recyclerView);

			dadosBD();

			// Criando o laayout manager
			mLayoutManager = new LinearLayoutManager( this );
			mRecyclerView.SetLayoutManager(mLayoutManager);
			mAdpter = new recycleAdpter(produto);
			mRecyclerView.SetAdapter(mAdpter);

			/*
			 
			 http://sharpmobilecode.com/android-listviews-reinvented/
			 https://developer.xamarin.com/guides/android/user_interface/layouts/recycler-view/extending-the-example/
			 */

		}

		public void dadosBD()
		{
			using (SQLiteConnection bd = new DataContext().GetDataBase() )
			{
				produto = bd.Table<cadProduto>().ToList();
				//bd.DeleteAll<cadProduto>();
			}
		}

		public void OnItemClick( object sender, int position )
		{
			int photoNum = position + 1;
			Toast.MakeText( this, "This is photo number " + photoNum, ToastLength.Short ).Show();
		}
	}
}