<template>
    <div class="row" v-if="isValid('CanViewStockReport')">

<div class="col-lg-12">
    <div class="row">
        <div class="col-sm-12">
            <div class="page-title-box">
                <div class="row">
                    <div class="col">
                        <h4 class="page-title">{{ $t('TrialBalanceReport.TrialBalanceReport') }}</h4>
                        <ol class="breadcrumb">
                            <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('TrialBalanceReport.Home') }}</a></li>
                            <li class="breadcrumb-item active">{{ $t('TrialBalanceReport.TrialBalanceReport') }}</li>
                        </ol>
                    </div>
                    <div class="col-auto align-self-center">
                        <a v-on:click="PrintRdlc(fromDate,toDate,true)" 
                            href="javascript:void(0);" class="btn btn-sm btn-outline-primary mx-1">
                            <i class="fas fa-print font-14"></i>
                            {{ $t('Print') }}
                        </a>
                        <a  v-if="isValid('CanPrintAccountLedger')"  v-on:click="PrintRdlc()"
                                    href="javascript:void(0);" class="btn btn-sm btn-outline-primary mx-1">
                                    <i class="fas fa-print font-14"></i>
                                    {{ $t('AccountLedger.Print') }}
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
                    <label>{{ $t('TrialBalanceReport.FromDate') }}</label>
                                    <datepicker v-model="fromDate" :key="rander" />
                </div>

                <div class=" col-lg-4   form-group">
                    <label>{{ $t('TrialBalanceReport.ToDate') }}</label>
                                    <datepicker v-model="toDate" :key="rander" />
                </div>
                <div class=" col-lg-4 form-group" v-if="allowBranches">
                    <label>{{ $t('Branches.SelectBranch') }}</label>
                    <branch-dropdown v-model="branchIds" :ismultiple="true" :islayout="false" />
                </div>

            </div>
                <div >
                    <iframe :key="changereport" height="1500" width="100%" :src="reportsrc"></iframe>
                </div>
                <invoicedetailsprint :show="showrpt" v-if="showrpt" :reportsrc="reportsrc1" :changereport="changereportt" @close="showrpt=false" @IsSave="IsSave" />

    <trialSubAccount :subAccount="subAccount"
                             :show="show"
                             v-if="show"
                             @close="show = false"
                              />
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
                reportsrc:'',
                changereport:0,
                reportsrc1:'',
                changereportt:0,
                showrpt:false,
                date: '',
                fromDate: '',
                toDate: '',
                rander: 0,
                printRender: 0,
                currentPage: 1,
                pageCount: '',
                rowCount: '',
                isShown: false,
                accounts: [],
                subAccount: [],
                advanceFilters: false,
                show: false,
                showReport: false,
                combineDate: '',
                language: 'Nothing',
                totalDebit: 0,
                totalCredit: 0

            }
        },
        watch: {
            toDate: function (todate) {
                var fromdate = this.fromDate;

                this.PrintRdlc(fromdate, todate);
            },
            fromDate: function (fromdate) {
                var todate = this.toDate;

                this.PrintRdlc(fromdate, todate);

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
            EditCity: function (subAccount) {
                this.subAccount = subAccount
              
                this.show = !this.show;
            },
            IsSave:function(){
                this.showReport = !this.showReport;
            },
            PrintCsv: function () {

                var root = this;
                root.$https.post('/Report/TrialBalanceCsv?language=' + this.$i18n.locale + '&fromDate=' + this.fromDate + '&toDate=' + this.toDate + '&formName=' + this.formName + '&companyId=' + localStorage.getItem('CompanyID'), root.accounts, { headers: { "Authorization": `Bearer ${localStorage.getItem('token')}` }, responseType: 'blob' })
                    .then(function (response) {

                        const url = window.URL.createObjectURL(new Blob([response.data]));
                        const link = document.createElement('a');
                        link.href = url;


                        link.setAttribute('download', 'TrialBalanceReport.csv');


                        document.body.appendChild(link);
                        link.click();

                    });
            },
            PrintRdlc:function(fromdate,todate,val) {
                
                var companyId = '';
                    companyId = localStorage.getItem('CompanyID');
                    
                    
                    if(val){
                        this.reportsrc1= this.$ReportServer+'/ReportManagementModule/AccountFinanceReports/AccountFinanceReports.aspx?companyId='+companyId+'&fromDate=' + fromdate + '&toDate=' + todate +'&formName=TrialBalanceReport' + "&Print=" + val + "&branchId=" + this.branchId
                        this.changereportt++;
                        this.showrpt = !this.showrpt;
                    }
                    else{
                        this.reportsrc= this.$ReportServer+'/ReportManagementModule/AccountFinanceReports/AccountFinanceReports.aspx?companyId='+companyId+'&fromDate=' + fromdate + '&toDate=' + todate +'&formName=TrialBalanceReport' + "&Print=" + val + "&branchId=" + this.branchId
                        this.changereport++;
                    }
                },

            languageChange: function (lan) {
                if (this.language == lan) {

                    var getLocale = this.$i18n.locale;
                    this.language = getLocale;

                    this.$router.go('/TrialBalanceReport');

                }
            },
            convertDate: function (date) {
                return moment(date).format('l');
            },
            getPage: function () {
                this.PrintRdlc(this.fromDate, this.toDate,false);
            },
           
            GetInventoryList: function (fromdate, todate) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                this.$https.get('/Report/TrialBalanceReportAsync?fromDate=' + fromdate + '&toDate=' + todate + "&branchId=" + this.branchId, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data != null) {
                            
                            root.accounts = response.data;
                            root.totalDebit = root.accounts.reduce(function (prev, item) {
                                return prev + Number(item.debit);

                            }, 0);
                            root.totalCredit = Math.abs(root.accounts.reduce(function (prev, item) {
                                return Math.abs(prev + Number(item.credit));

                            }, 0));
                        }
                    });

            },
        },
        mounted: function () {
            this.language = this.$i18n.locale;
            this.fromDate = moment().subtract(15, 'days').format("DD MMM YYYY");
            this.toDate = moment().format("DD MMM YYYY");
            this.rander++;
            this.PrintRdlc(this.fromDate,this.toDate,false);
            this.GetInventoryList(this.fromDate,this.toDate);
            this.allowBranches = localStorage.getItem('AllowBranches') == 'true' ? true : false;
        }
    }
</script>
