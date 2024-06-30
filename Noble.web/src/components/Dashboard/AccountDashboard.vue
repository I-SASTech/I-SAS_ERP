<template>
    <div class="col-12">
        <div class="row mt-2">
            <div class="col-lg-6 col-md-6 col-sm-6 pb-2">
                <span class="card-title "> {{ $t('AccountDashboard.Overview') }}</span>

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
                                <a class="dropdown-item" v-on:click="OverViewFilterFunction('Monthly')" href="javascript:void(0);">{{ $t('AccountDashboard.Monthly') }} </a>
                                <a class="dropdown-item" v-on:click="OverViewFilterFunction('3 Month')" href="javascript:void(0);">{{ $t('AccountDashboard.ThreeMonth') }}</a>
                                <a class="dropdown-item" v-on:click="OverViewFilterFunction('6 Month')" href="javascript:void(0);">{{ $t('AccountDashboard.HalfYear') }}</a>
                                <a class="dropdown-item" v-on:click="OverViewFilterFunction('Year')" href="javascript:void(0);">{{ $t('AccountDashboard.Year') }}</a>
                            </div>
                        </div>
                    </div>
                </div>


            </div>
            <div class="col-md-6 col-lg-3" >
                <div class="card report-card" style="background-color: rgb(255 230 161) !important;">
                    <div class="card-body">
                        <div class="row d-flex justify-content-center">
                            <div class="col">
                                <p class="text-dark mb-0 fw-semibold">{{ $t('AccountDashboard.Cash') }}</p>
                                <h3 class="m-0">{{(parseFloat(cash)).toFixed(0).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</h3>
                            </div>
                            <div class="col-auto align-self-center">
                                <div class="report-main-icon bg-light-alt" style="background-color:#FFD431 !important;color: white !important;">
                                    <i class="fas fa-fax"></i>     
                                                                </div>
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
                                <p class="text-dark mb-0 fw-semibold">{{ $t('AccountDashboard.BankBalance') }}</p>
                                <h3 class="m-0">{{(parseFloat(banks)).toFixed(0).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</h3>
                            </div>
                            <div class="col-auto align-self-center">
                                <div class="report-main-icon bg-light-alt" style="background-color:#2EBD64 !important;color:white !important">
                                    <i class="fab fa-twitch"></i>                                 </div>
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
                                <p class="text-dark mb-0 fw-semibold">{{ $t('AccountDashboard.AccountReceivable') }}</p>
                                <h3 class="m-0">{{(parseFloat(accountReceivable)).toFixed(0).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</h3>
                            </div>
                            <div class="col-auto align-self-center">
                                <div class="report-main-icon bg-light-alt" style="background-color:#6B57FB !important;color:white !important">
                                    <i class="fas fa-tags"></i>                                </div>
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
                                <p class="text-dark mb-0 fw-semibold">{{ $t('AccountDashboard.AccountPayable') }}</p>
                                <h3 class="m-0">{{(parseFloat(accountPayable)).toFixed(0).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</h3>
                            </div>
                            <div class="col-auto align-self-center">
                                <div class="report-main-icon bg-light-alt" style="background-color: rgb(107 218 255) !important;color: white !important;">
                                    <i class="fas fa-fax"></i>                                     </div>
                            </div>
                        </div>


                    </div>
                </div>
            </div>

        </div><!--end row-->

        <div class="row">
            <div class="col-lg-6 col-md-12 col-sm-12">
                <div class="card">
                    <div class="card-header">
                        <div class="row align-items-center">
                            <div class="col">
                                <h4 class="card-title">{{ $t('AccountDashboard.AccountReceivableandPayable') }}</h4>
                            </div><!--end col-->

                        </div>  <!--end row-->
                    </div><!--end card-header-->
                    <div class="card-body">
                        <div class="">
                            <apexchart type="area" height="350" v-bind:key="randerChart" :options="chartOptionsPayableAccount" :series="seriesPayableAccount"></apexchart>
                        </div>
                    </div><!--end card-body-->
                </div><!--end card-->
            </div>
            <div class="col-lg-6 col-md-12 col-sm-12">
                <div class="card">
                    <div class="card-header">
                        <div class="row align-items-center">
                            <div class="col">
                                <h4 class="card-title">{{ $t('AccountDashboard.IncomeVsExpense') }}</h4>
                            </div><!--end col-->

                        </div>  <!--end row-->
                    </div><!--end card-header-->
                    <div class="card-body">
                        <div class="">
                            <apexchart type="bar" height="350" :options="chartOptions2" :series="series2" v-bind:key="randerChart"></apexchart>
                        </div>
                    </div><!--end card-body-->
                </div><!--end card-->
            </div>
            <div class="col-lg-4 col-md-6 col-12">
                <div class="card">
                    <div class="card-header">
                        <div class="row align-items-center">
                            <div class="col">
                                <h4 class="card-title">{{ $t('AccountDashboard.IncomeVsExpense') }}</h4>
                            </div><!--end col-->

                        </div>  <!--end row-->
                    </div><!--end card-header-->
                    <div class="card-body">
                        <div class="">
                            <apexchart type="donut" width="320" height="330" :options="chartOptions" :series="series"></apexchart>
                        </div>
                    </div><!--end card-body-->
                </div><!--end card-->
            </div>
            <div class="col-lg-4 col-md-6 col-12">
                <div class="card">
                    <div class="card-header">
                        <div class="row align-items-center">
                            <div class="col">
                                <h4 class="card-title">{{ $t('AccountDashboard.VATOverView') }} </h4>
                            </div><!--end col-->

                        </div>  <!--end row-->
                    </div><!--end card-header-->
                    <div class="card-body">
                        <div class="">
                            <apexchart type="pie" width="340" height="330" :options="vatOptions" :series="vatSeries"></apexchart>
                        </div>
                    </div><!--end card-body-->
                </div><!--end card-->
            </div> 
            <div class="col-lg-4 col-md-6 col-12">
                <div class="card">
                    <div class="card-header">
                        <div class="row align-items-center">
                            <div class="col">
                                <h4 class="card-title">{{ $t('AccountDashboard.AdvancePayments') }} </h4>
                            </div><!--end col-->

                        </div>  <!--end row-->
                    </div><!--end card-header-->
                    <div class="card-body">
                        <div class="">
                            <apexchart type="donut" width="340" height="330" :options="advanceOptions" :series="advanceSeries"></apexchart>
                        </div>
                    </div><!--end card-body-->
                </div><!--end card-->
            </div>
            <div class="col-lg-6 col-md-12 col-sm-12">
                <div class="card">
                    <div class="card-header">
                        <div class="row align-items-center">
                            <div class="col">
                                <h4 class="card-title">{{ $t('AccountDashboard.Expense') }} </h4>
                            </div><!--end col-->

                        </div>  <!--end row-->
                    </div><!--end card-header-->
                    <div class="card-body">
                        <div class="">
                            <apexchart type="bar" height="350" :options="expensechartOptions" :series="expenseSeries" v-bind:key="randerChart"></apexchart>
                        </div>
                    </div><!--end card-body-->
                </div><!--end card-->
            </div>
            <!--<div class="col-lg-6 col-md-12 col-sm-12">
                <div class="card">
                    <div class="card-header">
                        <div class="row align-items-center">
                            <div class="col">
                                <h4 class="card-title"> Account Over View  </h4>
                            </div>--><!--end col-->

                        <!--</div>-->  <!--end row-->
                    <!--</div>--><!--end card-header-->
                    <!--<div class="card-body">
                        <div class="">
                            <apexchart type="bar" height="350" :options="accountOverViewchartOptions" :series="accountOverViewSeries" v-bind:key="randerChart"></apexchart>
                        </div>
                    </div>--><!--end card-body-->
                <!--</div>--><!--end card-->
            <!--</div>-->




            <!--<div class="col-lg-6 col-md-6 col-sm-6">
            <div class="card ">

                <div class="card-header ">

                    <span class="card-title DayHeading">Income and Expense </span>
                    <div class="row">

                        <div class="col-12 col-md-12 pt-2 pb-2">

                            <div class="text-left row ">
                                <div class="col-6 col-md-6">
                                    <span class="DashboardFontSize" style="padding-bottom:0px !important;color:#3178F6;font-weight:bold">Income</span><br />
                                    <div v-for="inv in incomeList" v-bind:key="inv.amount" >
                                     <span style="font-size:20px;font-weight:bold"> {{inv.costCenter}} <span style="color:#3178F6;float:right;">{{currency}} {{(parseFloat(inv.amount)).toFixed(0).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</span></span>


                                    </div>

                                </div>
                                <div class="col-6 col-md-6">
                                    <span class="DashboardFontSize" style="padding-bottom:0px !important;color:#3178F6;font-weight:bold">Expense</span><br />
                                    <div v-for="inv in expenseList" v-bind:key="inv.amount">
                                        <span style="font-size:20px;font-weight:bold"> {{inv.costCenter}} <span style="color:#3178F6;float:right;font-weight:bold"> {{currency}} {{(parseFloat(inv.amount)).toFixed(0).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</span></span>

                                    </div>
                                </div>




                            </div>

                        </div>


                    </div>


                </div>

            </div>


        </div>-->




        </div>
        <!--<loading v-model:active="loading" :can-cancel="false" :is-full-page="true"></loading>-->

    </div>
</template>
<script>

    import clickMixin from '@/Mixins/clickMixin'
    import moment from "moment";
    //
    //
    export default {
        props: ["active"],
        name: 'AccountDashboard',
      
        mixins: [clickMixin],

        data: function () {

            return {
                overView: 'Year',
                currency: '',
                incomeList: [],
                expenseList: [],
                randerChart: 0,
                randerDropdown: 0,
                cash: 0,
                banks: 0,
                vatPayable: 0,
                vatReceivable: 0,
                advancePayable: 0,
                advanceReceivable: 0,
                accountPayable: 0,
                accountReceivable: 0,
                totalInvoices: 0,
                totalReturn: 0,
                expense: 0,
                purchase: 0,
                creditAmount: 0,
                series: [],
                chartOptions: {
                    labels: [this.$t('AccountDashboard.Income'),this.$t('AccountDashboard.Expense')]
                },
                vatSeries:[],
                vatOptions: {
                    labels: [this.$t('AccountDashboard.Payable') , this.$t('AccountDashboard.Receivable') ]
                },
                advanceSeries: [],

                advanceOptions: {
                    labels: [this.$t('AccountDashboard.Payable'), this.$t('AccountDashboard.Receivable')]
                },
              
                expenseSeries: [{
                    name: this.$t('AccountDashboard.Expense'),
                    data: []
                }],
              
                expensechartOptions: {
                    chart: {
                        type: 'bar',
                        height: 380
                    },
                    plotOptions: {
                        bar: {
                            barHeight: '100%',
                            distributed: true,
                            horizontal: true,
                            dataLabels: {
                                position: 'bottom'
                            },
                        }
                    },
                    colors: ['#33b2df', '#546E7A', '#d4526e', '#13d8aa', '#A5978B', '#2b908f', '#f9a3a4', '#90ee7e',
                        '#f48024', '#69d2e7'
                    ],
                    dataLabels: {
                        enabled: true,
                        textAnchor: 'Ahsan',
                        style: {
                            colors: ['black']
                        },
                        formatter: function (val, opt) {
                            return opt.w.globals.labels[opt.dataPointIndex] + ":  " + val
                        },
                        offsetX: 0,
                        dropShadow: {
                            enabled: true
                        }
                    },
                    stroke: {
                        width: 1,
                        colors: ['#fff']
                    },
                    xaxis: {
                        categories: [],
                    },
                    yaxis: {
                        labels: {
                            show: false
                        }
                    },
                    title: {
                        text: this.$t('AccountDashboard.Expense'),
                        align: 'center',
                        floating: true
                    },
                    subtitle: {
                        text: this.$t('AccountDashboard.CostCenterofExpenseAccount'),
                        align: 'center',
                    },
                    tooltip: {
                        theme: 'dark',
                        x: {
                            show: false
                        },
                        y: {
                            title: {
                                formatter: function () {
                                    return ''
                                }
                            }
                        }
                    }
                },
                accountOverViewSeries: [{
                    name: this.$t('AccountDashboard.Accounts'),
                    data: []
                }],
                accountOverViewchartOptions: {
                    chart: {
                        type: 'bar',
                        height: 380
                    },
                    plotOptions: {
                        bar: {
                            barHeight: '100%',
                            distributed: true,
                            horizontal: true,
                            dataLabels: {
                                position: 'bottom'
                            },
                        }
                    },
                    colors: ['#33b2df', '#546E7A', '#d4526e', '#13d8aa', '#A5978B', '#2b908f', '#f9a3a4', '#90ee7e',
                        '#f48024', '#69d2e7'
                    ],
                    dataLabels: {
                        enabled: true,
                        textAnchor: 'Ahsan',
                        style: {
                            colors: ['black']
                        },
                        formatter: function (val, opt) {
                            return opt.w.globals.labels[opt.dataPointIndex] + ":  " + val
                        },
                        offsetX: 0,
                        dropShadow: {
                            enabled: true
                        }
                    },
                    stroke: {
                        width: 1,
                        colors: ['#fff']
                    },
                    xaxis: {
                        categories: [this.$t('AccountDashboard.Cash'), this.$t('AccountDashboard.Bank') , this.$t('AccountDashboard.AccountReceivable'), this.$t('AccountDashboard.Account Payable')],
                    },
                    yaxis: {
                        labels: {
                            show: false
                        }
                    },
                    title: {
                        text: this.$t('AccountDashboard.AccountOverView'),
                        align: 'center',
                        floating: true
                    },
                    subtitle: {
                        text: this.$t('AccountDashboard.CostCenterofExpenseAccount'),
                        align: 'center',
                    },
                    tooltip: {
                        theme: 'dark',
                        x: {
                            show: false
                        },
                        y: {
                            title: {
                                formatter: function () {
                                    return ''
                                }
                            }
                        }
                    }
                },

                chartOptionsPaidInvoices: {
                    labels: [this.$t('AccountDashboard.VatPayable'), this.$t('AccountDashboard.VATReceivable')]
                },
                paidInvoicesSeries: [343, 34434],
                loading: false,
                seriesPayableAccount: [{
                    name: this.$t('AccountDashboard.Receivable'),
                    data: []
                }, {
                    name: this.$t('AccountDashboard.Payable'),
                    data: []
                }],
                chartOptionsPayableAccount: {
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
                userID: 0,
                employeeId: 0,
                isChartLoad: false,
                fromDate: moment().format("DD MMM YYYY"),
                toDate: Date(),
                series2: [{
                    name: this.$t('AccountDashboard.Income'),
                    data: []
                },
                    {
                        name: this.$t('AccountDashboard.Expense'),
                    data: []
                
                     }],
            chartOptions2: {
                chart: {
                    type: 'bar',
                        height: 350
                },
                plotOptions: {
                    bar: {
                        horizontal: false,
                            columnWidth: '55%',
                                endingShape: 'rounded'
                    },
                },
                dataLabels: {
                    enabled: false
                },
                stroke: {
                    show: true,
                        width: 2,
                            colors: ['transparent']
                },
                xaxis: {
                    categories: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'],
                    }
            },

            series1: [{
                data: [25, 66, 41, 89, 63, 25, 44, 12, 36, 9, 54]
                }],
                series3: [{
                    data: [54, 9, 36, 12, 44, 25, 63, 89, 41, 66,25 ]
            }],

                chartOptions1: {
                chart: {
                    type: 'line',
                        width: 100,
                            height: 35,
                                sparkline: {
                        enabled: true
                    }
                },
                tooltip: {
                    fixed: {
                        enabled: false
                    },
                    x: {
                        show: false
                    },
                    y: {
                        title: {
                            formatter: function () {
                                return ''
                            }
                        }
                    },
                    marker: {
                        show: false
                    }
                }
                },
                chartOptions3: {
                chart: {
                    type: 'line',
                        width: 100,
                            height: 35,
                                sparkline: {
                        enabled: true
                    }
                },
                tooltip: {
                    fixed: {
                        enabled: false
                    },
                    x: {
                        show: false
                    },
                    y: {
                        title: {
                            formatter: function () {
                                return ''
                            }
                        }
                    },
                    marker: {
                        show: false
                    }
                }
            },


        }
    },
    watch: {

    },
    methods: {

        OverViewFilterFunction: function (x) {
            this.loading = false;          
            this.overView = x;
            this.GetCashTransaction();
        },

        getDate: function (date) {
            return moment(date).format('l');
        },

        GetCashTransaction: function () {
            
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            
            this.$https.get('/Company/GetAccountTransaction?overViewFilter=' + this.overView, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                if (response.data != null) {
                    
                    var totalIncome = 0;
                    var totalExpense = 0;
                    root.loading = false;
                    root.expenseSeries[0].data = [];
                    root.accountOverViewSeries[0].data = [];
                    root.series2[0].data = [];
                    root.series = [];
                    root.vatSeries = [];
                    root.advanceSeries = [];
                    root.chartOptions.labels = [];
                    root.series2[1].data = [];
                    root.seriesPayableAccount[0].data = [];
                    root.seriesPayableAccount[1].data = [];
                    root.incomeList = [];
                    root.expenseList = [];
                    root.chartOptionsPayableAccount.xaxis.categories = [];
                    root.expensechartOptions.xaxis.categories = [];
                    root.banks = response.data.banks;
                    root.vatReceivable = response.data.vatReceivable;
                    root.vatPayable = response.data.vatPayable;
                    root.advancePayable = response.data.advancePayable;
                    root.accountPayable = response.data.accountPayable;
                    root.accountReceivable = response.data.accountReceivable;
                    root.advanceReceivable = response.data.advanceReceivable;
                    root.cash = response.data.cash;
                    root.incomeList = response.data.incomeList;
                    root.expenseList = response.data.expenseList;

                    root.accountOverViewSeries[0].data.push(root.cash);
                    root.accountOverViewSeries[0].data.push(root.banks);
                    root.accountOverViewSeries[0].data.push(root.accountReceivable);
                    root.accountOverViewSeries[0].data.push(root.accountPayable);
                    response.data.expenseList.forEach(function (result) {
                        root.expenseSeries[0].data.push(result.amount);
                        totalExpense = totalExpense + result.amount;
                        root.expensechartOptions.xaxis.categories.push(result.costCenter);
                    });
                    response.data.incomeList.forEach(function (result) {
                        totalIncome = totalIncome + result.amount;
                       
                    });
                    root.series.push(totalIncome);
                    root.series.push(totalExpense);
                    root.vatSeries.push(root.vatPayable);
                    root.vatSeries.push(root.vatReceivable);
                    if (root.advancePayable == 0 && root.advanceReceivable == 0) {
                        root.advanceSeries.push(100);
                    }
                    else {
                        root.advanceSeries.push(root.advancePayable);
                        root.advanceSeries.push(root.advanceReceivable);
                    }
                 
                    response.data.incomesAndExpenseList.forEach(function (result) {
                        root.series2[0].data.push((parseFloat(result.incomeAmount)).toFixed(0));
                        root.series2[1].data.push(parseFloat(result.expenseAmount).toFixed(0));
                    });

                    response.data.payableReceivableLookUpModel.forEach(function (result) {
                        root.seriesPayableAccount[0].data.push((parseFloat(result.receivableAmount)).toFixed(0));
                        root.seriesPayableAccount[1].data.push(parseFloat(result.payableAmount).toFixed(0));
                        root.chartOptionsPayableAccount.xaxis.categories.push(result.date);
                    });

                    root.randerChart++;


                }
            });
        },


    },
    created: function () {

        this.$emit('update:modelValue', this.$route.name);
    },
    mounted: function () {
        
        if (this.active == 'Account') {
            this.currency = localStorage.getItem('currency');

            this.fromDate = moment().startOf('month').format("DD MMM YYYY");

                this.userID = localStorage.getItem('UserID');
                this.employeeId = localStorage.getItem('EmployeeId');
                this.fromDate = moment().startOf('month').format("DD MMM YYYY");

            
            this.GetCashTransaction();

            this.rander++;
            this.randerDropdown++;
        }



    },
    }
</script>
<style scoped>
    .DayHeading {
        font-size: 21px !important
    }

    .DashboardFontSize {
        font-size: 18px;
        color: black;
        font-weight: bold;
    }

    .NumberSize {
        font-size: 25px;
        font-weight: bold;
        color: #3178F6;
    }
    .NumberSizeForVat {
        font-size: 22px;
        font-weight: bold;
        color: #3178F6;
    }
</style>