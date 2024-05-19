using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EDziennik.Models.Baza
{
  [Table("uczen", Schema = "public")]
  public partial class Uczen
  {
    public DateTime? data_urodzenia
    {
      get;
      set;
    }
    public int? osoba_id
    {
      get;
      set;
    }

    public Osoba Osoba { get; set; }
    public int? rodzic_id
    {
      get;
      set;
    }

    public Rodzic Rodzic { get; set; }
    public int? klasa_id
    {
      get;
      set;
    }

    public Klasa Klasa { get; set; }
    [Key]
    public int uczen_id
    {
      get;
      set;
    }


    public ICollection<Uwaga> Uwagas { get; set; }

    public ICollection<Ocena> Ocenas { get; set; }

    public ICollection<Obecnosc> Obecnoscs { get; set; }
    public string adres
    {
      get;
      set;
    }
  }
}
