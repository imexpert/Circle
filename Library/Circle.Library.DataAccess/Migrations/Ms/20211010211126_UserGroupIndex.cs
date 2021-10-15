using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Circle.Library.DataAccess.Migrations.Ms
{
    public partial class UserGroupIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserGroups_GroupId",
                schema: "dbo",
                table: "UserGroups");

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Languages",
                keyColumn: "Id",
                keyValue: new Guid("2828e399-0b43-4c97-97c1-d5e17d940440"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Languages",
                keyColumn: "Id",
                keyValue: new Guid("a6a7af61-2f70-42f1-ae0a-eae09a0294ef"));

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Languages",
                columns: new[] { "Id", "Code", "Ip", "Name", "RecordDate", "RecordUsername", "UpdateDate", "UpdateUsername" },
                values: new object[] { new Guid("53f811f8-e79b-4f2f-ac90-e0373b12efd6"), "tr-TR", "1:1", "Türkçe", new DateTime(2021, 10, 11, 0, 11, 26, 374, DateTimeKind.Local).AddTicks(4258), "admin", new DateTime(2021, 10, 11, 0, 11, 26, 376, DateTimeKind.Local).AddTicks(3360), "admin" });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Languages",
                columns: new[] { "Id", "Code", "Ip", "Name", "RecordDate", "RecordUsername", "UpdateDate", "UpdateUsername" },
                values: new object[] { new Guid("b31eb9e0-7d14-4ac7-9d99-8b16cc6c6f08"), "en-US", "1:1", "English", new DateTime(2021, 10, 11, 0, 11, 26, 376, DateTimeKind.Local).AddTicks(3887), "admin", new DateTime(2021, 10, 11, 0, 11, 26, 376, DateTimeKind.Local).AddTicks(3898), "admin" });

            migrationBuilder.CreateIndex(
                name: "IX_UserGroups_GroupId_UserId",
                schema: "dbo",
                table: "UserGroups",
                columns: new[] { "GroupId", "UserId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserGroups_GroupId_UserId",
                schema: "dbo",
                table: "UserGroups");

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Languages",
                keyColumn: "Id",
                keyValue: new Guid("53f811f8-e79b-4f2f-ac90-e0373b12efd6"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Languages",
                keyColumn: "Id",
                keyValue: new Guid("b31eb9e0-7d14-4ac7-9d99-8b16cc6c6f08"));

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Languages",
                columns: new[] { "Id", "Code", "Ip", "Name", "RecordDate", "RecordUsername", "UpdateDate", "UpdateUsername" },
                values: new object[] { new Guid("a6a7af61-2f70-42f1-ae0a-eae09a0294ef"), "tr-TR", "1:1", "Türkçe", new DateTime(2021, 10, 5, 1, 25, 11, 911, DateTimeKind.Local).AddTicks(3636), "admin", new DateTime(2021, 10, 5, 1, 25, 11, 913, DateTimeKind.Local).AddTicks(3827), "admin" });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Languages",
                columns: new[] { "Id", "Code", "Ip", "Name", "RecordDate", "RecordUsername", "UpdateDate", "UpdateUsername" },
                values: new object[] { new Guid("2828e399-0b43-4c97-97c1-d5e17d940440"), "en-US", "1:1", "English", new DateTime(2021, 10, 5, 1, 25, 11, 913, DateTimeKind.Local).AddTicks(4511), "admin", new DateTime(2021, 10, 5, 1, 25, 11, 913, DateTimeKind.Local).AddTicks(4524), "admin" });

            migrationBuilder.CreateIndex(
                name: "IX_UserGroups_GroupId",
                schema: "dbo",
                table: "UserGroups",
                column: "GroupId");
        }
    }
}
