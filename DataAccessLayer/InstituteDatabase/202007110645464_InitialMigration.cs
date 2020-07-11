namespace DataAccessLayer.InstituteDatabase
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.institute_branches",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(unicode: false),
                        IsHeadBranch = c.Boolean(nullable: false),
                        ParentBranchId = c.Int(nullable: false),
                        IsParentBranch = c.Boolean(nullable: false),
                        Address = c.String(unicode: false),
                        CIty = c.String(unicode: false),
                        Country = c.String(unicode: false),
                        ZipCode = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.institute_branches");
        }
    }
}
