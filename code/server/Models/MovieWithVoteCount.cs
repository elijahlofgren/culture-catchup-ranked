using System;
using CultureCatchupRanked.Data.Entities;

namespace CultureCatchupRanked.Models
{
    public class MovieWithVoteCount
    {
      // for easy client-side searching
        public string MovieTitle { get;set; }
        
        public Movie Movie { get; set; }

        public int UpVoteCount {get; set;}

        public int DownVoteCount {get; set;}
        
        public int VoteSum {get; set;}

        public OMDBInfo MovieInfo { get;set;}
        
    }
}