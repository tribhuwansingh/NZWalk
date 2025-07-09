using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using NZWalkAPICore8.Model.Domain;
namespace NZWalkAPICore8.Data
{
    public class NZWalkDBContext :DbContext
    {
        public NZWalkDBContext(DbContextOptions<NZWalkDBContext> options ) :base(options)
        {

        }
        public DbSet<Walk> Walks { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Difficulty> Difficulty { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Seed the data for Differculty
            var differculties = new List<Difficulty>()
            {
                new Difficulty()
                {
                    Id = Guid.Parse("74ba7c86-388e-443e-9524-ffa05c9c6e5c"),
                    Code = "Easy"
                },
                 new Difficulty()
                {
                    Id = Guid.Parse("d5cf834c-bd84-4d87-bef9-061a860a8c0d"),
                    Code = "Medium"
                },
                  new Difficulty()
                {
                    Id = Guid.Parse("e7270e7a-e944-4897-9e6a-de937d8a6087"),
                    Code = "Hard"
                }

            };
            //Seed the differculties data in Database
            modelBuilder.Entity<Difficulty>().HasData(differculties);

            //Seed the data for Region
            var regions = new List<Region>()
            {
                new Region()
                {
                    Id = Guid.Parse("f7248fc3-2585-4efb-8d1d-1c555f4087f6"),
                    Code = "AKL",
                    Name="Auckland",
                    Area=8790,
                    Lat=342,
                    Long=3434,
                    Population=4545
                },
                new Region()
                {
                    Id = Guid.Parse("6884f7d7-ad1f-4101-8df3-7a6fa7387d81"),
                    Name = "Northland",
                    Code = "NTL",
                    Area=6512,
                    Lat=2323,
                    Long=3434,
                    Population=4545
                },
                new Region()
                {
                     Id = Guid.Parse("14ceba71-4b51-4777-9b17-46602cf66153"),
                    Name = "Bay Of Plenty",
                    Code = "BOP",
                    Area=6543,
                    Lat=2323,
                    Long=3434,
                    Population=4545
                },
                new Region()
                {
                     Id = Guid.Parse("cfa06ed2-bf65-4b65-93ed-c9d286ddb0de"),
                    Name = "Wellington",
                    Code = "WGN",
                    Area=2314,
                    Lat=2323,
                    Long=3434,
                    Population=4545
                },
                new Region()
                {
                    Id = Guid.Parse("906cb139-415a-4bbb-a174-1a1faf9fb1f6"),
                    Name = "Nelson",
                    Code = "NSN",
                    Area=6589,
                    Lat=2323,
                    Long=3434,
                    Population=4511
                },
                new Region()
                {
                      Id = Guid.Parse("f077a22e-4248-4bf6-b564-c7cf4e250263"),
                    Name = "Southland",
                    Code = "STL",
                    Area=7845,
                    Lat=2323,
                    Long=3434,
                    Population=78000
                },
                new Region()
                {
                    Id = Guid.Parse("761a465b-e718-4b45-81d1-ed17ea9b3183"),
                    Code = "WEST-DEL",
                    Name="WEST DELHI",
                    Area=1212,
                    Lat=2323,
                    Long=3434,
                    Population=360000
                },
                new Region()
                {
                    Id = Guid.Parse("6ae537d8-5725-427c-9c8c-3406feb70332"),
                    Code = "SOUTH-DEL",
                    Name="SOUTH DELHI",
                    Area=1212,
                    Lat=2323,
                    Long=3434,
                    Population=45000
                },
                new Region()
                {
                    Id = Guid.Parse("1ea9b01d-ae6d-4cbc-addc-20549f495a0a"),
                    Code = "EAST-DEL",
                    Name="EAST DELHI",
                    Area=235,
                    Lat=2393,
                    Long=3634,
                    Population=560000
                }

            };

            //Seed the regions data in DataBase
            modelBuilder.Entity<Region>().HasData(regions);
        }

    }
}
