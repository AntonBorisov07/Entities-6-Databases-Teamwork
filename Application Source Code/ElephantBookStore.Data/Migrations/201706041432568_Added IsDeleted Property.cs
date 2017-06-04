namespace ElephantBookStore.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedIsDeletedProperty : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Items", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.ProductTypes", "IsDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductTypes", "IsDeleted");
            DropColumn("dbo.Items", "IsDeleted");
            DropColumn("dbo.Categories", "IsDeleted");
        }
    }
}
