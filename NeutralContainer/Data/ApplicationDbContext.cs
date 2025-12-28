using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NeutralContainer.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Post> Posts => Set<Post>();

        public DbSet<Comment> Comments => Set<Comment>();

        public DbSet<CommentReport> CommentReports => Set<CommentReport>();

        public DbSet<CreatorBlock> CreatorBlocks => Set<CreatorBlock>();

        public DbSet<ModerationLog> ModerationLogs => Set<ModerationLog>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<CreatorBlock>()
                .HasOne<ApplicationUser>()
                .WithMany()
                .HasForeignKey(block => block.CreatorUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<CreatorBlock>()
                .HasOne<ApplicationUser>()
                .WithMany()
                .HasForeignKey(block => block.BlockedUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<CommentReport>()
                .HasOne(report => report.Comment)
                .WithMany()
                .HasForeignKey(report => report.CommentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<CommentReport>()
                .HasOne<ApplicationUser>()
                .WithMany()
                .HasForeignKey(report => report.ReporterUserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
