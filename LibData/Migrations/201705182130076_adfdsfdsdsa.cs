namespace LibData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adfdsfdsdsa : DbMigration
    {
        public override void Up()
        {
            AddColumn("VLD.Archivo", "CantidadColumnasHabilitadas", c => c.Int(nullable: false));
            AddColumn("VLD.Archivo", "CantidadColumnasInHabilitadas", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("VLD.Archivo", "CantidadColumnasInHabilitadas");
            DropColumn("VLD.Archivo", "CantidadColumnasHabilitadas");
        }
    }
}
