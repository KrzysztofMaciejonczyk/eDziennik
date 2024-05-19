using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EDziennik.Models.Baza
{
  [Table("uwaga", Schema = "public")]
  public partial class Uwaga
  {
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
    public int uwaga_id
    {
      get;
      set;
    }
  }
}
