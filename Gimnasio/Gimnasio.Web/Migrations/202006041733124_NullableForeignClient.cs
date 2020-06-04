namespace Gimnasio.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NullableForeignClient : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Clients", "CoachId", "dbo.Coaches");
            DropForeignKey("dbo.Clients", "NutritionistId", "dbo.Nutritionists");
            DropIndex("dbo.Clients", new[] { "CoachId" });
            DropIndex("dbo.Clients", new[] { "NutritionistId" });
            AlterColumn("dbo.Clients", "CoachId", c => c.Int());
            AlterColumn("dbo.Clients", "NutritionistId", c => c.Int());
            CreateIndex("dbo.Clients", "CoachId");
            CreateIndex("dbo.Clients", "NutritionistId");
            AddForeignKey("dbo.Clients", "CoachId", "dbo.Coaches", "Id");
            AddForeignKey("dbo.Clients", "NutritionistId", "dbo.Nutritionists", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Clients", "NutritionistId", "dbo.Nutritionists");
            DropForeignKey("dbo.Clients", "CoachId", "dbo.Coaches");
            DropIndex("dbo.Clients", new[] { "NutritionistId" });
            DropIndex("dbo.Clients", new[] { "CoachId" });
            AlterColumn("dbo.Clients", "NutritionistId", c => c.Int(nullable: false));
            AlterColumn("dbo.Clients", "CoachId", c => c.Int(nullable: false));
            CreateIndex("dbo.Clients", "NutritionistId");
            CreateIndex("dbo.Clients", "CoachId");
            AddForeignKey("dbo.Clients", "NutritionistId", "dbo.Nutritionists", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Clients", "CoachId", "dbo.Coaches", "Id", cascadeDelete: true);
        }
    }
}
