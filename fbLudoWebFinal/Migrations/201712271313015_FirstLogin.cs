namespace fbLudoWebFinal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstLogin : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "FirstLogin", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "FirstLogin");
        }
    }
}
