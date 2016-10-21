using PersistenceCSV_Briggs50.Controller;
using PersistenceCSV_Briggs50.Data;
using PersistenceCSV_Briggs50.Model;
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
        private static GameController _gameController;
        private List<Movie> MovieList;
        private List<string> MovieStringListWrite;

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

                // checks that the integer is in range and that it is a key
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

        /// <summary>
        /// displays an error message and continue prompt
        /// </summary>
        /// <param name="errorMessage"></param>
        public void DisplayErrorPrompt(string errorMessage)
        {

            Console.WriteLine(ConsoleUtil.Center("WE'VE ENCOUNTERED AN ERROR"));
            Console.WriteLine("  " + errorMessage);
            Console.WriteLine("\n\tPress any key to continue");

            Console.CursorVisible = false;
            Console.ReadKey();
        }

        /// <summary>
        /// displays an exit screen
        /// </summary>
        public void DisplayExitScreen()
        {
            Console.WriteLine("\n\n\n");
            Console.WriteLine(ConsoleUtil.Center("Thank you for playing our program"));
            Console.WriteLine("\n\n\n");
            Console.WriteLine(ConsoleUtil.Center("Press any key to exit"));
            Console.ReadKey();

            System.Environment.Exit(1);
        }

        /// <summary>
        /// displaying the movies
        /// </summary>
        /// <param name="MovieList"></param>
        public void DisplayMovies(List<Movie> MovieList)
        {
            if (MovieList.Count == 0)
            {
                Console.WriteLine(ConsoleUtil.Center("\n\nThere are no stored records to display"));
            }
            else
            {
                foreach (Movie movie in MovieList)
                {
                    Console.WriteLine("\nMovie Title: {0}\n Movie Year: {1}\n Movie Category: {2} \n Would Recommend: {3}", movie.MovieTitle, movie.MovieYear, movie.MovieCat, movie.WouldRecommend);
                }
            }

            Console.ReadKey();
            _currentViewState = ViewState.MainMenu;

        }

        /// <summary>
        /// displays the updated records to the screen
        /// </summary>
        /// <param name="MovieList"></param>
        public int[] DisplayUpdateRecordScreen(List<Movie> MovieList)
        {
            int[] updatedMovieData = { -1, -1 };

            bool selectingMovie = true;

            // find a valid player from saves
            while (selectingMovie)
            {
                Console.Clear();
                Console.CursorVisible = true;
                string promptMessage = "";

                Console.WriteLine("\n\tPlease input the name of the movie you wish to update");
                Console.Write("\tor press <Enter> to return to the main menu: ");
                string movieTitle = Console.ReadLine();

                if (movieTitle == "")
                {
                    selectingMovie = false;
                    break;
                }

                // check through all player scores, replace player score if the name entered matches
                bool movieFound = false;

                for (int i = 0; i < MovieList.Count; i++)
                {
                    if (MovieList[i].MovieTitle == movieTitle)
                    {
                        movieFound = true;

                        bool enteringNewMovie = true;

                        // enter new player score
                        while (enteringNewMovie)
                        {
                            int newMovie = -1;

                            Console.Clear();
                            Console.WriteLine(ConsoleUtil.Center("\n\tCurrently " + movieTitle + " has a score of: " + MovieList[i].MovieTitle));
                            Console.WriteLine(ConsoleUtil.Center("\n\tPlease input the updated score for " + movieTitle + "or press <enter> to select a diffent player: "));
                            string userScore = Console.ReadLine();                          
                        }

                        i = MovieList.Count;
                    }
                }

                Console.CursorVisible = false;
                if (!movieFound)
                {
                    Console.WriteLine("\n\n\tSorry, title with that name is on record");
                    Console.Write("\tPress any key to try another title");
                    Console.ReadKey();
                }
            }

            return updatedMovieData;
        }

        /// <summary>
        /// displays the updated movies
        /// </summary>
        /// <param name="movieData"></param>
        public void DisplayUpdatePrompt(Movie movieData)
        {
            Console.CursorVisible = false;
            Console.WriteLine("\tMovie Title: " + movieData.MovieTitle + " changed to " + movieData.MovieTitle);
            Console.WriteLine("\tMovie Title: " + movieData.MovieCat + " changed to " + movieData.MovieCat);
            Console.WriteLine("\tMovie Title: " + movieData.MovieYear + " changed to " + movieData.MovieYear);
            Console.WriteLine("\tMovie Title: " + movieData.WouldRecommend + " changed to " + movieData.WouldRecommend);
            Console.Write("\n\tPress any key to return to main menu");
            Console.ReadKey();
        }

        /// <summary>
        /// Method for adding a record
        /// </summary>
        public Movie DisplayAddRecordScreen()
        {
            bool addingRecord = true;
            string addedRecord = "";
            while (addingRecord)
            {
                Console.Clear();
                Console.CursorVisible = true;

                Console.WriteLine(ConsoleUtil.Center("\nPlease enter the name of the title that you want to add \n or press <Enter> to return to the main menu: "));
                string addTitle = Console.ReadLine();
                if (addTitle == "")
                {
                    break;
                }
                else
                {
                    Console.WriteLine(ConsoleUtil.Center("Now enter " + addTitle + " Movie Year."));
                    string addScore = Console.ReadLine();
                    addedRecord = addTitle + DataSetting.delineator + addScore;
                    addingRecord = false;
                }
            }
            string[] addedRecordArray = addedRecord.Split(DataSetting.delineator);
            Movie newRecord = new Movie() { MovieTitle = addedRecordArray[0], MovieYear = Convert.ToInt32(addedRecordArray[1]) };
            return newRecord;
        }
        #endregion

    }


}
