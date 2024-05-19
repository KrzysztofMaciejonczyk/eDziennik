using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EDziennik.Models.Baza
{
  [Table("data_opis", Schema = "public")]
  public partial class DataOpi
  {
    [Key]
    public int data_opis_id
    {
      get;
      set;
    }


    public ICollection<Uwaga> Uwagas { get; set; }

    public ICollection<Ocena> Ocenas { get; set; }

    public ICollection<Obecnosc> Obecnoscs { get; set; }
    public DateTime? data
    {
      get;
      set;
    }
    public string opis
    {
      get;
      set;
    }
  }
}
