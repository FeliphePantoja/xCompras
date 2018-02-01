using System;
using System.Collections.Generic;
using Android.Content;
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
		private Context context;

		public override int ItemCount
		{
			get { return produtos.Count; }
		}

		public recycleAdpter( List<cadProduto> p, Context c )
		{
			this.produtos = p;
			this.context = c;
		}

		public class MyView : RecyclerView.ViewHolder
		{
			public TextView produto { get; set; }
			public TextView valor { get; set; }
			public TextView localCompra { get; set; }
			public TextView Data { get; set; }
			public ImageView menu { get; set; }

			public MyView( View view, Action<int> listener ) : base( view )
			{
				produto = (TextView)view.FindViewById( Resource.Id.tvProduto );
				valor = (TextView)view.FindViewById( Resource.Id.tvValor );
				localCompra = (TextView)view.FindViewById( Resource.Id.tvLocal );
				Data = (TextView)view.FindViewById( Resource.Id.tvData );
				menu = (ImageView)view.FindViewById( Resource.Id.iv_informacao );

				view.Click += ( sender, e ) => listener( base.LayoutPosition );
			}
		}

		private void OnClick( int position )
		{
			// A linha abaixo recebe o id do produto
			position = this.produtos[position].codigo;

			if ( ItemClick != null )
			{
				ItemClick( this, position );
			}
		}

		public override void OnBindViewHolder( RecyclerView.ViewHolder holder, int position )
		{
			MyView myHolder = holder as MyView;
			myHolder.produto.Text = this.produtos[position].produto;
			myHolder.valor.Text = "R$ "+ this.produtos[position].valor;
			myHolder.localCompra.Text = this.produtos[position].local;
			myHolder.Data.Text = this.produtos[position].data;

			/*
			 assim que atribuimos o evento de click em algum botão do recyclerview lembrando que esse updateitem é um metodo
			 holder.moreButton.setOnClickListener(view -> updateItem(position)); 
			*/
			// Para o evento de click do botão na recyclerView
			myHolder.menu.Click+=( sender, e ) =>
			{
				// A linha abaixo infla o layout para que o menu seja exibido

				Android.Widget.PopupMenu popup = new Android.Widget.PopupMenu( context, myHolder.menu );

				popup.Inflate( Resource.Menu.menu_popup );

				//popup.MenuInflater.Inflate( Resource.Menu.menu_popup, popup.Menu );

				popup.MenuItemClick+=( s1, arg1 ) =>
				{
					switch ( arg1.Item.ItemId )
					{
						case Resource.Id.informacao:
							Toast.MakeText( context, "ola informação", ToastLength.Long ).Show();
							break;

						case Resource.Id.teste:
							Toast.MakeText( context, "ola teste", ToastLength.Long ).Show();
							break;

						default:

							break;
					}
				};

				popup.Show();

			};
		}

		public override RecyclerView.ViewHolder OnCreateViewHolder( ViewGroup parent, int viewType )
		{
			View row = LayoutInflater.From( parent.Context ).Inflate( Resource.Layout.layout_Lista, parent, false );

			MyView myViewHolder = new MyView( row, OnClick );
			return myViewHolder;
		}
	}
}