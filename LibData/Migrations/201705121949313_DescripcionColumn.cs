using System.Data.Entity.Migrations;

namespace LibData.Migrations
{
    public partial class DescripcionColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("VLD.Columna", "Descripcion", c => c.String(false, 1024, unicode: false));
        }

        public override void Down()
        {
            DropColumn("VLD.Columna", "Descripcion");
        }
    }
}