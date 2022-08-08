namespace ProjectName.Models
{
  public class ClassTwo
  {
    public ClassTwo()
    {
      this.JoinEntites = new HashSet<ClassOneClassTwo>();
    }

    public int ClassTwoId { get; set; }
    public virtual ICollection<ClassOneClassTwo> JoinEntites { get; set; }
  }
}