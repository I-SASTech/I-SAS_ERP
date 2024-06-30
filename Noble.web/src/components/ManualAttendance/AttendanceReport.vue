<template>
    <div class="row" v-if="isValid('CanViewAttendanceReport')">
        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">

                                <h4 class="page-title">{{ $t('AttendanceReport.AttendanceReport') }}</h4>
                            </div>
                            <div class="col-auto align-self-center">
                                <a v-on:click="PrintDetails(false)" href="javascript:void(0);"
                                   class="btn btn-sm btn-outline-primary mx-1">
                                    <i class=" fas fa-print  font-16"></i>
                                    {{ $t('AttendanceReport.Print') }}
                                </a>
                                <a v-on:click="PrintCsv" href="javascript:void(0);"
                                   class="btn btn-sm btn-outline-primary mx-1">
                                    <i class=" fas fa-file-excel  font-16"></i>
                                    {{ $t('AttendanceReport.ExportCVS') }}
                                </a>
                                <a v-on:click="GotoPage('/EmployeeTodayAttendence')" href="javascript:void(0);"
                                    class="btn btn-sm btn-outline-danger">
                                    {{ $t('AddSalaryTemplate.Close') }}
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <hr class="hr-dashed hr-menu mt-0" />

            <div class="row mb-5">
                <div class="col-lg-6">

                    <div class="row form-group">
                        <label class="col-form-label col-lg-4">
                            <span id="ember695" class="tooltip-container text-dashed-underline ">
                                {{ $t('AttendanceReport.FromDate') }}  <span class="text-danger">*</span>
                            </span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <datepicker v-model="fromDate" :key="render" />
                        </div>
                    </div>


                    <div class="row form-group">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline ">{{ $t('AttendanceReport.Employee') }} </span><span
                                class="text-danger"> *</span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <employeeDropdown v-bind:key="randerButton" @update:modelValue="GetEmployeeWiseRecord(employeeId)"
                                v-model="employeeId" :isMultiple="true" />
                        </div>
                    </div>
                    <div class="row form-group text-end">
                        <span> <a href="javascript:void(0)" class="btn btn-danger btn-sm btn-icon "
                                v-on:click="CloseAll()"><i class=" far fa-trash-alt "></i> {{$t('AttendanceReport.Remove')}}</a></span>

                    </div>

                </div>
                <div class="col-lg-6">

                    <div class="row form-group">
                        <label class="col-form-label col-lg-4">
                            <span id="ember695" class="tooltip-container text-dashed-underline ">
                                {{ $t('AttendanceReport.ToDate') }} <span class="text-danger">*</span>
                            </span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <datepicker v-model="toDate" :key="render" />
                        </div>
                    </div>



                </div>

                <hr class="hr-dashed hr-menu mt-0" />

                <div class="row">
                    <div class="col-lg-12">
                        <div class=" table-responsive mt-3">
                            <table class="table mb-0" style="table-layout:fixed;">
                                <thead class="thead-light">
                                    <tr>
                                        <th class="text-center">
                                            {{ $t('AttendanceReport.EmployeeName') }}
                                        </th>
                                        <th class="text-center">
                                            {{ $t('AttendanceReport.Date') }}
                                        </th>

                                        <th class="text-center">
                                            {{ $t('AttendanceReport.CheckIn') }}
                                        </th>
                                        <th class="text-center">
                                            {{ $t('AttendanceReport.CheckOut') }}
                                        </th>
                                        <th class="text-center">
                                            {{ $t('AttendanceReport.TotalHour') }}
                                        </th>
                                        <th class="text-center">
                                            {{ $t('AttendanceReport.Working') }} %
                                        </th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <template v-for="employee in employeelist">

                                        <tr v-if="employee.checkType == 'Week Holiday'" v-bind:key="employee.id">

                                            <td class="text-center"
                                                style="font-size:15px;font-weight:bold;background-color:azure"></td>
                                            <td class="text-center" colspan="4"
                                                style="font-size:15px;font-weight:bold;background-color:azure">
                                                {{$t('AttendanceReport.WeekHoliday')}} ({{ employee.description }})
                                            </td>
                                            <td class="text-center"
                                                style="font-size:15px;font-weight:bold;background-color:azure">
                                                100 %
                                            </td>
                                        </tr>
                                        <tr v-else-if="employee.checkType == 'Guested Holiday'" v-bind:key="employee.id">
                                            <td class="text-center"
                                                style="font-size:15px;font-weight:bold;background-color:azure"></td>
                                            <td class="text-center" colspan="4"
                                                style="font-size:15px;font-weight:bold;background-color:azure">
                                                {{$t('AttendanceReport.GuestedHoliday')}}  ({{ employee.description }})
                                            </td>
                                            <td class="text-center"
                                                style="font-size:15px;font-weight:bold;background-color:azure">
                                                100 %
                                            </td>
                                        </tr>
                                        <tr v-else-if="employee.checkType == 'On Leave'" v-bind:key="employee.id">
                                            <td class="text-center"
                                                style="font-size:15px;font-weight:bold;background-color:azure">
                                                {{ employee.employeeName }}
                                            </td>
                                            <td class="text-center" colspan="4"
                                                style="font-size:15px;font-weight:bold;background-color:azure">
                                                {{$t('AttendanceReport.OnLeave')}}
                                            </td>
                                            <td class="text-center"
                                                style="font-size:15px;font-weight:bold;background-color:azure">
                                                100 %
                                            </td>
                                        </tr>
                                        <tr v-else-if="employee.isAbsent" v-bind:key="employee.id">
                                            <td class="text-center"
                                                style="font-size:15px;font-weight:bold;background-color:azure">
                                                {{ employee.employeeName }}
                                            </td>
                                            <td class="text-center" colspan="4"
                                                style="font-size:15px;font-weight:bold;background-color:azure">
                                                {{$t('AttendanceReport.Absent')}}
                                            </td>
                                            <td class="text-center"
                                                style="font-size:15px;font-weight:bold;background-color:azure">
                                                0 %
                                            </td>
                                        </tr>


                                        <tr v-else v-bind:key="employee.id">

                                            <td class="text-center">
                                                {{ employee.employeeName }}
                                            </td>
                                            <td class="text-center">
                                                {{ getDate(employee.date) }}
                                            </td>


                                            <td class="text-center">{{ convertDate(employee.checkIn) }}</td>
                                            <td class="text-center">{{ convertDate(employee.checkOut) }}</td>
                                            <td class="text-center">
                                                {{ SubtractDateTime(employee.checkIn, employee.checkOut) }}</td>
                                            <td class="text-center">
                                                {{ CalculateWorkingPercentage(employee.totalHour, employee.officeHour, employee.totalMinute, employee.officeMinute) }}
                                                %</td>
                                        </tr>





                                    </template>
                                    <tr v-bind:key="employee.id">
                                        <td class="text-right" colspan="6"
                                            style="font-size:15px;font-weight:bold;background-color:azure">
                                            {{$t('AttendanceReport.TotalWorkingHour')}}: {{ tWorkingHour }} %
                                        </td>

                                    </tr>
                                </tbody>
                            </table>
                        </div>

                    </div>
                </div>


            </div>
           
        </div>
        <AttendenceFilterReport :printDetails="printDetails" :headerFooter="headerFooter" v-if="isShown" v-bind:key="printRender" />
        <!--<loading v-model:active="loading" :can-cancel="true" :is-full-page="true"></loading>-->
    </div>
    <div v-else>
        <acessdenied></acessdenied>
    </div>
