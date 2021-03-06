using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace HeadQuarters
{
    public class UserInterface
    {
        public void Initialize(bool isNew, GameData sv)
        {
            if (isNew == true)
            {
                DisplayIntro(sv);
            }

            bool end = false;

            //Game Loop
            while(end == false)
            {
                UpdateUI(sv);
                Choose(sv);
            }           
        } 
        
        private void DisplayIntro(GameData sv)
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
        private void UpdateUI(GameData sv)
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
        private void Choose(GameData sv)
        {
            bool canEnd = false;

            while (!canEnd)
            {
                switch (Console.ReadLine())
                {
                    default:
                        Console.WriteLine("Please enter a valid option");
                        break;
                    case "*":
                        GameSave.WriteToXmlFile<GameData>(Directory.GetCurrentDirectory() + @"\saves\" + sv.Name + ".xml", sv, false);
                        Console.WriteLine("Game Saved!", Console.ForegroundColor = ConsoleColor.Green);
                        Console.ForegroundColor = ConsoleColor.Red;
                        break;
                    case "1":
                        OpenEconomy(sv);
                        break;
                    case "2":
                        OpenLogistics(sv);
                        break;
                    case "-":
                        canEnd = true;
                        MainMenu.RunMenu();
                        break;
                }
            }
            
        }

        /// <summary>
        /// Open Economy tab
        /// </summary>
        /// <param name="sv"></param>
        private void OpenEconomy(GameData sv)
        {
            bool canLeaveEco = false;

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("1 : Earn 10 Felcoins");
            Console.WriteLine("- : Back");

            while (!canLeaveEco)
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
                    case "-":
                        canLeaveEco = true;
                        Initialize(false, sv);
                        break;

                }
            }
            
        }

        private void OpenLogistics(GameData sv)
        {
            bool canLeaveLogi = false;

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("1 : Manage Divisions");
            Console.WriteLine("- : Back");

            while (!canLeaveLogi)
            {
                switch (Console.ReadLine())
                {
                    default:
                        Console.WriteLine("Please choose a valid option", Console.ForegroundColor = ConsoleColor.Red);
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        break;
                    case "1":
                        ManageDiv(sv);
                        break;
                    case "-":
                        canLeaveLogi = true;
                        Initialize(false, sv);
                        break;
                }
            }

        }

        private void ManageDiv(GameData sv)
        {
            bool canLeaveManage = false;

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Divisions List");
            Console.WriteLine("--------------------");

            if(sv.divisionList.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No Divisions.");
                Console.ForegroundColor = ConsoleColor.White;
            }

            foreach(var div in sv.divisionList)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(div.Number + div.Ordinal + " " + div.TypeName + " Division");
                Console.ForegroundColor = ConsoleColor.White;
            }
            Console.WriteLine("--------------------");

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("1 : Create a Division");
            Console.WriteLine("2 : Delete a Division");
            Console.WriteLine("- : Back");

            while (!canLeaveManage)
            {
                switch (Console.ReadLine())
                {
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Please choose a valid option");
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        break;
                    case "1":
                        CreateDiv(sv);
                        break;
                    case "2":
                        DeleteDiv(sv);
                        break;
                    case "-":
                        canLeaveManage = true;
                        OpenLogistics(sv);
                        break;
                }
            }
        }

        private void CreateDiv(GameData sv)
        {
            bool isValid = false ;

            Console.Clear();
            Division div = new Division();
            Console.WriteLine("Which type of Division to you want to create?");
            Console.WriteLine("0 : Infantry\n 1 : Armored\n 2 : Cavalry\n 3: Mountain\n 4 : Airborne\n 5 : Artillery\n 6 : Security");

            while (!isValid)
            {
                switch (Console.ReadLine())
                {
                    case "0":
                        div.TypeIndex = 0;
                        isValid = true;
                        break;
                    case "1":
                        div.TypeIndex = 1;
                        isValid = true;
                        break;
                    case "2":
                        div.TypeIndex = 2;
                        isValid = true;
                        break;
                    case "3":
                        div.TypeIndex = 3;
                        isValid = true;
                        break;
                    case "4":
                        div.TypeIndex = 4;
                        isValid = true;
                        break;
                    case "5":
                        div.TypeIndex = 5;
                        isValid = true;
                        break;
                    case "6":
                        div.TypeIndex = 6;
                        isValid = true;
                        break;
                    default:
                        Console.WriteLine("Please enter a valid option");
                        break;
                }
            }

            foreach (var existingDivs in sv.divisionList)
            {
                if(div.ID == existingDivs.ID)
                {
                    Random r = new Random();
                    div.Number = r.Next(999);                                     
                }
            }

            sv.divisionList.Add(div);

            Console.Clear();
            ManageDiv(sv);          
        }

        private void DeleteDiv(GameData sv)
        {
            int i = 1;
            Dictionary<string, string> ids = new Dictionary<string, string>();
            bool isDeleted = false;

            while (!isDeleted)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Existing Divisions List");
                Console.WriteLine("--------------------");

                Console.ForegroundColor = ConsoleColor.Green;
                foreach (var division in sv.divisionList)
                {
                    Console.WriteLine(i + " : " + division.Number + division.Ordinal + " " + division.TypeName + " Division");
                    ids.Add(i.ToString(), division.ID);
                    i++;
                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("--------------------");
                Console.WriteLine("- : Back to Menu");
                Console.WriteLine();
                Console.WriteLine("Which division do you want to delete?");

                string deleteChoice = Console.ReadLine();

                if (deleteChoice == "-")
                {
                    isDeleted = true;
                    ManageDiv(sv);
                }

                if (ids.ContainsKey(deleteChoice))
                {
                    Division divToDel = new Division();
                    sv.divisionList.RemoveAll(divToDel => divToDel.ID == ids[deleteChoice]);
                    isDeleted = true;
                    Console.Clear();
                    ManageDiv(sv);
                }
                else
                {
                    Console.WriteLine("Please choose a valid option");
                }
            }
            
        }
    }
    
}
