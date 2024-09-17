using System.ComponentModel.DataAnnotations;

namespace Company.Web.Models
{
    public class ForgetPasswordViewModel
    {
        [EmailAddress(ErrorMessage = "Invalid Format For Email")]
        [Required(ErrorMessage = "Email is Required")]
        public string Email { get; set; }
    }
}
