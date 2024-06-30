<template>
    <div class="row" v-if="isValid('CanAddTodayAttendance')">

        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">{{ $t('EmployeeTodayAttendence.TodayAttendance') }} ( {{currentDate}})</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('EmployeeTodayAttendence.Home') }}</a></li>
                                    <li class="breadcrumb-item active">{{ $t('EmployeeTodayAttendence.TodayAttendance') }}</li>
                                </ol>
                            </div>
                            <div class="col-auto align-self-center">
                                <a v-if="isValid('CanCheckOutAllTodayAttendence')" v-on:click="SaveManualAttendenceRecord(employee,'checkOutAll')" href="javascript:void(0);"
                                   class="btn btn-sm btn-outline-primary mx-1">
                                    <i class="align-self-center icon-xs ti-plus"></i>
                                    {{ $t('EmployeeTodayAttendence.CheckOutAll') }}
                                </a>
                                <a v-if="isValid('CanViewAttendanceReport')" v-on:click="GotoPage('AttendanceReport')" href="javascript:void(0);"
                                   class="btn btn-sm btn-outline-primary mx-1">
                                    <i class="align-self-center icon-xs ti-plus"></i>
                                    {{ $t('EmployeeTodayAttendence.AttendanceReport') }}
                                </a>
                                <a v-on:click="GotoPage('/StartScreen')" href="javascript:void(0);"
                                    class="btn btn-sm btn-outline-danger">
                                    {{ $t('EmployeeRegistration.Close') }}
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="card">
               
                <div class="card-body">
                    <div class="table-responsive">
                        <table class="table mb-0">
                            <thead class="thead-light table-hover">
                                <tr>
                                    <th class="text-left"
                                        style="padding-top:1px !important;padding-bottom:1px !important;margin-top:0px !important;margin-bottom:0px !important;width:5%">
                                        #
                                    </th>
                                    <th class="text-center"
                                        style="padding-top:1px !important;padding-bottom:1px !important;margin-top:0px !important;margin-bottom:0px !important">
                                        {{ $t('EmployeeTodayAttendence.EmployeeName') }}
                                    </th>
                                    <th class="text-center"
                                        style="padding-top:2px !important;padding-bottom:2px !important;">
                                        {{ $t('EmployeeTodayAttendence.Department') }}
                                    </th>
                                    <th class="text-left"
                                        style="padding-top:2px !important;padding-bottom:2px !important;">{{ $t('EmployeeTodayAttendence.CheckIn') }}</th>
                                    <th class="text-left"
                                        style="padding-top:2px !important;padding-bottom:2px !important;">{{ $t('EmployeeTodayAttendence.OnLeave') }}</th>
                                    <th class="text-left"
                                        style="padding-top:2px !important;padding-bottom:2px !important;">{{ $t('EmployeeTodayAttendence.Absent') }}</th>
                                    <th class="text-left"
                                        style="padding-top:2px !important;padding-bottom:2px !important;">{{ $t('EmployeeTodayAttendence.CheckOut') }}</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr v-for="(employee,index) in employeelist" v-bind:key="employee.id">

                                    <td class="text-left" style="padding-top:2px !important;padding-bottom:2px !important;width:5%">
                                        {{index+1}}
                                    </td>
                                    <td class="text-center" style="padding-top:2px !important;padding-bottom:2px !important;">

                                        <strong>
                                            <a href="javascript:void(0)" v-on:click="EmployeeAttendence(employee.employeeId)">   {{employee.englishName}}</a>
                                        </strong>

                                    </td>
                                    <td class="text-center" style="padding-top:2px !important;padding-bottom:2px !important;">
                                        {{employee.departmentEng}}

                                    </td>
                                    <!--<td class="text-left" v-if="employee.isOnLeave" style="padding-top:2px !importa On Leavent;padding-bottom:2px !important;">
        <span style="color:red"> On Leave</span>

    </td>
    <td class="text-left" v-else-if="employee.isAbsent" style="padding-top:2px !important;padding-bottom:2px !important;">
        <span style="color:green">Absent</span>

    </td>-->
                                    <td class="text-left" v-if="employee.isOnLeave" style="padding-top:2px !important;padding-bottom:2px !important;">
                                        <span style="color:red"> {{ $t('EmployeeTodayAttendence.OnLeave') }}</span>

                                    </td>
                                    <td class="text-left" v-else-if="employee.isAbsent" style="padding-top:2px !important;padding-bottom:2px !important;">
                                        <span style="color:green">{{ $t('EmployeeTodayAttendence.Absent') }}</span>

                                    </td>
                                    <td class="text-left" style="padding-top:2px !important;padding-bottom:2px !important;" v-else>

                                        <input type="checkbox" @update:modelValue="SaveManualAttendenceRecord(employee,'checkIn')" v-model="employee.isCheckIn" value="" />
                                        <span style="font-size:10px;"> &nbsp;&nbsp; {{convertDate(employee.checkIn)}}</span>


                                    </td>
                                    <td class="text-left" style="padding-top:2px !important;padding-bottom:2px !important;" v-if="employee.isOnLeave">
                                        <span style="color:red"> {{ $t('EmployeeTodayAttendence.OnLeave') }}</span>

                                    </td>
                                    <td class="text-left" style="padding-top:2px !important;padding-bottom:2px !important;" v-else>
                                        <input type="checkbox" @update:modelValue="SaveManualAttendenceRecord(employee,'onLeave')" v-model="employee.isOnLeave" value="" />

                                    </td>
                                    <td class="text-left" style="padding-top:2px !important;padding-bottom:2px !important;" v-if="employee.isAbsent">
                                        <span style="color:green">{{ $t('EmployeeTodayAttendence.Absent') }}</span>
                                    </td>
                                    <td class="text-left" style="padding-top:2px !important;padding-bottom:2px !important;" v-else>
                                        <input type="checkbox" @update:modelValue="SaveManualAttendenceRecord(employee,'absent')" v-model="employee.isAbsent" value="" />
                                    </td>




                                    <td class="text-left" v-if="employee.isOnLeave" style="padding-top:2px !important;padding-bottom:2px !important;">
                                        <span style="color:red"> {{ $t('EmployeeTodayAttendence.OnLeave') }}</span>

                                    </td>
                                    <td class="text-left" v-else-if="employee.isAbsent" style="padding-top:2px !important;padding-bottom:2px !important;">
                                        <span style="color:green">{{ $t('EmployeeTodayAttendence.Absent') }}</span>

                                    </td>
                                    <td class="text-left" v-else style="padding-top:2px !important;padding-bottom:2px !important;" height="10">

                                        <input type="checkbox" @update:modelValue="SaveManualAttendenceRecord(employee,'checkOut')" v-model="employee.isCheckOut" value="" />
                                        <span style="font-size:10px;"> &nbsp;&nbsp; {{convertDate(employee.checkOut)}}</span>

                                    </td>

                                </tr>

                            </tbody>
                        </table>
                    </div>

                </div>
            </div>

            <TodayAttendenceModel :manualattendence="manualAttendence" :show="show" v-if="show" @close="IsSave"
                :type="type" />
                <loading v-model:active="loading" :can-cancel="true" :is-full-page="true"></loading>
        </div>

    </div>
    <div v-else>
        <acessdenied></acessdenied>
    </div>
