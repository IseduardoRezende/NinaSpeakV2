using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NinaSpeakV2.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUserInstitutionTableWriter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Escritor",
                table: "UsuarioInstituicao");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Escritor",
                table: "UsuarioInstituicao",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
