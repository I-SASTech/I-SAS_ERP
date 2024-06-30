import { createApp, defineAsyncComponent } from 'vue'
import { useMainStore } from '@/store/index'
import App from './App.vue'
import router from './router'
import i18n from './i18n'
import { createPinia } from "pinia";

import VueSweetalert2 from 'vue-sweetalert2';
import 'sweetalert2/dist/sweetalert2.min.css';

const app = createApp(App);
import BootstrapVueNext from 'bootstrap-vue-next'
app.use(BootstrapVueNext)
app.use(VueSweetalert2);
import piniaPluginPersistedState from "pinia-plugin-persistedstate"
const pinia = createPinia();
pinia.use(piniaPluginPersistedState)
app.use(pinia);


import moment from 'moment'
import axios from 'axios'

import ElementPlus from 'element-plus'
import en from 'element-plus/dist/locale/en.mjs'
import ar from 'element-plus/dist/locale/ar.mjs'
import pt from 'element-plus/dist/locale/pt.mjs'


app.use(ElementPlus, { locale: en });
import clickMixin from '@/Mixins/clickMixin';
import json from '../public/Config.json';


app.use(clickMixin);
app.use(moment);

if (i18n.locale === 'en') {
    app.use(ElementPlus, { locale: en });
}
else if (i18n.locale === 'pt') {
    app.use(ElementPlus, { locale: pt });
}
else {
    app.use(ElementPlus, { locale: ar });
}


import VueApexCharts from "vue3-apexcharts";
app.use(VueApexCharts);
app.component('apexchart', VueApexCharts);

import Loading from 'vue-loading-overlay';
import 'vue-loading-overlay/dist/css/index.css';
/*import store from './store'*/
app.use(Loading);

app.component('yeardropdown', defineAsyncComponent(() => import('./components/General/YearDropdown.vue')));
app.component('grouping', defineAsyncComponent(() => import('./components/General/Grouping.vue')));
app.component('email-compose', defineAsyncComponent(() => import('./components/General/EmailCompose.vue')));
app.component('email-compose-received', defineAsyncComponent(() => import('./components/General/EmailComposeReceived.vue')));
app.component('category-multidropdown', defineAsyncComponent(() => import('./components/General/CategoryMultiDropdown.vue')));
app.component('departmentmodel', defineAsyncComponent(() => import('./components/Hr/Employee/Department/AddDepartment.vue')));
app.component('categorymodal', defineAsyncComponent(() => import('./components/Category/AddCategory.vue')));
app.component('subcategorymodal', defineAsyncComponent(() => import('./components/SubCategory/AddSubCategory.vue')));
app.component('unitmodal', defineAsyncComponent(() => import('./components/Unit/AddUnit.vue')));
app.component('sizemodal', defineAsyncComponent(() => import('./components/Size/AddSize.vue')));
app.component('taxratemodal', defineAsyncComponent(() => import('./components/TaxRate/AddTaxRate.vue')));
app.component('companyprocessdropdown', defineAsyncComponent(() => import('./components/General/CompanyProcessDropdown.vue')));
app.component('subcategorydropdown', defineAsyncComponent(() => import('./components/General/SubCategoryDropdown.vue')));
app.component('branddropdown', defineAsyncComponent(() => import('./components/General/BrandDropdown.vue')));
app.component('origindropdown', defineAsyncComponent(() => import('./components/General/OriginDropdown.vue')));
app.component('unitdropdown', defineAsyncComponent(() => import('./components/General/UnitDropdown.vue')));
app.component('unitleveldropdown', defineAsyncComponent(() => import('./components/General/UnitLevelDropdown.vue')));
app.component('sizedropdown', defineAsyncComponent(() => import('./components/General/SizeDropdown.vue')));
app.component('size-multiselect-dropdown', defineAsyncComponent(() => import('./components/General/SizeMultiSelectDropdown.vue')));
app.component('colordropdown', defineAsyncComponent(() => import('./components/General/ColorDropdown.vue')));
app.component('color-multiselect-dropdown', defineAsyncComponent(() => import('./components/General/Color-Multiselect-Dropdown.vue')));
app.component('warranty-type-dropdown', defineAsyncComponent(() => import('./components/General/WarrantyTypeDropdown.vue')));
app.component('salutation-dropdown', defineAsyncComponent(() => import('./components/General/SalutationDropdown.vue')));
app.component('display-name-dropdown', defineAsyncComponent(() => import('./components/General/DisplayNameDropdown.vue')));
app.component('proforma-invoice-dropdown', defineAsyncComponent(() => import('./components/General/ProformaInvoiceDropdown.vue')));
app.component('deliverychallandropdown', defineAsyncComponent(() => import('./components/General/DeliveryChallanDropdown.vue')));




//Leave

// HR Management components
app.component('addleavetypes', defineAsyncComponent(() => import('./components/Hr/LeaveManagement/LeaveTypes/AddLeaveTypes.vue')));
app.component('addleaveperiod', defineAsyncComponent(() => import('./components/Hr/LeaveManagement/LeavePeriod/AddLeavePeriod.vue')));
app.component('addworkweek', defineAsyncComponent(() => import('./components/Hr/LeaveManagement/WorkWeek/AddWorkWeek.vue')));
app.component('addholiday', defineAsyncComponent(() => import('./components/Hr/LeaveManagement/Holiday/AddHoliday.vue')));
app.component('addleaverules', defineAsyncComponent(() => import('./components/Hr/LeaveManagement/LeaveRules/AddLeaveRules.vue')));
app.component('addleavegroups', defineAsyncComponent(() => import('./components/Hr/LeaveManagement/LeaveGroups/AddLeaveGroups.vue')));
app.component('addpaidtimeoff', defineAsyncComponent(() => import('./components/Hr/LeaveManagement/PaidTimeOff/AddPaidTimeOff.vue')));

// General dropdown components
app.component('leavegroupdropdown', defineAsyncComponent(() => import('./components/General/LeaveGroupDropdown.vue')));
app.component('leavetypedropdown', defineAsyncComponent(() => import('./components/General/LeaveTypeDropdown.vue')));
app.component('leaveperioddropdown', defineAsyncComponent(() => import('./components/General/LeavePeriodDropdown.vue')));
app.component('taxratedropdown', defineAsyncComponent(() => import('./components/General/TaxRateDropdown.vue')));
app.component('logisticDropdown', defineAsyncComponent(() => import('./components/General/LogisticDropdown.vue')));

