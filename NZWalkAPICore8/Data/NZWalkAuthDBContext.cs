using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace NZWalkAPICore8.Data
{
    public class NZWalkAuthDBContext : IdentityDbContext
    {
        public NZWalkAuthDBContext(DbContextOptions<NZWalkAuthDBContext> dbContextOptions) : base(dbContextOptions) 
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            string readRoleId = "f89687ec-d416-4343-8990-a630c0807769";
            string writeRoleId = "851ec73e-0998-467b-917b-ab248e31c3e9";

            var roles = new List<IdentityRole>()
            {
                new IdentityRole()
                {
                    Id = readRoleId,
                    ConcurrencyStamp = readRoleId,
                    Name = "Reader",
                    NormalizedName = "Reader".ToUpper()
                },
                new IdentityRole()
                {
                    Id = writeRoleId,
                    ConcurrencyStamp = writeRoleId,
                    Name = "Writer",
                    NormalizedName = "Writer".ToUpper()
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);
        }


    }
}
