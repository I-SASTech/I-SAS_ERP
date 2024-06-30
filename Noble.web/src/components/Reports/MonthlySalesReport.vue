<template>
    <div v-bind:style="$i18n.locale == 'ar' ? languageChange('en') : languageChange('ar')" v-if="(isValid('CanViewSaleInvoiceReport') && formName=='Sale') || (isValid('CanViewPurchaseInvoiceReport') && formName=='Purchase')">
        <div v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
            <div>
                <div class="card">
                    <div class="card-header">
                        <div class="row">
                            <div class="col-md-6 col-lg-6">
                                <div class="form-group ">
                                    <h5 class="card-title DayHeading" v-if="formName=='Sale'">{{ $t('MonthlySales.MonthlySalesReport') }}</h5>
                                    <h5 class="card-title DayHeading" v-if="formName=='Purchase'">{{ $t('MonthlySales.MonthlyPurchaseReport') }}</h5>
                                </div>
                            </div>
                            <div class="col-md-6 col-lg-6">
                                <div class="form-group " v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-right' : 'text-left' ">
                                    <router-link :to="'/AllReports'">
                                        <a href="javascript:void(0)" class="btn btn-outline-primary "><i class="fas fa-arrow-circle-left fa-lg"></i> </a>
                                    </router-link>
                                    <a href="javascript:void(0)" class="btn btn-outline-primary " v-on:click="PrintDetails(false)" v-if="(isValid('CanPrintSaleInvoiceReport') && formName=='Sale') || (isValid('CanPrintPurchaseInvoiceReport') && formName=='Purchase')">{{ $t('StockReport.Print') }}</a>
                                    <a href="javascript:void(0)" class="btn btn-outline-primary " v-on:click="PrintDetails(true)" v-if="(isValid('CanPrintSaleInvoiceReport') && formName=='Sale') || (isValid('CanPrintPurchaseInvoiceReport') && formName=='Purchase')"><i class="far fa-file-pdf"></i> Export PDF</a>
                                    <!--<a href="javascript:void(0)" class="btn btn-outline-primary " v-on:click="PrintCsv">{{ $t('MonthlySales.ExportCsv') }}</a>-->
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row" v-bind:key="render" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? '' : 'pr-3' ">
                        <div class="col-md-3 col-lg-3 ml-3 mr-3">
                            <div style="z-index:999 !important">
                                <label> {{ $t('MonthlySales.SelectMonth') }}</label>
                                <monthpicker v-model="fromDate" />
                            </div>
                        </div>





                    </div>


                    <div class="card-body">
                        <div>
                            <div class="table-responsive">
                                <table class="table table-striped table-hover table_list_bg">


                                    <thead>
                                        <tr>
                                            <th>#</th>
                                            <th class="text-center">
                                                {{ $t('MonthlySales.Date') }}
                                            </th>
                                            <th class="text-center" v-if="formName=='Sale'">
                                                Gross Sales

                                            </th>
                                            <th class="text-center" v-if="formName=='Purchase'">
                                                Gross Purchases

                                            </th>
                                            <th class="text-center">
                                                {{ $t('MonthlySales.Discount') }}

                                            </th>



                                            <th class="text-center">
                                                {{ $t('MonthlySales.Return') }}

                                            </th>
                                            <th class="text-center" v-if="formName=='Purchase'">
                                                Net Purchase Amount

                                            </th>
                                            <th class="text-center" v-if="formName=='Sale'">
                                                Net Sale Amount

                                            </th>
                                            <th class="text-center">
                                                {{ $t('MonthlySales.VATAmount') }}

                                            </th>
                                            <th class="text-center">
                                                Total Amount

                                            </th>
                                            <th class="text-center">
                                                {{ $t('MonthlySales.Cash') }}

                                            </th>
                                            <th class="text-center">
                                                {{ $t('MonthlySales.Bank') }}

                                            </th>
                                            <th class="text-center">
                                                {{ $t('MonthlySales.Credit') }}

                                            </th>



                                        </tr>

                                    </thead>

                                    <tbody>
                                        <tr v-for="(invoices,index) in invoiceList" v-bind:key="invoices.id">
                                            <td class="text-center">
                                                {{index+1}}
                                            </td>


                                            <td class="text-center">
                                                {{invoices.date}}
                                            </td>
                                            <td class="text-center">
                                                {{(parseFloat(invoices.grossAmount)).toFixed(3).slice(0,-1)}}
                                            </td>
                                            <td class="text-center">
                                                {{(parseFloat(invoices.discount)).toFixed(3).slice(0,-1)}}
                                            </td>



                                            <td class="text-center">
                                                {{(parseFloat(invoices.totalReturns)).toFixed(3).slice(0,-1)}}
                                            </td>
                                            <td class="text-center">
                                                {{(parseFloat(invoices.grossAmount-invoices.totalReturns-invoices.discount)).toFixed(3).slice(0,-1)}}
                                            </td>
                                            <td class="text-center">
                                                {{(parseFloat(invoices.totalTax)).toFixed(3).slice(0,-1)}}
                                            </td>
                                            <td class="text-center">
                                                {{(parseFloat((invoices.grossAmount-invoices.totalReturns-invoices.discount)+invoices.totalTax)).toFixed(3).slice(0,-1)}}
                                            </td>
                                            <td class="text-center">
                                                {{(parseFloat(invoices.cash)).toFixed(3).slice(0,-1)}}
                                            </td>
                                            <td class="text-center">
                                                {{(parseFloat(invoices.bank)).toFixed(3).slice(0,-1)}}
                                            </td>
                                            <td class="text-center">
                                                {{(parseFloat(invoices.credit)).toFixed(3).slice(0,-1)}}
                                            </td>


                                        </tr>
                                        <tr style="font-size:15px;font-weight:bold;background-color:azure">
                                            <td colspan="2" class="text-center" style="padding-top:60px">
                                            </td>


                                            <td class="text-center" style="padding-top:60px">
                                                {{(parseFloat(totalAmount)).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                            </td>
                                            <td class="text-center" style="padding-top:60px">
                                                {{(parseFloat(discount)).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                            </td>
                                            <td class="text-center" style="padding-top:60px">
                                                {{(parseFloat(totalReturns)).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                            </td>
                                            <td class="text-center" style="padding-top:60px">
                                                {{(parseFloat(total)).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                            </td>
                                            <td class="text-center" style="padding-top:60px">
                                                {{(parseFloat(totalTax)).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                            </td>
                                            <td class="text-center" style="padding-top:60px">
                                                {{(parseFloat(NetAmount)).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                            </td>
                                            <td class="text-center" style="padding-top:60px">
                                                {{(parseFloat(cashAmount)).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                            </td>
                                            <td class="text-center" style="padding-top:60px">
                                                {{(parseFloat(bankAmount)).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                            </td>
                                            <td class="text-center" style="padding-top:60px">
                                                {{(parseFloat(creditAmount)).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                            </td>

                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>

                    </div>


                </div>
            </div>
        </div>
        <monthlysalesReportPrint :invoice="invoice" :headerFooter="headerFooter" :isPrint="isShown" :formName="formName" :month="month" :printDetails="invoiceList" :year="year" v-if="isShown" v-bind:key="printRender"></monthlysalesReportPrint>
        <MonthlyPdf :invoice="invoice" :headerFooter="headerFooter" :isPrint="isDownload" :formName="formName" :month="month" :printDetails="invoiceList" :year="year" v-if="isDownload" v-bind:key="printRenderpdf"></MonthlyPdf>
    </div>
    <div v-else> <acessdenied></acessdenied></div>
</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'
    import moment from "moment";

    export default {
        mixins: [clickMixin],


        props: ['formName'],
        data: function () {
            return {
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
                isDownload: false,
                printRender: 0,
                printRenderpdf: 0,
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


        },
        watch: {
            fromDate: function (fromDate) {
                this.month = moment(fromDate).format("MM");
                this.year = moment(fromDate).format("YYYY");
                this.GetInvoiceRecord(this.month, this.year);
            },
            formName: function () {

                if (this.formName == 'Purchase') {
                    this.invoiceList = [];
                    //this.GetInvoiceRecord(this.fromDate, this.toDate);

                }
                else if (this.formName == 'Sale') {
                    this.invoiceList = [];
                    //this.GetInvoiceRecord(this.fromDate, this.toDate);

                }

                this.render++
            },


        },
        methods: {
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

            getDate: function (date) {
                return moment(date).format('l');
            },
            GetInvoiceRecord: function (month, year) {
                
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                var url;
                if (this.formName == 'Purchase') {

                    url = '/Report/MonthlyPurchaseReport?year=';
                }
                else if (this.formName == 'Sale') {
                    url = '/Report/MonthlySaleReport?year=';
                }


                this.$https.get(url + year + '&month=' + month, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data != null) {

                            root.invoiceList = response.data;
                        }
                    });
            },


            PrintCsv: function () {
                var root = this;
                if (this.formName == 'Purchase') {
                    root.$https.post('/Report/PurchaseInvoiceCsv?language=' + this.$i18n.locale + '&fromDate=' + this.fromDate + '&toDate=' + this.toDate + '&companyId=' + localStorage.getItem('CompanyID') + '&type=' + this.invoice, root.invoiceList, { headers: { "Authorization": `Bearer ${localStorage.getItem('token')}` }, responseType: 'blob' })
                        .then(function (response) {

                            const url = window.URL.createObjectURL(new Blob([response.data]));
                            const link = document.createElement('a');
                            link.href = url;
                            link.setAttribute('download', 'PurchaseInvoiceReport.csv');
                            document.body.appendChild(link);
                            link.click();

                        });
                }
                else {
                    root.$https.post('/Report/SaleInvoiceCsv?language=' + this.$i18n.locale + '&fromDate=' + this.fromDate + '&toDate=' + this.toDate + '&companyId=' + localStorage.getItem('CompanyID') + '&type=' + this.invoice, root.invoiceList, { headers: { "Authorization": `Bearer ${localStorage.getItem('token')}` }, responseType: 'blob' })
                        .then(function (response) {

                            const url = window.URL.createObjectURL(new Blob([response.data]));
                            const link = document.createElement('a');
                            link.href = url;
                            link.setAttribute('download', 'SaleReport.csv');
                            document.body.appendChild(link);
                            link.click();

                        });
                }
            },
            PrintDetails: function (download) {
                
                var root = this;
                if (download) {
                    this.GetHeaderDetail();
                    root.isDownload = true;
                    root.printRenderpdf++;
                }
                else {
                    this.GetHeaderDetail();
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
            this.fromDate = moment().format("DD MMM YYYY");
            this.toDate = moment().format("DD MMM YYYY");
            this.fromDate = moment().subtract(15, 'days').format('DD MMM YYYY');
            //this.GetInvoiceRecord(this.fromDate, this.toDate);
            this.render++;
        }
    }
</script>
<style scoped>


    table {
        width: 100%;
    }

    thead, tbody tr {
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
    }
  
</style>