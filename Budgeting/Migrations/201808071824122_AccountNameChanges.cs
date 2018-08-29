namespace Budgeting.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AccountNameChanges : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SavingsAccounts", "Name", c => c.String(nullable: false, maxLength: 8000, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SavingsAccounts", "Name", c => c.String(nullable: false));
        }
    }
}
