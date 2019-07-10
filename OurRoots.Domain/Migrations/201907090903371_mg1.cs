namespace OurRoots.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mg1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Products", "FolderPath");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "FolderPath", c => c.String(maxLength: 1000));
        }
    }
}
