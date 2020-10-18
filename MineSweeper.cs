using System;
using System.Collections.Generic;
using System.Text;

namespace MineSweeper
{
    // Typ för miniröjspelet.
    struct MineSweeper
    {
        private Board board;
        private bool quit;

        //Konstruktor som initierare ett nytt spel med en slumpmässig spelplan.
        public MineSweeper(string[] args)
        {
            board = new Board(args);
            quit = false;
        }

        //Läs ett nytt kommando från användare med giltlig syntax och
        //ett känt kommandotecken.
        static private string ReadCommando()
        {

            while (true)
            {
                System.Console.WriteLine("f = flagga r = reveal q = quit");
                System.Console.Write("> ");
                string input = Console.ReadLine();

                if (input.Length == 1 && Char.IsLetter(input[0]))
                {
                    //giltig syntax
                }
                else if (input.Length == 4 && Char.IsLetter(input[0]) &&
               input[1] == ' ' && (input[2] >= 'a' && input[2] <= 'j') && Char.IsDigit(input[3]))
                {
                    //giltigt syntax
                }
                else
                {
                    System.Console.WriteLine("Syntax error");
                    continue;
                }
                return input;
            }

        }

        private bool ExecuteCommand(string input)
        {
            //om f så ska den flagga
            if (input[0] == 'f')
            {
                if(input.Length == 1)
                {
                    System.Console.WriteLine("syntax error");
                    return false;
                }
                int col = input[2] - 97;// A=0 B=1 C=3 osv
                int row = input[3] - 48;
                return board.TryFlag(row, col);
                                
            }
            // om r så ska den sweepa
            if (input[0] == 'r')
            {               
                if(input.Length == 1)
                {
                    System.Console.WriteLine("syntax error");
                    return false;
                }
                int col = input[2] - 97;// A=0 B=1 C=3 osv
                int row = input[3] - 48;
                return board.TrySweep(row, col);
            }

            // om q så ska den quita
            if (input[0] == 'q')
            {
               return quit = true;
            }
            else if (input[0] != 'f' && input[0] != 'r')
            {
                System.Console.WriteLine("unknow command");
            }
            return true;
        }

        // Kör spelet efter initerun. Metoden retunerar när spelet tar
        //slut genom att något av följande händer:
        //- Spelaren avslutade spelet med kommando 'q'
        //- Spelaren förlorade spelet genom att röja en minerad ruta
        //- Spelare vann spelet genom att alla ej minerade rutor är röjda
        public void Run()
        {
            while (!(quit || board.PlayerWon || board.GameOver))
            {             
                    // först ska den printa ut board
                    //skriver ut board igen efter intput
                    board.Print();

                    while (true)
                    { 
                        string input = ReadCommando();
                        ExecuteCommand(input);
                        break;
                    }
                    if (board.GameOver)
                    {

                        board.Print();
                        System.Console.WriteLine("GAME OVER");
                        Environment.Exit(1);
                    }

                    if (board.PlayerWon)
                    {
                        board.Print();
                        System.Console.WriteLine("WELL DONE");
                        Environment.Exit(0);
                    }

                
            }
        }
    }
}