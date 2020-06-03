namespace Gimnasio.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Nutri_Coach : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Coaches", "FirstName", c => c.String());
            AddColumn("dbo.Coaches", "LastName", c => c.String());
            AddColumn("dbo.Nutritionists", "FirstName", c => c.String());
            AddColumn("dbo.Nutritionists", "LastName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Nutritionists", "LastName");
            DropColumn("dbo.Nutritionists", "FirstName");
            DropColumn("dbo.Coaches", "LastName");
            DropColumn("dbo.Coaches", "FirstName");
        }
    }
}
