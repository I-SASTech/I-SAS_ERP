using System;
using System.Collections.Generic;
using Focus.Domain.Interface;

namespace Focus.Domain.Entities
{
    public class Account : BaseEntity, IAuditedEntityBase, ITenantFilterableEntity, ITenant
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string NameArabic { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public Guid? CostCenterId { get; set; }
        public virtual CostCenter CostCenter { get; set; }
        public virtual ICollection<Category> PurchaseAccounts { get; set; }
        public virtual ICollection<Category> COGSAccounts { get; set; }
        public virtual ICollection<Category> InventoryAccounts { get; set; }
        public virtual ICollection<Category> IncomeAccounts { get; set; }
        public virtual ICollection<Category> SaleAccounts { get; set; }
        public virtual ICollection<Contact> Contacts { get; set; }
        public virtual ICollection<Contact> ContactAdvanceAccount { get; set; }
        public virtual ICollection<Contact> ContactCashAccount { get; set; }
        public virtual ICollection<EmployeeRegistration> EmployeePayableAccounts { get; set; }
        public virtual ICollection<EmployeeRegistration> EmployeeAccounts { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
        public virtual ICollection<JournalVoucherItem> JournalVoucherItems { get; set; }
        public virtual ICollection<Bank> Banks { get; set; }
        public virtual ICollection<Terminal> Terminals { get; set; }
        public virtual ICollection<BankPosTerminal> BankPosTerminals { get; set; }
        public virtual ICollection<Terminal> CashAccountTerminals { get; set; }
        public virtual ICollection<PrintSetting> CashAccountPrintSetting { get; set; }
        public virtual ICollection<PrintSetting> BankAccountPrintSetting { get; set; }
        public virtual ICollection<PaymentVoucher> PaymentVoucherForContacts { get; set; }
        public virtual ICollection<PaymentVoucher> PaymentVoucherForBanks { get; set; }
        public virtual ICollection<PaymentVoucher> PaymentVoucherForVat { get; set; }

        public virtual ICollection<PurchaseOrderExpense> PurchaseOrderExpenseForContacts { get; set; }
        public virtual ICollection<PurchaseOrderExpense> PurchaseOrderExpenseForBanks { get; set; }
        public virtual ICollection<PurchaseOrderExpense> PurchaseOrderExpenseForVat { get; set; }
        public virtual ICollection<Logistic> Logistics { get; set; }
        public virtual ICollection<DailyExpense> DailyExpenses { get; set; }
        public virtual ICollection<DailyExpenseDetail> DailyExpenseDetails { get; set; }
        public virtual ICollection<PurchaseBillItem> PurchaseBillItems { get; set; }
        public virtual ICollection<PurchaseBill> PurchaseBills { get; set; }
        public virtual ICollection<TemporaryCashAllocation> TemporaryCashAllocations { get; set; }
        public virtual ICollection<LedgerAccount> LeadgerAccounts { get; set; }
        public virtual ICollection<ExpenseType> ExpenseTypes { get; set; }
        public virtual ICollection<PaymentRefund> PaymentRefundBankCash { get; set; }
        public virtual ICollection<PaymentRefund> PaymentRefundContact { get; set; }
        public virtual ICollection<BranchVoucher> BranchVoucherBankCash { get; set; }
        public virtual ICollection<BranchVoucher> BranchVoucherContact { get; set; }
        public virtual ICollection<FinancialYearClosingBalance> FinancialYearClosingBalances { get; set; }

    }
}
