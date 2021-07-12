using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;
using CodingChallenge.DataAccess.Interfaces;
using CodingChallenge.DataAccess.Models;
using CodingChallenge.Utilities;


namespace CodingChallenge.DataAccess
{
    public class LibraryService : ILibraryService
    {
        public LibraryService() { }

        private IEnumerable<Movie> GetMovies()
        {
            return _movies ?? (_movies = ConfigurationManager.AppSettings["LibraryPath"].FromFileInExecutingDirectory().DeserializeFromXml<Library>().Movies);

        }

        //Sai: 07/08/2021: The below function returns only unique values based on the Title. 
        //We can change the combination if required.
        private IEnumerable<Movie> GetUniqueMovies()
        {
            IEnumerable<Movie> uniqueMovies = GetMovies()
                                           .GroupBy(o =>  o.Title)
                                           .Select(o => o.FirstOrDefault());

            return uniqueMovies;
        }

        private IEnumerable<Movie> _movies { get; set; }
        

        public int SearchMoviesCount(string title)
        {
            return SearchMovies(title).Count();
        }

        public IEnumerable<Movie> SearchMovies(string title, int? skip = null, int? take = null, string sortColumn = null, SortDirection sortDirection = SortDirection.Ascending)
        {
            //Sai: 07/08/2021: Replaced GetMovies with GetUniqueMovies for displaying unique movies
            var movies = GetUniqueMovies().Where(s => s.Title.Contains(title));

            bool Ascending = sortDirection == SortDirection.Ascending ? true : false;

            if (skip.HasValue && take.HasValue)
            {
                movies = movies.Skip(skip.Value).Take(take.Value);
            }            

            //Sai: 07/08/2021: Check if Sort Column as a value, then call the OrderBy function to sort based on the respective column.
            if (!string.IsNullOrEmpty(sortColumn))
            {
                //I see that we have issues with case sensitive and spaces while sorting title. ignore case option is not working. 
                //so removing spaces and change the case to upper for sorting
                if (sortColumn.Equals("Title"))
                {
                    movies = Ascending ?
                        from row in movies
                        orderby Regex.Replace(row.Title, @"(?i)^a\s|^an\s|^the\s|\s+", "").ToUpper()
                        select row
                        :
                        from row in movies
                        orderby Regex.Replace(row.Title, @"(?i)^a\s|^an\s|^the\s|\s+", "").ToUpper() descending
                        select row;
                }
                else
                { 
                    movies = LinqUtilities.OrderByField(movies, sortColumn, (sortDirection == SortDirection.Ascending ? true : false));
                }
            }

            return movies.ToList();
        }          
    }
}
