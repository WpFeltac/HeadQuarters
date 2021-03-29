using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace HeadQuarters
{
    public class UserInterface
    {
        public void InitializeUI(bool isNew, Game sv)
        {
            if (isNew == true)
            {
                DisplayIntro(sv);
            }

            bool end = false;

            //Game Loop
            while(end == false)
            {
                Update(sv);
                Choose(sv);
            }           
        } 
        
        private void DisplayIntro(Game sv)
        {
            Console.Clear();
            Console.WriteLine("Welcome, Commander " + sv.PlayerName);
            Console.WriteLine("--------------------");
            Console.WriteLine("You are now the highest ranked officer, and we need you to organise and lead our army towards victory.");
            Console.WriteLine("To help you completing this mission, our intel will provide you useful informations about ennemy.");
            Console.WriteLine("Then, all you have to do is to take the right decisions.\nGood luck!");
            Console.WriteLine("Press Enter to continue...", Console.ForegroundColor = ConsoleColor.DarkGray);
            while(Console.ReadKey(true).Key != ConsoleKey.Enter)
            {
                Console.WriteLine("Press Enter to continue...", Console.ForegroundColor = ConsoleColor.DarkGray);
            }
            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// Updates menu UI
        /// </summary>
        /// <param name="sv"></param>
        private void Update(Game sv)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("Level " + sv.Level);
            //Rank in green, name in white
            Console.Write(sv.Rank, Console.ForegroundColor = ConsoleColor.Green);
            Console.WriteLine(" " + sv.PlayerName, Console.ForegroundColor = ConsoleColor.White);
            //Money in yellow
            Console.WriteLine("Money : " + sv.Money + " Felcoins", Console.ForegroundColor = ConsoleColor.Yellow);
            Console.WriteLine();
            Console.WriteLine("What do you want to manage?", Console.ForegroundColor = ConsoleColor.White);
            //Red
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n* : Save");
            Console.WriteLine("--------------------");
            Console.WriteLine("1 : Economy");
            Console.WriteLine("2 : Logistics");
            Console.WriteLine("--------------------");
            Console.WriteLine("- : Back to Menu");
        }

        /// <summary>
        /// Root of all sub-menus
        /// </summary>
        /// <param name="sv"></param>
        private void Choose(Game sv)
        {
            switch (Console.ReadLine())
            {
                default:
                    Console.WriteLine("Please enter a valid option");
                    break;
                case "*":
                    GameSave.WriteToXmlFile<Game>(Directory.GetCurrentDirectory() + @"\saves\" + sv.Name + ".xml", sv, false);
                    Console.WriteLine("Game Saved!", Console.ForegroundColor = ConsoleColor.Green);
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case "1":
                    OpenEconomy(sv); 
                    break;
                case "-":
                    Program.RunMenu();
                    break;
            }
        }

        /// <summary>
        /// Open Economy tab
        /// </summary>
        /// <param name="sv"></param>
        private void OpenEconomy(Game sv)
        {
            bool toExit = false;

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("1 : Earn 10 Felcoins");
            Console.WriteLine("9 : Back");

            while (!toExit)
            {
                switch (Console.ReadLine())
                {
                    default:
                        Console.WriteLine("Please choose a valid option", Console.ForegroundColor = ConsoleColor.Red);
                        break;                    
                    case "1":
                        sv.Money += 10;
                        Console.WriteLine("You earned 10 Felcoins.", Console.ForegroundColor = ConsoleColor.Cyan);
                        Console.ForegroundColor = ConsoleColor.Red;
                        break;
                    case "9":
                        toExit = true;
                        break;

                }
            }
            
        }
    }
}
