using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Focus.Persistence.Migrations
{
    public partial class v_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountTemplates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    NameArabic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    JsonTemplate = table.Column<string>(type: "nvarchar(max)", maxLength: 2147483647, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountTemplates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Branches",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MainBranch = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsCentralized = table.Column<bool>(type: "bit", nullable: false),
                    IsApproval = table.Column<bool>(type: "bit", nullable: false),
                    IsOnline = table.Column<bool>(type: "bit", nullable: false),
                    BranchName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BranchType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BusinessId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branches", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BranchSetups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Prefix = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartingNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EndNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BranchSetups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Blocked = table.Column<bool>(type: "bit", nullable: false),
                    LogoPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HouseNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyRegNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameEnglish = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameArabic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VatRegistrationNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CityEnglish = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CityArabic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountryEnglish = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountryArabic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryInEnglish = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryInArabic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressEnglish = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressArabic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Landline = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Website = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Postcode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Town = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxInvoiceLabel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClientParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BusinessParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsMultiUnit = table.Column<bool>(type: "bit", nullable: false),
                    IsProduction = table.Column<bool>(type: "bit", nullable: false),
                    InvoiceWoInventory = table.Column<bool>(type: "bit", nullable: false),
                    IsArea = table.Column<bool>(type: "bit", nullable: false),
                    IsProceed = table.Column<bool>(type: "bit", nullable: false),
                    English = table.Column<bool>(type: "bit", nullable: false),
                    Arabic = table.Column<bool>(type: "bit", nullable: false),
                    CompanyNameEnglish = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyNameArabic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Terminal = table.Column<bool>(type: "bit", nullable: false),
                    DayStart = table.Column<bool>(type: "bit", nullable: false),
                    Step1 = table.Column<bool>(type: "bit", nullable: false),
                    Step2 = table.Column<bool>(type: "bit", nullable: false),
                    Step3 = table.Column<bool>(type: "bit", nullable: false),
                    Step4 = table.Column<bool>(type: "bit", nullable: false),
                    Step5 = table.Column<bool>(type: "bit", nullable: false),
                    TermsCondition = table.Column<bool>(type: "bit", nullable: false),
                    CashVoucher = table.Column<bool>(type: "bit", nullable: false),
                    IsOpenDay = table.Column<bool>(type: "bit", nullable: false),
                    IsTransferAllow = table.Column<bool>(type: "bit", nullable: false),
                    MasterProduct = table.Column<bool>(type: "bit", nullable: false),
                    SimpleInvoice = table.Column<bool>(type: "bit", nullable: false),
                    SoInventoryReserve = table.Column<bool>(type: "bit", nullable: false),
                    InternationalPurchase = table.Column<bool>(type: "bit", nullable: false),
                    PartiallyPurchase = table.Column<bool>(type: "bit", nullable: false),
                    VersionAllow = table.Column<bool>(type: "bit", nullable: false),
                    PurchaseOrder = table.Column<bool>(type: "bit", nullable: false),
                    ExpenseToGst = table.Column<bool>(type: "bit", nullable: false),
                    SaleOrder = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AutoPurchaseVoucher = table.Column<bool>(type: "bit", nullable: false),
                    IsForMedical = table.Column<bool>(type: "bit", nullable: false),
                    ExpenseAccount = table.Column<bool>(type: "bit", nullable: false),
                    SuperAdminDayStart = table.Column<bool>(type: "bit", nullable: false),
                    BankDetail = table.Column<bool>(type: "bit", nullable: false),
                    TaxInvoiceLabelAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SimplifyTaxInvoiceLabel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SimplifyTaxInvoiceLabelAr = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DefaultSettings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsSaleCredit = table.Column<bool>(type: "bit", nullable: false),
                    IsPurchaseCredit = table.Column<bool>(type: "bit", nullable: false),
                    IsCustomeCredit = table.Column<bool>(type: "bit", nullable: false),
                    IsCustomerPayCredit = table.Column<bool>(type: "bit", nullable: false),
                    IsSupplierPayCredit = table.Column<bool>(type: "bit", nullable: false),
                    IsSupplierCredit = table.Column<bool>(type: "bit", nullable: false),
                    IsCashCustomer = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DefaultSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GsmSmsSetups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ComPort = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DefaultMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GsmSmsSetups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LoginHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LogoutDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IpAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OperatingSystem = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginHistories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ModulesNames",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameArabic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModulesNames", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SyncPushPullRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Table = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ColumnId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ColumnType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Push = table.Column<bool>(type: "bit", nullable: false),
                    Pull = table.Column<bool>(type: "bit", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChangeDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PushDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PullDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ColumnName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsGeneral = table.Column<bool>(type: "bit", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocumentNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SyncPushPullRecords", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SyncRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Table = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    ColumnId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ColumnType = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Push = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Pull = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Action = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    ChangeDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PushDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PullDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ColumnName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsGeneral = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocumentNo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SyncRecords", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WhiteLabelings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Heading = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApplicationName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressLine1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressLine2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressLine3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FavName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SideMenuColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SideMenuBtnColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SideMenuBtnClickColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SaveBtnBgColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SaveBtnColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CancelBgBtnColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CancelBtnColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HeadingColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TableHeaderBgColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TableHeaderColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InvoiceTitleBgColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InvoiceTitleColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WhiteLabelings", x => x.Id);
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
                name: "AccountsLevelOne",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NameArabic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountsLevelOne", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountsLevelOne_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AccountsLevelTwo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameArabic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountsLevelTwo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountsLevelTwo_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AccountTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    NameArabic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountTypes_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AllowanceTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameArabic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllowanceTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AllowanceTypes_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUpdates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Version = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Detail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CounterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUpdates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationUpdates_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ApprovalSystems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DepartmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DesignationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ApprovaType = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApprovalSystems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApprovalSystems_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Attachments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameArabic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModuleId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attachments_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AutoPurchaseSettings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Day = table.Column<int>(type: "int", nullable: false),
                    TaxMethod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxRateId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    WareHouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AutoPurchaseSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AutoPurchaseSettings_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Boms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApprovalStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WareHouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SaleOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Boms_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BranchUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BranchUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BranchUsers_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BranchUsers_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BranchWisePermissions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PermissionName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Key = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NobleGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NobleModuleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BranchWisePermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BranchWisePermissions_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameArabic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Brands_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CashCustomer",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameArabic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CashCustomer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CashCustomer_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CashRequestUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CashRequestUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CashRequestUsers_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CashVouchers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VoucherNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    SaleReturnId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SaleInvoiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CashVouchers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CashVouchers_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameArabic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cities_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Colors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameArabic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Colors_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CompanyAccountSetups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InventoryAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    VatPayableAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    VatReceiableAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SaleAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DiscountPayableAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DiscountReceivableAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FreeofCostAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StockInAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StockOutAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BundleAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PromotionAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BankId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CashId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TaxRateId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CurrencyId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BarcodeType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxMethod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BackupPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AutoSync = table.Column<bool>(type: "bit", nullable: false),
                    AutoSyncInterval = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyAccountSetups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyAccountSetups_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CompanyAttachments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CommercialRegistration = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BusinessLicence = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CivilDefenceLicense = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CctvLicence = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Logo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyAttachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyAttachments_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompanyHolidays",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HolidayType = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Year = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaidStatus = table.Column<bool>(type: "bit", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyHolidays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyHolidays_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "companyInformations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameArabic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CommercialRegNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VatRegistrationNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CityArabic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountryArabic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Website = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressArabic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_companyInformations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_companyInformations_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CompanyLicences",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FromDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ToDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsBlock = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    NumberOfUsers = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyType = table.Column<int>(type: "int", nullable: false),
                    NoOfTransactionsAllow = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsTechnicalSupport = table.Column<bool>(type: "bit", nullable: false),
                    IsUpdateTechnicalSupport = table.Column<bool>(type: "bit", nullable: false),
                    TechnicalSupportPeriod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentFrequency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GracePeriod = table.Column<bool>(type: "bit", nullable: false),
                    LicenseType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActivationPlatform = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyLicences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyLicences_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompanyOptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Label = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<bool>(type: "bit", nullable: false),
                    OptionValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyOptions_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CompanyProcess",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProcessNameArabic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProcessName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyProcess", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyProcess_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Contractors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EnglishName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ArabicName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsOnFactory = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contractors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contractors_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Contributions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameInPayslip = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AmountType = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    NameInPayslipArabic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contributions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contributions_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameArabic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sign = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ArabicSign = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Currencies_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomerDiscount",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DiscountName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Discount = table.Column<double>(type: "float", nullable: false),
                    FreightDiscount = table.Column<double>(type: "float", nullable: false),
                    ExtraDiscount = table.Column<double>(type: "float", nullable: false),
                    OtherDiscount = table.Column<double>(type: "float", nullable: false),
                    OpenDiscount = table.Column<double>(type: "float", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerDiscount", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerDiscount_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomerGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameArabic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerGroups_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DayStarts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SaleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CounterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OpeningCash = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CashInHand = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VerifyCash = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ExpenseCash = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalCash = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SupervisorCash = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FromTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ToTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DuringSaleClose = table.Column<int>(type: "int", nullable: false),
                    DuringSaleCloseReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DayStartUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DayEndUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CarryCash = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsExpenseDay = table.Column<bool>(type: "bit", nullable: false),
                    IsDayStart = table.Column<bool>(type: "bit", nullable: false),
                    StartTerminalBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SuperVisorName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartTerminalFor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EndTerminalBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EndTerminalFor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreditReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DayStartId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BankAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NoOfTransaction = table.Column<int>(type: "int", nullable: false),
                    ReceivingAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsReceived = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DayStarts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DayStarts_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Deductions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameInPayslip = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameInPayslipArabic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AmountType = table.Column<int>(type: "int", nullable: false),
                    TaxPlan = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deductions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Deductions_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DenominationSetups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Number = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DenominationSetups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DenominationSetups_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Designations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NameArabic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Designations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Designations_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Discounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiscountName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiscountPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiscountAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Discounts_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DiscountSetups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LineItemBeforeVat = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    LineItemAfterVat = table.Column<bool>(type: "bit", nullable: false),
                    OverAllBeforeVat = table.Column<bool>(type: "bit", nullable: false),
                    OverAllAfterVat = table.Column<bool>(type: "bit", nullable: false),
                    DiscountOverQty = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    DiscountOver1Qty = table.Column<bool>(type: "bit", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscountSetups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DiscountSetups_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmailConfiguration",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SmtpServer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Port = table.Column<int>(type: "int", nullable: false),
                    Server = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailConfiguration", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmailConfiguration_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FinancialYearClosings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PeriodName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FromDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ToDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReOpen = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialYearClosings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FinancialYearClosings_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FinancialYearSettings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClosingType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsAutoClosing = table.Column<bool>(type: "bit", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialYearSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FinancialYearSettings_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HoldSetups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HoldRecordType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoldSetups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HoldSetups_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Holidays",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OutTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NameArabic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Month = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Year = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Holidays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Holidays_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ImportExportTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameArabic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImportExportTypes = table.Column<int>(type: "int", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImportExportTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImportExportTypes_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InquiryModules",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Label = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Compulsory = table.Column<bool>(type: "bit", nullable: false),
                    AttachmentCompulsory = table.Column<bool>(type: "bit", nullable: false),
                    RowNumber = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InquiryModules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InquiryModules_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InquiryPriorities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InquiryPriorities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InquiryPriorities_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InquiryProcesses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Label = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InquiryProcesses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InquiryProcesses_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InquiryStatusDynamics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InquiryStatusDynamics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InquiryStatusDynamics_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InquiryTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Label = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InquiryTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InquiryTypes_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceDefaults",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsSalePrice = table.Column<bool>(type: "bit", nullable: false),
                    IsCustomerPrice = table.Column<bool>(type: "bit", nullable: false),
                    IsSalePriceLabel = table.Column<bool>(type: "bit", nullable: false),
                    IsCustomerPriceLabel = table.Column<bool>(type: "bit", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceDefaults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoiceDefaults_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Issueds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameArabic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Issueds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Issueds_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ItemsListDisplayOrderSetup",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sequence = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemsListDisplayOrderSetup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemsListDisplayOrderSetup_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ItemViewSetupForPrint",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sequence = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemViewSetupForPrint", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemViewSetupForPrint_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ItemViewSetups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sequence = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemViewSetups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemViewSetups_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "JournalVouchers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VoucherNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Narration = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OpeningCash = table.Column<bool>(type: "bit", nullable: false),
                    IsStockTransfer = table.Column<bool>(type: "bit", nullable: false),
                    OneTimeEntry = table.Column<bool>(type: "bit", nullable: false),
                    PeriodId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ApprovalStatus = table.Column<int>(type: "int", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Year = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JournalVouchers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JournalVouchers_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LeaveGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeaveGroups_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LeaveHolidays",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveHolidays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeaveHolidays_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LeavePeriods",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PeriodStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PeriodEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeavePeriods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeavePeriods_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LedgerTransactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DocumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DocumentNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransactionType = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DocumentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ApprovalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Year = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Debit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Credit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ContactId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PeriodId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LedgerTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LedgerTransactions_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ListOrderSetups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sequence = table.Column<int>(type: "int", nullable: false),
                    DocumentName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListOrderSetups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ListOrderSetups_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LoginPermissions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChangePriceDuringSale = table.Column<bool>(type: "bit", nullable: false),
                    GiveDicountDuringSale = table.Column<bool>(type: "bit", nullable: false),
                    ViewCounterDetails = table.Column<bool>(type: "bit", nullable: false),
                    TransferCounter = table.Column<bool>(type: "bit", nullable: false),
                    CloseCounter = table.Column<bool>(type: "bit", nullable: false),
                    HoldCounter = table.Column<bool>(type: "bit", nullable: false),
                    CloseDay = table.Column<bool>(type: "bit", nullable: false),
                    StartDay = table.Column<bool>(type: "bit", nullable: false),
                    ProcessSaleReturn = table.Column<bool>(type: "bit", nullable: false),
                    DailyExpenseList = table.Column<bool>(type: "bit", nullable: false),
                    InvoiceWoInventory = table.Column<bool>(type: "bit", nullable: false),
                    IsTouchInvoice = table.Column<bool>(type: "bit", nullable: false),
                    AllowAll = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    PermissionToStartExpenseDay = table.Column<bool>(type: "bit", nullable: false),
                    IsSupervisor = table.Column<bool>(type: "bit", nullable: false),
                    IsInquiryHandler = table.Column<bool>(type: "bit", nullable: false),
                    TouchScreen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TemporaryCashReceiver = table.Column<bool>(type: "bit", nullable: false),
                    TemporaryCashIssuer = table.Column<bool>(type: "bit", nullable: false),
                    TemporaryCashRequester = table.Column<bool>(type: "bit", nullable: false),
                    IsExpenseAccount = table.Column<bool>(type: "bit", nullable: false),
                    AllowViewAllData = table.Column<bool>(type: "bit", nullable: false),
                    Days = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Limit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TerminalUserType = table.Column<int>(type: "int", nullable: false),
                    IsOverAllAccess = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginPermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoginPermissions_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MediaTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MediaTypes_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MobileOrders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameArabic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MobileOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MobileOrders_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MonthlyCosts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MonthlyRent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MonthlySaleries = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MonthlyUtilityBills = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MonthlyGovtFees = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MonthlyGovtZakat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GovtFeeForLabour = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonthlyCosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MonthlyCosts_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NobleGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GroupType = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NobleGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NobleGroups_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NobleModules",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModuleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ArabicName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NobleModules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NobleModules_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NobleRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NameArabic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NobleRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NobleRoles_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Origins",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameArabic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Origins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Origins_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PaymentLimits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FromDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ToDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentLimits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentLimits_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PaymentOptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameArabic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentOptions_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PaySchedules",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PayPeriod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PayPeriodStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PayPeriodEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IfPayDayFallOnHoliday = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PayDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Default = table.Column<bool>(type: "bit", nullable: false),
                    IsHourlyPay = table.Column<bool>(type: "bit", nullable: false),
                    Proceed = table.Column<bool>(type: "bit", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaySchedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaySchedules_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PermanentDeleteHoldSetups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HoldRecordType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermanentDeleteHoldSetups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PermanentDeleteHoldSetups_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Prefixies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SInvoice = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PerfomaInvoice = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SReturn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SOrder = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SQutation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PInvoice = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PReturn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    POrder = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SOrdeTracking = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SaleInvoiceCredit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GlobalInvoice = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReceiptInvoice = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SaleInvoiceHold = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProformaSaleInvoice = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PurchaseInvoiceDraft = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Employee = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DebitNote = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreditNote = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentVoucher = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerPayReceipt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdvanceSaleReceipt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SupplierPayReceipt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdvancePurchaseReceipt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prefixies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Prefixies_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PriceLabelings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameArabic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceLabelings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PriceLabelings_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Processes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnglishName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ArabicName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Processes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Processes_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameArabic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductGroups_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductionStockTransfers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DamageStock = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    RemainingStock = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RemainingWareHouse = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DamageWareHouse = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductionBatchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductionStockTransfers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductionStockTransfers_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductMasters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameArabic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductMasters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductMasters_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "QuotationTemplates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsService = table.Column<bool>(type: "bit", nullable: false),
                    RegistrationNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TemplateName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApprovalStatus = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuotationTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuotationTemplates_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReparingOrderTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameArabic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReparingOrderTypeEnums = table.Column<int>(type: "int", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReparingOrderTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReparingOrderTypes_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SalaryTemplates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StructureName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalaryTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalaryTemplates_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Sizes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameArabic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sizes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sizes_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TaxRates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameArabic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxRates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaxRates_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TaxSalaries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TaxYear = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    FinancialYear = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FromDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ToDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxSalaries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaxSalaries_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TemporaryCashRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NewUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegistrationNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApprovalStatus = table.Column<int>(type: "int", nullable: false),
                    IsCashRequesterUser = table.Column<bool>(type: "bit", nullable: false),
                    IsClose = table.Column<bool>(type: "bit", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Year = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PeriodId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemporaryCashRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TemporaryCashRequests_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TransactionApplicationLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FreshLogMovingDays = table.Column<int>(type: "int", nullable: false),
                    DeleteFromHistory = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    LocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionApplicationLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransactionApplicationLogs_Companies_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TransferHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CounterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OpeningCash = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CashInHand = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VerifyCash = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ExpenseCash = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalCash = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SupervisorCash = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FromTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ToTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DayStartUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DayEndUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CarryCash = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsExpenseDay = table.Column<bool>(type: "bit", nullable: false),
                    StartTerminalBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartTerminalFor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreditReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DayStartId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BankAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NoOfTransaction = table.Column<int>(type: "int", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransferHistories_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Transporters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transporters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transporters_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Units",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameArabic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Units", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Units_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserDefineFlows",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuotationToSaleOrder = table.Column<bool>(type: "bit", nullable: false),
                    QuotationToSaleInvoice = table.Column<bool>(type: "bit", nullable: false),
                    PartiallyQuotation = table.Column<bool>(type: "bit", nullable: false),
                    PartiallySaleOrder = table.Column<bool>(type: "bit", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDefineFlows", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserDefineFlows_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DocumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DocumentNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocumentType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApprovalStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserLogs_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VariationInventories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SizeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ColorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VariationInventories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VariationInventories_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VariationInventoryForReportings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DocumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TransactionType = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SizeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ColorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VariationInventoryForReportings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VariationInventoryForReportings_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Warehouses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StoreID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameArabic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LicenseNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LicenseExpiry = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CivilDefenceLicenseNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CivilDefenceLicenseExpiry = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CCTVLicenseNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CCTVLicenseExpiry = table.Column<DateTime>(type: "datetime2", nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Warehouses_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WarrantyTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameArabic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarrantyTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WarrantyTypes_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorkWeeks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Day = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkWeeks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkWeeks_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "YearlyPeriods",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsYearlyPeriodClosed = table.Column<bool>(type: "bit", nullable: false),
                    MonthType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YearlyPeriods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_YearlyPeriods_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Zones",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Zones_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ModulesRights",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModuleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModulesRights", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ModulesRights_ModulesNames_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "ModulesNames",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CostCenters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NameArabic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    VatDeductible = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    AccountTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CostCenters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CostCenters_AccountTypes_AccountTypeId",
                        column: x => x.AccountTypeId,
                        principalTable: "AccountTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CostCenters_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Allowances",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AllowanceTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AmountType = table.Column<int>(type: "int", nullable: false),
                    TaxPlan = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
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
                        name: "FK_Allowances_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CountryId = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    StateId = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    CityId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    Area = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Regions_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Regions_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OtherCurrencies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CurrencyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Rate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OtherCurrencies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OtherCurrencies_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OtherCurrencies_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GuestedHolidays",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HolidayId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuestedHolidays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GuestedHolidays_Holidays_HolidayId",
                        column: x => x.HolidayId,
                        principalTable: "Holidays",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "WeekHolidays",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameArabic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HolidayId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeekHolidays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WeekHolidays_Holidays_HolidayId",
                        column: x => x.HolidayId,
                        principalTable: "Holidays",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ModuleQuestions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Question = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Answer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Answer1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Answer2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Answer3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Answer4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Answer5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Answer6 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Answer7 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Answer8 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Answer9 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InquiryModuleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModuleQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ModuleQuestions_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ModuleQuestions_InquiryModules_InquiryModuleId",
                        column: x => x.InquiryModuleId,
                        principalTable: "InquiryModules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JournalVoucherAttachment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    JournalVoucherId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JournalVoucherAttachment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JournalVoucherAttachment_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JournalVoucherAttachment_JournalVouchers_JournalVoucherId",
                        column: x => x.JournalVoucherId,
                        principalTable: "JournalVouchers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LeaveTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LeaveName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LeaveColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LeavesPerLeavePeriod = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AdminAssignLeave = table.Column<bool>(type: "bit", nullable: false),
                    EmployeesApplyForLeaveType = table.Column<bool>(type: "bit", nullable: false),
                    BeyondCurrentLeaveBalance = table.Column<bool>(type: "bit", nullable: false),
                    PercentageLeaveCF = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaximumCFAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LeaveCarriedForward1 = table.Column<bool>(type: "bit", nullable: false),
                    CFLeaveAvailabilityPeriod1 = table.Column<int>(type: "int", nullable: false),
                    LeaveAccrueEnabled = table.Column<bool>(type: "bit", nullable: false),
                    ProportionateLeaves = table.Column<bool>(type: "bit", nullable: false),
                    EmployeeLeavePeriod = table.Column<bool>(type: "bit", nullable: false),
                    SendNotificationEmails = table.Column<bool>(type: "bit", nullable: false),
                    LeaveGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeaveTypes_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LeaveTypes_LeaveGroups_LeaveGroupId",
                        column: x => x.LeaveGroupId,
                        principalTable: "LeaveGroups",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MonthlyCostItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MonthlyCosts = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    YearlyCost = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Daily = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    MonthlyCostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonthlyCostItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MonthlyCostItems_MonthlyCosts_MonthlyCostId",
                        column: x => x.MonthlyCostId,
                        principalTable: "MonthlyCosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompanyPermissions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PermissionName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Key = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NobleGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NobleModuleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyPermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyPermissions_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanyPermissions_NobleGroups_NobleGroupId",
                        column: x => x.NobleGroupId,
                        principalTable: "NobleGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompanyPermissions_NobleModules_NobleModuleId",
                        column: x => x.NobleModuleId,
                        principalTable: "NobleModules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NoblePermissions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NobleModuleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NoblePermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NoblePermissions_NobleModules_NobleModuleId",
                        column: x => x.NobleModuleId,
                        principalTable: "NobleModules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NobleUserRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NobleUserRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NobleUserRoles_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NobleUserRoles_NobleRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "NobleRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RolesPermissions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModuleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameArabic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Save = table.Column<bool>(type: "bit", nullable: false),
                    Edit = table.Column<bool>(type: "bit", nullable: false),
                    Delete = table.Column<bool>(type: "bit", nullable: false),
                    List = table.Column<bool>(type: "bit", nullable: false),
                    Print = table.Column<bool>(type: "bit", nullable: false),
                    Draft = table.Column<bool>(type: "bit", nullable: false),
                    Approved = table.Column<bool>(type: "bit", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolesPermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RolesPermissions_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RolesPermissions_NobleRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "NobleRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RunPayrolls",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PayrollScheduleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    Hourly = table.Column<bool>(type: "bit", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RunPayrolls", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RunPayrolls_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RunPayrolls_PaySchedules_PayrollScheduleId",
                        column: x => x.PayrollScheduleId,
                        principalTable: "PaySchedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SalaryContributions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SalaryTemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ContributionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AmountType = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalaryContributions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalaryContributions_Contributions_ContributionId",
                        column: x => x.ContributionId,
                        principalTable: "Contributions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SalaryContributions_SalaryTemplates_SalaryTemplateId",
                        column: x => x.SalaryTemplateId,
                        principalTable: "SalaryTemplates",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SalaryDeductions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SalaryTemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeductionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AmountType = table.Column<int>(type: "int", nullable: false),
                    TaxPlan = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalaryDeductions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalaryDeductions_Deductions_DeductionId",
                        column: x => x.DeductionId,
                        principalTable: "Deductions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SalaryDeductions_SalaryTemplates_SalaryTemplateId",
                        column: x => x.SalaryTemplateId,
                        principalTable: "SalaryTemplates",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SalaryTaxSlabs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IncomeFrom = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IncomeTo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FixedTax = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Rate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    TaxSalaryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalaryTaxSlabs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalaryTaxSlabs_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalaryTaxSlabs_TaxSalaries_TaxSalaryId",
                        column: x => x.TaxSalaryId,
                        principalTable: "TaxSalaries",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TemporaryCashIssues",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NewUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegistrationNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApprovalStatus = table.Column<int>(type: "int", nullable: false),
                    IsCashRequesterUser = table.Column<bool>(type: "bit", nullable: false),
                    TemporaryCashRequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CashIssuerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Year = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PeriodId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemporaryCashIssues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TemporaryCashIssues_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TemporaryCashIssues_TemporaryCashRequests_TemporaryCashRequestId",
                        column: x => x.TemporaryCashRequestId,
                        principalTable: "TemporaryCashRequests",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TemporaryCashRequestItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TemporaryCashRequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemporaryCashRequestItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TemporaryCashRequestItems_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TemporaryCashRequestItems_TemporaryCashRequests_TemporaryCashRequestId",
                        column: x => x.TemporaryCashRequestId,
                        principalTable: "TemporaryCashRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InventoryBlinds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WarehouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DocumentId = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsCounted = table.Column<bool>(type: "bit", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryBlinds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InventoryBlinds_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InventoryBlinds_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StockAdjustments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Narration = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    TaxMethod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isDraft = table.Column<bool>(type: "bit", nullable: false),
                    WarehouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TaxRateId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StockAdjustmentType = table.Column<int>(type: "int", nullable: false),
                    IsSerial = table.Column<bool>(type: "bit", nullable: false),
                    BankCashAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Year = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PeriodId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockAdjustments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockAdjustments_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StockAdjustments_TaxRates_TaxRateId",
                        column: x => x.TaxRateId,
                        principalTable: "TaxRates",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StockAdjustments_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StockRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StockRequestStatus = table.Column<int>(type: "int", nullable: false),
                    FromWareHouse = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FromBranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ToBranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ApprovalStatus = table.Column<int>(type: "int", nullable: false),
                    WarehouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Year = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PeriodId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockRequests_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StockRequests_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "WareHouseTransfers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FromWareHouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ToWareHouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FromBranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ToBranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ApprovalStatus = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Year = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PeriodId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WareHouseTransfers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WareHouseTransfers_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WareHouseTransfers_Warehouses_ToWareHouseId",
                        column: x => x.ToWareHouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompanySubmissionPeriods",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Year = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    PeriodName = table.Column<int>(type: "int", nullable: false),
                    PeriodStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PeriodEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsPeriodClosed = table.Column<bool>(type: "bit", nullable: false),
                    YearlyPeriodId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanySubmissionPeriods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanySubmissionPeriods_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanySubmissionPeriods_YearlyPeriods_YearlyPeriodId",
                        column: x => x.YearlyPeriodId,
                        principalTable: "YearlyPeriods",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: true),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nic = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    JobType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SearchingKey = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DesignationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ZoneId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_Designations_DesignationId",
                        column: x => x.DesignationId,
                        principalTable: "Designations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Employees_Employees_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_Zones_ZoneId",
                        column: x => x.ZoneId,
                        principalTable: "Zones",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    NameArabic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CostCenterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accounts_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Accounts_CostCenters_CostCenterId",
                        column: x => x.CostCenterId,
                        principalTable: "CostCenters",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SalaryAllowances",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SalaryTemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AmountType = table.Column<int>(type: "int", nullable: false),
                    TaxPlan = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AllowanceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalaryAllowances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalaryAllowances_Allowances_AllowanceId",
                        column: x => x.AllowanceId,
                        principalTable: "Allowances",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SalaryAllowances_SalaryTemplates_SalaryTemplateId",
                        column: x => x.SalaryTemplateId,
                        principalTable: "SalaryTemplates",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "NobleRolePermissions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PermissionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NobleRolePermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NobleRolePermissions_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NobleRolePermissions_CompanyPermissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "CompanyPermissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NobleRolePermissions_NobleRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "NobleRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RunPayrollDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SalaryType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WeekendDayHourlySalary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IncomeTax = table.Column<bool>(type: "bit", nullable: false),
                    AutoIncomeTax = table.Column<bool>(type: "bit", nullable: false),
                    BaseSalary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AllowanceAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GrossSalary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DeductionAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ContributionAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TaxAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LoanAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NetSalary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RunPayrollId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WeekdayHourlySalary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HourAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Hour = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ZeroSalary = table.Column<bool>(type: "bit", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RunPayrollDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RunPayrollDetails_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RunPayrollDetails_RunPayrolls_RunPayrollId",
                        column: x => x.RunPayrollId,
                        principalTable: "RunPayrolls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TemporaryCashIssueExpenses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TemporaryCashIssueId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DocumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DocumentType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Year = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PeriodId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemporaryCashIssueExpenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TemporaryCashIssueExpenses_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TemporaryCashIssueExpenses_TemporaryCashIssues_TemporaryCashIssueId",
                        column: x => x.TemporaryCashIssueId,
                        principalTable: "TemporaryCashIssues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TemporaryCashIssueItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TemporaryCashIssueId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemporaryCashIssueItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TemporaryCashIssueItems_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TemporaryCashIssueItems_TemporaryCashIssues_TemporaryCashIssueId",
                        column: x => x.TemporaryCashIssueId,
                        principalTable: "TemporaryCashIssues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TemporaryCashReturns",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RegistrationNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsCashRequesterUser = table.Column<bool>(type: "bit", nullable: false),
                    TemporaryCashIssueId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Year = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PeriodId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemporaryCashReturns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TemporaryCashReturns_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TemporaryCashReturns_TemporaryCashIssues_TemporaryCashIssueId",
                        column: x => x.TemporaryCashIssueId,
                        principalTable: "TemporaryCashIssues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StockTransfers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApprovalStatus = table.Column<int>(type: "int", nullable: false),
                    StockTransferStatus = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WarehouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StockRequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StockRequesBranchtId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    VoucherId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    VehicalNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DriverName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DriverNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DriverNationalId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Year = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PeriodId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockTransfers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockTransfers_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StockTransfers_StockRequests_StockRequestId",
                        column: x => x.StockRequestId,
                        principalTable: "StockRequests",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EmployeeAttachments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AttachmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeAttachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeAttachments_Attachments_AttachmentId",
                        column: x => x.AttachmentId,
                        principalTable: "Attachments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeAttachments_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeAttachments_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BankPosTerminals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TerminalId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankPosTerminals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BankPosTerminals_Accounts_BankId",
                        column: x => x.BankId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BankPosTerminals_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Banks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BankName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameArabic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    AccoutName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccoutNameArabic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrencyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BranchName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BranchCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BranchAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IBNNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SwiftCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactPerson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ManagerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ManagerContectualNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccounType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Reference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShortName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Banks_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Banks_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Banks_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BranchVouchers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VoucherNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Narration = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChequeNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NatureOfPayment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ApprovalStatus = table.Column<int>(type: "int", nullable: false),
                    PaymentMode = table.Column<int>(type: "int", nullable: false),
                    PaymentMethod = table.Column<int>(type: "int", nullable: false),
                    BankCashAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContactAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TaxMethod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxRateId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BranchVouchers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BranchVouchers_Accounts_BankCashAccountId",
                        column: x => x.BankCashAccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BranchVouchers_Accounts_ContactAccountId",
                        column: x => x.ContactAccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BranchVouchers_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BranchVouchers_TaxRates_TaxRateId",
                        column: x => x.TaxRateId,
                        principalTable: "TaxRates",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NameArabic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    IsReturn = table.Column<bool>(type: "bit", nullable: false),
                    ReturnDays = table.Column<int>(type: "int", nullable: false),
                    PurchaseAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    COGSAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    InventoryAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IncomeAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SaleAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsService = table.Column<bool>(type: "bit", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Accounts_COGSAccountId",
                        column: x => x.COGSAccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Categories_Accounts_IncomeAccountId",
                        column: x => x.IncomeAccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Categories_Accounts_InventoryAccountId",
                        column: x => x.InventoryAccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Categories_Accounts_PurchaseAccountId",
                        column: x => x.PurchaseAccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Categories_Accounts_SaleAccountId",
                        column: x => x.SaleAccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Categories_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CashCustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Prefix = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnglishName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ArabicName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyNameEnglish = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyNameArabic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telephone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegistrationDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerGroup = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactNo1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CommercialRegistrationNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VatNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrencyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RegionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TaxRateId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Website = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BillingAttention = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BillingZipCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BillingPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Area = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BillingFax = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShippingAttention = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShippingCountry = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShippingZipCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShippingPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShippingArea = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShippingAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeliveryAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShippingOther = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeliveryOther = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShippingCity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShippingFax = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsCustomer = table.Column<bool>(type: "bit", nullable: false),
                    IsAddressOnAll = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsCashCustomer = table.Column<bool>(type: "bit", nullable: false),
                    PaymentTerms = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeliveryTerm = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreditDays = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreditLimit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreditPeriod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WhatsAppNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ContactPerson1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactPerson2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactNo2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SupplierType = table.Column<int>(type: "int", nullable: true),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ContactNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsExpire = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    ActiveDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CaptureDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AdvanceAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SupplierCashAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StatusDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsRaw = table.Column<bool>(type: "bit", nullable: false),
                    Facebook = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Twitter = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Skype = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PriceLabelId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsAllowEmail = table.Column<bool>(type: "bit", nullable: false),
                    IsAutoEmail = table.Column<bool>(type: "bit", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contacts_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Contacts_Accounts_AdvanceAccountId",
                        column: x => x.AdvanceAccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Contacts_Accounts_SupplierCashAccountId",
                        column: x => x.SupplierCashAccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Contacts_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contacts_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Contacts_CustomerGroups_CustomerGroupId",
                        column: x => x.CustomerGroupId,
                        principalTable: "CustomerGroups",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Contacts_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Contacts_TaxRates_TaxRateId",
                        column: x => x.TaxRateId,
                        principalTable: "TaxRates",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ExpenseTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExpenseTypeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ExpenseTypeArabic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExpenseTypes_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ExpenseTypes_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FinancialYearClosingBalances",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Debit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Credit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FinancialYearClosingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsSoftDelete = table.Column<bool>(type: "bit", nullable: false),
                    ReOpen = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialYearClosingBalances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FinancialYearClosingBalances_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FinancialYearClosingBalances_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FinancialYearClosingBalances_FinancialYearClosings_FinancialYearClosingId",
                        column: x => x.FinancialYearClosingId,
                        principalTable: "FinancialYearClosings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LedgerAccounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameArabic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AccountLedgerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountLeaderType = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsLock = table.Column<bool>(type: "bit", nullable: false),
                    IsLeave = table.Column<bool>(type: "bit", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LedgerAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LedgerAccounts_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LedgerAccounts_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Logistics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnglishName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ArabicName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LicenseNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ContactNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Website = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TermsAndCondition = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Xcoordinates = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ycoordinates = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ports = table.Column<int>(type: "int", nullable: false),
                    ServiceFor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClearanceAgent = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LogisticsForm = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logistics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Logistics_Accounts_ClearanceAgent",
                        column: x => x.ClearanceAgent,
                        principalTable: "Accounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Logistics_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PaymentRefunds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VoucherNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Narration = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChequeNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NatureOfPayment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ApprovalStatus = table.Column<int>(type: "int", nullable: false),
                    PaymentMode = table.Column<int>(type: "int", nullable: false),
                    PaymentMethod = table.Column<int>(type: "int", nullable: false),
                    PurchaseInvoice = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SaleInvoice = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PaymentVoucherId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BankCashAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContactAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TaxMethod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxRateId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Year = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PeriodId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentRefunds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentRefunds_Accounts_BankCashAccountId",
                        column: x => x.BankCashAccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PaymentRefunds_Accounts_ContactAccountId",
                        column: x => x.ContactAccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PaymentRefunds_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PaymentRefunds_TaxRates_TaxRateId",
                        column: x => x.TaxRateId,
                        principalTable: "TaxRates",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PurchaseBills",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RegistrationNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Narration = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Reference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxMethod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApprovalStatus = table.Column<int>(type: "int", nullable: false),
                    PartiallyInvoices = table.Column<int>(type: "int", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    RemainingAmount = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    BillerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BillDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseBills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseBills_Accounts_BillerId",
                        column: x => x.BillerId,
                        principalTable: "Accounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PurchaseBills_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TemporaryCashAllocations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VoucherNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Narration = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChequeNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ApprovalStatus = table.Column<int>(type: "int", nullable: false),
                    PaymentVoucherType = table.Column<int>(type: "int", nullable: false),
                    PaymentMode = table.Column<int>(type: "int", nullable: false),
                    PaymentMethod = table.Column<int>(type: "int", nullable: false),
                    BankCashAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserEmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DraftBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApprovedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RejectBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VoidBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DraftDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ApprovedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RejectDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    VoidDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PeriodId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Year = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BusinessId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CounterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    DayId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemporaryCashAllocations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TemporaryCashAllocations_Accounts_BankCashAccountId",
                        column: x => x.BankCashAccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TemporaryCashAllocations_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DocumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DocumentNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TransactionType = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DocumentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ApprovalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Year = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Debit = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Credit = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    ContactId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PeriodId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BusinessId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CounterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    DayId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Transactions_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transactions_CompanySubmissionPeriods_PeriodId",
                        column: x => x.PeriodId,
                        principalTable: "CompanySubmissionPeriods",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RunPayrollSalaryDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AmountType = table.Column<int>(type: "int", nullable: false),
                    TaxPlan = table.Column<bool>(type: "bit", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Percent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RunPayrollDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RunPayrollSalaryDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RunPayrollSalaryDetails_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RunPayrollSalaryDetails_RunPayrollDetails_RunPayrollDetailId",
                        column: x => x.RunPayrollDetailId,
                        principalTable: "RunPayrollDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StockReceived",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApprovalStatus = table.Column<int>(type: "int", nullable: false),
                    StockTransferStatus = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WarehouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FromBranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StockTransferId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StockRequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    VehicalNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DriverName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DriverNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DriverNationalId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Year = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PeriodId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockReceived", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockReceived_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StockReceived_StockTransfers_StockTransferId",
                        column: x => x.StockTransferId,
                        principalTable: "StockTransfers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Terminals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrinterName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MACAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IPAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyNameEnglish = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BusinessNameEnglish = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BusinessTypeEnglish = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyNameArabic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BusinessNameArabic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BusinessTypeArabic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BusinessLogo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    OverWrite = table.Column<bool>(type: "bit", nullable: false),
                    PosTerminalId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CashAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TerminalType = table.Column<int>(type: "int", nullable: false),
                    OnPageLoadItem = table.Column<bool>(type: "bit", nullable: false),
                    TerminalUserType = table.Column<int>(type: "int", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Terminals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Terminals_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Terminals_Accounts_CashAccountId",
                        column: x => x.CashAccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Terminals_BankPosTerminals_PosTerminalId",
                        column: x => x.PosTerminalId,
                        principalTable: "BankPosTerminals",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Terminals_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TerminalId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OnlineTerminalId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsProceed = table.Column<bool>(type: "bit", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocumentType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Banks_BankId",
                        column: x => x.BankId,
                        principalTable: "Banks",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ChequeBooks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoOfCheques = table.Column<int>(type: "int", nullable: false),
                    StartingNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Used = table.Column<int>(type: "int", nullable: false),
                    Remaining = table.Column<int>(type: "int", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsBlock = table.Column<bool>(type: "bit", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BankId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChequeBooks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChequeBooks_Banks_BankId",
                        column: x => x.BankId,
                        principalTable: "Banks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChequeBooks_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NameArabic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    BankId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Departments_Banks_BankId",
                        column: x => x.BankId,
                        principalTable: "Banks",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Departments_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PrintSettings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PrintSize = table.Column<int>(type: "int", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    WarrantyImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrintTemplate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TermsInAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TermsInEng = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    ReturnDays = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ExchangeDays = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Bank1Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Bank2Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BankAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CashAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PrinterName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsHeaderFooter = table.Column<bool>(type: "bit", nullable: false),
                    EnglishName = table.Column<bool>(type: "bit", nullable: false),
                    ArabicName = table.Column<bool>(type: "bit", nullable: false),
                    CustomerNameEn = table.Column<bool>(type: "bit", nullable: false),
                    CustomerNameAr = table.Column<bool>(type: "bit", nullable: false),
                    ItemDescription = table.Column<bool>(type: "bit", nullable: false),
                    CustomerAddress = table.Column<bool>(type: "bit", nullable: false),
                    CustomerVat = table.Column<bool>(type: "bit", nullable: false),
                    CustomerNumber = table.Column<bool>(type: "bit", nullable: false),
                    CargoName = table.Column<bool>(type: "bit", nullable: false),
                    CustomerTelephone = table.Column<bool>(type: "bit", nullable: false),
                    ItemPieceSize = table.Column<bool>(type: "bit", nullable: false),
                    StyleNo = table.Column<bool>(type: "bit", nullable: false),
                    BlindPrint = table.Column<bool>(type: "bit", nullable: false),
                    ShowBankAccount = table.Column<bool>(type: "bit", nullable: false),
                    BankAccount1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankIcon1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HeaderImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HeaderImage1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TagsImages = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProposalImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FooterImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankAccount2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankIcon2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InvoicePrint = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WelcomeLineEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WelcomeLineAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ManagementNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClosingLineEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClosingLineAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BusinessAddressEnglish = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BusinessAddressArabic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WalkInInvoiceType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsBlindPrint = table.Column<bool>(type: "bit", nullable: false),
                    AutoPaymentVoucher = table.Column<bool>(type: "bit", nullable: false),
                    IsDeliveryNote = table.Column<bool>(type: "bit", nullable: false),
                    IsQuotationPrint = table.Column<bool>(type: "bit", nullable: false),
                    TermAndConditionLength = table.Column<bool>(type: "bit", nullable: false),
                    WithSubTotal = table.Column<bool>(type: "bit", nullable: false),
                    ContinueWithPage = table.Column<bool>(type: "bit", nullable: false),
                    SubTotalWithDashes = table.Column<bool>(type: "bit", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TaxRateId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TaxMethod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiscountTypeOption = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrintSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrintSettings_Accounts_BankAccountId",
                        column: x => x.BankAccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PrintSettings_Accounts_CashAccountId",
                        column: x => x.CashAccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PrintSettings_Banks_Bank1Id",
                        column: x => x.Bank1Id,
                        principalTable: "Banks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PrintSettings_Banks_Bank2Id",
                        column: x => x.Bank2Id,
                        principalTable: "Banks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PrintSettings_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SubCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NameArabic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubCategories_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AutoPurchaseTemplates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RegistrationNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Day = table.Column<int>(type: "int", nullable: false),
                    SupplierId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    TaxRateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WareHouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TaxMethod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Raw = table.Column<bool>(type: "bit", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AutoPurchaseTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AutoPurchaseTemplates_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AutoPurchaseTemplates_Contacts_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContactAttachments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CommercialRegistration = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VATCertificate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ZakatCertificate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ContactId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactAttachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContactAttachments_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContactAttachments_Contacts_ContactId",
                        column: x => x.ContactId,
                        principalTable: "Contacts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ContactBankAccounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Iban = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameOfBank = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BranchName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoutingCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactBankAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContactBankAccounts_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContactBankAccounts_Contacts_ContactId",
                        column: x => x.ContactId,
                        principalTable: "Contacts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ContactPersons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Prefix = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactPersons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContactPersons_Contacts_ContactId",
                        column: x => x.ContactId,
                        principalTable: "Contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryAddresses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContactId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Langitutue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Latitude = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GoogleLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NearBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FromTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ToTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BillingZipCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Area = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BillingFax = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsOffice = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    AllDaySelection = table.Column<bool>(type: "bit", nullable: false),
                    AllHour = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryAddresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeliveryAddresses_Contacts_ContactId",
                        column: x => x.ContactId,
                        principalTable: "Contacts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "JournalVoucherItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Debit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Credit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentMode = table.Column<int>(type: "int", nullable: false),
                    PaymentMethod = table.Column<int>(type: "int", nullable: false),
                    ChequeNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ChequeNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JournalVoucherId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JournalVoucherItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JournalVoucherItems_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JournalVoucherItems_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JournalVoucherItems_Contacts_ContactId",
                        column: x => x.ContactId,
                        principalTable: "Contacts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_JournalVoucherItems_JournalVouchers_JournalVoucherId",
                        column: x => x.JournalVoucherId,
                        principalTable: "JournalVouchers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseOrders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RegistrationNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SupplierId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InvoiceNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InvoiceDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ApprovalStatus = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsClose = table.Column<bool>(type: "bit", nullable: false),
                    TaxRateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TaxMethod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Raw = table.Column<bool>(type: "bit", nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransactionLevelDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsDiscountOnTransaction = table.Column<bool>(type: "bit", nullable: false),
                    IsFixed = table.Column<bool>(type: "bit", nullable: false),
                    IsBeforeTax = table.Column<bool>(type: "bit", nullable: false),
                    IsProcessed = table.Column<bool>(type: "bit", nullable: false),
                    PartiallyReceived = table.Column<int>(type: "int", nullable: false),
                    TotalAfterDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalWithOutAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VatAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiscountAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DocumentType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Reference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SupplierQuotationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeliveryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ShippingId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BillingId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    NationalId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Year = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PeriodId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseOrders_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseOrders_Contacts_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SaleOrders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidityDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DispatchDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PickUpDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RegistrationNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuotationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    WareHouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PartiallyInvoices = table.Column<int>(type: "int", nullable: false),
                    Refrence = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentMethod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SheduleDelivery = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Days = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApprovalStatus = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoteAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BarCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsFreight = table.Column<bool>(type: "bit", nullable: false),
                    IsLabour = table.Column<bool>(type: "bit", nullable: false),
                    IsClose = table.Column<bool>(type: "bit", nullable: false),
                    InvoiceNote = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientPurchaseNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsQuotation = table.Column<bool>(type: "bit", nullable: false),
                    LogisticId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CustomerAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsService = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IncotermsId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CommodityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Commodities = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NatureOfCargo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Attn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Attiendie = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    For = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Purpose = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OneTimeDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuotationValidDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FreeTimePOL = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FreeTimePOD = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TaxMethod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxRateId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    WarrantyLogoPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransactionLevelDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsDiscountOnTransaction = table.Column<bool>(type: "bit", nullable: false),
                    IsFixed = table.Column<bool>(type: "bit", nullable: false),
                    IsBeforeTax = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TerminalId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsSaleOrderTracking = table.Column<bool>(type: "bit", nullable: false),
                    DeliveryNoteAndDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuotationValidUpto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PerfomaValidUpto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerInquiry = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PoNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PoDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReferredBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefrenceNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DoctorName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HospitalName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuotationNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SaleOrderNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PeroformaInvoiceNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PurchaseOrderNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InquiryNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeliveryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ShippingId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BillingId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    NationalId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SaleOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProformaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PurchaseOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    InquiryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeliveryChallanId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    NoteDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsProcessed = table.Column<bool>(type: "bit", nullable: false),
                    ProcessedNote = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalAfterDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalWithOutAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VatAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiscountAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsUsedForBom = table.Column<bool>(type: "bit", nullable: false),
                    Year = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PeriodId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SaleOrders_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SaleOrders_Contacts_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SaleOrders_ImportExportTypes_CommodityId",
                        column: x => x.CommodityId,
                        principalTable: "ImportExportTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SaleOrders_ImportExportTypes_IncotermsId",
                        column: x => x.IncotermsId,
                        principalTable: "ImportExportTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SaleOrders_ImportExportTypes_OrderTypeId",
                        column: x => x.OrderTypeId,
                        principalTable: "ImportExportTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SaleOrders_Logistics_LogisticId",
                        column: x => x.LogisticId,
                        principalTable: "Logistics",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SaleOrders_TaxRates_TaxRateId",
                        column: x => x.TaxRateId,
                        principalTable: "TaxRates",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BillAttachments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameArabic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PurchaseBillId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillAttachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BillAttachments_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BillAttachments_PurchaseBills_PurchaseBillId",
                        column: x => x.PurchaseBillId,
                        principalTable: "PurchaseBills",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DailyExpenses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VoucherNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDraft = table.Column<bool>(type: "bit", nullable: false),
                    ApprovalStatus = table.Column<int>(type: "int", nullable: false),
                    OperationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExpenseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OperationPerson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsExpenseAccount = table.Column<bool>(type: "bit", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymetType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OperationBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BillerAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PaymentMode = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReferenceNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GrossAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalVat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Year = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PeriodId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BusinessId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CounterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    DayId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyExpenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DailyExpenses_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DailyExpenses_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DailyExpenses_PurchaseBills_BillerAccountId",
                        column: x => x.BillerAccountId,
                        principalTable: "PurchaseBills",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PurchaseBillItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PurchaseBillId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseBillItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseBillItems_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PurchaseBillItems_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseBillItems_PurchaseBills_PurchaseBillId",
                        column: x => x.PurchaseBillId,
                        principalTable: "PurchaseBills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TerminalCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TerminalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TerminalCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TerminalCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TerminalCategories_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TerminalCategories_Terminals_TerminalId",
                        column: x => x.TerminalId,
                        principalTable: "Terminals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTerminals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TerminalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTerminals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserTerminals_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserTerminals_Terminals_TerminalId",
                        column: x => x.TerminalId,
                        principalTable: "Terminals",
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
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                name: "ChequeBookItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BookNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SerialNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChequeNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsUsed = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsBlock = table.Column<bool>(type: "bit", nullable: false),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BankId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChequeDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AlertDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StatusDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidityDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IssuedTo = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IssuerAccount = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ReservedAccount = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IssuedToName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShortDetail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ChequeType = table.Column<int>(type: "int", nullable: false),
                    ChequeTypeDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StatusType = table.Column<int>(type: "int", nullable: false),
                    StatusTypeDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CashType = table.Column<int>(type: "int", nullable: false),
                    CashTypeDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ChequeBookId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsCashed = table.Column<bool>(type: "bit", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChequeBookItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChequeBookItems_ChequeBooks_ChequeBookId",
                        column: x => x.ChequeBookId,
                        principalTable: "ChequeBooks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChequeBookItems_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeDepartments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DepartmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeDepartments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeDepartments_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeDepartments_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeDepartments_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeRegistrations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnglishName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FatherName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ArabicName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MartialStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nationality = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MobileNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OtherContact = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BloodGroup = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IbanNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BranchName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeAccess = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrimaryNameOfPerson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrimaryRelation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrimaryContactNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrimaryReferenceEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecondaryNameOfPerson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecondaryRelation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecondaryContactNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecondaryReferenceEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HomePersonName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HomeRelation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HomeContactNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HomeReferenceEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HomeCity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HomeCountry = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IDNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IDType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpiryDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PassportNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PassportIssueDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PassportExpiryDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PassportIssuingAuthority = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DrivingLicenseNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DrivingLicenseType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DrivingExpiryDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DrivingIssuingAuthority = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MedicalPolicNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MedicalPolicType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MedicalPolicProvider = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeType = table.Column<int>(type: "int", nullable: false),
                    MedicalPolicExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PayableAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EmployeeAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DesignationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DepartmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    SalaryType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NationalOrForeign = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SpouseName1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SpouseName2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SpouseName3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SpouseName4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TemporaryCashReceiver = table.Column<bool>(type: "bit", nullable: false),
                    TemporaryCashIssuer = table.Column<bool>(type: "bit", nullable: false),
                    TemporaryCashRequester = table.Column<bool>(type: "bit", nullable: false),
                    Days = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Limit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PerDayWorkHour = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeRegistrations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeRegistrations_Accounts_EmployeeAccountId",
                        column: x => x.EmployeeAccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeRegistrations_Accounts_PayableAccountId",
                        column: x => x.PayableAccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeRegistrations_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeRegistrations_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EmployeeRegistrations_Designations_DesignationId",
                        column: x => x.DesignationId,
                        principalTable: "Designations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PrintOptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PrintSettingId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Label = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LabelNameArabic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<bool>(type: "bit", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrintOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrintOptions_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PrintOptions_PrintSettings_PrintSettingId",
                        column: x => x.PrintSettingId,
                        principalTable: "PrintSettings",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DeliveryHolidays",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeliveryAddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    WeekHolidayId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryHolidays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeliveryHolidays_DeliveryAddresses_DeliveryAddressId",
                        column: x => x.DeliveryAddressId,
                        principalTable: "DeliveryAddresses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DeliveryHolidays_WeekHolidays_WeekHolidayId",
                        column: x => x.WeekHolidayId,
                        principalTable: "WeekHolidays",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GoodReceives",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RegistrationNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PurchaseOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SaleOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BomId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PartiallyReceived = table.Column<int>(type: "int", nullable: false),
                    SupplierId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApprovedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InvoiceNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InvoiceDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ApprovalStatus = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsClose = table.Column<bool>(type: "bit", nullable: false),
                    TaxMethod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Raw = table.Column<bool>(type: "bit", nullable: false),
                    TaxRateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsCredit = table.Column<bool>(type: "bit", nullable: false),
                    TotalAfterDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalWithOutAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VatAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiscountAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ReceivedAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SupplierQuotationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PoNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PoDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransactionLevelDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsDiscountOnTransaction = table.Column<bool>(type: "bit", nullable: false),
                    IsFixed = table.Column<bool>(type: "bit", nullable: false),
                    IsBeforeTax = table.Column<bool>(type: "bit", nullable: false),
                    PaymentType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankCashAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsProcessed = table.Column<bool>(type: "bit", nullable: false),
                    Reference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeliveryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ShippingId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BillingId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    NationalId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Year = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PeriodId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodReceives", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GoodReceives_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GoodReceives_Contacts_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GoodReceives_PurchaseOrders_PurchaseOrderId",
                        column: x => x.PurchaseOrderId,
                        principalTable: "PurchaseOrders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PurchaseAttachments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameArabic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PurchaseOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseAttachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseAttachments_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseAttachments_PurchaseOrders_PurchaseOrderId",
                        column: x => x.PurchaseOrderId,
                        principalTable: "PurchaseOrders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PurchaseOrderActions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProcessId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PurchaseOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CurrentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrderActions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderActions_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderActions_CompanyProcess_ProcessId",
                        column: x => x.ProcessId,
                        principalTable: "CompanyProcess",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderActions_PurchaseOrders_PurchaseOrderId",
                        column: x => x.PurchaseOrderId,
                        principalTable: "PurchaseOrders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PurchaseOrderVersions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Version = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PurchaseOrderVersionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PurchaseOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrderVersions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderVersions_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderVersions_PurchaseOrders_PurchaseOrderId",
                        column: x => x.PurchaseOrderId,
                        principalTable: "PurchaseOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Purchases",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RegistrationNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SupplierId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InvoiceNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsPurchaseReturn = table.Column<bool>(type: "bit", nullable: false),
                    IsPost = table.Column<bool>(type: "bit", nullable: false),
                    InvoiceDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PurchaseOrderNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PurchaseOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TaxRateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TaxMethod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WareHouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InvoiceFixDiscount = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Raw = table.Column<bool>(type: "bit", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SaleOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Year = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PeriodId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Purchases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Purchases_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Purchases_Contacts_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Purchases_PurchaseOrders_PurchaseOrderId",
                        column: x => x.PurchaseOrderId,
                        principalTable: "PurchaseOrders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DispatchNotes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RegistrationNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Refrence = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApprovalStatus = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsClose = table.Column<bool>(type: "bit", nullable: false),
                    SaleOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Year = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PeriodId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DispatchNotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DispatchNotes_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DispatchNotes_Contacts_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DispatchNotes_SaleOrders_SaleOrderId",
                        column: x => x.SaleOrderId,
                        principalTable: "SaleOrders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ImportExportItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StuffingLocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PortOfLoadingId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PortOfDestinationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CarrierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ft = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Etd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SaleOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImportExportItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImportExportItems_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ImportExportItems_ImportExportTypes_CarrierId",
                        column: x => x.CarrierId,
                        principalTable: "ImportExportTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ImportExportItems_ImportExportTypes_PortOfDestinationId",
                        column: x => x.PortOfDestinationId,
                        principalTable: "ImportExportTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ImportExportItems_ImportExportTypes_PortOfLoadingId",
                        column: x => x.PortOfLoadingId,
                        principalTable: "ImportExportTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ImportExportItems_ImportExportTypes_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "ImportExportTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ImportExportItems_ImportExportTypes_StuffingLocationId",
                        column: x => x.StuffingLocationId,
                        principalTable: "ImportExportTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ImportExportItems_SaleOrders_SaleOrderId",
                        column: x => x.SaleOrderId,
                        principalTable: "SaleOrders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SaleOrderVersions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DocumentNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Version = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SaleOrderVersionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsQuotation = table.Column<bool>(type: "bit", nullable: false),
                    SaleOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleOrderVersions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SaleOrderVersions_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SaleOrderVersions_SaleOrders_SaleOrderId",
                        column: x => x.SaleOrderId,
                        principalTable: "SaleOrders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DailyExpenseAttachments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameArabic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DailyExpenseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyExpenseAttachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DailyExpenseAttachments_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DailyExpenseAttachments_DailyExpenses_DailyExpenseId",
                        column: x => x.DailyExpenseId,
                        principalTable: "DailyExpenses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DailyExpenseDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DailyExpenseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TaxMethod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VatId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ExpenseAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyExpenseDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DailyExpenseDetails_Accounts_ExpenseAccountId",
                        column: x => x.ExpenseAccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DailyExpenseDetails_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DailyExpenseDetails_DailyExpenses_DailyExpenseId",
                        column: x => x.DailyExpenseId,
                        principalTable: "DailyExpenses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DailyExpenseDetails_TaxRates_VatId",
                        column: x => x.VatId,
                        principalTable: "TaxRates",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ApprovalSystemEmployees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ApprovalSystemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApprovalSystemEmployees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApprovalSystemEmployees_ApprovalSystems_ApprovalSystemId",
                        column: x => x.ApprovalSystemId,
                        principalTable: "ApprovalSystems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApprovalSystemEmployees_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ApprovalSystemEmployees_EmployeeRegistrations_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "EmployeeRegistrations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EmployeeRegistrationAttachments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CNIC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Passport = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DrivingLicense = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeRegistrationAttachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeRegistrationAttachments_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeRegistrationAttachments_EmployeeRegistrations_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "EmployeeRegistrations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeSalaries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SalaryType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PayPeriodId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BaseSalary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WeekdayHourlySalary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WeekendDayHourlySalary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SalaryTemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IncomeTax = table.Column<bool>(type: "bit", nullable: false),
                    AutoIncomeTax = table.Column<bool>(type: "bit", nullable: false),
                    GosiRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeSalaries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeSalaries_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeSalaries_EmployeeRegistrations_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "EmployeeRegistrations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Inquiries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InquiryNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DueDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Reference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TermAndCondition = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsTerm = table.Column<bool>(type: "bit", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ReceiverId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    HandlerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ReferedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MediaTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    InquiryPriorityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    InquiryStatusDynamicId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    InquiryProcessId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    InquiryTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inquiries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Inquiries_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Inquiries_Contacts_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Contacts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Inquiries_EmployeeRegistrations_ReferedBy",
                        column: x => x.ReferedBy,
                        principalTable: "EmployeeRegistrations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Inquiries_InquiryPriorities_InquiryPriorityId",
                        column: x => x.InquiryPriorityId,
                        principalTable: "InquiryPriorities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Inquiries_InquiryProcesses_InquiryProcessId",
                        column: x => x.InquiryProcessId,
                        principalTable: "InquiryProcesses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Inquiries_InquiryStatusDynamics_InquiryStatusDynamicId",
                        column: x => x.InquiryStatusDynamicId,
                        principalTable: "InquiryStatusDynamics",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Inquiries_InquiryTypes_InquiryTypeId",
                        column: x => x.InquiryTypeId,
                        principalTable: "InquiryTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Inquiries_MediaTypes_MediaTypeId",
                        column: x => x.MediaTypeId,
                        principalTable: "MediaTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LeaveGroupEmployees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LeaveGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveGroupEmployees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeaveGroupEmployees_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LeaveGroupEmployees_EmployeeRegistrations_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "EmployeeRegistrations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LeaveGroupEmployees_LeaveGroups_LeaveGroupId",
                        column: x => x.LeaveGroupId,
                        principalTable: "LeaveGroups",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LeaveRules",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LeaveTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LeaveGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DesignationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RequiredExperience = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DepartmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LeavePeriodId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LeavesPerLeavePeriod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdminAssignLeave = table.Column<bool>(type: "bit", nullable: false),
                    EmployeesApplyForLeaveType = table.Column<bool>(type: "bit", nullable: false),
                    BeyondCurrentLeaveBalance = table.Column<bool>(type: "bit", nullable: false),
                    LeaveAccrueEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LeaveCarriedForward1 = table.Column<bool>(type: "bit", nullable: false),
                    PercentageLeaveCF = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaximumCFAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CFLeaveAvailabilityPeriod1 = table.Column<int>(type: "int", nullable: false),
                    ProportionateLeaves = table.Column<bool>(type: "bit", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveRules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeaveRules_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LeaveRules_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LeaveRules_Designations_DesignationId",
                        column: x => x.DesignationId,
                        principalTable: "Designations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LeaveRules_EmployeeRegistrations_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "EmployeeRegistrations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LeaveRules_LeaveGroups_LeaveGroupId",
                        column: x => x.LeaveGroupId,
                        principalTable: "LeaveGroups",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LeaveRules_LeavePeriods_LeavePeriodId",
                        column: x => x.LeavePeriodId,
                        principalTable: "LeavePeriods",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LeaveRules_LeaveTypes_LeaveTypeId",
                        column: x => x.LeaveTypeId,
                        principalTable: "LeaveTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LoanPayments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeRegistrationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LoanType = table.Column<int>(type: "int", nullable: false),
                    RecoveryMethod = table.Column<int>(type: "int", nullable: false),
                    InstallmentMethod = table.Column<int>(type: "int", nullable: false),
                    LoanTakenDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LoanAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RemainingLoan = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Payment = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EmployeeSalary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DeductionValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RecoveryLoanAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InstallmentBaseSalary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ProvidentFundType = table.Column<bool>(type: "bit", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanPayments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoanPayments_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LoanPayments_EmployeeRegistrations_EmployeeRegistrationId",
                        column: x => x.EmployeeRegistrationId,
                        principalTable: "EmployeeRegistrations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ManualAttendances",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CheckIn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CheckOut = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TotalHour = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OverTime = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsOnLeave = table.Column<bool>(type: "bit", nullable: false),
                    IsAbsent = table.Column<bool>(type: "bit", nullable: false),
                    IsCheckIn = table.Column<bool>(type: "bit", nullable: false),
                    IsCheckOut = table.Column<bool>(type: "bit", nullable: false),
                    IsSmsSend = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManualAttendances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ManualAttendances_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ManualAttendances_EmployeeRegistrations_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "EmployeeRegistrations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MultiUps",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RegistrationNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstimateAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AdvanceAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CashAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsCashed = table.Column<bool>(type: "bit", nullable: false),
                    PaymentType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReceivedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExpectedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CompleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    JobAssignId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RegisteredById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsComplete = table.Column<bool>(type: "bit", nullable: false),
                    IsCollected = table.Column<bool>(type: "bit", nullable: false),
                    IsRepared = table.Column<bool>(type: "bit", nullable: false),
                    IsReturn = table.Column<bool>(type: "bit", nullable: false),
                    IsPrint = table.Column<bool>(type: "bit", nullable: false),
                    CradNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PurchaseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DealerRef = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FaultInfo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RemaningPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MultiUps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MultiUps_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MultiUps_Contacts_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Contacts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MultiUps_EmployeeRegistrations_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "EmployeeRegistrations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PaidTimeOffs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LeaveType = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LeavePeriodId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaidTimeOffs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaidTimeOffs_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PaidTimeOffs_EmployeeRegistrations_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "EmployeeRegistrations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PaidTimeOffs_LeavePeriods_LeavePeriodId",
                        column: x => x.LeavePeriodId,
                        principalTable: "LeavePeriods",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PaidTimeOffs_LeaveTypes_LeaveType",
                        column: x => x.LeaveType,
                        principalTable: "LeaveTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProcessContractors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContractorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProcessId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ContractorId2 = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessContractors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProcessContractors_Contractors_ContractorId2",
                        column: x => x.ContractorId2,
                        principalTable: "Contractors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProcessContractors_EmployeeRegistrations_ContractorId",
                        column: x => x.ContractorId,
                        principalTable: "EmployeeRegistrations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProcessContractors_Processes_ProcessId",
                        column: x => x.ProcessId,
                        principalTable: "Processes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ReparingOrders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RegistrationNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    WarrantyCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpsDescriptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProblemId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AcessoryIncludedId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SerialNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RemaningPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstimateAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AdvanceAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CashAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsCashed = table.Column<bool>(type: "bit", nullable: false),
                    PaymentType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReceivedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExpectedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CompleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    JobAssignId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RegisteredById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsComplete = table.Column<bool>(type: "bit", nullable: false),
                    IsCollected = table.Column<bool>(type: "bit", nullable: false),
                    IsRepared = table.Column<bool>(type: "bit", nullable: false),
                    IsReturn = table.Column<bool>(type: "bit", nullable: false),
                    IsPrint = table.Column<bool>(type: "bit", nullable: false),
                    CradNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PurchaseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DealerRef = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FaultInfo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Year = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PeriodId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReparingOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReparingOrders_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReparingOrders_Contacts_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Contacts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReparingOrders_EmployeeRegistrations_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "EmployeeRegistrations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReparingOrders_ReparingOrderTypes_AcessoryIncludedId",
                        column: x => x.AcessoryIncludedId,
                        principalTable: "ReparingOrderTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReparingOrders_ReparingOrderTypes_ProblemId",
                        column: x => x.ProblemId,
                        principalTable: "ReparingOrderTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReparingOrders_ReparingOrderTypes_UpsDescriptionId",
                        column: x => x.UpsDescriptionId,
                        principalTable: "ReparingOrderTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReparingOrders_ReparingOrderTypes_WarrantyCategoryId",
                        column: x => x.WarrantyCategoryId,
                        principalTable: "ReparingOrderTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Sales",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PoNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PoDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DispatchDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PickUpDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CustomerInquiry = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeliveryAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BillingAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReferredBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegistrationNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RefrenceNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxMethod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DoctorName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeliveryNoteAndDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuotationValidUpto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PerfomaValidUpto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HospitalName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuotationNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SaleOrderNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PeroformaInvoiceNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PurchaseOrderNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InquiryNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeliveryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ShippingId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BillingId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    NationalId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AreaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CashCustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsCredit = table.Column<bool>(type: "bit", nullable: false),
                    IsPurchaseOrder = table.Column<bool>(type: "bit", nullable: false),
                    WareHouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InvoiceType = table.Column<int>(type: "int", nullable: false),
                    DocumentType = table.Column<int>(type: "int", nullable: false),
                    OtherCurrencyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SaleInvoiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SaleReturnInvoiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsSaleReturn = table.Column<bool>(type: "bit", nullable: false),
                    IsSaleReturnPost = table.Column<bool>(type: "bit", nullable: false),
                    PeriodId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Change = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnRegisteredVatId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UnRegisteredVAtAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalWithOutAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalAfterDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DueAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ReceivedAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VatAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiscountAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsVatChange = table.Column<bool>(type: "bit", nullable: false),
                    BarCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PartiallyInvoices = table.Column<int>(type: "int", nullable: false),
                    SaleOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    QuotationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LogisticId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CustomerAddressWalkIn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TermAndCondition = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TermAndConditionAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WarrantyLogoPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageSizeHeight = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageSizeWidth = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InvoiceNote = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsCashCustomer = table.Column<bool>(type: "bit", nullable: false),
                    IsOverWrite = table.Column<bool>(type: "bit", nullable: false),
                    IsService = table.Column<bool>(type: "bit", nullable: false),
                    IsWarranty = table.Column<bool>(type: "bit", nullable: false),
                    TaxRateId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransactionLevelDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsDiscountOnTransaction = table.Column<bool>(type: "bit", nullable: false),
                    IsFixed = table.Column<bool>(type: "bit", nullable: false),
                    IsBeforeTax = table.Column<bool>(type: "bit", nullable: false),
                    IsProformaInvoice = table.Column<bool>(type: "bit", nullable: false),
                    MarkAsCompleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TerminalId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeliveryChallanId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    InquiryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProformaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PurchaseOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsProcessed = table.Column<bool>(type: "bit", nullable: false),
                    ProcessedNote = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsSendMsg = table.Column<bool>(type: "bit", nullable: false),
                    IsMsgSended = table.Column<bool>(type: "bit", nullable: false),
                    IsClose = table.Column<bool>(type: "bit", nullable: false),
                    IgnoreCashCreditForNumber = table.Column<bool>(type: "bit", nullable: false),
                    SaleHoldTypes = table.Column<int>(type: "int", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Year = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BusinessId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CounterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "GetUtcDate()"),
                    DayId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "GetUtcDate()"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sales_CashCustomer_CashCustomerId",
                        column: x => x.CashCustomerId,
                        principalTable: "CashCustomer",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Sales_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sales_Contacts_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Contacts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Sales_EmployeeRegistrations_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "EmployeeRegistrations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Sales_Logistics_LogisticId",
                        column: x => x.LogisticId,
                        principalTable: "Logistics",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Sales_OtherCurrencies_OtherCurrencyId",
                        column: x => x.OtherCurrencyId,
                        principalTable: "OtherCurrencies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Sales_Regions_AreaId",
                        column: x => x.AreaId,
                        principalTable: "Regions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Sales_SaleOrders_QuotationId",
                        column: x => x.QuotationId,
                        principalTable: "SaleOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sales_SaleOrders_SaleOrderId",
                        column: x => x.SaleOrderId,
                        principalTable: "SaleOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sales_TaxRates_TaxRateId",
                        column: x => x.TaxRateId,
                        principalTable: "TaxRates",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Sales_TaxRates_UnRegisteredVatId",
                        column: x => x.UnRegisteredVatId,
                        principalTable: "TaxRates",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ShiftAssigns",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShiftName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    FromDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ToDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReasonOfClosingShift = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Monday = table.Column<bool>(type: "bit", nullable: false),
                    Tuesday = table.Column<bool>(type: "bit", nullable: false),
                    Wednesday = table.Column<bool>(type: "bit", nullable: false),
                    Thursday = table.Column<bool>(type: "bit", nullable: false),
                    Friday = table.Column<bool>(type: "bit", nullable: false),
                    Saturday = table.Column<bool>(type: "bit", nullable: false),
                    Sunday = table.Column<bool>(type: "bit", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShiftAssigns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShiftAssigns_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ShiftAssigns_EmployeeRegistrations_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "EmployeeRegistrations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PurchasePosts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RegistrationNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SupplierId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InvoiceNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsPurchaseReturn = table.Column<bool>(type: "bit", nullable: false),
                    InvoiceDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PurchaseOrderNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PurchaseOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GoodReceiveNoteId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    WareHouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InvoiceFixDiscount = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    TaxRateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PurchaseInvoiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TaxMethod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Raw = table.Column<bool>(type: "bit", nullable: false),
                    IsClose = table.Column<bool>(type: "bit", nullable: false),
                    IsPurchasePost = table.Column<bool>(type: "bit", nullable: false),
                    ExpenseUse = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsCost = table.Column<bool>(type: "bit", nullable: false),
                    PeriodId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PartiallyInvoices = table.Column<int>(type: "int", nullable: false),
                    PaymentType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankCashAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransactionLevelDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsDiscountOnTransaction = table.Column<bool>(type: "bit", nullable: false),
                    IsFixed = table.Column<bool>(type: "bit", nullable: false),
                    IsBeforeTax = table.Column<bool>(type: "bit", nullable: false),
                    IsCredit = table.Column<bool>(type: "bit", nullable: false),
                    TotalAfterDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalWithOutAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VatAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiscountAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ReceivedAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SupplierQuotationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    poNumberAndDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GoodsRecieveNumberAndDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Reference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeliveryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ShippingId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BillingId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    NationalId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Year = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchasePosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchasePosts_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchasePosts_Contacts_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchasePosts_GoodReceives_GoodReceiveNoteId",
                        column: x => x.GoodReceiveNoteId,
                        principalTable: "GoodReceives",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PurchasePosts_PurchaseOrders_PurchaseOrderId",
                        column: x => x.PurchaseOrderId,
                        principalTable: "PurchaseOrders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EmployeeSalaryDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AmountType = table.Column<int>(type: "int", nullable: false),
                    TaxPlan = table.Column<bool>(type: "bit", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Percent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EmployeeSalaryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeSalaryDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeSalaryDetails_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeSalaryDetails_EmployeeSalaries_EmployeeSalaryId",
                        column: x => x.EmployeeSalaryId,
                        principalTable: "EmployeeSalaries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InquiryComments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InquiryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReplyCommentedId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InquiryComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InquiryComments_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InquiryComments_Inquiries_InquiryId",
                        column: x => x.InquiryId,
                        principalTable: "Inquiries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InquiryComments_InquiryComments_ReplyCommentedId",
                        column: x => x.ReplyCommentedId,
                        principalTable: "InquiryComments",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "InquiryEmails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsReceived = table.Column<bool>(type: "bit", nullable: false),
                    InquiryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InquiryEmails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InquiryEmails_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InquiryEmails_Inquiries_InquiryId",
                        column: x => x.InquiryId,
                        principalTable: "Inquiries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InquiryMeetings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Agenda = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InquiryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InquiryMeetingStatus = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InquiryMeetings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InquiryMeetings_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InquiryMeetings_Inquiries_InquiryId",
                        column: x => x.InquiryId,
                        principalTable: "Inquiries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InquiryModuleQuestions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InquiryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InquiryModuleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Question = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Answer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Answer1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Answer2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Answer3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Answer4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Answer5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Answer6 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Answer7 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Answer8 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Answer9 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InquiryModuleQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InquiryModuleQuestions_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InquiryModuleQuestions_Inquiries_InquiryId",
                        column: x => x.InquiryId,
                        principalTable: "Inquiries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InquiryModuleQuestions_InquiryModules_InquiryModuleId",
                        column: x => x.InquiryModuleId,
                        principalTable: "InquiryModules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InquiryStatuses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InquiryStatusDynamicId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    InquiryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InquiryStatuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InquiryStatuses_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InquiryStatuses_Inquiries_InquiryId",
                        column: x => x.InquiryId,
                        principalTable: "Inquiries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InquiryStatuses_InquiryStatusDynamics_InquiryStatusDynamicId",
                        column: x => x.InquiryStatusDynamicId,
                        principalTable: "InquiryStatusDynamics",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LoanPays",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RemainingLoan = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LoanPaymentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanPays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoanPays_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LoanPays_LoanPayments_LoanPaymentId",
                        column: x => x.LoanPaymentId,
                        principalTable: "LoanPayments",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MultiUPSLineItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WarrantyCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpsDescriptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProblemId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AcessoryIncludedId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JobAssignId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EstimateAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AdvanceAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CashAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsCashed = table.Column<bool>(type: "bit", nullable: false),
                    PaymentType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SerialNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReceivedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExpectedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CompleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsComplete = table.Column<bool>(type: "bit", nullable: false),
                    IsCollected = table.Column<bool>(type: "bit", nullable: false),
                    IsRepared = table.Column<bool>(type: "bit", nullable: false),
                    IsReturn = table.Column<bool>(type: "bit", nullable: false),
                    IsPrint = table.Column<bool>(type: "bit", nullable: false),
                    MultiUpId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MultiUPSLineItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MultiUPSLineItems_MultiUps_MultiUpId",
                        column: x => x.MultiUpId,
                        principalTable: "MultiUps",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MultiUPSLineItems_ReparingOrderTypes_AcessoryIncludedId",
                        column: x => x.AcessoryIncludedId,
                        principalTable: "ReparingOrderTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MultiUPSLineItems_ReparingOrderTypes_ProblemId",
                        column: x => x.ProblemId,
                        principalTable: "ReparingOrderTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MultiUPSLineItems_ReparingOrderTypes_UpsDescriptionId",
                        column: x => x.UpsDescriptionId,
                        principalTable: "ReparingOrderTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MultiUPSLineItems_ReparingOrderTypes_WarrantyCategoryId",
                        column: x => x.WarrantyCategoryId,
                        principalTable: "ReparingOrderTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PaymentVouchers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VoucherNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Narration = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ChequeNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NatureOfPayment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    ApprovalStatus = table.Column<int>(type: "int", nullable: false),
                    PettyCash = table.Column<int>(type: "int", nullable: false),
                    PaymentVoucherType = table.Column<int>(type: "int", nullable: false),
                    PaymentMode = table.Column<int>(type: "int", nullable: false),
                    PaymentMethod = table.Column<int>(type: "int", nullable: false),
                    PurchaseInvoice = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SaleInvoice = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BankCashAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContactAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VatAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BillsId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ReparingOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DraftBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransactionId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Prefix = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApprovedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RejectBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VoidBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DraftDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ApprovedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RejectDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    VoidDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PeriodId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TaxMethod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxRateId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MultiUpId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DocumentType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsRefund = table.Column<bool>(type: "bit", nullable: false),
                    InvoiceAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RemainingBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Year = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BusinessId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CounterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    DayId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentVouchers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentVouchers_Accounts_BankCashAccountId",
                        column: x => x.BankCashAccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PaymentVouchers_Accounts_ContactAccountId",
                        column: x => x.ContactAccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PaymentVouchers_Accounts_VatAccountId",
                        column: x => x.VatAccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PaymentVouchers_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PaymentVouchers_MultiUps_MultiUpId",
                        column: x => x.MultiUpId,
                        principalTable: "MultiUps",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PaymentVouchers_PurchaseBills_BillsId",
                        column: x => x.BillsId,
                        principalTable: "PurchaseBills",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PaymentVouchers_ReparingOrders_ReparingOrderId",
                        column: x => x.ReparingOrderId,
                        principalTable: "ReparingOrders",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PaymentVouchers_TaxRates_TaxRateId",
                        column: x => x.TaxRateId,
                        principalTable: "TaxRates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryChallanReserveds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsClose = table.Column<bool>(type: "bit", nullable: false),
                    IsService = table.Column<bool>(type: "bit", nullable: false),
                    BilingAddress = table.Column<bool>(type: "bit", nullable: false),
                    CustomerAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegistrationNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SaleOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SaleInvoiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeliveryChallanId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TemplateName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApprovalStatus = table.Column<int>(type: "int", nullable: false),
                    DeliveryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ShippingId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BillingId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    NationalId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryChallanReserveds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeliveryChallanReserveds_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeliveryChallanReserveds_Sales_SaleInvoiceId",
                        column: x => x.SaleInvoiceId,
                        principalTable: "Sales",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DeliveryChallans",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsClose = table.Column<bool>(type: "bit", nullable: false),
                    IsProcessed = table.Column<bool>(type: "bit", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsService = table.Column<bool>(type: "bit", nullable: false),
                    BilingAddress = table.Column<bool>(type: "bit", nullable: false),
                    CustomerAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegistrationNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SaleOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SaleInvoiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TemplateName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApprovalStatus = table.Column<int>(type: "int", nullable: false),
                    DeliveryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ShippingId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BillingId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    NationalId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PartiallyInvoices = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PeriodId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryChallans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeliveryChallans_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeliveryChallans_Contacts_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Contacts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DeliveryChallans_SaleOrders_SaleOrderId",
                        column: x => x.SaleOrderId,
                        principalTable: "SaleOrders",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DeliveryChallans_Sales_SaleInvoiceId",
                        column: x => x.SaleInvoiceId,
                        principalTable: "Sales",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SaleInvoiceAdvancePayments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SaleInvoiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SaleOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleInvoiceAdvancePayments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SaleInvoiceAdvancePayments_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SaleInvoiceAdvancePayments_SaleOrders_SaleOrderId",
                        column: x => x.SaleOrderId,
                        principalTable: "SaleOrders",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SaleInvoiceAdvancePayments_Sales_SaleInvoiceId",
                        column: x => x.SaleInvoiceId,
                        principalTable: "Sales",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SaleInvoiceDiscounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DiscountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SaleId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DiscountPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiscountAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleInvoiceDiscounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SaleInvoiceDiscounts_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SaleInvoiceDiscounts_Discounts_DiscountId",
                        column: x => x.DiscountId,
                        principalTable: "Discounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SaleInvoiceDiscounts_Sales_SaleId",
                        column: x => x.SaleId,
                        principalTable: "Sales",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SalePayments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DueAmount = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Received = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Change = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    PaymentType = table.Column<int>(type: "int", nullable: false),
                    PaymentOptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BankCashAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SaleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Rate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BusinessId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CounterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    DayId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalePayments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalePayments_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalePayments_PaymentOptions_PaymentOptionId",
                        column: x => x.PaymentOptionId,
                        principalTable: "PaymentOptions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SalePayments_Sales_SaleId",
                        column: x => x.SaleId,
                        principalTable: "Sales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShiftEmployeeAssigns",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ShiftAssignId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    FromDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ToDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReasonOfClosingShift = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShiftEmployeeAssigns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShiftEmployeeAssigns_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ShiftEmployeeAssigns_EmployeeRegistrations_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "EmployeeRegistrations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ShiftEmployeeAssigns_ShiftAssigns_ShiftAssignId",
                        column: x => x.ShiftAssignId,
                        principalTable: "ShiftAssigns",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ShiftRuns",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShiftAssignId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FromTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ToTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShiftRuns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShiftRuns_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ShiftRuns_ShiftAssigns_ShiftAssignId",
                        column: x => x.ShiftAssignId,
                        principalTable: "ShiftAssigns",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CreditNotes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ContactId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InvoiceNote = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WareHouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TaxMethod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxRateId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SaleId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PurchasePostId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsInventoryTransaction = table.Column<bool>(type: "bit", nullable: false),
                    IsItemDescription = table.Column<bool>(type: "bit", nullable: false),
                    IsService = table.Column<bool>(type: "bit", nullable: false),
                    Narration = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GrossAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VatAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsCreditNote = table.Column<bool>(type: "bit", nullable: false),
                    TerminalId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ApprovalStatus = table.Column<int>(type: "int", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Year = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PeriodId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditNotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CreditNotes_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CreditNotes_Contacts_ContactId",
                        column: x => x.ContactId,
                        principalTable: "Contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CreditNotes_PurchasePosts_PurchasePostId",
                        column: x => x.PurchasePostId,
                        principalTable: "PurchasePosts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CreditNotes_Sales_SaleId",
                        column: x => x.SaleId,
                        principalTable: "Sales",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PurchaseInvoiceActions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProcessId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PurchaseInvoiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PurchaseInvoicePostId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CurrentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseInvoiceActions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseInvoiceActions_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseInvoiceActions_CompanyProcess_ProcessId",
                        column: x => x.ProcessId,
                        principalTable: "CompanyProcess",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchaseInvoiceActions_PurchasePosts_PurchaseInvoicePostId",
                        column: x => x.PurchaseInvoicePostId,
                        principalTable: "PurchasePosts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PurchaseInvoiceActions_Purchases_PurchaseInvoiceId",
                        column: x => x.PurchaseInvoiceId,
                        principalTable: "Purchases",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PurchaseInvoiceAttachments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PurchaseInvoiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PurchaseInvoicePostId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseInvoiceAttachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseInvoiceAttachments_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseInvoiceAttachments_PurchasePosts_PurchaseInvoicePostId",
                        column: x => x.PurchaseInvoicePostId,
                        principalTable: "PurchasePosts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PurchaseInvoiceAttachments_Purchases_PurchaseInvoiceId",
                        column: x => x.PurchaseInvoiceId,
                        principalTable: "Purchases",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "InquiryEmailCcs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InquiryEmailId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsCc = table.Column<bool>(type: "bit", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InquiryEmailCcs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InquiryEmailCcs_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InquiryEmailCcs_InquiryEmails_InquiryEmailId",
                        column: x => x.InquiryEmailId,
                        principalTable: "InquiryEmails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InquiryMeetingAttendants",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MeetingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InquiryMeetingAttendants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InquiryMeetingAttendants_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InquiryMeetingAttendants_EmployeeRegistrations_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "EmployeeRegistrations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InquiryMeetingAttendants_InquiryMeetings_MeetingId",
                        column: x => x.MeetingId,
                        principalTable: "InquiryMeetings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PaymentVoucherDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContactAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PurchaseInvoice = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SaleInvoice = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ChequeNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentVoucherId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentVoucherDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentVoucherDetails_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PaymentVoucherDetails_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PaymentVoucherDetails_PaymentVouchers_PaymentVoucherId",
                        column: x => x.PaymentVoucherId,
                        principalTable: "PaymentVouchers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PaymentVoucherItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    DueAmount = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    PayAmount = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    ExtraAmount = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    IsAging = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PartiallyInvoices = table.Column<int>(type: "int", nullable: false),
                    PurchaseInvoiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SaleInvoiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DocumentType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChequeNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentVoucherId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentVoucherItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentVoucherItems_PaymentVouchers_PaymentVoucherId",
                        column: x => x.PaymentVoucherId,
                        principalTable: "PaymentVouchers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PurchaseOrderExpenses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VoucherNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BillId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Narration = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChequeNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UsedAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ApprovalStatus = table.Column<int>(type: "int", nullable: false),
                    PettyCash = table.Column<int>(type: "int", nullable: false),
                    PaymentVoucherType = table.Column<int>(type: "int", nullable: false),
                    PaymentMode = table.Column<int>(type: "int", nullable: false),
                    PaymentMethod = table.Column<int>(type: "int", nullable: false),
                    BankCashAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ContactAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    VatAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DraftBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApprovedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RejectBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VoidBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DraftDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ApprovedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RejectDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    VoidDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PurchaseOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PaymentVoucherId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ExpenseTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TaxMethod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxRateId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrderExpenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderExpenses_Accounts_BankCashAccountId",
                        column: x => x.BankCashAccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderExpenses_Accounts_ContactAccountId",
                        column: x => x.ContactAccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderExpenses_Accounts_VatAccountId",
                        column: x => x.VatAccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderExpenses_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderExpenses_ExpenseTypes_ExpenseTypeId",
                        column: x => x.ExpenseTypeId,
                        principalTable: "ExpenseTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderExpenses_PaymentVouchers_PaymentVoucherId",
                        column: x => x.PaymentVoucherId,
                        principalTable: "PaymentVouchers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PurchaseOrderExpenses_PurchaseBills_BillId",
                        column: x => x.BillId,
                        principalTable: "PurchaseBills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderExpenses_PurchaseOrders_PurchaseOrderId",
                        column: x => x.PurchaseOrderId,
                        principalTable: "PurchaseOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderExpenses_TaxRates_TaxRateId",
                        column: x => x.TaxRateId,
                        principalTable: "TaxRates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseOrderPayments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VoucherNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentMode = table.Column<int>(type: "int", nullable: false),
                    PaymentMethod = table.Column<int>(type: "int", nullable: false),
                    ChequeNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Narration = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PurchaseOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PaymentVoucherId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrderPayments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderPayments_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderPayments_PaymentVouchers_PaymentVoucherId",
                        column: x => x.PaymentVoucherId,
                        principalTable: "PaymentVouchers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PurchaseOrderPayments_PurchaseOrders_PurchaseOrderId",
                        column: x => x.PurchaseOrderId,
                        principalTable: "PurchaseOrders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PurchasePostExpenses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VoucherNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpenseType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BillId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Narration = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChequeNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VatAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ApprovalStatus = table.Column<int>(type: "int", nullable: false),
                    PettyCash = table.Column<int>(type: "int", nullable: false),
                    PaymentVoucherType = table.Column<int>(type: "int", nullable: false),
                    PaymentMode = table.Column<int>(type: "int", nullable: false),
                    PaymentMethod = table.Column<int>(type: "int", nullable: false),
                    PurchasePostId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PaymentVoucherId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BankCashAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ContactAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    VatAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DraftBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApprovedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RejectBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VoidBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DraftDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ApprovedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RejectDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    VoidDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TaxMethod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxRateId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BusinessId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CounterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    DayId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchasePostExpenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchasePostExpenses_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchasePostExpenses_PaymentVouchers_PaymentVoucherId",
                        column: x => x.PaymentVoucherId,
                        principalTable: "PaymentVouchers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchasePostExpenses_PurchaseBills_BillId",
                        column: x => x.BillId,
                        principalTable: "PurchaseBills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchasePostExpenses_PurchasePosts_PurchasePostId",
                        column: x => x.PurchasePostId,
                        principalTable: "PurchasePosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchasePostExpenses_TaxRates_TaxRateId",
                        column: x => x.TaxRateId,
                        principalTable: "TaxRates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SaleOrderPayments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VoucherNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentMode = table.Column<int>(type: "int", nullable: false),
                    PaymentMethod = table.Column<int>(type: "int", nullable: false),
                    ChequeNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Narration = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SaleOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PaymentVoucherId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleOrderPayments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SaleOrderPayments_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SaleOrderPayments_PaymentVouchers_PaymentVoucherId",
                        column: x => x.PaymentVoucherId,
                        principalTable: "PaymentVouchers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SaleOrderPayments_SaleOrders_SaleOrderId",
                        column: x => x.SaleOrderId,
                        principalTable: "SaleOrders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ShiftRunEmployees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShiftRunId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShiftRunEmployees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShiftRunEmployees_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ShiftRunEmployees_EmployeeRegistrations_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "EmployeeRegistrations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ShiftRunEmployees_ShiftRuns_ShiftRunId",
                        column: x => x.ShiftRunId,
                        principalTable: "ShiftRuns",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PaymentVoucherAttachments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameArabic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    PaymentVoucherId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PurchaseOrderExpenseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentVoucherAttachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentVoucherAttachments_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PaymentVoucherAttachments_PaymentVouchers_PaymentVoucherId",
                        column: x => x.PaymentVoucherId,
                        principalTable: "PaymentVouchers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PaymentVoucherAttachments_PurchaseOrderExpenses_PurchaseOrderExpenseId",
                        column: x => x.PurchaseOrderExpenseId,
                        principalTable: "PurchaseOrderExpenses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AutoPurchaseTemplateItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FixDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TaxRateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AutoPurchaseTemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HighQty = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    UnitPerPack = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AutoPurchaseTemplateItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AutoPurchaseTemplateItems_AutoPurchaseTemplates_AutoPurchaseTemplateId",
                        column: x => x.AutoPurchaseTemplateId,
                        principalTable: "AutoPurchaseTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AutoPurchaseTemplateItems_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AutoPurchaseTemplateItems_TaxRates_TaxRateId",
                        column: x => x.TaxRateId,
                        principalTable: "TaxRates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BatchCostingItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantity = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    UnitPrice = table.Column<int>(type: "int", nullable: false),
                    BatchCostingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BatchCostingItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BatchCostingItems_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BatchCostings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecipeNoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ApprovalStatus = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BatchCostings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BatchCostings_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BatchProcessContractors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FromDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ToDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ContractorType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Completions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductionBatchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BatchProcessId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ContractorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ContractorId2 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ApprovalStatus = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContractorId3 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BatchProcessContractors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BatchProcessContractors_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BatchProcessContractors_Contractors_ContractorId3",
                        column: x => x.ContractorId3,
                        principalTable: "Contractors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BatchProcessContractors_EmployeeRegistrations_ContractorId",
                        column: x => x.ContractorId,
                        principalTable: "EmployeeRegistrations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ContractorPayments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VoucherNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentMode = table.Column<int>(type: "int", nullable: false),
                    PaymentMethod = table.Column<int>(type: "int", nullable: false),
                    ChequeNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Narration = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BatchProcessContractorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PaymentVoucherId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractorPayments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractorPayments_BatchProcessContractors_BatchProcessContractorId",
                        column: x => x.BatchProcessContractorId,
                        principalTable: "BatchProcessContractors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ContractorPayments_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContractorPayments_PaymentVouchers_PaymentVoucherId",
                        column: x => x.PaymentVoucherId,
                        principalTable: "PaymentVouchers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BatchProcesses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnglishName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ArabicName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WarehouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProductionBatchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProcessId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExpectedEndDay = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ActualEndDay = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ApprovalStatus = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BatchProcesses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BatchProcesses_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BatchProcesses_Processes_ProcessId",
                        column: x => x.ProcessId,
                        principalTable: "Processes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BatchProcesses_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BomSaleOrderItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BomId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SaleOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BomSaleOrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BomSaleOrderItems_Boms_BomId",
                        column: x => x.BomId,
                        principalTable: "Boms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BomSaleOrderItems_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BomRawProducts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SaleOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CurrentQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BomSaleOrderItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BomRawProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BomRawProducts_BomSaleOrderItems_BomSaleOrderItemId",
                        column: x => x.BomSaleOrderItemId,
                        principalTable: "BomSaleOrderItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BomRawProducts_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BranchItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BranchItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BranchItems_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BundleCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Offer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameArabic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Buy = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Get = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    QuantityLimit = table.Column<int>(type: "int", nullable: false),
                    QuantityOut = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StockLimit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FromDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BundleCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BundleCategories_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BundleOfferBranches",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BundleCategoriesId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PromotionOfferId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BundleOfferBranches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BundleOfferBranches_BundleCategories_BundleCategoriesId",
                        column: x => x.BundleCategoriesId,
                        principalTable: "BundleCategories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BundleOfferBranches_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContractorItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HighQuantity = table.Column<int>(type: "int", nullable: false),
                    UnitPerPack = table.Column<int>(type: "int", nullable: false),
                    BasicUnit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LevelOneUnit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Waste = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BatchProcessContractorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WarehouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractorItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractorItems_BatchProcessContractors_BatchProcessContractorId",
                        column: x => x.BatchProcessContractorId,
                        principalTable: "BatchProcessContractors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContractorItems_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CreditNoteItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FixDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TaxMethod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxRateId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ServiceItem = table.Column<bool>(type: "bit", nullable: false),
                    IsFree = table.Column<bool>(type: "bit", nullable: false),
                    WareHouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreditNoteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TotalWithOutAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VatAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiscountAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditNoteItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CreditNoteItems_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CreditNoteItems_CreditNotes_CreditNoteId",
                        column: x => x.CreditNoteId,
                        principalTable: "CreditNotes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CreditNoteItems_TaxRates_TaxRateId",
                        column: x => x.TaxRateId,
                        principalTable: "TaxRates",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CreditNoteItems_Warehouses_WareHouseId",
                        column: x => x.WareHouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DeliveryChallanItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnitName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DeliveryChallanId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryChallanItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeliveryChallanItems_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeliveryChallanItems_DeliveryChallans_DeliveryChallanId",
                        column: x => x.DeliveryChallanId,
                        principalTable: "DeliveryChallans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryChallanReserverdItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UnitName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ServiceProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DeliveryChallanReservedId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryChallanReserverdItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeliveryChallanReserverdItems_DeliveryChallanReserveds_DeliveryChallanReservedId",
                        column: x => x.DeliveryChallanReservedId,
                        principalTable: "DeliveryChallanReserveds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DispatchNoteItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    DispatchNoteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DispatchNoteItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DispatchNoteItems_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DispatchNoteItems_DispatchNotes_DispatchNoteId",
                        column: x => x.DispatchNoteId,
                        principalTable: "DispatchNotes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GatePasses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FromDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ToDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RegistrationNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContractorType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Refrence = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApprovalStatus = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsClose = table.Column<bool>(type: "bit", nullable: false),
                    IsGatePass = table.Column<bool>(type: "bit", nullable: false),
                    BatchProcessContractorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProductionBatchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Year = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PeriodId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GatePasses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GatePasses_BatchProcessContractors_BatchProcessContractorId",
                        column: x => x.BatchProcessContractorId,
                        principalTable: "BatchProcessContractors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GatePasses_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GatePasses_EmployeeRegistrations_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "EmployeeRegistrations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GatePassItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WareHouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    GatePassId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HighQuantity = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GatePassItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GatePassItems_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GatePassItems_GatePasses_GatePassId",
                        column: x => x.GatePassId,
                        principalTable: "GatePasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GoodReceiveNoteItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsService = table.Column<bool>(type: "bit", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PoQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RemainingQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ReceiveQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FixDiscount = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    TaxRateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GoodReceiveNoteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BatchNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HighQty = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    UnitPerPack = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SerialNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnitName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalWithOutAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VatAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiscountAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodReceiveNoteItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GoodReceiveNoteItems_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GoodReceiveNoteItems_GoodReceives_GoodReceiveNoteId",
                        column: x => x.GoodReceiveNoteId,
                        principalTable: "GoodReceives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GoodReceiveNoteItems_TaxRates_TaxRateId",
                        column: x => x.TaxRateId,
                        principalTable: "TaxRates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InquiryItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InquiryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InquiryItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InquiryItems_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InquiryItems_Inquiries_InquiryId",
                        column: x => x.InquiryId,
                        principalTable: "Inquiries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InventoryBlindDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CurrentQuantity = table.Column<int>(type: "int", nullable: false),
                    PhysicalQuantity = table.Column<int>(type: "int", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InventoryBlindId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryBlindDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InventoryBlindDetails_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InventoryBlindDetails_InventoryBlinds_InventoryBlindId",
                        column: x => x.InventoryBlindId,
                        principalTable: "InventoryBlinds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MobileOrderItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    MobileOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MobileOrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MobileOrderItems_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MobileOrderItems_MobileOrders_MobileOrderId",
                        column: x => x.MobileOrderId,
                        principalTable: "MobileOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PriceRecords",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    PurchasePrice = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    SalePrice = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    CostPrice = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PriceLabelingId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsCustomer = table.Column<bool>(type: "bit", nullable: false),
                    NewPrice = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PriceRecords_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PriceRecords_PriceLabelings_PriceLabelingId",
                        column: x => x.PriceLabelingId,
                        principalTable: "PriceLabelings",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProcessItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProcessId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProcessItems_Processes_ProcessId",
                        column: x => x.ProcessId,
                        principalTable: "Processes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductionBatchs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DamageStock = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    RemainingStock = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    LateReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LateReasonCompletion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LateReasonTransfer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegistrationNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NoOfBatches = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SaleOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EmployeeRegistrationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RecipeNoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApprovalStatus = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsClose = table.Column<bool>(type: "bit", nullable: false),
                    CompleteBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProcessBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransferBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProcessDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TransferDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecipeQuantity = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductionBatchs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductionBatchs_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductionBatchs_EmployeeRegistrations_EmployeeRegistrationId",
                        column: x => x.EmployeeRegistrationId,
                        principalTable: "EmployeeRegistrations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductionBatchs_SaleOrders_SaleOrderId",
                        column: x => x.SaleOrderId,
                        principalTable: "SaleOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductionBatchItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HighQuantity = table.Column<int>(type: "int", nullable: false),
                    UnitPerPack = table.Column<int>(type: "int", nullable: false),
                    BasicUnit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LevelOneUnit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    CurrentQuantity = table.Column<int>(type: "int", nullable: false),
                    RemainingQuantity = table.Column<int>(type: "int", nullable: false),
                    Waste = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProductionBatchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WarehouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductionBatchItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductionBatchItems_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductionBatchItems_ProductionBatchs_ProductionBatchId",
                        column: x => x.ProductionBatchId,
                        principalTable: "ProductionBatchs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductionBatchItems_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    EnglishName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ArabicName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BrandId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SizeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProductMasterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ColorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BarCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BarCodeType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HsCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Length = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Width = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxRateId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TaxMethod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WholesalePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SalePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OriginId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StockLevel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SaleReturnDays = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Shelf = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsExpire = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsRaw = table.Column<bool>(type: "bit", nullable: false),
                    LevelOneUnit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BasicUnit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnitPerPack = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SalePriceUnit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Assortment = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    StyleNumber = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Guarantee = table.Column<bool>(type: "bit", nullable: false),
                    Serial = table.Column<bool>(type: "bit", nullable: false),
                    ServiceItem = table.Column<bool>(type: "bit", nullable: false),
                    HighUnitPrice = table.Column<bool>(type: "bit", nullable: false),
                    PromotionOfferId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BundleCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CogsAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    InventoryAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SaleAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    WholesaleQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SchemeQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Scheme = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PurchasePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CostPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CostValue = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CostSign = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DisplayNameForPrint = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SKU = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PartNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BarCodeDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Products_BundleCategories_BundleCategoryId",
                        column: x => x.BundleCategoryId,
                        principalTable: "BundleCategories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_Colors_ColorId",
                        column: x => x.ColorId,
                        principalTable: "Colors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Products_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_Origins_OriginId",
                        column: x => x.OriginId,
                        principalTable: "Origins",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Products_ProductGroups_ProductGroupId",
                        column: x => x.ProductGroupId,
                        principalTable: "ProductGroups",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Products_ProductMasters_ProductMasterId",
                        column: x => x.ProductMasterId,
                        principalTable: "ProductMasters",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Products_Sizes_SizeId",
                        column: x => x.SizeId,
                        principalTable: "Sizes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Products_TaxRates_TaxRateId",
                        column: x => x.TaxRateId,
                        principalTable: "TaxRates",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Products_Units_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Units",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductSizes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SizeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSizes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductSizes_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductSizes_Sizes_SizeId",
                        column: x => x.SizeId,
                        principalTable: "Sizes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PromotionOffers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Offer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiscountType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BaseQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UpToQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IncludingBaseQuantity = table.Column<bool>(type: "bit", nullable: false),
                    QuantityOut = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StockLimit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FromDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Sunday = table.Column<bool>(type: "bit", nullable: false),
                    Monday = table.Column<bool>(type: "bit", nullable: false),
                    Tuesday = table.Column<bool>(type: "bit", nullable: false),
                    Wednesday = table.Column<bool>(type: "bit", nullable: false),
                    Thursday = table.Column<bool>(type: "bit", nullable: false),
                    Friday = table.Column<bool>(type: "bit", nullable: false),
                    Saturday = table.Column<bool>(type: "bit", nullable: false),
                    Buy = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Get = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PromotionType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GetProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromotionOffers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PromotionOffers_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PromotionOffers_ProductGroups_ProductGroupId",
                        column: x => x.ProductGroupId,
                        principalTable: "ProductGroups",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PromotionOffers_Products_GetProductId",
                        column: x => x.GetProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PromotionOffers_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PurchaseItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FixDiscount = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    TaxRateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PurchaseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WareHouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BatchNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HighQty = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    UnitPerPack = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SerialNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GuaranteeDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AutoNumber = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseItems_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchaseItems_Purchases_PurchaseId",
                        column: x => x.PurchaseId,
                        principalTable: "Purchases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchaseItems_TaxRates_TaxRateId",
                        column: x => x.TaxRateId,
                        principalTable: "TaxRates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchaseItems_Warehouses_WareHouseId",
                        column: x => x.WareHouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseOrderItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsService = table.Column<bool>(type: "bit", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RemainingQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ReceiveQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FixDiscount = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    TaxRateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PurchaseOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BatchNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HighQty = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    UnitPerPack = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SerialNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GuaranteeDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PurchaseOrderVersionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    WarrantyTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TaxMethod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnitName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalWithOutAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VatAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiscountAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderItems_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PurchaseOrderItems_PurchaseOrders_PurchaseOrderId",
                        column: x => x.PurchaseOrderId,
                        principalTable: "PurchaseOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderItems_PurchaseOrderVersions_PurchaseOrderVersionId",
                        column: x => x.PurchaseOrderVersionId,
                        principalTable: "PurchaseOrderVersions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PurchaseOrderItems_TaxRates_TaxRateId",
                        column: x => x.TaxRateId,
                        principalTable: "TaxRates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PurchasePostItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsService = table.Column<bool>(type: "bit", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FixDiscount = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    TaxRateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WareHouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PurchasePostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BatchNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HighQty = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    UnitPerPack = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SerialNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GuaranteeDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RemainingQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WarrantyTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ColorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TaxMethod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnitName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalWithOutAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VatAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiscountAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchasePostItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchasePostItems_Colors_ColorId",
                        column: x => x.ColorId,
                        principalTable: "Colors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PurchasePostItems_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchasePostItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PurchasePostItems_PurchasePosts_PurchasePostId",
                        column: x => x.PurchasePostId,
                        principalTable: "PurchasePosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchasePostItems_TaxRates_TaxRateId",
                        column: x => x.TaxRateId,
                        principalTable: "TaxRates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchasePostItems_Warehouses_WareHouseId",
                        column: x => x.WareHouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchasePostItems_WarrantyTypes_WarrantyTypeId",
                        column: x => x.WarrantyTypeId,
                        principalTable: "WarrantyTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "QuotationTemplateItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QuotationTemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuotationTemplateItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuotationTemplateItems_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuotationTemplateItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_QuotationTemplateItems_QuotationTemplates_QuotationTemplateId",
                        column: x => x.QuotationTemplateId,
                        principalTable: "QuotationTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReparingItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Waste = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ReparingOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReparingItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReparingItems_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReparingItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReparingItems_ReparingOrders_ReparingOrderId",
                        column: x => x.ReparingOrderId,
                        principalTable: "ReparingOrders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SaleOrderItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QuantityOut = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FixDiscount = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    TaxMethod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxRateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SaleOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BatchNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WareHouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BatchExpiry = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ServiceItem = table.Column<bool>(type: "bit", nullable: false),
                    GuaranteeDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Serial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsFree = table.Column<bool>(type: "bit", nullable: false),
                    SaleOrderVersionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StyleNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SchemeQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Scheme = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SchemePhysicalQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    UnitName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalWithOutAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VatAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiscountAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsUsedForBom = table.Column<bool>(type: "bit", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleOrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SaleOrderItems_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SaleOrderItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SaleOrderItems_SaleOrders_SaleOrderId",
                        column: x => x.SaleOrderId,
                        principalTable: "SaleOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SaleOrderItems_SaleOrderVersions_SaleOrderVersionId",
                        column: x => x.SaleOrderVersionId,
                        principalTable: "SaleOrderVersions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SaleOrderItems_TaxRates_TaxRateId",
                        column: x => x.TaxRateId,
                        principalTable: "TaxRates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SaleOrderItems_Warehouses_WareHouseId",
                        column: x => x.WareHouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SampleRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnglishName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ArabicName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoOfSampleRequests = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReferredBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestGenerated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ApprovalStatus = table.Column<int>(type: "int", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SampleRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SampleRequests_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SampleRequests_Contacts_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SampleRequests_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StockAdjustmentDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,4)", maxLength: 5, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    WarehouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StockAdjustmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BatchNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BatchExpiry = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AutoNumber = table.Column<int>(type: "int", nullable: false),
                    Serial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GuaranteeDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SerialNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WarrantyTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsSerial = table.Column<bool>(type: "bit", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockAdjustmentDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockAdjustmentDetails_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StockAdjustmentDetails_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockAdjustmentDetails_StockAdjustments_StockAdjustmentId",
                        column: x => x.StockAdjustmentId,
                        principalTable: "StockAdjustments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StockAdjustmentDetails_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockAdjustmentDetails_WarrantyTypes_WarrantyTypeId",
                        column: x => x.WarrantyTypeId,
                        principalTable: "WarrantyTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StockReceivedItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    TransferQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ReceivedQuantity = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    TransferAmount = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StockReceivedId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RemainingQuantity = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    lineTotal = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockReceivedItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockReceivedItems_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StockReceivedItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockReceivedItems_StockReceived_StockReceivedId",
                        column: x => x.StockReceivedId,
                        principalTable: "StockReceived",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StockRequestItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    RemainingQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StockRequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockRequestItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockRequestItems_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StockRequestItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockRequestItems_StockRequests_StockRequestId",
                        column: x => x.StockRequestId,
                        principalTable: "StockRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Stocks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WareHouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CurrentQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AveragePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CurrentStockValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BusinessId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CounterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    DayId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stocks_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Stocks_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Stocks_Warehouses_WareHouseId",
                        column: x => x.WareHouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StockTransferItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    TransferQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    TransferAmount = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StockTransferId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RemainingQuantity = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    LineTotal = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockTransferItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockTransferItems_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StockTransferItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockTransferItems_StockTransfers_StockTransferId",
                        column: x => x.StockTransferId,
                        principalTable: "StockTransfers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WareHouseTransferItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    WareHouseTransferId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WareHouseTransferItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WareHouseTransferItems_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WareHouseTransferItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WareHouseTransferItems_WareHouseTransfers_WareHouseTransferId",
                        column: x => x.WareHouseTransferId,
                        principalTable: "WareHouseTransfers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PromotionOfferItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiscountType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BaseQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UpToQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IncludingBaseQuantity = table.Column<bool>(type: "bit", nullable: false),
                    QuantityOut = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StockLimit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Buy = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Get = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PromotionType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PromotionOfferId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GetProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromotionOfferItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PromotionOfferItems_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PromotionOfferItems_Products_GetProductId",
                        column: x => x.GetProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PromotionOfferItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PromotionOfferItems_PromotionOffers_PromotionOfferId",
                        column: x => x.PromotionOfferId,
                        principalTable: "PromotionOffers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecipeNos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpireDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RegistrationNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecipeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    ApprovalStatus = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsClose = table.Column<bool>(type: "bit", nullable: false),
                    SampleRequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeNos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecipeNos_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RecipeNos_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RecipeNos_SampleRequests_SampleRequestId",
                        column: x => x.SampleRequestId,
                        principalTable: "SampleRequests",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SampleRequestItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EnglishName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ArabicName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HighQuantity = table.Column<int>(type: "int", nullable: false),
                    UnitPerPack = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    SampleRequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SampleRequestItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SampleRequestItems_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SampleRequestItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SampleRequestItems_SampleRequests_SampleRequestId",
                        column: x => x.SampleRequestId,
                        principalTable: "SampleRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Inventories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    SalePrice = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    CurrentStockValue = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    AveragePrice = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CurrentQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TransactionType = table.Column<int>(type: "int", nullable: false),
                    DocumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StockId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WareHouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    WarrantyDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PromotionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BundleId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OfferQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Get = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Buy = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DocumentNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsOpen = table.Column<bool>(type: "bit", nullable: false),
                    BatchNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AutoNumbering = table.Column<int>(type: "int", nullable: false),
                    RemainingQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Serial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Year = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PeriodId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BusinessId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CounterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    DayId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Inventories_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Inventories_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Inventories_Stocks_StockId",
                        column: x => x.StockId,
                        principalTable: "Stocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SaleItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FixDiscount = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    TaxRateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WareHouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Get = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Buy = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OfferQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SaleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PromotionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PromotionItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BundleId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RemainingQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BatchNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BatchExpiry = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DocumentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TaxMethod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AutoNumber = table.Column<int>(type: "int", nullable: false),
                    ServiceItem = table.Column<bool>(type: "bit", nullable: false),
                    IsFree = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Serial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnitName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SerialNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GuaranteeDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ColorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StyleNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SchemeQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Scheme = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SchemePhysicalQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PromotionOfferQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    VatAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiscountAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsFreeWithBundle = table.Column<bool>(type: "bit", nullable: false),
                    TotalWithOutAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BusinessId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CounterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DayId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SaleItems_BundleCategories_BundleId",
                        column: x => x.BundleId,
                        principalTable: "BundleCategories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SaleItems_Colors_ColorId",
                        column: x => x.ColorId,
                        principalTable: "Colors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SaleItems_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SaleItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SaleItems_PromotionOfferItems_PromotionItemId",
                        column: x => x.PromotionItemId,
                        principalTable: "PromotionOfferItems",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SaleItems_PromotionOffers_PromotionId",
                        column: x => x.PromotionId,
                        principalTable: "PromotionOffers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SaleItems_Sales_SaleId",
                        column: x => x.SaleId,
                        principalTable: "Sales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SaleItems_TaxRates_TaxRateId",
                        column: x => x.TaxRateId,
                        principalTable: "TaxRates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SaleItems_Warehouses_WareHouseId",
                        column: x => x.WareHouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecipeAssortments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RecipeNoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Assortment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeAssortments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecipeAssortments_RecipeNos_RecipeNoId",
                        column: x => x.RecipeNoId,
                        principalTable: "RecipeNos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecipeItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HighQuantity = table.Column<int>(type: "int", nullable: false),
                    UnitPerPack = table.Column<int>(type: "int", nullable: false),
                    BasicUnit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LevelOneUnit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Waste = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RecipeNoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WareHouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecipeItems_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RecipeItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipeItems_RecipeNos_RecipeNoId",
                        column: x => x.RecipeNoId,
                        principalTable: "RecipeNos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipeItems_Warehouses_WareHouseId",
                        column: x => x.WareHouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SaleSizeAssortments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SaleItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PurchasePostItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SizeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    BusinessId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CounterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DayId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    SaleId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleSizeAssortments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SaleSizeAssortments_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SaleSizeAssortments_PurchasePostItems_PurchasePostItemId",
                        column: x => x.PurchasePostItemId,
                        principalTable: "PurchasePostItems",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SaleSizeAssortments_SaleItems_SaleItemId",
                        column: x => x.SaleItemId,
                        principalTable: "SaleItems",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SaleSizeAssortments_Sales_SaleId",
                        column: x => x.SaleId,
                        principalTable: "Sales",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SaleSizeAssortments_Sizes_SizeId",
                        column: x => x.SizeId,
                        principalTable: "Sizes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AccountTemplates",
                columns: new[] { "Id", "Description", "JsonTemplate", "Name", "NameArabic", "Type" },
                values: new object[] { new Guid("ecfe29c8-c6af-4a3d-9c24-f87b30bf831c"), "SmallBusinessCOA", "{\"AccountsType\":[{\"Name\":\"Assets\",\"NameArabic\":\"الاساس\",\"IsActive\":true,\"CostCenters\":[{\"Name\":\"Cash in Hand\",\"NameArabic\":\"صندوق\",\"Description\":\"Cash in Hand\",\"IsActive\":true,\"Code\":\"101000\",\"Accounts\":[{\"Name\":\"Cash in Hand\",\"NameArabic\":\"صندوق\",\"Description\":\"Cash in Hand\",\"IsActive\":true,\"Code\":\"10100001\"}]},{\"Name\":\"Cash in Hand - Store\",\"NameArabic\":\"صندوق المستودع\",\"Description\":\"Cash in Hand - Store\",\"IsActive\":true,\"Code\":\"101001\",\"Accounts\":[{\"Name\":\"Accounts receivable\",\"NameArabic\":\"الحسبات المستحقه\",\"Description\":\"Accounts receivable\",\"IsActive\":true,\"Code\":\"10100101\"}]},{\"Name\":\"Inventory\",\"NameArabic\":\"المخزون\",\"Description\":\"Inventory\",\"IsActive\":true,\"Code\":\"111000\",\"Accounts\":[{\"Name\":\"Inventory\",\"NameArabic\":\"المخزون\",\"Description\":\"Inventory\",\"IsActive\":true,\"Code\":\"11100001\"}]},{\"Name\":\"Customer Reciveables\",\"NameArabic\":\"مستحقات العملاء\",\"Description\":\"Customer Reciveables\",\"IsActive\":true,\"Code\":\"120000\",\"Accounts\":[{\"Name\":\"Customer Reciveables\",\"NameArabic\":\"مستحقات العملاء\",\"Description\":\"Customer Reciveables\",\"IsActive\":true,\"Code\":\"1200001\"}]},{\"Name\":\"VAT Paid\",\"NameArabic\":\"مدفوعات القيمه المضافه\",\"Description\":\"VAT Paid\",\"IsActive\":true,\"Code\":\"130000\",\"Accounts\":[{\"Name\":\"VAT Paid on Purchases\",\"NameArabic\":\"\",\"Description\":\"VAT Paid on Purchases\",\"IsActive\":true,\"Code\":\"1300001\"}]},{\"Name\":\"Banks\",\"NameArabic\":\"البنوك\",\"Description\":\"Banks\",\"IsActive\":true,\"Code\":\"105000\",\"Accounts\":[{\"Name\":\"Banks\",\"NameArabic\":\"البنوك\",\"Description\":\"Banks\",\"IsActive\":true,\"Code\":\"10500001\"}]},{\"Name\":\"Accumulated Depreciation\",\"NameArabic\":\"الاستهلاك التراكمي\",\"Description\":\"Accumulated Depreciation\",\"IsActive\":true,\"Code\":\"170000\",\"Accounts\":[{\"Name\":\"Accumulated Depreciation\",\"NameArabic\":\"الاستهلاك التراكمي\",\"Description\":\"Accumulated Depreciation\",\"IsActive\":true,\"Code\":\"17000001\"}]},{\"Name\":\"Fixed Assets\",\"NameArabic\":\"الاساس الثابت\",\"Description\":\"Fixed Assets\",\"IsActive\":true,\"Code\":\"150000\",\"Accounts\":[{\"Name\":\"Fixed Assets\",\"NameArabic\":\"الاساس الثابت\",\"Description\":\"Fixed Assets\",\"IsActive\":true,\"Code\":\"1500001\"}]},{\"Name\":\"Due from Employee\",\"NameArabic\":\"مديونات الموظف\",\"Description\":\"Due from Employee\",\"IsActive\":true,\"Code\":\"126000\",\"Accounts\":[{\"Name\":\"Due from Employee\",\"NameArabic\":\"مديونات الموظف\",\"Description\":\"Due from Employee\",\"IsActive\":true,\"Code\":\"12600001\"}]}]},{\"Name\":\"Liabilities\",\"NameArabic\":\"التزامات\",\"IsActive\":true,\"CostCenters\":[{\"Name\":\"Supplier Payable\",\"NameArabic\":\"مدفوعات الموردين\",\"Description\":\"Supplier Payable\",\"IsActive\":true,\"Code\":\"200000\",\"Accounts\":[{\"Name\":\"Supplier Payable\",\"NameArabic\":\"مدفوعات الموردين\",\"Description\":\"Supplier Payable\",\"IsActive\":true,\"Code\":\"20000001\"}]},{\"Name\":\"Payroll Liabilities\",\"NameArabic\":\"مدفوعات الرواتب\",\"Description\":\"Payroll Liabilities\",\"IsActive\":true,\"Code\":\"240000\",\"Accounts\":[{\"Name\":\"Payroll Liabilities\",\"NameArabic\":\"مدفوعات الرواتب\",\"Description\":\"Payroll Liabilities\",\"IsActive\":true,\"Code\":\"24000001\"}]},{\"Name\":\"VAT Payable\",\"NameArabic\":\"مدفوعات الضريبه\",\"Description\":\"VAT Payable\",\"IsActive\":true,\"Code\":\"250000\",\"Accounts\":[{\"Name\":\"VAT Payable on Sale\",\"NameArabic\":\"مدفوعات الضريبه\",\"Description\":\"VAT Payable on Sale\",\"IsActive\":true,\"Code\":\"25000001\"}]},{\"Name\":\"Loan Payable\",\"NameArabic\":\"قرض مستحق الدفع\",\"Description\":\"Loan Payable\",\"IsActive\":true,\"Code\":\"253001\",\"Accounts\":[{\"Name\":\"Loan Payable\",\"NameArabic\":\"قرض مستحق الدفع\",\"Description\":\"Loan Payable\",\"IsActive\":true,\"Code\":\"2530101\"}]}]},{\"Name\":\"Equity\",\"NameArabic\":\"رأس المال\",\"IsActive\":true,\"CostCenters\":[{\"Name\":\"Opening Balance Equity\",\"NameArabic\":\"الرصيد الافتتاحي\",\"Description\":\"Opening Balance Equity\",\"IsActive\":true,\"Code\":\"300000\",\"Accounts\":[{\"Name\":\"Opening Balance Equity\",\"NameArabic\":\"الرصيد الافتتاحي\",\"Description\":\"Opening Balance Equity\",\"IsActive\":true,\"Code\":\"30000001\"}]},{\"Name\":\"Owner Investment\",\"NameArabic\":\"استثمار المالك \",\"Description\":\"Owner Investment\",\"IsActive\":true,\"Code\":\"301001\",\"Accounts\":[{\"Name\":\"Owner Investment\",\"NameArabic\":\"استثمار المالك \",\"Description\":\"Owner Investment\",\"IsActive\":true,\"Code\":\"30100101\"}]},{\"Name\":\"Owner Withdrawals\",\"NameArabic\":\"انسحابات المالك \",\"Description\":\"Owner Withdrawals\",\"IsActive\":true,\"Code\":\"302001\",\"Accounts\":[{\"Name\":\"Owner Withdrawals\",\"NameArabic\":\"انسحابات المالك \",\"Description\":\"Owner Withdrawals\",\"IsActive\":true,\"Code\":\"30200101\"}]},{\"Name\":\"Retained Earnings\",\"NameArabic\":\"الارباح\",\"Description\":\"Retained Earnings\",\"IsActive\":true,\"Code\":\"320000\",\"Accounts\":[{\"Name\":\"Retained Earnings\",\"NameArabic\":\"الارباح\",\"Description\":\"Retained Earnings\",\"IsActive\":true,\"Code\":\"32000001\"}]},{\"Name\":\"Net Profit for the period\",\"NameArabic\":\"صافي الربح للفتره\",\"Description\":\"Net Profit for the period\",\"IsActive\":true,\"Code\":\"321002\",\"Accounts\":[{\"Name\":\"Net Profit for the period\",\"NameArabic\":\"صافي الربح للفتره\",\"Description\":\"Net Profit for the period\",\"IsActive\":true,\"Code\":\"32100201\"}]}]},{\"Name\":\"Income\",\"NameArabic\":\"ايرادات\",\"IsActive\":true,\"CostCenters\":[{\"Name\":\"Sale\",\"NameArabic\":\"مبيعات\",\"Description\":\"Sale\",\"IsActive\":true,\"Code\":\"420000\",\"Accounts\":[{\"Name\":\"Sale\",\"NameArabic\":\"مبيعات\",\"Description\":\"Sale\",\"IsActive\":true,\"Code\":\"42000001\"}]},{\"Name\":\"Teller\",\"NameArabic\":\"الصندوق\",\"Description\":\"Teller\",\"IsActive\":true,\"Code\":\"421000\",\"Accounts\":[{\"Name\":\"Teller\",\"NameArabic\":\"الصندوق\",\"Description\":\"Teller\",\"IsActive\":true,\"Code\":\"42100001\"}]},{\"Name\":\"POS-Terminal\",\"NameArabic\":\"نقاط البيع - بنك\",\"Description\":\"POS-Terminal\",\"IsActive\":true,\"Code\":\"425000\",\"Accounts\":[{\"Name\":\"POS-Terminal\",\"NameArabic\":\"نقاط البيع - بنك\",\"Description\":\"POS-Terminal\",\"IsActive\":true,\"Code\":\"42500001\"}]},{\"Name\":\"Discount Taken\",\"NameArabic\":\"الخصم المأخوذ\",\"Description\":\"Discount Taken\",\"IsActive\":true,\"Code\":\"426000\",\"Accounts\":[{\"Name\":\"Discount Taken\",\"NameArabic\":\"الخصم المأخوذ\",\"Description\":\"Discount Taken\",\"IsActive\":true,\"Code\":\"42600001\"}]}]},{\"Name\":\"Expenses\",\"NameArabic\":\"المصاريف\",\"IsActive\":true,\"CostCenters\":[{\"Name\":\"Cost of Goods Sold\",\"NameArabic\":\"تكلفه البضاعه المباعه\",\"Description\":\"Cost of Goods Sold\",\"IsActive\":true,\"Code\":\"600001\",\"Accounts\":[{\"Name\":\"Cost of Goods Sold\",\"NameArabic\":\"تكلفه البضاعه المباعه\",\"Description\":\"Cost of Goods Sold\",\"IsActive\":true,\"Code\":\"60000101\"}]},{\"Name\":\"Freight Paid\",\"NameArabic\":\"مدفوعات الشحن\",\"Description\":\"Freight Paid\",\"IsActive\":true,\"Code\":\"608001\",\"Accounts\":[{\"Name\":\"Freight Paid\",\"NameArabic\":\"مدفوعات الشحن\",\"Description\":\"Freight Paid\",\"IsActive\":true,\"Code\":\"60800101\"}]},{\"Name\":\"Discount Given\",\"NameArabic\":\"الخصم المقدم\",\"Description\":\"Discount Given\",\"IsActive\":true,\"Code\":\"607001\",\"Accounts\":[{\"Name\":\"Discount Given\",\"NameArabic\":\"الخصم المقدم\",\"Description\":\"Discount Given\",\"IsActive\":true,\"Code\":\"60700101\"}]},{\"Name\":\"Depreciation Expense\",\"NameArabic\":\"مصاريف الاستقدام\",\"Description\":\"Depreciation Expense\",\"IsActive\":true,\"Code\":\"606001\",\"Accounts\":[{\"Name\":\"Depreciation Expense\",\"NameArabic\":\"مصاريف الاستقدام\",\"Description\":\"Depreciation Expense\",\"IsActive\":true,\"Code\":\"60600101\"}]},{\"Name\":\"General Expenses\",\"NameArabic\":\"المصاريف العامة\",\"Description\":\"General Expenses\",\"IsActive\":true,\"Code\":\"605050\",\"Accounts\":[{\"Name\":\"General Expenses\",\"NameArabic\":\"المصاريف العامة\",\"Description\":\"General Expenses\",\"IsActive\":true,\"Code\":\"60505001\"}]},{\"Name\":\"Payroll\",\"NameArabic\":\"الرواتب\",\"Description\":\"Payroll\",\"IsActive\":true,\"Code\":\"603001\",\"Accounts\":[{\"Name\":\"Payroll\",\"NameArabic\":\"الرواتب\",\"Description\":\"Payroll\",\"IsActive\":true,\"Code\":\"60300101\"}]},{\"Name\":\"Utilities\",\"NameArabic\":\"الخدمات\",\"Description\":\"Utilities\",\"IsActive\":true,\"Code\":\"604001\",\"Accounts\":[{\"Name\":\"Utilities\",\"NameArabic\":\"الخدمات\",\"Description\":\"Utilities\",\"IsActive\":true,\"Code\":\"60400101\"}]},{\"Name\":\"Rent\",\"NameArabic\":\"ايجارات\",\"Description\":\"Rent\",\"IsActive\":true,\"Code\":\"604050\",\"Accounts\":[{\"Name\":\"Rent\",\"NameArabic\":\"ايجارات\",\"Description\":\"Rent\",\"IsActive\":true,\"Code\":\"60405001\"}]},{\"Name\":\"Legal Expenses\",\"NameArabic\":\"المصاريف القانونيه\",\"Description\":\"Legal Expenses\",\"IsActive\":true,\"Code\":\"605001\",\"Accounts\":[{\"Name\":\"Legal Expenses\",\"NameArabic\":\"المصاريف القانونيه\",\"Description\":\"Legal Expenses\",\"IsActive\":true,\"Code\":\"60500101\"}]}]}]}", "SmallBusinessCOA", null, "Business" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "52E0A954-7039-483B-9224-99990743636D", "0590a33c-cd2d-4c93-9e17-fce12bc2bd9d", "Super Admin", "SUPER ADMIN" },
                    { "C1448F88-49B4-476C-B07D-33514A0F3C1F", "0C90a33c-cd2d-4c93-9e17-fce14bc2bd9d", "Mobile Customer", "MOBILE CUSTOMER" },
                    { "C1448F88-49B4-476C-B07D-33514A0F9C1E", "0590a33c-cd2d-4c93-9e17-fce19bc2bd9d", "Noble Admin", "NOBLE ADMIN" },
                    { "C1448F88-49CS-476C-B07D-33514A0F9C1E", "4590A33c-cd2d-4c93-9e17-fcE19bc2bd9d", "Supervisor", "SUPERVISOR" },
                    { "CEA36602-E3BD-4CF6-B186-5D8A3E558A04", "0590a33c-cd2d-4d93-9e17-fce19bc2bd9d", "Admin", "ADMIN" },
                    { "E5480E8E-A150-4C80-82AB-62B5A8EBFD1B", "1590a33c-cd2d-4c93-9e17-fce19bc2bd9d", "User", "USER" },
                    { "S1448F55-49B4-476C-B07D-33514A0F3C1F", "0C90a33c-dd2d-5c93-9e17-FCS14bc2bd9d", "Sale Man", "SALE MAN" },
                    { "S1448F88-49B4-476C-B07D-33514A0F3C1F", "0C90a33c-dd2d-5c93-9e17-fce14bc2bd9d", "Order Tracker", "ORDER TRACKER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "BankId", "Code", "CompanyId", "ConcurrencyStamp", "DocumentType", "Email", "EmailConfirmed", "EmployeeId", "FirstName", "ImagePath", "IsActive", "IsProceed", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "OnlineTerminalId", "Password", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TerminalId", "TwoFactorEnabled", "UserName" },
                values: new object[] { "5f8d5614-2c7e-4ec0-868c-d254e6516b4d", 0, null, null, new Guid("5f8d5614-2c7e-4ec0-868c-d254e6516b4d"), "117c7248-5202-44d5-a7eb-8f2717eba7e9", null, "noble@gmail.com", true, null, "Noble App", null, false, false, "", true, null, "noble@gmail.com", "NOBLE@GMAIL.COM", null, null, "AQAAAAEAACcQAAAAEEMJll7GgXWk1z2fatxJWFPeucssBaOZ+EXMY5MYkhRNow+oaTxB0nH+sWvTX6wKWA==", null, false, "HDM6TKME4T5DISZCHW3MHD6YLQFNSWC2", null, false, "noble@gmail.com" });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "AddressArabic", "AddressEnglish", "Arabic", "AutoPurchaseVoucher", "BankDetail", "Blocked", "BusinessParentId", "CashVoucher", "CategoryInArabic", "CategoryInEnglish", "CityArabic", "CityEnglish", "ClientNo", "ClientParentId", "CompanyEmail", "CompanyNameArabic", "CompanyNameEnglish", "CompanyRegNo", "CountryArabic", "CountryEnglish", "CreatedDate", "DayStart", "English", "ExpenseAccount", "ExpenseToGst", "HouseNumber", "InternationalPurchase", "InvoiceWoInventory", "IsArea", "IsForMedical", "IsMultiUnit", "IsOpenDay", "IsProceed", "IsProduction", "IsTransferAllow", "Landline", "LogoPath", "MasterProduct", "NameArabic", "NameEnglish", "ParentId", "PartiallyPurchase", "PhoneNo", "Postcode", "PurchaseOrder", "SaleOrder", "SimpleInvoice", "SimplifyTaxInvoiceLabel", "SimplifyTaxInvoiceLabelAr", "SoInventoryReserve", "Step1", "Step2", "Step3", "Step4", "Step5", "SuperAdminDayStart", "TaxInvoiceLabel", "TaxInvoiceLabelAr", "Terminal", "TermsCondition", "Town", "VatRegistrationNo", "VersionAllow", "Website" },
                values: new object[] { new Guid("5f8d5614-2c7e-4ec0-868c-d254e6516b4d"), null, null, false, false, false, false, null, false, null, null, null, null, null, null, null, null, null, "56ty60", null, null, new DateTime(2024, 5, 9, 19, 27, 59, 321, DateTimeKind.Utc).AddTicks(4573), false, false, false, false, null, false, false, false, false, false, false, false, false, false, null, null, false, null, "noble@gmail.com", new Guid("00000000-0000-0000-0000-000000000000"), false, null, null, false, null, false, null, null, false, false, false, false, false, false, false, null, null, false, false, null, null, false, null });

            migrationBuilder.InsertData(
                table: "AspNetUserClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "UserId" },
                values: new object[,]
                {
                    { 1, "Email", "noble@gmail.com", "5f8d5614-2c7e-4ec0-868c-d254e6516b4d" },
                    { 2, "CompanyId", "5f8d5614-2c7e-4ec0-868c-d254e6516b4d", "5f8d5614-2c7e-4ec0-868c-d254e6516b4d" },
                    { 3, "Organization", "Noble POS", "5f8d5614-2c7e-4ec0-868c-d254e6516b4d" },
                    { 4, "FullName", "Noble App", "5f8d5614-2c7e-4ec0-868c-d254e6516b4d" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "C1448F88-49B4-476C-B07D-33514A0F9C1E", "5f8d5614-2c7e-4ec0-868c-d254e6516b4d" });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_Code_CompanyId",
                table: "Accounts",
                columns: new[] { "Code", "CompanyId" },
                unique: true)
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_CompanyId",
                table: "Accounts",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_CostCenterId",
                table: "Accounts",
                column: "CostCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountsLevelOne_Code_CompanyId",
                table: "AccountsLevelOne",
                columns: new[] { "Code", "CompanyId" },
                unique: true)
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_AccountsLevelOne_CompanyId",
                table: "AccountsLevelOne",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountsLevelTwo_CompanyId",
                table: "AccountsLevelTwo",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountTypes_CompanyId",
                table: "AccountTypes",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Allowances_AllowanceTypeId",
                table: "Allowances",
                column: "AllowanceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Allowances_CompanyId",
                table: "Allowances",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_AllowanceTypes_CompanyId",
                table: "AllowanceTypes",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUpdates_CompanyId",
                table: "ApplicationUpdates",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalSystemEmployees_ApprovalSystemId",
                table: "ApprovalSystemEmployees",
                column: "ApprovalSystemId");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalSystemEmployees_CompanyId",
                table: "ApprovalSystemEmployees",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalSystemEmployees_EmployeeId",
                table: "ApprovalSystemEmployees",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalSystems_CompanyId",
                table: "ApprovalSystems",
                column: "CompanyId");

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
                name: "IX_AspNetUsers_BankId",
                table: "AspNetUsers",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Attachments_CompanyId",
                table: "Attachments",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_AutoPurchaseSettings_CompanyId",
                table: "AutoPurchaseSettings",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_AutoPurchaseTemplateItems_AutoPurchaseTemplateId",
                table: "AutoPurchaseTemplateItems",
                column: "AutoPurchaseTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_AutoPurchaseTemplateItems_CompanyId",
                table: "AutoPurchaseTemplateItems",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_AutoPurchaseTemplateItems_ProductId",
                table: "AutoPurchaseTemplateItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_AutoPurchaseTemplateItems_TaxRateId",
                table: "AutoPurchaseTemplateItems",
                column: "TaxRateId");

            migrationBuilder.CreateIndex(
                name: "IX_AutoPurchaseTemplates_CompanyId",
                table: "AutoPurchaseTemplates",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_AutoPurchaseTemplates_SupplierId",
                table: "AutoPurchaseTemplates",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_BankPosTerminals_BankId",
                table: "BankPosTerminals",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_BankPosTerminals_CompanyId",
                table: "BankPosTerminals",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Banks_AccountId",
                table: "Banks",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Banks_CompanyId",
                table: "Banks",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Banks_CurrencyId",
                table: "Banks",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_BatchCostingItems_BatchCostingId",
                table: "BatchCostingItems",
                column: "BatchCostingId");

            migrationBuilder.CreateIndex(
                name: "IX_BatchCostingItems_CompanyId",
                table: "BatchCostingItems",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_BatchCostings_CompanyId",
                table: "BatchCostings",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_BatchCostings_RecipeNoId",
                table: "BatchCostings",
                column: "RecipeNoId");

            migrationBuilder.CreateIndex(
                name: "IX_BatchProcessContractors_BatchProcessId",
                table: "BatchProcessContractors",
                column: "BatchProcessId");

            migrationBuilder.CreateIndex(
                name: "IX_BatchProcessContractors_CompanyId",
                table: "BatchProcessContractors",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_BatchProcessContractors_ContractorId",
                table: "BatchProcessContractors",
                column: "ContractorId");

            migrationBuilder.CreateIndex(
                name: "IX_BatchProcessContractors_ContractorId3",
                table: "BatchProcessContractors",
                column: "ContractorId3");

            migrationBuilder.CreateIndex(
                name: "IX_BatchProcesses_CompanyId",
                table: "BatchProcesses",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_BatchProcesses_ProcessId",
                table: "BatchProcesses",
                column: "ProcessId");

            migrationBuilder.CreateIndex(
                name: "IX_BatchProcesses_ProductionBatchId",
                table: "BatchProcesses",
                column: "ProductionBatchId");

            migrationBuilder.CreateIndex(
                name: "IX_BatchProcesses_WarehouseId",
                table: "BatchProcesses",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_BillAttachments_CompanyId",
                table: "BillAttachments",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_BillAttachments_PurchaseBillId",
                table: "BillAttachments",
                column: "PurchaseBillId");

            migrationBuilder.CreateIndex(
                name: "IX_BomRawProducts_BomSaleOrderItemId",
                table: "BomRawProducts",
                column: "BomSaleOrderItemId");

            migrationBuilder.CreateIndex(
                name: "IX_BomRawProducts_CompanyId",
                table: "BomRawProducts",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_BomRawProducts_ProductId",
                table: "BomRawProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Boms_CompanyId",
                table: "Boms",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_BomSaleOrderItems_BomId",
                table: "BomSaleOrderItems",
                column: "BomId");

            migrationBuilder.CreateIndex(
                name: "IX_BomSaleOrderItems_CompanyId",
                table: "BomSaleOrderItems",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_BomSaleOrderItems_ProductId",
                table: "BomSaleOrderItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_BranchItems_CompanyId",
                table: "BranchItems",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_BranchItems_ProductId",
                table: "BranchItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_BranchUsers_BranchId",
                table: "BranchUsers",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_BranchUsers_CompanyId",
                table: "BranchUsers",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_BranchVouchers_BankCashAccountId",
                table: "BranchVouchers",
                column: "BankCashAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_BranchVouchers_CompanyId",
                table: "BranchVouchers",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_BranchVouchers_ContactAccountId",
                table: "BranchVouchers",
                column: "ContactAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_BranchVouchers_TaxRateId",
                table: "BranchVouchers",
                column: "TaxRateId");

            migrationBuilder.CreateIndex(
                name: "IX_BranchWisePermissions_CompanyId",
                table: "BranchWisePermissions",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Brands_CompanyId",
                table: "Brands",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_BundleCategories_CompanyId",
                table: "BundleCategories",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_BundleCategories_ProductId",
                table: "BundleCategories",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_BundleOfferBranches_BundleCategoriesId",
                table: "BundleOfferBranches",
                column: "BundleCategoriesId");

            migrationBuilder.CreateIndex(
                name: "IX_BundleOfferBranches_CompanyId",
                table: "BundleOfferBranches",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_BundleOfferBranches_PromotionOfferId",
                table: "BundleOfferBranches",
                column: "PromotionOfferId");

            migrationBuilder.CreateIndex(
                name: "IX_CashCustomer_CompanyId",
                table: "CashCustomer",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CashRequestUsers_CompanyId",
                table: "CashRequestUsers",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CashVouchers_CompanyId",
                table: "CashVouchers",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_COGSAccountId",
                table: "Categories",
                column: "COGSAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_CompanyId",
                table: "Categories",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_IncomeAccountId",
                table: "Categories",
                column: "IncomeAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_InventoryAccountId",
                table: "Categories",
                column: "InventoryAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_PurchaseAccountId",
                table: "Categories",
                column: "PurchaseAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_SaleAccountId",
                table: "Categories",
                column: "SaleAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_ChequeBookItems_ChequeBookId",
                table: "ChequeBookItems",
                column: "ChequeBookId");

            migrationBuilder.CreateIndex(
                name: "IX_ChequeBookItems_CompanyId",
                table: "ChequeBookItems",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_ChequeBooks_BankId",
                table: "ChequeBooks",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_ChequeBooks_CompanyId",
                table: "ChequeBooks",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_CompanyId",
                table: "Cities",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Colors_CompanyId",
                table: "Colors",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyAccountSetups_CompanyId",
                table: "CompanyAccountSetups",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyAttachments_CompanyId",
                table: "CompanyAttachments",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyHolidays_CompanyId",
                table: "CompanyHolidays",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_companyInformations_CompanyId",
                table: "companyInformations",
                column: "CompanyId",
                unique: true,
                filter: "[CompanyId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyLicences_CompanyId",
                table: "CompanyLicences",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyOptions_CompanyId",
                table: "CompanyOptions",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyPermissions_CompanyId",
                table: "CompanyPermissions",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyPermissions_NobleGroupId",
                table: "CompanyPermissions",
                column: "NobleGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyPermissions_NobleModuleId",
                table: "CompanyPermissions",
                column: "NobleModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyProcess_CompanyId",
                table: "CompanyProcess",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanySubmissionPeriods_CompanyId",
                table: "CompanySubmissionPeriods",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanySubmissionPeriods_YearlyPeriodId",
                table: "CompanySubmissionPeriods",
                column: "YearlyPeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactAttachments_CompanyId",
                table: "ContactAttachments",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactAttachments_ContactId",
                table: "ContactAttachments",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactBankAccounts_CompanyId",
                table: "ContactBankAccounts",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactBankAccounts_ContactId",
                table: "ContactBankAccounts",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactPersons_ContactId",
                table: "ContactPersons",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_AccountId",
                table: "Contacts",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_AdvanceAccountId",
                table: "Contacts",
                column: "AdvanceAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_CompanyId",
                table: "Contacts",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_CurrencyId",
                table: "Contacts",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_CustomerGroupId",
                table: "Contacts",
                column: "CustomerGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_RegionId",
                table: "Contacts",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_SupplierCashAccountId",
                table: "Contacts",
                column: "SupplierCashAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_TaxRateId",
                table: "Contacts",
                column: "TaxRateId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractorItems_BatchProcessContractorId",
                table: "ContractorItems",
                column: "BatchProcessContractorId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractorItems_CompanyId",
                table: "ContractorItems",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractorItems_ProductId",
                table: "ContractorItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractorPayments_BatchProcessContractorId",
                table: "ContractorPayments",
                column: "BatchProcessContractorId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractorPayments_CompanyId",
                table: "ContractorPayments",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractorPayments_PaymentVoucherId",
                table: "ContractorPayments",
                column: "PaymentVoucherId");

            migrationBuilder.CreateIndex(
                name: "IX_Contractors_CompanyId",
                table: "Contractors",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Contributions_CompanyId",
                table: "Contributions",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CostCenters_AccountTypeId",
                table: "CostCenters",
                column: "AccountTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CostCenters_Code_CompanyId",
                table: "CostCenters",
                columns: new[] { "Code", "CompanyId" },
                unique: true)
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_CostCenters_CompanyId",
                table: "CostCenters",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CreditNoteItems_CompanyId",
                table: "CreditNoteItems",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CreditNoteItems_CreditNoteId",
                table: "CreditNoteItems",
                column: "CreditNoteId");

            migrationBuilder.CreateIndex(
                name: "IX_CreditNoteItems_ProductId",
                table: "CreditNoteItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_CreditNoteItems_TaxRateId",
                table: "CreditNoteItems",
                column: "TaxRateId");

            migrationBuilder.CreateIndex(
                name: "IX_CreditNoteItems_WareHouseId",
                table: "CreditNoteItems",
                column: "WareHouseId");

            migrationBuilder.CreateIndex(
                name: "IX_CreditNotes_CompanyId",
                table: "CreditNotes",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CreditNotes_ContactId",
                table: "CreditNotes",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_CreditNotes_PurchasePostId",
                table: "CreditNotes",
                column: "PurchasePostId");

            migrationBuilder.CreateIndex(
                name: "IX_CreditNotes_SaleId",
                table: "CreditNotes",
                column: "SaleId");

            migrationBuilder.CreateIndex(
                name: "IX_Currencies_CompanyId",
                table: "Currencies",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerDiscount_CompanyId",
                table: "CustomerDiscount",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerGroups_CompanyId",
                table: "CustomerGroups",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyExpenseAttachments_CompanyId",
                table: "DailyExpenseAttachments",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyExpenseAttachments_DailyExpenseId",
                table: "DailyExpenseAttachments",
                column: "DailyExpenseId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyExpenseDetails_CompanyId",
                table: "DailyExpenseDetails",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyExpenseDetails_DailyExpenseId",
                table: "DailyExpenseDetails",
                column: "DailyExpenseId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyExpenseDetails_ExpenseAccountId",
                table: "DailyExpenseDetails",
                column: "ExpenseAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyExpenseDetails_VatId",
                table: "DailyExpenseDetails",
                column: "VatId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyExpenses_AccountId",
                table: "DailyExpenses",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyExpenses_BillerAccountId",
                table: "DailyExpenses",
                column: "BillerAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyExpenses_CompanyId",
                table: "DailyExpenses",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_DayStarts_CompanyId",
                table: "DayStarts",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Deductions_CompanyId",
                table: "Deductions",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryAddresses_ContactId",
                table: "DeliveryAddresses",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryChallanItems_CompanyId",
                table: "DeliveryChallanItems",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryChallanItems_DeliveryChallanId",
                table: "DeliveryChallanItems",
                column: "DeliveryChallanId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryChallanItems_ProductId",
                table: "DeliveryChallanItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryChallanReserveds_CompanyId",
                table: "DeliveryChallanReserveds",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryChallanReserveds_SaleInvoiceId",
                table: "DeliveryChallanReserveds",
                column: "SaleInvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryChallanReserverdItems_DeliveryChallanReservedId",
                table: "DeliveryChallanReserverdItems",
                column: "DeliveryChallanReservedId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryChallanReserverdItems_ProductId",
                table: "DeliveryChallanReserverdItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryChallans_CompanyId",
                table: "DeliveryChallans",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryChallans_CustomerId",
                table: "DeliveryChallans",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryChallans_SaleInvoiceId",
                table: "DeliveryChallans",
                column: "SaleInvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryChallans_SaleOrderId",
                table: "DeliveryChallans",
                column: "SaleOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryHolidays_DeliveryAddressId",
                table: "DeliveryHolidays",
                column: "DeliveryAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryHolidays_WeekHolidayId",
                table: "DeliveryHolidays",
                column: "WeekHolidayId");

            migrationBuilder.CreateIndex(
                name: "IX_DenominationSetups_CompanyId",
                table: "DenominationSetups",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_BankId",
                table: "Departments",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_CompanyId",
                table: "Departments",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Designations_CompanyId",
                table: "Designations",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Discounts_CompanyId",
                table: "Discounts",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_DiscountSetups_CompanyId",
                table: "DiscountSetups",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_DispatchNoteItems_CompanyId",
                table: "DispatchNoteItems",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_DispatchNoteItems_DispatchNoteId",
                table: "DispatchNoteItems",
                column: "DispatchNoteId");

            migrationBuilder.CreateIndex(
                name: "IX_DispatchNoteItems_ProductId",
                table: "DispatchNoteItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_DispatchNotes_CompanyId",
                table: "DispatchNotes",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_DispatchNotes_CustomerId",
                table: "DispatchNotes",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_DispatchNotes_SaleOrderId",
                table: "DispatchNotes",
                column: "SaleOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailConfiguration_CompanyId",
                table: "EmailConfiguration",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeAttachments_AttachmentId",
                table: "EmployeeAttachments",
                column: "AttachmentId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeAttachments_CompanyId",
                table: "EmployeeAttachments",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeAttachments_EmployeeId",
                table: "EmployeeAttachments",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDepartments_CompanyId",
                table: "EmployeeDepartments",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDepartments_DepartmentId",
                table: "EmployeeDepartments",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDepartments_EmployeeId",
                table: "EmployeeDepartments",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeRegistrationAttachments_CompanyId",
                table: "EmployeeRegistrationAttachments",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeRegistrationAttachments_EmployeeId",
                table: "EmployeeRegistrationAttachments",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeRegistrations_CompanyId",
                table: "EmployeeRegistrations",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeRegistrations_DepartmentId",
                table: "EmployeeRegistrations",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeRegistrations_DesignationId",
                table: "EmployeeRegistrations",
                column: "DesignationId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeRegistrations_EmployeeAccountId",
                table: "EmployeeRegistrations",
                column: "EmployeeAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeRegistrations_PayableAccountId",
                table: "EmployeeRegistrations",
                column: "PayableAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_CompanyId",
                table: "Employees",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DesignationId",
                table: "Employees",
                column: "DesignationId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ParentId",
                table: "Employees",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ZoneId",
                table: "Employees",
                column: "ZoneId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeSalaries_CompanyId",
                table: "EmployeeSalaries",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeSalaries_EmployeeId",
                table: "EmployeeSalaries",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeSalaryDetails_CompanyId",
                table: "EmployeeSalaryDetails",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeSalaryDetails_EmployeeSalaryId",
                table: "EmployeeSalaryDetails",
                column: "EmployeeSalaryId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseTypes_AccountId",
                table: "ExpenseTypes",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseTypes_CompanyId",
                table: "ExpenseTypes",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialYearClosingBalances_AccountId",
                table: "FinancialYearClosingBalances",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialYearClosingBalances_CompanyId",
                table: "FinancialYearClosingBalances",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialYearClosingBalances_FinancialYearClosingId",
                table: "FinancialYearClosingBalances",
                column: "FinancialYearClosingId");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialYearClosings_CompanyId",
                table: "FinancialYearClosings",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialYearSettings_CompanyId",
                table: "FinancialYearSettings",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_GatePasses_BatchProcessContractorId",
                table: "GatePasses",
                column: "BatchProcessContractorId");

            migrationBuilder.CreateIndex(
                name: "IX_GatePasses_CompanyId",
                table: "GatePasses",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_GatePasses_EmployeeId",
                table: "GatePasses",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_GatePasses_ProductionBatchId",
                table: "GatePasses",
                column: "ProductionBatchId");

            migrationBuilder.CreateIndex(
                name: "IX_GatePassItems_CompanyId",
                table: "GatePassItems",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_GatePassItems_GatePassId",
                table: "GatePassItems",
                column: "GatePassId");

            migrationBuilder.CreateIndex(
                name: "IX_GatePassItems_ProductId",
                table: "GatePassItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodReceiveNoteItems_CompanyId",
                table: "GoodReceiveNoteItems",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodReceiveNoteItems_GoodReceiveNoteId",
                table: "GoodReceiveNoteItems",
                column: "GoodReceiveNoteId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodReceiveNoteItems_ProductId",
                table: "GoodReceiveNoteItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodReceiveNoteItems_TaxRateId",
                table: "GoodReceiveNoteItems",
                column: "TaxRateId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodReceives_CompanyId",
                table: "GoodReceives",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodReceives_PurchaseOrderId",
                table: "GoodReceives",
                column: "PurchaseOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodReceives_SupplierId",
                table: "GoodReceives",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_GuestedHolidays_HolidayId",
                table: "GuestedHolidays",
                column: "HolidayId");

            migrationBuilder.CreateIndex(
                name: "IX_HoldSetups_CompanyId",
                table: "HoldSetups",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Holidays_CompanyId",
                table: "Holidays",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_ImportExportItems_CarrierId",
                table: "ImportExportItems",
                column: "CarrierId");

            migrationBuilder.CreateIndex(
                name: "IX_ImportExportItems_CompanyId",
                table: "ImportExportItems",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_ImportExportItems_PortOfDestinationId",
                table: "ImportExportItems",
                column: "PortOfDestinationId");

            migrationBuilder.CreateIndex(
                name: "IX_ImportExportItems_PortOfLoadingId",
                table: "ImportExportItems",
                column: "PortOfLoadingId");

            migrationBuilder.CreateIndex(
                name: "IX_ImportExportItems_SaleOrderId",
                table: "ImportExportItems",
                column: "SaleOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_ImportExportItems_ServiceId",
                table: "ImportExportItems",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ImportExportItems_StuffingLocationId",
                table: "ImportExportItems",
                column: "StuffingLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_ImportExportTypes_CompanyId",
                table: "ImportExportTypes",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Inquiries_CompanyId",
                table: "Inquiries",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Inquiries_CustomerId",
                table: "Inquiries",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Inquiries_InquiryPriorityId",
                table: "Inquiries",
                column: "InquiryPriorityId");

            migrationBuilder.CreateIndex(
                name: "IX_Inquiries_InquiryProcessId",
                table: "Inquiries",
                column: "InquiryProcessId");

            migrationBuilder.CreateIndex(
                name: "IX_Inquiries_InquiryStatusDynamicId",
                table: "Inquiries",
                column: "InquiryStatusDynamicId");

            migrationBuilder.CreateIndex(
                name: "IX_Inquiries_InquiryTypeId",
                table: "Inquiries",
                column: "InquiryTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Inquiries_MediaTypeId",
                table: "Inquiries",
                column: "MediaTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Inquiries_ReferedBy",
                table: "Inquiries",
                column: "ReferedBy");

            migrationBuilder.CreateIndex(
                name: "IX_InquiryComments_CompanyId",
                table: "InquiryComments",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_InquiryComments_InquiryId",
                table: "InquiryComments",
                column: "InquiryId");

            migrationBuilder.CreateIndex(
                name: "IX_InquiryComments_ReplyCommentedId",
                table: "InquiryComments",
                column: "ReplyCommentedId");

            migrationBuilder.CreateIndex(
                name: "IX_InquiryEmailCcs_CompanyId",
                table: "InquiryEmailCcs",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_InquiryEmailCcs_InquiryEmailId",
                table: "InquiryEmailCcs",
                column: "InquiryEmailId");

            migrationBuilder.CreateIndex(
                name: "IX_InquiryEmails_CompanyId",
                table: "InquiryEmails",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_InquiryEmails_InquiryId",
                table: "InquiryEmails",
                column: "InquiryId");

            migrationBuilder.CreateIndex(
                name: "IX_InquiryItems_CompanyId",
                table: "InquiryItems",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_InquiryItems_InquiryId",
                table: "InquiryItems",
                column: "InquiryId");

            migrationBuilder.CreateIndex(
                name: "IX_InquiryItems_ItemId",
                table: "InquiryItems",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_InquiryMeetingAttendants_CompanyId",
                table: "InquiryMeetingAttendants",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_InquiryMeetingAttendants_EmployeeId",
                table: "InquiryMeetingAttendants",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_InquiryMeetingAttendants_MeetingId",
                table: "InquiryMeetingAttendants",
                column: "MeetingId");

            migrationBuilder.CreateIndex(
                name: "IX_InquiryMeetings_CompanyId",
                table: "InquiryMeetings",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_InquiryMeetings_InquiryId",
                table: "InquiryMeetings",
                column: "InquiryId");

            migrationBuilder.CreateIndex(
                name: "IX_InquiryModuleQuestions_CompanyId",
                table: "InquiryModuleQuestions",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_InquiryModuleQuestions_InquiryId",
                table: "InquiryModuleQuestions",
                column: "InquiryId");

            migrationBuilder.CreateIndex(
                name: "IX_InquiryModuleQuestions_InquiryModuleId",
                table: "InquiryModuleQuestions",
                column: "InquiryModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_InquiryModules_CompanyId",
                table: "InquiryModules",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_InquiryPriorities_CompanyId",
                table: "InquiryPriorities",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_InquiryProcesses_CompanyId",
                table: "InquiryProcesses",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_InquiryStatusDynamics_CompanyId",
                table: "InquiryStatusDynamics",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_InquiryStatuses_CompanyId",
                table: "InquiryStatuses",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_InquiryStatuses_InquiryId",
                table: "InquiryStatuses",
                column: "InquiryId");

            migrationBuilder.CreateIndex(
                name: "IX_InquiryStatuses_InquiryStatusDynamicId",
                table: "InquiryStatuses",
                column: "InquiryStatusDynamicId");

            migrationBuilder.CreateIndex(
                name: "IX_InquiryTypes_CompanyId",
                table: "InquiryTypes",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventories_CompanyId",
                table: "Inventories",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventories_ProductId",
                table: "Inventories",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventories_StockId",
                table: "Inventories",
                column: "StockId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryBlindDetails_CompanyId",
                table: "InventoryBlindDetails",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryBlindDetails_InventoryBlindId",
                table: "InventoryBlindDetails",
                column: "InventoryBlindId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryBlindDetails_ProductId",
                table: "InventoryBlindDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryBlinds_CompanyId",
                table: "InventoryBlinds",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryBlinds_WarehouseId",
                table: "InventoryBlinds",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceDefaults_CompanyId",
                table: "InvoiceDefaults",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Issueds_CompanyId",
                table: "Issueds",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemsListDisplayOrderSetup_CompanyId",
                table: "ItemsListDisplayOrderSetup",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemViewSetupForPrint_CompanyId",
                table: "ItemViewSetupForPrint",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemViewSetups_CompanyId",
                table: "ItemViewSetups",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_JournalVoucherAttachment_CompanyId",
                table: "JournalVoucherAttachment",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_JournalVoucherAttachment_JournalVoucherId",
                table: "JournalVoucherAttachment",
                column: "JournalVoucherId");

            migrationBuilder.CreateIndex(
                name: "IX_JournalVoucherItems_AccountId",
                table: "JournalVoucherItems",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_JournalVoucherItems_CompanyId",
                table: "JournalVoucherItems",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_JournalVoucherItems_ContactId",
                table: "JournalVoucherItems",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_JournalVoucherItems_JournalVoucherId",
                table: "JournalVoucherItems",
                column: "JournalVoucherId");

            migrationBuilder.CreateIndex(
                name: "IX_JournalVouchers_CompanyId",
                table: "JournalVouchers",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveGroupEmployees_CompanyId",
                table: "LeaveGroupEmployees",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveGroupEmployees_EmployeeId",
                table: "LeaveGroupEmployees",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveGroupEmployees_LeaveGroupId",
                table: "LeaveGroupEmployees",
                column: "LeaveGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveGroups_CompanyId",
                table: "LeaveGroups",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveHolidays_CompanyId",
                table: "LeaveHolidays",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_LeavePeriods_CompanyId",
                table: "LeavePeriods",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRules_CompanyId",
                table: "LeaveRules",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRules_DepartmentId",
                table: "LeaveRules",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRules_DesignationId",
                table: "LeaveRules",
                column: "DesignationId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRules_EmployeeId",
                table: "LeaveRules",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRules_LeaveGroupId",
                table: "LeaveRules",
                column: "LeaveGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRules_LeavePeriodId",
                table: "LeaveRules",
                column: "LeavePeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRules_LeaveTypeId",
                table: "LeaveRules",
                column: "LeaveTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveTypes_CompanyId",
                table: "LeaveTypes",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveTypes_LeaveGroupId",
                table: "LeaveTypes",
                column: "LeaveGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_LedgerAccounts_AccountId",
                table: "LedgerAccounts",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_LedgerAccounts_CompanyId",
                table: "LedgerAccounts",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_LedgerTransactions_CompanyId",
                table: "LedgerTransactions",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_ListOrderSetups_CompanyId",
                table: "ListOrderSetups",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanPayments_CompanyId",
                table: "LoanPayments",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanPayments_EmployeeRegistrationId",
                table: "LoanPayments",
                column: "EmployeeRegistrationId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanPays_CompanyId",
                table: "LoanPays",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanPays_LoanPaymentId",
                table: "LoanPays",
                column: "LoanPaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_LoginPermissions_CompanyId",
                table: "LoginPermissions",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Logistics_ClearanceAgent",
                table: "Logistics",
                column: "ClearanceAgent");

            migrationBuilder.CreateIndex(
                name: "IX_Logistics_CompanyId",
                table: "Logistics",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_ManualAttendances_CompanyId",
                table: "ManualAttendances",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_ManualAttendances_EmployeeId",
                table: "ManualAttendances",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_MediaTypes_CompanyId",
                table: "MediaTypes",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_MobileOrderItems_CompanyId",
                table: "MobileOrderItems",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_MobileOrderItems_MobileOrderId",
                table: "MobileOrderItems",
                column: "MobileOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_MobileOrderItems_ProductId",
                table: "MobileOrderItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_MobileOrders_CompanyId",
                table: "MobileOrders",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_ModuleQuestions_CompanyId",
                table: "ModuleQuestions",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_ModuleQuestions_InquiryModuleId",
                table: "ModuleQuestions",
                column: "InquiryModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_ModulesRights_ModuleId",
                table: "ModulesRights",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_MonthlyCostItems_MonthlyCostId",
                table: "MonthlyCostItems",
                column: "MonthlyCostId");

            migrationBuilder.CreateIndex(
                name: "IX_MonthlyCosts_CompanyId",
                table: "MonthlyCosts",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_MultiUps_CompanyId",
                table: "MultiUps",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_MultiUps_CustomerId",
                table: "MultiUps",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_MultiUps_EmployeeId",
                table: "MultiUps",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_MultiUPSLineItems_AcessoryIncludedId",
                table: "MultiUPSLineItems",
                column: "AcessoryIncludedId");

            migrationBuilder.CreateIndex(
                name: "IX_MultiUPSLineItems_MultiUpId",
                table: "MultiUPSLineItems",
                column: "MultiUpId");

            migrationBuilder.CreateIndex(
                name: "IX_MultiUPSLineItems_ProblemId",
                table: "MultiUPSLineItems",
                column: "ProblemId");

            migrationBuilder.CreateIndex(
                name: "IX_MultiUPSLineItems_UpsDescriptionId",
                table: "MultiUPSLineItems",
                column: "UpsDescriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_MultiUPSLineItems_WarrantyCategoryId",
                table: "MultiUPSLineItems",
                column: "WarrantyCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_NobleGroups_CompanyId",
                table: "NobleGroups",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_NobleModules_CompanyId",
                table: "NobleModules",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_NoblePermissions_NobleModuleId",
                table: "NoblePermissions",
                column: "NobleModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_NobleRolePermissions_CompanyId",
                table: "NobleRolePermissions",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_NobleRolePermissions_PermissionId",
                table: "NobleRolePermissions",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_NobleRolePermissions_RoleId",
                table: "NobleRolePermissions",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_NobleRoles_CompanyId",
                table: "NobleRoles",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_NobleUserRoles_CompanyId",
                table: "NobleUserRoles",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_NobleUserRoles_RoleId",
                table: "NobleUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Origins_CompanyId",
                table: "Origins",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_OtherCurrencies_CompanyId",
                table: "OtherCurrencies",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_OtherCurrencies_CurrencyId",
                table: "OtherCurrencies",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_PaidTimeOffs_CompanyId",
                table: "PaidTimeOffs",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_PaidTimeOffs_EmployeeId",
                table: "PaidTimeOffs",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_PaidTimeOffs_LeavePeriodId",
                table: "PaidTimeOffs",
                column: "LeavePeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_PaidTimeOffs_LeaveType",
                table: "PaidTimeOffs",
                column: "LeaveType");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentLimits_CompanyId",
                table: "PaymentLimits",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentOptions_CompanyId",
                table: "PaymentOptions",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentRefunds_BankCashAccountId",
                table: "PaymentRefunds",
                column: "BankCashAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentRefunds_CompanyId",
                table: "PaymentRefunds",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentRefunds_ContactAccountId",
                table: "PaymentRefunds",
                column: "ContactAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentRefunds_TaxRateId",
                table: "PaymentRefunds",
                column: "TaxRateId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentVoucherAttachments_CompanyId",
                table: "PaymentVoucherAttachments",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentVoucherAttachments_PaymentVoucherId",
                table: "PaymentVoucherAttachments",
                column: "PaymentVoucherId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentVoucherAttachments_PurchaseOrderExpenseId",
                table: "PaymentVoucherAttachments",
                column: "PurchaseOrderExpenseId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentVoucherDetails_AccountId",
                table: "PaymentVoucherDetails",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentVoucherDetails_CompanyId",
                table: "PaymentVoucherDetails",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentVoucherDetails_PaymentVoucherId",
                table: "PaymentVoucherDetails",
                column: "PaymentVoucherId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentVoucherItems_PaymentVoucherId",
                table: "PaymentVoucherItems",
                column: "PaymentVoucherId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentVouchers_BankCashAccountId",
                table: "PaymentVouchers",
                column: "BankCashAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentVouchers_BillsId",
                table: "PaymentVouchers",
                column: "BillsId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentVouchers_CompanyId",
                table: "PaymentVouchers",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentVouchers_ContactAccountId",
                table: "PaymentVouchers",
                column: "ContactAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentVouchers_MultiUpId",
                table: "PaymentVouchers",
                column: "MultiUpId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentVouchers_ReparingOrderId",
                table: "PaymentVouchers",
                column: "ReparingOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentVouchers_TaxRateId",
                table: "PaymentVouchers",
                column: "TaxRateId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentVouchers_VatAccountId",
                table: "PaymentVouchers",
                column: "VatAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_PaySchedules_CompanyId",
                table: "PaySchedules",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_PermanentDeleteHoldSetups_CompanyId",
                table: "PermanentDeleteHoldSetups",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Prefixies_CompanyId",
                table: "Prefixies",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_PriceLabelings_CompanyId",
                table: "PriceLabelings",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_PriceRecords_CompanyId",
                table: "PriceRecords",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_PriceRecords_PriceLabelingId",
                table: "PriceRecords",
                column: "PriceLabelingId");

            migrationBuilder.CreateIndex(
                name: "IX_PriceRecords_ProductId",
                table: "PriceRecords",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_PrintOptions_CompanyId",
                table: "PrintOptions",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_PrintOptions_PrintSettingId",
                table: "PrintOptions",
                column: "PrintSettingId");

            migrationBuilder.CreateIndex(
                name: "IX_PrintSettings_Bank1Id",
                table: "PrintSettings",
                column: "Bank1Id");

            migrationBuilder.CreateIndex(
                name: "IX_PrintSettings_Bank2Id",
                table: "PrintSettings",
                column: "Bank2Id");

            migrationBuilder.CreateIndex(
                name: "IX_PrintSettings_BankAccountId",
                table: "PrintSettings",
                column: "BankAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_PrintSettings_CashAccountId",
                table: "PrintSettings",
                column: "CashAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_PrintSettings_CompanyId",
                table: "PrintSettings",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessContractors_ContractorId",
                table: "ProcessContractors",
                column: "ContractorId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessContractors_ContractorId2",
                table: "ProcessContractors",
                column: "ContractorId2");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessContractors_ProcessId",
                table: "ProcessContractors",
                column: "ProcessId");

            migrationBuilder.CreateIndex(
                name: "IX_Processes_CompanyId",
                table: "Processes",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessItems_ProcessId",
                table: "ProcessItems",
                column: "ProcessId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessItems_ProductId",
                table: "ProcessItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductGroups_CompanyId",
                table: "ProductGroups",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionBatchItems_CompanyId",
                table: "ProductionBatchItems",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionBatchItems_ProductId",
                table: "ProductionBatchItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionBatchItems_ProductionBatchId",
                table: "ProductionBatchItems",
                column: "ProductionBatchId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionBatchItems_WarehouseId",
                table: "ProductionBatchItems",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionBatchs_CompanyId",
                table: "ProductionBatchs",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionBatchs_EmployeeRegistrationId",
                table: "ProductionBatchs",
                column: "EmployeeRegistrationId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionBatchs_RecipeNoId",
                table: "ProductionBatchs",
                column: "RecipeNoId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionBatchs_SaleOrderId",
                table: "ProductionBatchs",
                column: "SaleOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionStockTransfers_CompanyId",
                table: "ProductionStockTransfers",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductMasters_CompanyId",
                table: "ProductMasters",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_BrandId",
                table: "Products",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_BundleCategoryId",
                table: "Products",
                column: "BundleCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ColorId",
                table: "Products",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CompanyId",
                table: "Products",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_OriginId",
                table: "Products",
                column: "OriginId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductGroupId",
                table: "Products",
                column: "ProductGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductMasterId",
                table: "Products",
                column: "ProductMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_PromotionOfferId",
                table: "Products",
                column: "PromotionOfferId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_SizeId",
                table: "Products",
                column: "SizeId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_TaxRateId",
                table: "Products",
                column: "TaxRateId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_UnitId",
                table: "Products",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSizes_ProductId",
                table: "ProductSizes",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSizes_SizeId",
                table: "ProductSizes",
                column: "SizeId");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionOfferItems_CompanyId",
                table: "PromotionOfferItems",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionOfferItems_GetProductId",
                table: "PromotionOfferItems",
                column: "GetProductId");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionOfferItems_ProductId",
                table: "PromotionOfferItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionOfferItems_PromotionOfferId",
                table: "PromotionOfferItems",
                column: "PromotionOfferId");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionOffers_CompanyId",
                table: "PromotionOffers",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionOffers_GetProductId",
                table: "PromotionOffers",
                column: "GetProductId");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionOffers_ProductGroupId",
                table: "PromotionOffers",
                column: "ProductGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionOffers_ProductId",
                table: "PromotionOffers",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseAttachments_CompanyId",
                table: "PurchaseAttachments",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseAttachments_PurchaseOrderId",
                table: "PurchaseAttachments",
                column: "PurchaseOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseBillItems_AccountId",
                table: "PurchaseBillItems",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseBillItems_CompanyId",
                table: "PurchaseBillItems",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseBillItems_PurchaseBillId",
                table: "PurchaseBillItems",
                column: "PurchaseBillId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseBills_BillerId",
                table: "PurchaseBills",
                column: "BillerId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseBills_CompanyId",
                table: "PurchaseBills",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseInvoiceActions_CompanyId",
                table: "PurchaseInvoiceActions",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseInvoiceActions_ProcessId",
                table: "PurchaseInvoiceActions",
                column: "ProcessId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseInvoiceActions_PurchaseInvoiceId",
                table: "PurchaseInvoiceActions",
                column: "PurchaseInvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseInvoiceActions_PurchaseInvoicePostId",
                table: "PurchaseInvoiceActions",
                column: "PurchaseInvoicePostId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseInvoiceAttachments_CompanyId",
                table: "PurchaseInvoiceAttachments",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseInvoiceAttachments_PurchaseInvoiceId",
                table: "PurchaseInvoiceAttachments",
                column: "PurchaseInvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseInvoiceAttachments_PurchaseInvoicePostId",
                table: "PurchaseInvoiceAttachments",
                column: "PurchaseInvoicePostId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseItems_CompanyId",
                table: "PurchaseItems",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseItems_ProductId",
                table: "PurchaseItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseItems_PurchaseId",
                table: "PurchaseItems",
                column: "PurchaseId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseItems_TaxRateId",
                table: "PurchaseItems",
                column: "TaxRateId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseItems_WareHouseId",
                table: "PurchaseItems",
                column: "WareHouseId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderActions_CompanyId",
                table: "PurchaseOrderActions",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderActions_ProcessId",
                table: "PurchaseOrderActions",
                column: "ProcessId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderActions_PurchaseOrderId",
                table: "PurchaseOrderActions",
                column: "PurchaseOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderExpenses_BankCashAccountId",
                table: "PurchaseOrderExpenses",
                column: "BankCashAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderExpenses_BillId",
                table: "PurchaseOrderExpenses",
                column: "BillId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderExpenses_CompanyId",
                table: "PurchaseOrderExpenses",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderExpenses_ContactAccountId",
                table: "PurchaseOrderExpenses",
                column: "ContactAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderExpenses_ExpenseTypeId",
                table: "PurchaseOrderExpenses",
                column: "ExpenseTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderExpenses_PaymentVoucherId",
                table: "PurchaseOrderExpenses",
                column: "PaymentVoucherId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderExpenses_PurchaseOrderId",
                table: "PurchaseOrderExpenses",
                column: "PurchaseOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderExpenses_TaxRateId",
                table: "PurchaseOrderExpenses",
                column: "TaxRateId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderExpenses_VatAccountId",
                table: "PurchaseOrderExpenses",
                column: "VatAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderItems_CompanyId",
                table: "PurchaseOrderItems",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderItems_ProductId",
                table: "PurchaseOrderItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderItems_PurchaseOrderId",
                table: "PurchaseOrderItems",
                column: "PurchaseOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderItems_PurchaseOrderVersionId",
                table: "PurchaseOrderItems",
                column: "PurchaseOrderVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderItems_TaxRateId",
                table: "PurchaseOrderItems",
                column: "TaxRateId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderPayments_CompanyId",
                table: "PurchaseOrderPayments",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderPayments_PaymentVoucherId",
                table: "PurchaseOrderPayments",
                column: "PaymentVoucherId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderPayments_PurchaseOrderId",
                table: "PurchaseOrderPayments",
                column: "PurchaseOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_CompanyId",
                table: "PurchaseOrders",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_SupplierId",
                table: "PurchaseOrders",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderVersions_CompanyId",
                table: "PurchaseOrderVersions",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderVersions_PurchaseOrderId",
                table: "PurchaseOrderVersions",
                column: "PurchaseOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchasePostExpenses_BillId",
                table: "PurchasePostExpenses",
                column: "BillId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchasePostExpenses_CompanyId",
                table: "PurchasePostExpenses",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchasePostExpenses_PaymentVoucherId",
                table: "PurchasePostExpenses",
                column: "PaymentVoucherId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchasePostExpenses_PurchasePostId",
                table: "PurchasePostExpenses",
                column: "PurchasePostId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchasePostExpenses_TaxRateId",
                table: "PurchasePostExpenses",
                column: "TaxRateId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchasePostItems_ColorId",
                table: "PurchasePostItems",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchasePostItems_CompanyId",
                table: "PurchasePostItems",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchasePostItems_ProductId",
                table: "PurchasePostItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchasePostItems_PurchasePostId",
                table: "PurchasePostItems",
                column: "PurchasePostId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchasePostItems_TaxRateId",
                table: "PurchasePostItems",
                column: "TaxRateId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchasePostItems_WareHouseId",
                table: "PurchasePostItems",
                column: "WareHouseId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchasePostItems_WarrantyTypeId",
                table: "PurchasePostItems",
                column: "WarrantyTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchasePosts_CompanyId",
                table: "PurchasePosts",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchasePosts_GoodReceiveNoteId",
                table: "PurchasePosts",
                column: "GoodReceiveNoteId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchasePosts_PurchaseOrderId",
                table: "PurchasePosts",
                column: "PurchaseOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchasePosts_SupplierId",
                table: "PurchasePosts",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_CompanyId",
                table: "Purchases",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_PurchaseOrderId",
                table: "Purchases",
                column: "PurchaseOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_SupplierId",
                table: "Purchases",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationTemplateItems_CompanyId",
                table: "QuotationTemplateItems",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationTemplateItems_ProductId",
                table: "QuotationTemplateItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationTemplateItems_QuotationTemplateId",
                table: "QuotationTemplateItems",
                column: "QuotationTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationTemplates_CompanyId",
                table: "QuotationTemplates",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeAssortments_RecipeNoId",
                table: "RecipeAssortments",
                column: "RecipeNoId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeItems_CompanyId",
                table: "RecipeItems",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeItems_ProductId",
                table: "RecipeItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeItems_RecipeNoId",
                table: "RecipeItems",
                column: "RecipeNoId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeItems_WareHouseId",
                table: "RecipeItems",
                column: "WareHouseId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeNos_CompanyId",
                table: "RecipeNos",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeNos_ProductId",
                table: "RecipeNos",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeNos_SampleRequestId",
                table: "RecipeNos",
                column: "SampleRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_Regions_CityId",
                table: "Regions",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Regions_CompanyId",
                table: "Regions",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_ReparingItems_CompanyId",
                table: "ReparingItems",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_ReparingItems_ProductId",
                table: "ReparingItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ReparingItems_ReparingOrderId",
                table: "ReparingItems",
                column: "ReparingOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_ReparingOrders_AcessoryIncludedId",
                table: "ReparingOrders",
                column: "AcessoryIncludedId");

            migrationBuilder.CreateIndex(
                name: "IX_ReparingOrders_CompanyId",
                table: "ReparingOrders",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_ReparingOrders_CustomerId",
                table: "ReparingOrders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_ReparingOrders_EmployeeId",
                table: "ReparingOrders",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_ReparingOrders_ProblemId",
                table: "ReparingOrders",
                column: "ProblemId");

            migrationBuilder.CreateIndex(
                name: "IX_ReparingOrders_UpsDescriptionId",
                table: "ReparingOrders",
                column: "UpsDescriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_ReparingOrders_WarrantyCategoryId",
                table: "ReparingOrders",
                column: "WarrantyCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ReparingOrderTypes_CompanyId",
                table: "ReparingOrderTypes",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_RolesPermissions_CompanyId",
                table: "RolesPermissions",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_RolesPermissions_RoleId",
                table: "RolesPermissions",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_RunPayrollDetails_CompanyId",
                table: "RunPayrollDetails",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_RunPayrollDetails_RunPayrollId",
                table: "RunPayrollDetails",
                column: "RunPayrollId");

            migrationBuilder.CreateIndex(
                name: "IX_RunPayrolls_CompanyId",
                table: "RunPayrolls",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_RunPayrolls_PayrollScheduleId",
                table: "RunPayrolls",
                column: "PayrollScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_RunPayrollSalaryDetails_CompanyId",
                table: "RunPayrollSalaryDetails",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_RunPayrollSalaryDetails_RunPayrollDetailId",
                table: "RunPayrollSalaryDetails",
                column: "RunPayrollDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_SalaryAllowances_AllowanceId",
                table: "SalaryAllowances",
                column: "AllowanceId");

            migrationBuilder.CreateIndex(
                name: "IX_SalaryAllowances_SalaryTemplateId",
                table: "SalaryAllowances",
                column: "SalaryTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_SalaryContributions_ContributionId",
                table: "SalaryContributions",
                column: "ContributionId");

            migrationBuilder.CreateIndex(
                name: "IX_SalaryContributions_SalaryTemplateId",
                table: "SalaryContributions",
                column: "SalaryTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_SalaryDeductions_DeductionId",
                table: "SalaryDeductions",
                column: "DeductionId");

            migrationBuilder.CreateIndex(
                name: "IX_SalaryDeductions_SalaryTemplateId",
                table: "SalaryDeductions",
                column: "SalaryTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_SalaryTaxSlabs_CompanyId",
                table: "SalaryTaxSlabs",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_SalaryTaxSlabs_TaxSalaryId",
                table: "SalaryTaxSlabs",
                column: "TaxSalaryId");

            migrationBuilder.CreateIndex(
                name: "IX_SalaryTemplates_CompanyId",
                table: "SalaryTemplates",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleInvoiceAdvancePayments_CompanyId",
                table: "SaleInvoiceAdvancePayments",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleInvoiceAdvancePayments_SaleInvoiceId",
                table: "SaleInvoiceAdvancePayments",
                column: "SaleInvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleInvoiceAdvancePayments_SaleOrderId",
                table: "SaleInvoiceAdvancePayments",
                column: "SaleOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleInvoiceDiscounts_CompanyId",
                table: "SaleInvoiceDiscounts",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleInvoiceDiscounts_DiscountId",
                table: "SaleInvoiceDiscounts",
                column: "DiscountId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleInvoiceDiscounts_SaleId",
                table: "SaleInvoiceDiscounts",
                column: "SaleId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleItems_BundleId",
                table: "SaleItems",
                column: "BundleId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleItems_ColorId",
                table: "SaleItems",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleItems_CompanyId",
                table: "SaleItems",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleItems_ProductId",
                table: "SaleItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleItems_PromotionId",
                table: "SaleItems",
                column: "PromotionId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleItems_PromotionItemId",
                table: "SaleItems",
                column: "PromotionItemId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleItems_SaleId",
                table: "SaleItems",
                column: "SaleId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleItems_TaxRateId",
                table: "SaleItems",
                column: "TaxRateId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleItems_WareHouseId",
                table: "SaleItems",
                column: "WareHouseId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleOrderItems_CompanyId",
                table: "SaleOrderItems",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleOrderItems_ProductId",
                table: "SaleOrderItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleOrderItems_SaleOrderId",
                table: "SaleOrderItems",
                column: "SaleOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleOrderItems_SaleOrderVersionId",
                table: "SaleOrderItems",
                column: "SaleOrderVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleOrderItems_TaxRateId",
                table: "SaleOrderItems",
                column: "TaxRateId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleOrderItems_WareHouseId",
                table: "SaleOrderItems",
                column: "WareHouseId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleOrderPayments_CompanyId",
                table: "SaleOrderPayments",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleOrderPayments_PaymentVoucherId",
                table: "SaleOrderPayments",
                column: "PaymentVoucherId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleOrderPayments_SaleOrderId",
                table: "SaleOrderPayments",
                column: "SaleOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleOrders_CommodityId",
                table: "SaleOrders",
                column: "CommodityId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleOrders_CompanyId",
                table: "SaleOrders",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleOrders_CustomerId",
                table: "SaleOrders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleOrders_IncotermsId",
                table: "SaleOrders",
                column: "IncotermsId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleOrders_LogisticId",
                table: "SaleOrders",
                column: "LogisticId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleOrders_OrderTypeId",
                table: "SaleOrders",
                column: "OrderTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleOrders_TaxRateId",
                table: "SaleOrders",
                column: "TaxRateId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleOrderVersions_CompanyId",
                table: "SaleOrderVersions",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleOrderVersions_SaleOrderId",
                table: "SaleOrderVersions",
                column: "SaleOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_SalePayments_CompanyId",
                table: "SalePayments",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_SalePayments_PaymentOptionId",
                table: "SalePayments",
                column: "PaymentOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_SalePayments_SaleId",
                table: "SalePayments",
                column: "SaleId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_AreaId",
                table: "Sales",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_CashCustomerId",
                table: "Sales",
                column: "CashCustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_CompanyId",
                table: "Sales",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_CustomerId",
                table: "Sales",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_EmployeeId",
                table: "Sales",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_LogisticId",
                table: "Sales",
                column: "LogisticId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_OtherCurrencyId",
                table: "Sales",
                column: "OtherCurrencyId",
                unique: true,
                filter: "[OtherCurrencyId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_QuotationId",
                table: "Sales",
                column: "QuotationId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_SaleOrderId",
                table: "Sales",
                column: "SaleOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_TaxRateId",
                table: "Sales",
                column: "TaxRateId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_UnRegisteredVatId",
                table: "Sales",
                column: "UnRegisteredVatId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleSizeAssortments_CompanyId",
                table: "SaleSizeAssortments",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleSizeAssortments_PurchasePostItemId",
                table: "SaleSizeAssortments",
                column: "PurchasePostItemId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleSizeAssortments_SaleId",
                table: "SaleSizeAssortments",
                column: "SaleId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleSizeAssortments_SaleItemId",
                table: "SaleSizeAssortments",
                column: "SaleItemId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleSizeAssortments_SizeId",
                table: "SaleSizeAssortments",
                column: "SizeId");

            migrationBuilder.CreateIndex(
                name: "IX_SampleRequestItems_CompanyId",
                table: "SampleRequestItems",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_SampleRequestItems_ProductId",
                table: "SampleRequestItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_SampleRequestItems_SampleRequestId",
                table: "SampleRequestItems",
                column: "SampleRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_SampleRequests_CompanyId",
                table: "SampleRequests",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_SampleRequests_CustomerId",
                table: "SampleRequests",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_SampleRequests_ProductId",
                table: "SampleRequests",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ShiftAssigns_CompanyId",
                table: "ShiftAssigns",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_ShiftAssigns_EmployeeId",
                table: "ShiftAssigns",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_ShiftEmployeeAssigns_CompanyId",
                table: "ShiftEmployeeAssigns",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_ShiftEmployeeAssigns_EmployeeId",
                table: "ShiftEmployeeAssigns",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_ShiftEmployeeAssigns_ShiftAssignId",
                table: "ShiftEmployeeAssigns",
                column: "ShiftAssignId");

            migrationBuilder.CreateIndex(
                name: "IX_ShiftRunEmployees_CompanyId",
                table: "ShiftRunEmployees",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_ShiftRunEmployees_EmployeeId",
                table: "ShiftRunEmployees",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_ShiftRunEmployees_ShiftRunId",
                table: "ShiftRunEmployees",
                column: "ShiftRunId");

            migrationBuilder.CreateIndex(
                name: "IX_ShiftRuns_CompanyId",
                table: "ShiftRuns",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_ShiftRuns_ShiftAssignId",
                table: "ShiftRuns",
                column: "ShiftAssignId");

            migrationBuilder.CreateIndex(
                name: "IX_Sizes_CompanyId",
                table: "Sizes",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_StockAdjustmentDetails_CompanyId",
                table: "StockAdjustmentDetails",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_StockAdjustmentDetails_ProductId",
                table: "StockAdjustmentDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_StockAdjustmentDetails_StockAdjustmentId",
                table: "StockAdjustmentDetails",
                column: "StockAdjustmentId");

            migrationBuilder.CreateIndex(
                name: "IX_StockAdjustmentDetails_WarehouseId",
                table: "StockAdjustmentDetails",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_StockAdjustmentDetails_WarrantyTypeId",
                table: "StockAdjustmentDetails",
                column: "WarrantyTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_StockAdjustments_CompanyId",
                table: "StockAdjustments",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_StockAdjustments_TaxRateId",
                table: "StockAdjustments",
                column: "TaxRateId");

            migrationBuilder.CreateIndex(
                name: "IX_StockAdjustments_WarehouseId",
                table: "StockAdjustments",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_StockReceived_CompanyId",
                table: "StockReceived",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_StockReceived_StockTransferId",
                table: "StockReceived",
                column: "StockTransferId");

            migrationBuilder.CreateIndex(
                name: "IX_StockReceivedItems_CompanyId",
                table: "StockReceivedItems",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_StockReceivedItems_ProductId",
                table: "StockReceivedItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_StockReceivedItems_StockReceivedId",
                table: "StockReceivedItems",
                column: "StockReceivedId");

            migrationBuilder.CreateIndex(
                name: "IX_StockRequestItems_CompanyId",
                table: "StockRequestItems",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_StockRequestItems_ProductId",
                table: "StockRequestItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_StockRequestItems_StockRequestId",
                table: "StockRequestItems",
                column: "StockRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_StockRequests_CompanyId",
                table: "StockRequests",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_StockRequests_WarehouseId",
                table: "StockRequests",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_CompanyId",
                table: "Stocks",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_ProductId",
                table: "Stocks",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_WareHouseId",
                table: "Stocks",
                column: "WareHouseId");

            migrationBuilder.CreateIndex(
                name: "IX_StockTransferItems_CompanyId",
                table: "StockTransferItems",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_StockTransferItems_ProductId",
                table: "StockTransferItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_StockTransferItems_StockTransferId",
                table: "StockTransferItems",
                column: "StockTransferId");

            migrationBuilder.CreateIndex(
                name: "IX_StockTransfers_CompanyId",
                table: "StockTransfers",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_StockTransfers_StockRequestId",
                table: "StockTransfers",
                column: "StockRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_SubCategories_CategoryId",
                table: "SubCategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SubCategories_CompanyId",
                table: "SubCategories",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_TaxRates_CompanyId",
                table: "TaxRates",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_TaxSalaries_CompanyId",
                table: "TaxSalaries",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_TemporaryCashAllocations_BankCashAccountId",
                table: "TemporaryCashAllocations",
                column: "BankCashAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_TemporaryCashAllocations_CompanyId",
                table: "TemporaryCashAllocations",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_TemporaryCashIssueExpenses_CompanyId",
                table: "TemporaryCashIssueExpenses",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_TemporaryCashIssueExpenses_TemporaryCashIssueId",
                table: "TemporaryCashIssueExpenses",
                column: "TemporaryCashIssueId");

            migrationBuilder.CreateIndex(
                name: "IX_TemporaryCashIssueItems_CompanyId",
                table: "TemporaryCashIssueItems",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_TemporaryCashIssueItems_TemporaryCashIssueId",
                table: "TemporaryCashIssueItems",
                column: "TemporaryCashIssueId");

            migrationBuilder.CreateIndex(
                name: "IX_TemporaryCashIssues_CompanyId",
                table: "TemporaryCashIssues",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_TemporaryCashIssues_TemporaryCashRequestId",
                table: "TemporaryCashIssues",
                column: "TemporaryCashRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_TemporaryCashRequestItems_CompanyId",
                table: "TemporaryCashRequestItems",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_TemporaryCashRequestItems_TemporaryCashRequestId",
                table: "TemporaryCashRequestItems",
                column: "TemporaryCashRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_TemporaryCashRequests_CompanyId",
                table: "TemporaryCashRequests",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_TemporaryCashReturns_CompanyId",
                table: "TemporaryCashReturns",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_TemporaryCashReturns_TemporaryCashIssueId",
                table: "TemporaryCashReturns",
                column: "TemporaryCashIssueId");

            migrationBuilder.CreateIndex(
                name: "IX_TerminalCategories_CategoryId",
                table: "TerminalCategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TerminalCategories_CompanyId",
                table: "TerminalCategories",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_TerminalCategories_TerminalId",
                table: "TerminalCategories",
                column: "TerminalId");

            migrationBuilder.CreateIndex(
                name: "IX_Terminals_AccountId",
                table: "Terminals",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Terminals_CashAccountId",
                table: "Terminals",
                column: "CashAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Terminals_CompanyId",
                table: "Terminals",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Terminals_PosTerminalId",
                table: "Terminals",
                column: "PosTerminalId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionApplicationLogs_LocationId",
                table: "TransactionApplicationLogs",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_AccountId",
                table: "Transactions",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_CompanyId",
                table: "Transactions",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_PeriodId",
                table: "Transactions",
                column: "PeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferHistories_CompanyId",
                table: "TransferHistories",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Transporters_CompanyId",
                table: "Transporters",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Units_CompanyId",
                table: "Units",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDefineFlows_CompanyId",
                table: "UserDefineFlows",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogs_CompanyId",
                table: "UserLogs",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTerminals_CompanyId",
                table: "UserTerminals",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTerminals_TerminalId",
                table: "UserTerminals",
                column: "TerminalId");

            migrationBuilder.CreateIndex(
                name: "IX_VariationInventories_CompanyId",
                table: "VariationInventories",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_VariationInventoryForReportings_CompanyId",
                table: "VariationInventoryForReportings",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Warehouses_CompanyId",
                table: "Warehouses",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_WareHouseTransferItems_CompanyId",
                table: "WareHouseTransferItems",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_WareHouseTransferItems_ProductId",
                table: "WareHouseTransferItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_WareHouseTransferItems_WareHouseTransferId",
                table: "WareHouseTransferItems",
                column: "WareHouseTransferId");

            migrationBuilder.CreateIndex(
                name: "IX_WareHouseTransfers_CompanyId",
                table: "WareHouseTransfers",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_WareHouseTransfers_ToWareHouseId",
                table: "WareHouseTransfers",
                column: "ToWareHouseId");

            migrationBuilder.CreateIndex(
                name: "IX_WarrantyTypes_CompanyId",
                table: "WarrantyTypes",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_WeekHolidays_HolidayId",
                table: "WeekHolidays",
                column: "HolidayId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkWeeks_CompanyId",
                table: "WorkWeeks",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_YearlyPeriods_CompanyId",
                table: "YearlyPeriods",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Zones_CompanyId",
                table: "Zones",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_AutoPurchaseTemplateItems_Products_ProductId",
                table: "AutoPurchaseTemplateItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BatchCostingItems_BatchCostings_BatchCostingId",
                table: "BatchCostingItems",
                column: "BatchCostingId",
                principalTable: "BatchCostings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BatchCostings_RecipeNos_RecipeNoId",
                table: "BatchCostings",
                column: "RecipeNoId",
                principalTable: "RecipeNos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BatchProcessContractors_BatchProcesses_BatchProcessId",
                table: "BatchProcessContractors",
                column: "BatchProcessId",
                principalTable: "BatchProcesses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BatchProcesses_ProductionBatchs_ProductionBatchId",
                table: "BatchProcesses",
                column: "ProductionBatchId",
                principalTable: "ProductionBatchs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BomSaleOrderItems_Products_ProductId",
                table: "BomSaleOrderItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BomRawProducts_Products_ProductId",
                table: "BomRawProducts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BranchItems_Products_ProductId",
                table: "BranchItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BundleCategories_Products_ProductId",
                table: "BundleCategories",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BundleOfferBranches_PromotionOffers_PromotionOfferId",
                table: "BundleOfferBranches",
                column: "PromotionOfferId",
                principalTable: "PromotionOffers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ContractorItems_Products_ProductId",
                table: "ContractorItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CreditNoteItems_Products_ProductId",
                table: "CreditNoteItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryChallanItems_Products_ProductId",
                table: "DeliveryChallanItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryChallanReserverdItems_Products_ProductId",
                table: "DeliveryChallanReserverdItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DispatchNoteItems_Products_ProductId",
                table: "DispatchNoteItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GatePasses_ProductionBatchs_ProductionBatchId",
                table: "GatePasses",
                column: "ProductionBatchId",
                principalTable: "ProductionBatchs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GatePassItems_Products_ProductId",
                table: "GatePassItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GoodReceiveNoteItems_Products_ProductId",
                table: "GoodReceiveNoteItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InquiryItems_Products_ItemId",
                table: "InquiryItems",
                column: "ItemId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryBlindDetails_Products_ProductId",
                table: "InventoryBlindDetails",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MobileOrderItems_Products_ProductId",
                table: "MobileOrderItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PriceRecords_Products_ProductId",
                table: "PriceRecords",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProcessItems_Products_ProductId",
                table: "ProcessItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductionBatchs_RecipeNos_RecipeNoId",
                table: "ProductionBatchs",
                column: "RecipeNoId",
                principalTable: "RecipeNos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductionBatchItems_Products_ProductId",
                table: "ProductionBatchItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_PromotionOffers_PromotionOfferId",
                table: "Products",
                column: "PromotionOfferId",
                principalTable: "PromotionOffers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_CostCenters_CostCenterId",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_TaxRates_TaxRateId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Accounts_COGSAccountId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Accounts_IncomeAccountId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Accounts_InventoryAccountId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Accounts_PurchaseAccountId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Accounts_SaleAccountId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_BundleCategories_BundleCategoryId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_PromotionOffers_Products_GetProductId",
                table: "PromotionOffers");

            migrationBuilder.DropForeignKey(
                name: "FK_PromotionOffers_Products_ProductId",
                table: "PromotionOffers");

            migrationBuilder.DropTable(
                name: "AccountsLevelOne");

            migrationBuilder.DropTable(
                name: "AccountsLevelTwo");

            migrationBuilder.DropTable(
                name: "AccountTemplates");

            migrationBuilder.DropTable(
                name: "ApplicationUpdates");

            migrationBuilder.DropTable(
                name: "ApprovalSystemEmployees");

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
                name: "AutoPurchaseSettings");

            migrationBuilder.DropTable(
                name: "AutoPurchaseTemplateItems");

            migrationBuilder.DropTable(
                name: "BatchCostingItems");

            migrationBuilder.DropTable(
                name: "BillAttachments");

            migrationBuilder.DropTable(
                name: "BomRawProducts");

            migrationBuilder.DropTable(
                name: "BranchItems");

            migrationBuilder.DropTable(
                name: "BranchSetups");

            migrationBuilder.DropTable(
                name: "BranchUsers");

            migrationBuilder.DropTable(
                name: "BranchVouchers");

            migrationBuilder.DropTable(
                name: "BranchWisePermissions");

            migrationBuilder.DropTable(
                name: "BundleOfferBranches");

            migrationBuilder.DropTable(
                name: "CashRequestUsers");

            migrationBuilder.DropTable(
                name: "CashVouchers");

            migrationBuilder.DropTable(
                name: "ChequeBookItems");

            migrationBuilder.DropTable(
                name: "CompanyAccountSetups");

            migrationBuilder.DropTable(
                name: "CompanyAttachments");

            migrationBuilder.DropTable(
                name: "CompanyHolidays");

            migrationBuilder.DropTable(
                name: "companyInformations");

            migrationBuilder.DropTable(
                name: "CompanyLicences");

            migrationBuilder.DropTable(
                name: "CompanyOptions");

            migrationBuilder.DropTable(
                name: "ContactAttachments");

            migrationBuilder.DropTable(
                name: "ContactBankAccounts");

            migrationBuilder.DropTable(
                name: "ContactPersons");

            migrationBuilder.DropTable(
                name: "ContractorItems");

            migrationBuilder.DropTable(
                name: "ContractorPayments");

            migrationBuilder.DropTable(
                name: "CreditNoteItems");

            migrationBuilder.DropTable(
                name: "CustomerDiscount");

            migrationBuilder.DropTable(
                name: "DailyExpenseAttachments");

            migrationBuilder.DropTable(
                name: "DailyExpenseDetails");

            migrationBuilder.DropTable(
                name: "DayStarts");

            migrationBuilder.DropTable(
                name: "DefaultSettings");

            migrationBuilder.DropTable(
                name: "DeliveryChallanItems");

            migrationBuilder.DropTable(
                name: "DeliveryChallanReserverdItems");

            migrationBuilder.DropTable(
                name: "DeliveryHolidays");

            migrationBuilder.DropTable(
                name: "DenominationSetups");

            migrationBuilder.DropTable(
                name: "DiscountSetups");

            migrationBuilder.DropTable(
                name: "DispatchNoteItems");

            migrationBuilder.DropTable(
                name: "EmailConfiguration");

            migrationBuilder.DropTable(
                name: "EmployeeAttachments");

            migrationBuilder.DropTable(
                name: "EmployeeDepartments");

            migrationBuilder.DropTable(
                name: "EmployeeRegistrationAttachments");

            migrationBuilder.DropTable(
                name: "EmployeeSalaryDetails");

            migrationBuilder.DropTable(
                name: "FinancialYearClosingBalances");

            migrationBuilder.DropTable(
                name: "FinancialYearSettings");

            migrationBuilder.DropTable(
                name: "GatePassItems");

            migrationBuilder.DropTable(
                name: "GoodReceiveNoteItems");

            migrationBuilder.DropTable(
                name: "GsmSmsSetups");

            migrationBuilder.DropTable(
                name: "GuestedHolidays");

            migrationBuilder.DropTable(
                name: "HoldSetups");

            migrationBuilder.DropTable(
                name: "ImportExportItems");

            migrationBuilder.DropTable(
                name: "InquiryComments");

            migrationBuilder.DropTable(
                name: "InquiryEmailCcs");

            migrationBuilder.DropTable(
                name: "InquiryItems");

            migrationBuilder.DropTable(
                name: "InquiryMeetingAttendants");

            migrationBuilder.DropTable(
                name: "InquiryModuleQuestions");

            migrationBuilder.DropTable(
                name: "InquiryStatuses");

            migrationBuilder.DropTable(
                name: "Inventories");

            migrationBuilder.DropTable(
                name: "InventoryBlindDetails");

            migrationBuilder.DropTable(
                name: "InvoiceDefaults");

            migrationBuilder.DropTable(
                name: "Issueds");

            migrationBuilder.DropTable(
                name: "ItemsListDisplayOrderSetup");

            migrationBuilder.DropTable(
                name: "ItemViewSetupForPrint");

            migrationBuilder.DropTable(
                name: "ItemViewSetups");

            migrationBuilder.DropTable(
                name: "JournalVoucherAttachment");

            migrationBuilder.DropTable(
                name: "JournalVoucherItems");

            migrationBuilder.DropTable(
                name: "LeaveGroupEmployees");

            migrationBuilder.DropTable(
                name: "LeaveHolidays");

            migrationBuilder.DropTable(
                name: "LeaveRules");

            migrationBuilder.DropTable(
                name: "LedgerAccounts");

            migrationBuilder.DropTable(
                name: "LedgerTransactions");

            migrationBuilder.DropTable(
                name: "ListOrderSetups");

            migrationBuilder.DropTable(
                name: "LoanPays");

            migrationBuilder.DropTable(
                name: "LoginHistories");

            migrationBuilder.DropTable(
                name: "LoginPermissions");

            migrationBuilder.DropTable(
                name: "ManualAttendances");

            migrationBuilder.DropTable(
                name: "MobileOrderItems");

            migrationBuilder.DropTable(
                name: "ModuleQuestions");

            migrationBuilder.DropTable(
                name: "ModulesRights");

            migrationBuilder.DropTable(
                name: "MonthlyCostItems");

            migrationBuilder.DropTable(
                name: "MultiUPSLineItems");

            migrationBuilder.DropTable(
                name: "NoblePermissions");

            migrationBuilder.DropTable(
                name: "NobleRolePermissions");

            migrationBuilder.DropTable(
                name: "NobleUserRoles");

            migrationBuilder.DropTable(
                name: "PaidTimeOffs");

            migrationBuilder.DropTable(
                name: "PaymentLimits");

            migrationBuilder.DropTable(
                name: "PaymentRefunds");

            migrationBuilder.DropTable(
                name: "PaymentVoucherAttachments");

            migrationBuilder.DropTable(
                name: "PaymentVoucherDetails");

            migrationBuilder.DropTable(
                name: "PaymentVoucherItems");

            migrationBuilder.DropTable(
                name: "PermanentDeleteHoldSetups");

            migrationBuilder.DropTable(
                name: "Prefixies");

            migrationBuilder.DropTable(
                name: "PriceRecords");

            migrationBuilder.DropTable(
                name: "PrintOptions");

            migrationBuilder.DropTable(
                name: "ProcessContractors");

            migrationBuilder.DropTable(
                name: "ProcessItems");

            migrationBuilder.DropTable(
                name: "ProductionBatchItems");

            migrationBuilder.DropTable(
                name: "ProductionStockTransfers");

            migrationBuilder.DropTable(
                name: "ProductSizes");

            migrationBuilder.DropTable(
                name: "PurchaseAttachments");

            migrationBuilder.DropTable(
                name: "PurchaseBillItems");

            migrationBuilder.DropTable(
                name: "PurchaseInvoiceActions");

            migrationBuilder.DropTable(
                name: "PurchaseInvoiceAttachments");

            migrationBuilder.DropTable(
                name: "PurchaseItems");

            migrationBuilder.DropTable(
                name: "PurchaseOrderActions");

            migrationBuilder.DropTable(
                name: "PurchaseOrderItems");

            migrationBuilder.DropTable(
                name: "PurchaseOrderPayments");

            migrationBuilder.DropTable(
                name: "PurchasePostExpenses");

            migrationBuilder.DropTable(
                name: "QuotationTemplateItems");

            migrationBuilder.DropTable(
                name: "RecipeAssortments");

            migrationBuilder.DropTable(
                name: "RecipeItems");

            migrationBuilder.DropTable(
                name: "ReparingItems");

            migrationBuilder.DropTable(
                name: "RolesPermissions");

            migrationBuilder.DropTable(
                name: "RunPayrollSalaryDetails");

            migrationBuilder.DropTable(
                name: "SalaryAllowances");

            migrationBuilder.DropTable(
                name: "SalaryContributions");

            migrationBuilder.DropTable(
                name: "SalaryDeductions");

            migrationBuilder.DropTable(
                name: "SalaryTaxSlabs");

            migrationBuilder.DropTable(
                name: "SaleInvoiceAdvancePayments");

            migrationBuilder.DropTable(
                name: "SaleInvoiceDiscounts");

            migrationBuilder.DropTable(
                name: "SaleOrderItems");

            migrationBuilder.DropTable(
                name: "SaleOrderPayments");

            migrationBuilder.DropTable(
                name: "SalePayments");

            migrationBuilder.DropTable(
                name: "SaleSizeAssortments");

            migrationBuilder.DropTable(
                name: "SampleRequestItems");

            migrationBuilder.DropTable(
                name: "ShiftEmployeeAssigns");

            migrationBuilder.DropTable(
                name: "ShiftRunEmployees");

            migrationBuilder.DropTable(
                name: "StockAdjustmentDetails");

            migrationBuilder.DropTable(
                name: "StockReceivedItems");

            migrationBuilder.DropTable(
                name: "StockRequestItems");

            migrationBuilder.DropTable(
                name: "StockTransferItems");

            migrationBuilder.DropTable(
                name: "SubCategories");

            migrationBuilder.DropTable(
                name: "SyncPushPullRecords");

            migrationBuilder.DropTable(
                name: "SyncRecords");

            migrationBuilder.DropTable(
                name: "TemporaryCashAllocations");

            migrationBuilder.DropTable(
                name: "TemporaryCashIssueExpenses");

            migrationBuilder.DropTable(
                name: "TemporaryCashIssueItems");

            migrationBuilder.DropTable(
                name: "TemporaryCashRequestItems");

            migrationBuilder.DropTable(
                name: "TemporaryCashReturns");

            migrationBuilder.DropTable(
                name: "TerminalCategories");

            migrationBuilder.DropTable(
                name: "TransactionApplicationLogs");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "TransferHistories");

            migrationBuilder.DropTable(
                name: "Transporters");

            migrationBuilder.DropTable(
                name: "UserDefineFlows");

            migrationBuilder.DropTable(
                name: "UserLogs");

            migrationBuilder.DropTable(
                name: "UserTerminals");

            migrationBuilder.DropTable(
                name: "VariationInventories");

            migrationBuilder.DropTable(
                name: "VariationInventoryForReportings");

            migrationBuilder.DropTable(
                name: "WareHouseTransferItems");

            migrationBuilder.DropTable(
                name: "WhiteLabelings");

            migrationBuilder.DropTable(
                name: "WorkWeeks");

            migrationBuilder.DropTable(
                name: "ApprovalSystems");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "AutoPurchaseTemplates");

            migrationBuilder.DropTable(
                name: "BatchCostings");

            migrationBuilder.DropTable(
                name: "BomSaleOrderItems");

            migrationBuilder.DropTable(
                name: "Branches");

            migrationBuilder.DropTable(
                name: "ChequeBooks");

            migrationBuilder.DropTable(
                name: "CreditNotes");

            migrationBuilder.DropTable(
                name: "DailyExpenses");

            migrationBuilder.DropTable(
                name: "DeliveryChallans");

            migrationBuilder.DropTable(
                name: "DeliveryChallanReserveds");

            migrationBuilder.DropTable(
                name: "DeliveryAddresses");

            migrationBuilder.DropTable(
                name: "WeekHolidays");

            migrationBuilder.DropTable(
                name: "DispatchNotes");

            migrationBuilder.DropTable(
                name: "Attachments");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "EmployeeSalaries");

            migrationBuilder.DropTable(
                name: "FinancialYearClosings");

            migrationBuilder.DropTable(
                name: "GatePasses");

            migrationBuilder.DropTable(
                name: "InquiryEmails");

            migrationBuilder.DropTable(
                name: "InquiryMeetings");

            migrationBuilder.DropTable(
                name: "Stocks");

            migrationBuilder.DropTable(
                name: "InventoryBlinds");

            migrationBuilder.DropTable(
                name: "JournalVouchers");

            migrationBuilder.DropTable(
                name: "LoanPayments");

            migrationBuilder.DropTable(
                name: "MobileOrders");

            migrationBuilder.DropTable(
                name: "InquiryModules");

            migrationBuilder.DropTable(
                name: "ModulesNames");

            migrationBuilder.DropTable(
                name: "MonthlyCosts");

            migrationBuilder.DropTable(
                name: "CompanyPermissions");

            migrationBuilder.DropTable(
                name: "LeavePeriods");

            migrationBuilder.DropTable(
                name: "LeaveTypes");

            migrationBuilder.DropTable(
                name: "PurchaseOrderExpenses");

            migrationBuilder.DropTable(
                name: "PriceLabelings");

            migrationBuilder.DropTable(
                name: "PrintSettings");

            migrationBuilder.DropTable(
                name: "Purchases");

            migrationBuilder.DropTable(
                name: "CompanyProcess");

            migrationBuilder.DropTable(
                name: "PurchaseOrderVersions");

            migrationBuilder.DropTable(
                name: "QuotationTemplates");

            migrationBuilder.DropTable(
                name: "NobleRoles");

            migrationBuilder.DropTable(
                name: "RunPayrollDetails");

            migrationBuilder.DropTable(
                name: "Allowances");

            migrationBuilder.DropTable(
                name: "Contributions");

            migrationBuilder.DropTable(
                name: "Deductions");

            migrationBuilder.DropTable(
                name: "SalaryTemplates");

            migrationBuilder.DropTable(
                name: "TaxSalaries");

            migrationBuilder.DropTable(
                name: "Discounts");

            migrationBuilder.DropTable(
                name: "SaleOrderVersions");

            migrationBuilder.DropTable(
                name: "PaymentOptions");

            migrationBuilder.DropTable(
                name: "PurchasePostItems");

            migrationBuilder.DropTable(
                name: "SaleItems");

            migrationBuilder.DropTable(
                name: "ShiftRuns");

            migrationBuilder.DropTable(
                name: "StockAdjustments");

            migrationBuilder.DropTable(
                name: "StockReceived");

            migrationBuilder.DropTable(
                name: "TemporaryCashIssues");

            migrationBuilder.DropTable(
                name: "CompanySubmissionPeriods");

            migrationBuilder.DropTable(
                name: "Terminals");

            migrationBuilder.DropTable(
                name: "WareHouseTransfers");

            migrationBuilder.DropTable(
                name: "Boms");

            migrationBuilder.DropTable(
                name: "Holidays");

            migrationBuilder.DropTable(
                name: "Zones");

            migrationBuilder.DropTable(
                name: "BatchProcessContractors");

            migrationBuilder.DropTable(
                name: "Inquiries");

            migrationBuilder.DropTable(
                name: "NobleGroups");

            migrationBuilder.DropTable(
                name: "NobleModules");

            migrationBuilder.DropTable(
                name: "LeaveGroups");

            migrationBuilder.DropTable(
                name: "ExpenseTypes");

            migrationBuilder.DropTable(
                name: "PaymentVouchers");

            migrationBuilder.DropTable(
                name: "RunPayrolls");

            migrationBuilder.DropTable(
                name: "AllowanceTypes");

            migrationBuilder.DropTable(
                name: "PurchasePosts");

            migrationBuilder.DropTable(
                name: "WarrantyTypes");

            migrationBuilder.DropTable(
                name: "PromotionOfferItems");

            migrationBuilder.DropTable(
                name: "Sales");

            migrationBuilder.DropTable(
                name: "ShiftAssigns");

            migrationBuilder.DropTable(
                name: "StockTransfers");

            migrationBuilder.DropTable(
                name: "TemporaryCashRequests");

            migrationBuilder.DropTable(
                name: "YearlyPeriods");

            migrationBuilder.DropTable(
                name: "BankPosTerminals");

            migrationBuilder.DropTable(
                name: "BatchProcesses");

            migrationBuilder.DropTable(
                name: "Contractors");

            migrationBuilder.DropTable(
                name: "InquiryPriorities");

            migrationBuilder.DropTable(
                name: "InquiryProcesses");

            migrationBuilder.DropTable(
                name: "InquiryStatusDynamics");

            migrationBuilder.DropTable(
                name: "InquiryTypes");

            migrationBuilder.DropTable(
                name: "MediaTypes");

            migrationBuilder.DropTable(
                name: "MultiUps");

            migrationBuilder.DropTable(
                name: "PurchaseBills");

            migrationBuilder.DropTable(
                name: "ReparingOrders");

            migrationBuilder.DropTable(
                name: "PaySchedules");

            migrationBuilder.DropTable(
                name: "GoodReceives");

            migrationBuilder.DropTable(
                name: "CashCustomer");

            migrationBuilder.DropTable(
                name: "OtherCurrencies");

            migrationBuilder.DropTable(
                name: "StockRequests");

            migrationBuilder.DropTable(
                name: "Processes");

            migrationBuilder.DropTable(
                name: "ProductionBatchs");

            migrationBuilder.DropTable(
                name: "ReparingOrderTypes");

            migrationBuilder.DropTable(
                name: "PurchaseOrders");

            migrationBuilder.DropTable(
                name: "Warehouses");

            migrationBuilder.DropTable(
                name: "EmployeeRegistrations");

            migrationBuilder.DropTable(
                name: "RecipeNos");

            migrationBuilder.DropTable(
                name: "SaleOrders");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Designations");

            migrationBuilder.DropTable(
                name: "SampleRequests");

            migrationBuilder.DropTable(
                name: "ImportExportTypes");

            migrationBuilder.DropTable(
                name: "Logistics");

            migrationBuilder.DropTable(
                name: "Banks");

            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "Currencies");

            migrationBuilder.DropTable(
                name: "CustomerGroups");

            migrationBuilder.DropTable(
                name: "Regions");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "CostCenters");

            migrationBuilder.DropTable(
                name: "AccountTypes");

            migrationBuilder.DropTable(
                name: "TaxRates");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "BundleCategories");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Colors");

            migrationBuilder.DropTable(
                name: "Origins");

            migrationBuilder.DropTable(
                name: "ProductMasters");

            migrationBuilder.DropTable(
                name: "PromotionOffers");

            migrationBuilder.DropTable(
                name: "Sizes");

            migrationBuilder.DropTable(
                name: "Units");

            migrationBuilder.DropTable(
                name: "ProductGroups");

            migrationBuilder.DropTable(
                name: "Companies");
        }
    }
}
