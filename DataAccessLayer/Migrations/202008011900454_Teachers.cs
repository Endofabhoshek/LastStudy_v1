namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Teachers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.teachers",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Qualification = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.lsusers", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.teachers", "Id", "dbo.lsusers");
            DropIndex("dbo.teachers", new[] { "Id" });
            DropTable("dbo.teachers");
        }
    }
}
