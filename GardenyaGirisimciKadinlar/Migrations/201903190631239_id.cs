namespace GardenyaGirisimciKadinlar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class id : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Urunlers", "GirisimciID", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Urunlers", "GirisimciID", c => c.Int(nullable: false));
        }
    }
}
