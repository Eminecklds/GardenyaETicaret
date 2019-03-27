namespace GardenyaGirisimciKadinlar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class subtotal : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Carts", "SubTotal", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Carts", "SubTotal");
        }
    }
}
