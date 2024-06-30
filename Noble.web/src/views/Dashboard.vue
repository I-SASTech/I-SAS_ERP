<template>

    <!--Left Sidenav-->
    <div class="left-sidenav">
        <!-- LOGO -->
        <div class="brand text-start ms-2">
            <a href="javascript:void(0)" class="logo" v-on:click="GoTo('/StartScreen')">
                <span>
                    <img src="noblelogo.png" alt="logo-small" class="logo-sm" style="width:100px;height:auto; max-height:45px;">
                </span>
            </a>
        </div>
        <!--end logo-->
        <div class="menu-content h-100" data-simplebar v-if="isHrPortal" hidden>
            <ul class="metismenu left-sidenav-menu" id="metismenu">
                <li>
                    <a href="javascript:void(0);" v-if="isValid('LeaveManagement') && isValid('EmployeePortalActivation')">
                        <i class="ti-control-record"></i>Leave Management<span class="menu-arrow left-has-menu">
                            <i class="mdi mdi-chevron-right"></i>
                        </span>
                    </a>

                    <ul class="nav-second-level" aria-expanded="false">
                        <li>
                            <a v-on:click="GoTo('/Leave', 'HR And PayRoll_token', 'true')" href="javascript:void(0);"> Leave</a>
                        </li>
                    </ul>
                </li>
            </ul>
        </div>
        <div class="menu-content h-100" data-simplebar v-else>
            <ul class="metismenu left-sidenav-menu">
                <li v-if="isValid('CanViewDashboard') && role != 'Noble Admin'">
                    <a href="javascript:void(0);" v-on:click="GoTo('/Dashboard1', 'DayStart_token', 'true')">
                        <i data-feather="trending-up" class="align-self-center menu-icon"></i><span>
                            {{
                                        $t('Dashboard.Dashboard')
                            }}
                        </span>
                    </a>
                </li>

                <li v-if="isValid('CanViewDashboard') && role == 'Noble Admin'">
                    <a href="javascript:void(0);" v-on:click="GoTo('/')">
                        <i data-feather="trending-up" class="align-self-center menu-icon"></i><span>{{ $t('Dashboard.Dashboard') }}</span>
                    </a>
                </li>
                <li v-if="role == 'Noble Admin'">
                    <a href="javascript:void(0);" v-on:click="GoTo('/clientManagement')">
                        <i data-feather="trending-up" class="align-self-center menu-icon"></i><span>Client Management</span>
                    </a>
                    <!-- <a href="javascript:void(0);" v-on:click="GoTo('/Branches')">
                        <i data-feather="trending-up" class="align-self-center menu-icon"></i><span>Branches</span>
                    </a>



                    <a href="javascript:void(0);" v-on:click="GoTo('/BranchesPrefix')">
                        <i data-feather="trending-up" class="align-self-center menu-icon"></i><span>Branch Prefix</span>
                    </a> -->

                </li>

                <li v-if="role == 'Admin'">
                    <a href="javascript:void(0);">
                        <i data-feather="grid" class="align-self-center menu-icon"></i><span>
                            {{
                                    $t('Dashboard.Dashboard')
                            }}
                        </span><span class="menu-arrow"><i class="mdi mdi-chevron-right"></i></span>
                    </a>
                    <ul class="nav-second-level" aria-expanded="false">
                        <li>
                            <a v-on:click="GoTo('/business')" href="javascript:void(0);">
                                {{
                                    $t('Dashboard.business_Heading')
                                }}
                            </a>
                        </li>
                        <li>
                            <a v-on:click="GoTo('/CompanyAdditionalInfo')" href="javascript:void(0);">
                                {{
                                    $t('Dashboard.Company_Additional_Information')
                                }}
                            </a>
                        </li>
                        <li>
                            <a v-on:click="GoTo('/CompanyAttachments')" href="javascript:void(0);">
                                Company
                                Attachments
                            </a>
                        </li>
                    </ul>
                </li>

                <li v-if="role == 'Admin'">
                    <a href="javascript:void(0);">
                        <i data-feather="grid" class="align-self-center menu-icon"></i><span> {{ $t('Dashboard.Manage') }}</span><span class="menu-arrow"><i class="mdi mdi-chevron-right"></i></span>
                    </a>
                    <ul class="nav-second-level" aria-expanded="false">
                        <li>
                            <a v-on:click="GoTo('/location')" href="javascript:void(0);">
                                {{
                                    $t('Dashboard.Location_Heading')
                                }}
                            </a>
                        </li>
                        <li>
                            <a v-on:click="GoTo('/CompanyAttachments')" href="javascript:void(0);">
                                Business
                                Attachments
                            </a>
                        </li>
                    </ul>
                </li>
                <!-- Day Start Side Menu Bar Code  -->

                <li v-if="isValid('StartOperationReport') || isValid('StartOperationSetup') || isValid('StartDay')">
                    <a href="javascript:void(0);">
                        <i data-feather="clock" class="align-self-center menu-icon"></i><span>
                            {{ $t('Dashboard.StartOperations')}}
                        </span><span class="menu-arrow"><i class="mdi mdi-chevron-right"></i></span>
                    </a>
                    <ul class="nav-second-level" aria-expanded="false">
                        <li v-if="isValid('StartDay') && isDayStart == 'true'">
                            <a v-on:click="GoTo('/WholeSaleDay', 'DayStart_token', 'true')" href="javascript:void(0);" v-if="WholeSale == 'true'"> {{ $t('Dashboard.StartOperation') }}</a>
                            <a v-on:click="GoTo('/daystart', 'DayStart_token', 'true')" href="javascript:void(0);" v-else> {{ $t('Dashboard.StartOperation') }}</a>
                        </li>
                        <!-- <li v-if="isValid('StartOperationReport')">
                            <a v-on:click="GoTo('/CommingSoon')" href="javascript:void(0);"> {{ $t('Dashboard.Reports') }}</a>
                        </li> -->

                        <!-- <li v-if="isValid('StartOperationSetup')">
                            <a href="javascript:void(0);">
                                <i class="ti-control-record"></i>{{ $t('Dashboard.Setup') }}<span class="menu-arrow left-has-menu">
                                    <i class="mdi mdi-chevron-right"></i>
                                </span>
                            </a>

                            <ul class="nav-second-level" aria-expanded="false">
                                <li>
                                    <a v-on:click="GoTo('/CommingSoon')" href="javascript:void(0);"> {{ $t('Dashboard.UserSetup') }}</a>
                                </li>
                                <li v-if="isValid('StartOperationReport')">
                                    <a v-on:click="GoTo('/CommingSoon')" href="javascript:void(0);"> {{ $t('Dashboard.CounterSetup') }}</a>
                                </li>
                                <li v-if="isValid('StartOperationReport')">
                                    <a v-on:click="GoTo('/CommingSoon')" href="javascript:void(0);"> {{ $t('Dashboard.AdditionalSetup') }}</a>
                                </li>
                            </ul>
                        </li> -->
                    </ul>
                </li>

                <!-- Daily Expense Side Menu Bar Code  -->

                <li v-if="isValid('CanViewExpense') || isValid('CanDraftExpense') || isValid('ExpenseSetup')">
                    <a href="javascript:void(0);">
                        <i data-feather="credit-card" class="align-self-center menu-icon"></i><span>
                            {{ $t('Dashboard.DailyExpenses') }}
                        </span><span class="menu-arrow"><i class="mdi mdi-chevron-right"></i></span>
                    </a>

                    <ul class="nav-second-level" aria-expanded="false">
                        <li v-if="isValid('CanViewExpense') || isValid('CanDraftExpense')">

                            <a v-on:click="GoTo('/dailyexpense', 'Expenses_token', 'true','generalexpense')" href="javascript:void(0);" v-if="IsDailyExpense!=true && IsExpenseAccount"> {{ $t('Dashboard.DailyExpense') }}</a>

                            <a v-on:click="GoTo('/dailyexpense', 'Expenses_token', 'true','generalexpense')" href="javascript:void(0);" v-else-if="IsDailyExpense==true"> {{ $t('Dashboard.DailyExpense') }}</a>

                            <a v-on:click="GoTo('/dailyexpense', 'Expenses_token', 'true','generalexpense')" href="javascript:void(0);" v-else-if="IsExpenseAccount==false"> {{ $t('Dashboard.DailyExpense') }}</a>
                        </li>
                        <!-- <li v-if="isValid('ExpenseSetup')">
                            <a v-on:click="GoTo('/CommingSoon')" href="javascript:void(0);"> {{ $t('Dashboard.ExpenseSetup') }}</a>
                        </li> -->

                    </ul>
                </li>

                <!-- Sale Invoice Side Menu Bar Code  -->

                <li @click="SaleValueChange(true)" v-if="(IsSimpleInvoice || AllSale) && (isValid('CanAddQuotation') || isValid('CanDraftQuotation') || isValid('CanViewQuotation')
                     || isValid('CustomerPurchaseOrder') || isValid('CustomerInquiry') ||isValid('SaleReport')|| isValid('DeliveryManagement')|| isValid('BillingManagement')
                     || isValid('ProposalManagement') ||isValid('CanChooseA4InvoiceType') || isValid('CanChooseThermalInvoiceType') || isValid('CanViewHoldInvoices')
                     || isValid('CanViewPaidInvoices') || isValid('CanViewCreditInvoices') ||isValid('CanHoldInvoices') || isValid('CashInvoices')
                     || isValid('CreditInvoices') || isValid('CanViewCustomer')|| isValid('CanAddCustomer') || isValid('TouchInvoiceTemplate1')|| isValid('TouchInvoiceTemplate2')
                     || isValid('TouchInvoiceTemplate3') || isValid('CanAddSaleReturn') || isValid('CanViewSaleReturn')|| isValid('CanAddSaleOrder') || isValid('CanViewSaleOrder')
                     || isValid('CanDraftSaleOrder') || isValid('CanDraftCPR') || isValid('CanViewCPR') || isValid('CanAddCPR') || isValid('CanAddServiceQuotation')
                     || (isValid('InquiryManagement') && (isValid('CanViewInquiryProcess') || isValid('CanViewInquirySetup') || isValid('CanViewInquiryType')
                     || isValid('CanViewInquiry') || isValid('CanAddInquiry'))))">

                    <a href="javascript:void(0);">
                        <i data-feather="shopping-cart" class="align-self-center menu-icon"></i><span>
                            POS-Sales
                        </span><span class="menu-arrow"><i class="mdi mdi-chevron-right"></i></span>
                    </a>

                    <ul class="nav-second-level" aria-expanded="false">
                        <li v-if="isValid('CanHoldInvoices') || isValid('CashInvoices') || isValid('CreditInvoices')">
                            <a v-on:click="GoTo('/AddSaleService', 'Sales_token', 'true', 'SaleInvoice','false')" href="javascript:void(0);"> {{ $t('Dashboard.AddInvoice') }}</a>
                        </li>
                        <li v-if="isValid('CanViewProforma')">
                            <a v-on:click="GoTo('/ServiceProformaInvoice', 'Sales_token', 'true', 'ProformaInvoice')" href="javascript:void(0);">{{ $t('Dashboard.ProformaInvoices') }} </a>

                        </li>
                        <li v-if="isValid('ViewCustomerPO')">
                            <a v-on:click="GoTo('/ServiceProformaInvoice', 'Sales_token', 'true', 'PurchaseOrder')" href="javascript:void(0);">{{ $t('Dashboard.CustomerPurchaseOrder') }}</a>

                        </li>

                        <li v-if="isValid('TouchInvoiceTemplate1')">
                            <a v-on:click="GoTo('/TouchScreen', 'Sales_token', 'true')" href="javascript:void(0);"> {{ $t('Dashboard.TouchInvoice') }}</a>
                        </li>

                        <li v-if="isValid('CanViewSaleReturn')">
                            <a v-on:click="GoTo('/SaleReturn', 'Sales_token', 'true')" href="javascript:void(0);"> {{ $t('Dashboard.ReturnInvoice') }}</a>
                        </li>
                        <li v-else-if="isValid('CanAddSaleReturn')">
                            <a v-on:click="GoTo('/AddSaleReturn', 'Sales_token', 'true')" href="javascript:void(0);"> {{ $t('Dashboard.ReturnInvoice') }}</a>
                        </li>
                        <li v-if="isValid('CanViewHoldInvoices') || isValid('CanViewPaidInvoices') || isValid('CanViewCreditInvoices') ">
                            <a v-on:click="GoTo('/SaleService', 'Sales_token', 'true')" href="javascript:void(0);"> {{ $t('Dashboard.SalesRecords') }}</a>
                        </li>
                        <li v-if="isValid('CanViewCPR') || isValid('CanDraftCPR')">
                            <a v-on:click="GoTo('/paymentVoucherList', 'Sales_token', 'true','BankReceipt','false')" href="javascript:void(0);"> {{ $t('Dashboard.CustomerPayReceipt') }}</a>
                        </li>
                        <li>
                            <a v-on:click="GoTo('/paymentVoucherList', 'Sales_token', 'true','AdvanceReceipt','false')" href="javascript:void(0);">Advance Receipt</a>
                        </li>
                        <li v-if="isValid('CanViewCustomer')">
                            <a v-on:click="GoTo('/Customer', 'Sales_token', 'true')" href="javascript:void(0);"> {{ $t('Dashboard.ClientCustomer') }}</a>
                        </li>
                        <li v-else-if="isValid('CanAddCustomer')">
                            <a v-on:click="GoTo('/addCustomer', 'Sales_token', 'true','BankReceipt')" href="javascript:void(0);"> {{ $t('Dashboard.ClientCustomer') }}</a>
                        </li>
                        <li v-if="isValid('CustomerInquiry')">
                            <a v-on:click="GoTo('/CommingSoon')" href="javascript:void(0);"> {{ $t('Dashboard.CustomerInquiry') }}</a>
                        </li>

                        <li v-if="(isValid('CanViewQuotation') || isValid('CanDraftQuotation')) ">
                            <a v-on:click="GoTo('/SaleServiceOrder', 'Sales_token', 'true', 'Quotation')" href="javascript:void(0);"> {{ $t('Dashboard.Quotation') }}</a>
                        </li>

                        <li>
                            <a v-on:click="GoTo('/CustomerGroup', 'Sales_token', 'true','CreditNote')" href="javascript:void(0);"> {{ $t('Dashboard.CustomerGroup') }}</a>
                        </li>
                        <li v-if="(isValid('CreditNote')) ">
                            <a v-on:click="GoTo('/CreditNote', 'Sales_token', 'true','CreditNote')" href="javascript:void(0);"> {{ $t('Dashboard.CreditNote') }}</a>
                        </li>

                        <!-- <li v-else-if="isValid('CanAddQuotation')">
                            <a v-on:click="GoTo('/AddQuotation', 'Sales_token', 'true','BankReceipt')" href="javascript:void(0);"> {{ $t('Dashboard.Quotation') }}</a>
                        </li> -->
                        <li v-if="(isValid('CanViewSaleOrder') || isValid('CanDraftSaleOrder')) ">
                            <a v-on:click="GoTo('/SaleServiceOrder', 'Sales_token', 'true', 'SaleOrder')" href="javascript:void(0);"> {{ $t('Dashboard.SalesOrder') }}</a>
                        </li>
                        <li v-if="(isValid('CanViewSaleOrderTracking') || isValid('CanDraftSaleOrderTracking')) ">
                            <a v-on:click="GoTo('/SaleOrder', 'Sales_token', 'true','SaleOrderTracking')" href="javascript:void(0);"> {{ $t('Dashboard.SalesOrderTracking') }}</a>
                        </li>
                        <li v-if="(isValid('CanViewSaleOrderTracking') || isValid('CanDraftSaleOrderTracking')) ">
                            <a v-on:click="GoTo('/SaleOrderTrackingReport', 'Sales_token', 'true')" href="javascript:void(0);"> {{ $t('Dashboard.SalesOrderTrackingReport') }}</a>
                        </li>
                        <!-- <li v-else-if="isValid('CanAddSaleOrder')">
                            <a v-on:click="GoTo('/AddSaleOrder', 'Sales_token', 'true','BankReceipt')" href="javascript:void(0);"> {{ $t('Dashboard.SalesOrder') }}</a>
                        </li> -->
                        <li v-if="isValid('SaleReport')  ">
                            <a v-on:click="GoTo('/CommingSoon')" href="javascript:void(0);"> {{ $t('Dashboard.SalesReports') }}</a>
                        </li>
                        <li v-if="isValid('CanViewDeliveryNote')  ">
                            <a v-on:click="GoTo('/DeliveryChallan')" href="javascript:void(0);"> Delivery Note</a>
                        </li>
                        <li v-if="isValid('SaleReport')  ">
                            <a v-on:click="GoTo('/SaleVerificationReport', 'Sales_token', 'true')" href="javascript:void(0);"> {{ $t('Dashboard.SaleVerificationReport') }}</a>
                        </li>
                        <li v-if="isValid('CanChooseA4InvoiceType') || isValid('CanChooseThermalInvoiceType')">
                            <a v-on:click="GoTo('/PrintSetting', 'Sales_token', 'true','BankReceipt')" href="javascript:void(0);"> {{ $t('Dashboard.InvoiceSetup') }}</a>
                        </li>
                        <li v-if="isValid('IsPricing')">
                            <a v-on:click="GoTo('/InvoiceDefault', 'Sales_token', 'true','BankReceipt')" href="javascript:void(0);"> {{ $t('Dashboard.InvoiceDefault') }}</a>
                        </li>
                        <li>
                            <a v-on:click="GoTo('/AddListSettingSetup', 'Sales_token', 'true','BankReceipt')" href="javascript:void(0);"> {{ $t('List Setting Setup') }}</a>
                        </li>

                        <li v-if="isValid('DeliveryManagement')">
                            <a href="javascript:void(0);">
                                <i class="ti-control-record"></i>{{ $t('Dashboard.DeliveryManagement') }}<span class="menu-arrow left-has-menu">
                                    <i class="mdi mdi-chevron-right"></i>
                                </span>
                            </a>

                            <ul class="nav-second-level" aria-expanded="false">
                                <li>
                                    <a v-on:click="GoTo('/CommingSoon')" href="javascript:void(0);"> {{ $t('Dashboard.Delivery') }}</a>
                                </li>
                                <li v-if="isValid('StartOperationReport')">
                                    <a v-on:click="GoTo('/CommingSoon')" href="javascript:void(0);"> {{ $t('Dashboard.DeliveryReports') }}</a>
                                </li>
                                <li v-if="isValid('StartOperationReport')">
                                    <a v-on:click="GoTo('/CommingSoon')" href="javascript:void(0);"> {{ $t('Dashboard.DeliverySetup') }}</a>
                                </li>
                            </ul>
                        </li>

                        <li v-if="isValid('BillingManagement')">
                            <a href="javascript:void(0);">
                                <i class="ti-control-record"></i>{{ $t('Dashboard.BillingManagement') }}<span class="menu-arrow left-has-menu">
                                    <i class="mdi mdi-chevron-right"></i>
                                </span>
                            </a>

                            <ul class="nav-second-level" aria-expanded="false">
                                <li>
                                    <a v-on:click="GoTo('/CommingSoon')" href="javascript:void(0);"> {{ $t('Dashboard.Billing') }}</a>
                                </li>
                                <li v-if="isValid('StartOperationReport')">
                                    <a v-on:click="GoTo('/CommingSoon')" href="javascript:void(0);"> {{ $t('Dashboard.BillingReports') }}</a>
                                </li>
                                <li v-if="isValid('StartOperationReport')">
                                    <a v-on:click="GoTo('/CommingSoon')" href="javascript:void(0);"> {{ $t('Dashboard.BillingSetup') }}</a>
                                </li>
                            </ul>
                        </li>

                        <li v-if="isValid('ProposalManagement')">
                            <a href="javascript:void(0);">
                                <i class="ti-control-record"></i>{{ $t('Dashboard.ProposalManagement') }}<span class="menu-arrow left-has-menu">
                                    <i class="mdi mdi-chevron-right"></i>
                                </span>
                            </a>

                            <ul class="nav-second-level" aria-expanded="false">
                                <li>
                                    <a v-on:click="GoTo('/CommingSoon')" href="javascript:void(0);"> {{ $t('Dashboard.CommercialProposal') }}</a>
                                </li>
                                <li v-if="isValid('StartOperationReport')"><a v-on:click="GoTo('/CommingSoon')" href="javascript:void(0);"> {{ $t('Dashboard.ProposalReports') }}</a></li>
                                <li v-if="isValid('StartOperationReport')"><a v-on:click="GoTo('/CommingSoon')" href="javascript:void(0);"> {{ $t('Dashboard.ProposalSetup') }}</a></li>
                            </ul>
                        </li>

                        <li v-if="(isValid('InquiryManagement') && (isValid('CanViewInquiryProcess') || isValid('CanViewInquirySetup') || isValid('CanViewInquiryType') || isValid('CanViewInquiry') || isValid('CanAddInquiry') ))">
                            <a href="javascript:void(0);">
                                <i class="ti-control-record"></i>{{ $t('Dashboard.InquiryManagement') }}
                                <span class="menu-arrow left-has-menu">
                                    <i class="mdi mdi-chevron-right"></i>
                                </span>
                            </a>

                            <ul class="nav-second-level" aria-expanded="false">

                                <li v-if="(isValid('CanViewInquiry') )">
                                    <a v-on:click="GoTo('/Inquiry', 'Inquiry Management_token', 'true')" href="javascript:void(0);"> {{ $t('Dashboard.Inquiry') }}</a>
                                </li>
                                <li v-else-if="(isValid('CanAddInquiry') )">
                                    <a v-on:click="GoTo('/AddInquiry', 'Inquiry Management_token', 'true')" href="javascript:void(0);"> {{ $t('Dashboard.Inquiry') }}</a>
                                </li>
                                <li><a v-on:click="GoTo('/CommingSoon')" href="javascript:void(0);"> {{ $t('Dashboard.InquiryReports') }}</a></li>
                                <li>
                                    <a v-on:click="GoTo('/InquiryModule', 'Inquiry Management_token', 'true')" href="javascript:void(0);"> {{ $t('InquirySetup.InquirySetup') }}</a>
                                </li>
                                <li v-if="(isValid('CanViewInquiryProcess') )">
                                    <a v-on:click="GoTo('/InquiryProcess', 'Inquiry Management_token', 'true')" href="javascript:void(0);">{{ $t('InquirySetup.InquiryProcess') }}</a>
                                </li>
                                <li v-if="(isValid('CanViewInquiryType') )">
                                    <a v-on:click="GoTo('/InquiryType', 'Inquiry Management_token', 'true')" href="javascript:void(0);"> {{ $t('InquirySetup.InquiryType') }}</a>
                                </li>
                            </ul>
                        </li>
                    </ul>
                </li>

                <!-- Sale Service Invoice Side Menu Bar Code  -->

                <li @click="SaleValueChange(false)" v-if="(!IsSimpleInvoice || AllSale) && (isValid('CustomerPurchaseOrder') || isValid('CustomerInquiry') || isValid('SaleServiceReport')
                    ||isValid('SaleServiceReturn') ||isValid('DeliveryManagement') ||isValid('BillingManagement') ||isValid('ProposalManagement')
                    || isValid('CanChooseA4InvoiceType') || isValid('CanViewCustomer')|| isValid('CanAddCustomer') || isValid('CanDraftCPR') || isValid('CanViewCPR')
                    || isValid('CanAddCPR') || isValid('CanAddServiceQuotation') || isValid('CanViewServiceQuotation') || isValid('CanDraftServiceQuotation')
                    || isValid('CanViewHoldServiceInvoices') || isValid('CanViewPaidServiceInvoices') || isValid('CanViewCreditServiceInvoices') || isValid('CanHoldServiceInvoices')
                     || isValid('CashServiceInvoices') || isValid('CreditServiceInvoices')  || isValid('CanAddServiceSaleOrder') || isValid('CanViewServiceSaleOrder')
                     || isValid('CanDraftServiceSaleOrder') || (isValid('InquiryManagement') && (isValid('CanViewInquiryProcess') || isValid('CanViewInquirySetup')
                     || isValid('CanViewInquiryType') || isValid('CanViewInquiry') || isValid('CanAddInquiry') )))">

                    <a href="javascript:void(0);">
                        <i data-feather="shopping-cart" class="align-self-center menu-icon"></i><span>
                            {{ $t('Dashboard.Services-Sales') }}
                        </span><span class="menu-arrow"><i class="mdi mdi-chevron-right"></i></span>
                    </a>

                    <ul class="nav-second-level" aria-expanded="false">
                        <!-- <li >
                            <a v-on:click="GoTo('/AddSaleService', 'Sales_token', 'true','ServiceQuotation')" href="javascript:void(0);"> ADD Service Quotation</a>
                        </li>
                        <li >
                            <a v-on:click="GoTo('/AddSaleService', 'Sales_token', 'true','ServiceSaleOrder')" href="javascript:void(0);"> ADD Service Sale Order</a>
                        </li> -->
                        <li v-if="isValid('CashServiceInvoices') || isValid('CreditServiceInvoices') || isValid('CanHoldServiceInvoices')">
                            <a v-on:click="GoTo('/AddSaleService', 'Sales_token', 'true','SaleInvoice','true')" href="javascript:void(0);"> {{ $t('Dashboard.AddInvoice') }}</a>
                        </li>
                        <li v-if="isValid('CanViewProforma')">
                            <a v-on:click="GoTo('/ServiceProformaInvoice', 'Sales_token', 'true', 'ServiceProformaInvoice')" href="javascript:void(0);"> {{ $t('Dashboard.ProformaInvoices') }}</a>

                        </li>
                        <li v-if="isValid('ViewCustomerPO')">
                            <a v-on:click="GoTo('/ServiceProformaInvoice', 'Sales_token', 'true', 'PurchaseOrder')" href="javascript:void(0);">{{ $t('Dashboard.CustomerPurchaseOrder') }}</a>

                        </li>

                        <li v-if="isValid('SaleServiceReturn')">
                            <a v-on:click="GoTo('/ServiceSaleReturn', 'Sales_token', 'true')" href="javascript:void(0);"> {{ $t('Dashboard.ReturnInvoice') }}</a>
                        </li>

                        <li v-if="isValid('CanViewHoldServiceInvoices') || isValid('CanViewPaidServiceInvoices') || isValid('CanViewCreditServiceInvoices')">
                            <a v-on:click="GoTo('/SaleService', 'Sales_token', 'true')" href="javascript:void(0);"> {{ $t('Dashboard.SalesRecords') }}</a>
                        </li>
                        <li v-if="isValid('CanViewCPR') || isValid('CanDraftCPR')">
                            <a v-on:click="GoTo('/paymentVoucherList', 'Sales_token', 'true','BankReceipt','true')" href="javascript:void(0);"> {{ $t('Dashboard.CustomerPayReceipt') }}</a>
                        </li>
                        <li v-if=" isValid('AdvancePayment')">
                            <a v-on:click="GoTo('/paymentVoucherList', 'Sales_token', 'true','AdvanceReceipt','false')" href="javascript:void(0);">Advance Receipt</a>
                        </li>
                        <li v-if="isValid('CanViewCustomer')">
                            <a v-on:click="GoTo('/Customer', 'Sales_token', 'true')" href="javascript:void(0);"> {{ $t('Dashboard.ClientCustomer') }}</a>
                        </li>
                        <li v-else-if="isValid('CanAddCustomer')">
                            <a v-on:click="GoTo('/addCustomer', 'Sales_token', 'true','Customer')" href="javascript:void(0);"> {{ $t('Dashboard.ClientCustomer') }}</a>
                        </li>
                        <li>
                            <a v-on:click="GoTo('/CustomerGroup', 'Sales_token', 'true','CreditNote')" href="javascript:void(0);"> {{ $t('Dashboard.CustomerGroup') }}</a>
                        </li>

                        <li v-if="isValid('CustomerInquiry')">
                            <a v-on:click="GoTo('/CommingSoon')" href="javascript:void(0);"> {{ $t('Dashboard.CustomerInquiry') }}</a>
                        </li>

                        <li v-if="isValid('CanViewServiceQuotation') || isValid('CanDraftServiceQuotation')">
                            <a v-on:click="GoTo('/SaleServiceOrder', 'Sales_token', 'true','ServiceQuotation')" href="javascript:void(0);"> {{ $t('Dashboard.Quotation') }}</a>
                        </li>
                        <li v-else-if="isValid('CanAddServiceQuotation')">
                            <a v-on:click="GoTo('/AddServiceQuotation', 'Sales_token', 'true','BankReceipt')" href="javascript:void(0);"> {{ $t('Dashboard.Quotation') }}</a>
                        </li>
                        <li v-if="isValid('CanViewServiceSaleOrder') || isValid('CanDraftServiceSaleOrder')">
                            <a v-on:click="GoTo('/SaleServiceOrder', 'Sales_token', 'true','ServiceSaleOrder')" href="javascript:void(0);"> {{ $t('Dashboard.SalesOrder') }}</a>
                        </li>
                        <li v-else-if="isValid('CanAddServiceSaleOrder')">
                            <a v-on:click="GoTo('/AddSaleServiceOrder', 'Sales_token', 'true','BankReceipt')" href="javascript:void(0);"> {{ $t('Dashboard.SalesOrder') }}</a>
                        </li>
                        <li>
                            <a v-on:click="GoTo('/SaleVerificationReport', 'Sales_token', 'true')" href="javascript:void(0);"> {{ $t('Dashboard.SaleVerificationReport') }}</a>
                        </li>
                        <li v-if="isValid('SaleReport')  ">
                            <a v-on:click="GoTo('/CommingSoon')" href="javascript:void(0);"> {{ $t('Dashboard.SalesReports') }}</a>
                        </li>
                        <li v-if="isValid('CanViewDeliveryNote')  ">
                            <a v-on:click="GoTo('/DeliveryChallan', 'Sales_token', 'true','',true)" href="javascript:void(0);"> Delivery Note</a>
                        </li>
                        <li v-if="isValid('CanChooseA4InvoiceType') || isValid('CanChooseThermalInvoiceType')">
                            <a v-on:click="GoTo('/PrintSetting', 'Sales_token', 'true','BankReceipt')" href="javascript:void(0);"> {{ $t('Dashboard.InvoiceSetup') }}</a>
                        </li>
                        <li v-if="isValid('IsPricing')">
                            <a v-on:click="GoTo('/InvoiceDefault', 'Sales_token', 'true','BankReceipt')" href="javascript:void(0);"> {{ $t('Dashboard.InvoiceDefault') }}</a>
                        </li>
                        <li>
                            <a v-on:click="GoTo('/AddListSettingSetup', 'Sales_token', 'true','BankReceipt')" href="javascript:void(0);"> {{ $t('List Setting Setup') }}</a>
                        </li>
                        <li v-if="(isValid('CreditNote')) ">
                            <a v-on:click="GoTo('/CreditNote', 'Sales_token', 'true','CreditNote',true)" href="javascript:void(0);"> {{ $t('Dashboard.CreditNote') }}</a>
                        </li>

                        <li v-if="isValid('DeliveryManagement')">
                            <a href="javascript:void(0);">
                                <i class="ti-control-record"></i>{{ $t('Dashboard.DeliveryManagement') }}<span class="menu-arrow left-has-menu">
                                    <i class="mdi mdi-chevron-right"></i>
                                </span>
                            </a>

                            <ul class="nav-second-level" aria-expanded="false">
                                <li>
                                    <a v-on:click="GoTo('/CommingSoon')" href="javascript:void(0);"> {{ $t('Dashboard.Delivery') }}</a>
                                </li>
                                <li v-if="isValid('StartOperationReport')">
                                    <a v-on:click="GoTo('/CommingSoon')" href="javascript:void(0);"> {{ $t('Dashboard.DeliveryReports') }}</a>
                                </li>
                                <li v-if="isValid('StartOperationReport')">
                                    <a v-on:click="GoTo('/CommingSoon')" href="javascript:void(0);"> {{ $t('Dashboard.DeliverySetup') }}</a>
                                </li>
                            </ul>
                        </li>

                        <li v-if="isValid('BillingManagement')">
                            <a href="javascript:void(0);">
                                <i class="ti-control-record"></i>{{ $t('Dashboard.BillingManagement') }}<span class="menu-arrow left-has-menu">
                                    <i class="mdi mdi-chevron-right"></i>
                                </span>
                            </a>

                            <ul class="nav-second-level" aria-expanded="false">
                                <li>
                                    <a v-on:click="GoTo('/CommingSoon')" href="javascript:void(0);"> {{ $t('Dashboard.Billing') }}</a>
                                </li>
                                <li v-if="isValid('StartOperationReport')">
                                    <a v-on:click="GoTo('/CommingSoon')" href="javascript:void(0);"> {{ $t('Dashboard.BillingReports') }}</a>
                                </li>
                                <li v-if="isValid('StartOperationReport')">
                                    <a v-on:click="GoTo('/CommingSoon')" href="javascript:void(0);"> {{ $t('Dashboard.BillingSetup') }}</a>
                                </li>
                            </ul>
                        </li>

                        <li v-if="isValid('ProposalManagement')">
                            <a href="javascript:void(0);">
                                <i class="ti-control-record"></i>{{ $t('Dashboard.ProposalManagement') }}<span class="menu-arrow left-has-menu">
                                    <i class="mdi mdi-chevron-right"></i>
                                </span>
                            </a>

                            <ul class="nav-second-level" aria-expanded="false">
                                <li>
                                    <a v-on:click="GoTo('/CommingSoon')" href="javascript:void(0);"> {{ $t('Dashboard.CommercialProposal') }}</a>
                                </li>
                                <li v-if="isValid('StartOperationReport')"><a v-on:click="GoTo('/CommingSoon')" href="javascript:void(0);"> {{ $t('Dashboard.ProposalReports') }}</a></li>
                                <li v-if="isValid('StartOperationReport')"><a v-on:click="GoTo('/CommingSoon')" href="javascript:void(0);"> {{ $t('Dashboard.ProposalSetup') }}</a></li>
                            </ul>
                        </li>

                        <li v-if="(isValid('InquiryManagement') && (isValid('CanViewInquiryProcess') || isValid('CanViewInquirySetup') || isValid('CanViewInquiryType') || isValid('CanViewInquiry') || isValid('CanAddInquiry') ))">
                            <a href="javascript:void(0);">
                                <i class="ti-control-record"></i>{{ $t('Dashboard.InquiryManagement') }}<span class="menu-arrow left-has-menu">
                                    <i class="mdi mdi-chevron-right"></i>
                                </span>
                            </a>

                            <ul class="nav-second-level" aria-expanded="false">

                                <li v-if="(isValid('CanViewInquiry') )">
                                    <a v-on:click="GoTo('/Inquiry', 'Inquiry Management_token', 'true')" href="javascript:void(0);"> {{ $t('Dashboard.Inquiry') }}</a>
                                </li>
                                <li v-else-if="(isValid('CanAddInquiry') )">
                                    <a v-on:click="GoTo('/AddInquiry', 'Inquiry Management_token', 'true')" href="javascript:void(0);"> {{ $t('Dashboard.Inquiry') }}</a>
                                </li>
                                <li><a v-on:click="GoTo('/CommingSoon')" href="javascript:void(0);"> {{ $t('Dashboard.InquiryReports') }}</a></li>
                                <li>
                                    <a v-on:click="GoTo('/InquiryModule', 'Inquiry Management_token', 'true')" href="javascript:void(0);"> {{ $t('InquirySetup.InquirySetup') }}</a>
                                </li>
                                <li v-if="(isValid('CanViewInquiryProcess') )">
                                    <a v-on:click="GoTo('/InquiryProcess', 'Inquiry Management_token', 'true')" href="javascript:void(0);">{{ $t('InquirySetup.InquiryProcess') }}</a>
                                </li>
                                <li v-if="(isValid('CanViewInquiryType') )">
                                    <a v-on:click="GoTo('/InquiryType', 'Inquiry Management_token', 'true')" href="javascript:void(0);"> {{ $t('InquirySetup.InquiryType') }}</a>
                                </li>
                            </ul>
                        </li>
                    </ul>
                </li>

                <!-- Create Document Side Menu Bar Code  -->
                <li v-if="createDocument">
                    <a href="javascript:void(0);">
                        <i data-feather="file" class="align-self-center menu-icon"></i><span>
                            {{ $t('Dashboard.Document') }}
                        </span><span class="menu-arrow"><i class="mdi mdi-chevron-right"></i></span>
                    </a>

                    <ul class="nav-second-level" aria-expanded="false">
                        <li v-if="IsSimpleInvoice">
                            <a v-on:click="GoTo('/AddSaleService', 'Sales_token', 'true','CreateDocument','false')" href="javascript:void(0);"> {{ $t('Dashboard.CreateDocument') }}</a>
                        </li>
                        <li v-if="!IsSimpleInvoice">
                            <a v-on:click="GoTo('/AddSaleService', 'Sales_token', 'true','CreateDocument','true')" href="javascript:void(0);"> {{ $t('Dashboard.CreateServiceDocument') }}</a>
                        </li>
                        <li v-if="IsSimpleInvoice">
                            <a v-on:click="GoTo('/SaleService', 'Sales_token', 'true', 'Document')" href="javascript:void(0);"> {{ $t('Dashboard.Document') }}</a>
                        </li>
                        <li v-if="!IsSimpleInvoice">
                            <a v-on:click="GoTo('/SaleService', 'Sales_token', 'true', 'Document')" href="javascript:void(0);"> {{ $t('Dashboard.ServiceDocument') }}</a>
                        </li>
                        <li v-if="isValid('CanViewHoldInvoices') || isValid('CanViewPaidInvoices') || isValid('CanViewCreditInvoices') ">
                            <a v-on:click="GoTo('/SaleService', 'Sales_token', 'true')" href="javascript:void(0);"> {{ $t('Dashboard.SalesRecords') }}</a>
                        </li>
                        <li v-if="isValid('CanViewCPR') || isValid('CanDraftCPR')">
                            <a v-on:click="GoTo('/paymentVoucherList', 'Sales_token', 'true','BankReceipt','false')" href="javascript:void(0);"> {{ $t('Dashboard.CustomerPayReceipt') }}</a>
                        </li>
                        <li v-if=" isValid('AdvancePayment')">
                            <a v-on:click="GoTo('/paymentVoucherList', 'Sales_token', 'true','AdvanceReceipt','false')" href="javascript:void(0);">Advance Receipt</a>
                        </li>
                        <li v-if="isValid('CanViewCustomer')">
                            <a v-on:click="GoTo('/Customer', 'Sales_token', 'true')" href="javascript:void(0);"> {{ $t('Dashboard.ClientCustomer') }}</a>
                        </li>

                        <li>
                            <a v-on:click="GoTo('/SaleVerificationReport', 'Sales_token', 'true')" href="javascript:void(0);"> {{ $t('Dashboard.SaleVerificationReport') }}</a>
                        </li>
                        <li v-if="isValid('CanChooseA4InvoiceType') || isValid('CanChooseThermalInvoiceType')">
                            <a v-on:click="GoTo('/PrintSetting', 'Sales_token', 'true','BankReceipt')" href="javascript:void(0);"> {{ $t('Dashboard.InvoiceSetup') }}</a>
                        </li>
                    </ul>
                </li>

                <!-- Promotional Offer Side Menu Bar Code  -->

                <li v-if="isValid('CanViewPromotionOffer') || isValid('PromotionOfferSetup') || isValid('PromotionOfferReport')">
                    <a href="javascript:void(0);">
                        <i data-feather="gift" class="align-self-center menu-icon"></i><span>
                            {{ $t('Dashboard.OffersPromotions') }}
                        </span><span class="menu-arrow"><i class="mdi mdi-chevron-right"></i></span>
                    </a>

                    <ul class="nav-second-level" aria-expanded="false">
                        <li v-if="isValid('CanViewPromotionOffer')">
                            <a v-on:click="GoTo('/promotion', 'Product And Inventory Management_token', 'true','StockOut')" href="javascript:void(0);"> {{ $t('Dashboard.OfferPromotion') }}</a>
                        </li>
                        <li v-if="isValid('CanViewPromotionOffer')">
                            <a v-on:click="GoTo('/Bundles', 'Product And Inventory Management_token', 'true','StockOut')" href="javascript:void(0);"> {{ $t('Dashboard.Bundles') }}</a>
                        </li>
                        <li v-if="isValid('PromotionOfferReport')">
                            <a v-on:click="GoTo('/CommingSoon')" href="javascript:void(0);"> {{ $t('Dashboard.OfferPromotionReports') }}</a>
                        </li>
                        <li v-if="isValid('PromotionOfferSetup')">
                            <a v-on:click="GoTo('/CommingSoon')" href="javascript:void(0);"> {{ $t('Dashboard.OfferPromotionSetup') }}</a>
                        </li>

                    </ul>
                </li>

                <!-- Loyality Membership Side Menu Bar Code  -->

                <li v-if="role == 'Admin'">
                    <a href="javascript:void(0);">
                        <i data-feather="circle" class="align-self-center menu-icon"></i><span>
                            {{ $t('Dashboard.LoylityMemberships') }}
                        </span><span class="menu-arrow"><i class="mdi mdi-chevron-right"></i></span>
                    </a>

                    <ul class="nav-second-level" aria-expanded="false">

                        <li>
                            <a v-on:click="GoTo('/CommingSoon')" href="javascript:void(0);"> {{ $t('Dashboard.LoylityMembership') }}</a>
                        </li>
                        <li>
                            <a v-on:click="GoTo('/CommingSoon')" href="javascript:void(0);"> {{ $t('Dashboard.Reports') }}</a>
                        </li>
                        <li>
                            <a v-on:click="GoTo('/CommingSoon')" href="javascript:void(0);"> {{ $t('Dashboard.Setup') }}</a>
                        </li>

                    </ul>
                </li>

                <!-- Inventory Management Side Menu Bar Code  -->

                <li v-if="isValid('QuickItemRegistration') ||isValid('InventoryReports') ||isValid('PriceManagement') ||isValid('InventoryCountManagement')
                    ||isValid('CanViewItem') || isValid('CanAddItem') || isValid('CanViewInventoryCount') || isValid('CanEditInventoryCount')|| isValid('CanAddInventoryCount')
                    || isValid('CanViewPromotionOffer')|| isValid('CanViewBundleOffer') || isValid('CanViewProduct')|| isValid('CanViewCategory')|| isValid('CanViewSubCategory')
                    || isValid('CanViewBrand')|| isValid('CanViewOrigin')|| isValid('CanViewSize')|| isValid('CanViewColor')|| isValid('CanViewUnit')
                    || isValid('CanViewWarrantyType')">

                    <a href="javascript:void(0);">
                        <i data-feather="package" class="align-self-center menu-icon"></i><span>
                            {{ $t('Dashboard.Inventory') }}
                        </span><span class="menu-arrow"><i class="mdi mdi-chevron-right"></i></span>
                    </a>

                    <ul class="nav-second-level" aria-expanded="false">
                        <!-- <li v-if="isValid('QuickItemRegistration')">
                            <a v-on:click="GoTo('/CommingSoon')" href="javascript:void(0);"> {{ $t('Dashboard.QuickItemRegistration') }}</a>
                        </li> -->

                        <li v-if="isValid('CanViewProduct')">
                            <a v-on:click="GoTo('/ProductMaster', 'Product And Inventory Management_token', 'true')" href="javascript:void(0);"> {{ $t('Dashboard.ProductRegistration') }}</a>
                        </li>
                        <li v-if="isValid('CanViewItem')">
                            <a v-on:click="GoTo('/products', 'Product And Inventory Management_token', 'true')" href="javascript:void(0);"> {{ $t('Dashboard.ItemRegistration') }}</a>
                        </li>
                        <li v-else-if="isValid('CanAddItem')">
                            <a v-on:click="GoTo('/addproduct', 'Product And Inventory Management_token', 'true')" href="javascript:void(0);"> {{ $t('Dashboard.ItemRegistration') }}</a>
                        </li>
                        <li v-if="isValid('CanViewInventoryCount') || isValid('CanEditInventoryCount')">
                            <a v-on:click="GoTo('/InventoryBlindList', 'Product And Inventory Management_token', 'true')" href="javascript:void(0);"> {{ $t('Dashboard.InventoryCount') }}</a>
                        </li>
                        <li v-else-if="isValid('CanAddInventoryCount')">
                            <a v-on:click="GoTo('/InventoryBlind', 'Product And Inventory Management_token', 'true')" href="javascript:void(0);"> {{ $t('Dashboard.InventoryCount') }}</a>
                        </li>


                        <li v-if="isValid('InventoryReports')">
                            <a v-on:click="GoTo('/CommingSoon')" href="javascript:void(0);"> {{ $t('Dashboard.InventoryReports') }}</a>
                        </li>
                        <li v-if="isValid('CanViewProduct')|| isValid('CanViewCategory')|| isValid('CanViewSubCategory')|| isValid('CanViewBrand')|| isValid('CanViewOrigin')|| isValid('CanViewSize')
                            || isValid('CanViewColor')|| isValid('CanViewUnit')|| isValid('CanViewWarrantyType')">

                            <a href="javascript:void(0);">
                                <i class="ti-control-record"></i>{{ $t('Dashboard.ItemSetup') }}<span class="menu-arrow left-has-menu">
                                    <i class="mdi mdi-chevron-right"></i>
                                </span>
                            </a>

                            <ul class="nav-second-level" aria-expanded="false">
                                <li v-if="isValid('CanViewCategory')">
                                    <a v-on:click="GoTo('/additemviewsetup', 'Product And Inventory Management_token', 'true')" href="javascript:void(0);"> {{ $t('Item View Setup') }}</a>
                                </li>
                                <li v-if="isValid('CanViewCategory')">
                                    <a v-on:click="GoTo('/category', 'Product And Inventory Management_token', 'true')" href="javascript:void(0);"> {{ $t('ProductManagement.Category') }}</a>
                                </li>
                                <li v-if="isValid('CanViewSubCategory')">
                                    <a v-on:click="GoTo('/subcategory', 'Product And Inventory Management_token', 'true')" href="javascript:void(0);"> {{ $t('ProductManagement.Sub-Category') }}</a>
                                </li>
                                <li v-if="isValid('CanViewBrand')">
                                    <a v-on:click="GoTo('/brand', 'Product And Inventory Management_token', 'true')" href="javascript:void(0);"> {{ $t('ProductManagement.Brand') }}</a>
                                </li>
                                <li v-if="isValid('CanViewOrigin')">
                                    <a v-on:click="GoTo('/origin', 'Product And Inventory Management_token', 'true')" href="javascript:void(0);"> {{ $t('ProductManagement.Origin') }}</a>
                                </li>
                                <li v-if="isValid('CanViewSize')">
                                    <a v-on:click="GoTo('/size', 'Product And Inventory Management_token', 'true')" href="javascript:void(0);"> {{ $t('ProductManagement.Size') }}</a>
                                </li>
                                <li v-if="isValid('CanViewColor')">
                                    <a v-on:click="GoTo('/color', 'Product And Inventory Management_token', 'true')" href="javascript:void(0);"> {{ $t('ProductManagement.Color') }}</a>
                                </li>
                                <li v-if="isValid('CanViewUnit')">
                                    <a v-on:click="GoTo('/unit', 'Product And Inventory Management_token', 'true')" href="javascript:void(0);"> {{ $t('ProductManagement.Unit') }}</a>
                                </li>
                                <li v-if="isValid('CanViewCategory')">
                                    <a v-on:click="GoTo('/ProductGroup', 'Product And Inventory Management_token', 'true')" href="javascript:void(0);"> {{ $t('ProductManagement.ProductGroup') }}</a>
                                </li>
                                <li v-if="isValid('CanViewWarrantyType')">
                                    <a v-on:click="GoTo('/WarrantyType', 'Product And Inventory Management_token', 'true')" href="javascript:void(0);"> {{ $t('ProductManagement.WarrantyType') }}</a>
                                </li>
                            </ul>

                        </li>

                        <li v-if="isValid('PriceManagement')">
                            <a href="javascript:void(0);" v-on:click="GoTo('/PriceLabeling', 'Product And Inventory Management_token', 'true')">
                                <i class="ti-control-record"></i>{{ $t('Dashboard.PriceManagement') }}


                            </a>


                        </li>
                        <li v-if="isValid('InventoryCountManagement')">
                            <a href="javascript:void(0);">
                                <i class="ti-control-record"></i>{{ $t('Dashboard.InventoryCount') }}<span class="menu-arrow left-has-menu">
                                    <i class="mdi mdi-chevron-right"></i>
                                </span>
                            </a>

                            <ul class="nav-second-level" aria-expanded="false">
                                <li>
                                    <a v-on:click="GoTo('/CommingSoon')" href="javascript:void(0);"> {{ $t('Dashboard.InventoryCount') }}</a>
                                </li>
                                <li>
                                    <a v-on:click="GoTo('/CommingSoon')" href="javascript:void(0);"> {{ $t('Dashboard.Reports') }}</a>
                                </li>
                                <li>
                                    <a v-on:click="GoTo('/CommingSoon')" href="javascript:void(0);"> {{ $t('Dashboard.Setup') }}</a>
                                </li>
                            </ul>
                        </li>
                    </ul>
                </li>

                <!-- Warehoues Management Side Menu Bar Code  -->

                <li v-if="isValid('WarehouseGatePass') ||isValid('WareHouseReport') ||isValid('CanViewWareHouse') || isValid('CanAddWareHouse') || isValid('CanViewStockIn')
                     || isValid('CanDraftStockIn')  || isValid('CanAddStockIn') || isValid('CanViewStockOut')|| isValid('CanAddStockOut')|| isValid('CanDraftStockOut')
                     || isValid('CanViewStockTransfer')|| isValid('CanDraftStockTransfer')|| isValid('CanAddStockTransfer')">

                    <a href="javascript:void(0);">
                        <i data-feather="home" class="align-self-center menu-icon"></i><span>
                            {{ $t('Dashboard.Warehouse') }}
                        </span><span class="menu-arrow"><i class="mdi mdi-chevron-right"></i></span>
                    </a>

                    <ul class="nav-second-level" aria-expanded="false">
                        <li v-if="allowBranches && !mainBranch">
                            <a v-on:click="GoTo('/BranchVouchers', 'WareHouse Management_token', 'true','IsSupplier')" href="javascript:void(0);"> Branch Vouchers</a>
                        </li>
                        <li v-if="allowBranches && mainBranch">
                            <a v-on:click="GoTo('/BranchVouchers', 'WareHouse Management_token', 'true','!IsSupplier')" href="javascript:void(0);"> Branch Vouchers</a>
                        </li>
                        <li v-if="allowBranches">
                            <a v-on:click="GoTo('/stockrequest', 'WareHouse Management_token', 'true')" href="javascript:void(0);"> Stock Request</a>
                        </li>
                        <li v-if="allowBranches">
                            <a v-on:click="GoTo('/stocktransfer', 'WareHouse Management_token', 'true')" href="javascript:void(0);"> Stock Transfer</a>
                        </li>
                        <li v-if="allowBranches">
                            <a v-on:click="GoTo('/stockreceived', 'WareHouse Management_token', 'true')" href="javascript:void(0);"> Stock Received</a>
                        </li>
                        <li v-if="isValid('CanViewWareHouse')">
                            <a v-on:click="GoTo('/warehouse', 'WareHouse Management_token', 'true')" href="javascript:void(0);"> {{ $t('Dashboard.Warehouses') }}</a>
                        </li>
                        <li v-else-if="isValid('CanAddWareHouse')">
                            <a v-on:click="GoTo('/AddWarehouse', 'WareHouse Management_token', 'true')" href="javascript:void(0);"> {{ $t('Dashboard.Warehouses') }}</a>
                        </li>
                        <li v-if="isValid('CanViewStockTransfer') || isValid('CanDraftStockTransfer')">
                            <a v-on:click="GoTo('/WareHouseTransfer', 'WareHouse Management_token', 'true')" href="javascript:void(0);"> {{ $t('Dashboard.InventoryTransfer') }}</a>
                        </li>
                        <li v-else-if="isValid('CanAddStockTransfer')">
                            <a v-on:click="GoTo('/addwareHouseTransfer', 'WareHouse Management_token', 'true')" href="javascript:void(0);"> {{ $t('Dashboard.InventoryTransfer') }}</a>
                        </li>

                        <li v-if="isValid('CanViewStockIn') || isValid('CanDraftStockIn')">
                            <a v-on:click="GoTo('/stockValue', 'WareHouse Management_token', 'true','StockIn')" href="javascript:void(0);"> {{ $t('Dashboard.InventoryAdjustment+') }}</a>
                        </li>
                        <li v-else-if="isValid('CanAddStockIn') ">
                            <a v-on:click="GoTo('/addStockValue', 'WareHouse Management_token', 'true','StockIn')" href="javascript:void(0);"> {{ $t('Dashboard.InventoryAdjustment+') }}</a>
                        </li>

                        <li v-if="isValid('CanDraftStockOut') || isValid('CanViewStockOut')">
                            <a v-on:click="GoTo('/stockValue', 'WareHouse Management_token', 'true','StockOut')" href="javascript:void(0);"> {{ $t('Dashboard.InventoryAdjustment-') }}</a>
                        </li>
                        <li v-else-if="isValid('CanAddStockOut') ">
                            <a v-on:click="GoTo('/addStockValue', 'WareHouse Management_token', 'true','StockOut')" href="javascript:void(0);"> {{ $t('Dashboard.InventoryAdjustment-') }}</a>
                        </li>
                        <li v-if="isValid('WarehouseGatePass')">
                            <a v-on:click="GoTo('/CommingSoon')" href="javascript:void(0);"> {{ $t('Dashboard.GatePass') }}</a>
                        </li>
                        <li v-if="isValid('WarehouseGatePass')">
                            <a v-on:click="GoTo('/Region')" href="javascript:void(0);">{{ $t('Dashboard.Region') }}</a>
                        </li>
                        <li v-if="isValid('WareHouseReport')">
                            <a v-on:click="GoTo('/CommingSoon')" href="javascript:void(0);"> {{ $t('Dashboard.Reports') }}</a>
                        </li>

                    </ul>
                </li>

                <!-- Purchasing Side Menu Bar Code  -->

                <li v-if="isValid('PurchaseBillingManagement') ||isValid('PurchaseDeliveryManagement') ||isValid('PurchaseReport') ||isValid('GoodReceives')
                    ||isValid('CanViewPurchaseDraft') || isValid('CanViewPurchasePost') || isValid('CanAddPurchaseInvoice') || isValid('CanViewDraftOrder')
                    || isValid('CanViewPostOrder') || isValid('CanAddPurchaseOrder')|| isValid('CanAllowOrderVersion') || isValid('CanAddPurchaseReturn')
                    || isValid('CanViewPurchaseReturn')|| isValid('CanViewSupplier')|| isValid('CanAddSupplier')|| isValid('CanViewSPR')|| isValid('CanAddSPR')
                    || isValid('CanDraftSPR')|| isValid('CanDraftExpenseBill')|| isValid('CanViewExpenseBill')|| isValid('CanAddExpenseBill')|| isValid('CanViewAutoTemplate')
                    || isValid('CanViewGoodsReceiveasDraft') || isValid('CanViewGoodsReceiveasPost')">

                    <a href="javascript:void(0);">
                        <i data-feather="plus-square" class="align-self-center menu-icon"></i><span>
                            {{ $t('Dashboard.Purchase') }}
                        </span><span class="menu-arrow"><i class="mdi mdi-chevron-right"></i></span>
                    </a>

                    <ul class="nav-second-level" aria-expanded="false">

                        <li v-if="isValid('CanViewPurchaseDraft') || isValid('CanViewPurchasePost')">
                            <a v-on:click="GoTo('/purchase', 'Purchase_token', 'true')" href="javascript:void(0);"> {{ $t('Dashboard.PurchaseInvoice') }}</a>
                        </li>
                        <li v-else-if="isValid('CanAddPurchaseInvoice')">
                            <a v-on:click="GoTo('/addpurchase', 'Purchase_token', 'true')" href="javascript:void(0);"> {{ $t('Dashboard.PurchaseInvoice') }}</a>
                        </li>
                        <li v-if="isValid('CanViewPurchaseReturn')">
                            <a v-on:click="GoTo('/PurchaseReturn', 'Purchase_token', 'true')" href="javascript:void(0);"> {{ $t('Dashboard.PurchaseReturn') }}</a>
                        </li>
                        <li v-else-if="isValid('CanAddPurchaseReturn')">
                            <a v-on:click="GoTo('/addPurchaseReturn', 'Purchase_token', 'true')" href="javascript:void(0);"> {{ $t('Dashboard.PurchaseReturn') }}</a>
                        </li>
                        <li v-if="isValid('CanViewDraftOrder') || isValid('CanViewPostOrder')|| (isValid('CanViewInProcessOrder') && isValid('CanAllowOrderVersion'))">
                            <a v-on:click="GoTo('/purchaseorder', 'Purchase_token', 'true')" href="javascript:void(0);"> {{ $t('Dashboard.PurchaseOrder') }}</a>
                        </li>
                        <li v-else-if="isValid('CanAddPurchaseOrder')">
                            <a v-on:click="GoTo('/addpurchaseorder', 'Purchase_token', 'true')" href="javascript:void(0);"> {{ $t('Dashboard.PurchaseOrder') }}</a>
                        </li>
                        <li v-if="isValid('CanViewSupplierQuotation')">
                            <a v-on:click="GoTo('/purchaseorder', 'Purchase_token', 'true', 'SupplierQuotation')" href="javascript:void(0);">{{ $t('Dashboard.SupplierQuotation') }} </a>
                        </li>
                        <li v-if="isValid('CanViewSupplier')">
                            <a v-on:click="GoTo('/supplier', 'Purchase_token', 'true')" href="javascript:void(0);"> {{ $t('Dashboard.Suppliers') }}</a>
                        </li>

                        <li v-else-if="isValid('CanAddSupplier')">
                            <a v-on:click="GoTo('/addCustomer', 'Purchase_token', 'true','Supplier')" href="javascript:void(0);"> {{ $t('Dashboard.Suppliers') }}</a>
                        </li>
                        <!-- <li v-if="isValid('CanViewAutoTemplate')">
                            <a v-on:click="GoTo('/autoPurchaseTemplate', 'Purchase_token', 'true')" href="javascript:void(0);"> {{ $t('Dashboard.AutoPurchaseTemplates') }}</a>
                        </li> -->
                        <li v-if="isValid('CanViewGoodsReceiveasDraft')">
                            <a v-on:click="GoTo('/GoodReceive', 'Purchase_token', 'true')" href="javascript:void(0);"> {{ $t('Dashboard.GoodsReceive') }}</a>
                        </li>
                        <li v-if=" isValid('CanDraftSPR')|| isValid('CanViewSPR')">
                            <a v-on:click="GoTo('/paymentVoucherList', 'Purchase_token', 'true','BankPay')" href="javascript:void(0);"> {{ $t('Dashboard.SupplierPaymentReceipt') }}</a>
                        </li>
                        <li v-else-if=" isValid('CanAddSPR')">
                            <a v-on:click="GoTo('/addPaymentVoucher', 'Purchase_token', 'true','BankPay')" href="javascript:void(0);"> {{ $t('Dashboard.SupplierPaymentReceipt') }}</a>
                        </li>
                        <li v-if=" isValid('AdvancePayment')">
                            <a v-on:click="GoTo('/paymentVoucherList', 'Purchase_token', 'true','AdvancePurchase','false')" href="javascript:void(0);">Advance Receipt</a>
                        </li>
                        <li v-if="(isValid('DebitNote')) ">
                            <a v-on:click="GoTo('/CreditNote', 'Purchase_token', 'true','DebitNote')" href="javascript:void(0);"> {{ $t('Dashboard.DebitNote') }}</a>
                        </li>
                        <li v-if="isValid('PurchaseReport')">
                            <a v-on:click="GoTo('/CommingSoon')" href="javascript:void(0);"> {{ $t('Dashboard.Reports') }}</a>
                        </li>

                        <li v-if="isValid('PurchaseDeliveryManagement') ">
                            <a href="javascript:void(0);">
                                <i class="ti-control-record"></i>{{ $t('Dashboard.DeliveryManagement') }}<span class="menu-arrow left-has-menu">
                                    <i class="mdi mdi-chevron-right"></i>
                                </span>
                            </a>

                            <ul class="nav-second-level" aria-expanded="false">
                                <li>
                                    <a v-on:click="GoTo('/CommingSoon')" href="javascript:void(0);"> {{ $t('Dashboard.Delivery') }}</a>
                                </li>
                                <li v-if="isValid('StartOperationReport')">
                                    <a v-on:click="GoTo('/CommingSoon')" href="javascript:void(0);"> {{ $t('Dashboard.GatePass') }}</a>
                                </li>
                                <li v-if="isValid('StartOperationReport')">
                                    <a v-on:click="GoTo('/CommingSoon')" href="javascript:void(0);"> {{ $t('Dashboard.Reports') }}</a>
                                </li>
                                <li v-if="isValid('StartOperationReport')">
                                    <a v-on:click="GoTo('/CommingSoon')" href="javascript:void(0);"> {{ $t('Dashboard.Setup') }}</a>
                                </li>
                            </ul>
                        </li>

                        <li v-if="isValid('PurchaseBillingManagement') ">
                            <a href="javascript:void(0);">
                                <i class="ti-control-record"></i>{{ $t('Dashboard.BillingManagement') }}<span class="menu-arrow left-has-menu">
                                    <i class="mdi mdi-chevron-right"></i>
                                </span>
                            </a>

                            <ul class="nav-second-level" aria-expanded="false">
                                <li>
                                    <a v-on:click="GoTo('/CommingSoon')" href="javascript:void(0);"> {{ $t('Dashboard.Billing') }}</a>
                                </li>
                                <li v-if="isValid('StartOperationReport')">
                                    <a v-on:click="GoTo('/CommingSoon')" href="javascript:void(0);"> {{ $t('Dashboard.Reports') }}</a>
                                </li>
                                <li v-if="isValid('StartOperationReport')">
                                    <a v-on:click="GoTo('/CommingSoon')" href="javascript:void(0);"> {{ $t('Dashboard.Setup') }}</a>
                                </li>
                            </ul>
                        </li>
                    </ul>
                </li>

                <!-- Repairing Order Menu Bar Code  -->

                <li v-if="isValid('CanViewReparingOrder') || isValid('CanViewWarrantyCategory') || isValid('CanViewDescription') || isValid('CanViewProblem') || isValid('CanViewAccessory')">

                    <a href="javascript:void(0);">
                        <i data-feather="tool" class="align-self-center menu-icon"></i><span>
                            {{ $t('Dashboard.RepairingOrder') }}
                        </span><span class="menu-arrow"><i class="mdi mdi-chevron-right"></i></span>
                    </a>

                    <ul class="nav-second-level" aria-expanded="false">

                        <li v-if="isValid('CanViewReparingOrder')">
                            <a v-on:click="GoTo('/ReparingOrder', 'Purchase_token', 'true')" href="javascript:void(0);"> {{ $t('Dashboard.RepairingOrder') }}</a>
                        </li>
                        <!-- <li v-if="isValid('CanViewReparingOrder')">
                            <a v-on:click="GoTo('/MultiReparingOrder', 'Purchase_token', 'true')" href="javascript:void(0);"> Multi Reparing Order</a>
                        </li> -->
                        <li>
                            <a v-on:click="GoTo('/CommingSoon')" href="javascript:void(0);"> {{ $t('Dashboard.Reports') }}</a>
                        </li>
                        <li v-if="isValid('CanViewWarrantyCategory') || isValid('CanViewDescription') || isValid('CanViewProblem') || isValid('CanViewAccessory')">
                            <a href="javascript:void(0);">
                                <i class="ti-control-record"></i>{{ $t('Dashboard.Setup') }}<span class="menu-arrow left-has-menu">
                                    <i class="mdi mdi-chevron-right"></i>
                                </span>
                            </a>

                            <ul class="nav-second-level" aria-expanded="false">
                                <li v-if="isValid('CanViewWarrantyCategory')">
                                    <a v-on:click="GoTo('/ReparingOrderType', 'Purchase_token', 'true', 'WarrantyCategory')" href="javascript:void(0);"> {{ $t('Dashboard.WarrantyCategory') }}</a>
                                </li>
                                <li v-if="isValid('CanViewDescription')">
                                    <a v-on:click="GoTo('/ReparingOrderType', 'Purchase_token', 'true', 'UpsDescription')" href="javascript:void(0);"> {{ $t('Dashboard.UpsDescription') }}</a>
                                </li>
                                <li v-if="isValid('CanViewProblem')">
                                    <a v-on:click="GoTo('/ReparingOrderType', 'Purchase_token', 'true', 'Problem')" href="javascript:void(0);"> {{ $t('Dashboard.Problem') }}</a>
                                </li>
                                <li v-if="isValid('CanViewAccessory')">
                                    <a v-on:click="GoTo('/ReparingOrderType', 'Purchase_token', 'true', 'AcessoryIncluded')" href="javascript:void(0);"> {{ $t('Dashboard.AcessoryIncluded') }}</a>
                                </li>

                            </ul>
                        </li>

                    </ul>
                </li>

                <!-- Account Management Menu Bar Code  -->

                <li v-if="isValid('CashManagement') || isValid('AccountGatePass') || isValid('AccountReport') || isValid('CashierManagement') || isValid('RecurringInvoices')
                    || isValid('RecurringPayments') || isValid('CanViewCheque') || isValid('CanViewJV') || isValid('CanAddJV') || isValid('CanAddOC') || isValid('CanViewOC')
                    || isValid('CanViewPettyCash') || isValid('CanAddPettyCash') || isValid('CanDraftJV') || isValid('CanDraftOC') || isValid('CanDraftPettyCash')
                    || isValid('CanViewTCRequest') || isValid('CanDraftTCRequest') || isValid('CanAddTCRequest') || isValid('CanViewTCIssue') || isValid('CanDraftTCIssue')
                    || isValid('CanAddTCIssue')|| isValid('CanViewTCAllocation')|| isValid('CanDraftTCAllocation')|| isValid('CanAddTCAllocation') || isValid('CanViewCurrency')
                     || isValid('CanViewBank')|| isValid('CanAddMonthlyCost')|| isValid('CanViewCOA')|| isValid('CanViewPaymentOption') || isValid('CanViewDenomination')
                     || isValid('CanViewVatRate') || isValid('CanViewExpenseType') || isValid('CanViewFinancialYear') ">

                    <a href="javascript:void(0);">
                        <i data-feather="credit-card" class="align-self-center menu-icon"></i><span>
                            {{ $t('Dashboard.Accounts') }}
                        </span><span class="menu-arrow"><i class="mdi mdi-chevron-right"></i></span>
                    </a>

                    <ul class="nav-second-level" aria-expanded="false">

                        <li v-if="isValid('CanDraftExpenseBill') || isValid('CanViewExpenseBill')">
                            <a v-on:click="GoTo('/PurchaseBill', 'Expenses_token', 'true')" href="javascript:void(0);"> {{ $t('Dashboard.ExpenseBills') }}</a>
                        </li>
                        <li v-else-if="isValid('CanAddExpenseBill')">
                            <a v-on:click="GoTo('/AddPurchaseBill', 'Expenses_token', 'true')" href="javascript:void(0);"> {{ $t('Dashboard.ExpenseBills') }}</a>
                        </li>
                        <li v-if=" isValid('CanViewJV') || isValid('CanDraftJV')">
                            <a v-on:click="GoTo('/journalvoucherview', 'Accounting_token', 'true','JournalVoucher')" href="javascript:void(0);"> {{ $t('Dashboard.JournalVoucher') }}</a>
                        </li>
                        <li v-else-if="isValid('CanAddJV')">
                            <a v-on:click="GoTo('/addjournalvoucher', 'Accounting_token', 'true','JournalVoucher')" href="javascript:void(0);"> {{ $t('Dashboard.JournalVoucher') }}</a>
                        </li>
                        <li v-if="isValid('CanViewOC') || isValid('CanDraftOC')">
                            <a v-on:click="GoTo('/journalvoucherview', 'Accounting_token', 'true','OpeningCash')" href="javascript:void(0);"> {{ $t('Dashboard.OpeningCash') }}</a>
                        </li>
                        <li v-else-if="isValid('CanAddOC')">
                            <a v-on:click="GoTo('/addjournalvoucher', 'Accounting_token', 'true','OpeningCash')" href="javascript:void(0);"> {{ $t('Dashboard.OpeningCash') }}</a>
                        </li>
                        <li v-if="isValid('CashManagement')">
                            <a v-on:click="GoTo('/CommingSoon')" href="javascript:void(0);"> {{ $t('Dashboard.CashManagement') }}</a>
                        </li>
                        <li v-if="isValid('CanViewCOA')">
                            <a v-on:click="GoTo('/coa', 'Accounting_token', 'true')" href="javascript:void(0);"> {{ $t('Dashboard.ChartofAccount-COA') }}</a>
                        </li>
                        <li v-if="isValid('AccountGatePass')">
                            <a v-on:click="GoTo('/CommingSoon')" href="javascript:void(0);"> {{ $t('Dashboard.GatePass') }}</a>
                        </li>
                        <li v-if="isValid('AccountReport')">
                            <a v-on:click="GoTo('/CommingSoon')" href="javascript:void(0);"> {{ $t('Dashboard.Reports') }}</a>
                        </li>
                        <li v-if="isValid('CanViewCurrency') || isValid('CanViewBank')|| isValid('CanAddMonthlyCost')|| isValid('CanViewCOA')|| isValid('CanViewPaymentOption')
                             || isValid('CanViewDenomination') || isValid('CanViewVatRate') || isValid('CanViewExpenseType') || isValid('CanViewFinancialYear') ">
                            <a href="javascript:void(0);">
                                <i class="ti-control-record"></i>{{ $t('Dashboard.Setup') }}<span class="menu-arrow left-has-menu">
                                    <i class="mdi mdi-chevron-right"></i>
                                </span>
                            </a>
                            <ul class="nav-second-level" aria-expanded="false">
                                <!-- <li>
                                    <a v-on:click="GoTo('/ExpenseType', 'Accounting_token', 'true')" href="javascript:void(0);">{{ $t('ExpenseTypes.ExpenseType') }}</a>
                                </li> -->
                                <!-- <li>
                                    <a v-on:click="GoTo('/currency', 'Accounting_token', 'true')" href="javascript:void(0);">{{ $t('Dashboard.SetupCurrency') }}</a>
                                </li> -->
                                <li v-if="isValid('CanViewCurrency')">
                                    <a v-on:click="GoTo('/currency', 'Accounting_token', 'true')" href="javascript:void(0);">{{ $t('Dashboard.SetupCurrency') }}</a>
                                </li>
                                <li v-if="isValid('CanViewBank')">
                                    <a v-on:click="GoTo('/Bank', 'Accounting_token', 'true')" href="javascript:void(0);">{{ $t('Dashboard.RegisterBank') }}</a>
                                </li>
                                <li v-if="isValid('CanAddMonthlyCost')">
                                    <a v-on:click="GoTo('/monthlycost', 'Accounting_token', 'true')" href="javascript:void(0);">{{ $t('Dashboard.SetupMonthlyCost') }}</a>
                                </li>
                                <li v-if="isValid('CanViewPaymentOption')">
                                    <a v-on:click="GoTo('/PaymentOptions', 'Accounting_token', 'true')" href="javascript:void(0);">{{ $t('Dashboard.PaymentOptions') }}</a>
                                </li>
                                <li v-if="isValid('CanViewDenomination')">
                                    <a v-on:click="GoTo('/DenominationSetup', 'Accounting_token', 'true')" href="javascript:void(0);">{{ $t('Dashboard.DenominationSetup') }}</a>
                                </li>
                                <li v-if="isValid('CanViewVatRate')">
                                    <a v-on:click="GoTo('/taxrate', 'Accounting_token', 'true')" href="javascript:void(0);">{{ $t('Dashboard.VATSetup') }}</a>
                                </li>
                                <li v-if="isValid('CanViewFinancialYear')">
                                    <a v-on:click="GoTo('/FinancialYear', 'Accounting_token', 'true')" href="javascript:void(0);">{{ $t('Dashboard.FinancialYear') }}</a>
                                </li>
                            </ul>
                        </li>
                        <li v-if="isValid('CanAddPettyCash') || isValid('CanViewPettyCash') || isValid('CanDraftPettyCash') || isValid('CanAddTCAllocation')
                            || isValid('CanViewTCAllocation') || isValid('CanDraftTCAllocation') || isValid('CanAddTCIssue') || isValid('CanViewTCIssue')
                            || isValid('CanDraftTCIssue') || isValid('CanViewTCRequest') || isValid('CanDraftTCRequest') || isValid('CanAddTCRequest')">
                            <a href="javascript:void(0);">
                                <i class="ti-control-record"></i>{{ $t('Dashboard.TemporaryPettyCash') }}<span class="menu-arrow left-has-menu">
                                    <i class="mdi mdi-chevron-right"></i>
                                </span>
                            </a>
                            <ul class="nav-second-level" aria-expanded="false">
                                <li v-if="isValid('CanViewTCRequest') || isValid('CanDraftTCRequest')">
                                    <a v-on:click="GoTo('/TemporaryCashRequest', 'Accounting_token', 'true')" href="javascript:void(0);">{{ $t('Dashboard.IssuanceRequest') }}</a>
                                </li>
                                <li v-else-if="isValid('CanAddTCRequest')">
                                    <a v-on:click="GoTo('/AddTemporaryCashRequest', 'Accounting_token', 'true')" href="javascript:void(0);">{{ $t('Dashboard.IssuanceRequest') }}</a>
                                </li>
                                <li v-if="isValid('CanViewTCIssue') || isValid('CanDraftTCIssue')">
                                    <a v-on:click="GoTo('/TemporaryCashIssue', 'Accounting_token', 'true')" href="javascript:void(0);">{{ $t('Dashboard.CashIssueance') }}</a>
                                </li>
                                <li v-else-if="isValid('CanAddTCIssue')">
                                    <a v-on:click="GoTo('/AddTemporaryCashIssue', 'Accounting_token', 'true')" href="javascript:void(0);">{{ $t('Dashboard.CashIssueance') }}</a>
                                </li>
                                <li v-if="isValid('CanViewTCAllocation') || isValid('CanDraftTCAllocation')">
                                    <a v-on:click="GoTo('/TemporaryCashAllocation', 'Accounting_token', 'true')" href="javascript:void(0);">{{ $t('Dashboard.Allocation') }}</a>
                                </li>
                                <li v-else-if="isValid('CanAddTCAllocation')">
                                    <a v-on:click="GoTo('/AddTemporaryCashAllocation', 'Accounting_token', 'true')" href="javascript:void(0);">{{ $t('Dashboard.Allocation') }}</a>
                                </li>
                                <li v-if="isValid('CanViewPettyCash') || isValid('CanDraftPettyCash')">
                                    <a v-on:click="GoTo('/paymentVoucherList', 'Accounting_token', 'true','PettyCash')" href="javascript:void(0);">{{ $t('Dashboard.PettyCash') }}</a>
                                </li>
                                <li v-else-if="isValid('CanAddPettyCash')">
                                    <a v-on:click="GoTo('/addPaymentVoucher', 'Accounting_token', 'true','PettyCash')" href="javascript:void(0);">{{ $t('Dashboard.PettyCash') }}</a>
                                </li>
                                <li>
                                    <a v-on:click="GoTo('/CommingSoon')" href="javascript:void(0);"> {{ $t('Dashboard.Reports') }}</a>
                                </li>
                                <li>
                                    <a v-on:click="GoTo('/CommingSoon')" href="javascript:void(0);"> {{ $t('Dashboard.Setup') }}</a>
                                </li>

                            </ul>
                        </li>

                        <li v-if="isValid('CashierManagement')">
                            <a href="javascript:void(0);">
                                <i class="ti-control-record"></i>{{ $t('Dashboard.CashierManagement') }}<span class="menu-arrow left-has-menu">
                                    <i class="mdi mdi-chevron-right"></i>
                                </span>
                            </a>

                            <ul class="nav-second-level" aria-expanded="false">
                                <li>
                                    <a v-on:click="GoTo('/CommingSoon')" href="javascript:void(0);"> {{ $t('Dashboard.Cashier') }}</a>
                                </li>
                                <li>
                                    <a v-on:click="GoTo('/CommingSoon')" href="javascript:void(0);"> {{ $t('Dashboard.GatePass') }}</a>
                                </li>
                                <li>
                                    <a v-on:click="GoTo('/CommingSoon')" href="javascript:void(0);"> {{ $t('Dashboard.CashierReports') }}</a>
                                </li>
                                <li>
                                    <a v-on:click="GoTo('/CommingSoon')" href="javascript:void(0);"> {{ $t('Dashboard.CashierSetup') }}</a>
                                </li>
                            </ul>
                        </li>

                        <li v-if="isValid('RecurringInvoices')">
                            <a href="javascript:void(0);">
                                <i class="ti-control-record"></i>{{ $t('Dashboard.RecurringInvoices') }}<span class="menu-arrow left-has-menu">
                                    <i class="mdi mdi-chevron-right"></i>
                                </span>
                            </a>

                            <ul class="nav-second-level" aria-expanded="false">
                                <li>
                                    <a v-on:click="GoTo('/CommingSoon')" href="javascript:void(0);"> {{ $t('Dashboard.RecurringInvoice') }}</a>
                                </li>
                                <li>
                                    <a v-on:click="GoTo('/CommingSoon')" href="javascript:void(0);"> {{ $t('Dashboard.Reports') }}</a>
                                </li>
                                <li>
                                    <a v-on:click="GoTo('/CommingSoon')" href="javascript:void(0);"> {{ $t('Dashboard.Setup') }}</a>
                                </li>

                            </ul>
                        </li>
                        <li v-if="isValid('RecurringPayments')">
                            <a href="javascript:void(0);">
                                <i class="ti-control-record"></i>{{ $t('Dashboard.RecurringPayments') }}<span class="menu-arrow left-has-menu">
                                    <i class="mdi mdi-chevron-right"></i>
                                </span>
                            </a>

                            <ul class="nav-second-level" aria-expanded="false">
                                <li>
                                    <a v-on:click="GoTo('/CommingSoon')" href="javascript:void(0);"> {{ $t('Dashboard.RecurringPayment') }}</a>
                                </li>
                                <li>
                                    <a v-on:click="GoTo('/CommingSoon')" href="javascript:void(0);"> {{ $t('Dashboard.Reports') }}</a>
                                </li>
                                <li>
                                    <a v-on:click="GoTo('/CommingSoon')" href="javascript:void(0);"> {{ $t('Dashboard.Setup') }}</a>
                                </li>

                            </ul>
                        </li>
                        <li v-if="isValid('CanViewCheque')">
                            <a href="javascript:void(0);">
                                <i class="ti-control-record"></i>{{ $t('Dashboard.Cheques&Guarantee') }}<span class="menu-arrow left-has-menu">
                                    <i class="mdi mdi-chevron-right"></i>
                                </span>
                            </a>

                            <ul class="nav-second-level" aria-expanded="false">
                                <li v-if="isValid('CanViewCheque')">
                                    <a v-on:click="GoTo('/ChequeAndGurantee', 'Accounting_token', 'true','OpeningCash')" href="javascript:void(0);"> {{ $t('Dashboard.ChequesGurantee') }}</a>
                                </li>
                                <li v-if="isValid('CanViewCheque')">
                                    <a v-on:click="GoTo('/ChequeAndGuranteeDashboard', 'Accounting_token', 'true','OpeningCash')" href="javascript:void(0);"> {{ $t('Dashboard.Dashboard') }}</a>
                                </li>
                                <li>
                                    <a v-on:click="GoTo('/CommingSoon')" href="javascript:void(0);"> {{ $t('Dashboard.Reports') }}</a>
                                </li>
                                <li>
                                    <a v-on:click="GoTo('/CommingSoon')" href="javascript:void(0);"> {{ $t('Dashboard.Setup') }}</a>
                                </li>

                            </ul>
                        </li>
                    </ul>
                </li>

                <!-- Human Resource Management Menu Bar Code  -->

                <li v-if="isValid('HRSetup') ||isValid('HRReport') ||isValid('AttendanceManagement') ||isValid('EmployeePortal') ||isValid('EmployeeTasks')
                    ||isValid('AssetsManagementI') ||isValid('DocumentManagement') ||isValid('CanViewRunPayroll') || isValid('CanAddRunPayroll') || isValid('CanDraftRunPayroll')
                    || isValid('CanViewEmployeeReg') || isValid('CanViewAllowanceType') || isValid('CanViewAllowance') || isValid('CanViewDeduction')
                    || isValid('CanViewContribution') || isValid('CanViewPayRollSchedule') || isValid('CanViewSaleryTemplate') || isValid('CanViewEmployeeSalary')
                    || isValid('CanViewLoanPayment') || isValid('CanViewSalaryTaxSlab')">

                    <a href="javascript:void(0);">
                        <i data-feather="user" class="align-self-center menu-icon"></i><span>
                            {{ $t('Dashboard.HumanResources') }}
                        </span><span class="menu-arrow"><i class="mdi mdi-chevron-right"></i></span>
                    </a>

                    <ul class="nav-second-level" aria-expanded="false">

                        <li v-if="isValid('CanViewEmployeeReg')">
                            <a v-on:click="GoTo('/employeeRegistration', 'HR And PayRoll_token', 'true')" href="javascript:void(0);"> {{ $t('Dashboard.EmployeeProfile') }}</a>
                        </li>
                        <li v-else-if="isValid('CanAddEmployeeReg')">
                            <a v-on:click="GoTo('/addEmployeeRegistration', 'HR And PayRoll_token', 'true')" href="javascript:void(0);"> {{ $t('Dashboard.EmployeeProfile') }}</a>
                        </li>
                        <li v-if="isValid('CanAddEmployeeReg')">
                            <a v-on:click="GoTo('/department', 'HR And PayRoll_token', 'true')" href="javascript:void(0);">Department</a>
                        </li>
                        <li v-if="isValid('HRReport')">
                            <a v-on:click="GoTo('/CommingSoon')" href="javascript:void(0);"> {{ $t('Dashboard.Reports') }}</a>
                        </li>
                        <li>
                            <a v-on:click="GoTo('/CommingSoon')" href="javascript:void(0);"> {{ $t('Dashboard.Setup') }}</a>
                        </li>
                        <li v-if="isValid('CanViewRunPayroll') || isValid('CanAddRunPayroll') || isValid('CanDraftRunPayroll') || isValid('CanViewAllowanceType')
                            || isValid('CanViewAllowance') || isValid('CanViewDeduction') || isValid('CanViewContribution') || isValid('CanViewPayRollSchedule')
                            || isValid('CanViewSaleryTemplate') || isValid('CanViewEmployeeSalary') || isValid('CanViewLoanPayment') || isValid('CanViewSalaryTaxSlab')">
                            <a href="javascript:void(0);">
                                <i class="ti-control-record"></i>{{ $t('Dashboard.PayrollManagement') }}<span class="menu-arrow left-has-menu">
                                    <i class="mdi mdi-chevron-right"></i>
                                </span>
                            </a>

                            <ul class="nav-second-level" aria-expanded="false">
                                <li>
                                    <a href="javascript:void(0);">
                                        <i class="ti-control-record"></i>Setup<span class="menu-arrow left-has-menu">
                                            <i class="mdi mdi-chevron-right"></i>
                                        </span>
                                    </a>

                                    <ul class="nav-second-level" aria-expanded="false">
                                        <li v-if="isValid('CanViewAllowanceType')">
                                            <a v-on:click="GoTo('/AllowanceType', 'HR And PayRoll_token', 'true')" href="javascript:void(0);"> {{ $t('Dashboard.AllowanceType') }}</a>
                                        </li>
                                        <li v-if="isValid('CanViewAllowance')">
                                            <a v-on:click="GoTo('/Allowance', 'HR And PayRoll_token', 'true')" href="javascript:void(0);"> {{ $t('Dashboard.Allowance') }}</a>
                                        </li>
                                        <li v-if="isValid('CanViewContribution')">
                                            <a v-on:click="GoTo('/Contribution', 'HR And PayRoll_token', 'true')" href="javascript:void(0);"> {{ $t('Dashboard.Contribution') }}</a>
                                        </li>
                                        <li v-if="isValid('CanViewDeduction')">
                                            <a v-on:click="GoTo('/Deduction', 'HR And PayRoll_token', 'true')" href="javascript:void(0);"> {{ $t('Dashboard.Deduction') }}</a>
                                        </li>
                                        <li v-if="isValid('CanViewSaleryTemplate')">
                                            <a v-on:click="GoTo('/SalaryTemplate', 'HR And PayRoll_token', 'true')" href="javascript:void(0);"> {{ $t('Dashboard.SalaryTemplate') }}</a>
                                        </li>
                                        <li v-if="isValid('CanViewPayRollSchedule')">
                                            <a v-on:click="GoTo('/PayrollSchedule', 'HR And PayRoll_token', 'true')" href="javascript:void(0);"> {{ $t('Dashboard.PayrollSchedule') }}</a>
                                        </li>

                                        <li v-if="isValid('CanViewEmployeeSalary')">
                                            <a v-on:click="GoTo('/EmployeeSalary', 'HR And PayRoll_token', 'true')" href="javascript:void(0);"> {{ $t('Dashboard.EmployeeSalary') }}</a>
                                        </li>
                                        <li v-if="isValid('CanViewLoanPayment')">
                                            <a v-on:click="GoTo('/LoanPayment', 'HR And PayRoll_token', 'true')" href="javascript:void(0);"> {{ $t('Dashboard.LoanPayment') }}</a>
                                        </li>
                                        <li v-if="isValid('CanViewSalaryTaxSlab')">
                                            <a v-on:click="GoTo('/SalaryTaxSlab', 'HR And PayRoll_token', 'true')" href="javascript:void(0);"> {{ $t('Dashboard.SalaryTaxSlab') }}</a>
                                        </li>

                                    </ul>
                                </li>

                                <li v-if="isValid('CanViewRunPayroll')">
                                    <a v-on:click="GoTo('/RunPayroll', 'HR And PayRoll_token', 'true')" href="javascript:void(0);"> {{ $t('Dashboard.Payroll') }}</a>
                                </li>
                                <li>
                                    <a v-on:click="GoTo('/CommingSoon')" href="javascript:void(0);"> {{ $t('Dashboard.Payslip') }}</a>
                                </li>
                                <li>
                                    <a v-on:click="GoTo('/CommingSoon')" href="javascript:void(0);"> {{ $t('Dashboard.Reports') }}</a>
                                </li>

                            </ul>
                        </li>
                        <li>
                            <a href="javascript:void(0);">
                                <i class="ti-control-record"></i>Shift Management<span class="menu-arrow left-has-menu">
                                    <i class="mdi mdi-chevron-right"></i>
                                </span>
                            </a>

                            <ul class="nav-second-level" aria-expanded="false">
                                <li>
                                    <a v-on:click="GoTo('/Shift', 'HR And PayRoll_token', 'true')" href="javascript:void(0);"> Shift</a>
                                </li>
                                <li>
                                    <a v-on:click="GoTo('/ShiftAssign', 'HR And PayRoll_token', 'true')" href="javascript:void(0);"> Shift Assign</a>
                                </li>
                                <li>
                                    <a v-on:click="GoTo('/HolidaySetup', 'HR And PayRoll_token', 'true')" href="javascript:void(0);"> Holidays</a>
                                </li>

                            </ul>
                        </li>
                        <li v-if="isValid('AttendanceManagement')">
                            <a href="javascript:void(0);">
                                <i class="ti-control-record"></i>{{ $t('Dashboard.AttendanceManagement') }}<span class="menu-arrow left-has-menu">
                                    <i class="mdi mdi-chevron-right"></i>
                                </span>
                            </a>

                            <ul class="nav-second-level" aria-expanded="false">
                                <li v-if="isValid('CanViewManualAttendance')">
                                    <a v-on:click="GoTo('/ManualAttendance', 'HR And PayRoll_token', 'true')" href="javascript:void(0);"> {{ $t('Dashboard.ManualAttendece') }}</a>
                                </li>
                                <li v-if="isValid('CanAddTodayAttendance')">
                                    <a v-on:click="GoTo('/EmployeeTodayAttendence', 'HR And PayRoll_token', 'true')" href="javascript:void(0);"> {{ $t('Dashboard.TodayAttendece') }} </a>
                                </li>
                                <li v-if="isValid('CanViewAttendanceReport')">
                                    <a v-on:click="GoTo('/AttendanceReport', 'HR And PayRoll_token', 'true')" href="javascript:void(0);"> {{ $t('Dashboard.Reports') }}</a>
                                </li>
                                <li v-if="isValid('CanAddHolidaySetup')">
                                    <a v-on:click="GoTo('/AddHolidayOfMonth', 'HR And PayRoll_token', 'true')" href="javascript:void(0);"> {{ $t('Dashboard.Setup') }}</a>
                                </li>

                            </ul>
                        </li>

                        <li v-if="isValid('EmployeePortal')">
                            <a href="javascript:void(0);">
                                <i class="ti-control-record"></i>{{ $t('Dashboard.EmployeePortal') }}<span class="menu-arrow left-has-menu">
                                    <i class="mdi mdi-chevron-right"></i>
                                </span>
                            </a>

                            <ul class="nav-second-level" aria-expanded="false">
                                <li>
                                    <a v-on:click="GoTo('/CommingSoon')" href="javascript:void(0);"> {{ $t('Dashboard.Portal') }}</a>
                                </li>
                                <li v-if="isValid('StartOperationReport')">
                                    <a v-on:click="GoTo('/CommingSoon')" href="javascript:void(0);"> {{ $t('Dashboard.Reports') }}</a>
                                </li>
                                <li v-if="isValid('StartOperationReport')">
                                    <a v-on:click="GoTo('/CommingSoon')" href="javascript:void(0);"> {{ $t('Dashboard.Setup') }}</a>
                                </li>
                            </ul>
                        </li>
                        <li v-if="isValid('EmployeeTasks')">
                            <a href="javascript:void(0);">
                                <i class="ti-control-record"></i>{{ $t('Dashboard.TasksManagement') }}<span class="menu-arrow left-has-menu">
                                    <i class="mdi mdi-chevron-right"></i>
                                </span>
                            </a>

                            <ul class="nav-second-level" aria-expanded="false">
                                <li>
                                    <a v-on:click="GoTo('/CommingSoon')" href="javascript:void(0);"> {{ $t('Dashboard.Tasks') }}</a>
                                </li>
                                <li v-if="isValid('StartOperationReport')">
                                    <a v-on:click="GoTo('/CommingSoon')" href="javascript:void(0);"> {{ $t('Dashboard.Reports') }}</a>
                                </li>
                                <li v-if="isValid('StartOperationReport')">
                                    <a v-on:click="GoTo('/CommingSoon')" href="javascript:void(0);"> {{ $t('Dashboard.Setup') }}</a>
                                </li>
                            </ul>
                        </li>
                        <li v-if="isValid('AssetsManagementI')">
                            <a href="javascript:void(0);">
                                <i class="ti-control-record"></i>{{ $t('Dashboard.AssetsManagement-I') }}<span class="menu-arrow left-has-menu">
                                    <i class="mdi mdi-chevron-right"></i>
                                </span>
                            </a>

                            <ul class="nav-second-level" aria-expanded="false">
                                <li>
                                    <a v-on:click="GoTo('/CommingSoon')" href="javascript:void(0);"> {{ $t('Dashboard.Assets') }}</a>
                                </li>
                                <li v-if="isValid('StartOperationReport')">
                                    <a v-on:click="GoTo('/CommingSoon')" href="javascript:void(0);"> {{ $t('Dashboard.Reports') }}</a>
                                </li>
                                <li v-if="isValid('StartOperationReport')">
                                    <a v-on:click="GoTo('/CommingSoon')" href="javascript:void(0);"> {{ $t('Dashboard.Setup') }}</a>
                                </li>
                            </ul>
                        </li>
                        <li v-if="isValid('DocumentManagement')">
                            <a href="javascript:void(0);">
                                <i class="ti-control-record"></i>{{ $t('Dashboard.DocumentManagement') }}<span class="menu-arrow left-has-menu">
                                    <i class="mdi mdi-chevron-right"></i>
                                </span>
                            </a>

                            <ul class="nav-second-level" aria-expanded="false">
                                <li>
                                    <a v-on:click="GoTo('/CommingSoon')" href="javascript:void(0);"> {{ $t('Dashboard.CreateDocument') }}</a>
                                </li>
                                <li v-if="isValid('StartOperationReport')">
                                    <a v-on:click="GoTo('/CommingSoon')" href="javascript:void(0);"> {{ $t('Dashboard.Reports') }}</a>
                                </li>
                                <li v-if="isValid('StartOperationReport')">
                                    <a v-on:click="GoTo('/CommingSoon')" href="javascript:void(0);"> {{ $t('Dashboard.Setup') }}</a>
                                </li>
                            </ul>
                        </li>
                    </ul>
                </li>

                <!-- Financial Management Menu Bar Code  -->

                <li v-if="isValid('NewModules')">

                    <a href="javascript:void(0);">
                        <i data-feather="bar-chart-2" class="align-self-center menu-icon"></i><span>
                            {{ $t('Dashboard.FinancialManagement') }}
                        </span><span class="menu-arrow"><i class="mdi mdi-chevron-right"></i></span>
                    </a>

                    <ul class="nav-second-level" aria-expanded="false">

                        <li>
                            <a v-on:click="GoTo('/CommingSoon')" href="javascript:void(0);"> {{ $t('Dashboard.OpeningBalances') }}</a>
                        </li>

                        <li>
                            <a v-on:click="GoTo('/CommingSoon')" href="javascript:void(0);"> {{ $t('Dashboard.CashManagement') }}</a>
                        </li>

                        <li>
                            <a v-on:click="GoTo('/CommingSoon')" href="javascript:void(0);"> {{ $t('Dashboard.Reports') }}</a>
                        </li>

                        <li>
                            <a v-on:click="GoTo('/CommingSoon')" href="javascript:void(0);"> {{ $t('Dashboard.Setup') }}</a>
                        </li>
                        <li>
                            <a href="javascript:void(0);">
                                <i class="ti-control-record"></i>{{ $t('Dashboard.AssetsManagement-II') }}<span class="menu-arrow left-has-menu">
                                    <i class="mdi mdi-chevron-right"></i>
                                </span>
                            </a>

                            <ul class="nav-second-level" aria-expanded="false">
                                <li>
                                    <a v-on:click="GoTo('/CommingSoon')" href="javascript:void(0);"> {{ $t('Dashboard.Assets') }}</a>
                                </li>
                                <li>
                                    <a v-on:click="GoTo('/CommingSoon')" href="javascript:void(0);"> {{ $t('Dashboard.Reports') }}</a>
                                </li>
                                <li>
                                    <a v-on:click="GoTo('/CommingSoon')" href="javascript:void(0);"> {{ $t('Dashboard.Setup') }}</a>
                                </li>

                            </ul>
                        </li>
                        <li>
                            <a href="javascript:void(0);">
                                <i class="ti-control-record"></i>{{ $t('Dashboard.BudgetManagement') }}<span class="menu-arrow left-has-menu">
                                    <i class="mdi mdi-chevron-right"></i>
                                </span>
                            </a>

                            <ul class="nav-second-level" aria-expanded="false">
                                <li>
                                    <a v-on:click="GoTo('/CommingSoon')" href="javascript:void(0);"> {{ $t('Dashboard.Budget') }}</a>
                                </li>
                                <li>
                                    <a v-on:click="GoTo('/CommingSoon')" href="javascript:void(0);"> {{ $t('Dashboard.Reports') }}</a>
                                </li>
                                <li>
                                    <a v-on:click="GoTo('/CommingSoon')" href="javascript:void(0);"> {{ $t('Dashboard.Setup') }}</a>
                                </li>

                            </ul>
                        </li>

                    </ul>
                </li>

                <!-- Logistic Menu Bar Code  -->

                <li v-if="isValid('CanViewTransporter') || isValid('CanAddTransporter')|| isValid('CanViewClearanceAgent')|| isValid('CanAddClearanceAgent')
                    || isValid('CanViewShippingLiner')|| isValid('CanAddShippingLiner')">

                    <a href="javascript:void(0);">
                        <i data-feather="truck" class="align-self-center menu-icon"></i><span>
                            {{ $t('Dashboard.Logistics') }}
                        </span><span class="menu-arrow"><i class="mdi mdi-chevron-right"></i></span>
                    </a>

                    <ul class="nav-second-level" aria-expanded="false">
                        <li v-if="isValid('CanViewTransporter')">
                            <a v-on:click="GoTo('/LogisticsList', 'Logistic_token', 'true','Transporter')" href="javascript:void(0);"> {{ $t('Dashboard.TransporterCargo') }}</a>
                        </li>
                        <li v-else-if="isValid('CanAddTransporter')">
                            <a v-on:click="GoTo('/AddLogistics', 'Logistic_token', 'true','Transporter')" href="javascript:void(0);"> {{ $t('Dashboard.TransporterCargo') }}</a>
                        </li>
                        <li v-if="isValid('CanViewClearanceAgent')">
                            <a v-on:click="GoTo('/LogisticsList', 'Logistic_token', 'true','ClearanceAgent')" href="javascript:void(0);"> {{ $t('Dashboard.ClearanceAgent') }}</a>
                        </li>
                        <li v-else-if="isValid('CanAddClearanceAgent')">
                            <a v-on:click="GoTo('/AddLogistics', 'Logistic_token', 'true','ClearanceAgent')" href="javascript:void(0);"> {{ $t('Dashboard.ClearanceAgent') }}</a>
                        </li>
                        <li v-if="isValid('CanViewClearanceAgent')">
                            <a v-on:click="GoTo('/LogisticsList', 'Logistic_token', 'true','ShippingLinear')" href="javascript:void(0);"> {{ $t('Dashboard.ShippingLiner') }}</a>
                        </li>
                        <li v-else-if="isValid('CanAddClearanceAgent')">
                            <a v-on:click="GoTo('/AddLogistics', 'Logistic_token', 'true','ShippingLinear')" href="javascript:void(0);"> {{ $t('Dashboard.ShippingLiner') }}</a>
                        </li>

                    </ul>
                </li>

                <!-- Import Export Menu Bar Code  -->

                <li v-if="importExportSale">

                    <a href="javascript:void(0);">
                        <i data-feather="globe" class="align-self-center menu-icon"></i><span>
                            {{ $t('Dashboard.ImportExport') }}
                        </span><span class="menu-arrow"><i class="mdi mdi-chevron-right"></i></span>
                    </a>

                    <ul class="nav-second-level" aria-expanded="false">

                        <li>
                            <a v-on:click="GoTo('/CommingSoon')" href="javascript:void(0);"> {{ $t('Dashboard.ImportExport') }}</a>
                        </li>
                        <li>
                            <a v-on:click="GoTo('/CommingSoon')" href="javascript:void(0);"> {{ $t('Dashboard.Reports') }}</a>
                        </li>
                        <li v-if="importExportSale && (isValid('CanViewStuffingLocation') || isValid('CanViewPartOfLoading') || isValid('CanViewPartOfDestination')
                            || isValid('CanViewOrderType') || isValid('CanViewService') || isValid('CanViewIncoterms') || isValid('CanViewCommodity') || isValid('CanViewCarrier')
                             || isValid('CanViewExportExw') || isValid('CanViewImportFob') || isValid('CanViewQuantityContainer'))">
                            <a href="javascript:void(0);">
                                <i class="ti-control-record"></i>{{ $t('Dashboard.Setup') }}<span class="menu-arrow left-has-menu">
                                    <i class="mdi mdi-chevron-right"></i>
                                </span>
                            </a>

                            <ul class="nav-second-level" aria-expanded="false">
                                <li v-if="isValid('CanViewStuffingLocation')">
                                    <a v-on:click="GoTo('/ImportExport', 'Sales_token', 'true', 'StuffingLocation')" href="javascript:void(0);"> {{ $t('Dashboard.StuffingLocation') }}</a>
                                </li>
                                <li v-if="isValid('CanViewPartOfLoading')">
                                    <a v-on:click="GoTo('/ImportExport', 'Sales_token', 'true', 'PortOfLoading')" href="javascript:void(0);"> {{ $t('Dashboard.PortOfLoading') }}</a>
                                </li>
                                <li v-if="isValid('CanViewPartOfDestination')">
                                    <a v-on:click="GoTo('/ImportExport', 'Sales_token', 'true', 'PortOfDestination')" href="javascript:void(0);"> {{ $t('Dashboard.PortOfDestination') }}</a>
                                </li>
                                <li v-if="isValid('CanViewOrderType')">
                                    <a v-on:click="GoTo('/ImportExport', 'Sales_token', 'true', 'OrderType')" href="javascript:void(0);"> {{ $t('Dashboard.OrderType') }}</a>
                                </li>
                                <li v-if="isValid('CanViewService')">
                                    <a v-on:click="GoTo('/ImportExport', 'Sales_token', 'true', 'Service')" href="javascript:void(0);"> {{ $t('Dashboard.Service') }}</a>
                                </li>
                                <li v-if="isValid('CanViewIncoterms')">
                                    <a v-on:click="GoTo('/ImportExport', 'Sales_token', 'true', 'Incoterms')" href="javascript:void(0);"> {{ $t('Dashboard.Incoterms') }}</a>
                                </li>
                                <li v-if="isValid('CanViewCommodity')">
                                    <a v-on:click="GoTo('/ImportExport', 'Sales_token', 'true', 'Commodity')" href="javascript:void(0);"> {{ $t('Dashboard.Commodity') }}</a>
                                </li>
                                <li v-if="isValid('CanViewCarrier')">
                                    <a v-on:click="GoTo('/ImportExport', 'Sales_token', 'true', 'Carrier')" href="javascript:void(0);"> {{ $t('Dashboard.Carrier') }}</a>
                                </li>
                                <li v-if="isValid('CanViewExportExw')">
                                    <a v-on:click="GoTo('/ImportExport', 'Sales_token', 'true', 'ExportEXW')" href="javascript:void(0);"> {{ $t('Dashboard.ExportEXW') }}</a>
                                </li>
                                <li v-if="isValid('CanViewImportFob')">
                                    <a v-on:click="GoTo('/ImportExport', 'Sales_token', 'true', 'ImportFOB')" href="javascript:void(0);"> {{ $t('Dashboard.ImportFOB') }}</a>
                                </li>
                                <li v-if="isValid('CanViewQuantityContainer')">
                                    <a v-on:click="GoTo('/ImportExport', 'Sales_token', 'true', 'QuantityContainer')" href="javascript:void(0);"> {{ $t('Dashboard.QuantityContainer') }}</a>
                                </li>

                            </ul>
                        </li>

                    </ul>
                </li>

                <!-- Production Menu Bar Code  -->

                <li v-if="(isValid('CanViewProductionRecipe') || isValid('CanViewProductionBatch') || isValid('CanViewDispatchNote') || isValid('Simple') )">

                    <a href="javascript:void(0);">
                        <i data-feather="target" class="align-self-center menu-icon"></i><span>
                            {{ $t('Dashboard.Production') }}
                        </span><span class="menu-arrow"><i class="mdi mdi-chevron-right"></i></span>
                    </a>

                    <ul class="nav-second-level" aria-expanded="false">

                        <li v-if="isValid('CanViewProductionBatch')">
                            <a v-on:click="GoTo('/ProductionBatch', 'Manufacturing And Production_token', 'true')" href="javascript:void(0);"> {{ $t('Dashboard.ProductionBatch') }}</a>
                        </li>

                        <li v-if="isValid('CanViewProductionRecipe')">
                            <a v-on:click="GoTo('/RecipeNo', 'Manufacturing And Production_token', 'true')" href="javascript:void(0);"> {{ $t('Dashboard.Recipe') }}</a>
                        </li>
                        <li v-if="isValid('CanViewProductionRecipe')">
                            <a v-on:click="GoTo('/Process', 'Manufacturing And Production_token', 'true')" href="javascript:void(0);"> {{ $t('Process') }}</a>
                        </li>
                        <li v-if="isValid('Simple')">
                            <a v-on:click="GoTo('/Bom', 'Manufacturing And Production_token', 'true')" href="javascript:void(0);"> {{ $t('BOM') }}</a>
                        </li>

                        <li v-if="isValid('CanViewDispatchNote')">
                            <a v-on:click="GoTo('/DispatchNotes', 'Manufacturing And Production_token', 'true')" href="javascript:void(0);"> {{ $t('Dashboard.DispatchNote') }}</a>
                        </li>

                    </ul>
                </li>

                <!-- Contract Management Menu Bar Code  -->

                <li v-if="isValid('NewModules')">

                    <a href="javascript:void(0);">
                        <i data-feather="file" class="align-self-center menu-icon"></i><span>
                            {{ $t('Dashboard.ContractManagement') }}
                        </span><span class="menu-arrow"><i class="mdi mdi-chevron-right"></i></span>
                    </a>

                    <ul class="nav-second-level" aria-expanded="false">

                        <li>
                            <a v-on:click="GoTo('/CommingSoon')" href="javascript:void(0);"> {{ $t('Dashboard.ContractAgreement') }}</a>
                        </li>
                        <li>
                            <a v-on:click="GoTo('/CommingSoon')" href="javascript:void(0);"> {{ $t('Dashboard.Reports') }}</a>
                        </li>
                        <li>
                            <a v-on:click="GoTo('/CommingSoon')" href="javascript:void(0);"> {{ $t('Dashboard.Setup') }}</a>
                        </li>

                    </ul>
                </li>

                <!-- Miscellaneous Menu Bar Code  -->

                <li v-if="isValid('InventoryItemLookUp') ||isValid('EmailManagement') ||isValid('SmsManagement') ||isValid('CanPrintRackBarcode') || isValid('CanPrintItemBarcode') || isValid('CanBackUpData') || isValid('CanRestoreData')">

                    <a href="javascript:void(0);">
                        <i data-feather="paperclip" class="align-self-center menu-icon"></i><span>
                            {{ $t('Dashboard.Miscellaneous') }}
                        </span><span class="menu-arrow"><i class="mdi mdi-chevron-right"></i></span>
                    </a>

                    <ul class="nav-second-level" aria-expanded="false">

                        <li v-if="isValid('CanPrintRackBarcode') || isValid('CanPrintItemBarcode')">
                            <a href="javascript:void(0);">
                                <i class="ti-control-record"></i>{{ $t('Dashboard.BarcodeManagement') }}<span class="menu-arrow left-has-menu">
                                    <i class="mdi mdi-chevron-right"></i>
                                </span>
                            </a>

                                <ul class="nav-second-level" aria-expanded="false">
                                    <li v-if="isValid('CanPrintItemBarcode')">
                                        <a v-on:click="GoTo('/MultiBarcodePrinting', 'Product And Inventory Management_token', 'true')" href="javascript:void(0);"> {{ $t('Dashboard.ItemBarcode') }}</a>
                                    </li>
                                    <li>
                                        <a v-on:click="GoTo('/MutlipleProductBarcodePrinting', 'Product And Inventory Management_token', 'true')" href="javascript:void(0);"> {{ $t('Mutliple Product Barcode Printing') }}</a>
                                    </li>
                                    <li v-if="isValid('CanPrintRackBarcode')">
                                        <a v-on:click="GoTo('/RackBarCodeCreate', 'Product And Inventory Management_token', 'true')" href="javascript:void(0);"> {{ $t('Dashboard.RacksBarcode') }}</a>
                                    </li>
                                    <li >
                                        <a v-on:click="GoTo('/ItemsBarCode', 'Product And Inventory Management_token', 'true')" href="javascript:void(0);"> {{ $t('Generate Items BarCode') }}</a>
                                    </li>
                                </ul>
                            </li>
                            <li>
                                <a v-on:click="GoTo('/CommingSoon')" href="javascript:void(0);"> {{ $t('Dashboard.PriceBarcode') }}</a>
                            </li>
                            <li>
                                <a v-on:click="GoTo('/CommingSoon')" href="javascript:void(0);"> {{ $t('Dashboard.ShelfBarcode') }}</a>
                            </li>
                            <li>
                                <a v-on:click="GoTo('/CommingSoon')" href="javascript:void(0);"> {{ $t('Dashboard.BarcodeTemplates') }}</a>
                            </li>
                            <li>
                                <a v-on:click="GoTo('/AddBarCodeSetup')" href="javascript:void(0);"> {{ $t('Dashboard.BarcodeSetup') }}</a>
                            </li>
                            <li>
                                <a v-on:click="GoTo('/CommingSoon')" href="javascript:void(0);"> {{ $t('Dashboard.Reports') }}</a>
                            </li>
                            <li v-if="isValid('CanBackUpData') || isValid('CanRestoreData')">
                                <a href="javascript:void(0);">
                                    <i class="ti-control-record"></i>{{ $t('Dashboard.CloudBackupResotre') }}<span class="menu-arrow left-has-menu">
                                        <i class="mdi mdi-chevron-right"></i>
                                    </span>
                                </a>

                            <ul class="nav-second-level" aria-expanded="false">
                                <li v-if="isValid('CanBackUpData')">
                                    <a v-on:click="GoTo('/Backup', 'Purchase_token', 'true')" href="javascript:void(0);"> {{ $t('Dashboard.DataBackup') }}</a>
                                </li>
                                <li v-if="isValid('CanRestoreData')">
                                    <a v-on:click="GoTo('/Restore', 'Purchase_token', 'true')" href="javascript:void(0);"> {{ $t('Dashboard.RestoreData') }}</a>
                                </li>

                            </ul>
                        </li>
                        <li>
                            <a v-on:click="GoTo('/CommingSoon')" href="javascript:void(0);"> {{ $t('Dashboard.InventoryItemLookup') }}</a>
                        </li>
                    </ul>
                </li>

                <!-- Emails Management Menu Bar Code  -->

                <li v-if="isValid('EmailManagement')">

                    <a href="javascript:void(0);">
                        <i data-feather="mail" class="align-self-center menu-icon"></i><span>
                            {{ $t('Dashboard.EmailManagement') }}
                        </span><span class="menu-arrow"><i class="mdi mdi-chevron-right"></i></span>
                    </a>

                    <ul class="nav-second-level" aria-expanded="false">

                        <li v-if="isValid('BasicMail') || isValid('StandardMail')">
                            <a v-on:click="GoTo('/Email')" href="javascript:void(0);"> {{ $t('Dashboard.Emails') }}</a>
                        </li>
                        <li>
                            <a v-on:click="GoTo('/CommingSoon')" href="javascript:void(0);"> {{ $t('Dashboard.Reports') }}</a>
                        </li>
                        <li>
                            <a v-on:click="GoTo('/CommingSoon')" href="javascript:void(0);"> {{ $t('Dashboard.Setup') }}</a>
                        </li>

                    </ul>
                </li>

                <!-- Sms Management Menu Bar Code  -->

                <li v-if="isValid('SmsManagement')">

                    <a href="javascript:void(0);">
                        <i data-feather="message-square" class="align-self-center menu-icon"></i><span>
                            {{ $t('Dashboard.SMSManagement') }}
                        </span><span class="menu-arrow"><i class="mdi mdi-chevron-right"></i></span>
                    </a>

                    <ul class="nav-second-level" aria-expanded="false">

                        <li>
                            <a v-on:click="GoTo('/CommingSoon')" href="javascript:void(0);"> {{ $t('Dashboard.SMS') }}</a>
                        </li>
                        <li>
                            <a v-on:click="GoTo('/CommingSoon')" href="javascript:void(0);"> {{ $t('Dashboard.Reports') }}</a>
                        </li>
                        <li>
                            <a href="javascript:void(0);">
                                <i class="ti-control-record"></i>{{ $t('Dashboard.Setup') }}<span class="menu-arrow left-has-menu">
                                    <i class="mdi mdi-chevron-right"></i>
                                </span>
                            </a>

                            <ul class="nav-second-level" aria-expanded="false">
                                <li>
                                    <a v-on:click="GoTo('/SmsSetup', 'token', 'true')" href="javascript:void(0);"> GMS Setup</a>
                                </li>

                            </ul>
                        </li>

                    </ul>
                </li>

                <!-- Report Management Menu Bar Code  -->

                <li v-if="isValid('CanViewStockReport') || isValid('CanViewSaleInvoiceReport') || isValid('CanViewPurchaseInvoiceReport') || isValid('CanViewInventoryReport')
                     || isValid('CanViewProductInventoryReport') || isValid('CanViewTrialBalance') || isValid('CanViewBalanceSheetReport') || isValid('CanViewIncomeStatementReport')
                      || isValid('CanViewCustomerLedger') || isValid('CanViewSupplieLedger') || isValid('CanViewStockValueReport') || isValid('CanViewStockAverageValue')
                      || isValid('CanViewTransactionTypeStock') || isValid('CanViewCustomerWiseProductsSale') || isValid('CanViewCustomersWiseProductSale')
                      || isValid('CanViewSupplierWiseProductsPurchase') || isValid('CanViewSuppliersWiseProductPurchase') || isValid('CanViewCustomerDiscountProducts')
                      || isValid('CanViewSupplierDiscountProducts') || isValid('CanViewProductDiscountCustomer') || isValid('CanViewProductDiscountSupplier')
                       || isValid('CanViewFreeOfCostPurchase') || isValid('CanViewFreeOfCostSale') || isValid('CanViewAccountLedger') || isValid('CanViewBanTransaction')
                        || isValid('CanViewCustomerBalance') || isValid('CanViewSupplierBalance') || isValid('CanViewVatPayableReport') || isValid('CanViewDayWiseTransactions')
                        || isValid('CanViewDayWiseReport')|| isValid('CanViewTCAllocationReport')">

                    <a href="javascript:void(0);">
                        <i data-feather="monitor" class="align-self-center menu-icon"></i><span>
                            {{ $t('Dashboard.ReportManagement') }}
                        </span><span class="menu-arrow"><i class="mdi mdi-chevron-right"></i></span>
                    </a>

                    <ul class="nav-second-level" aria-expanded="false">

                        <li v-if="isValid('CanViewStockReport') || isValid('CanViewProductInventoryReport')">
                            <a href="javascript:void(0);">
                                <i class="ti-control-record"></i>{{ $t('Dashboard.InventoryReports') }}<span class="menu-arrow left-has-menu">
                                    <i class="mdi mdi-chevron-right"></i>
                                </span>
                            </a>

                            <ul class="nav-second-level" aria-expanded="false">
                                <li v-if="isValid('CanViewStockReport')">
                                    <a v-on:click="GoTo('/StockReport', 'Reporting_token', 'true')" href="javascript:void(0);"> {{ $t('Dashboard.StockReport') }}</a>
                                </li>
                                <li v-if="isValid('CanViewProductInventoryReport')">
                                    <a v-on:click="GoTo('/ProductInventoryReport', 'Reporting_token', 'true')" href="javascript:void(0);"> {{ $t('Dashboard.ProductInventoryReport') }}</a>
                                </li>
                                <li>
                                    <a v-on:click="GoTo('/SupplierPurchaseReport', 'Reporting_token', 'true')" href="javascript:void(0);"> {{ $t('Dashboard.SupplierPurchaseReport') }}</a>
                                </li>
                                <li>
                                    <a v-on:click="GoTo('/ProductComparisonReport', 'Reporting_token', 'true')" href="javascript:void(0);"> {{ $t('Dashboard.ProductWiseAccountReports') }} </a>
                                </li>
                                <li>
                                    <a v-on:click="GoTo('/ProductLedgerReport', 'Reporting_token', 'true')" href="javascript:void(0);"> {{ $t('Dashboard.ProductLedgerReport') }}</a>
                                </li>
                                <li>
                                    <a v-on:click="GoTo('/ComparisionLedgerReport', 'Reporting_token', 'true')" href="javascript:void(0);"> {{ $t('Dashboard.ProductComparisionReport') }}</a>
                                </li>
                                <li>
                                    <a v-on:click="GoTo('/AdvanceIInventoryItem', 'Reporting_token', 'true')" href="javascript:void(0);"> {{ $t('Advance Inventory Item Report') }}</a>
                                </li>
                                <li>
                                    <a v-on:click="GoTo('/AdvanceQuantityWiseInventoryItem', 'Reporting_token', 'true')" href="javascript:void(0);"> {{ $t('Advance Quantity Wise Inventory Item') }}</a>
                                </li>
                                <li>
                                    <a v-on:click="GoTo('/AdvanceStockReport', 'Reporting_token', 'true')" href="javascript:void(0);"> {{ $t('Advance Stock Report') }}</a>
                                </li>

                            </ul>
                        </li>

                        <li v-if="isValid('CanViewSaleInvoiceReport') || isValid('CanViewPurchaseInvoiceReport') || isValid('SerialNumber')">
                            <a href="javascript:void(0);">
                                <i class="ti-control-record"></i>{{ $t('Dashboard.SalePurchaseReports') }}<span class="menu-arrow left-has-menu">
                                    <i class="mdi mdi-chevron-right"></i>
                                </span>
                            </a>

                            <ul class="nav-second-level" aria-expanded="false">
                                <li v-if="isValid('CanViewSaleInvoiceReport')">
                                    <a v-on:click="GoTo('/InvoiceReport', 'Reporting_token', 'true','Sale')" href="javascript:void(0);"> {{ $t('Dashboard.SaleInvoiceReport') }}</a>
                                </li>

                                <li v-if="isValid('CanViewPurchaseInvoiceReport')">
                                    <a v-on:click="GoTo('/InvoiceReport', 'Reporting_token', 'true','Purchase')" href="javascript:void(0);"> {{ $t('Dashboard.PurchaseInvoiceReport') }}</a>
                                </li>

                                <li>
                                    <a v-on:click="GoTo('/MonthlySalesComparisionReport', 'Reporting_token', 'true')" href="javascript:void(0);"> {{ $t('Dashboard.ComparisionReport') }}</a>
                                </li>
                                <li>
                                    <a v-on:click="GoTo('/VatExpenseReport', 'Reporting_token', 'true')" href="javascript:void(0);"> {{ $t('Dashboard.VATExpenseReport') }}</a>
                                </li>
                                <li>
                                    <a v-on:click="GoTo('/DailyExpenseReport', 'Reporting_token', 'true')" href="javascript:void(0);"> {{ $t('Dashboard.DailyExpenseReport') }}</a>
                                </li>
                                <li v-if="isValid('SerialNumber')">
                                    <a v-on:click="GoTo('/InvoiceSerialReport', 'Sales_token', 'true')" href="javascript:void(0);"> Invoice Serial Report</a>
                                </li>
                                <li>
                                    <a v-on:click="GoTo('/AdvanceSaleInvoice', 'Sales_token', 'true')" href="javascript:void(0);"> Advance Sale Invoice Report</a>
                                </li>
                                <li>
                                    <a v-on:click="GoTo('/SaleInvoiceSummary', 'Sales_token', 'true')" href="javascript:void(0);"> Sale Invoice Summary Report</a>
                                </li>
                                <li>
                                    <a v-on:click="GoTo('/PurchaseInvoiceSummary', 'Sales_token', 'true')" href="javascript:void(0);"> Purchase Invoice Summary Report</a>
                                </li>
                                <li>
                                    <a v-on:click="GoTo('/ProductSummary', 'Sales_token', 'true')" href="javascript:void(0);"> Product Summary Report</a>
                                </li>
                            </ul>
                        </li>
                        <li v-if="isValid('CanViewTrialBalance') || isValid('CanViewBalanceSheetReport') || isValid('CanViewIncomeStatementReport')">
                            <a href="javascript:void(0);">
                                <i class="ti-control-record"></i>{{ $t('Dashboard.AccountFinanceReports') }}<span class="menu-arrow left-has-menu">
                                    <i class="mdi mdi-chevron-right"></i>
                                </span>
                            </a>

                            <ul class="nav-second-level" aria-expanded="false">
                                <li v-if="isValid('CanViewTrialBalance')">
                                    <a v-on:click="GoTo('/TrialBalanceReport', 'Reporting_token', 'true')" href="javascript:void(0);"> {{ $t('Dashboard.TrialBalanceReport') }}</a>
                                </li>
                                <li>
                                    <a v-on:click="GoTo('/TrialBalanceAccountReport', 'Reporting_token', 'true')" href="javascript:void(0);"> {{ $t('Dashboard.TrialBalanceAccountReport') }}</a>
                                </li>
                                <li>
                                    <a v-on:click="GoTo('/TrialBalanceTreeReport', 'Reporting_token', 'true')" href="javascript:void(0);"> {{ $t('Dashboard.TrialBalanceDetailReport') }}</a>
                                </li>
                                <li v-if="isValid('CanViewBalanceSheetReport')">
                                    <a v-on:click="GoTo('/BalanceSheetReport', 'Reporting_token', 'true')" href="javascript:void(0);"> {{ $t('Dashboard.BalanceSheetReport') }}</a>
                                </li>
                                <li>
                                    <a v-on:click="GoTo('/AdvanceBalanceSheetReport', 'Reporting_token', 'true')" href="javascript:void(0);"> Advance Balance Sheet</a>
                                </li>
                                <li>
                                    <a v-on:click="GoTo('/MonthlyAdvanceIcomeStatementReport', 'Reporting_token', 'true')" href="javascript:void(0);"> Advance Icome Statement Report</a>
                                </li>
                                <li>
                                    <a v-on:click="GoTo('/AdvanceTrialBalance', 'Reporting_token', 'true')" href="javascript:void(0);"> Advance Trial Balance</a>
                                </li>
                                <li>
                                    <a v-on:click="GoTo('/AdvanceTrialBalanceAccount', 'Reporting_token', 'true')" href="javascript:void(0);"> Advance Trial Balance Account</a>
                                </li>


                                <li v-if="isValid('CanViewIncomeStatementReport')">
                                    <a v-on:click="GoTo('/IncomeStatementReport', 'Reporting_token', 'true')" href="javascript:void(0);"> {{ $t('Dashboard.IncomeStatementReport') }}</a>
                                </li>

                            </ul>
                        </li>
                        <li v-if="isValid('CanViewDayWiseTransactions') || isValid('CanViewDayWiseReport')">
                            <a href="javascript:void(0);">
                                <i class="ti-control-record"></i>{{ $t('Dashboard.TransactionReports') }}<span class="menu-arrow left-has-menu">
                                    <i class="mdi mdi-chevron-right"></i>
                                </span>
                            </a>

                            <ul class="nav-second-level" aria-expanded="false">
                                <li v-if="isValid('CanViewDayWiseTransactions')">
                                    <a v-on:click="GoTo('/DateWiseTransaction', 'Reporting_token', 'true')" href="javascript:void(0);"> {{ $t('Dashboard.DaywiseTransaction') }}</a>
                                </li>
                                <li v-if="isValid('CanViewDayWiseReport')">
                                    <a v-on:click="GoTo('/DayStartReport', 'Reporting_token', 'true')" href="javascript:void(0);"> {{ $t('Dashboard.DayWiseReport') }}</a>
                                </li>

                            </ul>
                        </li>
                        <li v-if="isValid('CanViewVatPayableReport') ">
                            <a href="javascript:void(0);">
                                <i class="ti-control-record"></i>{{ $t('Dashboard.VatReports') }}<span class="menu-arrow left-has-menu">
                                    <i class="mdi mdi-chevron-right"></i>
                                </span>
                            </a>

                            <ul class="nav-second-level" aria-expanded="false">

                                <li v-if="isValid('CanViewVatPayableReport')">
                                    <a v-on:click="GoTo('/VatPayableReport', 'Reporting_token', 'true')" href="javascript:void(0);"> {{ $t('Dashboard.VatPayableReport') }}</a>
                                </li>
                                <li v-if="isValid('CanViewVatPayableReport')">
                                    <a v-on:click="GoTo('/VatReturnReport', 'Reporting_token', 'true','English')" href="javascript:void(0);">Vat Detail Report</a>
                                </li>
                                <li v-if="isValid('CanViewVatPayableReport')">
                                    <a v-on:click="GoTo('/VatReturnReport', 'Reporting_token', 'true','Arabic')" href="javascript:void(0);">Vat Detail Report(Arabic)</a>
                                </li>

                                <li>
                                    <a v-on:click="GoTo('/VatMonthWiseReport', 'Reporting_token', 'true')" href="javascript:void(0);"> {{ $t('Dashboard.VatMonthWiseReport') }}</a>
                                </li>
                                <li>
                                    <a v-on:click="GoTo('/SaleMonthWiseReport', 'Reporting_token', 'true','SaleMonth')" href="javascript:void(0);"> {{ $t('Dashboard.SaleVatDetailReport') }}</a>
                                </li>
                                <li>
                                    <a v-on:click="GoTo('/SaleMonthWiseReport', 'Reporting_token', 'true','PurchaseMonth')" href="javascript:void(0);"> {{ $t('Dashboard.PurchaseVatDetailReport') }}</a>
                                </li>

                            </ul>
                        </li>
                        <li v-if="isValid('CanViewCustomerBalance') || isValid('CanViewSupplierBalance')">
                            <a href="javascript:void(0);">
                                <i class="ti-control-record"></i>{{ $t('Dashboard.BalanceReports') }}<span class="menu-arrow left-has-menu">
                                    <i class="mdi mdi-chevron-right"></i>
                                </span>
                            </a>

                            <ul class="nav-second-level" aria-expanded="false">
                                <li v-if="isValid('CanViewCustomerBalance')">
                                    <a v-on:click="GoTo('/CustomerBalanceReport', 'Reporting_token', 'true','Customer')" href="javascript:void(0);"> {{ $t('Dashboard.CustomerBalanceReport') }}</a>
                                </li>
                                <li v-if="isValid('CanViewSupplierBalance')">
                                    <a v-on:click="GoTo('/CustomerBalanceReport', 'Reporting_token', 'true','Supplier')" href="javascript:void(0);"> {{ $t('Dashboard.SupplierBalanceReport') }}</a>
                                </li>

                            </ul>
                        </li>
                        <li v-if="isValid('CanViewCustomerLedger') || isValid('CanViewSupplieLedger')|| isValid('CanViewAccountLedger')">
                            <a href="javascript:void(0);">
                                <i class="ti-control-record"></i>{{ $t('Dashboard.LedgerReports') }}<span class="menu-arrow left-has-menu">
                                    <i class="mdi mdi-chevron-right"></i>
                                </span>
                            </a>

                            <ul class="nav-second-level" aria-expanded="false">
                                <li v-if="isValid('CanViewAccountLedger')">
                                    <a v-on:click="GoTo('/AccountLedger', 'Reporting_token', 'true')" href="javascript:void(0);"> {{ $t('Dashboard.AccountLedger') }}</a>
                                </li>
                                <li v-if="isValid('CanViewAccountLedger')">
                                    <a v-on:click="GoTo('/AccountLedgerCostCenterWise', 'Reporting_token', 'true')" href="javascript:void(0);"> {{ $t('Accountledgerdetailreport.Accountledgerdetailreport') }}</a>
                                </li>
                                <li v-if="isValid('CanViewCustomerLedger')">
                                    <a v-on:click="GoTo('/CustomerLedgerReport', 'Reporting_token', 'true',true)" href="javascript:void(0);"> {{ $t('Dashboard.CustomerLedgerReport') }}</a>
                                </li>
                                <li v-if="isValid('CanViewSupplieLedger')">
                                    <a v-on:click="GoTo('/CustomerLedgerReport', 'Reporting_token', 'true',false)" href="javascript:void(0);"> {{ $t('Dashboard.SupplierLedgerReport') }}</a>
                                </li>
                                <li v-if="isValid('CanViewAccountLedger')">
                                    <a v-on:click="GoTo('/AdvanceAccountLedger', 'Reporting_token', 'true',false)" href="javascript:void(0);"> {{ $t('Advance Account Ledger') }}</a>
                                </li>
                                <li v-if="isValid('CanViewAccountLedger')">
                                    <a v-on:click="GoTo('/AdvanceAccountLedgerCostCenterWise', 'Reporting_token', 'true',false)" href="javascript:void(0);"> {{ $t('Advance Account Ledger Detail Report') }}</a>
                                </li>
                                <li v-if="isValid('CanViewCustomerLedger')">
                                    <a v-on:click="GoTo('/AdvanceCustomerLedger', 'Reporting_token', 'true',true)" href="javascript:void(0);"> {{ $t('Advance Custmer Ledger Report') }}</a>
                                </li>
                                <li v-if="isValid('CanViewCustomerLedger')">
                                    <a v-on:click="GoTo('/AdvanceCustomerLedger', 'Reporting_token', 'true',false)" href="javascript:void(0);"> {{ $t('Advance Supplier Ledger Report') }}</a>
                                </li>

                            </ul>

                        </li>
                        <li v-if="isValid('CanViewCustomerLedger') || isValid('CanViewSupplieLedger')|| isValid('CanViewAccountLedger')">
                            <a href="javascript:void(0);">
                                <i class="ti-control-record"></i>{{ $t('Customer & Supplier Advances Reports') }}<span class="menu-arrow left-has-menu">
                                    <i class="mdi mdi-chevron-right"></i>
                                </span>
                            </a>
                            <ul class="nav-second-level" aria-expanded="false">
                                <li>
                                    <a v-on:click="GoTo('/CustomerAdvancesReport', 'Reporting_token', 'true', 'CustomerAdvances')" href="javascript:void(0);"> {{ $t('Customer Advances Report') }}</a>
                                </li>
                                <li>
                                    <a v-on:click="GoTo('/CustomerAdvancesReport', 'Reporting_token', 'true','SupplierAdvances')" href="javascript:void(0);"> {{ $t('Supplier Advances Report') }}</a>
                                </li>
                                <li>
                                    <a v-on:click="GoTo('/CustomerAdvanceSummaryReport', 'Reporting_token', 'true', 'CustomerSummary')" href="javascript:void(0);"> {{ $t('Customer Advance Summary Report') }}</a>
                                </li>
                                <li>
                                    <a v-on:click="GoTo('/CustomerAdvanceSummaryReport', 'Reporting_token', 'true', 'SupplierSummary')" href="javascript:void(0);"> {{ $t('Supplier Advance Summary Report') }}</a>
                                </li>

                            </ul>
                        </li>

                    </ul>
                </li>

                <!-- System Management Menu Bar Code  -->

                <li v-if="isValid('CanViewUserRole') || isValid('CanViewUserPermission') || isValid('CanViewSignUpUser') || isValid('CanViewTerminal')
                    || isValid('CanViewPosTerminal') || isValid('CanUpdateCompanyInfo') || isValid('CanPushRecord')|| isValid('CanPullRecord')|| isValid('CanFlushDatabase')
                    || isValid('CanResetDatabase')">

                    <a href="javascript:void(0);">
                        <i data-feather="settings" class="align-self-center menu-icon"></i><span>
                            {{ $t('Dashboard.SystemManagement') }}
                        </span><span class="menu-arrow"><i class="mdi mdi-chevron-right"></i></span>
                    </a>

                    <ul class="nav-second-level" aria-expanded="false">

                        <li v-if="isValid('CanUpdateCompanyInfo')">
                            <a v-on:click="GoTo('/CompanyProfile', 'Setups And Configuration_token', 'true')" href="javascript:void(0);"> {{ $t('Dashboard.CompanyInfo') }}</a>
                        </li>
                        <li v-if="allowBranches">
                            <a v-on:click="GoTo('/Branches', 'Setups And Configuration_token', 'true')" href="javascript:void(0);"> Branches</a>
                        </li>
                        <li v-if="allowBranches">
                            <a v-on:click="GoTo('/BranchUsers', 'Setups And Configuration_token', 'true')" href="javascript:void(0);"> Branch Users</a>
                        </li>
                        <li v-if="allowBranches">
                            <a v-on:click="GoTo('/BranchesPermission', 'Setups And Configuration_token', 'true')" href="javascript:void(0);"> Branches Permission</a>
                        </li>


                        <li>
                            <a v-on:click="GoTo('/aboutUs', 'Sales_token', 'true')" href="javascript:void(0);"> {{ $t('Dashboard.LicenseInfo') }}</a>
                        </li>
                        <li>
                            <a v-on:click="GoTo('/TransactionLogs', 'Sales_token', 'true')" href="javascript:void(0);">Transaction Logs</a>
                        </li>
                        <li>
                            <a v-on:click="GoTo('/PushPullTransactionLog', 'Sales_token', 'true')" href="javascript:void(0);">Push Pull Transaction Log</a>
                        </li>

                        <li v-if="isValid('CanFlushDatabase')">
                            <a v-on:click="GoTo('/FlushDatabase', 'Setups And Configuration_token', 'true')" href="javascript:void(0);"> {{ $t('Dashboard.FlushDatabase') }}</a>
                        </li>
                        <li v-if="isValid('CanResetDatabase')">
                            <a v-on:click="SupervisorLogin" href="javascript:void(0);"> {{ $t('Dashboard.ResetDatabase') }}</a>
                        </li>
                        <li v-if="isValid('CanViewTerminal') || isValid('MachineWisePrefix') || isValid('Terminal') || isValid('CanStartDay') ||  isValid('TouchInvoiceTemplate1')">
                            <a v-on:click="GoTo('/terminal', 'DayStart_token', 'true')" href="javascript:void(0);"> {{ $t('Dashboard.SystemTerminals') }}</a>
                        </li>
                        <li v-if="isValid('CanViewPosTerminal')">
                            <a v-on:click="GoTo('/BankPosTerminal', 'DayStart_token', 'true')" href="javascript:void(0);"> {{ $t('Dashboard.BankPOSTerminals') }}</a>
                        </li>
                        <li v-if="isValid('CanViewUserPermission')">
                            <a v-on:click="GoTo('/Permissions', 'Settings And Permission_token','true')" href="javascript:void(0);"> {{ $t('Dashboard.SetupPermissions') }}</a>
                        </li>
                        <li>
                            <a v-on:click="GoTo('/DefaultSetting', 'Settings And Permission_token','true')" href="javascript:void(0);"> {{ $t('Dashboard.DefaultSettings') }}</a>
                        </li>

                        <li>
                            <a v-on:click="GoTo('/Prefixes', 'Settings And Permission_token','true')" href="javascript:void(0);"> {{ $t('Dashboard.Prefixes') }}</a>
                        </li>
                        <li v-if="isValid('CanPushRecord') || isValid('CanPullRecord')">
                            <a href="javascript:void(0);">
                                <i class="ti-control-record"></i>{{ $t('Dashboard.Synchronization') }}<span class="menu-arrow left-has-menu">
                                    <i class="mdi mdi-chevron-right"></i>
                                </span>
                            </a>

                            <ul class="nav-second-level" aria-expanded="false">
                                <li v-if="isValid('CanPushRecord')">
                                    <a v-on:click="GoTo('/PushRecords', 'Setups And Configuration_token', 'true')" href="javascript:void(0);"> {{ $t('Dashboard.PushRecords') }}</a>
                                </li>
                                <li v-if="isValid('CanPullRecord')">
                                    <a v-on:click="GoTo('/PullRecords', 'Setups And Configuration_token', 'true')" href="javascript:void(0);"> {{ $t('Dashboard.PullRecords') }}</a>
                                </li>
                                <li v-if="isValid('CanPushRecord')">
                                    <a v-on:click="GoTo('/Upload', 'Setups And Configuration_token', 'true')" href="javascript:void(0);"> {{ $t('Dashboard.ManageFile') }}</a>
                                </li>
                            </ul>
                        </li>
                        <li v-if=" isValid('CanViewUserRole') || isValid('CanViewUserPermission') || isValid('CanViewSignUpUser')">
                            <a href="javascript:void(0);">
                                <i class="ti-control-record"></i>{{ $t('Dashboard.UserSetup') }}<span class="menu-arrow left-has-menu">
                                    <i class="mdi mdi-chevron-right"></i>
                                </span>
                            </a>

                            <ul class="nav-second-level" aria-expanded="false">
                                <li v-if="isValid('CanViewSignUpUser')">
                                    <a v-on:click="GoTo('/signUp', 'Settings And Permission_token', 'true')" href="javascript:void(0);"> {{ $t('Dashboard.SignUpUser') }}</a>
                                </li>
                                <li v-if="isValid('CanViewUserRole')">
                                    <a v-on:click="GoTo('/Roles', 'Settings And Permission_token', 'true')" href="javascript:void(0);"> {{ $t('Dashboard.UserRoles') }}</a>
                                </li>
                                <!-- <li v-if="isValid('CanViewPermission')">
                                    <a v-on:click="GoTo('/CompanyOption', 'Settings And Permission_token', 'true')" href="javascript:void(0);">  {{ $t('Dashboard.CompanyOption') }}</a>
                                </li> -->
                                <li>
                                    <a v-on:click="GoTo('/UserDefineFlow', 'Settings And Permission_token', 'true')" href="javascript:void(0);"> {{ $t('Dashboard.UserDefineFlow') }}</a>
                                </li>
                            </ul>
                        </li>
                    </ul>
                </li>

            </ul>

        </div>
    </div>
    <!--end left-sidenav-->

    <div class="page-wrapper">
        <!--Top Bar Start-->
        <div class="topbar">
            <!--Navbar-->
            <nav class="navbar-custom">
                <ul class="list-unstyled topbar-nav float-end mb-0">
                    <li class="creat-btn">
                            <div class="nav-link">
                                <a class=" btn btn-sm btn-soft-primary" href="javascript:void(0);" v-on:click="LockScreen" role="button"><i class="fas fa-plus me-2"></i>Lock</a>
                            </div>                                
                        </li>

                    <li class="dropdown hide-phone">
                        <a class="nav-link dropdown-toggle arrow-none waves-light waves-effect" data-bs-toggle="dropdown" href="javascript:void(0)" role="button" aria-haspopup="false" aria-expanded="false">
                            <i data-feather="search" class="topbar-icon"></i>
                        </a>

                        <div class="dropdown-menu dropdown-menu-end dropdown-lg p-0">
                            <!-- Top Search Bar -->
                            <div class="app-search-topbar">
                                <form action="#" method="get">
                                    <input type="search" name="search" class="from-control top-search mb-0" placeholder="Type text...">
                                    <button type="submit"><i class="ti-search"></i></button>
                                </form>
                            </div>
                        </div>
                    </li>

                    <li class="dropdown notification-list">
                        
                        <a class="nav-link dropdown-toggle arrow-none waves-light waves-effect" data-bs-toggle="dropdown" href="javascript:void(0)" role="button" aria-haspopup="false" aria-expanded="false">
                            <i data-feather="bell" class="align-self-center topbar-icon"></i>
                            <span class="badge bg-danger rounded-pill noti-icon-badge">2</span>
                        </a>

                    </li>
                    <li class="dropdown" v-if="isValid('EmployeePortalActivation') && isHrPortal" hidden>
                        <a class="nav-link dropdown-toggle waves-effect waves-light nav-user pe-auto" v-on:click="PortalFunc(false)" data-bs-toggle="dropdown" href="javascript:void(0)" role="button" aria-haspopup="false" aria-expanded="false">
                            <i class="fas fa-user"></i>
                            HR Portal
                        </a>
                    </li>
                    <li class="dropdown" v-if="isValid('EmployeePortalActivation') && !isHrPortal" hidden>
                        <a class="nav-link dropdown-toggle waves-effect waves-light nav-user pe-auto" v-on:click="PortalFunc(true)" data-bs-toggle="dropdown" href="javascript:void(0)" role="button" aria-haspopup="false" aria-expanded="false">

                            <i class="fas fa-user"></i>
                            HR Portal
                        </a>
                    </li>
                    <li class="dropdown" v-if="role != 'Noble Admin' && allowBranches">
                        <a class="nav-link dropdown-toggle waves-effect waves-light nav-user" data-bs-toggle="dropdown" href="javascript:void(0)" role="button" aria-haspopup="false" aria-expanded="false">

                            <i class="fas fa-home"></i>
                            {{branchName}}
                        </a>
                        <branch-dropdown @update:modelValue="setLocalStorage" :islayout="true" />
                    </li>

                    <li class="dropdown">
                        <a class="nav-link dropdown-toggle waves-effect waves-light nav-user" data-bs-toggle="dropdown" href="javascript:void(0)" role="button" aria-haspopup="false" aria-expanded="false">

                            <i class="fas fa-globe"></i>
                            Language
                        </a>
                        <div class="dropdown-menu dropdown-menu-end">
                            <a v-if="english=='true'" @click="setLocale('en')" class="dropdown-item" href="javascript:void(0)">
                                English
                            </a>
                            <a v-if="arabic=='true'" @click="setLocale('ar')" class="dropdown-item" href="javascript:void(0)">
                                Arabic
                            </a>
                            <a v-if="portugues==true" @click="setLocale('pt')" class="dropdown-item" href="javascript:void(0)">
                                Portugues
                            </a>

                        </div>
                    </li>

                    <li class="dropdown">
                        <a class="nav-link dropdown-toggle waves-effect waves-light nav-user" data-bs-toggle="dropdown" href="javascript:void(0)" role="button" aria-haspopup="false" aria-expanded="false">
                            <span class="mx-1 nav-user-name hidden-sm">{{ DisplayUserName }}</span>
                            <img src="assets/images/users/user-5.jpg" alt="profile-user" class="rounded-circle thumb-xs" />
                        </a>
                        <div class="dropdown-menu dropdown-menu-end">
                            <a v-on:click="UserProfile" class="dropdown-item" href="javascript:void(0)">
                                <i data-feather="user" class="align-self-center icon-xs icon-dual me-1"></i> {{
                                                $t('Dashboard.MyProfile')
                                }}
                            </a>
                            <a v-on:click="UpdateCompanyPermission" v-if="role != 'Noble Admin'" class="dropdown-item" href="javascript:void(0)">
                                <i data-feather="settings" class="align-self-center icon-xs icon-dual me-1"></i> {{
                                                $t('Dashboard.SyncPermission')
                                }}
                            </a>
                            <div class="dropdown-divider mb-0"></div>
                            <a class="dropdown-item" href="javascript:void(0)" v-on:click="logout()">
                                <i data-feather="power" class="align-self-center icon-xs icon-dual me-1"></i> {{
                                                $t('Dashboard.LogOut')
                                }}
                            </a>
                        </div>
                    </li>
                </ul>

                <ul class="list-unstyled topbar-nav mb-0">
                    <li>
                        <button class="nav-link button-menu-mobile">
                            <i data-feather="menu" class="align-self-center topbar-icon"></i>
                        </button>
                    </li>

                </ul>
            </nav>
            <!--end navbar-->
        </div>
        <!--Top Bar End-->
        <!--Page Content-->
        <div class="page-content">
            <router-view @update:modelValue="logoutUser"></router-view>
            <div v-if="dashboard == '/dashboard'">
                <dashboard></dashboard>
            </div>
  
            <footer class="footer text-center text-sm-start">
                <span>
                    &copy;
                    2024 <a href="" target="_blank" class="fw-normal">Software developed by I-SAS Tech </a>
                </span>

                <span class="text-muted d-none d-sm-inline-block float-end">
                    Version 1.0 2024
                </span>
            </footer>
        </div>
    </div>
    <supervisor-login-model @close="onCloseEvent" :show="show" :isFlushData="true" :isReset="true" v-if="show" />
    <loading v-model:active="loading" :can-cancel="true" :is-full-page="true"></loading>



