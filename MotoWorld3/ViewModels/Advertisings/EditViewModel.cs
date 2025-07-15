using Microsoft.AspNetCore.Identity;
using MotoWorld3.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MotoWorld3.ViewModels.Advertisings
{
    public class EditViewModel
    {
        [Required, EmailAddress, DisplayName("Email cím")]
        public string Email { get; set; }

        [Required, Phone, DisplayName("Telefonszám")]
        public string PhoneNumber { get; set; }

        public MotorcycleType MotorcycleType { get; set; }
        public Motorcycle Motorcycle { get; set; }
        public Advertising Advertising { get; set; }
        public List<Picture> Pictures { get; set; }
        public Place Place { get; set; }

        public EditViewModel() { }

        public EditViewModel(string email, string phone, MotorcycleType motorcycleType, Motorcycle motorcycle, Advertising advertising, List<Picture> pictures, Place place)
        {
            Email = email;
            PhoneNumber = phone;
            MotorcycleType = motorcycleType;
            Motorcycle = motorcycle;
            Advertising = advertising;
            Pictures = pictures;
            Place = place;
        }
    }
}
