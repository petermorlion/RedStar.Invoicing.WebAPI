using System.Collections.Generic;
using Microsoft.Data.Entity.Relational.Migrations;
using Microsoft.Data.Entity.Relational.Migrations.Builders;
using Microsoft.Data.Entity.Relational.Migrations.Operations;

namespace RedStar.Invoicing.Migrations
{
    public partial class v001 : Migration
    {
        public override void Up(MigrationBuilder migration)
        {
            migration.CreateSequence(
                name: "DefaultSequence",
                type: "bigint",
                startWith: 1L,
                incrementBy: 10);
            migration.CreateTable(
                name: "Invoice",
                columns: table => new
                {
                    Html = table.Column(type: "nvarchar(max)", nullable: true),
                    Id = table.Column(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoice", x => x.Id);
                });
            migration.CreateTable(
                name: "UserSettings",
                columns: table => new
                {
                    Id = table.Column(type: "int", nullable: false),
                    InvoiceTemplate = table.Column(type: "nvarchar(max)", nullable: true),
                    LogoUrl = table.Column(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSettings", x => x.Id);
                });
        }
        
        public override void Down(MigrationBuilder migration)
        {
            migration.DropSequence("DefaultSequence");
            migration.DropTable("Invoice");
            migration.DropTable("UserSettings");
        }
    }
}
