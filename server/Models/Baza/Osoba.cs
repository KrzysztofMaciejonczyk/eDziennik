using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EDziennik.Models.Baza
{
  [Table("osoba", Schema = "public")]
  public partial class Osoba
  {
    [Key]
    public int osoba_id
    {
      get;
      set;
    }


    public ICollection<Rodzic> Rodzics { get; set; }

    public ICollection<Nauczyciel> Nauczyciels { get; set; }

    public ICollection<Uczen> Uczens { get; set; }
    public string imie
    {
      get;
      set;
    }
    public string email
    {
      get;
      set;
    }
    public string nazwisko
    {
      get;
      set;
    }
    public string telefon
    {
      get;
      set;
    }
    public string haslo
    {
      get;
      set;
    }
  }
}
