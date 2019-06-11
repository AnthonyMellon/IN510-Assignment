using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
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
    
        //vvv Structures vvv 

        public struct Contestant
        {
            public string fName;
            public string lName;
            public string interest;
        }

        public struct Answer
        {
            public string answer;
            public int points;
        }
        public struct Question
        {
            public string question;
            public Answer answer1;
            public Answer answer2;
            public Answer answer3;
            public Answer answer4;
            public Answer answer5;
            public Answer answer6;            
        }

        //^^^ Structures

        // vvv Other Class-wide Variables vvv

        public static Contestant[] contestants = new Contestant[43];

        public static string contestantsList = "familyFeud.txt";

        // ^^^ Other Class-wide Variables ^^^

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

        } //Menu End

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
                arrowPos = NumLoop(arrowPos, 0, settingsOptions.Length - 1);
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
            
        } //Settings End

        static void Prefernces()
        {
            Console.WriteLine("Preferences");
            Console.ReadLine();
            Settings();

        } //Preferences End

        static void ListContestants()
        {
            int columns = 5, count = 0, selection = 0;   
            //StreamReader reader = new StreamReader(contestantsList);
            bool loop = true;
            ConsoleKeyInfo keyPressed;            

            UpdateContestants(contestantsList);
            SortContestants();
            do

            {
                Console.Clear();
                Console.WriteLine("CONTESTANT LIST: \n");

                count = 0;
                while (count < contestants.Length) //Writing Each Contestant
                {
                    
                    for (int i = 0; i < columns; i++) //Writing the contestants names on one line
                    {
                        if (count + i == selection)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                        }
                        if (count + i < contestants.Length)
                        {
                            Console.Write((contestants[i + count].fName + " " + contestants[i + count].lName).PadRight(30));
                        }
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }                     
                    Console.WriteLine();
                    
                    for (int i = 0; i < columns; i++) //Writing the contestants interests on the next line
                    {
                        if (count + i == selection)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                        }
                        if (count + i < contestants.Length)
                        {
                            Console.Write(contestants[i + count].interest.PadRight(30));
                        }
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }

                    count += columns;
                    Console.WriteLine("\n");
                }

                Console.WriteLine("\nUse arrow keys to navigate | Press 'enter' to edit current selection | Press 'esc' to return");

                //Navigation and Controls
                keyPressed = Console.ReadKey();
                if (keyPressed.Key == ConsoleKey.Escape)
                {                   
                    loop = false;
                }
                else if (keyPressed.Key == ConsoleKey.UpArrow)
                {
                    selection -= columns;
                }
                else if (keyPressed.Key == ConsoleKey.DownArrow)
                {
                    selection += columns;
                }
                else if (keyPressed.Key == ConsoleKey.LeftArrow)
                {
                    selection--;
                }
                else if (keyPressed.Key == ConsoleKey.RightArrow)
                {
                    selection++;
                }
                else if (keyPressed.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine($"What do you want to change {contestants[selection].fName}s interest to?");
                    contestants[selection].interest = Console.ReadLine();
                    UpdateFile(contestantsList);
                }
                selection = NumLoop(selection, 0, contestants.Length - 1);

            } while (loop == true);
  
            Settings();

        } //List Contestants End


        static void PlayerStats()
        {
            Console.WriteLine("Player stats");
            Console.ReadLine();
            Settings();

        } //Player Stats End

        static void GetPlayers()
        {
            Random rand = new Random();
            bool orignalContestant = true;
            int newContestant;
            int[] finalists = new int[10];
            Contestant finalist;

            UpdateContestants(contestantsList);

            Console.WriteLine("The finalists are: \n");

            for (int i = 0; i < finalists.Length; i++) //Get 10 Finalists
            {                                
                do
                {
                    orignalContestant = true;
                    newContestant = rand.Next(contestants.Length);

                    for (int j = 0; j < finalists.Length; j++)
                    {
                        if (newContestant == finalists[j])
                        {
                            orignalContestant = false;
                        }
                    }

                } while (orignalContestant == false);
                finalists[i] = newContestant;
                Console.WriteLine($"{i + 1}: {contestants[finalists[i]].fName} {contestants[finalists[i]].lName} \n");
            }
            Console.WriteLine("Press Any Key to Continue \n \n");
            Console.ReadKey(true);
            finalist = contestants[finalists[rand.Next(finalists.Length)]];
            Console.Write("Our Finalist Is");
            for (int i = 0; i < 3; i++)
            {
                Console.Write(".");
                Thread.Sleep(250);
            }
            Console.WriteLine();
            Console.WriteLine($"{finalist.fName} {finalist.lName}! \n");
            Console.WriteLine("Press Any Key to Continue");
            Console.ReadKey();

        } //Get Players End

        static void Game()
        {
            int[] QuestionsNums = new int[2];
            Question[] Questions = new Question[QuestionsNums.Length];
            Random rand = new Random();
            bool originalQuestion;
            for (int i = 0; i < QuestionsNums.Length; i++) //Set Question Numbers
            {
                int newQuestion;
                do
                {
                    originalQuestion = true;                    
                    newQuestion = rand.Next(1, 4);
                    for (int j = 0; j < QuestionsNums.Length; j++)
                    {
                        if (newQuestion == QuestionsNums[j])
                        {
                            originalQuestion = false;
                        }
                    }

                } while (originalQuestion == false);
                QuestionsNums[i] = newQuestion;
            }
            Console.WriteLine("You are now playing Family Feud");
            GetPlayers();
            
            for (int i = 0; i < QuestionsNums.Length; i++)
            {
                string rawQuestion = GetLine(QuestionsNums[i]);
                Questions[i].question = rawQuestion.Split(':')[0];
                Console.WriteLine(Questions[i].question);

                Questions[i].answer1.answer = rawQuestion.Split(':')[1].Split(';')[0].Split('.')[0];
                Questions[i].answer1.points = Convert.ToInt16(rawQuestion.Split(':')[1].Split(';')[0].Split('.')[1]);
                Console.WriteLine(Questions[i].answer1.answer);
                Console.WriteLine(Questions[i].answer1.points);

                Questions[i].answer1.answer = rawQuestion.Split(':')[1].Split(';')[1].Split('.')[0];
                Questions[i].answer1.points = Convert.ToInt16(rawQuestion.Split(':')[1].Split(';')[1].Split('.')[1]);
                Console.WriteLine(Questions[i].answer1.answer);
                Console.WriteLine(Questions[i].answer1.points);

                Questions[i].answer1.answer = rawQuestion.Split(':')[1].Split(';')[2].Split('.')[0];
                Questions[i].answer1.points = Convert.ToInt16(rawQuestion.Split(':')[1].Split(';')[2].Split('.')[1]);
                Console.WriteLine(Questions[i].answer1.answer);
                Console.WriteLine(Questions[i].answer1.points);

                Questions[i].answer1.answer = rawQuestion.Split(':')[1].Split(';')[3].Split('.')[0];
                Questions[i].answer1.points = Convert.ToInt16(rawQuestion.Split(':')[1].Split(';')[3].Split('.')[1]);
                Console.WriteLine(Questions[i].answer1.answer);
                Console.WriteLine(Questions[i].answer1.points);

                Questions[i].answer1.answer = rawQuestion.Split(':')[1].Split(';')[4].Split('.')[0];
                Questions[i].answer1.points = Convert.ToInt16(rawQuestion.Split(':')[1].Split(';')[4].Split('.')[1]);
                Console.WriteLine(Questions[i].answer1.answer);
                Console.WriteLine(Questions[i].answer1.points);

                Questions[i].answer1.answer = rawQuestion.Split(':')[1].Split(';')[5].Split('.')[0];
                Questions[i].answer1.points = Convert.ToInt16(rawQuestion.Split(':')[1].Split(';')[5].Split('.')[1]);
                Console.WriteLine(Questions[i].answer1.answer);
                Console.WriteLine(Questions[i].answer1.points);

                Console.WriteLine();
            }


            Console.ReadLine();
            Menu();

        } //Game End



        static void SortContestants()
        {
            Contestant temp;
            for (int i = 0; i < contestants.Length - 1; i++)
            {
                for (int pos = 0; pos < contestants.Length - 1; pos++)
                {
                    if (contestants[pos].lName.CompareTo(contestants[pos+1].lName) > 0)
                    {
                        temp = contestants[pos];
                        contestants[pos] = contestants[pos + 1];
                        contestants[pos + 1] = temp;
                    }
                }
            }

        } //Sort Contestants End

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

        } //Number Loop End

        static void UpdateContestants(string filePath)
        {
            StreamReader reader = new StreamReader(filePath);
            for (int i = 0; i < contestants.Length; i++)
            {
                contestants[i].fName = reader.ReadLine();
                contestants[i].lName = reader.ReadLine();
                contestants[i].interest = reader.ReadLine();
            }
            reader.Close();

        } //Update Contestants End

        static void UpdateFile(string filePath)
        {
            StreamWriter writer = new StreamWriter(filePath);
            for (int i = 0; i < contestants.Length; i++)
            {
                writer.WriteLine(contestants[i].fName);
                writer.WriteLine(contestants[i].lName);
                writer.WriteLine(contestants[i].interest);
            }
            writer.Close();   
            
        } //Update File End

        static string GetLine(int lineNum)
        {
            StreamReader reader = new StreamReader(@"H:\My Documents\IN510 Programming 1\Family Fued\IN510-Assignment\Questions.txt");
            string line = "";
            for (int i = 0; i < lineNum; i++)
            {
                line = reader.ReadLine();
            }
            reader.Close();
            return line;
        }


        //^^^ Other methods ^^^

        static void Main()
        {
            Console.CursorVisible = false;
            Menu();
        } //Main End
    }
}
