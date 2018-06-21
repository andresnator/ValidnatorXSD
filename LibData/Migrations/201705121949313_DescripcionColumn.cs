namespace LibData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DescripcionColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("VLD.Columna", "Descripcion", c => c.String(nullable: false, maxLength: 1024, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("VLD.Columna", "Descripcion");
        }
    }
}
