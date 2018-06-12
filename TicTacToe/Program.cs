using System;
using System.Threading;
using System.Linq;

namespace TicTacToe
{
	class Program
	{
		static void Main(string[] args)
		{
			NewGame();
		}

		/// <summary>
		/// Initiates a new game of Tic Tac Toe
		/// </summary>
		static void NewGame()
		{
			bool isX = false;
			bool isO = false;

			int count = 0;
			int playerTurn = 0;

			bool isPlayerOne = true;

			string[] spaces = new string[9];

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

			if (input == "X")
			{
				isX = true;
			}

			else if (input == "O")
			{
				isO = true;
			}

			Thread.Sleep(400);
			Console.WriteLine("\nOkay!");
			Thread.Sleep(600);
			Console.WriteLine("\nLet's begin!");
			Thread.Sleep(600);
			Console.Clear();

			// Calls BuildBoard, passing in user chosen letter + initial status
			// (player one = true, nine empty spaces, 0 current moves made)
			BuildBoard(isX, isO, isPlayerOne, spaces, count, playerTurn);
		}

		/// <summary>
		/// Builds the board as players make moves
		/// </summary>
		/// <param name="isX">Detects if current player is placing X</param>
		/// <param name="isO">Detects if current player is placing O</param>
		/// <param name="isPlayerOne">Detects current player</param>
		/// <param name="spaces">Collects + displays current board values</param>
		/// <returns></returns>
		static void BuildBoard(bool isX, bool isO, bool isPlayerOne, string[] spaces, int filledSpaces, int playerTurn)
		{
			// Models grid for drawing board
			string topOfBoard = $"	{spaces[0]}	|	{spaces[1]}	|	{spaces[2]}";
			string line1 = "______________________________________";
			string midBoard = $"	{spaces[3]}	|	{spaces[4]}	|	{spaces[5]}";
			string line2 = $"______________________________________";
			string bottomOfBoard = $"	{spaces[6]}	|	{spaces[7]}	|	{spaces[8]}";

			string currentPlayer = null;

			// If odd turn, player 1
			// If even turn, player 2
			// This isn't reflected accurately by code since first move is considered turn 0;
			// But it makes sense in practice

			if (playerTurn % 2 == 0)
			{
				currentPlayer = "Player 1";
			}
			if (playerTurn % 2 == 1)
			{
				currentPlayer = "Player 2";
			}

			// Calls console to write board
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

			// WIN CONDITIONS -- easier way to write this??
			for (int i = 0; i < spaces.Length - 6; i++)
			{
				if (spaces[i] == "X" &&
					spaces[i + 3] == "X" &&
					spaces[i + 6] == "X")
				{
					Console.WriteLine("\nX WINS!");
					GameOver(playerTurn, true);
				}

				if (spaces[i] == "O" &&
					spaces[i + 3] == "O" &&
					spaces[i + 6] == "O")
				{
					Console.WriteLine("\nO WINS!");
					GameOver(playerTurn, true);
				}
			}

			for (int i = 0; i < spaces.Length - 8; i++)
			{
				if (spaces[i] == "X" &&
					spaces[i + 4] == "X" &&
					spaces[i + 8] == "X")
				{
					Console.WriteLine("\nX WINS!");
					GameOver(playerTurn, true);
				}
				if (spaces[i] == "O" &&
					spaces[i + 4] == "O" &&
					spaces[i + 8] == "O")
				{
					Console.WriteLine("\nO WINS!");
					GameOver(playerTurn, true);
				}
			}

			if (spaces[2] == "X" &&
				spaces[4] == "X" &&
				spaces[6] == "X" ||

				spaces[0] == "X" &&
				spaces[1] == "X" &&
				spaces[2] == "X" ||

				spaces[3] == "X" &&
				spaces[4] == "X" &&
				spaces[5] == "X" ||

				spaces[6] == "X" &&
				spaces[7] == "X" &&
				spaces[8] == "X")
			{
				Console.WriteLine("\nX WINS!");
				GameOver(playerTurn, true);
			}

			if (spaces[2] == "O" &&
				spaces[4] == "O" &&
				spaces[6] == "O" ||

				spaces[0] == "O" &&
				spaces[1] == "O" &&
				spaces[2] == "O" ||

				spaces[3] == "O" &&
				spaces[4] == "O" &&
				spaces[5] == "O" ||

				spaces[6] == "O" &&
				spaces[7] == "O" &&
				spaces[8] == "O")
			{
				Console.WriteLine("\nO WINS!");
				GameOver(playerTurn, true);
			}
			// END WIN CONDITIONS

			// If board is filled, and no winning pattern exists, game ends with no win state
			if (filledSpaces == 9)
			{
				GameOver(playerTurn, false);
			}
			else
				// If game is not over, user is prompted to choose a space
				Console.Write("Choose a space: ");

			// Current move is taken from user input
			string choiceForSpace = Console.ReadLine();

			// Console will only accept numbers 1 through 9 as valid input,
			// And will continue to prompt for move until correct input is received
			while (choiceForSpace != "1" &&
				choiceForSpace != "2" &&
				choiceForSpace != "3" &&
				choiceForSpace != "4" &&
				choiceForSpace != "5" &&
				choiceForSpace != "6" &&
				choiceForSpace != "7" &&
				choiceForSpace != "8" &&
				choiceForSpace != "9")
			{
				Console.SetCursorPosition(0, Console.CursorTop - 1);
				ClearCurrentConsoleLine();
				Console.Write("Choose a space: ");
				choiceForSpace = Console.ReadLine();
			}

			// If player is X, and space chosen is not already X or O, space chosen becomes X
			if (isX == true &&
				spaces[int.Parse(choiceForSpace) - 1] != "O" &&
				spaces[int.Parse(choiceForSpace) - 1] != "X")
			{
				filledSpaces++;
				spaces[int.Parse(choiceForSpace) - 1] = "X";
				Console.Clear();

				// Switch players
				isPlayerOne = !isPlayerOne;
				playerTurn++;

				// Switch X for O on player switch
				if (isPlayerOne = !isPlayerOne)
				{
					isX = !isX;
					isO = !isO;
				}

				// Rebuild board with updated state
				BuildBoard(isX, isO, isPlayerOne, spaces, filledSpaces, playerTurn);
			}

			// If player is O, and space chosen is not already X or O, space chosen becomes O
			else if (isO == true &&
				spaces[int.Parse(choiceForSpace) - 1] != "O" &&
				spaces[int.Parse(choiceForSpace) - 1] != "X")
			{
				filledSpaces++;
				spaces[int.Parse(choiceForSpace) - 1] = "O";
				Console.Clear();

				// Switch players
				isPlayerOne = !isPlayerOne;
				playerTurn++;

				// Switch X for O on player switch
				if (isPlayerOne = !isPlayerOne)
				{
					isX = !isX;
					isO = !isO;
				}

				//Rebuild board with updated state
				BuildBoard(isX, isO, isPlayerOne, spaces, filledSpaces, playerTurn);
			}

			// If above conditions are not met, user is prompted to choose a valid space
			else
			{
				Console.WriteLine("Please select an empty space!");
				Thread.Sleep(800);
				Console.Clear();
				BuildBoard(isX, isO, isPlayerOne, spaces, filledSpaces, playerTurn);
			}

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

