using System;
using System.Threading;
using System.Linq;

namespace TicTacToe
{
	class Program
	{
		// Possible spaces for X or O
		static string[] spaces = new string[9];

		static void Main(string[] args)
		{
			NewGame();
		}



		/// <summary>
		/// Initiates a new game of Tic Tac Toe
		/// </summary>
		static void NewGame()
		{
			spaces = new string[9];

			bool isX = false;
			bool isO = false;

			int count = 0;
			int playerTurn = 0;

			bool isPlayerOne = true;

			Console.Clear();
			Console.WriteLine("Please pick a letter for player 1: (X) or (O)\n");

			string input = Console.ReadLine().ToUpper();

			while (input != "X" && input != "O")
			{
				Console.WriteLine("");
				Console.SetCursorPosition(0, Console.CursorTop - 1);
				ClearCurrentConsoleLine();
				Console.SetCursorPosition(0, Console.CursorTop - 1);
				ClearCurrentConsoleLine();

				input = Console.ReadLine().ToUpper();
			}

			isX = (input == "X");
			isO = (input == "O");

			Thread.Sleep(400);
			Console.WriteLine("\nOkay!");
			Thread.Sleep(600);
			Console.WriteLine("\nLet's begin!");
			Thread.Sleep(800);
			Console.Clear();

			// Calls BuildBoard, passing in user chosen letter + initial status
			PlayGame(isX, isPlayerOne, count, playerTurn);
		}

		// Draws grid, current characters, and possible moves
		static void DrawBoard(string currentPlayer)
		{
			Console.Clear();

			string topOfBoard = $"	{spaces[0]}	|	{spaces[1]}	|	{spaces[2]}";
			string line1 = "______________________________________";
			string midBoard = $"	{spaces[3]}	|	{spaces[4]}	|	{spaces[5]}";
			string line2 = $"______________________________________";
			string bottomOfBoard = $"	{spaces[6]}	|	{spaces[7]}	|	{spaces[8]}";

			Console.WriteLine($"\n1.) Top Left");
			Console.WriteLine($"2.) Top Mid".PadRight(28) + topOfBoard);
			Console.WriteLine($"3.) Top Right".PadRight(30) + line1);
			Console.WriteLine($"4.) Mid Left");
			Console.WriteLine($"5.) Mid Mid".PadRight(28) + midBoard);
			Console.WriteLine($"6.) Mid Right".PadRight(30) + line2);
			Console.WriteLine("7.) Bottom Left");
			Console.WriteLine("8.) Bottom Mid".PadRight(28) + bottomOfBoard);
			Console.WriteLine("9.) Bottom Right");
			Console.WriteLine($"\n{currentPlayer}'s turn\n");
		}

		static bool HasWinner()
		{
			{
				return IsGameOver("X") || IsGameOver("O");
			}
		}

		// Declares win state
		static bool IsGameOver(string character)
		{

			return ((spaces[0] == character && spaces[3] == character && spaces[6] == character) ||
					(spaces[0] == character && spaces[4] == character && spaces[8] == character) ||
					(spaces[2] == character && spaces[4] == character && spaces[6] == character) ||
					(spaces[0] == character && spaces[1] == character && spaces[2] == character) ||
					(spaces[3] == character && spaces[4] == character && spaces[5] == character) ||
					(spaces[6] == character && spaces[7] == character && spaces[8] == character) ||
					(spaces[1] == character && spaces[4] == character && spaces[7] == character) ||
					(spaces[2] == character && spaces[5] == character && spaces[8] == character));
		}

		/// <summary>
		/// Places characters as players make moves
		/// </summary>
		/// <param name="isX">Detects if current player is placing X</param>
		/// <param name="isPlayerOne">Detects current player</param>
		/// <param name="spaces">Collects + displays current board values</param>
		/// <returns></returns>
		static void PlayGame(bool isX, bool isPlayerOne, int filledSpaces, int playerTurn)
		{

			string currentPlayer = (playerTurn % 2 == 0) ? "Player 1" : "Player 2";

			while (!HasWinner() && filledSpaces < 9)
			{
				DrawBoard(currentPlayer);

				string character = (isX) ? "X" : "O";

				string choiceForSpace = GetUserChoiceForSpace();

				// Gets space from player input (-1 to account for 0-based index)
				int index = int.Parse(choiceForSpace) - 1;

				spaces[index] = character;
				filledSpaces++;

				isPlayerOne = !isPlayerOne;
				playerTurn++;

				isX = !isX;
			}

			// Update board with winning/tie move
			DrawBoard(currentPlayer);

			if (IsGameOver("X"))
			{
				Console.WriteLine("\nX WINS!");
				GameOver(playerTurn, true);
			}

			else if (IsGameOver("O"))
			{
				Console.WriteLine("\nO WINS!");
				GameOver(playerTurn, true);
			}

			else if (filledSpaces == 9)
			{
				// draw
				GameOver(playerTurn, false);
			}
		}

