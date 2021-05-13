using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Task1.Migrations
{
    public partial class initialsetup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SAMOCHOD",
                columns: table => new
                {
                    samochod_id = table.Column<int>(type: "int", nullable: false),
                    pojemnosc = table.Column<decimal>(type: "numeric(5,3)", nullable: false),
                    cena = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SAMOCHOD", x => x.samochod_id);
                });

            migrationBuilder.CreateTable(
                name: "OSOBA",
                columns: table => new
                {
                    osoba_id = table.Column<int>(type: "int", nullable: false),
                    imie = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: false),
                    nazwisko = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: false),
                    samochod_id = table.Column<int>(type: "int", nullable: false),
                    data_prod = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OSOBA", x => x.osoba_id);
                    table.ForeignKey(
                        name: "FK__OSOBY__samochod___2B3F6F97",
                        column: x => x.samochod_id,
                        principalTable: "SAMOCHOD",
                        principalColumn: "samochod_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OSOBA_samochod_id",
                table: "OSOBA",
                column: "samochod_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OSOBA");

            migrationBuilder.DropTable(
                name: "SAMOCHOD");
        }
    }
}
