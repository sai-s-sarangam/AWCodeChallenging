//Sai: 07/11/2021: This class is for calling Repository Class to retrieve the data. 
//Controller will call Manager, Manager will call Repository.
//Repository class will be communicating with the database. 
//So the Controller is not going to communicate with the database directly.
using Entities;
using MoviesLibraryAPI.DataTransferObjects;
using MoviesLibraryAPI.Entities;
using MoviesLibraryAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppServices.Manager
{
    public interface IMoviesManager : IBaseManager<LibraryDTO> { }

    public class MoviesManager : IMoviesManager
    {
        IMoviesDbConfig iMoviesDbConfig;

        public MoviesManager (IMoviesDbConfig _IMoviesDbConfig)
        {
            iMoviesDbConfig = _IMoviesDbConfig;
        }
        public LibraryDTO Retrieve(string xmlfilePath)
        {
            return iMoviesDbConfig.ExecuteAsync(xmlfilePath);
        }

        public LibraryDTO Retrieve(string xmlfilePath, FilterOptions filterOptions)
        {
            var MoviesDTO = iMoviesDbConfig.ExecuteAsync(xmlfilePath);

            List<Movie> Movies = MoviesDTO?.Movies;

            switch (filterOptions.FilterBy)
            {
                case FilterType.Title:
                    if (String.IsNullOrEmpty(filterOptions.Title)) { break; }                    
                    Movies = Movies.Where(w => w.Title.Contains(filterOptions.Title)).OrderBy(o => o.Title).ToList();
                    break;
                case FilterType.RatingAbove:
                    Movies = Movies.Where(w => w.Rating > filterOptions.Rating).OrderBy(o => o.Rating).ToList();
                    break;
                case FilterType.RatingBelow:
                    Movies = Movies.Where(w => w.Rating < filterOptions.Rating).OrderBy(o => o.Rating).ToList();
                    break;
                case FilterType.DateRange:
                    Movies = Movies.Where(w => w.Year >= filterOptions.FromYear && w.Year <= filterOptions.ToYear).OrderBy(o => o.Year).ToList();
                    break;
                default:
                    break;
            }

            MoviesDTO.Movies = Movies;

            if (Movies == null ||  Movies.Count == 0)
            {
                MoviesDTO.ValidationMessage = Constants.ValidationMessage;
            }            

            return MoviesDTO;
        }
    }
}
