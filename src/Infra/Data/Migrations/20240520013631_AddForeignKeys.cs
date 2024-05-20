using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class AddForeignKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Tarefas_IdUsuario",
                table: "Tarefas",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_SubTarefas_IdTarefa",
                table: "SubTarefas",
                column: "IdTarefa");

            migrationBuilder.AddForeignKey(
                name: "FK_SubTarefas_Tarefas_IdTarefa",
                table: "SubTarefas",
                column: "IdTarefa",
                principalTable: "Tarefas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tarefas_Usuarios_IdUsuario",
                table: "Tarefas",
                column: "IdUsuario",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubTarefas_Tarefas_IdTarefa",
                table: "SubTarefas");

            migrationBuilder.DropForeignKey(
                name: "FK_Tarefas_Usuarios_IdUsuario",
                table: "Tarefas");

            migrationBuilder.DropIndex(
                name: "IX_Tarefas_IdUsuario",
                table: "Tarefas");

            migrationBuilder.DropIndex(
                name: "IX_SubTarefas_IdTarefa",
                table: "SubTarefas");
        }
    }
}
