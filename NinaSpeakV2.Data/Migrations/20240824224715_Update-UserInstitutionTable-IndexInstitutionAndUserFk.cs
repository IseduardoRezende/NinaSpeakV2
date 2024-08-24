using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NinaSpeakV2.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserInstitutionTableIndexInstitutionAndUserFk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UsuarioInstituicao",
                table: "UsuarioInstituicao");

            migrationBuilder.AddColumn<long>(
                name: "Id",
                table: "UsuarioInstituicao",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsuarioInstituicao",
                table: "UsuarioInstituicao",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioInstituicao_UsuarioFk_InstituicaoFk",
                table: "UsuarioInstituicao",
                columns: new[] { "UsuarioFk", "InstituicaoFk" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UsuarioInstituicao",
                table: "UsuarioInstituicao");

            migrationBuilder.DropIndex(
                name: "IX_UsuarioInstituicao_UsuarioFk_InstituicaoFk",
                table: "UsuarioInstituicao");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UsuarioInstituicao");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsuarioInstituicao",
                table: "UsuarioInstituicao",
                columns: new[] { "UsuarioFk", "InstituicaoFk" });
        }
    }
}
