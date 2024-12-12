namespace AppECommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateOrder2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "CustomerId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "CustomerId");
        }
    }
}
