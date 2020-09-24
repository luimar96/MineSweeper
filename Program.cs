using System;

namespace MineSweeper
{
    class Program
    {
        static void Main(string[] args)
        {
          char [,] board =new char[10,10];

             //initialize
          for (int row = 0; row < 10; row++)
          {
              for (int col = 0; col < 10; col++)
              {
                  board[row,col]='X';
              }
              
          }
          
         
          
          Console.WriteLine("  A B C D E F G H I J");
           System.Console.WriteLine("+---------------------");
          for (int row = 0; row < 10; row++)
          {
              
              Console.Write(row+" | ");
              for (int col = 0; col < 10; col++)
              {
                   
                   Console.Write(board[row, col]);
                    Console.Write(" ");

              }
              Console.WriteLine();
          }
        }


    }
}
