using System.Collections;
using System.Linq;
using System.Web.Mvc;
using CodingChallenge.DataAccess;
using CodingChallenge.DataAccess.Interfaces;
using CodingChallenge.UI.Models;

namespace CodingChallenge.UI.Controllers
{
    public class DefaultController : Controller
    {
        public ILibraryService LibraryService { get; private set; }        

        public DefaultController() { }

        public DefaultController(ILibraryService libraryService)
        {
            LibraryService = libraryService;
        }
        
        public ActionResult Index([ModelBinder(typeof(GridBinder))]GridOptions options)
        {
            options.TotalItems = LibraryService.SearchMoviesCount("");
            if (options.SortColumn == null)
                options.SortColumn = "ID";
            var model = new MovieListViewModel
            {
                GridOptions = options,
                Movies = LibraryService.SearchMovies("", (options.Page - 1) * options.ItemsPerPage, options.ItemsPerPage,options.SortColumn,options.SortDirection).ToList() //Sai: Sending SortColumn, SortDirection values
            };
            return View(model);
        }

        //Sai: 07/10/2021: Added this action for retrieving data to display in Angular UI.
        //As we are migrating project from asp.net/mvc we can develop UI screens in angular and can still communicate with mvc controllers to get/save data. When API's are available then we can redirect the call from angular 
        [AllowCrossSiteAttribute]
        public ActionResult GetData()
        {
            var Movies = LibraryService.SearchMovies("");
            return Json(Movies, JsonRequestBehavior.AllowGet); 
        }
    }
}
