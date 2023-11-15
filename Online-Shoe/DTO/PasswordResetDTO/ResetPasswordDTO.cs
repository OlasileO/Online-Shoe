using System.ComponentModel.DataAnnotations;

namespace Online_Shoe.DTO.PasswordResetDTO
{
    public class ResetPasswordDTO
    {
        
        public required string EmailAdrress { get; set; }

        public required string Password { get; set; }

        public required string ConfirmPassword { get; set; }
    }
}
