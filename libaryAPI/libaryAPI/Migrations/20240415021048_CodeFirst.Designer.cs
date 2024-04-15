﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using libaryAPI.Data;

#nullable disable

namespace libaryAPI.Migrations
{
    [DbContext(typeof(dbcontext))]
    [Migration("20240415021048_CodeFirst")]
    partial class CodeFirst
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("libaryAPI.Models.Authors", b =>
                {
                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("Book_AuthorID")
                        .HasColumnType("int");

                    b.HasKey("FullName");

                    b.HasIndex("Book_AuthorID");

                    b.ToTable("Authors");
                });

            modelBuilder.Entity("libaryAPI.Models.Book_Author", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("AuthorID")
                        .HasColumnType("int");

                    b.Property<int>("BooksID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("BooksID");

                    b.ToTable("Book_Author");
                });

            modelBuilder.Entity("libaryAPI.Models.Books", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("CoverUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateAdded")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateRead")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Grene")
                        .HasColumnType("int");

                    b.Property<int>("PublishersID")
                        .HasColumnType("int");

                    b.Property<int>("Rate")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("isRead")
                        .HasColumnType("bit");

                    b.HasKey("ID");

                    b.HasIndex("PublishersID");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("libaryAPI.Models.Publishers", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Publishers");
                });

            modelBuilder.Entity("libaryAPI.Models.Authors", b =>
                {
                    b.HasOne("libaryAPI.Models.Book_Author", null)
                        .WithMany("Authors")
                        .HasForeignKey("Book_AuthorID");
                });

            modelBuilder.Entity("libaryAPI.Models.Book_Author", b =>
                {
                    b.HasOne("libaryAPI.Models.Books", null)
                        .WithMany("Book_Authors")
                        .HasForeignKey("BooksID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("libaryAPI.Models.Books", b =>
                {
                    b.HasOne("libaryAPI.Models.Publishers", "Publishers")
                        .WithMany("Books")
                        .HasForeignKey("PublishersID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Publishers");
                });

            modelBuilder.Entity("libaryAPI.Models.Book_Author", b =>
                {
                    b.Navigation("Authors");
                });

            modelBuilder.Entity("libaryAPI.Models.Books", b =>
                {
                    b.Navigation("Book_Authors");
                });

            modelBuilder.Entity("libaryAPI.Models.Publishers", b =>
                {
                    b.Navigation("Books");
                });
#pragma warning restore 612, 618
        }
    }
}
