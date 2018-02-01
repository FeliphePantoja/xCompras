using System;
using System.Linq;

using Android.App;
using Android.Content;
using Android.OS;
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
		private AlertDialog dialog;
		private LinearLayout LayoutLogin;

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
			LayoutLogin = (LinearLayout)FindViewById( Resource.Id.layoutLogin );


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

			if ( usuario.Text.ToString().Equals( "" ) && senha.Text.ToString().Equals( "" ) )
			{
				Toast.MakeText( this, "Existe Campos Vazios", ToastLength.Short ).Show();
			}
			else
			{
				consultaLogin( usuario.Text.ToString(), senha.Text.ToString() );
			}
		}

		public void novoCadastroUsuario()
		{
			// Inflando o layout cadastro
			View view = LayoutInflater.Inflate( Resource.Layout.cadUsuario, null );

			Nome = (EditText)view.FindViewById( Resource.Id.edNome );
			Usuario = (EditText)view.FindViewById( Resource.Id.edUsuario );
			Senha = (EditText)view.FindViewById( Resource.Id.edSenha );
			cadastrar = (Button)view.FindViewById( Resource.Id.btSalvar );

			// variavel criada global
			AlertDialog.Builder alert = new AlertDialog.Builder( this );
			alert.SetTitle( "NOVO CADASTRO" );
			alert.SetView( view );
			dialog = alert.Create();
			dialog.Show();

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


					using ( SQLiteConnection db = new DadosContext().GetDatabase() )
					{
						db.Insert( nvUsuario );
						//Toast.MakeText( this, "id"+ nvUsuario.id, ToastLength.Short ).Show();
						Toast.MakeText( this, "Cadastro realizado com sucesso", ToastLength.Short ).Show();

						dialog.Dismiss();
					}
				}
				else
				{
					Toast.MakeText( this, "Existe Campo Vazios", ToastLength.Short ).Show();
				}

			}
			catch ( System.Exception ex )
			{
				Toast.MakeText( this, "Cadastro não realizado", ToastLength.Short ).Show();
				System.Diagnostics.Debug.WriteLine( "ERRO!"+ex.Message );
			}
		}

		private void consultaLogin( string nome, string senha )
		{

			using ( SQLiteConnection Bd = new DadosContext().GetDatabase() )
			{
				cadUsuario query = ( from usuario in Bd.Table<cadUsuario>() where usuario.nome == nome && usuario.senha ==senha select usuario ).SingleOrDefault();//.FirstOrDefault();

				//Toast.MakeText( this, "Usuario"+query, ToastLength.Short ).Show();

				if ( query == null )
				{
					Toast.MakeText( this, "Usuário não Cadastrado", ToastLength.Short ).Show();
				}
				else
				{
					salvarDados salvar = new salvarDados();

					Intent intent = new Intent( this, typeof( cad_ComprasActivity ) );
					StartActivity( intent );

					Finish();
				}

			}
		}
	}
}