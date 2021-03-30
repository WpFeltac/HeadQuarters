using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace HeadQuarters
{
    public class MainMenu
    {
        static void Main()
        {
            RunMenu();         
        }

        /// <summary>
        /// Runs the menu ; allow to be accessed anywhere else in the code
        /// </summary>
        public static void RunMenu()
        {
            Console.ForegroundColor = ConsoleColor.White;            

            switch (DisplayMenu())
            {
                case -1:
                    Quit();
                    break;
                case 1:
                    CreateNewGame();
                    break;
                case 2:
                    LoadSavedGame();
                    break;
            }
        }


        private static int DisplayMenu()
        {
            bool isValid = false;

            Console.Clear();
            Console.WriteLine("HeadQuarters v" + Assembly.GetEntryAssembly().GetName().Version.ToString() + "\n\n");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Menu");
            Console.WriteLine("--------------------");
            Console.WriteLine("1 : New Game");
            Console.WriteLine("2 : Load Game");
            Console.WriteLine("--------------------");
            Console.WriteLine("- : Quit");            
            Console.WriteLine();

            while (isValid == false)
            {
                switch (Console.ReadLine())
                {
                    case "1":
                        return 1;
                    case "2":
                        return 2;
                    case "-":
                        return -1;
                    default:
                        Console.WriteLine("Please enter a valid number.");
                        break;
                }
            }

            return -1;
        }

        private static void Quit()
        {
            Environment.Exit(0);
        }

        private static void CreateNewGame()
        {
            GameData sv1 = new GameData();            

            Console.Clear();
            Console.WriteLine("Create new game\n--------------------");
            Console.WriteLine("Enter the game save name :");
            sv1.Name = Console.ReadLine();
            Console.WriteLine("Enter your character name : ");
            sv1.PlayerName = Console.ReadLine();
            Console.WriteLine();

            //Save the new game
            bool exists = Directory.Exists("saves");
            if (!exists)
            {
                Directory.CreateDirectory("saves");
            }
            
            GameSave.WriteToXmlFile<GameData>(Directory.GetCurrentDirectory() + @"\saves\" + sv1.Name + ".xml", sv1, false);

            UserInterface ui = new UserInterface();
            ui.Initialize(true, sv1);
        }

        private static void LoadSavedGame()
        {
            //Source : https://stackoverflow.com/questions/11861151/find-all-files-in-a-folder            
            bool isLoaded = false;

            int i = 1;
            Dictionary<string, string> index = new Dictionary<string, string>();

            bool exists = Directory.Exists("saves");

            //Check if folder exists. If not, create it.
            if (!exists)
            {
                Directory.CreateDirectory("saves");
            }

            Console.Clear();
            string filepath = Directory.GetCurrentDirectory() + @"\saves\";
            DirectoryInfo d = new DirectoryInfo(filepath);           
            
            Console.WriteLine("\nWhat do you want to do?");
            Console.WriteLine();
            Console.WriteLine("1 : Load Saved Game");
            Console.WriteLine("2 : Delete Saved Game");
            Console.WriteLine("--------------------");
            Console.WriteLine("- : Back to Menu");

            Console.ForegroundColor = ConsoleColor.White;


            while (!isLoaded)
            {
                switch (Console.ReadLine())
                {
                    default:
                        Console.WriteLine("Please enter a valid number.");
                        break;

                    case "-":
                        isLoaded = true;
                        RunMenu();
                        break;

                    case "1":
                        Console.Clear();
                        Console.Clear();
                        //Get saved games list
                        Console.WriteLine("Saved Games List");
                        Console.WriteLine("--------------------");

                        int count = d.GetFiles().Length;

                        if (count == 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("No saved games.");
                            Console.ForegroundColor = ConsoleColor.White;

                        }
                        else
                        {
                            foreach (var file in d.GetFiles("*.xml"))
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                string name = Path.GetFileNameWithoutExtension(file.Name);
                                Console.WriteLine(i + " : " + name);
                                index.Add(i.ToString(), file.Name);
                                i++;
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                        }
                        
                        Console.WriteLine("--------------------");
                        Console.WriteLine("- : Back to Menu");
                        Console.WriteLine();
                        Console.WriteLine("Which save do you want to load?");
                        string userChoice = Console.ReadLine();

                        if(userChoice == "-")
                        {
                            RunMenu();
                        }

                        if (index.ContainsKey(userChoice))
                        {
                            GameData sv = GameSave.ReadFromXmlFile<GameData>(filepath + index[userChoice]);
                            UserInterface ui = new UserInterface();
                            ui.Initialize(false, sv);
                            isLoaded = true;
                        }
                        else
                        {
                            Console.WriteLine("Please choose a valid option");
                        }
                                               
                        break;

                    case "2":
                        Console.Clear();
                        //Get saved games list
                        Console.WriteLine("Saved Games List");
                        Console.WriteLine("--------------------");

                        int count2 = d.GetFiles().Length;

                        if (count2 == 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("No saved games.");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        else
                        {
                            foreach (var file in d.GetFiles("*.xml"))
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                string name = Path.GetFileNameWithoutExtension(file.Name);
                                Console.WriteLine(i + " : " + name);
                                index.Add(i.ToString(), file.Name);
                                i++;
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                        }

                        Console.WriteLine("--------------------");
                        Console.WriteLine("- : Back to Menu");
                        Console.WriteLine();
                        Console.WriteLine("Which file do you want to delete?");
                        string deleteChoice = Console.ReadLine();

                        if (deleteChoice == "-")
                        {
                            RunMenu();
                        }

                        if (index.ContainsKey(deleteChoice))
                        {
                            File.Delete(filepath + index[deleteChoice]);
                            isLoaded = true;
                        }
                        else
                        {
                            Console.WriteLine("Please choose a valid option");
                        }

                        RunMenu();
                        break;
                }

            }

        }
       
    }
}
