using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NinaSpeakV2.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddChatBotTableChatBotTypeFk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ChatBotTipoFk",
                table: "ChatBot",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_ChatBot_ChatBotTipoFk",
                table: "ChatBot",
                column: "ChatBotTipoFk");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatBot_ChatBotTipo_ChatBotTipoFk",
                table: "ChatBot",
                column: "ChatBotTipoFk",
                principalTable: "ChatBotTipo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatBot_ChatBotTipo_ChatBotTipoFk",
                table: "ChatBot");

            migrationBuilder.DropIndex(
                name: "IX_ChatBot_ChatBotTipoFk",
                table: "ChatBot");

            migrationBuilder.DropColumn(
                name: "ChatBotTipoFk",
                table: "ChatBot");
        }
    }
}
