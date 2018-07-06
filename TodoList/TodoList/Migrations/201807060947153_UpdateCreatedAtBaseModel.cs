namespace TodoList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCreatedAtBaseModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ToDos", "CategoryID", "dbo.Categories");
            DropPrimaryKey("dbo.Categories");
            DropPrimaryKey("dbo.ToDos");
            AlterColumn("dbo.Categories", "ID", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Categories", "CreatedAt", c => c.DateTime(nullable: false, defaultValueSql: "getdate()"));
            AlterColumn("dbo.ToDos", "ID", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.ToDos", "CreatedAt", c => c.DateTime(nullable: false, defaultValueSql: "getdate()"));
            AddPrimaryKey("dbo.Categories", "ID");
            AddPrimaryKey("dbo.ToDos", "ID");
            AddForeignKey("dbo.ToDos", "CategoryID", "dbo.Categories", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ToDos", "CategoryID", "dbo.Categories");
            DropPrimaryKey("dbo.ToDos");
            DropPrimaryKey("dbo.Categories");
            AlterColumn("dbo.ToDos", "CreatedAt", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ToDos", "ID", c => c.Int(nullable: false));
            AlterColumn("dbo.Categories", "CreatedAt", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Categories", "ID", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.ToDos", "ID");
            AddPrimaryKey("dbo.Categories", "ID");
            AddForeignKey("dbo.ToDos", "CategoryID", "dbo.Categories", "ID", cascadeDelete: true);
        }
    }
}
