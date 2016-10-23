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
        private string value;

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
            Console.WriteLine(ConsoleUtil.Center("1. Display all movie records"));
            Console.WriteLine(ConsoleUtil.Center("2. Add movie record"));
            Console.WriteLine(ConsoleUtil.Center("3. Update movie record"));
            Console.WriteLine(ConsoleUtil.Center("4. Delete movie record"));
            Console.WriteLine(ConsoleUtil.Center("5. Clear ALL movie records"));
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

            int[] updatedScoreData = { -1, -1 };

            bool selectingPlayer = true;

            // find a valid player from saves
            while (selectingPlayer)
            {
                Console.Clear();
                Console.CursorVisible = true;

                Console.WriteLine("\n\tPlease input the name of the player whos score you wish to update");
                Console.Write("\tor press <Enter> to return to the main menu: ");
                string userName = Console.ReadLine();

                if (userName == "")
                {
                    selectingPlayer = false;
                    break;
                }

                // check through all player scores, replace player score if the name entered matches
                bool playerFound = false;

                for (int i = 0; i < MovieList.Count; i++)
                {
                    if (MovieList[i].MovieTitle == userName)
                    {
                        playerFound = true;

                        bool enteringNewScore = true;

                        // enter new player score
                        while (enteringNewScore)
                        {
                            int newScore = -1;

                            Console.Clear();
                            Console.WriteLine("\n\tCurrently " + userName + " has a year of: " + MovieList[i].MovieYear);
                            Console.WriteLine("\n\tPlease input the updated year for " + userName);
                            Console.Write("\tor press <enter> to select a diffent movie: ");
                            string userScore = Console.ReadLine();

                            if (userScore == "") { enteringNewScore = false; }
                            else if (int.TryParse(userScore, out newScore) && newScore >= 0)
                            {
                                selectingPlayer = false;
                                enteringNewScore = false;
                                updatedScoreData[0] = i;
                                updatedScoreData[1] = newScore;
                            }

                            else
                            {
                                Console.CursorVisible = false;
                                Console.WriteLine("\n\tValid scores must be postive integers!");
                                Console.Write("\tPress any key to continue");
                                Console.ReadKey();
                                Console.CursorVisible = true;
                            }
                        }

                        i = MovieList.Count;
                    }
                }

                Console.CursorVisible = false;
                if (!playerFound)
                {
                    Console.WriteLine("\n\n\tSorry, no player with that name is on record");
                    Console.Write("\tPress any key to try another name");
                    Console.ReadKey();
                }
            }

            return updatedScoreData;
            //    int[] updatedMovieData = { -1, -1};

            //    bool selectingMovie = true;

            //    // find a valid movie from saves
            //    while (selectingMovie)
            //    {

            //        Console.Clear();
            //        Console.CursorVisible = true;

            //        Console.WriteLine(ConsoleUtil.Center("\n\tPlease input the name of the movie you wish to update"));
            //        Console.Write("\tor press <Enter> to return to the main menu: ");
            //        string userTitle = Console.ReadLine();

            //        if (userTitle == "")
            //        {
            //            selectingMovie = false;
            //            break;
            //        }

            //        // check through all player scores, replace player score if the name entered matches
            //        bool movieFound = false;

            //        for (int i = 0; i < MovieList.Count; i++)
            //        {
            //            if (MovieList[i].MovieTitle == userTitle)
            //            {
            //                movieFound = true;

            //                bool enteringNewMovie = true;

            //                // enter new player score
            //                while (enteringNewMovie)
            //                {


            //                    Console.Clear();
            //                    Console.WriteLine(ConsoleUtil.Center("\n\tCurrently " + userTitle + " has a title of: " + this.MovieList[i].MovieTitle));
            //                    Console.WriteLine(ConsoleUtil.Center("\n\tPlease input the updated title for " + userTitle + "or press <enter> to select a diffent movie: "));
            //                    userTitle = Console.ReadLine();

            //                    //if (userTitle == "") { enteringNewMovie = false; }
            //                    //else
            //                    //{
            //                    //    selectingMovie = false;
            //                    //    enteringNewMovie = false
            //                    //}
            //                    Console.WriteLine(ConsoleUtil.Center("\n\tPlease input the updated year for " + userTitle));
            //                    string userYear = Console.ReadLine();
            //                    Console.WriteLine(ConsoleUtil.Center("\n\tPlease input the updated category for " + userTitle));
            //                    string userCat = Console.ReadLine();
            //                    Console.WriteLine(ConsoleUtil.Center("\n\tPlease input the updated recomendation (true/false) for " + userTitle));
            //                    string userRecommendation = Console.ReadLine();
            //                }

            //                i = MovieList.Count;
            //            }
            //        }

            //        Console.CursorVisible = false;
            //        if (!movieFound)
            //        {
            //            Console.WriteLine("\n\n\tSorry, title with that name is on record");
            //            Console.Write("\tPress any key to try another title");
            //            Console.ReadKey();
            //        }
            //    }

            //    return updatedMovieData;
            //}

        }

        /// <summary>
        /// displays the updated movies
        /// </summary>
        /// <param name="movieData"></param>
        public void DisplayUpdatePrompt(Movie updatedMovie)
        {
            Console.CursorVisible = false;
            Console.WriteLine("\tScore for " + updatedMovie.MovieTitle + " changed to " + updatedMovie.MovieYear);
            Console.Write("\n\tPress any key to return to main menu");
            Console.ReadKey();
            //Console.CursorVisible = false;
            //Console.WriteLine("\tMovie Title: " + updatedMovie.MovieTitle + " changed to " + updatedMovie.MovieTitle);
            //Console.WriteLine("\tMovie Category: " + updatedMovie.MovieCat + " changed to " + updatedMovie.MovieCat);
            //Console.WriteLine("\tMovie Year: " + updatedMovie.MovieYear + " changed to " + updatedMovie.MovieYear);
            //Console.WriteLine("\tMovie Recommendation: " + updatedMovie.WouldRecommend + " changed to " + updatedMovie.WouldRecommend);
            //Console.Write("\n\tPress any key to return to main menu");
            //Console.ReadKey();
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
                string addYear;
              
                string addRecommendation;
                string addCat;

                Console.WriteLine(ConsoleUtil.Center("\nPlease enter the name of the title that you want to add \n or press <Enter> to return to the main menu: "));
                string addTitle = Console.ReadLine();
                if (addTitle == "")
                {
                    break;
                }
                else
                {
                    Console.WriteLine(ConsoleUtil.Center("Now enter " + addTitle + " Movie Year."));
                    addYear = Console.ReadLine();
                    addedRecord = addTitle + DataSetting.delineator + addYear;
                    
                }

                if (addYear == "")
                {
                    break;
                }
                else
                {
                    Console.WriteLine(ConsoleUtil.Center("Now enter " + addTitle + " Movie Category."));
                    addCat = Console.ReadLine();
                    addedRecord = addTitle + DataSetting.delineator + addYear + DataSetting.delineator + addCat;
                }
                if (addCat == "")
                {

                }
                else
                {
                    Console.WriteLine(ConsoleUtil.Center("Would you recommend " + addTitle + "? Enter true or false."));
                    addRecommendation = Console.ReadLine();
                    addedRecord = addTitle + DataSetting.delineator + addYear + DataSetting.delineator + addCat + DataSetting.delineator + addRecommendation;
                    addingRecord = false;
                }
            }
            string[] addedRecordArray = addedRecord.Split(DataSetting.delineator);
            Movie newRecord = new Movie() { MovieTitle = addedRecordArray[0], MovieYear = Convert.ToInt32(addedRecordArray[1]), WouldRecommend = Convert.ToBoolean(addedRecordArray[2]) }; //MovieCat = addedRecordArray[2] };
            return newRecord;//MovieCat = Movie.MovieCategory.TryParse<Movie.MovieCategory>(value, out addedRecordArray[2]),
        }

        /// <summary>
        /// Deleting a record
        /// </summary>
        public string DiplayDeleteRecordScreen()
        {
            bool deletingRecord = true;
            string deleteRecord = "";
            while (deletingRecord)
            {
                Console.Clear();
                Console.CursorVisible = true;

                Console.WriteLine("Please enter the name of the movie that you want to delete.");
                Console.Write("\tor press <Enter> to return to the main menu: ");
                string deleteMovieName = Console.ReadLine();

                if (deleteMovieName == "")
                {
                    break;
                }
                else
                {
                    deleteRecord = deleteMovieName;
                    deletingRecord = false;
                }
            }

            return deleteRecord;
        }

        /// <summary>
        /// displays a prompt if there's no record
        /// </summary>
        public void DisplayNoRecordPrompt()
        {
            Console.WriteLine("No player record found!");
            Console.ReadKey();
        }

        /// <summary>
        /// method to clear a message
        /// </summary>
        public void DisplayClearMessage()
        {
            Console.WriteLine("\n\n\t\t\tAll of your files have been cleared");

            Console.WriteLine("\n\t\t\tPress any key to continue");
            Console.ReadKey();

            _currentViewState = ViewState.MainMenu;
        }


        #endregion

    }


}
