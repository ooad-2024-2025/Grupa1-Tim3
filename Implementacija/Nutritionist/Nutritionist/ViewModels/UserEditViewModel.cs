using Nutritionist.Models;
using System.ComponentModel.DataAnnotations;

namespace Nutritionist.ViewModels
{
    public class UserEditViewModel
    {
        [Required]
        public Guid Id { get; set; }

        [Display(Name = "Ime"), Required]
        public string FirstName { get; set; }

        [Display(Name = "Prezime"), Required]
        public string LastName { get; set; }

        [Display(Name = "Korisnicko ime"), Required]
        public string UserName { get; set; }

        [Display(Name = "Spol")]
        public Gender? Gender { get; set; }

        [Display(Name = "Datum rođenja"), DataType(DataType.Date)]
        public DateTime? Birthday { get; set; }

        [Display(Name = "E-mail"), Required, EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Telefon"), Phone]
        public string? PhoneNumber { get; set; }

        [Display(Name = "Pretplata na newsletter")]
        public bool IsSubscribedToNewsletter { get; set; }
    }
}
