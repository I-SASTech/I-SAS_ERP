<template>
    <div class="row">

        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">{{ $t('EmployeeHourOfAttendence.EmployeeAttendanceDetail') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('EmployeeHourOfAttendence.Home') }}</a></li>
                                    <li class="breadcrumb-item active">{{ $t('EmployeeHourOfAttendence.EmployeeAttendanceDetail') }}</li>
                                </ol>
                            </div>
                            <div class="col-auto align-self-center">


                                <a v-on:click="GotoPage('/EmployeeTodayAttendence')" href="javascript:void(0);"
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
                                    <th class="text-center">
                                        #
                                    </th>
                                    <th class="text-center">
                                        {{ $t('EmployeeHourOfAttendence.EmployeeName') }}
                                    </th>



                                    <th class="text-center">
                                        {{ $t('EmployeeHourOfAttendence.CheckIn') }}
                                    </th>
                                    <th class="text-center">
                                        {{ $t('EmployeeHourOfAttendence.CheckOut') }}
                                    </th>
                                    <th class="text-center">
                                        {{ $t('EmployeeHourOfAttendence.TotalHour') }}
                                    </th>

                                    <th class="text-center">
                                        {{ $t('EmployeeHourOfAttendence.OverTime') }}
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <template v-for="(employee,index) in employeelist">
                                    <tr v-if="employee.checkType=='On Leave'" v-bind:key="employee.id">
                                        <td class="text-center" style="background-color:azure">
                                            {{index+1}}
                                        </td>
                                        <td class="text-center" style="font-size:15px;font-weight:bold;background-color:azure">
                                            {{employee.employeeName}}
                                        </td>
                                        <td class="text-center" colspan="3" style="font-size:15px;font-weight:bold;background-color:azure"> On Leave</td>
                                        <td class="text-center" style="font-size:15px;font-weight:bold;background-color:azure">
                                            100 %
                                        </td>
                                    </tr>
                                    <tr v-else-if="employee.isAbsent" v-bind:key="employee.id">
                                        <td class="text-center" style="background-color:azure">
                                            {{index+1}}
                                        </td>
                                        <td class="text-center" style="font-size:15px;font-weight:bold;background-color:azure">
                                            {{employee.employeeName}}
                                        </td>
                                        <td class="text-center" colspan="3" style="font-size:15px;font-weight:bold;background-color:azure">Absent</td>
                                        <td class="text-center" style="font-size:15px;font-weight:bold;background-color:azure">
                                            0 %
                                        </td>
                                    </tr>
                                    <tr v-else v-bind:key="employee.id">
                                        <td class="text-center">
                                            {{index+1}}
                                        </td>

                                        <td class="text-center">
                                            {{employee.employeeName}}

                                        </td>

                                        <td class="text-center">{{convertDate(employee.checkIn)}}</td>
                                        <td class="text-center">{{convertDate(employee.checkOut)}}</td>
                                        <td class="text-center">{{SubtractDateTime(employee.checkIn,employee.checkOut)}}</td>
                                        <td class="text-center">{{OverTime(employee.companyTimeIn,employee.companyTimeOut,employee.checkIn,employee.checkOut)}}</td>



                                    </tr>
                                </template>
                                <tr>
                                    <td colspan="5" class="text-right">
                                        {{ $t('EmployeeHourOfAttendence.TotalWorking') }} %
                                    </td>
                                    <td class="text-right">
                                        {{tWorkingHour}} %
                                    </td>
                                </tr>

                            </tbody>
                        </table>
                    </div>


                </div>
            </div>

            <currencymodel :currency="newCurrency"
                           :show="show"
                           v-if="show"
                           @close="show = false"
                           :type="type" />
        </div>

    </div>

    <!-- <div class="row" v-bind:class="$i18n.locale == 'en' ? 'text-left' : 'arabicLanguage'">
        <div class="col-md-8 ml-auto mr-auto" v-bind:class="$i18n.locale == 'en' ? '' : 'arabicLanguage'">
            <div class="row mb-4" v-bind:class="$i18n.locale == 'en' ? 'text-left' : 'arabicLanguage'">
                <div class="col-sm-6 col-md-6 col-lg-6">
                    <h5 class="page_title">Employee Attendance Detail</h5>

                </div>
                <div class=" col-sm-6 col-md-6 col-lg-6">
                    <div v-bind:class="$i18n.locale == 'en' ? 'text-right' : 'text-left'">
                        <router-link :to="'/EmployeeTodayAttendence'"><a href="javascript:void(0)" class="btn btn-outline-danger "> {{ $t('EmployeeRegistration.Close') }}</a></router-link>
                    </div>

                </div>

            </div>
            <div class="card">
                <div class="card-body">
                    <div class="col-lg-12">
                        <div class="mt-2">
                            <div class=" table-responsive">
                                <table class="table table-striped table-hover table_list_bg" v-bind:class="$i18n.locale == 'en' ? 'text-left' : 'arabicLanguage'">
                                    <thead class="m-0">
                                        <tr>
                                            <th class="text-center">
                                                #
                                            </th>
                                            <th class="text-center">
                                                Employee Name
                                            </th>



                                            <th class="text-center">
                                                Check In
                                            </th>
                                            <th class="text-center">
                                                Check Out
                                            </th>
                                            <th class="text-center">
                                                Total Hour
                                            </th>

                                            <th class="text-center">
                                                Over Time
                                            </th>

                                        </tr>
                                    </thead>
                                    <template v-for="(employee,index) in employeelist">
                                        <tr v-if="employee.checkType=='On Leave'" v-bind:key="employee.id">
                                            <td class="text-center" style="background-color:azure">
                                                {{index+1}}
                                            </td>
                                            <td class="text-center" style="font-size:15px;font-weight:bold;background-color:azure">
                                                {{employee.employeeName}}
                                            </td>
                                            <td class="text-center" colspan="3" style="font-size:15px;font-weight:bold;background-color:azure"> On Leave</td>
                                            <td class="text-center" style="font-size:15px;font-weight:bold;background-color:azure">
                                                100 %
                                            </td>
                                        </tr>
                                        <tr v-else-if="employee.isAbsent" v-bind:key="employee.id">
                                            <td class="text-center" style="background-color:azure">
                                                {{index+1}}
                                            </td>
                                            <td class="text-center" style="font-size:15px;font-weight:bold;background-color:azure">
                                                {{employee.employeeName}}
                                            </td>
                                            <td class="text-center" colspan="3" style="font-size:15px;font-weight:bold;background-color:azure">Absent</td>
                                            <td class="text-center" style="font-size:15px;font-weight:bold;background-color:azure">
                                                0 %
                                            </td>
                                        </tr>
                                        <tr v-else v-bind:key="employee.id">
                                            <td class="text-center">
                                                {{index+1}}
                                            </td>

                                            <td class="text-center">
                                                {{employee.employeeName}}

                                            </td>

                                            <td class="text-center">{{convertDate(employee.checkIn)}}</td>
                                            <td class="text-center">{{convertDate(employee.checkOut)}}</td>
                                            <td class="text-center">{{SubtractDateTime(employee.checkIn,employee.checkOut)}}</td>
                                            <td class="text-center">{{OverTime(employee.companyTimeIn,employee.companyTimeOut,employee.checkIn,employee.checkOut)}}</td>



                                        </tr>
                                    </template>
                                        <tr>
                                            <td colspan="5" class="text-right">
                                                Total Working %
                                            </td>
                                            <td class="text-right">
                                                {{tWorkingHour}} %
                                            </td>
                                        </tr>

                                </table>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>
        <currencymodel :newcurrency="newCurrency"
                       :show="show"
                       v-if="show"
                       @close="show = false"
                       :type="type" />
    </div> -->
