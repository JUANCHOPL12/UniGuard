using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniGuard.Web.Migrations
{
    /// <inheritdoc />
    public partial class ModuloEstudiantes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EstudianteId",
                table: "Equipos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Estudiantes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Documento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nombres = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apellidos = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Edad = table.Column<int>(type: "int", nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FotoBase64 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estudiantes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Equipos_EstudianteId",
                table: "Equipos",
                column: "EstudianteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Equipos_Estudiantes_EstudianteId",
                table: "Equipos",
                column: "EstudianteId",
                principalTable: "Estudiantes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Equipos_Estudiantes_EstudianteId",
                table: "Equipos");

            migrationBuilder.DropTable(
                name: "Estudiantes");

            migrationBuilder.DropIndex(
                name: "IX_Equipos_EstudianteId",
                table: "Equipos");

            migrationBuilder.DropColumn(
                name: "EstudianteId",
                table: "Equipos");
        }
    }
}
