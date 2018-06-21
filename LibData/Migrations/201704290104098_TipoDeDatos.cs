namespace LibData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TipoDeDatos : DbMigration
    {
        public override void Up()
        {
            AddColumn("VLD.RestriccionesColumna", "TipoDato", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("VLD.RestriccionesColumna", "TipoDato");
        }
    }
}
