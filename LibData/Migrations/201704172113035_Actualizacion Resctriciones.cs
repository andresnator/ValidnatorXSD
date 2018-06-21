using System.Data.Entity.Migrations;

namespace LibData.Migrations
{
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
            AlterColumn("VLD.RestriccionesColumna", "DateWhiteSpace", c => c.Boolean(false));
            AlterColumn("VLD.RestriccionesColumna", "DecimalWhiteSpace", c => c.Boolean(false));
            AlterColumn("VLD.RestriccionesColumna", "StringWhiteSpace", c => c.Boolean(false));
            AlterColumn("VLD.RestriccionesColumna", "IntWhiteSpace", c => c.Boolean(false));
        }
    }
}