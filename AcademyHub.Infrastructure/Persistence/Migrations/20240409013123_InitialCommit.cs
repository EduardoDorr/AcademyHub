using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AcademyHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCommit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "AcademyHub");

            migrationBuilder.CreateTable(
                name: "CourseModules",
                schema: "AcademyHub",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseModules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                schema: "AcademyHub",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Cover = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LearningTracks",
                schema: "AcademyHub",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Cover = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LearningTracks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Lessons",
                schema: "AcademyHub",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    VideoLink = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    AverageRating = table.Column<decimal>(type: "numeric(5,2)", nullable: false),
                    NumberOfRatings = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lessons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OutboxMessages",
                schema: "AcademyHub",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Error = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    ProcessedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutboxMessages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subscriptions",
                schema: "AcademyHub",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscriptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "AcademyHub",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Cpf = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Telephone = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CourseCourseModule",
                schema: "AcademyHub",
                columns: table => new
                {
                    CourseModulesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CoursesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseCourseModule", x => new { x.CourseModulesId, x.CoursesId });
                    table.ForeignKey(
                        name: "FK_CourseCourseModule_CourseModules_CourseModulesId",
                        column: x => x.CourseModulesId,
                        principalSchema: "AcademyHub",
                        principalTable: "CourseModules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseCourseModule_Courses_CoursesId",
                        column: x => x.CoursesId,
                        principalSchema: "AcademyHub",
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LearningTrackCourse",
                schema: "AcademyHub",
                columns: table => new
                {
                    LearningTracksId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CoursesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LearningTrackCourse", x => new { x.LearningTracksId, x.CoursesId});
                    table.ForeignKey(
                        name: "FK_LearningTrackCourse_Courses_CoursesId",
                        column: x => x.CoursesId,
                        principalSchema: "AcademyHub",
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LearningTrackCourse_LearningTracks_LearningTracksId",
                        column: x => x.LearningTracksId,
                        principalSchema: "AcademyHub",
                        principalTable: "LearningTracks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseModuleLesson",
                schema: "AcademyHub",
                columns: table => new
                {
                    CourseModulesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LessonsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseModuleLesson", x => new { x.CourseModulesId, x.LessonsId });
                    table.ForeignKey(
                        name: "FK_CourseModuleLesson_CourseModules_CourseModulesId",
                        column: x => x.CourseModulesId,
                        principalSchema: "AcademyHub",
                        principalTable: "CourseModules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseModuleLesson_Lessons_LessonsId",
                        column: x => x.LessonsId,
                        principalSchema: "AcademyHub",
                        principalTable: "Lessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionLearningTrack",
                schema: "AcademyHub",
                columns: table => new
                {
                    SubscriptionsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LearningTracksId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LearningTrackSubscription", x => new { x.SubscriptionsId, x.LearningTracksId});
                    table.ForeignKey(
                        name: "FK_SubscriptionLearningTrack_LearningTracks_LearningTracksId",
                        column: x => x.LearningTracksId,
                        principalSchema: "AcademyHub",
                        principalTable: "LearningTracks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubscriptionLearningTrack_Subscriptions_SubscriptionsId",
                        column: x => x.SubscriptionsId,
                        principalSchema: "AcademyHub",
                        principalTable: "Subscriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Enrollments",
                schema: "AcademyHub",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubscriptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enrollments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Enrollments_Subscriptions_SubscriptionId",
                        column: x => x.SubscriptionId,
                        principalSchema: "AcademyHub",
                        principalTable: "Subscriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Enrollments_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "AcademyHub",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LessonFinisheds",
                schema: "AcademyHub",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LessonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    FinishDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonFinisheds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LessonFinisheds_Lessons_LessonId",
                        column: x => x.LessonId,
                        principalSchema: "AcademyHub",
                        principalTable: "Lessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LessonFinisheds_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "AcademyHub",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseCourseModule_CoursesId",
                schema: "AcademyHub",
                table: "CourseCourseModule",
                column: "CoursesId");

            migrationBuilder.CreateIndex(
                name: "IX_LearningTrackCourse_LearningTracksId",
                schema: "AcademyHub",
                table: "LearningTrackCourse",
                column: "LearningTracksId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseModuleLesson_LessonsId",
                schema: "AcademyHub",
                table: "CourseModuleLesson",
                column: "LessonsId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseModules_Active",
                schema: "AcademyHub",
                table: "CourseModules",
                column: "Active");

            migrationBuilder.CreateIndex(
                name: "IX_CourseModules_Name",
                schema: "AcademyHub",
                table: "CourseModules",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Courses_Active",
                schema: "AcademyHub",
                table: "Courses",
                column: "Active");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_Name",
                schema: "AcademyHub",
                table: "Courses",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_Active",
                schema: "AcademyHub",
                table: "Enrollments",
                column: "Active");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_SubscriptionId",
                schema: "AcademyHub",
                table: "Enrollments",
                column: "SubscriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_UserId",
                schema: "AcademyHub",
                table: "Enrollments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_LearningTracks_Active",
                schema: "AcademyHub",
                table: "LearningTracks",
                column: "Active");

            migrationBuilder.CreateIndex(
                name: "IX_LearningTracks_Name",
                schema: "AcademyHub",
                table: "LearningTracks",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionLearningTrack_SubscriptionsId",
                schema: "AcademyHub",
                table: "SubscriptionLearningTrack",
                column: "SubscriptionsId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonFinisheds_Active",
                schema: "AcademyHub",
                table: "LessonFinisheds",
                column: "Active");

            migrationBuilder.CreateIndex(
                name: "IX_LessonFinisheds_LessonId",
                schema: "AcademyHub",
                table: "LessonFinisheds",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonFinisheds_UserId",
                schema: "AcademyHub",
                table: "LessonFinisheds",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_Active",
                schema: "AcademyHub",
                table: "Lessons",
                column: "Active");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_Name",
                schema: "AcademyHub",
                table: "Lessons",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_Active",
                schema: "AcademyHub",
                table: "Subscriptions",
                column: "Active");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_Name",
                schema: "AcademyHub",
                table: "Subscriptions",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Active",
                schema: "AcademyHub",
                table: "Users",
                column: "Active");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Cpf",
                schema: "AcademyHub",
                table: "Users",
                column: "Cpf",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                schema: "AcademyHub",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseCourseModule",
                schema: "AcademyHub");

            migrationBuilder.DropTable(
                name: "LearningTrackCourse",
                schema: "AcademyHub");

            migrationBuilder.DropTable(
                name: "CourseModuleLesson",
                schema: "AcademyHub");

            migrationBuilder.DropTable(
                name: "Enrollments",
                schema: "AcademyHub");

            migrationBuilder.DropTable(
                name: "SubscriptionLearningTrack",
                schema: "AcademyHub");

            migrationBuilder.DropTable(
                name: "LessonFinisheds",
                schema: "AcademyHub");

            migrationBuilder.DropTable(
                name: "OutboxMessages",
                schema: "AcademyHub");

            migrationBuilder.DropTable(
                name: "Courses",
                schema: "AcademyHub");

            migrationBuilder.DropTable(
                name: "CourseModules",
                schema: "AcademyHub");

            migrationBuilder.DropTable(
                name: "LearningTracks",
                schema: "AcademyHub");

            migrationBuilder.DropTable(
                name: "Subscriptions",
                schema: "AcademyHub");

            migrationBuilder.DropTable(
                name: "Lessons",
                schema: "AcademyHub");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "AcademyHub");
        }
    }
}
