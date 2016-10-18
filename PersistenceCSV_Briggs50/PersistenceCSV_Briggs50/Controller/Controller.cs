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
                    _gameView.DisplayMovies(MovieList);
                    break;
                case ConsoleView.ViewState.ClearAllRecords:
                    break;
                case ConsoleView.ViewState.AddRecord:
                    break;
                case ConsoleView.ViewState.DeleteRecord:
                    break;
                case ConsoleView.ViewState.UpdateRecord:

                    break;
                case ConsoleView.ViewState.Quit:
                    _gameView.DisplayExitScreen();
                    _usingApp = false;
                    break;
                default:
                    break;
            }
        }


        public List<Movie> InitializeListOfMovies(List<Movie> MovieList)
        {

            // initialize the list of movies 
            MovieList.Add(new Movie() { MovieTitle = "Star Wars", MovieYear = 1977, MovieCat = Movie.MovieCategory.Action, WouldRecommend = true });
            MovieList.Add(new Movie() { MovieTitle = "The Dark Knight", MovieYear = 2008, MovieCat = Movie.MovieCategory.Action, WouldRecommend = false });
            MovieList.Add(new Movie() { MovieTitle = "Up", MovieYear = 2009, MovieCat = Movie.MovieCategory.Family, WouldRecommend = true });
            MovieList.Add(new Movie() { MovieTitle = "Forest Gump", MovieYear = 1994, MovieCat = Movie.MovieCategory.Comedy, WouldRecommend = true });
            MovieList.Add(new Movie() { MovieTitle = "All About Eve", MovieYear = 1951, MovieCat = Movie.MovieCategory.Drama, WouldRecommend = true });
            MovieList.Add(new Movie() { MovieTitle = "Rear Window", MovieYear = 1954, MovieCat = Movie.MovieCategory.Thriller, WouldRecommend = false });
            MovieList.Add(new Movie() { MovieTitle = "It's a Wonderful Life", MovieYear = 1946, MovieCat = Movie.MovieCategory.Family, WouldRecommend = false });
            MovieList.Add(new Movie() { MovieTitle = "Monty Python and the Holy Grail", MovieYear = 1975, MovieCat = Movie.MovieCategory.Comedy, WouldRecommend = true });
            MovieList.Add(new Movie() { MovieTitle = "Goodfellas", MovieYear = 1990, MovieCat = Movie.MovieCategory.Drama, WouldRecommend = true });
            MovieList.Add(new Movie() { MovieTitle = "Ratatouille", MovieYear = 2007, MovieCat = Movie.MovieCategory.Family, WouldRecommend = true });

            return MovieList;
        }

        public void WriteMoviesToTextFile(List<Movie> MovieList, string dataFile, List<string> MovieStringListWrite)
        {
            string MovieListString;

            // build the list to write to the text file line by line
            foreach (var movie in MovieList)
            {
                //TODO         //  MovieListString = movie.MovieTitle + "," + movie.MovieYear + "," + movie.WouldRecommend;
                //  MovieStringListWrite.Add(MovieListString);
            }

            File.WriteAllLines(dataFile, MovieStringListWrite);

        }

        public List<Movie> ReadMoviesFromTextFile(List<Movie> MovieList, string dataFile)
        {
            const char delineator = ',';

            List<string> MovieStringList = new List<string>();

            // read each line and put it into an array and convert the array to a list
            MovieStringList = File.ReadAllLines(dataFile).ToList();

            foreach (string highScoreString in MovieStringList)
            {
                // use the Split method and the delineator on the array to separate each property into an array of properties
                string[] properties = highScoreString.Split(delineator);

                // TODO   //   MovieList.Add(new Movie() { MovieTitle = properties[0], MovieYear = Convert.ToInt32(properties[1]), MovieCat = properties[2], WouldRecommend =Convert.ToBoolean(properties[3]) });
            }

            return MovieList;
        }
        #endregion
    }
}
