<template>
    <div>
        <div ref="mychildcomponent" hidden id='inventoryDetailReport' class="col-md-7">
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
                                <span style="font-size:20px;color:black !important;font-weight:bold;padding-bottom:5px !important">{{ $t('Accountledgerdetailreport.Accountledgerdetailreport') }}</span>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>            <!--INFORMATION-->
            <!--<div v-else style="height: 40mm;">

            </div>-->
            <!--INFORMATION-->
            <div style="height:20mm;margin-top:1mm; border:2px solid #000000;">
                <div class="row">
                    <div class="col-md-12 ml-2 mr-2">
                        <table class="table table-borderless">
                            <!--Row 1-->
                            <tr>
                                <td class="pl-0 pr-0 pt-0 pb-0" style="width:14%; font-weight:bolder;text-align:right;color:black !important;font-size:14px !important;">From Date:</td>
                                <td class="pl-0 pr-0 pt-0 pb-0" style="width:25%; text-align:center;color:black !important;font-weight:bold;font-size:14px !important;">{{fromDate}}</td>
                                <td class="pl-0 pr-0 pt-0 pb-0" style="width:11%;font-weight:bolder;font-size:14px !important;color:black !important;" v-if="arabic=='true'">:من التاريخ</td>

                                <td class="pl-0 pr-0 pt-0 pb-0" style="width:14%; font-weight:bolder;text-align:right;color:black !important;font-size:14px !important;">To Date:</td>
                                <td class="pl-0 pr-0 pt-0 pb-0" style="width:25%; text-align:center;color:black !important;font-weight:bold;font-size:14px !important;">{{toDate}}</td>
                                <td class="pl-0 pr-0 pt-0 pb-0" style="width:11%;font-weight:bolder;font-size:14px !important;color:black !important;" v-if="arabic=='true'">  :حتي اليوم</td>
                            </tr>









                        </table>
                    </div>

                </div>
            </div>     
            <div class="col-md-12 ">
                <div class="row mt-2">
                    <div class="col-md-12 mt-3" v-for="ledger in transactionList" v-bind:key="ledger.name">
                        <h6 class="col-md-12 pt-2" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'" style="font-size:16px;">
                            {{ledger.name}}
                            <span class="pl-3">
                                {{ $t('AccountLedger.OpeningBalance') }}  :{{ledger.opening>0?'Dr':'Cr'}} {{nonNegative(ledger.opening) }}
                            </span>

                        </h6>
                        <div>
                            <div class="table-responsive" style="font-size:14px;">
                                <table class="table table-striped table-hover table_list_bg">
                                    <thead class="">
                                        <tr>
                                            <th>#</th>
                                            <th>
                                                {{ $t('AccountLedger.TransactionDate') }}
                                            </th>
                                            <th>
                                                {{ $t('AccountLedger.DocumentDate') }}
                                            </th>
                                            <th>
                                                {{ $t('AccountLedger.DocumentCode') }}
                                            </th>
                                            <th>
                                                {{ $t('AccountLedger.Narration') }}
                                            </th>
                                            <th v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ $t('AccountLedger.Debit') }}
                                            </th>
                                            <th v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ $t('AccountLedger.Credit') }}
                                            </th>
                                            <th v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ $t('AccountLedger.Balance') }}
                                            </th>
                                            <!--<th>
                                    {{ $t('AccountLedger.Details') }}
                                </th>-->
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr v-for="(account,index) in ledger.trialBalanceModel" v-bind:key="account.id">
                                            <td>
                                                {{index+1}}<br />
                                            </td>
                                            <td>
                                                {{account.transactionDate}}
                                            </td>
                                            <td>
                                                {{account.documentDate}}
                                            </td>
                                            <td>
                                                {{account.code}}
                                            </td>
                                            <td>
                                                {{getTransactionType(account.description)}}
                                            </td>
                                            <td v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{  nonNegative(account.debit)}}
                                            </td>
                                            <td v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{nonNegative(account.credit)}}
                                            </td>
                                            <td v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{account.total>0?'Dr':'Cr'}}  {{account.total}}
                                            </td>
                                        </tr>


                                    </tbody>
                                </table>
                            </div>
                        </div>
                        <h6 class="col-md-12" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-right' : 'text-left'">

                            <span>{{ $t('AccountLedger.ClosingBalance') }} :</span> <span class="pl-3">{{ledger.closing>0?'Dr':'Cr'}} {{nonNegative(ledger.closing) }}</span>


                        </h6>
                    </div>


                </div>

            </div>
        </div>
    </div>

