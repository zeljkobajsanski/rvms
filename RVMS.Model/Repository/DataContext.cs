using System.Data.Entity;
using RVMS.Model.Entities;

namespace RVMS.Model.Repository
{
    public class DataContext : DbContext
    {
        static DataContext()
        {
            //Database.SetInitializer(new DropCreateDatabaseIfModelChanges<DataContext>());
            Database.SetInitializer(new CreateDatabaseIfNotExists<DataContext>());
        }

        public DataContext() : base("RVMSConnection")
        {
        }

        public DbSet<Opstina> Opstine { get; set; }

        public DbSet<Mesto> Mesta { get; set; }

        public DbSet<Stajaliste> Stajalista { get; set; }

        public DbSet<Relacija> Relacije { get; set; }

        public DbSet<MedjustanicnoRastojanje> Daljinar { get; set; }

        public DbSet<Linija> Linije { get; set; }

        public DbSet<StajalisteLinije> StajalistaLinije { get; set; }

        public DbSet<Prevoznik> Prevoznici { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var stajalista = modelBuilder.Entity<Stajaliste>();
                stajalista.ToTable("Stajalista");
                stajalista.Property(x => x.GpsLatituda).HasPrecision(7, 4);
                stajalista.Property(x => x.GpsLongituda).HasPrecision(7, 4);

            var medjustanicoRastojanje = modelBuilder.Entity<MedjustanicnoRastojanje>();
                medjustanicoRastojanje.HasRequired(x => x.PolaznoStajaliste).WithMany().WillCascadeOnDelete(false);
                medjustanicoRastojanje.HasRequired(x => x.DolaznoStajaliste).WithMany().WillCascadeOnDelete(false);
                medjustanicoRastojanje.ToTable("Daljinar");

            var linije = modelBuilder.Entity<Linija>();
                linije.ToTable("Linije");
                //linije.HasMany(x => x.Stajalista).WithRequired().WillCascadeOnDelete(false);

            var stajalistaLinije = modelBuilder.Entity<StajalisteLinije>();
                stajalistaLinije.ToTable("StajalistaLinije");
                //stajalistaLinije.HasRequired(x => x.Linija).WithMany();
            var prevoznici = modelBuilder.Entity<Prevoznik>();
            prevoznici.ToTable("Prevoznici");
        }
    }
}