using LibrarySettings.Models;
using Microsoft.EntityFrameworkCore;

namespace LibrarySettings.DBContext
{
    public  class AgentVersionsValuesContext : DbContext
    {
        public AgentVersionsValuesContext()
        {
        
        }

        public AgentVersionsValuesContext(DbContextOptions<AgentVersionsValuesContext> options) : base(options)
        {

        }

        public virtual  DbSet<AgentPathModel> AgentPathModels { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(SqlReaderFactory.ReadSqlAppConfig());
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<AgentPathModel>(entity =>
            {
                entity.ToTable("AgentAgentInstallers");
                entity.HasNoKey();

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("Id");

                entity.Property(e => e.AgentInstallerPath)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("agentInstallerPath");
            });
        }
    }
}
