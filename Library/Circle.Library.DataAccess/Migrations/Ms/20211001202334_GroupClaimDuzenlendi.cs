using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Circle.Library.DataAccess.Migrations.Ms
{
    public partial class GroupClaimDuzenlendi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_GroupClaims",
                schema: "dbo",
                table: "GroupClaims");

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Languages",
                keyColumn: "Id",
                keyValue: new Guid("22f9b20e-f4d5-42f5-a88f-7f9a6d283c5f"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Languages",
                keyColumn: "Id",
                keyValue: new Guid("47551c83-25c7-47f5-9ba4-20015ecfc439"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_GroupClaims",
                schema: "dbo",
                table: "GroupClaims",
                column: "Id");

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Languages",
                columns: new[] { "Id", "Code", "Ip", "Name", "RecordDate", "RecordUsername", "UpdateDate", "UpdateUsername" },
                values: new object[] { new Guid("08e5f51c-c82a-4eaa-8fd7-dbc28101fb5c"), "tr-TR", "1:1", "Türkçe", new DateTime(2021, 10, 1, 23, 23, 33, 780, DateTimeKind.Local).AddTicks(971), "admin", new DateTime(2021, 10, 1, 23, 23, 33, 781, DateTimeKind.Local).AddTicks(9981), "admin" });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Languages",
                columns: new[] { "Id", "Code", "Ip", "Name", "RecordDate", "RecordUsername", "UpdateDate", "UpdateUsername" },
                values: new object[] { new Guid("e008b199-8d63-4906-b2ab-021ae6aebd90"), "en-US", "1:1", "English", new DateTime(2021, 10, 1, 23, 23, 33, 782, DateTimeKind.Local).AddTicks(483), "admin", new DateTime(2021, 10, 1, 23, 23, 33, 782, DateTimeKind.Local).AddTicks(495), "admin" });

            migrationBuilder.CreateIndex(
                name: "IX_GroupClaims_GroupId",
                schema: "dbo",
                table: "GroupClaims",
                column: "GroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_GroupClaims",
                schema: "dbo",
                table: "GroupClaims");

            migrationBuilder.DropIndex(
                name: "IX_GroupClaims_GroupId",
                schema: "dbo",
                table: "GroupClaims");

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Languages",
                keyColumn: "Id",
                keyValue: new Guid("08e5f51c-c82a-4eaa-8fd7-dbc28101fb5c"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Languages",
                keyColumn: "Id",
                keyValue: new Guid("e008b199-8d63-4906-b2ab-021ae6aebd90"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_GroupClaims",
                schema: "dbo",
                table: "GroupClaims",
                columns: new[] { "GroupId", "OperationClaimId" });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Languages",
                columns: new[] { "Id", "Code", "Ip", "Name", "RecordDate", "RecordUsername", "UpdateDate", "UpdateUsername" },
                values: new object[] { new Guid("47551c83-25c7-47f5-9ba4-20015ecfc439"), "tr-TR", "1:1", "Türkçe", new DateTime(2021, 10, 1, 23, 21, 20, 890, DateTimeKind.Local).AddTicks(5367), "admin", new DateTime(2021, 10, 1, 23, 21, 20, 892, DateTimeKind.Local).AddTicks(5050), "admin" });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Languages",
                columns: new[] { "Id", "Code", "Ip", "Name", "RecordDate", "RecordUsername", "UpdateDate", "UpdateUsername" },
                values: new object[] { new Guid("22f9b20e-f4d5-42f5-a88f-7f9a6d283c5f"), "en-US", "1:1", "English", new DateTime(2021, 10, 1, 23, 21, 20, 892, DateTimeKind.Local).AddTicks(5619), "admin", new DateTime(2021, 10, 1, 23, 21, 20, 892, DateTimeKind.Local).AddTicks(5632), "admin" });
        }
    }
}