// Add model components
app.component('brandmodel', defineAsyncComponent(() => import('./components/Brand/AddBrand.vue')));
app.component('originmodel', defineAsyncComponent(() => import('./components/Origin/AddOrigin.vue')));
app.component('unitmodel', defineAsyncComponent(() => import('./components/Unit/AddUnit.vue')));
app.component('addRoles', defineAsyncComponent(() => import('./components/UserRoles/AddRoles.vue')));
app.component('addPermissions', defineAsyncComponent(() => import('./components/UserPermissions/AddPermissions.vue')));
app.component('sizemodel', defineAsyncComponent(() => import('./components/Size/AddSize.vue')));
app.component('colormodel', defineAsyncComponent(() => import('./components/Color/AddColor.vue')));
app.component('addholidaysetup', defineAsyncComponent(() => import('./components/Hr/ShiftManagement/AddHolidaySetup.vue')));
app.component('taxratemodel', defineAsyncComponent(() => import('./components/TaxRate/AddTaxRate.vue')));
app.component('currencymodel', defineAsyncComponent(() => import('./components/Currency/AddCurrency.vue')));

// Other components
app.component('currency-dropdown', defineAsyncComponent(() => import('./components/General/CurrencyDropdown.vue')));
app.component('purchaseorder-payment', defineAsyncComponent(() => import('./components/PurchaseOrder/PurchaseOrderPayment.vue')));
app.component('purchaseorder-expense', defineAsyncComponent(() => import('./components/PurchaseOrder/PurchaseOrderExpense.vue')));
app.component('paymentmodel', defineAsyncComponent(() => import('./components/Payment_Options/AddPaymentOptions.vue')));
app.component('PaymentVoucherItem', defineAsyncComponent(() => import('./components/PaymentVouchers/PaymentVoucherItem.vue')));
app.component('productfiltermodel', defineAsyncComponent(() => import('./components/Product/ProductFilterModel.vue')));
app.component('AddAddress', defineAsyncComponent(() => import('./components/Customer/AddAddress.vue')));
app.component('invoicedetailsprint', defineAsyncComponent(() => import('./components/SaleServiceInvoice/InvoiceDetailsPrint.vue')));
app.component('supervisor-login-model', defineAsyncComponent(() => import('./components/DayStart/SupervisorLogin.vue')));
app.component('cash-receiving-model', defineAsyncComponent(() => import('./components/DayStart/CashReceiving.vue')));
app.component('cashcounterdetail', defineAsyncComponent(() => import('./components/TouchScreen/CashCounterDetail.vue')));
app.component('walk-customer-modal', defineAsyncComponent(() => import('./components/TouchScreen/WalkCustomerModel.vue')));
app.component('hold-modal', defineAsyncComponent(() => import('./components/TouchScreen/HoldModel.vue')));
app.component('unhold-modal', defineAsyncComponent(() => import('./components/TouchScreen/UnHoldModel.vue')));
app.component('return-item-model', defineAsyncComponent(() => import('./components/TouchScreen/ReturnItemModel.vue')));
app.component('denominationSetupmodel', defineAsyncComponent(() => import('./components/DenominationSetup/AddDenominationSetup.vue')));
app.component('customerdropdown', defineAsyncComponent(() => import(/* webpackChunkName: "customer-dropdown" */ './components/General/CustomerDropdown.vue')));
app.component('customersupplieraccountsdropdown', defineAsyncComponent(() => import('./components/General/CustomerSupplierAccountsDropdown.vue')));
// Reports components
app.component('dayEndReportPrint', defineAsyncComponent(() => import('./components/Reports/DayEndReportPrint.vue')));
app.component('SendEmail', defineAsyncComponent(() => import('./components/Email/SendEmail/SendEmail.vue')));
app.component('dayEndA4ReportPrint', defineAsyncComponent(() => import('./components/Reports/DayEndA4ReportPrint.vue')));
app.component('wholeSaleDayEndReport', defineAsyncComponent(() => import('./components/Reports/WholeSaleDayEndReport.vue')));
app.component('billsReport', defineAsyncComponent(() => import('./components/Reports/BillsReport.vue')));
app.component('BankTransactionReportPrint', defineAsyncComponent(() => import('./components/Reports/BankTransactionReportPrint.vue')));
app.component('InvoiceReportPrint', defineAsyncComponent(() => import('./components/Reports/InvoiceReportPrint.vue')));
app.component('invoiceReportPrintDownload', defineAsyncComponent(() => import('./components/Reports/InvoiceReportPrintDownload.vue')));
app.component('BankTransactionReport', defineAsyncComponent(() => import('./components/Reports/BankTransactionReport.vue')));
app.component('priceLabelingmodel', defineAsyncComponent(() => import('./components/PriceLabeling/AddPriceLabeling.vue')));
app.component('priceLabelingDropdown', defineAsyncComponent(() => import('./components/General/PriceLabelingDropdown.vue')));
app.component('bankmodel', defineAsyncComponent(() => import('./components/Bank/AddBank.vue')));
app.component('PaymentModel', defineAsyncComponent(() => import('./components/PurchaseBill/PaymentModel.vue')));
app.component('quickProductItem', defineAsyncComponent(() => import('./components/TouchScreen/QuickProductItem.vue')));
app.component('TransferModel', defineAsyncComponent(() => import('./components/ProductionBatch/TransferModel.vue')));
app.component('ProcessModel', defineAsyncComponent(() => import('./components/ProductionBatch/ProcessModel.vue')));
app.component('EmailModal', defineAsyncComponent(() => import('./components/Email/AddEmail.vue')));
app.component('HelpingMaterialOfEmailModal', defineAsyncComponent(() => import('./components/Email/HelpingMaterialOfEmail.vue')));
app.component('CompletionModel', defineAsyncComponent(() => import('./components/ProductionBatch/CompletionModel.vue')));
app.component('productMastermodel', defineAsyncComponent(() => import('./components/ProductMaster/AddProductMaster.vue')));
app.component('usersDropdown', defineAsyncComponent(() => import('./components/General/UsersDropdown.vue')));
app.component('acessdenied', defineAsyncComponent(() => import('./components/General/AcessDenied.vue')));
app.component('userRolesDropdown', defineAsyncComponent(() => import('./components/General/UserRolesDropdown.vue')));
app.component('moduleNamesDropdown', defineAsyncComponent(() => import('./components/General/ModuleNamesDropdown.vue')));
app.component('moduleCategoryDropdown', defineAsyncComponent(() => import('./components/General/ModuleCategoryDropdown.vue')));
app.component('moduleNamesDropdownForAdd', defineAsyncComponent(() => import('./components/General/ModuleNamesDropdownForAdd.vue')));
app.component('printer-list-dropdown', defineAsyncComponent(() => import('./components/General/PrinterListDropdown.vue')));
app.component('assignRoleToUsers', defineAsyncComponent(() => import('./components/UserPermissions/AssignRoleToUsers.vue')));
app.component('assignPermissionsToRole', defineAsyncComponent(() => import('./components/UserPermissions/AssignPermissionsToRole.vue')));
app.component('assignPermissionsToRoleForAdd', defineAsyncComponent(() => import('./components/UserPermissions/AssignPermissionsToRoleForAdd.vue')));
app.component('updatePermissionsToRole', defineAsyncComponent(() => import('./components/UserPermissions/UpdatePermissionsToRole.vue')));
app.component('quickemployeemodel', defineAsyncComponent(() => import('./components/EmployeeRegistration/AddQuickEmployee.vue')));
app.component('AddPriceRecord', defineAsyncComponent(() => import('./components/PriceRecord/AddPriceRecord.vue')));
app.component('addholdsetup', defineAsyncComponent(() => import('./components/HoldSetup/AddHoldSetup.vue')));
app.component('addpermanentdeleteholdsetup', defineAsyncComponent(() => import('./components/HoldSetup/AddPermanentDeleteHoldSetup.vue')));
app.component('addcustomergroup', defineAsyncComponent(() => import('./components/CustomerGroup/AddCustomerGroup.vue')));
app.component('customergroupdropdown', defineAsyncComponent(() => import('./components/General/CustomerGroupDropdown.vue')));
app.component('terminalmodel', defineAsyncComponent(() => import('./components/Terminal/AddTerminal.vue')));
app.component('modal', defineAsyncComponent(() => import('./components/modalcomponent.vue')));
app.component('datepicker', defineAsyncComponent(() => import('./components/DatePicker.vue')));
app.component('monthpicker', defineAsyncComponent(() => import('./components/MonthPicker.vue')));
app.component('coa-modal', defineAsyncComponent(() => import('./components/COA/chartOfAccountModal.vue')));
app.component('addupdate-coa', defineAsyncComponent(() => import('./components/COA/AddUpdateAccount.vue')));
app.component('licence-model', defineAsyncComponent(() => import('./components/Company/LicenceModel.vue')));
app.component('ftp-account-detail', defineAsyncComponent(() => import('./components/Company/FtpAccountDetail.vue')));
app.component('licence-history-model', defineAsyncComponent(() => import('./components/Company/LicenceHistoryModel.vue')));
app.component('company-attachment-modal', defineAsyncComponent(() => import('./components/Company/CompanyAttachmentModal.vue')));
app.component('company-attachments', defineAsyncComponent(() => import('./components/Company/CompanyAttachments.vue')));
app.component('paymentvoucherlinedetail', defineAsyncComponent(() => import('./components/PaymentVouchers/AddPaymentVoucherDetails/AddPaymentVoucherDetail.vue')));
app.component('stocklinedetail', defineAsyncComponent(() => import('./components/Product_Stock_Value/StockDetails/StockLineDetails.vue')));
app.component('viewstockitem', defineAsyncComponent(() => import('./components/Product_Stock_Value/ViewStockItem.vue')));
app.component('developersettingloginmodel', defineAsyncComponent(() => import('./components/Company/DeveloperSettingLoginModel.vue')));

