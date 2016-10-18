using PersistenceCSV_Briggs50.View;
using System;
using System.Collections.Generic;
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

        #endregion
    }
}
