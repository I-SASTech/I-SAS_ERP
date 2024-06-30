<template>
    <div class="row" v-if="isValid('CanViewIncomeStatementReport')">
        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">{{ $t('IncomeStatementReport.IncomeStatementReport') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{
                                        $t('IncomeStatementReport.Home') }}</a></li>
                                    <li class="breadcrumb-item active">{{ $t('IncomeStatementReport.IncomeStatementReport')
                                    }}</li>
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

            <div class="row ">

                <div class=" col-lg-4   form-group">
                    <label>{{ $t('IncomeStatementReport.FromDate') }}</label>
                    <datepicker v-model="fromDate" :key="rander" />
                </div>

                <div class=" col-lg-4   form-group">
                    <label>{{ $t('IncomeStatementReport.ToDate') }}</label>
                    <datepicker v-model="toDate" :key="rander" />
                </div>

                <div class=" col-lg-4 form-group" v-if="allowBranches">
                    <label>{{ $t('Branches.SelectBranch') }}</label>
                    <branch-dropdown v-model="branchIds" :ismultiple="true" :islayout="false" />
                </div>
            </div>
            <iframe :key="changereport" height="1500" width="100%" :src="reportsrc"></iframe>
        </div>
        <invoicedetailsprint :show="showReport" v-if="showReport" :reportsrc="reportsrc1" :changereport="changereportt"
            @close="showReport = false" @IsSave="IsSave" />

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
            changereport: 0,
            reportsrc1: '',
            changereportt: 0,
            showReport: false,
            date: '',
            fromDate: '',
            toDate: '',
            rander: 0,
            printRender: 0,
            currentPage: 1,
            pageCount: '',
            rowCount: '',
            isShown: false,
            Income: [],
            Expenses: [],
            advanceFilters: false,
            combineDate: '',
            language: 'Nothing',
            totalIncome: 0,
            totalExpenses: 0,
            NetIncome: 0,
            isNegative: false

        }
    },
    watch: {
        toDate: function (todate) {
            var fromdate = this.fromDate;

            this.PrintRdlc(fromdate, todate, false);
        },
        fromDate: function (fromdate) {
            var todate = this.toDate;

            this.PrintRdlc(fromdate, todate, false);

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
    methods: {
        GotoPage: function (link) {
            this.$router.push({ path: link });
        },
        PrintCsv: function () {

            var root = this;
            root.$https.get('/Report/IncomeStatementCsv?language=' + this.$i18n.locale + '&fromDate=' + this.fromDate + '&toDate=' + this.toDate + '&companyId=' + localStorage.getItem('CompanyID'), { headers: { "Authorization": `Bearer ${localStorage.getItem('token')}` }, responseType: 'blob' })
                .then(function (response) {

                    const url = window.URL.createObjectURL(new Blob([response.data]));
                    const link = document.createElement('a');
                    link.href = url;

                    link.setAttribute('download', 'IncomeStatementReport.csv');


                    document.body.appendChild(link);
                    link.click();

                });
        },
        languageChange: function (lan) {
            if (this.language == lan) {

                var getLocale = this.$i18n.locale;
                this.language = getLocale;

                this.$router.go('/IncomeStatementReport');

            }
        },
        IsSave: function () {
            this.showReport = !this.showReport;
        },
        PrintRdlc: function (fromdate, todate, val) {

            var companyId = '';
            companyId = localStorage.getItem('CompanyID');
            
            

            if (val) {
                this.reportsrc1 = this.$ReportServer + '/ReportManagementModule/AccountFinanceReports/AccountFinanceReports.aspx?companyId=' + companyId + '&fromDate=' + fromdate + '&toDate=' + todate + '&formName=IncomeStatementReport' + "&Print=" + val + "&branchId=" + this.branchId
                this.changereportt++;
                this.showReport = !this.showReport;
            }
            else {
                this.reportsrc = this.$ReportServer + '/ReportManagementModule/AccountFinanceReports/AccountFinanceReports.aspx?companyId=' + companyId + '&fromDate=' + this.fromDate + '&toDate=' + this.toDate + '&formName=IncomeStatementReport' + "&Print=" + val + "&branchId=" + this.branchId
                this.changereport++;
            }
        },
        convertDate: function (date) {
            return moment(date).format('l');
        },


        GetInventoryList: function (fromdate, todate) {
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            this.$https.get('/Report/IncomeStatement?fromDate=' + fromdate + '&toDate=' + todate + "&branchId=" + this.branchId, { headers: { "Authorization": `Bearer ${token}` } })
                .then(function (response) {
                    if (response.data != null) {

                        root.Income = response.data.income;
                        root.totalIncome = Math.abs(root.Income.reduce(function (prev, item) {
                            return prev + Number(item.amount);

                        }, 0));
                        root.Expenses = response.data.expenses;
                        root.totalExpenses = Math.abs(root.Expenses.reduce(function (prev, item) {
                            return prev + Number(item.amount);

                        }, 0));
                        root.NetIncome = root.totalIncome - root.totalExpenses
                        if (root.NetIncome < 0) {
                            root.isNegative = true
                        } else {
                            root.isNegative = false
                        }
                    }
                });

        },
    },
    mounted: function () {
        this.language = this.$i18n.locale;
        this.fromDate = moment().subtract(15, 'days').format("DD MMM YYYY");
        this.toDate = moment().format("DD MMM YYYY");
        this.PrintRdlc(this.fromDate, this.toDate, false);
        this.allowBranches = localStorage.getItem('AllowBranches') == 'true' ? true : false;
    }
}
</script>