// ...continuing from the previous list

// Journal Voucher Items
app.component('jvitems', defineAsyncComponent(() => import('./components/JournalVouchers/AddJvLineItem/AddLineItem.vue')));

// Daily Expense Row
app.component('adddailyexpenserow', defineAsyncComponent(() => import('./components/DailyExpense/AddDailyExpenseLineItem/AddLineItem.vue')));

// Mobile Order Items
// ... (insert your mobile order components here if any)

// Dropdowns
app.component('branchPrefixModel', defineAsyncComponent(() => import('./components/Company/BranchPrefixes.vue')));
app.component('BranchPrefixesList', defineAsyncComponent(() => import('./components/Company/BranchPrefixesList.vue')));
app.component('perioddropdown', defineAsyncComponent(() => import('./components/General/PeriodDropdown.vue')));
app.component('monthlydropdown', defineAsyncComponent(() => import('./components/General/MonthlyDropdown.vue')));
app.component('productMasterdropdown', defineAsyncComponent(() => import('./components/General/ProductMasterDropdown.vue')));
app.component('accountdropdown', defineAsyncComponent(() => import('./components/General/AccountNumberDropdown.vue')));
app.component('currencyinput', defineAsyncComponent(() => import('./components/General/InputDropdown.vue')));
app.component('categorydropdown', defineAsyncComponent(() => import('./components/General/CategoryDropdown.vue')));
app.component('supplierdropdown', defineAsyncComponent(() => import('./components/General/SupplierDropdown.vue')));
app.component('customerdropdown1', defineAsyncComponent(() => import('./components/General/CustomerDropdown1.vue')));
app.component('add-purchase', defineAsyncComponent(() => import('./components/Purchase/AddPurchase.vue')));
app.component('purchase-item', defineAsyncComponent(() => import('./components/Purchase/PurchaseItem.vue')));
app.component('purchase-costing-item', defineAsyncComponent(() => import('./components/PurchaseCosting/PurchaseCostingItem.vue')));
app.component('product-dropdown', defineAsyncComponent(() => import('./components/General/ProductDropdown.vue')));
app.component('product-for-promotion-dropdown', defineAsyncComponent(() => import('./components/General/ProductForPromotionDropdown.vue')));


