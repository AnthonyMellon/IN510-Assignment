using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

/* ---Instructions---
 *
 *A menu system that:
 *  -Loads a specefic text file and lists all the contestants on the screen sorted by last name. 43 people are in the file
 *  -Locate a player in the list and change their interest field, save this back to the text file
 *  -Generate 10 finalists, no duplicates are allowed
 *  -Randomly choose one of these finalists to be the player
 *  -Finish the game
 *  
 *The minimum requirements are:
 *  -Three questions
 *  -100 people have been surveyed and given their answers to the survey questions.
 *   The player has to try and guess the top three answers, each answer is worth the number
 *   of responses it got from the survey. The player is only allowed to give three answers.
 *   After each question show the correct answers, incorrect answers score zero.
 *  
 *External documentation required:
 *  -An activity diagram of one of your methods (this method must contain at least two structures)
 *  -An activity diagram of the method that contains the menu system
 *  -A list of extra features and any bugs
 *  -A printout of the code
 *
 * The program must use methods and an array of structs, it must contain a menu system and the program
 * must include internal documentation (comments)
 * 
 */

namespace Family_Fued
{
    class Program
    {
    
        public struct Contestant
        {
            public string fName;
            public string lName;
            public string interest;
        }

        public static Contestant[] contestants = new Contestant[43];

        //vvv Methods that relate to the game vvv

        static void Menu()
        {
            Console.Clear();

            ConsoleKeyInfo keyPressed;
            bool menuLoop = true;
            int arrowPos = 0;
            string[] menuOptions = { "Play", "Settings", "Quit" };

            while (menuLoop == true)
            {
                Console.WriteLine("Welcome to family fued!");

                for (int i = 0; i < menuOptions.Length; i++) //Write each menu option with the arrow pointer
                {
                    if (i == arrowPos)
                    {
                        Console.Write("> ");
                    }
                    else
                    {
                        Console.Write("  ");
                    }
                    Console.WriteLine(menuOptions[i]);
                }

                keyPressed = Console.ReadKey(); //Getting up or down arrow inputs to move the arrow pointer
                Console.Clear();
                if (keyPressed.Key == ConsoleKey.UpArrow)
                {
                    arrowPos--;
                }
                else if (keyPressed.Key == ConsoleKey.DownArrow)
                {
                    arrowPos++;
                }
                else if (keyPressed.Key == ConsoleKey.Enter)
                {
                    menuLoop = false;
                }
                arrowPos = NumLoop(arrowPos, 0, menuOptions.Length - 1);
            }

            if (arrowPos == 0)
            {
                Game();
            }
            else if (arrowPos == 1)
            {
                Settings();
            }
        }

        static void Settings()
        {
            Console.Clear();

            ConsoleKeyInfo keyPressed;
            int arrowPos = 0;
            bool settingsLoop = true;
            string[] settingsOptions = { "Preferences", "List Contestants", "Player Stats", "Back" };

            while (settingsLoop == true)
            {
                for (int i = 0; i < settingsOptions.Length; i++)
                {
                    if (arrowPos == i)
                    {
                        Console.Write("> ");
                    }
                    else
                    {
                        Console.Write("  ");
                    }
                    Console.WriteLine(settingsOptions[i]);
                }

                keyPressed = Console.ReadKey(); //Getting up or down arrow inputs to move the arrow pointer
                Console.Clear();
                if (keyPressed.Key == ConsoleKey.UpArrow)
                {
                    arrowPos--;
                }
                else if (keyPressed.Key == ConsoleKey.DownArrow)
                {
                    arrowPos++;
                }
                else if (keyPressed.Key == ConsoleKey.Enter)
                {
                    settingsLoop = false;
                }
                arrowPos = NumLoop(arrowPos, 2, settingsOptions.Length - 1);
            } 
            
            if (arrowPos == 0)
            {
                Prefernces();
            }
            else if (arrowPos == 1)
            {
                ListContestants();
            }
            else if (arrowPos == 2)
            {
                PlayerStats();
            }
            else if (arrowPos == 3)
            {
                Menu();
            }            
        }

        static void Prefernces()
        {
            Console.WriteLine("Preferences");
            Console.ReadLine();
            Settings();
        }

        static void ListContestants()
        {
            
            StreamReader reader = new StreamReader(@"familyFeud.txt");
            for (int i = 0; i < contestants.Length; i++)
            {
                contestants[i].fName = reader.ReadLine();
                contestants[i].lName = reader.ReadLine();
                contestants[i].interest = reader.ReadLine();
            }
            reader.Close();
            Console.ReadLine();
            Settings();
        }

        static void PlayerStats()
        {
            System.Diagnostics.Process.Start(@"familyFeud.txt");
            Console.WriteLine("Player stats");
            Console.ReadLine();
            Settings();
        }

        static void Game()
        {
            Console.WriteLine("Game");
            Console.ReadLine();
            Menu();
        }

        //^^^ Methods that relate to the game ^^^

        //vvv Other methods vvv

        static int NumLoop(int input, int lower, int upper)
        {            
            if (input < lower)
            {
                input = upper;
            }
            else if (input > upper)
            {
                input = lower;
            }

            return input;
        }

        //^^^ Other methods ^^^

        static void Main()
        {        
            Menu();
        }
    }
}
