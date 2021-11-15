using System.ComponentModel.DataAnnotations;

namespace AlkemyChallenge.Model.ViewModel
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "This field is required")]
        [MinLength(2, ErrorMessage = "The field must be more than {1} characters")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [MinLength(4, ErrorMessage = "The field must be more than {1} characters")]
        public string Password { get; set; }
    }
}
