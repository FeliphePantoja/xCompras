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
using SQLite;
using Xcompras.Controller;
using Xcompras.Model;

namespace Xcompras.Views.activity
{
	[Activity( Label = "Xcompras", MainLauncher = true )]
	public class LoginActivity : Activity
	{
		private EditText usuario;
		private EditText senha;
		private TextView novoCadastro;
		private Button acessar;

		// Para recuperar o objeto do cadastro usuário
		private EditText Nome;
		private EditText Usuario;
		private EditText Senha;
		private Button cadastrar;

		protected override void OnCreate( Bundle savedInstanceState )
		{
			base.OnCreate( savedInstanceState );

			// Create your application here
			SetContentView( Resource.Layout.Login );

			// Recuperando os objetos da tela
			usuario = (EditText)FindViewById( Resource.Id.etUsuario );
			senha = (EditText)FindViewById( Resource.Id.etSenha );
			acessar = (Button)FindViewById( Resource.Id.btAcessar );
			novoCadastro = (TextView)FindViewById( Resource.Id.tvNovoCadastro );

			// Evendos
			acessar.Click +=this.Acessar_Click;
			novoCadastro.Click +=this.NovoCadastro_Click;

		}

		private void NovoCadastro_Click( object sender, EventArgs e )
		{
			novoCadastroUsuario();
		}

		private void Acessar_Click( object sender, EventArgs e )
		{
			Intent intent = new Intent( this, typeof( cad_ComprasActivity ) );
			StartActivity( intent );
		}

		public void novoCadastroUsuario()
		{
			// Inflando o layout cadastro
			View view = LayoutInflater.Inflate( Resource.Layout.cadUsuario, null );

			Nome = (EditText)view.FindViewById( Resource.Id.edNome );
			Usuario = (EditText)view.FindViewById( Resource.Id.edUsuario );
			Senha = (EditText)view.FindViewById( Resource.Id.edUsuario );
			cadastrar = (Button)view.FindViewById( Resource.Id.btSalvar );

			AlertDialog.Builder alert = new AlertDialog.Builder( this );
			alert.Create();
			alert.SetTitle( "NOVO CADASTRO" );
			alert.SetView( view );
			alert.Show();

			// Evento
			cadastrar.Click +=this.Cadastrar_Click;

		}

		private void Cadastrar_Click( object sender, EventArgs e )
		{
			try
			{
				cadUsuario nvUsuario = new cadUsuario();

				if ( string.IsNullOrEmpty( this.Nome.Text ).Equals( false ) && string.IsNullOrEmpty( this.Usuario.Text ).Equals( false ) && string.IsNullOrEmpty( this.Senha.Text ).Equals( false ) )
				{
					nvUsuario.nome = this.Nome.Text;
					nvUsuario.usuario = this.Usuario.Text;
					nvUsuario.senha = this.Senha.Text;


					using ( SQLiteConnection db = new DataContext().GetDatabase() )
					{
						int check = db.Insert( nvUsuario );

						if ( check > 0 )
						{
							Toast.MakeText( this, "id"+ nvUsuario.id, ToastLength.Short ).Show();
							DismissDialog();
							//Toast.MakeText( this, "Cadastro realizado com sucesso", ToastLength.Short ).Show();
						}
						else
						{
							Toast.MakeText( this, "Cadastro não realizado", ToastLength.Short ).Show();
						}
					}

				}
				else
				{
					Toast.MakeText( this, "Existe Campo Vazios", ToastLength.Short ).Show();
				}

			}
			catch ( System.Exception ex )
			{
				System.Diagnostics.Debug.WriteLine( "ERRO!"+ex.Message );
				//Toast.MakeText( this, "ERRO"+ex.Message, ToastLength.Long ).Show();
			}
		}
	}
}