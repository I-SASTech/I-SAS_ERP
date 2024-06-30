<template>
    <div class="row">
        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">
                                    Product Summary Report
                                </h4>

                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{
                                        $t('InvoicePrintReport.Home') }}</a></li>
                                    <li class="breadcrumb-item active">
                                        Product Summary Report
                                    </li>
                                </ol>
                            </div>
                            <div class="col-auto align-self-center">
                                <a v-on:click="PrintRdlc()" class="btn btn-sm btn-outline-primary mx-1">
                                    <i class="fas fa-print font-14"></i>
                                    {{ $t('Print') }}
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
            <div class="row align-items-center">
                <div class="row align-items-center">
                    <div class=" col-lg-4 form-group">
                        <label>Products:</label>
                        <product-dropdown v-model="productId" :IsReport="true" :key="rander" />
                    </div>
                    <div class=" col-lg-4   form-group">
                        <label>Compare With:</label>
                        <multiselect v-model="compareWith"
                            :options="['Previous Year(s)', 'Previous Period(s)', 'Previous Quarter(s)', 'Previous Month(s)', 'Previous Day(s)']"
                            :show-labels="false" v-bind:placeholder="$t('Select an Option')" @update:modelValue="GetPeriods()">
                        </multiselect>
                    </div>
                    <div class=" col-lg-4 form-group" v-if="allowBranches">
                    <label>{{ $t('Branches.SelectBranch') }}</label>
                    <branch-dropdown v-model="branchIds" :ismultiple="true" :islayout="false" />
                </div>
                    <div class=" col-lg-4   form-group" v-if="isPreviousYear">
                        <label>Number of Year(s)</label>
                        <multiselect v-model="numberOfPeriods" :options="['1', '2', '3', '4', '5']" :show-labels="false"
                            v-bind:placeholder="$t('Select an Option')">
                        </multiselect>
                    </div>
                    <div class=" col-lg-4   form-group" v-if="isPreviousPeriod">
                        <label>Number of Period(s)</label>
                        <multiselect v-model="numberOfPeriods" :options="financialYears" :show-labels="false"
                            v-bind:placeholder="$t('Select an Option')">
                        </multiselect>
                    </div>
                    <div class=" col-lg-4   form-group" v-if="isPreviousQuarter">
                        <label>Number of Quarter(s)</label>
                        <multiselect v-model="numberOfPeriods" :options="['1', '2', '3', '4']" :show-labels="false"
                            v-bind:placeholder="$t('Select an Option')">
                        </multiselect>
                    </div>
                    <div class=" col-lg-4   form-group" v-if="isPreviousMonth">
                        <label>Number of Month(s)</label>
                        <multiselect v-model="numberOfPeriods"
                            :options="['1', '2', '3', '4', '5', '6', '7', '8', '9', '10', '11', '12']" :show-labels="false"
                            v-bind:placeholder="$t('Select an Option')">
                        </multiselect>
                    </div>
                    <div class=" col-lg-4   form-group" v-if="isPreviousDays">
                        <label>Number of Days(s)</label>
                        <multiselect v-model="numberOfPeriods" :options="['7 Days', '15 Days', '30 Days']"
                            :show-labels="false" v-bind:placeholder="$t('Select an Option')">
                        </multiselect>
                    </div>
                    <div class=" col-lg-4  form-group pe-0">
                        <button v-if="numberOfPeriods == 0 || productId == ''" disabled href="javascript:void(0);"
                            class="btn btn-outline-primary me-2">
                            Apply Filters
                        </button>
                        <button v-else v-on:click="GetProductSummaryList()" href="javascript:void(0);"
                            class="btn btn-outline-primary me-2">
                            Apply Filters
                        </button>
                        <a v-on:click="RemoveFilters()" href="javascript:void(0);" class="btn btn-outline-danger">
                            Clear Filters
                        </a>
                    </div>
                </div>
            </div>
            <hr class="hr-dashed hr-menu mt-0" />
            <div class="row" v-if="showTable">
               
                <div class="col-md-12">
                    <div class="accordion" id="accordionExample">
                    <div class="accordion-item">
                        <h5 class="accordion-header m-0" id="headingOne">
                            <button class="accordion-button fw-semibold collapsed" type="button" data-bs-toggle="collapse"
                                data-bs-target="#collapseOne" aria-expanded="false" aria-controls="collapseOne">
                                Product Sale Summary
                            </button>
                        </h5>
                        <div id="collapseOne" class="accordion-collapse collapse" aria-labelledby="headingOne"
                            data-bs-parent="#accordionExample" style="">
                            <div class="accordion-body">
                                <div id="chart">
                                    <apexchart type="line" height="350" :options="chartOptions" :series="series" :key="render">
                                    </apexchart>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="accordion-item">
                        <h5 class="accordion-header m-0" id="headingTwo">
                            <button class="accordion-button fw-semibold collapsed" type="button" data-bs-toggle="collapse"
                                data-bs-target="#collapseTwo" aria-expanded="false" aria-controls="collapseTwo">
                                Product Purchase Summary
                            </button>
                        </h5>
                        <div id="collapseTwo" class="accordion-collapse collapse" aria-labelledby="headingTwo"
                            data-bs-parent="#accordionExample" style="">
                            <div class="accordion-body">
                                <div id="chart">
                                    <apexchart type="line" height="350" :options="chartOptions1" :series="series1" :key="render">
                                    </apexchart>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                   
                </div>
                <div class="card col-md-12">
                    <div class="card-body"></div>
                    <div class="table-responsive" v-for="(sale, index) in productSummaryList" :key="index">
                        <h6>{{ sale.compareWith }}</h6>
                        <table class="table table-striped table-hover">
                            <thead class="thead-light table-hover">
                                <tr>
                                    <th class="text-center"></th>
                                    <th colspan="5" class="text-center fw-bold custborder">Sale</th>
                                    <th colspan="5" class="text-center fw-bold">Purchase</th>
                                </tr>
                                <tr>
                                    <th>#</th>
                                    <th class="text-center">
                                        Total {{ $t('InvoicePrintReport.GrossValue') }}
                                    </th>
                                    <th class="text-center">
                                        Total {{ $t('InvoicePrintReport.DisountAmount') }}
                                    </th>
                                    <th class="text-center">
                                        Total {{ $t('InvoicePrintReport.NetSaleAmount') }}
                                    </th>
                                    <th class="text-center">
                                        Total {{ $t('InvoicePrintReport.VATAmount') }}
                                    </th>
                                    <th class="text-center custborder">
                                        {{ $t('InvoicePrintReport.TotalAmount') }}
                                    </th>
                                    <th class="text-center">
                                        Total {{ $t('InvoicePrintReport.GrossValue') }}
                                    </th>
                                    <th class="text-center">
                                        Total {{ $t('InvoicePrintReport.DisountAmount') }}
                                    </th>
                                    <th class="text-center">
                                        Total {{ $t('InvoicePrintReport.NetSaleAmount') }}
                                    </th>
                                    <th class="text-center">
                                        Total {{ $t('InvoicePrintReport.VATAmount') }}
                                    </th>
                                    <th class="text-center">
                                        {{ $t('InvoicePrintReport.TotalAmount') }}
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td>
                                        {{ index + 1 }}
                                    </td>
                                    <td class="text-center">
                                        {{ sale.totalGrossAmount }}
                                    </td>
                                    <td class="text-center">
                                        {{ sale.totalDiscountAmount }}
                                    </td>
                                    <td class="text-center">
                                        {{
                                            (parseFloat((sale.totalGrossAmount) - (sale.totalDiscountAmount)))
                                                .toFixed(3)
                                                .slice(0, -1)
                                                .replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")
                                        }}
                                    </td>
                                    <td class="text-center">
                                        {{ sale.totalVatAmount }}
                                    </td>
                                    <td class="text-center custborder">
                                        {{ sale.totalTotalAmount }}
                                    </td>
                                    <td class="text-center">
                                        {{ sale.totalPurchaseGrossAmount }}
                                    </td>
                                    <td class="text-center">
                                        {{ sale.totalPurchaseDiscountAmount }}
                                    </td>
                                    <td class="text-center">
                                        {{
                                            (parseFloat((sale.totalPurchaseGrossAmount) - (sale.totalPurchaseDiscountAmount)))
                                                .toFixed(3)
                                                .slice(0, -1)
                                                .replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")
                                        }}
                                    </td>
                                    <td class="text-center">
                                        {{ sale.totalPurchaseVatAmount }}
                                    </td>
                                    <td class="text-center">
                                        {{ sale.totalPurchaseAmount }}
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
        <invoicedetailsprint :show="show" v-if="show" :reportsrc="reportsrc1" :changereport="changereportt"
            @close="show = false" @IsSave="IsSave" />
        <loading v-model:active="loading" :can-cancel="true" :is-full-page="true"></loading>
    </div>
