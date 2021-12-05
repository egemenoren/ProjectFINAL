namespace Project.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Plants",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        RequiredHumidityRate = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        CreatedTime = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Mail = c.String(),
                        Name = c.String(),
                        Surname = c.String(),
                        LastLoginIP = c.String(),
                        Password = c.String(),
                        SecurityQuestion = c.String(),
                        SecurityAnswer = c.String(),
                        PhoneNumber = c.String(),
                        CreatedTime = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.WateringHistories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PlantId = c.Int(nullable: false),
                        LastHumidityRate = c.Int(nullable: false),
                        CreatedTime = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.WateringHistories");
            DropTable("dbo.Users");
            DropTable("dbo.Plants");
        }
    }
}
