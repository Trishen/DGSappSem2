using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using DGSappSem2.Models.Reports;
using DGSappSem2.Models.Classes;

namespace DGSappSem2.Models.Subjects 
{
    public class _Subject
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int SubjectID { get; set; }

        [Required(ErrorMessage = "Enter a Subject Name")]
        [Display(Name = "Subject Name")]
        public string SubjectName { get; set; }

        public virtual ICollection<Course_Material> Course_Materials { get; set; }
        public virtual ICollection<SubjectReport> SubjectReports { get; set; }
        public virtual ICollection<_Class> Classes { get; set; }

    }
}