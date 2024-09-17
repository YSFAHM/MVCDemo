using System.ComponentModel.DataAnnotations;

namespace Company.Web.Models
{
    public class ResetPasswordViewModel
    {

        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*\W)(?=.{6,})(?=.*([a-zA-Z\d\W])(?!.*\1)).*$",
        ErrorMessage = "Password must be at least 6 characters long, and include at least one digit, one lowercase letter, one uppercase letter, one non-alphanumeric character, and two unique characters.")]
        [Required(ErrorMessage = "Password Is Required")]
        public string Password { get; set; }


        [Compare(nameof(Password), ErrorMessage = "Confirm Password does not match Password")]
        public string ConfirmPassword { get; set; }

        public string Email { get; set; }
        public string Token { get; set; }
    }
}
