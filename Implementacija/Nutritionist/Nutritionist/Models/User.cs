using Microsoft.AspNetCore.Identity;

namespace Nutritionist.Models
{
    public class User : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public DateTime Birthday { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
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
