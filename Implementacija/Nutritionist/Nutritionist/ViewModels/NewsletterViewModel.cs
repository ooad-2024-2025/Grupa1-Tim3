using System.ComponentModel.DataAnnotations;

namespace Nutritionist.ViewModels
{
    public class NewsletterViewModel
    {
        public bool IsSubscribed { get; set; }
    }

    public class NewsletterCreateViewModel
    {
        [Required, StringLength(100)]
        public string Title { get; set; }

        [Required, DataType(DataType.MultilineText)]
        public string Text { get; set; }
    }
}
