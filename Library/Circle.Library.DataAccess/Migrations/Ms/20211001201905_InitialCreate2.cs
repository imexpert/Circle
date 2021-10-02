using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Circle.Library.DataAccess.Migrations.Ms
{
    public partial class InitialCreate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Languages",
                keyColumn: "Id",
                keyValue: new Guid("706388a8-8cc8-4b0b-83d8-661dfb06419d"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Languages",
                keyColumn: "Id",
                keyValue: new Guid("baa13b40-bdec-4af2-82fc-77ad44b672ed"));

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                schema: "dbo",
                table: "UserGroups",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                schema: "dbo",
                table: "GroupClaims",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "dbo",
                table: "UserGroups");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "dbo",
                table: "GroupClaims");

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Languages",
                columns: new[] { "Id", "Code", "Ip", "Name", "RecordDate", "RecordUsername", "UpdateDate", "UpdateUsername" },
                values: new object[] { new Guid("706388a8-8cc8-4b0b-83d8-661dfb06419d"), "tr-TR", "1:1", "Türkçe", new DateTime(2021, 10, 1, 23, 10, 4, 600, DateTimeKind.Local).AddTicks(3240), "admin", new DateTime(2021, 10, 1, 23, 10, 4, 602, DateTimeKind.Local).AddTicks(4533), "admin" });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Languages",
                columns: new[] { "Id", "Code", "Ip", "Name", "RecordDate", "RecordUsername", "UpdateDate", "UpdateUsername" },
                values: new object[] { new Guid("baa13b40-bdec-4af2-82fc-77ad44b672ed"), "en-US", "1:1", "English", new DateTime(2021, 10, 1, 23, 10, 4, 602, DateTimeKind.Local).AddTicks(5107), "admin", new DateTime(2021, 10, 1, 23, 10, 4, 602, DateTimeKind.Local).AddTicks(5120), "admin" });
        }
    }
}
