namespace GardenyaGirisimciKadinlar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sepet : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SiparisUrunlers", "Siparis_SiparisID", "dbo.Siparis");
            DropForeignKey("dbo.SiparisUrunlers", "Urunler_UrunID", "dbo.Urunlers");
            DropIndex("dbo.SiparisUrunlers", new[] { "Siparis_SiparisID" });
            DropIndex("dbo.SiparisUrunlers", new[] { "Urunler_UrunID" });
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        RecordID = c.Int(nullable: false, identity: true),
                        CartID = c.String(),
                        UrunID = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.RecordID)
                .ForeignKey("dbo.Urunlers", t => t.UrunID, cascadeDelete: true)
                .Index(t => t.UrunID);
            
            AddColumn("dbo.Siparis", "Total", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.SiparisDetays", "UrunID", c => c.Int(nullable: false));
            CreateIndex("dbo.SiparisDetays", "UrunID");
            AddForeignKey("dbo.SiparisDetays", "UrunID", "dbo.Urunlers", "UrunID", cascadeDelete: true);
            DropTable("dbo.SiparisUrunlers");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.SiparisUrunlers",
                c => new
                    {
                        Siparis_SiparisID = c.Int(nullable: false),
                        Urunler_UrunID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Siparis_SiparisID, t.Urunler_UrunID });
            
            DropForeignKey("dbo.Carts", "UrunID", "dbo.Urunlers");
            DropForeignKey("dbo.SiparisDetays", "UrunID", "dbo.Urunlers");
            DropIndex("dbo.Carts", new[] { "UrunID" });
            DropIndex("dbo.SiparisDetays", new[] { "UrunID" });
            DropColumn("dbo.SiparisDetays", "UrunID");
            DropColumn("dbo.Siparis", "Total");
            DropTable("dbo.Carts");
            CreateIndex("dbo.SiparisUrunlers", "Urunler_UrunID");
            CreateIndex("dbo.SiparisUrunlers", "Siparis_SiparisID");
            AddForeignKey("dbo.SiparisUrunlers", "Urunler_UrunID", "dbo.Urunlers", "UrunID", cascadeDelete: true);
            AddForeignKey("dbo.SiparisUrunlers", "Siparis_SiparisID", "dbo.Siparis", "SiparisID", cascadeDelete: true);
        }
    }
}
