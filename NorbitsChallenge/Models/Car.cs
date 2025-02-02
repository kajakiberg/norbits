using System.ComponentModel.DataAnnotations;

namespace NorbitsChallenge.Models
{
    public class Car
    {
        [Required(ErrorMessage = "Registreringsnummer er påkrevd.")]
        [StringLength(10, MinimumLength = 2, ErrorMessage = "Registreringsnummeret må være mellom 2 og 10 tegn.")]
        [RegularExpression(@"^[A-Za-z0-9]+$", ErrorMessage = "Registreringsnummeret kan bare inneholde bokstaver og tall.")]
        public string LicensePlate { get; set; } 
        public string Description { get; set; }  = string.Empty;
        public string Model { get; set; } 
        public string Brand { get; set; }  
        [Range(0, int.MaxValue, ErrorMessage = "Antall dekk må være et positivt tall.")]
        public int TireCount { get; set; }  
        public int CompanyId { get; set; }  
    }
}
