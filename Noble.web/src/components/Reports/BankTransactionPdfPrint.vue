<template>
    <div>
        <div ref="mychildcomponent" hidden id='purchaseInvoice' class="col-md-12">
            <!--page1-->
            <div>
                <!--HEADER-->
                <div class="col-md-12">
                    <table class="table table-borderless">
                        <tr>
                            <td style="width:36%;" class="text-left pt-0 pb-0 pl-0 pr-0">
                                <p class="mb-0">
                                    <span style="font-size:23px;color:black !important;font-weight:bold;">{{headerFooters.company.nameEnglish}}</span><br />
                                    <span style="font-size:15px;color:black !important;font-weight:bold;">{{headerFooters.company.categoryEnglish}}</span><br />
                                    <span style="font-size:14px;color:black !important;font-weight:bold;">VAT No.: {{headerFooters.company.vatRegistrationNo}}</span><br />
                                    <span style="font-size:14px;color:black !important;font-weight:bold;">Cr No.:{{headerFooters.company.companyRegNo}}</span><br />
                                    <span style="font-size:13px;color:black !important;font-weight:bold;">
                                        Tel: {{headerFooters.company.phoneNo}}
                                    </span>
                                </p>
                            </td>
                            <td style="width:26%;" class="text-center pt-0 pb-0 pl-0 pr-0">
                                <img :src="headerFooters.company.logoPath" style="width:auto;max-width:300px; max-height:100px; padding:5px !important; margin:0 !important">
                            </td>
                            <td style="width:38%;" class="pt-0 pb-0 pl-0 pr-0">
                                <p class="text-right mb-0" v-if="arabic=='true'">
                                    <span style="font-size:23px;color:black !important;font-weight:bold;">{{headerFooters.company.nameArabic}}.</span><br />
                                    <span style="font-size:15px;color:black !important;font-weight:bold;">{{headerFooters.company.categoryArabic}}</span><br />
                                    <span style="font-size:14px;color:black !important;font-weight:bold;">رقم ضريبة القيمة المضافة: {{headerFooters.company.vatRegistrationNo}}</span><br />
                                    <span style="font-size:14px;color:black !important;font-weight:bold;">رقم السجل التجاري :{{headerFooters.company.companyRegNo}}</span><br />
                                    <span style="font-size:13px;color:black !important;font-weight:bold;">
                                        هاتف: {{headerFooters.company.phoneNo}}:
                                    </span>
                                </p>
                            </td>
                        </tr>

                        <tr>

                            <td style="width:100%;" class="pt-0 pb-0 pl-0 pr-0" colspan="3">
                                <div style="text-align: center;">
                                    <span style="font-size:20px;color:black !important;font-weight:bold;padding-bottom:5px !important">Bank Transaction Report</span>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>                <!--<div style="height:60mm;" v-else></div>-->
                <div style="height:15mm;margin-top:1mm; border:2px solid #000000;">
                    <div class="row">
                        <div class="col-md-12 ml-2 mr-2">
                            <table class="table table-borderless">
                                <!--Row 1-->
                                <tr>
                                    <td class="pl-0 pr-0 pt-0 pb-0" style="width:14%; font-weight:bolder;text-align:right;color:black !important;font-size:14px !important;">From Date:</td>
                                    <td class="pl-0 pr-0 pt-0 pb-0" style="width:25%; text-align:center;color:black !important;font-weight:bold;font-size:14px !important;">{{getDate(fromDate)}}</td>
                                    <td class="pl-0 pr-0 pt-0 pb-0" style="width:11%;font-weight:bolder;font-size:14px !important;color:black !important;" >:من التاريخ</td>

                                    <td class="pl-0 pr-0 pt-0 pb-0" style="width:14%; font-weight:bolder;text-align:right;color:black !important;font-size:14px !important;">To Date:</td>
                                    <td class="pl-0 pr-0 pt-0 pb-0" style="width:25%; text-align:center;color:black !important;font-weight:bold;font-size:14px !important;">{{getDate(toDate)}}</td>
                                    <td class="pl-0 pr-0 pt-0 pb-0" style="width:11%;font-weight:bolder;font-size:14px !important;color:black !important;">:حتى الآن</td>
                                </tr> 
    <!--Row 1-->
                                <tr v-if="bankInOut==='In/Out'">
                                    <td class="pl-0 pr-0 pt-0 pb-0" style="width:14%; font-weight:bolder;text-align:right;color:black !important;font-size:14px !important;">Opening Balanace:</td>
                                    <td class="pl-0 pr-0 pt-0 pb-0" style="width:25%; text-align:center;color:black !important;font-weight:bold;font-size:14px !important;">{{ledgers.openingBalance>0?'Dr':'Cr'}} {{nonNegative(ledgers.openingBalance) }}</td>
                                    <td class="pl-0 pr-0 pt-0 pb-0" style="width:11%;font-weight:bolder;font-size:14px !important;color:black !important;"> :الرصيد الافتتاحي </td>

                                   
                                </tr>
                            </table>
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
                
                var form = new FormData();
                form.append('htmlString', this.$refs.mychildcomponent.innerHTML);
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.post('/Report/PrintPdf', form, { headers: { "Authorization": `Bearer ${token}` }, responseType: 'blob' })
                    .then(function (response) {
                        const url = window.URL.createObjectURL(new Blob([response.data]));
                        const link = document.createElement('a');
                        link.href = url;
                        var date = moment().format('DD MMM YYYY');


                        link.setAttribute('download', 'Bank Transaction Report ' + date + '.pdf');
                        document.body.appendChild(link);
                        link.click();

                    });
            }



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

