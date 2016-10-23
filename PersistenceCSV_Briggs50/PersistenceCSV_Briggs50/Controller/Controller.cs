using PersistenceCSV_Briggs50.Data;
using PersistenceCSV_Briggs50.Model;
using PersistenceCSV_Briggs50.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersistenceCSV_Briggs50.Controller
{
    class GameController
    {
        #region FIELDS

        // instantiate the gameview
        private static ConsoleView _gameView = new ConsoleView();
        private bool _usingApp;
        private string dataFile;
        private List<Movie> MovieList = new List<Movie>();
        private List<string> MovieStringListWrite = new List<string>();
        private string errorMessage;

        #endregion

        #region CONSTRUCTORS

        public GameController()
        {
            InitializeGame();
            UseGame();
        }

        #endregion

        #region METHODS

        /// <summary>
        /// sets up the game on initialization
        /// </summary>
        public void InitializeGame()
        {
            _usingApp = true;
        }

        /// <summary>
        /// The main game loop
        /// </summary>
        public void UseGame()
        {
            while (_usingApp)
            {
                ManageStateTasks();
            }

            System.Environment.Exit(1);
        }

        /// <summary>
        /// manages the states and tasks of the game
        /// </summary>
        public void ManageStateTasks()
        {
            switch (_gameView.CurrentViewState)
            {
                case ConsoleView.ViewState.WelcomeScreen:
                    _gameView.DisplayWelcomeScreen();
                    break;
                case ConsoleView.ViewState.MainMenu:
                    _gameView.DisplayMainMenuScreen();
                    break;
                case ConsoleView.ViewState.DisplayAllRecords:
                    ReadMoviesFromTextFile();
                    _gameView.DisplayMovies(MovieList);
                    break;
                case ConsoleView.ViewState.ClearAllRecords:
                    ClearScores();
                    _gameView.DisplayClearMessage();
                    break;
                case ConsoleView.ViewState.AddRecord:
                    AddRecord();
                    break;
                case ConsoleView.ViewState.DeleteRecord:
                    DeleteRecord();
                    break;
                case ConsoleView.ViewState.UpdateRecord:
                    ProcessUpdateRecord();
                    break;
                case ConsoleView.ViewState.Quit:
                    _gameView.DisplayExitScreen();
                    _usingApp = false;
                    break;
                default:
                    break;
            }
        }


        /// <summary>
        /// writes to the text file
        /// </summary>
        public void WriteMoviesToTextFile()
        {           

            try
            {

                string MovieListString;

                // build the list to write to the text file line by line
                foreach (var movie in MovieList)
                {
                    MovieListString = movie.MovieTitle + DataSetting.delineator + movie.MovieYear + DataSetting.delineator + movie.MovieCat + DataSetting.delineator + movie.WouldRecommend;
                    MovieStringListWrite.Add(MovieListString);
                }

                File.WriteAllLines(DataSetting.textFilePath, MovieStringListWrite);         

                _gameView.CurrentViewState = ConsoleView.ViewState.MainMenu;
            }

            catch (Exception ex)
            {
                _gameView.DisplayErrorPrompt(ex.Message);
                throw;
            }

        }

        /// <summary>
        /// reads from the text file
        /// </summary>
        public List<Movie> ReadMoviesFromTextFile()
        {
            try
            {
                List<string> MovieStringList = new List<string>();

                MovieList.Clear();


                MovieStringList = File.ReadAllLines(DataSetting.textFilePath).ToList();

                foreach (string MovieString in MovieStringList)
                {//TO DO ADD IN THE ENUM
                    // use the Split method and the delineator on the array to separate each property into an array of properties
                    string[] properties = MovieString.Split(DataSetting.delineator);
                    MovieList.Add(new Movie() {
                        MovieTitle = properties[0],
                        MovieYear = Convert.ToInt32(properties[1]),
                       // MovieCat = Movie.MovieCategory.TryParse<Movie.MovieCategory>(, out properties[2]),
                        WouldRecommend = Convert.ToBoolean(properties[3])
                        
                   //TODO parse or try parse   //  MovieCat = 
                    });             
                }

                return MovieList;
            }
            catch (Exception ex)
            {
                _gameView.DisplayErrorPrompt(ex.Message);
                throw;
            }

        }

        /// <summary>
        /// method to process an update
        /// </summary>
        public void ProcessUpdateRecord()
        {

            try
            {
                ReadMoviesFromTextFile();

                // create score data as an array of list index + updated score info
                int[] scoreData = _gameView.DisplayUpdateRecordScreen(MovieList);

                // if valid score data is returned, update scorelist and write to file using score data array
                if (scoreData[0] != -1)
                {
                    MovieList[scoreData[0]].MovieYear = scoreData[1];
                    WriteMoviesToTextFile();
                    _gameView.DisplayUpdatePrompt(MovieList[scoreData[0]]);
                }

                //// create score data as an array of list index + updated score info
                //int[] movieData = _gameView.DisplayUpdateRecordScreen(MovieList);

                //// if valid score data is returned, update scorelist and write to file using score data array
                //if (movieData[0] != -1)
                //{
                //    MovieList[movieData[0]].MovieTitle = Convert.ToString(movieData[1]);
                //    WriteMoviesToTextFile();
                //    _gameView.DisplayUpdatePrompt(MovieList[movieData[0]]);
                
            }
            catch (Exception ex)
            {
                _gameView.DisplayErrorPrompt(ex.Message);
            }
            _gameView.CurrentViewState = ConsoleView.ViewState.MainMenu;
        }

        /// <summary>
        /// method to add records
        /// </summary>
        private void AddRecord()
        {
            try
            {
                Movie movie = _gameView.DisplayAddRecordScreen();

                MovieList.Add(movie);

                WriteMoviesToTextFile();

            }
            catch (Exception ex)
            {
                _gameView.DisplayErrorPrompt(ex.Message);
                throw;
            }
        }

        private void DeleteRecord()
        {
            try
            {
                string deletedMovieName = _gameView.DiplayDeleteRecordScreen();
                ReadMoviesFromTextFile();
                int MovieListIndex = 0;
                bool MovieFound = false;

                for (int i = 0; i < MovieList.Count; i++)
                {
                    if (deletedMovieName == MovieList[i].MovieTitle)
                    {
                        MovieListIndex = i;
                        MovieFound = true;
                    }
                }

                if (MovieFound)
                {
                    MovieList.Remove(MovieList[MovieListIndex]);
                    WriteMoviesToTextFile();
                }
                else
                {
                    _gameView.DisplayNoRecordPrompt();
                    _gameView.CurrentViewState = ConsoleView.ViewState.MainMenu;
                }
            }
            catch (Exception ex)
            {
                _gameView.DisplayErrorPrompt(ex.Message);

                throw;
            }

        }

        /// <summary>
        /// clears all high scores from the text file
        /// </summary>
        private void ClearScores()
        {
            try
            {
                string MovieListString;

                foreach (var movie in MovieList)
                {
                    MovieListString = movie.MovieTitle + DataSetting.delineator + movie.MovieYear + DataSetting.delineator + movie.MovieCat + DataSetting.delineator + movie.WouldRecommend;
                    MovieStringListWrite.Add(MovieListString);
                }
                File.WriteAllText(DataSetting.textFilePath, string.Empty);
            }

            catch (Exception)
            {
                _gameView.DisplayErrorPrompt(errorMessage);
                throw;
            }



        }


        #endregion
    }
}
