using Microsoft.EntityFrameworkCore;
public class ChannelDbContext : DbContext
{
    public ChannelDbContext(DbContextOptions<ChannelDbContext> options) : base(options) { }
    public DbSet<Channel> Channels{ get; set; }
}