// Warehouses and Dropdowns
app.component('warehouse-dropdown', defineAsyncComponent(() => import('./components/General/WarehouseDropdown.vue')));
app.component('employeeDropdown', defineAsyncComponent(() => import('./components/General/EmployeeDropdown.vue')));
app.component('purchaseinvoicedropdown', defineAsyncComponent(() => import('./components/General/PurchaseInvoiceDropDown.vue')));
app.component('productMultiSelectDropdown', defineAsyncComponent(() => import('./components/General/ProductMultiSelectDropdown.vue')));
app.component('company-dropdown', defineAsyncComponent(() => import('./components/General/CompanyDropdown.vue')));
app.component('terminal-dropdown', defineAsyncComponent(() => import('./components/General/TerminalDropDown.vue')));
app.component('barcodeDropdown', defineAsyncComponent(() => import('./components/General/BarcodeDropdown.vue')));
app.component('my-currency-input', defineAsyncComponent(() => import('./components/General/DecimalDropdown.vue')));
app.component('BillsDropdown', defineAsyncComponent(() => import('./components/General/BillsDropdown.vue')));
app.component('decimal-to-fixed', defineAsyncComponent(() => import('./components/General/DecimalToFixed.vue')));
app.component('expense-type-dropdown', defineAsyncComponent(() => import('./components/General/ExpensetypeDropdown.vue')));
app.component('reasonsaleorder', defineAsyncComponent(() => import('./components/SaleServiceOrder/ReasonOfSaleOrder.vue')));
app.component('rawProductDropdown', defineAsyncComponent(() => import('./components/General/RawProductDropdown.vue')));
app.component('PaymentImageList', defineAsyncComponent(() => import('./components/PaymentImageList.vue')));
app.component('add-serial-model', defineAsyncComponent(() => import('./components/Sale/AddSerialModel.vue')));
app.component('sale-item', defineAsyncComponent(() => import('./components/Sale/SaleItem.vue')));
app.component('credit-payment', defineAsyncComponent(() => import('./components/Sale/CreditPayment.vue')));
app.component('select-batch', defineAsyncComponent(() => import('./components/Sale/SelectBatch.vue')));
app.component('payment-option-dropdown', defineAsyncComponent(() => import('./components/General/PaymentOptionDropdown.vue')));
app.component('sale-return', defineAsyncComponent(() => import('./components/SaleReturn/SaleReturn.vue')));
app.component('add-sale-return', defineAsyncComponent(() => import('./components/SaleReturn/AddSaleReturn.vue')));
app.component('sale-invoice-dropdown', defineAsyncComponent(() => import('./components/General/SaleInvoiceDropdown.vue')));
app.component('cash-customer-dropdown', defineAsyncComponent(() => import('./components/General/CashCustomerDropdown.vue')));
app.component('sale-return-item', defineAsyncComponent(() => import('./components/SaleReturn/SaleReturnItem.vue')));
app.component('view-sale-return-item', defineAsyncComponent(() => import('./components/SaleReturn/ViewSaleReturnItem.vue')));
app.component('permissionCategoryDropdown', defineAsyncComponent(() => import('./components/General/PermissionCategoryDropdown.vue')));

// Sale Service
app.component('sale-service-item', defineAsyncComponent(() => import('./components/SaleServiceInvoice/SaleServiceItem.vue')));
app.component('sale-service-order-item', defineAsyncComponent(() => import('./components/SaleServiceOrder/SaleServiceOrderItem.vue')));

// Reports
app.component('cash-voucher-report', defineAsyncComponent(() => import('./components/Reports/CashVoucherReport.vue')));
app.component('ReparingOrderMultiPrint', defineAsyncComponent(() => import('./components/Reports/ReparingOrderMultiPrint.vue')));
app.component('ReparingOrderThermalPrint', defineAsyncComponent(() => import('./components/Reports/ReparingOrderThermalPrint.vue')));
app.component('SalesThermalpkReport', defineAsyncComponent(() => import('./components/Reports/SalesThermalpkReports.vue')));
app.component('SalesThermalpkReport2', defineAsyncComponent(() => import('./components/Reports/SalesThermalpkReports2.vue')));
app.component('SalesThermalSaudiReports3', defineAsyncComponent(() => import('./components/Reports/SalesThermalSaudiReports3.vue')));
app.component('pos-invoice-template1', defineAsyncComponent(() => import('./components/Reports/PosInvoiceTemplate1.vue')));
app.component('productPrintReport', defineAsyncComponent(() => import('./components/Reports/ProductPrintReport.vue')));
app.component('addExpense-type', defineAsyncComponent(() => import('./components/ExpenseType/AddExpenseType.vue')));



// FOR Reports
app.component('purchaseReturnDownload', defineAsyncComponent(() => import('./components/Reports/PurchaseReturnDownload.vue')));
app.component('purchaseReturn', defineAsyncComponent(() => import('./components/Reports/PurchaseReturn.vue')));
app.component('purchaseOrderDownload', defineAsyncComponent(() => import('./components/Reports/PurchaseOrderDownload.vue')));
app.component('purchaseOrder', defineAsyncComponent(() => import('./components/Reports/PurchaseOrder.vue')));
app.component('SaleInvoiceView', defineAsyncComponent(() => import('./components/Reports/SaleInvoiceView.vue')));
app.component('SaleDashboardViewReport', defineAsyncComponent(() => import('./components/Reports/SaleDashboardReport.vue')));
app.component('SaleInvoiceForMsFkahryPrint', defineAsyncComponent(() => import('./components/Reports/SaleInvoiceForMsFkahryPrint.vue')));
app.component('saleInvoice-template-one', defineAsyncComponent(() => import('./components/Reports/SaleInvoiceTemplateOne.vue')));
app.component('QuotationSmartDigitalInvoice', defineAsyncComponent(() => import('./components/Reports/QuotationSmartDigitalInvoice.vue')));
app.component('SaleInvoiceTemplate5', defineAsyncComponent(() => import('./components/Reports/SaleInvoiceTemplate5.vue')));
app.component('ObaagestSaleInvoice', defineAsyncComponent(() => import('./components/Reports/ObaagestSaleInvoice.vue')));
app.component('dailyExpenseA4', defineAsyncComponent(() => import('./components/Reports/DailyExpenseA4Report.vue')));
app.component('saleReturnReport', defineAsyncComponent(() => import('./components/Reports/SaleReturnReport.vue')));
app.component('quotation', defineAsyncComponent(() => import('./components/Reports/Quotation.vue')));

app.component('retail-report-print', defineAsyncComponent(() => import('./components/Reports/RetailReportPrint.vue')));
app.component('inventoryFilterReportPrint', defineAsyncComponent(() => import('./components/Reports/InventoryFilterReportPrint.vue')));
app.component('productInventoryRegisterPrintReport', defineAsyncComponent(() => import('./components/Reports/ProductInventoryRegisterPrintReport.vue')));
app.component('productStockValuePrintReport', defineAsyncComponent(() => import('./components/Reports/ProductStockValuePrintReport.vue')));
app.component('productStockAvgValuePrintReport', defineAsyncComponent(() => import('./components/Reports/ProductStockAvgValuePrintReport.vue')));

