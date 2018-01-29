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
            modelBuilder.Entity<Patient>()
                .HasMany(p => p.ObjectiveStatuses)
                .WithMany(o => o.Patients)
                .Map(m =>
                {
                    // Ссылка на промежуточную таблицу
                    m.ToTable("PatientObjectiveStatuses");

                    // Настройка внешних ключей промежуточной таблицы
                    m.MapLeftKey("Patient_Id");
                    m.MapRightKey("ObjectiveStatus_Id");
                });
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<ObjectiveStatus> ObjectiveStatuses { get; set; }
    }
}
