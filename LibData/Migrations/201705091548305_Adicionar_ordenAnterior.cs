namespace LibData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Adicionar_ordenAnterior : DbMigration
    {
        public override void Up()
        {
            AddColumn("VLD.Columna", "OrdenAnterior", c => c.Short(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("VLD.Columna", "OrdenAnterior");
        }
    }
}
