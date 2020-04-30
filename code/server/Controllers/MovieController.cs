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
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class MovieController : ControllerBase
{


    private readonly ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IConfiguration _configuration;
    private readonly ILogger _logger;
    private static readonly HttpClient client = new HttpClient();

    public MovieController(
      ILogger<MovieController> logger,
      UserManager<IdentityUser> userManager,
      ApplicationDbContext context,
      IConfiguration configuration)
    {
        _logger = logger;
        _userManager = userManager;
        _context = context;
        _configuration = configuration;
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

        foreach (Movie movie in movies)
        {

            var upVoteCount = votes.Where(x => x.MovieId.Equals(movie.Id) && x.UpVote.Equals(true)).ToList().Count();
            var downVoteCount = votes.Where(x => x.MovieId.Equals(movie.Id) && x.DownVote.Equals(true)).ToList().Count();
            var sum = upVoteCount - downVoteCount;

            MovieWithVoteCount movieWithVoteCount = new MovieWithVoteCount
            {
                MovieTitle = movie.Title,
                Movie = movie,
                DownVoteCount = downVoteCount,
                UpVoteCount = upVoteCount,
                VoteSum = sum
            };

            resultList.Add(movieWithVoteCount);
        }

        return resultList.OrderByDescending(x => x.VoteSum).ToList();
    }

    [HttpGet("GetListWithVotesAndMovieInfo")]
    public async Task<ActionResult<List<MovieWithVoteCount>>> GetListWithVotesAndMovieInfo()
    {
        var user = await _userManager.GetUserAsync(HttpContext.User);

        List<Vote> votes = _context.Votes.ToList();
        List<Movie> movies = _context.Movies.ToList();

        List<MovieWithVoteCount> resultList = new List<MovieWithVoteCount>();

        foreach (Movie movie in movies)
        {
            bool currentUserUpVoted = false;
            bool currentUserDownVoted = false;

            var myVoteForThisMovie = votes.Where(x => x.MovieId.Equals(movie.Id) &&  x.UserId.Equals(user.Id)).ToList().FirstOrDefault();
            if (myVoteForThisMovie != null)
            {
                if (myVoteForThisMovie.UpVote)
                {
                    currentUserUpVoted = true;
                }
                if (myVoteForThisMovie.DownVote)
                {
                    currentUserDownVoted = true;
                }
            }
            var upVoteCount = votes.Where(x => x.MovieId.Equals(movie.Id) && x.UpVote.Equals(true)).ToList().Count();
            var downVoteCount = votes.Where(x => x.MovieId.Equals(movie.Id) && x.DownVote.Equals(true)).ToList().Count();
            var sum = upVoteCount - downVoteCount;
            OMDBInfo movieInfo = await GetMovieInfo(movie.Title);

            MovieWithVoteCount movieWithVoteCount = new MovieWithVoteCount
            {
                CurrentUserUpVoted = currentUserUpVoted,
                CurrentUserDownVoted = currentUserDownVoted,
                MovieTitle = movie.Title,
                Movie = movie,
                DownVoteCount = downVoteCount,
                UpVoteCount = upVoteCount,
                VoteSum = sum,
                MovieInfo = movieInfo
            };

            resultList.Add(movieWithVoteCount);
        }

        return resultList.OrderByDescending(x => x.VoteSum).ToList();
    }

    /*[HttpGet("GetMyUpVotes")]
    public async Task<ActionResult<List<MovieWithVoteCount>>> GetMyUpVotes()
    {
      var user = await _userManager.GetUserAsync(HttpContext.User);

      List<Vote> votes = _context.Votes.ToList();
      List<Movie> movies = _context.Movies.ToList();

      List<MovieWithVoteCount> resultList = new List<MovieWithVoteCount>();

      foreach (Movie movie in movies)
      {

        var upVotes = votes.Where(x => x.MovieId.Equals(movie.Id) && x.UpVote.Equals(true)).ToList();
        var upVoteCount = upVotes.Count();
        var downVoteCount = votes.Where(x => x.MovieId.Equals(movie.Id) && x.DownVote.Equals(true)).ToList().Count();
        var sum = upVoteCount - downVoteCount;


        }
        // Only include movie and fetch info about it if user has upvoted it
        if (upVotes.Find(x => x.UserId.Equals(user.Id)) != null)
        {
          OMDBInfo movieInfo = await GetMovieInfo(movie.Title);
          MovieWithVoteCount movieWithVoteCount = new MovieWithVoteCount
          {
            //CurrentUserUpVoted = currentUserUpVoted,
            //CurrentUserDownVoted = currentUserDownVoted,
            MovieTitle = movie.Title,
            Movie = movie,
            DownVoteCount = downVoteCount,
            UpVoteCount = upVoteCount,
            VoteSum = sum,
            MovieInfo = movieInfo
          };
          resultList.Add(movieWithVoteCount);
        }
      }

      return resultList.OrderByDescending(x => x.VoteSum).ToList();
    }
  */
    private async Task<OMDBInfo> GetMovieInfo(string movieTitle)
    {
        OMDBInfo result = new OMDBInfo();
        try
        {
            // Fetch movie info using http://www.omdbapi.com (patreon subscription required to get api key
            // which is required for poster images)
            // Example http://www.omdbapi.com/?t=Speed&apikey=YOUR_API_KEY_HERE
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("User-Agent", "CultureCatchup.fun");
            var apiUrl = "http://www.omdbapi.com/?t=" + movieTitle + "&apikey=" + _configuration["ApiKeys:OMDBApiKey"];
            string movieInfoString = await client.GetStringAsync(apiUrl);
            result = JsonConvert.DeserializeObject<OMDBInfo>(movieInfoString);
        }
        catch (Exception e)
        {
            _logger.LogError("Error using OMBD API", e);
            _logger.LogError(e.ToString());
        }
        return result;
    }

    [HttpPost("Add")]
    public async Task<ActionResult> Add([FromBody] Movie movie)
    {

        var user = await _userManager.GetUserAsync(HttpContext.User);
        _logger.LogDebug("Movie Add: user id: " + user.Id);

        // TODO: Log name of user adding movie/
        Movie newEntity = new Movie
        {
            Title = movie.Title
        };
        _context.Add(newEntity);
        _context.SaveChanges();
        return Ok("Movie added");

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

        if (existingVote != null)
        {
            existingVote.DownVote = false;
            existingVote.UpVote = true;
            _context.SaveChanges();
        }
        else
        {
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

        if (existingVote != null)
        {
            existingVote.DownVote = true;
            existingVote.UpVote = false;
            _context.SaveChanges();
        }
        else
        {
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