using System.Data.Entity.Migrations;

namespace LibData.Migrations
{
    public partial class Inicial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                    "VLD.Archivo",
                    c => new
                    {
                        ArchivoId = c.Long(false, true),
                        Nombre = c.String(false, 50, unicode: false),
                        Descripcion = c.String(maxLength: 1024, unicode: false),
                        CantidadColumnas = c.Int(false),
                        Separador = c.String(false, 5),
                        Estado = c.Boolean(false),
                        FechaSistema = c.DateTime(false)
                    })
                .PrimaryKey(t => t.ArchivoId);

            CreateTable(
                    "VLD.Columna",
                    c => new
                    {
                        ColumnaId = c.Long(false, true),
                        Nombre = c.String(false, 20, unicode: false),
                        Descripcion = c.String(false, 20, unicode: false),
                        TipoDato = c.Int(false),
                        Orden = c.Short(false),
                        Estado = c.Boolean(false),
                        FechaSistema = c.DateTime(false),
                        RestriccionesColumnaId = c.Long(false)
                    })
                .PrimaryKey(t => t.ColumnaId)
                .ForeignKey("VLD.RestriccionesColumna", t => t.RestriccionesColumnaId, false)
                .Index(t => t.RestriccionesColumnaId);

            CreateTable(
                    "VLD.RestriccionesColumna",
                    c => new
                    {
                        RestriccionesColumnaId = c.Long(false, true),
                        ComunNombre = c.String(false, 20, unicode: false),
                        ComunDescripcion = c.String(false, 256, unicode: false),
                        ComunEstado = c.Boolean(false),
                        ComunFechaSitema = c.DateTime(false),
                        IntMinInclusive = c.Int(),
                        IntMaxInclusive = c.Int(),
                        IntPattern = c.String(),
                        IntWhiteSpace = c.Boolean(false),
                        StringLength = c.String(),
                        StringMaxLength = c.String(),
                        StringMinLength = c.String(),
                        StringPattern = c.String(),
                        StringWhiteSpace = c.Boolean(false),
                        DecimalFractionDigits = c.Int(),
                        DecimalMaxInclusive = c.String(),
                        DecimalMinInclusive = c.String(),
                        DecimalPattern = c.String(),
                        DecimalTotalDigits = c.Int(),
                        DecimalWhiteSpace = c.Boolean(false),
                        DateMaxInclusive = c.String(),
                        DateMinInclusive = c.String(),
                        DatePattern = c.String(),
                        DateWhiteSpace = c.Boolean(false)
                    })
                .PrimaryKey(t => t.RestriccionesColumnaId);
        }

        public override void Down()
        {
            DropForeignKey("VLD.Columna", "RestriccionesColumnaId", "VLD.RestriccionesColumna");
            DropIndex("VLD.Columna", new[] {"RestriccionesColumnaId"});
            DropTable("VLD.RestriccionesColumna");
            DropTable("VLD.Columna");
            DropTable("VLD.Archivo");
        }
    }
}