using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniGuard.Web.Migrations
{
    /// <inheritdoc />
    public partial class BaseDeDatosLimpiaFinal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Equipos_Estudiantes_EstudianteId",
                table: "Equipos");

            migrationBuilder.DropForeignKey(
                name: "FK_Movimientos_Asignaciones_AsignacionId",
                table: "Movimientos");

            migrationBuilder.DropColumn(
                name: "EstaEnCampus",
                table: "Equipos");

            migrationBuilder.DropColumn(
                name: "Marca",
                table: "Equipos");

            migrationBuilder.DropColumn(
                name: "PropietarioNombre",
                table: "Equipos");

            migrationBuilder.RenameColumn(
                name: "AsignacionId",
                table: "Movimientos",
                newName: "EstudianteId");

            migrationBuilder.RenameIndex(
                name: "IX_Movimientos_AsignacionId",
                table: "Movimientos",
                newName: "IX_Movimientos_EstudianteId");

            migrationBuilder.AddColumn<int>(
                name: "EquipoId",
                table: "Movimientos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioSistemaId",
                table: "Movimientos",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Modelo",
                table: "Equipos",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "EstudianteId",
                table: "Equipos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EstadoActualId",
                table: "Equipos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MarcaId",
                table: "Equipos",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Estados",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Nombre", "TipoEntidad" },
                values: new object[] { "Dentro", "Equipo" });

            migrationBuilder.UpdateData(
                table: "Estados",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Nombre", "TipoEntidad" },
                values: new object[] { "Fuera", "Equipo" });

            migrationBuilder.UpdateData(
                table: "Estados",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Nombre", "TipoEntidad" },
                values: new object[] { "Activo", "Estudiante" });

            migrationBuilder.InsertData(
                table: "Estados",
                columns: new[] { "Id", "Nombre", "TipoEntidad" },
                values: new object[] { 4, "Inactivo", "Estudiante" });

            migrationBuilder.InsertData(
                table: "MarcasEquipos",
                columns: new[] { "Id", "Nombre" },
                values: new object[] { 6, "Acer" });

            migrationBuilder.CreateIndex(
                name: "IX_Movimientos_EquipoId",
                table: "Movimientos",
                column: "EquipoId");

            migrationBuilder.CreateIndex(
                name: "IX_Movimientos_UsuarioSistemaId",
                table: "Movimientos",
                column: "UsuarioSistemaId");

            migrationBuilder.CreateIndex(
                name: "IX_Equipos_EstadoActualId",
                table: "Equipos",
                column: "EstadoActualId");

            migrationBuilder.CreateIndex(
                name: "IX_Equipos_MarcaId",
                table: "Equipos",
                column: "MarcaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Equipos_Estados_EstadoActualId",
                table: "Equipos",
                column: "EstadoActualId",
                principalTable: "Estados",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Equipos_Estudiantes_EstudianteId",
                table: "Equipos",
                column: "EstudianteId",
                principalTable: "Estudiantes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Equipos_MarcasEquipos_MarcaId",
                table: "Equipos",
                column: "MarcaId",
                principalTable: "MarcasEquipos",
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

            migrationBuilder.AddForeignKey(
                name: "FK_Movimientos_UsuariosSistema_UsuarioSistemaId",
                table: "Movimientos",
                column: "UsuarioSistemaId",
                principalTable: "UsuariosSistema",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Equipos_Estados_EstadoActualId",
                table: "Equipos");

            migrationBuilder.DropForeignKey(
                name: "FK_Equipos_Estudiantes_EstudianteId",
                table: "Equipos");

            migrationBuilder.DropForeignKey(
                name: "FK_Equipos_MarcasEquipos_MarcaId",
                table: "Equipos");

            migrationBuilder.DropForeignKey(
                name: "FK_Movimientos_Equipos_EquipoId",
                table: "Movimientos");

            migrationBuilder.DropForeignKey(
                name: "FK_Movimientos_Estudiantes_EstudianteId",
                table: "Movimientos");

            migrationBuilder.DropForeignKey(
                name: "FK_Movimientos_UsuariosSistema_UsuarioSistemaId",
                table: "Movimientos");

            migrationBuilder.DropIndex(
                name: "IX_Movimientos_EquipoId",
                table: "Movimientos");

            migrationBuilder.DropIndex(
                name: "IX_Movimientos_UsuarioSistemaId",
                table: "Movimientos");

            migrationBuilder.DropIndex(
                name: "IX_Equipos_EstadoActualId",
                table: "Equipos");

            migrationBuilder.DropIndex(
                name: "IX_Equipos_MarcaId",
                table: "Equipos");

            migrationBuilder.DeleteData(
                table: "Estados",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "MarcasEquipos",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DropColumn(
                name: "EquipoId",
                table: "Movimientos");

            migrationBuilder.DropColumn(
                name: "UsuarioSistemaId",
                table: "Movimientos");

            migrationBuilder.DropColumn(
                name: "EstadoActualId",
                table: "Equipos");

            migrationBuilder.DropColumn(
                name: "MarcaId",
                table: "Equipos");

            migrationBuilder.RenameColumn(
                name: "EstudianteId",
                table: "Movimientos",
                newName: "AsignacionId");

            migrationBuilder.RenameIndex(
                name: "IX_Movimientos_EstudianteId",
                table: "Movimientos",
                newName: "IX_Movimientos_AsignacionId");

            migrationBuilder.AlterColumn<string>(
                name: "Modelo",
                table: "Equipos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EstudianteId",
                table: "Equipos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<bool>(
                name: "EstaEnCampus",
                table: "Equipos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Marca",
                table: "Equipos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PropietarioNombre",
                table: "Equipos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Estados",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Nombre", "TipoEntidad" },
                values: new object[] { "Activo", "General" });

            migrationBuilder.UpdateData(
                table: "Estados",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Nombre", "TipoEntidad" },
                values: new object[] { "Inactivo", "General" });

            migrationBuilder.UpdateData(
                table: "Estados",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Nombre", "TipoEntidad" },
                values: new object[] { "Robado", "Equipo" });

            migrationBuilder.AddForeignKey(
                name: "FK_Equipos_Estudiantes_EstudianteId",
                table: "Equipos",
                column: "EstudianteId",
                principalTable: "Estudiantes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Movimientos_Asignaciones_AsignacionId",
                table: "Movimientos",
                column: "AsignacionId",
                principalTable: "Asignaciones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
