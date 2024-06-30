<template>
    <div v-if="isValid('CanViewStockReport')  || isValid('CanViewTCAllocationReport') || isValid('CanViewSaleInvoiceReport') || isValid('CanViewPurchaseInvoiceReport') || isValid('CanViewInventoryReport') || isValid('CanViewProductInventoryReport') || isValid('CanViewTrialBalance') || isValid('CanViewBalanceSheetReport') || isValid('CanViewIncomeStatementReport') || isValid('CanViewCustomerLedger') || isValid('CanViewSupplieLedger') || isValid('CanViewStockValueReport') || isValid('CanViewStockAverageValue') || isValid('CanViewTransactionTypeStock') || isValid('CanViewCustomerWiseProductsSale') || isValid('CanViewCustomersWiseProductSale') || isValid('CanViewSupplierWiseProductsPurchase') || isValid('CanViewSuppliersWiseProductPurchase') || isValid('CanViewCustomerDiscountProducts') || isValid('CanViewSupplierDiscountProducts') || isValid('CanViewProductDiscountCustomer') || isValid('CanViewProductDiscountSupplier') || isValid('CanViewFreeOfCostPurchase') || isValid('CanViewFreeOfCostSale') || isValid('CanViewAccountLedger') || isValid('CanViewBanTransaction') || isValid('CanViewCustomerBalance') || isValid('CanViewSupplierBalance') || isValid('CanViewVatPayableReport') || isValid('CanViewDayWiseTransactions') || isValid('CanViewDayWiseReport')" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
        <div class="setup_sidebar">
            <div class="row ">
                <div class="col-lg-12">
                    <div class="form-group">
                        <div>
                            <input type="text" class="form-control search_input" v-model="searchQuery" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" name="search" id="search" :placeholder="$t('AllReport.Search')" />
                            <span class="fas fa-search search_icon" style="top: 14px;"></span>
                        </div>
                    </div>
                </div>
            </div>
            <div class="setup_sidebar_wrapper">



                <template v-for="item in resultQuery">
                    <div class="setup_menu"  v-bind:style="$i18n.locale == 'pt'? 'height:116px !important ' : '' " v-on:click="GotoPage(item.uri,item.isCustomer,item.isForm)" v-if="CheckPermission(item)" :key="item.id">
                        <div class="setup_menu_icon">
                            <div class="setup_icon_wrapper">
                                <img src="Reports/Stock report.svg">
                            </div>
                        </div>
                        <div class="setup_menu_link" >
                            <div class="setup_menu_titile">
                                {{($i18n.locale == 'en' ||isLeftToRight()) ? item.titleEn : item.titleEn }}
                            </div>
                            <p class="setup_menu_desc">
                                {{$t(item.description)}}
                            </p>
                        </div>
                    </div>
                </template>


                <!--<div class="setup_menu" v-on:click="GotoPage('/StockReport')" v-if="isValid('Can View Report')">
            <div class="setup_menu_icon">
                <div class="setup_icon_wrapper">
                    <img src="Reports/Stock report.svg">
                </div>
            </div>
            <div class="setup_menu_link">
                <div class="setup_menu_titile">
                    {{$t('AllReports.StockReport')}}
                </div>
                <p class="setup_menu_desc">
                    {{$t('AllReports.StockReport')}}
                </p>
            </div>
        </div>
        <div class="setup_menu" v-on:click="GotoPage('/InvoiceReport',true,'Sale')">
            <div class="setup_menu_icon">
                <div class="setup_icon_wrapper">
                    <img src="Reports/Stock report.svg">
                </div>
            </div>
            <div class="setup_menu_link">
                <div class="setup_menu_titile">
                    {{$t('SaleInvoiceReport')}}
                </div>
                <p class="setup_menu_desc">
                    {{$t('SaleInvoiceReport')}}
                </p>
            </div>
        </div>
        <div class="setup_menu" v-on:click="GotoPage('/InvoiceReport',true,'Purchase')">
            <div class="setup_menu_icon">
                <div class="setup_icon_wrapper">
                    <img src="Reports/Stock report.svg">
                </div>
            </div>
            <div class="setup_menu_link">
                <div class="setup_menu_titile">
                    {{$t('PurchaseInvoiceReport')}}
                </div>
                <p class="setup_menu_desc">
                    {{$t('PurchaseInvoiceReport')}}
                </p>
            </div>
        </div>
        <div class="setup_menu" v-on:click="GotoPage('/InventoryFilterReport')">
            <div class="setup_menu_icon">
                <div class="setup_icon_wrapper">
                    <img src="Reports/Inventory report.svg">
                </div>
            </div>
            <div class="setup_menu_link">
                <div class="setup_menu_titile">
                    {{ $t('AllReports.InventoryReport') }}
                </div>
                <p class="setup_menu_desc">
                    {{ $t('AllReports.InventoryReport') }}
                </p>
            </div>
        </div>-->
                <!--<div class="setup_menu" v-on:click="GotoPage('/ProductInventoryReport')">
            <div class="setup_menu_icon">
                <div class="setup_icon_wrapper">
                    <img src="Reports/Inventory report.svg">
                </div>
            </div>
            <div class="setup_menu_link">
                <div class="setup_menu_titile">
                    {{ $t('ProductInventoryReport') }}
                </div>
                <p class="setup_menu_desc">
                    {{ $t('ProductInventoryReport') }}
                </p>
            </div>
        </div>-->
                <!--<div class="setup_menu" v-on:click="GotoPage('/TrialBalanceReport')">
            <div class="setup_menu_icon">
                <div class="setup_icon_wrapper">
                    <img src="Reports/Trial balance report.svg">
                </div>
            </div>
            <div class="setup_menu_link">
                <div class="setup_menu_titile">
                    {{ $t('AllReports.TrialBalanceReport') }}
                </div>
                <p class="setup_menu_desc">
                    {{ $t('AllReports.TrialBalanceReport') }}
                </p>
            </div>
        </div>
        <div class="setup_menu" v-on:click="GotoPage('/BalanceSheetReport')">
            <div class="setup_menu_icon">
                <div class="setup_icon_wrapper">
                    <img src="Reports/Balance sheet report.svg">
                </div>
            </div>
            <div class="setup_menu_link">
                <div class="setup_menu_titile">
                    {{ $t('AllReports.BalanceSheetReport') }}
                </div>
                <p class="setup_menu_desc">
                    {{ $t('AllReports.BalanceSheetReport') }}
                </p>
            </div>
        </div>-->
                <!--<div class="setup_menu" v-on:click="GotoPage('/IncomeStatementReport')">
            <div class="setup_menu_icon">
                <div class="setup_icon_wrapper">
                    <img src="Reports/Income state report.svg">
                </div>
            </div>
            <div class="setup_menu_link">
                <div class="setup_menu_titile">
                    {{ $t('AllReports.IncomeStatementReport') }}
                </div>
                <p class="setup_menu_desc">
                    {{ $t('AllReports.IncomeStatementReport') }}
                </p>
            </div>
        </div>
        <div class="setup_menu" v-on:click="GotoPage('/CustomerLedgerReport',true,true)">
            <div class="setup_menu_icon">
                <div class="setup_icon_wrapper">
                    <img src="Reports/Customer Ledger report.svg">
                </div>
            </div>
            <div class="setup_menu_link">
                <div class="setup_menu_titile">
                    {{ $t('AllReports.CustomerLedgerReport') }}
                </div>
                <p class="setup_menu_desc">
                    {{ $t('AllReports.CustomerLedgerReport') }}
                </p>
            </div>
        </div>
        <div class="setup_menu" v-on:click="GotoPage('/CustomerLedgerReport',false,false)">
            <div class="setup_menu_icon">
                <div class="setup_icon_wrapper">
                    <img src="Reports/Supplier ledger report.svg">
                </div>
            </div>
            <div class="setup_menu_link">
                <div class="setup_menu_titile">
                    {{ $t('AllReports.SupplierLedgerReport') }}
                </div>
                <p class="setup_menu_desc">
                    {{ $t('AllReports.SupplierLedgerReport') }}
                </p>
            </div>
        </div>-->
                <!--<div class="setup_menu" v-on:click="GotoPage('/ProductStockValue')" v-if="isValid('Can View Stock Value Report')">
            <div class="setup_menu_icon">
                <div class="setup_icon_wrapper">
                    <img src="Reports/Stock value.svg">
                </div>
            </div>
            <div class="setup_menu_link">
                <div class="setup_menu_titile">
                    {{ $t('AllReports.StockValue') }}
                </div>
                <p class="setup_menu_desc">
                    {{ $t('AllReports.StockValue') }}
                </p>
            </div>
        </div>
        <div class="setup_menu" v-on:click="GotoPage('/ProductStockAvgValue')" v-if="isValid('Can View Stock Average  Value Report')">
            <div class="setup_menu_icon">
                <div class="setup_icon_wrapper">
                    <img src="Reports/Stock average value.svg">
                </div>
            </div>
            <div class="setup_menu_link">
                <div class="setup_menu_titile">
                    {{ $t('AllReports.StockAverageValue') }}
                </div>
                <p class="setup_menu_desc">
                    {{ $t('AllReports.StockAverageValue') }}
                </p>
            </div>
        </div>
        <div class="setup_menu" v-on:click="GotoPage('/ProductStockValueTransactionType')" v-if="isValid('Can View Transaction Type Stock Value Report')">
            <div class="setup_menu_icon">
                <div class="setup_icon_wrapper">
                    <img src="Reports/Transactions type stock value.svg">
                </div>
            </div>
            <div class="setup_menu_link">
                <div class="setup_menu_titile">
                    {{ $t('AllReports.TransactionTypeStockValue') }}
                </div>
                <p class="setup_menu_desc">
                    {{ $t('AllReports.TransactionTypeStockValue') }}
                </p>
            </div>
        </div>
        <div class="setup_menu" v-on:click="GotoPage('/CustomerWiseProductSaleReport')" v-if="isValid('Can View Customer Wise Product Sale Report')">
            <div class="setup_menu_icon">
                <div class="setup_icon_wrapper">
                    <img src="Reports/Cutomer wise product sale.svg">
                </div>
            </div>
            <div class="setup_menu_link">
                <div class="setup_menu_titile">
                    {{ $t('AllReports.CustomerWiseProductsSale') }}
                </div>
                <p class="setup_menu_desc">
                    {{ $t('AllReports.CustomerWiseProductsSale') }}
                </p>
            </div>
        </div>
        <div class="setup_menu" v-on:click="GotoPage('/CustomersWiseProductSaleReport')" v-if="isValid('Can View Customer Wise Product Sale Report')">
            <div class="setup_menu_icon">
                <div class="setup_icon_wrapper">
                    <img src="Reports/Cutomer wise product Purchase.svg">
                </div>
            </div>
            <div class="setup_menu_link">
                <div class="setup_menu_titile">
                    {{ $t('AllReports.CustomersWiseProductSale') }}
                </div>
                <p class="setup_menu_desc">
                    {{ $t('AllReports.CustomersWiseProductSale') }}
                </p>
            </div>
        </div>-->
                <!--<div class="setup_menu" v-on:click="GotoPage('/SupplierWiseProductPurchaseReport')" v-if="isValid('Can View Supplier Wise Product Purchase Report')">
            <div class="setup_menu_icon">
                <div class="setup_icon_wrapper">
                    <img src="Reports/Supplier wise product purchase.svg">
                </div>
            </div>
            <div class="setup_menu_link">
                <div class="setup_menu_titile">
                    {{ $t('AllReports.SupplierWiseProductsPurchase') }}
                </div>
                <p class="setup_menu_desc">
                    {{ $t('AllReports.Detailsofthisreport') }}
                </p>
            </div>
        </div>
        <div class="setup_menu" v-on:click="GotoPage('/SuppliersWiseProductPurchaseReport')" v-if="isValid('Can View Supplier Wise Product Purchase Report')">
            <div class="setup_menu_icon">
                <div class="setup_icon_wrapper">
                    <img src="Reports/Supplier wise product purchase.svg">
                </div>
            </div>
            <div class="setup_menu_link">
                <div class="setup_menu_titile">
                    {{ $t('AllReports.SuppliersWiseProductPurchaseReport') }}
                </div>
                <p class="setup_menu_desc">
                    {{ $t('AllReports.Detailsofthisreport') }}
                </p>
            </div>
        </div>
        <div class="setup_menu" v-on:click="GotoPage('/CustomerDiscountProducts')" v-if="isValid('Can View Customer Discount Product Report')">
            <div class="setup_menu_icon">
                <div class="setup_icon_wrapper">
                    <img src="Reports/Customer discount products.svg">
                </div>
            </div>
            <div class="setup_menu_link">
                <div class="setup_menu_titile">
                    {{ $t('AllReports.CustomersDiscountProducts') }}
                </div>
                <p class="setup_menu_desc">
                    {{ $t('AllReports.CustomersDiscountProducts') }}
                </p>
            </div>
        </div>
        <div class="setup_menu" v-on:click="GotoPage('/SupplierDiscountProducts')" v-if="isValid('Can View Supplier Discount Product Report')">
            <div class="setup_menu_icon">
                <div class="setup_icon_wrapper">
                    <img src="Reports/Supplier discount products.svg">
                </div>
            </div>
            <div class="setup_menu_link">
                <div class="setup_menu_titile">
                    {{ $t('AllReports.SupplierDiscountProducts') }}
                </div>
                <p class="setup_menu_desc">
                    {{ $t('AllReports.SupplierDiscountProducts') }}
                </p>
            </div>
        </div>
        <div class="setup_menu" v-on:click="GotoPage('/ProductDiscountCustomers')" v-if="isValid('Can View Product Discount Customer Report')">
            <div class="setup_menu_icon">
                <div class="setup_icon_wrapper">
                    <img src="Reports/Product discount customer.svg">
                </div>
            </div>
            <div class="setup_menu_link">
                <div class="setup_menu_titile">
                    {{ $t('AllReports.ProductDiscountCustomers') }}
                </div>
                <p class="setup_menu_desc">
                    {{ $t('AllReports.ProductDiscountCustomers') }}
                </p>
            </div>
        </div>-->
                <!--<div class="setup_menu" v-on:click="GotoPage('/ProductDiscountSuppliers')" v-if="isValid('Can View Product Discount Supplier Report')">
            <div class="setup_menu_icon">
                <div class="setup_icon_wrapper">
                    <img src="Reports/Product discount supplier.svg">
                </div>
            </div>
            <div class="setup_menu_link">
                <div class="setup_menu_titile">
                    {{ $t('AllReports.ProductDiscountSuppliers') }}
                </div>
                <p class="setup_menu_desc">
                    {{ $t('AllReports.ProductDiscountSuppliers') }}
                </p>
            </div>
        </div>
        <div class="setup_menu" v-on:click="GotoPage('/FreeofCostPurchase')">
            <div class="setup_menu_icon">
                <div class="setup_icon_wrapper">
                    <img src="Reports/Free of cost purchase.svg">
                </div>
            </div>
            <div class="setup_menu_link">
                <div class="setup_menu_titile">
                    {{ $t('AllReports.FreeofCostPurchase') }}
                </div>
                <p class="setup_menu_desc">
                    {{ $t('AllReports.FreeofCostPurchase') }}
                </p>
            </div>
        </div>
        <div class="setup_menu" v-on:click="GotoPage('/FreeofCostSale')" v-if="isValid('Can View Free of Cost Sale Report')">
            <div class="setup_menu_icon">
                <div class="setup_icon_wrapper">
                    <img src="Reports/Free of cost sale.svg">
                </div>
            </div>
            <div class="setup_menu_link">
                <div class="setup_menu_titile">
                    {{ $t('AllReports.FreeofCostSale') }}
                </div>
                <p class="setup_menu_desc">
                    {{ $t('AllReports.FreeofCostSale') }}
                </p>
            </div>
        </div>
        <div class="setup_menu" v-on:click="GotoPage('/AccountLedger')" v-if="isValid('Can View Account Ledger Report')">
            <div class="setup_menu_icon">
                <div class="setup_icon_wrapper">
                    <img src="Reports/Account ledger.svg">
                </div>
            </div>
            <div class="setup_menu_link">
                <div class="setup_menu_titile">
                    {{ $t('AllReports.AccountLedger') }}
                </div>
                <p class="setup_menu_desc">
                    {{ $t('AllReports.AccountLedger') }}
                </p>
            </div>
        </div>
        <div class="setup_menu" v-on:click="GotoPage('/BankTransactionReport')">
            <div class="setup_menu_icon">
                <div class="setup_icon_wrapper">
                    <img src="Reports/Account ledger.svg">
                </div>
            </div>
            <div class="setup_menu_link">
                <div class="setup_menu_titile">
                    {{$t('BankTransactionReport')}}
                </div>
                <p class="setup_menu_desc">
                    {{$t('BankTransactionReport')}}
                </p>
            </div>
        </div>-->
                <!--<div class="setup_menu" v-on:click="GotoPage('/CustomerBalanceReport',true,true)">
            <div class="setup_menu_icon">
                <div class="setup_icon_wrapper">
                    <img src="Reports/Customer balance report.svg">
                </div>
            </div>
            <div class="setup_menu_link">
                <div class="setup_menu_titile">
                    {{ $t('CustomerBalanceReport') }}
                </div>
                <p class="setup_menu_desc">
                    {{ $t('CustomerBalanceReport') }}
                </p>
            </div>
        </div>
        <div class="setup_menu" v-on:click="GotoPage('/CustomerBalanceReport',false,false)">
            <div class="setup_menu_icon">
                <div class="setup_icon_wrapper">
                    <img src="Reports/Supplier balnce report.svg">
                </div>
            </div>
            <div class="setup_menu_link">
                <div class="setup_menu_titile">
                    {{ $t('SupplierBalanceReport') }}
                </div>
                <p class="setup_menu_desc">
                    {{ $t('SupplierBalanceReport') }}
                </p>
            </div>
        </div>
        <div class="setup_menu" v-on:click="GotoPage('/VatPayableReport')">
            <div class="setup_menu_icon">
                <div class="setup_icon_wrapper">
                    <img src="Reports/Tax report.svg">
                </div>
            </div>
            <div class="setup_menu_link">
                <div class="setup_menu_titile">
                    {{ $t('AllReports.VatPayableReport') }}
                </div>
                <p class="setup_menu_desc">
                    {{ $t('AllReports.Detailsofthisreport') }}
                </p>
            </div>
        </div>

        <div class="setup_menu" v-on:click="GotoPage('/DateWiseTransaction')">
            <div class="setup_menu_icon">
                <div class="setup_icon_wrapper">
                    <img src="Reports/Tax report.svg">
                </div>
            </div>
            <div class="setup_menu_link">
                <div class="setup_menu_titile">
                    Day Wise Transaction
                </div>
                <p class="setup_menu_desc">
                    Day Wise Transaction
                </p>
            </div>
        </div>

        <div class="setup_menu" v-on:click="GotoPage('/DayStartReport')">
            <div class="setup_menu_icon">
                <div class="setup_icon_wrapper">
                    <img src="Reports/Tax report.svg">
                </div>
            </div>
            <div class="setup_menu_link">
                <div class="setup_menu_titile">
                    Day Start Report
                </div>
                <p class="setup_menu_desc">
                    Day Start Report
                </p>
            </div>
        </div>-->
            </div>
        </div>
        <div class="setup_main_panel" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? '' : 'setup_main_panel_ar'">

            <div class="col-md-12 col-lg-12 ">
                <div class="card img mt-2 mb-2" style="background-color: #3178F6;" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'imageLeft' : 'imageRight'">
                    <div class="card-body">
                        <div class="row">
                            <div class="col-lg-2 pt-2 ">
                                <img src="Financial advisor.svg" />
                            </div>
                            <div class="col-lg-10 pt-3">
                                <h5 class="page_title  pt-3" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" style="color:white">{{ $t('AllReport.Reports') }}</h5>
                                <nav aria-label="breadcrumb" style="color:white !important">
                                    <ol class="breadcrumb" style="color:white !important">
                                        <li class="breadcrumb-item"><router-link :to="'/StartScreen'"><a href="javascript:void(0)" style="color:white !important"> {{ $t('AllReport.Home') }}</a></router-link></li>

                                        <li class="breadcrumb-item active" style="color:white !important" aria-current="page">{{ $t('AllReport.Reports') }}</li>
                                    </ol>
                                </nav>
                            </div>
                        </div>

                    </div>
                </div>
                <div v-if="path == 'AllReports' ">
                    <div class="row">
                        <div class="col-12" style="text-align:center;width:250px; height:250px;margin-top:141px">
                            <img src="Empty form.svg" />
                            <h5 class="HeadingOfEmpty">{{ $t('AllReport.EmptyForms') }}</h5>
                            <p>{{ $t('AllReport.Selectformtheleft') }}</p>
                        </div>
                    </div>
                </div>
                <div v-else>
                    <router-view></router-view>

                </div>
            </div>

        </div>

    </div>
    <div v-else> <acessdenied></acessdenied></div>
   
