<template>
    <div v-if="isValid('CanViewAccountLedger')"
        v-bind:style="$i18n.locale == 'ar' ? languageChange('en') : languageChange('ar')">
        <div v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
            <div class="card">
                <div class="card-header">
                    <div class="row">
                        <div class="col">
                            <h4 class="page-title">{{ $t('Accountledgerdetailreport.Accountledgerdetailreport') }}</h4>
                            <ol class="breadcrumb">
                                <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('AccountLedger.Home') }}</a>
                                </li>
                                <li class="breadcrumb-item active">{{
                                    $t('Accountledgerdetailreport.Accountledgerdetailreport') }}</li>
                            </ol>

                        </div>
                        <div class="col-auto align-self-center">
                            <a href="javascript:void(0)" v-if="isValid('CanPrintAccountLedger')"
                                class="btn btn-sm btn-outline-primary mx-1 "
                                v-on:click="PrintRdlc(fromdate, todate, true)">{{
                                    $t('AccountLedger.Print') }}</a>
                            <a href="javascript:void(0)" v-if="isValid('CanPrintAccountLedger')"
                                class="btn btn-sm btn-outline-primary mx-1 " v-on:click="PrintRdlc(fromdate, todate, true)">
                                <i class="far fa-file-pdf"></i> Export PDF</a>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 col-lg-3">
                            <div class="form-group ">
                                <label>{{ $t('AccountLedger.FromDate') }}</label>
                                <datepicker v-model="fromDate" :key="render" />
                            </div>
                        </div>
                        <div class="col-md-3 col-lg-3">
                            <div class="form-group ">
                                <label>{{ $t('AccountLedger.ToDate') }}</label>
                                <datepicker v-model="toDate" :key="render" />
                            </div>
                        </div>

                        <div class="col-md-3 col-lg-3">
                            <div class="form-group ">
                                <label>{{ $t('Accountledgerdetailreport.CostCenter') }}</label>
                                <multiselect v-model="costCenter" :options="optionsCostCenter"
                                    @update:modelValue="GetAccount(costCenter)" :multiple="true" track-by="name"
                                    :clear-on-select="false" :show-labels="false" label="name"
                                    placeholder="Select Cost Center"
                                    v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-left ' : 'arabicLanguage '"
                                    :preselect-first="true">
                                </multiselect>
                            </div>
                        </div>
                        <div class=" col-lg-3 form-group" v-if="allowBranches">
                            <label>{{ $t('Branches.SelectBranch') }}</label>
                            <branch-dropdown v-model="branchIds" :ismultiple="true" :islayout="false" />
                        </div>
                        <div class="col-md-6 col-lg-6">
                            <div class="form-group ">
                                <label>{{ $t('AccountLedger.AccountName') }}</label>

                                <multiselect class="multiselection" v-model="accounts" placeholder="Type to search"
                                    label="name" track-by="name" :options="options" :option-height="104" :multiple="true"
                                    group-values="libs" group-label="accountType" :group-select="true"
                                    :close-on-select="false" :show-labels="false" @select="checkedValue"
                                    @remove="checkedValue" @search-change="afterSearch" :limit="1" :limit-text="limitText">



                                    <template v-slot:option="props">

                                        <div v-if="props.option.$isLabel" class="option__desc">
                                            <span v-if="props.option.$groupLabel != 'Select All'">
                                                {{ props.option.$groupLabel }}
                                            </span>
                                        </div>
                                        <div v-else class="option__desc">
                                            <!--<input type="checkbox" @update:modelValue="AccountTesting(props.option)" :checked="props.option.check" />-->
                                            <span class="option__title pl-2">{{ props.option.name }}</span>
                                        </div>
                                    </template>


                                    
                                    <template v-slot:noResult>
                                        <span  class="text-danger">No result found</span><br />
                                    </template>
                                </multiselect>

                            </div>
                        </div>
                        <div class=" col-lg-4 mt-1">

                            <button v-on:click="search22(true)" type="button" class="btn btn-outline-primary mt-3">
                                {{ $t('Sale.ApplyFilter') }}
                            </button>
                            <button v-on:click="clearData(false)" type="button" class="btn btn-outline-primary mx-2 mt-3">
                                {{ $t('Sale.ClearFilter') }}
                            </button>

                        </div>

                    </div>
                    <div class="row">
                        <div class="card-body" v-for="ledger in transactionList" v-bind:key="ledger.name">
                            <h6 class="col-md-12"
                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-left' : 'text-right'">
                                {{ ledger.name }}
                                <span class="pl-3">
                                    {{ $t('AccountLedger.OpeningBalance') }} :{{ ledger.opening > 0 ? 'Dr' : 'Cr' }}
                                    {{ nonNegative(ledger.opening) }}
                                </span>

                            </h6>
                            <div class="col-md-12">
                                <div class="table-responsive">
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
                                                    {{ $t('AccountLedger.Description') }}
                                                </th>
                                                <th
                                                    v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                    {{ $t('AccountLedger.Debit') }}
                                                </th>
                                                <th
                                                    v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                    {{ $t('AccountLedger.Credit') }}
                                                </th>
                                                <th
                                                    v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                    {{ $t('AccountLedger.Balance') }}
                                                </th>
                                                <!--<th>
                                            {{ $t('AccountLedger.Details') }}
                                        </th>-->
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr v-for="(account, index) in ledger.trialBalanceModel"
                                                v-bind:key="account.id">
                                                <td>
                                                    {{ index + 1 }}<br />
                                                </td>
                                                <td>
                                                    {{ account.transactionDate }}
                                                </td>
                                                <td>
                                                    {{ account.documentDate }}
                                                </td>
                                                <td>
                                                    {{ account.code }}
                                                </td>
                                                <td>
                                                    {{ getTransactionType(account.description) }}
                                                </td>
                                                <td
                                                    v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                    {{ nonNegative(account.debit) }}
                                                </td>
                                                <td
                                                    v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                    {{ nonNegative(account.credit) }}
                                                </td>
                                                <td
                                                    v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                    {{ account.total > 0 ? 'Dr' : 'Cr' }} {{ nonNegative(account.total) }}
                                                </td>
                                            </tr>


                                        </tbody>
                                    </table>
                                </div>
                            </div>
                            <h6 class="col-md-12"
                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">

                                <span>{{ $t('AccountLedger.ClosingBalance') }} :</span> <span class="pl-3">{{ ledger.closing
                                    > 0 ? 'Dr' : 'Cr' }} {{ nonNegative(ledger.closing) }}</span>


                            </h6>
                        </div>

                    </div>
                </div>
            </div>
            <div>
                <iframe :key="changereport" height="1500" width="100%" :src="reportsrc"></iframe>
            </div>
            <invoicedetailsprint :show="show" v-if="show" :reportsrc="reportsrc1" :changereport="changereportt"
                @close="show = false" @IsSave="IsSave" />

            <LedgerAccountWisePrintReport :headerFooter="headerFooter" :transactionList="transactionList"
                :fromDate="fromDate" :toDate="toDate" :formName="formName" :isPrint="isShown" :isShown="advanceFilters"
                v-if="isShown" v-bind:key="printRender"></LedgerAccountWisePrintReport>
            <LedgerAccountWiseDownloadReport :headerFooter="headerFooter" :transactionList="transactionList"
                :fromDate="fromDate" :toDate="toDate" :formName="formName" :isPrint="isDownload" :isShown="advanceFilters"
                v-if="isDownload"></LedgerAccountWiseDownloadReport>

        </div>
    </div>
        <div v-else>
            <acessdenied></acessdenied>
        </div>
