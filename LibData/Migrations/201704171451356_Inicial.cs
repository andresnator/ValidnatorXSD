namespace LibData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Inicial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "VLD.Archivo",
                c => new
                    {
                        ArchivoId = c.Long(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 50, unicode: false),
                        Descripcion = c.String(maxLength: 1024, unicode: false),
                        CantidadColumnas = c.Int(nullable: false),
                        Separador = c.String(nullable: false, maxLength: 5),
                        Estado = c.Boolean(nullable: false),
                        FechaSistema = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ArchivoId);
            
            CreateTable(
                "VLD.Columna",
                c => new
                    {
                        ColumnaId = c.Long(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 20, unicode: false),
                        Descripcion = c.String(nullable: false, maxLength: 20, unicode: false),
                        TipoDato = c.Int(nullable: false),
                        Orden = c.Short(nullable: false),
                        Estado = c.Boolean(nullable: false),
                        FechaSistema = c.DateTime(nullable: false),
                        RestriccionesColumnaId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.ColumnaId)
                .ForeignKey("VLD.RestriccionesColumna", t => t.RestriccionesColumnaId, cascadeDelete: false)
                .Index(t => t.RestriccionesColumnaId);
            
            CreateTable(
                "VLD.RestriccionesColumna",
                c => new
                    {
                        RestriccionesColumnaId = c.Long(nullable: false, identity: true),
                        ComunNombre = c.String(nullable: false, maxLength: 20, unicode: false),
                        ComunDescripcion = c.String(nullable: false, maxLength: 256, unicode: false),
                        ComunEstado = c.Boolean(nullable: false),
                        ComunFechaSitema = c.DateTime(nullable: false),
                        IntMinInclusive = c.Int(),
                        IntMaxInclusive = c.Int(),
                        IntPattern = c.String(),
                        IntWhiteSpace = c.Boolean(nullable: false),
                        StringLength = c.String(),
                        StringMaxLength = c.String(),
                        StringMinLength = c.String(),
                        StringPattern = c.String(),
                        StringWhiteSpace = c.Boolean(nullable: false),
                        DecimalFractionDigits = c.Int(),
                        DecimalMaxInclusive = c.String(),
                        DecimalMinInclusive = c.String(),
                        DecimalPattern = c.String(),
                        DecimalTotalDigits = c.Int(),
                        DecimalWhiteSpace = c.Boolean(nullable: false),
                        DateMaxInclusive = c.String(),
                        DateMinInclusive = c.String(),
                        DatePattern = c.String(),
                        DateWhiteSpace = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.RestriccionesColumnaId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("VLD.Columna", "RestriccionesColumnaId", "VLD.RestriccionesColumna");
            DropIndex("VLD.Columna", new[] { "RestriccionesColumnaId" });
            DropTable("VLD.RestriccionesColumna");
            DropTable("VLD.Columna");
            DropTable("VLD.Archivo");
        }
    }
}
