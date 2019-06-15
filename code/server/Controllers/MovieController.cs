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

[Route("api/[controller]")]
[ApiController]
public class MovieController : ControllerBase
{


  private IApplicationDbContext _context = null;

  public MovieController(IApplicationDbContext context)
  {
    _context = context;
  }

  [HttpGet]
  public ActionResult<List<Movie>> Get()
  {
    List<Movie> products = _context.Movies.ToList();
    return products;
  }
}