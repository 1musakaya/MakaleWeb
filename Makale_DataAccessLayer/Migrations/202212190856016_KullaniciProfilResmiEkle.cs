namespace Makale_DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class KullaniciProfilResmiEkle : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Kullanici", "ProfilResim", c => c.String(maxLength: 30));
            Sql("Update Kullanici set ProfilResim='images.png'");
        }
        
        public override void Down()
        {
            DropColumn("dbo.Kullanici", "ProfilResim");
        }
    }
}
