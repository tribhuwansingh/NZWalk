using System.ComponentModel.DataAnnotations;

namespace NZWalkAPICore8.Model.DTO
{
    public class WalkDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public double Length { get; set; }

        //public Guid RegionId { get; set; }
        //public Guid DifficultyId { get; set; }
        public RegionDto Region { get; set; }
        public DiffercultyDto Difficulty { get; set; }
    }
    public class AddWalkDto
    {
        [Required]
        [MinLength(5,ErrorMessage ="Minimum length should be 5")]
        [MaxLength(50, ErrorMessage = "Maximum length should be 50")]
        public string Name { get; set; }
        [Required]
        [Range(1,999999,ErrorMessage ="Value should be in range of 1 to 999999")]
        public double Length { get; set; }
        [Required]
        public Guid RegionId { get; set; }
        [Required]
        public Guid DifficultyId { get; set; }
    }
    public class UpdateWalkDto
    {
        [Required]
        [MinLength(5, ErrorMessage = "Minimum length should be 5")]
        [MaxLength(50, ErrorMessage = "Maximum length should be 50")]
        public string Name { get; set; }
        [Required]
        [Range(1, 999999, ErrorMessage = "Value should be in range of 1 to 999999")]
        public double Length { get; set; }
        [Required]
        public Guid RegionId { get; set; }
        [Required]
        public Guid DifficultyId { get; set; }
    }
}
