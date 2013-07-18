namespace NuGetGallery.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PackageMetadataEditable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "PackageMetadatas",
                c => new
                    {
                        Key = c.Int(nullable: false),
                        PackageKey = c.Int(nullable: false),
                        UserKey = c.Int(nullable: false),
                        EditName = c.String(maxLength: 64),
                        Timestamp = c.DateTime(nullable: false),
                        IsCompleted = c.Boolean(nullable: false),
                        TriedCount = c.Int(nullable: false),
                        Authors = c.String(),
                        Copyright = c.String(),
                        Description = c.String(),
                        Hash = c.String(),
                        HashAlgorithm = c.String(maxLength: 10),
                        IconUrl = c.String(),
                        LicenseUrl = c.String(),
                        PackageFileSize = c.Long(nullable: false),
                        ProjectUrl = c.String(),
                        ReleaseNotes = c.String(),
                        Summary = c.String(),
                        Tags = c.String(),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.Key)
                .ForeignKey("Packages", t => t.PackageKey, cascadeDelete: true)
                .ForeignKey("Users", t => t.UserKey, cascadeDelete: true)
                .Index(t => t.PackageKey)
                .Index(t => t.UserKey)
                .Index(t => t.Key);

            AddColumn("Packages", "MetadataKey", c => c.Int());
            AddForeignKey("Packages", "MetadataKey", "PackageMetadatas");
        }
        
        public override void Down()
        {
            DropIndex("PackageMetadatas", new[] { "Key" });
            DropIndex("PackageMetadatas", new[] { "UserKey" });
            DropIndex("PackageMetadatas", new[] { "PackageKey" });
            DropForeignKey("PackageMetadatas", "UserKey", "Users");
            DropForeignKey("PackageMetadatas", "PackageKey", "Packages");
            DropForeignKey("Packages", "MetadataKey", "PackageMetadatas");
            DropColumn("Packages", "MetadataKey");
            DropTable("PackageMetadatas");
        }
    }
}
