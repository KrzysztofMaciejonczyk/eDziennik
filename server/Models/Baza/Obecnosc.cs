using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EDziennik.Models.Baza
{
  [Table("obecnosc", Schema = "public")]
  public partial class Obecnosc
  {
    public bool? czy_byl
    {
      get;
      set;
    }
    public int? uczen_id
    {
      get;
      set;
    }

    public Uczen Uczen { get; set; }
    public int? data_opis_id
    {
      get;
      set;
    }

    public DataOpi DataOpi { get; set; }
    [Key]
    public int ocena_id
    {
      get;
      set;
    }
  }
}