app.component('productDiscountCustomersPrintReport', defineAsyncComponent(() => import('./components/Reports/ProductDiscountCustomersPrintReport.vue')));
app.component('productDiscountSuppliersPrintReport', defineAsyncComponent(() => import('./components/Reports/ProductDiscountSuppliersPrintReport.vue')));
app.component('freeofCostSalePrintReport', defineAsyncComponent(() => import('./components/Reports/FreeofCostSalePrintReport.vue')));
app.component('ledgerPrintReport', defineAsyncComponent(() => import('./components/Reports/LedgerPrintReport.vue')));
app.component('rack-barcode-print', defineAsyncComponent(() => import('./components/Reports/RacksBarcodePrint.vue')));
app.component('trialBalanceReport', defineAsyncComponent(() => import('./components/Reports/TrialBalanceReport.vue')));
app.component('balanceSheetReport', defineAsyncComponent(() => import('./components/Reports/BalanceSheetReport.vue')));
app.component('incomeStatementReport', defineAsyncComponent(() => import('./components/Reports/IncomeStatementReport.vue')));
app.component('customerLedgerReport', defineAsyncComponent(() => import('./components/Reports/CustomerLedgerReport.vue')));
app.component('vatPayableReport', defineAsyncComponent(() => import('./components/Reports/VatPayableReport.vue')));

// Dashboard
app.component('dashboard', defineAsyncComponent(() => import('./components/Dashboard/Dashboard.vue')));
app.component('invoicedashboard', defineAsyncComponent(() => import('./components/Dashboard/InvocieDashboard.vue')));
app.component('forgotPassword', defineAsyncComponent(() => import('./views/ForgotPassword.vue')));
app.component('CompanyInfoImage', defineAsyncComponent(() => import('./components/CompanyInfoImage.vue')));
app.component('AddProductImage', defineAsyncComponent(() => import('./components/AddProductImage.vue')));
app.component('productimage', defineAsyncComponent(() => import('./components/productImageList.vue')));
app.component('PurchaseBillItem', defineAsyncComponent(() => import('./components/PurchaseBill/PurchaseBillItem.vue')));
app.component('pushpulllogmodel', defineAsyncComponent(() => import('./components/PushPullTransactionLog/PushPullLogModel.vue')));

// Ware House Transfer Items
app.component('warehousetransferitem', defineAsyncComponent(() => import('./components/WareHouseTransfer/WareHouseTransferItem.vue')));
app.component('stockTransferItem', defineAsyncComponent(() => import('./components/StockTransfer/StockTransferItem.vue')));
app.component('stockreceiveditem', defineAsyncComponent(() => import('./components/StockReceived/StockReceivedItem.vue')));

// Image
app.component('uploadImage', defineAsyncComponent(() => import('./components/UploadImage.vue')));
// Purchase Order Dropdown
app.component('purchase-order-dropdown', defineAsyncComponent(() => import('./components/General/PurchaseOrderDropdown.vue')));

// AuthorizeUserModel
app.component('authorize-user-model', defineAsyncComponent(() => import('./components/Sale/AuthorizeUserModel.vue')));
app.component('stockrequestdropdown', defineAsyncComponent(() => import('./components/General/StockRequestDropdown.vue')));

app.component('select-branches-model', defineAsyncComponent(() => import('./components/Product/SelectBranches.vue')));
app.component('purchase-order-dropdown', defineAsyncComponent(() => import('./components/General/PurchaseOrderDropdown.vue')));
app.component('product-single-dropdown', defineAsyncComponent(() => import('./components/General/ProductSingleDropdown.vue')));
app.component('sale-email-item', defineAsyncComponent(() => import('./components/Sale/SaleEmailItem.vue')));
app.component('bankPosTerminalmodal', defineAsyncComponent(() => import('./components/BankPosTerminal/AddBankPosTerminal.vue')));
app.component('citymodal', defineAsyncComponent(() => import('./components/City/AddCity.vue')));
app.component('regionmodal', defineAsyncComponent(() => import('./components/Region/AddRegion.vue')));
app.component('countrydropdown', defineAsyncComponent(() => import('./components/General/CountryDropdown.vue')));
app.component('countryfor-employee-dropdown', defineAsyncComponent(() => import('./components/General/CountryForEmployeeDropdown.vue')));
app.component('city-for-employee-dropdown', defineAsyncComponent(() => import('./components/General/CityForEmployeeDropdown.vue')));
app.component('provincedropdown', defineAsyncComponent(() => import('./components/General/ProvinceDropdown.vue')));
app.component('citydropdown', defineAsyncComponent(() => import('./components/General/CityDropdown.vue')));

// Area Dropdown
app.component('areadropdown', defineAsyncComponent(() => import('./components/General/AreaDropdown.vue')));
app.component('bankdropdown', defineAsyncComponent(() => import('./components/General/BankDropdown.vue')));
// Sale Order Item
app.component('saleorder-view-item', defineAsyncComponent(() => import('./components/SaleOrder/SaleOrderViewItem.vue')));
app.component('saleorder-item', defineAsyncComponent(() => import('./components/SaleOrder/SaleOrderItem.vue')));
app.component('saleorder-email-item', defineAsyncComponent(() => import('./components/SaleOrder/SaleOrderEmailItem.vue')));
app.component('quotation-item', defineAsyncComponent(() => import('./components/Quotation/QuotationItem.vue')));
app.component('quotation-email-item', defineAsyncComponent(() => import('./components/Quotation/QuotationEmailItem.vue')));

// Recipe Item
app.component('recipe-item', defineAsyncComponent(() => import('./components/RecipeNo/RecipeItem.vue')));
app.component('view-recipe-item', defineAsyncComponent(() => import('./components/RecipeNo/ViewRecipeItem.vue')));
// ProductionBatch Item
app.component('productionbatch-item', defineAsyncComponent(() => import('./components/ProductionBatch/ProductionBatchItem.vue')));
app.component('batch-view-item', defineAsyncComponent(() => import('./components/ProductionBatch/BatchViewItem.vue')));
app.component('saleorderdropdown', defineAsyncComponent(() => import('./components/General/SaleOrderDropdown.vue')));
app.component('quotationdropdown', defineAsyncComponent(() => import('./components/General/QuotationDropdown.vue')));
app.component('reciptdropdown', defineAsyncComponent(() => import('./components/General/ReciptNoDropDown.vue')));
app.component('roledropdown', defineAsyncComponent(() => import('./components/General/RolesDropdown.vue')));
app.component('actionmodal', defineAsyncComponent(() => import('./components/DailyExpense/ActionModel.vue')));

