namespace PestFinder.Models
{
  public class Pest
  {
    public Pest()
    {
      this.JoinEntities = new HashSet<PestClassTwo>();
    }
    public int PestId { get; set; }
   
    public virtual ICollerction<PestClassTwo> JoinEntites { get; }
  }
}