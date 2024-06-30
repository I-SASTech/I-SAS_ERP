<template>
    <div class="row" v-if="isValid('CanViewAccountLedger')">
        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">Advance Stock Report</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);"> {{
                                        $t('AccountLedger.Home')
                                    }}</a></li>
                                    <li class="breadcrumb-item active">Advance Stock Report</li>
                                </ol>
                            </div>
                            <div class="col-auto align-self-center">
                                <a href="javascript:void(0);" class="btn btn-sm btn-outline-primary mx-1">
                                    <i class="fas fa-print font-14"></i>
                                    {{ $t('StockReport.Print') }}
                                </a>
                                <a v-on:click="GotoPage('/StartScreen')" href="javascript:void(0);"
                                    class="btn btn-sm btn-outline-danger">
                                    {{ $t('Categories.Close') }}
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <hr class="hr-dashed hr-menu mt-0" />
            <div class="row align-items-baseline">
                <div class=" col-lg-3   form-group">
                    <label>Select a Period:</label>
                    <multiselect v-model="reportOpt" :disabled="disablePeriod" :key="disablePeriodRender"
                        :options="['Today', 'This Week', 'This Month', 'This Quarter', 'This Year', 'Yesterday', 'Previous Week', 'Previous Month', 'Previous Quarter', 'Previous Year', 'Custom']"
                        :show-labels="false" v-bind:placeholder="$t('Select an Option')" @update:modelValue="GetDateTime()">
                    </multiselect>
                </div>
                <div class="from-group col-lg-3">
                    <label for="">Products:</label>
                    <product-dropdown v-model="productId" :IsReport="true" :key="disablePeriodRender" />
                </div>
                <div class=" col-lg-3 form-group" v-if="allowBranches">
                            <label>{{ $t('Branches.SelectBranch') }}</label>
                            <branch-dropdown v-model="branchIds" :ismultiple="true" :islayout="false" />
                        </div>

                <div class=" col-lg-3  form-group">
                    <br>
                    <a class="btn btn-soft-primary mx-2" v-on:click="AdvanceFilters()" id="button-addon2"
                        value="Advance Filter">
                        <i class="fa fa-filter"></i>
                    </a>
                    <button
                        v-if="(reportOpt == '' || reportOpt == null) && (numberOfPeriods == 0) && (productId == '' || productId == null)"
                        disabled href="javascript:void(0);" class="btn btn-outline-primary me-2">
                        Apply Filters
                    </button>
                    <button v-else v-on:click="GetInventoryList()" href="javascript:void(0);"
                        class="btn btn-outline-primary me-2">
                        Apply Filters
                    </button>
                    <a v-on:click="RemoveFilters()" href="javascript:void(0);" class="btn btn-outline-danger">
                        Clear Filters
                    </a>
                </div>
            </div>
            <div class="row">
                <div class=" col-lg-4   form-group" v-if="showDates">
                    <label>From Date:</label>
                    <datepicker v-model="fromDate" :period="true" :key="rander" />
                </div>
                <div class=" col-lg-4   form-group" v-if="showDates">
                    <label>To Date:</label>
                    <datepicker v-model="toDate" :period="true" :key="rander" />
                </div>
                <div class=" col-lg-4   form-group" v-if="advanceFilters">
                    <label>Compare With:</label>
                    <multiselect v-model="compareWith"
                        :options="['Previous Year(s)', 'Previous Period(s)', 'Previous Quarter(s)', 'Previous Month(s)']"
                        :show-labels="false" v-bind:placeholder="$t('Select an Option')" @update:modelValue="GetPeriods()">
                    </multiselect>
                    <div class="mt-1 d-flex align-items-center">
                        <input type="checkbox" class="me-1"><span>Arrange period/year from latest to oldest</span>
                    </div>
                </div>
                <div class=" col-lg-4   form-group" v-if="isPreviousYear && advanceFilters">
                    <label>Number of Year(s)</label>
                    <multiselect v-model="numberOfPeriods" :options="['1', '2', '3', '4', '5']" :show-labels="false"
                        v-bind:placeholder="$t('Select an Option')">
                    </multiselect>
                </div>
                <div class=" col-lg-4   form-group" v-if="isPreviousPeriod && advanceFilters">
                    <label>Number of Period(s)</label>
                    <multiselect v-model="numberOfPeriods" :options="financialYears" :show-labels="false"
                        v-bind:placeholder="$t('Select an Option')">
                    </multiselect>
                </div>
                <div class=" col-lg-4   form-group" v-if="isPreviousQuarter && advanceFilters">
                    <label>Number of Quarter(s)</label>
                    <multiselect v-model="numberOfPeriods" :options="['1', '2', '3', '4']" :show-labels="false"
                        v-bind:placeholder="$t('Select an Option')">
                    </multiselect>
                </div>
                <div class=" col-lg-4   form-group" v-if="isPreviousMonth && advanceFilters">
                    <label>Number of Month(s)</label>
                    <multiselect v-model="numberOfPeriods"
                        :options="['1', '2', '3', '4', '5', '6', '7', '8', '9', '10', '11', '12']" :show-labels="false"
                        v-bind:placeholder="$t('Select an Option')">
                    </multiselect>
                </div>
            </div>
            <hr class="hr-dashed hr-menu mt-0" />
            <div class="row" v-if="showCompareTable">
                <div class="card col-md-12">
                    <div class="card-body">
                        <div class="table-responsive" v-for="(inventory, index) in inventoryItems" v-bind:key="inventory.id">
                            <h6><b>{{ inventory.compareWith }}</b></h6>
                            <table class="table table-striped table-hover table_list_bg">
                                <thead class="thead-light table-hover">
                                    <tr>
                                        <th>#</th>
                                        <th>
                                            Product Code
                                        </th>
                                        <th>
                                            {{ $t('StockReport.Product') }}
                                        </th>
                                        <th class="text-center">
                                            {{ $t('StockReport.Opening') }}
                                        </th>
                                        <th class="text-center">
                                            {{ $t('StockReport.QuantityIn') }}
                                        </th>
                                        <th class="text-center">
                                            {{ $t('StockReport.QuantityOut') }}
                                        </th>
                                        <th class="text-center">
                                            {{ $t('StockReport.Balance') }}
                                        </th>
                                        <th class="text-center">
                                            Average Price
                                        </th>
                                        <th class="text-center">
                                            Stock Value
                                        </th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td>
                                            {{ index + 1 }}
                                        </td>
                                        <td>{{ inventory.productCode }}</td>
                                        <td>
                                            <span v-if="language == 'en'">{{ inventory.productName }}</span>
                                            <span v-else>{{ inventory.productNameArabic }}</span>
                                        </td>
                                        <td class="text-center">
                                            {{ inventory.opening }}
                                        </td>
                                        <td class="text-center" v-if="isMultiUnit == 'true'">
                                            {{ inventory.highQtyIn }}
                                        </td>
                                        <td class="text-center" v-else>
                                            {{ inventory.quantityIn }}
                                        </td>
                                        <td class="text-center" v-if="isMultiUnit == 'true'">
                                            {{ inventory.highQtyOut }}
                                        </td>
                                        <td class="text-center" v-else>
                                            {{ inventory.quantityOut }}
                                        </td>
                                        <td class="text-center" v-if="isMultiUnit == 'true'">
                                            {{ inventory.highQtyBalance }}
                                        </td>
                                        <td class="text-center" v-else>
                                            {{ inventory.balance }}
                                        </td>
                                        <td class="text-center">
                                            {{ inventory.avgPrice }}
                                        </td>
                                        <td class="text-center" v-if="isMultiUnit == 'true'">
                                            {{ (parseFloat(inventory.quantityIn * inventory.avgPrice)).toFixed(3).slice(0,
                                                -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g,
                                                    "$1,")
                                            }}
                                        </td>
                                        <td class="text-center" v-else>
                                            {{ (parseFloat(inventory.quantityIn * inventory.avgPrice)).toFixed(3).slice(0,
                                                -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g,
                                                    "$1,")
                                            }}
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row" v-if="showTable">
                <div class="card col-md-12">
                    <div class="card-body">
                        <div class="table-responsive">
                            <table class="table table-striped table-hover table_list_bg">
                                <thead class="thead-light table-hover">
                                    <tr>
                                        <th>#</th>
                                        <th>
                                            Product Code
                                        </th>
                                        <th>
                                            {{ $t('StockReport.Product') }}
                                        </th>
                                        <th class="text-center">
                                            {{ $t('StockReport.Opening') }}
                                        </th>
                                        <th class="text-center">
                                            {{ $t('StockReport.QuantityIn') }}
                                        </th>
                                        <th class="text-center">
                                            {{ $t('StockReport.QuantityOut') }}
                                        </th>
                                        <th class="text-center">
                                            {{ $t('StockReport.Balance') }}
                                        </th>
                                        <th class="text-center">
                                            Average Price
                                        </th>
                                        <th class="text-center">
                                            Stock Value
                                        </th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr v-for="(inventory, index) in inventoryItems" v-bind:key="inventory.id">
                                        <td>
                                            {{ index + 1 }}
                                        </td>
                                        <td>{{ inventory.productCode }}</td>
                                        <td>
                                            <span v-if="language == 'en'">{{ inventory.productName }}</span>
                                            <span v-else>{{ inventory.productNameArabic }}</span>
                                        </td>
                                        <td class="text-center">
                                            {{ inventory.opening }}
                                        </td>
                                        <td class="text-center" v-if="isMultiUnit == 'true'">
                                            {{ inventory.highQtyIn }}
                                        </td>
                                        <td class="text-center" v-else>
                                            {{ inventory.quantityIn }}
                                        </td>
                                        <td class="text-center" v-if="isMultiUnit == 'true'">
                                            {{ inventory.highQtyOut }}
                                        </td>
                                        <td class="text-center" v-else>
                                            {{ inventory.quantityOut }}
                                        </td>
                                        <td class="text-center" v-if="isMultiUnit == 'true'">
                                            {{ inventory.highQtyBalance }}
                                        </td>
                                        <td class="text-center" v-else>
                                            {{ inventory.balance }}
                                        </td>
                                        <td class="text-center">
                                            {{ inventory.avgPrice }}
                                        </td>
                                        <td class="text-center" v-if="isMultiUnit == 'true'">
                                            {{ (parseFloat(inventory.quantityIn * inventory.avgPrice)).toFixed(3).slice(0,
                                                -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g,
                                                    "$1,")
                                            }}
                                        </td>
                                        <td class="text-center" v-else>
                                            {{ (parseFloat(inventory.quantityIn * inventory.avgPrice)).toFixed(3).slice(0,
                                                -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g,
                                                    "$1,")
                                            }}
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>

        </div>
        <loading v-model:active="loading" :can-cancel="true" :is-full-page="true"></loading>

    </div>
    <div v-else>
        <acessdenied></acessdenied>
    </div>
