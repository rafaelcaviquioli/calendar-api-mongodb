using Microsoft.EntityFrameworkCore.Migrations;

namespace CalendarAPI.Migrations
{
    public partial class ChangePropertyTimeType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "Time",
                table: "CalendarEvents",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Time",
                table: "CalendarEvents",
                type: "int",
                nullable: false,
                oldClrType: typeof(long));
        }
    }
}
