using PersistenceCSV_Briggs50.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersistenceCSV_Briggs50.View
{
    class ConsoleView
    {
        #region ENUMS


        public enum ViewState
        {
            WelcomeScreen,
            MainMenu,
            DisplayAllRecords,
            ClearAllRecords,
            AddRecord,
            DeleteRecord,
            UpdateRecord,
            Quit
        }

        #endregion

        #region FIELDS

        private ViewState _currentViewState;

        #endregion

        #region PROPERTIES

        public ViewState CurrentViewState
        {
            get { return _currentViewState; }
            set { _currentViewState = value; }
        }

        #endregion


        #region CONSTRUCTORS

        public ConsoleView()
        {
            _currentViewState = ViewState.WelcomeScreen;

            InitializeView();
        }

        #endregion

        #region METHODS

        /// <summary>
        /// Initializes the console view
        /// </summary>
        public void InitializeView()
        {

            InitializeConsole();
        }

        /// <summary>
        /// Configures the console window
        /// </summary>
        public void InitializeConsole()
        {
            ConsoleUtil.WindowWidth = ConsoleConfig.windowWidth;
            ConsoleUtil.WindowHeight = ConsoleConfig.windowHeight;

            Console.BackgroundColor = ConsoleConfig.bodyBackgroundColor;
            Console.ForegroundColor = ConsoleConfig.bodyBackgroundColor;

        }

        /// <summary>
        /// Displays the welcome screen
        /// </summary>
        public void DisplayWelcomeScreen()
        {

            ConsoleUtil.DisplayReset();
            Console.WriteLine("\n\n\n\n\n\n");
            Console.WriteLine(ConsoleUtil.Center("Welcome to my intricate Movie Information Keeper"));
            Console.WriteLine("\n\n\n\n\n\n");
            Console.WriteLine(ConsoleUtil.Center("Press any key to continue"));
            Console.CursorVisible = false;
            Console.ReadKey();
            _currentViewState = ViewState.MainMenu;
        }

        /// <summary>
        /// Displays the main menu screen
        /// </summary>
        public void DisplayMainMenuScreen()
        {
            Console.Clear();
            Console.WriteLine("\n\n\n");
            Console.WriteLine(ConsoleUtil.Center("Please select a menu option from the following:"));
            Console.WriteLine(ConsoleUtil.Center("1. Display all score records"));
            Console.WriteLine(ConsoleUtil.Center("2. Add score record"));
            Console.WriteLine(ConsoleUtil.Center("3. Update score record"));
            Console.WriteLine(ConsoleUtil.Center("4. Delete score record"));
            Console.WriteLine(ConsoleUtil.Center("5. Clear ALL score records"));
            Console.WriteLine(ConsoleUtil.Center("6. Quit"));
            Console.WriteLine("\n\n\n");


            Console.Write(ConsoleUtil.Center("Please enter a menu option between 1 and 6"));
            Console.WriteLine("\n\n\n");

            MainMenuChoice();
        }


        /// <summary>
        /// Makes the menu selections work
        /// </summary>
        private void MainMenuChoice()
        {
            int menuChoice = GetMainMenuChoice();

            switch (menuChoice)
            {
                case 1:
                    _currentViewState = ViewState.DisplayAllRecords;
                    // display all score records selected
                    break;
                case 2:
                    // add score record selected
                    _currentViewState = ViewState.AddRecord;
                    break;
                case 3:
                    // update score record selected
                    _currentViewState = ViewState.UpdateRecord;
                    break;
                case 4:
                    // delete score record selected
                    _currentViewState = ViewState.DeleteRecord;
                    break;
                case 5:
                    // delete ALL score records selected
                    _currentViewState = ViewState.ClearAllRecords;
                    break;
                case 6:
                    // quit selected
                    _currentViewState = ViewState.Quit;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Gets the main menu choice and validates it
        /// </summary>
        /// <returns int > main menu choice</returns>
        private int GetMainMenuChoice()
        {
            int maxAttempts = 3;
            int numberOfAttempts = 0;
            int menuChoice = -1;
            bool choosing = true;
            while (choosing & numberOfAttempts != maxAttempts)
            {

                // check for valid integer from readKey, and make sure integer is in range
                if (int.TryParse(Console.ReadKey().KeyChar.ToString(), out menuChoice) && menuChoice > 0 && menuChoice <= 6)
                {
                    choosing = false;
                }
                else
                {
                    Console.WriteLine(ConsoleUtil.Center("ERROR: Valid menu choices are numbers 1-6"));
                    numberOfAttempts++;
                    
                }

                
                
            }

            if (numberOfAttempts == maxAttempts)
            {
                Console.Clear();
                DisplayExitScreen();
            }
            return menuChoice;
        }


        public void DisplayExitScreen()
        {
            Console.WriteLine("\n\n\n");
            Console.WriteLine(ConsoleUtil.Center("Thank you for playing our program"));
            Console.WriteLine("\n\n\n");
            Console.WriteLine(ConsoleUtil.Center("Press any key to exit"));
            Console.ReadKey();

            System.Environment.Exit(1);
        }

        #endregion

    }


}