</template>

<script>
    import moment from "moment";
    export default {
        props: ['transactionList', 'isShown', 'formName', 'isPrint', 'dates', 'headerFooter', 'fromDate', 'toDate', ''],
        data: function () {
            return {
               
                fDate: '',
                tDate: '',
                headerFooters: {
                    footerEn: '',
                    footerAr: '',
                    company: ''
                },
                Print: false,
                render: 0,
                arabic: '',
                english: '',
            }
        },
        mounted: function () {

            this.fDate = this.fromDate;
            this.tDate = this.toDate;
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
            this.isHeaderFooter = localStorage.getItem('IsHeaderFooter');
            var root = this;

            this.headerFooters = this.headerFooter
            root.Print = root.isPrint;
            if (this.transactionList.length > 0 && root.Print) {

                setTimeout(function () {
                    root.printInvoice();
                }, 125)
            }
        },
        methods: {
            getTransactionType(transactionType) {
                if (transactionType == 'StockOut') return 'Stock Out'
                else if (transactionType == 'JournalVoucher') return 'Journal Voucher'
                else if (transactionType == 'BankPay') return 'Bank Pay'
                else if (transactionType == 'ExpenseVoucher') return 'Expense Voucher'
                else if (transactionType == 'Expense') return 'Expense'
                else if (transactionType == 'CashPay') return 'Cash Pay'

                else if (transactionType == 'BankReceipt') return 'Bank Receipt'
                else if (transactionType == 'StockIn') return 'Stock In'
                else if (transactionType == 'SaleInvoice') return 'Sale Invoice'
                else if (transactionType == 'PurchaseReturn') return 'PurchaseReturn'
                else if (transactionType == 'PurchasePost') return 'Purchase'
                else if (transactionType == 'CashReceipt') return 'Cash Receipt'
                else {
                    return transactionType;
                }

            },
            GetBalance: function (debit, credit, opening, index) {
                
                if (index == 0) {
                    this.balance = 0;
                    if (debit == 0) {
                        this.balance = parseFloat((opening)) + (parseFloat((debit)) - parseFloat((credit)));
                        return this.nonNegative(this.balance);
                    }
                    else {
                        this.balance = parseFloat((opening)) + (parseFloat((debit)) - parseFloat((credit)));
                        return this.nonNegative(this.balance);
                    }


                }
                else {
                    if (debit == 0) {
                        this.balance = this.balance + (parseFloat((debit)) - parseFloat((credit)));
                        return this.nonNegative(this.balance);
                    }
                    else {
                        this.balance = this.balance + (parseFloat((debit)) - parseFloat((credit)));
                        return this.nonNegative(this.balance);
                    }

                }


            },

            nonNegative: function (value) {
                return parseFloat(Math.abs(value)).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,");
            },
            convertDate: function (x) {
                return moment(x).format('DD MMM YYYY');
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


                        link.setAttribute('download', 'Account Ledger Detail Report ' + date + '.pdf');
                        document.body.appendChild(link);
                        link.click();

                        root.$emit('close');
                    });
            }
        }
    }
</script>


<style scoped>
    #font11 {
        font-size: 11px;
        line-height: 0;
    }

    #font16 {
        font-size: 16px;
    }
</style>