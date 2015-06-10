using System.Collections.Generic;
using Microsoft.Data.Entity.Relational.Migrations;
using Microsoft.Data.Entity.Relational.Migrations.Builders;
using Microsoft.Data.Entity.Relational.Migrations.Operations;

namespace RedStar.Invoicing.Migrations
{
    public partial class v002 : Migration
    {
        public override void Up(MigrationBuilder migration)
        {
            migration.AlterColumn(
                name: "UserId",
                table: "UserSettings",
                type: "nvarchar(max)",
                nullable: true);
        }
        
        public override void Down(MigrationBuilder migration)
        {
            migration.AlterColumn(
                name: "UserId",
                table: "UserSettings",
                type: "int",
                nullable: false);
        }
    }
}
