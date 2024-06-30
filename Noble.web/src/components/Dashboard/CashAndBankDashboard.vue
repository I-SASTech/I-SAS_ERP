<template>
    <div v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">
        <div class="row">
            <div class="col-lg-6 col-md-6 col-sm-6 pb-2">
                <span class="card-title DayHeading"> Overview</span>

            </div>
            <div class="col-lg-6 col-md-6 col-sm-6 pb-2 text-right" v-bind:key="randerDropdown">

                <button class="dropdown-toggle btn-md btn-round   " style="background-color:transparent" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                    {{overView}}
                </button>
                <div class="dropdown-menu dropdown-menu-right" aria-labelledby="dropdownMenuButton">
                    <a class="dropdown-item" v-on:click="OverViewFilterFunction('Monthly')" href="javascript:void(0);">Monthly </a>
                    <a class="dropdown-item" v-on:click="OverViewFilterFunction('3 Month')" href="javascript:void(0);">3 Month</a>
                    <a class="dropdown-item" v-on:click="OverViewFilterFunction('6 Month')" href="javascript:void(0);">6 Month</a>
                    <a class="dropdown-item" v-on:click="OverViewFilterFunction('Year')" href="javascript:void(0);">1 Year</a>

                </div>

            </div>

            <div class="col-lg-6 col-md-6 col-sm-6">
                <div class="card">
                    <div class="card-header">
                        <span class="card-title DayHeading">Account Receivable and Payable </span>

                        <div class="card-body mx-auto col-12" id="charts">

                            <!--<apexchart type="area" height="350" v-bind:key="randerChart" :options="chartOptionsPayableAccount" :series="seriesPayableAccount"></apexchart>-->

                        </div>
                    </div>

                </div>


            </div>
            <div class="col-lg-3 col-md-6 col-sm-6">
                <div class="card">
                    <div class="card-header">
                        <span class="card-title DayHeading"> Income VS Expense  </span>

                        <div class="card-body mx-auto col-12" id="charts">

                            <!--<apexchart type="donut" width="380" height="350" :options="chartOptions" :series="series"></apexchart>-->

                        </div>
                    </div>

                </div>


            </div>
            <div class="col-lg-6 col-md-6 col-sm-6">
                <div class="card">
                    <div class="card-header">
                        <span class="card-title DayHeading">Income Vs Expense  </span>

                        <div class="card-body mx-auto col-12" id="charts">

                            <!--<apexchart type="bar" height="350" :options="chartOptions2" :series="series2" v-bind:key="randerChart"></apexchart>-->

                        </div>
                    </div>

                </div>


            </div>
            <div class="col-lg-6 col-md-6 col-sm-6">
                <div class="card">
                    <div class="card-header">
                        <span class="card-title DayHeading"> Expense  </span>

                        <div class="card-body mx-auto col-12" id="charts">

                            <!--<apexchart type="bar" height="350" :options="expensechartOptions" :series="expenseSeries" v-bind:key="randerChart"></apexchart>-->

                        </div>
                    </div>

                </div>


            </div>





        </div>
        <loading v-model:active="loading" :can-cancel="false" :is-full-page="true"></loading>

    </div>
</template>
<script>

    import clickMixin from '@/Mixins/clickMixin'
    import moment from "moment";
     import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    
    export default {
        props: ["active"],
        name: 'AccountDashboard',
       components: {
            Loading
        },
        mixins: [clickMixin],
        data: function () {

            return {
                  loading: false,
                overView: 'Year',
                currency: '',
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
                
                this.$https.get('/Company/CashAndBankDashboardQuery?overViewFilter=' + this.overView, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                    if (response.data != null) {
                        
                        root.loading = false;


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

                if (this.$store.isAuthenticated) {
                    this.userID = localStorage.getItem('UserID');
                    this.employeeId = localStorage.getItem('EmployeeId');
                    this.fromDate = moment().startOf('month').format("DD MMM YYYY");

                }
                this.GetCashTransaction();

                this.rander++;
                this.randerDropdown++;
            }



        },
    }
</script>
<style scoped>

    .DashboardFontSize {
        font-size: 20px;
        color: black;
        font-weight: bold;
    }

    .NumberSize {
        font-size: 30px;
        font-weight: bold;
        color: #3178F6;
    }

    .NumberSizeForVat {
        font-size: 22px;
        font-weight: bold;
        color: #3178F6;
    }
</style>