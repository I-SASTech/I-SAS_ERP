<template>
    <div class="row" v-if="isValid('CanViewDeliveryNote')">
        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">{{ $t('DeliveryNote.DeliveryNote') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('Quotation.Home') }}</a>
                                    </li>
                                    <li class="breadcrumb-item active">
                                        {{ $t('DeliveryNote.DeliveryNote') }}
                                    </li>
                                </ol>
                            </div>

                            <div class="col-auto align-self-center">
                                <a v-if="isValid('CanAddDeliveryNote')" v-on:click="AddPurchaseOrder"
                                    href="javascript:void(0);" class="btn btn-sm btn-outline-primary mx-1">
                                    <i class="align-self-center icon-xs ti-plus"></i>
                                    {{ $t('Quotation.AddNew') }}
                                </a>
                                <a v-on:click="GotoPage('/StartScreen')" href="javascript:void(0);"
                                    class="btn btn-sm btn-outline-danger">
                                    {{ $t('Quotation.Close') }}
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
                                <input v-model="search" type="text" class="form-control"
                                    :placeholder="$t('DeliveryNote.SearchByQuotation')"
                                    aria-label="Example text with button addon" aria-describedby="button-addon1">
                                    <a class="btn btn-soft-primary" v-on:click="AdvanceFilterFor" id="button-addon2"
                                    value="Advance Filter">
                                    <i class="fa fa-filter"></i>
                                </a>
                            </div>
                        </div>
                        <div class=" col-lg-4 mt-1" v-if="!advanceFilters">

                            <button v-on:click="search22(true)" :disabled="!search"  type="button" class="btn btn-outline-primary mt-3">
                                {{ $t('Sale.ApplyFilter') }}
                            </button>
                            <button v-on:click="clearData(false)" :disabled="!search"  type="button" class="btn btn-outline-primary mx-2 mt-3">
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

                    <div class="tab-content">

                        <div class="table-responsive">
                            <table class="table mb-0">
                                <thead class="thead-light table-hover">
                                    <tr>
                                        <th>#</th>
                                        <th>
                                            {{ $t('DeliveryNote.DeliveryOrderNo') }}

                                        </th>
                                        <th>
                                            {{ $t('DeliveryNote.Date') }}
                                        </th>

                                        <th>
                                            {{ $t('DeliveryNote.CustomerName') }}

                                        </th>
                                        <th v-if="allowBranches">
                                            {{ $t('DailyExpense.BranchCode') }}
                                        </th>
                                        <th class="text-center">
                                            {{ $t('SaleOrder.Status') }}
                                        </th>

                                        <th style="width: 70px;" class="text-end">
                                        </th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr v-for="(purchaseOrder, index) in deliveryChallanList" v-bind:key="purchaseOrder.id">
                                        <td>
                                            {{ index + 1 }}
                                        </td>

                                        <td v-if="purchaseOrder.editDeliveryChallanId == null">
                                            <strong>
                                                <a href="javascript:void(0)" data-bs-dismiss="offcanvas" aria-label="Close"
                                                    v-on:click="EditDeliveryChallan(purchaseOrder.id, false)">{{
                                                        purchaseOrder.registrationNumber }}</a>
                                            </strong>

                                        </td>
                                        <td v-else>{{ purchaseOrder.registrationNumber }}</td>
                                        <td>
                                            {{ purchaseOrder.date }}
                                        </td>

                                        <td
                                            v-if="purchaseOrder.customerName == null || purchaseOrder.customerName == '' || purchaseOrder.customerName == undefined">
                                            <a href="javascript:void(0)"
                                                v-on:click="ViewCustomerInfo(purchaseOrder.customerId)"
                                                data-bs-toggle="offcanvas" data-bs-target="#offcanvasRight2"
                                                aria-controls="offcanvasRight">{{ purchaseOrder.customerNameAr }}</a>
                                        </td>
                                        <td v-else>
                                            <a href="javascript:void(0)"
                                                v-on:click="ViewCustomerInfo(purchaseOrder.customerId)"
                                                data-bs-toggle="offcanvas" data-bs-target="#offcanvasRight2"
                                                aria-controls="offcanvasRight">{{ purchaseOrder.customerName }}</a>
                                        </td>
                                        <td v-if="allowBranches">
                                            {{ purchaseOrder.branchCode }}
                                        </td>
                                        <td class="text-center" v-bind:key="randerToogle">
                                            <toggle-button v-on:change="openmodel(purchaseOrder.id)" class="ml-2 mt-2"
                                                color="#3178F6" v-bind:key="randerToogle" v-if="!purchaseOrder.isClose" />

                                            <div class="tooltip badge rounded-pill badge-soft-success"
                                                v-else-if="purchaseOrder.isProcessed">
                                                Issued
                                                <span class="tooltiptext">{{ purchaseOrder.reason }}</span>
                                            </div>

                                            <div class="tooltip badge rounded-pill badge-soft-success"
                                                v-else-if="purchaseOrder.isClose">
                                                Closed
                                                <span class="tooltiptext">{{ purchaseOrder.reason }}</span>
                                            </div>
                                        </td>

                                        <td class="text-end">

                                            <button type="button" class="btn btn-light dropdown-toggle"
                                                data-bs-toggle="dropdown" aria-expanded="false">{{ $t('SaleOrder.Action') }}
                                                <i class="mdi mdi-chevron-down"></i></button>
                                            <div class="dropdown-menu">
                                                <a class="dropdown-item" href="javascript:void(0)"
                                                    v-on:click="EditDeliveryChallan(purchaseOrder.id)"
                                                    v-if="!purchaseOrder.isClose">Edit Delivery Challan</a>
                                                <a class="dropdown-item" href="javascript:void(0)"
                                                    v-on:click="PrintRdlc(purchaseOrder.id, false)">View Delivery
                                                    Challan</a>
                                                <a class="dropdown-item" href="javascript:void(0)"
                                                    v-on:click="PrintRdlc(purchaseOrder.id, false)">{{
                                                        $t('SaleOrder.A4Print') }}</a>
                                                <a class="dropdown-item" href="javascript:void(0)"
                                                    v-on:click="PrintRdlc(purchaseOrder.id, true)">{{ $t('PdfDownload')
                                                    }}</a>

                                                <a class="dropdown-item" href="javascript:void(0)" v-if="purchaseOrder.isDefault"
                                                        v-on:click="sendEmail(purchaseOrder.id, purchaseOrder.customerEmail)">{{
                                                            $t('Email') }}</a>

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
                                    <b-pagination pills size="sm" v-model="currentPage" :total-rows="rowCount"
                                        :per-page="10" :first-text="$t('Table.First')" :prev-text="$t('Table.Previous')"
                                        :next-text="$t('Table.Next')" :last-text="$t('Table.Last')">
                                    </b-pagination>
                                </div>
                            </div>
                        </div>

                        <loading v-model:active="loading" :can-cancel="true" :is-full-page="false"></loading>
                        <invoicedetailsprint :show="show" v-if="show" :reportsrc="reportsrc" :changereport="changereport"
                            @close="show = false" @IsSave="IsSave" />


                    </div>

                </div>
            </div>
            <div class="offcanvas offcanvas-end px-0" tabindex="-1" id="offcanvasRight2"
                aria-labelledby="offcanvasRightLabel">
                <div class="offcanvas-header">
                    <h5 id="offcanvasRightLabel" class="m-0">Customer Info ({{ customerInformation.runningBalance }})</h5>
                    <button type="button" class="btn-close text-reset" data-bs-dismiss="offcanvas"
                        aria-label="Close"></button>
                </div>
                <div class="offcanvas-body">
                    <div class="row">
                        <div class="col-lg-12 form-group" v-if="english == 'true'">
                            <label>{{$englishLanguage($t('Sale.CustomerName'))   }}</label>
                            <input class="form-control" type="text" v-model="customerInformation.englishName" disabled />
                        </div>
                        <div class="col-lg-12 form-group" v-if="isOtherLang()">
                            <label>{{ $t('Sale.CustomerNameArabic') }}</label>
                            <input class="form-control" type="text" v-model="customerInformation.arabicName" disabled />
                        </div>
                        <div class="col-lg-12 form-group">
                            <label>{{ $t('AddCustomer.CustomerPhone') }}</label>
                            <input class="form-control" type="text" v-model="customerInformation.contactNo1" disabled />
                        </div>
                        <div class="col-lg-12 form-group">
                            <label>{{ $t('AddCustomer.CommercialRegistrationNo') }}</label>
                            <input class="form-control" type="text" v-model="customerInformation.commercialRegistrationNo"
                                disabled />
                        </div>
                        <div class="col-lg-12 form-group">
                            <label>{{ $t('AddCustomer.VAT/NTN/Tax No') }}</label>
                            <input class="form-control" type="text" v-model="customerInformation.vatNo" disabled />
                        </div>
                        <div class="col-lg-12 form-group text-center">
                            <button v-if="expandSale" v-on:click="expandSale = false" type="button"
                                class="btn btn-outline-info btn-icon-circle btn-icon-circle-sm"><i
                                    class="ti-angle-double-up"></i></button>
                            <button v-else v-on:click="expandSale = true" type="button"
                                class="btn btn-outline-info btn-icon-circle btn-icon-circle-sm"><i
                                    class="ti-angle-double-down"></i></button>
                        </div>
                        <div v-if="expandSale" class="col-lg-12 form-group">
                            <h6 class="text-danger">Showing Last Three Month Invoices</h6>
                            <p v-for="(sale, index) in customerInformation.invoiceList" v-bind:key="index"
                                style="border-bottom: 1px solid #cbcbcb; ">
                                <a href="javascript:void(0);" data-bs-dismiss="offcanvas" aria-label="Close"
                                    v-on:click="ViewInvoice(sale.id)">
                                    <span>{{ index + 1 }}- {{ sale.registrationNumber }} </span>
                                    <span class="float-end">{{ currency }}
                                        {{ parseFloat(sale.netAmount).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g,
                                            "$1,") }}</span>
                                </a>
                                <br />
                                <small>{{ getDate(sale.date) }}</small>
                            </p>
                        </div>
                    </div>
                </div>
            </div>
            <reasonsaleorder :show="show1" v-if="show1" @close="CloseRefresh" @SaveRecord="SaveRecord" />
            <SendEmail :show="emailComposeShow" v-if="emailComposeShow" @close="emailComposeShow = false" :id="saleId" :from="'DeliveryChallan'" :customerEmail="customerEmail"></SendEmail>

        </div>

    </div>
    <div v-else>
        <acessdenied></acessdenied>
    </div>
