namespace LibData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tablaArchivoAlter : DbMigration
    {
        public override void Up()
        {
            AlterColumn("VLD.ArchivosProgramados", "UrlArchivoCargado", c => c.String(nullable: false, maxLength: 400, unicode: false));
            AlterColumn("VLD.ArchivosProgramados", "UrlArchivoErrores", c => c.String(maxLength: 400, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("VLD.ArchivosProgramados", "UrlArchivoErrores", c => c.String(maxLength: 300));
            AlterColumn("VLD.ArchivosProgramados", "UrlArchivoCargado", c => c.String(nullable: false, maxLength: 300, unicode: false));
        }
    }
}
