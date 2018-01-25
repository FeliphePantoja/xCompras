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

namespace Xcompras.Model
{
	public class cadProduto
	{
		public int codigo { get; set; }
		public string produto { get; set; }
		public decimal valor  { get; set; }
		public int quantidade { get; set; }
		public string local { get; set; }

	}
}