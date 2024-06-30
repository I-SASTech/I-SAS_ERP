<template>
    <div class="row" v-if="isValid('CanViewPurchaseReturn')">

        <div class="col-lg-12 col-sm-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">{{ $t('PurchaseReturn.PurchaseReturn') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('PurchaseReturn.Home') }}</a></li>
                                    <li class="breadcrumb-item active">{{ $t('PurchaseReturn.PurchaseReturn') }}</li>
                                </ol>
                            </div>
                            <div class="col-auto align-self-center">
                                <a v-if="isValid('CanAddPurchaseReturn')" v-on:click="AddPurchaseOrder" href="javascript:void(0);" class="btn btn-sm btn-outline-primary mx-1">
                                    <i class="align-self-center icon-xs ti-plus"></i>
                                    {{ $t('PurchaseReturn.AddNew') }}
                                </a>
                                <a v-on:click="GotoPage('/StartScreen')" href="javascript:void(0);" class="btn btn-sm btn-outline-danger">
                                    {{ $t('PurchaseReturn.Close') }}
                                </a>
                            </div>
                        </div>
                        <div class="row">
                            <div class="accordion" id="accordionExample">

                                <div class="accordion-item">
                                    <h5 class="accordion-header m-0" id="headingTwo">
                                        <button class="accordion-button collapsed fw-semibold" type="button" data-bs-toggle="collapse" data-bs-target="#collapseTwo" aria-expanded="false" aria-controls="collapseTwo" v-on:click="GetSaleDashboardRecord">
                                            KPIs Dashboard
                                        </button>
                                    </h5>
                                    <div id="collapseTwo" class="accordion-collapse collapse" aria-labelledby="headingTwo" data-bs-parent="#accordionExample">
                                        <loading v-model:active="loading" :can-cancel="true" :is-full-page="false"></loading>

                                        <div class="accordion-body">
                                            <div class="row">
                                                <div class="col-lg-4 col-md-6 col-12">
                                                    <div class="card">
                                                        <div class="card-header">
                                                            <div class="row align-items-center">
                                                                <div class="col">
                                                                    <h4 class="card-title">Purchase Invoice Type</h4>
                                                                </div>
                                                                <!--end col-->

                                                            </div>
                                                            <!--end row-->
                                                        </div>
                                                        <!--end card-header-->
                                                        <div class="card-body">
                                                            <div class="">
                                                                <apexchart type="donut" width="287" height="330" :options="chartOptions" :series="series"></apexchart>
                                                                <span class="badge badge-soft-primary">Return Invoices</span> : {{parseFloat(saleListModel.hold ).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                                            </div>

                                                        </div>
                                                        <!--end card-body-->
                                                    </div>
                                                    <!--end card-->
                                                </div>
                                                <div class="col-lg-4 col-md-6 col-12">
                                                    <div class="card">
                                                        <div class="card-header">
                                                            <div class="row align-items-center">
                                                                <div class="col">
                                                                    <h4 class="card-title">Purchase Invocie Type By Amount</h4>
                                                                </div>
                                                                <!--end col-->

                                                            </div>
                                                            <!--end row-->
                                                        </div>
                                                        <!--end card-header-->
                                                        <div class="card-body">
                                                            <div class="">
                                                                <apexchart type="donut" width="287" height="330" :options="chartOptions3" :series="series3"></apexchart>
                                                                <span class="badge badge-soft-primary">Return Total:</span> : {{currency}} {{parseFloat(saleListModel.totalHold ).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                                            </div>
                                                        </div>
                                                        <!--end card-body-->
                                                    </div>
                                                    <!--end card-->
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
           
            <div class="card">
                <div class="card-header ">
                    <div class="row align-items-center" >
                        <div class="col-md-8" style="padding-top:20px">
                            <div class="input-group">
                                <button class="btn btn-secondary" type="button" id="button-addon1">
                                    <i class="fas fa-search"></i>
                                </button>
                                <input v-model="search" type="text" class="form-control"
                                :placeholder="$t('PurchaseReturn.Search')" aria-label="Example text with button addon"
                                    aria-describedby="button-addon1, button-addon2">
                                <a class="btn btn-soft-primary" v-on:click="AdvanceFilterFor" id="button-addon2" value="Advance Filter">
                                <i class="fa fa-filter"></i>
                            </a>
                            </div>
                        </div>
                        <div class=" col-lg-4 mt-1 " v-if="!advanceFilters">

<button v-on:click="search22(true)" :disabled="!search" type="button" class="btn btn-outline-primary mt-3">
    {{ $t('Sale.ApplyFilter') }}
</button>
<button v-on:click="clearData(false)" :disabled="!search" type="button" class="btn btn-outline-primary mx-2 mt-3">
    {{ $t('Sale.ClearFilter') }}
</button>

</div>
                    </div>
                    <div class="row mt-2" v-if="advanceFilters">
                        <div class="col-xs-12  col-lg-2">
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
                        <div class="col-lg-3 " v-bind:class="$i18n.locale == 'en' ? 'text-left' : 'arabicLanguage'">
                            <label>{{ $t('AddPurchase.Supplier') }} :</label>
                            <supplierdropdown ref=supplierDropdown v-model="supplierForFilterId" />

                        </div>

                        <div class="col-lg-3">
                            <label> {{ $t('DailyExpense.TotalAmount') }}</label>
                            <input type="number" class="form-control" @focus="$event.target.select()" :placeholder="$t('DailyExpense.TotalAmount')" v-model="netAmount" />
                        </div>
                        
                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-3">
                            <label class="text  font-weight-bolder"> {{$t('Sale.User1')}}</label>
                            <usersDropdown v-model="user" ref="userDropdown" :isloginhistory="isloginhistory" />
                        </div>
                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-3">
                            <label class="text  font-weight-bolder"> {{$t('Supplier Type')}}</label>
                            <multiselect :options="['Cash', 'Credit']" v-model="customerType">
                            </multiselect>
                        </div>

                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-3">

                            <button v-on:click="FilterRecord(true)" :disabled ="!isFilterButtonDisabled"  type="button" class="btn btn-outline-primary mt-3">
                                Search
                            </button>
                            <button v-on:click="FilterRecord(false)" :disabled ="!isFilterButtonDisabled"  type="button" class="btn btn-outline-primary mx-2 mt-3">
                                Clear Filter
                            </button>

                        </div>
                    </div>
                </div>
                <div class="card-body">
                    <div class="row">
                        <div class="col-lg-12">

                            <div class="table-responsive">
                                <table class="table mb-0">
                                    <thead class="thead-light table-hover">
                                        <tr>

                                            <th>#</th>
                                            <th>
                                                {{ $t('PurchaseReturn.PI-Invoice') }}
                                            </th>
                                            <!-- <th>
                                                {{ $t('PurchaseReturn.PiNumber') }}
                                            </th> -->
                                            <th>
                                                {{ $t('PurchaseReturn.CreatedDate') }}
                                            </th>
                                            <th v-if="english=='true'">
                                                {{ $t('PurchaseReturn.SupplierName') }}
                                            </th>
                                            <th v-else-if="isOtherLang()">
                                                {{ $t('PurchaseReturn.SupplierNameAr') }}
                                            </th>
                                            <th>
                                                {{ $t('Sale.CreatedBy') }}
                                            </th>
                                            <th>
                                                {{ $t('InvoiceNote')}}
                                            </th>
                                            
                                            <th v-if="allowBranches">
                                                {{ $t('DailyExpense.BranchCode') }}
                                            </th>
                                          <th>
                                                {{ $t('PurchaseReturn.NetAmount') }}
                                            </th>
                                            <th>

                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr v-for="(purchaseOrder,index) in purchaseOrderList" v-bind:key="purchaseOrder.id">


                                            <td v-if="currentPage === 1">
                                                {{index+1}}
                                            </td>
                                            <td v-else>
                                                {{((currentPage*10)-10) +(index+1)}}
                                            </td>
                                            <td>

                                                {{purchaseOrder.registrationNumber}}
                                            </td>
                                            <!-- <td>
                                                {{purchaseOrder.purchaseInvoiceNo}}
                                            </td> -->
                                            <td>
                                                {{purchaseOrder.date}}
                                            </td>
                                            <td v-if="english=='true'" >
                                                <a href="javascript:void(0)" v-on:click="ViewCustomerInfo(purchaseOrder.supplierId)" data-bs-toggle="offcanvas" data-bs-target="#offcanvasRight" aria-controls="offcanvasRight">{{purchaseOrder.supplierName}}</a>
                                            </td>
                                           
                                            <td v-else-if="isOtherLang()" >
                                                {{purchaseOrder.supplierNameArabic}}
                                            </td>
                                            <td>
                                                {{ purchaseOrder.createdBy }}
                                            </td>
                                            <td v-if="purchaseOrder.createdFrom != null">
                                                        <a href="javascript:void(0)" v-on:click="SaleIdForCanvas(purchaseOrder.registrationNumber, purchaseOrder.date, purchaseOrder.purchaseInvoiceId)" data-bs-toggle="offcanvas" data-bs-target="#offcanvasRight3" aria-controls="offcanvasRight">{{ purchaseOrder.createdFrom }}</a>
                                                    </td>
                                                    <td v-else>
                                                        ---
                                                    </td>
                                            
                                            
                                            <td v-if="allowBranches">
                                                {{purchaseOrder.branchCode}}
                                            </td>
                                            <td>
                                                {{currency}}  {{parseFloat(purchaseOrder.netAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                            </td>
                                            <td class="text-end">
                                                <button type="button" class="btn btn-light dropdown-toggle" data-bs-toggle="dropdown" aria-expanded="false"> {{ $t('PurchaseReturn.Action') }} <i class="mdi mdi-chevron-down"></i></button>
                                                <div class="dropdown-menu">
                                                    <a class="dropdown-item" href="javascript:void(0)" v-on:click="PrintRdlc(purchaseOrder.id, false)" v-if="isValid('CanViewDetailPurchaseReturn')">{{ $t('PurchaseReturn.ViewInvoice') }}</a>
                                                    <a class="dropdown-item" href="javascript:void(0)" v-on:click="PrintRdlc(purchaseOrder.id, false)">{{ $t('PurchaseReturn.A4Print') }}</a>
                                                    <a class="dropdown-item" href="javascript:void(0)" v-on:click="PrintRdlc(purchaseOrder.id, true)">{{ $t('PurchaseReturn.Pdf') }}</a>
                                                </div>
                                            </td>
                                            <!-- <td >
                                                <a href="javascript:void(0)" class="btn  btn-icon btn-primary btn-sm me-2" v-on:click="ViewPurchaseOrder(purchaseOrder.id)" v-if="isValid('CanViewDetailPurchaseReturn')"><i class=" fas fa-eye"></i></a>
                                                <a href="javascript:void(0)" class="btn  btn-icon btn-primary btn-sm me-2" v-on:click="PrintInvoice(purchaseOrder.id, false)"><i class=" fa fa-print"></i></a>
                                                <a href="javascript:void(0)" class="btn  btn-icon btn-primary btn-sm " v-on:click="PrintInvoice(purchaseOrder.id, true)"><i class="far fa-file-pdf"></i></a>

                                            </td> -->
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
                                        {{ currentPage }} {{ $t('Pagination.to') }}
                                        {{ rowCount }}
                                        {{ $t('Pagination.of') }}
                                        {{ rowCount }}
                                        {{ $t('Pagination.entries') }}
                                    </span>
                                    <span v-else-if="currentPage === 1 && rowCount >= 11">
                                        {{ $t('Pagination.Showing') }}
                                        {{ currentPage }}
                                        {{ $t('Pagination.to') }}
                                        {{ currentPage * 10 }}
                                        {{ $t('Pagination.of') }}
                                        {{ rowCount }}
                                        {{ $t('Pagination.entries') }}
                                    </span>
                                    <span v-else-if="currentPage === 1">
                                        {{ $t('Pagination.Showing') }}
                                        {{ currentPage }}
                                        {{ $t('Pagination.to') }}
                                        {{ currentPage * 10 }} of {{ rowCount }}
                                        {{ $t('Pagination.entries') }}
                                    </span>
                                    <span v-else-if="currentPage !== 1 && currentPage !== pageCount">
                                        {{ $t('Pagination.Showing') }}
                                        {{ (currentPage * 10) - 9 }}
                                        {{ $t('Pagination.to') }}
                                        {{ currentPage * 10 }}
                                        {{ $t('Pagination.of') }}
                                        {{ rowCount }}
                                        {{ $t('Pagination.entries') }}
                                    </span>
                                    <span v-else-if="currentPage === pageCount">
                                        {{ $t('Pagination.Showing') }}
                                        {{ (currentPage * 10) - 9 }}
                                        {{ $t('Pagination.to') }}
                                        {{ rowCount }}
                                        {{ $t('Pagination.of') }}
                                        {{ rowCount }}
                                        {{ $t('Pagination.entries') }}
                                    </span>
                                </div>
                                <div class=" col-lg-6">
                                    <div class=" float-end" v-on:click="getPage()">
                                        <b-pagination pills size="sm" v-model="currentPage"
                                                      :total-rows="rowCount"
                                                      :per-page="10"
                                                      :first-text="$t('Table.First')"
                                                      :prev-text="$t('Table.Previous')"
                                                      :next-text="$t('Table.Next')"
                                                      :last-text="$t('Table.Last')">
                                        </b-pagination>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="offcanvas offcanvas-end" tabindex="-1" id="offcanvasRight3" aria-labelledby="offcanvasRightLabel" style="width:600px !important">
                        <div class="offcanvas-header">
                            <h5 id="offcanvasRightLabel" class="m-0">{{ $t('Sale.MoreDetails') }} ({{ registrationNo }})</h5>
                            <button v-bind:style="($i18n.locale == 'en' ||isLeftToRight()) ? '' : 'margin-left:0px !important'" type="button" class="btn-close text-reset" data-bs-dismiss="offcanvas" aria-label="Close"></button>
                        </div>
                        <div class="offcanvas-body">
                            <!-- <div class="row">
                                <a class="btn btn-light my-2 col-md-4" href="javascript:void(0);" v-on:click="ViewDeliveryChallan(saleIdToCanvas)" data-bs-toggle="offcanvas" v-if="isValid('SaleToDeliveryNote') " data-bs-target="#offcanvasRight" aria-controls="offcanvasRight"> {{ $t('Sale.ViewDeliveryNote') }}</a>
                            </div> -->
                            <div class="row">
                               
                                <div class="col-md-12">

                                   
                                    
                                    <div class="row" v-if="(isValid('CanViewSupplierQuotation')) && quotationNo != null && quotationNo != ''">
                                        <div class="form-group text-right ">
                                            <b>Supplier Quotation</b>
                                            <!-- <span>{{invoiceNote}}</span> -->
                                        </div>
                                        <div class="col-lg-12 form-group text-left d-flex">
                                            <p style="border-bottom: 1px solid #cbcbcb; ">
                                                <span>1- {{ quotationNo }}--{{getDate(canvasDate) }}</span>
                                            </p>
                                        </div>

                                        <div class="col-lg-12 form-group">
                                            <div class="table-responsive">
                                                <table class="table mb-0">
                                                    <thead class="thead-light table-hover">
                                                    <th>{{ $t('AddPurchase.DiscountType') }}</th>
                                                    <th>{{ $t('AddPurchase.TaxMethod') }}</th>
                                                    <th>{{ $t('StockLineDetails.VAT') }}</th>
                                                    </thead>
                                                    <tbody>
                                                    <td v-if="discountType">
                                                        At Transaction Level
                                                    </td>
                                                    <td v-else>At Line Item Level</td>
                                                    <td>{{ canvasTaxMethod }}</td>
                                                    <td>{{ vAT }}</td>
                                                    </tbody>
                                                </table>
                                            </div>
                                            <div class="table-responsive mt-3">
                                                <table class="table mb-0">
                                                    <thead class="thead-light table-hover">
                                                    <th>#</th>
                                                    <th>{{ $t('AddBundles.ProductName') }}</th>
                                                    <th>{{ $t('StockLineDetails.Quantity') }}</th>
                                                    <th>{{ $t('StockLineDetails.UnitPrice') }}</th>
                                                    </thead>
                                                    <tbody v-for="(sale,index ) in saleItem" v-bind:key="index">
                                                    <td>{{ index + 1 }}</td>
                                                    <td v-if="sale.productName=='' || sale.productName==null">
                                                        {{ sale.description }}
                                                    </td>
                                                    <td v-else>
                                                        {{ sale.productName }}
                                                    </td>
                                                    <td>{{ sale.quantity }}</td>
                                                    <td>{{ sale.unitPrice }}</td>
                                                    </tbody>
                                                </table>
                                            </div>
                                            <div class="table-responsive mt-3">
                                                <table class="table mb-0">
                                                    <thead class="thead-light table-hover">
                                                    <th>{{ $t('SaleItem.Subtotal') }}</th>
                                                    <th>{{ $t('SaleItem.Discount') }}</th>
                                                    <th>{{ $t('Vat Amount') }}</th>
                                                    <th>{{ $t('Amount After Discount') }}</th>
                                                    <th>{{ $t('SaleItem.TotalDue') }}</th>
                                                    </thead>
                                                    <tbody>
                                                    <td>{{currency}} {{parseFloat(grossAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                                    <td>
                                                        {{parseFloat(discountAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                                    </td>
                                                    <td>
                                                        {{parseFloat(vatAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                                    </td>
                                                    <td>{{parseFloat(totalAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                                    <td> {{currency}} {{parseFloat(totalAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                
                               
                            </div>
                        </div>
                    </div>
            <loading v-model:active="loading" :can-cancel="true" :is-full-page="false"></loading>   
            <invoicedetailsprint :show="show" v-if="show" :reportsrc="reportsrc" :changereport="changereport" @close="show=false" @IsSave="IsSave" />
            
            <!-- <purchaseReturnDownload :printDetails="printDetails" :headerFooter="headerFooter" v-if="printDetails.length != 0 && download" />
            <purchaseReturn :printDetails="printDetails" :headerFooter="headerFooter" v-if="printDetails.length != 0 && printShow" v-bind:key="printRender" /> -->
        </div>
    </div>
    <div v-else> <acessdenied></acessdenied></div>
</template>

<script>
    import clickMixin from '@/Mixins/clickMixin'
    import Multiselect from "vue-multiselect";
    import moment from "moment";
    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    export default {
        mixins: [clickMixin],
        components: {
            Loading,
            Multiselect
        },
        data: function () {
            return {
                allowBranches: false,
                customerInformation: '',
                quotationNo: '',
                expandSale: false,
                isloginhistory: true,
                user: '',
                userId: '',
                customerType: '',
                advanceFilters: false,
                randerforempty: 0,
                monthObj: '',
                supplierForFilterId: '',
                isDisable: false,
                isDisableMonth: false,
                netAmount: '',
                year: '',
                month: 0,

                registrationNo:'',
                purchaseOrderIdToCanvas:'',
                canvasDate:'',
                purchaseOrderNo:'',
                PurchaseOrderItems:[],
                vAT:'',
                canvasTaxMethod:'',
                discountType: false,
                grossAmount: 0,
                discountAmount: 0,
                vatAmount: 0,
                afterDiscountAmount: 0,
                totalAmount: 0,

                show:false,
                reportsrc:'',
                changereport:0,
                loading: false,
                printRender: 0,
                pdfRander: 0,
                download: false,
                printShow: false,
                fromDate: '',
                toDate: '',
                arabic: '',
                english: '',
                active: 'Draft',
                search: '',
                searchQuery: '',
                purchaseOrderList: [],
                saleItem: [],
                currentPage: 1,
                pageCount: '',
                currency: '',
                rowCount: '',
                isMultiUnit: false,
                selected: [],
                selectAll: false,
                updateApprovalStatus: {
                    id: '',
                    approvalStatus: ''
                },
                counter: 0,
                rendr: 0,
                printDetails: [],
                headerFooter: {
                    footerEn: '',
                    footerAr: '',
                    company: ''
                },
                request: 0,
                hold: 0,
                credit: 0,
                cash: 0,
                partially: 0,
                unPaid: 0,
                randerChart: 0,
                fullyPaid: 0,
                saleListModel: {
                    hold: 0,
                    totalHold: 0.0,
                    unPaid: 0,
                    totalUnPaid: 0.0,
                    fullyPaid: 0,
                    totalFullyPaid: 0.0,
                    partially: 0,
                    totalPartially: 0.0,
                    credit: 0,
                    totalCredit: 0.0,
                    cash: 0,
                    totalCash: 0.0
                },

                series: [],
                chartOptions: {
                    labels: ['Return', ' ', ' ']
                },
                series2: [],
                chartOptions2: {
                    labels: ['UnPaid ', 'Partially ', 'FullyPaid ']
                },
                series3: [],
                chartOptions3: {
                    labels: ['Return', ' ', ' ']
                },
                series4: [],
                chartOptions4: {
                    labels: ['Un-Paid ', 'Partially ', 'Fully-Paid ']
                },

                seriesOfCustomer: [{
                    name: "Amount",
                    data: []
                }],
                chartOptionsOfCustomer: {
                    chart: {
                        height: 350,
                        type: 'line',
                        zoom: {
                            enabled: false
                        }
                    },
                    dataLabels: {
                        enabled: false
                    },
                    stroke: {
                        curve: 'straight'
                    },
                    title: {
                        text: 'Trending Supplier by Amount',
                        align: 'left'
                    },
                    grid: {
                        row: {
                            colors: ['#f3f3f3', 'transparent'], // takes an array which will be repeated on columns
                            opacity: 0.5
                        },
                    },
                    xaxis: {
                        categories: [],
                    }
                },

                earningSeries: [{
                    name: 'Cash Customer',
                    data: []
                }],
                earningChartOption: {
                    chart: {
                        height: 350,
                        type: 'area'
                    },
                    plotOptions: {
                        bar: {
                            horizontal: false,
                            columnWidth: '15%',
                            endingShape: 'rounded'
                        },
                    },
                    dataLabels: {
                        enabled: false
                    },
                    stroke: {
                        curve: 'smooth'
                    },
                    xaxis: {

                        categories: []
                    },

                },
                earningSeriesCash: [{
                    name: 'Cash Supplier',
                    data: []
                }],
                earningChartOptionCash: {
                    chart: {
                        height: 350,
                        type: 'area'
                    },
                    plotOptions: {
                        bar: {
                            horizontal: false,
                            columnWidth: '15%',
                            endingShape: 'rounded'
                        },
                    },
                    dataLabels: {
                        enabled: false
                    },
                    stroke: {
                        curve: 'smooth'
                    },
                    xaxis: {

                        categories: []
                    },

                },

                seriesPurchase: [{
                    name: 'Credit Supplier',
                    data: []
                }, ],
                chartOptionsPurchase: {
                    chart: {
                        height: 350,
                        type: 'area'
                    },
                    dataLabels: {
                        enabled: false
                    },
                    stroke: {
                        curve: 'smooth'
                    },
                    xaxis: {
                        categories: []
                    },

                },
            }
        },
        computed: {
            isFilterButtonDisabled() {
      return (
        this.customerType ||
        this.monthObj ||
        this.fromDate ||
        this.toDate ||
        this.supplierForFilterId ||
        this.user ||
        this.netAmount
      );
    },
  },
        watch: {
            // search: function (val) {
            //     this.getData(val, 1, this.fromDate, this.toDate,this.userId,this.customerType);
            // },
            // fromDate: function (fromDate) {
            //     this.counter++;
            //     if (this.counter != 1) {
            //         localStorage.setItem('fromDate', fromDate);
            //         this.getData(this.search, this.currentPage, fromDate, this.toDate);
            //     }
            // },
            // toDate: function (toDate) {
            //     this.counter++;
            //     if (this.counter != 2) {
            //         localStorage.setItem('toDate', toDate);

            //         this.getData(this.search, this.currentPage, this.fromDate, toDate);
            //     }
            // }
        },
        methods: {
            search22: function () {
    this.getData(this.search, this.currentPage, this.fromDate, this.toDate, this.userId,this.customerType);
            },

            clearData: function () {
                this.search="";
                this.getData(this.search, this.currentPage,  this.fromDate, this.toDate, this.userId,this.customerType);

            },
            SaleIdForCanvas: function ( registrationNumber, date, saleId) {
            this.isList=false;

                var root = this;
                var token = '';
                root.saleIdToCanvas = saleId;
                this.expandHistory = false;
                this.randerExpand++;
                if (saleId != null) {
                    if (token == '') {
                        token = localStorage.getItem('token');
                    }



                    root.$https.get('/PurchasePost/PurchasePostDetail?id=' + saleId + '&simpleQuery=' + true, {
                        headers: {
                            "Authorization": `Bearer ${token}`
                        }
                    })
                        .then(function (response) {
                            if (response.data != null) {
                                root.registrationNo = registrationNumber + ' - ' + root.getDate(date);
                                root.canvasDate = response.data.date;
                                root.quotationNo = response.data.registrationNo;
                                root.saleItem = response.data.purchasePostItems;
                                root.grossAmount = response.data.grossAmount;
                                root.discountAmount = response.data.discountAmount;
                                root.vatAmount = response.data.vatAmount;
                                root.afterDiscountAmount = response.data.afterDiscountAmount;
                                root.totalAmount = response.data.totalAmount;
                                root.vAT = response.data.taxRatesName;
                                root.canvasTaxMethod = response.data.taxMethod;
                                root.discountType = response.data.discountType;
                            }
                        },
                            function (error) {
                                console.log(error);
                            });
                }
                 
            },


            ViewCustomerInfo: function (id) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                root.$https.get('/Contact/ContactLedgerDetail?id=' + id + '&documentType=' + "PurchaseReturn" + '&lastThreeMonth=' + true, {
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
            GetSaleDashboardRecord: function () {
                if (this.request == 0) {
                    var root = this;
                    var token = '';
                    if (token == '') {
                        token = localStorage.getItem('token');
                    }
                    this.loading = true;
                    this.$https.get('/Sale/SaleDashboardList?isService=' + false + '&isPurchase=' + true + '&isPurchaseReturn=' + true, {
                            headers: {
                                "Authorization": `Bearer ${token}`
                            }
                        })
                        .then(function (response) {
    
                            if (response.data != null) {
                                
                                root.series = [];
                                root.series.push(response.data.hold);
                                root.series.push(response.data.cash);
                                root.series.push(response.data.credit);
                                root.series2 = [];
                                root.series2.push(response.data.unPaid);
                                root.series2.push(response.data.partially);
                                root.series2.push(response.data.fullyPaid);
                                root.series3 = [];
                                root.series3.push(response.data.totalHold);
                                root.series3.push(response.data.totalCash);
                                root.series3.push(response.data.totalCredit);
                                root.series4 = [];
                                root.series4.push(response.data.totalUnPaid);
                                root.series4.push(response.data.totalPartially);
                                root.series4.push(response.data.totalFullyPaid);
                                root.seriesOfCustomer[0].data = [];
                                root.chartOptionsOfCustomer.xaxis.categories = [];
                                root.earningSeries[0].data = [];
                                root.earningSeriesCash[0].data = [];
                                root.earningChartOption.xaxis.categories = [];
                                root.earningChartOptionCash.xaxis.categories = [];
                                root.seriesPurchase[0].data = [];
                                root.chartOptionsPurchase.xaxis.categories = [];
                                response.data.creditList.forEach(function (result) {
                                    root.seriesPurchase[0].data.push((parseFloat(result.amount)).toFixed(0));
                                    root.chartOptionsPurchase.xaxis.categories.push(result.name);
                                });
    
                                root.hold = response.data.hold;
                                root.credit = response.data.credit;
                                root.cash = response.data.cash;
                                root.partially = response.data.partially;
                                root.unPaid = response.data.unPaid;
                                root.fullyPaid = response.data.fullyPaid;
                                response.data.paidListByAmount.forEach(function (result) {
                                    root.earningSeries[0].data.push((parseFloat(result.amount)).toFixed(0));
                                    root.earningChartOption.xaxis.categories.push(result.name);
                                });
                                response.data.paidList.forEach(function (result) {
                                    root.earningSeriesCash[0].data.push((parseFloat(result.amount)).toFixed(0));
                                    root.earningChartOptionCash.xaxis.categories.push(result.name);
                                });
                                response.data.creditListByAmount.forEach(function (result) {
                                    
                                    root.seriesOfCustomer[0].data.push((parseFloat(result.amount)).toFixed(0));
                                    root.chartOptionsOfCustomer.xaxis.categories.push(result.name);
                                });
    
                                
                                root.randerChart++;
    
                                root.saleListModel = {
                                    hold: response.data.hold,
                                    totalHold: response.data.totalHold,
                                    unPaid: response.data.unPaid,
                                    totalUnPaid: response.data.totalUnPaid,
                                    fullyPaid: response.data.fullyPaid,
                                    totalFullyPaid: response.data.totalFullyPaid,
                                    partially: response.data.partially,
                                    totalPartially: response.data.totalPartially,
                                    credit: response.data.credit,
                                    totalCredit: response.data.totalCredit,
                                    cash: response.data.cash,
                                    totalCash: response.data.totalCash
                                };
                                root.loading = false;
    
                            }
                        });
    
                }
                this.request++;
            },
            getDate: function (date) {
                return moment(date).format('LL');
            },
           
            AdvanceFilterFor: function () {
                this.advanceFilters = !this.advanceFilters;
                if (this.advanceFilters == false) {
                    this.FilterRecord(false);
                }
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
                    if (this.paymentMode == 'السيولة النقدية') {
                        this.paymentMode = 'Cash';
                    }
                    if (this.paymentMode == 'مصرف') {
                        this.paymentMode = 'Bank';
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
                    if (this.$refs.supplierDropdown != null) {
                        this.$refs.supplierDropdown.EmptyRecord();
                    }
                    this.user = '';
                    this.supplierForFilterId = '';
    
                    this.year = '';
                    this.fromDate = '';
                    this.toDate = '';
                    this.month = '';
                    this.monthObj = '';
                    this.netAmount = '';
                    this.paymentMode = '';
                    this.customerType = '';
    
                    this.randerforempty++;
    
                }
    
                this.getData(this.search, this.currentPage, this.fromDate, this.toDate,this.userId,this.customerType);
    
            },
            IsSave : function()
            {
                this.show = false;
            },
            PrintRdlc:function(id,isDownload) {
                var root=this;
                var companyId = '';
                    companyId = localStorage.getItem('CompanyID');
                
                var isMultiUnit = localStorage.getItem('IsMultiUnit') == 'true' ? true : false;
                if(isDownload){
                    this.loading=true;
                    this.$https.get(this.$ReportServer+'/PurchaseModule/PurchaseReturn/PurchaseReturnReport.aspx?Id=' + id 
                        + '&isFifo=' + false 
                        + '&openBatch=' + 0 
                        + '&isReturn=' + false 
                        + '&deliveryChallan=' +false 
                        + '&simpleQuery=' + false
                        + '&colorVariants=' + false 
                        + '&isMultiUnit='+ isMultiUnit 
                        + '&isReturnView='+ true 
                        + '&CompanyId=' + companyId
                        + '&formName=PurchaseReturn'
                        + '&isDownload='+isDownload
                    , {  responseType: 'blob' } ) .then(function (response) {
                     root.loading=false;
                        
                        const url = window.URL.createObjectURL(new Blob([response.data]));
                        const link = document.createElement('a');
                        link.href = url;
                        var date = moment().format('DD MMM YYYY');
                        link.setAttribute('download', 'PurchaseReturn ' + date + '.pdf');
                        document.body.appendChild(link);
                        link.click();

                    })
                }
                else{
                var isBlind=localStorage.getItem('IsBlindPrint') == 'true' ? true : false;
                if(isBlind){
                    this.show = false;
                }
                else {
                    this.show = true;
                }
 
                this.reportsrc =   this.$ReportServer+'/PurchaseModule/PurchaseReturn/PurchaseReturnReport.aspx?Id=' + id 
                + '&isFifo=' + false 
                + '&openBatch=' + 0 
                + '&isReturn=' + false 
                + '&deliveryChallan=' +false 
                + '&simpleQuery=' + false
                +  '&colorVariants=' + false 
                + '&isMultiUnit='+ isMultiUnit 
                +'&isReturnView='+ true 
                + '&CompanyId=' + companyId
                +'&formName=PurchaseReturn'
                +'&isDownload='+isDownload
                this.changereport++;
            }
            },

            GotoPage: function (link) {
                this.$router.push({ path: link });
            },
            PrintInvoice: function (id, download) {

                if (download) {
                    this.download = true;
                    this.printShow = false;
                }
                else {
                    this.download = false;
                    this.printShow = true;
                }

                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                var isMultiUnit = localStorage.getItem('IsMultiUnit') == 'true' ? true : false;
                root.$https.get('/PurchasePost/PurchasePostDetail?Id=' + id + '&isReturnView=' + true + '&isMultiUnit=' + isMultiUnit, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {

                        if (response.data != null) {
                            root.printDetails = response.data;
                            if (!root.download) {
                                root.printRender++;
                            }
                            else {
                                root.pdfRander++;
                            }

                        }
                    },
                        function (error) {
                            this.loading = false;
                            console.log(error);
                        });
            },

            ViewPurchaseOrder: function (id) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                var isMultiUnit = localStorage.getItem('IsMultiUnit') == 'true' ? true : false;
                root.$https.get('/PurchasePost/PurchasePostDetail?Id=' + id + '&isReturnView=' + true + '&isMultiUnit=' + isMultiUnit, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {

                        if (response.data != null) {
                            root.$store.GetEditObj(response.data);
                            root.$router.push({
                                path: '/PurchaseReturnView',
                                query: { 
                                     data: '',
                                Add: false, }
                            })
                        }
                    },
                        function (error) {
                            this.loading = false;
                            console.log(error);
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
                    for (let i in this.purchaseOrderList) {
                        this.selected.push(this.purchaseOrderList[i].id);
                    }
                }
            },
            getPage: function () {
                this.getData(this.search, this.currentPage, this.fromDate, this.toDate, this.userId,this.customerType);
            },

            makeActive: function () {
                /*this.active = item;*/
                this.selectAll = false;
                this.selected = [];
                this.getData(this.search, 1, this.fromDate, this.toDate,this.userId,this.customerType);
            },

            getData: function (search, currentPage, fromDate, toDate, userId,customerType) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                localStorage.setItem('currentPage', this.currentPage);
                var branchId = localStorage.getItem('BranchId');


                this.$https.get('/PurchasePost/PurchasePostList?IsPost=' + true + '&searchTerm=' + search + '&pageNumber=' + currentPage + '&fromDate=' + fromDate + '&toDate=' + toDate + '&supplierForFilterId=' + this.supplierForFilterId + '&netAmount=' + this.netAmount + '&month=' + this.month + '&year=' + this.year + '&userId='+ userId + '&customerType=' + customerType+ '&branchId=' + branchId, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        root.purchaseOrderList = response.data.results;
                        root.pageCount = response.data.pageCount;
                        root.rowCount = response.data.rowCount;
                        root.currentPage = response.data.currentPage;
                        root.rendr++;

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
                        root.$https.get('/Purchase/DeletePurchaseOrder?Id=' + id, { headers: { "Authorization": `Bearer ${token}` } })
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

              //  this.$router.push('/addPurchaseReturn');
                this.$router.push({
                                path: '/addPurchaseReturn',
                                query: { 
                                Add: false, }
                            })
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

                root.$https.get('/Sale/printSettingDetail?branchId=' + localStorage.getItem('BranchId'), { headers: { "Authorization": `Bearer ${token}` } })
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
                            this.loading = false;
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
        },
        created: function () {

            if (this.$route.query.data == 'PurchaseReturns') {
                this.$emit('update:modelValue', 'PurchaseReturns');

            }
            else {
                this.$emit('update:modelValue', this.$route.name);

            }
            this.allowBranches = localStorage.getItem('AllowBranches') == 'true' ? true : false;
            // if (localStorage.getItem('fromDate') != null && localStorage.getItem('fromDate') != '' && localStorage.getItem('fromDate') != undefined) {
            //     this.fromDate = localStorage.getItem('fromDate');

            // }
            // else {
            //     this.fromDate = moment().add(-7, 'days').format("DD MMM YYYY");

            // }
            // if (localStorage.getItem('toDate') != null && localStorage.getItem('toDate') != '' && localStorage.getItem('toDate') != undefined) {
            //     this.toDate = localStorage.getItem('toDate');

            // }
            // else {
            //     this.toDate = moment().format("DD MMM YYYY");
            // }
        },
        mounted: function () {
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
            if (localStorage.getItem('currentPage') != null && localStorage.getItem('currentPage') != '' && localStorage.getItem('currentPage') != undefined) {
                this.currentPage = parseInt(localStorage.getItem('currentPage'));
                this.getPage();

            }
            else {
                this.makeActive();

            }
            this.currency = localStorage.getItem('currency');
            this.GetHeaderDetail();
        },
        updated: function () {
            if (this.selected.length < this.purchaseOrderList.length) {
                this.selectAll = false;
            } else if (this.selected.length == this.purchaseOrderList.length) {
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