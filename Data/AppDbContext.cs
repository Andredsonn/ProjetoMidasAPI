using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjetoMidasAPI.Models;

namespace ProjetoMidasAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // DbSets para as tabelas no banco
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Projecao> Projecoes { get; set; }
        public DbSet<Lancamento> Lancamentos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Tratamento para o enum ficar como inteiro
            modelBuilder.Entity<Lancamento>()
                .Property(l => l.TipoLancamento)
                .HasConversion<int>();

            // Relacionamento 1 para muitos entre Projecao e Lancamento
            modelBuilder.Entity<Projecao>()
                .HasMany(p => p.lancamentos)
                .WithOne(l => l.Projecao!)
                .HasForeignKey(l => l.FK_IdProjecao)
                .OnDelete(DeleteBehavior.Cascade);                
        }
    }
}