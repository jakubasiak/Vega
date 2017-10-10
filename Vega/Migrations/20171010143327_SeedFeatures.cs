using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Vega.Migrations
{
    public partial class SeedFeatures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Features (Name) VALUES ('Air conditioning')");
            migrationBuilder.Sql("INSERT INTO Features (Name) VALUES ('Electric windows')");
            migrationBuilder.Sql("INSERT INTO Features (Name) VALUES ('Radio')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Features WHERE Name IN ('Air conditioning','Electric windows','Radio')");
        }
    }
}
