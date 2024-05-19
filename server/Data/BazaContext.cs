using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

using EDziennik.Models.Baza;

namespace EDziennik.Data
{
    public partial class BazaContext : Microsoft.EntityFrameworkCore.DbContext
    {
        private readonly IHttpContextAccessor httpAccessor;

        public BazaContext(IHttpContextAccessor httpAccessor, DbContextOptions<BazaContext> options):base(options)
        {
            this.httpAccessor = httpAccessor;
        }

        public BazaContext(IHttpContextAccessor httpAccessor)
        {
            this.httpAccessor = httpAccessor;
        }

        partial void OnModelBuilding(ModelBuilder builder);

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<EDziennik.Models.Baza.Klasa>()
                  .HasOne(i => i.Nauczyciel)
                  .WithMany(i => i.Klasas)
                  .HasForeignKey(i => i.nauczyciel_id)
                  .HasPrincipalKey(i => i.nauczyciel_id);
            builder.Entity<EDziennik.Models.Baza.Nauczyciel>()
                  .HasOne(i => i.Osoba)
                  .WithMany(i => i.Nauczyciels)
                  .HasForeignKey(i => i.osoba_id)
                  .HasPrincipalKey(i => i.osoba_id);
            builder.Entity<EDziennik.Models.Baza.Obecnosc>()
                  .HasOne(i => i.Uczen)
                  .WithMany(i => i.Obecnoscs)
                  .HasForeignKey(i => i.uczen_id)
                  .HasPrincipalKey(i => i.uczen_id);
            builder.Entity<EDziennik.Models.Baza.Obecnosc>()
                  .HasOne(i => i.DataOpi)
                  .WithMany(i => i.Obecnoscs)
                  .HasForeignKey(i => i.data_opis_id)
                  .HasPrincipalKey(i => i.data_opis_id);
            builder.Entity<EDziennik.Models.Baza.Ocena>()
                  .HasOne(i => i.Nauczyciel)
                  .WithMany(i => i.Ocenas)
                  .HasForeignKey(i => i.nauczyciel_id)
                  .HasPrincipalKey(i => i.nauczyciel_id);
            builder.Entity<EDziennik.Models.Baza.Ocena>()
                  .HasOne(i => i.Uczen)
                  .WithMany(i => i.Ocenas)
                  .HasForeignKey(i => i.uczen_id)
                  .HasPrincipalKey(i => i.uczen_id);
            builder.Entity<EDziennik.Models.Baza.Ocena>()
                  .HasOne(i => i.DataOpi)
                  .WithMany(i => i.Ocenas)
                  .HasForeignKey(i => i.data_opis_id)
                  .HasPrincipalKey(i => i.data_opis_id);
            builder.Entity<EDziennik.Models.Baza.Przedmiot>()
                  .HasOne(i => i.Nauczyciel)
                  .WithMany(i => i.Przedmiots)
                  .HasForeignKey(i => i.nauczyciel_id)
                  .HasPrincipalKey(i => i.nauczyciel_id);
            builder.Entity<EDziennik.Models.Baza.Przedmiot>()
                  .HasOne(i => i.Klasa)
                  .WithMany(i => i.Przedmiots)
                  .HasForeignKey(i => i.klasa_id)
                  .HasPrincipalKey(i => i.klasa_id);
            builder.Entity<EDziennik.Models.Baza.Rodzic>()
                  .HasOne(i => i.Osoba)
                  .WithMany(i => i.Rodzics)
                  .HasForeignKey(i => i.osoba_id)
                  .HasPrincipalKey(i => i.osoba_id);
            builder.Entity<EDziennik.Models.Baza.Uczen>()
                  .HasOne(i => i.Osoba)
                  .WithMany(i => i.Uczens)
                  .HasForeignKey(i => i.osoba_id)
                  .HasPrincipalKey(i => i.osoba_id);
            builder.Entity<EDziennik.Models.Baza.Uczen>()
                  .HasOne(i => i.Rodzic)
                  .WithMany(i => i.Uczens)
                  .HasForeignKey(i => i.rodzic_id)
                  .HasPrincipalKey(i => i.rodzic_id);
            builder.Entity<EDziennik.Models.Baza.Uczen>()
                  .HasOne(i => i.Klasa)
                  .WithMany(i => i.Uczens)
                  .HasForeignKey(i => i.klasa_id)
                  .HasPrincipalKey(i => i.klasa_id);
            builder.Entity<EDziennik.Models.Baza.Uwaga>()
                  .HasOne(i => i.Nauczyciel)
                  .WithMany(i => i.Uwagas)
                  .HasForeignKey(i => i.nauczyciel_id)
                  .HasPrincipalKey(i => i.nauczyciel_id);
            builder.Entity<EDziennik.Models.Baza.Uwaga>()
                  .HasOne(i => i.Uczen)
                  .WithMany(i => i.Uwagas)
                  .HasForeignKey(i => i.uczen_id)
                  .HasPrincipalKey(i => i.uczen_id);
            builder.Entity<EDziennik.Models.Baza.Uwaga>()
                  .HasOne(i => i.DataOpi)
                  .WithMany(i => i.Uwagas)
                  .HasForeignKey(i => i.data_opis_id)
                  .HasPrincipalKey(i => i.data_opis_id);

