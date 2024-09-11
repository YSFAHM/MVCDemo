using System.ComponentModel.DataAnnotations;

namespace Company.Web.Models
{
    public class SignUpViewModel
    {
        [Required(ErrorMessage = "First Name Is Required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name Is Required")]
        public string LastName { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Format For Email")]
        public string Email { get; set; }

        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*\W)(?=.{6,})(?=.*([a-zA-Z\d\W])(?!.*\1)).*$",
        ErrorMessage = "Password must be at least 6 characters long, and include at least one digit, one lowercase letter, one uppercase letter, one non-alphanumeric character, and two unique characters.")]
        [Required(ErrorMessage = "Password Is Required")]
        public string Password { get; set; }


        [Compare(nameof(Password), ErrorMessage = "Confirm Password does not match Password")]
        public string ConfirmPassword { get; set; }


        [Required(ErrorMessage = "Required To Agree")]
        public bool IsAgree { get; set; }
    }
}
