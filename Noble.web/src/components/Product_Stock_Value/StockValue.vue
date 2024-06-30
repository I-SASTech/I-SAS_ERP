<template>
    <div class="row"
        v-if="((isValid('CanViewStockIn') || isValid('CanDraftStockIn')) && formName == 'StockIn') || ((isValid('CanViewStockOut') || isValid('CanDraftStockOut')) && formName == 'StockOut')">

        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page_title" v-if="formName == 'StockIn'">{{ $t('StockValue.StockInLists') }}</h4>
                                <h4 class="page_title" v-else-if="formName == 'StockProduction'">{{
                                    $t('StockValue.ProductionStock') }}</h4>
                                <h4 class="page_title" v-else>{{ $t('StockValue.StockOutLists') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('StockValue.Home') }}</a></li>
                                    <li class="breadcrumb-item active" aria-current="page" v-if="formName=='StockIn'">{{ $t('StockValue.StockInLists') }}</li>
                                    <li class="breadcrumb-item active" aria-current="page" v-else-if="formName=='StockProduction'">{{ $t('StockValue.ProductionStock') }}</li>
                                    <li class="breadcrumb-item active" aria-current="page" v-else> {{ $t('StockValue.StockOutLists') }}</li>
                                </ol>
                            </div>
                            <div class="col-auto align-self-center">
                                <a v-if="((isValid('CanAddStockIn') || isValid('CanDraftStockIn')) && formName == 'StockIn') || ((isValid('CanViewStockOut') || isValid('CanDraftStockOut')) && formName == 'StockOut')"
                                    v-on:click="AddStockValue" href="javascript:void(0);"
                                    class="btn btn-sm btn-outline-primary mx-1">
                                    <i class="align-self-center icon-xs ti-plus"></i>
                                    {{ $t('StockValue.AddNew') }}
                                </a>
                                <a v-on:click="GotoPage('/StartScreen')" href="javascript:void(0);"
                                    class="btn btn-sm btn-outline-danger">
                                    {{ $t('StockValue.Close') }}
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="card">
                <div class="card-header">
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>{{ $t('WareHouseTransfer.WareHouseTransferSearch') }}</label>
                                <div class="input-group">
                                    <button class="btn btn-secondary" type="button" id="button-addon1"><i
                                            class="fas fa-search"></i></button>
                                    <input v-model="search" type="text" class="form-control"
                                        :placeholder="$t('WareHouseTransfer.Search')"
                                        aria-label="Example text with button addon" aria-describedby="button-addon1">
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>{{ $t('StockValue.FromDate') }}</label>
                                <datepicker v-model="fromDate" />
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>{{ $t('StockValue.ToDate') }}</label>
                                <datepicker v-model="toDate" />
                            </div>
                        </div>
                        <div class="col-md-3 ">
                            <label></label>
                            <div class="form-group text-end" v-if="formName == 'StockIn'">
                                <a href="javascript:void(0)" class="btn btn-outline-primary "
                                    v-if="isValid('CanImportStockIn')"
                                    v-on:click="ImportDataFromCsv">{{ $t('StockValue.ImportInformation') }}</a>
                            </div>
                        </div>
                    </div>
                    <div class=" col-lg-4 mt-1" v-if="!advanceFilters">

                        <button v-on:click="search22(true)" :disabled ="!isFilterButtonDisabled"  type="button" class="btn btn-outline-primary mt-3">
                            {{ $t('Sale.ApplyFilter') }}
                        </button>
                        <button v-on:click="clearData(false)" :disabled ="!isFilterButtonDisabled"  type="button" class="btn btn-outline-primary mx-2 mt-3">
                            {{ $t('Sale.ClearFilter') }}
                        </button>

                    </div>


                </div>
                <div class="card-body" v-if="!loading">


                    <!-- Nav tabs -->
                    <ul class="nav nav-tabs" role="tablist">
                        <li class="nav-item"
                            v-if="(isValid('CanDraftStockIn') && formName == 'StockIn') || (isValid('CanDraftStockOut') && formName == 'StockOut')">
                            <a class="nav-link" v-bind:class="{ active: active == 'Draft' }"
                                v-on:click="makeActive('Draft')" data-bs-toggle="tab" href="#home" role="tab"
                                aria-selected="true">
                                {{ $t('StockValue.Draft') }}
                            </a>
                        </li>
                        <li class="nav-item"
                            v-if="(isValid('CanViewStockIn') && formName == 'StockIn') || (isValid('CanViewStockOut') && formName == 'StockOut')"
                            v-on:click="makeActive('Approved')">
                            <a class="nav-link" v-bind:class="{ active: active == 'Approved' }" data-bs-toggle="tab"
                                href="#profile" role="tab" aria-selected="false">
                                {{ $t('StockValue.Post') }}
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
                                            <th width="5%">#</th>
                                            <th width="5%">
                                                {{ $t('StockValue.Code') }}
                                            </th>
                                            <th class="text-center" width="20%">
                                                {{ $t('StockValue.Date') }}
                                            </th>
                                            <th width="15%">
                                                {{ $t('StockValue.WarehouseName') }}
                                            </th>
                                            <th width="25%" class="text-center">
                                                {{ $t('StockValue.Narration') }}
                                            </th>
                                            <th width="20%" class="text-end">
                                                {{ $t('StockValue.TotalAmount') }}
                                            </th>
                                            <th class="text-end">
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr v-for="(stockvalue, index) in stockValueList" v-bind:key="stockvalue.id">
                                            <td>
                                                {{ index + 1 }}
                                            </td>

                                            <td
                                                v-if="(isValid('CanEditStockIn') && formName == 'StockIn') || (isValid('CanEditStockOut') && formName == 'StockOut')">
                                                <strong>
                                                    <a href="javascript:void(0)"
                                                        v-on:click="EditStockValue(stockvalue.id)">{{ stockvalue.code }}</a>
                                                </strong>
                                            </td>
                                            <td v-else>
                                                {{ stockvalue.code }}
                                            </td>
                                            <td class="text-center">
                                                {{ changeDate(stockvalue.date) }}
                                            </td>
                                            <td>
                                                {{ stockvalue.warehouseName }}
                                            </td>
                                            <td class="text-center">
                                                {{ stockvalue.narration }}
                                            </td>
                                            <td class="text-end">
                                                {{ parseFloat(stockvalue.total).toFixed(3).slice(0, -1) }}
                                            </td>
                                            <td class="text-end">
                                                <button type="button" class="btn btn-light dropdown-toggle"
                                                    data-bs-toggle="dropdown" aria-expanded="false">{{
                                                        $t('StockValue.Action') }} <i class="mdi mdi-chevron-down"></i></button>
                                                <div class="dropdown-menu">
                                                    <a class="dropdown-item" href="javascript:void(0)"
                                                        v-on:click="ViewStock(stockvalue.id)"
                                                        v-if="(isValid('CanViewDetailStockIn') && formName == 'StockIn') || (isValid('CanViewDetailStockOut') && formName == 'StockOut')">{{
                                                            $t('StockValue.ViewInvoice') }}</a>
                                                    <a class="dropdown-item" href="javascript:void(0)"
                                                        v-on:click="PrintStock(stockvalue.id)"
                                                        v-if="(isValid('CanPrintStockIn') && formName == 'StockIn') || (isValid('CanPrintStockOut') && formName == 'StockOut')">{{
                                                            $t('StockValue.Print') }}</a>
                                                    <a class="dropdown-item" href="javascript:void(0)"
                                                        v-on:click="PrintStock(stockvalue.id, true)"
                                                        v-if="(isValid('CanPrintStockIn') && formName == 'StockIn') || (isValid('CanPrintStockOut') && formName == 'StockOut')">{{
                                                            $t('StockValue.PdfDownload') }}</a>
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
                                        {{ $t('Pagination.Showing') }} {{ currentPage }}
                                        {{ $t('Pagination.to') }} {{ currentPage * 10 }} of
                                        {{ rowCount }} {{ $t('Pagination.entries') }}
                                    </span>
                                    <span v-else-if="currentPage !== 1 && currentPage !== pageCount">
                                        {{ $t('Pagination.Showing') }} {{ (currentPage * 10) - 9 }} {{ $t('Pagination.to') }}
                                        {{ currentPage * 10 }} {{ $t('Pagination.of') }}
                                        {{ rowCount }} {{ $t('Pagination.entries') }}
                                    </span>
                                    <span v-else-if="currentPage === pageCount">
                                        {{ $t('Pagination.Showing') }}
                                        {{ (currentPage * 10) - 9 }} {{ $t('Pagination.to') }} {{ rowCount }}
                                        {{ $t('Pagination.of') }} {{ rowCount }} {{ $t('Pagination.entries') }}
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
                        <div class="tab-pane pt-3" id="profile" role="tabpanel"
                            v-bind:class="{ active: active == 'Approved' }">
                            <div class="row mb-3" v-if="selected.length > 0">
                                <div class="col-md-3 ">
                                    <div class="dropdown">
                                        <button class="dropdown-toggle btn btn-primary  btn-block" type="button"
                                            id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true"
                                            aria-expanded="false">
                                            {{ $t('WareHouseTransfer.BulkAction') }}
                                        </button>

                                    </div>
                                </div>
                            </div>
                            <div class="table-responsive">
                                <table class="table mb-0">
                                    <thead class="thead-light table-hover">
                                        <tr>
                                            <th width="5%">#</th>
                                            <th width="5%">
                                                {{ $t('StockValue.Code') }}
                                            </th>
                                            <th class="text-center" width="20%">
                                                {{ $t('StockValue.Date') }}
                                            </th>
                                            <th width="15%">
                                                {{ $t('StockValue.WarehouseName') }}
                                            </th>
                                            <th width="25%" class="text-center">
                                                {{ $t('StockValue.Narration') }}
                                            </th>
                                            <th width="20%" class="text-end">
                                                {{ $t('StockValue.TotalAmount') }}
                                            </th>
                                            <th class="text-end">
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr v-for="(stockvalue, index) in stockValueList" v-bind:key="stockvalue.id">
                                            <td>
                                                {{ index + 1 }}
                                            </td>
                                            <td>
                                                {{ stockvalue.code }}
                                            </td>
                                            <td class="text-center">
                                                {{ changeDate(stockvalue.date) }}
                                            </td>
                                            <td>
                                                {{ stockvalue.warehouseName }}
                                            </td>
                                            <td class="text-center">
                                                {{ stockvalue.narration }}
                                            </td>
                                            <td class="text-end">
                                                {{ parseFloat(stockvalue.total).toFixed(3).slice(0, -1) }}
                                            </td>
                                            <td class="text-end">
                                                <button type="button" class="btn btn-light dropdown-toggle"
                                                    data-bs-toggle="dropdown" aria-expanded="false">{{
                                                        $t('StockValue.Action') }} <i class="mdi mdi-chevron-down"></i></button>
                                                <div class="dropdown-menu">
                                                    <a class="dropdown-item" href="javascript:void(0)"
                                                        v-on:click="ViewStock(stockvalue.id)"
                                                        v-if="(isValid('CanViewDetailStockIn') && formName == 'StockIn') || (isValid('CanViewDetailStockOut') && formName == 'StockOut')">{{
                                                            $t('StockValue.ViewInvoice') }}</a>
                                                    <a class="dropdown-item" href="javascript:void(0)"
                                                        v-on:click="PrintStock(stockvalue.id)"
                                                        v-if="(isValid('CanPrintStockIn') && formName == 'StockIn') || (isValid('CanPrintStockOut') && formName == 'StockOut')">{{
                                                            $t('StockValue.Print') }}</a>
                                                    <a class="dropdown-item" href="javascript:void(0)"
                                                        v-on:click="PrintStock(stockvalue.id, true)"
                                                        v-if="(isValid('CanPrintStockIn') && formName == 'StockIn') || (isValid('CanPrintStockOut') && formName == 'StockOut')">{{
                                                            $t('StockValue.PdfDownload') }}</a>
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
                                        {{ currentPage }} {{ $t('Pagination.to') }} {{ currentPage * 10 }}
                                        {{ $t('Pagination.of') }} {{ rowCount }} {{ $t('Pagination.entries') }}
                                    </span>
                                    <span v-else-if="currentPage === 1">
                                        {{ $t('Pagination.Showing') }} {{ currentPage }} {{ $t('Pagination.to') }} {{
                                            currentPage * 10 }} of {{ rowCount }} {{ $t('Pagination.entries') }}
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
                <div class="card-footer col-md-3" v-else>
                    <loading :name="loading" v-model:active="loading"
                             :can-cancel="false"
                             :is-full-page="true"></loading>
                </div>
            </div>

        </div>
    </div>
    <div v-else>
        <acessdenied></acessdenied>
    </div>
