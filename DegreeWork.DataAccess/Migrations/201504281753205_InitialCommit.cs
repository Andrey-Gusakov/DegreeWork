namespace DegreeWork.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCommit : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DictionaryRecords",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Thesaurus_Id = c.Int(),
                        User_Id = c.Int(),
                        Word_Id = c.Int(),
                        WordImage_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Thesaurus", t => t.Thesaurus_Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .ForeignKey("dbo.Words", t => t.Word_Id)
                .ForeignKey("dbo.WordImages", t => t.WordImage_Id)
                .Index(t => t.Thesaurus_Id)
                .Index(t => t.User_Id)
                .Index(t => t.Word_Id)
                .Index(t => t.WordImage_Id);
            
            CreateTable(
                "dbo.RecordStatistics",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CorrectAnswers = c.Int(nullable: false),
                        TotalAnswers = c.Int(nullable: false),
                        LastTrainingTime = c.DateTime(nullable: false),
                        NextTrainingTime = c.DateTime(nullable: false),
                        Training_Id = c.Int(),
                        DictionaryRecord_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Trainings", t => t.Training_Id)
                .ForeignKey("dbo.DictionaryRecords", t => t.DictionaryRecord_Id)
                .Index(t => t.Training_Id)
                .Index(t => t.DictionaryRecord_Id);
            
            CreateTable(
                "dbo.Trainings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        WidgetName = c.String(),
                        Config = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Thesaurus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Definition = c.String(),
                        Word_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Words", t => t.Word_Id)
                .Index(t => t.Word_Id);
            
            CreateTable(
                "dbo.Translations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Representation = c.String(),
                        Word_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Words", t => t.Word_Id)
                .Index(t => t.Word_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Words",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Representation = c.String(),
                        PronuncationPath = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.WordImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ImagePath = c.String(),
                        Word_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Words", t => t.Word_Id)
                .Index(t => t.Word_Id);
            
            CreateTable(
                "dbo.DictionaryRecordTranslations",
                c => new
                    {
                        DictionaryRecord_Id = c.Int(nullable: false),
                        Translation_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.DictionaryRecord_Id, t.Translation_Id })
                .ForeignKey("dbo.DictionaryRecords", t => t.DictionaryRecord_Id, cascadeDelete: true)
                .ForeignKey("dbo.Translations", t => t.Translation_Id, cascadeDelete: true)
                .Index(t => t.DictionaryRecord_Id)
                .Index(t => t.Translation_Id);
            
            CreateTable(
                "dbo.UserTrainings",
                c => new
                    {
                        User_Id = c.Int(nullable: false),
                        Training_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_Id, t.Training_Id })
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .ForeignKey("dbo.Trainings", t => t.Training_Id, cascadeDelete: true)
                .Index(t => t.User_Id)
                .Index(t => t.Training_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DictionaryRecords", "WordImage_Id", "dbo.WordImages");
            DropForeignKey("dbo.DictionaryRecords", "Word_Id", "dbo.Words");
            DropForeignKey("dbo.WordImages", "Word_Id", "dbo.Words");
            DropForeignKey("dbo.Translations", "Word_Id", "dbo.Words");
            DropForeignKey("dbo.Thesaurus", "Word_Id", "dbo.Words");
            DropForeignKey("dbo.UserTrainings", "Training_Id", "dbo.Trainings");
            DropForeignKey("dbo.UserTrainings", "User_Id", "dbo.Users");
            DropForeignKey("dbo.DictionaryRecords", "User_Id", "dbo.Users");
            DropForeignKey("dbo.DictionaryRecordTranslations", "Translation_Id", "dbo.Translations");
            DropForeignKey("dbo.DictionaryRecordTranslations", "DictionaryRecord_Id", "dbo.DictionaryRecords");
            DropForeignKey("dbo.DictionaryRecords", "Thesaurus_Id", "dbo.Thesaurus");
            DropForeignKey("dbo.RecordStatistics", "DictionaryRecord_Id", "dbo.DictionaryRecords");
            DropForeignKey("dbo.RecordStatistics", "Training_Id", "dbo.Trainings");
            DropIndex("dbo.UserTrainings", new[] { "Training_Id" });
            DropIndex("dbo.UserTrainings", new[] { "User_Id" });
            DropIndex("dbo.DictionaryRecordTranslations", new[] { "Translation_Id" });
            DropIndex("dbo.DictionaryRecordTranslations", new[] { "DictionaryRecord_Id" });
            DropIndex("dbo.WordImages", new[] { "Word_Id" });
            DropIndex("dbo.Translations", new[] { "Word_Id" });
            DropIndex("dbo.Thesaurus", new[] { "Word_Id" });
            DropIndex("dbo.RecordStatistics", new[] { "DictionaryRecord_Id" });
            DropIndex("dbo.RecordStatistics", new[] { "Training_Id" });
            DropIndex("dbo.DictionaryRecords", new[] { "WordImage_Id" });
            DropIndex("dbo.DictionaryRecords", new[] { "Word_Id" });
            DropIndex("dbo.DictionaryRecords", new[] { "User_Id" });
            DropIndex("dbo.DictionaryRecords", new[] { "Thesaurus_Id" });
            DropTable("dbo.UserTrainings");
            DropTable("dbo.DictionaryRecordTranslations");
            DropTable("dbo.WordImages");
            DropTable("dbo.Words");
            DropTable("dbo.Users");
            DropTable("dbo.Translations");
            DropTable("dbo.Thesaurus");
            DropTable("dbo.Trainings");
            DropTable("dbo.RecordStatistics");
            DropTable("dbo.DictionaryRecords");
        }
    }
}
