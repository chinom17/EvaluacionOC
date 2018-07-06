using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EvaluacionOC.Data.Migrations
{
    public partial class cambioCampoPassword : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordEncriptado",
                table: "Usuario");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Usuario",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Usuario");

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordEncriptado",
                table: "Usuario",
                nullable: false,
                defaultValue: new byte[] {  });
        }
    }
}
