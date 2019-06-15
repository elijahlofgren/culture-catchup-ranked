using System;

namespace CultureCatchupRanked.Data.Entities
{
  public class Vote
  {
    public long Id { get; set; }
    public bool UpVote { get; set; }

    public bool DownVote { get; set; }

    public string UserId {get; set;}
  }
}