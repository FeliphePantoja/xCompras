using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;
using Xcompras.Model;

namespace Xcompras.Controller
{
	public class DataContext
	{
		public string path { get; set; }

		public DataContext()
		{
			string nomeBanco = "appBanco.db";
			string documentsPath = System.Environment.GetFolderPath( System.Environment.SpecialFolder.Personal );
			path = Path.Combine( documentsPath, nomeBanco );

			// Criando o banco de dados
			createDataBase();
		}

		public void createDataBase()
		{
			try
			{
				using (SQLiteConnection db = new SQLiteConnection(path))
				{
					db.CreateTable<cadUsuario>();
					db.CreateTable<cadProduto>();
				}
			}
			catch ( System.Exception e)
			{
				System.Diagnostics.Debug.WriteLine( "ERRRO!!! " + e.Message );
			}
		}

		public SQLiteConnection GetDataBase()
		{
			return new SQLiteConnection(path);
		}

		internal SQLiteConnection GetDatabase()
		{
			return new SQLiteConnection( path );
		}
	}
}