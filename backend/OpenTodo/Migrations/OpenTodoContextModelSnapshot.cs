﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using OpenTodo.Data;

#nullable disable

namespace OpenTodo.Migrations
{
    [DbContext(typeof(OpenTodoContext))]
    partial class OpenTodoContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("OpenTodo.Models.BoardParticipantSchema", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<int>("BoardId")
                        .HasColumnType("integer");

                    b.Property<int>("BoardsID")
                        .HasColumnType("integer");

                    b.Property<DateTime>("JoinedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValue(new DateTime(2024, 10, 1, 2, 46, 16, 198, DateTimeKind.Utc).AddTicks(6123));

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<int>("UsersId")
                        .HasColumnType("integer");

                    b.HasKey("ID");

                    b.HasIndex("BoardsID");

                    b.HasIndex("UsersId");

                    b.ToTable("board_participant", (string)null);
                });

            modelBuilder.Entity("OpenTodo.Models.BoardSchema", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar")
                        .HasColumnName("name");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.HasKey("ID");

                    b.HasIndex("UserId");

                    b.ToTable("board", (string)null);
                });

            modelBuilder.Entity("OpenTodo.Models.CategorySchema", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<int>("BoardId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar")
                        .HasColumnName("name");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.HasKey("ID");

                    b.HasIndex("BoardId");

                    b.ToTable("category", (string)null);
                });

            modelBuilder.Entity("OpenTodo.Models.TaskSchema", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<int>("AssignedUserId")
                        .HasColumnType("integer")
                        .HasColumnName("assigned_user_id");

                    b.Property<int>("BoardId")
                        .HasColumnType("integer")
                        .HasColumnName("board_id");

                    b.Property<short>("Category")
                        .HasColumnType("smallint");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValue(new DateTime(2024, 10, 1, 2, 46, 16, 198, DateTimeKind.Utc).AddTicks(6123));

                    b.Property<int>("CreatorUserId")
                        .HasColumnType("integer")
                        .HasColumnName("creator_id");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateOnly>("DueDate")
                        .HasColumnType("date");

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("UsersId")
                        .HasColumnType("integer");

                    b.HasKey("ID");

                    b.HasIndex("AssignedUserId");

                    b.HasIndex("BoardId");

                    b.HasIndex("UsersId");

                    b.ToTable("task", (string)null);
                });

            modelBuilder.Entity("OpenTodo.Models.UserSchema", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<DateOnly>("DOB")
                        .HasColumnType("date")
                        .HasColumnName("dob");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar")
                        .HasColumnName("last_name");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar")
                        .HasColumnName("password_hash");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar")
                        .HasColumnName("username");

                    b.HasKey("Id");

                    b.ToTable("user", (string)null);
                });

            modelBuilder.Entity("OpenTodo.Models.BoardParticipantSchema", b =>
                {
                    b.HasOne("OpenTodo.Models.BoardSchema", "Boards")
                        .WithMany("BoardParticipants")
                        .HasForeignKey("BoardsID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OpenTodo.Models.UserSchema", "Users")
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Boards");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("OpenTodo.Models.BoardSchema", b =>
                {
                    b.HasOne("OpenTodo.Models.UserSchema", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("OpenTodo.Models.CategorySchema", b =>
                {
                    b.HasOne("OpenTodo.Models.BoardSchema", "Board")
                        .WithMany()
                        .HasForeignKey("BoardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Board");
                });

            modelBuilder.Entity("OpenTodo.Models.TaskSchema", b =>
                {
                    b.HasOne("OpenTodo.Models.UserSchema", "AssignedUser")
                        .WithMany()
                        .HasForeignKey("AssignedUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OpenTodo.Models.BoardSchema", "Board")
                        .WithMany("TaskParticipants")
                        .HasForeignKey("BoardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OpenTodo.Models.UserSchema", "Users")
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AssignedUser");

                    b.Navigation("Board");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("OpenTodo.Models.BoardSchema", b =>
                {
                    b.Navigation("BoardParticipants");

                    b.Navigation("TaskParticipants");
                });
#pragma warning restore 612, 618
        }
    }
}
