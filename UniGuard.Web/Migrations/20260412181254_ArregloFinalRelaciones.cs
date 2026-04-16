using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniGuard.Web.Migrations
{
    /// <inheritdoc />
    public partial class ArregloFinalRelaciones : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Equipos_Estudiantes_EstudianteId",
                table: "Equipos");

            migrationBuilder.DropForeignKey(
                name: "FK_Movimientos_Equipos_EquipoId",
                table: "Movimientos");

            migrationBuilder.DropForeignKey(
                name: "FK_Movimientos_Estudiantes_EstudianteId",
                table: "Movimientos");

            migrationBuilder.AddForeignKey(
                name: "FK_Equipos_Estudiantes_EstudianteId",
                table: "Equipos",
                column: "EstudianteId",
                principalTable: "Estudiantes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Movimientos_Equipos_EquipoId",
                table: "Movimientos",
                column: "EquipoId",
                principalTable: "Equipos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Movimientos_Estudiantes_EstudianteId",
                table: "Movimientos",
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

            migrationBuilder.DropForeignKey(
                name: "FK_Movimientos_Equipos_EquipoId",
                table: "Movimientos");

            migrationBuilder.DropForeignKey(
                name: "FK_Movimientos_Estudiantes_EstudianteId",
                table: "Movimientos");

            migrationBuilder.AddForeignKey(
                name: "FK_Equipos_Estudiantes_EstudianteId",
                table: "Equipos",
                column: "EstudianteId",
                principalTable: "Estudiantes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Movimientos_Equipos_EquipoId",
                table: "Movimientos",
                column: "EquipoId",
                principalTable: "Equipos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Movimientos_Estudiantes_EstudianteId",
                table: "Movimientos",
                column: "EstudianteId",
                principalTable: "Estudiantes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
