using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersistenceCSV_Briggs50.Util
{
   public static class ConsoleUtil
    {
        #region FIELDS

        private static int _windowWidth = 90;
        private static int _windowHeight = 30;

        private static int _windowLeft = 20;
        private static int _windowTop = 20;

        private static ConsoleColor _bodyBackgroundColor = ConsoleColor.DarkCyan;
        private static ConsoleColor _bodyForegroundColor = ConsoleColor.Black;

        #endregion

        #region PROPERTIES

        public static int WindowWidth
        {
            get { return _windowWidth; }
            set { _windowWidth = value; }
        }

        public static int WindowHeight
        {
            get { return _windowHeight; }
            set { _windowHeight = value; }
        }

        public static int WindowLeft
        {
            get { return _windowLeft; }
            set { _windowLeft = value; }
        }

        public static int WindowTop
        {
            get { return _windowTop; }
            set { _windowTop = value; }
        }

        public static ConsoleColor BodyBackgroundColor
        {
            get { return _bodyBackgroundColor = ConsoleColor.DarkCyan; }
            set { _bodyBackgroundColor = value; }
        }

        public static ConsoleColor BodyForegroundColor
        {
            get { return _bodyForegroundColor = ConsoleColor.Black; }
            set { _bodyForegroundColor = value; }
        }

        #endregion


        #region METHODS
        /// <summary>
        /// reset display to default size and colors including the header
        /// </summary>
        public static void DisplayReset()
        {
            Console.SetWindowSize(_windowWidth, _windowHeight);

            Console.Clear();
            Console.ResetColor();

            Console.ForegroundColor = _bodyForegroundColor;
            Console.BackgroundColor = _bodyBackgroundColor;


        }

        /// <summary>
        /// center text as a function of the window width with padding on both sides
        /// Note: the method currently assumes the text will fit on one line
        /// </summary>
        /// <param name="text">text to center</param>
        /// <param name="windowWidth">the width of the window in characters</param>
        /// <returns>string with spaces and centered text</returns>
        public static string Center(string text)
        {
            int leftPadding = (_windowWidth - text.Length) / 2 + text.Length;
            return text.PadLeft(leftPadding).PadRight(_windowWidth);
        }

        #endregion
    }
}
