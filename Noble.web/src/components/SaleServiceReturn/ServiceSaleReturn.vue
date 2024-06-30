<template>
    <div class="row" v-if="isValid('CanViewSaleReturn')">
        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">{{ $t('SaleReturn.ServiceSaleReturn') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('SaleReturn.Home')
                                    }}</a></li>
                                    <li class="breadcrumb-item active">
                                        {{ $t('SaleReturn.ServiceSaleReturn') }}
                                    </li>
                                </ol>
                            </div>

                            <div class="col-auto align-self-center">
                                <a v-if="isValid('CanAddSaleReturn')" v-on:click="AddSaleReturn" href="javascript:void(0);"
                                    class="btn btn-sm btn-outline-primary mx-1">
                                    <i class="align-self-center icon-xs ti-plus"></i>
                                    {{ $t('SaleReturn.AddNew') }}
                                </a>
                                <a v-on:click="GotoPage('/StartScreen')" href="javascript:void(0);"
                                    class="btn btn-sm btn-outline-danger">
                                    {{ $t('SaleReturn.Close') }}
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
                                <button class="btn btn-soft-primary" v-on:click="searchData()" type="button"
                                    id="button-addon1"><i class="fas fa-search"></i></button>
                                <input v-model="search" type="text" class="form-control"
                                    :placeholder="$t('SaleReturn.SearchOfInvoice')"
                                    aria-label="Example text with button addon" aria-describedby="button-addon1">
                                <a class="btn btn-soft-primary" v-on:click="AdvanceFilterFor" id="button-addon2"
                                    value="Advance Filter">
                                    <i class="fa fa-filter"></i>
                                </a>
                            </div>
                        </div>
                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-3" v-if="!advanceFilters">

                            <button v-on:click="searchData()" type="button" class="btn btn-outline-primary mt-3">
                                {{ $t('Sale.ApplyFilter') }}
                            </button>

                            <button v-on:click="clearData()" type="button" class="btn btn-outline-primary mx-2 mt-3">
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

                            <button v-on:click="FilterRecord(true)" type="button" class="btn btn-outline-primary mt-3">
                                {{ $t('Sale.ApplyFilter') }}
                            </button>
                            <button v-on:click="FilterRecord(false)" type="button"
                                class="btn btn-outline-primary mx-2 mt-3">
                                {{ $t('Sale.ClearFilter') }}
                            </button>

                        </div>
                    </div>
                </div>
                <div class="card-body">
                    <div class="tab-content">
                        <div class="tab-pane pt-3" id="profile" role="tabpanel"
                            v-bind:class="{ active: active == 'Approved' }">
                            <div class="table-responsive">
                                <table class="table mb-0">
                                    <thead class="thead-light table-hover">
                                        <tr>
                                            <th>
                                                #
                                            </th>
                                            <th>
                                                {{ $t('SaleReturn.SaleReturnNo') }}
                                            </th>
                                            <th>
                                                {{ $t('SaleReturn.InvoiceNo') }}
                                            </th>
                                            <th>
                                                {{ $t('SaleReturn.Type') }}
                                            </th>
                                            <th>
                                                {{ $t('SaleReturn.Date') }}
                                            </th>
                                            <th>
                                                {{ $t('SaleReturn.CustomerName') }}
                                            </th>
                                            <th>
                                                {{ $t('SaleReturn.CreatedBy') }}
                                            </th>
                                            <th v-if="isDayStarts == 'true'">
                                                {{ $t('SaleReturn.CounterId') }}
                                            </th>
                                            <th>
                                                {{ $t('SaleReturn.NetAmount') }}
                                            </th>
                                            <th style="width: 70px;" class="text-end"></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr v-for="(sale, index) in saleList" v-bind:key="index">
                                            <td v-if="currentPage === 1">
                                                {{ index + 1 }}
                                            </td>
                                            <td v-else>
                                                {{ ((currentPage * 10) - 10) + (index + 1) }}
                                            </td>
                                            <td>
                                                {{ sale.registrationNumber }}
                                            </td>
                                            <td>
                                                {{ sale.invoiceNo }}
                                            </td>
                                            <td>
                                                {{ sale.isCredit ? 'Credit' : 'Cash' }}
                                            </td>
                                            <td>
                                                {{ getDate(sale.date) }}
                                            </td>
                                            <td>

                                                <a href="javascript:void(0)"
                                                    v-on:click="ViewCustomerInfo(sale.customerId, sale.cashCustomerId)"
                                                    data-bs-toggle="offcanvas" data-bs-target="#offcanvasRight"
                                                    aria-controls="offcanvasRight">{{ sale.customerName ==
                                                        null ? sale.customerNameArabic : sale.customerName }}</a>

                                            </td>
                                            <td>
                                                {{ sale.userName }}
                                            </td>
                                            <td v-if="isDayStarts == 'true'">
                                                {{ sale.counterName }}
                                            </td>
                                            <td>
                                                {{ currency }}
                                                {{ parseFloat(sale.netAmount).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g,
                                                    "$1,") }}
                                            </td>
                                            <td class="text-end">
                                                <button type="button" class="btn btn-light dropdown-toggle"
                                                    data-bs-toggle="dropdown" aria-expanded="false">{{
                                                        $t('SaleReturn.Action') }} <i class="mdi mdi-chevron-down"></i></button>
                                                <div class="dropdown-menu">
                                                    <a class="dropdown-item" href="javascript:void(0)"
                                                        v-on:click="ViewInvoice(sale.id)"
                                                        v-if="isValid('CanViewDetailSaleReturn') && printTemplate != 'Template6'">{{
                                                            $t('SaleReturn.ViewInvoice') }}</a>
                                                    <a class="dropdown-item" href="javascript:void(0)"
                                                        v-on:click="PrintRdlc(sale.id)"
                                                        v-if="isValid('CanViewServiceInvoiceDetail') && printTemplate == 'Template6'">{{
                                                            $t('ProformaInvoices.TemplateView') }}</a>
                                                    <a class="dropdown-item" href="javascript:void(0)"
                                                        v-on:click="PrintRdlc(sale.id)" v-if="isValid('CanPrintInvoice')">{{
                                                            $t('SaleReturn.A4-Print') }}</a>
                                                    <a class="dropdown-item" href="javascript:void(0)"
                                                        v-on:click="PrintRdlc(sale.id)" v-if="isValid('CanPrintInvoice')">{{
                                                            $t('SaleReturn.POS-Print') }}</a>
                                                    <a class="dropdown-item" href="javascript:void(0)"
                                                        v-on:click="PrintRdlc(sale.id)" v-if="isValid('CanPrintInvoice')">{{
                                                            $t('SaleReturn.PdfDownload') }}</a>
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
                                    <div class="float-end" v-on:click="getPage()">
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
            <invoicedetailsprint :show="show" v-if="show" :reportsrc="reportsrc" :changereport="changereport"
                @close="show = false" @IsSave="IsSave" />
            <saleReturnReport :printDetails="printDetails" :headerFooter="headerFooter"
                v-if="printDetails.length != 0 && show && printTemplate != 'Template6'" v-bind:key="printRender">
            </saleReturnReport>
            <SaleReturnPdf :printDetails="printDetails" :headerFooter="headerFooter"
                v-if="printDetails.length != 0 && pdfShow && printTemplate != 'Template6'" v-bind:key="printpdfRender" />


            <div class="offcanvas offcanvas-end px-0" tabindex="-1" id="offcanvasRight"
                aria-labelledby="offcanvasRightLabel">
                <div class="offcanvas-header">
                    <h5 id="offcanvasRightLabel" class="m-0">Customer Info ({{ customerInformation.runningBalance }})</h5>
                    <button type="button" class="btn-close text-reset" data-bs-dismiss="offcanvas"
                        aria-label="Close"></button>
                </div>
                <div class="offcanvas-body">
                    <div class="row">
                        <div class="col-lg-12 form-group" v-if="english == 'true'">
                            <label>{{ $englishLanguage($t('Sale.CustomerName')) }}</label>
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

        </div>

    </div>
    <div v-else>
        <acessdenied></acessdenied>
    </div>
