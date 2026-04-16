using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniGuard.Web.Migrations
{
    /// <inheritdoc />
    public partial class RefuerzoEstructuraPro : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Edad",
                table: "Estudiantes");

            migrationBuilder.DropColumn(
                name: "Telefono",
                table: "Estudiantes");

            migrationBuilder.AlterColumn<string>(
                name: "Direccion",
                table: "Estudiantes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<bool>(
                name: "EstaActivo",
                table: "Estudiantes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstaActivo",
                table: "Estudiantes");

            migrationBuilder.AlterColumn<string>(
                name: "Direccion",
                table: "Estudiantes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Edad",
                table: "Estudiantes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Telefono",
                table: "Estudiantes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
