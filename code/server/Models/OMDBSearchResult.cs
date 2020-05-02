using System.Collections.Generic;

public class OMDBSearchResult
{

  public List<OMDBInfo> Search {get; set;}
  public string totalResults {get; set; }
  public string Response {get; set;}
}