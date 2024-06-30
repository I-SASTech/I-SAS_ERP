<template>
    <div class="row" v-if="isValid('CanDraftCPR') || isValid('CanViewCPR') || isValid('CanViewSPR') || isValid('CanDraftSPR') || isValid('CanViewPettyCash') || isValid('CanDraftPettyCash')">

        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">Branch Vouchers</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('PaymentVoucherList.Home') }}</a></li>
                                    <li class="breadcrumb-item active">
                                        Branch Vouchers
                                    </li>
                                </ol>
                            </div>
                            <div class="col-auto align-self-center">
                                <a v-on:click="AddPaymentVoucher" href="javascript:void(0);" class="btn btn-sm btn-outline-primary mx-1">
                                    <i class="align-self-center icon-xs ti-plus"></i>
                                    {{ $t('PaymentVoucherList.AddNew') }}
                                </a>
                                <a v-on:click="GotoPage('/StartScreen')" href="javascript:void(0);" class="btn btn-sm btn-outline-danger">
                                    {{ $t('Categories.Close') }}
                                </a>
                            </div>
                        </div>

                        <!--end row-->
                    </div>
                </div>
            </div>

            <div class="card">
                <div class="card-header">
                    <div class="row">
                        <div class="col-lg-8" style="padding-top:20px">
                            <div class="input-group">
                                <button class="btn btn-soft-primary" type="button" id="button-addon1">
                                    <i class="fas fa-search"></i>
                                </button>
                                <input v-model="search" type="text" class="form-control" :placeholder="$t('PaymentVoucherList.SearchByVoucher')" aria-label="Example text with button addon" aria-describedby="button-addon1">
                                <a class="btn btn-soft-primary" v-on:click="AdvanceFilter" id="button-addon2" value="Advance Filter">
                                    <i class="fa fa-filter"></i>
                                </a>
                            </div>
                        </div>
                        <div class=" col-lg-4 mt-1" v-if="!advanceFilters">

                            <button v-on:click="search22(true)" type="button" class="btn btn-outline-primary mt-3">
                                {{ $t('Sale.ApplyFilter') }}
                            </button>
                            <button v-on:click="clearData(false)" type="button"
                                    class="btn btn-outline-primary mx-2 mt-3">
                                {{ $t('Sale.ClearFilter') }}
                            </button>

                        </div>

                    </div>
                    <div class="row pb-3" v-if="advanceFilters">
                        <!-- <div class="col-lg-3">
                            <div class="form-group">
                                <label>Year</label>
                                <yeardropdown ref="yearDropdown" v-model="year"></yeardropdown>

                            </div>
                        </div> -->

                        <div class="col-lg-3">
                            <div class="form-group">
                                <label>Month</label>
                                <monthpicker v-model="monthObj" @update:modelValue="GetMonth" v-bind:disabled="isDisableMonth" v-if="!isDisableMonth" v-bind:key="randerforempty" />
                                <input class="form-control" v-else disabled />
                            </div>
                        </div>
                        <div class="col-lg-3">
                            <div class="form-group">
                                <label>{{ $t('Sale.FromDate') }}</label>
                                <datepicker v-model="fromDate" v-bind:isDisable="isDisable" @update:modelValue="GetDate1" v-bind:key="randerforempty" />
                            </div>
                        </div>
                        <div class="col-lg-3">
                            <div class="form-group">
                                <label>{{ $t('Sale.ToDate') }}</label>
                                <datepicker v-model="toDate" v-bind:isDisable="isDisable" @update:modelValue="GetDate1" v-bind:key="randerforempty" />
                            </div>
                        </div>
                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-3">
                            <label class="text  font-weight-bolder"> Nature of Payment :</label>
                            <multiselect v-model="natureOfPayment" :options="['Normal', 'Advance Payment','Security Deposit']" :show-labels="false" placeholder="Select Nature of Payment">
                            </multiselect>

                        </div>

                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-3">

                            <label class="text  font-weight-bolder"> Created By :</label>
                            <usersDropdown v-model="user" ref="userDropdown" :isloginhistory="isloginhistory" />

                        </div>
                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-3">

                            <button v-on:click="FilterRecord(true)" type="button" class="btn btn-outline-primary mt-3">
                                {{ $t('Sale.ApplyFilter') }}
                            </button>
                            <button v-on:click="FilterRecord(false)" type="button" class="btn btn-outline-primary mx-2 mt-3">
                                {{ $t('Sale.ClearFilter') }}
                            </button>

                        </div>

                    </div>

                </div>

                <div class="card-body">

                    <!-- Nav tabs -->
                    <ul class="nav nav-tabs" role="tablist">
                        <li class="nav-item">
                            <a class="nav-link" v-bind:class="{ active: active == 'Draft' }" v-on:click="makeActive('Draft')" data-bs-toggle="tab" href="#home" role="tab" aria-selected="true">
                                {{$t('PaymentVoucherList.Draft')}}
                            </a>
                        </li>
                        <li class="nav-item" v-on:click="makeActive('Approved')">
                            <a class="nav-link" v-bind:class="{ active: active == 'Approved' }" data-bs-toggle="tab" href="#profile" role="tab" aria-selected="false">
                                {{$t('PaymentVoucherList.Post')}}
                            </a>
                        </li>
                    </ul>

                    <!-- Tab panes -->
                    <div class="tab-content">
                        <div class="tab-pane pt-3" id="home" role="tabpanel" v-bind:class="{ active: active == 'Draft' }">
                            <div class="table-responsive">
                                <table class="table mb-0">
                                    <thead class="thead-light table-hover">
                                        <tr>
                                            <th style="width:40px;">#</th>
                                            <th style="width:150px;">
                                                {{ $t('PaymentVoucherList.VoucherNumber') }}
                                            </th>
                                            <th style="width:130px;">
                                                {{ $t('PaymentVoucherList.CreatedDate') }}
                                            </th>
                                            
                                            <th>
                                                {{ $t('PaymentVoucherList.PaymentMode') }}
                                            </th>
                                            <th>
                                                {{ $t('PaymentVoucherList.PaymentType') }}
                                            </th>
                                            <th>
                                                {{ $t('PaymentVoucherList.NatureOfPayment') }}
                                            </th>
                                            <th>
                                                {{ $t('PaymentVoucherList.BankCashAccount') }}

                                            </th>
                                            <th>
                                                <span v-if="formName != 'IsSupplier'">
                                                    {{ $t('PaymentVoucherList.CustomerAccount') }}
                                                </span>
                                                <span v-if="formName == 'IsSupplier'">
                                                    {{ $t('PaymentVoucherList.SupplierAccount') }}
                                                </span>
                                            </th>
                                            <th v-if="allowBranches">
                                                {{ $t('DailyExpense.BranchCode') }}
                                            </th>
                                            <th>
                                                {{ $t('PaymentVoucherList.NetAmount') }}
                                            </th>
                                            <th>

                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr v-for="(voucher, index) in vouchersList" v-bind:key="voucher.id">

                                            <td v-if="currentPage === 1">
                                                {{ index + 1 }}
                                            </td>
                                            <td v-else>
                                                {{ ((currentPage * 10) - 10) + (index + 1) }}
                                            </td>

                                            <td v-if="(isValid('CanEditPettyCash') && formName == 'PettyCash') || (isValid('CanEditCPR') && formName == 'BankReceipt') || (isValid('CanEditSPR') && formName == 'BankPay')">
                                                <strong>
                                                    <a href="javascript:void(0)" v-on:click="EditPaymentVoucher(voucher.id)">
                                                        {{ voucher.voucherNumber }}
                                                    </a>
                                                </strong>
                                            </td>
                                            <td v-else>
                                                {{ voucher.voucherNumber }}
                                            </td>
                                            <td>
                                                {{ getDate(voucher.date) }}
                                            </td>

                                            <td>
                                                <div class="badge badge-soft-primary" v-if="voucher.paymentMode == 0">
                                                    {{ ($i18n.locale == 'en' || isLeftToRight()) ? ' Cash' : 'السيول النقدية' }}

                                                </div>
                                                <div class="badge badge-soft-success" v-if="voucher.paymentMode == 1">
                                                    {{ ($i18n.locale == 'en' || isLeftToRight()) ? ' Bank' : 'مصرف' }}
                                                </div>
                                                <div class="badge badge-soft-info" v-if="voucher.paymentMode == 5">
                                                    {{ ($i18n.locale == 'en' || isLeftToRight()) ? ' Advance' : 'يتقدم' }}
                                                </div>
                                            </td>
                                            <td>
                                                {{ voucher.paymentMethods==0?'':voucher.paymentMethods }}
                                            </td>
                                            <td>
                                                {{ voucher.natureOfPayment }}
                                            </td>
                                            <td>
                                                {{ voucher.bankName }}
                                            </td>
                                            <td>
                                                {{ voucher.contactName }}
                                            </td>

                                            <td v-if="allowBranches">
                                                {{ voucher.branchCode }}
                                            </td>

                                            <td>
                                                {{ currency }}
                                                {{
 parseFloat(voucher.amount).toFixed(3).slice(0,
                                                    -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g,
                                                        "$1,")
                                                }}
                                            </td>
                                            <td>
                                                <div class="dropdown-menu">
                                                    <a class="dropdown-item" href="javascript:void(0)">{{ $t('PaymentVoucherList.View') }}</a>
                                                    <a class="dropdown-item" href="javascript:void(0)" v-on:click="PrintRdlc(voucher.id,'A5')">{{ $t('Print') }}</a>
                                                </div>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                            <hr />
                            <div class="row">
                                <div class="col-lg-6">
                                    <span v-if="currentPage === 1 && rowCount === 0">
                                        {{ $t('Pagination.ShowingEntries') }}
                                    </span>
                                    <span v-else-if="currentPage === 1 && rowCount < 10">
                                        {{ $t('Pagination.Showing') }}
                                        {{ currentPage }} {{ $t('Pagination.to') }} {{ rowCount }} {{ $t('Pagination.of') }}
                                        {{ rowCount }} {{ $t('Pagination.entries') }}
                                    </span>
                                    <span v-else-if="currentPage === 1 && rowCount >= 11">
                                        {{ $t('Pagination.Showing') }}
                                        {{ currentPage }} {{ $t('Pagination.to') }} {{ currentPage * 10 }} {{
                                            $t('Pagination.of')
                                        }} {{ rowCount }} {{ $t('Pagination.entries') }}
                                    </span>
                                    <span v-else-if="currentPage === 1">
                                        {{ $t('Pagination.Showing') }} {{ currentPage }} {{
                                        $t('Pagination.to')
                                        }} {{ currentPage * 10 }} of {{ rowCount }} {{
 $t('Pagination.entries')
                                        }}
                                    </span>
                                    <span v-else-if="currentPage !== 1 && currentPage !== pageCount">
                                        {{
                                        $t('Pagination.Showing')
                                        }} {{ (currentPage * 10) - 9 }} {{ $t('Pagination.to') }}
                                        {{ currentPage * 10 }} {{ $t('Pagination.of') }} {{ rowCount }} {{
                                            $t('Pagination.entries')
                                        }}
                                    </span>
                                    <span v-else-if="currentPage === pageCount">
                                        {{ $t('Pagination.Showing') }}
                                        {{ (currentPage * 10) - 9 }} {{ $t('Pagination.to') }} {{ rowCount }} {{
                                            $t('Pagination.of')
                                        }} {{ rowCount }} {{ $t('Pagination.entries') }}
                                    </span>
                                </div>
                                <div class=" col-lg-6">
                                    <div class=" float-end" v-on:click="getPage()">
                                        <b-pagination pills size="sm" v-model="currentPage" :total-rows="rowCount" :per-page="10" :first-text="$t('Table.First')" :prev-text="$t('Table.Previous')" :next-text="$t('Table.Next')" :last-text="$t('Table.Last')">
                                        </b-pagination>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="tab-pane pt-3" id="profile" role="tabpanel" v-bind:class="{ active: active == 'Approved' }">
                            <div class="table-responsive">
                                <table class="table mb-0">
                                    <thead class="thead-light table-hover">
                                        <tr>
                                            <th style="width:40px;">#</th>
                                            <th style="width:150px;">
                                                {{ $t('PaymentVoucherList.VoucherNumber') }}
                                            </th>
                                            <th style="width:130px;">
                                                {{ $t('PaymentVoucherList.CreatedDate') }}
                                            </th>
                                            
                                            <th>
                                                {{ $t('PaymentVoucherList.PaymentMode') }}
                                            </th>
                                            <th>
                                                {{ $t('PaymentVoucherList.PaymentType') }}
                                            </th>
                                            <th>
                                                {{ $t('PaymentVoucherList.NatureOfPayment') }}
                                            </th>
                                            <th>
                                                {{ $t('PaymentVoucherList.BankCashAccount') }}
                                            </th>
                                            <th>
                                                <span v-if="formName != 'IsSupplier'">
                                                    {{ $t('PaymentVoucherList.CustomerAccount') }}
                                                </span>
                                                <span v-if="formName == 'IsSupplier'">
                                                    {{ $t('PaymentVoucherList.SupplierAccount') }}
                                                </span>
                                            </th>
                                            <th v-if="allowBranches">
                                                {{ $t('DailyExpense.BranchCode') }}
                                            </th>
                                            <th>
                                                {{ $t('PaymentVoucherList.NetAmount') }}
                                            </th>

                                            <th>

                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr v-for="(voucher, index) in vouchersList" v-bind:key="voucher.id">
                                            <td v-if="currentPage === 1">
                                                {{ index + 1 }}
                                            </td>
                                            <td v-else>
                                                {{ ((currentPage * 10) - 10) + (index + 1) }}
                                            </td>

                                            <td v-if="(isValid('CanEditPettyCash') && formName == 'PettyCash') || (isValid('CanEditCPR') && formName == 'BankReceipt') || (isValid('CanEditSPR') && formName == 'BankPay')">
                                                <strong>
                                                    <a href="javascript:void(0)" v-on:click="EditPaymentVoucher(voucher.id,false)">{{ voucher.voucherNumber }}</a>
                                                </strong>
                                            </td>
                                            <td v-else>
                                                {{ voucher.voucherNumber }}
                                            </td>
                                            <td>
                                                {{ getDate(voucher.date) }}
                                            </td>
                                            
                                            <td>
                                                <div class="badge badge-soft-primary" v-if="voucher.paymentMode == 0">
                                                    {{ ($i18n.locale == 'en' || isLeftToRight()) ? ' Cash' : 'السيولة النقدية' }}

                                                </div>
                                                <div class="badge badge-soft-success" v-else-if="voucher.paymentMode == 1">
                                                    {{ ($i18n.locale == 'en' || isLeftToRight()) ? ' Bank' : 'مصرف' }}
                                                </div>
                                                <div class="badge badge-soft-info" v-if="voucher.paymentMode == 5">
                                                    {{ ($i18n.locale == 'en' || isLeftToRight()) ? ' Advance' : 'يتقدم' }}
                                                </div>
                                            </td>
                                            <td>
                                                {{ voucher.paymentMethods==0?'':voucher.paymentMethods }}
                                            </td>
                                            <td>
                                                {{ voucher.natureOfPayment }}
                                            </td>
                                            <td>
                                                {{ voucher.bankName }}
                                            </td>
                                            <td>
                                                {{ voucher.contactName }}
                                            </td>
                                            <td v-if="allowBranches">
                                                {{ voucher.branchCode }}
                                            </td>

                                            <td>
                                                {{ currency }} {{ parseFloat(voucher.amount).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                            </td>

                                            <td>
                                                <button type="button" class="btn btn-light dropdown-toggle" data-bs-toggle="dropdown" aria-expanded="false">{{ $t('PaymentVoucherList.Action') }} <i class="mdi mdi-chevron-down"></i></button>
                                                <div class="dropdown-menu">
                                                    <a class="dropdown-item" href="javascript:void(0)">{{ $t('PaymentVoucherList.View') }}</a>
                                                    <a class="dropdown-item" href="javascript:void(0)" v-on:click="PrintRdlc(voucher.id,'A5')">{{ $t('Print') }}</a>
                                                </div>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                            <hr />
                            <div class="row">
                                <div class="col-lg-6">
                                    <span v-if="currentPage === 1 && rowCount === 0">
                                        {{
 $t('Pagination.ShowingEntries')
                                        }}
                                    </span>
                                    <span v-else-if="currentPage === 1 && rowCount < 10">
                                        {{ $t('Pagination.Showing') }}
                                        {{ currentPage }} {{ $t('Pagination.to') }} {{ rowCount }} {{ $t('Pagination.of') }}
                                        {{ rowCount }} {{ $t('Pagination.entries') }}
                                    </span>
                                    <span v-else-if="currentPage === 1 && rowCount >= 11">
                                        {{ $t('Pagination.Showing') }}
                                        {{ currentPage }} {{ $t('Pagination.to') }} {{ currentPage * 10 }} {{
                                            $t('Pagination.of')
                                        }} {{ rowCount }} {{ $t('Pagination.entries') }}
                                    </span>
                                    <span v-else-if="currentPage === 1">
                                        {{ $t('Pagination.Showing') }} {{ currentPage }} {{
                                        $t('Pagination.to')
                                        }} {{ currentPage * 10 }} of {{ rowCount }} {{
        $t('Pagination.entries')
                                        }}
                                    </span>
                                    <span v-else-if="currentPage !== 1 && currentPage !== pageCount">
                                        {{
                                        $t('Pagination.Showing')
                                        }} {{ (currentPage * 10) - 9 }} {{ $t('Pagination.to') }}
                                        {{ currentPage * 10 }} {{ $t('Pagination.of') }} {{ rowCount }} {{
                                            $t('Pagination.entries')
                                        }}
                                    </span>
                                    <span v-else-if="currentPage === pageCount">
                                        {{ $t('Pagination.Showing') }}
                                        {{ (currentPage * 10) - 9 }} {{ $t('Pagination.to') }} {{ rowCount }} {{
                                            $t('Pagination.of')
                                        }} {{ rowCount }} {{ $t('Pagination.entries') }}
                                    </span>
                                </div>
                                <div class=" col-lg-6">
                                    <div class=" float-end" v-on:click="getPage()">
                                        <b-pagination pills size="sm" v-model="currentPage" :total-rows="rowCount" :per-page="10" :first-text="$t('Table.First')" :prev-text="$t('Table.Previous')" :next-text="$t('Table.Next')" :last-text="$t('Table.Last')">
                                        </b-pagination>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <invoicedetailsprint :show="show" v-if="show" :reportsrc="reportsrc" :changereport="changereport" @close="show=false" @IsSave="IsSave" />

        </div>
    </div>
    <div v-else>
        <acessdenied></acessdenied>
    </div>
</template>

<script>
    import moment from "moment";
    import Multiselect from 'vue-multiselect'
    import clickMixin from '@/Mixins/clickMixin'
    export default {
        mixins: [clickMixin],
        components: {
            Multiselect
        },
        props: ['formName'],
        data: function () {
            return {
                request: 0,
                randerDiv: 0,
                allowBranches: false,
                isAccordionOpen: false,
                loading: false,
                series: [],
                chartOptions: {
                    labels: ['Post', 'Draft', 'Rejected']
                },

                series2: [],
                chartOptions2: {
                    labels: ['Cash ', 'Bank ']
                },
                series3: [],
                chartOptions3: {
                    labels: ['Normal', 'Advance', 'Security']
                },
                paymentVoucherLookUp: {
                    posted: 0,
                    draft: 0,
                    rejected: 0,
                    totalReceipt: 0,
                    cashReceipt: 0,
                    totalCashReceipt: 0,
                    bankReceipt: 0,
                    totalBankReceipt: 0,
                    totalCashBank: 0,
                    normal: 0,
                    advance: 0,
                    security: 0,
                    totalNormal: 0
                },
                changereport: 0,
                isloginhistory: true,
                isDisable: false,
                isDisableMonth: false,

                counter: 0,
                reportsrc: '',
                user: '',
                natureOfPayment: '',
                fromDate: '',
                toDate: '',
                monthObj: '',
                year: '',
                randerforempty: 0,
                month: 0,

                selected: [],
                optionsOfYear: [],
                selectAll: false,
                advanceFilters: false,
                search: '',
                show3: false,
                show: false,
                show2: false,
                vouchersList: [],
                type: '',
                currentPage: 1,
                pageCount: '',
                rowCount: '',
                currency: '',
                IsPaksitanClient: false,
                userID: '',

                active: 'Draft',
                printDetails: [],
                printId: '00000000-0000-0000-0000-000000000000',
                printRender: 0,
                printed: false,
                updateApprovalStatus: {
                    approvalStatus: '',
                    id: [],
                },
                paymentVoucher: {
                    id: '00000000-0000-0000-0000-000000000000',
                    date: '',
                    voucherNumber: '',
                    chequeNumber: '',
                    narration: '',
                    paymentVoucherType: '',
                    amount: 0,
                    approvalStatus: 'Void',
                    purchaseInvoice: '00000000-0000-0000-0000-000000000000',
                    saleInvoice: '00000000-0000-0000-0000-000000000000',
                    bankCashAccountId: '00000000-0000-0000-0000-000000000000',
                    contactAccountId: '00000000-0000-0000-0000-000000000000',
                    userName: ''
                },
                refundVoucher: {},
                showRefund: false,

                headerFooter: {
                    footerEn: '',
                    footerAr: '',
                    company: ''
                },
            }
        },

        methods: {

            GetDate1: function () {

                if (this.fromDate != '' || this.toDate != '') {
                    this.isDisableMonth = true;
                    this.year = '';
                    this.month = '';
                    this.monthObj = '';

                } else {
                    this.isDisableMonth = false;
                }

            },
            GetMonth: function () {


                if (this.monthObj != undefined) {
                    this.isDisable = true;
                    this.fromDate = '';
                    this.toDate = '';

                    this.month = moment(this.monthObj).format("MM");
                    this.year = moment(this.monthObj).format("YYYY");
                }

            },

            AdvanceFilter: function () {

                this.advanceFilters = !this.advanceFilters;
                if (this.advanceFilters == false) {
                    this.FilterRecord(false);
                }

            },
            FilterRecord: function (type) {

                if (type) {
                    if (this.fromDate != '') {
                        if (this.toDate == '') {
                            this.$swal({
                                title: 'Error',
                                text: "Please Select To Date ",
                                type: 'error',
                                icon: 'error',
                                showConfirmButton: false,
                                timer: 2000,
                                timerProgressBar: true,
                            });

                            return;

                        }
                    }
                    if (this.toDate != '') {
                        if (this.fromDate == '') {
                            this.$swal({
                                title: 'Error',
                                text: "Please Select From Date ",
                                type: 'error',
                                icon: 'error',
                                showConfirmButton: false,
                                timer: 2000,
                                timerProgressBar: true,
                            });

                            return;

                        }
                    }

                    if (this.monthObj != undefined) {
                        this.month = moment(this.monthObj).format("MM");
                        this.year = moment(this.monthObj).format("YYYY");
                    }
                    if (this.user.id != undefined) {
                        this.userId = this.user.id;

                    }

                    this.GetPaymentVoucherData(this.formName, this.active, '', 1);

                } else {
                    this.isDisable = false;
                    this.isDisableMonth = false;
                    if (this.$refs.userDropdown != null) {
                        this.$refs.userDropdown.EmptyRecord();
                    }
                    this.user = '';
                    this.userId = '';
                    if (this.$refs.yearDropdown != null) {
                        this.$refs.yearDropdown.EmptyRecord();
                    }
                    this.year = '';
                    this.fromDate = '';
                    this.toDate = '';
                    this.natureOfPayment = '';
                    this.month = '';
                    this.monthObj = '';
                    this.randerforempty++;
                    this.GetPaymentVoucherData(this.formName, this.active, '', 1);

                }

            },

            // search22: function () {
            //     this.GetPaymentVoucherData(this.search,this.formName, this.currentPage, this.active, this.fromDate, this.toDate, this.fromTime, this.toTime, this.terminalId, this.userId, this.customerId, this.customerType,this.natureOfPayment,this.year,this.month);
            // },
            search22: function (val) {
                if (val != '' && val != undefined && val != null) {
                    this.GetPaymentVoucherData(this.formName, this.active, val, 1, this.currentPage, this.fromDate, this.toDate, this.terminalId, this.userId, this.customerId, this.customerType, this.natureOfPayment, this.year, this.month);
                } else {
                    this.GetPaymentVoucherData(this.formName, this.active, '', 1, this.currentPage, this.fromDate, this.toDate, this.terminalId, this.userId, this.customerId, this.customerType, this.natureOfPayment, this.year, this.month);
                }
            },

            clearData: function () {
                this.search = "";
                this.GetPaymentVoucherData(this.formName, this.active, '', 1,);

            },

            IsSave: function () {
                this.show = false;
            },
            GotoPage: function (link) {
                this.$router.push({
                    path: link
                });
            },
            getBase64Image: function (path) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.headerFooter.company.logoPath = 'data:image/png;base64,' + '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';

                root.$https
                    .get('/Contact/GetBaseImage?filePath=' + path, {
                        headers: {
                            "Authorization": `Bearer ${token}`
                        }
                    }).then(function (response) {

                        if (response.data != null) {
                            root.filePath = response.data;
                            root.headerFooter.company.logoPath = 'data:image/png;base64,' + root.filePath;
                        }
                    });
            },
            GetHeaderDetail: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                root.$https.get("/Company/GetCompanyDetail?id=" + localStorage.getItem('CompanyID'), {
                    headers: {
                        Authorization: `Bearer ${token}`
                    },
                })
                    .then(function (response) {
                        if (response.data != null) {
                            root.headerFooter.company = response.data;
                            root.GetInvoicePrintSetting();
                            root.getBase64Image(root.headerFooter.company.logoPath);
                        }
                    });
            },
            GetInvoicePrintSetting: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                root.$https.get('/Sale/printSettingDetail?branchId=' + localStorage.getItem('BranchId'), {
                    headers: {
                        "Authorization": `Bearer ${token}`
                    }
                })
                    .then(function (response) {
                        if (response.data != null && response.data != '') {
                            root.headerFooter.footerEn = response.data.termsInEng;
                            root.headerFooter.footerAr = response.data.termsInAr;
                            root.headerFooter.isHeaderFooter = response.data.isHeaderFooter;
                            root.headerFooter.englishName = response.data.englishName;
                            root.headerFooter.arabicName = response.data.arabicName;
                            root.headerFooter.customerAddress = response.data.customerAddress;
                            root.headerFooter.customerVat = response.data.customerVat;
                            root.headerFooter.customerNumber = response.data.customerNumber;
                            root.headerFooter.cargoName = response.data.cargoName;
                            root.headerFooter.customerTelephone = response.data.customerTelephone;
                            root.headerFooter.itemPieceSize = response.data.itemPieceSize;
                            root.headerFooter.styleNo = response.data.styleNo;
                            root.headerFooter.blindPrint = response.data.blindPrint;
                            root.headerFooter.showBankAccount = response.data.showBankAccount;
                            root.headerFooter.bankAccount1 = response.data.bankAccount1;
                            root.headerFooter.bankAccount2 = response.data.bankAccount2;
                            root.headerFooter.bankIcon1 = response.data.bankIcon1;
                            root.headerFooter.bankIcon2 = response.data.bankIcon2;
                            root.headerFooter.customerNameEn = response.data.customerNameEn;
                            root.headerFooter.customerNameAr = response.data.customerNameAr;

                        }
                    },
                        function (error) {
                            root.loading = false;
                            console.log(error);
                        });
            },

            select: function () {

                this.selected = [];
                if (!this.selectAll) {
                    for (let i in this.vouchersList) {
                        this.selected.push(this.vouchersList[i].id);
                    }
                }
            },
            getDate: function (x) {
                return moment(x).format('DD MMM YYYY');
            },
            makeActive: function (item) {
                this.active = item;
                this.selectAll = false;
                this.selected = [];
                this.GetPaymentVoucherData(this.formName, item, this.search, this.currentPage);
            },

            AddPaymentVoucher: function () {
                // this.$router.push(');
                this.$router.push({
                            path: '/AddBranchVoucher',
                            query: {
                                Add: true,
                                formName:  this.formName,
                            }
                        })
            },
            getPage: function () {

                this.GetPaymentVoucherData(this.formName, this.active, this.search, this.currentPage);
            },
            GetSaleDashboardRecord: function () {
                this.isAccordionOpen = !this.isAccordionOpen;
                if (this.request == 0) {
                    var root = this;
                    var token = '';
                    if (token == '') {
                        token = localStorage.getItem('token');
                    }
                    this.loading = true;
                    var branchId = localStorage.getItem('BranchId');


                    this.$https.get('/PaymentVoucher/PaymentVoucherListDashboard?paymentVoucherType=' + this.formName + '&branchId=' + branchId, { headers: { "Authorization": `Bearer ${token}` } })
                        .then(function (response) {

                            if (response.data != null) {

                                root.series = [];
                                root.series.push(response.data.posted);
                                root.series.push(response.data.draft);
                                root.series.push(response.data.rejected);
                                root.series2 = [];
                                root.series2.push(response.data.totalCashReceipt);
                                root.series2.push(response.data.totalBankReceipt);
                                root.series3 = [];
                                root.series3.push(response.data.normal);
                                root.series3.push(response.data.advance);
                                root.series3.push(response.data.security);
                                root.paymentVoucherLookUp = {
                                    posted: response.data.posted,
                                    draft: response.data.draft,
                                    rejected: response.data.rejected,
                                    totalReceipt: response.data.totalReceipt,
                                    cashReceipt: response.data.cashReceipt,
                                    totalCashReceipt: response.data.totalCashReceipt,
                                    bankReceipt: response.data.bankReceipt,
                                    totalBankReceipt: response.data.totalBankReceipt,
                                    totalCashBank: response.data.totalCashBank,
                                    normal: response.data.normal,
                                    advance: response.data.advance,
                                    security: response.data.security,
                                    totalNormal: response.data.totalNormal
                                };
                                root.loading = false;

                            }
                        });

                }
                this.request++;

            },
            GetPaymentVoucherData: function (vtype, status, search, currentPage) {


                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                var branchId = localStorage.getItem('BranchId');


                localStorage.setItem('currentPage', this.currentPage);
                localStorage.setItem('active', this.active);
                search == undefined ? '' : search;
                root.$https.get('PaymentVoucher/BranchVoucherList?paymentVoucherType=' + vtype + '&status=' + status + '&searchTerm=' + search + '&pageNumber=' + currentPage + '&fromDate=' + this.fromDate + '&toDate=' + this.toDate + '&natureOfPayment=' + this.natureOfPayment + '&userId=' + this.userId + '&month=' + this.month + '&year=' + this.year + '&branchId=' + branchId, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {
                        root.vouchersList = response.data.results;
                        root.pageCount = response.data.pageCount;
                        root.rowCount = response.data.rowCount;
                        root.currentPage = currentPage;
                    }
                });
            },
            RemovePaymentVoucher: function (id) {
                var root = this;
                this.$swal({
                    title: (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Are you sure?' : 'هل أنت متأكد؟',
                    text: (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'You will not be able to recover this!' : 'لن تتمكن من استرداد هذا!',
                    type: "warning",
                    showCancelButton: true,
                    confirmButtonColor: "#DD6B55",
                    confirmButtonText: (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Yes, delete it!' : 'نعم ، احذفها!',
                    closeOnConfirm: false,
                    closeOnCancel: true
                }).then(function (result) {
                    if (result.isConfirmed == true) {
                        var token = '';
                        if (token == '') {
                            token = localStorage.getItem('token');
                        }
                        root.$https.get('/PaymentVoucher/PaymentVoucherDelete?id=' + id, {
                            headers: {
                                "Authorization": `Bearer ${token}`
                            }
                        })
                            .then(function (response) {

                                if (response.data.message.id != '00000000-0000-0000-0000-000000000000') {
                                    root.$swal({
                                        title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Deleted!' : 'تم الحذف!',
                                        text: response.data.message.isDeleted,
                                        type: 'success',
                                        icon: 'success',
                                        showConfirmButton: false,
                                        timer: 800,
                                        timerProgressBar: true,
                                        confirmButtonClass: "btn btn-success",
                                        buttonsStyling: false
                                    });
                                }
                            },
                                function () {
                                    root.$swal({
                                        title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                                        text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Delete UnSuccessfully' : 'حذف غير ناجح',
                                        type: 'error',
                                        confirmButtonClass: "btn btn-danger",
                                        buttonsStyling: false
                                    });
                                });
                    } else {
                        root.$swal({
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Cancelled!' : 'ألغيت!',
                            text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Cancelled' : 'ألغيت!',
                            type: 'success',
                            icon: 'success',
                            showConfirmButton: false,
                            timer: 800,
                            timerProgressBar: true,
                            confirmButtonClass: "btn btn-success",
                            buttonsStyling: false
                        });
                    }
                });
            },
            PrintRdlc: function (id,val) {
                var isBlind = localStorage.getItem('IsBlindPrint') == 'true' ? true : false;
                if (isBlind) {
                    this.show = false;
                } else {
                    this.show = true;
                }
                var companyId = '';
                if (this.$store.isAuthenticated) {
                    companyId = localStorage.getItem('CompanyID');
                }

                this.reportsrc = this.$ReportServer + '/Invoice/A4_DefaultTempletForm.aspx?Id=' + id  + '&CompanyId=' + companyId + '&formName=BranchVouchers' +'&pageType='+val; 
                this.changereport++;
            },
            EditPaymentVoucher: function (id, view) {
                var root = this;
                var childFormName = this.formName;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                if (view) {
                    root.$https.get('/PaymentVoucher/BranchVoucherDetail?Id=' + id, {
                        headers: {
                            "Authorization": `Bearer ${token}`
                        }
                    }).then(function (response) {
                        if (response.data != null) {
                            root.$store.GetEditObj(response.data);
                            root.$router.push({
                                path: '/addPaymentVoucher',
                                query: {
                                    data: '',
                                    Add: false,
                                    isView: true,
                                    formName : childFormName,
                                }
                            })
                        }
                    });
                } else {
                    root.$https.get('/PaymentVoucher/PaymentVoucherDetails?Id=' + id, {
                        headers: {
                            "Authorization": `Bearer ${token}`
                        }
                    }).then(function (response) {
                        if (response.data != null) {
                            root.$store.GetEditObj(response.data);
                            root.$router.push({
                                path: '/addPaymentVoucher',
                                query: {
                                    data: '',
                                    Add: false,
                                    formName : childFormName,
                                }
                            })
                        }
                    });
                }
            },
            PrintPaymentVoucher: function (value, print2) {

                if (print2 == 'A4') {
                    this.show3 = true;
                } else {
                    this.show = false;
                    this.show2 = false;
                    if (print2) {
                        this.show2 = true;
                    } else {
                        this.show = true;
                    }
                }

                this.GetHeaderDetail();
                var id = value;
                this.printId = id;
                this.printRender++;
            },
        },
        updated: function () {
            if (this.selected.length < this.vouchersList.length) {
                this.selectAll = false;
            } else if (this.selected.length == this.vouchersList.length) {

                if (this.selected.length == 0) {
                    this.selectAll = false;
                } else {
                    this.selectAll = true
                }
            }
        },
        
        created: function () {

            if (this.$route.query.data == 'PaymentVoucherLists' + this.formName) {
                this.$emit('update:modelValue', 'PaymentVoucherLists' + this.formName);

            }
            else {
                this.$emit('update:modelValue', this.$route.name + this.formName);

                localStorage.removeItem("active");
                localStorage.removeItem("currentPage");

            }
            if (localStorage.getItem('active') != null && localStorage.getItem('active') != '' && localStorage.getItem('active') != undefined) {
                this.currentPage = parseInt(localStorage.getItem('currentPage'));
                this.active = (localStorage.getItem('active'));
                this.GetPaymentVoucherData(this.formName, this.active, this.search, this.currentPage);

            }
            else {
                this.GetPaymentVoucherData(this.formName, 'Draft', this.search, 1);
            }

            this.allowBranches = localStorage.getItem('AllowBranches') == 'true' ? true : false;
        },

        mounted: function () {
            this.IsPaksitanClient = localStorage.getItem('IsPaksitanClient') == "true" ? true : false;
            this.currency = localStorage.getItem('currency');
        }
    }
</script>
