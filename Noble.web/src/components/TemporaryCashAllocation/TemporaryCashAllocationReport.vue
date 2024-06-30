<template>
    <div class="row" v-if=" isValid('CanViewTCAllocationReport')" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage pr-4'">
        <div class="col-lg-12 col-sm-12 ml-auto mr-auto">

            <div class="card">
                <div class="card-header">
                    <div class="row">
                        <div class="col-md-6 col-lg-6">
                            <div class="form-group ">
                                <h5 class="card-title DayHeading">{{ $t('TemporaryCashAllocationReport.TemporaryCashAllocationReport') }}</h5>
                            </div>
                        </div>
                        <div class="col-md-6 col-lg-6">
                            <div class="form-group " v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-right' : 'text-left' ">
                                <router-link :to="'/AllReports'">
                                    <a href="javascript:void(0)" class="btn btn-outline-primary "><i class="fas fa-arrow-circle-left fa-lg"></i> </a>
                                </router-link>
                                <a href="javascript:void(0)" v-on:click="PrintInvoice()" class="btn btn-outline-primary " v-if=" isValid('CanPrintTCAllocationReport')">{{ $t('TemporaryCashAllocationReport.Print') }}</a>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? '' : 'pr-3' ">
                    <div class="col-md-3 col-lg-3 ml-3 mr-3">
                        <div style="z-index:999 !important">
                            <label> {{ $t('TemporaryCashAllocationReport.SelectMonth') }}</label>
                            <monthpicker v-model="fromDate" @update:modelValue="GetPaymentVoucherData(fromDate)" />
                        </div>
                    </div>
                </div>

                <div class="row mt-2">
                    <div v-bind:class="detailShow ? 'col-lg-4' : 'col-lg-12'">

                        <div class="mt-3 table-responsive">
                            <table class="table table-hover table-striped add_table_list_bg mt-3 " v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                                <thead class="">
                                    <tr>
                                        <th style="width:10%;">#</th>
                                        <!--<th style="width:15%;">
                                        {{ $t('TemporaryCashAllocationReport.VoucherNumber') }}
                                    </th>
                                    <th style="width:20%;" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                                        {{ $t('TemporaryCashAllocationReport.Date') }}
                                    </th>-->
                                        <th style="width:50%;">
                                            {{ $t('TemporaryCashAllocationReport.Employee') }}
                                        </th>
                                        <th style="width:40%;">
                                            {{ $t('TemporaryCashAllocationReport.Amount') }}
                                        </th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr v-for="(voucher,index) in vouchersList" v-bind:key="voucher.id">
                                        <td>
                                            {{index+1}}
                                        </td>

                                        <!--<td>
                                        {{voucher.voucherNumber}}
                                    </td>
                                    <td v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                                        {{getDate(voucher.date)}}
                                    </td>-->
                                        <td>
                                            <a href="javascript:void(0)" v-on:click="GetCashIssueDetail(voucher)">
                                                <span v-if="($i18n.locale == 'en' ||isLeftToRight())">{{voucher.userName}}</span>
                                                <span v-else>{{voucher.userNameAr}}</span>
                                            </a>

                                        </td>

                                        <td>
                                            {{currency}}  {{parseFloat(voucher.amount).toFixed(2).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-right' : 'text-left'">
                                            {{ $t('TemporaryCashAllocationReport.TotalAmount') }}
                                        </td>
                                        <td>
                                            {{currency}}  {{parseFloat(sumAmount).toFixed(2).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                        </td>
                                    </tr>
                                </tbody>
                            </table>

                        </div>

                    </div>

                    <div class="col-lg-8" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" v-if="detailShow">

                        <div class="mt-3 table-responsive">
                            <table class="table table-hover table-striped add_table_list_bg mt-3 " v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                                <thead class="">
                                    <tr>
                                        <th style="width:5%;">#</th>
                                        <th style="width:10%;">
                                            {{ $t('TemporaryCashAllocationReport.Date') }}
                                        </th>
                                        <th style="width:20%;">
                                            {{ $t('TemporaryCashAllocationReport.Employee') }}
                                        </th>
                                        <th style="width:15%;">
                                            {{ $t('TemporaryCashAllocationReport.VoucherNumber') }}
                                        </th>
                                        <th style="width:20%;">
                                            {{ $t('TemporaryCashAllocationReport.Amount') }}
                                        </th>
                                        <th style="width:20%;">
                                            {{ $t('TemporaryCashAllocationReport.ReturnAmount') }}
                                        </th>
                                        <th style="width:20%;">
                                            {{ $t('TemporaryCashAllocationReport.Document') }}
                                        </th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr v-for="(voucher,index) in cashIssueDetailList" v-bind:key="voucher.id">
                                        <td>
                                            {{index+1}}
                                        </td>

                                        <td>
                                            {{voucher.date}}
                                        </td>

                                        <td>
                                            {{voucher.userName}}
                                        </td>

                                        <td>
                                            {{voucher.registrationNo}}
                                        </td>

                                        <td>
                                            {{currency}}  {{parseFloat(voucher.amount).toFixed(2).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                        </td>
                                        <td>
                                            {{currency}}  {{parseFloat(voucher.returnAmount).toFixed(2).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                        </td>
                                        <td>
                                            <span v-for="(expense) in voucher.temporaryCashIssueExpenses" v-bind:key="expense.id">{{expense.documentNo}} {{currency}} {{expense.amount}}, </span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-right' : 'text-left'">
                                            {{ $t('TemporaryCashAllocationReport.TotalAmount') }}
                                        </td>
                                        <td colspan="3">
                                            {{currency}}  {{parseFloat(sumAmountDetail).toFixed(2).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                        </td>
                                    </tr>
                                </tbody>
                            </table>

                        </div>

                    </div>

                </div>
            </div>
        </div>
        <temporary-cash-issue-detail-Print :printDetails="cashIssueDetailList" :employeeName="employeeName" :amount="amount" :headerFooter="headerFooter" v-if="printShow" v-bind:key="printRender" />
    </div>
    <div v-else> <acessdenied></acessdenied></div>
</template>

<script>
    import moment from "moment";
    import clickMixin from '@/Mixins/clickMixin'
    export default {
        mixins: [clickMixin],
        props: ['formName'],
        data: function () {
            return {
                printShow: false,
                show: false,
                detailShow: false,
                currency: '',
                fromDate: '',
                month: '',
                year: '',
                headerFooter: {
                    footerEn: '',
                    footerAr: '',
                    company: ''
                },
                vouchersList: [],
                cashIssueDetailList: [],
                sumAmount: 0,
                sumAmountDetail: 0,
                printRender: 0,
                employeeName: '',
                amount: 0,
            }
        },

        methods: {
            PrintInvoice: function () {
                this.GetHeaderDetail();
                var root = this;

                root.printShow = true;
                root.printRender++;
            },

            getBase64Image: function (path) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.headerFooter.company.logoPath = 'data:image/png;base64,' + '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';

                root.$https
                    .get('/Contact/GetBaseImage?filePath=' + path, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                        if (response.data != null) {
                            root.filePath = response.data;
                            root.headerFooter.company.logoPath = 'data:image/png;base64,' + root.filePath;
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
                            this.loading = false;
                            console.log(error);
                        });
            },



            getDate: function (x) {
                return moment(x).format('DD MMM YYYY');
            },

            GetPaymentVoucherData: function (fromDate) {

                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.month = moment(fromDate).format("MM");
                this.year = moment(fromDate).format("YYYY");

                root.$https.get('Report/TemporaryCashAllocationList?month=' + this.month + '&year=' + this.year, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {
                        root.vouchersList = response.data.results;
                        root.sumAmount = root.vouchersList.reduce(function (a, c) { return a + Number((c.amount) || 0) }, 0)
                    }
                });
            },

            GetCashIssueDetail: function (voucher) {

                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                
                this.employeeName = voucher.userName;
                this.amount = voucher.amount;

                root.$https.get('Report/TemporaryCashIssueList?month=' + this.month + '&year=' + this.year + '&employeeId=' + voucher.id, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {
                        root.cashIssueDetailList = response.data.results;
                        root.sumAmountDetail = root.cashIssueDetailList.reduce(function (a, c) { return a + Number((c.amount) || 0) }, 0)
                        root.detailShow = true;
                    }
                });
            },
        },
        created: function () {

            this.$emit('update:modelValue', this.$route.name);
        },

        mounted: function () {

            this.currency = localStorage.getItem('currency');
        }
    }
</script>