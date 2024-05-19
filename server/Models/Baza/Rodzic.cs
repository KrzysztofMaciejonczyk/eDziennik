using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EDziennik.Models.Baza
{
  [Table("rodzic", Schema = "public")]
  public partial class Rodzic
  {
    public int? osoba_id
    {
      get;
      set;
    }

    public Osoba Osoba { get; set; }
    [Key]
    public int rodzic_id
    {
      get;
      set;
    }


    public ICollection<Uczen> Uczens { get; set; }
  }
}