// Dispatch Note
app.component('dispatch-item', defineAsyncComponent(() => import('./components/DispatchNote/DispatchItem.vue')));
app.component('dispatch-dropdown', defineAsyncComponent(() => import('./components/General/DispatchNoteDropdown.vue')));
app.component('Backup', defineAsyncComponent(() => import('./components/DatabaseBackup/Backup.vue')));
app.component('Restore', defineAsyncComponent(() => import('./components/Restore/Restore.vue')));
app.component('Upload', defineAsyncComponent(() => import('./components/Upload/Upload.vue')));
app.component('rejectionmodel', defineAsyncComponent(() => import('./components/DailyExpense/RejectionModel.vue')));

// PrintSetting Modal
app.component('Userprofileimage', defineAsyncComponent(() => import('./components/UserProfileImage.vue')));
app.component('purchaseReturnHistorymodel', defineAsyncComponent(() => import('./components/PurchaseReturn/PurchaseReturnHistory.vue')));

// Inventory Blind Print
app.component('add-company-process', defineAsyncComponent(() => import('./components/CompanyProcess/AddCompanyProcess.vue')));
app.component('add-company-action', defineAsyncComponent(() => import('./components/CompanyAction/AddCompanyAction.vue')));
app.component('import-attachment', defineAsyncComponent(() => import('./components/CompanyAction/ImportAttachment.vue')));
app.component('bulk-attachment', defineAsyncComponent(() => import('./components/Attachment/ImportAttachment.vue')));
app.component('attachment-view', defineAsyncComponent(() => import('./components/Attachment/AttachmentView.vue')));
app.component('purchase-order-payment-view', defineAsyncComponent(() => import('./components/PurchaseOrder/PurchaseOrderPaymentView.vue')));
app.component('add-company-option', defineAsyncComponent(() => import('./components/CompanyOptions/AddCompanyOption.vue')));
app.component('day-dropdown', defineAsyncComponent(() => import('./components/General/DayDropdown.vue')));
app.component('sample-request-dropdown', defineAsyncComponent(() => import('./components/General/SampleRequestDropDown.vue')));
app.component('purchaseorder-expensebill', defineAsyncComponent(() => import('./components/PurchaseOrder/PurchaseOrderExpenseBill.vue')));

// HR
app.component('add-deduction', defineAsyncComponent(() => import('./components/Hr/Payroll/Deduction/AddDeduction.vue')));
app.component('add-shift', defineAsyncComponent(() => import('./components/Hr/Shift/AddShift.vue')));
app.component('shift-dropdown', defineAsyncComponent(() => import('./components/General/ShiftDropdown.vue')));
app.component('add-contribution', defineAsyncComponent(() => import('./components/Hr/Payroll/Contribution/AddContribution.vue')));

// PayRoll
app.component('add-payroll-schedule', defineAsyncComponent(() => import('./components/Hr/Payroll/PayrollSchedule/AddPayrollSchedule.vue')));
app.component('addAllowance', defineAsyncComponent(() => import('./components/Hr/Payroll/Allowance/AddAllowance.vue')));
app.component('AddAllowanceType', defineAsyncComponent(() => import('./components/Hr/Payroll/AllowanceType/AddAllowanceType.vue')));
app.component('allowanceTypeDropdown', defineAsyncComponent(() => import('./components/General/AllowanceTypeDropdown.vue')));
app.component('allowanceDropdown', defineAsyncComponent(() => import('./components/General/AllowanceDropdown.vue')));
app.component('deductionDropdown', defineAsyncComponent(() => import('./components/General/DeductionDropdown.vue')));
app.component('addDeduction', defineAsyncComponent(() => import('./components/Hr/Payroll/Deduction/AddDeduction.vue')));
app.component('contributionDropdown', defineAsyncComponent(() => import('./components/General/ContributionDropdown.vue')));
app.component('salary-template-dropdown', defineAsyncComponent(() => import('./components/General/SalaryTemplateDropdown.vue')));
app.component('payroll-schedule-dropdown', defineAsyncComponent(() => import('./components/General/PayrollScheduleDropdown.vue')));

app.component('sample-item', defineAsyncComponent(() => import('./components/ProductionModule/SampleRequest/SampleItem.vue')));
app.component('process-item', defineAsyncComponent(() => import('./components/ProductionModule/Process/ProcessItem.vue')));

app.component('contractor-item', defineAsyncComponent(() => import('./components/ProductionBatch/ContractorItem.vue')));
app.component('gate-pass-report', defineAsyncComponent(() => import('./components/Reports/GatePassReport.vue')));

app.component('departmentModel', defineAsyncComponent(() => import('./components/Hr/Employee/Department/AddDepartment.vue')));
app.component('designationModel', defineAsyncComponent(() => import('./components/Hr/Employee/Designation/AddDesignation.vue')));
app.component('designationDropdown', defineAsyncComponent(() => import('./components/General/DesignationDropdown.vue')));
app.component('designation-multi-dropdown', defineAsyncComponent(() => import('./components/General/DesignationMultiDropdown.vue')));
app.component('department-multi-dropdown', defineAsyncComponent(() => import('./components/General/DepartmentMultiDropdown.vue')));

app.component('departmentDropdown', defineAsyncComponent(() => import('./components/General/DepartmentDropdown.vue')));
app.component('loanDetailModel', defineAsyncComponent(() => import('./components/Hr/Payroll/LoanPayment/LoanDetailModel.vue')));
app.component('DeliveryChallanModel', defineAsyncComponent(() => import('./components/DeliveryChallan/AddDeliveryChallanModel.vue')));

app.component('loanRecovery', defineAsyncComponent(() => import('./components/Hr/Payroll/LoanPayment/LoanRecoveryModel.vue')));
app.component('salaryTemplateModel', defineAsyncComponent(() => import('./components/Hr/Payroll/RunPayroll/SalaryTemplateModel.vue')));

app.component('run-payroll-model', defineAsyncComponent(() => import('./components/Hr/Payroll/RunPayroll/RunPayrollView.vue')));
app.component('run-payroll-report-print', defineAsyncComponent(() => import('./components/Reports/PayRunReportPrint.vue')));

app.component('monthlysalesReportPrint', defineAsyncComponent(() => import('./components/Reports/MonthlySalesReportPrint.vue')));

/* Service Quotation */
app.component('quotation-service-item', defineAsyncComponent(() => import('./components/QuotationService/ServiceQuotationItem.vue')));

