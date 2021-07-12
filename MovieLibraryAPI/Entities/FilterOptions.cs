using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities
{
    /// <summary>
    /// 
    /// </summary>
    public enum FilterType
    {
        /// <summary>
        /// Title
        /// </summary>
        Title = 1,
        /// <summary>
        /// RatingAbove
        /// </summary>
        RatingAbove = 2,
        /// <summary>
        /// RatingBelow
        /// </summary>
        RatingBelow = 3,
        /// <summary>
        /// DateRange
        /// </summary>
        DateRange = 4,
        /// <summary>
        /// Franchise
        /// </summary>
        //Franchise = 5, : Not implimented as i couldnt get chance to impliment the other change
        /// <summary>
        /// All
        /// </summary>
        All = 6
    }
    /// <summary>
    /// 
    /// </summary>
    public class FilterOptions
    {
        /// <summary>
        /// FilterBy : Title = 1; RatingAbove = 2; RatingBelow = 3; DateRange = 4; Franchise = 5; All = 6
        /// </summary>      
        [Required]
        public FilterType FilterBy { get; set; }
        /// <summary>
        /// Title
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Rating
        /// </summary>
        public decimal Rating { get; set; }
        /// <summary>
        /// From Review Year 
        /// </summary>
        public int FromYear { get; set; }
        /// <summary>
        /// To Review Year
        /// </summary>
        public int ToYear { get; set; }
    }
}
