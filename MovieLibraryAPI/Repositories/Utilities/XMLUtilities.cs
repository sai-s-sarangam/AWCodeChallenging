//Sai: 07/11/2021: Utilities are common methods which can be used across the project when needed with out instantiating the class. 
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace MoviesLibraryAPI.Repositories
{
    public static class XMLUtilities
    {
        //To deserialize the XML data to the class objects
        public static T DeserializeToObject<T>(string filepath) where T : class
        {
            System.Xml.Serialization.XmlSerializer ser = new System.Xml.Serialization.XmlSerializer(typeof(T));

            using (StreamReader sr = new StreamReader(filepath))
            {
                return (T)ser.Deserialize(sr);
            }
        }
    }
}