</template>

<script>
    import clickMixin from '@/Mixins/clickMixin'
    import axios from 'axios'
    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';


    export default {
        mixins: [clickMixin],
        name: 'locale-changer',
        components: {
            Loading
        },
        data() {
            return {
                idleTimer: null,
                isLocked: false,
                mainBranch: false,
                isHrPortal: false,
                aboutUsDetail: '',
                createDocument: false,
                loading: false,
                IsDailyExpense: false,
                noblePermissions: '',
                paymentLimitInterval: '',
                companyId: '',
                langs: ['en', 'ar', 'pt'],
                invoiveItem: false,
                invoiveBarCode: false,
                invoiveBarCodeItem: false,
                saleOrderPerm: false,
                WholeSale: '',
                purchaseOrder: false,
                isMouseover: false,
                expenseBill: false,
                IsExpenseAccount: false,
                DisplayUserName: '',
                role: '',
                dashboard: '',
                ur: '',
                isAccount: '',
                isDayStart: '',
                arabic: '',
                english: '',
                isMasterProduct: false,
                nobleRole: '',
                show: false,
                loginHistory: {
                    userId: '',
                    isLogin: false,
                    companyId: ''
                },
                dayStart: '',
                propValvue: '',
                saleMenu: false,
                inventoryMenu: false,
                wareHouseMenu: false,
                startOperationMenu: false,
                startOpSetupMenu: false,
                accountMenu: false,
                purchaseMenu: false,
                settingMenu: false,
                humanMenu: false,
                financialMenu: false,
                contractMenu: false,
                logisticMenu: false,
                importExportMenu: false,
                systemMenu: false,
                productionMenu: false,
                inquiryMenu: false,
                portugues: false,
                leftToRight: false,

                importExportSale: false,
                IsSimpleInvoice: false,
                AllSale: false,
                applicationName: '',
                branchName: '',
                allowBranches: false,
            }
        },
        methods: {
            setLocalStorage: function (value) {

                localStorage.setItem('BranchId', value.id);
                localStorage.setItem('BranchName', value.name);
                localStorage.setItem('MainBranch', value.mainBranch);
                this.branchName = value.name;
                this.$router.go();
            },
            SaleValueChange: function (value) {
                if (this.AllSale) {
                    if (value == true) {
                        localStorage.removeItem('IsSimpleInvoice');
                        localStorage.setItem('IsSimpleInvoice', true);
                    }
                    else {
                        localStorage.removeItem('IsSimpleInvoice');
                        localStorage.setItem('IsSimpleInvoice', false);
                    }
                }
            },
            PortalFunc: function (val) {
                if (val == true) {

                    localStorage.setItem('HrPortal', true)
                    this.$router.push('/StartScreen');
                    this.$router.go();
                }
                if (val == false) {

                    localStorage.setItem('HrPortal', false)
                    this.$router.push('/StartScreen');
                    this.$router.go();
                }
            },
            GoTo: function (link, token, fromDashboard, formName, fromService) {

                localStorage.setItem('IsService', fromService);

                this.$router.push({
                    path: link,
                    query: {
                        token_name: token,
                        fromDashboard: fromDashboard,
                        formName: formName,
                        fromService: fromService,
                    }
                });
            },

            StartScreen: function () {
                this.$router.push('/StartScreen')
            },
            commingSoonPage: function () {
                this.$router.push('/CommingSoon')
            },
            UpdateCompanyPermission: function () {
                this.loading = true
                var root = this;

                axios.get(root.$PermissionIp + '/NoblePermission/GetAllPermissionData?id=' + this.companyId + '&systemType=' + root.$SystemType).then(function (response) {
                    if (response.data != null) {

                        root.noblePermissions = response.data.result
                        root.SaveNoblePermissions(root.companyId)
                    }

                }).catch(error => {
                    console.log(error)
                    root.$swal.fire({
                        icon: 'error',
                        title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Something Went Wrong!' : 'هل هناك خطب ما!',
                        text: 'Please Contact to support to update license',

                        showConfirmButton: false,
                        timer: 5000,
                        timerProgressBar: true,
                    });
                    root.loading = false

                });
            },
            SaveNoblePermissions: function (locationId) {
                var root = this;
                var token = '';
                /*if (token == '') {*/
                token = localStorage.getItem('token');
                /*  }*/
                this.noblePermissions.locationId = locationId;
                if (this.noblePermissions != '' || this.noblePermissions != null || this.noblePermissions != undefined) {
                    this.$https.post('/Company/SaveNoblePermission', this.noblePermissions, {
                        headers: {
                            "Authorization": `Bearer ${token}`
                        }
                    })
                        .then(function (response) {
                            if (response.data.isSuccess == true) {

                                root.$swal({
                                    title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved!' : '!تم الحفظ',
                                    text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Update Successfully!' : 'تم التحديث بنجاح',
                                    type: 'success',
                                    icon: 'success',
                                    showConfirmButton: false,
                                    timer: 1500,
                                    timerProgressBar: true,
                                });
                                root.logout()
                            } else {
                                root.$swal({
                                    title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                                    text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Something Went Wrong!' : 'هل هناك خطب ما!',
                                    type: 'error',
                                    icon: 'error',
                                    showConfirmButton: false,
                                    timer: 1500,
                                    timerProgressBar: true,
                                });
                            }
                            root.loading = false
                        }).catch(error => {
                            console.log(error)
                            root.$swal.fire({
                                icon: 'error',
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Something Went Wrong!' : 'هل هناك خطب ما!',
                                text: error,

                                showConfirmButton: false,
                                timer: 5000,
                                timerProgressBar: true,
                            });

                            root.loading = false
                        });
                }
            },
            PaymentLimitIntervalFunc: function () {
                var root = this;
                this.paymentLimitInterval = setInterval(() => {
                    root.AddUpdateNoblePaymentLimit();
                }, 1800000);
            },
            PaymentLimitIntervalClear: function () {
                clearInterval(this.paymentLimitInterval)
                this.paymentLimitInterval = ''
            },
            AddUpdateNoblePaymentLimit: function () {
                var token = '';
                /* if (token == '') {*/
                token = localStorage.getItem('token');
                /*         }*/
                this.$https.get('/Company/AddUpdateNoblePaymentLimit', {
                    headers: {
                        "Authorization": `Bearer ${token}`
                    }
                });
            },

            AddSalaryTemplate: function () {

                var root = this;
                var token = '';
                /*if (token == '') {*/
                token = localStorage.getItem('token');
                /*}*/
                root.$https.get('/Payroll/ChecKPaySchedule', {
                    headers: {
                        "Authorization": `Bearer ${token}`
                    }
                })
                    .then(function (response) {
                        if (response.data > 0) {
                            root.$router.push('/AddRunPayroll');
                        } else {
                            root.$swal({
                                title: 'Warning!',
                                text: 'Please publish the opened payrolls before running the next payroll.',
                                type: 'warning',
                                confirmButtonClass: "btn btn-warning",
                                icon: 'warning',
                                timer: 1500,
                                timerProgressBar: true,
                            });
                        }
                    },
                        function (error) {
                            root.loading = false;
                            console.log(error);
                        });

            },
            logoutUser: function (user) {

                if (user == 'AddDailyExpensedailyexpense') {

                    user = 'DailyExpense';
                } else if (user == 'AddDailyExpense') {

                    user = 'DailyExpense';
                } else if (user == 'AddDailyExpensegeneralexpense') {

                    user = 'DailyExpense';
                } else if (user == 'InvoiceView') {

                    user = 'InvoiceView';
                } else if (user == 'WareHouseTransfers') {

                    user = 'WareHouseTransfer';
                } else if (user == 'AddStockValueStockIn') {

                    user = 'AddStockValueStockIn';
                } else if (user == 'AddStockValueStockOut') {

                    user = 'AddStockValueStockOut';
                } else if (user == 'StockValuesStockIn') {

                    user = 'StockValueStockIn';
                } else if (user == 'StockValuesStockOut') {

                    user = 'StockValueStockOut';
                } else if (user == 'AddWareHouseTransfer') {

                    user = 'AddWareHouseTransfer';
                } else if (user == 'Addpurchase') {

                    user = 'purchase';
                } else if (user == 'DailyExpenseView') {

                    user = 'DailyExpenseView';
                } else if (user == 'PurchaseView') {

                    user = 'PurchaseView';
                } else if (user == 'AddProduct') {

                    user = 'Product';
                } else if (user == 'AddSales') {

                    user = 'Sale';
                } else if (user == 'AddSale') {

                    user = 'AddSale';
                } else if (user == 'AddSaleReturns') {

                    user = 'SaleReturn';
                } else if (user == 'AddSaleReturn') {

                    user = 'AddSaleReturn';
                } else if (user == 'ViewSaleReturn') {

                    user = 'ViewSaleReturn';
                } else if (user == 'AddSaleOrders') {

                    user = 'SaleOrder';
                } else if (user == 'AddSaleOrder') {

                    user = 'AddSaleOrder';
                } else if (user == 'SaleOrderView') {

                    user = 'SaleOrderView';
                } else if (user == 'purchaseorders') {

                    user = 'purchaseorder';
                } else if (user == 'Addpurchaseorder') {

                    user = 'Addpurchaseorder';
                } else if (user == 'PurchaseOrderView') {

                    user = 'PurchaseOrderView';
                } else if (user == 'goodReceives') {

                    user = 'goodReceives';
                } else if (user == 'AddGoodReceive') {

                    user = 'AddGoodReceive';
                } else if (user == 'PurchaseReturns') {

                    user = 'PurchaseReturn';
                } else if (user == 'AddPurchaseReturn') {

                    user = 'AddPurchaseReturn';
                } else if (user == 'PurchaseReturnView') {

                    user = 'PurchaseReturnView';
                } else if (user == 'PurchaseBills') {

                    user = 'PurchaseBill';
                } else if (user == 'AddPurchaseBill') {

                    user = 'AddPurchaseBill';
                } else if (user == 'PurchaseBillView') {

                    user = 'PurchaseBillView';
                } else if (user == 'ManualAttendance') {

                    user = 'ManualAttendance';
                } else if (user == 'EmployeeTodayAttendence') {

                    user = 'EmployeeTodayAttendence';
                } else if (user == 'PaymentVoucherListsBankReceipt') {

                    user = 'PaymentVoucherListBankReceipt';
                } else if (user == 'addPaymentVoucherBankReceipt') {

                    user = 'addPaymentVoucherBankReceipt';
                } else if (user == 'PaymentVoucherViewBankReceipt') {

                    user = 'PaymentVoucherViewBankReceipt';
                } else if (user == 'PaymentVoucherListsBankPay') {

                    user = 'PaymentVoucherListBankPay';
                } else if (user == 'addPaymentVoucherBankPay') {

                    user = 'addPaymentVoucherBankPay';
                } else if (user == 'PaymentVoucherViewBankPay') {

                    user = 'PaymentVoucherViewBankPay';
                } else if (user == 'PaymentVoucherListsPettyCash') {

                    user = 'PaymentVoucherListPettyCash';
                } else if (user == 'addPaymentVoucherPettyCash') {

                    user = 'addPaymentVoucherPettyCash';
                } else if (user == 'PaymentVoucherViewPettyCash') {

                    user = 'PaymentVoucherViewPettyCash';
                } else if (user == 'JournalVoucherViewsJournalVoucher') {

                    user = 'JournalVoucherViewJournalVoucher';
                } else if (user == 'AddJournalVoucherJournalVoucher') {

                    user = 'AddJournalVoucherJournalVoucher';
                } else if (user == 'JournalVoucherViewsOpeningCash') {

                    user = 'JournalVoucherViewOpeningCash';
                } else if (user == 'AddJournalVoucherOpeningCash') {

                    user = 'AddJournalVoucherOpeningCash';
                } else if (user == 'SaleServiceOrder') {

                    user = 'SaleServiceOrder';
                } else if (user == 'AddSaleServiceOrder') {

                    user = 'AddSaleServiceOrder';
                } else if (user == 'purchase') {

                    localStorage.removeItem("fromDate");
                    localStorage.removeItem("toDate");
                    localStorage.removeItem("active");
                    localStorage.removeItem("currentPage");
                } else {
                    localStorage.removeItem("fromDate");
                    localStorage.removeItem("toDate");
                    localStorage.removeItem("active");
                    localStorage.removeItem("currentPage");
                }
                this.propValvue = user;
                this.saleMenu = false;
                this.inventoryMenu = false;
                this.wareHouseMenu = false;
                this.startOperationMenu = false;
                this.startOpSetupMenu = false;
                this.purchaseMenu = false;
                this.accountMenu = false;
                this.settingMenu = false;
                this.humanMenu = false;
                this.financialMenu = false;
                this.contractMenu = false;
                this.logisticMenu = false;
                this.importExportMenu = false;
                this.systemMenu = false;
                this.productionMenu = false;
                this.inquiryMenu = false;

                if (this.propValvue == 'AddSale' ||
                    this.propValvue == 'Customer' ||
                    this.propValvue == 'AddSaleOrder' ||
                    this.propValvue == 'SaleOrder' ||
                    this.propValvue == 'AddQuotation' ||
                    this.propValvue == 'Quotation' ||
                    this.propValvue == 'AddSaleReturn' ||
                    this.propValvue == 'SaleReturn' ||
                    this.propValvue == 'Sales' ||
                    this.propValvue == 'AddSaleService' ||
                    this.propValvue == 'SaleService' ||
                    this.propValvue == 'addPaymentVoucherBankReceipt' ||
                    this.propValvue == 'SaleServiceOrder' ||
                    this.propValvue == 'AddSaleServiceOrder' ||
                    this.propValvue == 'ServiceQuotation' ||
                    this.propValvue == 'AddServiceQuotation' ||
                    this.propValvue == 'PaymentVoucherListBankReceipt' ||
                    this.propValvue == 'PrintSetting' ||
                    this.propValvue == 'InquirySetup' ||
                    this.propValvue == 'Inquiry') {
                    this.saleMenu = true;
                }
                if (this.propValvue == 'ProductMaster' ||
                    this.propValvue == 'AddProduct' ||
                    this.propValvue == 'InventoryBlind' ||
                    this.propValvue == 'InventoryBlindList' ||
                    this.propValvue == 'AddBundles' ||
                    this.propValvue == 'Bundles' ||
                    this.propValvue == 'AddPromotion' ||
                    this.propValvue == 'Promotion' ||
                    this.propValvue == 'Product' ||
                    this.propValvue == 'Category' ||
                    this.propValvue == 'Brand' ||
                    this.propValvue == 'Origin' ||
                    this.propValvue == 'Size' ||
                    this.propValvue == 'Color' ||
                    this.propValvue == 'Unit' ||
                    this.propValvue == 'SubCategory' ||
                    this.propValvue == 'ProductManagement') {
                    this.inventoryMenu = true;
                }
                if (this.propValvue == 'AddWareHouseTransfer' ||
                    this.propValvue == 'WareHouseTransfer' ||
                    this.propValvue == 'AddStockValueStockIn' ||
                    this.propValvue == 'StockValueStockIn' ||
                    this.propValvue == 'StockValueStockOut' ||
                    this.propValvue == 'AddStockValueStockOut' ||
                    this.propValvue == 'AddWarehouse' ||
                    this.propValvue == 'Warehouse') {
                    this.wareHouseMenu = true;
                }
                if (this.propValvue == 'StartOperation' ||
                    this.propValvue == 'StartOperationSetup' ||
                    this.propValvue == 'StartOperationReport') {
                    this.startOperationMenu = true;
                }
                if (this.propValvue == 'UserSetup' ||
                    this.propValvue == 'CounterSetup' ||
                    this.propValvue == 'AdditionalSetup') {
                    this.startOpSetupMenu = true;
                }
                if (this.propValvue == 'Addpurchase' ||
                    this.propValvue == 'purchase' ||
                    this.propValvue == 'Addpurchaseorder' ||
                    this.propValvue == 'GoodReceive' ||
                    this.propValvue == 'goodReceives' ||
                    this.propValvue == 'AddGoodReceive' ||
                    this.propValvue == 'AddPurchaseReturn' ||
                    this.propValvue == 'PurchaseReturn' ||
                    this.propValvue == 'AddAutoPurchaseTemplate' ||
                    this.propValvue == 'autoPurchaseTemplate' ||
                    this.propValvue == 'AddSupplier' ||
                    this.propValvue == 'Supplier' ||
                    this.propValvue == 'addPaymentVoucherBankPay' ||
                    this.propValvue == 'PaymentVoucherListBankPay' ||
                    this.propValvue == 'purchaseorder') {
                    this.purchaseMenu = true;
                }
                if (this.propValvue == 'addPaymentVoucherPettyCash' ||
                    this.propValvue == 'AddJournalVoucherJournalVoucher' ||
                    this.propValvue == 'JournalVoucherViewJournalVoucher' ||
                    this.propValvue == 'AddJournalVoucherOpeningCash' ||
                    this.propValvue == 'JournalVoucherViewOpeningCash' ||
                    this.propValvue == 'AddTemporaryCashRequest' ||
                    this.propValvue == 'TemporaryCashRequest' ||
                    this.propValvue == 'AddTemporaryCashIssue' ||
                    this.propValvue == 'TemporaryCashIssue' ||
                    this.propValvue == 'TemporaryCashAllocation' ||
                    this.propValvue == 'AddTemporaryCashAllocation' ||
                    this.propValvue == 'PaymentVoucherListPettyCash' ||
                    this.propValvue == 'Currency' ||
                    this.propValvue == 'Bank' ||
                    this.propValvue == 'AddBank' ||
                    this.propValvue == 'MonthlyCost' ||
                    this.propValvue == 'coa' ||
                    this.propValvue == 'PaymentOptions' ||
                    this.propValvue == 'DenominationSetup' ||
                    this.propValvue == 'TaxRate' ||
                    this.propValvue == 'ExpenseType' ||
                    this.propValvue == 'FinancialSetup') {
                    this.accountMenu = true;
                }
                if (this.propValvue == 'ProductMaster' ||
                    this.propValvue == 'AddSignUp' ||
                    this.propValvue == 'SignUp' ||
                    this.propValvue == 'Roles' ||
                    this.propValvue == 'Permissions' ||
                    this.propValvue == 'UserSetup') {
                    this.settingMenu = true;
                }
                if (this.propValvue == 'EmployeeRegistration' ||
                    this.propValvue == 'AddEmployeeRegistration' ||
                    this.propValvue == 'City' ||
                    this.propValvue == 'GeographicalSetup' ||
                    this.propValvue == 'Region') {
                    this.humanMenu = true;
                }
                if (this.propValvue == 'EmployeeRegistration' ||
                    this.propValvue == 'AddEmployeeRegistration' ||
                    this.propValvue == 'City' ||
                    this.propValvue == 'GeographicalSetup' ||
                    this.propValvue == 'Region') {
                    this.financialMenu = true;
                }
                if (this.propValvue == 'EmployeeRegistration' ||
                    this.propValvue == 'AddEmployeeRegistration' ||
                    this.propValvue == 'City' ||
                    this.propValvue == 'GeographicalSetup' ||
                    this.propValvue == 'Region') {
                    this.contractMenu = true;
                }
                if (this.propValvue == 'ManualAttendance') {
                    this.humanMenu = true;
                }
                if (this.propValvue == 'EmployeeTodayAttendence') {
                    this.humanMenu = true;
                }
                if (this.propValvue == 'AttendanceReport') {
                    this.humanMenu = true;
                }
                if (this.propValvue == 'AddHolidayOfMonth') {
                    this.humanMenu = true;
                }

                if (this.propValvue == 'LogisticsListTransporter' ||
                    this.propValvue == 'AddLogisticsTransporter' ||
                    this.propValvue == 'LogisticsListClearanceAgent' ||
                    this.propValvue == 'AddLogisticsClearanceAgent' ||
                    this.propValvue == 'AddLogisticsShippingLinear' ||
                    this.propValvue == 'LogisticsListShippingLinear') {
                    this.logisticMenu = true;
                }
                //if (this.propValvue == 'LogisticsListTransporter'
                //    || this.propValvue == 'AddLogisticsTransporter'
                //    || this.propValvue == 'LogisticsListClearanceAgent'
                //    || this.propValvue == 'AddLogisticsClearanceAgent'
                //    || this.propValvue == 'AddLogisticsShippingLinear'
                //    || this.propValvue == 'LogisticsListShippingLinear') {
                //    this.importExportMenu = true;
                //}
                if (this.propValvue == 'CompanyProfile' ||
                    this.propValvue == 'Restore' ||
                    this.propValvue == 'PushRecords' ||
                    this.propValvue == 'PullRecords' ||
                    this.propValvue == 'FlushDatabase' ||
                    this.propValvue == 'Terminal' ||
                    this.propValvue == 'BankPosTerminal' ||
                    this.propValvue == 'MultiBarcodePrinting' ||
                    this.propValvue == 'RackBarcodeCreate' ||
                    this.propValvue == 'ApplicationUpdate' ||
                    this.propValvue == 'Backup' || this.propValvue == 'aboutUs') {
                    this.systemMenu = true;
                }
                if (this.propValvue == 'SaleOrder' ||
                    this.propValvue == 'AddRecipeNo' ||
                    this.propValvue == 'RecipeNo' ||
                    this.propValvue == 'AddProductionBatch' ||
                    this.propValvue == 'ProductionBatch' ||
                    this.propValvue == 'AddDispatchNote' ||
                    this.propValvue == 'DispatchNotes' ||
                    this.propValvue == 'AddSaleOrder') {
                    this.productionMenu = true;
                }

                localStorage.removeItem("BarcodeScan");
            },
            SupervisorLogin: function () {
                this.show = true;
            },
            CloseLockScreen: function (x) {
                alert(x);
                this.isLocked = x;
            },
            onCloseEvent: function (flag) {
                if (flag) {
                    this.flush()
                }
                this.show = false
            },
            flush: function () {

                var root = this;
                this.loading = true;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.loading = true;
                var records = 'Reset'
                root.$https
                    .get('/System/FlushRecords?records=' + records, {
                        headers: {
                            "Authorization": `Bearer ${token}`
                        }
                    }).then(function (response) {
                        if (response.status == 200) {
                            root.logout();
                            root.$swal({
                                title: "Success!",
                                text: "Flush data successfully",
                                type: 'error',
                                confirmButtonClass: "btn btn-Success",
                                buttonStyling: false,

                            });

                        }
                        root.loading = false;

                    }).catch(error => {
                        console.log(error)
                        root.$swal.fire({
                            icon: 'error',
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Something Went Wrong!' : 'هل هناك خطب ما!',
                            text: error.response.data,
                            showConfirmButton: false,
                            timer: 5000,
                            timerProgressBar: true,
                        });
                        root.loading = false;
                    });
            },
            setLocale(locale) {

                this.$i18n.locale = locale;
                //if (locale == 'en') {
                //    language.use(en)
                //} else if (locale == 'pt') {
                //    language.use(pt)
                //} else {
                //    language.use(ar)
                //}

                localStorage.setItem('locales', locale);
                this.$router.go(this.$router.currentRoute.fullPath)

            },

            UserProfile: function () {
                this.$router.push('/RegisterUser');
            },
            logoutHistorySave: function () {

                this.loginHistory.userId = localStorage.getItem('UserID')
                this.loginHistory.companyId = localStorage.getItem('CompanyID')
                this.$https.post('/account/LoginHistory', this.loginHistory).then(function (response) {
                    if (response.data == 1)
                        console.log('Logout History save done');
                    else
                        console.log('Logout History not save due to some error ' + response.data);
                });
            },
            logout: function () {
                var root = this;
                //  var Swal = this.$swal
                var url = '/account/logout';
                this.$https.post(url, this.login).then(function (response) {

                    if (response.data == "Success") {
                        root.PaymentLimitIntervalClear();
                        root.logoutHistorySave();

                        var getLocale = localStorage.getItem('locales');

                        root.$store.logout();
                        localStorage.clear();

                        localStorage.setItem('locales', getLocale);

                        root.$router.push('/')

                    } else {
                        root.$swal.fire({
                            icon: 'error',
                            title: 'Error Logging Out'
                        });
                    }

                });

            },
            
            GetAboutUsDetail: function () {
                var root = this;
                var token = '';
                /*if (token == '') {*/
                token = localStorage.getItem('token');
                /* }*/
                root.$https.get('Company/AboutUs', {
                    headers: {
                        "Authorization": `Bearer ${token}`
                    }
                }).then(function (response) {
                    if (response.data.isSuccess) {
                        root.aboutUsDetail = response.data.message
                        root.loading = false;
                    }
                    root.loading = false;
                });
            },
            LockScreen:function(){
                this.$router.push({ path: 'LockScreen' });
            },
            resetIdleTimer() {
                clearTimeout(this.idleTimer);
                this.idleTimer = setTimeout(() =>  
                               this.$router.push({ path: 'LockScreen' })
             , 900000); 
                },
        },
        beforeUnmount() {
            window.removeEventListener('mousemove', this.resetIdleTimer);
            window.removeEventListener('keypress', this.resetIdleTimer);
            clearTimeout(this.idleTimer);
        },
        created() {
            /*this.GetAboutUsDetail()*/
            this.portugues = localStorage.getItem('Portugues') == "true" ? true : false;
            this.leftToRight = localStorage.getItem('LeftToRight') == "true" ? true : false;
            this.isHrPortal = localStorage.getItem('HrPortal') == "true" ? true : false;
            this.applicationName = localStorage.getItem('ApplicationName')
            this.resetIdleTimer();
            window.addEventListener('mousemove', this.resetIdleTimer);
            window.addEventListener('keypress', this.resetIdleTimer); 
               },
        mounted: function () {
            this.isLocked = localStorage.getItem('lockScreen')=='true'?true:false;
            this.companyId = localStorage.getItem('CompanyID');

            /*if (token == '') {*/

            this.IsExpenseAccount = localStorage.getItem('IsExpenseAccount') == 'true' ? true : false;
            this.createDocument = localStorage.getItem('CreateDocument') == 'true' ? true : false;

            this.invoiveItem = localStorage.getItem('invoiveItem') == "true" ? true : false;
            this.invoiveBarCode = localStorage.getItem('invoiveBarCode') == "true" ? true : false;
            this.invoiveBarCodeItem = localStorage.getItem('invoiveBarCodeItem') == "true" ? true : false;
            this.saleOrderPerm = localStorage.getItem('saleOrderPerm') == "true" ? true : false;
            this.dayStart = localStorage.getItem('DayStart') == "true" ? true : false;
            this.isMasterProduct = localStorage.getItem('IsMasterProductPermission') == 'true' ? true : false;
            this.IsDailyExpense = localStorage.getItem('IsDailyExpense') == 'true' ? true : false;
            this.expenseBill = localStorage.getItem('expenseBill') == 'true' ? true : false;
            this.importExportSale = localStorage.getItem('ImportExportSale') == 'true' ? true : false;

            this.WholeSale = localStorage.getItem('BankDetail');

            this.DisplayUserName = localStorage.getItem('UserName');
            this.purchaseOrder = localStorage.getItem('PurchaseOrder') == 'true' ? true : false;
            this.role = localStorage.getItem('RoleName');
            this.isAccount = localStorage.getItem('isAccount');
            this.dashboard = this.$router.options.routes;

            this.isDayStart = localStorage.getItem('DayStart');
            this.nobleRole = localStorage.getItem('NobleRole');


            /*   if (!token == '') {*/
            /*  this.$router.push('/')*/

            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
            this.IsSimpleInvoice = localStorage.getItem('IsSimpleInvoice') == 'true' ? true : false;
            this.AllSale = localStorage.getItem('AllSale') == 'true' ? true : false;

            this.branchName = localStorage.getItem('BranchName');
            this.allowBranches = localStorage.getItem('AllowBranches') == 'true' ? true : false;
            this.mainBranch = localStorage.getItem('MainBranch') == 'true' ? true : false;

        },

    }
</script>
