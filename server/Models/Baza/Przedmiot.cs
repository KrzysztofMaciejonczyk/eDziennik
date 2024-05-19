using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EDziennik.Models.Baza
{
  [Table("przedmiot", Schema = "public")]
  public partial class Przedmiot
  {
    public int? nauczyciel_id
    {
      get;
      set;
    }

    public Nauczyciel Nauczyciel { get; set; }
    public int? klasa_id
    {
      get;
      set;
    }

    public Klasa Klasa { get; set; }
    [Key]
    public int przedmiot_id
    {
      get;
      set;
    }
    public string nazwa
    {
      get;
      set;
    }
  }
}
