using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NZWalkAPICore8.Migrations
{
    /// <inheritdoc />
    public partial class seedthedataforregionandDifferculty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulty",
                columns: new[] { "Id", "Code" },
                values: new object[,]
                {
                    { new Guid("74ba7c86-388e-443e-9524-ffa05c9c6e5c"), "Easy" },
                    { new Guid("d5cf834c-bd84-4d87-bef9-061a860a8c0d"), "Medium" },
                    { new Guid("e7270e7a-e944-4897-9e6a-de937d8a6087"), "Hard" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Area", "Code", "Lat", "Long", "Name", "Population" },
                values: new object[,]
                {
                    { new Guid("14ceba71-4b51-4777-9b17-46602cf66153"), 6543.0, "BOP", 2323.0, 3434.0, "Bay Of Plenty", 4545L },
                    { new Guid("1ea9b01d-ae6d-4cbc-addc-20549f495a0a"), 235.0, "EAST-DEL", 2393.0, 3634.0, "EAST DELHI", 560000L },
                    { new Guid("6884f7d7-ad1f-4101-8df3-7a6fa7387d81"), 6512.0, "NTL", 2323.0, 3434.0, "Northland", 4545L },
                    { new Guid("6ae537d8-5725-427c-9c8c-3406feb70332"), 1212.0, "SOUTH-DEL", 2323.0, 3434.0, "SOUTH DELHI", 45000L },
                    { new Guid("761a465b-e718-4b45-81d1-ed17ea9b3183"), 1212.0, "WEST-DEL", 2323.0, 3434.0, "WEST DELHI", 360000L },
                    { new Guid("906cb139-415a-4bbb-a174-1a1faf9fb1f6"), 6589.0, "NSN", 2323.0, 3434.0, "Nelson", 4511L },
                    { new Guid("cfa06ed2-bf65-4b65-93ed-c9d286ddb0de"), 2314.0, "WGN", 2323.0, 3434.0, "Wellington", 4545L },
                    { new Guid("f077a22e-4248-4bf6-b564-c7cf4e250263"), 7845.0, "STL", 2323.0, 3434.0, "Southland", 78000L },
                    { new Guid("f7248fc3-2585-4efb-8d1d-1c555f4087f6"), 8790.0, "AKL", 342.0, 3434.0, "Auckland", 4545L }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulty",
                keyColumn: "Id",
                keyValue: new Guid("74ba7c86-388e-443e-9524-ffa05c9c6e5c"));

            migrationBuilder.DeleteData(
                table: "Difficulty",
                keyColumn: "Id",
                keyValue: new Guid("d5cf834c-bd84-4d87-bef9-061a860a8c0d"));

            migrationBuilder.DeleteData(
                table: "Difficulty",
                keyColumn: "Id",
                keyValue: new Guid("e7270e7a-e944-4897-9e6a-de937d8a6087"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("14ceba71-4b51-4777-9b17-46602cf66153"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("1ea9b01d-ae6d-4cbc-addc-20549f495a0a"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("6884f7d7-ad1f-4101-8df3-7a6fa7387d81"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("6ae537d8-5725-427c-9c8c-3406feb70332"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("761a465b-e718-4b45-81d1-ed17ea9b3183"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("906cb139-415a-4bbb-a174-1a1faf9fb1f6"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("cfa06ed2-bf65-4b65-93ed-c9d286ddb0de"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("f077a22e-4248-4bf6-b564-c7cf4e250263"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("f7248fc3-2585-4efb-8d1d-1c555f4087f6"));
        }
    }
}
