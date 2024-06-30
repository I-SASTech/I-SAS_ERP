<template>
    <div class="row">
        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">Stock Received </h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{
                                        $t('WareHouseTransfer.Home') }}</a></li>
                                    <li class="breadcrumb-item active">
                                        Stock Received
                                    </li>
                                </ol>
                            </div>
                            <div class="col-auto align-self-center">
                                <a v-on:click="GotoPage('/StartScreen')" href="javascript:void(0);"
                                    class="btn btn-sm btn-outline-danger">
                                    {{ $t('WareHouseTransfer.Close') }}
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="card">
                <div class="card-header">
                    <div class="row">
                        <div class="col-md-4">
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
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>{{ $t('WareHouseTransfer.FromDate') }}</label>
                                <datepicker v-model="fromDate" />
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>{{ $t('WareHouseTransfer.ToDate') }}</label>
                                <datepicker v-model="toDate" />
                            </div>
                        </div>
                        <div class="col-md-4">
                            <button class="btn btn-outline-primary me-2" v-on:click="getData(currentPage, active)">
                                {{ $t('Filter Data') }}
                            </button>
                            <button class="btn btn-outline-primary me-2" v-on:click="ClearFilters()">
                                {{ $t('Clear Filter') }}
                            </button>
                        </div>
                    </div>


                </div>
                <div class="card-body">


                    <!-- Nav tabs -->
                    <ul class="nav nav-tabs" role="tablist">
                        <li class="nav-item" v-if="isValid('CanDraftStockTransfer')">
                            <a class="nav-link" v-bind:class="{ active: active == 'Draft' }"
                                v-on:click="makeActive('Draft')" data-bs-toggle="tab" href="#home" role="tab"
                                aria-selected="true">
                                {{ $t('WareHouseTransfer.Draft') }}
                            </a>
                        </li>
                        <li class="nav-item" v-if="isValid('CanDraftStockTransfer')" v-on:click="makeActive('Approved')">
                            <a class="nav-link" v-bind:class="{ active: active == 'Approved' }" data-bs-toggle="tab"
                                href="#profile" role="tab" aria-selected="false">
                                {{ $t('WareHouseTransfer.Post') }}
                            </a>
                        </li>
                    </ul>

                    <!-- Tab panes -->
                    <div class="tab-content">
                        <div class="tab-pane pt-3" id="home" role="tabpanel" v-bind:class="{ active: active == 'Draft' }">
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
                                            <th style="width:40px;">#</th>
                                            <th style="width:130px;">
                                                {{ $t('WareHouseTransfer.CODE') }}
                                            </th>
                                            <th>
                                                {{ $t('WareHouseTransfer.Date') }}
                                            </th>
                                            <th>
                                                {{ $t('Stock Transfer Branch') }}
                                            </th>
                                            <th>
                                                {{ $t('Total') }}
                                            </th>
                                            <th class="text-end"></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr v-for="( item, index ) in wareHouseTransferList" :key="item.id">
                                            <td v-if="currentPage === 1">
                                                {{ index + 1 }}
                                            </td>
                                            <td v-else>
                                                {{ ((currentPage * 10) - 10) + (index + 1) }}
                                            </td>
                                            <td v-if="isValid('CanEditStockTransfer')">
                                                <strong>
                                                    <a href="javascript:void(0)"
                                                        v-on:click="EditWareHouseTransfer(item.id)">{{ item.code }}</a>
                                                </strong>
                                            </td>
                                            <td v-else>
                                                {{ item.code }}
                                            </td>
                                            <td>
                                                {{ getDate(item.date) }}
                                            </td>
                                            <td>
                                                {{ item.stockTransferBranch }}
                                            </td>
                                            <td>
                                                {{ item.totalAmount }}
                                            </td>
                                            <td class="text-end">
                                                <button type="button" class="btn btn-light dropdown-toggle"
                                                    data-bs-toggle="dropdown" aria-expanded="false">{{ $t('Sale.Action') }}
                                                    <i class="mdi mdi-chevron-down"></i></button>
                                                <div class="dropdown-menu">
                                                    <a class="dropdown-item" href="javascript:void(0)"
                                                        v-on:click="PrintRdlc(item.id, false)"
                                                        v-if="isValid('CanA4Print')">{{ $t('A4Print') }} </a>
                                                    <a class="dropdown-item" href="javascript:void(0)"
                                                        v-on:click="PrintRdlc(item.id, true)"
                                                        v-if="isValid('CanA4Print')">{{ $t('PdfDownload') }}</a>
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
                                            <th style="width:40px;">#</th>
                                            <th style="width:130px;">
                                                {{ $t('WareHouseTransfer.CODE') }}
                                            </th>
                                            <th>
                                                {{ $t('WareHouseTransfer.Date') }}
                                            </th>
                                            <th>
                                                {{ $t('Stock Transfer Branch') }}
                                            </th>
                                            <th>
                                                {{ $t('Total') }}
                                            </th>
                                            <th class="text-end"></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr v-for="( item, index ) in wareHouseTransferList" :key="item.id">
                                            <td v-if="currentPage === 1">
                                                {{ index + 1 }}
                                            </td>
                                            <td v-else>
                                                {{ ((currentPage * 10) - 10) + (index + 1) }}
                                            </td>
                                            <td>
                                                {{ item.code }}
                                            </td>
                                            <td>
                                                {{ getDate(item.date) }}
                                            </td>
                                            <td>
                                                {{ item.stockTransferBranch }}
                                            </td>
                                            <td>
                                                {{ item.totalAmount }}
                                            </td>
                                            <td class="text-end">
                                                <button type="button" class="btn btn-light dropdown-toggle"
                                                    data-bs-toggle="dropdown" aria-expanded="false">{{ $t('Sale.Action') }}
                                                    <i class="mdi mdi-chevron-down"></i></button>
                                                <div class="dropdown-menu">
                                                    <a class="dropdown-item" href="javascript:void(0)"
                                                        v-on:click="PrintRdlc(item.id, false)"
                                                        v-if="isValid('CanA4Print')">{{ $t('A4Print') }} </a>
                                                    <a class="dropdown-item" href="javascript:void(0)"
                                                        v-on:click="PrintRdlc(item.id, true)"
                                                        v-if="isValid('CanA4Print')">{{ $t('PdfDownload') }}</a>
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
                                    <div class="float-end" v-on:click="getPage()">
                                        <b-pagination pills size="sm" v-model="currentPage" :total-rows="rowCount"
                                            :per-page="10" :first-text="$t('Table.First')" :prev-text="$t('Table.Previous')"
                                            :next-text="$t('Table.Next')" :last-text="$t('Table.Last')">
                                        </b-pagination>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <invoicedetailsprint :show="show" v-if="show" :reportsrc="reportsrc" :changereport="changereport"
                            @close="show = false" @IsSave="IsSave" />
                    </div>

                </div>
            </div>

        </div>
    </div>
    <!-- <div v-else>
        <acessdenied></acessdenied>
    </div> -->
