using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System;

namespace PestFinder.Models
{
  public class Pest
  {
    public Pest()
    {
      this.JoinEntities = new HashSet<PestLocation>();
    }

    public int PestId { get; set; }
    public string Type { get; set; }
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyy}")]
    public DateTime Sighting { get; set; }
    public int Quantity { get; set; }
    public bool Alive { get; set; }
    public string Action { get; set; }
    [NotMapped] // excludes the following property from the database
    public string DateType { get { return Type +" "+ Sighting; } }
    
    public virtual ApplicationUser User { get; set; }
    public virtual ICollection<PestLocation> JoinEntities { get; }
  }
}