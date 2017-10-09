using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Vega.Migrations
{
    public partial class SeedDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Makes (Name) VALUES ('Ford')");
            migrationBuilder.Sql("INSERT INTO Makes (Name) VALUES ('Kia')");
            migrationBuilder.Sql("INSERT INTO Makes (Name) VALUES ('Fiat')");

            migrationBuilder.Sql("INSERT INTO Models (Name, MakeId) VALUES ('Focus',(SELECT ID FROM Makes WHERE Name='Ford'))");
            migrationBuilder.Sql("INSERT INTO Models (Name, MakeId) VALUES ('Kuga',(SELECT ID FROM Makes WHERE Name='Ford'))");
            migrationBuilder.Sql("INSERT INTO Models (Name, MakeId) VALUES ('Fiesta',(SELECT ID FROM Makes WHERE Name='Ford'))");
            migrationBuilder.Sql("INSERT INTO Models (Name, MakeId) VALUES ('Mondeo',(SELECT ID FROM Makes WHERE Name='Ford'))");

            migrationBuilder.Sql("INSERT INTO Models (Name, MakeId) VALUES ('Ceed',(SELECT ID FROM Makes WHERE Name='Kia'))");
            migrationBuilder.Sql("INSERT INTO Models (Name, MakeId) VALUES ('Optima',(SELECT ID FROM Makes WHERE Name='Kia'))");
            migrationBuilder.Sql("INSERT INTO Models (Name, MakeId) VALUES ('Sportage',(SELECT ID FROM Makes WHERE Name='Kia'))");

            migrationBuilder.Sql("INSERT INTO Models (Name, MakeId) VALUES ('Panda',(SELECT ID FROM Makes WHERE Name='Fiat'))");
            migrationBuilder.Sql("INSERT INTO Models (Name, MakeId) VALUES ('500',(SELECT ID FROM Makes WHERE Name='Fiat'))");
            migrationBuilder.Sql("INSERT INTO Models (Name, MakeId) VALUES ('Pubto',(SELECT ID FROM Makes WHERE Name='Fiat'))");
            migrationBuilder.Sql("INSERT INTO Models (Name, MakeId) VALUES ('Tipo',(SELECT ID FROM Makes WHERE Name='Fiat'))");
            migrationBuilder.Sql("INSERT INTO Models (Name, MakeId) VALUES ('126p',(SELECT ID FROM Makes WHERE Name='Fiat'))");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Makes WHERE Name IN ('Ford','Kia','Fiat')");
        }
    }
}
