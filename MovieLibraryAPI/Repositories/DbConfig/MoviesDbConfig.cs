//Sai: 07/11/2021: This class is for communicating with database / data file. I am using xml file to fetch the data same like as ASP.NET MVC project.
using MoviesLibraryAPI.DataTransferObjects;
using MoviesLibraryAPI.Entities;
using System;
using System.Collections.Generic;

namespace MoviesLibraryAPI.Repositories
{
    public class MoviesDbConfig : IMoviesDbConfig
    {
        public LibraryDTO ExecuteAsync(string xmlfilePath)
        {   
            try
            {
                return XMLUtilities.DeserializeToObject<LibraryDTO>(xmlfilePath);
            }
            catch (Exception ex)
            {
                throw ex;
                //We can log the errors here
            }
            
        }
    }
}