</template>

<script>
import moment from 'moment';
//import Checkbox from 'vue-material-checkbox'
import clickMixin from '@/Mixins/clickMixin'
import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
export default {
    components: {
            Loading
        },
    mixins: [clickMixin],
    data: function () {
        return {
            month: '',
            value: false,
            checkbox: false,
            show: false,
            currentDate: '',
            currentMonth: '',
            nextMonth: '',
            year: '',
            arabic: '',
            english: '',
            searchQuery: '',
            employeeCheckIn: [],
            employeelist: [],
            daysList: [],
            search: '',
            manualAttendence: {
                id: '00000000-0000-0000-0000-000000000000',
                checkIn: '',
                isOnLeave: false,
                isAbsent: false,
                isCheckIn: false,
                isCheckOut: false,
                isPreviousAttendence: false,
                checkOut: '',
                employeeId: '',
                checkType: '',
                description: '',
            },
        }
    },


    methods: {
        GotoPage: function (link) {
                this.$router.push({path: link});
            },
        IsSave: function () {
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            root.employeelist = [];
            root.$https.get('Payroll/EmployeeTodayAttendence', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                if (response.data != null) {
                    root.employeelist = response.data;
                    root.show = false;
                }
            });
        },
        EmployeeAttendence: function (id) {
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            root.$https.get('/Payroll/EmployeeOverTimeQuery?Id=' + id, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                if (response.data != null) {
                    root.$store.GetEditObj(response.data);
                    root.$router.push({
                        path: '/EmployeeHourOfAttendence',
                        query: {
                            data: '',
                            Add: false
                        }
                    })
                }
            });
        },
        SaveManualAttendenceRecord: function (employee, checkType) {


            
            
            {
                this.loading = true;
                   
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                if (checkType == 'checkIn') {
                    this.manualAttendence.id = employee.attendenceId;
                    this.manualAttendence.employeeId = employee.employeeId;
                    this.manualAttendence.checkType = checkType;
                    this.manualAttendence.isCheckIn = true;
                    this.manualAttendence.isOnLeave = false;
                    this.manualAttendence.isAbsent = false;
                }
               else if (checkType == 'onLeave') {
                    this.manualAttendence.id = employee.attendenceId;
                    this.manualAttendence.employeeId = employee.employeeId;
                    this.manualAttendence.checkType = checkType;
                    this.manualAttendence.isOnLeave = true;
                    this.manualAttendence.isCheckIn = false;
                    this.manualAttendence.isAbsent = false;
                }
               else if (checkType == 'absent') {
                    this.manualAttendence.id = employee.attendenceId;
                    this.manualAttendence.employeeId = employee.employeeId;
                    this.manualAttendence.checkType = checkType;
                    this.manualAttendence.isAbsent = true;
                    this.manualAttendence.isOnLeave = false;
                    this.manualAttendence.isCheckIn = false;
                }
               else if (checkType == 'checkOutAll') {
                    this.manualAttendence.checkType = checkType;
                    this.manualAttendence.id = '6bbfc474-ef95-4746-5b8f-08dab0080051';
                }
                else {
                    this.manualAttendence.id = employee.attendenceId;
                    this.manualAttendence.employeeId = employee.employeeId;
                    this.manualAttendence.checkType = checkType;
                    this.manualAttendence.isCheckIn = true;
                    this.manualAttendence.isCheckOut = true;
                    this.manualAttendence.isOnLeave = false;
                    this.manualAttendence.isAbsent = false;
                }

                this.$https
                    .post('/Payroll/SaveManualAttendence', this.manualAttendence, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(response => {
                        root.loading = false
                        root.info = response.data.bpi

                        root.$swal.fire({
                            icon: 'success',
                            title: 'Saved Successfully',
                            showConfirmButton: false,

                            timer: 800,
                            timerProgressBar: true,

                        });
                        root.GetManualRecord();
                    })
                    .catch(error => {

                        console.log(error)
                        root.$swal.fire(
                            {
                                icon: 'error',
                                title: error.response.data,
                                text: 'You Enter Wrong Steps',
                            });

                        root.loading = false;
                        root.GetManualRecord();
                    })
                    .finally(() => root.loading = false)
            }



        },
        convertDate: function (date) {
            if (date == null)
                return '';
            return moment(date).format('DD MMM YYYY hh:mm A');
        },

        GetManualRecord: function () {
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            root.$https.get('Payroll/EmployeeTodayAttendence', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                if (response.data != null) {
                    root.employeelist = response.data;
                }
            });
        },

    },
    created: function () {
        this.$emit('update:modelValue', this.$route.name);
        this.GetManualRecord();
        //this.GetEmployeeData(this.search);


    },
    mounted: function () {
        this.english = localStorage.getItem('English');
        this.arabic = localStorage.getItem('Arabic');
        this.currentDate = moment().format('ddd, DD-MMM-YYYY');
    }
}
</script>
