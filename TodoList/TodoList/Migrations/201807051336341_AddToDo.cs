namespace TodoList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddToDo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ToDos",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Done = c.Boolean(nullable: false),
                        DeadLine = c.DateTime(nullable: false),
                        Description = c.String(),
                        CategoryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Categories", t => t.CategoryID, cascadeDelete: true)
                .Index(t => t.CategoryID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ToDos", "CategoryID", "dbo.Categories");
            DropIndex("dbo.ToDos", new[] { "CategoryID" });
            DropTable("dbo.ToDos");
        }
    }
}
