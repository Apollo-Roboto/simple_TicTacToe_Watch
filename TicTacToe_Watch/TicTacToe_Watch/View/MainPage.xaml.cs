using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using TicTacToe_Watch.ViewModel;

namespace TicTacToe_Watch
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
		}

		private void TicTacToe_OnEnd(object sender, EventArgs e)
		{
			Device.BeginInvokeOnMainThread(async () =>
			{
				await Task.Delay(2000);
				(sender as TicTacToe).Reset();
			});
		}
	}
}