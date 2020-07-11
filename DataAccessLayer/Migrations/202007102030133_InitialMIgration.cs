namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMIgration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.institute_connections",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InstituteCode = c.String(nullable: false, unicode: false),
                        DatabaseName = c.String(nullable: false, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.lsroles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(unicode: false),
                        Name = c.String(nullable: false, maxLength: 256, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.lsuser_roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                        LSRole_Id = c.Int(),
                        LSUser_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.lsroles", t => t.LSRole_Id)
                .ForeignKey("dbo.lsusers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.lsusers", t => t.LSUser_Id)
                .ForeignKey("dbo.lsroles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId)
                .Index(t => t.LSRole_Id)
                .Index(t => t.LSUser_Id);
            
            CreateTable(
                "dbo.lsusers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FullName = c.String(unicode: false),
                        DateCreated = c.DateTime(nullable: false, precision: 0),
                        IsInstituteAdmin = c.Boolean(nullable: false),
                        InstituteConnectionId = c.Int(nullable: false),
                        Email = c.String(maxLength: 256, storeType: "nvarchar"),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(unicode: false),
                        SecurityStamp = c.String(unicode: false),
                        PhoneNumber = c.String(unicode: false),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(precision: 0),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.institute_connections", t => t.InstituteConnectionId, cascadeDelete: true)
                .Index(t => t.InstituteConnectionId)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.lsuser_claims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ClaimType = c.String(unicode: false),
                        ClaimValue = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.lsusers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.lsuser_logins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        ProviderKey = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.lsusers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.lsuser_roles", "RoleId", "dbo.lsroles");
            DropForeignKey("dbo.lsuser_roles", "LSUser_Id", "dbo.lsusers");
            DropForeignKey("dbo.lsuser_roles", "UserId", "dbo.lsusers");
            DropForeignKey("dbo.lsuser_logins", "UserId", "dbo.lsusers");
            DropForeignKey("dbo.lsusers", "InstituteConnectionId", "dbo.institute_connections");
            DropForeignKey("dbo.lsuser_claims", "UserId", "dbo.lsusers");
            DropForeignKey("dbo.lsuser_roles", "LSRole_Id", "dbo.lsroles");
            DropIndex("dbo.lsuser_logins", new[] { "UserId" });
            DropIndex("dbo.lsuser_claims", new[] { "UserId" });
            DropIndex("dbo.lsusers", "UserNameIndex");
            DropIndex("dbo.lsusers", new[] { "InstituteConnectionId" });
            DropIndex("dbo.lsuser_roles", new[] { "LSUser_Id" });
            DropIndex("dbo.lsuser_roles", new[] { "LSRole_Id" });
            DropIndex("dbo.lsuser_roles", new[] { "RoleId" });
            DropIndex("dbo.lsuser_roles", new[] { "UserId" });
            DropIndex("dbo.lsroles", "RoleNameIndex");
            DropTable("dbo.lsuser_logins");
            DropTable("dbo.lsuser_claims");
            DropTable("dbo.lsusers");
            DropTable("dbo.lsuser_roles");
            DropTable("dbo.lsroles");
            DropTable("dbo.institute_connections");
        }
    }
}
