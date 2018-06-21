namespace LibData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EliminarTipoDatoCOlumna : DbMigration
    {
        public override void Up()
        {
            DropColumn("VLD.Columna", "TipoDato");
        }
        
        public override void Down()
        {
            AddColumn("VLD.Columna", "TipoDato", c => c.Int(nullable: false));
        }
    }
}
