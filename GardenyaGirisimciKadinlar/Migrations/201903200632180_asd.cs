namespace GardenyaGirisimciKadinlar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class asd : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "Kullanici_Id", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetUsers", new[] { "Kullanici_Id" });
            DropColumn("dbo.AspNetUsers", "Kullanici_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Kullanici_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.AspNetUsers", "Kullanici_Id");
            AddForeignKey("dbo.AspNetUsers", "Kullanici_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
