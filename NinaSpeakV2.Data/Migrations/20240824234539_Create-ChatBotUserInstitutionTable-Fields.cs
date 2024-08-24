using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NinaSpeakV2.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreateChatBotUserInstitutionTableFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChatBotUsuarioInstituicao",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChatBotFk = table.Column<long>(type: "bigint", nullable: false),
                    UsuarioInstituicaoFk = table.Column<long>(type: "bigint", nullable: false),
                    Escritor = table.Column<bool>(type: "bit", nullable: false),
                    Leitor = table.Column<bool>(type: "bit", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    DataExclusao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatBotUsuarioInstituicao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatBotUsuarioInstituicao_ChatBot_ChatBotFk",
                        column: x => x.ChatBotFk,
                        principalTable: "ChatBot",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChatBotUsuarioInstituicao_UsuarioInstituicao_UsuarioInstituicaoFk",
                        column: x => x.UsuarioInstituicaoFk,
                        principalTable: "UsuarioInstituicao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChatBotUsuarioInstituicao_ChatBotFk",
                table: "ChatBotUsuarioInstituicao",
                column: "ChatBotFk");

            migrationBuilder.CreateIndex(
                name: "IX_ChatBotUsuarioInstituicao_UsuarioInstituicaoFk",
                table: "ChatBotUsuarioInstituicao",
                column: "UsuarioInstituicaoFk");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatBotUsuarioInstituicao");
        }
    }
}
