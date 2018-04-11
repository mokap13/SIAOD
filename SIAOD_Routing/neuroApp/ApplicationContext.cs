using neuroApp.Analyzes.AccompanyingIllness;
using neuroApp.Analyzes.ClinicalLaboratoryData;
using neuroApp.Analyzes.Complaint;
using neuroApp.Analyzes.HIV;
using neuroApp.Analyzes.HIVAssociateDisease;
using neuroApp.Analyzes.ObjectiveStatus;
using neuroApp.Analyzes.Tuberculosis;
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
            modelBuilder.Entity<TuberculosisStatus>()
                .ToTable("TuberculosisStatuses");
            modelBuilder.Entity<HIVStatus>()
                .ToTable("HIVStatuses");

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Patient> Patients { get; set; }
        
        public DbSet<BloodChemistry> BloodChemistries { get; set; }
        public DbSet<CompleteBloodCount> CompleteBloodCounts { get; set; }
        public DbSet<Immunogram> Immunograms { get; set; }

        public DbSet<Complaint> Complaints { get; set; }
        public DbSet<HIV> HIVs { get; set; }
        public DbSet<HIVPhase> HIVPhases { get; set; }
        public DbSet<HIVStage> HIVStages { get; set; }
        public DbSet<HIVStatus> HIVStatuses { get; set; }

        public DbSet<HIVAssociateDisease> HIVAssociateDiseases { get; set; }
        public DbSet<HIVAssociateDiseaseGroup> HIVAssociateDiseaseGroups { get; set; }

        public DbSet<HealthState> HealthStates { get; set; }
        public DbSet<ObjectiveStatusDisease> ObjectiveStatusDiseases { get; set; }
        public DbSet<ObjectiveStatus> ObjectiveStatuses { get; set; }

        public DbSet<TuberculosisForm> TuberculosisForms { get; set; }
        public DbSet<DrugResistance> DrugResistances { get; set; }
        public DbSet<TuberculosisStatus> TuberculosisStatuses { get; set; }
        public DbSet<AccompanyingIllness> AccompanyingIllnesses { get; set; }
    }
}
