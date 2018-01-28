namespace BlockchainData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Blocks",
                c => new
                    {
                        BlockId = c.Int(nullable: false, identity: true),
                        Version = c.Int(nullable: false),
                        Code = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        Hash = c.String(),
                        PreviousHash = c.String(),
                        Data = c.String(),
                        User = c.String(),
                    })
                .PrimaryKey(t => t.BlockId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Blocks");
        }
    }
}
