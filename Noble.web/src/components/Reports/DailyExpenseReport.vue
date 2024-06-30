<template>
    <div class="col-lg-12">
        <div class="row">
            <div class="col-sm-12">
                <div class="page-title-box">
                    <div class="row">
                        <div class="col">
                            <h4 class="page-title">{{
                                $t('InvoicePrintReport.DailyExpenseReport') }}</h4>
                          
                            <ol class="breadcrumb">
                                <li class="breadcrumb-item">
                                    <a href="javascript:void(0);">{{ $t('InvoicePrintReport.Home')}}</a></li>
                                <li class="breadcrumb-item active">{{
                                    $t('InvoicePrintReport.DailyExpenseReport') }}</li>
                            </ol>
                        </div>
                        <div class="col-auto align-self-center">
                            <a v-on:click="PrintRdlc(fromDate, toDate,true)" href="javascript:void(0);"
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
                <label>{{ $t('StockReport.FromDate') }}</label>
                <datepicker v-model="fromDate" :key="render" />
            </div>

            <div class=" col-lg-3   form-group">
                <label>{{ $t('StockReport.ToDate') }}</label>
                <datepicker v-model="toDate" :key="render" />
            </div>

            <div class=" col-lg-3   form-group">
                <label>{{ $t('InvoicePrintReport.PaymentType') }}</label>
                <multiselect v-model="paymentType" :options="['Month', '3 Month', '6 Month', 'Year']" :show-labels="false"
                    placeholder="Select Type" @update:modelValue="PaymentTypeToFromDate()">
                </multiselect>
            </div>
            <div class=" col-lg-3 form-group" v-if="allowBranches">
                    <label>{{ $t('Branches.SelectBranch') }}</label>
                    <branch-dropdown v-model="branchIds" :ismultiple="true" :islayout="false" />
                </div>
            <div class="col-sm-4 form-group">
                    <a v-on:click="SearchFilter" href="javascript:void(0);" class="btn btn-sm btn-outline-primary mx-1">
                        Search Filter
                    </a>
                    <a @click="ClearFilter" href="javascript:void(0);" class="btn btn-sm btn-outline-danger">
                        Clear Filter
                    </a>

                </div>

        </div>
        <div class="card">

        </div>
        <iframe :key="changereport" height="1080" width="100%" :src="reportsrc"></iframe>

        <invoicedetailsprint :show="show" v-if="show" :reportsrc="reportsrc1" :changereport="changereportt"
            @close="show = false" @IsSave="IsSave" />

    </div>
</template>
<script>
import clickMixin from '@/Mixins/clickMixin'
import moment from "moment";
import Multiselect from 'vue-multiselect'

export default {
    mixins: [clickMixin],
    components: {
        Multiselect,
    },
    

    data: function () {
        return {
            allowBranches: false,
            branchIds: [],
            branchId: '',
            documentName:'',
            paymentType: '',
            reportsrc: '',
            changereport: 0,
            reportsrc1: '',
            changereportt: 0,
            show: false,
            render: 0,
            fromDate: '',
            toDate: '',
            currentPage: 1,
            pageCount: '',
            rowCount: '',
        }
    },
    watch: {
        
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
        SearchFilter: function () {
            
            this.PrintRdlc(this.fromDate, this.toDate, false,this.paymentType,this.branchId);
        },
        ClearFilter() {
            // Reset the filter conditions here
            this.fromDate = '';
            this.toDate = '';
            this.paymentType = '';
            this.render++

        },
        PaymentTypeToFromDate: function()
        {
            if(this.paymentType == 'Month')
            {
                this.fromDate = moment().subtract(1, 'months').format("DD MMM YYYY");
                this.toDate = moment().format("DD MMM YYYY");
               // this.PrintRdlc(this.fromDate,this.toDate,false);
            }
            else if(this.paymentType == '3 Month')
            {
                this.fromDate = moment().subtract(3, 'months').format("DD MMM YYYY");
                this.toDate = moment().format("DD MMM YYYY");
               // this.PrintRdlc(this.fromDate,this.toDate,false);
            }
            else if(this.paymentType == '6 Month')
            {
                this.fromDate = moment().subtract(6, 'months').format("DD MMM YYYY");
                this.toDate = moment().format("DD MMM YYYY");
               // this.PrintRdlc(this.fromDate,this.toDate,false);
            }
            else if(this.paymentType == 'Year')
            {
                this.fromDate = moment().subtract(12, 'months').format("DD MMM YYYY");
                this.toDate = moment().format("DD MMM YYYY");
                //this.PrintRdlc(this.fromDate,this.toDate,false);
            }
            this.render++;
        },
        GotoPage: function (link) {
            this.$router.push({ path: link });
            this.PrintRdlc(this.fromDate,this.toDate,false);
        },


        GetInvoiceRecord: function (fromdate, todate,paymentType,branchId) {
            
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            
            this.$https.get('/Report/SaleInvoiceReportMonthWise?fromDate=' + fromdate + '&toDate=' + todate + '&paymentType=' + paymentType +'&documentName=DailyExpense' +'&branchId=' + branchId , { headers: { "Authorization": `Bearer ${token}` } })
                .then(function (response) {
                    if (response.data != null) {
                        

                        root.saleInvoiceReportMonthWise = response.data;
                        root.loading = false;
                    }
                    root.loading = false;
                });

        },

        IsSave: function () {
            this.showReport = !this.showReport;
        },
        PrintRdlc: function (fromdate, todate, val) {
            var companyId = '';
            companyId = localStorage.getItem('CompanyID');
            

            if (val) {
                this.reportsrc1 = this.$ReportServer + '/ReportManagementModule/SalePurchase/SalePurchaseReports.aspx?companyId=' + companyId + '&fromDate=' + fromdate + '&toDate=' + todate + '&dateType=' + '&paymentType=' + this.paymentType + '&documentName=DailyExpense' + "&Print=" + val+'&formName=DailyExpenseReport' + "&branchId=" + this.branchId
                this.changereportt++;
                this.show = !this.show;
            }
            else {
                this.reportsrc = this.$ReportServer + '/ReportManagementModule/SalePurchase/SalePurchaseReports.aspx?companyId=' + companyId + '&fromDate=' + fromdate + '&toDate=' + todate + '&dateType=' + '&paymentType=' + this.paymentType + '&documentName=DailyExpense' + "&Print=" + val+'&formName=DailyExpenseReport' + "&branchId=" + this.branchId
                this.changereport++;
            }
        },

        created: function () {

          this.language = this.$i18n.locale;
          this.fromDate = moment().format("DD MMM YYYY");
          this.toDate = moment().format("DD MMM YYYY");
      //    this.PrintRdlc(this.fromDate,this.toDate,false);
          this.render++;

          this.allowBranches = localStorage.getItem('AllowBranches') == 'true' ? true : false;
        }
    }
}
</script>