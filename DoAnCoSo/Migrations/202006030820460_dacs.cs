namespace DoAnCoSo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dacs : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DanhMucs",
                c => new
                    {
                        DanhMucID = c.Int(nullable: false, identity: true),
                        TenDanhMuc = c.String(nullable: false, maxLength: 50),
                        DonViID = c.Int(),
                    })
                .PrimaryKey(t => t.DanhMucID)
                .ForeignKey("dbo.DonVis", t => t.DonViID)
                .Index(t => t.DonViID);
            
            CreateTable(
                "dbo.DonVis",
                c => new
                    {
                        DonViID = c.Int(nullable: false, identity: true),
                        TenDonVi = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.DonViID);
            
            CreateTable(
                "dbo.HocVus",
                c => new
                    {
                        HocVuID = c.Int(nullable: false, identity: true),
                        NgayTao = c.DateTime(nullable: false, storeType: "date"),
                        NoiDung = c.String(storeType: "ntext"),
                        TinhTrang = c.Boolean(nullable: false),
                        ParentID = c.Int(nullable: false),
                        NgayHen = c.DateTime(nullable: false, storeType: "date"),
                        DanhMucID = c.Int(),
                        UserID = c.Int(),
                        DonViID = c.Int(),
                    })
                .PrimaryKey(t => t.HocVuID)
                .ForeignKey("dbo.DanhMucs", t => t.DanhMucID)
                .ForeignKey("dbo.DonVis", t => t.DonViID)
                .ForeignKey("dbo.Users", t => t.UserID)
                .Index(t => t.DanhMucID)
                .Index(t => t.UserID)
                .Index(t => t.DonViID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        UserName = c.String(maxLength: 50),
                        Email = c.String(nullable: false, maxLength: 30),
                        DonViID = c.Int(),
                        LopID = c.Int(),
                        RolesID = c.Int(),
                    })
                .PrimaryKey(t => t.UserID)
                .ForeignKey("dbo.DonVis", t => t.DonViID)
                .ForeignKey("dbo.Lops", t => t.LopID)
                .ForeignKey("dbo.Roles", t => t.RolesID)
                .Index(t => t.DonViID)
                .Index(t => t.LopID)
                .Index(t => t.RolesID);
            
            CreateTable(
                "dbo.Lops",
                c => new
                    {
                        LopID = c.Int(nullable: false, identity: true),
                        TenLop = c.String(nullable: false, maxLength: 50),
                        DonViID = c.Int(),
                    })
                .PrimaryKey(t => t.LopID)
                .ForeignKey("dbo.DonVis", t => t.DonViID)
                .Index(t => t.DonViID);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RolesID = c.Int(nullable: false, identity: true),
                        TenRole = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.RolesID);
            
            CreateTable(
                "dbo.FunctionRoles",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FunctionID = c.Int(),
                        RolesID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Functions", t => t.FunctionID)
                .ForeignKey("dbo.Roles", t => t.RolesID)
                .Index(t => t.FunctionID)
                .Index(t => t.RolesID);
            
            CreateTable(
                "dbo.Functions",
                c => new
                    {
                        FunctionID = c.Int(nullable: false, identity: true),
                        FunctionName = c.String(),
                        Formname = c.String(),
                    })
                .PrimaryKey(t => t.FunctionID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "RolesID", "dbo.Roles");
            DropForeignKey("dbo.FunctionRoles", "RolesID", "dbo.Roles");
            DropForeignKey("dbo.FunctionRoles", "FunctionID", "dbo.Functions");
            DropForeignKey("dbo.Users", "LopID", "dbo.Lops");
            DropForeignKey("dbo.Lops", "DonViID", "dbo.DonVis");
            DropForeignKey("dbo.HocVus", "UserID", "dbo.Users");
            DropForeignKey("dbo.Users", "DonViID", "dbo.DonVis");
            DropForeignKey("dbo.HocVus", "DonViID", "dbo.DonVis");
            DropForeignKey("dbo.HocVus", "DanhMucID", "dbo.DanhMucs");
            DropForeignKey("dbo.DanhMucs", "DonViID", "dbo.DonVis");
            DropIndex("dbo.FunctionRoles", new[] { "RolesID" });
            DropIndex("dbo.FunctionRoles", new[] { "FunctionID" });
            DropIndex("dbo.Lops", new[] { "DonViID" });
            DropIndex("dbo.Users", new[] { "RolesID" });
            DropIndex("dbo.Users", new[] { "LopID" });
            DropIndex("dbo.Users", new[] { "DonViID" });
            DropIndex("dbo.HocVus", new[] { "DonViID" });
            DropIndex("dbo.HocVus", new[] { "UserID" });
            DropIndex("dbo.HocVus", new[] { "DanhMucID" });
            DropIndex("dbo.DanhMucs", new[] { "DonViID" });
            DropTable("dbo.Functions");
            DropTable("dbo.FunctionRoles");
            DropTable("dbo.Roles");
            DropTable("dbo.Lops");
            DropTable("dbo.Users");
            DropTable("dbo.HocVus");
            DropTable("dbo.DonVis");
            DropTable("dbo.DanhMucs");
        }
    }
}
