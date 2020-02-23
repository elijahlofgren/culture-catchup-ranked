public class OMDBInfo
{

  public string Title { get; set; } // e.g. "Speed"
  public string Year { get; set; } // e.g. "1994"
  public string Rated { get; set; } // e.g.	"R"
  public string Released { get; set; } // e.g.	"10 Jun 1994"
  public string Runtime { get; set; }   // e.g. "116 min"
  public string Genre { get; set; } // e.g.	"Action, Adventure, Thriller"
  public string Director { get; set; } // e.g.	"Jan de Bont"
  public string Writer { get; set; } // e.g.	"Graham Yost"
  public string Actors { get; set; }  // e.g. "Keanu Reeves, Dennis Hopper, Sandra Bullock, Joe Morton"
  public string Plot { get; set; } // e.g.	"A young police officer must prevent a bomb exploding aboard a city bus by keeping its speed above 50 mph."
  public string Language { get; set; } // e.g.	"English"
  public string Country { get; set; } // e.g. "USA"
  public string Awards { get; set; }  // e.g. "Won 2 Oscars. Another 17 wins & 20 nominations."
  public string Poster { get; set; }
  public string imdbID { get; set; } // e.g. tt0111257
}