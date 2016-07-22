using System.Data.Entity;
using TaskManager.Core.Domain;
using TaskManager.Core.Domain.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace TaskManager.Repositories
{
    public class TaskManagerContext: IdentityDbContext<User>
    {
        public TaskManagerContext() : base("TaskManagerContext")
        {
            // ROLA - This is a hack to ensure that Entity Framework SQL Provider is copied across to the output folder.
            // As it is installed in the GAC, Copy Local does not work. It is required for probing.
            // Fixed "Provider not loaded" error
            var ensureDLLIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }

        public DbSet<UserTask> UserTasks { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityUser>().ToTable("Users");
            modelBuilder.Entity<User>()
                .ToTable("Users")
                .Property(u => u.UserName)
                .IsRequired();

            //Custom Tables
            modelBuilder.Entity<IdentityUserRole>().ToTable("UserRoles");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogins");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaims");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles");

            modelBuilder.Entity<UserTask>()
                .HasRequired<User>(task => task.User)
                .WithMany(user => user.Tasks)
                .HasForeignKey(t => t.UserId);
        }

    }
}
