using System.Collections.Generic;

namespace PestFinder.Models
{
  public class Location
  {
    public Location()
    {
      this.JoinEntites = new HashSet<PestLocation>();
    }
    
    public string PestRating { get; set; }
    public string Name { get; set; }
    public int LocationId { get; set; }
    public string ActionPlan { get; set; }
    public virtual ApplicationUser User { get; set; }
    public virtual ICollection<PestLocation> JoinEntites { get; set; }
  }
}