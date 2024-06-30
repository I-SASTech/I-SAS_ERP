<template>
    <div class="row" v-if="isValid('CanViewVatPayableReport')">
        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">VAT Detail Report</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('VatPayableReport.Home')
                                    }}</a></li>
                                    <li class="breadcrumb-item active">VAT Detail Report</li>
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

                <div class=" col-lg-3   form-group">
                    <label>{{ $t('VatPayableReport.FromDate') }}</label>
                    <datepicker v-model="fromDate" :key="rander" />
                </div>

                <div class=" col-lg-3   form-group">
                    <label>{{ $t('VatPayableReport.ToDate') }}</label>
                    <datepicker v-model="toDate" :key="rander" />
                </div>
                <div class=" col-lg-3 form-group" v-if="allowBranches">
                    <label>{{ $t('Branches.SelectBranch') }}</label>
                    <branch-dropdown v-model="branchIds" :ismultiple="true" :islayout="false" />
                </div>
            </div>
            <div>
                <iframe :key="changereport" height="1100" width="100%" :src="reportsrc"></iframe>
            </div>

        </div>
        <invoicedetailsprint :show="show" v-if="show" :reportsrc="reportsrc1" :changereport="changereportt"
            @close="show = false" @IsSave="IsSave" />

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
    props: ['formName'],

    data: function () {
        return {
            documentName: this.formName,
            allowBranches: false,
            branchIds: [],
            branchId: '',
            reportsrc: '',
            changereport: 0,
            reportsrc1: '',
            changereportt: 0,
            show: false,
            vatPaidDetail: '',
            date: '',
            fromDate: '',
            rander: 0,
            printRenderPdf: 0,
            printRender: 0,
            advanceFilters: false,
            isShown: false,
            isDownload: false,
            combineDate: '',
            language: '',
            totalvatpaid: 0,
            totalvatpayable: 0,
            VatPayable: 0,
            printDetails: [],
            headerFooter: {
                footerEn: '',
                footerAr: '',
                company: ''
            },

        }
    },
    watch: {
        documentName: function () {

            if (this.$route.query.formName == "Arabic") {

                this.language='ar';
                this.PrintRdlc(this.fromdate, this.todate, false);
            }
            else {
                this.language='en';

                this.PrintRdlc(this.fromdate, this.todate, false);
            }
        },
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

      
        convertDate: function (date) {
            return moment(date).format('l');
        },
        getPage: function () {

            this.PrintRdlc(this.fromDate, this.toDate, false);
        },

        GetInventoryList: function (fromdate, todate) {

            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            //todate=    moment().add(1, 'days').format("DD MMM YYYY")
            this.$https.get('/Report/VatPayable?fromDate=' + fromdate + '&toDate=' + todate, { headers: { "Authorization": `Bearer ${token}` } })
                .then(function (response) {
                    if (response.data != null) {
                        root.vatPaidDetail = response.data;
                        root.paidaccounts = response.data.vatPaid;
                        root.totalvatpaid = Math.abs(root.paidaccounts.reduce(function (prev, item) {
                            return prev + Number(item.amount);

                        }, 0));
                        root.payableaccounts = response.data.vatPayable;
                        root.totalvatpayable = Math.abs(root.payableaccounts.reduce(function (prev, item) {
                            return prev + Number(item.amount);

                        }, 0));
                        root.VatPayable = root.totalvatpayable - root.totalvatpaid;
                        root.VatPayable = root.VatPayable < 0 ? root.VatPayable * -1 : root.VatPayable;
                    }
                });


        },
        IsSave: function () {
            this.showReport = !this.showReport;
        },
        PrintRdlc: function (fromdate, todate, val) {

            var companyId = '';
            companyId = localStorage.getItem('CompanyID');
            let currency = localStorage.getItem('currency');

            if (val) {
                this.reportsrc1 = this.$ReportServer + '/ReportManagementModule/VatReports/VatReports.aspx?companyId=' + companyId + '&fromDate=' + fromdate + '&toDate=' + todate + '&formName=VatReturnReport' + "&Print=" + val + "&branchId=" + this.branchId + "&language=" + this.language + "&currency=" + currency
                this.changereportt++;
                this.show = !this.show;
            }
            else {

                this.reportsrc = this.$ReportServer + '/ReportManagementModule/VatReports/VatReports.aspx?companyId=' + companyId + '&fromDate=' + fromdate + '&toDate=' + todate + '&formName=VatReturnReport' + "&Print=" + val + "&branchId=" + this.branchId + "&currency=" + currency + "&language=" + this.language 
                this.changereport++;
            }
        },

    },
    mounted: function () {
        this.language = this.$route.query.formName == "Arabic" ? 'ar' : 'en';
        this.fromDate = moment().subtract(15, 'days').format("DD MMM YYYY");
        this.toDate = moment().format("DD MMM YYYY");
        this.rander++;
        this.PrintRdlc(this.fromDate, this.toDate, false);
        this.allowBranches = localStorage.getItem('AllowBranches') == 'true' ? true : false;
    }
}
</script>