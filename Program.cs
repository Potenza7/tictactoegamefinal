using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to tic-tac-toe!");

            int menuChoice = -1;
            while (true)
            {
                mainMenu();
                Console.Write("> ");
                menuChoice = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine();

                if (menuChoice == 1)
                {
                    newGame();
                }
                else if(menuChoice == 2)
                {
                    Console.WriteLine("Author: Eray Kılıç\n");
                }
                else if (menuChoice == 3)
                {
                    Console.WriteLine("Are you sure want to quit? [y/n]");
                    Console.Write("> ");

                    string exitChoice = Console.ReadLine();

                    if (exitChoice == "y")
                        break;

                    Console.WriteLine();
                }
            }

            Console.Read();
        }

        static void newGame()
        {
            string[,] board = new string[3, 3];

            bool isXPlayerMove = true;
            bool availableToContinue = true;
            bool isAvailable = true;
            while (availableToContinue)
            {
                if (isAvailable)
                {
                    writeBoard(board);

                    //  reverse behance
                    string playerMark = isXPlayerMove ? "O" : "X";
                    if (isWinner(board, playerMark))
                    {
                        Console.WriteLine(playerMark + " won!");
                        Console.WriteLine("[Press Enter to return to main menu...]");
                        Console.ReadLine();
                        Console.WriteLine("\n");
                        break;
                    }

                    if (isGameOver(board))
                    {
                        Console.WriteLine("Game over!");
                        Console.WriteLine("[Press Enter to return to main menu...]");
                        Console.ReadLine();
                        Console.WriteLine("\n");
                        break;
                    }
                }

                if (isXPlayerMove)
                {
                    Console.Write("X's move > ");
                }
                else
                {
                    Console.Write("O's move > ");
                }


                int move = Convert.ToInt32(Console.ReadLine());

                int[] indices = getIndices(board, move);
                if (indices[0] == -1 || indices[1] == -1)
                {
                    Console.WriteLine("Illegal move! Try again.");
                    continue;
                }

                isAvailable = isAvailableMove(board, indices);
                if (isAvailable)
                {
                    Console.WriteLine();
                    board[indices[0], indices[1]] = isXPlayerMove ? "X" : "O";
                }
                else
                {
                    Console.WriteLine("Illegal move! Try again.");
                    continue;
                }

                isXPlayerMove = !isXPlayerMove;
            }
        }

        static void writeBoard(string[,] board)
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (board[i, j] == "X" || board[i, j] == "O")
                    {
                        Console.Write(" " + board[i, j] + " ");
                    }
                    else
                    {
                        Console.Write("   ");
                    }

                    if (j != board.GetLength(1) - 1)
                    {
                        Console.Write("|");
                    }
                }

                if (i != board.GetLength(0) - 1)
                {
                    Console.WriteLine();
                    for (int j = 0; j < board.GetLength(1); j++)
                    {
                        Console.Write("---");

                        if (j != board.GetLength(1) - 1)
                        {
                            Console.Write("+");
                        }
                    }
                }

                Console.WriteLine();
            }

            Console.WriteLine();
        }

        static int[] getIndices(string[,] board, int move)
        {
            int[] indices = { -1, -1 };
            if (move <= 0 || move > 9)
            {
                return indices;
            }

            int counter = 1;

            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (counter == move)
                    {
                        indices[0] = i;
                        indices[1] = j;

                        return indices;
                    }
                    else
                    {
                        counter++;
                    }
                }
            }

            return indices;
        }

        static bool isAvailableMove(string[,] board, int[] indices)
        {
            return board[indices[0], indices[1]] == null;
        }

        static bool isGameOver(string[,] board)
        {
            bool gameOver = true;

            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (board[i, j] == null)
                    {
                        gameOver = false;
                        break;
                    }
                }
            }

            return gameOver;
        }

        static void mainMenu()
        {
            Console.WriteLine("1. New game");
            Console.WriteLine("2. About the author");
            Console.WriteLine("3. Exit");
        }

        static bool isHorizontalMatchFounded(string[,] board, string playerMark)
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                int matchCounter = 0;

                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (board[i, j] == playerMark)
                    {
                        matchCounter++;
                    }
                }

                if (matchCounter == board.GetLength(1))
                {
                    return true;
                }
            }

            return false;
        }

        static bool isVerticalMatchFounded(string[,] board, string playerMark)
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                int matchCounter = 0;

                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (board[j, i] == playerMark)
                    {
                        matchCounter++;
                    }
                }

                if (matchCounter == board.GetLength(1))
                {
                    return true;
                }
            }

            return false;
        }

        static bool isDiagonalMatchFounded(string[,] board, string playerMark)
        {
            //  Left diagonal check
            int matchCounter = 0;
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (i == j && board[i, j] == playerMark)
                    {
                        matchCounter++;

                        if (matchCounter == 3)
                        {
                            break;
                        }
                    }
                }

                
            }

            if (matchCounter == board.GetLength(1))
            {
                return true;
            }

            //  ************************************

            //  Right diagonal check
            matchCounter = 0;
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (i == (board.GetLength(1) - 1 - j) && board[i, j] == playerMark)
                    {
                        matchCounter++;

                        if (matchCounter == 3)
                        {
                            break;
                        }
                    }
                }
            }

            if (matchCounter == board.GetLength(1))
            {
                return true;
            }

            return false;
        }

        static bool isWinner(string[,] board, string playerMark)
        {
            return isHorizontalMatchFounded(board, playerMark) || isVerticalMatchFounded(board, playerMark) || isDiagonalMatchFounded(board, playerMark);
        }
    }
}