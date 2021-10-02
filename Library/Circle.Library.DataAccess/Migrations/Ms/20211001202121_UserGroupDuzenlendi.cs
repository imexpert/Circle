using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Circle.Library.DataAccess.Migrations.Ms
{
    public partial class UserGroupDuzenlendi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserGroups",
                schema: "dbo",
                table: "UserGroups");

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Languages",
                keyColumn: "Id",
                keyValue: new Guid("173cca8e-9bd1-41c0-a163-9648e7769f0c"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Languages",
                keyColumn: "Id",
                keyValue: new Guid("61a61d63-e656-4ed5-9ca8-8dad3b670707"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserGroups",
                schema: "dbo",
                table: "UserGroups",
                column: "Id");

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

            migrationBuilder.CreateIndex(
                name: "IX_UserGroups_UserId",
                schema: "dbo",
                table: "UserGroups",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserGroups",
                schema: "dbo",
                table: "UserGroups");

            migrationBuilder.DropIndex(
                name: "IX_UserGroups_UserId",
                schema: "dbo",
                table: "UserGroups");

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
                name: "PK_UserGroups",
                schema: "dbo",
                table: "UserGroups",
                columns: new[] { "UserId", "GroupId" });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Languages",
                columns: new[] { "Id", "Code", "Ip", "Name", "RecordDate", "RecordUsername", "UpdateDate", "UpdateUsername" },
                values: new object[] { new Guid("61a61d63-e656-4ed5-9ca8-8dad3b670707"), "tr-TR", "1:1", "Türkçe", new DateTime(2021, 10, 1, 23, 19, 4, 741, DateTimeKind.Local).AddTicks(2686), "admin", new DateTime(2021, 10, 1, 23, 19, 4, 743, DateTimeKind.Local).AddTicks(8441), "admin" });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Languages",
                columns: new[] { "Id", "Code", "Ip", "Name", "RecordDate", "RecordUsername", "UpdateDate", "UpdateUsername" },
                values: new object[] { new Guid("173cca8e-9bd1-41c0-a163-9648e7769f0c"), "en-US", "1:1", "English", new DateTime(2021, 10, 1, 23, 19, 4, 743, DateTimeKind.Local).AddTicks(9153), "admin", new DateTime(2021, 10, 1, 23, 19, 4, 743, DateTimeKind.Local).AddTicks(9169), "admin" });
        }
    }
}
