using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EDziennik.Models.Baza
{
  [Table("klasa", Schema = "public")]
  public partial class Klasa
  {
    public int? nauczyciel_id
    {
      get;
      set;
    }

    public Nauczyciel Nauczyciel { get; set; }
    [Key]
    public int klasa_id
    {
      get;
      set;
    }


    public ICollection<Uczen> Uczens { get; set; }

    public ICollection<Przedmiot> Przedmiots { get; set; }
    public string nazwa
    {
      get;
      set;
    }
  }
}
