using System.Diagnostics;
using System.IO;
using LibrarySettings.Models;
using Microsoft.EntityFrameworkCore;

namespace LibrarySettings.DBContext
{
    public class SqlIteTestValuesContext : DbContext
    {
        public virtual DbSet<TokenValue> TokenValues { get; set; }

        public SqlIteTestValuesContext()
        {

        }

        public SqlIteTestValuesContext(DbContextOptions<SqlIteTestValuesContext> options)
            : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                if (Debugger.IsAttached)
                {
                    var pathSqlIte = 
                        optionsBuilder.UseSqlite(@"Data Source = C:\Users\BondarenkoAS\source\repos\taxcomagent\TaxcomAgent\UnitTests\Integration\AppTest\TestValues.db");
                }
                else
                {
                    var pathSqlIteBuildVer = optionsBuilder.UseSqlite(@"Data Source = .\TestValues.db");
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<TokenValue>(entity =>
            {
                
                entity.HasNoKey();
                
                entity.Property(e => e.SignContentBase64)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("signContentBase64");

                entity.Property(e => e.TokenId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("TokenId");

                entity.Property(e => e.TokenLogin)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("tokenLogin");

                entity.Property(e => e.TokenName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("tokenName");

                entity.Property(e => e.TokenThumb)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("tokenThumb");

                entity.Property(e => e.MrDecryptContent)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("MrDecryptContent");

                entity.Property(e => e.RecipientInfoContent)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("RecipientInfoContent");

                entity.Property(e => e.MrEncryptContent)
                    .HasMaxLength(900)
                    .IsUnicode(false)
                    .HasColumnName("MrEncryptContent");

                entity.Property(e => e.SignSoapContentB64)
                    .HasMaxLength(900)
                    .IsUnicode(false)
                    .HasColumnName("SignSoapContentB64");

                entity.Property(e => e.SignSoapXmlSigners)
                    .HasMaxLength(900)
                    .IsUnicode(false)
                    .HasColumnName("SignSoapXmlSigners");

                entity.Property(e => e.MrCheckSignSgnContent)
                    .HasMaxLength(900)
                    .IsUnicode(false)
                    .HasColumnName("MrCheckSignSgnContent");

                entity.ToTable("TokenValues");
            });
        }
    }
}
