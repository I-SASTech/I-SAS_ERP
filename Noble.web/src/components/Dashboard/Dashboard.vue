<template>
    <div class="container-fluid" style="background-color:#F2F3F9 !important">
        <!-- Page-Title -->
        <ul class="nav nav-tabs" role="tablist">
            <li class="nav-item">
                <a class="nav-link " style="background-color:transparent  !important" data-bs-toggle="tab" href="#Dashboard" role="tab" aria-selected="true" v-bind:class="{active:active == 'Dashboard'}" v-on:click="makeActive('Dashboard')">{{ $t('Dashboard.Dashboard') }}</a>
            </li>
            <li class="nav-item">
                <a class="nav-link"  style="background-color:transparent  !important"  data-bs-toggle="tab" href="#Account" role="tab" aria-selected="false" v-bind:class="{active:active == 'Account'}" v-on:click="makeActive('Account')">{{ $t('Dashboard.Account') }}</a>
            </li>
            <li class="nav-item">
                <a class="nav-link"  style="background-color:transparent  !important"  data-bs-toggle="tab" href="#Inventory" role="tab" aria-selected="false" v-bind:class="{active:active == 'Inventory'}" v-on:click="makeActive('Inventory')">{{ $t('Dashboard.Inventory') }}</a>
            </li>
           
        </ul>
        <div class="col-12">
            <div v-if="active == 'Dashboard'">
                <div class="row mt-2">
                    <div class="col-lg-6 col-md-6 col-sm-6 pb-2">
                        <span class="card-title ">{{ $t('Dashboard.Overview') }} </span>

                    </div>
                    <div class="col-lg-6 col-md-6 col-sm-6 pb-2 text-end" v-bind:key="randerDropdown">

                        <div class="col">

                        </div>


                        <div class="col-auto">
                            <div class="col-auto">
                                <div class="dropdown">
                                    <a href="javascript:void(0)" class="btn btn-sm btn-outline-light dropdown-toggle" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                        {{overView}}<i class="las la-angle-down ms-1"></i>
                                    </a>
                                    <div class="dropdown-menu dropdown-menu-end">
                                        <a class="dropdown-item" v-on:click="OverViewFilterFunction('Today')" href="javascript:void(0);">{{ $t('Dashboard.Today') }} </a>
                                        <a class="dropdown-item" v-on:click="OverViewFilterFunction('Weekly')" href="javascript:void(0);">{{ $t('Dashboard.Weekly') }}</a>
                                        <a class="dropdown-item" v-on:click="OverViewFilterFunction('Monthly')" href="javascript:void(0);">{{ $t('Dashboard.Monthly') }}</a>
                                        <a class="dropdown-item" v-on:click="OverViewFilterFunction('6 Monthly')" href="javascript:void(0);">{{ $t('Dashboard.HalfYear') }}</a>
                                    </div>
                                </div>
                            </div>
                        </div>


                    </div>
                    <div class="col-md-6 col-lg-3">
                        <div class="card report-card" style="background-color: rgb(255 230 161) !important;">
                            <div class="card-body">
                                <div class="row d-flex justify-content-center" >
                                    <div class="col">
                                        <p class="text-dark mb-0 fw-semibold">{{ $t('Dashboard.Sales') }}</p>
                                        <h3 class="m-0">{{(parseFloat(income)).toFixed(0).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</h3>
                                    </div>
                                    <div class="col-auto align-self-center " >
                                        <div class="report-main-icon bg-light-alt " style="background-color:#FFD431 !important;color:white !important" >
                                            <i class="fab fa-twitch"></i>                                        </div>
                                    </div>
                                </div>


                            </div>
                        </div>
                    </div>
                    <div class="col-md-6 col-lg-3">
                        <div class="card report-card" style="background-color: rgb(110 255 174) !important;">
                            <div class="card-body">
                                <div class="row d-flex justify-content-center">
                                    <div class="col">
                                        <p class="text-dark mb-0 fw-semibold">{{ $t('Dashboard.Expense') }}</p>
                                        <h3 class="m-0">{{(parseFloat(expense)).toFixed(0).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</h3>
                                    </div>
                                    <div class="col-auto align-self-center">
                                        <div class="report-main-icon bg-light-alt " style="background-color:#2EBD64 !important;color:white !important">
                                            <i class="fas fa-fax"></i>                                        </div>
                                    </div>
                                </div>


                            </div>
                        </div>
                    </div>
                    <div class="col-md-6 col-lg-3">
                        <div class="card report-card" style="background-color: rgb(218 213 253) !important;">
                            <div class="card-body">
                                <div class="row d-flex justify-content-center">
                                    <div class="col">
                                        <p class="text-dark mb-0 fw-semibold">{{ $t('Dashboard.Credit') }}</p>
                                        <h3 class="m-0">{{(parseFloat(creditAmount)).toFixed(0).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</h3>
                                    </div>
                                    <div class="col-auto align-self-center">
                                        <div class="report-main-icon bg-light-alt" style="background-color:#6B57FB !important;color:white !important">
                                            <i class="fas fa-wallet"></i>                                        </div>
                                    </div>
                                </div>


                            </div>
                        </div>
                    </div>
                    <div class="col-md-6 col-lg-3">
                        <div class="card report-card" style="background-color: rgb(203 250 253) !important;">
                            <div class="card-body">
                                <div class="row d-flex justify-content-center">
                                    <div class="col">
                                        <p class="text-dark mb-0 fw-semibold">{{ $t('Dashboard.Purchase') }}</p>
                                        <h3 class="m-0">{{(parseFloat(purchase)).toFixed(0).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</h3>
                                    </div>
                                    <div class="col-auto align-self-center">
                                        
                                        <div class="report-main-icon bg-light-alt" style="background-color: rgb(107 218 255) !important;color: white !important;">
                                            <i class="fab fa-google-wallet"></i>                                         </div>
                                    </div>
                                </div>


                            </div>
                        </div>
                    </div>

                </div><!--end row-->

                <div class="row">
                    <div class="col-lg-4 col-md-6 col-12">
                        <div class="card">
                            <div class="card-header">
                                <div class="row align-items-center">
                                    <div class="col">
                                        <h4 class="card-title">
                                            {{ $t('Dashboard.InvoiceOverview') }}
                                        </h4>
                                    </div><!--end col-->

                                </div>  <!--end row-->
                            </div><!--end card-header-->
                            <div class="card-body">
                                <div class="">
                                    <apexchart type="donut" width="340" height="330" :options="chartOptions" :series="series9"></apexchart>
                                </div>
                            </div><!--end card-body-->
                        </div><!--end card-->
                    </div>
                    <div class="col-lg-4 col-md-6 col-12">
                        <div class="card">
                            <div class="card-header">
                                <div class="row align-items-center">
                                    <div class="col">
                                        <h4 class="card-title">
                                            {{ $t('Dashboard.Paid/Un-PaidInvoices') }}
                                        </h4>
                                    </div><!--end col-->

                                </div>  <!--end row-->
                            </div><!--end card-header-->
                            <div class="card-body">
                                <div class="">
                                    <apexchart type="pie" width="330" height="330" :options="chartOptionsPaidInvoices" :series="paidInvoicesSeries"></apexchart>
                                </div>
                            </div><!--end card-body-->
                        </div><!--end card-->
                    </div>
                    <div class="col-lg-4 col-md-6 col-12">
                        <div class="card">
                            <div class="card-header">
                                <div class="row align-items-center">
                                    <div class="col">
                                        <h4 class="card-title">
                                            {{ $t('Dashboard.Sale/Purchase') }}
                                        </h4>
                                    </div><!--end col-->

                                </div>  <!--end row-->
                            </div><!--end card-header-->
                            <div class="card-body">
                                <div class="">
                                    <apexchart type="donut" width="310" height="330" :options="salesOptions" :series="purchaseSeries"></apexchart>
                                </div>
                            </div><!--end card-body-->
                        </div><!--end card-->
                    </div>
                    <div class="col-lg-6">
                        <div class="card">
                            <div class="card-header">
                                <div class="row align-items-center">
                                    <div class="col">
                                        <h4 class="card-title">{{ $t('Dashboard.Earning') }}</h4>
                                    </div><!--end col-->

                                </div>  <!--end row-->
                            </div><!--end card-header-->
                            <div class="card-body">
                                <div class="">
                                    <apexchart type="area" height="350" v-bind:key="randerChart" :options="earningChartOption" :series="earningSeries"></apexchart>
                                </div>
                            </div><!--end card-body-->
                        </div><!--end card-->
                    </div>
                    <div class="col-lg-6">
                        <div class="card">
                            <div class="card-header">
                                <div class="row align-items-center">
                                    <div class="col">
                                        <h4 class="card-title">
                                            {{ $t('Dashboard.Sale/PurchaseOverview') }}
                                        </h4>
                                    </div><!--end col-->

                                </div>  <!--end row-->
                            </div><!--end card-header-->
                            <div class="card-body">
                                <div class="">
                                    <apexchart type="bar" height="350" v-bind:key="randerChart" :options="chartOptionsPurchase" :series="seriesPurchase"></apexchart>
                                </div>
                            </div><!--end card-body-->
                        </div><!--end card-->
                    </div>

                    

                </div>
            </div>
            <div v-if="active == 'Account'">

                <AccountDashboard :active="'Account'"></AccountDashboard>

            </div>
            <div v-if="active == 'Inventory'">

                <InventoryDashboard :active="'Inventory'"></InventoryDashboard>
            </div>
            <div v-if="active == 'CashAndBank'">

                <CashAndBankDashboard :active="'CashAndBank'"></CashAndBankDashboard>
            </div>
            <div v-if="active == 'HR'">

                <HRDashboard :active="'Account'"></HRDashboard>
            </div>
        </div>



    </div>




    
</template>
<script>

    import clickMixin from '@/Mixins/clickMixin'
    import moment from "moment";

    export default {


        mixins: [clickMixin],

        data: function () {
                  return {
                active: 'Dashboard',
                overView: 'Monthly',
                currency: '',
                randerChart: 0,
                income: 0,
                randerDropdown: 0,
                totalBank: 0,
                totalInvoices: 0,
                totalReturn: 0,
                expense: 0,
                purchase: 0,
                creditAmount: 0,
                earningSeries: [{
                    name: this.$t('Dashboard.Earning'),
                    data: []
                }],
                earningChartOption: {
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
                        type: 'datetime',
                        categories: []
                    },
                    tooltip: {
                        x: {
                            format: 'dd/MM/yy HH:mm'
                        },
                    },

                },

                chartOptions: {

                    labels: [this.$t('Dashboard.CreditInvoices'), this.$t('Dashboard.CashInvoices')]
            
  
                },
                salesOptions: {
                    labels: [this.$t('Dashboard.Sales'), this.$t('Dashboard.Purchase')]
                },
                chartOptionsPaidInvoices: {
                    labels: [this.$t('Dashboard.UnPaid'), this.$t('Dashboard.PartiallyPaid'), this.$t('Dashboard.FullyPaid')]
                },
                series9: [],
                purchaseSeries: [],
                paidInvoicesSeries: [],
                creditInvoicesSeries: [],
                chartOptionsCreditInvoices: {
                    chart: {
                        width: 380,
                        type: 'donut',
                    },
                    plotOptions: {
                        pie: {
                            startAngle: -90,
                            endAngle: 270
                        }
                    },
                    dataLabels: {
                        enabled: false
                    },
                    fill: {
                        type: 'gradient',
                    },
                    legend: {
                        formatter: function (val, opts) {
                            return val + " - " + opts.w.globals.creditInvoicesSeries[opts.seriesIndex]
                        }
                    },
                    title: {

                    },
                    responsive: [{
                        breakpoint: 480,
                        options: {
                            chart: {
                                width: 200
                            },
                            legend: {
                                position: 'bottom'
                            }
                        }
                    }]
                },

                seriesPurchase: [{
                    name: this.$t('Dashboard.Purchase'),
                    data: []
                }, {
                    name: this.$t('Dashboard.Sales'),
                    data: []
                }],
                chartOptionsPurchase: {
                    chart: {
                        height: 350,
                        type: 'bar'
                    },
                    dataLabels: {
                        enabled: false
                    },
                    plotOptions: {
                bar: {
                    columnWidth: '100%',  // Adjust the width of the bars
                    // Other bar configurations...
                }
            },
                    stroke: {
                        curve: 'smooth'
                    },
                    xaxis: {
                        type: 'datetime',
                        categories: []
                    },
                    tooltip: {
                        x: {
                            format: 'dd/MM/yy HH:mm'
                        },
                    },
                },


                loading: true,

                search: '',

                userID: 0,
                employeeId: 0,
                isChartLoad: false,
                fromDate: moment().format("DD MMM YYYY"),
                toDate: Date()
            }
        },
        watch: {

        },
        methods: {
            makeActive: function (item) {

                this.active = item;
            },
            OverViewFilterFunction: function (x) {


                this.overView = x;
                this.GetCashTransaction();
            },
            DetailOfProduct: function (x) {
try{
                this.$router.push({
                    path: '/ProductDetailDashboard',
                    query: { date: this.search, productId: x }
                })
            }
                catch (error) 
                {
                console.log(error);
               } 
            },
            getDate: function (date) {
                try{
                return moment(date).format('l');
            }
                catch (error) 
                {
                console.log(error);
               } 
            },

            GetCashTransaction: function () {
            try{
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                this.$https.get('/Company/GetTransaction?currentDate=' + root.search + '&overViewFilter=' + this.overView + '&activeTab=' + this.active, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                    if (response.data != null) {

                        root.series9 = [];
                        root.purchaseSeries = [];
                        root.paidInvoicesSeries = [];
                        root.earningSeries[0].data = [];
                        root.earningChartOption.xaxis.categories = [];
                        root.seriesPurchase[0].data = [];
                        root.seriesPurchase[1].data = [];
                        root.chartOptionsPurchase.xaxis.categories = [];
                        root.income = response.data.income;
                        root.creditAmount = response.data.creditAmount;
                        root.totalInvoices = response.data.totalInvoices;
                        root.totalReturn = response.data.totalReturn;
                        root.purchase = response.data.purchase;
                        root.expense = response.data.expense;
                        root.totalCreditInvoices = response.data.totalCreditInvoices;
                        response.data.earningList.forEach(function (result) {
                            root.earningSeries[0].data.push((parseFloat(result.amount)).toFixed(0));
                            root.earningChartOption.xaxis.categories.push(result.date);
                        });
                        response.data.salePurchaseLookUpModel.forEach(function (result) {
                            root.seriesPurchase[0].data.push((parseFloat(result.purchaseAmount)).toFixed(0));
                            root.seriesPurchase[1].data.push(parseFloat(result.saleAmount).toFixed(0));
                            root.chartOptionsPurchase.xaxis.categories.push(result.date);
                        });

                        var creditInvoices = parseInt((response.data.totalCreditInvoices / response.data.totalInvoices) * 100);
                        var cashInvoices = parseInt((response.data.totalCashInvoices / response.data.totalInvoices) * 100);
                        root.series9.push(creditInvoices);
                        root.series9.push(cashInvoices);
                        root.purchaseSeries.push(root.income);
                        root.purchaseSeries.push(root.purchase);
                        var unPaidInvoices = parseInt((response.data.unPaidInvoices / response.data.totalInvoices) * 100);
                        var partially = parseInt((response.data.partiallyInvoices / response.data.totalInvoices) * 100);
                        var fullyInvoices = parseInt((response.data.fullyInvoices / response.data.totalInvoices) * 100);
                        root.paidInvoicesSeries.push(unPaidInvoices);
                        root.paidInvoicesSeries.push(partially);
                        root.paidInvoicesSeries.push(fullyInvoices);
                        root.randerChart++;


                    }
                });
            }
                catch (error) 
                {
                console.log(error);
               } 
            },


        },
        created: function () {
            try{

            this.$emit('update:modelValue', this.$route.name);
        }
                catch (error) 
                {
                console.log(error);
               } 
        },
        mounted: function () {

try{
            this.currency = localStorage.getItem('currency');

            this.fromDate = moment().startOf('month').format("DD MMM YYYY");

            if (this.$store.isAuthenticated) {
                this.userID = localStorage.getItem('UserID');
                this.employeeId = localStorage.getItem('EmployeeId');
                this.fromDate = moment().startOf('month').format("DD MMM YYYY");
                //this.isDayStart();

                // this.getDataSaleIncome();
            }

            this.GetCashTransaction();
            this.search = moment().format("DD MMM YYYY");

            this.chartbymonth = moment().format("DD MMM YYYY");
            this.rander++;
            this.randerDropdown++;

            //this.GetInventory(this.search, 1);
        }
                catch (error) 
                {
                console.log(error);
               } 
        },
        
    }
</script>
<style  scoped>
.ChangeColor{
    background-image: linear-gradient(310deg, #7928ca, #ff0080);}

</style>