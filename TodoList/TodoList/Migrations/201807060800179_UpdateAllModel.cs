namespace TodoList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateAllModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ToDos", "CategoryID", "dbo.Categories");
            DropPrimaryKey("dbo.Categories");
            DropPrimaryKey("dbo.ToDos");
            AddColumn("dbo.Categories", "CreatedAt", c => c.DateTime(nullable: false, defaultValueSql: "getdate()"));
            AddColumn("dbo.Categories", "Deleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Categories", "DeletedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.ToDos", "CreatedAt", c => c.DateTime(nullable: false, defaultValueSql: "getdate()"));
            AddColumn("dbo.ToDos", "Deleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.ToDos", "DeletedAt", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Categories", "ID", c => c.Int(nullable: false));
            AlterColumn("dbo.ToDos", "ID", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Categories", "ID");
            AddPrimaryKey("dbo.ToDos", "ID");
            AddForeignKey("dbo.ToDos", "CategoryID", "dbo.Categories", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ToDos", "CategoryID", "dbo.Categories");
            DropPrimaryKey("dbo.ToDos");
            DropPrimaryKey("dbo.Categories");
            AlterColumn("dbo.ToDos", "ID", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Categories", "ID", c => c.Int(nullable: false, identity: true));
            DropColumn("dbo.ToDos", "DeletedAt");
            DropColumn("dbo.ToDos", "Deleted");
            DropColumn("dbo.ToDos", "CreatedAt");
            DropColumn("dbo.Categories", "DeletedAt");
            DropColumn("dbo.Categories", "Deleted");
            DropColumn("dbo.Categories", "CreatedAt");
            AddPrimaryKey("dbo.ToDos", "ID");
            AddPrimaryKey("dbo.Categories", "ID");
            AddForeignKey("dbo.ToDos", "CategoryID", "dbo.Categories", "ID", cascadeDelete: true);
        }
    }
}
