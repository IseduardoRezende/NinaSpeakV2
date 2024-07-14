using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NinaSpeakV2.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateInstitutionTableCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Codigo",
                table: "Instituicao",
                type: "nvarchar(8)",
                maxLength: 8,
                nullable: false,
                defaultValueSql: "LEFT(LOWER(NEWID()), 8)",
                oldClrType: typeof(string),
                oldType: "nvarchar(8)",
                oldMaxLength: 8,
                oldDefaultValue: "da90d7c8");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Codigo",
                table: "Instituicao",
                type: "nvarchar(8)",
                maxLength: 8,
                nullable: false,
                defaultValue: "da90d7c8",
                oldClrType: typeof(string),
                oldType: "nvarchar(8)",
                oldMaxLength: 8,
                oldDefaultValueSql: "LEFT(LOWER(NEWID()), 8)");
        }
    }
}
