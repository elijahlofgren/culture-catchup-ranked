using System;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace CultureCatchupRanked.Data.Entities
{
  public class Vote
  {
    public long Id { get; set; }
    public bool UpVote { get; set; }

    public bool DownVote { get; set; }

    public long MovieId {get;set;}

    [ForeignKey("MovieId")]
    public Movie Movie {get; set;}

    public string UserId {get; set;}

    [ForeignKey("UserId")]
    public IdentityUser User {get;set;}
  }
}