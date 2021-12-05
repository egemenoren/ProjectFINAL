namespace Project.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class humidityhistorychangedtopublicprops : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HumidityHistories", "PlantId", c => c.Int(nullable: false));
            AddColumn("dbo.HumidityHistories", "HumidityRate", c => c.Double(nullable: false));
            AddColumn("dbo.HumidityHistories", "Temperature", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.HumidityHistories", "Temperature");
            DropColumn("dbo.HumidityHistories", "HumidityRate");
            DropColumn("dbo.HumidityHistories", "PlantId");
        }
    }
}
