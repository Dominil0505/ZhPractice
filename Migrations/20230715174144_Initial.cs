using System;
using Microsoft.EntityFrameworkCore.Migrations;
using ZhPractice.Models.Question.Question;

#nullable disable

namespace ZhPractice.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Question",
                columns: table => new
                {
                    Question_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Question", x => x.Question_Id);
                });

            migrationBuilder.CreateTable(
                name: "Module",
                columns: table => new
                {
                    Module_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Module", x => x.Module_Id);
                });

            migrationBuilder.CreateTable(
                name: "Question_Module",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Question_Id = table.Column<int>(type: "int", nullable: false),
                    Module_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Question_Module", x => x.Id);
                    table.ForeignKey(
                        name: "PK_QuestionModule_Question",
                        column: x => x.Question_Id,
                        principalTable: "Question",
                        principalColumn: "Question_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "PK_QuestionModule_Module",
                        column: x => x.Module_Id,
                        principalTable: "Module",
                        principalColumn: "Module_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Answer",
                columns: table => new
                {
                    Answer_id = table.Column<int>(type: "int", nullable: false),
                    Text = table.Column<int>(type: "nvarchar(150)", nullable: false),
                    Question_Id = table.Column<int>(type: "int", nullable: false),
                    Module_Id = table.Column<int>(type: "int", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answer", x => x.Answer_id);
                    table.ForeignKey(
                        name: "FK_Answer_Question",
                        column: x => x.Question_Id,
                        principalTable: "Question",
                        principalColumn: "Question_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Answer_Module",
                        column: x => x.Module_Id,
                        principalTable: "Module",
                        principalColumn: "Module_Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Module");

            migrationBuilder.DropTable(
                name: "Question_Module");

            migrationBuilder.DropTable(
                name: "Answer");

            migrationBuilder.DropTable(
                name: "Question");
        }
    }
}
