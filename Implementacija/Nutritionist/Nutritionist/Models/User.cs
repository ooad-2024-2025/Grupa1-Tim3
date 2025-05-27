using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Nutritionist.Models
{
    public class User : IdentityUser<Guid>
    {
        [Display(Name = "Korisničko ime")]           
        public override string UserName
        {
            get => base.UserName;
            set => base.UserName = value;
        }

        [Display(Name = "Telefon")]                    
        public override string PhoneNumber
        {
            get => base.PhoneNumber;
            set => base.PhoneNumber = value;
        }
        [Display(Name = "Ime")]
        public string FirstName { get; set; }
        [Display(Name = "Prezime")]
        public string LastName { get; set; }
        [Display(Name = "Spol")]
        public Gender Gender { get; set; }
        [Display(Name = "Datum rodjenja")]
        public DateTime Birthday { get; set; }
        [Display(Name = "Datum kreiranja")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [Display(Name = "Newsletter pretplatnik")]
        public bool IsSubscribedToNewsletter { get; set; } = false;

        public ICollection<Recipe> Recipes { get; set; }
        public ICollection<Favorite> Favorites { get; set; }
        public ICollection<Newsletter> Newsletters { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }

    public enum Gender
    {
        MALE,
        FEMALE,
        OTHER
    }
}
