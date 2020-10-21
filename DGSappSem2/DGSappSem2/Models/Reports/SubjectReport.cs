using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using DGSappSem2.Models.Subjects;
using DGSappSem2.Models.Assessments;

namespace DGSappSem2.Models.Reports 
{
    public class SubjectReport
    {

        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int SubjectReportID { get; set; }

        public int SubjectID { get; set; }
        public _Subject Subjects { get; set; }

        public int AssesssmentId { get; set; }
        public Assessment Assessments { get; set; }

    }
}