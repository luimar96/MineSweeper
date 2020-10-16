using System;
using System.Collections.Generic;
using System.Text;

namespace MineSweeper
{
    // Typ för spelplanen i minröj
    struct Board
    {
        private bool isGameover, isPlayerWon;
        private Square[,] board;
        private int flagCount, sweepedCount, maxFlagCount;

        //Konstruktor som initaliserar en ny spelplan.
        public Board(string[] args)
        {
            isGameover = false;
            isPlayerWon = false;
            Helper.Initialize(args);
            board = new Square[10, 10]; //brädans plan
            //initilazera alla rutor på spel planen
            for (int row = 0; row < 10; row++)
            {
                for (int col = 0; col < 10; col++)
                { //lägger till random med bomb i planen
                    board[row, col] = new Square(Helper.BoobyTrapped(row, col));
                    if (Helper.BoobyTrapped(row, col))
                    {
                        System.Console.WriteLine($" mine: {row}, {col}");
                    }

                }

            }
            //måste veta att bomb är där för att lägga till som increment
            //ska skicka tillbaka att det är bomb i närheten
            for (int row = 0; row < 10; row++)
            {
                for (int col = 0; col < 10; col++)
                {
                    if (Helper.BoobyTrapped(row, col + 1))
                    {
                        board[row, col].IncrementCloseMineCount();
                    }
                    if (Helper.BoobyTrapped(row, col - 1))
                    {
                        board[row, col].IncrementCloseMineCount();
                    }
                    if (Helper.BoobyTrapped(row - 1, col))
                    {
                        board[row, col].IncrementCloseMineCount();
                    }
                    if (Helper.BoobyTrapped(row - 1, col + 1))
                    {
                        board[row, col].IncrementCloseMineCount();
                    }
                    if (Helper.BoobyTrapped(row - 1, col - 1))
                    {
                        board[row, col].IncrementCloseMineCount();
                    }
                    if (Helper.BoobyTrapped(row + 1, col + 1))
                    {
                        board[row, col].IncrementCloseMineCount();
                    }
                    if (Helper.BoobyTrapped(row + 1, col))
                    {
                        board[row, col].IncrementCloseMineCount();
                    }
                    if (Helper.BoobyTrapped(row + 1, col - 1))
                    {
                        board[row, col].IncrementCloseMineCount();
                    }




                }

            }

            flagCount = 0;

            sweepedCount = 0;
            maxFlagCount = 25;

        }

        //Egenskap som säger om spelaren har vunnit spelet.
        public bool PlayerWon => isPlayerWon; 

        //Egenskap som säger om spelaren förlora.
        public bool GameOver
        {
            get => isGameover;
            private set
            {
                isGameover = value;
                if (isGameover)
                {
                    for (int r = 0; r < 10; r++)
                    {
                        for (int c = 0; c < 10; c++)
                        {
                            board[r, c].GameOver = true;
                        }

                    }

                }
            }
        }





        //Försök röja en ruta. Retunerar false om ogiltigt drag. Annars true
        public bool TrySweep(int row, int col)
        {
            if (board[row, col].TrySweep())
            {
                if (board[row, col].BoobyTrapped)
                {
                    GameOver = true;
                }


                else if (board[row, col].Symbol == (char)GameSymbol.SweepedZeroCloseMine)
                {
                    for (int r = row - 1; r <= row + 1; r++)
                    {
                        for (int c = col - 1; c <= col + 1; c++)
                        {
                            if (r != -1 && r != 10 && c != -1 && c != 10)
                                TrySweep(r, c);
                        }

                    }

                }
                sweepedCount++;
                if (sweepedCount == 90)
                {
                    isPlayerWon = true;
                }
                return true;
            }

            return false;
        }
        // Försok flagga en ruta. Returnerar false om ogiltigt drag, annars true
        public bool TryFlag(int row, int col)
        {
            if (board[row, col].TryFlag())
            {
                flagCount++;
                return true;
            }

            if (flagCount > maxFlagCount)
            {
                System.Console.WriteLine("not allowed!");
                return true;
            }
            return false;


        }
        // Skriv ut spelplan till konsolen
        public void Print()
        {
            Console.WriteLine("    A B C D E F G H I J");
            Console.WriteLine("  +--------------------");
            for (int row = 0; row < 10; row++)
            {
                Console.Write(row + " | ");
                for (int col = 0; col < 10; col++)
                {
                    Console.Write(board[row, col].Symbol + " ");
                }
                Console.WriteLine();
            }


        }



    }
}