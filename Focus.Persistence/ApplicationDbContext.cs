using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Interface;
using Focus.Business.Models;
using Focus.Business.Users;
using Focus.Domain.Entities;
using Focus.Domain.Interface;
using Focus.Persistence.Extensions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ModelBuilder = Microsoft.EntityFrameworkCore.ModelBuilder;
using Size = Focus.Domain.Entities.Size;

namespace Focus.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
    {
        private readonly IUserHttpContextProvider _httpContextProvider;
        private readonly ILogger _logger;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IUserHttpContextProvider httpContext, ILogger<ApplicationDbContext> logger) : base(options)
        {
            _logger = logger;
            _httpContextProvider = httpContext;
        }
        public virtual DbSet<FinancialYearClosing> FinancialYearClosings { get; set; }
        public virtual DbSet<FinancialYearClosingBalance> FinancialYearClosingBalances { get; set; }
        public virtual DbSet<FinancialYearSetting> FinancialYearSettings { get; set; }
        public virtual DbSet<BomRawProducts> BomRawProducts { get; set; }
        public virtual DbSet<EmailConfiguration> EmailConfiguration { get; set; }
        public virtual DbSet<BomSaleOrderItems> BomSaleOrderItems { get; set; }
        public virtual DbSet<Bom> Boms { get; set; }
        public virtual DbSet<ListOrderSetup> ListOrderSetups { get; set; }
        public virtual DbSet<ItemsListDisplayOrderSetup> ItemsListDisplayOrderSetup { get; set; }
        public virtual DbSet<ItemViewSetupForPrint> ItemViewSetupForPrint { get; set; }
        public virtual DbSet<ItemViewSetup> ItemViewSetups { get; set; }
        public virtual DbSet<BranchVoucher> BranchVouchers { get; set; }
        public virtual DbSet<SyncPushPullRecord> SyncPushPullRecords { get; set; }
        public virtual DbSet<TransactionApplicationLog> TransactionApplicationLogs { get; set; }
        public virtual DbSet<StockReceived> StockReceived { get; set; }
        public virtual DbSet<StockReceivedItems> StockReceivedItems { get; set; }
        public virtual DbSet<StockTransferItems> StockTransferItems { get; set; }
        public virtual DbSet<StockTransfer> StockTransfers { get; set; }
        public virtual DbSet<StockRequestItems> StockRequestItems { get; set; }
        public virtual DbSet<StockRequest> StockRequests { get; set; }
        public virtual DbSet<BranchItems> BranchItems { get; set; }
        public virtual DbSet<BundleOfferBranches> BundleOfferBranches { get; set; }
        public virtual DbSet<BranchSetup> BranchSetups { get; set; }
        public virtual DbSet<BranchUser> BranchUsers { get; set; }
        public virtual DbSet<Branch> Branches { get; set; }
        public virtual DbSet<PaymentRefund> PaymentRefunds { get; set; }
        public virtual DbSet<PaymentVoucherItem> PaymentVoucherItems { get; set; }
        public virtual DbSet<PromotionOfferItem> PromotionOfferItems { get; set; }
        public virtual DbSet<ProductGroup> ProductGroups { get; set; }
        public virtual DbSet<DeliveryHoliday> DeliveryHolidays { get; set; }
        public virtual DbSet<DeliveryAddress> DeliveryAddresses { get; set; }
        public virtual DbSet<CreditNoteItem> CreditNoteItems { get; set; }
        public virtual DbSet<CreditNote> CreditNotes { get; set; }
        public virtual DbSet<ShiftRunEmployee> ShiftRunEmployees { get; set; }
        public virtual DbSet<ShiftRun> ShiftRuns { get; set; }
        public virtual DbSet<LedgerTransaction> LedgerTransactions { get; set; }
        public virtual DbSet<LedgerAccount> LedgerAccounts { get; set; }
        public virtual DbSet<Prefixies> Prefixies { get; set; }
        public virtual DbSet<DeliveryChallanReserved> DeliveryChallanReserveds { get; set; }
        public virtual DbSet<DeliveryChallanReserverdItem> DeliveryChallanReserverdItems { get; set; }
        public virtual DbSet<DeliveryChallan> DeliveryChallans { get; set; }
        public virtual DbSet<DeliveryChallanItem> DeliveryChallanItems { get; set; }
        public virtual DbSet<ContactPerson> ContactPersons { get; set; }
        public virtual DbSet<QuotationTemplate> QuotationTemplates { get; set; }
        public virtual DbSet<QuotationTemplateItem> QuotationTemplateItems { get; set; }
        public virtual DbSet<Holiday> Holidays { get; set; }
        public virtual DbSet<WeekHoliday> WeekHolidays { get; set; }
        public virtual DbSet<GuestedHoliday> GuestedHolidays { get; set; }
        public virtual DbSet<ManualAttendance> ManualAttendances { get; set; }
        public virtual DbSet<VariationInventoryForReporting> VariationInventoryForReportings { get; set; }
        public virtual DbSet<VariationInventory> VariationInventories { get; set; }
        public virtual DbSet<SaleSizeAssortment> SaleSizeAssortments { get; set; }
        public virtual DbSet<ProductSize> ProductSizes { get; set; }
        public virtual DbSet<RecipeAssortment> RecipeAssortments { get; set; }
        public virtual DbSet<MultiUp> MultiUps { get; set; }
        public virtual DbSet<MultiUPSLineItem> MultiUPSLineItems { get; set; }
        public virtual DbSet<ReparingItem> ReparingItems { get; set; }
        public virtual DbSet<ReparingOrder> ReparingOrders { get; set; }
        public virtual DbSet<ReparingOrderType> ReparingOrderTypes { get; set; }
        public virtual DbSet<UserLog> UserLogs { get; set; }
        public virtual DbSet<GoodReceiveNote> GoodReceives { get; set; }
        public virtual DbSet<GoodReceiveNoteItem> GoodReceiveNoteItems { get; set; }
        public virtual DbSet<UserDefineFlow> UserDefineFlows { get; set; }
        public virtual DbSet<SaleOrderVersion> SaleOrderVersions { get; set; }
        public virtual DbSet<ImportExportItem> ImportExportItems { get; set; }
        public virtual DbSet<ImportExportType> ImportExportTypes { get; set; }
        public virtual DbSet<IssuedTo> Issueds { get; set; }
        public virtual DbSet<ChequeBookItem> ChequeBookItems { get; set; }
        public virtual DbSet<ChequeBook> ChequeBooks { get; set; }
        public virtual DbSet<TemporaryCashIssueExpense> TemporaryCashIssueExpenses { get; set; }
        public virtual DbSet<TemporaryCashReturn> TemporaryCashReturns { get; set; }
        public virtual DbSet<TemporaryCashIssueItem> TemporaryCashIssueItems { get; set; }
        public virtual DbSet<TemporaryCashIssue> TemporaryCashIssues { get; set; }
        public virtual DbSet<CashRequestUser> CashRequestUsers { get; set; }
        public virtual DbSet<TemporaryCashAllocation> TemporaryCashAllocations { get; set; }
        public virtual DbSet<TemporaryCashRequestItem> TemporaryCashRequestItems { get; set; }
        public virtual DbSet<TemporaryCashRequest> TemporaryCashRequests { get; set; }
        public virtual DbSet<RunPayrollSalaryDetail> RunPayrollSalaryDetails { get; set; }
        public virtual DbSet<RunPayrollDetail> RunPayrollDetails { get; set; }
        public virtual DbSet<RunPayroll> RunPayrolls { get; set; }
        public virtual DbSet<LoanPay> LoanPays { get; set; }
        public virtual DbSet<LoanPayment> LoanPayments { get; set; }
        public virtual DbSet<EmployeeSalaryDetail> EmployeeSalaryDetails { get; set; }
        public virtual DbSet<EmployeeSalary> EmployeeSalaries { get; set; }
        public virtual DbSet<SalaryTaxSlab> SalaryTaxSlabs { get; set; }
        public virtual DbSet<TaxSalary> TaxSalaries { get; set; }
        public virtual DbSet<PaySchedule> PaySchedules { get; set; }
        public virtual DbSet<SalaryContribution> SalaryContributions { get; set; }
        public virtual DbSet<SalaryAllowance> SalaryAllowances { get; set; }
        public virtual DbSet<SalaryDeduction> SalaryDeductions { get; set; }
        public virtual DbSet<SalaryTemplate> SalaryTemplates { get; set; }
        public virtual DbSet<Deduction> Deductions { get; set; }
        public virtual DbSet<Contribution> Contributions { get; set; }
        public virtual DbSet<Allowance> Allowances { get; set; }
        public virtual DbSet<AllowanceType> AllowanceTypes { get; set; }
        public virtual DbSet<GatePass> GatePasses { get; set; }
        public virtual DbSet<GatePassItem> GatePassItems { get; set; }
        public virtual DbSet<ContractorItem> ContractorItems { get; set; }
        public virtual DbSet<ContractorPayment> ContractorPayments { get; set; }
        public virtual DbSet<BatchCosting> BatchCostings { get; set; }
        public virtual DbSet<BatchCostingItem> BatchCostingItems { get; set; }
        public virtual DbSet<SampleRequestItem> SampleRequestItems { get; set; }
        public virtual DbSet<SampleRequest> SampleRequests { get; set; }
        public virtual DbSet<BatchProcess> BatchProcesses { get; set; }
        public virtual DbSet<BatchProcessContractor> BatchProcessContractors { get; set; }
        public virtual DbSet<Process> Processes { get; set; }
        public virtual DbSet<ProcessItem> ProcessItems { get; set; }
        public virtual DbSet<Contractor> Contractors { get; set; }
        public virtual DbSet<ProcessContractor> ProcessContractors { get; set; }
        public virtual DbSet<PrintOption> PrintOptions { get; set; }
        public virtual DbSet<PriceRecord> PriceRecords { get; set; }
        public virtual DbSet<PriceLabeling> PriceLabelings { get; set; }
        public virtual DbSet<DailyExpenseAttachment> DailyExpenseAttachments { get; set; }
        public virtual DbSet<Logistic> Logistics { get; set; }
        public virtual DbSet<CompanyOption> CompanyOptions { get; set; }
        public virtual DbSet<SaleInvoiceDiscount> SaleInvoiceDiscounts { get; set; }
        public virtual DbSet<Discount> Discounts { get; set; }
        public virtual DbSet<PurchaseBill> PurchaseBills { get; set; }
        public virtual DbSet<PurchaseBillItem> PurchaseBillItems { get; set; }
        public virtual DbSet<BillAttachment> BillAttachments { get; set; }
        public virtual DbSet<SaleInvoiceAdvancePayment> SaleInvoiceAdvancePayments { get; set; }
        public virtual DbSet<PaymentVoucherAttachment> PaymentVoucherAttachments { get; set; }
        public virtual DbSet<ProductMaster> ProductMasters { get; set; }
        public virtual DbSet<BankPosTerminal> BankPosTerminals { get; set; }
        public virtual DbSet<PrintSetting> PrintSettings { get; set; }
        public virtual DbSet<TransferHistory> TransferHistories { get; set; }
        public virtual DbSet<InventoryBlind> InventoryBlinds { get; set; }
        public virtual DbSet<InventoryBlindDetail> InventoryBlindDetails { get; set; }
        public virtual DbSet<LoginHistory> LoginHistories { get; set; }
        public virtual DbSet<ProductionStockTransfer> ProductionStockTransfers { get; set; }
        public virtual DbSet<ProductionBatch> ProductionBatchs { get; set; }
        public virtual DbSet<ProductionBatchItem> ProductionBatchItems { get; set; }
        public virtual DbSet<Region> Regions { get; set; }
        public virtual DbSet<RecipeNo> RecipeNos { get; set; }
        public virtual DbSet<RecipeItem> RecipeItems { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<ModulesRights> ModulesRights { get; set; }
        public virtual DbSet<ModulesName> ModulesNames { get; set; }
        public virtual DbSet<RolesPermissions> RolesPermissions { get; set; }
        public virtual DbSet<NobleUserRoles> NobleUserRoles { get; set; }
        public virtual DbSet<NobleRoles> NobleRoles { get; set; }
        public virtual DbSet<CompanyAccountSetup> CompanyAccountSetups { get; set; }
        public virtual DbSet<MobileOrder> MobileOrders { get; set; }
        public virtual DbSet<MobileOrderItem> MobileOrderItems { get; set; }
        public virtual DbSet<Bank> Banks { get; set; }
        public virtual DbSet<StockAdjustment> StockAdjustments { get; set; }
        public virtual DbSet<StockAdjustmentDetail> StockAdjustmentDetails { get; set; }
        public virtual DbSet<DailyExpense> DailyExpenses { get; set; }
        public virtual DbSet<DailyExpenseDetail> DailyExpenseDetails { get; set; }
        public virtual DbSet<PaymentVoucher> PaymentVouchers { get; set; }
        public virtual DbSet<PaymentVoucherDetail> PaymentVoucherDetails { get; set; }
        public virtual DbSet<MonthlyCost> MonthlyCosts { get; set; }
        public virtual DbSet<MonthlyCostItem> MonthlyCostItems { get; set; }
        public virtual DbSet<LoginPermissions> LoginPermissions { get; set; }
        public virtual DbSet<JournalVoucher> JournalVouchers { get; set; }
        public virtual DbSet<JournalVoucherItem> JournalVoucherItems { get; set; }
        public virtual DbSet<EmployeeRegistration> EmployeeRegistrations { get; set; }
        public virtual DbSet<DayStart> DayStarts { get; set; }
        public virtual DbSet<BundleCategory> BundleCategories { get; set; }
        public virtual DbSet<PaymentOption> PaymentOptions { get; set; }
        public virtual DbSet<Currency> Currencies { get; set; }
        public virtual DbSet<PromotionOffer> PromotionOffers { get; set; }
        public virtual DbSet<TaxRate> TaxRates { get; set; }
        public virtual DbSet<Terminal> Terminals { get; set; }
        public virtual DbSet<Origin> Origins { get; set; }
        public virtual DbSet<Color> Colors { get; set; }
        public virtual DbSet<SubCategory> SubCategories { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<Unit> Units { get; set; }
        public virtual DbSet<Size> Sizes { get; set; }
        public virtual DbSet<Warehouse> Warehouses { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<ContactBankAccount> ContactBankAccounts { get; set; }
        public virtual DbSet<ContactAttachment> ContactAttachments { get; set; }
        public virtual DbSet<Zone> Zones { get; set; }
        public DbSet<CompanyInformation> companyInformations { get; set; }

        public virtual DbSet<CustomerDiscount> CustomerDiscount { get; set; }

        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<EmployeeDepartment> EmployeeDepartments { get; set; }
        public virtual DbSet<Designation> Designations { get; set; }
        public virtual DbSet<Transporter> Transporters { get; set; }
        public virtual DbSet<Attachment> Attachments { get; set; }
        public virtual DbSet<EmployeeAttachment> EmployeeAttachments { get; set; }
        public virtual DbSet<AccountTemplate> AccountTemplates { get; set; }
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<CostCenter> CostCenters { get; set; }
        public virtual DbSet<AccountType> AccountTypes { get; set; }
        public virtual DbSet<AccountsLevelOne> AccountsLevelOne { get; set; }
        public virtual DbSet<AccountsLevelTwo> AccountsLevelTwo { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Purchase> Purchases { get; set; }
        public virtual DbSet<PurchaseItem> PurchaseItems { get; set; }
        public virtual DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public virtual DbSet<PurchaseOrderItem> PurchaseOrderItems { get; set; }
        public virtual DbSet<CompanyLicence> CompanyLicences { get; set; }

        public virtual DbSet<PurchasePost> PurchasePosts { get; set; }
        public virtual DbSet<PurchasePostItem> PurchasePostItems { get; set; }
        public virtual DbSet<Inventory> Inventories { get; set; }
        public virtual DbSet<Stock> Stocks { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<UserTerminal> UserTerminals { get; set; }
        public virtual DbSet<Sale> Sales { get; set; }
        public virtual DbSet<SaleItem> SaleItems { get; set; }
        public virtual DbSet<SalePayment> SalePayments { get; set; }
        public virtual DbSet<CashCustomer> CashCustomer { get; set; }


        public virtual DbSet<WareHouseTransfer> WareHouseTransfers { get; set; }
        public virtual DbSet<WareHouseTransferItem> WareHouseTransferItems { get; set; }
        public virtual DbSet<NobleGroup> NobleGroups { get; set; }
        public virtual DbSet<NobleModule> NobleModules { get; set; }
        public virtual DbSet<Focus.Domain.Entities.NoblePermission> NoblePermissions { get; set; }
        public virtual DbSet<CompanyPermission> CompanyPermissions { get; set; }
        public virtual DbSet<BranchWisePermission> BranchWisePermissions { get; set; }
        public virtual DbSet<NobleRolePermission> NobleRolePermissions { get; set; }
        public virtual DbSet<CompanyAttachment> CompanyAttachments { get; set; }
        public virtual DbSet<EmployeeRegistrationAttachment> EmployeeRegistrationAttachments { get; set; }
        public virtual DbSet<OtherCurrency> OtherCurrencies { get; set; }
        public virtual DbSet<PurchaseAttachment> PurchaseAttachments { get; set; }
        public virtual DbSet<SaleOrder> SaleOrders { get; set; }
        public virtual DbSet<SaleOrderItem> SaleOrderItems { get; set; }
        public virtual DbSet<DispatchNote> DispatchNotes { get; set; }
        public virtual DbSet<DispatchNoteItem> DispatchNoteItems { get; set; }
        public virtual DbSet<DenominationSetup> DenominationSetups { get; set; }
        public virtual DbSet<SyncRecord> SyncRecords { get; set; }
        public virtual DbSet<CashVoucher> CashVouchers { get; set; }
        public virtual DbSet<CompanyProcess> CompanyProcess { get; set; }
        public virtual DbSet<PurchaseOrderAction> PurchaseOrderActions { get; set; }
        public virtual DbSet<PurchaseInvoiceAction> PurchaseInvoiceActions { get; set; }
        public virtual DbSet<PurchaseInvoiceAttachment> PurchaseInvoiceAttachments { get; set; }
        public virtual DbSet<PurchaseOrderPayment> PurchaseOrderPayments { get; set; }
        public virtual DbSet<ExpenseType> ExpenseTypes { get; set; }
        public virtual DbSet<SaleOrderPayment> SaleOrderPayments { get; set; }
        public virtual DbSet<ApplicationUpdate> ApplicationUpdates { get; set; }
        public virtual DbSet<YearlyPeriod> YearlyPeriods { get; set; }
        public virtual DbSet<CompanySubmissionPeriod> CompanySubmissionPeriods { get; set; }
        public virtual DbSet<PurchaseOrderExpense> PurchaseOrderExpenses { get; set; }
        public virtual DbSet<PurchaseOrderVersion> PurchaseOrderVersions { get; set; }
        public virtual DbSet<PurchasePostExpense> PurchasePostExpenses { get; set; }
        public virtual DbSet<TerminalCategory> TerminalCategories { get; set; }
        public virtual DbSet<AutoPurchaseSetting> AutoPurchaseSettings { get; set; }
        public virtual DbSet<AutoPurchaseTemplate> AutoPurchaseTemplates { get; set; }
        public virtual DbSet<AutoPurchaseTemplateItem> AutoPurchaseTemplateItems { get; set; }
        public virtual DbSet<WarrantyType> WarrantyTypes { get; set; }
        public virtual DbSet<PaymentLimit> PaymentLimits { get; set; }
        public virtual DbSet<MediaType> MediaTypes { get; set; }
        public virtual DbSet<Inquiry> Inquiries { get; set; }
        public virtual DbSet<InquiryItem> InquiryItems { get; set; }
        public virtual DbSet<InquiryProcess> InquiryProcesses { get; set; }
        public virtual DbSet<InquiryModule> InquiryModules { get; set; }
        public virtual DbSet<ModuleQuestion> ModuleQuestions { get; set; }
        public virtual DbSet<InquiryModuleQuestion> InquiryModuleQuestions { get; set; }
        public virtual DbSet<InquiryType> InquiryTypes { get; set; }
        public virtual DbSet<InquiryComment> InquiryComments { get; set; }
        public virtual DbSet<InquiryMeeting> InquiryMeetings { get; set; }
        public virtual DbSet<InquiryMeetingAttendant> InquiryMeetingAttendants { get; set; }
        public virtual DbSet<InquiryEmail> InquiryEmails { get; set; }
        public virtual DbSet<InquiryEmailCc> InquiryEmailCcs { get; set; }
        public virtual DbSet<InquiryStatus> InquiryStatuses { get; set; }
        public virtual DbSet<WhiteLabeling> WhiteLabelings { get; set; }
        public virtual DbSet<InquiryPriority> InquiryPriorities { get; set; }
        public virtual DbSet<InquiryStatusDynamic> InquiryStatusDynamics { get; set; }
        public virtual DbSet<DiscountSetup> DiscountSetups { get; set; }
        public virtual DbSet<DefaultSetting> DefaultSettings { get; set; }
        public virtual DbSet<GsmSmsSetup> GsmSmsSetups { get; set; }
        public virtual DbSet<CompanyHolidays> CompanyHolidays { get; set; }
        public virtual DbSet<ShiftAssign> ShiftAssigns { get; set; }
        public virtual DbSet<ShiftEmployeeAssign> ShiftEmployeeAssigns { get; set; }

        public virtual DbSet<LeaveTypes> LeaveTypes { get; set; }
        public virtual DbSet<LeavePeriod> LeavePeriods { get; set; }
        public virtual DbSet<WorkWeek> WorkWeeks { get; set; }
        public virtual DbSet<LeaveHoliday> LeaveHolidays { get; set; }
        public virtual DbSet<LeaveRules> LeaveRules { get; set; }
        public virtual DbSet<PaidTimeOff> PaidTimeOffs { get; set; }
        public virtual DbSet<LeaveGroup> LeaveGroups { get; set; }
        public virtual DbSet<LeaveGroupEmployee> LeaveGroupEmployees { get; set; }
        public virtual DbSet<ApprovalSystem> ApprovalSystems { get; set; }
        public virtual DbSet<ApprovalSystemEmployees> ApprovalSystemEmployees { get; set; }
        public virtual DbSet<HoldSetup> HoldSetups { get; set; }
        public virtual DbSet<CustomerGroup> CustomerGroups { get; set; }

        public virtual DbSet<InvoiceDefault> InvoiceDefaults { get; set; }
        public virtual DbSet<PermanentDeleteHoldSetup> PermanentDeleteHoldSetups { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ShadowProperties();
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
            SetGlobalQueryFilters(modelBuilder);
            base.OnModelCreating(modelBuilder);
            modelBuilder.Seed();
        }
        //public string LogTrackedEntities()
        //{



        //}
        public override int SaveChanges()
        {
            ChangeTracker.SetShadowProperties(_httpContextProvider);
            return base.SaveChanges();
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            ChangeTracker.SetShadowProperties(_httpContextProvider);
            return await base.SaveChangesAsync(cancellationToken);

        }
        public List<SyncRecordModel> SyncLog()
        {
            var a = ChangeTracker.LogTrackedEntities(_logger, _httpContextProvider);
            return a;

        }



        public virtual void SetModified(object entity, string attribute, Guid value)
        {
            Entry(entity).Property(attribute).CurrentValue = value;
        }
        public virtual int SaveChangesAfter()
        {
            return base.SaveChanges();
        }
        private void SetGlobalQueryFilters(ModelBuilder modelBuilder)
        {
            foreach (var tp in modelBuilder.Model.GetEntityTypes())
            {
                var t = tp.ClrType;
                // set global filters
                if (typeof(ISoftDelete).IsAssignableFrom(t))
                {
                    if (typeof(ITenantFilterableEntity).IsAssignableFrom(t))
                    {
                        // softdeletable and tenant (note do not filter just ITenant - too much filtering!
                        // just top level classes that have ITenantFilterableEntity
                        var method = SetGlobalQueryForSoftDeleteAndTenantMethodInfo.MakeGenericMethod(t);
                        method.Invoke(this, new object[] { modelBuilder });
                    }
                    else
                    {
                        // softdeletable
                        var method = SetGlobalQueryForSoftDeleteMethodInfo.MakeGenericMethod(t);
                        method.Invoke(this, new object[] { modelBuilder });
                    }
                }
                else if (typeof(ITenantFilterableEntity).IsAssignableFrom(t))
                {
                    // just to filter any entities with ITenantFilterableEntity
                    var method = SetGlobalQueryForTenantMethodInfo.MakeGenericMethod(t);
                    method.Invoke(this, new object[] { modelBuilder });
                }
            }
        }
        private static readonly MethodInfo SetGlobalQueryForSoftDeleteMethodInfo = typeof(ApplicationDbContext).GetMethods(BindingFlags.Public | BindingFlags.Instance)
            .Single(t => t.IsGenericMethod && t.Name == "SetGlobalQueryForSoftDelete");
        private static readonly MethodInfo SetGlobalQueryForSoftDeleteAndTenantMethodInfo = typeof(ApplicationDbContext).GetMethods(BindingFlags.Public | BindingFlags.Instance)
            .Single(t => t.IsGenericMethod && t.Name == "SetGlobalQueryForSoftDeleteAndTenant");
        private static readonly MethodInfo SetGlobalQueryForTenantMethodInfo = typeof(ApplicationDbContext).GetMethods(BindingFlags.Public | BindingFlags.Instance)
            .Single(t => t.IsGenericMethod && t.Name == "SetGlobalQueryForTenant");
        public void SetGlobalQueryForSoftDelete<T>(ModelBuilder builder) where T : class, ISoftDelete
        {
            builder.Entity<T>().HasQueryFilter(item => !EF.Property<bool>(item, "IsDeleted"));
        }
        public void SetGlobalQueryForSoftDeleteAndTenant<T>(ModelBuilder builder) where T : class, ISoftDelete, ITenant
        {
            builder.Entity<T>().HasQueryFilter(
                item => !EF.Property<bool>(item, "IsDeleted") &&
                        (DisableTenantFilter || EF.Property<Guid>(item, "CompanyId") == _httpContextProvider.GetCompanyId()));
        }
        public void SetGlobalQueryForTenant<T>(ModelBuilder builder) where T : class, ITenant
        {
            builder.Entity<T>().HasQueryFilter(
                item => (DisableTenantFilter || EF.Property<Guid>(item, "CompanyId") == _httpContextProvider.GetCompanyId()));
        }
        public bool DisableTenantFilter { get; set; }

    }
}
