using MoviesLibraryAPI.Entities;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace MoviesLibraryAPI.DataTransferObjects
{
    [XmlRoot(ElementName = "Library")]
    public class LibraryDTO
    {
        [XmlArrayItemAttribute("Movie")]
        public List<Movie> Movies { get; set; }

        public string ValidationMessage { get; set; }
    }
}
