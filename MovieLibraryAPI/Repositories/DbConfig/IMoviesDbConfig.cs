//Sai: 07/11/2021:  This interface is for implimenting the database communication functionality for the respective controllers.
using MoviesLibraryAPI.DataTransferObjects;
using System;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MoviesLibraryAPI.Repositories
{
    public interface IDbConfig<TBaseDTO> where TBaseDTO : class
    {
        TBaseDTO ExecuteAsync(string xmlfilePath);
    }

    public interface IMoviesDbConfig : IDbConfig<LibraryDTO>
    {        
    }
}

