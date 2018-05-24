namespace fbLudoWebFinal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DelTel : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "Telefon");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Telefon", c => c.String());
        }
    }
}
