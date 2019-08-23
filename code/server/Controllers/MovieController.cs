using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CultureCatchupRanked.Models;
using CultureCatchupRanked.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CultureCatchupRanked.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using CultureCatchupRanked.Data;
using Microsoft.Extensions.Logging;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class MovieController : ControllerBase
{


  private readonly ApplicationDbContext _context;
  private readonly UserManager<IdentityUser> _userManager;

  private readonly ILogger _logger;

  public MovieController(
    ILogger<MovieController> logger,
    UserManager<IdentityUser> userManager,
    ApplicationDbContext context)
  {
    _logger = logger;
    _userManager = userManager;
    _context = context;
  }

  [HttpGet]
  public ActionResult<List<Movie>> Get()
  {
    List<Movie> movies = _context.Movies.ToList();
    return movies;
  }

    [HttpGet("GetListWithVotes")]
  public ActionResult<List<MovieWithVoteCount>> GetListWithVotes()
  {
    List<Vote> votes = _context.Votes.ToList();
    List<Movie> movies = _context.Movies.ToList();

    List<MovieWithVoteCount> resultList = new List<MovieWithVoteCount>();

    foreach(Movie movie in movies) {

      var upVoteCount = votes.Where(x => x.MovieId.Equals(movie.Id) && x.UpVote.Equals(true)).ToList().Count();
      var downVoteCount = votes.Where(x => x.MovieId.Equals(movie.Id) && x.DownVote.Equals(true)).ToList().Count();
      var sum = upVoteCount - downVoteCount;

      MovieWithVoteCount movieWithVoteCount = new MovieWithVoteCount {
        Movie = movie,
        DownVoteCount = downVoteCount,
        UpVoteCount = upVoteCount,
        VoteSum = sum
      };

      resultList.Add(movieWithVoteCount);
    } 

    return resultList.OrderByDescending(x => x.VoteSum).ToList();
  }


  [HttpPost("UpVote/{movieId}")]
  public async Task<ActionResult> UpVote(int movieId)
  {
    var user = await _userManager.GetUserAsync(HttpContext.User);
    _logger.LogDebug("user id: " + user.Id);
    Movie movie = _context.Movies.Where(x => x.Id.Equals(movieId)).FirstOrDefault();

    // See if user has already submitted a vote for this movie, if so we need 
    // to change their vote rather than adding another vote record
    Vote existingVote = _context.Votes.Where(x => x.MovieId.Equals(movie.Id) && x.UserId.Equals(user.Id)).FirstOrDefault();

    if (existingVote != null) {
      existingVote.DownVote = false;
      existingVote.UpVote = true;
      _context.SaveChanges();
    } else {
      Vote vote = new Vote
      {
        User = user,
        Movie = movie,
        UpVote = true,
        DownVote = false
      };
      _context.Add(vote);
      _context.SaveChanges();
    }
    return Ok("Upvoted");
  }

  [HttpPost("DownVote/{movieId}")]
  public async Task<ActionResult> DownVote(int movieId)
  {
    var user = await _userManager.GetUserAsync(HttpContext.User);
    _logger.LogDebug("user id: " + user.Id);
    Movie movie = _context.Movies.Where(x => x.Id.Equals(movieId)).FirstOrDefault();


    // See if user has already submitted a vote for this movie, if so we need 
    // to change their vote rather than adding another vote record
    Vote existingVote = _context.Votes.Where(x => x.MovieId.Equals(movie.Id) && x.UserId.Equals(user.Id)).FirstOrDefault();

    if (existingVote != null) {
      existingVote.DownVote = true;
      existingVote.UpVote = false;
      _context.SaveChanges();
    } else {
      Vote vote = new Vote
      {
        User = user,
        Movie = movie,
        UpVote = false,
        DownVote = true
      };
      _context.Add(vote);
      _context.SaveChanges();
    }
    return Ok("Downvoted");
  }
}