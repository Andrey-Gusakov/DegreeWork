namespace DegreeWork.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LastTrainingTimeToLastAccessTime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RecordStatistics", "LastAccessTime", c => c.DateTime(nullable: false));
            DropColumn("dbo.RecordStatistics", "LastTrainingTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RecordStatistics", "LastTrainingTime", c => c.DateTime(nullable: false));
            DropColumn("dbo.RecordStatistics", "LastAccessTime");
        }
    }
}
