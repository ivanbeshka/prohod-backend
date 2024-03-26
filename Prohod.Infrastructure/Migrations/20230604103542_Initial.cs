using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Prohod.Domain.Users;
using Prohod.Domain.VisitRequests;

#nullable disable

namespace Prohod.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:role", "user,security,admin")
                .Annotation("Npgsql:Enum:visit_request_status", "not_processed,reject,accept");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Surname = table.Column<string>(type: "text", nullable: false),
                    UserEmail = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<Role>(type: "role", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AssociatedUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Login = table.Column<string>(type: "text", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accounts_Users_AssociatedUserId",
                        column: x => x.AssociatedUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Forms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Passport_FullName = table.Column<string>(type: "text", nullable: false),
                    Passport_Series = table.Column<string>(type: "text", nullable: false),
                    Passport_Number = table.Column<string>(type: "text", nullable: false),
                    Passport_WhoIssued = table.Column<string>(type: "text", nullable: false),
                    Passport_IssueDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    VisitTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    VisitReason = table.Column<string>(type: "text", nullable: false),
                    UserToVisitId = table.Column<Guid>(type: "uuid", nullable: false),
                    EmailToSendReply = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Forms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Forms_Users_UserToVisitId",
                        column: x => x.UserToVisitId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VisitRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FormId = table.Column<Guid>(type: "uuid", nullable: false),
                    WhoProcessedId = table.Column<Guid>(type: "uuid", nullable: true),
                    Status = table.Column<VisitRequestStatus>(type: "visit_request_status", nullable: false),
                    RejectionReason = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisitRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VisitRequests_Forms_FormId",
                        column: x => x.FormId,
                        principalTable: "Forms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VisitRequests_Users_WhoProcessedId",
                        column: x => x.WhoProcessedId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_AssociatedUserId",
                table: "Accounts",
                column: "AssociatedUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_Login",
                table: "Accounts",
                column: "Login",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Forms_UserToVisitId",
                table: "Forms",
                column: "UserToVisitId");

            migrationBuilder.CreateIndex(
                name: "IX_VisitRequests_FormId",
                table: "VisitRequests",
                column: "FormId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VisitRequests_WhoProcessedId",
                table: "VisitRequests",
                column: "WhoProcessedId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "VisitRequests");

            migrationBuilder.DropTable(
                name: "Forms");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
