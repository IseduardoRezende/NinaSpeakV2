﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NinaSpeakV2.Data;

#nullable disable

namespace NinaSpeakV2.Data.Migrations
{
    [DbContext(typeof(NinaSpeakContext))]
    partial class NinaSpeakContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("NinaSpeakV2.Data.Models.ChatBot", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("ChatBotGenreFk")
                        .HasColumnType("bigint")
                        .HasColumnName("ChatBotGeneroFk");

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

                    b.HasIndex("InstitutionFk");

                    b.ToTable("ChatBot", (string)null);
                });

            modelBuilder.Entity("NinaSpeakV2.Data.Models.ChatBotConversation", b =>
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

            modelBuilder.Entity("NinaSpeakV2.Data.Models.ChatBotGenre", b =>
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

            modelBuilder.Entity("NinaSpeakV2.Data.Models.Institution", b =>
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

            modelBuilder.Entity("NinaSpeakV2.Data.Models.User", b =>
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

                    b.HasKey("Id");

                    b.ToTable("Usuario", (string)null);
                });

            modelBuilder.Entity("NinaSpeakV2.Data.Models.UserInstitution", b =>
                {
                    b.Property<long>("UserFk")
                        .HasColumnType("bigint")
                        .HasColumnName("UsuarioFk");

                    b.Property<long>("InstitutionFk")
                        .HasColumnType("bigint")
                        .HasColumnName("InstituicaoFk");

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

                    b.Property<bool>("Owner")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false)
                        .HasColumnName("Proprietario");

                    b.Property<bool>("Writer")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false)
                        .HasColumnName("Escritor");

                    b.HasKey("UserFk", "InstitutionFk");

                    b.HasIndex("InstitutionFk");

                    b.ToTable("UsuarioInstituicao", (string)null);
                });

            modelBuilder.Entity("NinaSpeakV2.Data.Models.ChatBot", b =>
                {
                    b.HasOne("NinaSpeakV2.Data.Models.ChatBotGenre", "ChatBotGenre")
                        .WithMany("ChatBots")
                        .HasForeignKey("ChatBotGenreFk")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("NinaSpeakV2.Data.Models.Institution", "Institution")
                        .WithMany("ChatBots")
                        .HasForeignKey("InstitutionFk")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ChatBotGenre");

                    b.Navigation("Institution");
                });

            modelBuilder.Entity("NinaSpeakV2.Data.Models.ChatBotConversation", b =>
                {
                    b.HasOne("NinaSpeakV2.Data.Models.ChatBot", "ChatBot")
                        .WithMany("ChatBotConversations")
                        .HasForeignKey("ChatBotFk")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ChatBot");
                });

            modelBuilder.Entity("NinaSpeakV2.Data.Models.UserInstitution", b =>
                {
                    b.HasOne("NinaSpeakV2.Data.Models.Institution", "Institution")
                        .WithMany("UserInstitutions")
                        .HasForeignKey("InstitutionFk")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("NinaSpeakV2.Data.Models.User", "User")
                        .WithMany("UserInstitutions")
                        .HasForeignKey("UserFk")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Institution");

                    b.Navigation("User");
                });

            modelBuilder.Entity("NinaSpeakV2.Data.Models.ChatBot", b =>
                {
                    b.Navigation("ChatBotConversations");
                });

            modelBuilder.Entity("NinaSpeakV2.Data.Models.ChatBotGenre", b =>
                {
                    b.Navigation("ChatBots");
                });

            modelBuilder.Entity("NinaSpeakV2.Data.Models.Institution", b =>
                {
                    b.Navigation("ChatBots");

                    b.Navigation("UserInstitutions");
                });

            modelBuilder.Entity("NinaSpeakV2.Data.Models.User", b =>
                {
                    b.Navigation("UserInstitutions");
                });
#pragma warning restore 612, 618
        }
    }
}
