//Sai: 07/11/2021: Unit Testing to test the data retrieve functionality with Filter options
//This test helps for basic testing.
using MoviesLibraryAPI.DataTransferObjects;
using MoviesLibraryAPI.Repositories;
using System;
using Xunit;
using System.Linq;
using System.IO;
using System.Reflection;
using AppServices.Manager;
using Entities;

namespace MoviesUnitTesting
{
    public class UnitTestDataRetrieve
    {
        private readonly IMoviesManager _MoviesManager = null;
        private readonly IMoviesDbConfig _MoviesDbConfig = null;
        LibraryDTO _dto;

        public UnitTestDataRetrieve()
        {
            _MoviesDbConfig = new MoviesDbConfig();
            _MoviesManager = new MoviesManager(_MoviesDbConfig);
        }
        [Fact]
        public void TestReadData()
        {
            _dto = _MoviesManager.Retrieve(GetXMLFilePath());
            Assert.True(_dto != null, "DTO object should not be null");
            Assert.True(_dto?.Movies != null, "Movies should not be null");
            Assert.True(_dto.Movies.Count > 0, "Expected actualCount to be greater than 0.");
            Assert.True(_dto.Movies.FirstOrDefault().ID > 0, "Values should not be blank in Movies List-ID");
        }

        [Fact]
        public void TestReadAllRecords()
        {
            _dto = _MoviesManager.Retrieve(GetXMLFilePath(), new FilterOptions { FilterBy = FilterType.All });
            Assert.True(_dto.Movies.Count() == 34, "Total Record count is not matching with the expected output");
        }

        [Fact]
        public void TestSearchWithMovieTitle()
        {
            _dto = _MoviesManager.Retrieve(GetXMLFilePath(),new FilterOptions { FilterBy = FilterType.Title, Title = "Kid" });
            Assert.True(_dto.Movies.Count() == 2, "Record count with the Title Name is not matching with the expected output");
        }

        [Fact]
        public void TestSearchWithRatingAbove()
        {
            _dto = _MoviesManager.Retrieve(GetXMLFilePath(), new FilterOptions { FilterBy = FilterType.RatingAbove, Rating = 8 });
            Assert.True(_dto.Movies.Count() == 10, "Record count with the Rating Greater Than is not matching with the expected output");
        }

        [Fact]
        public void TestSearchWithRatingBelow()
        {
            _dto = _MoviesManager.Retrieve(GetXMLFilePath(), new FilterOptions { FilterBy = FilterType.RatingBelow, Rating = 4 });
            Assert.True(_dto.Movies.Count() == 1, "Record count with the Rating Less Than is not matching with the expected output");
        }

        [Fact]
        public void TestSearchMovieYearRange()
        {
            _dto = _MoviesManager.Retrieve(GetXMLFilePath(), new FilterOptions { FilterBy = FilterType.DateRange, FromYear = 1980, ToYear = 1985});
            Assert.True(_dto.Movies.Count() == 16, "Record count with the Movie Year Date Range is not matching with the expected output");
        }

        private string GetXMLFilePath()
        {
            var baseDirectory = new Uri(Assembly.GetExecutingAssembly().GetName().CodeBase).LocalPath;
            var directory = Path.GetDirectoryName(baseDirectory);
            return Path.Combine(directory, "Library.xml");  
        }
    }
}
