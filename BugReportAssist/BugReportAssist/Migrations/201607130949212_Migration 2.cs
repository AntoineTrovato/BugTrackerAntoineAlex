namespace BugReportAssist.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tickets", "DateCreation", c => c.DateTime(nullable: false));
            AddColumn("dbo.Tickets", "DateModification", c => c.DateTime(nullable: false));
            DropColumn("dbo.Tickets", "Date");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tickets", "Date", c => c.DateTime(nullable: false));
            DropColumn("dbo.Tickets", "DateModification");
            DropColumn("dbo.Tickets", "DateCreation");
        }
    }
}