</template>

<script>
import clickMixin from '@/Mixins/clickMixin'
import moment from 'moment'

export default {
    mixins: [clickMixin],
    data: function () {
        return {
            arabic: '',
            english: '',
            searchQuery: '',
            employeelist: [],
            search: '',
            newCurrency: {
                id: '00000000-0000-0000-0000-000000000000',
                name: '',
                nameArabic: '',
                sign: '',
                arabicSign: '',
                image: '',
                isActive: true,
                isDisable: false,
            },
        }
    },
    computed: {
        tWorkingHour: function () {

            var totalHour = this.employeelist.reduce(function (a, c) { return a + Number((c.totalHour) || 0) }, 0);
            //var totalMinute = parseInt(this.employeelist.reduce(function (a, c) { return a + Number((c.totalMinute) || 0) }, 0)/60);
            //var officeMinute = parseInt(this.employeelist.reduce(function (a, c) { return a + Number((c.officeMinute) || 0) }, 0) / 60);
            var totalWorkingHour = this.employeelist.reduce(function (a, c) { return a + Number((c.officeHour) || 0) }, 0);
            var total = ((totalHour) / (totalWorkingHour)) * 100;
            if (totalHour == 0) {
                return '0';
            }
            return total.toFixed(3).slice(0, -1);
        },


    },
    methods: {
        GotoPage: function (link) {
                this.$router.push({path: link});
            },
        EditEmployeeAttendence: function (id) {

            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            root.$https.get('/Payroll/EmployeeDetailQuery?Id=' + id, { headers: { "Authorization": `Bearer ${token}` } })
                .then(function (response) {
                    if (response.data) {
                        //root.newCurrency.id = response.data.id;
                        //root.newCurrency.name = response.data.name;
                        //root.newCurrency.nameArabic = response.data.nameArabic;
                        //root.newCurrency.sign = response.data.sign;
                        //root.newCurrency.arabicSign = response.data.arabicSign;
                        //root.newCurrency.image = response.data.image;
                        //root.newCurrency.isActive = response.data.isActive;
                        //root.show = !root.show;
                        //root.type = "Edit"
                    } else {
                        console.log("error: something wrong from db.");
                    }
                },
                    function (error) {
                        root.loading = false;
                        console.log(error);
                    });

        },
        convertDate: function (date) {
            if (date == undefined || date == null) {
                return "";
            }
            return moment(date).format('ddd DD-MMM-YYYY, hh:mm A');
        },
        OverTime: function (companyTimeIn, companyTimeOut, checkIn, checkOut) {

            if (companyTimeOut == undefined || companyTimeOut == null || companyTimeIn == undefined || companyTimeIn == null) {
                return "";
            }
            if (checkIn == undefined || checkIn == null || checkOut == undefined || checkOut == null) {
                return "";
            }
            //Company Time
            const startTimeCompany = moment(companyTimeIn);
            const endTimeCompany = moment(companyTimeOut);
            const durationOfCompany = moment.duration(endTimeCompany.diff(startTimeCompany));
            const hoursCompany = parseInt(durationOfCompany.asHours());
            const minutesCompany = parseInt(durationOfCompany.asMinutes()) % 60;

            //EmployeeTime
            const startTime = moment(checkIn);
            const endTime = moment(checkOut);
            const duration = moment.duration(endTime.diff(startTime));
            const hours = parseInt(duration.asHours());
            const minutes = parseInt(duration.asMinutes()) % 60;

            if (hoursCompany > hours) {
                return '--';
            }
            else {
                const hh = parseInt(hours) - parseInt(hoursCompany);
                const mm = parseInt(minutes) - parseInt(minutesCompany);
                return hh + ':' + mm;
            }




        },
        SubtractDateTime: function (x, y) {

            if (x == undefined || x == null) {
                return "";
            }
            if (y == undefined || y == null) {
                return "";
            }

            const startTime = moment(x);
            const endTime = moment(y);
            const duration = moment.duration(endTime.diff(startTime));
            const hours = parseInt(duration.asHours());
            const minutes = parseInt(duration.asMinutes()) % 60;

            return (hours + ':' + minutes)


        },

    },
    created: function () {

        this.$emit('update:modelValue', this.$route.name);
        if (this.$route.query.data != undefined) {

            this.employeelist = this.$route.query.data;

        }

    },
    mounted: function () {
        this.english = localStorage.getItem('English');
        this.arabic = localStorage.getItem('Arabic');
    }
}
</script>
