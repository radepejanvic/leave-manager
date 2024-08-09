using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ChangedEmployeeKeyToEmail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeaveRequests_Employees_EmployeeId",
                table: "LeaveRequests");

            migrationBuilder.DropIndex(
                name: "IX_LeaveRequests_EmployeeId",
                table: "LeaveRequests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Employees",
                table: "Employees");

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "LeaveRequests");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Employees");

            migrationBuilder.AddColumn<string>(
                name: "EmployeeEmail",
                table: "LeaveRequests",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Employees",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employees",
                table: "Employees",
                column: "Email");

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Email", "Name", "Phone", "Surname" },
                values: new object[,]
                {
                    { "ajs@nigucci.com", "Vladan", "0631233999", "Aksentijevic" },
                    { "marko@example.com", "Marko", "0631239999", "Markovic" },
                    { "milan@example.com", "Milan", "0631234999", "Mladenovic" }
                });

            migrationBuilder.UpdateData(
                table: "LeaveRequests",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EmployeeEmail", "End", "Start" },
                values: new object[] { "ajs@nigucci.com", new DateOnly(2024, 8, 14), new DateOnly(2024, 8, 9) });

            migrationBuilder.UpdateData(
                table: "LeaveRequests",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "EmployeeEmail", "End", "Start" },
                values: new object[] { "ajs@nigucci.com", new DateOnly(2025, 1, 9), new DateOnly(2024, 12, 9) });

            migrationBuilder.UpdateData(
                table: "LeaveRequests",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "EmployeeEmail", "End", "Start" },
                values: new object[] { "milan@example.com", new DateOnly(2024, 8, 14), new DateOnly(2024, 8, 9) });

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRequests_EmployeeEmail",
                table: "LeaveRequests",
                column: "EmployeeEmail");

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveRequests_Employees_EmployeeEmail",
                table: "LeaveRequests",
                column: "EmployeeEmail",
                principalTable: "Employees",
                principalColumn: "Email",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeaveRequests_Employees_EmployeeEmail",
                table: "LeaveRequests");

            migrationBuilder.DropIndex(
                name: "IX_LeaveRequests_EmployeeEmail",
                table: "LeaveRequests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Employees",
                table: "Employees");

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Email",
                keyValue: "ajs@nigucci.com");

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Email",
                keyValue: "marko@example.com");

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Email",
                keyValue: "milan@example.com");

            migrationBuilder.DropColumn(
                name: "EmployeeEmail",
                table: "LeaveRequests");

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "LeaveRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employees",
                table: "Employees",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Email", "Name", "Phone", "Surname" },
                values: new object[,]
                {
                    { 1, "marko@example.com", "Marko", "0631239999", "Markovic" },
                    { 2, "milan@example.com", "Milan", "0631234999", "Mladenovic" },
                    { 3, "ajs@nigucci.com", "Vladan", "0631233999", "Aksentijevic" }
                });

            migrationBuilder.UpdateData(
                table: "LeaveRequests",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EmployeeId", "End", "Start" },
                values: new object[] { 3, new DateOnly(2024, 8, 13), new DateOnly(2024, 8, 8) });

            migrationBuilder.UpdateData(
                table: "LeaveRequests",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "EmployeeId", "End", "Start" },
                values: new object[] { 3, new DateOnly(2025, 1, 8), new DateOnly(2024, 12, 8) });

            migrationBuilder.UpdateData(
                table: "LeaveRequests",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "EmployeeId", "End", "Start" },
                values: new object[] { 1, new DateOnly(2024, 8, 13), new DateOnly(2024, 8, 8) });

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRequests_EmployeeId",
                table: "LeaveRequests",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveRequests_Employees_EmployeeId",
                table: "LeaveRequests",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