app.component('payslip-print', defineAsyncComponent(() => import('./components/Reports/PaySlipPrint.vue')));

app.component('add-warranty', defineAsyncComponent(() => import('./components/warrantyType/AddWarranty.vue')));

app.component('auto-purchase-template-dropdown', defineAsyncComponent(() => import('./components/General/AutoPurchaseOrderDropdown.vue')));

/* Temporary Cash Request */
app.component('temporary-cash-request-item', defineAsyncComponent(() => import('./components/TemporaryCashRequest/TemporaryCashRequestItem.vue')));
app.component('temporary-cash-request-print', defineAsyncComponent(() => import('./components/Reports/TemporaryCashRequestPrint.vue')));
app.component('temporary-cash-issue-print', defineAsyncComponent(() => import('./components/Reports/TemporaryCashIssuePrint.vue')));
app.component('ReportPrintView', defineAsyncComponent(() => import('./components/Reports/ReportPrintView.vue')));

//Temporary Cash Issue
app.component('temporary-cash-issue-item', defineAsyncComponent(() => import('./components/TemporaryCashIssue/TemporaryCashIssueItem.vue')));
app.component('temporary-cash-request-dropdown', defineAsyncComponent(() => import('./components/General/TemporaryCashRequestDropdown.vue')));
app.component('temporary-cash-issue-Return', defineAsyncComponent(() => import('./components/TemporaryCashIssue/TemporaryCashIssueReturn.vue')));
app.component('temporary-cash-issue-history', defineAsyncComponent(() => import('./components/TemporaryCashIssue/TemporaryCashIssueHistory.vue')));
app.component('temporary-cash-issue-detail-Print', defineAsyncComponent(() => import('./components/Reports/TemporaryCashIssueDetailPrint.vue')));
app.component('temporary-cash-allocation-Print', defineAsyncComponent(() => import('./components/Reports/TemporaryCashAllocationPrint.vue')));

app.component('chequelistmodel', defineAsyncComponent(() => import('./components/Bank/ChequeListModel.vue')));
app.component('BankMultiDropdown', defineAsyncComponent(() => import('./components/General/BankMultiDropdown.vue')));
app.component('chequemodel', defineAsyncComponent(() => import('./components/Bank/ChequeModel.vue')));
app.component('blockmodel', defineAsyncComponent(() => import('./components/Bank/BlockModel.vue')));
app.component('chequeandguranteemodel', defineAsyncComponent(() => import('./components/Bank/ChequeAndGuranteeModel.vue')));
app.component('issuedtodropdown', defineAsyncComponent(() => import('./components/General/IssuedToDropdown.vue')));

app.component('usc-report-print', defineAsyncComponent(() => import('./components/Reports/USCReportPrint.vue')));

app.component('monthlycostitem', defineAsyncComponent(() => import('./components/MonthlyCost/MonthlyCostItem.vue')));
app.component('trialSubAccount', defineAsyncComponent(() => import('./components/Reports/TrialSubAccountModel.vue')));

app.component('SalesDashboardReport', defineAsyncComponent(() => import('./components/Reports/SalesDashboardReport.vue')));
app.component('importExportmodel', defineAsyncComponent(() => import('./components/ImportExport/AddImportExport.vue')));
app.component('reparingOrdermodel', defineAsyncComponent(() => import('./components/ReparingOrderType/AddReparingOrderType.vue')));

app.component('importexportdropdown', defineAsyncComponent(() => import('./components/General/ImportExportDropdown.vue')));
app.component('reparingordertypeDropdown', defineAsyncComponent(() => import('./components/General/ReparingOrderTypeDropdown.vue')));
app.component('sale-order-tracking-report-download', defineAsyncComponent(() => import('./components/Reports/SaleOrderTrackingReportDownload.vue')));
app.component('sale-order-tracking-report-print', defineAsyncComponent(() => import('./components/Reports/SaleOrderTrackingReportPrint.vue')));

app.component('QuotationPdf', defineAsyncComponent(() => import('./components/Reports/QuotationPdf.vue')));
app.component('SaleReturnPdf', defineAsyncComponent(() => import('./components/Reports/SaleReturnPdf.vue')));
app.component('LedgerDownloadReport', defineAsyncComponent(() => import('./components/Reports/LedgerDownloadReport.vue')));
app.component('MonthlyPdf', defineAsyncComponent(() => import('./components/Reports/MonthlySalesPdfPrint.vue')));
app.component('BankTransactionPdf', defineAsyncComponent(() => import('./components/Reports/BankTransactionPdfPrint.vue')));
app.component('AccountDashboard', defineAsyncComponent(() => import('./components/Dashboard/AccountDashboard.vue')));
app.component('HRDashboard', defineAsyncComponent(() => import('./components/Dashboard/HRDashboard.vue')));
app.component('InventoryDashboard', defineAsyncComponent(() => import('./components/Dashboard/InventoryDashboard.vue')));
app.component('CashAndBankDashboard', defineAsyncComponent(() => import('./components/Dashboard/CashAndBankDashboard.vue')));

// AddInquiry Process
app.component('add-inquiry-process-model', defineAsyncComponent(() => import('./components/InquiryProcess/AddInquiryProcess.vue')));
app.component('inquiry-process-dropdown', defineAsyncComponent(() => import('./components/General/InquiryProcessDropdown.vue')));

// Add Inquiry Module
app.component('add-inquiry-module-model', defineAsyncComponent(() => import('./components/InquiryModule/AddInquiryModule.vue')));
app.component('inquiry-module-dropdown', defineAsyncComponent(() => import('./components/General/InquiryModuleDropdown.vue')));
app.component('GoodReceiveDropdown', defineAsyncComponent(() => import('./components/General/GoodReceiveDropdown.vue')));