		private static string GetUserChoiceForSpace()
		{
			// Console will only accept numbers 1 through 9 as valid input,
			// And will continue to prompt for move until correct input is received
			string[] validChoices = { "1", "2", "3", "4", "5", "6", "7", "8", "9" };

			Console.Write("Choose a space: ");
			string choiceForSpace = Console.ReadLine();
			Console.WriteLine();

			while (!validChoices.Contains(choiceForSpace) || !IsAvailable(choiceForSpace))
			{

				Console.SetCursorPosition(0, Console.CursorTop - 1);
				Console.WriteLine("");
				Console.SetCursorPosition(0, Console.CursorTop - 1);
				ClearCurrentConsoleLine();
				Console.SetCursorPosition(0, Console.CursorTop - 1);
				ClearCurrentConsoleLine();
				Console.WriteLine("Please select an empty space!");

				choiceForSpace = Console.ReadLine();
			}

			return choiceForSpace;
		}

		/// <summary>
		/// Returns if a space is available
		/// </summary>
		/// <param name="choiceForSpace"></param>
		/// <param name="spaces"></param>
		/// <returns></returns>
		private static bool IsAvailable(string choiceForSpace)
		{
			int index = int.Parse(choiceForSpace) - 1;

			return String.IsNullOrEmpty(spaces[index]);
		}

		/// <summary>
		/// Concludes game
		/// </summary>
		/// <param name="playerTurn">Congratulates appropriate player</param>
		/// <param name="winState">Detects if winner exists or game was tied</param>
		static void GameOver(int playerTurn, bool winState)
		{
			string userChoice = null;

			if (winState)
			{
				Console.Beep(800, 200);
				Console.Beep(800, 200);

				Thread.Sleep(800);

				if (playerTurn % 2 == 1)
				{
					Thread.Sleep(800);
					Console.WriteLine("\nCongratulations Player 1!");
				}
				else
				{
					Console.WriteLine("\nCongratulations Player 2!");
				}
			}
			else
			{
				Console.Beep(230, 1000);
				Console.Beep(200, 1000);
				Console.WriteLine("\nTIE! Better luck next time.");
			}

			Thread.Sleep(800);
			Console.WriteLine("Game Over!");
			Thread.Sleep(1000);
			Console.Write("\nPlay Again? (Y) (N)\n\n");

			userChoice = Console.ReadLine().ToUpper();

			while (userChoice != "Y" && userChoice != "N")
			{
				Console.WriteLine();
				Console.SetCursorPosition(0, Console.CursorTop - 1);
				ClearCurrentConsoleLine();
				ClearCurrentConsoleLine();
				Console.SetCursorPosition(0, Console.CursorTop - 1);
				ClearCurrentConsoleLine();
				userChoice = Console.ReadLine().ToUpper();

			}

			// Basic animation for beginning a new game
			if (userChoice == "Y")
			{
				Console.Write("\n");
				Thread.Sleep(100);
				Console.Write("A");
				Thread.Sleep(100);
				Console.Write("l");
				Thread.Sleep(100);
				Console.Write("r");
				Thread.Sleep(100);
				Console.Write("i");
				Thread.Sleep(100);
				Console.Write("g");
				Thread.Sleep(100);
				Console.Write("h");
				Thread.Sleep(100);
				Console.Write("t");
				Thread.Sleep(100);
				Console.Write(",");
				Thread.Sleep(100);
				Console.Write(" ");
				Thread.Sleep(100);
				Console.Write("g");
				Thread.Sleep(100);
				Console.Write("e");
				Thread.Sleep(100);
				Console.Write("t");
				Thread.Sleep(100);
				Console.Write(" ");
				Thread.Sleep(100);
				Console.Write("r");
				Thread.Sleep(100);
				Console.Write("e");
				Thread.Sleep(100);
				Console.Write("a");
				Thread.Sleep(100);
				Console.Write("d");
				Thread.Sleep(100);
				Console.Write("y");
				Thread.Sleep(200);
				Console.Write(".");
				Thread.Sleep(400);
				Console.Write(".");
				Thread.Sleep(600);
				Console.Write(".");
				Thread.Sleep(600);
				Console.Write(".");
				Thread.Sleep(1000);

				NewGame();
			}

			// Exits game if user is sick of playing
			if (userChoice == "N")
			{
				Console.Clear();
				Console.WriteLine("\nGoodbye!\n");
				Environment.Exit(0);
			}

		}

		// Sets cursor to beginning of line and rewrites line
		// This allows input field to be reset without bumping down to new line
		public static void ClearCurrentConsoleLine()
		{
			int currentLineCursor = Console.CursorTop;
			Console.SetCursorPosition(0, Console.CursorTop);
			Console.Write(new string(' ', Console.WindowWidth));
			Console.SetCursorPosition(0, currentLineCursor);
		}
	}
}

