using System.ComponentModel.DataAnnotations;

namespace daza_store_be.Dtos.Request
{
    public class LoginRequestDto
    {
        private string _email = string.Empty;

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