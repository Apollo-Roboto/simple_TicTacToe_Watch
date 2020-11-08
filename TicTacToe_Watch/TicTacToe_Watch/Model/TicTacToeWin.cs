using System;
using System.Collections.Generic;
using System.Text;

using TicTacToe_Watch.ViewModel;

namespace TicTacToe_Watch.Model
{
	public class TicTacToeWin
	{
		public enum WinLine
		{
			NONE,
			TOP_HORIZONTAL,
			MIDDLE_HORIZONTAL,
			BOTTOM_HORIZONTAL,
			LEFT_VERTICAL,
			MIDDLE_VERTICAL,
			RIGHT_VERTICAL,
			BACK_DIAGONAL,
			FRONT_DIAGONAL
		}

		public ViewModel.TicTacToe.GameState gameState;
		public WinLine winLine;
		
	}
}
