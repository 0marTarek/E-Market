namespace EEMarket.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class lll : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        item_id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(),
                        add_at = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.item_id)
                .ForeignKey("dbo.products", t => t.ProductId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.products",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Double(nullable: false),
                        Image = c.String(),
                        Description = c.String(),
                        CategoryId = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Categories", t => t.CategoryId)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Number_of_product = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Carts", "ProductId", "dbo.products");
            DropForeignKey("dbo.products", "CategoryId", "dbo.Categories");
            DropIndex("dbo.products", new[] { "CategoryId" });
            DropIndex("dbo.Carts", new[] { "ProductId" });
            DropTable("dbo.Categories");
            DropTable("dbo.products");
            DropTable("dbo.Carts");
        }
    }
}
