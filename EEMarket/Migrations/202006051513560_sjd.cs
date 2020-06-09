namespace EEMarket.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sjd : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Carts", "ProductId", "dbo.products");
            DropPrimaryKey("dbo.Carts");
            AddPrimaryKey("dbo.Carts", "ProductId");
            AddForeignKey("dbo.Carts", "ProductId", "dbo.products", "ID");
            DropColumn("dbo.Carts", "item_id");
            DropColumn("dbo.Carts", "card_id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Carts", "card_id", c => c.String());
            AddColumn("dbo.Carts", "item_id", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.Carts", "ProductId", "dbo.products");
            DropPrimaryKey("dbo.Carts");
            AddPrimaryKey("dbo.Carts", "item_id");
            AddForeignKey("dbo.Carts", "ProductId", "dbo.products", "ID", cascadeDelete: true);
        }
    }
}
