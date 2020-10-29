using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using DGSappSem2.Models.Classes;
using DGSappSem2.Models.Reports;
using DGSappSem2.Models.Staffs;
using DGSappSem2.Models.Assessments;
using DGSappSem2.Models.Students;
using DGSappSem2.Models.Subjects;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DGSappSem2.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        public System.Data.Entity.DbSet<DGSappSem2.Models.FileUpload.FileUploadModel> fileUploadModel { get; set; }

        public DbSet<Student> students { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<_Subject> Subjects { get; set; }
        public DbSet<Course_Material> Course_Materials { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<_Class> Classes { get; set; }
        public DbSet<Assessment> Assessments { get; set; }
        public DbSet<ClassList> ClassLists { get; set; }
        public DbSet<SubjectReport> SubjectReports { get; set; }
        public DbSet<StaffAttendance> StaffAttendances { get; set; }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}