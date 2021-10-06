using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Circle.Library.DataAccess.Migrations.Ms
{
    public partial class GroupClaimsUniqueKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_GroupClaims_OperationClaimId",
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
                name: "IX_GroupClaims_OperationClaimId_GroupId",
                schema: "dbo",
                table: "GroupClaims",
                columns: new[] { "OperationClaimId", "GroupId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_GroupClaims_OperationClaimId_GroupId",
                schema: "dbo",
                table: "GroupClaims");

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
                values: new object[] { new Guid("08e5f51c-c82a-4eaa-8fd7-dbc28101fb5c"), "tr-TR", "1:1", "Türkçe", new DateTime(2021, 10, 1, 23, 23, 33, 780, DateTimeKind.Local).AddTicks(971), "admin", new DateTime(2021, 10, 1, 23, 23, 33, 781, DateTimeKind.Local).AddTicks(9981), "admin" });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Languages",
                columns: new[] { "Id", "Code", "Ip", "Name", "RecordDate", "RecordUsername", "UpdateDate", "UpdateUsername" },
                values: new object[] { new Guid("e008b199-8d63-4906-b2ab-021ae6aebd90"), "en-US", "1:1", "English", new DateTime(2021, 10, 1, 23, 23, 33, 782, DateTimeKind.Local).AddTicks(483), "admin", new DateTime(2021, 10, 1, 23, 23, 33, 782, DateTimeKind.Local).AddTicks(495), "admin" });

            migrationBuilder.CreateIndex(
                name: "IX_GroupClaims_OperationClaimId",
                schema: "dbo",
                table: "GroupClaims",
                column: "OperationClaimId");
        }
    }
}
