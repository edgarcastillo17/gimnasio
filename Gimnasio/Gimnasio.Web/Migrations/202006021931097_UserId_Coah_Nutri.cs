namespace Gimnasio.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserId_Coah_Nutri : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Coaches", name: "ApplicationUser_Id", newName: "UserId");
            RenameColumn(table: "dbo.Nutritionists", name: "ApplicationUser_Id", newName: "UserId");
            RenameIndex(table: "dbo.Coaches", name: "IX_ApplicationUser_Id", newName: "IX_UserId");
            RenameIndex(table: "dbo.Nutritionists", name: "IX_ApplicationUser_Id", newName: "IX_UserId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Nutritionists", name: "IX_UserId", newName: "IX_ApplicationUser_Id");
            RenameIndex(table: "dbo.Coaches", name: "IX_UserId", newName: "IX_ApplicationUser_Id");
            RenameColumn(table: "dbo.Nutritionists", name: "UserId", newName: "ApplicationUser_Id");
            RenameColumn(table: "dbo.Coaches", name: "UserId", newName: "ApplicationUser_Id");
        }
    }
}
