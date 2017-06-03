namespace ElephantBookStore.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Firstchange : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Gifts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Description = c.String(),
                        CategoryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Categories", t => t.CategoryID, cascadeDelete: true)
                .Index(t => t.CategoryID);
            
            AddColumn("dbo.Books", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Gifts", "CategoryID", "dbo.Categories");
            DropIndex("dbo.Gifts", new[] { "CategoryID" });
            DropColumn("dbo.Books", "Price");
            DropTable("dbo.Gifts");
        }
    }
}
