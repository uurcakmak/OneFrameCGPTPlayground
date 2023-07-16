// <copyright file="20230716172117_InitDb.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>
using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OneFrameCGPTPlayground.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicationSettingCategory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationSettingCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AuditLog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AuditData = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EntityType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuditDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AuditUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TablePk = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmailNotification",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    From = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    To = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bcc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsSent = table.Column<bool>(type: "bit", nullable: false),
                    InsertedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InsertedUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RetryCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailNotification", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmailTemplate",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Bcc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InsertedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InsertedUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    To = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedUser = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailTemplate", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EventLog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AuditData = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuditDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AuditUser = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventLog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Language",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    InsertedUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InsertedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Language", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LoginAuditLog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApplicationUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BrowserDetail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BrowserGuid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hostname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InsertedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InsertedUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ip = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MacAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OsName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestHeaderInfo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Success = table.Column<bool>(type: "bit", nullable: false),
                    RequestUserName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginAuditLog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Menu",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false, defaultValue: 999),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menu", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Menu_Menu_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Menu",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TestEncryptionEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestEncryptionEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InsertedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InsertedUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LastPasswordChangedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProfilePhoto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeZone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationSetting",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Key = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ValueType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsStatic = table.Column<bool>(type: "bit", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationSetting", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationSetting_ApplicationSettingCategory_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "ApplicationSettingCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmailTemplateTranslation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(max)", maxLength: 2147483647, nullable: false),
                    EmailContent = table.Column<string>(type: "nvarchar(max)", maxLength: 2147483647, nullable: false),
                    Language = table.Column<int>(type: "int", nullable: false),
                    ReferenceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailTemplateTranslation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmailTemplateTranslation_EmailTemplate_ReferenceId",
                        column: x => x.ReferenceId,
                        principalTable: "EmailTemplate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MenuTranslation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DisplayText = table.Column<string>(type: "nvarchar(max)", maxLength: 2147483647, nullable: false),
                    Language = table.Column<int>(type: "int", nullable: false),
                    ReferenceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuTranslation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MenuTranslation_Menu_ReferenceId",
                        column: x => x.ReferenceId,
                        principalTable: "Menu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaim",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaim_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleTranslation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DisplayText = table.Column<string>(type: "nvarchar(max)", maxLength: 2147483647, nullable: false),
                    Language = table.Column<int>(type: "int", nullable: false),
                    ReferenceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleTranslation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleTranslation_Role_ReferenceId",
                        column: x => x.ReferenceId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserClaim",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaim_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserConfirmationHistory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CodeType = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpiredDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsSent = table.Column<bool>(type: "bit", nullable: false),
                    SentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsUsed = table.Column<bool>(type: "bit", nullable: false),
                    UsedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserConfirmationHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserConfirmationHistory_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLogin",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogin", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_UserLogin_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserPasswordHistory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InsertedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InsertedUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPasswordHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserPasswordHistory_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRole_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRole_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserToken",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsActivated = table.Column<bool>(type: "bit", nullable: false),
                    SentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserToken", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UserToken_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ApplicationSettingCategory",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("0637d6bb-ea07-4d50-a9cb-e6be151e743a"), "Startup configurations for all applications", "system-shared" },
                    { new Guid("528f6ed0-dea7-4364-9670-15982c59b7ff"), "Startup specific configurations for Web API", "system-web-api" },
                    { new Guid("5482df4f-158e-404f-8b82-f86a5c3f0f19"), "Startup specific configurations for React UI", "system-react" },
                    { new Guid("d438be21-d852-4417-abf7-f2c188176859"), "Startup specific configurations for MVC UI", "system-mvc-ui" },
                    { new Guid("f08bb24d-6b2c-4c48-bb55-6e7cd1c233c1"), "Startup specific configurations for Hangfire", "system-hangfire" }
                });

            migrationBuilder.InsertData(
                table: "EmailTemplate",
                columns: new[] { "Id", "Bcc", "Cc", "InsertedDate", "InsertedUser", "IsDeleted", "Name", "To", "UpdatedDate", "UpdatedUser" },
                values: new object[,]
                {
                    { new Guid("3d9c744e-5d4a-4f52-822b-e777e301b7cf"), null, null, null, null, false, "Welcome", null, null, null },
                    { new Guid("9459e146-558a-44c5-972e-28a2469b4d1a"), null, null, null, null, false, "ForgotPassword", null, null, null },
                    { new Guid("b811c8ca-d12f-404b-affd-a5dc6e402945"), null, null, null, null, false, "TwoFAVerificationCode", null, null, null },
                    { new Guid("db0eada7-a618-435e-834c-919ac95a97ce"), null, null, null, null, false, "EmailActivation", null, null, null }
                });

            migrationBuilder.InsertData(
                table: "Language",
                columns: new[] { "Id", "Code", "Image", "InsertedDate", "InsertedUser", "IsActive", "IsDefault", "IsDeleted", "Name", "UpdatedDate", "UpdatedUser" },
                values: new object[,]
                {
                    { new Guid("5a41be5e-0cb9-4a3e-a1a7-0244b53134cc"), "en-EN", "data:image/svg+xml;base64,PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0iaXNvLTg4NTktMSI/Pg0KPCEtLSBHZW5lcmF0b3I6IEFkb2JlIElsbHVzdHJhdG9yIDE5LjAuMCwgU1ZHIEV4cG9ydCBQbHVnLUluIC4gU1ZHIFZlcnNpb246IDYuMDAgQnVpbGQgMCkgIC0tPg0KPHN2ZyB4bWxucz0iaHR0cDovL3d3dy53My5vcmcvMjAwMC9zdmciIHhtbG5zOnhsaW5rPSJodHRwOi8vd3d3LnczLm9yZy8xOTk5L3hsaW5rIiB2ZXJzaW9uPSIxLjEiIGlkPSJMYXllcl8xIiB4PSIwcHgiIHk9IjBweCIgdmlld0JveD0iMCAwIDUxMiA1MTIiIHN0eWxlPSJlbmFibGUtYmFja2dyb3VuZDpuZXcgMCAwIDUxMiA1MTI7IiB4bWw6c3BhY2U9InByZXNlcnZlIj4NCjxnPg0KCTxwb2x5Z29uIHN0eWxlPSJmaWxsOiM0MTQ3OUI7IiBwb2ludHM9IjE4OC42MzIsMCAwLDAgMCwzOS45NTQgMTg4LjYzMiwxNjMuNTQgICIvPg0KCTxwb2x5Z29uIHN0eWxlPSJmaWxsOiM0MTQ3OUI7IiBwb2ludHM9IjAsMTM2LjU5OCAwLDE4OC42MzIgNzkuNDE5LDE4OC42MzIgICIvPg0KCTxwb2x5Z29uIHN0eWxlPSJmaWxsOiM0MTQ3OUI7IiBwb2ludHM9IjAsMzIzLjM2OSAwLDM3NS40MDIgNzkuNDE5LDMyMy4zNjkgICIvPg0KCTxwb2x5Z29uIHN0eWxlPSJmaWxsOiM0MTQ3OUI7IiBwb2ludHM9IjUxMiwzOS45NTQgNTEyLDAgMzIzLjM2OCwwIDMyMy4zNjgsMTYzLjU0ICAiLz4NCgk8cG9seWdvbiBzdHlsZT0iZmlsbDojNDE0NzlCOyIgcG9pbnRzPSI1MTIsMzc1LjQwMiA1MTIsMzIzLjM2OSA0MzIuNTgxLDMyMy4zNjkgICIvPg0KCTxwb2x5Z29uIHN0eWxlPSJmaWxsOiM0MTQ3OUI7IiBwb2ludHM9IjMyMy4zNjgsMzQ4LjQ2IDMyMy4zNjgsNTEyIDUxMiw1MTIgNTEyLDQ3Mi4wNDYgICIvPg0KCTxwb2x5Z29uIHN0eWxlPSJmaWxsOiM0MTQ3OUI7IiBwb2ludHM9IjUxMiwxODguNjMyIDUxMiwxMzYuNTk4IDQzMi41ODEsMTg4LjYzMiAgIi8+DQoJPHBvbHlnb24gc3R5bGU9ImZpbGw6IzQxNDc5QjsiIHBvaW50cz0iMCw0NzIuMDQ2IDAsNTEyIDE4OC42MzIsNTEyIDE4OC42MzIsMzQ4LjQ2ICAiLz4NCjwvZz4NCjxnPg0KCTxwb2x5Z29uIHN0eWxlPSJmaWxsOiNGNUY1RjU7IiBwb2ludHM9IjUxMiw0NzIuMDQ2IDUxMiw0MzIuMTE4IDM0NC40NjUsMzIzLjM2OSAzOTMuOTYxLDMyMy4zNjkgNTEyLDM5OS45ODkgNTEyLDM3NS40MDIgICAgNDMyLjU4MSwzMjMuMzY5IDUxMiwzMjMuMzY5IDUxMiwyOTYuNDIxIDI5Ni40MjEsMjk2LjQyMSAyOTYuNDIxLDUxMiAzMjMuMzY4LDUxMiAzMjMuMzY4LDM0OC40NiAgIi8+DQoJPHBvbHlnb24gc3R5bGU9ImZpbGw6I0Y1RjVGNTsiIHBvaW50cz0iMCwyOTYuNDIxIDAsMzIzLjM2OSA3OS40MTksMzIzLjM2OSAwLDM3NS40MDIgMCw0MTMuMjAzIDEzOC4zOTUsMzIzLjM2OSAxODcuODkxLDMyMy4zNjkgICAgMCw0NDUuMzMyIDAsNDcyLjA0NiAxODguNjMyLDM0OC40NiAxODguNjMyLDUxMiAyMTUuNTc5LDUxMiAyMTUuNTc5LDI5Ni40MjEgICIvPg0KCTxwb2x5Z29uIHN0eWxlPSJmaWxsOiNGNUY1RjU7IiBwb2ludHM9IjIxNS41NzksMCAxODguNjMyLDAgMTg4LjYzMiwxNjMuNTQgMCwzOS45NTQgMCw4My42NzkgMTYxLjY4NCwxODguNjMyIDExMi4xODgsMTg4LjYzMiAgICAwLDExNS44MDcgMCwxMzYuNTk4IDc5LjQxOSwxODguNjMyIDAsMTg4LjYzMiAwLDIxNS41NzkgMjE1LjU3OSwyMTUuNTc5ICAiLz4NCgk8cG9seWdvbiBzdHlsZT0iZmlsbDojRjVGNUY1OyIgcG9pbnRzPSI1MTIsMjE1LjU3OSA1MTIsMTg4LjYzMiA0MzIuNTgxLDE4OC42MzIgNTEyLDEzNi41OTggNTEyLDk4LjMxNCAzNzIuODY0LDE4OC42MzIgICAgMzIzLjM2OCwxODguNjMyIDUxMiw2Ni4xODUgNTEyLDM5Ljk1NCAzMjMuMzY4LDE2My41NCAzMjMuMzY4LDAgMjk2LjQyMSwwIDI5Ni40MjEsMjE1LjU3OSAgIi8+DQo8L2c+DQo8Zz4NCgk8cG9seWdvbiBzdHlsZT0iZmlsbDojRkY0QjU1OyIgcG9pbnRzPSI1MTIsMjk2LjQyMSA1MTIsMjE1LjU3OSAyOTYuNDIxLDIxNS41NzkgMjk2LjQyMSwwIDIxNS41NzksMCAyMTUuNTc5LDIxNS41NzkgMCwyMTUuNTc5ICAgIDAsMjk2LjQyMSAyMTUuNTc5LDI5Ni40MjEgMjE1LjU3OSw1MTIgMjk2LjQyMSw1MTIgMjk2LjQyMSwyOTYuNDIxICAiLz4NCgk8cG9seWdvbiBzdHlsZT0iZmlsbDojRkY0QjU1OyIgcG9pbnRzPSIxMzguMzk1LDMyMy4zNjkgMCw0MTMuMjAzIDAsNDQ1LjMzMiAxODcuODkxLDMyMy4zNjkgICIvPg0KCTxwb2x5Z29uIHN0eWxlPSJmaWxsOiNGRjRCNTU7IiBwb2ludHM9IjM0NC40NjUsMzIzLjM2OSA1MTIsNDMyLjExOCA1MTIsMzk5Ljk4OSAzOTMuOTYxLDMyMy4zNjkgICIvPg0KCTxwb2x5Z29uIHN0eWxlPSJmaWxsOiNGRjRCNTU7IiBwb2ludHM9IjE2MS42ODQsMTg4LjYzMiAwLDgzLjY3OSAwLDExNS44MDcgMTEyLjE4OCwxODguNjMyICAiLz4NCgk8cG9seWdvbiBzdHlsZT0iZmlsbDojRkY0QjU1OyIgcG9pbnRzPSIzNzIuODY0LDE4OC42MzIgNTEyLDk4LjMxNCA1MTIsNjYuMTg1IDMyMy4zNjgsMTg4LjYzMiAgIi8+DQo8L2c+DQo8Zz4NCjwvZz4NCjxnPg0KPC9nPg0KPGc+DQo8L2c+DQo8Zz4NCjwvZz4NCjxnPg0KPC9nPg0KPGc+DQo8L2c+DQo8Zz4NCjwvZz4NCjxnPg0KPC9nPg0KPGc+DQo8L2c+DQo8Zz4NCjwvZz4NCjxnPg0KPC9nPg0KPGc+DQo8L2c+DQo8Zz4NCjwvZz4NCjxnPg0KPC9nPg0KPGc+DQo8L2c+DQo8L3N2Zz4NCg==", new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, true, false, "English", new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("a5c5d7bd-b5bf-4a71-bc1c-3738ea14297f"), "ar-AE", "data:image/svg+xml;base64,PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0iaXNvLTg4NTktMSI/Pg0KPCEtLSBHZW5lcmF0b3I6IEFkb2JlIElsbHVzdHJhdG9yIDE5LjAuMCwgU1ZHIEV4cG9ydCBQbHVnLUluIC4gU1ZHIFZlcnNpb246IDYuMDAgQnVpbGQgMCkgIC0tPg0KPHN2ZyB2ZXJzaW9uPSIxLjEiIGlkPSJMYXllcl8xIiB4bWxucz0iaHR0cDovL3d3dy53My5vcmcvMjAwMC9zdmciIHhtbG5zOnhsaW5rPSJodHRwOi8vd3d3LnczLm9yZy8xOTk5L3hsaW5rIiB4PSIwcHgiIHk9IjBweCINCgkgdmlld0JveD0iMCAwIDUxMiA1MTIiIHN0eWxlPSJlbmFibGUtYmFja2dyb3VuZDpuZXcgMCAwIDUxMiA1MTI7IiB4bWw6c3BhY2U9InByZXNlcnZlIj4NCjxyZWN0IHN0eWxlPSJmaWxsOiM3M0FGMDA7IiB3aWR0aD0iNTEyIiBoZWlnaHQ9IjUxMiIvPg0KPGc+DQoJPHBhdGggc3R5bGU9ImZpbGw6I0Y1RjVGNTsiIGQ9Ik03Ni44NTQsMTkxLjczM2MxLjAzOSw2LjY1Nyw1LjA2Myw4Ljc0Nyw4LjU2Miw4LjQ3NWM0LjEyNi0wLjMyLDcuNzQ1LTQuMDkzLDcuNzQ1LTguMDc3di0yMi44NDgNCgkJYzAtMS4xNDQsMC45MzEtMi4wNzcsMi4wNzctMi4wNzdzMi4wNzcsMC45MzMsMi4wNzcsMi4wNzd2MjIuOTQ2YzAsMi43NTksMS4zNjMsNS4zMzEsMy42NDksNi44NzYNCgkJYzEuMzk2LDAuOTQ1LDMuMDIsMS40MzIsNC42NTcsMS40MzJjMS4wNDMsMCwyLjA4OS0wLjE5OSwzLjA4Ny0wLjU5NmwyLjA3Ni0wLjgzYzAuMTkxLDE5LjE3OS0wLjk3NSwzNi4wMzUtMC45OTcsMzYuMzQ0DQoJCWMtMC4xNjIsMi4yODgsMS41Niw0LjI3NiwzLjg0OCw0LjQzOGMwLjEwMSwwLjAwOCwwLjIwMSwwLjAxMiwwLjMsMC4wMTJjMi4xNiwwLDMuOTg0LTEuNjcxLDQuMTQtMy44NTgNCgkJYzAuMDk3LTEuMzUxLDEuMzc3LTE5LjgxLDAuOTkzLTQwLjI1bDEuMjU1LTAuNTAyYzEuMTAxLTAuNDM4LDIuMzM3LTAuMzg1LDMuMzk2LDAuMTQyYzQuNTc2LDIuMjgsMTAuMDgxLDEuMzkyLDEzLjY5OC0yLjIyMw0KCQlsMC43NDgtMC43NDZjMy4zNTctMy4zNjMsNC41MTEtOC4yNDgsMy4wMDgtMTIuNzU1bC0zLjUzOC0xMC42MTNjMS45MTMsMC41NzYsNC4wMTQtMC4yNzIsNC45MzctMi4xMTQNCgkJYzEuMDI2LTIuMDUzLDAuMTk1LTQuNTQ4LTEuODU4LTUuNTc0bC04LjMwOS00LjE1NGMtMS41MDMtMC43NTUtMy4zMDYtMC41MjMtNC41OCwwLjU3NmMtMS4yNywxLjEwMy0xLjc1MSwyLjg2LTEuMjE5LDQuNDU0DQoJCWw2LjY4NCwyMC4wNTNjMC41MDEsMS41MDEsMC4xMTYsMy4xMzItMS4wMDIsNC4yNTJsLTAuNzQ2LDAuNzQ2Yy0xLjA4MywxLjA4My0yLjczMiwxLjM0Ny00LjExMiwwLjY2NQ0KCQljLTIuNjkzLTEuMzQ1LTUuNzU3LTEuNTk3LTguNjMzLTAuODY0Yy0wLjU4NC0xMy4zMzQtMi4wMi0yNi42MTItNS4wNzktMzUuNzljLTAuNzI0LTIuMTc0LTMuMDczLTMuMzU1LTUuMjU2LTIuNjI1DQoJCWMtMi4xNzcsMC43MjItMy4zNTMsMy4wNzUtMi42MjcsNS4yNTRjMi45OTMsOC45NzcsNC4yOTgsMjIuNjU3LDQuNzU5LDM2LjI1OWwtNC45NzIsMS45ODl2LTIyLjk0Ng0KCQljMC01LjcyOC00LjY1OS0xMC4zODYtMTAuMzg2LTEwLjM4NnMtMTAuMzg2LDQuNjU3LTEwLjM4NiwxMC4zODZ2MjAuMTFsLTQuMjM3LTIwLjkzNGMtMC40NTItMi4yNTItMi42NDEtMy43Mi00Ljg5NS0zLjI1DQoJCWMtMi4yNSwwLjQ1OC0zLjcwNCwyLjY0OS0zLjI0OCw0Ljg5N0w3Ni44NTQsMTkxLjczM3oiLz4NCgk8cGF0aCBzdHlsZT0iZmlsbDojRjVGNUY1OyIgZD0iTTg0Ljg1MiwxNTYuODJjMi4yOTQsMCw0LjE1NC0xLjg1OCw0LjE1NC00LjE1NHYtNC4xNTRjMC0yLjI5Ni0xLjg2LTQuMTU0LTQuMTU0LTQuMTU0DQoJCXMtNC4xNTQsMS44NTgtNC4xNTQsNC4xNTR2NC4xNTRDODAuNjk4LDE1NC45NjEsODIuNTU4LDE1Ni44Miw4NC44NTIsMTU2LjgyeiIvPg0KCTxwYXRoIHN0eWxlPSJmaWxsOiNGNUY1RjU7IiBkPSJNMTI2LjM5NSwxNTIuNjY1aDQuMTU0YzIuMjk0LDAsNC4xNTQtMS44NTgsNC4xNTQtNC4xNTRzLTEuODYtNC4xNTQtNC4xNTQtNC4xNTRoLTQuMTU0DQoJCWMtMi4yOTQsMC00LjE1NCwxLjg1OC00LjE1NCw0LjE1NEMxMjIuMjQsMTUwLjgwNywxMjQuMSwxNTIuNjY1LDEyNi4zOTUsMTUyLjY2NXoiLz4NCgk8cGF0aCBzdHlsZT0iZmlsbDojRjVGNUY1OyIgZD0iTTk5LjMzMSwyMDcuMjYzTDc4LjU2LDIxOS43MjZjLTEuOTY4LDEuMTgxLTIuNjA1LDMuNzMyLTEuNDI0LDUuNw0KCQljMC43NzksMS4yOTgsMi4xNTQsMi4wMTYsMy41NjYsMi4wMTZjMC43MjgsMCwxLjQ2NS0wLjE5MSwyLjEzNC0wLjU5MmwyMC43NzEtMTIuNDYzYzEuOTY4LTEuMTgxLDIuNjA1LTMuNzMyLDEuNDI0LTUuNw0KCQlTMTAxLjMwMSwyMDYuMDgyLDk5LjMzMSwyMDcuMjYzeiIvPg0KCTxwYXRoIHN0eWxlPSJmaWxsOiNGNUY1RjU7IiBkPSJNMTU1LjMzOCwyMTQuNjExbDAuMDk3LTAuMDY2YzAuMzQ5LDEuNDA0LDAuOTQ3LDIuNzYzLDEuOTE3LDMuOTAzDQoJCWMxLjY4OCwxLjk4OCw0LjIxOSwyLjk4Niw3LjA3OSwyLjc1MWM0LjA1Ny0wLjI5Niw2LjgxNi0yLjE1LDguNjQxLTQuNTQ0YzAuMjc4LDAuMTkxLDAuNTcsMC4zNjUsMC44NzYsMC41MTkNCgkJYzIuMjQ1LDEuMTMyLDQuODQyLDEuMDQ3LDcuMzE1LTAuMjM1YzYuMjc4LTMuMjMzLDcuNjMxLTEwLjcxNCw3LjQ0LTE0LjYyNWMtMC4xMTItMi4yOC0yLjA1MS0zLjg5MS00LjMyNS0zLjkzNQ0KCQljLTIuMjgyLDAuMDk3LTQuMDU3LDIuMDMyLTMuOTc2LDQuMzEyYzAuMDAyLDAuMDUzLDAuMDk1LDQuOTgyLTIuOTE5LDYuODEyYy0wLjQ5MS0wLjY4Ni0xLjA3NS0yLjIyNy0xLjI5NC0zLjUwMQ0KCQljLTAuMzUxLTIuMTM4LTIuMzE0LTMuNjI3LTQuNDI4LTMuNDU2Yy0yLjE1NCwwLjE3LTMuODE4LDEuOTgtMy44MjYsNC4xNDJjLTAuMDAyLDAuNjA0LTAuMTM2LDUuOTM1LTQuMjUyLDYuMzgxDQoJCWMtMC40NzktMC42NTMtMC40ODctMy40MDQtMC4wMTYtNS40MzZjMC4zOTgtMS42NjctMC4yNzItMy40MDgtMS42ODItNC4zODFjLTEuNDE0LTAuOTc4LTMuMjc4LTAuOTc4LTQuNjk2LTAuMDE2bC0yLjMxOSwxLjU3Nw0KCQljLTEuMTAxLTI0LjA5MS0zLjYzNy01Mi4xNjUtMy42NjktNTIuNTIzYy0wLjIwNy0yLjI4LTIuMjE1LTMuOTgtNC41MTMtMy43NjFjLTIuMjg2LDAuMjA3LTMuOTcsMi4yMjctMy43NjMsNC41MTUNCgkJYzAuMDM2LDAuMzg0LDIuOTcsMzIuODY0LDMuODc1LDU3LjY2OWMtMC4yMTIsMC4yMDItMC40MzEsMC40MjMtMC42MzksMC42MTZjLTEuNjQzLTQuMTQyLTQuODk5LTYuNzUxLTkuMDkzLTcuMTQ4DQoJCWMtNS45MTktMC41MjMtMTIuMzMzLDMuNTA1LTE0LjYzMSw5LjI1OGMtMS45ODIsNC45NDktMC43MzQsOS44MTQsMy4xNzcsMTIuMzljMi44MTEsMS44NTQsNy4wNTUsMi40MzgsMTIuMzksMC41NjgNCgkJYy0xLjMwMiw0LjU3Ni0zLjY3NCw5LjIxNy02Ljc1NywxMi44MDhjLTIuNzYxLDMuMjE3LTcuMzcxLDYuOTI5LTEzLjI3Miw2Ljc2M2MtMy40MzQtMC4xMzQtNi4wNzMtMS4yNTgtOC4wNjctMy40NA0KCQljLTQuODA5LTUuMjU0LTQuMzk2LTE0Ljc2My00LjM5Mi0xNC44NjRjMC4xMi0yLjI4OC0xLjYzMy00LjIzOS0zLjkxOS00LjM2NWMtMi4zMDItMC4yNDMtNC4yNTIsMS42MjctNC4zNzcsMy45MTkNCgkJYy0wLjAyOCwwLjUyNy0wLjYzMSwxMy4wMjMsNi41MjMsMjAuODgxYzMuNTIxLDMuODc0LDguMjAxLDUuOTQ3LDEzLjkwNyw2LjE3MWMwLjI3OCwwLjAxMiwwLjU1NiwwLjAxNiwwLjgzNiwwLjAxNg0KCQljNi45NDksMCwxMy42OTItMy40MDgsMTkuMDYzLTkuNjY4YzUuMDUyLTUuODgzLDguNDgzLTEzLjg3NCw5LjUxMy0yMS4yOTNjLTAuMDE0LDE0LjIxMS04LjU3NywzMC4yNC0xOS45MzMsMzcuMzAxDQoJCWMtNC44MjIsMi45OTQtMTIuMzM5LDUuNjk2LTIwLjgwOCwxLjUyMWMtMTAuNDM4LTUuMTUyLTkuMTk1LTE3LjA4NC05LjE0LTE3LjU3YzAuMjg2LTIuMjc2LTEuMzMxLTQuMzUzLTMuNjA3LTQuNjM3DQoJCWMtMi4yNjYtMC4yNzYtNC4zNTUsMS4zMjctNC42MzcsMy42MDdjLTAuMDkzLDAuNzQyLTIuMDg5LDE4LjI1NiwxMy43MDgsMjYuMDQ5YzQuMDU3LDIuMDA0LDguMywyLjk5OCwxMi41NzYsMi45OTgNCgkJYzUuNDkzLDAsMTEuMDM3LTEuNjQzLDE2LjI5Mi00LjkxM2MxMy44MjQtOC41OTIsMjMuODU5LTI3LjI2MiwyMy44NTktNDQuMzlDMTU1LjQ3NCwyMjAuNjEzLDE1NS40MjQsMjE3LjY4NywxNTUuMzM4LDIxNC42MTF6DQoJCSBNMTMwLjI4OSwyMTguODk0Yy0wLjc0Mi0wLjQ5MS0wLjE2Mi0yLjA2MS0wLjAzNy0yLjM3M2MwLjg3NC0yLjE5MSwzLjYxMS00LjA4NSw1Ljc5Ny00LjA4NWMwLjExMiwwLDAuMjI1LDAuMDA0LDAuMzM1LDAuMDE2DQoJCWMwLjUzMSwwLjA0OSwxLjk0MSwwLjE4MywyLjU0OCwzLjMxOWMwLjA2NSwwLjMzMywwLjEyLDAuNjczLDAuMTYyLDEuMDIyQzEzNC40NjQsMjE5LjUxMSwxMzEuMzYsMjE5LjU5NiwxMzAuMjg5LDIxOC44OTR6Ii8+DQoJPHBhdGggc3R5bGU9ImZpbGw6I0Y1RjVGNTsiIGQ9Ik04Ni45NDksMjU3LjEwMmMtMS45NjgtMS4xNTYtNC41MTEtMC40OTUtNS42OCwxLjQ3M2MtMC4xOTEsMC4zMi0wLjQ0NCwwLjY2OS0wLjcwMiwwLjk4Ng0KCQljLTAuMDQ1LTAuMTA1LTAuMDg5LTAuMjE5LTAuMTM0LTAuMzQxYy0xLjczMi00LjYyMS0wLjQ5OS0xMS43NDEsMC4xMjQtMTQuMDk0YzAuNTg2LTIuMjE1LTAuNzMyLTQuNDg3LTIuOTQ3LTUuMDc5DQoJCWMtMi4yMTEtMC41NjgtNC40ODUsMC43MjItNS4wNzksMi45MzdjLTAuMzAyLDEuMTI0LTIuODU2LDExLjIwOSwwLjEyMiwxOS4xNTNjMS45MDcsNS4wNzksNC44MzQsNi4zNjEsNi45NTYsNi41MzYNCgkJYzAuMTg5LDAuMDE2LDAuMzc1LDAuMDI0LDAuNTYsMC4wMjRjNC40NzMsMCw3LjQ0OC00LjUwNyw4LjI3Ni01LjkzNUM4OS41OTMsMjYwLjc4MSw4OC45MjEsMjU4LjI2Miw4Ni45NDksMjU3LjEwMnoiLz4NCgk8cGF0aCBzdHlsZT0iZmlsbDojRjVGNUY1OyIgZD0iTTE2My42MTIsMTgxLjc0NWMwLjg5OSwwLDEuODAzLTAuMjg4LDIuNTY0LTAuODg4bDIwLjk0NC0xNi40NDMNCgkJYzAuMjg5LTAuMjI3LDAuNDc0LTAuNTI0LDAuNjg1LTAuODA0YzMuMDE2LTEuODE5LDUuMDU3LTUuMDk1LDUuMDU3LTguODY4YzAtNS43MjgtNC42NTktMTAuMzg2LTEwLjM4Ni0xMC4zODYNCgkJcy0xMC4zODYsNC42NTctMTAuMzg2LDEwLjM4NmMwLDMuMjA1LDEuNDg5LDYuMDM4LDMuNzc3LDcuOTQ1bC0xNC44MjQsMTEuNjM4Yy0xLjgwMywxLjQxNi0yLjExOCw0LjAyOC0wLjcwMiw1LjgzDQoJCUMxNjEuMTYyLDE4MS4yMDEsMTYyLjM4MSwxODEuNzQ1LDE2My42MTIsMTgxLjc0NXogTTE4Mi40NzcsMTUyLjY2NWMxLjE0NiwwLDIuMDc3LDAuOTMzLDIuMDc3LDIuMDc3cy0wLjkzMSwyLjA3Ny0yLjA3NywyLjA3Nw0KCQlzLTIuMDc3LTAuOTMzLTIuMDc3LTIuMDc3UzE4MS4zMzEsMTUyLjY2NSwxODIuNDc3LDE1Mi42NjV6Ii8+DQoJPHBhdGggc3R5bGU9ImZpbGw6I0Y1RjVGNTsiIGQ9Ik0xNzcuNTYsMTc3LjgwNmMtMi4xODktMC43My00LjUzMiwwLjQ1LTUuMjU2LDIuNjI1Yy0wLjQ0NCwxLjMzMS0yLjQ1Miw4LjE4MywxLjYxOSwxMi4yNTINCgkJYzEuNTgsMS41ODIsMy42NTcsMi4zNzMsNi4yMDcsMi4zNjljMS43MDQsMCwzLjYxOS0wLjM0OSw1LjczOC0xLjA1OWMyLjE3Ny0wLjcyMiwzLjM1My0zLjA3NSwyLjYyNy01LjI1NA0KCQljLTAuNzI2LTIuMTc0LTMuMDc1LTMuMzU5LTUuMjU2LTIuNjI1Yy0yLjMyMSwwLjc3MS0zLjMwMiwwLjY0NS0zLjM5MiwwLjcyNmMtMC4yNDUtMC42MTctMC4wODMtMi40NzEsMC4zNDMtMy43OTcNCgkJQzE4MC45MDcsMTgwLjg3MywxNzkuNzMsMTc4LjUyOCwxNzcuNTYsMTc3LjgwNnoiLz4NCgk8cGF0aCBzdHlsZT0iZmlsbDojRjVGNUY1OyIgZD0iTTIyNS4xMDUsMTYwLjYzM2MwLjM4OSwwLjExNCwwLjc4MSwwLjE2NiwxLjE2NiwwLjE2NmMxLjc5OSwwLDMuNDU2LTEuMTc2LDMuOTg2LTIuOTlsMi40MjQtOC4zMDkNCgkJYzAuNjQzLTIuMjAzLTAuNjIzLTQuNTExLTIuODI0LTUuMTUyYy0yLjIwOS0wLjYzNy00LjUwOSwwLjYyMS01LjE1MiwyLjgyNGwtMi40MjQsOC4zMDgNCgkJQzIyMS42MzgsMTU3LjY4NCwyMjIuOTA0LDE1OS45OTIsMjI1LjEwNSwxNjAuNjMzeiIvPg0KCTxwYXRoIHN0eWxlPSJmaWxsOiNGNUY1RjU7IiBkPSJNMjAwLjg5MSwxNjAuMTQ2YzAuNjU5LDAuMzg1LDEuMzgxLDAuNTY4LDIuMDkzLDAuNTY4YzEuNDI4LDAsMi44MTctMC43MzQsMy41OS0yLjA1Nw0KCQljMS4xNTgtMS45OCwwLjQ5MS00LjUyNy0xLjQ4OS01LjY4NGMtMC44MzYtMC40ODctMC43MjItMS44NjItMC42OTItMi4xMzRjMC4xMjQtMS4wOTEsMC43NzUtMi40MjIsMS45MDctMi43NzUNCgkJYzIuMTkxLTAuNjgyLDMuNDE0LTMuMDEsMi43MzItNS4yMDFjLTAuNjg2LTIuMTk1LTMuMDEtMy40MDgtNS4yMDMtMi43MzRjLTQuMTQyLDEuMjktNy4xNiw1LjEyNC03LjY5Miw5Ljc2NQ0KCQlDMTk1LjY0NSwxNTQuMjE1LDE5Ny40NjcsMTU4LjE0MiwyMDAuODkxLDE2MC4xNDZ6Ii8+DQoJPHBhdGggc3R5bGU9ImZpbGw6I0Y1RjVGNTsiIGQ9Ik0xNjcuOTM3LDIyNy40NDJjLTIuMjk0LDAtNC4xNTQsMS44NTgtNC4xNTQsNC4xNTRjMCwyLjAyOCwxLjQ1MiwzLjcxNiwzLjM3NSw0LjA4MQ0KCQljMC4yMTksMC4zMiwwLjc3OSwxLjQyLDAuNzc5LDQuMjI3YzAsMi4yOTYsMS44Niw0LjE1NCw0LjE1NCw0LjE1NHM0LjE1NC0xLjg1OCw0LjE1NC00LjE1NA0KCQlDMTc2LjI0NSwyMjcuNTg0LDE2OC4yNzYsMjI3LjQ0MiwxNjcuOTM3LDIyNy40NDJ6Ii8+DQoJPHBhdGggc3R5bGU9ImZpbGw6I0Y1RjVGNTsiIGQ9Ik0yMzguNDE1LDIxMC4yNDFjMC41MjEsMC4wODUsMS4wNDEsMC4xMjYsMS41NjIsMC4xMjZjMy4yNjQsMCw2LjU2Mi0xLjY0Nyw5LjgyOC00LjkxMw0KCQljMS42MjMtMS42MjMsMS42MjMtNC4yNTIsMC01Ljg3NGMtMS42MjMtMS42MjMtNC4yNTItMS42MjMtNS44NzQsMGMtMi40ODUsMi40ODctMy44MjIsMi41NC00LjE2NCwyLjQ2Mw0KCQljLTAuNzkzLTAuMTE0LTEuNTExLTEuMTQ4LTEuNjcxLTEuNDMyYy0xLjA0My0yLjAyLTMuNTEzLTIuODI4LTUuNTQ4LTEuODA5Yy0yLjA1MywxLjAyNi0yLjg4NCwzLjUyMS0xLjg1OCw1LjU3NA0KCQlDMjMwLjk0NiwyMDQuODksMjMzLjM0NiwyMDkuNDE3LDIzOC40MTUsMjEwLjI0MXoiLz4NCgk8cGF0aCBzdHlsZT0iZmlsbDojRjVGNUY1OyIgZD0iTTMwOS4wMjksMTgxLjA0N2MtMC4wMjYtMC4wNjUtMC4wNTEtMC4xMjYtMC4wNzEtMC4xODNjLTAuNzM2LTIuMTU4LTMuMDg5LTMuMzMxLTUuMjQ0LTIuNTk2DQoJCWMtMi4xNzksMC43MjItMy4zNTUsMy4wNzUtMi42MjksNS4yNTRjMC4zMjUsMC45NzQsMi4xNzQsNS44NTgsNi43NTksNi40NzVjMC4zMDYsMC4wNDEsMC42MTEsMC4wNjEsMC45MTMsMC4wNjENCgkJYzIuODc4LDAsNS41NzgtMS44NjIsOC4wMzUtNS41NDZjMS4yNzItMS45MDcsMC43NTctNC40ODctMS4xNTItNS43NjFjLTEuOTExLTEuMjctNC40OTEtMC43NTktNS43NjEsMS4xNTINCgkJQzMwOS41NTEsMTgwLjM5NCwzMDkuMjY3LDE4MC43NjcsMzA5LjAyOSwxODEuMDQ3eiIvPg0KCTxwYXRoIHN0eWxlPSJmaWxsOiNGNUY1RjU7IiBkPSJNMzAwLjg3MywyNTYuNTIxYzAtMTcuNzE2LTAuNzc5LTM0LjMzNS0xLjg2LTQ4Ljg5NmM1Ljg5OSwxMC4yMywxMS4yMDUsMjAuNTY4LDE0LjU4NiwyOS41ODYNCgkJYzAuNjI3LDEuNjY3LDIuMjA5LDIuNjk0LDMuODkxLDIuNjk0YzAuNDg1LDAsMC45NzgtMC4wODUsMS40NTgtMC4yNjRjMi4xNDgtMC44MDcsMy4yMzctMy4yMDEsMi40MzItNS4zNTENCgkJYy01LjI1OC0xNC4wMTgtMTQuNzA5LTMwLjcxMy0yNC4wMjQtNDUuNTMyYy0yLjI3MS0yMi4zNzgtNC44MTMtMzYuNTgyLTQuODU5LTM2LjgzNWMtMC40MS0yLjI2LTIuNTY4LTMuNzg1LTQuODMtMy4zNDMNCgkJYy0yLjI1OCwwLjQxLTMuNzU1LDIuNTcyLTMuMzQ1LDQuODI4YzAuMDMzLDAuMTgxLDEuMzQyLDcuNDk3LDIuOTA3LDE5LjgxMmMtMy4xOTEtNC43MzYtNi4xNTctOS4wMDktOC42NDUtMTIuNTI2bDAuMjAzLDAuMDY1DQoJCWMyLjE4OSwwLjczLDQuNTMtMC40NSw1LjI1Ni0yLjYyNWMwLjcyNi0yLjE3OS0wLjQ1LTQuNTMyLTIuNjI3LTUuMjU0bC0xMi40NjMtNC4xNTRjLTEuNzI4LTAuNTg0LTMuNjI1LDAuMDMyLTQuNjg0LDEuNTA5DQoJCWMtMC4yODMsMC4zOTQtMC40NzcsMC44MjYtMC42MDcsMS4yNzNjLTIuNDg3LTEuODM5LTUuNjc2LTMuMzMzLTguNjcxLTIuMDhjLTEuODExLDAuNzUxLTMuOTcsMi42NDEtMy45Nyw3LjM5Mg0KCQljMCwzLjY0NywxLjM5Miw3LjU5LDIuODY2LDExLjc2OWMyLjQ0Niw2LjkyOSwzLjk3OCwxMi4wMjUsMS4zODcsMTQuNDU1Yy0xLjQzMiwxLjM0My0yLjg2MiwxLjg1LTQuMjU4LDEuNDk3DQoJCWMtMi41NjItMC42NDktNS4wOTctNC4wNjEtNi43ODctOS4xMjhjLTAuMDczLTAuMjItMC4yMjctMC4zNzktMC4zMzEtMC41NzhjMC44NzQtNi40MzUsMC40OTMtMTQuMTg4LTEuMjY3LTIyLjk4NQ0KCQljLTAuNDQ4LTIuMjUyLTIuNjMxLTMuNzE2LTQuODg5LTMuMjU4Yy0yLjI1LDAuNDUtMy43MDgsMi42MzctMy4yNTgsNC44ODljMi43MTIsMTMuNTY2LDEuODk5LDI0LjYzMy0yLjE3NywyOS42MDMNCgkJYy0xLjU3NiwxLjkyMy0zLjU1LDIuODE1LTYuMjEzLDIuODE1Yy0wLjg0MiwwLTEuNDE2LTAuMjY4LTEuOTg4LTAuOTI1Yy0zLjI3OC0zLjc3Ny0zLjI2NC0xNS45NjQtMi4yMDctMjMuNDE2DQoJCWMwLjE2OC0xLjE4OS0wLjE4OS0yLjM5OC0wLjk3OC0zLjMwNmMtMC43ODktMC45MDktMS45MzMtMS40MzItMy4xMzYtMS40MzJjLTcuMTQ4LDAtOS44NTgsOC4xMy0xMi4yNSwxNS4zMDMNCgkJYy0xLjA0OSwzLjE1Mi0zLjAwNCw5LjAxNC00LjM2Nyw5LjYyM2MtMi41MTUsMC00LjE0LTcuMzg0LTQuMTU0LTEyLjQ2N2MtMC4wMDYtMi4yOTItMS44NjQtNC4xNDYtNC4xNTQtNC4xNDYNCgkJYzAsMC0wLjAwNCwwLTAuMDA2LDBjLTIuMjkyLDAtNC4xNDgsMS44NTgtNC4xNDgsNC4xNWMwLDkuNTgyLDMuMjY0LDIwLjc3MSwxMi40NjMsMjAuNzcxYzYuODUsMCw5LjYyNS03LjQ2NSwxMS45NDgtMTQuNDAyDQoJCWMwLjQwNCw1LjUxNywxLjY0OSwxMS4yMzQsNC43MTIsMTQuNzYzYzIuMTUyLDIuNDgzLDUuMDEsMy43OTMsOC4yNjYsMy43OTNjNS4xMjgsMCw5LjQ5Ny0yLjAyNCwxMi42MzctNS44NTQNCgkJYzAuNTY4LTAuNjkzLDEuMDU5LTEuNDg5LDEuNTQzLTIuMjkzYzIuNDEzLDMuNDMsNS4zNzEsNS42OSw4LjcwNSw2LjUzMmMwLjk3OCwwLjI0NywxLjk2MiwwLjM3MywyLjkzNSwwLjM3Mw0KCQljMy4yMDcsMCw2LjM0MS0xLjMyMyw5LjA0Ny0zLjg2NmMwLjU2My0wLjUyOSwwLjk1OS0xLjEwNywxLjM4Ny0xLjY3NmMxLjAxNiwwLjI5LDIuMDQ2LDAuNDYzLDMuMDY5LDAuNDYzDQoJCWMzLjM2NSwwLDYuNjA1LTEuMzU1LDguNjE1LTIuNjQ1YzEuOTIzLTEuMjMzLDIuNDc5LTMuNzgxLDEuMjU4LTUuNzEyYy0xLjIyMy0xLjkyNy0zLjc4My0yLjUwMy01LjcyLTEuMjk0DQoJCWMtMC4wMzUsMC4wMTctMi44MSwxLjYxMS00LjcwMywxLjIzOGMwLjAwNS00LjU1NC0xLjY0My05LjM5MS0zLjE0Ny0xMy42NTJjLTAuODU2LTIuNDI2LTEuNzM0LTQuOTEzLTIuMTQ4LTYuOTQ1DQoJCWMwLjM0MywwLjI4OCwwLjY3NywwLjU4OCwwLjk3NiwwLjg4YzEuNjE5LDEuNjIzLDQuMjUsMS42MjMsNS44NzIsMGMwLjI2OC0wLjI2OCwwLjQyNC0wLjU5NSwwLjYwMy0wLjkwOQ0KCQljNC4zMDYsNS45MzIsMTMuMTYzLDE4LjM5OCwyMi4yNzMsMzIuNzY4YzEuNzM5LDE3LjY2MiwzLjI2Niw0MC4xOTUsMy4yNjYsNjQuOTA2YzAsMi4yOTYsMS44Niw0LjE1NCw0LjE1NCw0LjE1NA0KCQlTMzAwLjg3MywyNTguODE4LDMwMC44NzMsMjU2LjUyMXoiLz4NCgk8cGF0aCBzdHlsZT0iZmlsbDojRjVGNUY1OyIgZD0iTTMwNS4wMjcsMTY1LjEyOGMwLDIuMjk2LDEuODYsNC4xNTQsNC4xNTQsNC4xNTRzNC4xNTQtMS44NTgsNC4xNTQtNC4xNTQNCgkJYzAtNC44ODktMy44MjYtMTIuNzg3LTQuNTkyLTE0LjMyMWMtMS4wMjYtMi4wNDktMy41MTktMi44NzYtNS41NzQtMS44NThjLTIuMDUzLDEuMDI2LTIuODg0LDMuNTIxLTEuODU4LDUuNTc0DQoJCUMzMDIuODEsMTU3LjUyMSwzMDUuMDI3LDE2Mi45NjIsMzA1LjAyNywxNjUuMTI4eiIvPg0KCTxwYXRoIHN0eWxlPSJmaWxsOiNGNUY1RjU7IiBkPSJNMjg2LjEyNCwyNTYuOTc2Yy0yLjAzNy0xLjAzOS00LjU0LTAuMjE1LTUuNTc2LDEuODNjLTAuNDQ2LDAuODcyLTEuMTM4LDEuODQyLTEuNzIyLDIuNDY3DQoJCWMtMC4xODUtMS42NTEsMC4yNTYtNC45NDksMS4wOTMtNy42ODRjMC41NTgtMS44MjYtMC4xOTktMy43OTctMS44MzQtNC43NzljLTEuNjI5LTAuOTg2LTMuNzI4LTAuNzMtNS4wNzUsMC42MjFsLTguMzA5LDguMzA5DQoJCWMtMS42MjMsMS42MjMtMS42MjMsNC4yNTIsMCw1Ljg3NHM0LjI1MiwxLjYyMyw1Ljg3NCwwbDAuMjExLTAuMjExYzAuMzU5LDEuNjY3LDEuMDQ3LDMuMTg5LDIuMjIzLDQuMzY1DQoJCWMxLjU1OCwxLjU1OCwzLjQ0LDIuMzgxLDUuMzk4LDIuMzgxYzAuMzc5LDAsMC43NjEtMC4wMzIsMS4xNDItMC4wOTNjNC43MDYtMC43NjMsNy42MzUtNS45NTEsOC40MjItNy41MjENCgkJQzI4OC45OTQsMjYwLjQ4NSwyODguMTY3LDI1OC4wMDYsMjg2LjEyNCwyNTYuOTc2eiIvPg0KCTxwYXRoIHN0eWxlPSJmaWxsOiNGNUY1RjU7IiBkPSJNMjg0LjUxNiwyMjMuNTQ3aC0yOS4yNzljMC4yNi0xLjAzNSwwLjUyNy0yLjAyLDAuNzk5LTIuOTU3DQoJCWMxMS42NzYtMi40MjYsMTguNjE1LTYuMTk1LDIwLjY1Ni0xMS4yMjVjMC43MDYtMS43NDQsMS40NDItNS4yNy0xLjU2Mi05LjMyM2MtMi43MDgtMy42NTEtNi41MzItNS4yOS0xMC41MDEtNC40NzkNCgkJYy01LjkwMywxLjE5Ny0xMS4xNjMsNy41NTgtMTQuOTM3LDE3Ljc0MWMtMTEuNzI3LDIuMDA4LTE5LjIzNSw2LjIxNy0yMy45ODYsMTAuMzcyYy0xLjY4Ni0xNy4wMjMtOC4yOTUtMjcuMTM2LTguNjE1LTI3LjYxOA0KCQljLTEuMjc0LTEuOTExLTMuODU0LTIuNDI2LTUuNzYxLTEuMTUyYy0xLjkwOSwxLjI3NC0yLjQyNCwzLjg1LTEuMTUyLDUuNzYxYzAuMDc1LDAuMTE0LDcuNjExLDExLjYzOSw3LjYxMSwzMC45Mw0KCQljMCwxOC45NjItMTUuMTIsMzMuMjM0LTI0LjkyNSwzMy4yMzRjLTQuNjY1LDAtNi43MDYtNS41NDUtNy42MDQtMTAuMjMyYzkuNjAxLTYuNzI5LDE4LjA3Mi0xNC41MTUsMTkuOTQzLTIxLjk5NQ0KCQljMi41MzEtMTAuMTIyLTAuNjU1LTE3LjU4My0yLjc2My0yMi41MmMtMC4wOTktMC4yMjctMC4yMDMtMC40NzEtMC4zMDgtMC43MjJjMC4wODksMC4wODEsMC4xNzQsMC4xNjYsMC4yNiwwLjI0Nw0KCQljMS42MTksMS42MjMsNC4yNSwxLjYyMyw1Ljg3MiwwYzEuNjIzLTEuNjIzLDEuNjIzLTQuMjUyLDAtNS44NzRjLTIuNDI4LTIuNDMtNy4xNDQtNi4yNDgtMTEuNDMtNC40NTQNCgkJYy0xLjgxMSwwLjc1MS0zLjk3LDIuNjQxLTMuOTcsNy4zOTJjMCwyLjE0NiwwLjg5Nyw0LjI0MywxLjkzNSw2LjY3OGMxLjg3LDQuMzc3LDQuMTk3LDkuODI2LDIuMzQzLDE3LjI0Mg0KCQljLTEuMDk3LDQuMzg4LTYuNDg3LDkuNzEyLTEzLjU5OCwxNC45OTdjLTAuNzYyLTAuOTExLTEuODY0LTEuNTI0LTMuMTQzLTEuNTI0aC0wLjAwNmMtMi4yOTIsMC4wMDQtNC4xNDgsMS44NTgtNC4xNDgsNC4xNQ0KCQljMCwwLjc0NywwLjAzMiwxLjU0NywwLjA4MSwyLjM2NmMtOC44OSw1Ljc1OC0xOC45NTEsMTEuMDg3LTI2LjcyNywxNC42MjRjLTIuMDg3LDAuOTQ5LTMuMDEsMy40MTItMi4wNjEsNS41MDENCgkJYzAuNjk2LDEuNTI5LDIuMjA1LDIuNDM0LDMuNzgzLDIuNDM0YzAuNTc2LDAsMS4xNi0wLjExOCwxLjcxNi0wLjM3M2MyLjMzNi0xLjA2MiwxMy40MjMtNi4yMjcsMjQuODExLTEzLjI4Ng0KCQljMi4wOTcsNy4xMjIsNi41NDIsMTMuNjU5LDE1LjAxMywxMy42NTljOS45NjYsMCwyMS4yNzItOC4xNDksMjcuNzk2LTIwLjMxNmMwLjc4NywxLjM5MSwxLjY5MywyLjcyMywyLjc3MiwzLjk2Nw0KCQljMTAuNDU3LDEyLjA1NywzMC44ODEsMTIuMTk1LDMxLjc0NSwxMi4xOTVjMi4yMzcsMCw0LjA3MS0xLjc2OSw0LjE1Mi00LjAwNGMwLjA3OS0yLjIzNS0xLjYyNS00LjEzNC0zLjg1NC00LjI5Mg0KCQljLTAuMDMyLTAuMDA0LTMuMzQzLTAuMzY5LTUuMTg1LTIuNTE5Yy0xLjExMi0xLjI5OC0xLjU1Ni0zLjA4My0xLjM1OS01LjQ1N2MwLjQyNi01LjEyLDE2LjkzOS04Ljk0NSwzMS40OTQtOS4xNzMNCgkJYzEuNzM0LTAuMDI4LDMuMjctMS4xMjgsMy44NTItMi43NjNsNC4xNTQtMTEuNjg0YzAuNDUyLTEuMjcsMC4yNTgtMi42ODYtMC41MjEtMy43ODUNCgkJQzI4Ny4xMzIsMjI0LjIwNCwyODUuODY0LDIyMy41NDcsMjg0LjUxNiwyMjMuNTQ3eiBNMjY2LjI4LDIwMy43MDljMC4wNzktMC4wMTYsMC4xODUtMC4wNDEsMC4zMTYtMC4wNDENCgkJYzAuMzk4LDAsMS4wMjQsMC4xOTUsMS44NiwxLjMyM2MwLjE1OCwwLjIxMSwwLjY2MywwLjkzMywwLjUzNiwxLjI1Yy0wLjQ3NywxLjE3Ni0zLjM0NSwzLjEyLTkuMzIxLDQuODgNCgkJQzI2Mi4zLDIwNS45OTMsMjY0Ljg3LDIwMy45OTMsMjY2LjI4LDIwMy43MDl6IE0yNDEuMTkyLDI1OC4zMTljLTQuMjIxLTEuMzcxLTguNTIxLTMuNTQ2LTExLjQ5OS02Ljk5DQoJCWMtMy4yNzItMy43ODUtNC40ODEtOC41NzItMy42ODgtMTQuNjI5YzAuMTItMC4zMTgsMC4zOTEtMC45NDYsMC44NjYtMS43NjFjNS44NS0wLjUzNiwyMS40NzgsMS43NywyMi41NjEsNi4yNDYNCgkJYy00LjgyMSwyLjQ2MS04LjM3MSw1LjkyMS04Ljc4MiwxMC44MzlDMjQwLjQ0NiwyNTQuNDg5LDI0MC42OTcsMjU2LjU3OCwyNDEuMTkyLDI1OC4zMTl6Ii8+DQoJPHBhdGggc3R5bGU9ImZpbGw6I0Y1RjVGNTsiIGQ9Ik0zMjkuNzQsMTYzLjgxNGwtNC4xNTQtMTIuNDYzYy0wLjY2OS0yLjAwNC0yLjc0OS0zLjIwMS00LjgwOS0yLjc0Nw0KCQljLTIuMDcxLDAuNDQyLTMuNDc3LDIuMzY5LTMuMjY2LDQuNDc1YzIuODY2LDI4LjY2Niw4LjI4OCw4NC41Nyw4LjI4OCw5MC45OGMwLDUuNzU3LTEwLjI0LDE3LjA5Mi0yNi43ODQsMjUuMzY0DQoJCWMtMi4wNTMsMS4wMjYtMi44ODQsMy41MjEtMS44NTgsNS41NzRjMC43MjgsMS40NTYsMi4xOTUsMi4yOTYsMy43MiwyLjI5NmMwLjYyMywwLDEuMjU4LTAuMTQyLDEuODU0LTAuNDM4DQoJCWMxNS4xMTItNy41NTgsMzEuMzc2LTIxLjA0MywzMS4zNzYtMzIuNzk2YzAtNi4xMzgtNC4yNDYtNTAuMzAxLTYuNjc4LTc1LjEwOUMzMjkuNDAxLDE2OC4xMSwzMzAuNDI5LDE2NS44ODMsMzI5Ljc0LDE2My44MTR6Ii8+DQoJPHBhdGggc3R5bGU9ImZpbGw6I0Y1RjVGNTsiIGQ9Ik0zNjQuNDA0LDIwMS4yOTljMC43OTksMC43OTksMS44NjIsMS4yMTcsMi45MzksMS4yMTdjMC43MDIsMCwxLjQxLTAuMTc5LDIuMDUzLTAuNTQ0DQoJCWMxLjYyNS0wLjkyNSwyLjQzMi0yLjgyNCwxLjk3Mi00LjYzN2MtMC43NDQtMi45MjEtMC45OTItNi41NzItMC41OTgtOC4wNjVjMi4wODEsMC4wNjUsNC4wMTItMS40MTYsNC4zMTktMy41NjINCgkJYzAuMzI1LTIuMjcyLTEuMjU0LTQuMzc3LTMuNTIzLTQuNzAyYy0yLjc0Ni0wLjM3My01LjE1OCwwLjQzNC02Ljg0NCwyLjMwOGMtMS4wOTEsMS4yMTctMS43NCwyLjc3OS0yLjA5Myw0LjQ1OWwtNC44MTEtNC44MTENCgkJYy0xLjYyMy0xLjYyMy00LjI1Mi0xLjYyMy01Ljg3NCwwYy0xLjYyMywxLjYyMy0xLjYyMyw0LjI1MiwwLDUuODc0TDM2NC40MDQsMjAxLjI5OXoiLz4NCgk8cGF0aCBzdHlsZT0iZmlsbDojRjVGNUY1OyIgZD0iTTM3My43NDksMjA1LjYzMmwtMTUuNTc4LDExLjE2NWMtMS44NjQsMS4zMzUtMi4yOTQsMy45MzEtMC45NTcsNS43OTcNCgkJYzAuODExLDEuMTMyLDIuMDg3LDEuNzMyLDMuMzgxLDEuNzMyYzAuODM4LDAsMS42ODQtMC4yNTIsMi40MTYtMC43NzlsMTUuNTc4LTExLjE2NWMxLjg2NC0xLjMzNSwyLjI5NC0zLjkzMSwwLjk1Ny01Ljc5Nw0KCQlDMzc4LjIwNywyMDQuNzE5LDM3NS42MDcsMjA0LjI4NSwzNzMuNzQ5LDIwNS42MzJ6Ii8+DQoJPHBhdGggc3R5bGU9ImZpbGw6I0Y1RjVGNTsiIGQ9Ik0zNzIuMzY5LDIyMS4yNTVjLTEuMzA2LDEuNjg0LTEuMTIsNC4wNTMsMC4zNjMsNS41MDljMC4xNzgsMC4zLDAuOTg4LDEuODkxLDAuODQyLDUuOTgNCgkJYy0wLjA4MSwyLjI5NiwxLjcxMiw0LjIxOSw0LjAwNCw0LjNjMC4wNTEsMC4wMDQsMC4xMDEsMC4wMDQsMC4xNSwwLjAwNGMyLjIyNywwLDQuMDY5LTEuNzY1LDQuMTUtNC4wMDQNCgkJYzAuMzAyLTguNDc5LTIuNDczLTExLjU3OC0zLjY4LTEyLjUxNUMzNzYuMzkyLDIxOS4xMTMsMzczLjc3NywyMTkuNDQ2LDM3Mi4zNjksMjIxLjI1NXoiLz4NCgk8cGF0aCBzdHlsZT0iZmlsbDojRjVGNUY1OyIgZD0iTTQwOC42MjQsMjUxLjg0OGMyLjI5NCwwLDQuMTU0LTEuODU4LDQuMTU0LTQuMTU0YzAtNi4xMy0wLjMxNS0xMi45NzItMC44My0yMC4wOTENCgkJYzQuMTE5LDYuNzUsNy41NDEsMTIuOTczLDkuNzM1LDE4LjA5MWMwLjY3MywxLjU3OCwyLjIwNywyLjUxOSwzLjgyLDIuNTE5YzAuNTQ2LDAsMS4xMDEtMC4xMDUsMS42MzUtMC4zMzcNCgkJYzIuMTA4LTAuOTA1LDMuMDg1LTMuMzQ3LDIuMTgxLTUuNDUyYy0zLjgwMy04Ljg3Ni0xMC45MTEtMjAuNjMzLTE5LjAwNi0zMi43NzFjLTEuNjMtMTUuMjQ4LTMuNzU4LTMwLjE4OC01LjQxMi00MC45MDYNCgkJYzAuODgsMC4wODksMS43OTMtMC4xMjYsMi41OTQtMC42NDVjMS4zMTItMC44NDEsMS45MjgtMi4zLDEuODM5LTMuNzUzYzAuMTA3LDAuNDA4LDAuMjIxLDAuODE4LDAuNDU5LDEuMTkzDQoJCWMwLjAyOCwwLjA0MSwyLjY5OCw0LjQ1LDEuNTEzLDEwLjE3MWMtMC40NjUsMi4yNDMsMC45OCw0LjQ0MiwzLjIyNyw0LjkwOWMwLjI4NCwwLjA1NywwLjU2OCwwLjA4NSwwLjg0NiwwLjA4NQ0KCQljMS45MjksMCwzLjY1Ny0xLjM1MSw0LjA2NS0zLjMxNGMxLjg3Ni05LjA3NS0yLjQ1Ni0xNi4wMjEtMi42NDEtMTYuMzEzYy0xLjIzMy0xLjkzMS0zLjgwMS0yLjQ5OS01LjczNi0xLjI3NA0KCQljLTEuMzE3LDAuODQtMS45NCwyLjMwMS0xLjg1NSwzLjc1NmMtMC4xMDgtMC40MTEtMC4yMjYtMC44MjQtMC40NjgtMS4yMDFsLTcuMDEtMTAuOTA1Yy0xLjA3MS0xLjY2My0zLjE2Ni0yLjMzNy01LjAwNi0xLjYyMw0KCQljLTEuODQ0LDAuNzIyLTIuOTIzLDIuNjQxLTIuNTgsNC41ODhjMC4wNTgsMC4zMjcsMy4yMzMsMTguNTU3LDYuMDQ2LDQwLjUyOWMtMy4xODUtNC40OTgtNi4zNy04Ljg5NS05LjQ1OC0xMy4wOTENCgkJYy0wLjUwMi02LjI4NS0xLjAwNS0xMi4wOTYtMS40NDEtMTYuOTAxYzAuMjI5LTAuMDY5LDAuNDU0LTAuMTU4LDAuNjc1LTAuMjY4YzIuMDUzLTEuMDI2LDIuODg0LTMuNTIxLDEuODU4LTUuNTc0bC00LjE1NC04LjMwOQ0KCQljLTAuODk3LTEuNzkzLTIuOTQzLTIuNjg2LTQuODctMi4xMzRjLTEuOTI1LDAuNTYtMy4xNzksMi40MS0yLjk4LDQuNDA2YzAuNDk2LDQuOTU4LDEuMDExLDEwLjQ0NiwxLjUxOCwxNi4yMTENCgkJYy0yLjYyOC0zLjQ2NC01LjA1OS02LjYzNC03LjEzOS05LjMxOGMwLjQwNi0wLjM0OSwwLjc1My0wLjc4MywxLjAwOC0xLjI5NGMxLjAyNi0yLjA1MywwLjE5NS00LjU0OC0xLjg1OC01LjU3NGwtOC4zMDktNC4xNTQNCgkJYy0xLjc0Mi0wLjg2OC0zLjg1OC0wLjQxNC01LjA4OSwxLjEwM2MtMS4yMjUsMS41MTctMS4yMzEsMy42ODQtMC4wMTIsNS4yMDljMC4yMjQsMC4yNzksMTAuNDUzLDEzLjEwMSwyMi42ODgsMjkuNjUxDQoJCWMyLjEyMiwyNy40NDgsMy41MzUsNTYsMS41MzksNjEuOTg2Yy0wLjM2NSwxLjA5NS0xLjAwMiwyLjM4NS0xLjYsMi40M2MtMi41MTEsMC4zNDktOC41MDMtNS4yNjYtMTEuNTEzLTExLjI4Mg0KCQljLTEuNzM0LTMuNDY1LTQuMjI5LTMuODI2LTUuNTY0LTMuNzY1Yy00LjQ2NSwwLjI3Ni02LjIwNyw1LjE4OS05LjM3MywxNC4xMDZjLTIuMjcsNi4zOS01LjA4OSwxNC4zMzMtOC4yNTYsMTcuNTgzDQoJCWMtMS43MTUtMi4wOTYtMy45MzMtOS4xMjktNS4xNjYtMTYuMzU2YzAuNTc5LDEuNTk4LDIuMDc0LDIuNzUzLDMuODcyLDIuNzUzYzIuMjk0LDAsNC4xNTQtMS44NTgsNC4xNTQtNC4xNTQNCgkJYzAtNi4xOTUtNC4zMzMtNTMuMzc2LTYuNzUzLTc5LjIzNWMyLjAyLTAuODExLDMuMDgzLTMuMDcxLDIuMzg1LTUuMTY0bC00LjE1NC0xMi40NjNjLTAuNjY3LTIuMDA0LTIuNzE4LTMuMTg1LTQuODAxLTIuNzUxDQoJCWMtMi4wNjcsMC40MzgtMy40NzcsMi4zNTctMy4yNzYsNC40NTljMi44MjMsMjkuNjM2LDguMTE3LDg2Ljg3OCw4LjI4Miw5NC43NzhjLTAuMDEtMC4wNzEtMC4wMjUtMC4xNDEtMC4wMzUtMC4yMTINCgkJYy0wLjMyMy0yLjI3Mi0yLjQ0Mi0zLjgzLTQuNy0zLjUyNWMtMi4yNzIsMC4zMjktMy44NDgsMi40My0zLjUyMyw0LjcwMmMxLjIxMyw4LjQ5MSw0LjMxMiwyMy4wOTIsMTEuNDUzLDI1LjQ2OQ0KCQljMC41NjQsMC4xOTEsMS4zMjMsMC4zNjEsMi4yMTMsMC4zNjFjMS42MzcsMCwzLjcxOC0wLjU4LDUuODQ2LTIuNzFjNC41MjUtNC41MjMsNy41ODYtMTMuMTQ4LDEwLjI4OC0yMC43NTUNCgkJYzAuNjExLTEuNzIsMS4zMzMtMy43NTcsMi4wMTQtNS41MDFjMy44MTEsNS41NjYsMTAuNDYxLDEyLjQ5NSwxNy4xOTcsMTEuOTUyYzIuMzY5LTAuMTcsNi42NzgtMS40NDQsOC44OTMtOC4wODkNCgkJYzIuMjA5LTYuNjI3LDEuMzM2LTI5LjczOS0wLjE1My01MS44NjVjMy41LDQuOTA2LDcuMDAzLDkuOTI4LDEwLjM0NiwxNC44OWMxLjI5NiwxMi40NCwyLjIyMywyNC45MDQsMi4yMjMsMzUuMTQxDQoJCUM0MDQuNDY5LDI0OS45OSw0MDYuMzI5LDI1MS44NDgsNDA4LjYyNCwyNTEuODQ4eiIvPg0KCTxwYXRoIHN0eWxlPSJmaWxsOiNGNUY1RjU7IiBkPSJNNDQwLjE1LDIyOS45OThjLTIuOTQzLTIxLjkyMy03LjA0My00Ny4zMTktOS42MzktNjIuOTA2YzEuMTcyLDAuMjg4LDIuNDUsMC4wNTMsMy40ODEtMC43MzgNCgkJYzEuODI2LTEuMzkyLDIuMTc0LTQsMC43ODMtNS44MjJsLTcuNTMtOS44NjZjLTEuMTYyLTEuNTE3LTMuMjExLTIuMDUzLTQuOTY2LTEuMjg2cy0yLjc1NywyLjYzMy0yLjQzLDQuNTE5DQoJCWMwLjA3NSwwLjQyNiw3LjQ4Myw0My4wNzIsMTIuMDY1LDc3LjIwMmMxLjc1MSwxMy4wNDMtMC44MDUsMjQuNjk4LTYuODQsMzEuMTczYy0zLjg3OCw0LjE1OC05LjAxOCw2LjA0MS0xNS4yMzgsNS42OA0KCQljLTIuMjg0LTAuMTIyLTQuMjY0LDEuNTk4LTQuNDA4LDMuODg3Yy0wLjE0NCwyLjI4OCwxLjU5NCw0LjI2LDMuODg0LDQuNDA2YzAuNzAyLDAuMDQ1LDEuMzk0LDAuMDY1LDIuMDc5LDAuMDY1DQoJCWM3LjgzNCwwLDE0LjYzMy0yLjg3MiwxOS43NTktOC4zNzNDNDM4Ljk3OSwyNTkuNTQsNDQyLjI1OSwyNDUuNzEsNDQwLjE1LDIyOS45OTh6Ii8+DQoJPHBhdGggc3R5bGU9ImZpbGw6I0Y1RjVGNTsiIGQ9Ik0zOTQuNDQ5LDI1Ny4zODJjLTEuNTI3LDEuNjU5LTEuNDEsNC4yNzIsMC4yMDUsNS44MzhjMC4yMjUsMC4yMTksMC40OTUsMC42NDksMC40OTcsMC44Ng0KCQljMC4wMDIsMC4xMTgtMC4xNjYsMC40NjctMC42MjksMC45MjljLTEuNjIzLDEuNjIzLTEuNjIzLDQuMjUyLDAsNS44NzRjMC44MTEsMC44MTEsMS44NzQsMS4yMTcsMi45MzcsMS4yMTcNCgkJczIuMTI2LTAuNDA2LDIuOTM3LTEuMjE3YzIuNjM1LTIuNjMzLDMuMTE4LTUuMzA2LDMuMDU5LTcuMDg3Yy0wLjEyOC0zLjgzNC0yLjczOC02LjMwOC0zLjI2LTYuNzY3DQoJCUMzOTguNSwyNTUuNTUyLDM5NS45NzQsMjU1LjczLDM5NC40NDksMjU3LjM4MnoiLz4NCgk8cGF0aCBzdHlsZT0iZmlsbDojRjVGNUY1OyIgZD0iTTM3OS44MDQsMzQ4LjQ4NmgtNC4xNTRoLTI0LjkyNnYtNC4xNTRjMC0yLjI5Ni0xLjg2LTQuMTU0LTQuMTU0LTQuMTU0cy00LjE1NCwxLjg1OC00LjE1NCw0LjE1NA0KCQl2NC4xNTRIMTIyLjI0YzguMzA4LDguMzA5LDI0LjkyNSwxMi40NjMsMzcuMzg4LDEyLjQ2M2M4LjkzOCwwLDEyMC40MzIsMCwxODIuNzg3LDB2OC4zMDljMCwyLjI5NiwxLjg2LDQuMTU0LDQuMTU0LDQuMTU0aDMzLjIzNA0KCQljNi44NzIsMCwxMi40NjMtNS41OSwxMi40NjMtMTIuNDYzQzM5Mi4yNjYsMzU0LjA3NywzODYuNjc2LDM0OC40ODYsMzc5LjgwNCwzNDguNDg2eiBNMzc5LjgwNCwzNjUuMTAzaC0yOS4wOHYtNC4xNTQNCgkJYzIwLjAyLDAsMzMuMjM0LDAsMzMuMjM0LDBsLTAuNzQ1LTIuMjM2YzAuNDMyLDAuNjU1LDAuNzQ1LDEuMzk0LDAuNzQ1LDIuMjM2QzM4My45NTgsMzYzLjI0MSwzODIuMDk0LDM2NS4xMDMsMzc5LjgwNCwzNjUuMTAzeiINCgkJLz4NCjwvZz4NCjxnPg0KPC9nPg0KPGc+DQo8L2c+DQo8Zz4NCjwvZz4NCjxnPg0KPC9nPg0KPGc+DQo8L2c+DQo8Zz4NCjwvZz4NCjxnPg0KPC9nPg0KPGc+DQo8L2c+DQo8Zz4NCjwvZz4NCjxnPg0KPC9nPg0KPGc+DQo8L2c+DQo8Zz4NCjwvZz4NCjxnPg0KPC9nPg0KPGc+DQo8L2c+DQo8Zz4NCjwvZz4NCjwvc3ZnPg0K", new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, false, false, "عربي", new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("ddec791a-d71f-4c07-91d7-20e4eea2ad7a"), "tr-TR", "data:image/svg+xml;base64,PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0iaXNvLTg4NTktMSI/Pg0KPCEtLSBHZW5lcmF0b3I6IEFkb2JlIElsbHVzdHJhdG9yIDE5LjAuMCwgU1ZHIEV4cG9ydCBQbHVnLUluIC4gU1ZHIFZlcnNpb246IDYuMDAgQnVpbGQgMCkgIC0tPg0KPHN2ZyB2ZXJzaW9uPSIxLjEiIGlkPSJMYXllcl8xIiB4bWxucz0iaHR0cDovL3d3dy53My5vcmcvMjAwMC9zdmciIHhtbG5zOnhsaW5rPSJodHRwOi8vd3d3LnczLm9yZy8xOTk5L3hsaW5rIiB4PSIwcHgiIHk9IjBweCINCgkgdmlld0JveD0iMCAwIDUxMiA1MTIiIHN0eWxlPSJlbmFibGUtYmFja2dyb3VuZDpuZXcgMCAwIDUxMiA1MTI7IiB4bWw6c3BhY2U9InByZXNlcnZlIj4NCjxyZWN0IHN0eWxlPSJmaWxsOiNGRjRCNTU7IiB3aWR0aD0iNTEyIiBoZWlnaHQ9IjUxMiIvPg0KPGc+DQoJPHBhdGggc3R5bGU9ImZpbGw6I0Y1RjVGNTsiIGQ9Ik0yNzcuOTkyLDIxMS42ODRsMTUuNjYzLDIwLjQ4OWwyNC40NTYtOC4xODZjMS4zNjgtMC40NTgsMi41MzEsMS4wODksMS43MTIsMi4yNzZsLTE0LjY0NywyMS4yMjcNCgkJbDE1LjM0MywyMC43M2MwLjg1OCwxLjE1OS0wLjI1MywyLjc0My0xLjYzNSwyLjMzMWwtMjQuNzE1LTcuMzdsLTE0Ljk3NCwyMC45OThjLTAuODM3LDEuMTc0LTIuNjg3LDAuNjA3LTIuNzIyLTAuODM1DQoJCWwtMC42MjgtMjUuNzgybC0yNC41OTctNy43NTJjLTEuMzc1LTAuNDMzLTEuNDA4LTIuMzY4LTAuMDQ3LTIuODQ3bDI0LjMyNi04LjU2NGwtMC4yMjgtMjUuNzg5DQoJCUMyNzUuMjg2LDIxMS4xNjcsMjc3LjExNiwyMTAuNTM5LDI3Ny45OTIsMjExLjY4NHoiLz4NCgk8cGF0aCBzdHlsZT0iZmlsbDojRjVGNUY1OyIgZD0iTTE5MS4zNzksMzI1LjkwNmMtNDQuMTM5LDAtNzkuOTQ1LTM1LjgwNi03OS45NDUtNzkuOTg1YzAtNDQuMDk4LDM1LjgwNi03OS45ODQsNzkuOTQ1LTc5Ljk4NA0KCQljMTYuNDIyLDAsMzEuNTY3LDUuMDUxLDQ0LjE4NywxMy41OTljMS45MzMsMS4zMDksNC4xMzktMS4yMzEsMi41Mi0yLjkxM2MtMTguODYyLTE5LjYxMS00NS41NTktMzEuNTc0LTc1LjE1OC0zMC41NjQNCgkJYy01MC44NTMsMS43MzUtOTIuOTUyLDQyLjUwNy05Ni4yMDEsOTMuMjg2Yy0zLjcyMiw1OC4xNjMsNDIuMzMyLDEwNi40OTksOTkuNjcyLDEwNi40OTljMjguMjQ1LDAsNTMuNjI5LTExLjgwOCw3MS43NTctMzAuNjg4DQoJCWMxLjYwMi0xLjY2OC0wLjYwOS00LjE2OS0yLjUyMy0yLjg3MkMyMjIuOTk5LDMyMC44NDksMjA3LjgzLDMyNS45MDYsMTkxLjM3OSwzMjUuOTA2eiIvPg0KPC9nPg0KPGc+DQo8L2c+DQo8Zz4NCjwvZz4NCjxnPg0KPC9nPg0KPGc+DQo8L2c+DQo8Zz4NCjwvZz4NCjxnPg0KPC9nPg0KPGc+DQo8L2c+DQo8Zz4NCjwvZz4NCjxnPg0KPC9nPg0KPGc+DQo8L2c+DQo8Zz4NCjwvZz4NCjxnPg0KPC9nPg0KPGc+DQo8L2c+DQo8Zz4NCjwvZz4NCjxnPg0KPC9nPg0KPC9zdmc+DQo=", new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, false, false, "Türkçe", new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null }
                });

            migrationBuilder.InsertData(
                table: "Menu",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "Icon", "Name", "OrderId", "ParentId", "Url" },
                values: new object[,]
                {
                    { 1, null, null, "user-circle", "User Management", 996, null, null },
                    { 4, null, null, "id-card", "Role Management", 997, null, null },
                    { 7, null, null, "building", "Setting Management", 999, null, null },
                    { 10, null, null, "file", "Reports", 998, null, null }
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("00bb2e85-4474-414c-bed4-6d4fef568ec4"), "4b2b5364-a3a5-45d1-884a-bd89e93a759b", "Admin", "ADMIN" },
                    { new Guid("09c0b51b-f9ac-48a0-8a7c-b5b6b987a4c6"), "2b1129f9-b878-4311-ad1d-833f1ce71ca6", "Guest", "GUEST" },
                    { new Guid("7255e4e1-bcbf-4c1b-89d4-15f3343dc572"), "225ab332-e2d0-4ef7-a31f-752528fcaf15", "PowerUser", "POWERUSER" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "InsertedDate", "InsertedUser", "IsActive", "IsDeleted", "LastPasswordChangedDate", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePhoto", "SecurityStamp", "Surname", "TimeZone", "TwoFactorEnabled", "UpdatedDate", "UpdatedUser", "UserName" },
                values: new object[] { new Guid("e0cb33f3-591a-4a25-aaba-bd05f796b5fb"), 0, "ab72d39f-7f3a-4bbe-9228-fa2555d8063c", "ugur.cakmak@kocsistem.com.tr", true, new DateTime(2023, 7, 16, 20, 21, 17, 390, DateTimeKind.Local).AddTicks(6247), "System", true, false, new DateTime(2023, 7, 16, 20, 21, 17, 390, DateTimeKind.Local).AddTicks(6230), false, null, "Uğur", "UGUR.CAKMAK@KOCSISTEM.COM.TR", "UGUR.CAKMAK@KOCSISTEM.COM.TR", "AQAAAAIAAYagAAAAECIrFaYcG3Pa1DWQ3RQYvA1hCueLO5EZptAByVGTirihYDDmE5vTXaAX3tiYyO2WvQ==", "02165561100", false, null, "ce8b16e3-eb01-4263-9418-43103d1a3557", "Çakmak", "Europe/Istanbul", false, null, null, "ugur.cakmak@kocsistem.com.tr" });

            migrationBuilder.InsertData(
                table: "ApplicationSetting",
                columns: new[] { "Id", "CategoryId", "IsStatic", "Key", "Value", "ValueType" },
                values: new object[,]
                {
                    { new Guid("04d858bf-6ec6-4753-b0a3-60e0a81dfd09"), new Guid("0637d6bb-ea07-4d50-a9cb-e6be151e743a"), true, "Identity:AutoLogout:DialogTimeout", "30000", "System.Int32" },
                    { new Guid("09783eac-26f9-4bec-8ef4-8c9e47ed58e2"), new Guid("0637d6bb-ea07-4d50-a9cb-e6be151e743a"), true, "Identity:AutoLogout:IdleTimeout", "600000", "System.Int32" },
                    { new Guid("11cab984-9b33-49e5-b803-f4e49986d55d"), new Guid("0637d6bb-ea07-4d50-a9cb-e6be151e743a"), true, "Notification:Email:IsActive", "true", "System.Boolean" },
                    { new Guid("127d4b3e-2f09-40a7-9033-448cb9dfd187"), new Guid("0637d6bb-ea07-4d50-a9cb-e6be151e743a"), true, "Identity:ProfilePhoto:MaxSize", "160000", "System.Int32" },
                    { new Guid("3da733ae-b51c-45cb-893d-355b0a0ee0a7"), new Guid("0637d6bb-ea07-4d50-a9cb-e6be151e743a"), true, "Identity:2FASettings:AuthenticatorLinkName", "OneFrame", "System.String" },
                    { new Guid("84f55a47-6c93-44c1-803b-35f2170b4f3a"), new Guid("0637d6bb-ea07-4d50-a9cb-e6be151e743a"), true, "Identity:2FASettings:IsEnabled", "false", "System.Boolean" },
                    { new Guid("918f1747-c7c4-49c1-98a4-042b4618c8a7"), new Guid("0637d6bb-ea07-4d50-a9cb-e6be151e743a"), true, "Identity:AutoLogout:IsEnabled", "true", "System.Boolean" },
                    { new Guid("a670744c-02a6-4cb8-bb39-7a799479e3a0"), new Guid("0637d6bb-ea07-4d50-a9cb-e6be151e743a"), true, "Identity:2FASettings:VerificationTime", "60", "System.Int32" },
                    { new Guid("e516a25c-701e-454f-b569-dc6893725480"), new Guid("0637d6bb-ea07-4d50-a9cb-e6be151e743a"), true, "Identity:2FASettings:Type", "Authenticator", "System.String" }
                });

            migrationBuilder.InsertData(
                table: "EmailTemplateTranslation",
                columns: new[] { "Id", "EmailContent", "Language", "ReferenceId", "Subject" },
                values: new object[,]
                {
                    { new Guid("0d069e15-2a36-46f0-8c14-21dc5d7e5df4"), "<!DOCTYPE html>\r\n<html lang=\"en\">\r\n<head>\r\n    <title>OneFrame</title>\r\n    <meta charset=\"utf-8\">\r\n    <meta http-equiv=\"X-UA-Compatible\" content=\"IE =edge\">\r\n    <meta name=\"viewport\" content=\"width =device-width, initial-scale=1\">\r\n    <link href=\"maxcdn.bootstrapcdn.com/bootstrap/3.3.1/css/bootstrap.min.css\" rel=\"stylesheet\">\r\n</head>\r\n<body>\r\n    <table style=\"\r\n        font-family: Arial, Helvetica, sans-serif;\r\n        margin: 0 auto;\r\n        display: table;\r\n      \"\r\n           width=\"800\"\r\n           align=\"center\"\r\n           cellspacing=\"0\"\r\n           cellpadding=\"0\"\r\n           border=\"0\">\r\n        <tr>\r\n            <td width=\"800\" height=\"199\" align=\"center\">\r\n                <img src=\"http://oneframe-livedemo-api.azurewebsites.net/img/customer-email/oneFrameLogo.svg\" alt=\"\" style=\"display: block; border: 0\" width=\"500\" />\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <td width=\"800\">\r\n                <p align=\"center\">\r\n                    <font size=\"4\" color=\"#525d7d\">\r\n                        Hello!\r\n                    </font>\r\n                </p>\r\n                <p align=\"center\">\r\n                    <font size=\"4\" color=\"#525d7d\" style=\"line-height: 1.86\">\r\n                        Please click the link to activate your email address!\r\n                    </font>\r\n                </p>\r\n                <p align=\"center\">\r\n                    <br />\r\n                    <font color=\"#41a3e9\" size=\"4\">\r\n                        <strong>\r\n                                <a href=\"RESET_URL\" class=\" btn-lg px-4 gap-3\">Click Me!</a>\r\n                        </strong>\r\n                    </font>\r\n                    <br />\r\n                    <br />\r\n                </p>\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <td width=\"800\" height=\"658\">\r\n                <a href=\"#\">\r\n                    <img src=\"http://oneframe-livedemo-api.azurewebsites.net/img/customer-email/03.jpg\" alt=\"\" style=\"display: block; border: 0\" />\r\n\r\n                </a>\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <td bgcolor=\"#0B1D44\" width=\"800\" height=\"100\">\r\n                <table>\r\n                    <tr>\r\n                        <td width=\"260\"></td>\r\n                        <td width=\"280\" align=\"center\">\r\n                            <img src=\"http://oneframe-livedemo-api.azurewebsites.net/img/customer-email/logo-en.png\"\r\n                                 alt=\"\"\r\n                                 style=\"display: block; border: 0\" />\r\n                        </td>\r\n                        <td width=\"260\"></td>\r\n                    </tr>\r\n                </table>\r\n            </td>\r\n        </tr>\r\n    </table>\r\n    <script src=\"https://ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js\"></script>\r\n    <script async src=\"maxcdn.bootstrapcdn.com/bootstrap/3.3.1/js/bootstrap.min.js\"></script>\r\n</body>\r\n</html>", 1, new Guid("db0eada7-a618-435e-834c-919ac95a97ce"), "Email Activation" },
                    { new Guid("131eaa1c-b3c3-4d24-a2d7-85a612f75295"), "<!DOCTYPE html>\r\n<html lang=\"ar\">\r\n<head>\r\n    <title>OneFrame</title>\r\n    <meta charset=\"utf-8\">\r\n    <meta http-equiv=\"X-UA-Compatible\" content=\"IE =edge\">\r\n    <meta name=\"viewport\" content=\"width =device-width, initial-scale=1\">\r\n    <link href=\"maxcdn.bootstrapcdn.com/bootstrap/3.3.1/css/bootstrap.min.css\" rel=\"stylesheet\">\r\n</head>\r\n<body>\r\n    <table style=\"\r\n        font-family: Arial, Helvetica, sans-serif;\r\n        margin: 0 auto;\r\n        display: table;\r\n      \"\r\n           width=\"800\"\r\n           align=\"center\"\r\n           cellspacing=\"0\"\r\n           cellpadding=\"0\"\r\n           border=\"0\">\r\n        <tr>\r\n            <td width=\"800\" height=\"199\" align=\"center\">\r\n                <img src=\"http://oneframe-livedemo-api.azurewebsites.net/img/customer-email/oneFrameLogo.svg\" alt=\"\" style=\"display: block; border: 0\" width=\"500\" />\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <td width=\"800\">\r\n                <p align=\"center\">\r\n                    <font size=\"4\" color=\"#525d7d\">\r\n                        أهلا بك!\r\n                    </font>\r\n                </p>\r\n                <p align=\"center\">\r\n                    <font size=\"4\" color=\"#525d7d\" style=\"line-height: 1.86\">\r\n                        لرجاء الضغط على الرابط لتفعيل عنوان بريدك الإلكتروني!\r\n                    </font>\r\n                </p>\r\n                <p align=\"center\">\r\n                    <br />\r\n                    <font color=\"#41a3e9\" size=\"4\">\r\n                        <strong>\r\n                            <a href=\"RESET_URL\" class=\" btn-lg px-4 gap-3\">نقر فوق لي!</a>\r\n                        </strong>\r\n                    </font>\r\n                    <br />\r\n                    <br />\r\n                </p>\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <td width=\"800\" height=\"658\">\r\n                <a href=\"#\">\r\n                    <img src=\"http://oneframe-livedemo-api.azurewebsites.net/img/customer-email/03.jpg\" alt=\"\" style=\"display: block; border: 0\" />\r\n\r\n                </a>\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <td bgcolor=\"#0B1D44\" width=\"800\" height=\"100\">\r\n                <table>\r\n                    <tr>\r\n                        <td width=\"260\"></td>\r\n                        <td width=\"280\" align=\"center\">\r\n                            <img src=\"http://oneframe-livedemo-api.azurewebsites.net/img/customer-email/logo-en.png\"\r\n                                 alt=\"\"\r\n                                 style=\"display: block; border: 0\" />\r\n                        </td>\r\n                        <td width=\"260\"></td>\r\n                    </tr>\r\n                </table>\r\n            </td>\r\n        </tr>\r\n    </table>\r\n    <script src=\"https://ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js\"></script>\r\n    <script async src=\"maxcdn.bootstrapcdn.com/bootstrap/3.3.1/js/bootstrap.min.js\"></script>\r\n</body>\r\n</html>", 3, new Guid("db0eada7-a618-435e-834c-919ac95a97ce"), "تفعيل البريد الإلكتروني" },
                    { new Guid("1869f4f7-e8ae-4156-b3ba-4dead5542b66"), "<!DOCTYPE html>\r\n<html lang=\"ar\">\r\n<head>\r\n    <title>OneFrame</title>\r\n    <meta charset=\"utf-8\">\r\n    <meta http-equiv=\"X-UA-Compatible\" content=\"IE =edge\">\r\n    <meta name=\"viewport\" content=\"width =device-width, initial-scale=1\">\r\n    <link href=\"maxcdn.bootstrapcdn.com/bootstrap/3.3.1/css/bootstrap.min.css\" rel=\"stylesheet\">\r\n</head>\r\n<body>\r\n    <table style=\"\r\n        font-family: Arial, Helvetica, sans-serif;\r\n        margin: 0 auto;\r\n        display: table;\r\n      \"\r\n           width=\"800\"\r\n           align=\"center\"\r\n           cellspacing=\"0\"\r\n           cellpadding=\"0\"\r\n           border=\"0\">\r\n        <tr>\r\n            <td width=\"800\" height=\"199\" align=\"center\">\r\n                <img src=\"http://oneframe-livedemo-api.azurewebsites.net/img/customer-email/oneFrameLogo.svg\" alt=\"\" style=\"display: block; border: 0\" width=\"500\" />\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <td width=\"800\">\r\n                <p align=\"center\">\r\n                    <font size=\"4\" color=\"#525d7d\">\r\n                        مرحبًا!\r\n                    </font>\r\n                </p>\r\n                <p align=\"center\">\r\n                    <font size=\"4\" color=\"#525d7d\" style=\"line-height: 1.86\">\r\n                        مرحبًا بكم في OneFrame!\r\n                    </font>\r\n                </p>\r\n                <p align=\"center\">\r\n                    <font size=\"4\" color=\"#525d7d\" style=\"line-height: 1.86\">\r\n                        اكتمل حسابك بنجاح.\r\n                    </font>\r\n                </p>\r\n                <p align=\"center\">\r\n                    <br />\r\n                    <font color=\"#41a3e9\" size=\"4\">\r\n                        <strong>\r\n                            <a href=\"{resetUrl}\" class=\" btn-lg px-4 gap-3\">قم بتسجيل الدخول باستخدام بريدك الإلكتروني وكلمة المرور</a>\r\n                        </strong>\r\n                    </font>\r\n                    <br />\r\n                    <br />\r\n                </p>\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <td width=\"800\" height=\"658\">\r\n                <a href=\"#\">\r\n                    <img src=\"http://oneframe-livedemo-api.azurewebsites.net/img/customer-email/03.jpg\" alt=\"\" style=\"display: block; border: 0\" />\r\n\r\n                </a>\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <td bgcolor=\"#0B1D44\" width=\"800\" height=\"100\">\r\n                <table>\r\n                    <tr>\r\n                        <td width=\"260\"></td>\r\n                        <td width=\"280\" align=\"center\">\r\n                            <img src=\"http://oneframe-livedemo-api.azurewebsites.net/}img/customer-email/logo-en.png\"\r\n                                 alt=\"\"\r\n                                 style=\"display: block; border: 0\" />\r\n                        </td>\r\n                        <td width=\"260\"></td>\r\n                    </tr>\r\n                </table>\r\n            </td>\r\n        </tr>\r\n    </table>\r\n    <script src=\"https://ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js\"></script>\r\n    <script async src=\"maxcdn.bootstrapcdn.com/bootstrap/3.3.1/js/bootstrap.min.js\"></script>\r\n</body>\r\n</html>", 3, new Guid("3d9c744e-5d4a-4f52-822b-e777e301b7cf"), "أهلا بك" },
                    { new Guid("3612a90c-e6fb-4059-a281-ab38e69ecdcb"), "<!DOCTYPE html>\r\n<html lang=\"tr\">\r\n<head>\r\n    <title>OneFrame</title>\r\n    <meta charset=\"utf-8\">\r\n    <meta http-equiv=\"X-UA-Compatible\" content=\"IE =edge\">\r\n    <meta name=\"viewport\" content=\"width =device-width, initial-scale=1\">\r\n    <link href=\"maxcdn.bootstrapcdn.com/bootstrap/3.3.1/css/bootstrap.min.css\" rel=\"stylesheet\">\r\n</head>\r\n<body>\r\n    <table style=\"\r\n        font-family: Arial, Helvetica, sans-serif;\r\n        margin: 0 auto;\r\n        display: table;\r\n      \"\r\n           width=\"800\"\r\n           align=\"center\"\r\n           cellspacing=\"0\"\r\n           cellpadding=\"0\"\r\n           border=\"0\">\r\n        <tr>\r\n            <td width=\"800\" height=\"199\" align=\"center\">\r\n                <img src=\"http://oneframe-livedemo-api.azurewebsites.net/img/customer-email/oneFrameLogo.svg\" alt=\"\" style=\"display: block; border: 0\" width=\"500\" />\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <td width=\"800\">\r\n                <p align=\"center\">\r\n                    <font size=\"4\" color=\"#525d7d\">\r\n                        Merhaba!\r\n                    </font>\r\n                </p>\r\n                <p align=\"center\">\r\n                    <font size=\"4\" color=\"#525d7d\" style=\"line-height: 1.86\">\r\n                        E-posta adresinizi doğrulamak için lütfen bağlantıya tıklayın!\r\n                    </font>\r\n                </p>\r\n                <p align=\"center\">\r\n                    <br />\r\n                    <font color=\"#41a3e9\" size=\"4\">\r\n                        <strong>\r\n                            <a href=\"RESET_URL\" class=\" btn-lg px-4 gap-3\">Doğrula!</a>\r\n                        </strong>\r\n                    </font>\r\n                    <br />\r\n                    <br />\r\n                </p>\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <td width=\"800\" height=\"658\">\r\n                <a href=\"#\">\r\n                    <img src=\"http://oneframe-livedemo-api.azurewebsites.net/img/customer-email/03.jpg\" alt=\"\" style=\"display: block; border: 0\" />\r\n\r\n                </a>\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <td bgcolor=\"#0B1D44\" width=\"800\" height=\"100\">\r\n                <table>\r\n                    <tr>\r\n                        <td width=\"260\"></td>\r\n                        <td width=\"280\" align=\"center\">\r\n                            <img src=\"http://oneframe-livedemo-api.azurewebsites.net/img/customer-email/logo-tr.png\"\r\n                                 alt=\"\"\r\n                                 style=\"display: block; border: 0\" />\r\n                        </td>\r\n                        <td width=\"260\"></td>\r\n                    </tr>\r\n                </table>\r\n            </td>\r\n        </tr>\r\n    </table>\r\n    <script src=\"https://ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js\"></script>\r\n    <script async src=\"maxcdn.bootstrapcdn.com/bootstrap/3.3.1/js/bootstrap.min.js\"></script>\r\n</body>\r\n</html>", 2, new Guid("db0eada7-a618-435e-834c-919ac95a97ce"), "Mail Aktivasyon" },
                    { new Guid("6f22b299-ae8b-495b-a7a3-1847838e6c65"), "<!DOCTYPE html>\r\n<html lang=\"tr\">\r\n<head>\r\n    <title>OneFrame</title>\r\n    <meta charset=\"utf-8\">\r\n    <meta http-equiv=\"X-UA-Compatible\" content=\"IE =edge\">\r\n    <meta name=\"viewport\" content=\"width =device-width, initial-scale=1\">\r\n    <link href=\"maxcdn.bootstrapcdn.com/bootstrap/3.3.1/css/bootstrap.min.css\" rel=\"stylesheet\">\r\n</head>\r\n<body>\r\n    <table style=\"\r\n        font-family: Arial, Helvetica, sans-serif;\r\n        margin: 0 auto;\r\n        display: table;\r\n      \"\r\n           width=\"800\"\r\n           align=\"center\"\r\n           cellspacing=\"0\"\r\n           cellpadding=\"0\"\r\n           border=\"0\">\r\n        <tr>\r\n            <td width=\"800\" height=\"199\" align=\"center\">\r\n                <img src=\"http://oneframe-livedemo-api.azurewebsites.net/img/customer-email/oneFrameLogo.svg\" alt=\"\" style=\"display: block; border: 0\" width=\"500\" />\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <td width=\"800\">\r\n                <p align=\"center\">\r\n                    <font size=\"4\" color=\"#525d7d\">\r\n                        Merhaba!\r\n                    </font>\r\n                </p>\r\n                <p align=\"center\">\r\n                    <font size=\"4\" color=\"#525d7d\" style=\"line-height: 1.86\">\r\n                        OneFrame hesabınızı etkinleştirmek için e-posta adresinizi doğrulamanız gerekir.\r\n                    </font>\r\n                </p>\r\n                <p align=\"center\">\r\n                    <br />\r\n                    <font color=\"#41a3e9\" size=\"4\">\r\n                        <strong>\r\n                            İki faktörlü doğrulama kodunuz: {token}\r\n                        </strong>\r\n                    </font>\r\n                    <br />\r\n                    <br />\r\n                </p>\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <td width=\"800\" height=\"658\">\r\n                <a href=\"#\">\r\n                    <img src=\"http://oneframe-livedemo-api.azurewebsites.net/img/customer-email/03.jpg\" alt=\"\" style=\"display: block; border: 0\" />\r\n\r\n                </a>\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <td bgcolor=\"#0B1D44\" width=\"800\" height=\"100\">\r\n                <table>\r\n                    <tr>\r\n                        <td width=\"260\"></td>\r\n                        <td width=\"280\" align=\"center\">\r\n                            <img src=\"http://oneframe-livedemo-api.azurewebsites.net/img/customer-email/logo-tr.png\"\r\n                                 alt=\"\"\r\n                                 style=\"display: block; border: 0\" />\r\n                        </td>\r\n                        <td width=\"260\"></td>\r\n                    </tr>\r\n                </table>\r\n            </td>\r\n        </tr>\r\n    </table>\r\n    <script src=\"https://ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js\"></script>\r\n    <script async src=\"maxcdn.bootstrapcdn.com/bootstrap/3.3.1/js/bootstrap.min.js\"></script>\r\n</body>\r\n</html>", 2, new Guid("b811c8ca-d12f-404b-affd-a5dc6e402945"), "Doğrulama Kodunuz" },
                    { new Guid("8d396fb3-675c-477c-8e14-278bb549fbae"), "<!DOCTYPE html>\r\n<html lang=\"en\">\r\n<head>\r\n    <title>OneFrame</title>\r\n    <meta charset=\"utf-8\">\r\n    <meta http-equiv=\"X-UA-Compatible\" content=\"IE =edge\">\r\n    <meta name=\"viewport\" content=\"width =device-width, initial-scale=1\">\r\n    <link href=\"maxcdn.bootstrapcdn.com/bootstrap/3.3.1/css/bootstrap.min.css\" rel=\"stylesheet\">\r\n</head>\r\n<body>\r\n    <table style=\"\r\n        font-family: Arial, Helvetica, sans-serif;\r\n        margin: 0 auto;\r\n        display: table;\r\n      \"\r\n           width=\"800\"\r\n           align=\"center\"\r\n           cellspacing=\"0\"\r\n           cellpadding=\"0\"\r\n           border=\"0\">\r\n        <tr>\r\n            <td width=\"800\" height=\"199\" align=\"center\">\r\n                <img src=\"http://oneframe-livedemo-api.azurewebsites.net/img/customer-email/oneFrameLogo.svg\" alt=\"\" style=\"display: block; border: 0\" width=\"500\" />\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <td width=\"800\">\r\n                <p align=\"center\">\r\n                    <font size=\"4\" color=\"#525d7d\">\r\n                        Hello!\r\n                    </font>\r\n                </p>\r\n                <p align=\"center\">\r\n                    <font size=\"4\" color=\"#525d7d\" style=\"line-height: 1.86\">\r\n                        Someone, hopefully you, has requested to reset the password.\r\n\r\n                    </font>\r\n                </p>\r\n                <p align=\"center\">\r\n                    <font size=\"4\" color=\"#525d7d\" style=\"line-height: 1.86\">\r\n                        If you did not perform this request, you can safely ignore this email.\r\n                    </font>\r\n                </p>\r\n                <p align=\"center\">\r\n                    <font size=\"4\" color=\"#525d7d\" style=\"line-height: 1.86\">\r\n                        Otherwise, click the link below to complete the process.\r\n                    </font>\r\n                </p>\r\n                <p align=\"center\">\r\n                    <br />\r\n                    <font color=\"#41a3e9\" size=\"4\">\r\n                        <strong>\r\n                            <a href=\"{resetUrl}\" class=\" btn-lg px-4 gap-3\">Reset Password!</a>\r\n                        </strong>\r\n                    </font>\r\n                    <br />\r\n                    <br />\r\n                </p>\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <td width=\"800\" height=\"658\">\r\n                <a href=\"#\">\r\n                    <img src=\"http://oneframe-livedemo-api.azurewebsites.net/img/customer-email/03.jpg\" alt=\"\" style=\"display: block; border: 0\" />\r\n\r\n                </a>\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <td bgcolor=\"#0B1D44\" width=\"800\" height=\"100\">\r\n                <table>\r\n                    <tr>\r\n                        <td width=\"260\"></td>\r\n                        <td width=\"280\" align=\"center\">\r\n                            <img src=\"http://oneframe-livedemo-api.azurewebsites.net/img/customer-email/logo-en.png\"\r\n                                 alt=\"\"\r\n                                 style=\"display: block; border: 0\" />\r\n                        </td>\r\n                        <td width=\"260\"></td>\r\n                    </tr>\r\n                </table>\r\n            </td>\r\n        </tr>\r\n    </table>\r\n    <script src=\"https://ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js\"></script>\r\n    <script async src=\"maxcdn.bootstrapcdn.com/bootstrap/3.3.1/js/bootstrap.min.js\"></script>\r\n</body>\r\n</html>", 1, new Guid("9459e146-558a-44c5-972e-28a2469b4d1a"), "You wanted to reset your password" },
                    { new Guid("b9e86b4a-f377-46cc-8d88-7c2d08e9f3c3"), "<!DOCTYPE html>\r\n<html lang=\"tr\">\r\n<head>\r\n    <title>OneFrame</title>\r\n    <meta charset=\"utf-8\">\r\n    <meta http-equiv=\"X-UA-Compatible\" content=\"IE =edge\">\r\n    <meta name=\"viewport\" content=\"width =device-width, initial-scale=1\">\r\n    <link href=\"maxcdn.bootstrapcdn.com/bootstrap/3.3.1/css/bootstrap.min.css\" rel=\"stylesheet\">\r\n</head>\r\n<body>\r\n    <table style=\"\r\n        font-family: Arial, Helvetica, sans-serif;\r\n        margin: 0 auto;\r\n        display: table;\r\n      \"\r\n           width=\"800\"\r\n           align=\"center\"\r\n           cellspacing=\"0\"\r\n           cellpadding=\"0\"\r\n           border=\"0\">\r\n        <tr>\r\n            <td width=\"800\" height=\"199\" align=\"center\">\r\n                <img src=\"http://oneframe-livedemo-api.azurewebsites.net/img/customer-email/oneFrameLogo.svg\" alt=\"\" style=\"display: block; border: 0\" width=\"500\" />\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <td width=\"800\">\r\n                <p align=\"center\">\r\n                    <font size=\"4\" color=\"#525d7d\">\r\n                        Merhaba!\r\n                    </font>\r\n                </p>\r\n                <p align=\"center\">\r\n                    <font size=\"4\" color=\"#525d7d\" style=\"line-height: 1.86\">\r\n                        Birisi, umarız siz, parolanın sıfırlanmasını talep etmiştir.\r\n                    </font>\r\n                </p>\r\n                <p align=\"center\">\r\n                    <font size=\"4\" color=\"#525d7d\" style=\"line-height: 1.86\">\r\n                        Bu isteği siz gerçekleştirmediyseniz, bu e-postayı güvenle yok sayabilirsiniz.\r\n                    </font>\r\n                </p>\r\n                <p align=\"center\">\r\n                    <font size=\"4\" color=\"#525d7d\" style=\"line-height: 1.86\">\r\n                        Aksi takdirde, işlemi tamamlamak için aşağıdaki bağlantıya tıklayın.\r\n                    </font>\r\n                </p>\r\n                <p align=\"center\">\r\n                    <br />\r\n                    <font color=\"#41a3e9\" size=\"4\">\r\n                        <strong>\r\n                            <a href=\"{resetUrl}\" class=\" btn-lg px-4 gap-3\">Şifreyi Yenile!</a>\r\n                        </strong>\r\n                    </font>\r\n                    <br />\r\n                    <br />\r\n                </p>\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <td width=\"800\" height=\"658\">\r\n                <a href=\"#\">\r\n                    <img src=\"http://oneframe-livedemo-api.azurewebsites.net/img/customer-email/03.jpg\" alt=\"\" style=\"display: block; border: 0\" />\r\n\r\n                </a>\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <td bgcolor=\"#0B1D44\" width=\"800\" height=\"100\">\r\n                <table>\r\n                    <tr>\r\n                        <td width=\"260\"></td>\r\n                        <td width=\"280\" align=\"center\">\r\n                            <img src=\"http://oneframe-livedemo-api.azurewebsites.net/img/customer-email/logo-tr.png\"\r\n                                 alt=\"\"\r\n                                 style=\"display: block; border: 0\" />\r\n                        </td>\r\n                        <td width=\"260\"></td>\r\n                    </tr>\r\n                </table>\r\n            </td>\r\n        </tr>\r\n    </table>\r\n    <script src=\"https://ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js\"></script>\r\n    <script async src=\"maxcdn.bootstrapcdn.com/bootstrap/3.3.1/js/bootstrap.min.js\"></script>\r\n</body>\r\n</html>", 2, new Guid("9459e146-558a-44c5-972e-28a2469b4d1a"), "Şifrenizi sıfırlamak istediniz" },
                    { new Guid("be109d28-74d9-4d8a-b155-50f56cce7758"), "<!DOCTYPE html>\r\n<html lang=\"ar\">\r\n<head>\r\n    <title>OneFrame</title>\r\n    <meta charset=\"utf-8\">\r\n    <meta http-equiv=\"X-UA-Compatible\" content=\"IE =edge\">\r\n    <meta name=\"viewport\" content=\"width =device-width, initial-scale=1\">\r\n    <link href=\"maxcdn.bootstrapcdn.com/bootstrap/3.3.1/css/bootstrap.min.css\" rel=\"stylesheet\">\r\n</head>\r\n<body>\r\n    <table style=\"\r\n        font-family: Arial, Helvetica, sans-serif;\r\n        margin: 0 auto;\r\n        display: table;\r\n      \"\r\n           width=\"800\"\r\n           align=\"center\"\r\n           cellspacing=\"0\"\r\n           cellpadding=\"0\"\r\n           border=\"0\">\r\n        <tr>\r\n            <td width=\"800\" height=\"199\" align=\"center\">\r\n                <img src=\"http://oneframe-livedemo-api.azurewebsites.net/img/customer-email/oneFrameLogo.svg\" alt=\"\" style=\"display: block; border: 0\" width=\"500\" />\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <td width=\"800\">\r\n                <p align=\"center\">\r\n                    <font size=\"4\" color=\"#525d7d\">\r\n                        مرحبًا!\r\n                    </font>\r\n                </p>\r\n                <p align=\"center\">\r\n                    <font size=\"4\" color=\"#525d7d\" style=\"line-height: 1.86\">\r\n                        شخص ما ، نأمل أنت ، قد طلب إعادة تعيين كلمة المرور.\r\n                    </font>\r\n                </p>\r\n                <p align=\"center\">\r\n                    <font size=\"4\" color=\"#525d7d\" style=\"line-height: 1.86\">\r\n                        إذا لم تقم بتنفيذ هذا الطلب ، فيمكنك تجاهل هذا البريد الإلكتروني بأمان.\r\n                    </font>\r\n                </p>\r\n                <p align=\"center\">\r\n                    <font size=\"4\" color=\"#525d7d\" style=\"line-height: 1.86\">\r\n                        خلاف ذلك ، انقر فوق الارتباط أدناه لإكمال العملية.\r\n                    </font>\r\n                </p>\r\n                <p align=\"center\">\r\n                    <br />\r\n                    <font color=\"#41a3e9\" size=\"4\">\r\n                        <strong>\r\n                            <a href=\"{resetUrl}\" class=\" btn-lg px-4 gap-3\">إعادة تعيين كلمة المرور!</a>\r\n                        </strong>\r\n                    </font>\r\n                    <br />\r\n                    <br />\r\n                </p>\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <td width=\"800\" height=\"658\">\r\n                <a href=\"#\">\r\n                    <img src=\"http://oneframe-livedemo-api.azurewebsites.net/img/customer-email/03.jpg\" alt=\"\" style=\"display: block; border: 0\" />\r\n\r\n                </a>\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <td bgcolor=\"#0B1D44\" width=\"800\" height=\"100\">\r\n                <table>\r\n                    <tr>\r\n                        <td width=\"260\"></td>\r\n                        <td width=\"280\" align=\"center\">\r\n                            <img src=\"http://oneframe-livedemo-api.azurewebsites.net/img/customer-email/logo-en.png\"\r\n                                 alt=\"\"\r\n                                 style=\"display: block; border: 0\" />\r\n                        </td>\r\n                        <td width=\"260\"></td>\r\n                    </tr>\r\n                </table>\r\n            </td>\r\n        </tr>\r\n    </table>\r\n    <script src=\"https://ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js\"></script>\r\n    <script async src=\"maxcdn.bootstrapcdn.com/bootstrap/3.3.1/js/bootstrap.min.js\"></script>\r\n</body>\r\n</html>", 3, new Guid("9459e146-558a-44c5-972e-28a2469b4d1a"), "كنت تريد إعادة تعيين كلمة المرور الخاصة بك" },
                    { new Guid("c2bf84bb-8f5f-4f11-b6c1-ea1bfcdc915f"), "<!DOCTYPE html>\r\n<html lang=\"en\">\r\n<head>\r\n    <title>OneFrame</title>\r\n    <meta charset=\"utf-8\">\r\n    <meta http-equiv=\"X-UA-Compatible\" content=\"IE =edge\">\r\n    <meta name=\"viewport\" content=\"width =device-width, initial-scale=1\">\r\n    <link href=\"maxcdn.bootstrapcdn.com/bootstrap/3.3.1/css/bootstrap.min.css\" rel=\"stylesheet\">\r\n</head>\r\n<body>\r\n    <table style=\"\r\n        font-family: Arial, Helvetica, sans-serif;\r\n        margin: 0 auto;\r\n        display: table;\r\n      \"\r\n           width=\"800\"\r\n           align=\"center\"\r\n           cellspacing=\"0\"\r\n           cellpadding=\"0\"\r\n           border=\"0\">\r\n        <tr>\r\n            <td width=\"800\" height=\"199\" align=\"center\">\r\n                <img src=\"http://oneframe-livedemo-api.azurewebsites.net/img/customer-email/oneFrameLogo.svg\" alt=\"\" style=\"display: block; border: 0\" width=\"500\" />\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <td width=\"800\">\r\n                <p align=\"center\">\r\n                    <font size=\"4\" color=\"#525d7d\">\r\n                        Hello!\r\n                    </font>\r\n                </p>\r\n                <p align=\"center\">\r\n                    <font size=\"4\" color=\"#525d7d\" style=\"line-height: 1.86\">\r\n                        Welcome OneFrame!\r\n                    </font>\r\n                </p>\r\n                <p align=\"center\">\r\n                    <font size=\"4\" color=\"#525d7d\" style=\"line-height: 1.86\">\r\n                        Your account has been successfully completed.\r\n                    </font>\r\n                </p>\r\n                <p align=\"center\">\r\n                    <br />\r\n                    <font color=\"#41a3e9\" size=\"4\">\r\n                        <strong>\r\n                            <a href=\"{resetUrl}\" class=\" btn-lg px-4 gap-3\">Sign in with your email and password</a>\r\n                        </strong>\r\n                    </font>\r\n                    <br />\r\n                    <br />\r\n                </p>\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <td width=\"800\" height=\"658\">\r\n                <a href=\"#\">\r\n                    <img src=\"http://oneframe-livedemo-api.azurewebsites.net/img/customer-email/03.jpg\" alt=\"\" style=\"display: block; border: 0\" />\r\n\r\n                </a>\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <td bgcolor=\"#0B1D44\" width=\"800\" height=\"100\">\r\n                <table>\r\n                    <tr>\r\n                        <td width=\"260\"></td>\r\n                        <td width=\"280\" align=\"center\">\r\n                            <img src=\"http://oneframe-livedemo-api.azurewebsites.net/img/customer-email/logo-en.png\"\r\n                                 alt=\"\"\r\n                                 style=\"display: block; border: 0\" />\r\n                        </td>\r\n                        <td width=\"260\"></td>\r\n                    </tr>\r\n                </table>\r\n            </td>\r\n        </tr>\r\n    </table>\r\n    <script src=\"https://ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js\"></script>\r\n    <script async src=\"maxcdn.bootstrapcdn.com/bootstrap/3.3.1/js/bootstrap.min.js\"></script>\r\n</body>\r\n</html>", 1, new Guid("3d9c744e-5d4a-4f52-822b-e777e301b7cf"), "Welcome" },
                    { new Guid("d26fe09f-c2fd-4274-a915-06818937f463"), "<!DOCTYPE html>\r\n<html lang=\"en\">\r\n<head>\r\n    <title>OneFrame</title>\r\n    <meta charset=\"utf-8\">\r\n    <meta http-equiv=\"X-UA-Compatible\" content=\"IE =edge\">\r\n    <meta name=\"viewport\" content=\"width =device-width, initial-scale=1\">\r\n    <link href=\"maxcdn.bootstrapcdn.com/bootstrap/3.3.1/css/bootstrap.min.css\" rel=\"stylesheet\">\r\n</head>\r\n<body>\r\n    <table style=\"\r\n        font-family: Arial, Helvetica, sans-serif;\r\n        margin: 0 auto;\r\n        display: table;\r\n      \"\r\n           width=\"800\"\r\n           align=\"center\"\r\n           cellspacing=\"0\"\r\n           cellpadding=\"0\"\r\n           border=\"0\">\r\n        <tr>\r\n            <td width=\"800\" height=\"199\" align=\"center\">\r\n                <img src=\"http://oneframe-livedemo-api.azurewebsites.net/img/customer-email/oneFrameLogo.svg\" alt=\"\" style=\"display: block; border: 0\" width=\"500\" />\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <td width=\"800\">\r\n                <p align=\"center\">\r\n                    <font size=\"4\" color=\"#525d7d\">\r\n                        Hello!\r\n                    </font>\r\n                </p>\r\n                <p align=\"center\">\r\n                    <font size=\"4\" color=\"#525d7d\" style=\"line-height: 1.86\">\r\n                        You need to verify your email address to activate your OneFrame account.\r\n                    </font>\r\n                </p>\r\n                <p align=\"center\">\r\n                    <br />\r\n                    <font color=\"#41a3e9\" size=\"4\">\r\n                        <strong>\r\n                            Your two factor verification code is: {token}\r\n                        </strong>\r\n                    </font>\r\n                    <br />\r\n                    <br />\r\n                </p>\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <td width=\"800\" height=\"658\">\r\n                <a href=\"#\">\r\n                    <img src=\"http://oneframe-livedemo-api.azurewebsites.net/img/customer-email/03.jpg\" alt=\"\" style=\"display: block; border: 0\" />\r\n\r\n                </a>\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <td bgcolor=\"#0B1D44\" width=\"800\" height=\"100\">\r\n                <table>\r\n                    <tr>\r\n                        <td width=\"260\"></td>\r\n                        <td width=\"280\" align=\"center\">\r\n                            <img src=\"http://oneframe-livedemo-api.azurewebsites.net/img/customer-email/logo-en.png\"\r\n                                 alt=\"\"\r\n                                 style=\"display: block; border: 0\" />\r\n                        </td>\r\n                        <td width=\"260\"></td>\r\n                    </tr>\r\n                </table>\r\n            </td>\r\n        </tr>\r\n    </table>\r\n    <script src=\"https://ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js\"></script>\r\n    <script async src=\"maxcdn.bootstrapcdn.com/bootstrap/3.3.1/js/bootstrap.min.js\"></script>\r\n</body>\r\n</html>", 1, new Guid("b811c8ca-d12f-404b-affd-a5dc6e402945"), "Your Verification Code" },
                    { new Guid("dc3a6ca1-aa63-43ad-a7a4-42d09c1267f9"), "<!DOCTYPE html>\r\n<html lang=\"ar\">\r\n<head>\r\n    <title>OneFrame</title>\r\n    <meta charset=\"utf-8\">\r\n    <meta http-equiv=\"X-UA-Compatible\" content=\"IE =edge\">\r\n    <meta name=\"viewport\" content=\"width =device-width, initial-scale=1\">\r\n    <link href=\"maxcdn.bootstrapcdn.com/bootstrap/3.3.1/css/bootstrap.min.css\" rel=\"stylesheet\">\r\n</head>\r\n<body>\r\n    <table style=\"\r\n        font-family: Arial, Helvetica, sans-serif;\r\n        margin: 0 auto;\r\n        display: table;\r\n      \"\r\n           width=\"800\"\r\n           align=\"center\"\r\n           cellspacing=\"0\"\r\n           cellpadding=\"0\"\r\n           border=\"0\">\r\n        <tr>\r\n            <td width=\"800\" height=\"199\" align=\"center\">\r\n                <img src=\"http://oneframe-livedemo-api.azurewebsites.net/img/customer-email/oneFrameLogo.svg\" alt=\"\" style=\"display: block; border: 0\" width=\"500\" />\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <td width=\"800\">\r\n                <p align=\"center\">\r\n                    <font size=\"4\" color=\"#525d7d\">\r\n                        مرحبًا!\r\n                    </font>\r\n                </p>\r\n                <p align=\"center\">\r\n                    <font size=\"4\" color=\"#525d7d\" style=\"line-height: 1.86\">\r\n                        تحتاج إلى التحقق من عنوان بريدك الإلكتروني لتفعيل حساب OneFrame الخاص بك.\r\n                    </font>\r\n                </p>\r\n                <p align=\"center\">\r\n                    <br />\r\n                    <font color=\"#41a3e9\" size=\"4\">\r\n                        <strong>\r\n                            رمز التحقق الثنائي الخاص بك هو: {token}\r\n                        </strong>\r\n                    </font>\r\n                    <br />\r\n                    <br />\r\n                </p>\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <td width=\"800\" height=\"658\">\r\n                <a href=\"#\">\r\n                    <img src=\"http://oneframe-livedemo-api.azurewebsites.net/img/customer-email/03.jpg\" alt=\"\" style=\"display: block; border: 0\" />\r\n\r\n                </a>\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <td bgcolor=\"#0B1D44\" width=\"800\" height=\"100\">\r\n                <table>\r\n                    <tr>\r\n                        <td width=\"260\"></td>\r\n                        <td width=\"280\" align=\"center\">\r\n                            <img src=\"http://oneframe-livedemo-api.azurewebsites.net/img/customer-email/logo-en.png\"\r\n                                 alt=\"\"\r\n                                 style=\"display: block; border: 0\" />\r\n                        </td>\r\n                        <td width=\"260\"></td>\r\n                    </tr>\r\n                </table>\r\n            </td>\r\n        </tr>\r\n    </table>\r\n    <script src=\"https://ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js\"></script>\r\n    <script async src=\"maxcdn.bootstrapcdn.com/bootstrap/3.3.1/js/bootstrap.min.js\"></script>\r\n</body>\r\n</html>", 3, new Guid("b811c8ca-d12f-404b-affd-a5dc6e402945"), "رمز التحقق" },
                    { new Guid("ed93391e-b050-4625-b6e9-4391e349dcd6"), "<!DOCTYPE html>\r\n<html lang=\"tr\">\r\n<head>\r\n    <title>OneFrame</title>\r\n    <meta charset=\"utf-8\">\r\n    <meta http-equiv=\"X-UA-Compatible\" content=\"IE =edge\">\r\n    <meta name=\"viewport\" content=\"width =device-width, initial-scale=1\">\r\n    <link href=\"maxcdn.bootstrapcdn.com/bootstrap/3.3.1/css/bootstrap.min.css\" rel=\"stylesheet\">\r\n</head>\r\n<body>\r\n    <table style=\"\r\n        font-family: Arial, Helvetica, sans-serif;\r\n        margin: 0 auto;\r\n        display: table;\r\n      \"\r\n           width=\"800\"\r\n           align=\"center\"\r\n           cellspacing=\"0\"\r\n           cellpadding=\"0\"\r\n           border=\"0\">\r\n        <tr>\r\n            <td width=\"800\" height=\"199\" align=\"center\">\r\n                <img src=\"http://oneframe-livedemo-api.azurewebsites.net/img/customer-email/oneFrameLogo.svg\" alt=\"\" style=\"display: block; border: 0\" width=\"500\" />\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <td width=\"800\">\r\n                <p align=\"center\">\r\n                    <font size=\"4\" color=\"#525d7d\">\r\n                        Merhaba!\r\n                    </font>\r\n                </p>\r\n                <p align=\"center\">\r\n                    <font size=\"4\" color=\"#525d7d\" style=\"line-height: 1.86\">\r\n                        OneFrame'e Hoş geldiniz!\r\n                    </font>\r\n                </p>\r\n                <p align=\"center\">\r\n                    <font size=\"4\" color=\"#525d7d\" style=\"line-height: 1.86\">\r\n                        Hesabınız başarıyla tamamlandı.\r\n                    </font>\r\n                </p>\r\n                <p align=\"center\">\r\n                    <br />\r\n                    <font color=\"#41a3e9\" size=\"4\">\r\n                        <strong>\r\n                            <a href=\"{resetUrl}\" class=\" btn-lg px-4 gap-3\">E-posta adresiniz ve şifrenizle giriş yapın</a>\r\n                        </strong>\r\n                    </font>\r\n                    <br />\r\n                    <br />\r\n                </p>\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <td width=\"800\" height=\"658\">\r\n                <a href=\"#\">\r\n                    <img src=\"http://oneframe-livedemo-api.azurewebsites.net/img/customer-email/03.jpg\" alt=\"\" style=\"display: block; border: 0\" />\r\n\r\n                </a>\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <td bgcolor=\"#0B1D44\" width=\"800\" height=\"100\">\r\n                <table>\r\n                    <tr>\r\n                        <td width=\"260\"></td>\r\n                        <td width=\"280\" align=\"center\">\r\n                            <img src=\"http://oneframe-livedemo-api.azurewebsites.net/img/customer-email/logo-tr.png\"\r\n                                 alt=\"\"\r\n                                 style=\"display: block; border: 0\" />\r\n                        </td>\r\n                        <td width=\"260\"></td>\r\n                    </tr>\r\n                </table>\r\n            </td>\r\n        </tr>\r\n    </table>\r\n    <script src=\"https://ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js\"></script>\r\n    <script async src=\"maxcdn.bootstrapcdn.com/bootstrap/3.3.1/js/bootstrap.min.js\"></script>\r\n</body>\r\n</html>", 2, new Guid("3d9c744e-5d4a-4f52-822b-e777e301b7cf"), "Hoşgeldiniz" }
                });

            migrationBuilder.InsertData(
                table: "Menu",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "Icon", "Name", "OrderId", "ParentId", "Url" },
                values: new object[,]
                {
                    { 2, "KsPermission", "Management_User_List", "user", "Users", 1, 1, "/users" },
                    { 3, "KsPermission", "Management_User_ClaimList", "id-badge", "User Claim Management", 2, 1, "/users/user-claims" },
                    { 5, "KsPermission", "Management_Role_List", "flag", "Roles", 1, 4, "/roles" },
                    { 6, "KsPermission", "Management_Role_ClaimList", "id-badge", "Role Claim Management", 2, 4, "/roles/role-claims" },
                    { 8, "KsPermission", "Management_ApplicationSetting_List", "square", "Application Settings", 2, 7, "/application-settings" },
                    { 9, "KsPermission", "Management_ApplicationSettingCategory_List", "square", "Application Setting Categories", 1, 7, "/application-setting-categories" },
                    { 11, "KsPermission", "Report_LoginAuditLog_List", "file-alt", "Login Audit Logs", 1, 10, "/login-audit-logs" }
                });

            migrationBuilder.InsertData(
                table: "Menu",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "Icon", "Name", "ParentId", "Url" },
                values: new object[] { 12, "KsPermission", "Management_EmailTemplate_List", "envelope", "Email Template", 7, "/email-templates" });

            migrationBuilder.InsertData(
                table: "Menu",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "Icon", "Name", "OrderId", "ParentId", "Url" },
                values: new object[,]
                {
                    { 13, "KsPermission", "Report_EmailNotification_List", "envelope", "Email Notifications", 2, 10, "/email-notifications" },
                    { 14, "KsPermission", "Management_Menu_List", "list-alt", "Menu Order", 3, 7, "/menus/order" }
                });

            migrationBuilder.InsertData(
                table: "Menu",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "Icon", "Name", "ParentId", "Url" },
                values: new object[] { 15, "KsPermission", "Management_Language_List", "flag", "Languages", 7, "/languages" });

            migrationBuilder.InsertData(
                table: "MenuTranslation",
                columns: new[] { "Id", "DisplayText", "Language", "ReferenceId" },
                values: new object[,]
                {
                    { 1, "User Management", 1, 1 },
                    { 2, "Kullanıcı Yönetimi", 2, 1 },
                    { 3, "إدارةالمستخدم", 3, 1 },
                    { 10, "Role Management", 1, 4 },
                    { 11, "Rol Yönetimi", 2, 4 },
                    { 12, "إدارة الأدوار", 3, 4 },
                    { 19, "Setting Management", 1, 7 },
                    { 20, "Ayar Yönetimi", 2, 7 },
                    { 21, "وضع الإدارة", 3, 7 },
                    { 28, "Reports", 1, 10 },
                    { 29, "Raporlar", 2, 10 },
                    { 30, "نقل", 3, 10 }
                });

            migrationBuilder.InsertData(
                table: "RoleClaim",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "RoleId" },
                values: new object[,]
                {
                    { 1, "KsPermission", "Management_Role_AddClaim", new Guid("00bb2e85-4474-414c-bed4-6d4fef568ec4") },
                    { 2, "KsPermission", "Management_Role_AddUser", new Guid("00bb2e85-4474-414c-bed4-6d4fef568ec4") },
                    { 3, "KsPermission", "Management_Role_ClaimList", new Guid("00bb2e85-4474-414c-bed4-6d4fef568ec4") },
                    { 4, "KsPermission", "Management_Role_Create", new Guid("00bb2e85-4474-414c-bed4-6d4fef568ec4") },
                    { 5, "KsPermission", "Management_Role_Delete", new Guid("00bb2e85-4474-414c-bed4-6d4fef568ec4") },
                    { 6, "KsPermission", "Management_Role_Edit", new Guid("00bb2e85-4474-414c-bed4-6d4fef568ec4") },
                    { 7, "KsPermission", "Management_Role_List", new Guid("00bb2e85-4474-414c-bed4-6d4fef568ec4") },
                    { 8, "KsPermission", "Management_Role_RemoveClaim", new Guid("00bb2e85-4474-414c-bed4-6d4fef568ec4") },
                    { 9, "KsPermission", "Management_Role_RemoveUser", new Guid("00bb2e85-4474-414c-bed4-6d4fef568ec4") },
                    { 10, "KsPermission", "Management_Role_UserList", new Guid("00bb2e85-4474-414c-bed4-6d4fef568ec4") },
                    { 11, "KsPermission", "Management_User_AddClaim", new Guid("00bb2e85-4474-414c-bed4-6d4fef568ec4") },
                    { 12, "KsPermission", "Management_User_ClaimList", new Guid("00bb2e85-4474-414c-bed4-6d4fef568ec4") },
                    { 13, "KsPermission", "Management_User_Create", new Guid("00bb2e85-4474-414c-bed4-6d4fef568ec4") },
                    { 14, "KsPermission", "Management_User_Delete", new Guid("00bb2e85-4474-414c-bed4-6d4fef568ec4") },
                    { 15, "KsPermission", "Management_User_Edit", new Guid("00bb2e85-4474-414c-bed4-6d4fef568ec4") },
                    { 16, "KsPermission", "Management_User_List", new Guid("00bb2e85-4474-414c-bed4-6d4fef568ec4") },
                    { 17, "KsPermission", "Management_User_RemoveClaim", new Guid("00bb2e85-4474-414c-bed4-6d4fef568ec4") },
                    { 18, "KsPermission", "Management_Role_AddClaim", new Guid("7255e4e1-bcbf-4c1b-89d4-15f3343dc572") },
                    { 19, "KsPermission", "Management_Role_AddUser", new Guid("7255e4e1-bcbf-4c1b-89d4-15f3343dc572") },
                    { 20, "KsPermission", "Management_Role_ClaimList", new Guid("7255e4e1-bcbf-4c1b-89d4-15f3343dc572") },
                    { 21, "KsPermission", "Management_Role_Delete", new Guid("7255e4e1-bcbf-4c1b-89d4-15f3343dc572") },
                    { 22, "KsPermission", "Management_Role_Create", new Guid("7255e4e1-bcbf-4c1b-89d4-15f3343dc572") },
                    { 23, "KsPermission", "Management_Role_Edit", new Guid("7255e4e1-bcbf-4c1b-89d4-15f3343dc572") },
                    { 24, "KsPermission", "Management_Role_List", new Guid("7255e4e1-bcbf-4c1b-89d4-15f3343dc572") },
                    { 25, "KsPermission", "Management_Role_RemoveClaim", new Guid("7255e4e1-bcbf-4c1b-89d4-15f3343dc572") },
                    { 26, "KsPermission", "Management_Role_RemoveUser", new Guid("7255e4e1-bcbf-4c1b-89d4-15f3343dc572") },
                    { 27, "KsPermission", "Management_Role_UserList", new Guid("7255e4e1-bcbf-4c1b-89d4-15f3343dc572") },
                    { 28, "KsPermission", "Management_ApplicationSetting_List", new Guid("00bb2e85-4474-414c-bed4-6d4fef568ec4") },
                    { 29, "KsPermission", "Management_ApplicationSetting_Edit", new Guid("00bb2e85-4474-414c-bed4-6d4fef568ec4") },
                    { 30, "KsPermission", "Management_ApplicationSetting_Delete", new Guid("00bb2e85-4474-414c-bed4-6d4fef568ec4") },
                    { 31, "KsPermission", "Management_ApplicationSetting_Create", new Guid("00bb2e85-4474-414c-bed4-6d4fef568ec4") },
                    { 32, "KsPermission", "Management_ApplicationSettingCategory_List", new Guid("00bb2e85-4474-414c-bed4-6d4fef568ec4") },
                    { 33, "KsPermission", "Management_ApplicationSettingCategory_Edit", new Guid("00bb2e85-4474-414c-bed4-6d4fef568ec4") },
                    { 34, "KsPermission", "Management_ApplicationSettingCategory_Delete", new Guid("00bb2e85-4474-414c-bed4-6d4fef568ec4") },
                    { 35, "KsPermission", "Management_ApplicationSettingCategory_Create", new Guid("00bb2e85-4474-414c-bed4-6d4fef568ec4") },
                    { 36, "KsPermission", "Report_LoginAuditLog_List", new Guid("00bb2e85-4474-414c-bed4-6d4fef568ec4") },
                    { 37, "KsPermission", "Management_Excel_Export", new Guid("00bb2e85-4474-414c-bed4-6d4fef568ec4") },
                    { 38, "KsPermission", "Management_EmailTemplate_List", new Guid("00bb2e85-4474-414c-bed4-6d4fef568ec4") },
                    { 39, "KsPermission", "Management_EmailTemplate_Edit", new Guid("00bb2e85-4474-414c-bed4-6d4fef568ec4") },
                    { 40, "KsPermission", "Report_EmailNotification_List", new Guid("00bb2e85-4474-414c-bed4-6d4fef568ec4") },
                    { 41, "KsPermission", "Report_EmailNotification_Send", new Guid("00bb2e85-4474-414c-bed4-6d4fef568ec4") },
                    { 42, "KsPermission", "Management_User_Role", new Guid("00bb2e85-4474-414c-bed4-6d4fef568ec4") },
                    { 43, "KsPermission", "Management_Menu_List", new Guid("00bb2e85-4474-414c-bed4-6d4fef568ec4") },
                    { 44, "KsPermission", "Management_Menu_Edit", new Guid("00bb2e85-4474-414c-bed4-6d4fef568ec4") },
                    { 45, "KsPermission", "Management_Language_List", new Guid("00bb2e85-4474-414c-bed4-6d4fef568ec4") },
                    { 46, "KsPermission", "Management_Language_Edit", new Guid("00bb2e85-4474-414c-bed4-6d4fef568ec4") },
                    { 47, "KsPermission", "Management_ApplicationSettingCategory_List", new Guid("09c0b51b-f9ac-48a0-8a7c-b5b6b987a4c6") },
                    { 48, "KsPermission", "Management_ApplicationSetting_List", new Guid("09c0b51b-f9ac-48a0-8a7c-b5b6b987a4c6") },
                    { 49, "KsPermission", "Management_EmailTemplate_List", new Guid("09c0b51b-f9ac-48a0-8a7c-b5b6b987a4c6") },
                    { 50, "KsPermission", "Management_Excel_Export", new Guid("09c0b51b-f9ac-48a0-8a7c-b5b6b987a4c6") },
                    { 51, "KsPermission", "Management_Language_List", new Guid("09c0b51b-f9ac-48a0-8a7c-b5b6b987a4c6") },
                    { 52, "KsPermission", "Management_Menu_List", new Guid("09c0b51b-f9ac-48a0-8a7c-b5b6b987a4c6") },
                    { 53, "KsPermission", "Management_Role_ClaimList", new Guid("09c0b51b-f9ac-48a0-8a7c-b5b6b987a4c6") },
                    { 54, "KsPermission", "Management_Role_List", new Guid("09c0b51b-f9ac-48a0-8a7c-b5b6b987a4c6") },
                    { 55, "KsPermission", "Management_Role_UserList", new Guid("09c0b51b-f9ac-48a0-8a7c-b5b6b987a4c6") },
                    { 56, "KsPermission", "Management_User_ClaimList", new Guid("09c0b51b-f9ac-48a0-8a7c-b5b6b987a4c6") },
                    { 57, "KsPermission", "Management_User_List", new Guid("09c0b51b-f9ac-48a0-8a7c-b5b6b987a4c6") },
                    { 58, "KsPermission", "Report_EmailNotification_List", new Guid("09c0b51b-f9ac-48a0-8a7c-b5b6b987a4c6") },
                    { 59, "KsPermission", "Report_LoginAuditLog_List", new Guid("09c0b51b-f9ac-48a0-8a7c-b5b6b987a4c6") },
                    { 60, "KsPermission", "Management_Pdf_Export", new Guid("09c0b51b-f9ac-48a0-8a7c-b5b6b987a4c6") },
                    { 61, "KsPermission", "Management_Pdf_Export", new Guid("00bb2e85-4474-414c-bed4-6d4fef568ec4") },
                    { 62, "KsPermission", "Management_Pdf_Export", new Guid("7255e4e1-bcbf-4c1b-89d4-15f3343dc572") }
                });

            migrationBuilder.InsertData(
                table: "RoleTranslation",
                columns: new[] { "Id", "Description", "DisplayText", "Language", "ReferenceId" },
                values: new object[,]
                {
                    { new Guid("03875513-2d3d-48ee-a4d8-927ca716ba00"), "Guest Description", "Guest", 1, new Guid("09c0b51b-f9ac-48a0-8a7c-b5b6b987a4c6") },
                    { new Guid("40434644-6dde-464d-b65b-1a5e5ac9e5d6"), "وصف المسؤول", "مشرف", 3, new Guid("00bb2e85-4474-414c-bed4-6d4fef568ec4") },
                    { new Guid("83ad9a8b-a2ea-4de0-b9dd-01af7aafaac7"), "Ziyaretçi Açıklaması", "Ziyaretçi", 2, new Guid("09c0b51b-f9ac-48a0-8a7c-b5b6b987a4c6") },
                    { new Guid("a5111850-8d65-48ad-a3d8-077213fde69a"), "وصف المستخدم القوي", "مستخدم متميز", 3, new Guid("7255e4e1-bcbf-4c1b-89d4-15f3343dc572") },
                    { new Guid("a7465df1-896b-4c73-bc67-81bd50b191ca"), "وصف الضيف", "زائر", 3, new Guid("09c0b51b-f9ac-48a0-8a7c-b5b6b987a4c6") },
                    { new Guid("c3877e22-8fad-4cd2-8480-f629f0d481a2"), "Admin Description", "Admin", 1, new Guid("00bb2e85-4474-414c-bed4-6d4fef568ec4") },
                    { new Guid("e64211a1-3e2a-4906-b4f7-f1864b9ac777"), "Güçlü Kullanıcı Açıklaması", "Güçlü Kullanıcı", 2, new Guid("7255e4e1-bcbf-4c1b-89d4-15f3343dc572") },
                    { new Guid("f376d12e-3eca-4255-8592-41946e808b3c"), "Yönetici Açıklaması", "Yönetici", 2, new Guid("00bb2e85-4474-414c-bed4-6d4fef568ec4") },
                    { new Guid("f9ca3efb-8334-46a6-807f-c2881990c273"), "Power User Description", "Power User", 1, new Guid("7255e4e1-bcbf-4c1b-89d4-15f3343dc572") }
                });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("00bb2e85-4474-414c-bed4-6d4fef568ec4"), new Guid("e0cb33f3-591a-4a25-aaba-bd05f796b5fb") });

            migrationBuilder.InsertData(
                table: "MenuTranslation",
                columns: new[] { "Id", "DisplayText", "Language", "ReferenceId" },
                values: new object[,]
                {
                    { 4, "Users", 1, 2 },
                    { 5, "Kullanıcılar", 2, 2 },
                    { 6, "المستخدمون", 3, 2 },
                    { 7, "User Claim Management", 1, 3 },
                    { 8, "Kullanıcı Yetki Yönetimi", 2, 3 },
                    { 9, "إدارة مطالبة المستخدم", 3, 3 },
                    { 13, "Roles", 1, 5 },
                    { 14, "Roller", 2, 5 },
                    { 15, "الأدوار", 3, 5 },
                    { 16, "Role Claim Management", 1, 6 },
                    { 17, "Rol Yetki Yönetimi", 2, 6 },
                    { 18, "إدارة مطالبات الدور", 3, 6 },
                    { 22, "Application Settings", 1, 8 },
                    { 23, "Uygulama Ayarları", 2, 8 },
                    { 24, "إعدادات التطبيق", 3, 8 },
                    { 25, "Application Setting Categories", 1, 9 },
                    { 26, "Uygulama Ayar Kategorileri", 2, 9 },
                    { 27, "فئات إعداد التطبيق", 3, 9 },
                    { 31, "Login Audit Log", 1, 11 },
                    { 32, "Giriş Denetim Günlüğü", 2, 11 },
                    { 33, "سجل تدقيق تسجيل الدخول", 3, 11 },
                    { 34, "Email Templates", 1, 12 },
                    { 35, "E-posta Şablonları", 2, 12 },
                    { 36, "قوالب البريد الإلكتروني", 3, 12 },
                    { 37, "Email Notifications", 1, 13 },
                    { 38, "E-posta Bildirimleri", 2, 13 },
                    { 39, "اشعارات البريد الالكتروني", 3, 13 },
                    { 40, "Menu Order Management", 1, 14 },
                    { 41, "Menü Sıralama Yönetimi", 2, 14 },
                    { 42, "إدارة فرز القائمة", 3, 14 },
                    { 43, "Languages", 1, 15 },
                    { 44, "Diller", 2, 15 },
                    { 45, "اللغة", 3, 15 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationSetting_CategoryId",
                table: "ApplicationSetting",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationSetting_Key",
                table: "ApplicationSetting",
                column: "Key",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationSettingCategory_Name",
                table: "ApplicationSettingCategory",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmailTemplateTranslation_ReferenceId",
                table: "EmailTemplateTranslation",
                column: "ReferenceId");

            migrationBuilder.CreateIndex(
                name: "IX_Menu_ParentId",
                table: "Menu",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuTranslation_ReferenceId",
                table: "MenuTranslation",
                column: "ReferenceId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "Role",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaim_RoleId",
                table: "RoleClaim",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleTranslation_ReferenceId",
                table: "RoleTranslation",
                column: "ReferenceId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "User",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "User",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaim_UserId",
                table: "UserClaim",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserConfirmationHistory_UserId",
                table: "UserConfirmationHistory",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogin_UserId",
                table: "UserLogin",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPasswordHistory_UserId",
                table: "UserPasswordHistory",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_RoleId",
                table: "UserRole",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationSetting");

            migrationBuilder.DropTable(
                name: "AuditLog");

            migrationBuilder.DropTable(
                name: "EmailNotification");

            migrationBuilder.DropTable(
                name: "EmailTemplateTranslation");

            migrationBuilder.DropTable(
                name: "EventLog");

            migrationBuilder.DropTable(
                name: "Language");

            migrationBuilder.DropTable(
                name: "LoginAuditLog");

            migrationBuilder.DropTable(
                name: "MenuTranslation");

            migrationBuilder.DropTable(
                name: "RoleClaim");

            migrationBuilder.DropTable(
                name: "RoleTranslation");

            migrationBuilder.DropTable(
                name: "TestEncryptionEntity");

            migrationBuilder.DropTable(
                name: "UserClaim");

            migrationBuilder.DropTable(
                name: "UserConfirmationHistory");

            migrationBuilder.DropTable(
                name: "UserLogin");

            migrationBuilder.DropTable(
                name: "UserPasswordHistory");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "UserToken");

            migrationBuilder.DropTable(
                name: "ApplicationSettingCategory");

            migrationBuilder.DropTable(
                name: "EmailTemplate");

            migrationBuilder.DropTable(
                name: "Menu");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
