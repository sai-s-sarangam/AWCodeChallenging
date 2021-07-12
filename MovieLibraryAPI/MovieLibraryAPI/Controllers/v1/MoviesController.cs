//Sai: 07/11/2021: This Controller is for Movies Library
//It has only Get Method to retrieve respective movies based on the search criteria.
//If no filter option selected then it will return all the values, else it returns based on the filter criteria
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Reflection;
using AppServices.Manager;
using Entities;
using Microsoft.AspNetCore.Mvc;
using MovieLibraryAPI;
using MoviesLibraryAPI.DataTransferObjects;
using MoviesLibraryAPI.Entities;
using MoviesLibraryAPI.Repositories;

namespace MoviesLibraryAPI.API.Controllers.v1
{
    /// <summary>
    /// Movies controller v1
    /// </summary>
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[Controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMoviesManager _MoviesManager = null;
        private readonly IMoviesDbConfig _MoviesDbConfig = null;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="MoviesManager"></param>
        /// <param name="MoviesDbConfig"></param>
        public MoviesController(IMoviesManager MoviesManager, IMoviesDbConfig MoviesDbConfig)
        {
            _MoviesManager = MoviesManager;
            _MoviesDbConfig = MoviesDbConfig;
        }
        /// <summary>
        /// Retrieve
        /// </summary>
        /// <param name="filterOptions">FilterOptions</param>
        /// <returns>Movie</returns>
        // GET: api/Movies/5
        [HttpGet( Name = "Get_Movies_V1")]
        public LibraryDTO Get([Required][FromQuery] FilterOptions filterOptions)
        {  
            try
            {
                var baseDirectory = new Uri(Assembly.GetExecutingAssembly().GetName().CodeBase).LocalPath;
                var directory = Path.GetDirectoryName(baseDirectory);
                string filePath = Path.Combine(directory, "Library.xml");  //We can get the name of the file from appsettings.json, but for time being hard coded this value.

                var MoviesDTO = _MoviesManager.Retrieve(filePath, filterOptions);                

                return MoviesDTO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
