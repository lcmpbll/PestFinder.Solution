namespace PestFinder.Models
{
  public class PestLocation
  {
    public int PestLocationId { get; set; }
    public int PestId { get; set; }
    public int LocationId { get; set; }
    public virtual Pest Pest { get; set; }
    public virtual Location Location { get; set; }

    public virtual ApplicationUser User { get; set; }
  }
}