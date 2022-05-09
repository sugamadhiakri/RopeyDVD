using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RopeyDVD.Areas.Identity.Data;
using RopeyDVD.Models;

namespace RopeyDVD.Data;

public class RopeyDVDContext : IdentityDbContext<RopeyDVDUser>
{
    public DbSet<Actor> Actor { get; set; }
    public DbSet<DVDCategory> DVDCategory { get; set; }
    public DbSet<DVDCopy> DVDCopy { get; set; }
    public DbSet<DVDTitle> DVDTitle { get; set; }
    public DbSet<Loan> Loan { get; set; }
    public DbSet<LoanType> LoanType { get; set; }
    public DbSet<Member> Member { get; set; }
    public DbSet<MembershipCategory> MembershipCategory { get; set; }
    public DbSet<Producer> Producer { get; set; }
    public DbSet<Studio> Studio { get; set; }


    public RopeyDVDContext(DbContextOptions<RopeyDVDContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);


    }
}
