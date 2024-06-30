<template>
    <div>
        <div hidden  ref="mychildcomponent" id='JournalVoucher' class="col-md-12" style="color:black !important">
            <!--HEADER-->
            <div  style="height:45mm;border:2px solid #000000;background-color:white !important" v-if="isHeaderFooter=='true'">
                <div class="col-md-12">
                    <div class="row p-1" style="height:35mm;background-color:white">
                        <div class="col-md-4  my-5" style="padding:0px !important; margin:0 !important">
                            <img :src="headerFooters.company.logoPath" style="width:auto;max-width:300px; max-height:100px;  margin:0 !important;padding:5px">
                        </div>
                        <div class="col-md-8 ">
                            <table class="text-center">
                                <tr>
                                    <td>
                                        <p>

                                            <span style="font-size:27px;color:black !important;font-weight:bold;">{{headerFooters.company.nameEnglish}}</span><br />
                                            <span style="font-size:16px;color:black !important;font-weight:bold;">{{headerFooters.company.addressEnglish}}</span><br />
                                            <span style="font-size:18px;color:black !important;font-weight:bold;float:left">NTN :&nbsp;&nbsp;&nbsp; {{headerFooters.company.vatRegistrationNo}}</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <span style="font-size:18px;color:black !important;font-weight:bold;float:right">STR:&nbsp;&nbsp;&nbsp;   {{headerFooters.company.companyRegNo}}</span>
                                        </p>
                                    </td>
                                </tr>
                            </table>
                        </div>

                        <!--<div class="col-md-4 ">
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
        </div>-->
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <p style="text-align: center; margin: 0px;; padding: 0px; line-height: 1; ">
                                <span style="font-size:25px;color:black !important;font-weight:bold;padding-bottom:5px !important">Journal Voucher</span>
                            </p>
                        </div>
                    </div>
                </div>
              
            </div>
            <div style="height:60mm;" v-else></div>

            <div style="height:30mm;margin-top:1mm; border:2px solid #000000; background-color:white !important">
                <div class="row">
                    <div class="col-md-12 ml-2 mr-2" style="height:6mm;font-size:16px;">
                        <div>
                            <!--Row 1-->
                            <div class="row">
                                <div class="col-md-6" style="display:flex;">
                                    <div style="width:28%; font-weight:bolder;text-align:right;color:black !important;">Voucher No:</div>
                                    <div style="width:50%; text-align:center;color:black !important;font-weight:bold;"> {{list.voucherNumber}}</div>
                                    <div style="width:22%;font-weight:bolder;font-size:15px !important;color:black !important;" v-if="arabic=='true'">: رقم القسيمة </div>
                                </div>
                                <div class="col-md-6" style="display:flex;">
                                    <div style="width:28%; font-weight:bolder;text-align:right;color:black !important;"> Date:</div>
                                    <div style="width:50%; text-align:center;color:black !important;font-weight:bold;">{{list.date}}</div>
                                    <div style="width:22%;font-weight:bolder;font-size:15px !important;color:black !important;" v-if="arabic=='true'">:تاريخ</div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-12" style="display:flex;">
                                    <div style="width:13%; font-weight:bolder;text-align:right;color:black !important;">Narration:</div>
                                    <div style="width:74%; text-align:left;color:black !important;font-weight:bold;padding-left:10px"> {{list.narration}}</div>
                                    <div style="width:13%;font-weight:bolder;font-size:15px !important;color:black !important;" v-if="arabic=='true'">:السرد</div>
                                </div>

                            </div>
                            <div class="row">
                                <div class="col-md-12" style="display:flex;">
                                    <div style="width:13%; font-weight:bolder;text-align:right;color:black !important;">Comments:</div>
                                    <div style="width:74%; text-align:left;color:black !important;font-weight:bold;padding-left:10px"> {{list.comments}}</div>
                                    <div style="width:13%;font-weight:bolder;font-size:15px !important;color:black !important;" v-if="arabic=='true'">:تعليقات</div>
                                </div>

                            </div>

                        </div>
                    </div>
                </div>
            </div>

            <!--INFORMATION-->
            <div style="border:2px solid !important; background-color:white !important">
                <div class="row p-1 " style="height: 280mm;">
                    <div class="col-md-12 ">
                        <table class="table">
                            <tr class="heading" style="font-size:17px !important;padding-top:5px;" v-if="invoicePrint == 'العربية'">
                                <th style="width:2%;padding-top:3px !important; padding-bottom:3px !important;border-top:0px !important" class="text-center">#</th>
                                <th style="width:30%;">{{ $t('JvAddLineItem.Account') }}</th>
                                <th style="width:30%;" class="text-center">{{ $t('JvAddLineItem.description') }}</th>

                                <th style="width:19%;" class="text-center">Payment Mode</th>
                                <th style="width:19%;" class="text-center">Payment Method</th>
                                <th style="width:19%;" class="text-center">Cheque No</th>
                                <th style="width:19%;" class="text-center">{{ $t('JvAddLineItem.Debit') }}</th>
                                <th style="width:19%;" class="text-center">{{ $t('JvAddLineItem.Credit') }}</th>
                            </tr>

                            <tr class="heading" style="font-size:17px !important;padding-top:5px;" v-else>
                                <th style="width:2%;padding-top:3px !important; padding-bottom:3px !important;border-top:0px !important" class="text-center">#</th>
                                <th style="width:20%;" class="text-center">{{ $t('JvAddLineItem.Account') }}</th>
                                <th style="width:28%;" class="text-center">{{ $t('JvAddLineItem.description') }}</th>
                                <th style="width:10%;" class="text-center">Payment Mode</th>
                                <th style="width:10%;" class="text-center">Payment Method</th>
                                <th style="width:10%;" class="text-center">Cheque No</th>

                                <th style="width:10%;" class="text-center">{{ $t('JvAddLineItem.Debit') }}</th>
                                <th style="width:10%;" class="text-center">{{ $t('JvAddLineItem.Credit') }}</th>

                            </tr>

                            <tr style="font-size:16px;" v-for="(item, index) in list.journalVoucherItems" v-bind:key="item.id">
                                <td class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;border-top:0 !important; border-bottom:0 !important;color:black !important;">{{index+1}}</td>
                                <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{item.accountName}}</td>
                                <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{item.description}}</td>
                                <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{item.paymentModes}}</td>
                                <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{item.paymentMethods}}</td>
                                <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{item.chequeNo}}</td>
                                <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{item.debit}}</td>
                                <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{item.credit}}</td>
                            </tr>
                            <tfoot>
                                <tr style="font-weight:bold;font-size:18px">
                                    <td colspan="3">
                                        <h6 class="m-0 text-right" style="font-weight:bold;font-size:18px">Total Debit: &nbsp;&nbsp;&nbsp;{{currency}} {{$roundOffFilter(totalDebitAmounts) }}</h6>
                                    </td>
                                    <td colspan="2">
                                        <h6 class="m-0 text-right" style="font-weight:bold;font-size:18px">Total Credit: &nbsp;&nbsp;&nbsp;{{currency}} {{$roundOffFilter(totalCreditAmount)  }}</h6>
                                    </td>
                                    <td colspan="3">
                                        <h6 class="m-0 text-right" style="font-weight:bold;font-size:18px">Total: &nbsp;&nbsp;&nbsp;{{currency}}  {{$roundOffFilter((total))  }}</h6>
                                    </td>

                                </tr>

                            </tfoot>
                        </table>
                    </div>
                </div>
                <div class="col-12" style=" background-color:white;font-size:16px;padding-right:10px">
                    <div class="col-12">
                        <table class="table text-center">
                            <tr>
                                <td style="width: 33%;text-decoration-line: overline;border:0px;color:black;font-weight:bold">
                                    Prepared By
                                </td>
                                <td style="width: 33%;text-decoration-line: overline;border:0px;color:black;font-weight:bold">
                                    Approved By
                                </td>
                                <td style="width: 33%;text-decoration-line: overline;border:0px;color:black;font-weight:bold">
                                    Received By

                                </td>
                            </tr>


                        </table>
                        <table class="table text-center">

                            <tr>
                                <td style="width: 20%;border:0px;color:black">
                                </td>
                                <td style="width: 60%;border:0px;color:black;font-weight:bold;font-size:16px">
                                    Tel: &nbsp; &nbsp; &nbsp;{{headerFooters.company.phoneNo}} &nbsp; &nbsp; &nbsp; Email:&nbsp; &nbsp; &nbsp;{{headerFooters.company.email}}
                                </td>
                                <td style="width: 20%;border:0px;color:black">
                                </td>
                            </tr>

                        </table>


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
    import clickMixin from '@/Mixins/clickMixin'
    export default {
        mixins: [clickMixin],
        props: ['printDetails', 'headerFooter'],
        data: function () {
            return {
                qrValue: "",
                IsExpenseAccount: false,
                isHeaderFooter: '',
                currency: '',
                invoicePrint: '',
                IsDeliveryNote: '',
                arabic: '',
                english: '',
                userName: '',

                list: {
                    voucherNo: '',
                    date: '',
                    narration: '',
                    journalVoucherItems:
                        [

                        ]
                },
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
        filters: {
            roundOffFilter: function (value) {

                return parseFloat(value).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,");
            }
        },
        computed: {
           
            totalDebitAmounts: function () {
                return this.list.journalVoucherItems.reduce(function (a, c) { return a + Number((c.debit ) || 0) }, 0)
            },
            totalCreditAmount: function () {
                return this.list.journalVoucherItems.reduce(function (a, c) { return a + Number((c.credit ) || 0) }, 0)
            },
            total: function () {
                return this.list.journalVoucherItems.reduce(function (a, c) { return a + Number((c.debit-c.credit ) || 0) }, 0)
            },
           


        },
        methods: {

            paymentMode: function (x) {
                if (this.invoicePrint == 'العربية') {

                    if (x == 0) {
                        return 'السيولة النقدية';
                    }
                    if (x == 1) {
                        return 'مصرف';
                    }
                    if (x == 5) {
                        return 'يتقدم';
                    }



                }
                else {

                    if (x == 0) {
                        return 'Cash';
                    }
                    if (x == 1) {
                        return 'Bank';
                    } if (x == 5) {
                        return 'Advance';
                    }

                }
            },

            printInvoice: function () {

                this.$htmlToPaper('JournalVoucher', options, () => {

                });
            },

        },
        created: function () {
            if (this.printDetails.journalVoucherItems.length > 0) {
                this.list = this.printDetails;
                this.headerFooters = this.headerFooter;

            }
        },
        mounted: function () {


            this.currency = localStorage.getItem('currency');
            this.invoicePrint = localStorage.getItem('InvoicePrint');
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
            this.isHeaderFooter = localStorage.getItem('IsHeaderFooter');
            this.userName = localStorage.getItem('FullName');
            var root = this;
            if (this.printDetails.journalVoucherItems.length > 0) {
                this.list.date = moment().format('DD MMM YYYY');
                setTimeout(function () {
                    root.printInvoice();
                }, 125)

            }
        },

    }
</script>