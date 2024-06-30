<template>
    <div>
        <div hidden id='purchaseInvoice' class="col-md-12">
            <!--page1-->
            <div>
                <!--HEADER-->
                <div class="col-md-12" style="height:45mm;border:2px solid #000000;" >
                    <div class="row" style="height:35mm">
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
                    <div class="row">
                        <div class="col-md-12" style="margin-bottom:10px !important;height:10mm">
                            <p style="text-align: center; margin: 0px; padding: 0px; line-height: 1; ">
                                <span style="font-size:25px;color:black !important;font-weight:bold;padding-bottom:5px !important">Bank Transaction Report</span>
                            </p>
                        </div>
                    </div>
                </div>
                <!--<div style="height:60mm;" v-else></div>-->

                <div style="height:22mm;margin-top:1mm; border:2px solid #000000;">
                    <div class="row mt-3">
                        <div class="col-md-12 ml-2 mr-2" style="font-size:16px;">
                            <div class="row">
                                <div class="col-md-6" style="display:flex;">
                                    <div style="width:28%;font-weight:bolder;text-align:right;color:black !important;">From Date:</div>
                                    <div style="width:50%; text-align:center;color:black !important;font-weight:bold;">{{getDate(fromDate)}}</div>
                                    <div style="width:22%;font-weight:bolder; padding-right:20px;font-size:15px !important;color:black !important;" v-if="arabic=='true'">:تاريخ</div>
                                </div>
                                <div class="col-md-6" style="display:flex;">
                                    <div style="width:28%; font-weight:bolder;text-align:right;color:black !important;">To Date.:</div>
                                    <div style="width:50%; text-align:center;font-weight:bold;color:black !important;">{{getDate(toDate)}}</div>
                                    <div style="width:22%;font-weight:bolder;color:black !important;font-size:15px !important" v-if="arabic=='true'" >
                                        : تاريخ الاستحقاق
                                    </div>
                                </div>
                            </div>
                            <div class="row" v-if="bankInOut==='In/Out'" >
                                <div class="col-md-12" style="display:flex;">
                                    <div style="width:28%;font-weight:bolder;text-align:right;color:black !important;">Opening Balanace:</div>
                                    <div style="width:50%;text-align:center;font-weight:bold;color:black !important;"><span>{{ledgers.openingBalance>0?'Dr':'Cr'}} {{nonNegative(ledgers.openingBalance) }}</span> </div>
                                    <div style="width:22%;font-weight:bolder;color:black !important; padding-right:20px;font-size:15px !important" v-if="arabic=='true'">
                                        :الرصيد الافتتاحي
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <!--INFORMATION-->
                <div>
                    <div class="row ">
                        <div class="col-md-12 mt-4 ">
                            <table class="table">
                                <tr class="heading" style="font-size:18px !important;padding-top:5px;border-bottom:1px solid" v-if="invoicePrint == 'العربية'">
                                    <th style="width:2%;padding-top:3px !important; padding-bottom:3px !important;border-top:0px !important">#</th>
                                    <th class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Date</th>
                                    <th class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Document No</th>
                                    <th class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Transaction Type</th>
                                    <th class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Description</th>
                                    <th class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Bank</th>
                                    <th class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;" v-if="bankInOut=='In/Out'">Debit</th>
                                    <th class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;" v-if="bankInOut=='In/Out'">Credit</th>
                                    <th class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Amount</th>
                                </tr>
                                <tr class="heading" style="font-size:18px;border-bottom:1px solid;padding-bottom:15px" v-else>
                                    <th style="width:2%;padding-top:3px !important; padding-bottom:3px !important;border-top:0px !important">#</th>
                                    <th class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Date</th>
                                    <th class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Document No</th>
                                    <th class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Transaction Type</th>
                                    <th class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Description</th>
                                    <th class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Bank</th>
                                    <th class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;" v-if="bankInOut=='In/Out'">Debit</th>
                                    <th class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;" v-if="bankInOut=='In/Out'">Credit</th>
                                    <th class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Amount</th>
                                </tr>


                                <tr style="font-size:17px;font-weight:bold;" v-for="(item, index) in list" v-bind:key="item.id">
                                    <td class="text-center" style="color:black !important;">{{index+1}}</td>
                                    <td class="text-center" style="color:black !important;">{{item.date }}</td>
                                    <td class="text-center" style="color:black !important;">{{item.documentNo}}</td>
                                    <td class="text-center" style="color:black !important;">{{getTransactionType(item.transactionType)}}</td>
                                    <td class="text-center" style="color:black !important;">{{item.description}}</td>
                                    <td class="text-center" style="color:black !important;">  {{item.bankName==null || item.bankName==''  ?'Bank':item.bankName}}</td>
                                    <td class="text-center" style="color:black !important;" v-if="bankInOut==='In' || bankInOut==='In/Out'">{{bankInOut!='In/Out'?'Dr':''}}  {{ item.debit}}</td>
                                    <td class="text-center" style="color:black !important;" v-if="bankInOut==='Out' || bankInOut==='In/Out'">{{bankInOut!='In/Out'?'Cr':''}} {{ item.credit}}</td>
                                    <td class="text-center" style="color:black !important;" v-if="bankInOut==='In/Out'"> {{item.amount>0?'Dr':'Cr'}} {{nonNegative(item.amount) }}</td>
                                </tr>
                                <tr style="font-size:15px;font-weight:bold;background-color:azure">

                                    <td colspan="6" class="text-center" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-right' : 'text-left'">
                                        <h6>{{ $t('BankTransactionReportPrint.ClosingBalance') }}:</h6>
                                    </td>

                                    <td v-if="bankInOut==='In' || bankInOut==='In/Out'" class="text-center" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-right' : 'text-left'">
                                        <h6>Dr {{nonNegative(ledgers.totalDebit)}}</h6>
                                    </td>
                                    <td v-if="bankInOut==='Out' || bankInOut==='In/Out'" class="text-center" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-right' : 'text-left'">
                                        <h6>Cr {{nonNegative(ledgers.totalCredit)}}</h6>
                                    </td>

                                    <td v-if="bankInOut==='In/Out'" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-right' : 'text-left'">
                                        <h6>{{ledgers.runningBalance>0?'Dr':'Cr'}} {{nonNegative(ledgers.runningBalance) }}</h6>
                                    </td>

                                </tr>

                                <!--<tr style="font-size:15px;font-weight:bold;" >
                                    <td colspan="7" class="text-center" style="padding-top:60px !important; padding-bottom:3px !important;border-top:0 !important; border-bottom:0 !important;color:black !important;">Total</td>

                                    <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 60px !important; padding-bottom: 3px !important;color:black !important;">{{(parseFloat(totalAmount)).toFixed(2).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                </tr>-->


                            </table>
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
    import clickMixin from '@/Mixins/clickMixin'
    export default {
        mixins: [clickMixin],
        props: ['printDetails', 'headerFooter', 'bankInOut', 'fromDate', 'toDate', 'ledger'],

        data: function () {
            return {
                ledgers: {
                    bankList: [],
                    openingBalance: 0,
                    runningBalance: 0,
                    totalCredit: 0,
                    totalDebit: 0,
                },

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
        filters: {
            toWords: function (value) {
                var converter = require('number-to-words');
                if (!value) return ''
                return converter.toWords(value);
            }
        },
        computed: {

            totalAmount: function () {
                if (this.bankInOut == 'In') {
                    return this.list.reduce(function (a, c) { return a + Number((c.debit) || 0) }, 0)

                }
                else if (this.bankInOut == 'Out') {
                    return this.list.reduce(function (a, c) { return a + Number((c.credit) || 0) }, 0)

                }
                else {
                    return this.list.reduce(function (a, c) { return a + Number((c.amount < 0 ? c.amount * -1 : c.amount) || 0) }, 0)
                }
            },

        },


        methods: {
            nonNegative: function (value) {
                return parseFloat(Math.abs(value)).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,");
            },
            getTransactionType(transactionType) {
                if (transactionType == 'StockOut') return this.$t('BankTransactionReportPrint.StockOut')
                if (transactionType == 'JournalVoucher') return 'Journal Voucher'
                if (transactionType == 'BankPay') return 'Bank Pay'
                if (transactionType == 'ExpenseVoucher') return 'Expense Voucher'
                if (transactionType == 'Expense') return 'Expense'

                if (transactionType == 'BankReceipt') return 'Bank Receipt'
                if (transactionType == 'StockIn') return this.$t('BankTransactionReportPrint.StockIn')
                if (transactionType == 'SaleInvoice') return this.$t('BankTransactionReportPrint.SaleInvoice')
                if (transactionType == 'PurchaseReturn') return this.$t('BankTransactionReportPrint.PurchaseReturn')
                if (transactionType == 'PurchasePost') return this.$t('BankTransactionReportPrint.Purchase')
                if (transactionType == 'CashReceipt') return this.$t('BankTransactionReportPrint.Cash')
            },

            getDate: function (date) {
                return moment(date).format('l');
            },
            printInvoice: function () {

                this.$htmlToPaper('purchaseInvoice', options, () => {




                });
            },



        },
        mounted: function () {
            
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
            this.isHeaderFooter = localStorage.getItem('IsHeaderFooter');
            this.invoicePrint = localStorage.getItem('InvoicePrint');
            var root = this;
            this.ledgers = this.ledger;
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

