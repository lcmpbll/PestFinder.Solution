namespace PestFinder.Models
{
  public class Location
  {
    public Location()
    {
      this.JoinEntites = new HashSet<PestLocation>();
    }

    public int LocationId { get; set; }
    public virtual ICollection<PestLocation> JoinEntites { get; set; }
  }
}