</template>
<script>
import clickMixin from '@/Mixins/clickMixin'
import moment from "moment";

export default {

    mixins: [clickMixin],


    data: function () {
        return {
            printDetails: [],
            employeelist: [],
            render: 0,
            randerButton: 0,
            fromDate: '',
            employee: [],
            toDate: '',
            employeeId: '',
            currentPage: 1,
            pageCount: '',
            rowCount: '',
            invoice: '',
            invoiceList: [],
            resultList: [],
            isShown: false,
            isDownload: false,
            printRender: 0,
            advanceFilters: true,
            combineDate: '',
            openingCash: 0,
            totalBalance: 0,
            counter: 0,

            language: 'Nothing',
            headerFooter: {
                footerEn: '',
                footerAr: '',
                company: ''
            },
        }
    },
    computed: {
        tWorkingHour: function () {
            
            var totalHour = this.employeelist.reduce(function (a, c) { return a + Number((c.totalHour) || 0) }, 0);

            var totalWorkingHour = this.employeelist.reduce(function (a, c) { return a + Number((c.officeHour) || 0) }, 0);
            if (totalHour == 0) {
                return '0';
            }
            var total = ((totalHour) / (totalWorkingHour)) * 100;

            return total.toFixed(3).slice(0, -1);

        },


    },
    watch: {
        fromDate: function (fromdate) {
            this.counter++;
            if (this.counter != 1) {
                var todate = this.toDate;

                this.EmployeeAttendence(fromdate, todate);
            }



        },
        toDate: function (todate) {
            this.counter++;
            if (this.counter != 2) {
                var fromdate = this.fromDate;
                this.EmployeeAttendence(fromdate, todate);
            }


        },

    },
    methods: {
        GotoPage: function (link) {
            this.$router.push({ path: link });
        },
        CloseAll: function () {

            this.employeeId = '';
            this.employee = [];
            this.randerButton++;
            this.EmployeeAttendence(this.fromDate, this.toDate);

        },
        PrintCsv: function () {
            var root = this;


            root.$https.post('/Report/ManualListQuery?language=' + this.languageForCSV() + '&fromDate=' + this.fromDate + '&toDate=' + this.toDate + '&companyId=' + localStorage.getItem('CompanyID'), root.employeelist, { headers: { "Authorization": `Bearer ${localStorage.getItem('token')}` }, responseType: 'blob' })
                .then(function (response) {

                    const url = window.URL.createObjectURL(new Blob([response.data]));
                    const link = document.createElement('a');
                    link.href = url;
                    link.setAttribute('download', 'SaleReport.csv');
                    document.body.appendChild(link);
                    link.click();

                });

        },


        GetEmployeeWiseRecord: function (value) {
            
            this.employee = [];
            for (var i = 0; i < value.length; i++) {
                this.employee[i] = value[i].id
            }
            this.EmployeeAttendence(this.fromDate, this.toDate);

        },
        PrintDetails: function (download) {

            var root = this;
            if (download) {
                root.isDownload = true;
                root.printDetails = root.employeelist;
                root.isShown = true;
                root.printRender++;
            }
            else {
                root.printDetails = root.employeelist;
                root.isShown = true;
                root.printRender++;
            }
        },
        GetHeaderDetail: function () {
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            root.$https.get("/Company/GetCompanyDetail?id=" + localStorage.getItem('CompanyID'), { headers: { Authorization: `Bearer ${token}` }, })
                .then(function (response) {
                    if (response.data != null) {

                        root.headerFooter.company = response.data;

                        root.getBase64Image(root.headerFooter.company.logoPath);
                    }
                });
        },
        languageChange: function (lan) {
            if (this.language == lan) {

                var getLocale = this.$i18n.locale;
                this.language = getLocale;

                this.$router.go('/CustomerDiscountProducts');

            }


        },

        getDate: function (date) {
            if (date == '0001-01-01T00:00:00')
                return '';
            return moment(date).format('ddd, DD-MMM-YYYY');
        },
        EmployeeAttendence: function (fromDate, toDate) {
            

            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            root.$https.post('/Payroll/EmployeeAttendenceDateWiseQuery?fromDate=' + fromDate + '&toDate=' + toDate, this.employee, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                if (response.data != null) {

                    root.employeelist = response.data
                }
            });
        },


        convertDate: function (date) {
            if (date == undefined || date == null) {
                return "";
            }
            const dateValue = moment(date).format('DD-MMM-YYYY, hh:mm A');
            return moment(dateValue).format('hh:mm A');
        },
        CalculateWorkingPercentage: function (totalHour, officeHour) {
            if (totalHour == 0) {
                return 0;
            }
            var total = (totalHour / officeHour) * 100;
            if (total == undefined) {
                return 0;
            }

            return total.toFixed(0);
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

            if (y == null || y == undefined) {
                return '';
            }
            const startTime = moment(x);
            const endTime = moment(y);
            const duration = moment.duration(endTime.diff(startTime));
            const hours = parseInt(duration.asHours());
            const minutes = parseInt(duration.asMinutes()) % 60;

            return (hours + ':' + minutes)


        },
        getBase64Image: function (path) {
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            this.headerFooter.company.logoPath = 'data:image/png;base64,' + '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';

            root.$https
                .get('/Contact/GetBaseImage?filePath=' + path, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                    if (response.data != null) {
                        root.filePath = response.data;
                        root.headerFooter.company.logoPath = 'data:image/png;base64,' + root.filePath;

                    }
                });
        },

    },
    created: function () {
        this.fromDate = moment().format("DD MMM YYYY");
        this.toDate = moment().format("DD MMM YYYY");
        this.fromDate = moment().subtract(1, 'months').format('DD MMM YYYY');
        this.toDate = moment().format('DD MMM YYYY');
    },

    mounted: function () {
        this.headerFooter.footerEn = localStorage.getItem('TermsInEng');
        this.headerFooter.footerAr = localStorage.getItem('TermsInAr');
        this.language = this.$i18n.locale;
        this.GetHeaderDetail();


        this.EmployeeAttendence(this.fromDate, this.toDate);
    }
}
</script>
<style scoped>
table {
    width: 100%;
}

thead,
tbody tr {
    display: table;
    width: 100%;
    table-layout: fixed;
}

tbody {
    display: block;
    overflow-y: auto;
    table-layout: fixed;
    max-height: 600px;
}

::-webkit-scrollbar {
    width: 11px !important;
    height: 10px !important;
}
</style>