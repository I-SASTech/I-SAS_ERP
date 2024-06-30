﻿
namespace Focus.Domain.Enum
{
    public enum TransactionType
    {
        PurchasePost,
        CashReceipt,
        BankReceipt,
        CashPay,
        BankPay,
        StockIn,
        StockOut,
        JournalVoucher,
        ExpenseVoucher,
        SaleInvoice,
        PurchaseReturn,
        SaleReturn,
        DayEnd,
        WareHouseTransferFrom,
        ProductionBatch,
        StockProduction,
        ProductionRemainingStock,
        ProductionDamageStock,
        PettyCash,
        WareHouseTransferTo,
        AdvanceReceipt,
        AdvancePay,
        Expense,
        AdvanceExpense,
        AdvanceCashReceipt,
        AdvanceBankReceipt,
        AdvanceCashPay,
        AdvanceBankPay,
        Payroll,
        Reserved,
        NotReserved,
        TemporaryCashAllocation,
        TemporaryCashIssue,
        TemporaryCashReturn,
        PaymentRefund,
        StockTransferToBranch,
    }
}
