namespace AppECommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateProductsAddOriginalQuantity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "OriginalQuantity", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "OriginalQuantity");
        }
    }
}
