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
                    <a class="dropdown-item" v-on:click="OverViewFilterFunction('Today')" href="javascript:void(0);">Today </a>
                    <a class="dropdown-item" v-on:click="OverViewFilterFunction('3 Month')" href="javascript:void(0);">3 Month</a>
                    <a class="dropdown-item" v-on:click="OverViewFilterFunction('6 Month')" href="javascript:void(0);">6 Month</a>
                    <a class="dropdown-item" v-on:click="OverViewFilterFunction('Year')" href="javascript:void(0);">1 Year</a>

                </div>

            </div>
            <div class="col-lg-2 col-md-6 col-sm-6">
                <div class="card card-stats">
                    <div class="card-body ">
                        <div class="text-left ">
                            <span class="DashboardFontSize" style="padding-bottom:0px !important;font-size:16px !important">Total Employee</span><br />
                            <span class="NumberSize"> {{totalEmployee}}</span>

                        </div>
                    </div>

                </div>
                <div class="card card-stats">
                    <div class="card-body ">
                        <div class="text-left ">
                            <span class="DashboardFontSize" style="padding-bottom:0px !important;font-size:16px !important">Present Employee</span><br />
                            <span class="NumberSize"> {{totalPresentEmployee}}</span>

                        </div>
                    </div>

                </div>
                <div class="card card-stats" style="height:100px">
                    <div class="card-body ">
                        <div class="text-left ">
                            <span class="DashboardFontSize" style="padding-bottom:0px !important;font-size:16px !important">On Leave</span><br />
                            <span class="NumberSize"> {{employeeOnLeave}}</span>

                        </div>
                    </div>

                </div>
            </div>
            <div class="col-lg-2 col-md-6 col-sm-6">
                <div class="card card-stats">
                    <div class="card-body ">
                        <div class="text-left ">
                            <span class="DashboardFontSize" style="padding-bottom:0px !important;font-size:16px !important">Total Department</span><br />
                            <span class="NumberSize"> {{totalDepartment}}</span>

                        </div>
                    </div>

                </div>
                <div class="card card-stats">
                    <div class="card-body ">
                        <div class="text-left ">
                            <span class="DashboardFontSize" style="padding-bottom:0px !important;font-size:16px !important">Gross Salaries</span><br />
                            <span class="NumberSize"> {{(parseFloat(grossSalary)).toFixed(0).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</span>

                        </div>
                    </div>

                </div>
                <div class="card card-stats">
                    <div class="card-body ">
                        <div class="text-left ">
                            <span class="DashboardFontSize" style="padding-bottom:0px !important;font-size:16px !important">Remaining Loan Amount</span><br />
                            <span class="NumberSize"> {{(parseFloat(remainingLoan)).toFixed(0).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</span>

                        </div>
                    </div>

                </div>

            </div>

            <div class="col-lg-4 col-md-6 col-sm-12">
                <div class="card" style="height:350px !important">
                    <div class="card-header">
                        <span class="card-title DayHeading"> Over All Attendence  </span>

                        <div class="card-body mx-auto col-12" id="charts">

                            <apexchart type="donut" width="380" height="350" v-bind:key="randerChart" :options="chartOptions" :series="series"></apexchart>

                        </div>
                    </div>

                </div>


            </div>
            <div class="col-lg-4 col-md-6 col-sm-12">
                <div class="card" style="height:350px !important">
                    <div class="card-header">
                        <span class="card-title DayHeading"> Employee by Gender </span>

                        <div class="card-body mx-auto col-12" id="charts">

                            <apexchart type="pie" width="380" height="350" v-bind:key="randerChart" :options="genderOptions" :series="genderListSeries"></apexchart>
                        </div>
                    </div>

                </div>


            </div>

            <div class="col-lg-6 col-md-6 col-sm-12">
                <div class="card" style="height:350px !important">
                    <div class="card-header">
                        <span class="card-title DayHeading"> Gross Salary By Department </span>

                        <div class="card-body mx-auto col-12" id="charts">
                            <apexchart type="bar" height="250" v-bind:key="randerChart" :options="employeeDepartmentchartOptions" :series="seriesEmployeeByDepartment"></apexchart>
                        </div>
                    </div>

                </div>


            </div>
            <div class="col-lg-6 col-md-6 col-sm-12">
                <div class="card" style="height:350px !important">
                    <div class="card-header">
                        <span class="card-title DayHeading"> Employee Count by department </span>

                        <div class="card-body mx-auto col-12" id="charts">
                            <apexchart type="bar" height="250" :options="employeeCountchartOptions" :series="seriesCountEmployeeByDepartment" v-bind:key="randerChart" ></apexchart>
                        </div>
                    </div>

                </div>


            </div>
            <!--<div class="col-lg-6 col-md-6 col-sm-12">
                <div class="card" style="height:350px !important">
                    <div class="card-header">
                        <span class="card-title DayHeading"> Employee by Experience And Designation </span>

                        <div class="card-body mx-auto col-12" id="charts">
                            <apexchart type="bar" height="250" :options="employeeDepartmentchartOptions" :series="seriesEmployeeByDepartment"></apexchart>
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
                overView: 'Today',
                randerDropdown: 0,
                remainingLoan: 0,
                totalEmployee: 0,
                grossSalary: 0,
                totalDepartment: 0,
                randerChart: 0,
                totalAbsentEmployee: 0,
                totalPresentEmployee: 0,
                employeeOnLeave: 0,
                departmentWiseSalaryList: [],
                series: [],
                seriesEmployeeByDepartment: [{
                    name: 'Salary By Department',
                    data: []
                }],
                seriesCountEmployeeByDepartment: [{
                    name: 'Employee Wise  Department',
                    data: []
                }],
                employeeCountchartOptions: {
                    chart: {
                        height: 350,
                        type: 'bar',
                       
                    },
                    plotOptions: {
                        bar: {
                            columnWidth: '45%',
                            distributed: true,
                        }
                    },
                    dataLabels: {
                        enabled: false
                    },
                    legend: {
                        show: false
                    },
                    xaxis: {
                        categories: [],
                        labels: {
                            style: {
                                fontSize: '12px'
                            }
                        }
                    }
                },
                employeeDepartmentchartOptions: {
                    chart: {
                        type: 'bar',
                        height: 250
                    },
                    plotOptions: {
                        bar: {
                            borderRadius: 10,
                            dataLabels: {
                                position: 'top', // top, center, bottom
                            },
                        }
                    },
                    dataLabels: {
                        enabled: false
                    },
                    xaxis: {
                        categories: [],
                    }
                },


                genderListSeries: [],
                chartOptions: {
                    labels: ['Present', 'Absent','On Leave']
                },
                genderOptions: {
                    labels: ['Male', 'Female','Others']
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
            
            this.$https.get('/Company/GetEmployeeRecord?overViewFilter=' + this.overView, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                if (response.data != null) {
                    
                    root.series = [];
                    root.genderListSeries = [];
                    root.departmentWiseSalaryList = [];
                    root.seriesCountEmployeeByDepartment.data = [];
                    root.seriesEmployeeByDepartment.data = [];
                    root.employeeCountchartOptions.xaxis.categories = [];
                    root.employeeDepartmentchartOptions.xaxis.categories = [];
                    root.departmentWiseSalaryList = response.data.departmentWiseSalaryList;
                      response.data.departmentWiseSalaryList.forEach(function (result) {
                          root.seriesCountEmployeeByDepartment.data.push(parseInt(result.countEmployee));
                          root.seriesEmployeeByDepartment.data.push(parseInt(result.salary));
                          root.employeeCountchartOptions.xaxis.categories.push(result.name);
                          root.employeeDepartmentchartOptions.xaxis.categories.push(result.name);
                    });
                    root.totalDepartment = response.data.totalDepartment;
                    root.totalEmployee = response.data.totalEmployee;
                    root.grossSalary = response.data.grossSalary;
                    root.remainingLoan = response.data.remainingLoan;
                    root.totalPresentEmployee = response.data.totalPresentEmployee;
                    root.employeeOnLeave = response.data.employeeOnLeave;
                    root.totalAbsentEmployee = response.data.totalAbsentEmployee;
                    root.series.push(root.totalPresentEmployee);
                    root.series.push(root.totalAbsentEmployee);
                    root.series.push(root.employeeOnLeave);
                    root.genderListSeries = response.data.genderList;
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