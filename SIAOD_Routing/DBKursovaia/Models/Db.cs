using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBKursovaia.Models
{
    public class Db : IDisposable
    {
        private static Db instance;
        public static Db Postgres
        {
            get
            {
                if (Db.instance == null)
                {
                    Db.instance = new Db();
                }
                return Db.instance;
            }
        }

        private NpgsqlConnection psql;

        public bool IsConnected => this.psql != null;

        public void Connect(string connectionString)
        {
            if (this.psql == null)
            {
                this.psql = new NpgsqlConnection(connectionString);
            }
        }

        public List<Department> GetDepartments()
        {
            string sqlRequest = "SELECT \"Id\",\"Name\" FROM \"Department\";";

            List<Department> departments = new List<Department>();

            lock (this)
            {
                using (NpgsqlCommand command = new NpgsqlCommand(sqlRequest, psql))
                {
                    this.psql.Open();

                    NpgsqlDataReader dbData = command.ExecuteReader();

                    while (dbData.Read())
                    {
                        departments.Add(new Department
                        {
                            Id = int.Parse(dbData[0].ToString()),
                            Name = dbData[1].ToString(),
                        });
                    }
                    this.psql.Close();
                };
            }

            return departments;
        }

        public List<Sector> GetSectors(Department department)
        {
            string sqlRequest = "SELECT * FROM \"Sector\" s,\"DepartmentSector\" ds " +
                                $"WHERE s.\"Id\" = ds.\"Sector_Id\" AND \"ds\".\"Department_Id\" = {department.Id};";

            List<Sector> sectors = new List<Sector>();

            lock (this)
            {
                using (NpgsqlCommand command = new NpgsqlCommand(sqlRequest, psql))
                {
                    this.psql.Open();

                    NpgsqlDataReader dbData = command.ExecuteReader();

                    while (dbData.Read())
                    {
                        sectors.Add(new Sector
                        {
                            Id = int.Parse(dbData[0].ToString()),
                            Name = dbData[1].ToString(),
                        });
                    }
                    this.psql.Close();
                };
            }

            return sectors;
        }

        public List<CncMachine> GetCncMachines(Sector sector)
        {
            string sqlRequest = "SELECT c.\"Id\",c.\"Name\",c.\"Inventary_Number\" FROM \"CncMachine\" c, \"Sector\" s " +
                                $"WHERE s.\"Id\" = c.\"Sector_Id\" AND \"s\".\"Id\" = {sector.Id}; ";

            List<CncMachine> machines = new List<CncMachine>();

            lock (this)
            {
                using (NpgsqlCommand command = new NpgsqlCommand(sqlRequest, psql))
                {
                    this.psql.Open();

                    NpgsqlDataReader dbData = command.ExecuteReader();

                    while (dbData.Read())
                    {
                        machines.Add(new CncMachine
                        {
                            Id = int.Parse(dbData[0].ToString()),
                            Name = dbData[1].ToString(),
                            InventaryNumber = dbData[2].ToString(),
                        });
                    }
                    this.psql.Close();
                };
            }

            return machines;
        }

        public List<Measure> GetMeasures(CncMachine machine, DateTime startDate)
        {
            string sqlRequest = "SELECT \"Indicator\".\"Name\", \"Measure\".\"Timestamp\", \"Measure\".\"Value\" " +
                                "FROM \"Measure\",\"CncMachineIndicator\",\"Indicator\",\"CncMachine\" " +
                                "WHERE \"Measure\".\"CncMachineIndicator_Id\" = \"CncMachineIndicator\".\"Id\" " +
                                    "AND \"CncMachineIndicator\".\"Indicator_Id\" = \"Indicator\".\"Id\" " +
                                    "AND \"CncMachineIndicator\".\"CncMachine_Id\" = \"CncMachine\".\"Id\" " +
                                    $"AND \"CncMachine\".\"Id\" = {machine.Id} " +
                                    $"AND \"Measure\".\"Timestamp\" > '{startDate}'";

            List<Measure> measures = new List<Measure>();

            lock (this)
            {
                using (NpgsqlCommand command = new NpgsqlCommand(sqlRequest, psql))
                {
                    this.psql.Open();
                    try
                    {
                        NpgsqlDataReader dbData = command.ExecuteReader();

                        while (dbData.Read())
                        {
                            measures.Add(new Measure
                            {
                                IndicatorName = dbData[0].ToString(),
                                TimeStamp = (DateTime)dbData[1],
                                Value = double.Parse(dbData[2].ToString())
                            });
                        }
                    }
                    finally 
                    {
                        this.psql.Close();
                    }
                };
            }

            return measures;
        }



        public void Dispose()
        {
            if (this.psql != null)
            {
                this.psql.Close();
                this.psql.Dispose();
            }
            GC.SuppressFinalize(this);
        }
    }
}
