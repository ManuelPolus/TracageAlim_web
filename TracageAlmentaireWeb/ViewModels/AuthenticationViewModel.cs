using System.ComponentModel.DataAnnotations;

namespace TracageAlmentaireWeb.ViewModels
{
    public class AuthenticationViewModel 
    {

        [Required]
        [EmailAddress]
        public string Email { get; set; }


        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}