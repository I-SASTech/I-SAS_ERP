<template>
    <div class="row" v-if="(isValid('CreditNote') || isValid('DebitNote'))">

        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col" v-if="formName == 'CreditNote'">
                                <h4 class="page-title">{{ $t('CreditNote.CreditNote') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('SaleOrder.Home') }}</a>
                                    </li>
                                    <li class="breadcrumb-item active">
                                        {{ $t('CreditNote.CreditNote') }}
                                    </li>
                                </ol>
                            </div>
                            <div class="col" v-else>
                                <h4 class="page-title">{{ $t('CreditNote.DebitNote') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('SaleOrder.Home') }}</a>
                                    </li>
                                    <li class="breadcrumb-item active">
                                        {{ $t('CreditNote.DebitNote') }}
                                    </li>
                                </ol>
                            </div>

                            <div class="col-auto align-self-center">
                                <a v-on:click="AddPurchaseOrder" href="javascript:void(0);"
                                    class="btn btn-sm btn-outline-primary mx-1">
                                    <i class="align-self-center icon-xs ti-plus"></i>
                                    {{ $t('SaleOrder.AddNew') }}
                                </a>
                                <a v-on:click="GotoPage('/StartScreen')" href="javascript:void(0);"
                                    class="btn btn-sm btn-outline-danger">
                                    {{ $t('SaleOrder.Close') }}
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="card">
                <div class="card-header">
                    <div class="row">
                        <div class="col-lg-8" style="padding-top:20px">
                            <div class="input-group">
                                <button class="btn btn-soft-primary" type="button" id="button-addon1"><i
                                        class="fas fa-search"></i></button>
                                <input v-model="search" v-if="formName == 'SaleOrderTracking'" type="text"
                                    class="form-control" :placeholder="$t('SaleOrder.SearchBySO')"
                                    aria-label="Example text with button addon" aria-describedby="button-addon1">
                                <input v-model="search" v-else type="text" class="form-control"
                                    :placeholder="$t('SaleOrder.SearchBySO')" aria-label="Example text with button addon"
                                    aria-describedby="button-addon1">
                                <a class="btn btn-soft-primary" v-on:click="AdvanceFilterFor" id="button-addon2"
                                    value="Advance Filter">
                                    <i class="fa fa-filter"></i>
                                </a>
                            </div>
                        </div>
                        <div class=" col-lg-4 mt-1" v-if="!advanceFilters">

                            <button v-on:click="search22(true)" :disabled="!search" type="button" class="btn btn-outline-primary mt-3">
                                {{ $t('Sale.ApplyFilter') }}
                            </button>
                            <button v-on:click="clearData(false)" :disabled="!search" type="button" class="btn btn-outline-primary mx-2 mt-3">
                                {{ $t('Sale.ClearFilter') }}
                            </button>

                        </div>
                    </div>
                    <div class="row " v-if="advanceFilters">
                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-4">
                            <label class="text  font-weight-bolder">{{ $t('Sale.Customer') }}</label>
                            <customerdropdown v-model="customerId" ref="CustomerDropdown" />
                        </div>
                        <div class="col-xs-12  col-lg-2">
                            <div class="form-group">
                                <label>{{ $t('Sale.Month') }}</label>
                                <monthpicker v-model="monthObj" @update:modelValue="GetMonth" v-bind:disabled="isDisableMonth" v-if="!isDisableMonth" v-bind:key="randerforempty" />
                                <input class="form-control" v-else disabled />
                            </div>
                        </div>
                        <div class="col-lg-3">
                            <div class="form-group">
                                <label>{{ $t('Sale.FromDate') }}</label>
                                <datepicker v-model="fromDate" v-bind:isDisable="isDisable" @update:modelValue="GetDate1"
                                    v-bind:key="randerforempty" />
                            </div>
                        </div>
                        <div class="col-lg-3">
                            <div class="form-group">
                                <label>{{ $t('Sale.ToDate') }}</label>
                                <datepicker v-model="toDate" v-bind:isDisable="isDisable" @update:modelValue="GetDate1"
                                    v-bind:key="randerforempty" />
                            </div>
                        </div>

                        <div class="col-xs-12  col-lg-3 " v-if="isValid('CanViewTerminal') || isValid('MachineWisePrefix') || isValid('Terminal') || isValid('CanStartDay') ||  isValid('TouchInvoiceTemplate1')">
                            <label class="text  font-weight-bolder"> {{ $t('Sale.Counter') }}:</label>
                            <terminal-dropdown v-model="terminalId" :terminalType="terminalType"
                                :terminalUserType="'Offline'" :isSelect="true" />

                        </div>
                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-3">
                            <label class="text  font-weight-bolder"> {{ $t('Sale.User1') }}:</label>
                            <usersDropdown v-model="user" ref="userDropdown" :isloginhistory="isloginhistory" />
                        </div>

                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-3">

                            <button v-on:click="FilterRecord(true)" :disabled ="!isFilterButtonDisabled" type="button" class="btn btn-outline-primary mt-3">
                                {{ $t('Sale.ApplyFilter') }}
                            </button>
                            <button v-on:click="FilterRecord(false)" :disabled ="!isFilterButtonDisabled" type="button"
                                class="btn btn-outline-primary mx-2 mt-3">
                                {{ $t('Sale.ClearFilter') }}
                            </button>

                        </div>
                    </div>
                </div>
                <div class="card-body">
                    <!-- Nav tabs -->
                    <ul class="nav nav-tabs" role="tablist">
                        <li class="nav-item" v-on:click="makeActive('Approved')">
                            <a class="nav-link" v-bind:class="{ active: active == 'Approved' }" data-bs-toggle="tab"
                                href="#profile" role="tab" aria-selected="false">
                                {{ $t('SaleOrder.Post') }}
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
                                            <th>#</th>
                                            <th>
                                                <span v-if="formName == 'CreditNote'">Credit Note</span>
                                                <span v-else>{{ $t('SaleOrder.DebitNote') }}</span>
                                            </th>
                                            <th>
                                                {{ $t('SaleOrder.CreatedDate') }}
                                            </th>
                                            <th>
                                                {{ $t('SaleOrder.CustomerName') }}
                                            </th>
                                            <th v-if="allowBranches">
                                                {{ $t('DailyExpense.BranchCode') }}
                                            </th>
                                            <th>
                                                {{ $t('SaleOrder.NetAmount') }}
                                            </th>
                                            <th style="width: 70px;" class="text-end">
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr v-for="(purchaseOrder, index) in saleOrderList" v-bind:key="purchaseOrder.id">
                                            <td v-if="currentPage === 1">
                                                {{ index + 1 }}
                                            </td>
                                            <td v-else>
                                                {{ ((currentPage * 10) - 10) + (index + 1) }}
                                            </td>
                                            <td>
                                                <strong>
                                                    <a href="javascript:void(0)"
                                                        v-on:click="EditPurchaseOrder(purchaseOrder.id)">{{
                                                            purchaseOrder.code }}</a>
                                                </strong>

                                            </td>
                                            <td>
                                                {{ purchaseOrder.code }}
                                            </td>


                                            <td>
                                                {{ getDate(purchaseOrder.date) }}
                                            </td>
                                            <td>
                                                <a href="javascript:void(0)"
                                                    v-on:click="ViewCustomerInfo(purchaseOrder.customerId)">{{
                                                        purchaseOrder.customerDisplayName }}</a>
                                            </td>
                                            <td v-if="allowBranches">
                                                {{ purchaseOrder.branchCode }}
                                            </td>
                                            <td>
                                                {{ currency }}
                                                {{ parseFloat(purchaseOrder.amount).toFixed(3).slice(0,
                                                    -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g,
                                                        "$1,") }}
                                            </td>
                                            <td class="text-end">

                                                <button type="button" class="btn btn-light dropdown-toggle"
                                                    data-bs-toggle="dropdown" aria-expanded="false">{{
                                                        $t('SaleOrder.Action') }} <i class="mdi mdi-chevron-down"></i></button>
                                                <div class="dropdown-menu">
                                                    <a class="dropdown-item" href="javascript:void(0)"
                                                        v-on:click="EditPurchaseOrder(purchaseOrder.id)">{{
                                                            $t('SaleOrder.EditInvoice') }}</a>
                                                    <a class="dropdown-item" href="javascript:void(0)"
                                                        v-on:click="PrintInvoice(purchaseOrder.id)">{{
                                                            $t('SaleOrder.A4Print') }}</a>
                                                    <a class="dropdown-item" href="javascript:void(0)"
                                                        v-on:click="PrintInvoicePdf(purchaseOrder.id)">{{
                                                            $t('SaleOrder.PdfDownload') }}</a>

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
                                        {{ currentPage }} {{ $t('Pagination.to') }} {{ currentPage * 10 }}
                                        {{
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
                                        <b-pagination pills size="sm" v-model="currentPage" :total-rows="rowCount"
                                            :per-page="10" :first-text="$t('Table.First')" :prev-text="$t('Table.Previous')"
                                            :next-text="$t('Table.Next')" :last-text="$t('Table.Last')">
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
                                            <th>#</th>
                                            <th>
                                                <span v-if="formName == 'CreditNote'">Credit Note</span>
                                                <span v-else>{{ $t('SaleOrder.DebitNote') }}</span>
                                            </th>
                                            <th>
                                                {{ $t('SaleOrder.CreatedDate') }}
                                            </th>
                                            <th>
                                                {{ $t('SaleOrder.CustomerName') }}
                                            </th>
                                            <th v-if="allowBranches">
                                                {{ $t('DailyExpense.BranchCode') }}
                                            </th>
                                            <th>
                                                {{ $t('SaleOrder.NetAmount') }}
                                            </th>
                                            <th style="width: 70px;" class="text-end">

                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr v-for="(purchaseOrder, index) in saleOrderList" v-bind:key="purchaseOrder.id">
                                            <td v-if="currentPage === 1">
                                                {{ index + 1 }}
                                            </td>
                                            <td v-else>
                                                {{ ((currentPage * 10) - 10) + (index + 1) }}
                                            </td>

                                            <td>
                                                {{ purchaseOrder.code }}
                                            </td>
                                            <td>
                                                {{ getDate(purchaseOrder.date) }}
                                            </td>
                                            <td>
                                                <a href="javascript:void(0)"
                                                    v-on:click="ViewCustomerInfo(purchaseOrder.customerId)">{{
                                                        purchaseOrder.customerDisplayName }}</a>
                                            </td>
                                            <td v-if="allowBranches">
                                                {{ purchaseOrder.branchCode }}
                                            </td>
                                            <td>
                                                {{ currency }}
                                                {{ parseFloat(purchaseOrder.amount).toFixed(3).slice(0,
                                                    -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g,
                                                        "$1,") }}
                                            </td>
                                            <td class="text-end">
                                                <button type="button" class="btn btn-light dropdown-toggle"
                                                    data-bs-toggle="dropdown" aria-expanded="false">{{
                                                        $t('SaleOrder.Action') }} <i class="mdi mdi-chevron-down"></i></button>
                                                <div class="dropdown-menu">
                                                    <a class="dropdown-item" href="javascript:void(0)"
                                                        v-on:click="PrintRdlc(purchaseOrder.id, false)">{{
                                                            $t('SaleOrder.A4Print') }}</a>
                                                    <a class="dropdown-item" href="javascript:void(0)"
                                                        v-on:click="PrintRdlc(purchaseOrder.id, true)">{{
                                                            $t('SaleOrder.PdfDownload') }}</a>

                                                    <a class="dropdown-item" href="javascript:void(0)" v-if="purchaseOrder.isDefault"
                                                        v-on:click="sendEmail(purchaseOrder.id, purchaseOrder.customerEmail)">{{
                                                            $t('Email') }}
                                                    </a>
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
                                        {{ currentPage }} {{ $t('Pagination.to') }} {{ currentPage * 10 }}
                                        {{
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
                                        <b-pagination pills size="sm" v-model="currentPage" :total-rows="rowCount"
                                            :per-page="10" :first-text="$t('Table.First')" :prev-text="$t('Table.Previous')"
                                            :next-text="$t('Table.Next')" :last-text="$t('Table.Last')">
                                        </b-pagination>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>

                </div>
            </div>
            <loading v-model:active="loading" :can-cancel="true" :is-full-page="false"></loading>
            <invoicedetailsprint :show="show" v-if="show" :reportsrc="reportsrc" :changereport="changereport"
                @close="show = false" @IsSave="IsSave" />

            <SendEmail :show="emailComposeShow" v-if="emailComposeShow" @close="emailComposeShow = false" :id="saleId" :from="formName" :customerEmail="customerEmail"></SendEmail>

        </div>

    </div>
    <div v-else>
        <acessdenied></acessdenied>
    </div>
</template>

<script>
import clickMixin from '@/Mixins/clickMixin';
import moment from "moment";
import Loading from 'vue-loading-overlay';
import 'vue-loading-overlay/dist/css/index.css';

export default {
    mixins: [clickMixin],
    props: ['formName'],
    components: {
            Loading
        },
    data: function () {
        return {
            customerEmail:'',
            emailComposeShow: false,
            saleId:'',
            
            allowBranches: false,
            isloginhistory: true,
            terminalType: '',
            randerToogle: 0,
            customerId: '',
            monthObj: '',
            year: '',
            randerforempty: 0,
            month: 0,
            isDisable: false,
            isDisableMonth: false,
            fromDate: '',
            toDate: '',
            user: '',
            userId: '',
            terminalId: '',
            advanceFilters: false,
            isDisabled: false,
            orderId: '',
            loading: false,
            isDevliveryChallan: false,
            active: 'Approved',
            isPurchase: false,
            show: false,
            isCloseChallan: false,
            saleOrderId: '',
            isReservedId: '',
            ReservedDeliveryChallanbool: false,
            isAddChallan: false,
            isAdd: false,
            pdfShow: false,
            payment: false,
            totalAmount: 0,
            printpdfRender: 0,
            customerAccountId: '',
            purchaseId: '',
            search: '',
            searchQuery: '',
            saleOrderList: [],
            deliveryChallanList: [],
            currentPage: 1,
            pageCount: '',
            rowCount: '',
            currency: '',
            headerFooter: {
                footerEn: '',
                footerAr: '',
                company: ''
            },

            selected: [],
            selectAll: false,
            isSaleOrderTracking: false,
            updateApprovalStatus: {
                id: '',
                approvalStatus: ''
            },
            printDetails: [],
            printRender: 0,
            randerList: 0,
            isFifo: false,
            openBatch: 0,

            english: '',
            arabic: '',

            customerInformation: '',
            expandSale: false,
        }
    },
    computed: {
            isFilterButtonDisabled() {
      return (
        this.customerId ||
        this.monthObj ||
        this.fromDate ||
        this.toDate ||
        this.terminalId ||
        this.user
      );
    },
  },
    watch: {
        // search: function (val) {
        //     this.getData(this.search, val, this.active);
        // },
        formName: function () {
            this.makeActive('Approved');
        }
    },

    methods: {
        sendEmail: function (saleId, email) {
            this.saleId = saleId
            this.customerEmail = email
            this.emailComposeShow = true;
        },
        // sendEmail(id)
        //     {
        //         var root = this;
        //         var token = '';

        //         if (token == '') 
        //         {
        //             token = localStorage.getItem('token');
        //         }
        //         this.loading = true;
        //         root.$https.get('Sale/SendSaleEmail?id=' + id + '&isCreditNote=true', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
        //             if (response.data.isSuccess) {
        //                 root.$swal({
        //                     title: "Email",
        //                     text: response.data.isAddUpdate,
        //                     type: 'success',
        //                     icon: 'success',
        //                     showConfirmButton: false,
        //                     timer: 1500,
        //                     timerProgressBar: true,
        //                 });
        //             }
        //             else
        //             {
        //                 root.$swal({
        //                     title: 'Error',
        //                     text: response.data.isAddUpdate,
        //                     type: 'error',
        //                     icon: 'error',
        //                     showConfirmButton: false,
        //                     timer: 1500,
        //                     timerProgressBar: true,
        //                 });
        //             }
        //             root.loading = false;
        //         });
        //     },
        AdvanceFilterFor: function () {
            

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

            } else {
                this.isDisable = false;
                this.isDisableMonth = false;
                if (this.$refs.userDropdown != null) {
                    this.$refs.userDropdown.EmptyRecord();
                }
                this.user = '';
                this.userId = '';

                this.year = '';
                this.search = "";
                this.fromDate = '';
                this.toDate = '';
                this.month = '';
                this.monthObj = '';
                this.terminalId = '';
                this.customerId = '';
                this.customerType = '';
                this.randerforempty++;

                if (this.$refs.CustomerDropdown != undefined)
                    this.$refs.CustomerDropdown.Remove();

            }
            
            this.getData(this.search, this.currentPage, this.active, this.fromDate, this.toDate, this.fromTime, this.toTime, this.terminalId, this.userId, this.customerId, this.customerType);

        },


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
        ViewCustomerInfo: function (id) {
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }

            root.$https.get('/Contact/ContactLedgerDetail?id=' + id + '&documentType=' + "SaleOrder" + '&isService=false', { headers: { "Authorization": `Bearer ${token}` } })
                .then(function (response) {
                    if (response.data != null) {
                        root.customerInformation = response.data;
                    }
                },
                    function (error) {
                        console.log(error);
                    });
        },
        search22: function () {
            this.getData(this.search, this.currentPage, this.active);
        },

        clearData: function () {
            this.search = "";
            this.getData(this.search, this.currentPage, this.active);

        },

        getDate: function (date) {
            return moment(date).format('LL');
        },

        GotoPage: function (link) {
            this.$router.push({ path: link });
        },
        paymentModel: function (purchaseId, totalAmount, customerAccountId) {
            this.purchaseId = purchaseId;
            this.totalAmount = totalAmount;
            this.customerAccountId = customerAccountId;
            this.payment = true;
        },
        paymentSave: function () {
            this.payment = false;
        },
        changeStatus: function (id) {
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }

            var saleOrder = { id: id, isClose: true, isFifo: this.isFifo };
            this.$https.post('/Purchase/SaveSaleOrderInformation', saleOrder, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                if (response.data != null) {
                    root.$swal({
                        title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved!' : '!تم الحفظ',
                        text: "Sale Order Closed Successfully!",
                        type: 'success',
                        icon: 'success',
                        showConfirmButton: true
                    });
                    root.getPage();
                }
            });
        },
        DeleteFile: function () {
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            this.$https.get('/Purchase/DeletePo', this.selected, { headers: { "Authorization": `Bearer ${token}` } })
                .then(function (response) {
                    if (response.data != null) {
                        if (response.data.message.id != '00000000-0000-0000-0000-000000000000') {
                            root.$swal({
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Deleted!' : 'تم الحذف!',
                                text: response.data.message.isAddUpdate,
                                type: 'success',
                                confirmButtonClass: "btn btn-success",
                                buttonsStyling: false
                            }).then(function (result) {
                                if (result) {
                                    root.$router.push('/purchase');
                                }
                            });
                        } else {
                            root.$swal({
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                                text: response.data.message.isAddUpdate,
                                type: 'error',
                                confirmButtonClass: "btn btn-danger",
                                buttonsStyling: false
                            });
                        }
                    }
                },
                    function () {

                        root.$swal({
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                            text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Update UnSuccessfully' : 'التحديث غير ناجح',
                            type: 'error',
                            confirmButtonClass: "btn btn-danger",
                            buttonsStyling: false
                        });
                    });
        },
        select: function () {
            this.selected = [];
            if (!this.selectAll) {
                for (let i in this.saleOrderList) {
                    this.selected.push(this.saleOrderList[i].id);
                }
            }
        },
        getPage: function () {
            this.getData(this.search, this.currentPage, this.active);
        },

        makeActive: function (item) {
            this.active = item;
            this.selectAll = false;
            this.selected = [];
            this.getData(this.search, this.search, item);
        },

        getData: function (search, currentPage, status) {
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            var isCreditNote = false;
            if (this.formName == 'CreditNote') {
                isCreditNote = true;
            }

            var branchId = localStorage.getItem('BranchId');

            localStorage.setItem('currentPage', this.currentPage);
            localStorage.setItem('active', this.active);
            this.$https.get('/PaymentVoucher/CreditNoteList?status=' + status + '&searchTerm=' + search + '&pageNumber=' + currentPage + '&isCreditNote=' + isCreditNote + '&branchId=' + branchId + '&fromDate=' + this.fromDate + '&toDate=' + this.toDate + '&fromTime=' + this.fromTime + '&toTime=' + this.toTime + '&userId=' + this.userId + '&customerId=' + this.customerId + '&month=' + this.month + '&year=' + this.year + '&terminalId=' + this.terminalId, { headers: { "Authorization": `Bearer ${token}` } })
                .then(function (response) {

                    root.saleOrderList = response.data.results;
                    root.pageCount = response.data.pageCount;
                    root.rowCount = response.data.rowCount;
                    root.currentPage = response.data.currentPage;
                    root.randerList++;

                });
        },

        RemovePurchaseOrder: function (id) {


            var root = this;
            // working with IE and Chrome both
            this.$swal({
                title: (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Are you sure?' : 'هل أنت متأكد؟',
                text: (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'You will not be able to recover this!' : 'لن تتمكن من استرداد هذا!',
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Yes, delete it!' : 'نعم ، احذفها!',
                closeOnConfirm: false,
                closeOnCancel: false
            }).then(function (result) {
                if (result) {

                    var token = '';
                    if (token == '') {
                        token = localStorage.getItem('token');
                    }
                    root.$https.get('/Purchase/DeleteSaleOrder?Id=' + id, { headers: { "Authorization": `Bearer ${token}` } })
                        .then(function (response) {
                            if (response.data.message.id != '00000000-0000-0000-0000-000000000000') {
                                root.$swal({
                                    title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Deleted!' : 'تم الحذف!',
                                    text: response.data.message.isAddUpdate,
                                    type: 'success',
                                    confirmButtonClass: "btn btn-success",
                                    buttonsStyling: false
                                });
                            } else {
                                root.$swal({
                                    title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                                    text: response.data.message.isAddUpdate,
                                    type: 'error',
                                    confirmButtonClass: "btn btn-danger",
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
                }
                else {
                    this.$swal((this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Cancelled!' : 'ألغيت!', (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Your file is still intact!' : 'ملفك لا يزال سليما!', (this.$i18n.locale == 'en' || root.isLeftToRight()) ? 'info' : 'معلومات');
                }
            });
        },

        AddPurchaseOrder: function () {
            // this.$router.push('/AddCreditNote?formName=' + this.formName);
             this.$router.push({
                path: '/AddCreditNote',
                query: {
                    Add: true,
                    isDeliveryChallan: true,
                    formName: this.formName,
                }
            })
        },

        PrintDeliveryChallan: function (id) {
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            root.$https.get('/Purchase/DeliveryChallanDetail?id=' + id + '&isSale=' + false, { headers: { "Authorization": `Bearer ${token}` } })
                .then(function (response) {

                    if (response.data != null) {

                        root.printDetails = response.data;
                        root.printRender++;
                        root.isDevliveryChallan = true;
                    }
                });
        },

        EditPurchaseOrder: function (id) {
               this.loading = true;
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            root.$https.get('/PaymentVoucher/CreditNoteDetail?id=' + id, { headers: { "Authorization": `Bearer ${token}` } })
                .then(function (response) {

                    if (response.data != null) {
                        root.$store.GetEditObj(response.data);
                        root.$router.push({
                            path: '/AddCreditNote',
                            query: { 
                                data: '',
                                Add: false,
                                formName: root.formName,
                            }
                        })
                    }
                },
                    function (error) {
                        root.loading = false;
                        console.log(error);
                    });
        },
        DuplicateSaleOrder: function (id) {
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            var childFormName = this.formName;
            root.$https.get('/Purchase/SaleOrderDetail?Id=' + id + '&isFifo=' + this.isFifo + '&openBatch=' + this.openBatch, { headers: { "Authorization": `Bearer ${token}` } })
                .then(function (response) {
                    if (response.data != null) {
                        response.data.isDuplicate = true;
                        root.$router.push({
                            path: '/AddSaleOrder',
                            query: 
                            { 
                                data: response.data,
                                formName: childFormName,
                            }
                        })
                    }
                },
                    function (error) {
                        root.loading = false;
                        console.log(error);
                    });
        },

        GetRecordOfDelivery: function () {
            this.ReservedDeliveryChallanbool = false;
            this.ViewDeliveryChallan(this.saleOrderId);

        },

        EditDeliveryChallan: function (id, View) {

            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            var isView = false;
            if (View == true) {
                isView = true;
            }
            root.$https.get('/Purchase/DeliveryChallanDetail?id=' + id + '&isSale=' + false, { headers: { "Authorization": `Bearer ${token}` } })
                .then(function (response) {
                    if (response.data != null) {
                        root.$store.GetEditObj(response.data);
                        root.$router.push({
                            path: '/AddDeliveryChallan',
                            query: {
                                data: '',
                                Add: false,
                                isSaleOrder: true,
                                isView: isView,


                            }
                        })

                    }
                },
                    function (error) {
                        root.loading = false;
                        console.log(error);
                    });
        },
        ReservedDeliveryChallan: function (id, fromSale, close) {

            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            if (fromSale) {
                root.$https.get('/Purchase/SaleOrderDetail?id=' + id + '&isSale=' + false + '&DeliveryChallan=' + true, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data != null) {
                            root.deliveryChallanRecord = response.data;
                            root.isAdd = true;
                            root.ReservedDeliveryChallanbool = true;

                            root.$router.push({
                                query: {
                                    data: undefined,

                                }
                            })
                        }
                    },
                        function (error) {
                            root.loading = false;
                            console.log(error);
                        });
            }
            else {
                var manualClose = false;
                if (close == true) {
                    manualClose = true;
                }
                root.$https.get('/Purchase/DeliveryChallanDetail?id=' + id + '&isSale=' + false + '&isReserved=' + true + '&manualClose=' + manualClose, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data != null) {
                            root.deliveryChallanRecord = response.data;
                            root.isCloseChallan = response.data.isClose;
                            if (root.isCloseChallan) {
                                root.isAdd = false;
                                root.ReservedDeliveryChallanbool = false;
                            }
                            else {
                                root.isAdd = false;
                                root.ReservedDeliveryChallanbool = true;
                            }



                        }
                    },
                        function (error) {
                            root.loading = false;
                            console.log(error);
                        });
            }

        },

        ViewDeliveryChallan: function (id) {

            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            root.saleOrderId = id
            root.$https.get('/Purchase/DeliveryChallanList?documentId=' + id + '&isSale=' + false + '&isDropdown=' + true + '&openBatch=' + this.openBatch, { headers: { "Authorization": `Bearer ${token}` } })
                .then(function (response) {
                    if (response.data != null) {
                        root.deliveryChallanList = response.data.deliveryChallanListLookUpModels;
                        if (response.data.isReserved != null) {
                            root.isReservedId = response.data.isReserved
                            root.isCloseChallan = response.data.isClose
                            root.isAddChallan = false;

                        }
                        else {
                            root.isAddChallan = true;
                            root.isCloseChallan = response.data.isClose

                        }


                    }
                },
                    function (error) {
                        root.loading = false;
                        console.log(error);
                    });
        },

        ViewInvoice: function (id) {
            var root = this;
            var token = '';
            var childFormName = this.formName;
            if (token == '') {
                token = localStorage.getItem('token');

            }
            root.$https.get('/Purchase/SaleOrderDetail?Id=' + id + '&isFifo=' + this.isFifo + '&openBatch=' + this.openBatch, { headers: { "Authorization": `Bearer ${token}` } })
                .then(function (response) {
                    if (response.data != null) {
                        root.$store.GetEditObj(response.data);
                        root.$router.push({
                            path: '/SaleOrderView',
                            query: { 
                                data: '',
                                Add: false, 
                                formName: childFormName,
                            }
                        })
                    }
                },
                    function (error) {
                        root.loading = false;
                        console.log(error);
                    });
        },
        DeliveryChllan: function (id) {

            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            root.$https.get('/Purchase/SaleOrderDetail?Id=' + id + '&isFifo=' + this.isFifo + '&openBatch=' + this.openBatch + '&deliveryChallan=' + true, { headers: { "Authorization": `Bearer ${token}` } })
                .then(function (response) {
                    if (response.data != null) {
                        root.$store.GetEditObj(response.data);
                        root.$router.push({
                            path: '/AddDeliveryChallan',
                            query: {
                                data: '',
                                Add: false,
                                isService: false,
                                isSaleOrder: true,

                            }
                        })

                    }
                },
                    function (error) {
                        root.loading = false;
                        console.log(error);
                    });
        },
        getBase64Image: function (path) {
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            this.headerFooter.company.logoPath = 'data:image/png;base64,' + 'iVBORw0KGgoAAAANSUhEUgAAATYAAACjCAMAAAA3vsLfAAAAhFBMVEX///8AcuEAZ98Ab+AAa+AAbeAAad9Tk+cAbuC+1fUFeOLS4/no8/0AZd8AaODv9v1to+vk7/z1+v7d6vqBru3F2vemxfKHsu5Zl+iYvfDU5PnM3/g9iea20PWNte5Rk+gwhOVIjuepx/NjnOl2p+wlfuS30fUAX96cvvAde+M1huWoxfIDNGHbAAAHKElEQVR4nO2d65aiOhBGJQlpVAKId6VVtJ3ROe//fgfvqSQocS3EjrV/NsEJ38qlqlKVabUQBEEQBEEQBEEQBEEQBEEQBEEQBEEQBEEQBEEQBEGQ30E6nG6ybcfrbPNkN2+6N68kDWVimzdHOYlYEFDP8yj1GWGT79q6+W7kQoZWfi/8IfwgmEzA/UVaY1/fiCX49k7V1wbC90wwMq2zt2/D1zOyDSkzinaAb/v19vgteEa2hSgV7TBVxQcscU/IlpB7qhUI9yeqvWxJ9EC1QrdR7f1uGGvZpndn6EW3Yf09bxRb2WZVVCsWOMcNEVvZtqqxZsafvKDvDWIp245XUq2Ypm77WpayaWONRYIxwTXjl2av6H1j2Mm2VgYb88bzwo8Nhz9MFU50X9L/hrCTbQVHmxhcn8QbxS7xk3o73ixWsvXgNgrdgYViBbM6u900VrLBORrt4NMf6KgSlzcFK9k2gdRWX/SV0eayi2Ul215uTLQ1/xsMxmBTV5/fACvZ5KWNGtoC2eiqlg6/BzayxbJs/o/eIAc/Vj1U/PuwkQ1spGynNxgA443X1OV3wEa2viwbNwQjFwxl0wGjja/1BlA2lw23p9c2k33xAz2smrr8DljtpLL/ZLIvgO8VuOzMW8kGZNE3yhi4V047pVaygUnI/6iPp3BHcPlAwUo24AZo9m4Ko0rC5fNSK9nAnuAxZRZmAZDN5R3BMt4G3QAykJ9NYACEjWvsdeMosg3bJZxO8Nowpsaz60Scb5X8BtFr7JteAJTN42X8PeVwdWBzKrLdcDZrL1ZCOWVwOv6hyVYKP8n2Rz0mpYyTiDPtV9webLaytfLgcdtDc6dXNnvZwvIULQm6bfarasdWtla7SjYDc3uKPiFba/dYN8eP5FvPyNYaPdCNEtfzjZ6SrTWM7u0LzHPZqzrzjGytOFfNtNtQEy4HPq48JVvh1Hci04tUrJxf1o48KVurtV4JxcgNmMjbjXzE61lWlC3SC2Rm41UUMRYU+IWvwLKp62bHjaUgMtERyRdlB/wCY1ZpPByNk8lkkizWXcfTTiHxrKB/otfrnUuvIGlB0/1EnKQ3/16P1t/DiutUf3hsPv+cZU2nu8gOYaADEWHL0QMt5uMVuTbn+XT2ml6+F+HCIwwksBUGRbmbFI6L5uAAkJHO7tPWwDiJDCEhKnKzpxQmxNScs/FHCTfVU+TPNiwxZBaVN/cY/YD6vjNxdqf8jGinAnebe8LtU4QbM/9upJsvYfN+cD8wzrY2Jfe/lpnRK5d1+LJq7gXeB+hW4XQgWsjNH3uxgetHCa1qNXtiZtXcY86vb4MqNXvByqq5WiDjHn218D1gJGJcMeLo37MM/WpVuMUbbttvE7grUpKNZnEaz8e+L/1xewk/fmmbKPUZ8/WJyxZ3/9lfjjJ62PYa004nZ+OMRvvrjNNKvhnPB9Ppzz/dxWAuD7cBzO0Gpdrj4/yNOtIylUDngPKLExqO1UMZp5MpgQx+Dh8mzOMeyKOHY8pfSfZZ31OSkVYv6H5DdOGGoD72oGitYXRHmFiZpSKstetNApKUIy2HWY1+jGFOs+ILdOHCZyqTcQRQIHrNQO13z8xmXYkZzEHV6zngyucP1OfOIJv8t8/cwNOsM6IDV0KubZXQBqS5+twZ5EnHr0fDiTGYRv/BetKl/nMgRZXuX/klrwQkzJPrSlYi2xLWXBns2QQYw86WqoWyDtF15zPLFmxCuwo/ZwsjgQ7kgWz+IHxUTzr9DNlSINs1OGSWje3AJOX/6b8HDRT/lZ/yUkDp2XX4mGWLhmBLYAb7Al544W6sUq50vBUylsjWAxd9mJwn0D5w9y4ysPVdJ5VZNk9JhiPamf0c+F4OF12N5Fgtv5j9RtkOgwcs+fqNFrCQjbubIgjDbZe9dPP3dOczkWU4GBzQ81evGlvDuIBwOOAG7frO6UvjS5ZbJj09mnVwAHIQ45hDT97kRTgDtLRoRxYizXxVBWhhUCZl1ihjzRBPcYgYpiVQPr1MrXQHzupPJRqhog2ZnILoaVvLb6h8PfmvRLl0zeP+ZtQeDkdJYLznKVGaB1GwWi5XvnZSHxl8L4eItbOTgPGIc+WE6nL/sN7cowXaH50/lx89uiH8wO0aXXUJK0E4n1mZl6WqSbBb/PurSkEpcfqU9EjqPczqAIWO28f1y9xdv+pGaFiboGpgeU/3j4anmhDnKOH+7sQTyqaYZvfTZz6jxu/ApjwjJmB6tvigtC7ykOzr8nG8wndgHnBU5KbMyPm2JKWyaP9RhR3pmGv/AZNHyb7MRVp3iL41+GTlbtijhHS3F1I5R+BzsrwnQvuLcKk9ZRFPnL5+vZT+buKJw9myECwbtB9FftL2IGPn9nS5cP9igTukvUOJZPVY2bF96HBsDUEQBEEQBEEQBEEQBEEQBEEQBEEQBEEQBEEQBEEQBEEQmf8BckJbXFsgizoAAAAASUVORK5CYII=';

            root.$https
                .get('/Contact/GetBaseImage?filePath=' + path, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

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

            root.$https.get("/Company/GetCompanyDetail?id=" + localStorage.getItem('CompanyID'), { headers: { Authorization: `Bearer ${token}` }, })
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

            root.$https.get('/Sale/printSettingDetail', { headers: { "Authorization": `Bearer ${token}` } })
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

        PrintInvoice: function (value) {
            this.GetHeaderDetail();
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            root.$https.get("/PaymentVoucher/CreditNoteDetail?id=" + value, { headers: { Authorization: `Bearer ${token}` }, })
                .then(function (response) {

                    if (response.data != null) {
                        root.printDetails = response.data;
                        if (localStorage.getItem('IsMultiUnit') == 'true' && localStorage.getItem('decimalQuantity') == 'true') {
                            root.printDetails.saleOrderItems.forEach(function (x) {
                                x.highQty = parseInt(parseFloat(x.quantity) / parseFloat(x.product.unitPerPack));
                                x.newQuantity = parseFloat(parseFloat(x.quantity) % parseFloat(x.product.unitPerPack));
                                x.unitPerPack = x.product.unitPerPack;
                            });
                        }
                        if (localStorage.getItem('IsMultiUnit') == 'true' && localStorage.getItem('decimalQuantity') != 'true') {
                            root.printDetails.saleOrderItems.forEach(function (x) {
                                x.highQty = parseInt(parseInt(x.quantity) / parseInt(x.product.unitPerPack));
                                x.newQuantity = parseInt(parseInt(x.quantity) % parseInt(x.product.unitPerPack));
                                x.unitPerPack = x.product.unitPerPack;
                            });
                        }
                        root.printRender++;
                        root.show = true;
                    }
                });
        },
        PrintRdlc: function (val, isDownload) {
            var root = this;
            var companyId = '';
            if (this.$store.isAuthenticated) {
                companyId = localStorage.getItem('CompanyID');
            }
            if (isDownload) {
                this.$https.get(this.$ReportServer + '/SalesModuleReports/CreditNote/CreditNoteReport.aspx?id=' + val + '&CompanyId=' + companyId + '&formName=' + this.formName + '&isDownload=' + isDownload
                    , { responseType: 'blob' }).then(function (response) {
                        root.loading = false;
                        
                        const url = window.URL.createObjectURL(new Blob([response.data]));
                        const link = document.createElement('a');
                        link.href = url;
                        var date = moment().format('DD MMM YYYY');
                        link.setAttribute('download', root.formName + date + '.pdf');
                        document.body.appendChild(link);
                        link.click();

                    })
            }
            else {
                this.reportsrc = this.$ReportServer + '/SalesModuleReports/CreditNote/CreditNoteReport.aspx?id=' + val + '&CompanyId=' + companyId + '&formName=' + this.formName + '&isDownload=' + isDownload
              
                this.changereport++;
                this.show = !this.show;
            }
        },

        PrintInvoicePdf: function (value) {
            this.GetHeaderDetail();
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            root.$https.get("/Purchase/SaleOrderDetail?id=" + value + '&isFifo=' + this.isFifo + '&openBatch=' + this.openBatch, {
                headers: { Authorization: `Bearer ${token}` },
            })
                .then(function (response) {
                    if (response.data != null) {
                        root.printDetails = response.data;
                        if (localStorage.getItem('IsMultiUnit') == 'true' && localStorage.getItem('decimalQuantity') == 'true') {
                            root.printDetails.saleOrderItems.forEach(function (x) {
                                x.highQty = parseInt(parseFloat(x.quantity) / parseFloat(x.product.unitPerPack));
                                x.newQuantity = parseFloat(parseFloat(x.quantity) % parseFloat(x.product.unitPerPack));
                                x.unitPerPack = x.product.unitPerPack;
                            });
                        }
                        if (localStorage.getItem('IsMultiUnit') == 'true' && localStorage.getItem('decimalQuantity') != 'true') {
                            root.printDetails.saleOrderItems.forEach(function (x) {
                                x.highQty = parseInt(parseInt(x.quantity) / parseInt(x.product.unitPerPack));
                                x.newQuantity = parseInt(parseInt(x.quantity) % parseInt(x.product.unitPerPack));
                                x.unitPerPack = x.product.unitPerPack;
                            });
                        }
                        root.printpdfRender++;
                        root.pdfShow = true;
                    }
                });
        },
    },
    created: function () {

        this.GetHeaderDetail();
        if (this.$route.query.data == 'AddSaleOrders' + this.formName) {
            this.$emit('update:modelValue', 'AddSaleOrders' + this.formName);

        }
        else {
            this.$emit('update:modelValue', this.$route.name + this.formName);

        }
        this.allowBranches = localStorage.getItem('AllowBranches') == 'true' ? true : false;
    },
    mounted: function () {
        this.english = localStorage.getItem('English');
        this.arabic = localStorage.getItem('Arabic');

        if (localStorage.getItem('active') != null && localStorage.getItem('active') != '' && localStorage.getItem('active') != undefined) {
            this.currentPage = parseInt(localStorage.getItem('currentPage'));
            this.active = (localStorage.getItem('active'));
            this.getPage();
        }
        else {
            if (this.isValid('CanDraftSaleOrder') || (this.isValid('CanDraftSaleOrderTracking') && this.formName == 'SaleOrderTracking')) {
                this.makeActive("Approved");
            }
            else if (this.isValid('CanViewSaleOrder') || (this.isValid('CanViewSaleOrderTracking') && this.formName == 'SaleOrderTracking')) {
                this.makeActive("Approved");
            }
        }

        this.isFifo = localStorage.getItem('fIFO');
        var batch = localStorage.getItem('openBatch')
        if (batch != undefined && batch != null && batch != "") {
            this.openBatch = batch
        }
        else {
            this.openBatch = 1
        }

        this.currency = localStorage.getItem('currency');
    },
    updated: function () {
        if (this.selected.length < this.saleOrderList.length) {
            this.selectAll = false;
        } else if (this.selected.length == this.saleOrderList.length) {
            if (this.selected.length == 0) {
                this.selectAll = false;
            }
            else {
                this.selectAll = true
            }
        }
    }
}
</script>