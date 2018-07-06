using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EvaluacionOC.Data.Migrations
{
    public partial class password : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordEncriptado",
                table: "Usuario",
                nullable: false,
                defaultValue: new byte[] {  });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordEncriptado",
                table: "Usuario");
        }
    }
}
