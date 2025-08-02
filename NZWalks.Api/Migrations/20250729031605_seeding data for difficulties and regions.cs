using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NZWalks.Api.Migrations
{
    /// <inheritdoc />
    public partial class seedingdatafordifficultiesandregions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("0f979d1a-5f56-4c03-8ac8-5ea842a40364"), "Easy" },
                    { new Guid("c57640ef-b130-49bf-ab1c-c7f820fe6725"), "Medium" },
                    { new Guid("fdcf6bc0-d118-4be7-9c0d-71a03af164db"), "Hard" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "Name", "RegionImageUrl" },
                values: new object[,]
                {
                    { new Guid("12345678-90ab-cdef-1234-567890abcdef"), "NSN", "Nelson", "https://images.pexels.com/photos/3396651/pexels-photo-3396651.jpeg" },
                    { new Guid("a2b1c3d4-e5f6-4a7b-8c9d-0e1f2a3b4c5d"), "CHC", "Christchurch", "https://images.pexels.com/photos/27912270/pexels-photo-27912270.jpeg" },
                    { new Guid("abcdef12-3456-7890-abcd-ef1234567890"), "TGA", "Tauranga", "https://images.pexels.com/photos/17982626/pexels-photo-17982626.jpeg" },
                    { new Guid("b1f0a2d3-4e6c-4b5a-8f7b-9c8d1e2f3a4b"), "WLG", "Wellington", "https://images.pexels.com/photos/395939/pexels-photo-395939.jpeg" },
                    { new Guid("cdde7bd2-de9a-47b3-ac79-42190d98e619"), "AKL", "Auckland", "https://images.pexels.com/photos/17824133/pexels-photo-17824133.jpeg" },
                    { new Guid("f1e2d3c4-b5a6-47b8-9c0d-1e2f3a4b5c6d"), "DUD", "Dunedin", "https://images.pexels.com/photos/424611/pexels-photo-424611.jpeg" },
                    { new Guid("fedcba98-7654-3210-fedc-ba9876543210"), "NPE", "Napier", "https://images.pexels.com/photos/20326273/pexels-photo-20326273.jpeg" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("0f979d1a-5f56-4c03-8ac8-5ea842a40364"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("c57640ef-b130-49bf-ab1c-c7f820fe6725"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("fdcf6bc0-d118-4be7-9c0d-71a03af164db"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("12345678-90ab-cdef-1234-567890abcdef"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("a2b1c3d4-e5f6-4a7b-8c9d-0e1f2a3b4c5d"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("abcdef12-3456-7890-abcd-ef1234567890"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("b1f0a2d3-4e6c-4b5a-8f7b-9c8d1e2f3a4b"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("cdde7bd2-de9a-47b3-ac79-42190d98e619"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("f1e2d3c4-b5a6-47b8-9c0d-1e2f3a4b5c6d"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("fedcba98-7654-3210-fedc-ba9876543210"));
        }
    }
}
