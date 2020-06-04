namespace Gimnasio.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateClients : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clients", "FirstName", c => c.String(nullable: false));
            AddColumn("dbo.Clients", "LastName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Clients", "LastName");
            DropColumn("dbo.Clients", "FirstName");
        }
    }
}
