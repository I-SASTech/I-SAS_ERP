<template>
    <div>
        <div ref="mychildcomponent" hidden id='purchaseInvoice'>
            <div style="margin-left:20px;margin-right:20px">
                <!--HEADER-->
                <div>
                    <div class="row" style="border:2px solid #000000;">
                        <div style="width:100%;">
                            <div style="text-align: center;margin-top:5px;">
                                <span style="font-size:30px;font-weight:bold;color:black;">{{headerFooters.company.nameEnglish}} <span v-if="arabic=='true'">{{headerFooters.company.nameArabic}}</span></span><br />


                            </div>
                            <div style="text-align: center;margin-top:5px;" v-if="isTransfer">
                                <span style="font-size:30px;font-weight:bold;color:black;">Cash Counter Transfer<span v-if="arabic=='true'">/تحويل العداد النقدي</span></span><br />


                            </div>
                            <div style="text-align: center;margin-top:5px;" v-else>
                                <span style="font-size:30px;font-weight:bold;color:black;">Cash Counter Close<span v-if="arabic=='true'">/إغلاق عداد النقد  </span></span><br />


                            </div>
                        </div>


                    </div>
                </div>


                <div class="row " style="height:360mm;margin-top:1mm;border:2px solid #000000;">
                    <div style="margin-top:2rem;">
                        <div class="row ml-2 mr-2">
                            <table class="table table-borderless ">
                                <tr style="font-size:16px;">
                                    <td class="" style="border-top: 0;padding-left:1px;padding-right:1px;color:black;"><span style="font-weight:bold;color:black;">From Date/</span><span v-if="arabic=='true'">من التاريخ</span><br>{{printDetails.startDate}}</td>
                                    <td style="text-align:right; border-top: 0;padding-left:1px;padding-right:1px;color:black;"><span style="font-weight:bold;color:black;">To Date/</span><span v-if="arabic=='true'">حتي اليوم</span><br>{{printDetails.date}}</td>

                                </tr>

                            </table>

                        </div>

                        <div class="row ml-2 mr-2">
                            <table class="table table-borderless ">
                                <!--<tr style="font-size:16px;" v-if="isTransfer">
                                    <td class="" style="border-top: 0;padding-left:1px;padding-right:1px;color:black;"><span style="font-weight:bold;color:black;">From Time/</span> من وقت  <br>{{printDetails.startTime}}</td>
                                    <td style="text-align:right; border-top: 0;padding-left:1px;padding-right:1px;color:black;"><span style="font-weight:bold;color:black;">To Time/</span> الى وقت <br>{{printDetails.toTime}}</td>

                                </tr>
                                <tr style="font-size:16px;" v-else>
                                    <td class="" style="border-top: 0;padding-left:1px;padding-right:1px;color:black;"><span style="font-weight:bold;color:black;">From Time/</span> من وقت  <br>{{printDetails.startTime.split(' ')[1]}}</td>
                                    <td style="text-align:right; border-top: 0;padding-left:1px;padding-right:1px;color:black;"><span style="font-weight:bold;color:black;">To Time/</span> الى وقت <br>{{printDetails.toTime}}</td>
                                </tr>-->

                                <tr style="font-size:16px;" v-if="isTransfer">

                                    <td class="" style="border-top: 0;padding-left:1px;padding-right:1px;color:black;"><span style="font-weight:bold;color:black;">Transfer From:</span><span v-if="arabic=='true'">تحويل من</span>  <br>{{printDetails.user}}</td>
                                    <td style="text-align:right; border-top: 0;padding-left:1px;padding-right:1px;color:black;"><span style="font-weight:bold;color:black;">Transfer to:</span>حول إلى<span></span>   <br>{{printDetails.toUser}}</td>

                                </tr>
                                <tr style="font-size:16px;">


                                    <td class="" style="border-top: 0;padding-left:1px;padding-right:1px;color:black;"><span style="font-weight:bold;color:black;">Counter#</span> رقم الكاونتر  <br>{{printDetails.counterCode}}</td>
                                    <td v-if="!isTransfer" style="text-align:right; border-top: 0;padding-left:1px;padding-right:1px;color:black;"><span style="font-weight:bold;color:black;">User</span> أسم المستخدم <br>{{printDetails.user}}</td>
                                </tr>
                            </table>
                        </div>
                        <div class="row ml-2 mr-2" style="border-top:1px solid; color:black">
                            <div class="col-sm-12 mt-2">
                                <h6 class="modal-title" id="myModalLabel" style="padding-top:5px; padding-bottom:3px;text-align:center; font-size:15px">Cash Sale & Expense Detail / البيع النقدي وتفاصيل المصاريف</h6>

                            </div>
                            <table class="table table-borderless ">
                                <tr style="font-size:16px;">
                                    <td class="" style="border-top: 0;padding-left:1px;padding-right:1px;color:black;"><span style="font-weight:bold;color:black;">Opening Cash/</span>فتح النقدية<br>{{parseFloat(printDetails.openingCash).toFixed(3).slice(0,-1)}}</td>
                                    <td style="text-align:right; border-top: 0;padding-left:1px;padding-right:1px;color:black;"><span style="font-weight:bold;color:black;">Cash Sale/</span>بيع نقدا<br>{{parseFloat(printDetails.totalCash).toFixed(3).slice(0,-1)}}</td>
                                    <td style="text-align:right; border-top: 0;padding-left:1px;padding-right:1px;color:black;"><span style="font-weight:bold;color:black;">Total VAT/</span>إجمالي ضريبة القيمة المضافة<br>{{parseFloat(printDetails.totalVat).toFixed(3).slice(0,-1)}}</td>

                                </tr>
                                <tr style="font-size:16px;">
                                    <td class="" style="border-top: 0;padding-left:1px;padding-right:1px;color:black;"><span style="font-weight:bold;color:black;">Cash Expense/</span>المصاريف النقدية<br>{{parseFloat(printDetails.expenseCash).toFixed(3).slice(0,-1)}}</td>
                                    <td v-if="isBankDetailShow" style="text-align:right; border-top: 0;padding-left:1px;padding-right:1px;color:black;"><span style="font-weight:bold;color:black;"  >Bank Expense/</span>مصاريف البنك<br>{{parseFloat(printDetails.bankExpense).toFixed(3).slice(0,-1)}}</td>

                                </tr>
                                <!--<tr style="font-size:16px;">
        <td class="" style="border-top: 0;padding-left:1px;padding-right:1px;color:black;"><span style="font-weight:bold;color:black;">Total Cash/</span>مجموع المبالغ النقدية<br>{{parseFloat(printDetails.totalCash).toFixed(3).slice(0,-1)}}</td>
        <td style="text-align:right; border-top: 0;padding-left:1px;padding-right:1px;color:black;"><span style="font-weight:bold;color:black;">Expense/</span>نفقات<br>{{parseFloat(printDetails.expenseCash).toFixed(3).slice(0,-1)}}</td>

    </tr>-->
                            </table>

                            <div class="col-sm-6 ml-auto mr-auto mb-4 " style=" color:black ;text-align:center;margin-top:2rem;">
                                <p style="font-size:15px;font-weight:bold;color:black;">Cash In Hand: <span style="margin-right:20px;margin-left:10px;padding-left:20px;color:black;">{{parseFloat(parseFloat(printDetails.cashInHand) ).toFixed(3).slice(0,-1)}}</span> : نقد في اليد</p>
                            </div>



                        </div>
                        <div class="row ml-2 mr-2" style="border-top:1px solid; color:black" v-if="!isTransfer">
                            <div class="col-sm-12 mt-4">
                                <h6 class="modal-title" id="myModalLabel" style="padding-top:3px; padding-bottom:3px;padding-left:3px;text-align:center;font-size:15px">Bank Sale Detail /تفاصيل بيع البنك</h6>

                            </div>
                            <table class="table table-borderless" v-if="!isBankDetailShow">
                                <tr style="font-size:14px ; border-color:black !important" v-for="bank in printDetails.bankDetails" v-bind:key="bank.bankName">
                                    <td style="color: black !important; border-top:1px solid; font-weight:bold"> <span>{{bank.bankName}}</span>:</td>
                                    <td style="color: black !important ;border-top:1px solid;font-weight:bold">{{parseFloat(bank.totalAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                </tr>
                                <tr style="border-top: 1px solid !important; border-color:black !important">
                                    <td style="color: black;font-size:14px;padding-top:0px !important; padding-bottom:0px !important; font-weight:bold"> <span>Bank Total / مجموع البنك</span>:</td>
                                    <td style="color: black;font-size:14px;padding-top:0px !important; padding-bottom:0px !important;font-weight:bold">{{parseFloat(printDetails.bankAmount).toFixed(3).slice(0,-1)}}</td>
                                </tr>
                            </table>

                            <table class="table table-borderless" v-else>
                                <tr style="font-size:14px ; border-color:black !important" v-for="(bank, index) in printDetails.bankDetailList" v-bind:key="index">
                                    <td style="color: black !important; border-top:1px solid; font-weight:bold"> <span>{{bank.bankName}}</span>:</td>
                                    <td style="color: black !important ;border-top:1px solid;font-weight:bold">{{parseFloat(bank.amount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                </tr>
                                <tr style="border-top: 1px solid !important; border-color:black !important">
                                    <td style="color: black;font-size:14px;padding-top:0px !important; padding-bottom:0px !important; font-weight:bold"> <span>Bank Total / مجموع البنك</span>:</td>
                                    <td style="color: black;font-size:14px;padding-top:0px !important; padding-bottom:0px !important;font-weight:bold">{{parseFloat(printDetails.bankDetailListTotal).toFixed(3).slice(0,-1)}}</td>
                                </tr>
                            </table>
                            <div class="col-sm-6 ml-auto mr-auto  " style=" color:black ;text-align:center;margin-top:2rem;">
                                <p style="font-size:15px;font-weight:bold;color:black;">Total Sale: <span style="margin-right:20px;margin-left:10px;padding-left:20px;color:black;">{{parseFloat(parseFloat(printDetails.totalCash) + parseFloat(printDetails.bankAmount)).toFixed(3).slice(0,-1)}}</span>:إجمالي البيع</p>
                            </div>

                        </div>
                    </div>

                    <div style="width:95%;padding-left:5px;padding-right:5px">
                        <table class="table report-tbl">

                            <tr style="font-size:16px;font-weight:bold;">
                                <td colspan="3" style="text-align: right; border-bottom: 0; padding-bottom: 1px; padding-left: 1px; padding-right: 0px; color: black;">Total/المجموع</td>
                                <td colspan="2" style="padding-right:3rem; text-align: right; border-bottom: 0; padding-bottom: 1px; padding-left: 10px; padding-right: 1px; color: black;">{{parseFloat(printDetails.total).toFixed(3).slice(0,-1)}}</td>
                            </tr>
                            <tr style="font-size:16px;font-weight:bold;">
                                <td colspan="3" style="text-align: right; padding-left: 1px; padding-right: 0px; border-top: 0; padding-top: 0; border-bottom: 0; padding-bottom: 1px; color: black;">Verify Cash/تحقق من النقد:</td>
                                <td colspan="2" style="padding-right: 3rem; text-align: right; padding-left: 10px; padding-right: 1px; border-top: 0; padding-top: 0; border-bottom: 0; padding-bottom: 1px; color: black; ">{{parseFloat(printDetails.verifyCash).toFixed(3).slice(0,-1)}}</td>
                            </tr>

                            <!--<tr style="font-size:16px;font-weight:bold;" v-if="!isTransfer">
                                <td style="text-align: right; padding-left: 1px; padding-right: 0px; border-top: 0; padding-top: 0; border-bottom: 0; padding-bottom: 1px; color: black;" colspan="3">Carry Cash/يحمل نقودا:</td>
                                <td colspan="2" style="padding-right: 3rem; text-align: right; padding-left: 10px; padding-right: 1px; border-top: 0; padding-top: 0; border-bottom: 0; padding-bottom: 1px; color: black; ">{{parseFloat(printDetails.carryCash).toFixed(3).slice(0,-1)}}</td>
                            </tr>-->
                            <tr style="font-size:16px;font-weight:bold;" v-if="!isTransfer">
                                <td style="text-align: right; padding-left: 1px; padding-right: 0px; border-top: 0; padding-top: 0; border-bottom: 0; padding-bottom: 1px; color: black;" colspan="3">Next Day Opening Cash  / فتح اليوم التالي نقدا :</td>
                                <td colspan="2" style="padding-right: 3rem; text-align: right; padding-left: 10px; padding-right: 1px; border-top: 0; padding-top: 0; border-bottom: 0; padding-bottom: 1px; color: black; ">{{parseFloat(parseFloat(printDetails.verifyCash) - parseFloat(printDetails.carryCash)).toFixed(3).slice(0,-1)}}</td>
                            </tr>
                            <tr style="font-size:16px;font-weight:bold;" v-if="printDetails.carryCash>0 && printDetails.isSupervisor == false">

                                <td style="text-align: right; padding-left: 1px; padding-right: 0px; padding-top: 5px; color: black;border-top: 0;" colspan="3">Cash Received By /استلم النقد من:</td>
                                <td colspan="2" style="padding-right: 3rem; text-align: right; padding-left: 10px; padding-right: 1px; padding-top: 5px; color: black;border-top: 0; ">{{printDetails.supervisorName}}</td>

                            </tr>
                            <tr style="font-size:16px;font-weight:bold;" v-if="!isTransfer">
                                <td style="text-align: right; padding-left: 1px; padding-right: 0px; padding-top: 5px; color: black;border-top: 0;" colspan="3">Cash Transfer/ تحويل نقدي:</td>
                                <td colspan="2" style="padding-right: 3rem; text-align: right; padding-left: 10px; padding-right: 1px; padding-top: 5px; color: black;border-top: 0; ">{{parseFloat(printDetails.carryCash).toFixed(3).slice(0,-1)}}</td>
                            </tr>
                            <tr style="font-size:19px;font-weight:bold;" v-if="printDetails.total != printDetails.verifyCash">
                                <td style="text-align:left; color: black;border-top: 1px solid #000000;" colspan="5">Reason/سبب</td>
                            </tr>
                            <tr style="font-size:19px;" v-if="printDetails.total != printDetails.verifyCash">

                                <td style="text-align: left; padding-left: 1px; padding-right: 0px; padding-top: 5px; color: black;border-top: 0;" colspan="5">{{printDetails.creditReason}}</td>

                            </tr>

                            <!--<tr style="font-size:16px;font-weight:bold;">
                                <td style="text-align: right; padding-left: 1px; padding-right: 0px; border-top: 0; padding-top: 0; border-bottom: 0; padding-bottom: 1px; color: black;" colspan="3">Carry Cash/يحمل نقودا:</td>
                                <td colspan="2" style="padding-right: 3rem; text-align: right; padding-left: 10px; padding-right: 1px; border-top: 0; padding-top: 0; border-bottom: 0; padding-bottom: 1px; color: black; ">{{parseFloat(printDetails.carryCash).toFixed(3).slice(0,-1)}}</td>
                            </tr>-->



                        </table>
                    </div>

                    <div style="width:95%;padding-left:5px;padding-right:5px"  v-if="printDetails.outStandingBalance!=0 && printDetails.outStandingBalance!='' && printDetails.outStandingBalance!=undefined">
                        <div>
                            <div>
                                <div class="card" v-bind:key="randerList">
                                    <div class="card-header">
                                        <h6 class="card-title " v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">OutStanding Balance</h6>

                                    </div>
                                    <div class="card-body">
                                        <div class="col-lg-12">
                                            <table class="table table-shopping table-borderless">
                                                <thead class="">
                                                    <tr style="border-bottom:2px solid #000000">
                                                        <th class="text-left">
                                                            {{$t('DayEndA4ReportPrint.Date')}}
                                                        </th>
                                                        <th class="text-left">
                                                            {{$t('DayEndA4ReportPrint.Total')}}
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr v-for="(po,index) in printDetails.dayWiseList" :key="index">


                                                        <td class="text-left" style="border: none;">
                                                            {{po[0].date}} {{po[0].fromTime}} - {{po[0].date}} {{po[0].toTime}}
                                                        </td>
                                                        <td class="text-left" style="border: none;">
                                                            {{parseFloat(po[0].totalCash).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                                        </td>


                                                    </tr>
                                                    <tr style="border-top:2px solid #000000">


                                                        <td class="text-left" style="font-size: 19px; border:none; font-weight: bold">
                                                            Total OutStanding Balance
                                                        </td>
                                                        <td class="text-left" style="font-size: 19px; border: none; font-weight: bold">
                                                            {{printDetails.outStandingBalance}}
                                                        </td>


                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>

                                    </div>
                                </div>
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
    //import axios from 'axios'
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
        windowTitle: window.document.title
    }
    import clickMixin from '@/Mixins/clickMixin'
    export default {
        mixins: [clickMixin],
        props: ['printDetail', 'headerFooter', 'isTransfer'],
        data: function () {
            return {
                isBankDetailShow: false,
                isHeaderFooter: '',
                arabic: '',
                english: '',
                UserName: '',
                total: 0,
                grandTotal: 0,
                transactions: 0,
                printDetails: [],

                render: 0,
                headerFooters: {
                    footerEn: '',
                    footerAr: '',
                    company: ''
                },
                htmlData: {
                    htmlString: ''
                }
            }
        },
        methods: {
            logout: function () {

                var root = this;
                var url = '/account/logout';
                this.$https.post(url, this.login).then(function (response) {
                    if (response.data == "Success") {

                        //root.$session.destroy();
                        root.$store.commit('logout');
                        localStorage.clear();
                        //document.cookie.split(';').forEach(cookie => document.cookie = cookie.replace(/^ +/, '').replace(/=.*/, `=;expires=${new Date(0).toUTCString()};path=/`));

                        root.$router.push('/')
                    }
                    else {
                        root.$swal.fire(
                            {
                                icon: 'error',
                                title: 'Error Logging Out'
                            });
                    }

                });

            },
            getDate: function () {

                return moment(this.printDetails.startDate).format(' YYYY-MM-DD');
            },
            getTimeOnly: function (x) {
                return moment(x).format('HH:mm ');
            },
            printInvoice: function () {
                var root = this;
                //this.$htmlToPaper('purchaseInvoice');
                
                this.$htmlToPaper('purchaseInvoice', options, () => {
                    if (root.isTransfer) {
                        root.logout()
                    }
                    else {
                        root.$router.go();
                    }

                });
            },
        },
        mounted: function () {
            this.isBankDetailShow = localStorage.getItem('BankDetail') == 'true' ? true : false;
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
            this.isHeaderFooter = localStorage.getItem('IsHeaderFooter');
            var root = this;
            
            this.printDetails = this.printDetail;
            this.printDetails.user = localStorage.getItem('LoginUserName');
            this.headerFooters = this.headerFooter;
            setTimeout(function () {
                root.printInvoice();
            }, 125);
        },

    }
</script>

