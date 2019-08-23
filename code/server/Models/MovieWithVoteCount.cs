using System;
using CultureCatchupRanked.Data.Entities;

namespace CultureCatchupRanked.Models
{
    public class MovieWithVoteCount
    {
        public Movie Movie { get; set; }

        public int VoteCount {get; set;}
    }
}