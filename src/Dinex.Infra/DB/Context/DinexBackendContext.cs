namespace Dinex.Infra;

public class DinexBackendContext : DbContext
{
    public DinexBackendContext(DbContextOptions<DinexBackendContext> options) : base(options)
    {
        
    }

    public DbSet<CodeManager> CodeManager { get; set; }
    //public DbSet<Category> Categories { get; set; }
    //public DbSet<CategoryToUser> CategoriesToUsers { get; set; }
    //public DbSet<Launch> Launches { get; set; }
    //public DbSet<PayMethodFromLaunch> PayMethodFromLaunches { get; set; }
    //public DbSet<ScheduledLaunches> ScheduledLaunches { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Account> UserAccounts { get; set; }
    //public DbSet<UserAmountAvailable> UserAmountAvailable { get; set; }
    public DbSet<QueueIn> QueueIn { get; set; }

    public DbSet<HistoryFile> HistoryFiles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Filename=DinExDB.sqlite");
    }
}