</template>
<script>
import clickMixin from '@/Mixins/clickMixin'
import moment from "moment";
import Multiselect from "vue-multiselect";



export default {

    mixins: [clickMixin],
    props: ['formNameProp'],
    components: {
        Multiselect,

    },
    data: function () {
        return {
            allowBranches: false,
            branchIds: [],
            branchId: '',
            reportsrc: '',
            changereport: 0,
            reportsrc1: '',
            changereportt: 0,
            show: false,
            render: 0,
            randerAll: 0,
            fromDate: '',
            toDate: '',

            transactionList: [],


            isShown: false,
            isDownload: false,
            formName: 'Account Ledger',
            costCenter: '',
            optionsCostCenter: [],
            options: [],
            accounts: [],
            selectValue: [],
            printRender: 0,
            balance: 0,
            printRenderPdf: 0,
            advanceFilters: false,
            filterData: false,
            allSelected: false,
            combineDate: '',
            dateType: '',
            language: 'Nothing',
            headerFooter: {
                footerEn: '',
                footerAr: '',
                company: ''
            },

        }
    },


    watch: {
        // accounts: function () {

        //     if (this.accounts.length != 0)
        //         this.GetInventoryList(this.fromDate, this.toDate, this.accounts);
        //     else {
        //         this.transactionList = [];

        //     }

        // },
        // fromDate: function (fromDate) {
        //     this.isShown = false;
        //     if (this.accounts != null && this.accounts != '' && this.accounts.length != 0) {
        //         this.fromDate = fromDate;

        //         this.GetInventoryList(this.fromDate, this.toDate, this.accounts);

        //     }
        // },
        // toDate: function (toDate) {
        //     this.isShown = false;

        //     if (this.accounts != null && this.accounts != '' && this.accounts.length != 0) {

        //         this.GetInventoryList(this.fromDate, toDate, this.accounts);
        //     }
        // }
        branchIds: function () {
            var root = this;
            this.branchId = '';
            this.branchIds.forEach(function (result) {
                root.branchId = root.branchId == '' ? result.id : root.branchId + ',' + result.id;
            })
            this.GetInventoryList(this.fromDate, this.toDate, this.accounts);
        }
    },


    methods: {
        search22: function () {
            this.GetInventoryList(this.fromDate, this.toDate, this.accounts, this.costCenter);
        },

        clearData: function () {
            this.fromDate = "";
            this.toDate = "";
            this.accounts = "";
            this.costCenter = "";
            this.GetInventoryList(this.fromDate, this.toDate, this.accounts, this.costCenter);

        },
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
        GetAccount: function (costCenter) {

            var root = this;
            this.options = [];
            costCenter.forEach(function (costs) {
                root.options.push({
                    accountType: costs.name,
                    libs: costs.accounts
                });
            });
            if (this.costCenter.length == 0) {
                this.accounts = [];
                this.options = [];
                this.transactionList = [];
                this.randerAll++;

            }

        },
        AccountTesting: function (x) {
            console.log(x);

        },
        checkedValue: function (e) {

            this.allSelected = false;
            var root = this;
            if (e.length > 1) {
                e.forEach(function (x) {
                    x.check = !x.check;
                    if (!x.check) {
                        if (root.accounts.length != 0) {
                            root.accounts = root.accounts.filter(function (y) {
                                return y.id != x.id
                            })
                        }


                    }

                });
            }
            else {
                e.check = true;
                if (e.check) {
                    if (root.accounts.length != 0) {
                        root.accounts = root.accounts.filter(function (x) {
                            return x.id != e.id
                        })
                    }


                }
            }

            if (this.accounts.length != 0)
                this.GetInventoryList(this.fromDate, this.toDate, this.accounts);
            else {
                this.transactionList = [];
            }



        },

        getData: function () {

            var root = this;
            var token = "";
            if (token == '') {
                token = localStorage.getItem("token");
            }

            this.$https
                .get('/Report/CostCenterWiseAccountQuery?isDropDown=' + true, {
                    headers: { Authorization: `Bearer ${token}` },
                })
                .then(function (response) {

                    if (response.data != null) {
                        response.data.forEach(function (sup) {
                            root.optionsCostCenter.push({
                                id: sup.id,
                                name: sup.code + ' ' + sup.name,
                                accounts: sup.trialBalanceModel
                            });
                        });
                    }
                });

        },

        IsSave: function () {
            this.showReport = !this.showReport;
        },
        PrintRdlc: function (fromdate, todate, val) {

            var companyId = localStorage.getItem('CompanyID');

            if (val) {

                const requestData = {
                    companyId: companyId,
                    fromDate: fromdate,
                    toDate: todate,
                    formName: 'AccountLedgerCostCenterWise',
                    Print: val,
                    accounts: this.accounts // The array of accounts you want to send
                };

                // Make the POST request to the ASP.NET backend
                fetch(this.reportsrc1 = this.$ReportServer + '/ReportManagementModule/LedgerReports/LedgerReports.aspx', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify(requestData)
                })
                    .then(response => response.json())
                    .then(data => {
                        // Handle the response from the backend, if needed
                        console.log(data);
                    })
                    .catch(error => console.error('Error:', error));
                // this.reportsrc1=   this.$ReportServer+'/ReportManagementModule/LedgerReports/LedgerReports.aspx?companyId='+companyId+'&fromDate=' + fromdate + '&toDate=' + todate  + '&dateType=' +'&formName=CustomerLedgerReport_'+this.formName +this.dateType+ "&Print=" + val
                this.changereportt++;
                this.show = !this.show;
            }
            else {
                var Account = JSON.stringify(this.account)
                this.reportsrc = this.$ReportServer + '/ReportManagementModule/LedgerReports/LedgerReports.aspx?companyId=' + companyId + '&fromDate=' + fromdate + '&toDate=' + todate + '&dateType=' + '&formName=CustomerLedgerReport_' + this.formName + this.dateType + '&Account=' + Account + "&Print=" + val
                this.changereport++;
            }

        },
        PrintCsv: function () {

            var root = this;
            root.$https.post('/Report/AccountWiseLadgerCsv?language=' + this.$i18n.locale + '&fromDate=' + this.fromDate + '&toDate=' + this.toDate + '&companyId=' + localStorage.getItem('CompanyID'), root.transactionList, { headers: { "Authorization": `Bearer ${localStorage.getItem('token')}` }, responseType: 'blob' })
                .then(function (response) {

                    const url = window.URL.createObjectURL(new Blob([response.data]));
                    const link = document.createElement('a');
                    link.href = url;


                    link.setAttribute('download', 'AllAccountLedgerReport.csv');


                    document.body.appendChild(link);
                    link.click();

                });
        },
        DateTypeSelection: function () {
            if (this.accounts != null && this.accounts != '' && this.accounts != '00000000-0000-0000-0000-000000000000') {

                this.GetInventoryList(this.fromDate, this.toDate, this.accounts);
            }
        },
        nonNegative: function (value) {
            return parseFloat(Math.abs(value)).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,");
        },

        languageChange: function (lan) {
            if (this.language == lan) {

                var getLocale = this.$i18n.locale;
                this.language = getLocale;

                this.$router.go('/FreeofCostSale');

            }
        },


        getPage: function () {
            this.GetInventoryList(this.fromDate, this.toDate, this.accounts);
        },
        GetInventoryList: function (fromdate, todate) {

            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            todate = moment().add(1, 'days').format("DD MMM YYYY")

            this.isShown = false;

            this.$https.post('/Company/GetCostCenterWiseLedgerLedgerList?fromDate=' + fromdate + '&toDate=' + todate + '&isLedger=true' + '&dateType=' + this.dateType + '&branchId=' + this.branchId, this.accounts, { headers: { "Authorization": `Bearer ${token}` } })
                .then(function (response) {

                    if (response.data != null) {

                        root.transactionList = [];
                        root.transactionList = response.data;

                    }
                });
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
                        root.GetInvoicePrintSetting();

                        root.getBase64Image(root.headerFooter.company.logoPath);

                    }
                });
        },
        selectAll: function () {

            var root = this;
            this.accounts = [];
            this.allSelected = !this.allSelected;
            if (this.allSelected) {
                this.options.forEach(function (option) {
                    option.libs.forEach(function (x) {
                        x.check = root.allSelected;
                        root.accounts.push({ name: x.name, id: x.id, check: root.allSelected });
                    })
                })
            } else {
                this.options.forEach(function (option) {
                    option.libs.forEach(function (x) {
                        x.check = root.allSelected;
                    })
                })
            }
        },
        clearAll: function () {

            this.allSelected = false;
            this.options.forEach(function (option) {
                option.libs.forEach(function (x) {
                    x.check = false;
                })
            })
        },
        afterSearch: function (search) {

            if (search == '') {
                this.filterData = true;
            } else {
                this.filterData = false;
            }
        },
        limitText(count) {
            return `and ${count} more`
        },
        GetInvoicePrintSetting: function () {
            var root = this;

            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }

            root.$https.get('/Sale/printSettingDetail', { headers: { "Authorization": `Bearer ${token}` } })
                .then(function (response) {
                    if (response.data != null && response.data != '') {

                        root.headerFooter.footerEn = response.data.termsInEng;
                        root.headerFooter.footerAr = response.data.termsInAr;
                        root.headerFooter.isHeaderFooter = response.data.isHeaderFooter;
                        root.headerFooter.englishName = response.data.englishName;
                        root.headerFooter.arabicName = response.data.arabicName;
                        root.headerFooter.customerAddress = response.data.customerAddress;
                        root.headerFooter.customerVat = response.data.customerVat;
                        root.headerFooter.customerNumber = response.data.customerNumber;
                        root.headerFooter.cargoName = response.data.cargoName;
                        root.headerFooter.customerTelephone = response.data.customerTelephone;
                        root.headerFooter.itemPieceSize = response.data.itemPieceSize;
                        root.headerFooter.styleNo = response.data.styleNo;
                        root.headerFooter.blindPrint = response.data.blindPrint;
                        root.headerFooter.showBankAccount = response.data.showBankAccount;
                        root.headerFooter.bankAccount1 = response.data.bankAccount1;
                        root.headerFooter.bankAccount2 = response.data.bankAccount2;
                        root.headerFooter.bankIcon1 = response.data.bankIcon1;
                        root.headerFooter.bankIcon2 = response.data.bankIcon2;
                        root.headerFooter.customerNameEn = response.data.customerNameEn;
                        root.headerFooter.customerNameAr = response.data.customerNameAr;


                    }
                },
                    function (error) {
                        root.loading = false;
                        console.log(error);
                    });
        },


        DetailOfDocumentCode: function (id, type) {

            var documentId = id;
            var transactionType = type;
            var root = this;
            var token = '';
            var childFormName = '';
            //FOR PV CashReceipt
            if (transactionType == 1) {
                childFormName = 'CashReceipt';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('/PaymentVoucher/PaymentVoucherDetails?Id=' + documentId, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {
                        root.$store.GetEditObj(response.data);
                            root.$router.push({
                                path: '/addPaymentVoucher',
                                query: 
                                { 
                                    data: '',
                                    Add: false,
                                    formName :  childFormName,
                                }
                            })
                    }
                });
            }
            //FOR PV BankReceipt
            if (transactionType == 2) {
                childFormName = 'BankReceipt';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('/PaymentVoucher/PaymentVoucherDetails?Id=' + documentId, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {
                        root.$store.GetEditObj(response.data);
                            root.$router.push({
                                path: '/addPaymentVoucher',
                                query: 
                                { 
                                    data: '',
                                    Add: false,
                                    formName :  childFormName,
                                }
                            })
                    }
                });
            }
            //FOR PV CashPay
            if (transactionType == 3) {
                childFormName = 'CashPay';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('/PaymentVoucher/PaymentVoucherDetails?Id=' + documentId, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {
                        root.$store.GetEditObj(response.data);
                            root.$router.push({
                                path: '/addPaymentVoucher',
                                query: 
                                { 
                                    data: '',
                                    Add: false,
                                    formName :  childFormName,
                                }
                            })
                    }
                });
            }
            //FOR PV BankPay
            if (transactionType == 4) {
                childFormName = 'BankPay';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('/PaymentVoucher/PaymentVoucherDetails?Id=' + documentId, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {
                        root.$store.GetEditObj(response.data);
                            root.$router.push({
                                path: '/addPaymentVoucher',
                                query: 
                                { 
                                    data: '',
                                    Add: false,
                                    formName :  childFormName,
                                }
                            })
                    }
                });
            }
            //FOR PurchasePost
            if (transactionType == 0) {
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('/PaymentVoucher/PaymentVoucherDetails?Id=' + documentId, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {
                        root.$store.GetEditObj(response.data);
                            root.$router.push({
                                path: '/addPaymentVoucher',
                                query: 
                                { 
                                    data: '',
                                    Add: false,
                                    formName :  childFormName,
                                }
                            })
                    }
                });
            }
            //FOR SaleInvoice
            if (transactionType == 9) {
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('/Sale/SaleDetail?Id=' + documentId, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data != null) {
                            root.$store.GetEditObj(response.data);
                            root.$router.push({
                                path: '/SalePaymentDetail',
                                query: 
                                { 
                                    data: '',
                                    Add: false
                                }
                            })
                        }
                    },
                        function (error) {
                            console.log(error);
                        });
            }
        },

        PrintDetails: function (download) {


            var root = this;

            if (download) {

                root.isDownload = true;


            }
            else {

                root.isShown = true;
                root.printRender++;
            }



        },

        getBase64Image: function (path) {
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            this.headerFooter.company.logoPath = 'data:image/png;base64,' + 'iVBORw0KGgoAAAANSUhEUgAAATYAAACjCAMAAAA3vsLfAAAAhFBMVEX///8AcuEAZ98Ab+AAa+AAbeAAad9Tk+cAbuC+1fUFeOLS4/no8/0AZd8AaODv9v1to+vk7/z1+v7d6vqBru3F2vemxfKHsu5Zl+iYvfDU5PnM3/g9iea20PWNte5Rk+gwhOVIjuepx/NjnOl2p+wlfuS30fUAX96cvvAde+M1huWoxfIDNGHbAAAHKElEQVR4nO2d65aiOhBGJQlpVAKId6VVtJ3ROe//fgfvqSQocS3EjrV/NsEJ38qlqlKVabUQBEEQBEEQBEEQBEEQBEEQBEEQBEEQBEEQBEEQBEGQ30E6nG6ybcfrbPNkN2+6N68kDWVimzdHOYlYEFDP8yj1GWGT79q6+W7kQoZWfi/8IfwgmEzA/UVaY1/fiCX49k7V1wbC90wwMq2zt2/D1zOyDSkzinaAb/v19vgteEa2hSgV7TBVxQcscU/IlpB7qhUI9yeqvWxJ9EC1QrdR7f1uGGvZpndn6EW3Yf09bxRb2WZVVCsWOMcNEVvZtqqxZsafvKDvDWIp245XUq2Ypm77WpayaWONRYIxwTXjl2av6H1j2Mm2VgYb88bzwo8Nhz9MFU50X9L/hrCTbQVHmxhcn8QbxS7xk3o73ixWsvXgNgrdgYViBbM6u900VrLBORrt4NMf6KgSlzcFK9k2gdRWX/SV0eayi2Ul215uTLQ1/xsMxmBTV5/fACvZ5KWNGtoC2eiqlg6/BzayxbJs/o/eIAc/Vj1U/PuwkQ1spGynNxgA443X1OV3wEa2viwbNwQjFwxl0wGjja/1BlA2lw23p9c2k33xAz2smrr8DljtpLL/ZLIvgO8VuOzMW8kGZNE3yhi4V047pVaygUnI/6iPp3BHcPlAwUo24AZo9m4Ko0rC5fNSK9nAnuAxZRZmAZDN5R3BMt4G3QAykJ9NYACEjWvsdeMosg3bJZxO8Nowpsaz60Scb5X8BtFr7JteAJTN42X8PeVwdWBzKrLdcDZrL1ZCOWVwOv6hyVYKP8n2Rz0mpYyTiDPtV9webLaytfLgcdtDc6dXNnvZwvIULQm6bfarasdWtla7SjYDc3uKPiFba/dYN8eP5FvPyNYaPdCNEtfzjZ6SrTWM7u0LzHPZqzrzjGytOFfNtNtQEy4HPq48JVvh1Hci04tUrJxf1o48KVurtV4JxcgNmMjbjXzE61lWlC3SC2Rm41UUMRYU+IWvwLKp62bHjaUgMtERyRdlB/wCY1ZpPByNk8lkkizWXcfTTiHxrKB/otfrnUuvIGlB0/1EnKQ3/16P1t/DiutUf3hsPv+cZU2nu8gOYaADEWHL0QMt5uMVuTbn+XT2ml6+F+HCIwwksBUGRbmbFI6L5uAAkJHO7tPWwDiJDCEhKnKzpxQmxNScs/FHCTfVU+TPNiwxZBaVN/cY/YD6vjNxdqf8jGinAnebe8LtU4QbM/9upJsvYfN+cD8wzrY2Jfe/lpnRK5d1+LJq7gXeB+hW4XQgWsjNH3uxgetHCa1qNXtiZtXcY86vb4MqNXvByqq5WiDjHn218D1gJGJcMeLo37MM/WpVuMUbbttvE7grUpKNZnEaz8e+L/1xewk/fmmbKPUZ8/WJyxZ3/9lfjjJ62PYa004nZ+OMRvvrjNNKvhnPB9Ppzz/dxWAuD7cBzO0Gpdrj4/yNOtIylUDngPKLExqO1UMZp5MpgQx+Dh8mzOMeyKOHY8pfSfZZ31OSkVYv6H5DdOGGoD72oGitYXRHmFiZpSKstetNApKUIy2HWY1+jGFOs+ILdOHCZyqTcQRQIHrNQO13z8xmXYkZzEHV6zngyucP1OfOIJv8t8/cwNOsM6IDV0KubZXQBqS5+twZ5EnHr0fDiTGYRv/BetKl/nMgRZXuX/klrwQkzJPrSlYi2xLWXBns2QQYw86WqoWyDtF15zPLFmxCuwo/ZwsjgQ7kgWz+IHxUTzr9DNlSINs1OGSWje3AJOX/6b8HDRT/lZ/yUkDp2XX4mGWLhmBLYAb7Al544W6sUq50vBUylsjWAxd9mJwn0D5w9y4ysPVdJ5VZNk9JhiPamf0c+F4OF12N5Fgtv5j9RtkOgwcs+fqNFrCQjbubIgjDbZe9dPP3dOczkWU4GBzQ81evGlvDuIBwOOAG7frO6UvjS5ZbJj09mnVwAHIQ45hDT97kRTgDtLRoRxYizXxVBWhhUCZl1ihjzRBPcYgYpiVQPr1MrXQHzupPJRqhog2ZnILoaVvLb6h8PfmvRLl0zeP+ZtQeDkdJYLznKVGaB1GwWi5XvnZSHxl8L4eItbOTgPGIc+WE6nL/sN7cowXaH50/lx89uiH8wO0aXXUJK0E4n1mZl6WqSbBb/PurSkEpcfqU9EjqPczqAIWO28f1y9xdv+pGaFiboGpgeU/3j4anmhDnKOH+7sQTyqaYZvfTZz6jxu/ApjwjJmB6tvigtC7ykOzr8nG8wndgHnBU5KbMyPm2JKWyaP9RhR3pmGv/AZNHyb7MRVp3iL41+GTlbtijhHS3F1I5R+BzsrwnQvuLcKk9ZRFPnL5+vZT+buKJw9myECwbtB9FftL2IGPn9nS5cP9igTukvUOJZPVY2bF96HBsDUEQBEEQBEEQBEEQBEEQBEEQBEEQBEEQBEEQBEEQBEEQmf8BckJbXFsgizoAAAAASUVORK5CYII=';

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
        this.GetHeaderDetail();
        this.getData();
        this.language = this.$i18n.locale;
        this.fromDate = moment().subtract(15, 'days').format("DD MMM YYYY");
        this.toDate = moment().format("DD MMM YYYY");
        this.allowBranches = localStorage.getItem('AllowBranches') == 'true' ? true : false;
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
}</style>