using System.ComponentModel.DataAnnotations;

namespace NZWalkAPICore8.Model.DTO
{
    public class RegionDto
    {
        public Guid Id { get; set; }
        public  string Code { get; set; }
        public  string Name { get; set; }
        public double Area { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
        public long Population { get; set; }
    }

    public class AddRegionDto
    {
        [Required]
        [MinLength(6,ErrorMessage ="Minimum length should be 6")]
        [MaxLength(20, ErrorMessage = "Maximum length should be 20")]
        public  string Code { get; set; }
        [Required]
        [MinLength(10, ErrorMessage = "Minimum length should be 10")]
        [MaxLength(100, ErrorMessage = "Maximum length should be 100")]
        public  string Name { get; set; }
        [Required]
        [Range(1,999999,ErrorMessage ="Value should be in range of 1 to 999999")]
        public double Area { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
        [Required]
        [Range(1, 999999, ErrorMessage = "Value should be in range of 1 to 999999")]
        public long Population { get; set; }
    }
    public class UpdateRegionDto
    {
        [Required]
        [MinLength(6, ErrorMessage = "Minimum length should be 6")]
        [MaxLength(20, ErrorMessage = "Maximum length should be 20")]
        public string Code { get; set; }
        [Required]
        [MinLength(10, ErrorMessage = "Minimum length should be 10")]
        [MaxLength(100, ErrorMessage = "Maximum length should be 100")]
        public  string Name { get; set; }
        [Required]
        [Range(1, 999999, ErrorMessage = "Value should be in range of 1 to 999999")]
        public double Area { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
        [Required]
        [Range(1, 999999, ErrorMessage = "Value should be in range of 1 to 999999")]
        public long Population { get; set; }
    }

}
