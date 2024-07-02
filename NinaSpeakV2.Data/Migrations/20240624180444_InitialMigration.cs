using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NinaSpeakV2.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChatBotGenero",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    DataExclusao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Descricao = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatBotGenero", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Instituicao",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Imagem = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    DataExclusao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Descricao = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instituicao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Senha = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Sal = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    DataExclusao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChatBot",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    ChatBotGeneroFk = table.Column<long>(type: "bigint", nullable: false),
                    InstituicaoFk = table.Column<long>(type: "bigint", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    DataExclusao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Descricao = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatBot", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatBot_ChatBotGenero_ChatBotGeneroFk",
                        column: x => x.ChatBotGeneroFk,
                        principalTable: "ChatBotGenero",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChatBot_Instituicao_InstituicaoFk",
                        column: x => x.InstituicaoFk,
                        principalTable: "Instituicao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioInstituicao",
                columns: table => new
                {
                    UsuarioFk = table.Column<long>(type: "bigint", nullable: false),
                    InstituicaoFk = table.Column<long>(type: "bigint", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    DataExclusao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioInstituicao", x => new { x.UsuarioFk, x.InstituicaoFk });
                    table.ForeignKey(
                        name: "FK_UsuarioInstituicao_Instituicao_InstituicaoFk",
                        column: x => x.InstituicaoFk,
                        principalTable: "Instituicao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UsuarioInstituicao_Usuario_UsuarioFk",
                        column: x => x.UsuarioFk,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ChatBotConversa",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChatBotFk = table.Column<long>(type: "bigint", nullable: false),
                    Mensagem = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Resposta = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    DataExclusao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatBotConversa", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatBotConversa_ChatBot_ChatBotFk",
                        column: x => x.ChatBotFk,
                        principalTable: "ChatBot",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChatBot_ChatBotGeneroFk",
                table: "ChatBot",
                column: "ChatBotGeneroFk");

            migrationBuilder.CreateIndex(
                name: "IX_ChatBot_InstituicaoFk",
                table: "ChatBot",
                column: "InstituicaoFk");

            migrationBuilder.CreateIndex(
                name: "IX_ChatBotConversa_ChatBotFk",
                table: "ChatBotConversa",
                column: "ChatBotFk");

            migrationBuilder.CreateIndex(
                name: "IX_ChatBotGenero_Descricao",
                table: "ChatBotGenero",
                column: "Descricao",
                unique: true,
                filter: "[Descricao] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioInstituicao_InstituicaoFk",
                table: "UsuarioInstituicao",
                column: "InstituicaoFk");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatBotConversa");

            migrationBuilder.DropTable(
                name: "UsuarioInstituicao");

            migrationBuilder.DropTable(
                name: "ChatBot");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "ChatBotGenero");

            migrationBuilder.DropTable(
                name: "Instituicao");
        }
    }
}
