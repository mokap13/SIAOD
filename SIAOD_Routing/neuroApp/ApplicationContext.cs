using neuroApp.Analyzes;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace neuroApp
{
    class ApplicationContext : DbContext
    {
        public ApplicationContext() : base("DefaultConnection")
        {
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TuberculosisForm>()
                .HasMany(p => p.Patients)
                .WithRequired(t => t.TuberculosisForm);
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<TuberculosisForm> TuberculosisForms { get; set; }
        //public DbSet<ObjectiveStatus> ObjectiveStatuses { get; set; }
    }
}
