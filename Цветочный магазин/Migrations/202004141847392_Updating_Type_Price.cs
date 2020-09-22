namespace Цветочный_магазин.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Updating_Type_Price : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Flowers", "price", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Flowers", "price", c => c.Decimal(precision: 18, scale: 0));
        }
    }
}
