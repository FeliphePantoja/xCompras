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
	class dadosProduto
	{
		public int id { get; set; }
		public string produto { get; set; }
		public string valor { get; set; }
		public string localCompra { get; set; }
	}
}