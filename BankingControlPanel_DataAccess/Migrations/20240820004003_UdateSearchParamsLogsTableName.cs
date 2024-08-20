using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankingControlPanel_DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UdateSearchParamsLogsTableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_searchParamsLogs",
                table: "searchParamsLogs");

            migrationBuilder.RenameTable(
                name: "searchParamsLogs",
                newName: "SearchParamsLogs");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SearchParamsLogs",
                table: "SearchParamsLogs",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SearchParamsLogs",
                table: "SearchParamsLogs");

            migrationBuilder.RenameTable(
                name: "SearchParamsLogs",
                newName: "searchParamsLogs");

            migrationBuilder.AddPrimaryKey(
                name: "PK_searchParamsLogs",
                table: "searchParamsLogs",
                column: "Id");
        }
    }
}
