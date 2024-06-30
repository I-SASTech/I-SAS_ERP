<template>
    <div v-bind:style="$i18n.locale == 'ar' ? languageChange('en') : languageChange('ar')" v-if="isValid('CanViewAttendanceReport')">
        <div v-bind:class="$i18n.locale == 'en' ? 'text-left' : 'arabicLanguage'">
            <div>
                <div class="card">
                    <div class="card-header">
                        <div class="row">
                            <div class="col-md-6 col-lg-6">
                                <div class="form-group ">
                                    <h5 class="card-title DayHeading"> Attendance Report </h5>
                                </div>
                            </div>
                            <div class="col-md-6 col-lg-6">
                                <div class="form-group " v-bind:class="$i18n.locale == 'en' ? 'text-right' : 'text-left' ">
                                    <router-link :to="'/ManualAttendance'">
                                        <a href="javascript:void(0)" class="btn btn-outline-primary "><i class="fas fa-arrow-circle-left fa-lg"></i> </a>
                                    </router-link>
                                    <a href="javascript:void(0)" class="btn btn-outline-primary " v-on:click="PrintDetails(false)"><i class=" fa fa-print"></i> Print</a>
                                    <!--<a href="javascript:void(0)" class="btn btn-outline-primary " v-on:click="PrintDetails(true)"><i class="far fa-file-pdf"></i> Export PDF</a>-->
                                    <!--<a href="javascript:void(0)" class="btn btn-outline-primary " v-on:click="PrintCsv"><i class="fas fa-file-excel"></i> Export CSV</a>-->
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row" v-bind:key="render" v-bind:class="$i18n.locale == 'en' ? '' : 'pr-3' ">
                        <div class="col-md-3 col-lg-3">
                            <div class="form-group ml-3 ">
                                <label> From Date </label>
                                <datepicker v-model="fromDate" :key="render" />
                            </div>
                        </div>
                        <div class="col-md-3 col-lg-3">
                            <div class="form-group ">
                                <label> To Date </label>
                                <datepicker v-model="toDate" :key="render" />
                            </div>
                        </div>



                    </div>

                    <div style=" overflow: auto;display:block;display:flex;height:350px;font-size:15px;width:auto">
                        <div v-for="details in employeelist" v-bind:key="details.date" style="width:350px !important">
                            <div style="width:350px !important;">
                                <div style="text-align:center;font-weight: 700;font-size:20px;">{{getDate(details.date)}} </div>
                                <div class="row" style="width:350px !important;">
                                    <span style="width:10%;text-align:left;font-weight: 700; font-size: 14px !important; padding-top: 3px !important; padding-bottom: 3px !important; "></span>
                                    <span style="width:10%;text-align:left;font-weight: 700; font-size: 14px !important; padding-top: 3px !important; padding-bottom: 3px !important;padding-right:1px;background-color:#E1ECFF ">#</span>
                                    <span style="width:40%;text-align:left;font-weight: 700; font-size: 14px !important; padding-top: 3px !important; padding-bottom: 3px !important;padding-right:1px;background-color:#E1ECFF ">Employee Name</span>
                                    <span style="width:40%;text-align:left;font-weight: 700; font-size: 14px !important; padding-top: 3px !important; padding-bottom: 3px !important;padding-right:1px;background-color:#E1ECFF ">Check In/Out</span>
                                </div>
                                <div class="row" v-for="(employee,index) in details.manualAttendenceLookUpModel" v-bind:key="employee.id" style="width:350px !important">
                                    <span style="width:10%;text-align:center;font-weight: 700; font-size: 14px !important; padding-top: 3px !important; padding-bottom: 3px !important; "></span>
                                    <span style="width:10%;text-align:left;font-weight: normal; font-size: 14px !important; padding-top: 3px !important; padding-bottom: 3px !important;padding-right:1px ">{{index+1}}</span>
                                    <span style="width:40%;text-align:left;font-weight: normal; font-size: 14px !important; padding-top: 3px !important; padding-bottom: 3px !important;padding-right:1px ">{{employee.employeeName}}</span>
                                    <span style="width:40%;text-align:left;font-weight: normal; font-size: 14px !important; padding-top: 3px !important; padding-bottom: 3px !important;padding-right:1px; display:flex; align-items:center" v-if="employee.checkIn==null || employee.checkIn==undefined ">
                                        Leave

                                    </span>
                                    <!--<span style="width:40%;text-align:left;font-weight: normal; font-size: 14px !important; padding-top: 3px !important; padding-bottom: 3px !important;padding-right:1px; display:flex; align-items:center" v-else>
                                        <div style="display:flex;  flex-direction:column">
                                            <span>{{convertDate(employee.checkIn)}}</span><span>{{convertDate(employee.checkOut)}}</span>

                                        </div>
                                        <small style="font-size:9px">Hour</small>
                                        <span style="padding-left:5px;font-size:12px">({{SubtractDateTime(employee.checkIn,employee.checkOut)}})</span>

                                    </span>-->
                                    <span style="width:40%;text-align:left;font-weight: normal; font-size: 14px !important; padding-top: 3px !important; padding-bottom: 3px !important;padding-right:1px" v-else>

                                        <span>{{convertDate(employee.checkIn)}}</span>
                                        <small style="font-size:9px">&nbsp;Hour</small>
                                        <span style="padding-left:5px;font-size:12px">({{SubtractDateTime(employee.checkIn,employee.checkOut)}})</span>
                                        <br />
                                        <span>{{convertDate(employee.checkOut)}}</span>
                                        <small style="font-size:9px">&nbsp;Ov.Time</small>
                                        <span style="padding-left:5px;font-size:12px">({{OverTime(employee.companyTimeIn,employee.companyTimeOut,employee.checkIn,employee.checkOut)}})</span>


                                    </span>

                                </div>
                            </div>

                        </div>

                    </div>

                    <!--<div class="card-body" id="content">

                        <div class="row">
                            <div class="col-12" v-for="details in employeelist" v-bind:key="details.date">
                                {{getDate(details.date)}}

                                <div class=" table-responsive">
                                    <table class="table g" v-bind:class="$i18n.locale == 'en' ? 'text-left' : 'arabicLanguage'">
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



                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr v-for="(employee,index) in details.manualAttendenceLookUpModel" v-bind:key="employee.id">
                                                <td class="text-center">
                                                    {{index+1}}
                                                </td>

                                                <td class="text-center">
                                                    {{employee.employeeName}}
                                                </td>

                                                <td class="text-center">{{convertDate(employee.checkIn)}}</td>
                                                <td class="text-center">{{convertDate(employee.checkOut)}}</td>
                                                <td class="text-center">{{SubtractDateTime(employee.checkIn,employee.checkOut)}}</td>
                                                <td class="text-center">{{OverTime(employee.companyTimeOut,employee.checkOut)}}</td>



                                            </tr>
                                        </tbody>
                                    </table>
                                </div>

                            </div>

                        </div>

                    </div>-->


                </div>
            </div>
        </div>
        <AttendenceFilterReport :printDetails="printDetails" :headerFooter="headerFooter" v-if="isShown" v-bind:key="printRender" />

    </div>
    <div v-else> <acessdenied></acessdenied></div>
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
                fromDate: '',
                toDate: '',
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
            PrintDetails: function (download) {

                var root = this;
                if (download) {
                    this.GetHeaderDetail();
                    root.isDownload = true;
                }
                else {
                    this.GetHeaderDetail();
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
                return moment(date).format('ddd, DD-MMM-YYYY');
            },
            EmployeeAttendence: function (fromDate, toDate) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('/Payroll/EmployeeAttendenceFilterReport?fromDate=' + fromDate + '&toDate=' + toDate, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

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
                            root.printDetails = root.employeelist;
                            root.isShown = true;
                            root.printRender++;
                        }
                    });
            },

        },
        created: function () {
            this.fromDate = moment().format("DD MMM YYYY");
            this.toDate = moment().format("DD MMM YYYY");
            this.fromDate = moment().subtract(1, 'days').format('DD MMM YYYY');
            this.toDate = moment().add(5, 'days').format('DD MMM YYYY');
        },

        mounted: function () {
            this.headerFooter.footerEn = localStorage.getItem('TermsInEng');
            this.headerFooter.footerAr = localStorage.getItem('TermsInAr');
            this.language = this.$i18n.locale;


            this.EmployeeAttendence(this.fromDate, this.toDate);
        }
    }
</script>
<style scoped>

    table {
        width: 100%;
    }

    thead, tbody tr {
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