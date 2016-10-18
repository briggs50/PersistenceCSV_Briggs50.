using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersistenceCSV_Briggs50.Model
{
    class Movie
    {
        #region ENUMS

        public enum MovieCategory
        {
            Drama,
            Thriller,
            Action,
            Comedy,
            Family
        }

        #endregion

        #region FIELDS

        private string movieTitle;

        private int movieYear;

        private bool wouldRecommend;

        private MovieCategory movieCategory;

        #endregion


        #region PROPERTIES

        public string MovieTitle
        {
            get { return movieTitle; }
            set { movieTitle = value; }
        }

        public int MovieYear
        {
            get { return movieYear; }
            set { movieYear = value; }
        }

        public bool WouldRecommend
        {
            get { return wouldRecommend; }
            set { wouldRecommend = value; }
        }

        public MovieCategory MovieCat
        {
            get { return movieCategory; }
            set { movieCategory = value; }
        }

        #endregion

        public Movie()
        {

        }

        public Movie(string movietitle, int movieyear, bool wouldrecommend, MovieCategory moviecategory)
        {
            this.MovieTitle = movietitle;
            this.MovieYear = movieyear;
            this.WouldRecommend = wouldrecommend;
            this.MovieCat = moviecategory;
           
        }
    }
}
