namespace Dinex.Infra;

public class DinexBackendContext : DbContext
{
    public DinexBackendContext(DbContextOptions<DinexBackendContext> options) : base(options)
    {
        
    }

    #region User and Account
    public DbSet<User> Users { get; set; }
    public DbSet<Account> UserAccounts { get; set; }
    public DbSet<CodeManager> CodeManager { get; set; }
    //public DbSet<UserAmountAvailable> UserAmountAvailable { get; set; }
    public DbSet<Launch> Launches { get; set; }
    #endregion

    #region Queue and file imports
    public DbSet<QueueIn> QueueIn { get; set; }
    public DbSet<InvestingHistoryFile> InvestingHistoryFiles { get; set; }
    #endregion

    #region Investing launches
    public DbSet<InvestingBrokerage> InvestingBrokerages { get; set; }
    public DbSet<InvestingProduct> InvestingProducts { get; set; }
    public DbSet<InvestingLaunch> InvestingLaunches { get; set; }
    #endregion

    #region financial planning
    //public DbSet<Category> Categories { get; set; }
    //public DbSet<CategoryToUser> CategoriesToUsers { get; set; }
    //public DbSet<PayMethodFromLaunch> PayMethodFromLaunches { get; set; }
    //public DbSet<ScheduledLaunches> ScheduledLaunches { get; set; }
    #endregion

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Filename=DinExDB.sqlite");
    }
}
