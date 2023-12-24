using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WorkForcePro.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Attendence",
                columns: table => new
                {
                    ComId = table.Column<int>(type: "integer", nullable: false),
                    EmpId = table.Column<int>(type: "integer", nullable: false),
                    dtDate = table.Column<DateTime>(type: "date", nullable: false),
                    AttStatus = table.Column<string>(type: "text", nullable: false),
                    InTime = table.Column<TimeSpan>(type: "interval", nullable: true),
                    OutTime = table.Column<TimeSpan>(type: "interval", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendence", x => new { x.ComId, x.EmpId, x.dtDate });
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    ComId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ComName = table.Column<string>(type: "text", nullable: false),
                    Basic = table.Column<int>(type: "integer", nullable: false),
                    HRent = table.Column<int>(type: "integer", nullable: false),
                    Medical = table.Column<int>(type: "integer", nullable: false),
                    IsInactive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.ComId);
                });

            migrationBuilder.CreateTable(
                name: "Salaries",
                columns: table => new
                {
                    ComId = table.Column<int>(type: "integer", nullable: false),
                    EmpId = table.Column<int>(type: "integer", nullable: false),
                    dtYear = table.Column<int>(type: "integer", nullable: false),
                    dtMonth = table.Column<int>(type: "integer", nullable: false),
                    Gross = table.Column<int>(type: "integer", nullable: false),
                    Basic = table.Column<int>(type: "integer", nullable: false),
                    Hrent = table.Column<int>(type: "integer", nullable: false),
                    Medical = table.Column<int>(type: "integer", nullable: false),
                    AbsentAmount = table.Column<int>(type: "integer", nullable: false),
                    PayableAmount = table.Column<int>(type: "integer", nullable: false),
                    IsPaid = table.Column<bool>(type: "boolean", nullable: false),
                    PaidAmount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salaries", x => new { x.ComId, x.EmpId, x.dtYear, x.dtMonth });
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    DeptId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DeptName = table.Column<string>(type: "text", nullable: false),
                    ComId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.DeptId);
                    table.ForeignKey(
                        name: "FK_Departments_Companies_ComId",
                        column: x => x.ComId,
                        principalTable: "Companies",
                        principalColumn: "ComId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Designations",
                columns: table => new
                {
                    DesigId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DesigName = table.Column<string>(type: "text", nullable: false),
                    ComId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Designations", x => x.DesigId);
                    table.ForeignKey(
                        name: "FK_Designations_Companies_ComId",
                        column: x => x.ComId,
                        principalTable: "Companies",
                        principalColumn: "ComId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Shifts",
                columns: table => new
                {
                    ShiftId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ShiftName = table.Column<string>(type: "text", nullable: false),
                    ShiftIn = table.Column<TimeSpan>(type: "interval", nullable: false),
                    ShiftOut = table.Column<TimeSpan>(type: "interval", nullable: false),
                    ShiftLateTime = table.Column<TimeSpan>(type: "interval", nullable: false),
                    ComId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shifts", x => x.ShiftId);
                    table.ForeignKey(
                        name: "FK_Shifts_Companies_ComId",
                        column: x => x.ComId,
                        principalTable: "Companies",
                        principalColumn: "ComId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmpId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EmpName = table.Column<string>(type: "text", nullable: false),
                    EmpCode = table.Column<int>(type: "integer", nullable: false),
                    Gender = table.Column<string>(type: "text", nullable: false),
                    Gross = table.Column<int>(type: "integer", nullable: false),
                    Basic = table.Column<int>(type: "integer", nullable: true),
                    HRent = table.Column<int>(type: "integer", nullable: true),
                    Medical = table.Column<int>(type: "integer", nullable: true),
                    Others = table.Column<int>(type: "integer", nullable: true),
                    DtJoin = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ComId = table.Column<int>(type: "integer", nullable: false),
                    ShiftId = table.Column<int>(type: "integer", nullable: false),
                    DeptId = table.Column<int>(type: "integer", nullable: false),
                    DesigId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmpId);
                    table.ForeignKey(
                        name: "FK_Employees_Companies_ComId",
                        column: x => x.ComId,
                        principalTable: "Companies",
                        principalColumn: "ComId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Employees_Departments_DeptId",
                        column: x => x.DeptId,
                        principalTable: "Departments",
                        principalColumn: "DeptId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Employees_Designations_DesigId",
                        column: x => x.DesigId,
                        principalTable: "Designations",
                        principalColumn: "DesigId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Employees_Shifts_ShiftId",
                        column: x => x.ShiftId,
                        principalTable: "Shifts",
                        principalColumn: "ShiftId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "AttendanceSummaries",
                columns: table => new
                {
                    ComId = table.Column<int>(type: "integer", nullable: false),
                    EmpId = table.Column<int>(type: "integer", nullable: false),
                    DtYear = table.Column<int>(type: "integer", nullable: false),
                    DtMonth = table.Column<int>(type: "integer", nullable: false),
                    Present = table.Column<int>(type: "integer", nullable: false),
                    Late = table.Column<int>(type: "integer", nullable: false),
                    Absent = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttendanceSummaries", x => new { x.ComId, x.EmpId, x.DtYear, x.DtMonth });
                    table.ForeignKey(
                        name: "FK_AttendanceSummaries_Companies_ComId",
                        column: x => x.ComId,
                        principalTable: "Companies",
                        principalColumn: "ComId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_AttendanceSummaries_Employees_EmpId",
                        column: x => x.EmpId,
                        principalTable: "Employees",
                        principalColumn: "EmpId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AttendanceSummaries_EmpId",
                table: "AttendanceSummaries",
                column: "EmpId");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_ComId",
                table: "Departments",
                column: "ComId");

            migrationBuilder.CreateIndex(
                name: "IX_Designations_ComId",
                table: "Designations",
                column: "ComId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ComId",
                table: "Employees",
                column: "ComId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DeptId",
                table: "Employees",
                column: "DeptId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DesigId",
                table: "Employees",
                column: "DesigId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ShiftId",
                table: "Employees",
                column: "ShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_Shifts_ComId",
                table: "Shifts",
                column: "ComId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AttendanceSummaries");

            migrationBuilder.DropTable(
                name: "Attendence");

            migrationBuilder.DropTable(
                name: "Salaries");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Designations");

            migrationBuilder.DropTable(
                name: "Shifts");

            migrationBuilder.DropTable(
                name: "Companies");
        }
    }
}