</template>

<script>
import clickMixin from '@/Mixins/clickMixin'
import moment from "moment";
export default {
    mixins: [clickMixin],
    name: "SaleReturn",
    data: function () {
        return {
            english: '',
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
            bankDetail: false,
            thermalShow: false,
            show: false,
            pdfShow: false,
            saleReturnRegNo: '',

            active: 'Draft',
            search: '',
            searchQuery: '',
            saleList: [],
            purchasePostList: [],
            currentPage: 1,
            pageCount: '',
            rowCount: '',
            currency: '',

            selected: [],
            selectAll: false,
            updateApprovalStatus: {
                id: '',
                approvalStatus: ''
            },
            CompanyID: '',
            UserID: '',
            employeeId: '',
            isDayAlreadyStart: false,
            saleReturnDesign: false,
            IsProduction: false,
            isDayStarts: '',
            rendr: 0,
            counter: 0,
            printDetails: [],
            printDetailsPos: [],
            printRender: 0,
            printpdfRender: 0,
            printRenderPos: 0,
            headerFooter: {
                footerEn: '',
                footerAr: '',
                company: ''
            },

            overWrite: false,
            businessLogo: '',
            businessNameArabic: '',
            businessNameEnglish: '',
            businessTypeArabic: '',
            businessTypeEnglish: '',
            companyNameArabic: '',
            companyNameEnglish: '',
            printTemplate: '',

            customerInformation: '',
            expandSale: false,
        }
    },
    methods: {
        searchData: function () {
            this.getData(this.search, this.currentPage, this.active, this.fromDate, this.toDate);
        },
        clearData: function () {
            this.search = "";
            this.getData(this.search, this.currentPage, this.active, this.fromDate, this.toDate);
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

            this.getData(this.search, this.currentPage, this.active, this.fromDate, this.toDate, this.terminalId, this.userId, this.customerId, this.customerType);

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
            root.$https.get('/Contact/ContactLedgerDetail?id=' + id + '&documentType=' + "SaleReturn" + '&cashCustomer=' + cashCustomer + '&isService=true', { headers: { "Authorization": `Bearer ${token}` } })
                .then(function (response) {
                    if (response.data != null) {
                        root.customerInformation = response.data;
                    }
                },
                    function (error) {
                        console.log(error);
                    });
        },

        PrintInvoicePos: function (value) {
            var root = this;
            var token = '';
            root.saleReturnRegNo = value.invoiceNo;
            this.isDisabled = true;
            this.PrinterInterval();
            if (token == '') {
                token = localStorage.getItem('token');
            }
            root.$https.get("/Sale/SaleDetail?id=" + value.id, {
                headers: { Authorization: `Bearer ${token}` },
            })
                .then(function (response) {
                    if (response.data != null) {
                        root.printDetailsPos = response.data;
                        root.show = true;

                        root.printRenderPos++;
                    }
                });
        },

        PrinterInterval: function () {
            var root = this;

            this.printInterval = setInterval(() => {
                root.isDisabled = false;
            }, 3000);
        },
        getDate: function (date) {
            return moment(date).format('LLL');
        },
        isDayStart: function () {
            var root = this;

            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            root.loading = true;
            root.$https.get('/Product/IsDayStart?userId=' + this.UserID + '&employeeId=' + this.employeeId, { headers: { "Authorization": `Bearer ${token}` } })
                .then(function (response) {
                    if (response.data.isDayStart == true) {
                        root.isDayAlreadyStart = true;
                        localStorage.setItem('token', response.data.token);
                    } else {
                        root.isDayAlreadyStart = false;
                    }
                    root.loading = false;

                });
        },
        EditSale: function (id) {
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }

            root.$https.get('/Sale/SaleDetail?Id=' + id + '&isReturn=' + true, { headers: { "Authorization": `Bearer ${token}` } })
                .then(function (response) {
                    if (response.data != null) {
                        root.$store.GetEditObj(response.data);
                        root.$router.push({
                            path: '/AddServiceSaleReturn',
                            query: { data: '',
                                Add: false, }
                        })
                    }
                },
                    function (error) {
                        console.log(error);
                    });
        },
        ViewInvoiceTemplate: function (id) {
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            var batch = localStorage.getItem('openBatch')
            var openBatch = 0;
            if (batch != undefined && batch != null && batch != "") {
                openBatch = batch
            }
            else {
                openBatch = 1
            }
            var isFifo = localStorage.getItem('fIFO') == 'true' ? true : false;
            root.$https.get('/Sale/SaleDetail?Id=' + id + '&isFifo=' + isFifo + '&openBatch=' + openBatch, { headers: { "Authorization": `Bearer ${token}` } })
                .then(function (response) {

                    if (response.data != null) {
                        response.data.saleItems.forEach(function (x) {
                            x.remainingQuantity = parseInt(x.quantity);
                        });
                        
                        root.$router.push({
                            path: '/SaleServiceView',
                            query: {
                                printDetails: response.data,
                                headerFooter: root.headerFooter,
                                formName: 'SaleReturn'
                            }
                        })
                    }
                },
                    function (error) {
                        console.log(error);
                    });
        },
        ViewInvoice: function (id) {
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            var batch = localStorage.getItem('openBatch')
            var openBatch = 0;
            if (batch != undefined && batch != null && batch != "") {
                openBatch = batch
            }
            else {
                openBatch = 1
            }
            var isFifo = localStorage.getItem('fIFO') == 'true' ? true : false;
            root.$https.get('/Sale/SaleDetail?Id=' + id + '&isFifo=' + isFifo + '&openBatch=' + openBatch, { headers: { "Authorization": `Bearer ${token}` } })
                .then(function (response) {

                    if (response.data != null) {
                        response.data.saleItems.forEach(function (x) {
                            x.remainingQuantity = parseInt(x.quantity);
                        });
                        root.$store.GetEditObj(response.data);
                        root.$router.push({
                            path: '/ViewSaleReturn',
                            query: {
                                data: '',
                                Add: false,
                                page: root.currentPage,
                                active: root.active
                            }
                        })
                    }
                },
                    function (error) {
                        console.log(error);
                    });
        },
        PrintRdlc: function (id) {
            var isBlind = localStorage.getItem('IsBlindPrint') == 'true' ? true : false;
            if (isBlind) {
                this.show = false;
            }
            else {
                this.show = true;
            }
            var companyId = '';
            companyId = localStorage.getItem('CompanyID');



            this.reportsrc = this.$ReportServer + '/SalesModuleReports/SaleInvoice/SaleReport.aspx?Id=' + id + '&isFifo=' + false + '&openBatch=' + 0 + '&isReturn=' + false + '&deliveryChallan=' + false + '&simpleQuery=' + false + '&colorVariants=' + false + '&CompanyId=' + companyId + '&formName=ReturnInvoice'
            this.changereport++;
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
                        root.headerFooter.headerImage = response.data.headerImageForPrint;
                        root.headerFooter.footerImage = response.data.footerImageForPrint;
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
                        if (root.overWrite) {

                            root.headerFooter.company.nameArabic = root.businessNameArabic;
                            root.headerFooter.company.nameEnglish = root.businessNameEnglish;
                            root.headerFooter.company.categoryArabic = root.businessTypeArabic;
                            root.headerFooter.company.categoryEnglish = root.businessTypeEnglish;

                            root.headerFooter.company.companyNameArabic = root.companyNameArabic;
                            root.headerFooter.company.companyNameEnglish = root.companyNameEnglish;
                            root.headerFooter.company.logoPath = root.businessLogo;

                        }
                        root.getBase64Image(root.headerFooter.company.logoPath);
                    }
                });
        },
        PrintInvoicePdf: function (value) {

            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            root.printDetails = [];

            root.show = false
            root.pdfShow = false;
            root.$https.get("/Sale/SaleDetail?id=" + value, {
                headers: { Authorization: `Bearer ${token}` },
            })
                .then(function (response) {
                    if (response.data != null) {
                        root.saleReturnDesign = true;

                        root.printDetails = response.data;
                        if (localStorage.getItem('IsMultiUnit') == 'true' && localStorage.getItem('decimalQuantity') == 'true') {
                            root.printDetails.saleItems.forEach(function (x) {
                                x.highQty = parseInt(parseFloat(x.quantity) / parseFloat(x.product.unitPerPack));
                                x.newQuantity = parseFloat(parseFloat(x.quantity) % parseFloat(x.product.unitPerPack)).toFixed(3).slice(0, -1);
                                x.unitPerPack = x.product.unitPerPack;
                            });
                        }
                        if (localStorage.getItem('IsMultiUnit') == 'true' && localStorage.getItem('decimalQuantity') != 'true') {
                            root.printDetails.saleItems.forEach(function (x) {
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
        GotoPage: function (link) {
            this.$router.push({ path: link });
        },

        PrintInvoice: function (value) {

            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            root.printDetails = [];

            root.show = false
            root.pdfShow = false;
            root.$https.get("/Sale/SaleDetail?id=" + value, {
                headers: { Authorization: `Bearer ${token}` },
            })
                .then(function (response) {
                    if (response.data != null) {
                        root.saleReturnDesign = true;

                        root.printDetails = response.data;
                        root.show = true
                        root.printRender++;
                    }
                });
        },

        goToPaymentDetail: function (id) {
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            root.$https.get('/Sale/SaleDetail?Id=' + id, { headers: { "Authorization": `Bearer ${token}` } })
                .then(function (response) {
                    if (response.data != null) {
                        root.$store.GetEditObj(response.data);
                        root.$router.push({
                            path: '/SalePaymentDetail',
                            query: { data: '', Add: false }
                        })
                    }
                },
                    function (error) {
                        console.log(error);
                    });
        },
        makeActive: function (item) {
            this.active = item;
            this.getData(this.search, 1, item, this.fromDate, this.toDate);
        },
        getPage: function () {
            this.getData(this.search, this.currentPage, this.active, this.fromDate, this.toDate);
        },

        getData: function (search, currentPage, status) {
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            localStorage.setItem('currentPage', this.currentPage);

            var isSaleReturnPost = status == 'Draft' ? false : true;
            this.$https.get('/Sale/SaleList?status=' + 'SaleReturn' + '&searchTerm=' + search + '&pageNumber=' + currentPage + '&isSaleReturnPost=' + isSaleReturnPost + '&isService=true' + '&fromDate=' + this.fromDate + '&toDate=' + this.toDate + '&terminalId=' + this.terminalId + '&userId=' + this.userId + '&month=' + this.month + '&year=' + this.year + '&CustomerId=' + this.customerId, { headers: { "Authorization": `Bearer ${token}` } })
                .then(function (response) {
                    if (response.data != null) {
                        root.saleList = response.data.results.sales;
                        root.pageCount = response.data.pageCount;
                        root.rowCount = response.data.rowCount;
                        root.currentPage = response.data.currentPage;
                        root.rendr++;
                    }
                });
        },

        RemoveSale: function (id) {
            var root = this;
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
                    root.$https.get('/Sale/DeleteSale?Id=' + id, { headers: { "Authorization": `Bearer ${token}` } })
                        .then(function (response) {
                            if (response.data.message.id != '00000000-0000-0000-0000-000000000000') {

                                root.saleList.splice(root.saleList.findIndex(function (i) {
                                    return i.id === response.data.message.id;
                                }), 1);
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
                    this.$swal('Cancelled', 'Your invoice is still intact', 'info');
                }
            });
        },
        AddSaleReturn: function () {
            this.$router.push('/AddServiceSaleReturn');
        }
    },
    created: function () {
        this.GetHeaderDetail();
        if (this.$route.query.data == 'AddSaleReturns') {
            this.$emit('update:modelValue', 'AddSaleReturns');

        }
        else {
            this.$emit('update:modelValue', this.$route.name);

        }

    },

    mounted: function () {
        this.overWrite = localStorage.getItem('overWrite') == 'true' ? true : false;

        this.businessLogo = localStorage.getItem('BusinessLogo');
        this.businessNameArabic = localStorage.getItem('BusinessNameArabic');
        this.businessNameEnglish = localStorage.getItem('BusinessNameEnglish');
        this.businessTypeArabic = localStorage.getItem('BusinessTypeArabic');
        this.businessTypeEnglish = localStorage.getItem('BusinessTypeEnglish');
        this.companyNameArabic = localStorage.getItem('CompanyNameArabic');
        this.companyNameEnglish = localStorage.getItem('CompanyNameEnglish');
        this.printTemplate = localStorage.getItem('PrintTemplate');
        this.bankDetail = localStorage.getItem('BankDetail') == 'true' ? true : false;
        var IsDayStart = localStorage.getItem('DayStart');
        this.isDayStarts = localStorage.getItem('DayStart');
        var IsDayStartOn = localStorage.getItem('IsDayStart');

        var AllowAll = localStorage.getItem('AllowAll');
        if (IsDayStart != 'true') {
            this.isDayAlreadyStart = true;
            this.currentPage = this.$route.query.page == undefined ? 1 : parseInt(this.$route.query.page);
            this.search = '';
            this.active = 'Approved';
            this.getData(this.search, this.currentPage, this.active, this.fromDate, this.toDate);
            this.currency = localStorage.getItem('currency');
        }
        else {
            if (AllowAll == 'true') {
                this.isDayAlreadyStart = true;


                if (localStorage.getItem('currentPage') != null && localStorage.getItem('currentPage') != '' && localStorage.getItem('currentPage') != undefined) {
                    this.currentPage = parseInt(localStorage.getItem('currentPage'));

                }
                else {
                    this.currentPage = 1;

                }


                this.search = '';
                this.active = 'Approved';
                this.getData(this.search, this.currentPage, this.active, this.fromDate, this.toDate);
                this.currency = localStorage.getItem('currency');
            }
            else if (IsDayStartOn == 'true') {
                this.isDayAlreadyStart = true;
                if (localStorage.getItem('currentPage') != null && localStorage.getItem('currentPage') != '' && localStorage.getItem('currentPage') != undefined) {
                    this.currentPage = parseInt(localStorage.getItem('currentPage'));

                }
                else {
                    this.currentPage = 1;

                }
                this.search = '';
                this.active = 'Approved';
                this.getData(this.search, this.currentPage, this.active, this.fromDate, this.toDate);
                this.currency = localStorage.getItem('currency');
            }
            else {
                this.CompanyID = localStorage.getItem('CompanyID');
                this.UserID = localStorage.getItem('UserID');
                this.employeeId = localStorage.getItem('EmployeeId');
                this.isDayAlreadyStart = false;
            }
        }

    }
}
</script>