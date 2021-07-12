//Sai: 07/11/2021: All the properities / fields of Movies
using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace MoviesLibraryAPI.Entities
{
    /// <summary>
    /// Entity for Movies 
    /// </summary>
    [XmlRoot(ElementName = "Movie")]
    public class Movie
    {
        /// <summary>
        /// ID
        /// </summary>
        [XmlElement(ElementName = "ID")]
        public int ID;
        /// <summary>
        /// Title
        /// </summary>
        [XmlElement(ElementName = "Title")]
        public string Title;
        /// <summary>
        /// Year
        /// </summary>
        [XmlElement(ElementName = "Year")]
        public int Year;
        /// <summary>
        /// Rating
        /// </summary>
        [XmlElement(ElementName = "Rating")]
        public decimal Rating;
    }
}
