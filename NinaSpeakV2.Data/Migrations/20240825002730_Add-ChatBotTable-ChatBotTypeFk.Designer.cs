﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NinaSpeakV2.Data;

#nullable disable

namespace NinaSpeakV2.Data.Migrations
{
    [DbContext(typeof(NinaSpeakContext))]
    [Migration("20240825002730_Add-ChatBotTable-ChatBotTypeFk")]
    partial class AddChatBotTableChatBotTypeFk
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("NinaSpeakV2.Data.Entities.ChatBot", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("ChatBotGenreFk")
                        .HasColumnType("bigint")
                        .HasColumnName("ChatBotGeneroFk");

                    b.Property<long>("ChatBotTypeFk")
                        .HasColumnType("bigint")
                        .HasColumnName("ChatBotTipoFk");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("DataCriacao")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("DataExclusao");

                    b.Property<string>("Description")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)")
                        .HasColumnName("Descricao");

                    b.Property<long>("InstitutionFk")
                        .HasColumnType("bigint")
                        .HasColumnName("InstituicaoFk");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)")
                        .HasColumnName("Nome");

                    b.HasKey("Id");

                    b.HasIndex("ChatBotGenreFk");

                    b.HasIndex("ChatBotTypeFk");

                    b.HasIndex("InstitutionFk");

                    b.ToTable("ChatBot", (string)null);
                });

            modelBuilder.Entity("NinaSpeakV2.Data.Entities.ChatBotConversation", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("ChatBotFk")
                        .HasColumnType("bigint")
                        .HasColumnName("ChatBotFk");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("DataCriacao")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("DataExclusao");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)")
                        .HasColumnName("Mensagem");

                    b.Property<string>("Response")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)")
                        .HasColumnName("Resposta");

                    b.HasKey("Id");

                    b.HasIndex("ChatBotFk");

                    b.ToTable("ChatBotConversa", (string)null);
                });

            modelBuilder.Entity("NinaSpeakV2.Data.Entities.ChatBotGenre", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("DataCriacao")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("DataExclusao");

                    b.Property<string>("Description")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)")
                        .HasColumnName("Descricao");

                    b.HasKey("Id");

                    b.HasIndex("Description")
                        .IsUnique()
                        .HasFilter("[Descricao] IS NOT NULL");

                    b.ToTable("ChatBotGenero", (string)null);
                });

            modelBuilder.Entity("NinaSpeakV2.Data.Entities.ChatBotType", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("DataCriacao")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("DataExclusao");

                    b.Property<string>("Description")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)")
                        .HasColumnName("Descricao");

                    b.HasKey("Id");

                    b.HasIndex("Description")
                        .IsUnique()
                        .HasFilter("[Descricao] IS NOT NULL");

                    b.ToTable("ChatBotTipo", (string)null);
                });

            modelBuilder.Entity("NinaSpeakV2.Data.Entities.ChatBotUserInstitution", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("ChatBotFk")
                        .HasColumnType("bigint")
                        .HasColumnName("ChatBotFk");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("DataCriacao")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("DataExclusao");

                    b.Property<bool>("Reader")
                        .HasColumnType("bit")
                        .HasColumnName("Leitor");

                    b.Property<long>("UserInstitutionFk")
                        .HasColumnType("bigint")
                        .HasColumnName("UsuarioInstituicaoFk");

                    b.Property<bool>("Writer")
                        .HasColumnType("bit")
                        .HasColumnName("Escritor");

                    b.HasKey("Id");

                    b.HasIndex("ChatBotFk");

                    b.HasIndex("UserInstitutionFk");

                    b.ToTable("ChatBotUsuarioInstituicao", (string)null);
                });

            modelBuilder.Entity("NinaSpeakV2.Data.Entities.Institution", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)")
                        .HasColumnName("Codigo")
                        .HasDefaultValueSql("LEFT(LOWER(NEWID()), 8)");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("DataCriacao")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("DataExclusao");

                    b.Property<string>("Description")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)")
                        .HasColumnName("Descricao");

                    b.Property<string>("Image")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)")
                        .HasColumnName("Imagem");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)")
                        .HasColumnName("Nome");

                    b.HasKey("Id");

                    b.ToTable("Instituicao", (string)null);
                });

            modelBuilder.Entity("NinaSpeakV2.Data.Entities.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<bool>("Authenticated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false)
                        .HasColumnName("Autenticado");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("DataCriacao")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("DataExclusao");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)")
                        .HasColumnName("Email");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)")
                        .HasColumnName("Senha");

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)")
                        .HasColumnName("Sal");

                    b.Property<short?>("VerificationCode")
                        .HasMaxLength(5)
                        .HasColumnType("smallint")
                        .HasColumnName("CodigoVerificacao");

                    b.HasKey("Id");

                    b.ToTable("Usuario", (string)null);
                });

            modelBuilder.Entity("NinaSpeakV2.Data.Entities.UserInstitution", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("DataCriacao")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<bool>("Creator")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false)
                        .HasColumnName("Criador");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("DataExclusao");

                    b.Property<long>("InstitutionFk")
                        .HasColumnType("bigint")
                        .HasColumnName("InstituicaoFk");

                    b.Property<bool>("Owner")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false)
                        .HasColumnName("Proprietario");

                    b.Property<long>("UserFk")
                        .HasColumnType("bigint")
                        .HasColumnName("UsuarioFk");

                    b.HasKey("Id");

                    b.HasIndex("InstitutionFk");

                    b.HasIndex("UserFk", "InstitutionFk")
                        .IsUnique();

                    b.ToTable("UsuarioInstituicao", (string)null);
                });

            modelBuilder.Entity("NinaSpeakV2.Data.Entities.ChatBot", b =>
                {
                    b.HasOne("NinaSpeakV2.Data.Entities.ChatBotGenre", "ChatBotGenre")
                        .WithMany("ChatBots")
                        .HasForeignKey("ChatBotGenreFk")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("NinaSpeakV2.Data.Entities.ChatBotType", "ChatBotType")
                        .WithMany("ChatBots")
                        .HasForeignKey("ChatBotTypeFk")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("NinaSpeakV2.Data.Entities.Institution", "Institution")
                        .WithMany("ChatBots")
                        .HasForeignKey("InstitutionFk")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ChatBotGenre");

                    b.Navigation("ChatBotType");

                    b.Navigation("Institution");
                });

            modelBuilder.Entity("NinaSpeakV2.Data.Entities.ChatBotConversation", b =>
                {
                    b.HasOne("NinaSpeakV2.Data.Entities.ChatBot", "ChatBot")
                        .WithMany("ChatBotConversations")
                        .HasForeignKey("ChatBotFk")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ChatBot");
                });

            modelBuilder.Entity("NinaSpeakV2.Data.Entities.ChatBotUserInstitution", b =>
                {
                    b.HasOne("NinaSpeakV2.Data.Entities.ChatBot", "ChatBot")
                        .WithMany("ChatBotUserInstitutions")
                        .HasForeignKey("ChatBotFk")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("NinaSpeakV2.Data.Entities.UserInstitution", "UserInstitution")
                        .WithMany("ChatBotUserInstitutions")
                        .HasForeignKey("UserInstitutionFk")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ChatBot");

                    b.Navigation("UserInstitution");
                });

            modelBuilder.Entity("NinaSpeakV2.Data.Entities.UserInstitution", b =>
                {
                    b.HasOne("NinaSpeakV2.Data.Entities.Institution", "Institution")
                        .WithMany("UserInstitutions")
                        .HasForeignKey("InstitutionFk")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("NinaSpeakV2.Data.Entities.User", "User")
                        .WithMany("UserInstitutions")
                        .HasForeignKey("UserFk")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Institution");

                    b.Navigation("User");
                });

            modelBuilder.Entity("NinaSpeakV2.Data.Entities.ChatBot", b =>
                {
                    b.Navigation("ChatBotConversations");

                    b.Navigation("ChatBotUserInstitutions");
                });

            modelBuilder.Entity("NinaSpeakV2.Data.Entities.ChatBotGenre", b =>
                {
                    b.Navigation("ChatBots");
                });

            modelBuilder.Entity("NinaSpeakV2.Data.Entities.ChatBotType", b =>
                {
                    b.Navigation("ChatBots");
                });

            modelBuilder.Entity("NinaSpeakV2.Data.Entities.Institution", b =>
                {
                    b.Navigation("ChatBots");

                    b.Navigation("UserInstitutions");
                });

            modelBuilder.Entity("NinaSpeakV2.Data.Entities.User", b =>
                {
                    b.Navigation("UserInstitutions");
                });

            modelBuilder.Entity("NinaSpeakV2.Data.Entities.UserInstitution", b =>
                {
                    b.Navigation("ChatBotUserInstitutions");
                });
#pragma warning restore 612, 618
        }
    }
}
