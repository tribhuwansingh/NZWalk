namespace NZWalkAPICore8.Model.Domain
{
    
    public class Region
    {
        public Guid Id { get; set; }
        public required string  Code { get; set; }
        public required string Name { get; set; }
        public double Area { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
        public long Population { get; set; }
        
    }

}
