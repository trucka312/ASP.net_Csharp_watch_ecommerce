namespace WebWatchOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTableThongke1 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ThongKes", newName: "tb_ThongKe");
            AddColumn("dbo.tb_ThongKe", "SoTruyCap", c => c.Long(nullable: false));
            DropColumn("dbo.tb_ThongKe", "SoTryCap");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tb_ThongKe", "SoTryCap", c => c.Long(nullable: false));
            DropColumn("dbo.tb_ThongKe", "SoTruyCap");
            RenameTable(name: "dbo.tb_ThongKe", newName: "ThongKes");
        }
    }
}
