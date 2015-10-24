using System.ComponentModel.DataAnnotations;

namespace EWG.Frontend.ViewModels
{
    public class UserInputModel
    {
        public static string DefaultUsername = "noname";

        private const string EmailRegex = @"^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@
(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+(?:[A-Z]{2}|com|org|net|edu|gov|mil|
biz|info|mobi|name|aero|asia|jobs|museum)\b$";

        [Required]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Your password must be at least {2} characters long")]
        public string Password { get; set; }

        [Required]
        //[RegularExpression(EmailRegex, ErrorMessage = "Invalid email.")]
        public string Email { get; set; }

        public string ReturnUrl { get; set; }
    }
}