</template>
<script>
import clickMixin from '@/Mixins/clickMixin'
import moment from "moment";
import Multiselect from 'vue-multiselect'



export default {
    mixins: [clickMixin],
    components: {
        Multiselect,
        
    },
    data: function () {
        return {
            allowBranches: false,
            branchIds: [],
            branchId: '',
            financialYears: [],
            compareWith: '',
            reportsrc1: '',
            changereportt: 0,
            show: false,
            isPreviousYear: false,
            isPreviousPeriod: false,
            isPreviousQuarter: false,
            isPreviousMonth: false,
            isPreviousDays: false,
            numberOfPeriods: '',
            loading: false,
            showTable: false,
            productId: '',
            productSummaryList: [],
            render: 0,
            rander: 0,
            series: [],
            chartOptions: {
                chart: {
                    height: 350,
                    type: 'line',
                    zoom: {
                        enabled: false
                    },
                },
                dataLabels: {
                    enabled: false
                },
                stroke: {
                    width: [5, 7, 5],
                    curve: 'straight',
                    dashArray: [0, 8, 5]
                },
                title: {
                    text: 'Product Summary',
                    align: 'left'
                },
                legend: {
                    tooltipHoverFormatter: function (val, opts) {
                        return val + ' - ' + opts.w.globals.series[opts.seriesIndex][opts.dataPointIndex] + ''
                    }
                },
                markers: {
                    size: 0,
                    hover: {
                        sizeOffset: 6
                    }
                },
                xaxis: {
                    categories: [],
                    convertedCatToNumeric: false
                },
                grid: {
                    borderColor: '#f1f1f1',
                }
            },
            series1: [],
            chartOptions1: {
                chart: {
                    height: 350,
                    type: 'line',
                    zoom: {
                        enabled: false
                    },
                },
                dataLabels: {
                    enabled: false
                },
                stroke: {
                    width: [5, 7, 5],
                    curve: 'straight',
                    dashArray: [0, 8, 5]
                },
                title: {
                    text: 'Product Summary',
                    align: 'left'
                },
                legend: {
                    tooltipHoverFormatter: function (val, opts) {
                        return val + ' - ' + opts.w.globals.series[opts.seriesIndex][opts.dataPointIndex] + ''
                    }
                },
                markers: {
                    size: 0,
                    hover: {
                        sizeOffset: 6
                    }
                },
                xaxis: {
                    categories: [],
                    convertedCatToNumeric: false
                },
                grid: {
                    borderColor: '#f1f1f1',
                }
            },
        }
    },

    watch: {
        
        branchIds: function () {
            var root = this;
            this.branchId = '';
            this.branchIds.forEach(function (result) {
                root.branchId = root.branchId == '' ? result.id : root.branchId + ',' + result.id;
            })
            this.GetProductSummaryList();
        }
    },
    methods: {
        GotoPage: function (link) {
            this.$router.push({ path: link });
        },

        getDate: function (date) {
            return moment(date).format('l');
        },
        AdvanceFilters: function () {
            this.fromDate = moment().format("DD MMM YYYY");
            this.toDate = moment().format("DD MMM YYYY");
            this.showDates = false;
            this.compareWith = "";
            this.numberOfPeriods = "";
        },
        RemoveFilters: function () {
            this.numberOfPeriods = '';
            this.compareWith = '';
            this.customerId = '';
            this.productId = '';
            this.rander++;
            this.showTable = false;
        },
        convertDate: function (date) {
            return moment(date).format("l");
        },
        GetPeriods: function () {
            if (this.compareWith == 'Previous Year(s)') {
                this.financialYears = [];
                this.isPreviousYear = true;
                this.isPreviousPeriod = false;
                this.isPreviousQuarter = false;
                this.isPreviousMonth = false;
                this.isPreviousDays = false;
                this.numberOfPeriods = '';
            }
            if (this.compareWith == 'Previous Period(s)') {
                this.isPreviousYear = false;
                this.isPreviousPeriod = true;
                this.isPreviousQuarter = false;
                this.isPreviousMonth = false;
                this.isPreviousDays = false;
                this.numberOfPeriods = '';
                this.getFinancialYears();
            }
            if (this.compareWith == 'Previous Quarter(s)') {
                this.financialYears = [];
                this.isPreviousYear = false;
                this.isPreviousPeriod = false;
                this.isPreviousQuarter = true;
                this.isPreviousMonth = false;
                this.isPreviousDays = false;
                this.numberOfPeriods = '';
            }
            if (this.compareWith == 'Previous Month(s)') {
                this.financialYears = [];
                this.isPreviousYear = false;
                this.isPreviousPeriod = false;
                this.isPreviousQuarter = false;
                this.isPreviousDays = false;
                this.isPreviousMonth = true;
                this.numberOfPeriods = '';
            }
            if (this.compareWith == 'Previous Day(s)') {
                this.financialYears = [];
                this.isPreviousYear = false;
                this.isPreviousPeriod = false;
                this.isPreviousQuarter = false;
                this.isPreviousMonth = false;
                this.isPreviousDays = true;
                this.numberOfPeriods = '';
            }
        },
        IsSave: function () {
            this.show = !this.show;
        },
        PrintRdlc:function() {
            
            var companyId = '';
                        companyId = localStorage.getItem('CompanyID');
                    
                        this.reportsrc1=  this.$ReportServer+'/ReportManagementModule/SalePurchase/SalePurchaseReports.aspx?invoiceType=' + this.invoiceType + '&numberOfPeriods=' + this.numberOfPeriods + '&compareWith=' + this.compareWith +'&formName=PurchaseInvoiceSummary'+'&companyId='+companyId 
                        this.changereportt++;
                        this.show = !this.show;
                },
        GetProductSummaryList: function () {
            var root = this;
            var token = '';
            this.loading = true;
            if (token == '') {
                token = localStorage.getItem('token');
            }
            this.$https.get('/Report/GetProductSummary?productId=' + this.productId + '&numberOfPeriods=' + this.numberOfPeriods + '&compareWith=' + this.compareWith + '&branchId=' + this.branchId, { headers: { "Authorization": `Bearer ${token}` } })
                .then(function (response) {
                    if (response.data != null) {
                        root.showTable = true;
                        root.productSummaryList = response.data;

                        var totalGrossAmount = [];
                        var totalDiscountAmount = [];
                        var totalNetAmount = [];
                        var totalVatAmount = [];
                        var totalAmount = [];
                        var totalPurchaseGrossAmount = [];
                        var totalPurchaseDiscountAmount = [];
                        var totalPurchaseNetAmount = [];
                        var totalPurchaseVatAmount = [];
                        var totalPurchaseAmount = [];
                        root.chartOptions.xaxis.categories = [];
                        root.productSummaryList.forEach(x => {
                            totalGrossAmount.push(x.totalGrossAmount);
                            totalDiscountAmount.push(x.totalDiscountAmount);
                            totalNetAmount.push(x.totalNetTotal);
                            totalVatAmount.push(x.totalVatAmount);
                            totalAmount.push(x.totalTotalAmount);
                            totalPurchaseGrossAmount.push(x.totalPurchaseGrossAmount);
                            totalPurchaseDiscountAmount.push(x.totalPurchaseDiscountAmount);
                            totalPurchaseNetAmount.push(x.totalPurchaseNetTotal);
                            totalPurchaseVatAmount.push(x.totalPurchaseVatAmount);
                            totalPurchaseAmount.push(x.totalPurchaseAmount);
                            root.chartOptions.xaxis.categories.push(x.apexChartValue);
                            root.chartOptions1.xaxis.categories.push(x.apexChartValue);
                        });
                        root.series = [];
                        root.series1 = [];
                        root.series.push({
                            name: "Gross Amount",
                            data: totalGrossAmount
                        });
                        root.series.push({
                            name: "Discount Amount",
                            data: totalDiscountAmount
                        });
                        root.series.push({
                            name: "Net Amount",
                            data: totalNetAmount
                        });
                        root.series.push({
                            name: "Vat Amount",
                            data: totalVatAmount
                        });
                        root.series.push({
                            name: "Total Amount",
                            data: totalAmount
                        });
                        root.series1.push({
                            name: "Gross Amount",
                            data: totalPurchaseGrossAmount
                        });
                        root.series1.push({
                            name: "Discount Amount",
                            data: totalPurchaseDiscountAmount
                        });
                        root.series1.push({
                            name: "Net Amount",
                            data: totalPurchaseNetAmount
                        });
                        root.series1.push({
                            name: "Vat Amount",
                            data: totalPurchaseVatAmount
                        });
                        root.series1.push({
                            name: "Total Amount",
                            data: totalPurchaseAmount
                        });
                        root.render++;
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
                        root.financialYears = [];
                        financialYear.forEach((item) => {
                            root.financialYears.push(item.name);
                        })
                    }
                });
        },
    },
    mounted: function () {
        this.getFinancialYears();
        this.allowBranches = localStorage.getItem('AllowBranches') == 'true' ? true : false;
    }
}
</script>
<style scoped>
.custborder{
    border-right: 1px solid rgb(93, 91, 91) !important;
}
</style>