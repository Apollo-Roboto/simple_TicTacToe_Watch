using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TicTacToe_Watch.ViewModel
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Cell : ContentView
	{
		public enum CellState
		{
			EMPTY,
			X,
			O
		}

		//private double strokeThickness;
		//public double StrokeThickness
		//{
		//	get => strokeThickness;
		//	set
		//	{
		//		strokeThickness = value;
		//		OnPropertyChanged();
		//	}
		//}
		//private Brush stroke;
		//public Brush Stroke
		//{
		//	get => stroke;
		//	set
		//	{
		//		stroke = value;
		//		OnPropertyChanged();
		//	}
		//}

		private CellState state { get; set; }
		public CellState State
		{
			get => state;
			set { state = value; UpdateCell(); }
		}

		public event EventHandler Tapped;

		public Cell()
		{
			InitializeComponent();
			BindingContext = this;
		}

		protected void UpdateCell()
		{
			switch (State)
			{
				case CellState.EMPTY:
					DisplayNone();
					break;
				case CellState.O:
					DisplayO();
					break;
				case CellState.X:
					DisplayX();
					break;
			}
		}

		private void DisplayX()
		{
			x_img.IsVisible = true;
			o_img.IsVisible = false;
		}

		private void DisplayO()
		{
			x_img.IsVisible = false;
			o_img.IsVisible = true;
		}

		private void DisplayNone()
		{
			x_img.IsVisible = false;
			o_img.IsVisible = false;
		}

		private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
		{
			Tapped?.Invoke(this, e);
		}
	}
}