
using System.ComponentModel.DataAnnotations;

namespace daza_store_be.Dtos.Request
{
    public class SignupRequestDto
    {   
        private string _firstname = string.Empty;
        private string _lastname = string.Empty;
        private string _email = string.Empty;

        [Required]
        public string FirstName
        {
          get => _firstname;
          set => _firstname = value?.Trim() ?? string.Empty;  
        } 

        [Required]
        public string LastName
        {
            get => _lastname;
            set => _lastname = value?.Trim() ?? string.Empty;
        }

        [Required]
        [EmailAddress]
        public string Email
        {
            get => _email;
            set => _email = value?.Trim() ?? string.Empty;
        } 

        [Required]
        public string Password { get; set; } = string.Empty;

    }
}