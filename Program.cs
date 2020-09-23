using System;

namespace MineSweeper
{
    class Program
    {
        static void Main(string[] args)
        {
          char [,] board =new char[10,10];
          System.Console.WriteLine("    A   B   C   D   E   F   G   H   I   J");
           System.Console.WriteLine("+------------------------------------------");
          for (int row = 0; row < 10; row++)
          {
              System.Console.Write(row+ " | ");
              for (int col = 0; col < 10; col++)
              {
                   System.Console.Write(board[row, col]);
                    System.Console.Write(" | ");
              }
              System.Console.WriteLine();
          }
        }
    }
}
