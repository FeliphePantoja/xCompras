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
using Xcompras.Model;

namespace Xcompras.Views.adpter
{
	public class recycleAdpter : RecyclerView.Adapter
	{
		private List<cadProduto> produtos;
		public event EventHandler<int> ItemClick;

		public override int ItemCount
		{
			get { return produtos.Count; }
		}

		public recycleAdpter(List<cadProduto> p)
		{
			this.produtos = p;
		}

		public class MyView : RecyclerView.ViewHolder
		{
			public TextView produto { get; set; }
			public TextView valor { get; set; }
			public TextView localCompra { get; set; }

			public MyView( View view , Action<int> listener) : base( view )
			{
				produto = (TextView)view.FindViewById( Resource.Id.tvProduto );
				valor = (TextView)view.FindViewById(Resource.Id.tvValor);
				localCompra = (TextView)view.FindViewById(Resource.Id.tvLocal);

				view.Click += ( sender, e ) => listener( base.LayoutPosition );

			}
		}

		private void OnClick( int position )
		{
			if ( ItemClick != null )
			{
				ItemClick( this, position );
			}
		}


		public override void OnBindViewHolder( RecyclerView.ViewHolder holder, int position )
		{
			MyView myHolder = holder as MyView;
			myHolder.produto.Text = this.produtos[position].produto;
			myHolder.valor.Text = this.produtos[position].valor;
			myHolder.localCompra.Text = this.produtos[position].local;
		}

		public override RecyclerView.ViewHolder OnCreateViewHolder( ViewGroup parent, int viewType )
		{
			View row = LayoutInflater.From( parent.Context ).Inflate(Resource.Layout.layout_Lista, parent, false);

			MyView myViewHolder = new MyView(row, OnClick);

			return myViewHolder;
		}
	}
}