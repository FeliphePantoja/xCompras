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

namespace Xcompras.Views.activity
{
	[Activity( Label = "Configuracao_Activity" )]
	public class Configuracao_Activity : Activity
	{
		private EditText nome;
		private EditText usuario;
		private EditText senha;
		private ImageView fechar;

		protected override void OnCreate( Bundle savedInstanceState )
		{
			base.OnCreate( savedInstanceState );
			SetContentView( Resource.Layout.configuracao );
			
			/*
			* Recuperando os Objetos do layout 
			*/
			nome = (EditText)FindViewById(Resource.Id.et_Nome);
			usuario  = (EditText)FindViewById(Resource.Id.et_Usuario);
			senha = (EditText)FindViewById(Resource.Id.etSenha);
			fechar = (ImageView)FindViewById( Resource.Id.iv_fechar );


			/*
			* Eventos
			*/

			fechar.Click +=this.Fechar_Click;

		}

		private void Fechar_Click( object sender, EventArgs e )
		{
			Finish();
		}
	}
}