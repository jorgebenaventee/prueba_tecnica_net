using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alicunde.PruebaTecnica.Database.Models;

[Table("retailers")]
public class Retailer
{
    [Key] public string ReCode { get; set; }

    public string ReName { get; set; }

    public string Country { get; set; }

    public string CodingScheme { get; set; }
}