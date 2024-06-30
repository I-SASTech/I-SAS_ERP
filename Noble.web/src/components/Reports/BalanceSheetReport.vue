<template>
    <div class="row" v-if="isValid('CanViewBalanceSheetReport')">
        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">{{ $t('BalanceSheetReport.BalanceSheetReport') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);"> {{
                                        $t('BalanceSheetReport.Home') }}</a></li>
                                    <li class="breadcrumb-item active">{{ $t('BalanceSheetReport.BalanceSheetReport') }}
                                    </li>
                                </ol>
                            </div>
                            <div class="col-auto align-self-center">
                                <a v-on:click="PrintRdlc(fromDate, toDate, true)" href="javascript:void(0);"
                                    class="btn btn-sm btn-outline-primary mx-1">
                                    <i class="fas fa-print font-14"></i>
                                    {{ $t('Print') }}
                                </a>
                                <a v-on:click="GotoPage('/StartScreen')" href="javascript:void(0);"
                                    class="btn btn-sm btn-outline-danger">
                                    {{ $t('Categories.Close') }}
                                </a>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row align-items-center">

                <div class=" col-lg-4   form-group">
                    <label>{{ $t('BalanceSheetReport.FromDate') }}</label>
                    <datepicker v-model="fromDate" :key="rander" />
                </div>

                <div class=" col-lg-4   form-group">
                    <label>{{ $t('BalanceSheetReport.ToDate') }}</label>
                    <datepicker v-model="toDate" :key="rander" />
                </div>
                <div class=" col-lg-4 form-group" v-if="allowBranches">
                    <label>{{ $t('Branches.SelectBranch') }}</label>
                    <branch-dropdown v-model="branchIds" :ismultiple="true" :islayout="false" />
                </div>

            </div>
            <div class="card">
                <div class="card-body" style="display: flex; justify-content: center; align-items: center;">
                    <iframe :key="changereport" height="1500" width="100%" :src="reportsrc"></iframe>
                    <invoicedetailsprint :show="showReport" v-if="showReport" :reportsrc="reportsrc1"
                        :changereport="changereportt" @close="showReport = false" @IsSave="IsSave" />

                </div>
            </div>

        </div>
    </div>
    <div v-else>
        <acessdenied></acessdenied>
    </div>
</template>


<script>
import clickMixin from '@/Mixins/clickMixin'


import moment from "moment";
export default {
    mixins: [clickMixin],
    data: function () {
        return {
            allowBranches: false,
            branchIds: [],
            branchId: '',
            reportsrc: '',
            showReport: false,
            reportsrc1: '',
            changereportt: 0,
            changereport: 0,
            date: '',
            fromDate: '',
            toDate: '',
            rander: 0,
            printRender: 0,
            currentPage: 1,
            pageCount: '',
            rowCount: '',
            isShown: false,
            assets: [],
            liabilities: [],
            equity: [],
            records: [],
            advanceFilters: false,
            combineDate: '',
            language: 'Nothing',
            totalAssets: 0,
            totalLiabilities: 0,
            totalEquity: 0,
            totalEquityLiability: 0,
            totalIncome: 0,
            totalExpense: 0,
            NetIncome: 0,
            isNegative: false,
            show: false,
        }
    },
    watch: {
        toDate: function (todate) {
            this.PrintRdlc(this.fromDate, todate, false);
        },
        fromDate: function (fromdate) {
            this.PrintRdlc(fromdate, this.toDate, false);

        },
        branchIds: function () {
            var root = this;
            this.branchId = '';
            this.branchIds.forEach(function (result) {
                root.branchId = root.branchId == '' ? result.id : root.branchId + ',' + result.id;
            })
            this.PrintRdlc(this.fromDate, this.toDate, false);
        }
    },
    IsSave: function () {
        this.show = !this.show;
    },
    methods: {
        GotoPage: function (link) {
            this.$router.push({ path: link });
        },

        PrintRdlc: function (fromDate, toDate, val) {

            var companyId = '';
                companyId = localStorage.getItem('CompanyID');
            
            

            if (val) {
                this.reportsrc1 = this.$ReportServer + '/ReportManagementModule/AccountFinanceReports/AccountFinanceReports.aspx?companyId=' + companyId + '&fromDate=' + fromDate + '&toDate=' + toDate + '&formName=BalanceSheetReport' + '&Print=' + val + "&branchId=" + this.branchId
                this.changereportt++;
                this.showReport = !this.showReport;
            }
            else {
                this.reportsrc = this.$ReportServer + '/ReportManagementModule/AccountFinanceReports/AccountFinanceReports.aspx?companyId=' + companyId + '&fromDate=' + fromDate + '&toDate=' + toDate + '&formName=BalanceSheetReport' + '&Print=' + val + "&branchId=" + this.branchId
                this.changereport++;
            }
        },
        PrintCsv: function () {

            var root = this;
            root.$https.get('/Report/BalanceSheetCsv?language=' + this.$i18n.locale + '&fromDate=' + this.fromDate + '&toDate=' + this.toDate + '&companyId=' + localStorage.getItem('CompanyID'), { headers: { "Authorization": `Bearer ${localStorage.getItem('token')}` }, responseType: 'blob' })
                .then(function (response) {

                    const url = window.URL.createObjectURL(new Blob([response.data]));
                    const link = document.createElement('a');
                    link.href = url;

                    link.setAttribute('download', 'BalanceSheetReport.csv');


                    document.body.appendChild(link);
                    link.click();

                });
        },

        languageChange: function (lan) {
            if (this.language == lan) {

                var getLocale = this.$i18n.locale;
                this.language = getLocale;

                this.$router.go('/BalanceSheetReport');

            }
        },
        convertDate: function (date) {
            return moment(date).format('l');
        },
        getPage: function () {
            this.GetInventoryList(this.fromDate, this.toDate, this.currentPage);
        },

        GetInventoryList: function (fromdate, todate) {

            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            this.$https.get('/Report/BalanceSheet?fromDate=' + fromdate + '&toDate=' + todate + "&branchId=" + this.branchId, { headers: { "Authorization": `Bearer ${token}` } })
                .then(function (response) {
                    if (response.data != null) {
                        root.records = response.data;
                        root.assets = response.data.asset;

                        root.totalAssets = root.assets.reduce(function (prev, item) {
                            return prev + Number(item.amount);
                        }, 0);


                        root.liabilities = response.data.liability;

                        root.totalLiabilities = root.liabilities.reduce(function (prev, item) {
                            return (prev + Number(item.amount));
                        }, 0);


                        root.NetIncome = response.data.yearlyIncome;

                        root.equity = response.data.equity;

                        root.totalEquity = root.NetIncome + (root.equity.reduce(function (prev, item) {
                            return (prev + Number(item.amount));

                        }, 0));


                        root.totalEquityLiability = root.totalEquity + root.totalLiabilities




                    }

                });


        },

        PrintDetails: function () {
            var root = this;
            root.isShown = true;
            root.combineDate = 'From Date: ' + this.fromDate + ' To Date: ' + this.toDate;
            root.printRender++;
        }
    },
    mounted: function () {
        this.language = this.$i18n.locale;
        this.fromDate = moment().subtract(15, 'days').format("DD MMM YYYY");
        this.toDate = moment().format("DD MMM YYYY");
        this.rander++;
        this.PrintRdlc(this.fromDate, this.toDate, false);
        this.allowBranches = localStorage.getItem('AllowBranches') == 'true' ? true : false;
    }
}
</script>