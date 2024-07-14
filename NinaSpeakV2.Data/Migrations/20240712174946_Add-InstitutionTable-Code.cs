using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NinaSpeakV2.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddInstitutionTableCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Codigo",
                table: "Instituicao",
                type: "nvarchar(8)",
                maxLength: 8,
                nullable: false,
                defaultValue: "da90d7c8");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Codigo",
                table: "Instituicao");
        }
    }
}
