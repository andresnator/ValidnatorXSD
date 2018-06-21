namespace LibData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tablaArchisasasas : DbMigration
    {
        public override void Up()
        {
            AlterColumn("VLD.ArchivosProgramados", "EstadosValidacion", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("VLD.ArchivosProgramados", "EstadosValidacion", c => c.Int(nullable: false));
        }
    }
}
