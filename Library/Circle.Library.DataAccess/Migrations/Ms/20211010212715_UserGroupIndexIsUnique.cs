using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Circle.Library.DataAccess.Migrations.Ms
{
    public partial class UserGroupIndexIsUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                values: new object[] { new Guid("1e610fd4-f7f9-4074-ae9a-a3c385cb6e41"), "tr-TR", "1:1", "Türkçe", new DateTime(2021, 10, 11, 0, 27, 14, 814, DateTimeKind.Local).AddTicks(7731), "admin", new DateTime(2021, 10, 11, 0, 27, 14, 816, DateTimeKind.Local).AddTicks(6378), "admin" });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Languages",
                columns: new[] { "Id", "Code", "Ip", "Name", "RecordDate", "RecordUsername", "UpdateDate", "UpdateUsername" },
                values: new object[] { new Guid("2c53852d-400a-44f3-9345-18ebcbedfb77"), "en-US", "1:1", "English", new DateTime(2021, 10, 11, 0, 27, 14, 816, DateTimeKind.Local).AddTicks(6878), "admin", new DateTime(2021, 10, 11, 0, 27, 14, 816, DateTimeKind.Local).AddTicks(6889), "admin" });

            migrationBuilder.CreateIndex(
                name: "IX_UserGroups_GroupId_UserId",
                schema: "dbo",
                table: "UserGroups",
                columns: new[] { "GroupId", "UserId" },
                unique: true);
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
                keyValue: new Guid("1e610fd4-f7f9-4074-ae9a-a3c385cb6e41"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Languages",
                keyColumn: "Id",
                keyValue: new Guid("2c53852d-400a-44f3-9345-18ebcbedfb77"));

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
    }
}