</template>
<script>
import clickMixin from '@/Mixins/clickMixin'
import moment from "moment";
import Multiselect from "vue-multiselect";



export default {

    mixins: [clickMixin],
    props: ['formNameProp'],
    components: {
        Multiselect,
        
    },
    data: function () {
        return {
            allowBranches: false,
            branchIds: [],
            branchId: '',
            isPeriod: true,
            showDates: false,
            financialYears: [],
            showComparisonTable: false,
            compareWith: '',
            isPreviousYear: false,
            isPreviousPeriod: false,
            isPreviousQuarter: false,
            isPreviousMonth: false,
            numberOfPeriods: '',
            showTable: false,
            loading: false,
            reportOpt: "",
            dateRender: 0,
            rander: 0,
            advanceFilters: false,
            productId: "",
            inventoryItems: [],
            productsList: [],

            disablePeriod: false,
            disablePeriodRender: 0,

            showCompareTable: false,
          

            warehouseId: '00000000-0000-0000-0000-000000000000',
            combineDate: '',
            highQtyIn: 0,
            highQtyOut: 0,
            highQtyBalance: 0,
            isMultiUnit: '',
        }
    },
    watch: {
        
        branchIds: function () {
            var root = this;
            this.branchId = '';
            this.branchIds.forEach(function (result) {
                root.branchId = root.branchId == '' ? result.id : root.branchId + ',' + result.id;
            })
            this.GetInventoryList();
        }
    },
    methods: {
        AdvanceFilters: function () {
            this.fromDate = moment().format("DD MMM YYYY");
            this.toDate = moment().format("DD MMM YYYY");
            this.reportOpt = '';
            this.advanceFilters = !this.advanceFilters;
            this.showDates = false;
            this.disablePeriod = !this.disablePeriod;
            this.disablePeriodRender++;
            this.showDates = false;
            this.compareWith = "";
            this.numberOfPeriods = "";
            this.showTable = false;
            this.showCompareTable = false;
        },
        RemoveFilters: function () {
            this.reportOpt = '';
            this.show = false;
            this.showDates = false;
            this.numberOfPeriods = '';
            this.compareWith = '';
            this.showTable = false;
            this.showComparisonTable = false;
            this.showCompareTable = false;
            this.advanceFilters = false;
            this.disablePeriod = false;
            this.disablePeriodRender++;
            this.showDates = false;
            this.productId = "";
        },
        convertDate: function (date) {
            return moment(date).format("l");
        },
        GetDateTime: function () {

            if (this.reportOpt == 'Today') {
                this.fromDate = moment().format("DD MMM YYYY");
                this.toDate = moment().format("DD MMM YYYY");
                this.show = false;
                this.showDates = false;
                this.showTable = false;
                this.showComparisonTable = false;
            }
            if (this.reportOpt == 'This Week') {
                // Get the current date
                const currentDate = moment();

                // Get the Monday date of the current week (first day of the week)
                const firstDayOfWeek = moment(currentDate).startOf('week');

                // Format the dates as "DD MMM YYYY"
                this.fromDate = firstDayOfWeek.format("DD MMM YYYY");
                this.toDate = moment().format("DD MMM YYYY");
                this.show = false;
                this.showDates = false;
                this.showTable = false;
                this.showComparisonTable = false;
            }
            if (this.reportOpt == 'This Month') {
                // Get the current date
                const currentDate = moment();

                // Get the first day of the current month
                const firstDayOfMonth = moment(currentDate).startOf('month');

                // Format the dates as "DD MMM YYYY"
                this.fromDate = firstDayOfMonth.format("DD MMM YYYY");
                this.toDate = moment().format("DD MMM YYYY");

                this.show = false;
                this.showDates = false;
                this.showTable = false;
                this.showComparisonTable = false;
            }
            if (this.reportOpt == 'This Quarter') {
                // Get the current date
                const currentDate = moment();

                // Get the first day of the current quarter
                const firstDayOfQuarter = moment(currentDate).startOf('quarter');

                // Format the dates as "DD MMM YYYY"
                this.fromDate = firstDayOfQuarter.format("DD MMM YYYY");
                this.toDate = moment().format("DD MMM YYYY");

                this.show = false;
                this.showDates = false;
                this.showTable = false;
                this.showComparisonTable = false;
            }
            if (this.reportOpt == 'This Year') {
                // Get the current date
                const currentDate = moment();

                // Get the first day of the current year
                const firstDayOfYear = moment(currentDate).startOf('year');

                // Format the dates as "DD MMM YYYY"
                this.fromDate = firstDayOfYear.format("DD MMM YYYY");
                this.toDate = moment().format("DD MMM YYYY");
                this.show = false;
                this.showDates = false;
                this.showTable = false;
                this.showComparisonTable = false;
            }
            if (this.reportOpt == 'Yesterday') {
                // Get the current date
                const currentDate = moment();

                // Get yesterday's date
                const yesterday = moment(currentDate).subtract(1, 'day');

                // Format the dates as "DD MMM YYYY"
                this.fromDate = yesterday.format("DD MMM YYYY");
                this.toDate = moment().format("DD MMM YYYY");
                this.show = false;
                this.showDates = false;
                this.showTable = false;
                this.showComparisonTable = false;
            }
            if (this.reportOpt == 'Previous Week') {
                // Get the current date
                const currentDate = moment();

                // Get the first day (Monday) of the previous week
                const firstDayOfPreviousWeek = moment(currentDate).subtract(1, 'week').startOf('isoWeek');

                // Get the end day (Sunday) of the previous week
                const endDayOfPreviousWeek = moment(currentDate).subtract(1, 'week').endOf('isoWeek');

                // Format the dates as "DD MMM YYYY"
                this.fromDate = firstDayOfPreviousWeek.format("DD MMM YYYY");
                this.toDate = endDayOfPreviousWeek.format("DD MMM YYYY");
                this.show = false;
                this.showDates = false;
                this.showTable = false;
                this.showComparisonTable = false;
            }
            if (this.reportOpt == 'Previous Month') {
                // Get the current date
                const currentDate = moment();

                // Get the first day of the previous month
                const firstDayOfPreviousMonth = moment(currentDate).subtract(1, 'month').startOf('month');

                // Get the end day of the previous month
                const endDayOfPreviousMonth = moment(currentDate).subtract(1, 'month').endOf('month');

                // Format the dates as "DD MMM YYYY"
                this.fromDate = firstDayOfPreviousMonth.format("DD MMM YYYY");
                this.toDate = endDayOfPreviousMonth.format("DD MMM YYYY");
                this.show = false;
                this.showDates = false;
                this.showTable = false;
                this.showComparisonTable = false;
            }
            if (this.reportOpt == 'Previous Quarter') {
                // Get the current date
                const currentDate = moment();

                // Get the first day of the previous quarter
                const firstDayOfPreviousQuarter = moment(currentDate).subtract(1, 'quarter').startOf('quarter');

                // Get the end day of the previous quarter
                const endDayOfPreviousQuarter = moment(currentDate).subtract(1, 'quarter').endOf('quarter');

                // Format the dates as "DD MMM YYYY"
                this.fromDate = firstDayOfPreviousQuarter.format("DD MMM YYYY");
                this.toDate = endDayOfPreviousQuarter.format("DD MMM YYYY");
                this.show = false;
                this.showDates = false;
                this.showTable = false;
                this.showComparisonTable = false;
            }
            if (this.reportOpt == 'Previous Year') {
                // Get the current date
                const currentDate = moment();

                // Get the first day of the previous year
                const firstDayOfPreviousYear = moment(currentDate).subtract(1, 'year').startOf('year');

                // Get the end day of the previous year
                const endDayOfPreviousYear = moment(currentDate).subtract(1, 'year').endOf('year');

                // Format the dates as "DD MMM YYYY"
                this.fromDate = firstDayOfPreviousYear.format("DD MMM YYYY");
                this.toDate = endDayOfPreviousYear.format("DD MMM YYYY");

                this.show = false;
                this.showDates = false;
                this.showTable = false;
                this.showComparisonTable = false;
            }
            if (this.reportOpt == 'Custom') {

                this.toDate = moment().format("DD MMM YYYY");
                const yesterday = moment(this.toDate).subtract(6, 'day');
                // Format the dates as "DD MMM YYYY"
                this.fromDate = yesterday.format("DD MMM YYYY");
                this.show = false;
                this.showDates = true
                this.dateRender++;
                this.showTable = false;
                this.showComparisonTable = false;
            }
        },
        GetPeriods: function () {
            if (this.compareWith == 'Previous Year(s)') {
                this.financialYears = [];
                this.isPreviousYear = true;
                this.isPreviousPeriod = false;
                this.isPreviousQuarter = false;
                this.isPreviousMonth = false;
                this.isPeriod = false;
                this.numberOfPeriods = '';
            }
            if (this.compareWith == 'Previous Period(s)') {
                this.isPreviousYear = false;
                this.isPreviousPeriod = true;
                this.isPreviousQuarter = false;
                this.isPreviousMonth = false;
                this.getFinancialYears();
                this.numberOfPeriods = '';
                this.isPeriod = false;
            }
            if (this.compareWith == 'Previous Quarter(s)') {
                this.financialYears = [];
                this.isPreviousYear = false;
                this.isPreviousPeriod = false;
                this.isPreviousQuarter = true;
                this.isPreviousMonth = false;
                this.numberOfPeriods = '';
                this.isPeriod = false;
            }
            if (this.compareWith == 'Previous Month(s)') {
                this.financialYears = [];
                this.isPreviousYear = false;
                this.isPreviousPeriod = false;
                this.isPreviousQuarter = false;
                this.isPreviousMonth = true;
                this.numberOfPeriods = '';
                this.isPeriod = false;
            }
        },
        GotoPage: function (link) {
            this.$router.push({ path: link });
        },

        GetInventoryList: function () {
            var root = this;
            root.showTable = false;
            root.showCompareTable = false;
            var token = '';
            this.loading = true;
            if (token == '') {
                token = localStorage.getItem('token');
            }
            this.isShown = false;

            this.$https.get('/Report/GetAdvanceStockReport?fromDate=' + root.fromDate + '&toDate=' + root.toDate + '&numberOfPeriods=' + this.numberOfPeriods + '&compareWith=' + this.compareWith + '&productId=' + this.productId + '&branchId=' + this.branchId, { headers: { "Authorization": `Bearer ${token}` } })
                .then(function (response) {
                    if (response.data != null) {
                        if (root.compareWith != '' && root.compareWith != null) {
                            root.showCompareTable = true;
                            root.inventoryItems= response.data;
                            root.inventoryItems.forEach(function (x) {
                                x.highQtyIn = parseInt(parseFloat(x.quantityIn == 0 ? 0 : x.quantityIn) / parseFloat(x.unitPerPack == null ? 0 : x.unitPerPack)) + '-' + parseInt(parseFloat(x.quantityIn == undefined ? 0 : x.quantityIn) % parseFloat(x.unitPerPack == null ? 0 : x.unitPerPack))
                                x.highQtyOut = parseInt(parseFloat(x.quantityOut == 0 ? 0 : x.quantityOut) / parseFloat(x.unitPerPack == null ? 0 : x.unitPerPack)) + '-' + parseInt(parseFloat(x.quantityOut == undefined ? 0 : x.quantityOut) % parseFloat(x.unitPerPack == null ? 0 : x.unitPerPack))
                                x.highQtyBalance = parseInt(parseFloat(x.balance == 0 ? 0 : x.balance) / parseFloat(x.unitPerPack == null ? 0 : x.unitPerPack)) + '-' + parseInt(parseFloat(x.balance == undefined ? 0 : x.balance) % parseFloat(x.unitPerPack == null ? 0 : x.unitPerPack))
                            });

                        }
                        else {
                            root.showTable = true;
                            root.inventoryItems = response.data;
                            root.inventoryItems.forEach(function (x) {
                                x.highQtyIn = parseInt(parseFloat(x.quantityIn == undefined ? 0 : x.quantityIn) / parseFloat(x.unitPerPack == null ? 0 : x.unitPerPack)) + '-' + parseInt(parseFloat(x.quantityIn == undefined ? 0 : x.quantityIn) % parseFloat(x.unitPerPack == null ? 0 : x.unitPerPack))
                                x.highQtyOut = parseInt(parseFloat(x.quantityOut == undefined ? 0 : x.quantityOut) / parseFloat(x.unitPerPack == null ? 0 : x.unitPerPack)) + '-' + parseInt(parseFloat(x.quantityOut == undefined ? 0 : x.quantityOut) % parseFloat(x.unitPerPack == null ? 0 : x.unitPerPack))
                                x.highQtyBalance = parseInt(parseFloat(x.balance == undefined ? 0 : x.balance) / parseFloat(x.unitPerPack == null ? 0 : x.unitPerPack)) + '-' + parseInt(parseFloat(x.balance == undefined ? 0 : x.balance) % parseFloat(x.unitPerPack == null ? 0 : x.unitPerPack))
                            })
                        }
                    }
                    root.loading = false;
                });
        },
        getFinancialYears: function () {
            var root = this;
            var token = "";
            if (token == '') {
                token = localStorage.getItem("token");
            }
            this.$https.get("/Report/GetYearlyPeriodList", { headers: { Authorization: `Bearer ${token}` } })
                .then(function (response) {
                    if (response.data != null) {
                        const financialYear = response.data;

                        financialYear.forEach((item) => {
                            root.financialYears.push(item.name);
                        })
                    }
                });
        },
       
    },
    created: function () {
        this.isMultiUnit = localStorage.getItem('IsMultiUnit');
        this.language = this.$i18n.locale;
        this.fromDate = moment().subtract(15, 'days').format("DD MMM YYYY");
        this.toDate = moment().format("DD MMM YYYY");
        this.getFinancialYears();
        this.allowBranches = localStorage.getItem('AllowBranches') == 'true' ? true : false;
    }
}
</script>
<style scoped>
.bor {
    border: 1px solid #e3ebf6;
    overflow: auto;
}

.table-responsive {
    overflow-x: hidden !important;
}</style>