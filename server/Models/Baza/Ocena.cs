using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EDziennik.Models.Baza
{
  [Table("ocena", Schema = "public")]
  public partial class Ocena
  {
    public int? ocena_n
    {
      get;
      set;
    }
    public int? nauczyciel_id
    {
      get;
      set;
    }

    public Nauczyciel Nauczyciel { get; set; }
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
