<template>
    <div>
        <div hidden id='AttendenceReport' class="col-md-12;" style="background-color:white">
            <!--page1-->
            <div style="background-color:white">
                <!--HEADER-->
                <div class="col-md-12 " style="height:45mm;border:1px solid #000000;background-color:white" v-if="IsPaksitanClient">
                    <div class="row" style="height:35mm">
                        <div class="col-md-4  my-5" style="padding:0px !important; margin:0 !important">
                            <img :src="headerFooters.company.logoPath" style="width:auto;max-width:300px; max-height:100px;  margin:0 !important;padding:5px">
                        </div>
                        <div class="col-md-8 ">
                            <table class="text-center">
                                <tr>
                                    <td>
                                        <p>
                                            <u style="font-size:16px;color:black !important;font-weight:bold;">
                                                Attendance Report
                                            </u><br />
                                            <span style="font-size:27px;color:black !important;font-weight:bold;">{{headerFooters.company.nameEnglish}}</span><br />
                                            <span style="font-size:16px;color:black !important;font-weight:bold;">{{headerFooters.company.addressEnglish}}</span><br />
                                            <span style="font-size:18px;color:black !important;font-weight:bold;float:left">NTN :&nbsp;&nbsp;&nbsp; {{headerFooters.company.vatRegistrationNo}}</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <span style="font-size:18px;color:black !important;font-weight:bold;float:right">STR:&nbsp;&nbsp;&nbsp;   {{headerFooters.company.companyRegNo}}</span><br />
                                            <span style="font-size:25px;color:black !important;font-weight:bold;padding-bottom:5px !important">
                                                <span style="font-size:25px;color:black !important;font-weight:bold;padding-bottom:5px !important" v-if="formName=='Sale'">{{$t('InvoicePrintReport.SaleInvoiceReport')}}</span>
                                                <span style="font-size:25px;color:black !important;font-weight:bold;padding-bottom:5px !important" v-if="formName=='Purchase'">{{$t('InvoicePrintReport.PurchaseInvoiceReport')}}</span>
                                            </span>
                                        </p>
                                    </td>
                                </tr>
                            </table>
                        </div>

                    </div>



                </div>
                <div class="col-md-12" style="height:45mm;border:2px solid #000000;" v-else-if="isHeaderFooter=='true'">
                   
                    <div class="row" style="height:35mm;background-color:white">
                        <div class="col-md-4 ">
                            <table class="text-left">
                                <tr>
                                    <td>
                                        <p>
                                            <span style="font-size:25px;color:black !important;font-weight:bold;">{{headerFooters.company.nameEnglish}}</span><br />
                                            <span style="font-size:17px;color:black !important;font-weight:bold;">{{headerFooters.company.categoryEnglish}}</span><br />
                                            <span style="font-size:16px;color:black !important;font-weight:bold;">VAT No.: {{headerFooters.company.vatRegistrationNo}}</span><br />
                                            <span style="font-size:16px;color:black !important;font-weight:bold;">Cr No.:{{headerFooters.company.companyRegNo}}</span><br />
                                            <span style="font-size:15px;color:black !important;font-weight:bold;">
                                                Tel: {{headerFooters.company.phoneNo}}
                                            </span>
                                        </p>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="col-md-4 text-center my-5" style="padding:0px !important; margin:0 !important">
                            <img :src="headerFooters.company.logoPath" style="width:auto;max-width:300px; max-height:100px; padding:5px !important; margin:0 !important">
                        </div>
                        <div class="col-md-4 ">
                            <table class="text-right" v-if="arabic=='true'">
                                <tr>
                                    <td>
                                        <p>
                                            <span style="font-size:25px;color:black !important;font-weight:bold;">{{headerFooters.company.nameArabic}}.</span><br />
                                            <span style="font-size:17px;color:black !important;font-weight:bold;">{{headerFooters.company.categoryArabic}}</span><br />
                                            <span style="font-size:16px;color:black !important;font-weight:bold;">رقم ضريبة القيمة المضافة: {{headerFooters.company.vatRegistrationNo}}</span><br />
                                            <span style="font-size:16px;color:black !important;font-weight:bold;">رقم السجل التجاري :{{headerFooters.company.companyRegNo}}</span><br />
                                            <span style="font-size:15px;color:black !important;font-weight:bold;">
                                                هاتف: {{headerFooters.company.phoneNo}}:
                                            </span>
                                        </p>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <div class="row" style="background-color:white">
                        <div class="col-md-12" style="margin-bottom:10px !important;height:10mm" v-if="($i18n.locale == 'en' ||isLeftToRight())">
                            <p style="text-align: center; margin: 0px; padding: 0px; line-height: 1; ">
                                <span style="font-size:25px;color:black !important;font-weight:bold;padding-bottom:5px !important" >Attendance Report </span>
                            </p>
                        </div>
                        <div class="col-md-12" style="margin-bottom:10px !important;height:10mm" v-else>
                            <p style="text-align: center; margin: 0px; padding: 0px; line-height: 1; ">
                                <span style="font-size:25px;color:black !important;font-weight:bold;padding-bottom:5px !important">Attendance Report </span>

                            </p>
                        </div>
                    </div>
                </div>
                <div style="height:45mm;" v-else></div>

                <div class="col-12" style=" border:2px solid #000000;background-color:white">
                    <div class="row pt-3" style="margin-top:1mm;background-color:white">
                        <div class="col-md-12 ml-2 mr-2" style="height:6mm;font-size:16px;background-color:white">
                            <div class="row">
                                <div class="col-md-6" style="display:flex;">
                                    <div style="width:28%;font-weight:bolder;text-align:right;color:black !important;">From Date:</div>
                                    <div style="width:50%; text-align:center;color:black !important;font-weight:bold;">{{getDate(fromDate)}}</div>
                                    <div style="width:22%;font-weight:bolder; padding-right:20px;font-size:15px !important;color:black !important;" v-if="!IsPaksitanClient">:تاريخ</div>
                                </div>
                                <div class="col-md-6" style="display:flex;">
                                    <div style="width:28%; font-weight:bolder;text-align:right;color:black !important;">To Date.:</div>
                                    <div style="width:50%; text-align:center;font-weight:bold;color:black !important;">{{getDate(toDate)}}</div>
                                    <div style="width:22%;font-weight:bolder;color:black !important;font-size:15px !important" v-if="!IsPaksitanClient">
                                        : تاريخ الاستحقاق
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <!--INFORMATION-->
                <div>
                    <div class="row " style="background-color: white">
                        <div class="col-12">

                            <div class=" table-responsive">
                                <table class="table ">
                                    <thead class="m-0" style="background-color: lightgray">
                                        <tr>

                                            <th class="text-center">
                                                Employee Name
                                            </th>
                                            <th class="text-center">
                                                Date
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
                                                Working %
                                            </th>



                                        </tr>
                                    </thead>
                                    <template v-for="employee in list">

                                        <tr v-if="employee.checkType=='Week Holiday'" v-bind:key="employee.id">
                                            <td class="text-center" style="font-size:15px;font-weight:bold;background-color:azure"></td>
                                            <td class="text-center" colspan="4" style="font-size:15px;font-weight:bold;background-color:azure">Week Holiday ({{employee.description}})</td>
                                            <td class="text-center" style="font-size:15px;font-weight:bold;background-color:azure">
                                                100 %
                                            </td>
                                        </tr>
                                        <tr v-else-if="employee.checkType=='Guested Holiday'" v-bind:key="employee.id">
                                            <td class="text-center" style="font-size:15px;font-weight:bold;background-color:azure"></td>
                                            <td class="text-center" colspan="4" style="font-size:15px;font-weight:bold;background-color:azure"> Guested Holiday  ({{employee.description}})</td>
                                            <td class="text-center" style="font-size:15px;font-weight:bold;background-color:azure">
                                                100 %
                                            </td>
                                        </tr>
                                        <tr v-else-if="employee.checkType=='On Leave'" v-bind:key="employee.id">
                                            <td class="text-center" style="font-size:15px;font-weight:bold;background-color:azure">
                                                {{employee.employeeName}}
                                            </td>
                                            <td class="text-center" colspan="4" style="font-size:15px;font-weight:bold;background-color:azure"> On Leave</td>
                                            <td class="text-center" style="font-size:15px;font-weight:bold;background-color:azure">
                                                100 %
                                            </td>
                                        </tr>
                                        <tr v-else-if="employee.isAbsent" v-bind:key="employee.id">
                                            <td class="text-center" style="font-size:15px;font-weight:bold;background-color:azure">
                                                {{employee.employeeName}}
                                            </td>
                                            <td class="text-center" colspan="4" style="font-size:15px;font-weight:bold;background-color:azure">Absent</td>
                                            <td class="text-center" style="font-size:15px;font-weight:bold;background-color:azure">
                                                0 %
                                            </td>
                                        </tr>


                                        <tr v-else v-bind:key="employee.id">

                                            <td class="text-center">
                                                {{employee.employeeName}}
                                            </td>
                                            <td class="text-center">
                                                {{getDate(employee.date)}}
                                            </td>


                                            <td class="text-center">{{convertDate(employee.checkIn)}}</td>
                                            <td class="text-center">{{convertDate(employee.checkOut)}}</td>
                                            <td class="text-center">{{SubtractDateTime(employee.checkIn,employee.checkOut)}}</td>
                                            <td class="text-center">{{CalculateWorkingPercentage(employee.totalHour,employee.officeHour,employee.totalMinute,employee.officeMinute)}} %</td>
                                        </tr>




                                    </template>
                                    <tr >
                                        <td class="text-right" colspan="6" style="font-size:15px;font-weight:bold;background-color:azure">
                                            Total Working Hour:   {{tWorkingHour}} %
                                        </td>
                                    </tr>
                                </table>
                            </div>

                        </div>
                    </div>
                </div>




            </div>
        </div>
    </div>

