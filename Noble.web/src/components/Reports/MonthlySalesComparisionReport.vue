<template>
    <div class="row" v-if="(isValid('CanViewSaleInvoiceReport') ) ">

        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">{{ $t('ComparisionReport.MonthlySalesPurchaseComparisionReport') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('ComparisionReport.Home') }}</a></li>
                                    <li class="breadcrumb-item active">{{ $t('ComparisionReport.MonthlySalesPurchaseComparisionReport') }}</li>
                                </ol>
                            </div>
                            <div class="col-auto align-self-center">
                                <a v-on:click="PrintRdlc(fromDate.monthIndex, fromDate.selectedYear,true)"
                                   href="javascript:void(0);" class="btn btn-sm btn-outline-primary mx-1">
                                    <i class="fas fa-print font-14"></i>
                                    {{ $t('StockReport.Print') }}
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


    <div class="card">
        <div class="card-header">

            <div class="row ">
                <div class="col-6">
                    <label> {{ $t('MonthlySales.SelectMonth') }}</label>
                    <monthpicker v-model="fromDate"  />

                </div>
               
            </div>
        </div>
        <iframe :key="changereport" height="1080" width="auto" :src="reportsrc"></iframe>
        
    </div>
    <monthlysalesReportPrint :invoice="invoice" :headerFooter="headerFooter" :isPrint="isShown" :formName="formName" :month="month" :printDetails="invoiceList" :year="year" v-if="isShown" v-bind:key="printRender"></monthlysalesReportPrint>

        </div>

        <invoicedetailsprint :show="show" v-if="show" :reportsrc="reportsrc1" :changereport="changereportt" @close="show=false" @IsSave="IsSave" />
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
                branchIds: [],
                branchId: '',
                reportsrc: '',
                changereport: 0,
                reportsrc1: '',
                changereportt: 0,
                show: false,
                optionsForSale: [],
                optionsForPurchase: [],
                render: 0,
                fromDate: '',
                toDate: '',
                month: '',
                year: '',
                currentPage: 1,
                pageCount: '',
                rowCount: '',
                invoice: '',
                invoiceList: [],
                resultList: [],
                isShown: false,
                printRender: 0,
                advanceFilters: true,
                combineDate: '',
                openingCash: 0,
                totalBalance: 0,
                counter: 0,

                language: 'Nothing',
                headerFooter: {
                    footerEn: '',
                    footerAr: '',
                    company: ''
                },
            }
        },
        computed: {

            cashAmount: function () {
                return this.invoiceList.reduce(function (a, c) {


                    return a + Number((c.cash.toFixed(3).slice(0, -1)) || 0)


                }, 0)
            },
            bankAmount: function () {
                return this.invoiceList.reduce(function (a, c) {


                    return a + Number((c.bank.toFixed(3).slice(0, -1)) || 0)


                }, 0)
            },
            creditAmount: function () {
                return this.invoiceList.reduce(function (a, c) {


                    return a + Number((c.credit.toFixed(3).slice(0, -1)) || 0)


                }, 0)
            },
            totalAmount: function () {
                return this.invoiceList.reduce(function (a, c) {


                    return a + Number(((c.grossAmount).toFixed(3).slice(0, -1)) || 0)


                }, 0)
            },
            discount: function () {
                return this.invoiceList.reduce(function (a, c) {


                    return a + Number(((c.discount).toFixed(3).slice(0, -1)) || 0)


                }, 0)
            },
            totalReturns: function () {
                return this.invoiceList.reduce(function (a, c) {


                    return a + Number(((c.totalReturns).toFixed(3).slice(0, -1)) || 0)


                }, 0)
            },
            total: function () {
                return this.invoiceList.reduce(function (a, c) {


                    return a + Number(((c.grossAmount - c.totalReturns - c.discount).toFixed(3).slice(0, -1)) || 0)


                }, 0)
            },
            totalTax: function () {
                return this.invoiceList.reduce(function (a, c) {


                    return a + Number(((c.totalTax).toFixed(3).slice(0, -1)) || 0)


                }, 0)
            },
            NetAmount: function () {
                return this.invoiceList.reduce(function (a, c) {


                    return a + Number(((((c.grossAmount - c.totalReturns - c.discount) + c.totalTax)).toFixed(3).slice(0, -1)) || 0)


                }, 0)
            },
            purchasecashAmount: function () {
                return this.invoiceList.reduce(function (a, c) {


                    return a + Number((c.purchaseCash.toFixed(3).slice(0, -1)) || 0)


                }, 0)
            },
            purchasebankAmount: function () {
                return this.invoiceList.reduce(function (a, c) {


                    return a + Number((c.purchaseBank.toFixed(3).slice(0, -1)) || 0)


                }, 0)
            },
            purchasecreditAmount: function () {
                return this.invoiceList.reduce(function (a, c) {


                    return a + Number((c.purchaseCredit.toFixed(3).slice(0, -1)) || 0)


                }, 0)
            },
            purchasetotalAmount: function () {
                return this.invoiceList.reduce(function (a, c) {


                    return a + Number(((c.grossPurchaseAmount).toFixed(3).slice(0, -1)) || 0)


                }, 0)
            },
            purchasediscount: function () {
                return this.invoiceList.reduce(function (a, c) {


                    return a + Number(((c.purchaseDiscount).toFixed(3).slice(0, -1)) || 0)


                }, 0)
            },
            purchasetotalReturns: function () {
                return this.invoiceList.reduce(function (a, c) {


                    return a + Number(((c.purchaseTotalReturns).toFixed(3).slice(0, -1)) || 0)


                }, 0)
            },
            purchasetotal: function () {
                return this.invoiceList.reduce(function (a, c) {


                    return a + Number(((c.grossPurchaseAmount - c.purchaseTotalReturns - c.purchaseDiscount).toFixed(3).slice(0, -1)) || 0)


                }, 0)
            },
            purchasetotalTax: function () {
                return this.invoiceList.reduce(function (a, c) {


                    return a + Number(((c.purchaseTotalTax).toFixed(3).slice(0, -1)) || 0)


                }, 0)
            },
            purchaseNetAmount: function () {
                return this.invoiceList.reduce(function (a, c) {


                    return a + Number(((((c.grossPurchaseAmount - c.purchaseTotalReturns - c.purchaseDiscount) + c.purchaseTotalTax)).toFixed(3).slice(0, -1)) || 0)


                }, 0)
            },


        },
        watch: {
            fromDate: function (fromDate) {
                this.month = moment(fromDate).format("MM");
                this.year = moment(fromDate).format("YYYY");
                this.PrintRdlc(this.month, this.year, false);
            },
            branchIds: function () {
                var root = this;
                this.branchId = '';
                this.branchIds.forEach(function (result) {
                    root.branchId = root.branchId == '' ? result.id : root.branchId + ',' + result.id;
                })
                this.PrintRdlc(this.month, this.year, false);
            }

        },
        methods: {
            GotoPage: function (link) {
                this.$router.push({ path: link });
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
                            root.getBase64Image(root.headerFooter.company.logoPath);
                        }
                    });
            },
            languageChange: function (lan) {
                if (this.language == lan) {

                    var getLocale = this.$i18n.locale;
                    this.language = getLocale;

                    this.$router.go('/CustomerDiscountProducts');

                }


            },
            IsSave: function () {
                this.showReport = !this.showReport;
            },
            getDate: function (date) {
                return moment(date).format('l');
            },
            // GetInvoiceRecord: function (month, year) {

            //     var root = this;
            //     var token = '';
            //     if (token == '') {
            //         token = localStorage.getItem('token');
            //     }
            //     var url;
            //     url = '/Report/MonthlyComparisionSaleReport?year=';



            //     this.$https.get(url + year + '&month=' + month, { headers: { "Authorization": `Bearer ${token}` } })
            //         .then(function (response) {
            //             if (response.data != null) {

            //                 root.invoiceList = response.data;
            //             }
            //         });
            // },


            PrintCsv: function () {

                var root = this;
                {
                    root.$https.post('/Report/SalePurchaseComparisionReport?language=' + this.$i18n.locale + '&month=' + this.month + '&year=' + this.year + '&companyId=' + localStorage.getItem('CompanyID') + '&type=' + this.invoice, root.invoiceList, { headers: { "Authorization": `Bearer ${localStorage.getItem('token')}` }, responseType: 'blob' })
                        .then(function (response) {

                            const url = window.URL.createObjectURL(new Blob([response.data]));
                            const link = document.createElement('a');
                            link.href = url;
                            link.setAttribute('download', 'ComparisionReport.csv');
                            document.body.appendChild(link);
                            link.click();

                        });
                }

            },
            PrintRdlc: function (month, year, val) {
                var companyId = '';
                    companyId = localStorage.getItem('CompanyID');
                
                
                if (val) {
                    this.reportsrc1 = this.$ReportServer + '/ReportManagementModule/SalePurchase/SalePurchaseReports.aspx?companyId=' + companyId + '&year=' + year + '&month=' + month + '&formName=MonthlySalesComparisionReport' + "&Print=" + val + "&branchId=" + this.branchId
                    this.changereportt++;
                    this.show = !this.show;
                }
                else {
                    this.reportsrc = this.$ReportServer + '/ReportManagementModule/SalePurchase/SalePurchaseReports.aspx?companyId=' + companyId + '&year=' + year + '&month=' + month + '&formName=MonthlySalesComparisionReport' + "&Print=" + val + "&branchId=" + this.branchId
                    this.changereport++;
                }
            },
            PrintDetails: function () {

                this.GetHeaderDetail();
                var root = this;
                root.isShown = true;
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

        },
        mounted: function () {

            this.headerFooter.footerEn = localStorage.getItem('TermsInEng');
            this.headerFooter.footerAr = localStorage.getItem('TermsInAr');
            this.language = this.$i18n.locale;
            this.toDate = moment().format("DD MMM YYYY");
            this.fromDate = moment().subtract(15, 'days').format('DD MMM YYYY');
            this.PrintRdlc()
            //this.GetInvoiceRecord(this.fromDate, this.toDate);
            this.render++;
        }
    }
</script>

