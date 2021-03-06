﻿using neuroApp.Analyzes.AccompanyingIllness;
using neuroApp.Analyzes.ClinicalLaboratoryData;
using neuroApp.Analyzes.Complaint;
using neuroApp.Analyzes.HIV;
using neuroApp.Analyzes.HIVAssociateDisease;
using neuroApp.Analyzes.ObjectiveStatus;
using neuroApp.Analyzes.Tuberculosis;
using neuroApp.Model;
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
            modelBuilder.Entity<ObjectiveStatus>()
                .ToTable("ObjectiveStatuses");
            modelBuilder.Entity<CompleteBloodCount>()
                .ToTable("CompleteBloodCounts");
            modelBuilder.Entity<HIV>()
                .ToTable("HIVs");

            //modelBuilder.Entity<Patient>()
            //    .HasMany(c => c.ObjectiveStatuses)    
            //    .WithRequired(o => o.Patient)
            //    .WillCascadeOnDelete(true);

            modelBuilder.Entity<Patient>()
                .HasMany(h => h.ObjectiveStatusDiseases)
                .WithMany(w => w.Patients)
                .Map(m =>
                {
                    m.ToTable("PatientObjectiveStatusDiseases")
                        .MapRightKey("ObjectiveStatusDisease_Id")
                        .MapLeftKey("Patient_Id");
                });
            //modelBuilder.Entity<Patient>()
            //    .HasRequired(r => r.TuberculosisForm)
            //    .WithMany(m => m.Patients)
            //    .HasForeignKey(k => k.TuberculosisFormId)
            //    .WillCascadeOnDelete(true);
            //modelBuilder.Entity<HIVAssociateDisease>()
            //    .ToTable("HIVAssociateDiseases");
            //modelBuilder.Entity<HIVAssociateDiseaseGroup>()
            //    .ToTable("HIVAssociateDiseaseGroups");
            //modelBuilder.Entity<Patient>()
            //    .HasMany(o => o.HIVAssociateDiseases)
            //    .WithMany(m => m.Patients);

            //modelBuilder.Entity<HIVAssociateDisease>()
            //    .HasRequired(r => r.HIVAssociateDiseaseGroup)
            //    .WithMany(m => m.HIVAssociateDiseases)
            //    .HasForeignKey(k => k.HIVAssociateDiseaseGroupId)

            //modelBuilder.Entity<ObjectiveStatusDiseaseObjectiveStatus>()
            //            .HasKey(i => new { i.ObjectiveStatusId, i.ObjectiveStatusDiseaseId });

            //modelBuilder.Entity<ObjectiveStatusDiseaseObjectiveStatus>()
            //   .HasRequired(i => i.ObjectiveStatus)
            //   .WithMany(i => i.ObjectiveStatusDiseaseObjectiveStatus)
            //   .HasForeignKey(i => i.ObjectiveStatusId)
            //   .WillCascadeOnDelete(true);

            //modelBuilder.Entity<ObjectiveStatusDiseaseObjectiveStatus>()
            //   .HasRequired(i => i.ObjectiveStatusDisease)
            //   .WithMany(i => i.ObjectiveStatusDiseaseObjectiveStatus)
            //   .HasForeignKey(i => i.ObjectiveStatusDisease)
            //   .WillCascadeOnDelete(true);

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
        public DbSet<TuberculosisPhase> TuberculosisPhases{ get; set; }
        public DbSet<Risk> Risks { get; set; }
    }
}
