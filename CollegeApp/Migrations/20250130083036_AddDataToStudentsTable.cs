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
                    { 1, "India", new DateTime(2022, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "jonh@gmail.com", "John" },
                    { 2, "Delhi India", new DateTime(2020, 2, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "amit@gmail.com", "Amit" },
                    { 3, "WB,India", new DateTime(2021, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "sumit@gmail.com", "Sumit" }
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

            migrationBuilder.DeleteData(
                table: "tbl_Students",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
