using Microsoft.EntityFrameworkCore;

namespace Slovko.NL.Api.DataAccess;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Word> FiveLetterWords { get; set; }

}



