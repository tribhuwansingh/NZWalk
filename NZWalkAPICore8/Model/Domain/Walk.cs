namespace NZWalkAPICore8.Model.Domain
{
    public class Walk
    {


        public Guid Id { get; set; }
        public required string Name { get; set; }
        public double Length { get; set; }

        public Guid RegionId { get; set; }
        public Guid DifficultyId { get; set; }

        // Navigation Properties
        public required Region Region { get; set; }
        public required Difficulty Difficulty { get; set; }
    }

}
