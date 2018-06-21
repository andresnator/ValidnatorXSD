namespace LibData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ActualizacionResctriciones : DbMigration
    {
        public override void Up()
        {
            AlterColumn("VLD.RestriccionesColumna", "IntWhiteSpace", c => c.Boolean());
            AlterColumn("VLD.RestriccionesColumna", "StringWhiteSpace", c => c.Boolean());
            AlterColumn("VLD.RestriccionesColumna", "DecimalWhiteSpace", c => c.Boolean());
            AlterColumn("VLD.RestriccionesColumna", "DateWhiteSpace", c => c.Boolean());
        }
        
        public override void Down()
        {
            AlterColumn("VLD.RestriccionesColumna", "DateWhiteSpace", c => c.Boolean(nullable: false));
            AlterColumn("VLD.RestriccionesColumna", "DecimalWhiteSpace", c => c.Boolean(nullable: false));
            AlterColumn("VLD.RestriccionesColumna", "StringWhiteSpace", c => c.Boolean(nullable: false));
            AlterColumn("VLD.RestriccionesColumna", "IntWhiteSpace", c => c.Boolean(nullable: false));
        }
    }
}
