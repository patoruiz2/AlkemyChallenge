using System.ComponentModel.DataAnnotations;

namespace AlkemyChallenge.Model.ViewModel
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="This field is required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string Password { get; set; }
    }
}
