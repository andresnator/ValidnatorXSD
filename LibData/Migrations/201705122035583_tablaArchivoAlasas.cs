namespace LibData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tablaArchivoAlasas : DbMigration
    {
        public override void Up()
        {
            AddColumn("VLD.ArchivosProgramados", "EstadosValidacion", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("VLD.ArchivosProgramados", "EstadosValidacion");
        }
    }
}
