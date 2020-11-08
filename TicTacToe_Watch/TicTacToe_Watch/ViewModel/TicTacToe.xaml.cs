using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe_Watch.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TicTacToe_Watch.ViewModel
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TicTacToe : ContentView
	{
		public enum Turn
		{
			X, O
		}

		public enum GameState
		{
			ONGOING,
			X_WIN,
			O_WIN,
			TIE,
		}

		public event EventHandler OnEnd;

		public Cell[,] Cells;
		public Turn CurrentTurn { get; set; } = Turn.X;
		public GameState CurrentGame { get; set; }

		public TicTacToe()
		{
			InitializeComponent();

			Cells = new Cell[3, 3]{
				{cell_00, cell_10, cell_20 },
				{cell_01, cell_11, cell_21 },
				{cell_02, cell_12, cell_22 }
			};
		}

		public void SwitchTurn()
		{
			if (CurrentTurn == Turn.X)
				CurrentTurn = Turn.O;
			else
				CurrentTurn = Turn.X;
		}

		public TicTacToeWin CheckWin()
		{
			Cell.CellState[] checking = { Cell.CellState.O, Cell.CellState.X };

			foreach (var turn in checking)
			{
				GameState win = GameState.ONGOING;

				if (turn == Cell.CellState.X)
					win = GameState.X_WIN;
				else if (turn == Cell.CellState.O)
					win = GameState.O_WIN;

				// check vertical
				if (Cells[0, 0].State == turn && Cells[1, 0].State == turn && Cells[2, 0].State == turn)
					return new TicTacToeWin { gameState = win, winLine = TicTacToeWin.WinLine.LEFT_VERTICAL };
				if (Cells[0, 1].State == turn && Cells[1, 1].State == turn && Cells[2, 1].State == turn)
					return new TicTacToeWin { gameState = win, winLine = TicTacToeWin.WinLine.MIDDLE_VERTICAL };
				if (Cells[0, 2].State == turn && Cells[1, 2].State == turn && Cells[2, 2].State == turn)
					return new TicTacToeWin { gameState = win, winLine = TicTacToeWin.WinLine.RIGHT_VERTICAL };

				// check horizontal
				if (Cells[0, 0].State == turn && Cells[0, 1].State == turn && Cells[0, 2].State == turn)
					return new TicTacToeWin { gameState = win, winLine = TicTacToeWin.WinLine.TOP_HORIZONTAL };
				if (Cells[1, 0].State == turn && Cells[1, 1].State == turn && Cells[1, 2].State == turn)
					return new TicTacToeWin { gameState = win, winLine = TicTacToeWin.WinLine.MIDDLE_HORIZONTAL };
				if (Cells[2, 0].State == turn && Cells[2, 1].State == turn && Cells[2, 2].State == turn)
					return new TicTacToeWin { gameState = win, winLine = TicTacToeWin.WinLine.BOTTOM_HORIZONTAL };

				// check front diagonal
				if (Cells[0, 0].State == turn && Cells[1, 1].State == turn && Cells[2, 2].State == turn)
					return new TicTacToeWin { gameState = win, winLine = TicTacToeWin.WinLine.FRONT_DIAGONAL };

				// check back diagonal
				if (Cells[2, 0].State == turn && Cells[1, 1].State == turn && Cells[0, 2].State == turn)
					return new TicTacToeWin { gameState = win, winLine = TicTacToeWin.WinLine.BACK_DIAGONAL };
			}

			if (isFull())
				return new TicTacToeWin { gameState = GameState.TIE, winLine = TicTacToeWin.WinLine.NONE };
			else
				return new TicTacToeWin { gameState = GameState.ONGOING, winLine = TicTacToeWin.WinLine.NONE };
		}

		public bool isFull()
		{
			for (int i = 0; i < 3; i++)
			{
				for (int j = 0; j < 3; j++)
				{
					if (Cells[i, j].State == Cell.CellState.EMPTY)
					{
						return false;
					}
				}
			}
			return true;
		}

		public void Reset()
		{
			foreach (Cell cell in Cells)
			{
				cell.State = Cell.CellState.EMPTY;
			}

			CurrentGame = GameState.ONGOING;
			CurrentTurn = Turn.X;

			th_line.IsVisible = false;
			mh_line.IsVisible = false;
			bh_line.IsVisible = false;
			lv_line.IsVisible = false;
			mv_line.IsVisible = false;
			rv_line.IsVisible = false;
			backDiag_line.IsVisible = false;
			frontDiag_line.IsVisible = false;

		}

		private void Cell_Tapped(object sender, EventArgs e)
		{
			//do nothing if the game is done
			if (CurrentGame != GameState.ONGOING)
			{
				return;
			}

			Cell cell = sender as Cell;

			if (cell.State != Cell.CellState.EMPTY)
			{
				return;
			}

			if (CurrentTurn == Turn.X)
				cell.State = Cell.CellState.X;
			else
				cell.State = Cell.CellState.O;

			TicTacToeWin state = CheckWin();

			CurrentGame = state.gameState;

			switch (state.winLine)
			{
				case TicTacToeWin.WinLine.NONE:
					break;
				case TicTacToeWin.WinLine.TOP_HORIZONTAL:
					th_line.IsVisible = true;
					break;
				case TicTacToeWin.WinLine.MIDDLE_HORIZONTAL:
					mh_line.IsVisible = true;
					break;
				case TicTacToeWin.WinLine.BOTTOM_HORIZONTAL:
					bh_line.IsVisible = true;
					break;
				case TicTacToeWin.WinLine.LEFT_VERTICAL:
					lv_line.IsVisible = true;
					break;
				case TicTacToeWin.WinLine.MIDDLE_VERTICAL:
					mv_line.IsVisible = true;
					break;
				case TicTacToeWin.WinLine.RIGHT_VERTICAL:
					rv_line.IsVisible = true;
					break;
				case TicTacToeWin.WinLine.BACK_DIAGONAL:
					backDiag_line.IsVisible = true;
					break;
				case TicTacToeWin.WinLine.FRONT_DIAGONAL:
					frontDiag_line.IsVisible = true;
					break;
			}

			switch (CurrentGame)
			{
				case GameState.ONGOING:
					break;
				case GameState.TIE:
					OnEnd?.Invoke(this, new EventArgs());
					break;
				case GameState.O_WIN:
					OnEnd?.Invoke(this, new EventArgs());
					break;
				case GameState.X_WIN:
					OnEnd?.Invoke(this, new EventArgs());
					break;
			}

			SwitchTurn();
		}
	}
}