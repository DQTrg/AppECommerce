namespace AppECommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedb2711 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ReviewProducts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        UserName = c.String(),
                        FullName = c.String(),
                        Email = c.String(),
                        Content = c.String(),
                        Rate = c.Int(nullable: false),
                        Avatar = c.String(),
                        CreatedBy = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                        Modifiedby = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Wishlists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        UserName = c.String(),
                        CreatedBy = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                        Modifiedby = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Wishlists", "ProductId", "dbo.Products");
            DropForeignKey("dbo.ReviewProducts", "ProductId", "dbo.Products");
            DropIndex("dbo.Wishlists", new[] { "ProductId" });
            DropIndex("dbo.ReviewProducts", new[] { "ProductId" });
            DropTable("dbo.Wishlists");
            DropTable("dbo.ReviewProducts");
        }
    }
}