</template>
<script>
import clickMixin from '@/Mixins/clickMixin'
import moment from "moment";
export default {
    mixins: [clickMixin],
    data: function () {
        return {
            reportsrc: '',
            show: false,
            changereport: 0,
            fromDate: '',
            toDate: '',
            search: '',
            searchQuery: '',
            wareHouseTransferList: [],
            currentPage: 1,
            pageCount: '',
            rowCount: '',
            active: 'Draft',
            selected: [],
            selectAll: false,
            counter: 0,
            randerList: 0,
        }
    },
    watch: {

    },
    methods: {
        ClearFilters: function () {
            this.search = '';
            this.fromDate = '';
            this.toDate = '';
            this.getData(this.currentPage, this.active);
        },
        getDate: function (date) {
            return moment(date).format('DD/MM/YYYY');
        },

        GotoPage: function (link) {
            this.$router.push({ path: link });
        },
        getPage: function () {
            this.getData(this.currentPage, this.active);
        },
        makeActive: function (item) {
            this.active = item;
            this.selectAll = false;
            this.selected = [];
            this.getData(1, item);
        },
        EditWareHouseTransfer: function (id) {
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            root.$https.get('/Purchase/GetStockReceivedDetails?Id=' + id + '&branchId=' + localStorage.getItem('WareHouseId'), { headers: { "Authorization": `Bearer ${token}` } })
                .then(function (response) {
                    if (response.data != null) {

                        var isMultiUnit = localStorage.getItem('IsMultiUnit');
                        if (isMultiUnit == 'true' && localStorage.getItem('decimalQuantity') == 'true') {
                            response.data.wareHouseTransferItems.forEach(function (x) {
                                x.highQty = parseInt(parseFloat(x.quantity) / parseFloat(x.product.unitPerPack));
                                x.quantity = parseFloat(parseFloat(x.quantity) % parseFloat(x.product.unitPerPack)).toFixed(3).slice(0, -1);
                            });
                        }
                        if (isMultiUnit == 'true' && localStorage.getItem('decimalQuantity') != 'true') {
                            response.data.wareHouseTransferItems.forEach(function (x) {
                                x.highQty = parseInt(parseInt(x.quantity) / parseInt(x.product.unitPerPack));
                                x.quantity = parseInt(parseInt(x.quantity) % parseInt(x.product.unitPerPack));
                            });
                        }
                        root.$store.GetEditObj(response.data);
                        root.$router.push({
                            path: '/addstockreceived',
                            query: { data:'', Add: false }
                        })
                    }
                },
                    function (error) {
                        console.log(error);
                    });
        },
        IsSave: function () {
            this.show = false;
        },
        PrintRdlc: function (id, isDownload) {
            var root = this;
            var companyId = '';
            companyId = localStorage.getItem('CompanyID');
            if (isDownload) {
                this.loading = true;
                this.$https.get(this.$ReportServer + '/Stocks/StocksReport.aspx?id=' + id + '&CompanyId=' + companyId + '&formName=stockreceived' + '&isDownload=' + isDownload
                    , { responseType: 'blob' }).then(function (response) {
                        root.loading = false;

                        const url = window.URL.createObjectURL(new Blob([response.data]));
                        const link = document.createElement('a');
                        link.href = url;
                        var date = moment().format('DD MMM YYYY');
                        link.setAttribute('download', 'Stock Request ' + date + '.pdf');
                        document.body.appendChild(link);
                        link.click();
                    })
            }
            else {
                var isBlind = localStorage.getItem('IsBlindPrint') == 'true' ? true : false;
                if (isBlind) {
                    this.show = false;
                } else {
                    this.show = true;
                }
                this.reportsrc = this.$ReportServer + '/Stocks/StocksReport.aspx?id=' + id + '&CompanyId=' + companyId + '&formName=stockreceived' + '&isDownload=' + isDownload
                this.changereport++;
            }


        },
        getData: function (currentPage, status) {
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            var branchId = localStorage.getItem('BranchId');

            localStorage.setItem('currentPage', this.currentPage);
            localStorage.setItem('active', this.active);
            this.$https.get('/Purchase/GetStockReceivedList?status=' + status + '&searchTerm=' + this.search + '&pageNumber=' + currentPage + '&fromDate=' + this.fromDate + '&toDate=' + this.toDate + '&branchId=' + branchId, { headers: { "Authorization": `Bearer ${token}` } })
                .then(function (response) {
                    if (response.data != null) {
                        root.wareHouseTransferList = response.data.results;
                        root.pageCount = response.data.pageCount;
                        root.rowCount = response.data.rowCount;
                        root.currentPage = currentPage;
                        root.randerList++;
                    }

                });
        },


        AddWareHouseTransfer: function () {
            this.$router.push('/addstockreceived');
        },
    },
    created: function () {

        if (this.$route.query.data == 'WareHouseTransfers') {
            this.$emit('update:modelValue', 'WareHouseTransfers');

        }
        else {
            this.$emit('update:modelValue', this.$route.name);

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
        if (localStorage.getItem('active') != null && localStorage.getItem('active') != '' && localStorage.getItem('active') != undefined) {
            this.currentPage = parseInt(localStorage.getItem('currentPage'));
            this.active = (localStorage.getItem('active'));
            this.getPage();


        }
        else {
            if (this.isValid('CanDraftStockTransfer')) {
                this.makeActive('Draft')
            }
            else if (this.isValid('CanViewStockTransfer')) {
                this.makeActive('Approved')
            }

        }
    },
    updated: function () {

    }
}
</script>