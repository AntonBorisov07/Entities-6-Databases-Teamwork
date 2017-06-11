namespace ElephantBookStore.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Magazinesadded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Magazines",
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
            DropForeignKey("dbo.Magazines", "ID", "dbo.Items");
            DropIndex("dbo.Magazines", new[] { "ID" });
            DropTable("dbo.Magazines");
        }
    }
}
