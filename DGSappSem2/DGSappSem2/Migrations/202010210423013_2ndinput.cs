namespace DGSappSem2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2ndinput : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Assessments",
                c => new
                    {
                        AssessmentID = c.Int(nullable: false, identity: true),
                        Grade = c.String(nullable: false),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        AssessmentDate = c.DateTime(nullable: false),
                        AssessmentVenue = c.String(nullable: false),
                        Term = c.String(nullable: false),
                        Type = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.AssessmentID);
            
            CreateTable(
                "dbo._Class",
                c => new
                    {
                        ClassID = c.Int(nullable: false, identity: true),
                        Grade = c.String(),
                        SubjectID = c.Int(nullable: false),
                        ClassListID = c.Int(nullable: false),
                        StaffID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ClassID)
                .ForeignKey("dbo.ClassLists", t => t.ClassListID, cascadeDelete: true)
                .ForeignKey("dbo.Staffs", t => t.StaffID, cascadeDelete: true)
                .ForeignKey("dbo._Subject", t => t.SubjectID, cascadeDelete: true)
                .Index(t => t.SubjectID)
                .Index(t => t.ClassListID)
                .Index(t => t.StaffID);
            
            CreateTable(
                "dbo.ClassLists",
                c => new
                    {
                        ClassListID = c.Int(nullable: false, identity: true),
                        StID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ClassListID)
                .ForeignKey("dbo.Students", t => t.StID, cascadeDelete: true)
                .Index(t => t.StID);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        StID = c.Int(nullable: false, identity: true),
                        StudentName = c.String(nullable: false),
                        StudentSurname = c.String(nullable: false),
                        StudentGender = c.String(nullable: false),
                        StudentAddress = c.String(nullable: false),
                        StudentTown = c.String(nullable: false),
                        StudentContact = c.String(nullable: false),
                        StudentGrade = c.String(nullable: false),
                        StudentEmail = c.String(nullable: false),
                        StudentBirthCertURL = c.String(nullable: false),
                        StudentReportURL = c.String(nullable: false),
                        StudentProofResURL = c.String(nullable: false),
                        StudentPermitURL = c.String(),
                        StudentAllowReg = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.StID);
            
            CreateTable(
                "dbo.Staffs",
                c => new
                    {
                        StaffId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Surname = c.String(nullable: false),
                        Gender = c.String(nullable: false),
                        DateOfBirth = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        PhoneNo = c.String(nullable: false),
                        Grade = c.String(nullable: false),
                        Subject = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        Position = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.StaffId);
            
            CreateTable(
                "dbo.Course_Material",
                c => new
                    {
                        CourseMaterialID = c.Int(nullable: false, identity: true),
                        title = c.String(nullable: false, maxLength: 50),
                        SubjectID = c.Int(nullable: false),
                        StaffId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CourseMaterialID)
                .ForeignKey("dbo.Staffs", t => t.StaffId, cascadeDelete: true)
                .ForeignKey("dbo._Subject", t => t.SubjectID, cascadeDelete: true)
                .Index(t => t.SubjectID)
                .Index(t => t.StaffId);
            
            CreateTable(
                "dbo._Subject",
                c => new
                    {
                        SubjectID = c.Int(nullable: false, identity: true),
                        SubjectName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.SubjectID);
            
            CreateTable(
                "dbo.SubjectReports",
                c => new
                    {
                        SubjectReportID = c.Int(nullable: false, identity: true),
                        SubjectID = c.Int(nullable: false),
                        AssesssmentId = c.Int(nullable: false),
                        Assessments_AssessmentID = c.Int(),
                    })
                .PrimaryKey(t => t.SubjectReportID)
                .ForeignKey("dbo.Assessments", t => t.Assessments_AssessmentID)
                .ForeignKey("dbo._Subject", t => t.SubjectID, cascadeDelete: true)
                .Index(t => t.SubjectID)
                .Index(t => t.Assessments_AssessmentID);
            
            CreateTable(
                "dbo.FileUploadModels",
                c => new
                    {
                        FileId = c.Int(nullable: false, identity: true),
                        File = c.Binary(),
                        FileName = c.String(),
                    })
                .PrimaryKey(t => t.FileId);
            
            CreateTable(
                "dbo.Reports",
                c => new
                    {
                        ReportID = c.Int(nullable: false, identity: true),
                        StudentID = c.Int(nullable: false),
                        marks = c.Int(nullable: false),
                        AssessmentId = c.Int(nullable: false),
                        Students_StID = c.Int(),
                    })
                .PrimaryKey(t => t.ReportID)
                .ForeignKey("dbo.Assessments", t => t.AssessmentId, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.Students_StID)
                .Index(t => t.AssessmentId)
                .Index(t => t.Students_StID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reports", "Students_StID", "dbo.Students");
            DropForeignKey("dbo.Reports", "AssessmentId", "dbo.Assessments");
            DropForeignKey("dbo.SubjectReports", "SubjectID", "dbo._Subject");
            DropForeignKey("dbo.SubjectReports", "Assessments_AssessmentID", "dbo.Assessments");
            DropForeignKey("dbo.Course_Material", "SubjectID", "dbo._Subject");
            DropForeignKey("dbo._Class", "SubjectID", "dbo._Subject");
            DropForeignKey("dbo.Course_Material", "StaffId", "dbo.Staffs");
            DropForeignKey("dbo._Class", "StaffID", "dbo.Staffs");
            DropForeignKey("dbo.ClassLists", "StID", "dbo.Students");
            DropForeignKey("dbo._Class", "ClassListID", "dbo.ClassLists");
            DropIndex("dbo.Reports", new[] { "Students_StID" });
            DropIndex("dbo.Reports", new[] { "AssessmentId" });
            DropIndex("dbo.SubjectReports", new[] { "Assessments_AssessmentID" });
            DropIndex("dbo.SubjectReports", new[] { "SubjectID" });
            DropIndex("dbo.Course_Material", new[] { "StaffId" });
            DropIndex("dbo.Course_Material", new[] { "SubjectID" });
            DropIndex("dbo.ClassLists", new[] { "StID" });
            DropIndex("dbo._Class", new[] { "StaffID" });
            DropIndex("dbo._Class", new[] { "ClassListID" });
            DropIndex("dbo._Class", new[] { "SubjectID" });
            DropTable("dbo.Reports");
            DropTable("dbo.FileUploadModels");
            DropTable("dbo.SubjectReports");
            DropTable("dbo._Subject");
            DropTable("dbo.Course_Material");
            DropTable("dbo.Staffs");
            DropTable("dbo.Students");
            DropTable("dbo.ClassLists");
            DropTable("dbo._Class");
            DropTable("dbo.Assessments");
        }
    }
}
