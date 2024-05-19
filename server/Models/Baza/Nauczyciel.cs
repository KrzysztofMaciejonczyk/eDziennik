using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EDziennik.Models.Baza
{
  [Table("nauczyciel", Schema = "public")]
  public partial class Nauczyciel
  {
    public int? osoba_id
    {
      get;
      set;
    }

    public Osoba Osoba { get; set; }
    [Key]
    public int nauczyciel_id
    {
      get;
      set;
    }


    public ICollection<Klasa> Klasas { get; set; }

    public ICollection<Przedmiot> Przedmiots { get; set; }

    public ICollection<Uwaga> Uwagas { get; set; }

    public ICollection<Ocena> Ocenas { get; set; }
  }
}
