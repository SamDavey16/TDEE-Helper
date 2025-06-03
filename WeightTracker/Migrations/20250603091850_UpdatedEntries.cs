using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeightTracker.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedEntries : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Weight",
                table: "Entries",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<string>(
                name: "ActivityChoice",
                table: "Entries",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "Entries",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "FormulaChoice",
                table: "Entries",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "Height",
                table: "Entries",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Sex",
                table: "Entries",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActivityChoice",
                table: "Entries");

            migrationBuilder.DropColumn(
                name: "Age",
                table: "Entries");

            migrationBuilder.DropColumn(
                name: "FormulaChoice",
                table: "Entries");

            migrationBuilder.DropColumn(
                name: "Height",
                table: "Entries");

            migrationBuilder.DropColumn(
                name: "Sex",
                table: "Entries");

            migrationBuilder.AlterColumn<decimal>(
                name: "Weight",
                table: "Entries",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }
    }
}
