namespace DegreeWork.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Training_ColumnAdded_Name : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Trainings", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Trainings", "Name");
        }
    }
}
