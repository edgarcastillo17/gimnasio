namespace Gimnasio.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserId_Client : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Clients", name: "ApplicationUser_Id", newName: "UserId");
            RenameIndex(table: "dbo.Clients", name: "IX_ApplicationUser_Id", newName: "IX_UserId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Clients", name: "IX_UserId", newName: "IX_ApplicationUser_Id");
            RenameColumn(table: "dbo.Clients", name: "UserId", newName: "ApplicationUser_Id");
        }
    }
}
