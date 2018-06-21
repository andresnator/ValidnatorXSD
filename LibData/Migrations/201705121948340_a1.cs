namespace LibData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("VLD.Columna", "Descripcion");
        }
        
        public override void Down()
        {
            AddColumn("VLD.Columna", "Descripcion", c => c.String(nullable: false, maxLength: 1024, unicode: false));
        }
    }
}