</template>

<script>
import clickMixin from '@/Mixins/clickMixin'
import moment from "moment";
 import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
export default {
    mixins: [clickMixin],
     components: {
            Loading
        },
    data: function () {
        return {
            customerEmail:'',
            saleId:'',

            formName: "DeliveryChallan",
            allowBranches: false,
            isloginhistory: true,
            terminalType:'',
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
            colorVariants: false,
            orderId: '',
            emailComposeShow: false,
            show: false,
            isDevliveryChallan: false,
            loading: false,
            IsService: false,
            showTemplate: false,
            pdfShow: false,
            active: 'Draft',
            search: '',
            searchQuery: '',
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
            printDetails: [],
            printRender: 0,
            printpdfRender: 0,
            selected: [],
            selectAll: false,
            updateApprovalStatus: {
                id: '',
                approvalStatus: ''
            },
            quotationTemplate: {
                id: '00000000-0000-0000-0000-000000000000',
                templateName: '',
                description: '',
            },

            english: '',
            arabic: '',

            customerInformation: '',
            purchaseOrderId: '',
            expandSale: false,
            show1: false,
        }
    },
    computed: {
            isFilterButtonDisabled() {
      return (
        this.customerId ||
        this.monthObj ||
        this.fromDate ||
        this.toDate ||
        this.user
      );
    },
  },
    methods: {
        sendEmail: function (saleId, email) {
            this.saleId = saleId;
            this.customerEmail = email;
            this.emailComposeShow = true;
        },
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
        search22: function () {
            this.getData(this.search, this.currentPage, this.active);
        },

        clearData: function () {
            this.search = "";
            this.getData(this.search, this.currentPage, this.active);

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
        ViewCustomerInfo: function (id, cashCustomerId) {
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }

            var cashCustomer = false;
            if (id == null) {
                cashCustomer = true;
                id = cashCustomerId;
            }
            var documentType = "DeliveryChallan";
            root.$https.get('/Contact/ContactLedgerDetail?id=' + id + '&documentType=' + documentType + '&cashCustomer=' + cashCustomer + '&isService=' + this.isService + '&lastThreeMonth=' + true, {
                headers: {
                    "Authorization": `Bearer ${token}`
                }
            })
                .then(function (response) {
                    if (response.data != null) {
                        root.customerInformation = response.data;
                    }
                },
                    function (error) {
                        console.log(error);
                    });
        },
        CloseRefresh: function () {

            this.show1 = false;
            this.getPage();
        },
        SaveRecord: function (x) {
             this.loading = true;
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }

            var saleOrder = {
                id: this.purchaseOrderId,
                isClose: true,
                isDeliveryChallan: true,
                reason: x
            };
            this.$https.post('/sale/SaveSaleInformationReason', saleOrder, {
                headers: {
                    "Authorization": `Bearer ${token}`
                }
            }).then(function (response) {
                root.loading = false
                if (response != undefined) {
                    root.show1 = false;
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
        openmodel: function (id) {
            this.purchaseOrderId = id;
            this.show1 = true;
        },

        getDate: function (date) {
            return moment(date).format('LL');
        },

        GotoPage: function (link) {
            this.$router.push({
                path: link
            });
        },
        getPage: function () {
            this.getData(this.search, this.currentPage, this.active);
        },
        

        PrintDeliveryChallan: function (id) {
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            root.$https.get('/Purchase/DeliveryChallanDetail?id=' + id + '&isSale=' + true, {
                headers: {
                    "Authorization": `Bearer ${token}`
                }
            })
                .then(function (response) {

                    if (response.data != null) {

                        root.printDetails = response.data;
                        root.printRender++;
                        root.isDevliveryChallan = true;
                    }
                });
        },

        getData: function (id) {

            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            this.IsService = localStorage.getItem('IsSimpleInvoice') == 'true' ? false : true;
            var branchId = localStorage.getItem('BranchId');

            this.SaleInvoiceId = id;
            root.$https.get('/Purchase/DeliveryChallanList?documentId=' + '00000000-0000-0000-0000-000000000000' + '&isSale=' + true + '&openBatch=' + this.openBatch + '&IsDropDown=' + false + '&IsService=' + this.IsService + '&branchId=' + branchId + '&searchTerm=' + this.search +'&fromDate=' + this.fromDate + '&toDate=' + this.toDate + '&fromTime=' + this.fromTime + '&toTime=' + this.toTime + '&userId=' + this.userId + '&customerId=' + this.customerId + '&month=' + this.month + '&year=' + this.year+ '&pageNumber=' + this.currentPage , {
                headers: {
                    "Authorization": `Bearer ${token}`
                }
            })
                .then(function (response) {
                    if (response.data != null) {

                        root.deliveryChallanList = response.data.deliveryChallanListLookUpModels;
                        root.pageCount = response.data.pageCount;
                        root.rowCount = response.data.rowCount;
                        root.currentPage = response.data.currentPage;
                    }
                },
                    function (error) {
                        root.loading = false;
                        console.log(error);
                    });
        },

        AddPurchaseOrder: function () {

            this.$router.push({
                path: '/AddDeliveryChallan',
                query: {
                    Add: true,
                    isDeliveryChallan: true,

                }
            })

        },
        EditPurchaseOrder: function (id) {
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            root.$https.get('/Purchase/SaleOrderDetail?Id=' + id, {
                headers: {
                    "Authorization": `Bearer ${token}`
                }
            })
                .then(function (response) {
                    if (response.data != null) {
                        root.$store.GetEditObj(response.data);
                        root.$router.push({
                            path: '/AddQuotation',
                            query: {
                                data: '',
                                Add: false,
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
            root.$https.get('/Purchase/DeliveryChallanDetail?id=' + id, {
                headers: {
                    "Authorization": `Bearer ${token}`
                }
            })
                .then(function (response) {
                    if (response.data != null) {
                        root.$store.GetEditObj(response.data);
                        root.$router.push({
                            path: '/AddDeliveryChallan',
                            query: {
                                data: '',
                                Add: false,
                                isDeliveryChallan: true,
                                Edit: true,
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

                        root.headerFooter.isQuotationPrint = response.data.isQuotationPrint;
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
                        root.headerFooter.exchangeDays = response.data.exchangeDays;
                        root.headerFooter.printOptions = response.data.printOptions;
                        root.headerFooter.invoicePrint = response.data.invoicePrint;
                        root.headerFooter.welcomeLineEn = response.data.welcomeLineEn;
                        root.headerFooter.welcomeLineAr = response.data.welcomeLineAr;
                        root.headerFooter.closingLineEn = response.data.closingLineEn;
                        root.headerFooter.closingLineAr = response.data.closingLineAr;
                        root.headerFooter.contactNo = response.data.contactNo;
                        root.headerFooter.managementNo = response.data.managementNo;

                        root.headerFooter.businessAddressArabic = response.data.businessAddressArabic;
                        root.headerFooter.businessAddressEnglish = response.data.businessAddressEnglish;
                        root.headerFooter.headerImage = response.data.headerImage1ForPrint;
                        root.headerFooter.tagImageForPrint = response.data.tagImageForPrint;
                        root.headerFooter.proposalImageForPrint = response.data.proposalImageForPrint;
                    }

                },
                    function (error) {
                        root.loading = false;
                        console.log(error);
                    });
        },
        PrintRdlc: function (id, isDownload) {
            var root = this;
            var companyId =  localStorage.getItem('CompanyID');
            
            if (isDownload) {
                this.loading = true;
                this.$https.get(this.$ReportServer + '/SalesModuleReports/DeliveryNote/DeliveryNoteReport.aspx?Id=' + id + '&isFifo=' + true + '&openBatch=' + 0 + '&isReturn=' + false + '&deliveryChallan=' + false + '&simpleQuery=' + false + '&colorVariants=' + true + '&CompanyId=' + companyId + '&formName=DeliveryChallan' + '&isDownload=' + isDownload
                    , { responseType: 'blob' }).then(function (response) {
                        root.loading = false;
                        
                        const url = window.URL.createObjectURL(new Blob([response.data]));
                        const link = document.createElement('a');
                        link.href = url;
                        var date = moment().format('DD MMM YYYY');


                        link.setAttribute('download', 'DeliveryChallan ' + date + '.pdf');
                        document.body.appendChild(link);
                        link.click();

                    })
            }
            else {
                var isBlind = localStorage.getItem('IsBlindPrint') == 'true' ? true : false;
                if (isBlind) {
                    this.show = false;
                }
                else {
                    this.show = true;
                }
                this.reportsrc = this.$ReportServer + '/SalesModuleReports/DeliveryNote/DeliveryNoteReport.aspx?Id=' + id + '&isFifo=' + true + '&openBatch=' + 0 + '&isReturn=' + false + '&deliveryChallan=' + false + '&simpleQuery=' + false + '&colorVariants=' + true + '&CompanyId=' + companyId + '&formName=DeliveryChallan' + '&isDownload=' + isDownload
                this.changereport++;
            }
        },
        PrintInvoice: function (value) {
            this.GetHeaderDetail();
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            root.$https.get("/Purchase/SaleOrderDetail?id=" + value, {
                headers: { Authorization: `Bearer ${token}` },
            })
                .then(function (response) {
                    if (response.data != null) {

                        root.printDetails = response.data;
                        if (localStorage.getItem('IsMultiUnit') == 'true') {

                            root.printDetails.saleOrderItems.forEach(function (x) {

                                x.highQty = parseInt(parseInt(x.quantity) / parseInt(x.product.unitPerPack));
                                x.newQuantity = parseInt(parseInt(x.quantity) % parseInt(x.product.unitPerPack));
                                x.unitPerPack = x.product.unitPerPack;
                            });
                        }
                        root.show = true;
                        root.printRender++;
                    }
                });
        },
        PrintInvoicePdf: function (value) {

            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            root.$https.get("/Purchase/SaleOrderDetail?id=" + value, {
                headers: {
                    Authorization: `Bearer ${token}`
                },
            })
                .then(function (response) {
                    if (response.data != null) {

                        root.printDetails = response.data;
                        if (localStorage.getItem('IsMultiUnit') == 'true') {

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
        this.$emit('update:modelValue', this.$route.name);
        this.IsService = localStorage.getItem('IsSimpleInvoice') == 'true' ? false : true;
        this.allowBranches = localStorage.getItem('AllowBranches') == 'true' ? true : false;

    },
    mounted: function () {

        this.colorVariants = localStorage.getItem('ColorVariants') == 'true' ? true : false;
        this.currency = localStorage.getItem('currency');
        this.english = localStorage.getItem('English');
        this.arabic = localStorage.getItem('Arabic');
        this.IsService = localStorage.getItem('IsSimpleInvoice') == 'true' ? false : true;

        this.getData();
    },

}
</script>

<style scoped>
.vue__time-picker input.display-time {
    height: 40px !important;
    background-color: #f2f6f9;
    border: 1px solid #f2f6f9;
}

.tooltip {
    position: relative;
    display: inline-block;
    opacity: 1 !important;
    z-index: 1 !important;
    /* If you want dots under the hoverable text */
}

/* Tooltip text */
.tooltip .tooltiptext {
    visibility: hidden;
    width: 150px;
    background-color: #555;
    color: #fff;
    text-align: center;
    padding: 5px 0;
    border-radius: 6px;
    /* Position the tooltip text */
    position: absolute;
    z-index: 1 !important;
    bottom: 125%;
    left: 50%;
    margin-left: -60px;
    font-weight: 700 !important;
    /* Fade in tooltip */
}

/* Tooltip arrow */
.tooltip .tooltiptext::after {
    content: "";
    position: absolute;
    top: 100%;
    left: 40%;
    margin-left: -5px;
    border-width: 5px;
    border-style: solid;
    border-color: #555 transparent transparent transparent;
}

/* Show the tooltip text when you mouse over the tooltip container */
.tooltip:hover .tooltiptext {
    visibility: visible;
    opacity: 1;
}</style>
