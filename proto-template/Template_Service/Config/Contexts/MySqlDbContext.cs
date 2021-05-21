using Microsoft.EntityFrameworkCore;
using Template_Service.Persistence.Entities;

namespace Template_Service.Config.Contexts {
    public class MySqlDbContext : DbContext {
        public MySqlDbContext(DbContextOptions<MySqlDbContext> options) : base(options) {
            this.Database.EnsureCreated();
        }

        public DbSet<SqlTemplateEntity> Templates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            // Place here stuff about foreign keys etc

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }
    }
}