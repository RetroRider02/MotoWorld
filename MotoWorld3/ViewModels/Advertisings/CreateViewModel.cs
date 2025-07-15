using MotoWorld3.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MotoWorld3.ViewModels.Advertisings
{
    public class CreateViewModel
    {
        [Required, EmailAddress, DisplayName("Email cím")]
        public string Email { get; set; }

        [Required, Phone, DisplayName("Telefonszám")]
        public string PhoneNumber { get; set; }

        public MotorcycleType MotorcycleType { get; set; }
        public Motorcycle Motorcycle { get; set; }
        public Advertising Advertising { get; set; }
        public Place Place { get; set; }
        
        public CreateViewModel() { }
        
        public CreateViewModel(string email, string phoneNumber, MotorcycleType motorcycleType, Motorcycle motorcycle, Advertising advertising, Place place)
        {
            Email = email;
            PhoneNumber = phoneNumber;
            MotorcycleType = motorcycleType;
            Motorcycle = motorcycle;
            Advertising = advertising;
            Place = place;
        }
    }
}
