//import { h } from 'vue';
import { createRouter, createWebHistory } from 'vue-router';
//import { stringifyQuery } from 'vue-router'
import { useMainStore } from '@/store/index'
import { defineAsyncComponent } from 'vue'


import Login from '../views/Login.vue';
import Dashboard from '../views/Dashboard.vue';
import NewPassword from '../views/NewPassword.vue';
import ResetPassword from '../views/ResetPassword.vue';
import Navbar from '../views/Navbar.vue';
import sync from '../views/Sync.vue';
import CommingSoon from '../views/CommingSoon.vue';
import TouchScreen from '../components/TouchScreen/TouchScreen.vue';
import LockScreen from '../views/LockScreen.vue';




// import App from '../App.vue'

const routes = [

    {
        path: '/',
        name: 'Login',
        component: Login
    },
    {
        path: '/NewPassword',
        name: 'NewPassword',
        component: NewPassword
    },
    {
        path: '/LockScreen',
        name: 'LockScreen',
        component: LockScreen
    },
    {
        path: '/NewPassword',
        name: 'NewPassword',
        component: NewPassword
    },
    {
        path: '/ResetPassword',
        name: 'ResetPassword',
        component: ResetPassword
    },

    {
        path: '/TouchScreen',
        name: 'TouchScreen',
        component: TouchScreen,
    },
    {
        path: '/Welcome',
        name: 'Welcome',
        component: defineAsyncComponent(() => import('../components/UserScreen/Welcome.vue')),
    },
    {
        path: '/SaleEmail',
        name: 'SaleEmail',
        component:  defineAsyncComponent(() => import('../components/Sale/SaleEmail.vue'))
    },
    {
        path: '/SaleOrderEmail',
        name: 'SaleOrderEmail',
        component: defineAsyncComponent(() => import('../components/SaleOrder/SaleOrderEmail.vue'))
    },
    {
        path: '/QuotationEmail',
        name: 'QuotationEmail',
        component: defineAsyncComponent(() => import('../components/Quotation/QuotationEmail.vue'))
    },
    {
        path: '/TermDashboard',
        name: 'TermDashboard',
        component: defineAsyncComponent(() => import('../components/Dashboard/TermsDashboard.vue')),
        children: [
            {
                path: '/TermsAndConditions',
                name: 'TermsAndConditions',
                component:  defineAsyncComponent(() => import('../components/TermsAndConditions/TermAndCondition.vue'))

            },
            {
                path: '/Setup',
                name: 'Setup',
                component: defineAsyncComponent(() => import('../components/TermsAndConditions/Setup.vue'))
            },
            {
                path: '/Setupcoa',
                name: 'Setupcoa',
                component: defineAsyncComponent(() => import('../components/COA/AddUpdateAccount.vue'))
            },
            {
                path: '/CoaTemplate',
                name: 'CoaTemplate',
                component: defineAsyncComponent(() => import('../components/COA/CoaTemplate.vue')),
            },
            {
                path: '/SetupAccount',
                name: 'SetupAccount',
                component:  defineAsyncComponent(() => import('../components/CompanyAccountSetup/AddAccountSetup.vue'))
            },
            {
                path: '/Support',
                name: 'Support',
                component: defineAsyncComponent(() => import('../views/Support.vue'))
            },
            {
                path: '/SetupUser',
                name: 'SetupUser',
                component: defineAsyncComponent(() => import('../components/RegisterUser.vue'))
            },
            {
                path: '/CompanyInfo',
                name: 'CompanyInfo',
                component: defineAsyncComponent(() => import('../components/CompanyProfile/CompanyInfo.vue')),
            },
            {
                path: '/FinancialYearSetup',
                name: 'FinancialYearSetup',
                component: defineAsyncComponent(() => import('../components/FinancialYear/FinancialYearSetup.vue')),
            }
        ]
    },

    {
        path: '/dashboard',
        name: 'Dashboard',
        component: Dashboard,
        children: [
            {
                path: '/SmsSetup',
                name: 'SmsSetup',
                component: defineAsyncComponent(() => import('../components/SmsSetup/SmsSetup.vue')),
            },
            {
                path: '/ComparisionLedgerReport',
                name: 'ComparisionLedgerReport',
                component: defineAsyncComponent(() => import('../components/Reports/ComparisionLedgerReport.vue')),
            },
            {
                path: '/DefaultSetting',
                name: 'DefaultSetting',
                component: defineAsyncComponent(() => import('../components/DefaultSetting/DefaultSetting.vue')),
            },
            {
                path: '/Prefixes',
                name: 'Prefixes',
                component: defineAsyncComponent(() => import('../components/Prefixes/Prefixes.vue')),
            },
            {
                path: '/registeruser',
                name: 'RegisterUser',
                component: defineAsyncComponent(() => import('../components/RegisterUser.vue'))
            },
            {
                path: '/about',
                name: 'About',
                component: defineAsyncComponent(() => import('../views/About.vue'))
            },
            {
                path: '/navbar',
                name: 'Navbar',
                component: Navbar
            },
            {
                path: '/Company',
                name: 'Company',
                component: defineAsyncComponent(() => import('../components/Company/Company.vue'))
            },
            {
                path: '/Branches',
                name: 'Branches',
                component: defineAsyncComponent(() => import('../components/Branches/Branches.vue')),
            },
            {
                path: '/BranchesPermission',
                name: 'BranchesPermission',
                component: defineAsyncComponent(() => import('../components/Branches/BranchesPermission.vue')),
            },
            {
                path: '/BranchUsers',
                name: 'BranchUsers',
                component: defineAsyncComponent(() => import('../components/BranchUsers/BranchUsers.vue')),

            },
            {
                path: '/CompanyAttachments',
                name: 'CompanyAttachments',
                component: defineAsyncComponent(() => import('../components/Company/CompanyAttachments.vue'))
            },

            {
                path: '/superadmin',
                name: 'Superadmin',
                component:  defineAsyncComponent(() => import('../components/Dashboard/SuperAdmin.vue'))
            },

            {
                path: '/addcompany',
                name: 'AddCompany',
                component: defineAsyncComponent(() => import('../components/Company/AddCompany.vue'))
            },


            {
                path: '/addbusiness',
                name: 'AddBusiness',
                component: defineAsyncComponent(() => import('../components/Business/AddBusiness.vue'))
            },


            {
                path: '/business',
                name: 'Business',
                component: defineAsyncComponent(() => import('../components/Business/Business.vue'))
            },
            {
                path: '/location',
                name: 'Location',
                component: defineAsyncComponent(() => import('../components/Location/Location.vue'))
            },
            {
                path: '/addLocation',
                name: 'AddLocation',
                component: defineAsyncComponent(() => import('../components/Location/AddLocation.vue'))
            },
            {
                path: '/companyAdditionalInfo',
                name: 'CompanyAdditionalInfo',
                component:  defineAsyncComponent(() => import('../components/Company/CompanyAdditionalInfo.vue'))
            },
            {
                path: '/companyAdditionalList',
                name: 'CompanyAdditionalList',
                component:  defineAsyncComponent(() => import('../components/Company/CompanyAdditionalList.vue'))
            },

            {
                path: '/supplier',
                name: 'Supplier',
                component: defineAsyncComponent(() => import('../components/Supplier/Supplier.vue'))
            }
            ,
            {
                path: '/addsupplier',
                name: 'AddSupplier',
                component: defineAsyncComponent(() => import('../components/Supplier/AddSupplier.vue'))
            }
            ,
            {
                path: '/customer',
                name: 'Customer',
                component: defineAsyncComponent(() => import('../components/Customer/Customer.vue'))
            }
            ,
            {
                path: '/AddReparingOrder',
                name: 'AddReparingOrder',
                component: defineAsyncComponent(() => import('../components/ReparingOrder/AddReparingOrder.vue')),
            }
            ,
            {
                path: '/ReparingOrder',
                name: 'ReparingOrder',
                component: defineAsyncComponent(() => import('../components/ReparingOrder/ReparingOrder.vue')),
            },
            {
                path: '/GoodReceive',
                name: 'GoodReceive',
                component: defineAsyncComponent(() => import('../components/GRN/GoodReceive.vue')),
            }
            ,
            {
                path: '/AddQuotationTemplate',
                name: 'AddQuotationTemplate',
                component: defineAsyncComponent(() => import('../components/Quotation/AddQuotationTemplate.vue')),
            }
            ,
            {
                path: '/ServiceQuotationTemplate',
                name: 'ServiceQuotationTemplate',
                component: defineAsyncComponent(() => import('../components/QuotationService/ServiceQuotationTemplate.vue')),
            }
            ,
            {
                path: '/AddDeliveryChallan',
                name: 'AddDeliveryChallan',
                props(route) {
                    return { formName: route.query.formName };
                },
                component: defineAsyncComponent(() => import('../components/DeliveryChallan/AddDeliveryChallan.vue')),
            }
            ,
            {
                path: '/QuotationTemplate',
                name: 'QuotationTemplate',
                component: defineAsyncComponent(() => import('../components/Quotation/QuotationTemplate.vue')),
            }
            ,
            {
                path: '/AddGoodReceive',
                name: 'AddGoodReceive',
                component: defineAsyncComponent(() => import('../components/GRN/AddGoodReceive.vue')),
            }
            ,
            {
                path: '/addCustomer',
                name: 'AddCustomer',
                component: defineAsyncComponent(() => import('../components/Customer/AddCustomer.vue')),
                props(route) {
                    return { formName: route.query.formName };
                },
            },
            {
                path: '/AddReparingOrder',
                name: 'AddReparingOrder',
                component: defineAsyncComponent(() => import('../components/ReparingOrder/AddReparingOrder.vue')),
            }
            ,
            {
                path: '/ReparingOrder',
                name: 'ReparingOrder',
                component: defineAsyncComponent(() => import('../components/ReparingOrder/ReparingOrder.vue')),
            },

            {
                path: '/userprofile',
                name: 'UserProfileInfo',
                component: defineAsyncComponent(() => import('../components/UserProfileInfo.vue'))
            },

            {
                path: '/products',
                name: 'Product',
                component: defineAsyncComponent(() => import('../components/Product/Product.vue'))
            }
            ,
            {
                path: '/addproduct',
                name: 'AddProduct',
                component: () => import('../components/Product/AddProduct.vue')
            },

            {
                path: '/terminal',
                name: 'Terminal',
                component: defineAsyncComponent(() => import('../components/Terminal/Terminal.vue'))
            },
            {
                path: '/addpurchase',
                name: 'Addpurchase',
                component: defineAsyncComponent(() => import('../components/Purchase/AddPurchase.vue'))
            },
            {
                path: '/promotion',
                name: 'Promotion',
                component: defineAsyncComponent(() => import('../components/Promotion_Discount/Promotion.vue'))
            },
            {
                path: '/addPromotion',
                name: 'AddPromotion',
                component: defineAsyncComponent(() => import('../components/Promotion_Discount/AddPromotion.vue'))
            },
            {
                path: '/bundles',
                name: 'Bundles',
                component: defineAsyncComponent(() => import('../components/Bundle_Category/Bundles.vue'))
            },
            {
                path: '/addBundles',
                name: 'AddBundles',
                component: defineAsyncComponent(() => import('../components/Bundle_Category/AddBundles.vue'))
            },

            {
                path: '/addCurrency',
                name: 'AddCurrency',
                component: defineAsyncComponent(() => import('../components/Currency/AddCurrency.vue'))
            },

            {
                path: '/addPaymentOptions',
                name: 'AddPaymentOptions',
                component: defineAsyncComponent(() => import('../components/Payment_Options/AddPaymentOptions.vue'))
            },
            {
                path: '/addpurchaseorder',
                name: 'Addpurchaseorder',
                component: defineAsyncComponent(() => import('../components/PurchaseOrder/AddPurchaseOrder.vue')),
                props(route) {
                    return { formName: route.query.formName };
                },
            },
            {
                path: '/purchaseorder',
                name: 'purchaseorder',
                component: defineAsyncComponent(() => import('../components/PurchaseOrder/PurchaseOrder.vue')),
                props(route) {
                    return { formName: route.query.formName };
                },
            },
            {
                path: '/EmployeeTodayAttendence',
                name: 'EmployeeTodayAttendence',
                component: defineAsyncComponent(() => import('../components/ManualAttendance/EmployeeTodayAttendence.vue')),
            },
            {
                path: '/daystart',
                name: 'daystart',
                component: defineAsyncComponent(() => import('../components/DayStart/DayStart.vue'))
            },
            {
                path: '/WholeSaleDay',
                name: 'WholeSaleDay',
                component:  defineAsyncComponent(() => import('../components/WholeSaleDay/WholeSaleDay.vue'))
            },
            {
                path: '/employeeRegistration',
                name: 'EmployeeRegistration',
                component: defineAsyncComponent(() => import('../components/EmployeeRegistration/EmployeeRegistration.vue'))
            },
            {
                path: '/EmployeeView',
                name: 'EmployeeView',
                component: defineAsyncComponent(() => import('../components/EmployeeRegistration/EmployeeView.vue'))
            },
            {
                path: '/addEmployeeRegistration',
                name: 'AddEmployeeRegistration',
                component: defineAsyncComponent(() => import('../components/EmployeeRegistration/AddEmployeeRegistration.vue'))
            },
            {
                path: '/addSignUp',
                name: 'AddSignUp',
                component: defineAsyncComponent(() => import('../components/LoginCredentials/AddSignUp.vue'))
            },

            {
                path: '/addPurchaseReturn',
                name: 'AddPurchaseReturn',
                component: defineAsyncComponent(() => import('../components/PurchaseReturn/AddPurchaseReturn.vue'))
            },
            {
                path: '/PurchaseReturn',
                name: 'PurchaseReturn',
                component: defineAsyncComponent(() => import('../components/PurchaseReturn/PurchaseReturn.vue'))
            },
            {
                path: '/ClientManagement',
                name: 'clientManagement',
                component: defineAsyncComponent(() => import('../components/Company/ClientManagement.vue'))
            },
            {
                path: '/addjournalvoucher',
                name: 'AddJournalVoucher',
                props(route) {
                    return { formName: route.query.formName };
                },
                component: defineAsyncComponent(() => import('../components/JournalVouchers/AddJournalVoucher.vue'))
            },
            {
                path: '/journalvoucherview',
                name: 'JournalVoucherView',
                props(route) {
                    return { formName: route.query.formName };
                },
                component: defineAsyncComponent(() => import('../components/JournalVouchers/JournalVoucherView.vue'))
            },
            {
                path: '/paymentVoucherList',
                name: 'PaymentVoucherList',
                props(route) {
                    return { formName: route.query.formName };
                },
                component: defineAsyncComponent(() => import('../components/PaymentVouchers/PaymentVoucherList.vue'))
            },
            {
                path: '/AddBranchVoucher',
                name: 'AddBranchVoucher',
                props(route) {
                    return { formName: route.query.formName };
                },
                component: defineAsyncComponent(() => import('../components/BranchVouchers/AddBranchVoucher.vue'))
            },
            {
                path: '/BranchVouchers',
                name: 'BranchVouchers',
                props(route) {
                    return { formName: route.query.formName };
                },
                component: defineAsyncComponent(() => import('../components/BranchVouchers/BranchVouchers.vue'))
            },
            {
                path: '/addPaymentVoucher',
                name: 'addPaymentVoucher',
                props(route) {
                    return { formName: route.query.formName };
                },
                component: defineAsyncComponent(() => import('../components/PaymentVouchers/AddPaymentVoucher.vue'))
            },
            {

                path: '/PaymentVoucherView',
                name: 'PaymentVoucherView',
                props(route) {
                    return { formName: route.query.formName };
                },
                component: defineAsyncComponent(() => import('../components/PaymentVouchers/PaymentVoucherView.vue')),
            },
            {
                path: '/stockValue',
                name: 'StockValue',
                props(route) {
                    return { formName: route.query.formName };
                },
                component: defineAsyncComponent(() => import('../components/Product_Stock_Value/StockValue.vue'))
            },
            {
                path: '/addStockValue',
                name: 'AddStockValue',
                props(route) {
                    return { formName: route.query.formName };
                },
                component:  defineAsyncComponent(() => import('../components/Product_Stock_Value/AddStockValue.vue'))
            },
            {
                path: '/ViewStock',
                name: 'ViewStock',
                props(route) {
                    return { formName: route.query.formName };
                },
                component: defineAsyncComponent(() => import('../components/Product_Stock_Value/ViewStock.vue'))
            },
            {
                path: '/dailyexpense',
                name: 'DailyExpense',
                props(route) {
                    return { formName: route.query.formName };
                },
                component: defineAsyncComponent(() => import('../components/DailyExpense/DailyExpense.vue')),
            },
            {
                path: '/adddailyexpense',
                name: 'AddDailyExpense',
                props(route) {
                    return { formName: route.query.formName };
                },
                component: defineAsyncComponent(() => import('../components/DailyExpense/AddDailyExpense.vue')),
            },
            {
                path: '/DailyExpenseView',
                props(route) {
                    return { formName: route.query.formName };
                },
                name: 'DailyExpenseView',
                component: defineAsyncComponent(() => import('../components/DailyExpense/DailyExpenseView.vue'))
            },
            {
                path: '/addwarehouse',
                name: 'AddWarehouse',
                component: defineAsyncComponent(() => import('../components/Warehouse/AddWarehouse.vue'))
            },
            {
                path: '/warehouse',
                name: 'Warehouse',
                component: defineAsyncComponent(() => import('../components/Warehouse/Warehouse.vue'))
            },
            //{
            //    path: '/SaleInvoice',
            //    name: 'SaleInvoice',
            //    component: SaleInvoice,
            //    props(route) {
            //        return { formName: route.query.formName };
            //    },
            //},
            {
                path: '/AddSaleServiceOrder',
                name: 'AddSaleServiceOrder',
                component: defineAsyncComponent(() => import('../components/SaleServiceOrder/AddSaleServiceOrder.vue')),
            },
            {
                path: '/SaleServiceOrder',
                name: 'SaleServiceOrder',
                props(route) {
                    return { formName: route.query.formName };
                },
                component: defineAsyncComponent(() => import('../components/SaleServiceOrder/SaleServiceOrder.vue')),
            },
            {
                path: '/addAccountSetup',
                name: 'AddAccountSetup',
                component: defineAsyncComponent(() => import('../components/CompanyAccountSetup/AddAccountSetup.vue'))
            },
            {
                path: '/AddBank',
                name: 'AddBank',
                component: defineAsyncComponent(() => import('../components/Bank/AddBank.vue'))
            },

            {
                path: '/Dashboard1',
                name: 'Dashboard1',
                component: defineAsyncComponent(() => import('../components/Dashboard/Dashboard.vue'))
            },

            {
                path: '/ProductDetailDashboard',
                name: 'ProductDetailDashboard',
                component:  defineAsyncComponent(() => import('../components/Dashboard/ProductDetailDashBoard.vue'))
            },
            {
                path: '/ProductFilterDashBoard',
                name: 'ProductFilterDashBoard',
                component: defineAsyncComponent(() => import('../components/Reports/ProductFilterDashBoard.vue'))
            },

            {
                path: '/SaleReturn',
                name: 'SaleReturn',
                component: defineAsyncComponent(() => import('../components/SaleReturn/SaleReturn.vue'))
            },
            {
                path: '/AddSaleReturn',
                name: 'AddSaleReturn',
                component: defineAsyncComponent(() => import('../components/SaleReturn/AddSaleReturn.vue'))
            },

            {
                path: '/ServiceSaleReturn',
                name: 'ServiceSaleReturn',
                component:  defineAsyncComponent(() => import('../components/SaleServiceReturn/ServiceSaleReturn.vue'))
            },
            {
                path: '/AddServiceSaleReturn',
                name: 'AddServiceSaleReturn',
                component:  defineAsyncComponent(() => import('../components/SaleServiceReturn/AddServiceSaleReturn.vue'))
            },
            {
                path: '/ViewSaleReturn',
                name: 'ViewSaleReturn',
                component:  defineAsyncComponent(() => import('../components/SaleReturn/ViewSaleReturn.vue'))
            },

            {
                path: '/WareHouseTransfer',
                name: 'WareHouseTransfer',
                component: defineAsyncComponent(() => import('../components/WareHouseTransfer/WareHouseTransfer.vue'))
            },
            {
                path: '/AddWareHouseTransfer',
                name: 'AddWareHouseTransfer',
                component: defineAsyncComponent(() => import('../components/WareHouseTransfer/AddWareHouseTransfer.vue'))
            },
            {
                path: '/AddRoles',
                name: 'AddRoles',
                component:  defineAsyncComponent(() => import('../components/UserRoles/AddRoles.vue'))
            },

            {
                path: '/AddPermissions',
                name: 'AddPermissions',
                component: defineAsyncComponent(() => import('../components/UserPermissions/AddPermissions.vue'))
            },
            {
                path: '/ImportCategory',
                name: 'ImportCategory',
                component: defineAsyncComponent(() => import('../components/Imports/ImportCategory.vue'))
            },
            {
                path: '/ImportProduct',
                name: 'ImportProduct',
                component: defineAsyncComponent(() => import('../components/Imports/ImportProduct.vue'))
            },
            {
                path: '/ImportExportRecords',
                name: 'ImportExportRecords',
                component: defineAsyncComponent(() => import('../components/Imports/ImportExportRecords.vue'))
            },
            {
                path: '/ImportStock',
                name: 'ImportStock',
                component: defineAsyncComponent(() => import('../components/Imports/ImportStockIn.vue'))
            },
            {
                path: '/AddSaleService',
                name: 'AddSaleService',
                component: defineAsyncComponent(() => import('../components/SaleServiceInvoice/AddSaleService.vue')),
                props(route) {
                    return { formName: route.query.formName };
                },
            },
            {
                path: '/SaleService',
                name: 'SaleService',
                component: defineAsyncComponent(() => import('../components/SaleServiceInvoice/SaleService.vue')),
                props(route) {
                    return { formName: route.query.formName };
                },
            },
            {
                path: '/CustomerGroup',
                name: 'CustomerGroup',
                component: defineAsyncComponent(() => import('../components/CustomerGroup/CustomerGroup.vue'))
            },
            {
                path: '/AddServiceQuotation',
                name: 'AddServiceQuotation',
                component: defineAsyncComponent(() => import('../components/QuotationService/AddServiceQuotation.vue')),
            },
            {
                path: '/ServiceQuotation',
                name: 'ServiceQuotation',
                props(route) {
                    return { formName: route.query.formName };
                },
                component: defineAsyncComponent(() => import('../components/QuotationService/ServiceQuotation.vue')),
            },
            {
                path: '/DeliveryChallan',
                name: 'DeliveryChallan',
                component: defineAsyncComponent(() => import('../components/DeliveryChallan/DeliveryChallan.vue')),
            },
            {
                path: '/TemporaryCashRequest',
                name: 'TemporaryCashRequest',
                component: defineAsyncComponent(() => import('../components/TemporaryCashRequest/TemporaryCashRequest.vue')),
            },
            {
                path: '/AddTemporaryCashRequest',
                name: 'AddTemporaryCashRequest',
                component: defineAsyncComponent(() => import('../components/TemporaryCashRequest/AddTemporaryCashRequest.vue')),
            },
            {
                path: '/AddTemporaryCashAllocation',
                name: 'AddTemporaryCashAllocation',
                component: defineAsyncComponent(() => import('../components/TemporaryCashAllocation/AddTemporaryCashAllocation.vue')),
            },
            {
                path: '/TemporaryCashAllocation',
                name: 'TemporaryCashAllocation',
                component: defineAsyncComponent(() => import('../components/TemporaryCashAllocation/TemporaryCashAllocation.vue')),
            },
            {
                path: '/TemporaryCashIssue',
                name: 'TemporaryCashIssue',
                component: defineAsyncComponent(() => import('../components/TemporaryCashIssue/TemporaryCashIssue.vue')),
            },
            {
                path: '/AddTemporaryCashIssue',
                name: 'AddTemporaryCashIssue',
                component: defineAsyncComponent(() => import('../components/TemporaryCashIssue/AddTemporaryCashIssue.vue')),
            },
            {
                path: '/MonthlySalesComparisionReport',
                name: 'MonthlySalesComparisionReport',
                component: defineAsyncComponent(() => import('../components/Reports/MonthlySalesComparisionReport.vue'))
            },
            {
                path: '/StockReport',
                name: 'StockReport',
                component: defineAsyncComponent(() => import('../components/Reports/StockReport.vue'))
            },
            {
                path: '/Email',
                name: 'Email',
                component: defineAsyncComponent(() => import('../components/Email/Email.vue'))
            },
            {
                path: '/InvoiceReport',
                name: 'InvoiceReport',
                props(route) {
                    return { formName: route.query.formName };
                },
                component:  defineAsyncComponent(() => import('../components/Reports/InvoiceReport.vue'))
            },
            {
                path: '/SaleMonthWiseReport',
                name: 'SaleMonthWiseReport',
                props(route) {
                    return { formName: route.query.formName };
                },
                component: defineAsyncComponent(() => import('../components/Reports/SaleMonthWiseReport.vue'))
            },
            {
                path: '/CustomerAdvancesReport',
                name: 'CustomerAdvancesReport',
                props(route) {
                    return { formName: route.query.formName };
                },
                component: defineAsyncComponent(() => import('../components/Reports/CustomerAdvancesReport.vue'))
            },
            {
                path: '/CustomerAdvanceSummaryReport',
                name: 'CustomerAdvanceSummaryReport',
                props(route) {
                    return { formName: route.query.formName };
                },
                component: defineAsyncComponent(() => import('../components/Reports/CustomerAdvanceSummaryReport.vue'))
            },

            {
                path: '/DailyExpenseReport',
                name: 'DailyExpenseReport',
                component: defineAsyncComponent(() => import('../components/Reports/DailyExpenseReport.vue'))
            },
            {
                path: '/Bom',
                name: 'Bom',
                component: defineAsyncComponent(() => import('../components/Bom/Bom.vue'))
            },
            {
                path: '/AddBom',
                name: 'AddBom',
                component: defineAsyncComponent(() => import('../components/Bom/AddBom.vue'))
            },
            {
                path: '/MonthlySalesReport',
                name: 'MonthlySalesReport',
                props(route) {
                    return { formName: route.query.formName };
                },
                component: defineAsyncComponent(() => import('../components/Reports/MonthlySalesReport.vue'))
            },
            {
                path: '/InventoryFilterReport',
                name: 'InventoryFilterReport',
                component: defineAsyncComponent(() => import('../components/Reports/InventoryFilterReport.vue'))
            },
            {
                path: '/Process',
                name: 'Process',
                component: defineAsyncComponent(() => import('../components/ProductionModule/Process/Process.vue'))
            },
            {
                path: '/AddProcess',
                name: 'AddProcess',
                component: defineAsyncComponent(() => import('../components/ProductionModule/Process/AddProcess.vue'))
            },
            {
                path: '/MutlipleProductBarcodePrinting',
                name: 'MutlipleProductBarcodePrinting',
                component: defineAsyncComponent(() => import('../components/BarcodePrinting/MutlipleProductBarcodePrinting.vue'))
            },
            {
                path: '/AddBarCodeSetup',
                name: 'AddBarCodeSetup',
                component: defineAsyncComponent(() => import('../components/BarCodeSetup/AddBarCodeSetup.vue'))
            },
            {
                path: '/ProductInventoryReport',
                name: 'ProductInventoryReport',
                component: defineAsyncComponent(() => import('../components/Reports/ProductInventoryReport.vue'))
            },
            {
                path: '/TrialBalanceReport',
                name: 'TrialBalanceReport',
                component: defineAsyncComponent(() => import('../components/Reports/TrialBalanceReport.vue'))
            },
            {
                path: '/TrialBalanceAccountReport',
                name: 'TrialBalanceAccountReport',
                component: defineAsyncComponent(() => import('../components/Reports/TrialBalanceAccountReport.vue'))
            },
            {
                path: '/TrialBalanceTreeReport',
                name: 'TrialBalanceTreeReport',
                component: defineAsyncComponent(() => import('../components/Reports/TrialBalanceTreeReport.vue'))
            },
            {
                path: '/BalanceSheetReport',
                name: 'BalanceSheetReport',
                component: defineAsyncComponent(() => import('../components/Reports/BalanceSheetReport.vue'))
            },
            {
                path: '/AdvanceBalanceSheetReport',
                name: 'AdvanceBalanceSheetReport',
                component: defineAsyncComponent(() => import('../components/Reports/MonthlyAdvanceBalanceSheetReport.vue'))
            },
            {
                path: '/MonthlyAdvanceIcomeStatementReport',
                name: 'MonthlyAdvanceIcomeStatementReport',
                component: defineAsyncComponent(() => import('../components/Reports/MonthlyAdvanceIcomeStatementReport.vue'))
            },
            {
                path: '/MonthlyAdvanceIncomeStatementReport',
                name: 'MonthlyAdvanceIncomeStatementReport',
                component: defineAsyncComponent(() => import('../components/Reports/MonthlyAdvanceIcomeStatementReport.vue'))
            },
            {
                path: '/IncomeStatementReport',
                name: 'IncomeStatementReport',
                component: defineAsyncComponent(() => import('../components/Reports/IncomeStatementReport.vue'))
            },
            {
                path: '/CustomerLedgerReport',
                name: 'CustomerLedgerReport',
                props(route) {
                    return { formName: route.query.formName };
                },
                component: defineAsyncComponent(() => import('../components/Reports/CustomerLedgerReport.vue')),


            },
            {
                path: '/VatPayableReport',
                name: 'VatPayableReport',
                component: defineAsyncComponent(() => import('../components/Reports/VatPayableReport.vue'))
            },
            {
                path: '/VatReturnReport',
                name: 'VatReturnReport',
                props(route) {
                    return { formName: route.query.formName };
                },
                component: defineAsyncComponent(() => import('../components/Reports/VatReturnReport.vue'))
            },
            {
                path: '/VatMonthWiseReport',
                name: 'VatMonthWiseReport',
                component: defineAsyncComponent(() => import('../components/Reports/VatMonthWiseReport.vue'))
            },
            {
                path: '/DateWiseTransaction',
                name: 'DateWiseTransaction',
                component: defineAsyncComponent(() => import('../components/Reports/DaywisetransactionReport.vue'))
            },

            {
                path: '/ProductStockValue',
                name: 'ProductStockValue',
                component:  defineAsyncComponent(() => import('../components/Reports/ProductStockValue.vue'))
            },
            {
                path: '/SupplierPurchaseReport',
                name: 'SupplierPurchaseReport',
                component: defineAsyncComponent(() => import('../components/Reports/SupplierPurchaseReport.vue'))
            },
            {
                path: '/ProductComparisonReport',
                name: 'ProductComparisonReport',
                component: defineAsyncComponent(() => import('../components/Reports/ProductComparisonReport.vue'))
            },
            {
                path: '/ProductLedgerReport',
                name: 'ProductLedgerReport',
                component:  defineAsyncComponent(() => import('../components/Reports/ProductLedgerReport.vue'))
            },
            {
                path: '/InvoiceSerialReport',
                name: 'InvoiceSerialReport',
                component:  defineAsyncComponent(() => import('../components/Reports/InvoiceSerialReport.vue'))
            },
            {
                path: '/ProductStockValueTransactionType',
                name: 'ProductStockValueTransactionType',
                component:  defineAsyncComponent(() => import('../components/Reports/ProductStockValueTransactionType.vue'))
            },
            {
                path: '/ProductStockAvgValue',
                name: 'ProductStockAvgValue',
                component: defineAsyncComponent(() => import('../components/Reports/ProductStockAvgValue.vue'))
            },
            {
                path: '/CustomerWiseProductSaleReport',
                name: 'CustomerWiseProductSaleReport',
                component:  defineAsyncComponent(() => import('../components/Reports/CustomerWiseProductSaleReport.vue'))
            },
            {
                path: '/CustomersWiseProductSaleReport',
                name: 'CustomersWiseProductSaleReport',
                component: defineAsyncComponent(() => import('../components/Reports/CustomersWiseProductSaleReport.vue'))
            },
            {
                path: '/SupplierWiseProductPurchaseReport',
                name: 'SupplierWiseProductPurchaseReport',
                component: defineAsyncComponent(() => import('../components/Reports/SupplierWiseProductPurchaseReport.vue')),
            },
            {
                path: '/SuppliersWiseProductPurchaseReport',
                name: 'SuppliersWiseProductPurchaseReport',
                component: defineAsyncComponent(() => import('../components/Reports/SuppliersWiseProductPurchaseReport.vue'))
            },
            {
                path: '/CustomerDiscountProducts',
                name: 'CustomerDiscountProducts',
                component: defineAsyncComponent(() => import('../components/Reports/CustomerDiscountProducts.vue'))
            },
            {
                path: '/ProductDiscountCustomers',
                name: 'ProductDiscountCustomers',
                component: defineAsyncComponent(() => import('../components/Reports/ProductDiscountCustomers.vue'))
            },
            {
                path: '/ProductDiscountSuppliers',
                name: 'ProductDiscountSuppliers',
                component: defineAsyncComponent(() => import('../components/Reports/ProductDiscountSuppliers.vue'))
            },
            {
                path: '/FreeofCostPurchase',
                name: 'FreeofCostPurchase',
                component: defineAsyncComponent(() => import('../components/Reports/FreeofCostPurchase.vue'))
            },
            {
                path: '/FreeofCostSale',
                name: 'FreeofCostSale',
                component:  defineAsyncComponent(() => import('../components/Reports/FreeofCostSale.vue'))
            },
            {
                path: '/AccountLedger',
                name: 'AccountLedger',
                component: defineAsyncComponent(() => import('../components/Reports/AccountLedger.vue'))
            },
            {
                path: '/AccountLedgerCostCenterWise',
                name: 'AccountLedgerCostCenterWise',
                component: defineAsyncComponent(() => import('../components/Reports/AccountLedgerCostCenterWise.vue'))
            },
            {
                path: '/BankTransactionReport',
                name: 'BankTransactionReport',
                component:  defineAsyncComponent(() => import('../components/Reports/BankTransactionReport.vue'))
            },
            {
                path: '/CustomerBalanceReport',
                name: 'CustomerBalanceReport',
                props(route) {
                    return { formName: route.query.formName };
                },
                component:  defineAsyncComponent(() => import('../components/Reports/CustomerBalanceReport.vue'))
            },

            {
                path: '/DayStartReport',
                name: 'DayStartReport',
                component: defineAsyncComponent(() => import('../components/Reports/DayStartWiseReport.vue')),
            }
            ,
            {
                path: '/VatExpenseReport',
                name: 'VatExpenseReport',
                component: defineAsyncComponent(() => import('../components/Reports/VatExpenseReport.vue')),
            },
            {
                path: '/AdvanceTrialBalance',
                name: 'AdvanceTrialBalance',
                component: defineAsyncComponent(() => import('../components/Reports/AdvanceTrialBalanceReport.vue')),
            },
            {
                path: '/AdvanceTrialBalanceAccount',
                name: 'AdvanceTrialBalanceAccount',
                component: defineAsyncComponent(() => import('../components/Reports/AdvanceTrialBalanceAccountReport.vue')),
            },
            {
                path: '/AdvanceAccountLedger',
                name: 'AdvanceAccountLedger',
                component: defineAsyncComponent(() => import('../components/Reports/AdvanceAccountLedger.vue')),
            },
            {
                path: '/AdvanceIInventoryItem',
                name: 'AdvanceIInventoryItem',
                component: defineAsyncComponent(() => import('../components/Reports/AdvanceIInventoryItemReport.vue')),
            },
            {
                path: '/AdvanceQuantityWiseInventoryItem',
                name: 'AdvanceQuantityWiseInventoryItem',
                component: defineAsyncComponent(() => import('../components/Reports/AdvanceQuantityWiseInventoryItemReport.vue')),
            },
            {
                path: '/AdvanceStockReport',
                name: 'AdvanceStockReport',
                component: defineAsyncComponent(() => import('../components/Reports/AdvanceStockReport.vue')),
            },
            {
                path: '/stockrequest',
                name: 'stockrequest',
                component: defineAsyncComponent(() => import('../components/StockRequest/StockRequest.vue')),
            },
            {
                path: '/addstocktransfer',
                name: 'addstocktransfer',
                component: defineAsyncComponent(() => import('../components/StockTransfer/AddStockTransfer.vue')),
            },
            {
                path: '/stocktransfer',
                name: 'stocktransfer',
                component: defineAsyncComponent(() => import('../components/StockTransfer/StockTransfer.vue')),
            },
            {
                path: '/stockreceived',
                name: 'stockreceived',
                component: defineAsyncComponent(() => import('../components/StockReceived/StockReceived.vue')),
            },
            {
                path: '/addstockreceived',
                name: 'addstockreceived',
                component: defineAsyncComponent(() => import('../components/StockReceived/AddStockReceived.vue')),
            },
            {
                path: '/addstockrequest',
                name: 'addstockrequest',
                component: defineAsyncComponent(() => import('../components/StockRequest/AddStockRequest.vue')),
            },
            {
                path: '/AdvanceSaleInvoice',
                name: 'AdvanceSaleInvoice',
                component: defineAsyncComponent(() => import('../components/Reports/AdvanceSaleInvoiceReport.vue')),
            },
            {
                path: '/SaleInvoiceSummary',
                name: 'SaleInvoiceSummary',
                component: defineAsyncComponent(() => import('../components/Reports/SaleInvoiceSummaryReport.vue')),
            },
            {
                path: '/PurchaseInvoiceSummary',
                name: 'PurchaseInvoiceSummary',
                component: defineAsyncComponent(() => import('../components/Reports/PurchaseInvoiceSummaryReport.vue')),
            },
            {
                path: '/ProductSummary',
                name: 'ProductSummary',
                component: defineAsyncComponent(() => import('../components/Reports/ProductSummaryReport.vue')),
            },
            {
                path: '/AdvanceCustomerLedger',
                name: 'AdvanceCustomerLedger',
                props(route) {
                    return { formName: route.query.formName };
                },
                component: defineAsyncComponent(() => import('../components/Reports/AdvanceCustomerLedgerReport.vue')),


            },
            {
                path: '/AdvanceAccountLedgerCostCenterWise',
                name: 'AdvanceAccountLedgerCostCenterWise',
                component: defineAsyncComponent(() => import('../components/Reports/AdvanceAccountLedgerCostCenterWise.vue')),
            },
            {
                path: '/ExpenseType',
                name: 'ExpenseType',
                component: defineAsyncComponent(() => import('../components/ExpenseType/ExpenseType.vue')),
            },
            {
                path: '/BranchesPrefix',
                name: 'BranchesPrefix',
                component: defineAsyncComponent(() => import('../components/Prefixes/BranchesPrefix.vue'))
            },
            {
                path: '/TransactionLogs',
                name: 'TransactionLogs',
                component: defineAsyncComponent(() => import('../components/TransactionLogs/TransactionLogs.vue'))
            },
            {
                path: '/PushPullTransactionLog',
                name: 'PushPullTransactionLog',
                component:  defineAsyncComponent(() => import('../components/PushPullTransactionLog/PushPullTransactionLog.vue'))
            },
            {
                path: '/TemporaryCashAllocationReport',
                name: 'TemporaryCashAllocationReport',
                component: defineAsyncComponent(() => import('../components/TemporaryCashAllocation/TemporaryCashAllocationReport.vue')),
            }
            ,
            {
                path: '/SalesDashboardReport',
                name: 'SalesDashboardReport',
                component: defineAsyncComponent(() => import('../components/Reports/SalesDashboardReport.vue')),
            },
            {
                path: '/ProductMaster',
                name: 'ProductMaster',
                component: defineAsyncComponent(() => import('../components/ProductMaster/ProductMaster.vue')),
            },
            {
                path: '/category',
                name: 'Category',
                component: defineAsyncComponent(() => import('../components/Category/Category.vue'))
            },
            {
                path: '/additemviewsetup',
                name: 'additemviewsetup',
                component: defineAsyncComponent(() => import('../components/ItemViewSetup/AddItemViewSetup.vue'))
            },
            {
                path: '/PriceLabeling',
                name: 'PriceLabeling',
                component:  defineAsyncComponent(() => import('../components/PriceLabeling/PriceLabeling.vue'))
            },
            {
                path: '/PriceRecord',
                name: 'PriceRecord',
                component: defineAsyncComponent(() => import('../components/PriceRecord/PriceRecord.vue'))
            },
            {
                path: '/PriceRecordChange',
                name: 'PriceRecordChange',
                component:  defineAsyncComponent(() => import('../components/PriceRecordChange/PriceRecordChange.vue'))
            },
            {
                path: '/subcategory',
                name: 'SubCategory',
                component: defineAsyncComponent(() => import('../components/SubCategory/SubCategory.vue'))
            },
            {
                path: '/brand',
                name: 'Brand',
                component:  defineAsyncComponent(() => import('../components/Brand/Brand.vue'))
            },
            {
                path: '/unit',
                name: 'Unit',
                component: defineAsyncComponent(() => import('../components/Unit/Unit.vue'))
            },
            {
                path: '/color',
                name: 'Color',
                component: defineAsyncComponent(() => import('../components/Color/Color.vue'))
            },
            {
                path: '/HolidaySetup',
                name: 'HolidaySetup',
                component: defineAsyncComponent(() => import('../components/Hr/ShiftManagement/HolidaySetup.vue'))
            },
            {
                path: '/size',
                name: 'Size',
                component: defineAsyncComponent(() => import('../components/Size/Size.vue'))
            },
            {
                path: '/origin',
                name: 'Origin',
                component: defineAsyncComponent(() => import('../components/Origin/Origin.vue'))
            },
            {
                path: '/Backup',
                name: 'Backup',
                component: defineAsyncComponent(() => import('../components/DatabaseBackup/Backup.vue'))
            },
            {
                path: '/Restore',
                name: 'Restore',
                component: defineAsyncComponent(() => import('../components/BranchUsers/BranchUsers.vue')),
            },
            {
                path: '/CompanyProfile',
                name: 'CompanyProfile',
                component: defineAsyncComponent(() => import('../components/CompanyProfile/CompanyProfile.vue')),
            },
            {
                path: '/MultiBarcodePrinting',
                name: 'MultiBarcodePrinting',
                component:  defineAsyncComponent(() => import('../components/BarcodePrinting/MultiBarcodePrinting.vue'))
            },
            {
                path: '/RackBarcodeCreate',
                name: 'RackBarcodeCreate',
                component: defineAsyncComponent(() => import('../components/RacksBarcode/RackBarcodeCreate.vue')),
            },
            {
                path: '/purchase',
                name: 'purchase',
                component: defineAsyncComponent(() => import('../components/Purchase/Purchase.vue'))
            },
            {
                path: '/WarrantyType',
                name: 'WarrantyType',
                component: defineAsyncComponent(() => import('../components/warrantyType/WarrantyType.vue')),
            },
            {
                path: '/Roles',
                name: 'Roles',
                component: defineAsyncComponent(() => import('../components/UserRoles/Roles.vue'))
            },
            {
                path: '/signUp',
                name: 'SignUp',
                component:  defineAsyncComponent(() => import('../components/LoginCredentials/SignUp.vue'))
            },
            {
                path: '/AddLoginPermission',
                name: 'AddLoginPermission',
                component: defineAsyncComponent(() => import('../components/LoginPermission/AddLoginPermission.vue'))
            },

            {
                path: '/UserSetup',
                name: 'UserSetup',
                component: defineAsyncComponent(() => import('../components/SidebarMenu/UserSetup.vue')),
                children: [





                ],
            },

            {
                path: '/UserDefineFlow',
                name: 'UserDefineFlow',
                component: defineAsyncComponent(() => import('../components/UserDefineFlow/UserDefineFlow.vue')),
            },
            {
                path: '/Permissions',
                name: 'Permissions',
                component: defineAsyncComponent(() => import('../components/UserPermissions/Permissions.vue'))
            },
            {
                path: '/NoblePermissions',
                name: 'NoblePermissions',
                component: defineAsyncComponent(() => import('../components/UserPermissions/NoblePermission.vue'))
            },
            {
                path: '/currency',
                name: 'Currency',
                component: defineAsyncComponent(() => import('../components/Currency/Currency.vue'))
            },
            {
                path: '/Bank',
                name: 'Bank',
                component:  defineAsyncComponent(() => import('../components/Bank/Bank.vue'))
            },

            {
                path: '/monthlycost',
                name: 'MonthlyCost',
                component: defineAsyncComponent(() => import('../components/MonthlyCost/MonthlyCost.vue'))
            },
            {
                path: '/paymentOptions',
                name: 'PaymentOptions',
                component: defineAsyncComponent(() => import('../components/Payment_Options/PaymentOptions.vue'))
            },
            {
                path: '/DenominationSetup',
                name: 'DenominationSetup',
                component: defineAsyncComponent(() => import('../components/DenominationSetup/DenominationSetup.vue')),
            },
            {
                path: '/taxrate',
                name: 'TaxRate',
                component: defineAsyncComponent(() => import('../components/TaxRate/TaxRate.vue'))
            },

            {
                path: '/FinancialYear',
                name: 'FinancialYear',
                component: defineAsyncComponent(() => import('../components/FinancialYear/FinancialYear.vue')),
            },
            {
                path: '/FinancialYearClosing',
                name: 'FinancialYearClosing',
                component: defineAsyncComponent(() => import('../components/FinancialYear/FinancialYearClosing.vue')),
            },
            {
                path: '/FinancialYearClosingView',
                name: 'FinancialYearClosingView',
                component: defineAsyncComponent(() => import('../components/FinancialYear/FinancialYearClosingView.vue')),
            },
            {
                path: '/Deduction',
                name: 'Deduction',
                component: defineAsyncComponent(() => import('../components/Hr/Payroll/Deduction/Deduction.vue')),
            },
            {
                path: '/Shift',
                name: 'Shift',
                component: defineAsyncComponent(() => import('../components/Hr/Shift/Shift.vue')),
            },
            {
                path: '/Leave',
                name: 'Leave',
                component: defineAsyncComponent(() => import('../components/Hr/LeaveManagement/Leave.vue')),
            },
            {
                path: '/ShiftAssign',
                name: 'ShiftAssign',
                component: defineAsyncComponent(() => import('../components/Hr/ShiftAssign/ShiftAssign.vue')),
            },
            {
                path: '/AddShiftAssign',
                name: 'AddShiftAssign',
                component: defineAsyncComponent(() => import('../components/Hr/ShiftAssign/AddShiftAssign.vue')),
            },
            {
                path: '/Contribution',
                name: 'Contribution',
                component: defineAsyncComponent(() => import('../components/Hr/Payroll/Contribution/Contribution.vue')),
            },
            {
                path: '/Allowance',
                name: 'Allowance',
                component: defineAsyncComponent(() => import('../components/Hr/Payroll/Allowance/Allowance.vue')),
            },
            {
                path: '/SalaryTemplate',
                name: 'SalaryTemplate',
                component: defineAsyncComponent(() => import('../components/Hr/Payroll/SalaryTemplate/SalaryTemplate.vue')),
            },
            {
                path: '/AddSalaryTemplate',
                name: 'AddSalaryTemplate',
                component: defineAsyncComponent(() => import('../components/Hr/Payroll/SalaryTemplate/AddSalaryTemplate.vue')),
            },
            {
                path: '/AllowanceType',
                name: 'AllowanceType',
                component: defineAsyncComponent(() => import('../components/Hr/Payroll/AllowanceType/AllowanceType.vue')),
            },
            {
                path: '/AddEmployeeSalary',
                name: 'AddEmployeeSalary',
                component: defineAsyncComponent(() => import('../components/Hr/Payroll/EmployeeSalary/AddEmployeeSalary.vue')),
            },
            {
                path: '/EmployeeSalary',
                name: 'EmployeeSalary',
                component: defineAsyncComponent(() => import('../components/Hr/Payroll/EmployeeSalary/EmployeeSalary.vue')),
            },
            {
                path: '/LoanPayment',
                name: 'LoanPayment',
                component: defineAsyncComponent(() => import('../components/Hr/Payroll/LoanPayment/LoanPayment.vue')),
            },
            {
                path: '/PayrollSchedule',
                name: 'PayrollSchedule',
                component: defineAsyncComponent(() => import('../components/Hr/Payroll/PayrollSchedule/PayrollSchedule.vue')),
            },
            {
                path: '/AddLoanPayment',
                name: 'AddLoanPayment',
                component: defineAsyncComponent(() => import('../components/Hr/Payroll/LoanPayment/AddLoanPayment.vue')),
            },
            {
                path: '/SalaryTaxSlab',
                name: 'SalaryTaxSlab',
                component: defineAsyncComponent(() => import('../components/Hr/Payroll/SalaryTaxSlab/SalaryTaxSlab.vue')),
            },
            {
                path: '/AddSalaryTaxSlab',
                name: 'AddSalaryTaxSlab',
                component: defineAsyncComponent(() => import('../components/Hr/Payroll/SalaryTaxSlab/AddSalaryTaxSlab.vue')),
            },


            {
                path: '/ChequeAndGurantee',
                name: 'ChequeAndGurantee',
                component: defineAsyncComponent(() => import('../components/Bank/ChequeAndGurantee.vue')),
            },
            {
                path: '/ChequeAndGuranteeDashboard',
                name: 'ChequeAndGuranteeDashboard',
                component: defineAsyncComponent(() => import('../components/Bank/ChequeAndGuranteeDashboard.vue')),
            },
            {
                path: '/coa',
                name: 'coa',
                component: defineAsyncComponent(() => import('../components/COA/AddUpdateAccount.vue'))
            },
            {
                path: '/RunPayroll',
                name: 'RunPayroll',
                component: defineAsyncComponent(() => import('../components/Hr/Payroll/RunPayroll/RunPayroll.vue')),
            },
            {
                path: '/AddRunPayroll',
                name: 'AddRunPayroll',
                component: defineAsyncComponent(() => import('../components/Hr/Payroll/RunPayroll/AddRunPayroll.vue')),
            },
            {
                path: '/SaleOrder',
                name: 'SaleOrder',
                props(route) {
                    return { formName: route.query.formName };
                },
                component: defineAsyncComponent(() => import('../components/SaleOrder/SaleOrder.vue'))
            },
            {
                path: '/SaleOrderTrackingReport',
                name: 'SaleOrderTrackingReport',
                component: defineAsyncComponent(() => import('../components/SaleOrderTrackingReport/SaleOrderTrackingReport.vue'))
            },
            {
                path: '/AddSaleOrder',
                name: 'AddSaleOrder',
                props(route) {
                    return { formName: route.query.formName };
                },
                component: defineAsyncComponent(() => import('../components/SaleOrder/AddSaleOrder.vue'))
            },
            {
                path: '/SaleOrderView',
                name: 'SaleOrderView',
                props(route) {
                    return { formName: route.query.formName };
                },
                component: defineAsyncComponent(() => import('../components/SaleOrder/SaleOrderView.vue'))
            },
            {
                path: '/SyncData',
                name: 'SyncData',
                component: sync
            },
            {
                path: '/RecipeNo',
                name: 'RecipeNo',
                component:  defineAsyncComponent(() => import('../components/RecipeNo/RecipeNo.vue'))
            },
            {
                path: '/AddPurchaseBill',
                name: 'AddPurchaseBill',
                component: defineAsyncComponent(() => import('../components/PurchaseBill/AddPurchaseBill.vue'))
            }, {
                path: '/PurchaseBillView',
                name: 'PurchaseBillView',
                component: defineAsyncComponent(() => import('../components/PurchaseBill/PurchaseBillView.vue'))
            },
            {
                path: '/PurchaseBill',
                name: 'PurchaseBill',
                component: defineAsyncComponent(() => import('../components/PurchaseBill/PurchaseBill.vue'))
            },

            {
                path: '/AddRecipeNo',
                name: 'AddRecipeNo',
                component: defineAsyncComponent(() => import('../components/RecipeNo/AddRecipeNo.vue'))
            },
            {
                path: '/ViewRecipe',
                name: 'ViewRecipe',
                component: defineAsyncComponent(() => import('../components/RecipeNo/ViewRecipe.vue'))
            },
            {
                path: '/AddProductionBatch',
                name: 'AddProductionBatch',
                component:  defineAsyncComponent(() => import('../components/ProductionBatch/AddProductionBatch.vue'))
            },
            {
                path: '/ProductionBatch',
                name: 'ProductionBatch',
                component: defineAsyncComponent(() => import('../components/ProductionBatch/ProductionBatch.vue'))
            },
            {
                path: '/BatchView',
                name: 'BatchView',
                component: defineAsyncComponent(() => import('../components/ProductionBatch/BatchView.vue'))
            },
            {
                path: '/AddDispatchNote',
                name: 'AddDispatchNote',
                component: defineAsyncComponent(() => import('../components/DispatchNote/AddDispatchNote.vue'))
            },
            {
                path: '/DispatchNotes',
                name: 'DispatchNotes',
                component: defineAsyncComponent(() => import('../components/DispatchNote/DispatchNotes.vue'))
            },
            {
                path: '/SampleRequest',
                name: 'SampleRequest',
                component: defineAsyncComponent(() => import('../components/ProductionModule/SampleRequest/SampleRequest.vue')),
            },
            {
                path: '/AddSampleRequest',
                name: 'AddSampleRequest',
                component: defineAsyncComponent(() => import('../components/ProductionModule/SampleRequest/AddSampleRequest.vue')),
            },
            {
                path: '/ProcessBatchScreen',
                name: 'ProcessBatchScreen',
                component: defineAsyncComponent(() => import('../components/ProductionBatch/ProcessBatchScreen.vue'))
            },
            {
                path: '/Process',
                name: 'Process',
                component: defineAsyncComponent(() => import('../components/ProductionModule/Process/Process.vue')),
            },
            {
                path: '/AddProcess',
                name: 'AddProcess',
                component: defineAsyncComponent(() => import('../components/ProductionModule/Process/AddProcess.vue')),
            },
            {
                path: '/StartScreen',
                name: 'StartScreen',
                component: defineAsyncComponent(() => import('../components/UserScreen/StartScreen.vue')),
            },
            {
                path: '/NotPermission',
                name: 'NotPermission',
                component: defineAsyncComponent(() => import('../components/UserScreen/NotPermission.vue')),
            },
            {
                path: '/FlushDatabase',
                name: 'FlushDatabase',
                component: defineAsyncComponent(() => import('../components/FlushDatabase/FlushDatabase.vue')),
            },
            {
                path: '/GeographicalSetup',
                name: 'GeographicalSetup',
                component: defineAsyncComponent(() => import('../components/SidebarMenu/GeographicalSetup.vue')),
                children: [
                    {
                        path: '/City',
                        name: 'City',
                        component: defineAsyncComponent(() => import('../components/City/City.vue'))
                    },

                ]
            },
            {
                path: '/Region',
                name: 'Region',
                component: defineAsyncComponent(() => import('../components/Region/Region.vue'))
            },
            {
                path: '/ImportExportSetup',
                name: 'ImportExportSetup',
                component: defineAsyncComponent(() => import('../components/SidebarMenu/ImportExportSetup.vue')),
                children: [
                    {
                        path: '/ImportExport',
                        props(route) {
                            return { formName: route.query.formName }
                        },
                        name: 'ImportExport',
                        component: defineAsyncComponent(() => import('../components/ImportExport/ImportExport.vue'))
                    }

                ],
            },
            {
                path: '/ReparingOrderType',
                name: 'ReparingOrderType',
                props(route) {

                    return { formName: route.query.formName };
                },
                component: defineAsyncComponent(() => import('../components/ReparingOrderType/ReparingOrderType.vue')),

            },
            {
                path: '/BarcodeSetup',
                name: 'BarcodeSetup',
                component: defineAsyncComponent(() => import('../components/SidebarMenu/BarcodeSetup.vue')),
                children: [

                ],
            },
            {
                path: '/PushRecords',
                name: 'PushRecords',
                component: defineAsyncComponent(() => import('../components/PushRecords/PushRecords.vue')),
            },
            {
                path: '/PullRecords',
                name: 'PullRecords',
                component: defineAsyncComponent(() => import('../components/PullRecords/PullRecords.vue')),
            },

            {
                path: '/Upload',
                name: 'Upload',
                component: defineAsyncComponent(() => import('../components/Upload/Upload.vue')),
            },

            {
                path: '/Synchronization',
                name: 'Synchronization',
                component:  defineAsyncComponent(() => import('../components/SidebarMenu/Synchronization.vue')),
                children: [



                ],
            },
            {
                path: '/InquiryProcess',
                name: 'InquiryProcess',
                component: defineAsyncComponent(() => import('../components/InquiryProcess/InquiryProcess.vue')),

            },
            {
                path: '/InquiryModule',
                name: 'InquiryModule',
                component: defineAsyncComponent(() => import('../components/InquiryModule/InquiryModule.vue')),

            },
            {
                path: '/InquiryType',
                name: 'InquiryType',
                component: defineAsyncComponent(() => import('../components/InquiryType/InquiryType.vue')),
            },
            {
                path: '/InquirySetup',
                name: 'InquirySetup',
                component: defineAsyncComponent(() => import('../components/SidebarMenu/InquirySetup.vue')),

            },

            {
                path: '/BackupAndRestore',
                name: 'BackupAndRestore',
                component: defineAsyncComponent(() => import('../components/SidebarMenu/Backup.vue')),
                children: [



                ],
            },
            {
                path: '/PrintSetting',
                name: 'PrintSetting',
                component: defineAsyncComponent(() => import('../components/PrintSetting/PrintSetting.vue'))
            },
            {
                path: '/PurchaseReturnView',
                name: 'PurchaseReturnView',
                component: defineAsyncComponent(() => import('../components/PurchaseReturn/PurchaseReturnView.vue'))
            },
            {
                path: '/PurchaseOrderView',
                name: 'PurchaseOrderView',
                component: defineAsyncComponent(() => import('../components/PurchaseOrder/PurchaseOrderView.vue'))
            },
            {
                path: '/AddListSettingSetup',
                name: 'AddListSettingSetup',
                component: defineAsyncComponent(() => import('../components/ListSettings/AddListSettingSetup.vue'))
            },
            {
                path: '/ItemsBarCode',
                name: 'ItemsBarCode',
                component: defineAsyncComponent(() => import('../components/ItemsBarCode/ItemsBarCode.vue'))
            },

            {
                path: '/ProductionBatchView',
                name: 'ProductionBatchView',
                component: defineAsyncComponent(() => import('../components/ProductionBatch/ProductionBatchView.vue')),
            },
            {
                path: '/ProductView',
                name: 'ProductView',
                component: defineAsyncComponent(() => import('../components/Product/ProductView.vue')),
            },
            {
                path: '/PurchaseReturnHistory',
                name: 'PurchaseReturnHistory',
                component: defineAsyncComponent(() => import('../components/PurchaseReturn/PurchaseReturnHistory.vue')),
            },

            {
                path: '/InventoryBlind',
                name: 'InventoryBlind',
                component: defineAsyncComponent(() => import('../components/InventoryBlind/AddInventoryBlind.vue')),
            },
            {
                path: '/InventoryBlindList',
                name: 'InventoryBlindList',
                component: defineAsyncComponent(() => import('../components/InventoryBlind/InventoryBlind.vue')),
            },
            {
                path: '/BankPosTerminal',
                name: 'BankPosTerminal',
                component: defineAsyncComponent(() => import('../components/BankPosTerminal/BankPosTerminal.vue')),
            },
            {
                path: '/RackBarcodeCreate',
                name: 'RackBarcodeCreate',
                component: defineAsyncComponent(() => import('../components/RacksBarcode/RackBarcodeCreate.vue')),
            },




            {
                path: '/AddQuotation',
                name: 'AddQuotation',
                component: defineAsyncComponent(() => import('../components/Quotation/AddQuotation.vue'))
            },
            {
                path: '/Quotation',
                name: 'Quotation',
                component: defineAsyncComponent(() => import('../components/Quotation/Quotation.vue'))
            },
            {
                path: '/LogisticsList',
                name: 'LogisticsList',
                props(route) {
                    return { formName: route.query.formName };
                },
                component: defineAsyncComponent(() => import('../components/Logistics/LogisticsList.vue')),
            },
            {
                path: '/AddLogistics',
                name: 'AddLogistics',
                props(route) {
                    return { formName: route.query.formName };
                },
                component: defineAsyncComponent(() => import('../components/Logistics/AddLogistics.vue')),
            },
            {
                path: '/ItemRules',
                name: 'ItemRules',
                component: defineAsyncComponent(() => import('../components/Rules/ItemRules.vue')),

            },
            {
                path: '/CompanyProcess',
                name: 'CompanyProcess',
                component:  defineAsyncComponent(() => import('../components/CompanyProcess/CompanyProcessList.vue'))
            },

            {
                path: '/ImportContact',
                name: 'ImportContact',
                component:  defineAsyncComponent(() => import('../components/Imports/ImportContact.vue'))

            },
            {
                path: '/ImportReparingOrder',
                name: 'ImportReparingOrder',
                component: defineAsyncComponent(() => import('../components/Imports/ImportReparingOrder.vue'))

            },
            {
                path: '/ApplicationUpdate',
                name: 'ApplicationUpdate',
                component:  defineAsyncComponent(() => import('../components/ApplicationUpdate/UpdateApplication.vue'))
            },


            {
                path: '/PurchaseCosting',
                name: 'PurchaseCosting',
                component: defineAsyncComponent(() => import('../components/PurchaseCosting/AddPurchaseCosting.vue')),

            },
            {
                path: '/FinancialYear',
                name: 'FinancialYear',
                component: defineAsyncComponent(() => import('../components/FinancialYear/FinancialYear.vue')),
            },
            {
                path: '/MedicalReport',
                name: 'MedicalReport',
                component:  defineAsyncComponent(() => import('../components/Reports/MedicalReport.vue'))
            },
            {
                path: '/RetailReport',
                name: 'RetailReport',
                component:  defineAsyncComponent(() => import('../components/Reports/RetailReport.vue'))
            },
            {
                path: '/CompanyOption',
                name: 'CompanyOption',
                component: defineAsyncComponent(() => import('../components/CompanyOptions/CompanyOption.vue')),
            },
            {
                path: '/Department',
                name: 'Department',
                component: defineAsyncComponent(() => import('../components/Hr/Employee/Department/Department.vue')),
            },
            {
                path: '/Designation',
                name: 'Designation',
                component: defineAsyncComponent(() => import('../components/Hr/Employee/Designation/Designation.vue')),
            },
            {
                path: '/aboutUs',
                name: 'aboutUs',
                component: defineAsyncComponent(() => import('../components/Company/AboutUs.vue'))
            },
            {
                path: '/AddInquiry',
                name: 'AddInquiry',
                component: defineAsyncComponent(() => import('../components/Inquiry/AddInquiry.vue')),
            },
            {
                path: '/Inquiry',
                name: 'Inquiry',
                component: defineAsyncComponent(() => import('../components/Inquiry/Inquiry.vue')),
            },
            {
                path: '/ManualAttendance',
                name: 'ManualAttendance',
                component: defineAsyncComponent(() => import('../components/ManualAttendance/ManualAttendance.vue')),
            },
            {
                path: '/AddHolidayOfMonth',
                name: 'AddHolidayOfMonth',
                component: defineAsyncComponent(() => import('../components/ManualAttendance/AddHolidayOfMonth.vue')),
            },
            {
                path: '/EmployeeHourOfAttendence',
                name: 'EmployeeHourOfAttendence',
                component: defineAsyncComponent(() => import('../components/ManualAttendance/EmployeeHourOfAttendence.vue')),
            },
            {
                path: '/AttendanceReport',
                name: 'AttendanceReport',
                component: defineAsyncComponent(() => import('../components/ManualAttendance/AttendanceReport.vue')),
            },
            {
                path: '/AttendanceReport2',
                name: 'AttendanceReport2',
                component: defineAsyncComponent(() => import('../components/ManualAttendance/AttendanceReport2.vue')),
            },
            {
                path: '/AddProformaInvoice',
                name: 'AddProformaInvoice',
                component: defineAsyncComponent(() => import('../components/ProformaInvoice/AddProformaInvoice.vue')),
            },
            {
                path: '/AddServiceProformaInvoice',
                name: 'AddServiceProformaInvoice',
                component: defineAsyncComponent(() => import('../components/ProformaInvoice/AddServiceProformaInvoice.vue')),
            },
            {
                path: '/ProformaInvoices',
                name: 'ProformaInvoices',
                component: defineAsyncComponent(() => import('../components/ProformaInvoice/ProformaInvoices.vue')),
            },
            {
                path: '/ServiceProformaInvoice',
                name: 'ServiceProformaInvoice',
                props(route) {
                    return { formName: route.query.formName };
                },
                component: defineAsyncComponent(() => import('../components/ProformaInvoice/ServiceProformaInvoice.vue')),
            },
            {
                path: '/SupplierDetail',
                name: 'SupplierDetail',
                component: defineAsyncComponent(() => import('../components/Supplier/SupplierDetail.vue'))
            },
            {
                path: '/CommingSoon',
                name: 'CommingSoon',
                component: CommingSoon
            },
            {
                path: '/SaleVerificationReport',
                name: 'SaleVerificationReport',
                component: defineAsyncComponent(() => import('../components/SaleReport/SaleVerificationReport.vue')),
            },
            {
                path: '/ProductGroup',
                name: 'ProductGroup',
                component:  defineAsyncComponent(() => import('../components/ProductGroup/ProductGroup.vue'))
            },
            {
                path: '/CreditNote',
                name: 'CreditNote',
                component: defineAsyncComponent(() => import('../components/CreditNote/CreditNote.vue')),
                props(route) {
                    return { formName: route.query.formName };
                },
            },
            {
                path: '/AddCreditNote',
                name: 'AddCreditNote',
                component: defineAsyncComponent(() => import('../components/CreditNote/AddCreditNote.vue')),
                props(route) {
                    return { formName: route.query.formName };
                },
            },
            {
                path: '/InvoiceDefault',
                name: 'InvoiceDefault',
                component: defineAsyncComponent(() => import('../components/InvoiceDefault/InvoiceDefault.vue')),
            },
        ]
    }
];


const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
//   stringifyQuery: (query) => stringifyQuery(query),
  routes
})

export default router

//function greet() {
//    $('.metismenu').metisMenu();
//}

router.beforeEach((to, from, next) => {
    
    let recaptchaScript = document.createElement('script')
    recaptchaScript.setAttribute('src', '/assets/js/app.js')
    document.head.appendChild(recaptchaScript);


    if (to.query.fromDashboard === 'true') {
        localStorage.setItem('token', localStorage.getItem(to.query.token_name))


    }
    localStorage.setItem('CurrentPath', to.name);

    if (to.name == "Login") {
        next();
    }
    else {
        const mainStore = useMainStore();
        if (!mainStore.isLockOut) {
            next({ path: '/LockScreen' });
                      
        }
       else if (mainStore.isAuthenticated) {
            next();
                      
        }
        else {
            next({ path: '/' });
        }
    }
    
});
