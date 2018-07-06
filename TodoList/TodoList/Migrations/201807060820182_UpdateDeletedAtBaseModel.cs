namespace TodoList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDeletedAtBaseModel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Categories", "DeletedAt", c => c.DateTime());
            AlterColumn("dbo.ToDos", "DeletedAt", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ToDos", "DeletedAt", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Categories", "DeletedAt", c => c.DateTime(nullable: false));
        }
    }
}
