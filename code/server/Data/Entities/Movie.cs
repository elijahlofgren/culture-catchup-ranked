using System;

namespace CultureCatchupRanked.Data.Entities 
{
  public class Movie
  {
    public long Id { get; set; }
    public string Title { get; set; }

    public string imdbID {get; set; }
  }
}