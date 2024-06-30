<template>
    <div>
        <div hidden id='LeaderAccount' class="col-md-7">
            <!--HEADER-->
            <div class="col-md-12" style="height:45mm;border:2px solid #000000;">
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
                        <p style="text-align: center;  padding-top: 10px; font-size: 20px; line-height: 1; ">
                            <span style="font-size:20px;font-weight:bold">{{ $t('Accountledgerdetailreport.Accountledgerdetailreport') }}</span>
                        </p>
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
              
            </div>           
            <div class="border border-dark col-md-12 my-1 " style="height:25mm">
                <div class="row" >
                    <div class="col-md-6" style="display:flex;">
                        <div style="width:28%; font-weight:bolder;text-align:right;color:black !important;">From Date:</div>
                        <div style="width:50%; text-align:center;font-weight:bold;color:black !important;">{{fromDate}}</div>
                        <div style="width:22%;font-weight:bolder;color:black !important;font-size:15px !important" v-if="arabic=='true'">:من التاريخ</div>
                    </div>
                    <div class="col-md-6" style="display:flex;">
                        <div style="width:28%;font-weight:bolder;text-align:right;color:black !important;">To Date:</div>
                        <div style="width:50%;text-align:center;font-weight:bold;color:black !important;"><span>{{toDate}}</span> </div>
                        <div style="width:22%;font-weight:bolder;color:black !important; padding-right:20px;font-size:15px !important" v-if="arabic=='true'">
                            :حتي اليوم
                        </div>
                    </div>
                  
                </div>
            </div>
            <div class="col-md-12 ">
                <div class="row mt-2">
                    <div class="col-md-12 mt-3"  v-for="ledger in transactionList" v-bind:key="ledger.name">
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
                                                {{account.total>0?'Dr':'Cr'}}  {{nonNegative(account.total)}}
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
        props: ['transactionList', 'isShown', 'formName', 'isPrint', 'dates', 'headerFooter', 'fromDate', 'toDate'],
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
                this.$htmlToPaper('LeaderAccount');
            }
        }
    }
</script>

