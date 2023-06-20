﻿namespace WebWatchOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatePay : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_Order", "TypePayment", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tb_Order", "TypePayment");
        }
    }
}