</template>


<script>
    
    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    import clickMixin from '@/Mixins/clickMixin'
    import moment from "moment";
    export default {
        props: ['formName'],
        mixins: [clickMixin],
        components: {
            Loading
        },
        data: function () {
            return {
                advanceFilters: false,
                fromDate: '',
                toDate: '',
                selected: [],
                show: false,
                stockValueList: [],
                type: '',
                currentPage: 1,
                pageCount: '',
                rowCount: '',
                active: 'Draft',
                search: '',
                status: true,
                loading: false,
                printRender: 0,
                data: '',
                showPrint: false,
                download: false,

            counter: 0,
            DateRander: 0,
            randerList: 0,
            isFifo: false,
            openBatch: 0,
            printDetails: [],
            headerFooter: {
                isStockIn: true,
                footerEn: '',
                footerAr: '',
                company: ''
            },
        }
    },
    computed: {
            isFilterButtonDisabled() {
      return (
        this.search ||
        this.fromDate ||
        this.toDate 
      );
    },
  },

    watch: {
        formName: function () {

                if (this.$route.query.data == 'StockValues' + this.formName) {
                    this.$emit('update:modelValue', 'StockValues' + this.formName);

                }
                else {
                    localStorage.removeItem("fromDate");
                    localStorage.removeItem("toDate");
                    localStorage.removeItem("active");
                    localStorage.removeItem("currentPage");
                    this.$emit('update:modelValue', this.$route.name + this.formName);

            }
            this.counter++;

            if (localStorage.getItem('active') != null && localStorage.getItem('active') != '' && localStorage.getItem('active') != undefined) {
                this.currentPage = parseInt(localStorage.getItem('currentPage'));
                this.active = (localStorage.getItem('active'));
                if (this.active == 'Draft') {
                    this.status = true;
                }
                if (this.active == 'Approved') {
                    this.status = false;
                }
                this.getPage();



            }
            else {
                this.fromDate = moment().add(-7, 'days').format("DD MMM YYYY");
                this.toDate = moment().format("DD MMM YYYY");
                this.status = true;
                this.currentPage = 1;
                this.counter = 0;
                this.DateRander++;
                this.makeActive('Draft');

            }
        },
       
        // search: function (val) {

        //     this.GetStockValueData(this.status, this.formName, val, 1, this.fromDate, this.toDate);
        // },
        // fromDate: function (fromDate) {

        //     this.counter++;
        //     if (this.counter != 1) {
        //         localStorage.setItem('fromDate', fromDate);
        //         this.GetStockValueData(this.status, this.formName, this.search, 1, fromDate, this.toDate);
        //     }
        // },
        // toDate: function (toDate) {

        //     this.counter++;
        //     if (this.counter != 2) {
        //         localStorage.setItem('toDate', toDate);
        //         this.GetStockValueData(this.status, this.formName, this.search, 1, this.fromDate, toDate);
        //     }
        // }
    },

    methods: {

        search22: function () {
            this.GetStockValueData(this.status, this.formName, this.search, 1, this.fromDate, this.toDate);
        },

        clearData: function () {
            this.search = "";
            this.GetStockValueData(this.status, this.formName, this.search, 1, this.fromDate, this.toDate);

        },

        PrintStock: function (id, download) {
            var root = this;


            root.$https.get('/Product/StockAdjustmentDetails?id=' + id, { headers: { "Authorization": `Bearer ${localStorage.getItem('token')}` } }).then(function (response) {
                if (response.data != null) {

                    root.data = response.data
                    if (download) {
                        root.download = true;
                        root.showPrint = false;
                    }
                    else {
                        root.download = false;
                        root.showPrint = true;
                    }

                    root.printRender++

                }
            });
        },
        GotoPage: function (link) {
            this.$router.push({ path: link });
        },
        ImportDataFromCsv: function () {

            var root = this;
            root.$router.push({
                path: '/ImportExportRecords',
                query: { data: 'StockIn' }
            })
        },
        changeDate: function (x) {
            return moment(x).format("LLL");
        },
        makeActive: function (item) {
            this.active = item;
            this.selectAll = false;
            this.selected = [];

                if (item == 'Draft') {
                    this.status = true;
                }
                else {
                    this.status = false;
                }
                this.GetStockValueData(this.status, this.formName, this.search, this.currentPage, this.fromDate, this.toDate);
            },
            AddStockValue: function () {
                this.$router.push({
                            path: '/addStockValue' ,
                            query: {
                                Add: true,
                                formName: this.formName, 
                            }
                        })
            },
            getPage: function () {
                this.GetStockValueData(this.status, this.formName, this.search, this.currentPage, this.fromDate, this.toDate);
            },
            GetStockValueData: function (status, type, search, currentPage, fromDate, toDate) {

                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

            if (root.formName == 'StockIn')
                root.loading = true;
            localStorage.setItem('currentPage', this.currentPage);
            if (status) {
                localStorage.setItem('active', 'Draft');

            }
            if (!status) {
                localStorage.setItem('active', 'Approved');
            }
            var branchId = localStorage.getItem('BranchId');

                root.$https.get('Product/StockAdjustmentList?status=' + status + '&stockAdjustmentType=' + type + '&searchTerm=' + search + '&pageNumber=' + currentPage + '&fromDate=' + fromDate + '&toDate=' + toDate + '&branchId=' + branchId, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {
                        root.loading = false
                        root.stockValueList = response.data.results;
                        root.pageCount = response.data.pageCount;
                        root.rowCount = response.data.rowCount;
                        root.currentPage = currentPage;
                        root.randerList++;
                    }
                    else {
                        root.loading = false
                    }
                }).catch(error => {
                    console.log(error)
                    root.loading = false;
                });

            },
            RemoveStockValue: function (id) {
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
                        root.$https.get('/Product/StockAdjustmentDelete?id=' + id, { headers: { "Authorization": `Bearer ${token}` } })
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
                    }
                    else {
                        root.$swal({
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Cancelled!' : 'ألغيت!',
                            text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Cancelled!' : 'ألغيت!',
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
            EditStockValue: function (id) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('/Product/StockAdjustmentDetails?id=' + id + '&isFifo=' + this.isFifo + '&openBatch=' + this.openBatch, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {

                        root.$store.GetEditObj(response.data);
                        root.$router.push({
                            path: '/addStockValue',
                            query: { data: '',
                                Add: false, 
                                formName: root.formName,
                            }
                        })
                    }
                });

            },
            ViewStock: function (id) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                root.$https.get('/Product/StockAdjustmentDetails?id=' + id, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {
                        var isMultiUnit = localStorage.getItem('IsMultiUnit');
                        if (isMultiUnit == 'true' && localStorage.getItem('decimalQuantity') == 'true') {
                            response.data.stockAdjustmentDetails.forEach(function (x) {
                                x.totalPiece = parseInt(x.quantity)
                                x.highQty = parseInt(parseFloat(x.quantity) / parseFloat(x.product.unitPerPack));
                                x.quantity = parseFloat(parseFloat(x.quantity) % parseFloat(x.product.unitPerPack)).toFixed(3).slice(0, -1);
                            });
                        }
                        if (isMultiUnit == 'true' && localStorage.getItem('decimalQuantity') != 'true') {
                            response.data.stockAdjustmentDetails.forEach(function (x) {
                                x.totalPiece = parseInt(x.quantity)
                                x.highQty = parseInt(parseInt(x.quantity) / parseInt(x.product.unitPerPack));
                                x.quantity = parseInt(parseInt(x.quantity) % parseInt(x.product.unitPerPack));
                            });
                        }
                        root.$store.GetEditObj(response.data);
                        root.$router.push({
                            path: '/ViewStock',
                            query: 
                            { 
                                data: '',
                                Add: false,
                                formName: root.formName, 
                            }
                        })
                    }
                });

        },

        GetHeaderDetail: function () {
            if (this.formName == 'StockIn')
                this.headerFooter.isStockIn = true;
            else
                this.headerFooter.isStockIn = false;

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

            getBase64Image: function (path) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.headerFooter.company.logoPath = 'data:image/png;base64,' + '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';

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

        this.isFifo = localStorage.getItem('fIFO') == 'true' ? true : false;
        var batch = localStorage.getItem('openBatch')
        if (batch != undefined && batch != null && batch != "") {
            this.openBatch = batch
        }
        else {
            this.openBatch = 1
        }

            if (this.$route.query.data == 'StockValues' + this.formName) {
                this.$emit('update:modelValue', 'StockValues' + this.formName);

            }
            else {
                this.$emit('update:modelValue', this.$route.name + this.formName);

        }
        if (localStorage.getItem('fromDate') != null && localStorage.getItem('fromDate') != '' && localStorage.getItem('fromDate') != undefined) {
            this.fromDate = localStorage.getItem('fromDate');

        }
        else {
            this.fromDate = moment().add(-7, 'days').format("DD MMM YYYY");

        }
        if (localStorage.getItem('toDate') != null && localStorage.getItem('toDate') != '' && localStorage.getItem('toDate') != undefined) {
            this.toDate = localStorage.getItem('toDate');

        }
        else {
            this.toDate = moment().format("DD MMM YYYY");
        }
    },

    mounted: function () {

        this.GetHeaderDetail();
        this.showPrint = false;

        if (localStorage.getItem('active') != null && localStorage.getItem('active') != '' && localStorage.getItem('active') != undefined) {
            this.currentPage = parseInt(localStorage.getItem('currentPage'));
            this.active = (localStorage.getItem('active'));
            if (this.active == 'Draft') {
                this.status = true;
            }
            if (this.active == 'Approved') {
                this.status = false;
            }
            this.getPage();


        }
        else {
            if (this.isValid('CanDraftStockIn') && this.formName == 'StockIn') {
                this.active == 'Draft'
                this.makeActive('Draft')
            }
            else if (this.isValid('CanViewStockIn') && this.formName == 'StockIn') {
                this.active == 'Approved'
                this.makeActive('Approved')
            }
            else if (this.isValid('CanDraftStockOut') && this.formName == 'StockOut') {
                this.active == 'Draft'
                this.makeActive('Draft')
            }
            else if (this.isValid('CanViewStockOut') && this.formName == 'StockOut') {
                this.active == 'Approved'
                this.makeActive('Approved')
            }


        }

        this.GetHeaderDetail();
    }
}
</script>
