using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructures.Migrations
{
    /// <inheritdoc />
    public partial class BinhNTBatchAndBuilding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Batchs_BatchId",
                table: "Sessions");

            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Buildings_BuildingId",
                table: "Sessions");

            migrationBuilder.DropIndex(
                name: "IX_Sessions_BatchId",
                table: "Sessions");

            migrationBuilder.DropIndex(
                name: "IX_Sessions_BuildingId",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "BatchId",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "BuildingId",
                table: "Sessions");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "StartTime",
                table: "Sessions",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "EndTime",
                table: "Sessions",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Sessions",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValueSql: "NEWID()");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Batchs",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BatchOfBuildings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    BatchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BuildingId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModificationBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BatchOfBuildings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BatchOfBuildings_Batchs_BatchId",
                        column: x => x.BatchId,
                        principalTable: "Batchs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BatchOfBuildings_Buildings_BuildingId",
                        column: x => x.BuildingId,
                        principalTable: "Buildings",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BatchOfBuildings_BatchId",
                table: "BatchOfBuildings",
                column: "BatchId");

            migrationBuilder.CreateIndex(
                name: "IX_BatchOfBuildings_BuildingId",
                table: "BatchOfBuildings",
                column: "BuildingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BatchOfBuildings");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Batchs");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartTime",
                table: "Sessions",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(TimeSpan),
                oldType: "time");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndTime",
                table: "Sessions",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(TimeSpan),
                oldType: "time");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Sessions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NEWID()",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "BatchId",
                table: "Sessions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "BuildingId",
                table: "Sessions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_BatchId",
                table: "Sessions",
                column: "BatchId");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_BuildingId",
                table: "Sessions",
                column: "BuildingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Batchs_BatchId",
                table: "Sessions",
                column: "BatchId",
                principalTable: "Batchs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Buildings_BuildingId",
                table: "Sessions",
                column: "BuildingId",
                principalTable: "Buildings",
                principalColumn: "Id");
        }
    }
}
