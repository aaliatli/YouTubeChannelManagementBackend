using Microsoft.EntityFrameworkCore;
public class ChannelDbContext : DbContext
{
    public ChannelDbContext(DbContextOptions<ChannelDbContext> options) : base(options) { }
    public DbSet<Channel> Channels { get; set; }
    public DbSet<ChannelSubscriber> ChannelSubscribers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Channel>().HasKey(c => c.ChannelId);
        modelBuilder.Entity<Channel>().Property(c => c.ChannelName).HasMaxLength(200).IsRequired();
        modelBuilder.Entity<ChannelSubscriber>().HasKey(cs => new { cs.ChannelId, cs.UserId });
        modelBuilder.Entity<ChannelSubscriber>().HasOne(cs => cs.Channel).WithMany(c => c.ChannelSubscribers).HasForeignKey(cs => cs.ChannelId).OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<ChannelSubscriber>().HasIndex(cs => cs.UserId);
    }
}