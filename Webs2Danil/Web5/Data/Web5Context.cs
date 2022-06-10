using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Web5.Models;

namespace Web5.Data
{
    public class Web5Context : DbContext
    {
        public Web5Context (DbContextOptions<Web5Context> options)
            : base(options)
        {
        }

        public DbSet<Web5.Models.HospitalModel>? HospitalModel { get; set; }

        public DbSet<Web5.Models.LabModel>? LabModel { get; set; }

        public DbSet<Web5.Models.DoctorModel>? DoctorModel { get; set; }

        public DbSet<Web5.Models.PatientModel>? PatientModel { get; set; }
    }
}
