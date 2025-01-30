using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CollegeApp.Migrations
{
    /// <inheritdoc />
    public partial class AddDataToStudentsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "tbl_Students",
                columns: new[] { "Id", "Address", "DOB", "Email", "StudentName" },
                values: new object[,]
                {
                    { 1, "Mumbai", new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "jonh@gmail.com", "John" },
                    { 2, "Kolkata", new DateTime(2022, 11, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "amit@gmail.com", "Amit" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "tbl_Students",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "tbl_Students",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
