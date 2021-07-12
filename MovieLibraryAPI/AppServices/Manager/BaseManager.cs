//Sai: 07/11/2021: This interface is for implimenting the retrieve/save functionality for the respective controller managers.
using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppServices.Manager
{
    public interface IBaseManager<TDTO>
        where TDTO : class 
    {   
        TDTO Retrieve(string xmlfilePath);

        TDTO Retrieve(string xmlfilePath, FilterOptions filterOptions);
    }
}
