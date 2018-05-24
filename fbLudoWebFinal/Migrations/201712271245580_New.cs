namespace fbLudoWebFinal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class New : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Anrede", c => c.String());
            AddColumn("dbo.AspNetUsers", "Vorname", c => c.String());
            AddColumn("dbo.AspNetUsers", "Nachname", c => c.String());
            AddColumn("dbo.AspNetUsers", "PLZ", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "Ort", c => c.String());
            AddColumn("dbo.AspNetUsers", "Adresse", c => c.String());
            AddColumn("dbo.AspNetUsers", "Telefon", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Telefon");
            DropColumn("dbo.AspNetUsers", "Adresse");
            DropColumn("dbo.AspNetUsers", "Ort");
            DropColumn("dbo.AspNetUsers", "PLZ");
            DropColumn("dbo.AspNetUsers", "Nachname");
            DropColumn("dbo.AspNetUsers", "Vorname");
            DropColumn("dbo.AspNetUsers", "Anrede");
        }
    }
}