</template>

<script>
    import moment from "moment";


    const options = {
        name: '',
        specs: [
            'fullscreen=no',
            'titlebar=yes',
            'scrollbars=yes'
        ],
        styles: [
            'https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css',
            'https://unpkg.com/kidlat-css/css/kidlat.css'
        ],
        timeout: 700,
        autoClose: true,
        windowTitle: window.document.title,

    }
    export default {
        props: ['printDetails', 'headerFooter', 'formName', 'fromDate', 'toDate','invoice'],
       
        data: function () {
            return {
              
                IsPaksitanClient: false,

                isHeaderFooter: '',
                invoicePrint: '',
                arabic: '',
                english: '',
                list: [],
                render: 0,
                headerFooters: {
                    footerEn: '',
                    footerAr: '',
                    company: ''
                }
            }
        },
        computed: {
            tWorkingHour: function () {
                
                var totalHour = this.list.reduce(function (a, c) { return a + Number((c.totalHour) || 0) }, 0);
                //var totalMinute = parseInt(this.list.reduce(function (a, c) { return a + Number((c.totalMinute) || 0) }, 0) / 60);
                //var officeMinute = parseInt(this.list.reduce(function (a, c) { return a + Number((c.officeMinute) || 0) }, 0) / 60);
                var totalWorkingHour = this.list.reduce(function (a, c) { return a + Number((c.officeHour) || 0) }, 0);
                var total = ((totalHour ) / (totalWorkingHour )) * 100;
                if (totalHour == 0) {
                    return '0';
                }
                return total.toFixed(3).slice(0, -1);
            },


        },
        methods: {
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

            getDate: function (date) {
                if (date == '0001-01-01T00:00:00')
                    return '';
                return moment(date).format('ddd, DD-MMM-YYYY');
            },
            convertDate: function (date) {
                if (date == undefined || date == null) {
                    return "";
                }
                const dateValue = moment(date).format('DD-MMM-YYYY, hh:mm A');
                return moment(dateValue).format('hh:mm A');
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

          
            printInvoice: function () {

                this.$htmlToPaper('AttendenceReport', options, () => {
                    

                   

                });
            },



        },
        mounted: function () {
            
            this.IsPaksitanClient = localStorage.getItem('IsPaksitanClient') == "true" ? true : false;
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
            this.isHeaderFooter = localStorage.getItem('IsHeaderFooter');
            this.invoicePrint = localStorage.getItem('InvoicePrint');
            var root = this;
            if (this.printDetails.length > 0) {
                this.list = this.printDetails;
                this.headerFooters = this.headerFooter;

                setTimeout(function () {
                    root.printInvoice();
                }, 125)
            }
        },

    }
</script>

