using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Pgvector;

#nullable disable

namespace Trivo.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("CREATE EXTENSION IF NOT EXISTS vector;");

            migrationBuilder.CreateTable(
                name: "Administrator",
                columns: table => new
                {
                    PKAdministratorId = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    LastName = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Biography = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    PasswordHash = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Username = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ProfilePicture = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    LinkedIn = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PKAdministratorId", x => x.PKAdministratorId);
                });

            migrationBuilder.CreateTable(
                name: "Chat",
                columns: table => new
                {
                    PKChatId = table.Column<Guid>(type: "uuid", nullable: false),
                    ChatType = table.Column<string>(type: "varchar(50)", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PKChatId", x => x.PKChatId);
                });

            migrationBuilder.CreateTable(
                name: "InterestCategory",
                columns: table => new
                {
                    PKCategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PKInterestCategoryId", x => x.PKCategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Skill",
                columns: table => new
                {
                    PKSkillId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    RegisteredAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PKSkillId", x => x.PKSkillId);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    PKUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    LastName = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Biography = table.Column<string>(type: "text", nullable: false),
                    IsAccountConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    Email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Username = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Location = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    ProfilePicture = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    LinkedIn = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    UserStatus = table.Column<string>(type: "varchar(50)", nullable: false),
                    Position = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ProfileEmbedding = table.Column<Vector>(type: "vector(1536)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PKUserId", x => x.PKUserId);
                });

            migrationBuilder.CreateTable(
                name: "ChatUser",
                columns: table => new
                {
                    FKChatId = table.Column<Guid>(type: "uuid", nullable: false),
                    FKUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ChatName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    JoinedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LeftAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatUser", x => new { x.FKChatId, x.FKUserId });
                    table.ForeignKey(
                        name: "FK_ChatUser_Chat_FKChatId",
                        column: x => x.FKChatId,
                        principalTable: "Chat",
                        principalColumn: "PKChatId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChatUser_User_FKUserId",
                        column: x => x.FKUserId,
                        principalTable: "User",
                        principalColumn: "PKUserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Code",
                columns: table => new
                {
                    PKCodeId = table.Column<Guid>(type: "uuid", nullable: false),
                    FKUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false),
                    IsUsed = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    Type = table.Column<string>(type: "varchar(50)", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsRevoked = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    RefreshCode = table.Column<bool>(type: "boolean", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PKCodeId", x => x.PKCodeId);
                    table.ForeignKey(
                        name: "FK_Code_User_FKUserId",
                        column: x => x.FKUserId,
                        principalTable: "User",
                        principalColumn: "PKUserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Expert",
                columns: table => new
                {
                    PKExpertId = table.Column<Guid>(type: "uuid", nullable: false),
                    FKUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    AvailableForProjects = table.Column<bool>(type: "boolean", nullable: false),
                    IsHired = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PKExpertId", x => x.PKExpertId);
                    table.ForeignKey(
                        name: "FK_Expert_User_FKUserId",
                        column: x => x.FKUserId,
                        principalTable: "User",
                        principalColumn: "PKUserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Interest",
                columns: table => new
                {
                    PKInterestId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    FKCategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    FKCreatedById = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PKInterestId", x => x.PKInterestId);
                    table.ForeignKey(
                        name: "FK_Interest_InterestCategory_FKCategoryId",
                        column: x => x.FKCategoryId,
                        principalTable: "InterestCategory",
                        principalColumn: "PKCategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Interest_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "PKUserId");
                });

            migrationBuilder.CreateTable(
                name: "Message",
                columns: table => new
                {
                    MessageId = table.Column<Guid>(type: "uuid", nullable: false),
                    FKChatId = table.Column<Guid>(type: "uuid", nullable: false),
                    FKSenderId = table.Column<Guid>(type: "uuid", nullable: false),
                    FKReceiverId = table.Column<Guid>(type: "uuid", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<string>(type: "varchar(50)", nullable: true),
                    Status = table.Column<string>(type: "varchar(50)", nullable: false),
                    SentAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PKMessageId", x => x.MessageId);
                    table.ForeignKey(
                        name: "FK_Message_Chat_FKChatId",
                        column: x => x.FKChatId,
                        principalTable: "Chat",
                        principalColumn: "PKChatId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Message_User_FKReceiverId",
                        column: x => x.FKReceiverId,
                        principalTable: "User",
                        principalColumn: "PKUserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Message_User_FKSenderId",
                        column: x => x.FKSenderId,
                        principalTable: "User",
                        principalColumn: "PKUserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    PKNotificationId = table.Column<Guid>(type: "uuid", nullable: false),
                    FKUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<string>(type: "varchar(50)", nullable: false),
                    Content = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsRead = table.Column<bool>(type: "boolean", nullable: false),
                    ReadAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PKNotificationId", x => x.PKNotificationId);
                    table.ForeignKey(
                        name: "FK_Notification_User_FKUserId",
                        column: x => x.FKUserId,
                        principalTable: "User",
                        principalColumn: "PKUserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Recruiter",
                columns: table => new
                {
                    PKRecruiterId = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    FKUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PKRecruiterId", x => x.PKRecruiterId);
                    table.ForeignKey(
                        name: "FK_Recruiter_User_FKUserId",
                        column: x => x.FKUserId,
                        principalTable: "User",
                        principalColumn: "PKUserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserSkill",
                columns: table => new
                {
                    FKUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    FKSkillId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSkill", x => new { x.FKUserId, x.FKSkillId });
                    table.ForeignKey(
                        name: "FK_UserSkill_Skill_FKSkillId",
                        column: x => x.FKSkillId,
                        principalTable: "Skill",
                        principalColumn: "PKSkillId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserSkill_User_FKUserId",
                        column: x => x.FKUserId,
                        principalTable: "User",
                        principalColumn: "PKUserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserInterest",
                columns: table => new
                {
                    FKUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    FKInterestId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInterest", x => new { x.FKUserId, x.FKInterestId });
                    table.ForeignKey(
                        name: "FK_UserInterest_Interest_FKInterestId",
                        column: x => x.FKInterestId,
                        principalTable: "Interest",
                        principalColumn: "PKInterestId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserInterest_User_FKUserId",
                        column: x => x.FKUserId,
                        principalTable: "User",
                        principalColumn: "PKUserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Report",
                columns: table => new
                {
                    PKReportId = table.Column<Guid>(type: "uuid", nullable: false),
                    FKReportedById = table.Column<Guid>(type: "uuid", nullable: false),
                    FKMessageId = table.Column<Guid>(type: "uuid", nullable: false),
                    ReportStatus = table.Column<string>(type: "varchar(50)", nullable: false),
                    Note = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PKReportId", x => x.PKReportId);
                    table.ForeignKey(
                        name: "FK_Report_Message_FKMessageId",
                        column: x => x.FKMessageId,
                        principalTable: "Message",
                        principalColumn: "MessageId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Report_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "PKUserId");
                });

            migrationBuilder.CreateTable(
                name: "Match",
                columns: table => new
                {
                    PKMatchId = table.Column<Guid>(type: "uuid", nullable: false),
                    FKRecruiterId = table.Column<Guid>(type: "uuid", nullable: false),
                    FKExpertId = table.Column<Guid>(type: "uuid", nullable: false),
                    ExpertStatus = table.Column<string>(type: "varchar(50)", nullable: false),
                    RecruiterStatus = table.Column<string>(type: "varchar(50)", nullable: false),
                    MatchStatus = table.Column<string>(type: "varchar(50)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PKMatchId", x => x.PKMatchId);
                    table.ForeignKey(
                        name: "FK_Match_Expert_FKExpertId",
                        column: x => x.FKExpertId,
                        principalTable: "Expert",
                        principalColumn: "PKExpertId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Match_Recruiter_FKRecruiterId",
                        column: x => x.FKRecruiterId,
                        principalTable: "Recruiter",
                        principalColumn: "PKRecruiterId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "UQAdministratorEmail",
                table: "Administrator",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQAdministratorUsername",
                table: "Administrator",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChatUser_FKUserId",
                table: "ChatUser",
                column: "FKUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Code_FKUserId",
                table: "Code",
                column: "FKUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Expert_FKUserId",
                table: "Expert",
                column: "FKUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Interest_FKCategoryId",
                table: "Interest",
                column: "FKCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Interest_UserId",
                table: "Interest",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Match_FKExpertId",
                table: "Match",
                column: "FKExpertId");

            migrationBuilder.CreateIndex(
                name: "IX_Match_FKRecruiterId",
                table: "Match",
                column: "FKRecruiterId");

            migrationBuilder.CreateIndex(
                name: "IX_Message_FKChatId",
                table: "Message",
                column: "FKChatId");

            migrationBuilder.CreateIndex(
                name: "IX_Message_FKReceiverId",
                table: "Message",
                column: "FKReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_Message_FKSenderId",
                table: "Message",
                column: "FKSenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_FKUserId",
                table: "Notification",
                column: "FKUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Recruiter_FKUserId",
                table: "Recruiter",
                column: "FKUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Report_FKMessageId",
                table: "Report",
                column: "FKMessageId");

            migrationBuilder.CreateIndex(
                name: "IX_Report_UserId",
                table: "Report",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "UQUserEmail",
                table: "User",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQUserUsername",
                table: "User",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserInterest_FKInterestId",
                table: "UserInterest",
                column: "FKInterestId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSkill_FKSkillId",
                table: "UserSkill",
                column: "FKSkillId");

            migrationBuilder.Sql(
                "CREATE INDEX IF NOT EXISTS ix_users_profile_embedding_hnsw " +
                "ON \"User\" USING hnsw (\"ProfileEmbedding\" vector_cosine_ops);");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Administrator");

            migrationBuilder.DropTable(
                name: "ChatUser");

            migrationBuilder.DropTable(
                name: "Code");

            migrationBuilder.DropTable(
                name: "Match");

            migrationBuilder.DropTable(
                name: "Notification");

            migrationBuilder.DropTable(
                name: "Report");

            migrationBuilder.DropTable(
                name: "UserInterest");

            migrationBuilder.DropTable(
                name: "UserSkill");

            migrationBuilder.DropTable(
                name: "Expert");

            migrationBuilder.DropTable(
                name: "Recruiter");

            migrationBuilder.DropTable(
                name: "Message");

            migrationBuilder.DropTable(
                name: "Interest");

            migrationBuilder.DropTable(
                name: "Skill");

            migrationBuilder.DropTable(
                name: "Chat");

            migrationBuilder.DropTable(
                name: "InterestCategory");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
