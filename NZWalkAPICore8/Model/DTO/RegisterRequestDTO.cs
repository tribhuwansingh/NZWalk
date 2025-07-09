using System.ComponentModel.DataAnnotations;

namespace NZWalkAPICore8.Model.DTO
{
    public class RegisterRequestDTO
    {
        [Required]
        [MinLength(10,ErrorMessage ="Minium length should be 10")]
        [MaxLength(50,ErrorMessage ="Maximum length should be 20")]
        [DataType(DataType.EmailAddress)]
        public string UserName { get; set; }

        [Required(ErrorMessage ="Password is required")]
        
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength =5)]
        public string  Password { get; set; }

        [Required]
        public string[] Roles { get; set; }
    }
}