            builder.Entity<EDziennik.Models.Baza.DataOpi>()
                  .Property(p => p.data_opis_id)
                  .HasDefaultValueSql("nextval('data_opis_data_opis_id_seq'::regclass)");

            builder.Entity<EDziennik.Models.Baza.Klasa>()
                  .Property(p => p.klasa_id)
                  .HasDefaultValueSql("nextval('klasa_klasa_id_seq'::regclass)");

            builder.Entity<EDziennik.Models.Baza.Nauczyciel>()
                  .Property(p => p.nauczyciel_id)
                  .HasDefaultValueSql("nextval('nauczyciel_nauczyciel_id_seq'::regclass)");

            builder.Entity<EDziennik.Models.Baza.Obecnosc>()
                  .Property(p => p.ocena_id)
                  .HasDefaultValueSql("nextval('obecnosc_ocena_id_seq'::regclass)");

            builder.Entity<EDziennik.Models.Baza.Ocena>()
                  .Property(p => p.ocena_id)
                  .HasDefaultValueSql("nextval('ocena_ocena_id_seq'::regclass)");

            builder.Entity<EDziennik.Models.Baza.Osoba>()
                  .Property(p => p.osoba_id)
                  .HasDefaultValueSql("nextval('osoba_osoba_id_seq'::regclass)");

            builder.Entity<EDziennik.Models.Baza.Przedmiot>()
                  .Property(p => p.przedmiot_id)
                  .HasDefaultValueSql("nextval('przedmiot_przedmiot_id_seq'::regclass)");

            builder.Entity<EDziennik.Models.Baza.Rodzic>()
                  .Property(p => p.rodzic_id)
                  .HasDefaultValueSql("nextval('rodzic_rodzic_id_seq'::regclass)");

            builder.Entity<EDziennik.Models.Baza.Uczen>()
                  .Property(p => p.uczen_id)
                  .HasDefaultValueSql("nextval('uczen_uczen_id_seq'::regclass)");

            builder.Entity<EDziennik.Models.Baza.Uwaga>()
                  .Property(p => p.uwaga_id)
                  .HasDefaultValueSql("nextval('uwaga_uwaga_id_seq'::regclass)");

            this.OnModelBuilding(builder);
        }


        public DbSet<EDziennik.Models.Baza.DataOpi> DataOpis
        {
          get;
          set;
        }

        public DbSet<EDziennik.Models.Baza.Klasa> Klasas
        {
          get;
          set;
        }

        public DbSet<EDziennik.Models.Baza.Nauczyciel> Nauczyciels
        {
          get;
          set;
        }

        public DbSet<EDziennik.Models.Baza.Obecnosc> Obecnoscs
        {
          get;
          set;
        }

        public DbSet<EDziennik.Models.Baza.Ocena> Ocenas
        {
          get;
          set;
        }

        public DbSet<EDziennik.Models.Baza.Osoba> Osobas
        {
          get;
          set;
        }

        public DbSet<EDziennik.Models.Baza.Przedmiot> Przedmiots
        {
          get;
          set;
        }

        public DbSet<EDziennik.Models.Baza.Rodzic> Rodzics
        {
          get;
          set;
        }

        public DbSet<EDziennik.Models.Baza.Uczen> Uczens
        {
          get;
          set;
        }

        public DbSet<EDziennik.Models.Baza.Uwaga> Uwagas
        {
          get;
          set;
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Conventions.Add(_ => new BlankTriggerAddingConvention());
        }
    }
}
