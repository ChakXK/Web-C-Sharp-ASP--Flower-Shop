namespace Цветочный_магазин.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Updating : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "name", c => c.String(nullable: false, maxLength: 20, unicode: false));
            AlterColumn("dbo.Orders", "surname", c => c.String(nullable: false, maxLength: 50, unicode: false));
            AlterColumn("dbo.Orders", "phone", c => c.String(nullable: false, maxLength: 20));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "phone", c => c.String(maxLength: 20));
            AlterColumn("dbo.Orders", "surname", c => c.String(maxLength: 50, unicode: false));
            AlterColumn("dbo.Orders", "name", c => c.String(maxLength: 20, unicode: false));
        }
    }
}
