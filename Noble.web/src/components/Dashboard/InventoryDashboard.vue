<template>
    <div class="col-12 mt-2">
        <div class="row ">
            <div class="col-lg-6 col-md-6 col-sm-6 pb-2">
                <span class="card-title "> {{ $t('InventoryDashboard.Overview') }}</span>

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
                                <a class="dropdown-item" v-on:click="OverViewFilterFunction('Monthly')" href="javascript:void(0);">{{ $t('InventoryDashboard.Monthly') }} </a>
                                <a class="dropdown-item" v-on:click="OverViewFilterFunction('3 Month')" href="javascript:void(0);">{{ $t('InventoryDashboard.ThreeMonth') }}</a>
                                <a class="dropdown-item" v-on:click="OverViewFilterFunction('6 Month')" href="javascript:void(0);">{{ $t('InventoryDashboard.HalfYear') }}</a>
                                <a class="dropdown-item" v-on:click="OverViewFilterFunction('Year')" href="javascript:void(0);">{{ $t('InventoryDashboard.Year') }}</a>
                            </div>
                        </div>
                    </div>
                </div>


            </div>
            <div class="col-lg-6 col-md-12 col-sm-12">
                <div class="card">
                    <div class="card-header">
                        <div class="row align-items-center">
                            <div class="col">
                                <h4 class="card-title">{{ $t('InventoryDashboard.TrendingFiveProductByMonth') }}</h4>
                            </div><!--end col-->

                        </div>  <!--end row-->
                    </div><!--end card-header-->
                    <div class="card-body">
                        <div class="">
                            <apexchart type="bar" height="400" :options="chartOptions2" :series="series2" v-bind:key="randerChart2"></apexchart>
                        </div>
                    </div><!--end card-body-->
                </div><!--end card-->
            </div>
            <div class="col-lg-6 col-md-12 col-sm-12">
                <div class="card">
                    <div class="card-header">
                        <div class="row align-items-center">
                            <div class="col">
                                <h4 class="card-title">{{ $t('InventoryDashboard.TrendingFiveProductCategoryWiseByMonth') }}</h4>
                            </div><!--end col-->

                        </div>  <!--end row-->
                    </div><!--end card-header-->
                    <div class="card-body">
                        <div class="">
                            <apexchart type="bar" height="400" :options="chartOptions3" :series="series3" v-bind:key="randerChart"></apexchart>
                        </div>
                    </div><!--end card-body-->
                </div><!--end card-->
            </div>
            <div class="col-lg-6 col-md-12 col-sm-12">
                <div class="card">
                    <div class="card-header">
                        <div class="row align-items-center">
                            <div class="col">
                                <h4 class="card-title">{{ $t('InventoryDashboard.WorstFiveProduct') }}</h4>
                            </div><!--end col-->

                        </div>  <!--end row-->
                    </div><!--end card-header-->
                    <div class="card-body row">
                        <div class=" col-6" v-for="inv in wrostInventoires" v-bind:key="inv.productName">
                            <span class="text-danger">* </span>  {{inv.displayName }},
                        </div>
                    </div><!--end card-body-->
                </div><!--end card-->
            </div>
            <div class="col-lg-6 col-md-12 col-sm-12">
                <div class="card">
                    <div class="card-header">
                        <div class="row align-items-center">
                            <div class="col">
                                <h4 class="card-title">{{ $t('InventoryDashboard.WorstFiveProductCategoryWise') }}</h4>
                            </div><!--end col-->

                        </div>  <!--end row-->
                    </div><!--end card-header-->
                    <div class="card-body row">
                        <div class=" col-6" v-for="inv in wrostCategoryInventoires" v-bind:key="inv.categoryName">

                            <span class="text-danger">* </span>  {{inv.displayName}} ,

                        </div>
                    </div><!--end card-body-->
                </div><!--end card-->
            </div>





        </div>
    </div>
</template>
<script>

    import clickMixin from '@/Mixins/clickMixin'
    import moment from "moment";
    export default {
        props: ["active"],
        name: 'InventoryDashboard',
     
        mixins: [clickMixin],

        data: function () {

            return {
                overView: 'Year',
                currency: '',
                wrostInventoires: [],
                wrostCategoryInventoires: [],
                randerChart2: 0,
                randerChart: 0,
                randerDropdown: 0,
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
               
                userID: 0,
                employeeId: 0,
                isChartLoad: false,
                //Line Chart 2
                series2: [],
                series3: [],
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
                        categories: [],
                    }
                },
                chartOptions3: {
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
                        categories: [],
                    }
                },

            }
        },
        watch: {

        },
        methods: {
           
            OverViewFilterFunction: function (x) {
                this.loading = true;
                
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
                
                this.$https.get('/Company/InventoryDashboardQuery?overViewFilter=' + this.overView, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                    if (response.data != null) {
                        
                        root.loading = false;
                        root.series2 = [];
                        root.series3 = [];
                        root.wrostInventoires = [];
                        root.wrostCategoryInventoires = [];
                        root.chartOptions2.xaxis.categories = [];
                        root.chartOptions3.xaxis.categories = [];
                        root.banks = response.data.banks;
                        root.wrostInventoires = response.data.wrostInventoires;
                        root.wrostCategoryInventoires = response.data.wrostCategoryInventoires;
                        
                         {
                            root.chartOptions2.xaxis.categories = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
                            response.data.trendingProducts.forEach(function (item) {
                                root.series2.push({
                                    name: item.name,
                                    data: [item.janSale, item.febSale, item.marSale, item.aprSale, item.maySale, item.junSale, item.julSale, item.augSale, item.sepSale, item.octSale, item.novSale, item.decSale]
                                })
                            });
                            root.chartOptions3.xaxis.categories = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
                            response.data.trendingCategoryProducts.forEach(function (item) {
                                root.series3.push({
                                    name: item.categoryName,
                                    data: [item.janSale, item.febSale, item.marSale, item.aprSale, item.maySale, item.junSale, item.julSale, item.augSale, item.sepSale, item.octSale, item.novSale, item.decSale]
                                })
                            });

                        }
                      
                        root.randerChart++;
                        root.randerChart2++;
                       

                    }
                });
            },


        },
        created: function () {

            this.$emit('update:modelValue', this.$route.name);
        },
        mounted: function () {
            
            if (this.active == 'Inventory') {
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
</style>