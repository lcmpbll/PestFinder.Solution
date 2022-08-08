namespace ProjectName.Models
{
  public class ClassOne
  {
    public ClassOne()
    {
      this.JoinEntities = new HashSet<ClassOneClassTwo>();
    }
    public int ClassOneId { get; set; }
   
    public virtual ICollerction<ClassOneClassTwo> JoinEntites { get; }
  }
}