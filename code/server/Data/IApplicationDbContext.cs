using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CultureCatchupRanked.Data.Entities;

public interface IApplicationDbContext {
          DbSet<Movie> Movies { get; set; }    
}