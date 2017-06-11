namespace ElephantBookStore.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(maxLength: 50),
                        ProductTypeID = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ProductTypes", t => t.ProductTypeID, cascadeDelete: true)
                .Index(t => t.CategoryName, unique: true)
                .Index(t => t.ProductTypeID);
            
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CategoryID = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Description = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Categories", t => t.CategoryID, cascadeDelete: true)
                .Index(t => t.CategoryID);
            
            CreateTable(
                "dbo.ProductTypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ProductTypeName = c.String(nullable: false, maxLength: 100),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ProductTypeName, unique: true, name: "IX_UniqueProductTypeName");
            
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        Author = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Items", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "dbo.Gifts",
                c => new
                    {
                        ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Items", t => t.ID)
                .Index(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Gifts", "ID", "dbo.Items");
            DropForeignKey("dbo.Books", "ID", "dbo.Items");
            DropForeignKey("dbo.Categories", "ProductTypeID", "dbo.ProductTypes");
            DropForeignKey("dbo.Items", "CategoryID", "dbo.Categories");
            DropIndex("dbo.Gifts", new[] { "ID" });
            DropIndex("dbo.Books", new[] { "ID" });
            DropIndex("dbo.ProductTypes", "IX_UniqueProductTypeName");
            DropIndex("dbo.Items", new[] { "CategoryID" });
            DropIndex("dbo.Categories", new[] { "ProductTypeID" });
            DropIndex("dbo.Categories", new[] { "CategoryName" });
            DropTable("dbo.Gifts");
            DropTable("dbo.Books");
            DropTable("dbo.ProductTypes");
            DropTable("dbo.Items");
            DropTable("dbo.Categories");
        }
    }
}
