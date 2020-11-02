namespace DGSappSem2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newstaffattfixes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StaffAttendances",
                c => new
                    {
                        StaffAttendanceId = c.Int(nullable: false, identity: true),
                        StaffAttName = c.String(),
                        Staffrecord = c.String(),
                        GetDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.StaffAttendanceId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.StaffAttendances");
        }
    }
}