// Add Inquiry Update Status
app.component('inquiry-status-update', defineAsyncComponent(() => import('./components/Inquiry/UpdateStatus.vue')));
app.component('add-inquiry-type-model', defineAsyncComponent(() => import('./components/InquiryType/AddInquiryType.vue')));
app.component('inquiry-type-dropdown', defineAsyncComponent(() => import('./components/General/InquiryTypeDropdown.vue')));
app.component('sale-invoice-default', defineAsyncComponent(() => import('./components/Reports/SaleInvoiceDefault.vue')));
app.component('sale-invoice-service-default-Download', defineAsyncComponent(() => import('./components/Reports/SaleInvoiceServicePdfDownload.vue')));
app.component('quotation-service-pdf', defineAsyncComponent(() => import('./components/Reports/QuotationServicePdf.vue')));
app.component('GoodReceiveItem', defineAsyncComponent(() => import('./components/GRN/GoodReceiveItem.vue')));
app.component('ReparingOrderItem', defineAsyncComponent(() => import('./components/ReparingOrder/ReparingOrderItem.vue')));
app.component('LedgerAccountWisePrintReport', defineAsyncComponent(() => import('./components/Reports/LedgerAccountWisePrintReport.vue')));
app.component('LedgerAccountWiseDownloadReport', defineAsyncComponent(() => import('./components/Reports/LedgerAccountWiseDownloadReport.vue')));
app.component('ProductFilterDownloadReport', defineAsyncComponent(() => import('./components/Reports/ProductFilterDownloadReport.vue')));
app.component('AttendenceFilterReport', defineAsyncComponent(() => import('./components/Reports/AttendenceFilterReportPrint.vue')));
app.component('ReparingOrderPaymentPrint', defineAsyncComponent(() => import('./components/Reports/ReparingOrderPaymentPrint.vue')));
app.component('PrintSettingImages', defineAsyncComponent(() => import('./components/PrintSettingImages.vue')));

app.component('JournalVoucherPrint', defineAsyncComponent(() => import('./components/Reports/JournalVoucherPrint.vue')));
app.component('TodayAttendenceModel', defineAsyncComponent(() => import('./components/ManualAttendance/AddManualAttendence.vue')));
app.component('QuotationItemTemplate', defineAsyncComponent(() => import('./components/Quotation/QuotationItemTemplate.vue')));
app.component('DeliveryChallanItem', defineAsyncComponent(() => import('./components/DeliveryChallan/DeliveryChallanItem.vue')));
app.component('QuotationTemplateDropdown', defineAsyncComponent(() => import('./components/General/QuotationTemplateDropdown.vue')));
app.component('ComparisionLedgerPrintReport', defineAsyncComponent(() => import('./components/Reports/ComparisionLedgerPrintReport.vue')));
app.component('ComparisionLedgerPrintDownload', defineAsyncComponent(() => import('./components/Reports/ComparisionLedgerPrintDownload.vue')));
app.component('AddCustomerModel', defineAsyncComponent(() => import('./components/ReparingOrder/AddCustomer.vue')));

// DeliveryChallan
app.component('creditnoteitem', defineAsyncComponent(() => import('./components/CreditNote/CreditNoteItem.vue')));

// Branches
app.component('addbranches', defineAsyncComponent(() => import('./components/Branches/AddBranches.vue')));
app.component('branch-dropdown', defineAsyncComponent(() => import('./components/General/BranchDropdown.vue')));

// BranchUsers
app.component('addbranchuser', defineAsyncComponent(() => import('./components/BranchUsers/AddBranchUser.vue')));

// ProductGroup
app.component('addproductgroup', defineAsyncComponent(() => import('./components/ProductGroup/AddProductGroup.vue')));
app.component('productgroupdropdown', defineAsyncComponent(() => import('./components/General/ProductGroupDropdown.vue')));

// PaymentVouchers
app.component('payment-refund-model', defineAsyncComponent(() => import('./components/PaymentVouchers/PaymentRefundModel.vue')));



app.config.globalProperties.$https = axios;
axios.defaults.baseURL = json.ServerIP;
app.config.globalProperties.$ServerIp = json.ServerIP;
app.config.globalProperties.$PermissionIp = json.PermissionIP;
app.config.globalProperties.$ClientIP = json.ClientIP;
app.config.globalProperties.$ReportServer = json.ReportServer;
app.config.globalProperties.$macAddess = 'D8-FC-93-2E-5B-94';
app.config.globalProperties.$SystemType = json.SystemType;


app.config.globalProperties.$englishLanguage = function (value) {
    var getEnglishLanguage = localStorage.getItem('English');
    var getArabicLanguage = localStorage.getItem('Arabic');
    var getPortuguesLanguage = localStorage.getItem('Portugues');
    if (getEnglishLanguage == 'true' && getArabicLanguage == 'false' && getPortuguesLanguage == 'false') {
        return value;
    }
    else if (getEnglishLanguage == 'false' && getArabicLanguage == 'true' && getPortuguesLanguage == 'false') {
        return value;
    }
    else if (getEnglishLanguage == 'false' && getArabicLanguage == 'false' && getPortuguesLanguage == 'true') {
        return value;
    }

    else {
        var lan = localStorage.getItem('locales');

        if (lan == 'en') {
            return value + ' (English)';
        }
        else if (lan == 'pt') {
            return value + ' (Inglês)';
        }
        else {
            return value + ' (انجليزي)';
        }

    }
};


app.config.globalProperties.$arabicLanguage = function (value) {
    var getEnglishLanguage = localStorage.getItem('English');
    var getArabicLanguage = localStorage.getItem('Arabic');
    var getPortuguesLanguage = localStorage.getItem('Portugues');
    if (getEnglishLanguage == 'true' && getArabicLanguage == 'false' && getPortuguesLanguage == 'false') {
        return value;
    }
    else if (getEnglishLanguage == 'false' && getArabicLanguage == 'true' && getPortuguesLanguage == 'false') {
        return value;
    }
    else if (getEnglishLanguage == 'false' && getArabicLanguage == 'false' && getPortuguesLanguage == 'true') {
        return value;
    }
    else {
        var lan = localStorage.getItem('locales');
        if (lan == 'en' && getArabicLanguage == 'false' && getPortuguesLanguage == 'true') {
            return value + ' (Portugues)';
        }
        else if (lan == 'en' && getArabicLanguage == 'true' && getPortuguesLanguage == 'false') {
            return value + ' (Arabic)';
        }
        else if (lan == 'pt') {
            return value + ' (Portugues)';
        }
        else {
            return value + ' (عربي)';
        }

    }
};

app.config.globalProperties.$formatAmount = function (value) {
    if (!value) return '';
    return parseFloat(value).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,");
};

app.config.globalProperties.$roundOffFilter = function (value) {
    if (!value) {
        value = 0;
    }
    return parseFloat(value).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,");
};

app.config.globalProperties.$roundAmount = function (value) {
    if (!value) return 0;
    return parseFloat(value).toFixed(3).slice(0, -1);
};

app.config.globalProperties.$store = useMainStore();
app.use(router);
app.use(i18n);


app.mount('#app')
