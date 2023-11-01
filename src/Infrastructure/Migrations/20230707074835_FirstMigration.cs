using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace hrOT.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AllowanceTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsPayTax = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EligibilityCriteria = table.Column<string>(name: "Eligibility_Criteria", type: "nvarchar(max)", nullable: false),
                    Requirements = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllowanceTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AnnualWorkingDays",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Day = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Coefficients = table.Column<double>(type: "float", nullable: false),
                    TypeDate = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnnualWorkingDays", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Fullname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthDay = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Banks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BankName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeviceCodes",
                columns: table => new
                {
                    UserCode = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DeviceCode = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    SubjectId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    SessionId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ClientId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Expiration = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Data = table.Column<string>(type: "nvarchar(max)", maxLength: 50000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceCodes", x => x.UserCode);
                });

            migrationBuilder.CreateTable(
                name: "Exchanges",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MucQuyDoi = table.Column<double>(name: "Muc_Quy_Doi", type: "float", nullable: true),
                    GiamTru = table.Column<double>(name: "Giam_Tru", type: "float", nullable: true),
                    ThueSuat = table.Column<double>(name: "Thue_Suat", type: "float", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exchanges", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Keys",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Use = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Algorithm = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsX509Certificate = table.Column<bool>(type: "bit", nullable: false),
                    DataProtected = table.Column<bool>(type: "bit", nullable: false),
                    Data = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Keys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LeaveTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Benefit = table.Column<int>(type: "int", nullable: false),
                    IsReward = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersistedGrants",
                columns: table => new
                {
                    Key = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SubjectId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    SessionId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ClientId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Expiration = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ConsumedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Data = table.Column<string>(type: "nvarchar(max)", maxLength: 50000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersistedGrants", x => x.Key);
                });

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SkillName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SkillDescription = table.Column<string>(name: "Skill_Description", type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaxInComes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Mucchiuthue = table.Column<double>(name: "Muc_chiu_thue", type: "float", nullable: true),
                    Thuesuat = table.Column<double>(name: "Thue_suat", type: "float", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxInComes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TodoLists",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ColourCode = table.Column<string>(name: "Colour_Code", type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TodoLists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Positions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DepartmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Positions_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TodoItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ListId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    Reminder = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Done = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TodoItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TodoItems_TodoLists_ListId",
                        column: x => x.ListId,
                        principalTable: "TodoLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PositionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CitizenIdentificationNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDateCIN = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PlaceForCIN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhotoCIOnTheFront = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhotoCIOnTheBack = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CVPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    District = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Province = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employees_Positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Positions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BankAccounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BankId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BankAccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BankAccountName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Selected = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BankAccounts_Banks_BankId",
                        column: x => x.BankId,
                        principalTable: "Banks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BankAccounts_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Degrees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Degrees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Degrees_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeContracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    File = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Job = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Salary = table.Column<double>(type: "float", nullable: true),
                    CustomSalary = table.Column<double>(type: "float", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true),
                    SalaryType = table.Column<int>(type: "int", nullable: true),
                    ContractType = table.Column<int>(type: "int", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeContracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeContracts_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Experiences",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameProject = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TeamSize = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TechStack = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Experiences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Experiences_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Families",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Relationship = table.Column<int>(type: "int", nullable: false),
                    IsDependent = table.Column<bool>(type: "bit", nullable: false),
                    CitizenIdentificationNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDateCI = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PlaceForCI = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhotoCIOnTheFront = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhotoCIOnTheBack = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Families", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Families_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LeaveLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LeaveHours = table.Column<double>(type: "float", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: true),
                    LeaveTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeaveLogs_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LeaveLogs_LeaveTypes_LeaveTypeId",
                        column: x => x.LeaveTypeId,
                        principalTable: "LeaveTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OvertimeLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalHours = table.Column<double>(type: "float", nullable: false),
                    Coefficients = table.Column<double>(type: "float", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OvertimeLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OvertimeLogs_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Skill_Employees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SkillId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Level = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skill_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Skill_Employees_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Skill_Employees_Skills_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TimeAttendanceLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ducation = table.Column<double>(type: "float", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeAttendanceLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TimeAttendanceLogs_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Allowances",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeContractId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AllowanceTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Allowances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Allowances_AllowanceTypes_AllowanceTypeId",
                        column: x => x.AllowanceTypeId,
                        principalTable: "AllowanceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Allowances_EmployeeContracts_EmployeeContractId",
                        column: x => x.EmployeeContractId,
                        principalTable: "EmployeeContracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PaySlips",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeContractId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StandardWorkHours = table.Column<int>(name: "Standard_Work_Hours", type: "int", nullable: true),
                    ActualWorkHours = table.Column<double>(name: "Actual_Work_Hours", type: "float", nullable: true),
                    OtHours = table.Column<int>(name: "Ot_Hours", type: "int", nullable: true),
                    LeaveHours = table.Column<int>(name: "Leave_Hours", type: "int", nullable: true),
                    Salary = table.Column<double>(type: "float", nullable: true),
                    BHXHEmp = table.Column<double>(name: "BHXH_Emp", type: "float", nullable: true),
                    BHYTEmp = table.Column<double>(name: "BHYT_Emp", type: "float", nullable: true),
                    BHTNEmp = table.Column<double>(name: "BHTN_Emp", type: "float", nullable: true),
                    BHXHComp = table.Column<double>(name: "BHXH_Comp", type: "float", nullable: true),
                    BHYTComp = table.Column<double>(name: "BHYT_Comp", type: "float", nullable: true),
                    BHTNComp = table.Column<double>(name: "BHTN_Comp", type: "float", nullable: true),
                    TaxInCome = table.Column<double>(name: "Tax_In_Come", type: "float", nullable: true),
                    Bonus = table.Column<double>(type: "float", nullable: true),
                    Deduction = table.Column<double>(type: "float", nullable: true),
                    TotalAllowance = table.Column<double>(name: "Total_Allowance", type: "float", nullable: true),
                    FinalSalary = table.Column<double>(name: "Final_Salary", type: "float", nullable: true),
                    CompanyPaid = table.Column<double>(name: "Company_Paid", type: "float", nullable: true),
                    Paiddate = table.Column<DateTime>(name: "Paid_date", type: "datetime2", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankAcountName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankAcountNumber = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaySlips", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaySlips_EmployeeContracts_EmployeeContractId",
                        column: x => x.EmployeeContractId,
                        principalTable: "EmployeeContracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DetailTaxIncomes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PaySlipId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: true),
                    Payment = table.Column<double>(type: "float", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetailTaxIncomes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetailTaxIncomes_PaySlips_PaySlipId",
                        column: x => x.PaySlipId,
                        principalTable: "PaySlips",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AllowanceTypes",
                columns: new[] { "Id", "Created", "CreatedBy", "Description", "Eligibility_Criteria", "IsDeleted", "IsPayTax", "LastModified", "LastModifiedBy", "Name", "Requirements" },
                values: new object[,]
                {
                    { new Guid("123e4567-e89b-12d3-a456-426655440000"), new DateTime(2023, 6, 28, 10, 0, 0, 0, DateTimeKind.Unspecified), "Admin", "Trợ cấp được cấp cho nhân viên dựa trên thâm niên làm việc trong công ty.", "Nhân viên có thâm niên làm việc từ 5 năm trở lên.", false, true, new DateTime(2023, 6, 28, 10, 30, 0, 0, DateTimeKind.Unspecified), "Admin", "Trợ cấp thâm niên", "Không yêu cầu." },
                    { new Guid("223e4567-e89b-12d3-a456-426655440000"), new DateTime(2023, 6, 28, 11, 0, 0, 0, DateTimeKind.Unspecified), "Admin", "Trợ cấp được cấp cho nhân viên để chi trả các chi phí đi lại.", "Áp dụng cho tất cả nhân viên công ty.", false, false, new DateTime(2023, 6, 28, 11, 30, 0, 0, DateTimeKind.Unspecified), "Admin", "Trợ cấp đi lại", "Không yêu cầu." },
                    { new Guid("323e4567-e89b-12d3-a456-426655440000"), new DateTime(2023, 6, 28, 12, 0, 0, 0, DateTimeKind.Unspecified), "Admin", "Trợ cấp được cấp cho nhân viên để trang trải chi phí ăn trưa hàng ngày.", "Áp dụng cho tất cả nhân viên công ty.", false, true, new DateTime(2023, 6, 28, 12, 30, 0, 0, DateTimeKind.Unspecified), "Admin", "Trợ cấp ăn trưa", "Không yêu cầu." },
                    { new Guid("423e4567-e89b-12d3-a456-426655440000"), new DateTime(2023, 6, 28, 13, 0, 0, 0, DateTimeKind.Unspecified), "Admin", "Trợ cấp được cấp cho nhân viên khi làm thêm giờ làm việc.", "Áp dụng cho nhân viên làm thêm giờ.", false, true, new DateTime(2023, 6, 28, 13, 30, 0, 0, DateTimeKind.Unspecified), "Admin", "Trợ cấp làm thêm giờ", "Yêu cầu có xác nhận của quản lý." },
                    { new Guid("523e4567-e89b-12d3-a456-426655440000"), new DateTime(2023, 6, 28, 14, 0, 0, 0, DateTimeKind.Unspecified), "Admin", "Trợ cấp được cấp cho nhân viên để hỗ trợ chi phí chăm sóc sức khỏe.", "Áp dụng cho tất cả nhân viên công ty.", false, true, new DateTime(2023, 6, 28, 14, 30, 0, 0, DateTimeKind.Unspecified), "Admin", "Trợ cấp sức khỏe", "Không yêu cầu." }
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Discriminator", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "37dde3f4-d0c9-4477-97d6-ff29f677dccc", null, "Quản lí hệ thống", "AppIdentityRole", "Manager", "MANAGER" },
                    { "b9cf3487-3d04-4cbf-85b7-e33360566485", null, "Nhân viên công ty", "AppIdentityRole", "Employee", "EMPLOYEE" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "BirthDay", "ConcurrencyStamp", "Email", "EmailConfirmed", "Fullname", "Image", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "871a809a-b3fa-495b-9cc2-c5d738a866cf", 0, new DateTime(2002, 6, 20, 8, 30, 56, 0, DateTimeKind.Unspecified), "445607b7-57f3-4092-9129-c8becc104929", "tekato2002@gmail.com", false, "string", null, true, null, "TEKATO2002@GMAIL.COM", "LEWIS", "AQAAAAIAAYagAAAAELBNBVEaKLPiH0GMl0YJtU00Ss5zZODHsIRzLlxZlgsxD1ZOy8YBBpvTdyxPsp2+AQ==", "0899248435", false, "FHSBRSP7Q6SW6JWBVKCFBC6LKFR4MAR7", false, "lewis" },
                    { "fe30e976-2640-4d35-8334-88e7c3b1eac1", 0, new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "40495f9c-e853-41e8-8c5b-6b3c93d3791b", "test@gmail.com", true, "Lewis", "TESTIMAGE", true, new DateTimeOffset(new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)), "test@gmail.com", "test", "AQAAAAIAAYagAAAAEFNwXlIXp0mbDE5k1gIQdlbAczn8BwINQnF5S0qULxDK/6luT/bumpD+HFOXM0k59A==", "123456789", true, "VEPOTJNXQCZMK3J7R27HMLXD64T72GU6", false, "admin" }
                });

            migrationBuilder.InsertData(
                table: "Banks",
                columns: new[] { "Id", "BankName", "Created", "CreatedBy", "Description", "IsDeleted", "LastModified", "LastModifiedBy" },
                values: new object[,]
                {
                    { new Guid("123e4567-e89b-12d3-a456-426655440001"), "Ngân hàng TMCP Á Châu", new DateTime(2023, 6, 28, 10, 0, 0, 0, DateTimeKind.Unspecified), "Admin", "Asia Commercial Joint Stock Bank", false, null, null },
                    { new Guid("223e4567-e89b-12d3-a456-426655440002"), "Ngân hàng TMCP Tiên Phong", new DateTime(2023, 6, 28, 11, 0, 0, 0, DateTimeKind.Unspecified), "Admin", "Tien Phong Commercial Joint Stock Bank", false, null, null },
                    { new Guid("323e4567-e89b-12d3-a456-426655440003"), "Ngân hàng TMCP Đông Á", new DateTime(2023, 6, 28, 12, 0, 0, 0, DateTimeKind.Unspecified), "Admin", "Dong A Commercial Joint Stock Bank", false, null, null },
                    { new Guid("423e4567-e89b-12d3-a456-426655440004"), "Ngân Hàng TMCP Đông Nam Á", new DateTime(2023, 6, 28, 13, 0, 0, 0, DateTimeKind.Unspecified), "Admin", "Southeast Asia Commercial Joint Stock Bank", false, null, null },
                    { new Guid("523e4567-e89b-12d3-a456-426655440005"), "Ngân hàng TMCP An Bình", new DateTime(2023, 6, 28, 14, 0, 0, 0, DateTimeKind.Unspecified), "Admin", "An Binh Commercial Joint Stock Bank", false, null, null },
                    { new Guid("623e4567-e89b-12d3-a456-426655440006"), "Ngân hàng TMCP Bắc Á", new DateTime(2023, 6, 28, 15, 0, 0, 0, DateTimeKind.Unspecified), "Admin", "Bac A Commercial Joint Stock Bank", false, null, null },
                    { new Guid("723e4567-e89b-12d3-a456-426655440007"), "Ngân hàng TMCP Bản Việt", new DateTime(2023, 6, 28, 16, 0, 0, 0, DateTimeKind.Unspecified), "Admin", "Vietcapital Commercial Joint Stock Bank", false, null, null },
                    { new Guid("823e4567-e89b-12d3-a456-426655440008"), "Ngân hàng TMCP Hàng hải Việt Nam", new DateTime(2023, 6, 28, 17, 0, 0, 0, DateTimeKind.Unspecified), "Admin", "Vietnam Maritime Joint – Stock Commercial Bank", false, null, null },
                    { new Guid("923e4567-e89b-12d3-a456-426655440009"), "Ngân hàng TMCP Hàng hóa", new DateTime(2023, 6, 28, 18, 0, 0, 0, DateTimeKind.Unspecified), "Admin", "Vietnam Bank for Industry and Trade", false, null, null },
                    { new Guid("a23e4567-e89b-12d3-a456-426655440010"), "Ngân hàng TMCP Nam Á", new DateTime(2023, 6, 28, 19, 0, 0, 0, DateTimeKind.Unspecified), "Admin", "Nam A Commercial Joint Stock Bank", false, null, null }
                });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "Created", "CreatedBy", "Description", "IsDeleted", "LastModified", "LastModifiedBy", "Name" },
                values: new object[,]
                {
                    { new Guid("123e4567-e89b-12d3-a456-426655440000"), new DateTime(2023, 6, 28, 10, 0, 0, 0, DateTimeKind.Unspecified), "Admin", "Bộ phận chịu trách nhiệm phát triển các ứng dụng và giải pháp công nghệ mới.", false, new DateTime(2023, 6, 28, 10, 30, 0, 0, DateTimeKind.Unspecified), "Admin", "Bộ phận Phát triển" },
                    { new Guid("223e4567-e89b-12d3-a456-426655440000"), new DateTime(2023, 6, 28, 11, 0, 0, 0, DateTimeKind.Unspecified), "Admin", "Bộ phận cung cấp hỗ trợ kỹ thuật và giải quyết sự cố cho khách hàng.", false, new DateTime(2023, 6, 28, 11, 30, 0, 0, DateTimeKind.Unspecified), "Admin", "Bộ phận Hỗ trợ kỹ thuật" },
                    { new Guid("323e4567-e89b-12d3-a456-426655440000"), new DateTime(2023, 6, 28, 12, 0, 0, 0, DateTimeKind.Unspecified), "Admin", "Bộ phận quản lý và điều phối các dự án công nghệ trong công ty.", false, new DateTime(2023, 6, 28, 12, 30, 0, 0, DateTimeKind.Unspecified), "Admin", "Bộ phận Quản lý Dự án" },
                    { new Guid("423e4567-e89b-12d3-a456-426655440000"), new DateTime(2023, 6, 28, 13, 0, 0, 0, DateTimeKind.Unspecified), "Admin", "Bộ phận chịu trách nhiệm quản lý và duy trì hệ thống mạng trong công ty.", false, new DateTime(2023, 6, 28, 13, 30, 0, 0, DateTimeKind.Unspecified), "Admin", "Bộ phận Kỹ thuật Mạng" },
                    { new Guid("523e4567-e89b-12d3-a456-426655440000"), new DateTime(2023, 6, 28, 14, 0, 0, 0, DateTimeKind.Unspecified), "Admin", "Bộ phận thực hiện phân tích và xử lý dữ liệu để đưa ra thông tin và thông báo phù hợp.", false, new DateTime(2023, 6, 28, 14, 30, 0, 0, DateTimeKind.Unspecified), "Admin", "Bộ phận Phân tích Dữ liệu" }
                });

            migrationBuilder.InsertData(
                table: "Exchanges",
                columns: new[] { "Id", "Created", "CreatedBy", "Giam_Tru", "IsDeleted", "LastModified", "LastModifiedBy", "Muc_Quy_Doi", "Thue_Suat" },
                values: new object[,]
                {
                    { new Guid("08d5ef29-44a5-47d0-9a85-28d9d0dc30f3"), new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "test", 750000.0, false, new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "test", 16050000.0, 0.84999999999999998 },
                    { new Guid("3a1d42d9-9ee7-4a4c-b283-6e8f9126e44a"), new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "test", 250000.0, false, new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "test", 9250000.0, 0.90000000000000002 },
                    { new Guid("5ef5f6be-ef53-4af2-8b18-78fc6b8a295f"), new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "test", 5850000.0, false, new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "test", 61850000.0, 0.69999999999999996 },
                    { new Guid("7f1b1d11-3070-4b4b-96db-801d448a8920"), new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "test", 9850000.0, false, new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "test", 1.7976931348623157E+308, 0.65000000000000002 },
                    { new Guid("9218741c-99f6-40a2-87f4-d4baf4c9e15d"), new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "test", 1650000.0, false, new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "test", 27250000.0, 0.80000000000000004 },
                    { new Guid("d9f3a521-36e4-4a5c-9a89-0c733e92e85b"), new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "test", 0.0, false, new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "test", 4750000.0, 0.94999999999999996 },
                    { new Guid("e28a08ad-2b30-4df5-bc95-684d56ad8a56"), new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "test", 3250000.0, false, new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "test", 42250000.0, 0.75 }
                });

            migrationBuilder.InsertData(
                table: "LeaveTypes",
                columns: new[] { "Id", "Benefit", "Created", "CreatedBy", "Description", "IsDeleted", "IsReward", "LastModified", "LastModifiedBy", "Name" },
                values: new object[,]
                {
                    { new Guid("0d87bff5-e9ca-4e8a-af18-6cc2e804e57d"), 10, new DateTime(2023, 7, 7, 14, 48, 35, 70, DateTimeKind.Local).AddTicks(6323), null, "Nghỉ mà nhân viên nam được nghỉ để hỗ trợ đối tác trong thời kỳ mang thai và sau sinh.", false, true, null, null, "Nghỉ phép cha" },
                    { new Guid("1611aee4-0a90-464d-927c-462f51d19120"), 10, new DateTime(2023, 7, 7, 14, 48, 35, 70, DateTimeKind.Local).AddTicks(6315), null, "Nghỉ mà nhân viên nữ được nghỉ trong thời kỳ mang thai và sau sinh.", false, true, null, null, "Nghỉ thai sản" },
                    { new Guid("18167791-8248-4924-a1c6-90858852f905"), 2, new DateTime(2023, 7, 7, 14, 48, 35, 70, DateTimeKind.Local).AddTicks(6325), null, "Nghỉ dành cho nhân viên để nghỉ ngơi, thư giãn và có thời gian cá nhân.", false, false, null, null, "Nghỉ phép hàng năm" },
                    { new Guid("51c55016-58c4-49d3-b0b2-2d7e0a9cd7d7"), 2, new DateTime(2023, 7, 7, 14, 48, 35, 70, DateTimeKind.Local).AddTicks(6328), null, "Nghỉ khi nhân viên bị ốm hoặc cần chăm sóc y tế.", false, false, null, null, "Nghỉ ốm" }
                });

            migrationBuilder.InsertData(
                table: "Skills",
                columns: new[] { "Id", "Created", "CreatedBy", "IsDeleted", "LastModified", "LastModifiedBy", "SkillName", "Skill_Description" },
                values: new object[,]
                {
                    { new Guid("723e4567-e89b-12d3-a456-426655440051"), new DateTime(2023, 7, 7, 14, 48, 35, 70, DateTimeKind.Local).AddTicks(6359), "Admin", false, null, null, "Lập trình Java", "Mô tả kỹ năng lập trình Java" },
                    { new Guid("723e4567-e89b-12d3-a456-426655440052"), new DateTime(2023, 7, 7, 14, 48, 35, 70, DateTimeKind.Local).AddTicks(6363), "Admin", false, null, null, "Phân tích yêu cầu", "Mô tả kỹ năng phân tích yêu cầu" },
                    { new Guid("723e4567-e89b-12d3-a456-426655440053"), new DateTime(2023, 7, 7, 14, 48, 35, 70, DateTimeKind.Local).AddTicks(6366), "Admin", false, null, null, "Thử nghiệm phần mềm", "Mô tả kỹ năng thử nghiệm phần mềm" },
                    { new Guid("723e4567-e89b-12d3-a456-426655440054"), new DateTime(2023, 7, 7, 14, 48, 35, 70, DateTimeKind.Local).AddTicks(6370), "Admin", false, null, null, "Quản lý dự án", "Mô tả kỹ năng quản lý dự án" },
                    { new Guid("723e4567-e89b-12d3-a456-426655440055"), new DateTime(2023, 7, 7, 14, 48, 35, 70, DateTimeKind.Local).AddTicks(6373), "Admin", false, null, null, "Công nghệ web", "Mô tả kỹ năng công nghệ web" }
                });

            migrationBuilder.InsertData(
                table: "TaxInComes",
                columns: new[] { "Id", "Created", "CreatedBy", "IsDeleted", "LastModified", "LastModifiedBy", "Muc_chiu_thue", "Thue_suat" },
                values: new object[,]
                {
                    { new Guid("2d6a8e64-6130-456b-9c9d-95a1bc0a11fd"), new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "test", false, new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "test", 10000000.0, 0.10000000000000001 },
                    { new Guid("4ae0e892-5369-4ef1-9e37-5d4e0a9a3e2e"), new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "test", false, new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "test", 1.7976931348623157E+308, 0.34999999999999998 },
                    { new Guid("78a65c98-2d7a-4c57-98f0-81f5a870a915"), new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "test", false, new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "test", 80000000.0, 0.29999999999999999 },
                    { new Guid("a279788d-0fa2-4d9e-9e8e-5d689e853972"), new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "test", false, new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "test", 5000000.0, 0.050000000000000003 },
                    { new Guid("c0b17a9e-ee6f-4fe0-8e6f-33d5c63640c8"), new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "test", false, new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "test", 32000000.0, 0.20000000000000001 },
                    { new Guid("e582dd24-ec47-4c64-b0a7-6f8647b488a7"), new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "test", false, new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "test", 18000000.0, 0.14999999999999999 },
                    { new Guid("f0f3e78c-67c9-4e5e-a9fc-c8d2e7c1e5f5"), new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "test", false, new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "test", 52000000.0, 0.25 }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "37dde3f4-d0c9-4477-97d6-ff29f677dccc", "871a809a-b3fa-495b-9cc2-c5d738a866cf" });

            migrationBuilder.InsertData(
                table: "Positions",
                columns: new[] { "Id", "Created", "CreatedBy", "DepartmentId", "IsDeleted", "LastModified", "LastModifiedBy", "Name" },
                values: new object[,]
                {
                    { new Guid("123e4567-e89b-12d3-a456-426655440001"), new DateTime(2023, 6, 28, 10, 0, 0, 0, DateTimeKind.Unspecified), "Admin", new Guid("123e4567-e89b-12d3-a456-426655440000"), false, new DateTime(2023, 6, 28, 10, 30, 0, 0, DateTimeKind.Unspecified), "Admin", "Quản lý Dự án" },
                    { new Guid("223e4567-e89b-12d3-a456-426655440002"), new DateTime(2023, 6, 28, 11, 0, 0, 0, DateTimeKind.Unspecified), "Admin", new Guid("223e4567-e89b-12d3-a456-426655440000"), false, new DateTime(2023, 6, 28, 11, 30, 0, 0, DateTimeKind.Unspecified), "Admin", "Kỹ sư Phần mềm" },
                    { new Guid("323e4567-e89b-12d3-a456-426655440003"), new DateTime(2023, 6, 28, 12, 0, 0, 0, DateTimeKind.Unspecified), "Admin", new Guid("323e4567-e89b-12d3-a456-426655440000"), false, new DateTime(2023, 6, 28, 12, 30, 0, 0, DateTimeKind.Unspecified), "Admin", "Chuyên viên Hỗ trợ kỹ thuật" },
                    { new Guid("423e4567-e89b-12d3-a456-426655440004"), new DateTime(2023, 6, 28, 13, 0, 0, 0, DateTimeKind.Unspecified), "Admin", new Guid("423e4567-e89b-12d3-a456-426655440000"), false, new DateTime(2023, 6, 28, 13, 30, 0, 0, DateTimeKind.Unspecified), "Admin", "Chuyên gia Bảo mật" },
                    { new Guid("523e4567-e89b-12d3-a456-426655440005"), new DateTime(2023, 6, 28, 14, 0, 0, 0, DateTimeKind.Unspecified), "Admin", new Guid("523e4567-e89b-12d3-a456-426655440000"), false, new DateTime(2023, 6, 28, 14, 30, 0, 0, DateTimeKind.Unspecified), "Admin", "Trưởng Nhóm Phân tích Dữ liệu" }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Address", "ApplicationUserId", "CVPath", "CitizenIdentificationNumber", "Created", "CreatedBy", "CreatedDateCIN", "District", "IsDeleted", "LastModified", "LastModifiedBy", "PhotoCIOnTheBack", "PhotoCIOnTheFront", "PlaceForCIN", "PositionId", "Province" },
                values: new object[] { new Guid("123e4567-e89b-12d3-a456-426655440010"), null, "fe30e976-2640-4d35-8334-88e7c3b1eac1", null, null, new DateTime(2023, 6, 28, 15, 0, 0, 0, DateTimeKind.Unspecified), "Admin", null, null, false, null, null, null, null, null, new Guid("123e4567-e89b-12d3-a456-426655440001"), null });

            migrationBuilder.InsertData(
                table: "BankAccounts",
                columns: new[] { "Id", "BankAccountName", "BankAccountNumber", "BankId", "Created", "CreatedBy", "EmployeeId", "IsDeleted", "LastModified", "LastModifiedBy", "Selected" },
                values: new object[,]
                {
                    { new Guid("223e4567-e89b-12d3-a456-426655440001"), "John Doe", "0123456789", new Guid("123e4567-e89b-12d3-a456-426655440001"), new DateTime(2023, 7, 7, 14, 48, 35, 70, DateTimeKind.Local).AddTicks(5978), "Admin", new Guid("123e4567-e89b-12d3-a456-426655440010"), false, null, null, true },
                    { new Guid("223e4567-e89b-12d3-a456-426655440002"), "Jane Smith", "9876543210", new Guid("223e4567-e89b-12d3-a456-426655440002"), new DateTime(2023, 7, 7, 14, 48, 35, 70, DateTimeKind.Local).AddTicks(5984), "Admin", new Guid("123e4567-e89b-12d3-a456-426655440010"), false, null, null, false },
                    { new Guid("223e4567-e89b-12d3-a456-426655440003"), "Robert Johnson", "5555555555", new Guid("323e4567-e89b-12d3-a456-426655440003"), new DateTime(2023, 7, 7, 14, 48, 35, 70, DateTimeKind.Local).AddTicks(5990), "Admin", new Guid("123e4567-e89b-12d3-a456-426655440010"), false, null, null, false }
                });

            migrationBuilder.InsertData(
                table: "Degrees",
                columns: new[] { "Id", "Created", "CreatedBy", "EmployeeId", "IsDeleted", "LastModified", "LastModifiedBy", "Name", "Photo", "Status", "Type" },
                values: new object[,]
                {
                    { new Guid("223e4567-e89b-12d3-a456-426655440011"), new DateTime(2023, 7, 7, 14, 48, 35, 70, DateTimeKind.Local).AddTicks(6016), "Admin", new Guid("123e4567-e89b-12d3-a456-426655440010"), false, null, null, "Bằng Cử nhân Công nghệ thông tin", "test", 2, 3 },
                    { new Guid("223e4567-e89b-12d3-a456-426655440012"), new DateTime(2023, 7, 7, 14, 48, 35, 70, DateTimeKind.Local).AddTicks(6021), "Admin", new Guid("123e4567-e89b-12d3-a456-426655440010"), false, null, null, "Thạc sĩ Khoa học Máy tính", "test", 2, 3 },
                    { new Guid("223e4567-e89b-12d3-a456-426655440013"), new DateTime(2023, 7, 7, 14, 48, 35, 70, DateTimeKind.Local).AddTicks(6027), "Admin", new Guid("123e4567-e89b-12d3-a456-426655440010"), false, null, null, "Tiến sĩ Công nghệ Thông tin", "test", 2, 3 },
                    { new Guid("223e4567-e89b-12d3-a456-426655440014"), new DateTime(2023, 7, 7, 14, 48, 35, 70, DateTimeKind.Local).AddTicks(6031), "Admin", new Guid("123e4567-e89b-12d3-a456-426655440010"), false, null, null, "Bằng Cử nhân Khoa học Máy tính", "test", 2, 3 },
                    { new Guid("223e4567-e89b-12d3-a456-426655440015"), new DateTime(2023, 7, 7, 14, 48, 35, 70, DateTimeKind.Local).AddTicks(6036), "Admin", new Guid("123e4567-e89b-12d3-a456-426655440010"), false, null, null, "Tiến sĩ Khoa học Máy tính ứng dụng", "test", 2, 3 }
                });

            migrationBuilder.InsertData(
                table: "EmployeeContracts",
                columns: new[] { "Id", "ContractType", "Created", "CreatedBy", "CustomSalary", "EmployeeId", "EndDate", "File", "IsDeleted", "Job", "LastModified", "LastModifiedBy", "Salary", "SalaryType", "StartDate", "Status" },
                values: new object[] { new Guid("123e4567-e89b-12d3-a456-426655440020"), 2, new DateTime(2023, 6, 28, 16, 0, 0, 0, DateTimeKind.Unspecified), "Admin", null, new Guid("123e4567-e89b-12d3-a456-426655440010"), new DateTime(2023, 12, 31, 23, 59, 59, 0, DateTimeKind.Unspecified), null, false, "Nhân viên", null, null, 5000000.0, 1, new DateTime(2023, 6, 28, 16, 0, 0, 0, DateTimeKind.Unspecified), 1 });

            migrationBuilder.InsertData(
                table: "Experiences",
                columns: new[] { "Id", "Created", "CreatedBy", "Description", "EmployeeId", "EndDate", "IsDeleted", "LastModified", "LastModifiedBy", "NameProject", "StartDate", "Status", "TeamSize", "TechStack" },
                values: new object[,]
                {
                    { new Guid("323e4567-e89b-12d3-a456-426655440016"), new DateTime(2023, 7, 7, 14, 48, 35, 70, DateTimeKind.Local).AddTicks(6069), "Admin", "Dự án Quản lý hệ thống CRM cho khách hàng lớn", new Guid("123e4567-e89b-12d3-a456-426655440010"), new DateTime(2021, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, "Dự án Quản lý hệ thống CRM", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 5, ".NET, SQL Server, Angular" },
                    { new Guid("323e4567-e89b-12d3-a456-426655440017"), new DateTime(2023, 7, 7, 14, 48, 35, 70, DateTimeKind.Local).AddTicks(6075), "Admin", "Dự án Phát triển ứng dụng di động trên nền tảng iOS và Android", new Guid("123e4567-e89b-12d3-a456-426655440010"), new DateTime(2020, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, "Dự án Phát triển ứng dụng di động", new DateTime(2019, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 8, "Swift, Kotlin, Firebase" },
                    { new Guid("323e4567-e89b-12d3-a456-426655440018"), new DateTime(2023, 7, 7, 14, 48, 35, 70, DateTimeKind.Local).AddTicks(6081), "Admin", "Dự án Xây dựng hệ thống quản lý dữ liệu cho khách hàng trong lĩnh vực tài chính", new Guid("123e4567-e89b-12d3-a456-426655440010"), new DateTime(2019, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, "Dự án Xây dựng hệ thống quản lý dữ liệu", new DateTime(2018, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 4, "Java, Spring Boot, MySQL" },
                    { new Guid("323e4567-e89b-12d3-a456-426655440019"), new DateTime(2023, 7, 7, 14, 48, 35, 70, DateTimeKind.Local).AddTicks(6087), "Admin", "Dự án Migrating hệ thống cũ sang môi trường mới", new Guid("123e4567-e89b-12d3-a456-426655440010"), new DateTime(2018, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, "Dự án Migrating hệ thống cũ", new DateTime(2017, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 3, "ASP.NET, SQL Server, Azure" },
                    { new Guid("323e4567-e89b-12d3-a456-426655440020"), new DateTime(2023, 7, 7, 14, 48, 35, 70, DateTimeKind.Local).AddTicks(6092), "Admin", "Dự án Phát triển ứng dụng web sử dụng công nghệ mới", new Guid("123e4567-e89b-12d3-a456-426655440010"), new DateTime(2017, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, "Dự án Phát triển ứng dụng web", new DateTime(2016, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 6, "React, Node.js, MongoDB" }
                });

            migrationBuilder.InsertData(
                table: "Families",
                columns: new[] { "Id", "CitizenIdentificationNumber", "Created", "CreatedBy", "CreatedDateCI", "DateOfBirth", "EmployeeId", "IsDeleted", "IsDependent", "LastModified", "LastModifiedBy", "Name", "PhotoCIOnTheBack", "PhotoCIOnTheFront", "PlaceForCI", "Relationship" },
                values: new object[,]
                {
                    { new Guid("423e4567-e89b-12d3-a456-426655440021"), "123456789", new DateTime(2023, 7, 7, 14, 48, 35, 70, DateTimeKind.Local).AddTicks(6121), "Admin", new DateTime(2020, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1980, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("123e4567-e89b-12d3-a456-426655440010"), false, true, null, null, "Nguyễn Văn A", "back.jpg", "front.jpg", "Hà Nội", 1 },
                    { new Guid("423e4567-e89b-12d3-a456-426655440022"), "987654321", new DateTime(2023, 7, 7, 14, 48, 35, 70, DateTimeKind.Local).AddTicks(6129), "Admin", new DateTime(2019, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1985, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("123e4567-e89b-12d3-a456-426655440010"), false, true, null, null, "Nguyễn Thị B", "back.jpg", "front.jpg", "Hồ Chí Minh", 1 },
                    { new Guid("423e4567-e89b-12d3-a456-426655440023"), "654321789", new DateTime(2023, 7, 7, 14, 48, 35, 70, DateTimeKind.Local).AddTicks(6134), "Admin", new DateTime(2022, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2010, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("123e4567-e89b-12d3-a456-426655440010"), false, true, null, null, "Nguyễn Minh C", "back.jpg", "front.jpg", "Đà Nẵng", 4 },
                    { new Guid("423e4567-e89b-12d3-a456-426655440024"), "321789654", new DateTime(2023, 7, 7, 14, 48, 35, 70, DateTimeKind.Local).AddTicks(6142), "Admin", new DateTime(2023, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2015, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("123e4567-e89b-12d3-a456-426655440010"), false, true, null, null, "Nguyễn Đức D", "back.jpg", "front.jpg", "Hải Phòng", 4 },
                    { new Guid("423e4567-e89b-12d3-a456-426655440025"), null, new DateTime(2023, 7, 7, 14, 48, 35, 70, DateTimeKind.Local).AddTicks(6147), "Admin", null, new DateTime(1990, 6, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("123e4567-e89b-12d3-a456-426655440010"), false, false, null, null, "Nguyễn Thị E", null, null, null, 3 }
                });

            migrationBuilder.InsertData(
                table: "LeaveLogs",
                columns: new[] { "Id", "Created", "CreatedBy", "EmployeeId", "EndDate", "IsDeleted", "LastModified", "LastModifiedBy", "LeaveHours", "LeaveTypeId", "Reason", "StartDate", "Status" },
                values: new object[,]
                {
                    { new Guid("623e4567-e89b-12d3-a456-426655440041"), new DateTime(2023, 7, 7, 14, 48, 35, 70, DateTimeKind.Local).AddTicks(6262), "Admin", new Guid("123e4567-e89b-12d3-a456-426655440010"), new DateTime(2023, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, 8.0, new Guid("1611aee4-0a90-464d-927c-462f51d19120"), "Nghỉ ốm", new DateTime(2023, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { new Guid("623e4567-e89b-12d3-a456-426655440042"), new DateTime(2023, 7, 7, 14, 48, 35, 70, DateTimeKind.Local).AddTicks(6273), "Admin", new Guid("123e4567-e89b-12d3-a456-426655440010"), new DateTime(2023, 6, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, 4.0, new Guid("1611aee4-0a90-464d-927c-462f51d19120"), "Nghỉ phép", new DateTime(2023, 6, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { new Guid("623e4567-e89b-12d3-a456-426655440043"), new DateTime(2023, 7, 7, 14, 48, 35, 70, DateTimeKind.Local).AddTicks(6279), "Admin", new Guid("123e4567-e89b-12d3-a456-426655440010"), new DateTime(2023, 6, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, 8.0, new Guid("0d87bff5-e9ca-4e8a-af18-6cc2e804e57d"), "Nghỉ ốm", new DateTime(2023, 6, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { new Guid("623e4567-e89b-12d3-a456-426655440044"), new DateTime(2023, 7, 7, 14, 48, 35, 70, DateTimeKind.Local).AddTicks(6284), "Admin", new Guid("123e4567-e89b-12d3-a456-426655440010"), new DateTime(2023, 6, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, 4.0, new Guid("18167791-8248-4924-a1c6-90858852f905"), "Nghỉ phép", new DateTime(2023, 6, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { new Guid("623e4567-e89b-12d3-a456-426655440045"), new DateTime(2023, 7, 7, 14, 48, 35, 70, DateTimeKind.Local).AddTicks(6289), "Admin", new Guid("123e4567-e89b-12d3-a456-426655440010"), new DateTime(2023, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, 8.0, new Guid("51c55016-58c4-49d3-b0b2-2d7e0a9cd7d7"), "Nghỉ ốm", new DateTime(2023, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 }
                });

            migrationBuilder.InsertData(
                table: "OvertimeLogs",
                columns: new[] { "Id", "Coefficients", "Created", "CreatedBy", "EmployeeId", "EndDate", "IsDeleted", "LastModified", "LastModifiedBy", "StartDate", "Status", "TotalHours" },
                values: new object[,]
                {
                    { new Guid("523e4567-e89b-12d3-a456-426655440031"), 1.5, new DateTime(2023, 7, 7, 14, 48, 35, 70, DateTimeKind.Local).AddTicks(6175), "Admin", new Guid("123e4567-e89b-12d3-a456-426655440010"), new DateTime(2023, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, new DateTime(2023, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 2.5 },
                    { new Guid("523e4567-e89b-12d3-a456-426655440032"), 1.5, new DateTime(2023, 7, 7, 14, 48, 35, 70, DateTimeKind.Local).AddTicks(6222), "Admin", new Guid("123e4567-e89b-12d3-a456-426655440010"), new DateTime(2023, 6, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, new DateTime(2023, 6, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 3.0 },
                    { new Guid("523e4567-e89b-12d3-a456-426655440033"), 1.5, new DateTime(2023, 7, 7, 14, 48, 35, 70, DateTimeKind.Local).AddTicks(6227), "Admin", new Guid("123e4567-e89b-12d3-a456-426655440010"), new DateTime(2023, 6, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, new DateTime(2023, 6, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 1.5 },
                    { new Guid("523e4567-e89b-12d3-a456-426655440034"), 2.0, new DateTime(2023, 7, 7, 14, 48, 35, 70, DateTimeKind.Local).AddTicks(6232), "Admin", new Guid("123e4567-e89b-12d3-a456-426655440010"), new DateTime(2023, 6, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, new DateTime(2023, 6, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 4.0 },
                    { new Guid("523e4567-e89b-12d3-a456-426655440035"), 2.0, new DateTime(2023, 7, 7, 14, 48, 35, 70, DateTimeKind.Local).AddTicks(6236), "Admin", new Guid("123e4567-e89b-12d3-a456-426655440010"), new DateTime(2023, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, new DateTime(2023, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 2.0 }
                });

            migrationBuilder.InsertData(
                table: "Skill_Employees",
                columns: new[] { "Id", "Created", "CreatedBy", "EmployeeId", "IsDeleted", "LastModified", "LastModifiedBy", "Level", "SkillId" },
                values: new object[,]
                {
                    { new Guid("823e4567-e89b-12d3-a456-426655440061"), new DateTime(2023, 7, 7, 14, 48, 35, 70, DateTimeKind.Local).AddTicks(6397), "Admin", new Guid("123e4567-e89b-12d3-a456-426655440010"), false, null, null, "Chuyên gia", new Guid("723e4567-e89b-12d3-a456-426655440051") },
                    { new Guid("823e4567-e89b-12d3-a456-426655440062"), new DateTime(2023, 7, 7, 14, 48, 35, 70, DateTimeKind.Local).AddTicks(6403), "Admin", new Guid("123e4567-e89b-12d3-a456-426655440010"), false, null, null, "Trung bình", new Guid("723e4567-e89b-12d3-a456-426655440052") },
                    { new Guid("823e4567-e89b-12d3-a456-426655440063"), new DateTime(2023, 7, 7, 14, 48, 35, 70, DateTimeKind.Local).AddTicks(6407), "Admin", new Guid("123e4567-e89b-12d3-a456-426655440010"), false, null, null, "Trung bình", new Guid("723e4567-e89b-12d3-a456-426655440053") },
                    { new Guid("823e4567-e89b-12d3-a456-426655440064"), new DateTime(2023, 7, 7, 14, 48, 35, 70, DateTimeKind.Local).AddTicks(6414), "Admin", new Guid("123e4567-e89b-12d3-a456-426655440010"), false, null, null, "Chuyên gia", new Guid("723e4567-e89b-12d3-a456-426655440054") },
                    { new Guid("823e4567-e89b-12d3-a456-426655440065"), new DateTime(2023, 7, 7, 14, 48, 35, 70, DateTimeKind.Local).AddTicks(6419), "Admin", new Guid("123e4567-e89b-12d3-a456-426655440010"), false, null, null, "Trung bình", new Guid("723e4567-e89b-12d3-a456-426655440055") }
                });

            migrationBuilder.InsertData(
                table: "Allowances",
                columns: new[] { "Id", "AllowanceTypeId", "Amount", "Created", "CreatedBy", "EmployeeContractId", "IsDeleted", "LastModified", "LastModifiedBy" },
                values: new object[,]
                {
                    { new Guid("123e4567-e89b-12d3-a456-426655440030"), new Guid("123e4567-e89b-12d3-a456-426655440000"), 500000.0, new DateTime(2023, 6, 28, 17, 0, 0, 0, DateTimeKind.Unspecified), "Admin", new Guid("123e4567-e89b-12d3-a456-426655440020"), false, null, null },
                    { new Guid("223e4567-e89b-12d3-a456-426655440031"), new Guid("223e4567-e89b-12d3-a456-426655440000"), 1000000.0, new DateTime(2023, 6, 28, 17, 30, 0, 0, DateTimeKind.Unspecified), "Admin", new Guid("123e4567-e89b-12d3-a456-426655440020"), false, null, null },
                    { new Guid("323e4567-e89b-12d3-a456-426655440032"), new Guid("323e4567-e89b-12d3-a456-426655440000"), 2000000.0, new DateTime(2023, 6, 28, 18, 0, 0, 0, DateTimeKind.Unspecified), "Admin", new Guid("123e4567-e89b-12d3-a456-426655440020"), false, null, null },
                    { new Guid("423e4567-e89b-12d3-a456-426655440033"), new Guid("423e4567-e89b-12d3-a456-426655440000"), 3000000.0, new DateTime(2023, 6, 28, 18, 30, 0, 0, DateTimeKind.Unspecified), "Admin", new Guid("123e4567-e89b-12d3-a456-426655440020"), false, null, null },
                    { new Guid("523e4567-e89b-12d3-a456-426655440034"), new Guid("523e4567-e89b-12d3-a456-426655440000"), 4000000.0, new DateTime(2023, 6, 28, 19, 0, 0, 0, DateTimeKind.Unspecified), "Admin", new Guid("123e4567-e89b-12d3-a456-426655440020"), false, null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Allowances_AllowanceTypeId",
                table: "Allowances",
                column: "AllowanceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Allowances_EmployeeContractId",
                table: "Allowances",
                column: "EmployeeContractId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BankAccounts_BankId",
                table: "BankAccounts",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_BankAccounts_EmployeeId",
                table: "BankAccounts",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Degrees_EmployeeId",
                table: "Degrees",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_DetailTaxIncomes_PaySlipId",
                table: "DetailTaxIncomes",
                column: "PaySlipId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceCodes_DeviceCode",
                table: "DeviceCodes",
                column: "DeviceCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DeviceCodes_Expiration",
                table: "DeviceCodes",
                column: "Expiration");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeContracts_EmployeeId",
                table: "EmployeeContracts",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ApplicationUserId",
                table: "Employees",
                column: "ApplicationUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_PositionId",
                table: "Employees",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_Experiences_EmployeeId",
                table: "Experiences",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Families_EmployeeId",
                table: "Families",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Keys_Use",
                table: "Keys",
                column: "Use");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveLogs_EmployeeId",
                table: "LeaveLogs",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveLogs_LeaveTypeId",
                table: "LeaveLogs",
                column: "LeaveTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_OvertimeLogs_EmployeeId",
                table: "OvertimeLogs",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_PaySlips_EmployeeContractId",
                table: "PaySlips",
                column: "EmployeeContractId");

            migrationBuilder.CreateIndex(
                name: "IX_PersistedGrants_ConsumedTime",
                table: "PersistedGrants",
                column: "ConsumedTime");

            migrationBuilder.CreateIndex(
                name: "IX_PersistedGrants_Expiration",
                table: "PersistedGrants",
                column: "Expiration");

            migrationBuilder.CreateIndex(
                name: "IX_PersistedGrants_SubjectId_ClientId_Type",
                table: "PersistedGrants",
                columns: new[] { "SubjectId", "ClientId", "Type" });

            migrationBuilder.CreateIndex(
                name: "IX_PersistedGrants_SubjectId_SessionId_Type",
                table: "PersistedGrants",
                columns: new[] { "SubjectId", "SessionId", "Type" });

            migrationBuilder.CreateIndex(
                name: "IX_Positions_DepartmentId",
                table: "Positions",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Skill_Employees_EmployeeId",
                table: "Skill_Employees",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Skill_Employees_SkillId",
                table: "Skill_Employees",
                column: "SkillId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeAttendanceLogs_EmployeeId",
                table: "TimeAttendanceLogs",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_TodoItems_ListId",
                table: "TodoItems",
                column: "ListId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Allowances");

            migrationBuilder.DropTable(
                name: "AnnualWorkingDays");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "BankAccounts");

            migrationBuilder.DropTable(
                name: "Degrees");

            migrationBuilder.DropTable(
                name: "DetailTaxIncomes");

            migrationBuilder.DropTable(
                name: "DeviceCodes");

            migrationBuilder.DropTable(
                name: "Exchanges");

            migrationBuilder.DropTable(
                name: "Experiences");

            migrationBuilder.DropTable(
                name: "Families");

            migrationBuilder.DropTable(
                name: "Keys");

            migrationBuilder.DropTable(
                name: "LeaveLogs");

            migrationBuilder.DropTable(
                name: "OvertimeLogs");

            migrationBuilder.DropTable(
                name: "PersistedGrants");

            migrationBuilder.DropTable(
                name: "Skill_Employees");

            migrationBuilder.DropTable(
                name: "TaxInComes");

            migrationBuilder.DropTable(
                name: "TimeAttendanceLogs");

            migrationBuilder.DropTable(
                name: "TodoItems");

            migrationBuilder.DropTable(
                name: "AllowanceTypes");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Banks");

            migrationBuilder.DropTable(
                name: "PaySlips");

            migrationBuilder.DropTable(
                name: "LeaveTypes");

            migrationBuilder.DropTable(
                name: "Skills");

            migrationBuilder.DropTable(
                name: "TodoLists");

            migrationBuilder.DropTable(
                name: "EmployeeContracts");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Positions");

            migrationBuilder.DropTable(
                name: "Departments");
        }
    }
}