</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'
    export default {
        mixins: [clickMixin],
        data: function () {
            return {

                path: 'AllReports',
                searchQuery: null,
                resources: [
                    { id: 1, titleEn: this.$t('AllReport.StockReport'), titleAr: "المخزون المخزون", uri: "/StockReport", isCustomer: undefined, isForm: undefined, description: "AllReport.StockReport", img: "Reports/Stock report.svg", permission:"CanViewStockReport" },
                    { id: 2, titleEn: this.$t('AllReport.SaleInvoiceReport'), titleAr: "تقرير فاتورة البيع", uri: "/InvoiceReport", isCustomer: true, isForm: 'Sale', description: "AllReport.SaleInvoiceReport", img: "Reports/Stock report.svg", permission: "CanViewSaleInvoiceReport" },
                    { id: 3, titleEn: this.$t('AllReport.PurchaseInvoiceReport'), titleAr: "تقرير فاتورة الشراء", uri: "/InvoiceReport", isCustomer: true, isForm: 'Purchase', description: "AllReport.PurchaseInvoiceReport", img: "Reports/Stock report.svg", permission: "CanViewPurchaseInvoiceReport" },
                    { id: 4, titleEn: this.$t('AllReport.InventoryReport'), titleAr: "تقرير المخزونات", uri: "/InventoryFilterReport", isCustomer: undefined, isForm: undefined, description: "AllReport.InventoryReport", img: "Reports/Inventory report.svg", permission: "CanViewInventoryReport"  },
                    { id: 5, titleEn: this.$t('AllReport.ProductInventoryReport'), titleAr: "تقرير جرد المنتج", uri: "/ProductInventoryReport", isCustomer: undefined, isForm: undefined, description: "", img: "Reports/Inventory report.svg", permission: "CanViewProductInventoryReport"  },
                    { id: 6, titleEn: this.$t('AllReport.TrialBalanceReport'), titleAr: "تقرير ميزان المراجعة", uri: "/TrialBalanceReport", isCustomer: undefined, isForm: undefined, description: "AllReport.TrialBalanceReport", img: "Reports/Trial balance report.svg", permission: "CanViewTrialBalance" },
                    { id: 38, titleEn: this.$t('VATExpenseReport.VATExpenseReport'), titleAr: "تقرير مصروفات ضريبة القيمة المضافة", uri: "/VatExpenseReport", description: "VAT Expense Report", img: "Reports/Stock report.svg", permission: "CanViewPurchaseInvoiceReport" },
                    { id: 7, titleEn: this.$t('AllReport.TrialBalanceAccountReport'), titleAr: "تقرير حساب ميزان المراجعة", uri: "/TrialBalanceAccountReport", isCustomer: undefined, isForm: undefined, description: "AllReport.TrialBalanceAccountReport", img: "Reports/Trial balance report.svg", permission: "CanViewTrialBalance" },
                    { id: 8, titleEn: this.$t('TrialBalanceDetailReport.TrialBalanceDetailReport'), titleAr: "تقرير تفاصيل ميزان المراجعة", uri: "/TrialBalanceTreeReport", isCustomer: undefined, isForm: undefined, description: "Trial Balance Report", img: "Reports/Trial balance report.svg", permission: "CanViewTrialBalance" },
                    { id: 9, titleEn: this.$t('AllReport.BalanceSheetReport'), titleAr: "تقرير الميزانية العمومية", uri: "/BalanceSheetReport", isCustomer: undefined, isForm: undefined, description: "AllReport.BalanceSheetReport", img: "Reports/Balance sheet report.svg", permission: "CanViewBalanceSheetReport"  },
                    { id: 10, titleEn: this.$t('AllReport.IncomeStatementReport'), titleAr: "تقرير بيان الدخل", uri: "/IncomeStatementReport", isCustomer: undefined, isForm: undefined, description: "AllReport.IncomeStatementReport", img: "Reports/Income state report.svg", permission: "CanViewIncomeStatementReport"  },
                    { id: 11, titleEn: this.$t('AllReport.CustomerLedgerReport'), titleAr: "تقرير دفتر الأستاذ العميل", uri: "/CustomerLedgerReport", isCustomer: true, isForm: true, description: "AllReport.CustomerLedgerReport", img: "Reports/Customer Ledger report.svg", permission: "CanViewCustomerLedger"  },
                    { id: 12, titleEn: this.$t('AllReport.SupplierLedgerReport'), titleAr: "تقرير دفتر الأستاذ المورد", uri: "/CustomerLedgerReport", isCustomer: false, isForm: false, description: "AllReport.SupplierLedgerReport", img: "Reports/Supplier ledger report.svg", permission: "CanViewSupplieLedger"  },
                    { id: 13, titleEn: this.$t('AllReport.StockValue'), titleAr: "قيمة السهم", uri: "/ProductStockValue", isCustomer: undefined, isForm: undefined, description: "AllReport.StockValue", img: "Reports/Stock value.svg", permission: "CanViewStockValueReport"  },
                    { id: 14, titleEn: this.$t('AllReport.StockAverageValue'), titleAr: "قيمة متوسط ​​المخزون", uri: "/ProductStockAvgValue", isCustomer: undefined, isForm: undefined, description: "AllReport.StockAverageValue", img: "Reports/Stock average value.svg", permission: "CanViewStockAverageValue"  },
                    { id: 15, titleEn: this.$t('AllReport.TransactionTypeStockValue'), titleAr: "قيمة المخزون نوع المعاملة", uri: "/ProductStockValueTransactionType", isCustomer: undefined, isForm: undefined, description: "AllReport.TransactionTypeStockValue", img: "Reports/Transactions type stock value.svg", permission: "CanViewTransactionTypeStock"  },
                    { id: 16, titleEn: this.$t('AllReport.CustomerWiseProductsSale'), titleAr: "بيع منتجات العملاء الحكيمة", uri: "/CustomerWiseProductSaleReport", isCustomer: undefined, isForm: undefined, description: "AllReport.CustomerWiseProductsSale", img: "Reports/Cutomer wise product sale.svg", permission: "CanViewCustomerWiseProductsSale"  },
                    { id: 17, titleEn: this.$t('AllReport.CustomersWiseProductSale'), titleAr: "بيع المنتجات الحكيمة للعملاء", uri: "/CustomersWiseProductSaleReport", isCustomer: undefined, isForm: undefined, description: "AllReport.CustomersWiseProductSale", img: "Reports/Cutomer wise product Purchase.svg", permission: "CanViewCustomersWiseProductSale"  },
                    { id: 18, titleEn: this.$t('AllReport.SupplierWiseProductsPurchase'), titleAr: "شراء المنتجات الحكيمة من المورد", uri: "/SupplierWiseProductPurchaseReport", isCustomer: undefined, isForm: undefined, description: "AllReport.Detailsofthisreport", img: "Reports/Supplier wise product purchase.svg", permission: "CanViewSupplierWiseProductsPurchase"  },
                    { id: 19, titleEn: this.$t('AllReport.SuppliersWiseProductPurchaseReport'), titleAr: "تقرير شراء المنتج الحكيم من الموردين", uri: "/SuppliersWiseProductPurchaseReport", isCustomer: undefined, isForm: undefined, description: "AllReport.Detailsofthisreport", img: "Reports/Supplier wise product purchase.svg", permission: "CanViewSuppliersWiseProductPurchase"  },
                    { id: 20, titleEn: this.$t('AllReport.CustomersDiscountProducts'), titleAr: "منتجات الخصم للعملاء", uri: "/CustomerDiscountProducts", isCustomer: undefined, isForm: undefined, description: "AllReport.CustomersDiscountProducts", img: "Reports/Customer discount products.svg", permission: "CanViewCustomerDiscountProducts"  },
                    { id: 21, titleEn: this.$t('AllReport.SupplierDiscountProducts'), titleAr: "منتجات الخصم من الموردين", uri: "/SupplierDiscountProducts", isCustomer: undefined, isForm: undefined, description: "AllReport.SupplierDiscountProducts", img: "Reports/Supplier discount products.svg", permission: "CanViewSupplierDiscountProducts"  },
                    { id: 22, titleEn: this.$t('AllReport.ProductDiscountCustomers'), titleAr: "عملاء خصم المنتج", uri: "/ProductDiscountCustomers", isCustomer: undefined, isForm: undefined, description: "AllReport.ProductDiscountCustomers", img: "Reports/Product discount customer.svg", permission: "CanViewProductDiscountCustomer"  },
                    { id: 23, titleEn: this.$t('AllReport.ProductDiscountSuppliers'), titleAr: "عملاء خصم المنتج", uri: "/ProductDiscountSuppliers", isCustomer: undefined, isForm: undefined, description: "AllReport.ProductDiscountSuppliers", img: "Reports/Product discount supplier.svg", permission: "CanViewProductDiscountSupplier"  },
                    { id: 24, titleEn: this.$t('AllReport.FreeofCostPurchase'), titleAr: "شراء خالية من التكلفة", uri: "/FreeofCostPurchase", isCustomer: undefined, isForm: undefined, description: "AllReport.FreeofCostPurchase", img: "Reports/Free of cost purchase.svg", permission: "CanViewFreeOfCostPurchase"  },
                    { id: 25, titleEn: this.$t('AllReport.FreeofCostSale'), titleAr: "بيع خالية من التكلفة", uri: "/FreeofCostSale", isCustomer: undefined, isForm: undefined, description: "AllReport.FreeofCostSale", img: "Reports/Free of cost sale.svg", permission: "CanViewFreeOfCostSale"  },
                    { id: 26, titleEn: this.$t('AllReport.AccountLedger'), titleAr: "دفتر الأستاذ", uri: "/AccountLedger", isCustomer: undefined, isForm: undefined, description: "AllReport.AccountLedger", img: "Reports/Account ledger.svg", permission: "CanViewAccountLedger"  },
                    { id: 27, titleEn: this.$t('Accountledgerdetailreport.Accountledgerdetailreport'), titleAr: "تقرير تفاصيل دفتر الأستاذ", uri: "/AccountLedgerCostCenterWise", isCustomer: undefined, isForm: undefined, description: "Detail Report", img: "Reports/Account ledger.svg", permission: "CanViewAccountLedger"  },
                    { id: 28, titleEn: this.$t('AllReport.BankTransactionReport'), titleAr: "تقرير المعاملات المصرفية", uri: "/BankTransactionReport", isCustomer: undefined, isForm: undefined, description: "AllReport.BankTransactionReport", img: "Reports/Account ledger.svg", permission: "CanViewBanTransaction"  },
                    { id: 29, titleEn: this.$t('AllReport.CustomerBalanceReport'), titleAr: "تقرير رصيد العميل", uri: "/CustomerBalanceReport", isCustomer: true, isForm: true, description: "AllReport.CustomerBalanceReport", img: "Reports/Customer balance report.svg", permission: "CanViewCustomerBalance" },
                    { id: 30, titleEn: this.$t('AllReport.SupplierBalanceReport'), titleAr: "تقرير رصيد المورد", uri: "/CustomerBalanceReport", isCustomer: false, isForm: false, description: "AllReport.SupplierBalanceReport", img: "Reports/Supplier balnce report.svg", permission: "CanViewSupplierBalance"  },
                    { id: 31, titleEn: this.$t('AllReport.VatPayableReport'), titleAr: "تقرير ضريبة القيمة المضافة المستحقة الدفع", uri: "/VatPayableReport", isCustomer: undefined, isForm: undefined, description: "AllReport.Detailsofthisreport", img: "Reports/Tax report.svg", permission: "CanViewVatPayableReport" },
                    { id: 32, titleEn: "Day wise Transaction", titleAr: "Daywise Transaction", uri: "/DateWiseTransaction", isCustomer: undefined, isForm: undefined, description: "Day Wise Transaction", img: "Reports/Tax report.svg", permission: "CanViewDayWiseTransactions" },
                    { id: 33, titleEn: this.$t('DayStartWiseReport.DayWiseReport'), titleAr: "تقرير اليوم الحكيم", uri: "/DayStartReport", isCustomer: undefined, isForm: undefined, description: "Day Start Report", img: "Reports/Tax report.svg", permission: "CanViewDayWiseReport" },
                    { id: 34, titleEn: this.$t('MonthlySales.MonthlySalesReport'), titleAr: "تقرير المبيعات الشهرية", uri: "/MonthlySalesReport", isCustomer: true, isForm: 'Sale', description: "MonthlySales.MonthlySalesReport", img: "Reports/Stock report.svg", permission: "CanViewSaleInvoiceReport" },
                    { id: 35, titleEn: this.$t('MonthlySales.MonthlyPurchaseReport'), titleAr: "تقرير المشتريات الشهري", uri: "/MonthlySalesReport", isCustomer: true, isForm: 'Purchase', description: "MonthlySales.MonthlyPurchaseReport", img: "Reports/Stock report.svg", permission: "CanViewPurchaseInvoiceReport" },
                    { id: 36, titleEn: "Comparision Report", titleAr: "تقرير المشتريات الشهري", uri: "/MonthlySalesComparisionReport", isCustomer: undefined, isForm: undefined, description: "Comparision Report", img: "Reports/Stock report.svg", permission: "CanViewPurchaseInvoiceReport" },
                    { id: 37, titleEn: this.$t('TemporaryCashAllocationReport.TemporaryCashAllocationReport'), titleAr: "تقرير التخصيص النقدي المؤقت", uri: "/TemporaryCashAllocationReport", isCustomer: undefined, isForm: undefined, description: "Temporary Cash Allocation", img: "Reports/Stock report.svg", permission: "CanViewPurchaseInvoiceReport" },


                ]

            }
        },
        computed: {
            resultQuery() {
                if (this.searchQuery) {
                    return this.resources.filter((item) => {
                        if ((this.$i18n.locale == 'en' || this.isLeftToRight())) {
                            return this.searchQuery.toLowerCase().split(' ').every(v => item.titleEn.toLowerCase().includes(v))
                        }
                        else {
                            return this.searchQuery.toLowerCase().split(' ').every(v => item.titleAr.toLowerCase().includes(v))
                        }
                    })
                } else {
                    return this.resources;
                }
            }
            
        },
        methods: {
            CheckPermission: function (x) {
                if (this.isValid(x.permission))
                    return true
                return false
            },
            GotoPage: function (link, isCustomer, isForm) {
                this.path = link;


                if (isCustomer == undefined || isForm == undefined) {
                    this.$router.push({
                        path: link
                    });
                }
                else if (isForm == 'Sale' || isForm == 'Purchase') {
                    this.$router.push({
                        path: link,
                        query: {
                            formName: isForm,
                        }

                    });
                }
                else {
                    var customer = false;
                    if (isCustomer == true) {
                        customer = true;
                    }
                    var formNames = 'Supplier';
                    if (isForm == true) {
                        formNames = 'Customer'
                    }
                    this.$router.push({
                        path: link,
                        query: {
                            isCustomer: customer,
                            formName: formNames,
                        }

                    });
                }

            },

        },
        created: function () {

            this.path = this.$route.name;
            this.$emit('update:modelValue', this.$route.name);
        },
    }
</script>
<style scoped>
    .img {
        height: 160px;
        background-size: contain !important;
        background-repeat: no-repeat !important;
    }
</style>
