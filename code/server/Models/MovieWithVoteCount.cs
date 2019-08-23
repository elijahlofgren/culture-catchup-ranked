using System;
using CultureCatchupRanked.Data.Entities;

namespace CultureCatchupRanked.Models
{
    public class MovieWithVoteCount
    {
        public Movie Movie { get; set; }

        public int UpVoteCount {get; set;}

        public int DownVoteCount {get; set;}
        
        public int VoteSum {get; set;}
        
    }
}