using System;
using System.Collections.Generic;
using System.Text;

namespace MineSweeper
{
    enum GameSymbol
    {
        Flagged = 'F',
        NotSweeped = 'X',
        SweepedZeroCloseMine = '.'
    }

    enum GameOverSymbol
    {
        ExplodedMine = 'M',
        FlaggesMine = 'ɯ',
        Mine = 'm',
        MisplacedFlag = 'Ⅎ'


    }
    //Typ för en ruta på spelplan.
    struct Square
    {

        private int closeMineCount;
        private bool flagged, boobyTrapped, sweeped;
        private char symbol;

        //Konstruktor som initiera en ny ruta på spelplan. detta är tar värde från hela början
        public Square(bool isBoobyTrapped)
        { // finna inga miner börjar med 0
            closeMineCount = 0;
            //ny rutan är inte flaggad
            flagged = false;
            // styr om rutan är minerad eller inte, på vilket värde vi skickar som argumment
            boobyTrapped = isBoobyTrapped;
            //ny ruta är inte röjd än
            sweeped = false;


            symbol = (char)GameSymbol.NotSweeped;

        }
        private int CloseMineCount
        {
            get { return closeMineCount; }
            set { closeMineCount = value; }
        }

        //Enbart läsbar Egenskap som säger om rutan är flaggad för tillfället
        public bool IsFlaggad => flagged;//returnerar värdet av flagged


        //Enabrt läsbar Egenskap som säger om rutan är minerad.
        public bool BoobyTrapped => boobyTrapped;  //"minerad"//

        //Enbart läsbar Egenskap som säger om rutan har blivit röjd
        // den som skapat ruta ska inte sätta värdet i sweeped. bara genom att röja rutan
        public bool IsSweeped => sweeped;

        //Egenska som är symbolen som represterar rutan just nu om spelaren skall ritas ut
        //om spelaplanen skall ritas ut
        public char Symbol => symbol;

        //Enabrt skrivbar egenskap som tilldelas true för alla rutor om spelare
        //röjer en minerad ruta
        //tilldelas till true när spelet är slut
        public bool GameOver
        { // value till de kommer vara sant eller falskt
            set
            {
                if (value)
                {
                    if (flagged && boobyTrapped)
                    {
                        symbol = (char)GameOverSymbol.FlaggesMine;
                    }
                    if (boobyTrapped && !sweeped)
                    {
                        symbol = (char)GameOverSymbol.Mine;
                    }
                    if (sweeped && boobyTrapped)
                    {
                        symbol = (char)GameOverSymbol.ExplodedMine;
                    }
                    if (flagged && !boobyTrapped)
                    {
                        symbol = (char)GameOverSymbol.MisplacedFlag;
                    }
                }
            }

        }

        //Öka räknare av antalet minor på intilliggande rutor med 1 
        public void IncrementCloseMineCount()
        {
            CloseMineCount += 1;
        }

        //Försök att flagga rutan. Returnerar false om ogiltligt drag, annars true. 
        public bool TryFlag()
        {
            //om sweeped är true, då är den redan röjd. då misslyckar operationen och retuneran flase
            if (sweeped)
            {
                Console.WriteLine("not allowed");
                return false;
            }
            else
            { //om det redan är flaggad så körs detta 
                if (IsFlaggad)
                {
                    //tarbort flaggan på redan flagad position.
                    symbol = (char)GameSymbol.NotSweeped;
                }
                //annars om den inte är flaggad så körs detta
                else
                {
                    symbol = (char)GameSymbol.Flagged;
                }
                flagged = !flagged;
                return true;

            }

        }

        //Försök röja rutan. Returnerar false om ogiltligt drag
        public bool TrySweep()
        {
           
            if (!sweeped && !flagged)// den ska inte vara röjd och flaggad
            {
                sweeped = true; //från false till true
                if (closeMineCount == 0)
                {
                    symbol = (char)GameSymbol.SweepedZeroCloseMine;
                }
                if (BoobyTrapped)
                {
                    GameOver = true;
                }
                else if (closeMineCount > 0)
                {//annars skriver den antal minor
                    symbol = char.Parse(closeMineCount.ToString()); //
                }
                return true;
            }
            else if( IsSweeped )
            {
                System.Console.WriteLine("not allowed");
            }
            return false;

        }





    }
}