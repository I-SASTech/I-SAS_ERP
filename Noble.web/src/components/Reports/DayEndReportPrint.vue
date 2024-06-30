<template>
    <div>
        <div ref="mychildcomponent" hidden id='purchaseInvoice' style="width:400px;z-index:-9999">
            <div style="width:400px;margin-left:20px;">
                <!--HEADER-->
                <div>
                    <div class="row">
                        <div style="width:100%;">
                            <div style="text-align: center;">
                                <span style="font-size:30px;font-weight:bold;color:black;">{{headerFooters.company.nameEnglish}} {{headerFooters.company.nameArabic}}</span><br />


                            </div>
                            <div style="text-align: center;margin-top:5px;" v-if="isTransfer">
                                <span style="font-size:30px;font-weight:bold;color:black;">Day Transfer Report/تقرير التحويل اليومي</span><br />


                            </div>
                            <div style="text-align: center;margin-top:5px;" v-else>
                                <span style="font-size:30px;font-weight:bold;color:black;">Day End Report/تقرير نهاية اليوم</span><br />


                            </div>
                        </div>


                    </div>
                </div>


                <div style="height:120mm;margin-top:1mm;width:100%; text-align: center;">
                    <div style="width:100%;text-align:center;margin-top:2rem;">
                        <table class="table table-borderless " style="width:400px;margin-bottom:15px;">
                            <tr style="font-size:16px;">
                                <td class="" style="border-top: 0;padding-left:1px;padding-right:1px;color:black;"><span style="font-weight:bold;color:black;">From Date / </span><span style="font-weight:bold;color:black;font-size:18px">من التاريخ</span><br>{{printDetails.startDate}}</td>
                                <td style="text-align:right; border-top: 0;padding-left:1px;padding-right:1px;color:black;"><span style="font-weight:bold;color:black;">To Date / </span><span style="font-weight:bold;color:black;font-size:18px">حتي اليوم</span><br>{{printDetails.date}}</td>

                            </tr>

                        </table>

                        <table class="table table-borderless " style="width:400px;">
                            <!--<tr style="font-size:16px;" v-if="isTransfer">
                                <td class="" style="border-top: 0;padding-left:1px;padding-right:1px;color:black;"><span style="font-weight:bold;color:black;">From Time / </span><span style="font-weight:bold;color:black;font-size:18px">من وقت</span><br>{{printDetails.startTime}}</td>
                                <td style="text-align:right; border-top: 0;padding-left:1px;padding-right:1px;color:black;"><span style="font-weight:bold;color:black;">To Time / </span><span style="font-weight:bold;color:black;font-size:18px">الى وقت</span>  <br>{{printDetails.toTime}}</td>

                            </tr>-->
                            <!--<tr style="font-size:16px;" v-else>
                                <td class="" style="border-top: 0;padding-left:1px;padding-right:1px;color:black;"><span style="font-weight:bold;color:black;">From Time / </span><span style="font-weight:bold;color:black;font-size:18px">من وقت</span><br>{{printDetails.startTime}}</td>
                                <td style="text-align:right; border-top: 0;padding-left:1px;padding-right:1px;color:black;"><span style="font-weight:bold;color:black;">To Time / </span><span style="font-weight:bold;color:black;font-size:18px">الى وقت</span><br>{{printDetails.toTime}}</td>

                            </tr>-->
                            <tr style="font-size:16px;" v-if="isTransfer">

                                <td class="" style="border-top: 0;padding-left:1px;padding-right:1px;color:black;"><span style="font-weight:bold;color:black;">Transfer From / </span><span style="font-weight:bold;color:black;font-size:18px">تحويل من</span>  <br>{{printDetails.user}}</td>
                                <td style="text-align:right; border-top: 0;padding-left:1px;padding-right:1px;color:black;"><span style="font-weight:bold;color:black;">Transfer to / </span><span style="font-weight:bold;color:black;font-size:18px">حول إلى</span>   <br>{{printDetails.toUser}}</td>

                            </tr>
                            <tr style="font-size:16px;">


                                <td class="" style="border-top: 0;padding-left:1px;padding-right:1px;color:black;"><span style="font-weight:bold;color:black;">Counter#</span><span style="font-weight:bold;color:black;font-size:18px">رقم الكاونتر</span>   <br>{{printDetails.counterCode}}</td>
                                <td v-if="!isTransfer" style="text-align:right; border-top: 0;padding-left:1px;padding-right:1px;color:black;"><span style="font-weight:bold;color:black;">User</span> <span style="font-weight:bold;color:black;font-size:18px">أسم المستخدم</span>  <br>{{printDetails.user}}</td>
                            </tr>
                        </table>
                        <div style="border-top:2px solid; color:black; border-color:black !important;margin-top:15px">
                            <div class="col-sm-12" style="margin-top: 15px">
                                <span class="modal-title" id="myModalLabel" style="padding-top:3px; padding-bottom:3px;color:black !important;font-size:18px;font-weight:bold">Cash Sale & Expense Detail / <span style="font-weight:bold;color:black;font-size:18px">البيع النقدي وتفاصيل المصاريف</span> </span>

                            </div>
                            <table class="table table-borderless " style="width:400px;">
                                <tr style="font-size:16px;">
                                    <td class="" style="border-top: 0;padding-left:1px;padding-right:1px;color:black;"><span style="font-weight:bold;color:black;">Opening Cash / </span><span style="font-weight:bold;color:black;font-size:18px">فتح النقدية</span><br>{{parseFloat(printDetails.openingCash).toFixed(3).slice(0,-1)}}</td>
                                    <td style="text-align:right; border-top: 0;padding-left:1px;padding-right:3px;color:black;"><span style="font-weight:bold;color:black;">Cash Sale / </span><span style="font-weight:bold;color:black;font-size:18px">بيع نقدا</span><br>{{parseFloat(printDetails.totalCash).toFixed(3).slice(0,-1)}}</td>

                                </tr>
                                <tr style="font-size:16px;">
                                    <td class="" style="border-top: 0;padding-left:1px;padding-right:3px;color:black;"><span style="font-weight:bold;color:black;">Total Cash/  </span><span style="font-weight:bold;color:black;font-size:18px">مجموع المبالغ النقدية</span><br>{{parseFloat(printDetails.cashInHand).toFixed(3).slice(0,-1)}}</td>
                                    <td style="text-align:right; border-top: 0;padding-left:1px;padding-right:1px;color:black;"><span style="font-weight:bold;color:black;">Expense/  </span><span style="font-weight:bold;color:black;font-size:18px">نفقات</span><br>{{parseFloat(printDetails.expenseCash).toFixed(3).slice(0,-1)}}</td>

                                </tr>
                            </table>

                            <div style="color:black ;width:100%;text-align:center;margin-top:2rem;">
                                <p style="font-size:15px;font-weight:bold;color:black;">Total VAT: <span style="margin-right:20px;margin-left:10px;padding-left:20px;color:black;">{{parseFloat(printDetails.totalVat).toFixed(3).slice(0,-1)}}</span> :<span style="font-weight:bold;color:black;font-size:18px" > إجمالي ضريبة القيمة المضافة</span></p>
                                <!--<p style="font-size:15px;font-weight:bold;color:black;">Cash In Hand: <span style="margin-right:20px;margin-left:10px;padding-left:20px;color:black;">{{parseFloat(printDetails.openingCash).toFixed(3).slice(0,-1)}}</span> : <span style="font-weight:bold;color:black;font-size:18px">نقد في اليد</span></p>-->

                            </div>

                        </div>

                        <div class="row" style="border-top:1px solid; color:black;margin-top:15px">
                            <div class="col-sm-12">
                                <span class="modal-title" id="myModalLabel" style="padding-top:3px; padding-bottom:3px;padding-left:3px;font-size:18px;font-weight:bold">Bank Sale Detail /  <span style="font-weight:bold;color:black;font-size:18px">تفاصيل بيع البنك</span></span>

                            </div>
                            <table class="table table-borderless " style="width:400px;">
                                <tr style="font-size:14px ; " v-for="bank in printDetails.bankDetails" v-bind:key="bank.bankName">
                                    <td style="color: black !important; border-top:1px solid; font-weight:bold;border-color:black !important;padding:10px 0px 10px 0px"> <span>{{bank.bankName}}</span>:</td>
                                    <td style="color: black !important ;border-top:1px solid;font-weight:bold;border-color:black !important;padding:10px 0px 10px 0px">{{parseFloat(bank.totalAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                </tr>
                                <tr style="border-top: 1px solid !important; border-color: black !important">
                                    <td style="color: black;font-size:14px;padding-top:0px !important; padding-bottom:0px !important; font-weight:bold;"> <span>Bank Total /<span style="font-weight:bold;color:black;font-size:18px">  مجموع البنك</span> </span>:</td>
                                    <td style="color: black;font-size:14px;padding-top:0px !important; padding-bottom:0px !important;font-weight:bold;">{{parseFloat(printDetails.bankAmount).toFixed(3).slice(0,-1)}}</td>
                                </tr>
                            </table>


                        </div>
                        <div style="border-top: 1px solid;border-bottom: 1px solid; border-color: black !important; width: 100%; text-align: center; margin-top: 2rem;">
                            <p style="font-size:15px;font-weight:bold;color:black;">Total Sale(Cash Sale + Bank): <span style="margin-right:20px;margin-left:10px;padding-left:20px;color:black;">{{parseFloat(printDetails.getTotalSale).toFixed(3).slice(0,-1)}}</span>:<span style="font-weight:bold;color:black;font-size:18px">إجمالي البيع</span></p>

                        </div>



                    </div>

                    <div style="width:100%;">
                        <table class="table report-tbl" style="width:400px;">

                            <tr style="font-size:18px;font-weight:bold;">
                                <td colspan="3" style="text-align: right; border-bottom: 0; padding-bottom: 5px; padding-left: 1px; padding-right: 0px; color: black;">Total / المجموع </td>
                                <td colspan="2" style="padding-right:3rem; text-align: right; border-bottom: 0; padding-bottom: 1px; padding-left: 10px; padding-right: 1px; color: black;">{{parseFloat(printDetails.total).toFixed(3).slice(0,-1)}}</td>
                            </tr>
                            <tr style="font-size:18px;font-weight:bold;">
                                <td colspan="3" style="text-align: right; padding-left: 1px; padding-right: 0px; border-top: 0; padding-top: 0; border-bottom: 0; padding-bottom: 5px; color: black;">Verify Cash / تحقق من النقد :</td>
                                <td colspan="2" style="padding-right: 3rem; text-align: right; padding-left: 10px; padding-right: 1px; border-top: 0; padding-top: 0; border-bottom: 0; padding-bottom: 1px; color: black; ">{{parseFloat(printDetails.verifyCash).toFixed(3).slice(0,-1)}}</td>
                            </tr>

                            <tr style="font-size:18px;font-weight:bold;" v-if="!isTransfer">
                                <td style="text-align: right; padding-left: 1px; padding-right: 0px; border-top: 0; padding-top: 0; border-bottom: 0; padding-bottom: 5px; color: black;" colspan="3">Cash Transfer / تحويل نقدي:</td>
                                <td colspan="2" style="padding-right: 3rem; text-align: right; padding-left: 10px; padding-right: 1px; border-top: 0; padding-top: 0; border-bottom: 0; padding-bottom: 5px; color: black; ">{{parseFloat(printDetails.carryCash).toFixed(3).slice(0,-1)}}</td>
                            </tr>
                            <tr style="font-size:18px;font-weight:bold;" v-if="printDetails.carryCash>0 && printDetails.isSupervisor == false">

                                <td style="text-align: right; padding-left: 1px; padding-right: 0px; padding-top: 5px; color: black;border-top: 0;" colspan="3">Cash Received By / استلم النقد من:</td>
                                <td colspan="2" style="padding-right: 3rem; text-align: right; padding-left: 10px; padding-right: 1px; padding-top: 5px; color: black;border-top: 0; ">{{printDetails.supervisorName}}</td>

                            </tr>
                            <tr style="font-size:18px;font-weight:bold;" v-if="!isTransfer">
                                <td style="text-align: right; padding-left: 1px; padding-right: 0px; padding-top: 5px; color: black;border-top: 0;" colspan="3">Next Day Opening Cash  / فتح اليوم التالي نقدا :</td>
                                <td colspan="2" style="padding-right: 3rem; text-align: right; padding-left: 10px; padding-right: 1px; padding-top: 5px; color: black;border-top: 0; ">{{parseFloat(parseFloat(printDetails.verifyCash) - parseFloat(printDetails.carryCash)).toFixed(3).slice(0,-1)}}</td>
                            </tr>
                            <tr style="font-size:19px;font-weight:bold;" v-if="printDetails.total != printDetails.verifyCash">
                                <td style="text-align:center; color: black;border-top: 1px solid #000000;" colspan="5">Reason / سبب </td>
                            </tr>
                            <tr style="font-size:19px;" v-if="printDetails.total != printDetails.verifyCash">

                                <td style="text-align: left; padding-left: 1px; padding-right: 0px; padding-top: 5px; color: black;border-top: 0;" colspan="5">{{printDetails.creditReason}}</td>

                            </tr>

                            <!--<tr style="font-size:16px;font-weight:bold;">
                                <td style="text-align: right; padding-left: 1px; padding-right: 0px; border-top: 0; padding-top: 0; border-bottom: 0; padding-bottom: 1px; color: black;" colspan="3">Carry Cash/يحمل نقودا:</td>
                                <td colspan="2" style="padding-right: 3rem; text-align: right; padding-left: 10px; padding-right: 1px; border-top: 0; padding-top: 0; border-bottom: 0; padding-bottom: 1px; color: black; ">{{parseFloat(printDetails.carryCash).toFixed(3).slice(0,-1)}}</td>
                            </tr>-->



                        </table>
                        <table class="table report-tbl" style="width:400px;" v-if="printDetails.outStandingBalance!=0 && printDetails.outStandingBalance!='' && printDetails.outStandingBalance!=undefined">
                            <tr style="font-size:18px;font-weight:bold;">
                                <td style="text-align: right; border-bottom: 0; padding-bottom: 1px; padding-left: 1px; padding-right: 0px; color: black;">OutStanding Balance / اتزان رائع</td>
                            </tr>
                            <tr style="font-size:16px;font-weight:bold;">
                                <td colspan="7" style="text-align: center; padding-left: 1px; padding-right: 0px; border-top: 0; padding-top: 0; border-bottom: 0; padding-bottom: 1px; color: black;">Date / تاريخ:</td>
                                <td colspan="5" style="text-align: right; padding-left: 1px; padding-right: 0px; border-top: 0; padding-top: 0; border-bottom: 0; padding-bottom: 1px; color: black;">Total / مجموع:</td>
                            </tr>
                            <tr style="font-size:16px;" v-for="(po,index) in printDetails.dayWiseList" :key="index">

                                <td colspan="7" style="padding-right: 3rem; text-align: right; padding-left: 10px; padding-right: 1px; border-top: 0; padding-top: 0; border-bottom: 0; padding-bottom: 1px; color: black; ">{{po[0].date}} {{po[0].fromTime}} - {{po[0].date}} {{po[0].toTime}}</td>

                                <td colspan="5" style="padding-right: 3rem; text-align: right; padding-left: 10px; padding-right: 1px; border-top: 0; padding-top: 0; border-bottom: 0; padding-bottom: 1px; color: black; ">{{parseFloat(po[0].totalCash).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                            </tr>
                            <tr style="font-size:18px;font-weight:bold; border-top:2px solid #000000" >
                                <td colspan="6" style="text-align: right; padding-left: 1px; padding-right: 0px; border-top: 0; padding-top: 0; border-bottom: 0; padding-bottom: 1px; color: black;">Total OutStanding Balance / إجمالي الرصيد المتبقي:</td>
                                <td colspan="6" style="padding-right: 3rem; text-align: right; padding-left: 10px; padding-right: 1px; border-top: 0; padding-top: 0; border-bottom: 0; padding-bottom: 1px; color: black; ">{{printDetails.outStandingBalance}}</td>
                            </tr>
                        </table>
                    </div>


                    <!--<div style="width:100%;height:30mm">

                    </div>-->


                </div>
            </div>
        </div>
    </div>

</template>

<script>
    import moment from "moment";
    import axios from 'axios'
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
            }, getTimeOnly: function (x) {
                return moment(x).format('HH:mm ');
            },
            printInvoice: function () {

                var root = this;
                // this.$htmlToPaper('purchaseInvoice');
                this.htmlData.htmlString = this.$refs.mychildcomponent.innerHTML;
                //  var html1 = this.$refs.mychildcomponent.innerHTML;
                //  var data = { html: html1 }
                //
                var printerName = localStorage.getItem('PrinterName')
                var form = new FormData();
                form.append('htmlString', this.htmlData.htmlString);
                form.append('PrintType', 'invoice');
                form.append('PrinterName', printerName);
                //this.$htmlToPaper('purchaseInvoice');
                //axios.post('http://localhos:7013/print/from-pdf', form);
                //axios.post('http://127.0.0.1:7013/print/from-pdf', form);
                //alert();
                //var root = this;
                var isBlindPrint = localStorage.getItem("IsBlindPrint")
                
                if (isBlindPrint === 'true') {
                    axios.post('http://127.0.0.1:7014/print/from-pdf', form).then(function () {

                        if (root.isTransfer) {
                            root.logout();
                        }
                        else {
                            
                            root.$router.go();
                        }
                        //

                    }).catch(error => {
                        console.log(error)
                        root.$router.go();
                    });
                    //if (root.isTouchScreen === true) {
                    //    root.$router.go('/TouchInvoice')
                    //}
                }
                else {
                    this.$htmlToPaper('purchaseInvoice', options, () => {
                        if (root.isTransfer) {
                            root.logout();
                        }
                        else {
                            root.$router.go();
                        }

                    });

                }



            },
        },
        mounted: function () {

            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
            this.isHeaderFooter = localStorage.getItem('IsHeaderFooter');
            var root = this;
            this.printDetails = this.printDetail;
            
            this.headerFooters = this.headerFooter;
            setTimeout(function () {
                root.printInvoice();
            }, 125);
        },

    }
</script>

