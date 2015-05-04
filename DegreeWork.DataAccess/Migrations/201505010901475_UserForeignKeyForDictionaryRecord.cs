namespace DegreeWork.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserForeignKeyForDictionaryRecord : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DictionaryRecords", "User_Id", "dbo.Users");
            DropIndex("dbo.DictionaryRecords", new[] { "User_Id" });
            RenameColumn(table: "dbo.DictionaryRecords", name: "User_Id", newName: "UserId");
            AlterColumn("dbo.DictionaryRecords", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.DictionaryRecords", "UserId");
            AddForeignKey("dbo.DictionaryRecords", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DictionaryRecords", "UserId", "dbo.Users");
            DropIndex("dbo.DictionaryRecords", new[] { "UserId" });
            AlterColumn("dbo.DictionaryRecords", "UserId", c => c.Int());
            RenameColumn(table: "dbo.DictionaryRecords", name: "UserId", newName: "User_Id");
            CreateIndex("dbo.DictionaryRecords", "User_Id");
            AddForeignKey("dbo.DictionaryRecords", "User_Id", "dbo.Users", "Id");
        }
    }
}
