<template>
    <div class="row" v-if="(isValid('CanViewSaleInvoiceReport') && formName=='Sale') || (isValid('CanViewPurchaseInvoiceReport') && formName=='Purchase')">
        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title" v-if="formName=='Sale'">{{ $t('InvoicePrintReport.SaleInvoiceReport') }}</h4>
                                <h4 class="page-title" v-if="formName=='Purchase'">{{$t('InvoicePrintReport.PurchaseInvoiceReport')}}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('InvoicePrintReport.Home') }}</a></li>
                                    <li class="breadcrumb-item active" v-if="formName=='Sale'">{{ $t('InvoicePrintReport.SaleInvoiceReport') }}</li>
                                    <li class="breadcrumb-item active" v-if="formName=='Purchase'">{{$t('InvoicePrintReport.PurchaseInvoiceReport')}}</li>
                                </ol>
                            </div>
                            <div class="col-auto align-self-center">
                                <a v-on:click="PrintRdlc(fromDate,toDate,true,invoice,customerId)"
                                   href="javascript:void(0);" class="btn btn-sm btn-outline-primary mx-1">
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
                <div class=" col-lg-3 form-group">
                    <label>{{ $t('StockReport.FromDate') }}</label>
                    <datepicker v-model="fromDate" :key="render" />
                </div>

                <div class=" col-lg-3 form-group">
                    <label>{{ $t('StockReport.ToDate') }}</label>
                    <datepicker v-model="toDate" :key="render" />
                </div>
                <div class=" col-lg-3 form-group" v-if="formName=='Sale'">
                    <label class="text  font-weight-bolder">{{ $t('Sale.Customer') }}</label>
                    <customerdropdown v-model="customerId" ref="CustomerDropdown" />
                </div>
                <div class=" col-lg-3 form-group" v-if="formName=='Purchase'">
                    <label>{{ $t('AddPurchase.Supplier') }} :</label>
                    <supplierdropdown ref=supplierDropdown v-model="customerId" />
                </div>
                <div class=" col-lg-3 form-group" v-if="formName=='Sale'">
                    <label>{{ $t('InvoicePrintReport.SaleInvoice/SaleReturn') }}</label>
                    <multiselect v-model="invoice" :preselect-first="true" :options="optionsForSale" :show-labels="false" placeholder="Select Type">
                    </multiselect>
                </div>
                
                <div class=" col-lg-3 form-group" v-if="formName=='Purchase'">
                    <label> {{ $t('InvoicePrintReport.PurchaseInvoice/PurchaseReturn') }}</label>
                    <multiselect v-model="invoice" :preselect-first="true" :options="optionsForPurchase" :show-labels="false" placeholder="Select Type">
                    </multiselect>
                </div>
                <div class=" col-lg-3 form-group" v-if="allowBranches">
                    <label>{{ $t('Branches.SelectBranch') }}</label>
                    <branch-dropdown v-model="branchIds" :ismultiple="true" :islayout="false" />
                </div>
                
            </div>
            <iframe :key="changereport" height="1000" width="100%" :src="reportsrc"></iframe>
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
import Multiselect from 'vue-multiselect'

    export default {
        mixins: [clickMixin],
        components: {
            Multiselect,
        },

        props: ['formName'],
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
                optionsForSale: [],
                optionsForPurchase: [],
                render: 0,
                fromDate: '',
                toDate: '',
                currentPage: 1,
                pageCount: '',
                rowCount: '',
                invoice: '',
                invoiceList: [],
                resultList: [],
                isShown: false,
                IsPaksitanClient: false,
                isDownload: false,
                printRender: 0,
                advanceFilters: true,
                combineDate: '',
                openingCash: 0,
                totalBalance: 0,
                counter: 0,
                customerId:'',
                language: 'Nothing',
                headerFooter: {
                footerEn: '',
                footerAr: '',
                company: ''
            },
        }
    },
    computed: {
        resultQuery: function () {

            var root = this;
            if (this.invoice == 'Sale Return' || this.invoice == 'عودة البيع') {
                return root.invoiceList.filter((region) => {
                    return region.saleInvoiceId != null


                })
            }
            else if (this.invoice == 'Purchase Return' || this.invoice == 'عودة شراء') {
                return root.invoiceList.filter((region) => {
                    return region.purchaseInvoiceId != null


                })
            }
            else if (this.invoice == 'Sale Invoice' || this.invoice == 'فاتورة البيع') {
                if (this.formName == 'Sale') {
                    return root.invoiceList.filter((region) => {
                        return region.saleInvoiceId == null


                    })
                }


                else {
                    return root.invoiceList;
                }


            }
            else if (this.invoice == 'Purchase Invoice' || this.invoice == 'فاتورة الشراء') {


                if (this.formName == 'Purchase') {
                    return root.invoiceList.filter((region) => {
                        return region.purchaseInvoiceId == null


                    })
                }
                else {
                    return root.invoiceList;
                }


            }

            else {
                return this.invoiceList;

            }
        },
        grossAmount: function () {
            if (this.invoice == 'Both' || this.invoice == 'كلاهما') {
                return this.resultQuery.reduce(function (a, c) {
                    if (c.saleInvoiceId != null || c.purchaseInvoiceId != null) {
                        if (c.taxMethod == 'Inclusive' || c.TaxMethod == 'شامل') {
                            return a - Number(((c.amount - c.vaTamount).toFixed(3).slice(0, -1)) || 0)
                        }
                        else {
                            return a - Number((c.amount.toFixed(3).slice(0, -1)) || 0)
                        }

                    }
                    else {
                        if (c.taxMethod == 'Inclusive' || c.TaxMethod == 'شامل') {
                            return a + Number(((c.amount - c.vaTamount).toFixed(3).slice(0, -1)) || 0)
                        }
                        else {
                            return a + Number((c.amount.toFixed(3).slice(0, -1)) || 0)
                        }
                    }
                }, 0)
            }
            else {
                return this.resultQuery.reduce(function (a, c) {
                    if (c.taxMethod == 'Inclusive' || c.TaxMethod == 'شامل') {
                        return a + Number(((c.amount - c.vaTamount).toFixed(3).slice(0, -1)) || 0)
                    }
                    else {
                        return a + Number((c.amount.toFixed(3).slice(0, -1)) || 0)
                    }

                }, 0)
            }
        },
        discount: function () {
            if (this.invoice == 'Both' || this.invoice == 'كلاهما') {
                return this.resultQuery.reduce(function (a, c) {
                    if (c.saleInvoiceId != null || c.purchaseInvoiceId != null) {
                        return a - Number((c.discount.toFixed(3).slice(0, -1) < 0 ? c.discount.toFixed(3).slice(0, -1) * -1 : c.discount.toFixed(3).slice(0, -1)) || 0)
                    }
                    else {
                        return a + Number((c.discount.toFixed(3).slice(0, -1) < 0 ? c.discount.toFixed(3).slice(0, -1) * -1 : c.discount.toFixed(3).slice(0, -1)) || 0)
                    }
                }, 0)
            }
            else {
                return this.resultQuery.reduce(function (a, c) {


                    return a + Number((c.discount.toFixed(3).slice(0, -1) < 0 ? c.discount.toFixed(3).slice(0, -1) * -1 : c.discount.toFixed(3).slice(0, -1)) || 0)
                }, 0)
            }
        },
        amountwithDiscount: function () {
            if (this.invoice == 'Both' || this.invoice == 'كلاهما') {
                return this.resultQuery.reduce(function (a, c) {
                    if (c.saleInvoiceId != null || c.purchaseInvoiceId != null) {
                        if (c.taxMethod == 'Inclusive' || c.TaxMethod == 'شامل') {
                            return a - Number(((c.amountwithDiscount - c.vaTamount).toFixed(3).slice(0, -1)) || 0)
                        }
                        else {
                            return a - Number((c.amountwithDiscount.toFixed(3).slice(0, -1)) || 0)
                        }
                    }
                    else {
                        if (c.taxMethod == 'Inclusive' || c.TaxMethod == 'شامل') {
                            return a + Number(((c.amountwithDiscount - c.vaTamount).toFixed(3).slice(0, -1)) || 0)
                        }
                        else {
                            return a + Number((c.amountwithDiscount.toFixed(3).slice(0, -1) < 0 ? c.amountwithDiscount.toFixed(3).slice(0, -1) * -1 : c.amountwithDiscount.toFixed(3).slice(0, -1)) || 0)
                        }

                    }
                }, 0)
            }
            else {
                return this.resultQuery.reduce(function (a, c) {

                    if (c.taxMethod == 'Inclusive' || c.TaxMethod == 'شامل') {
                        return a + Number(((c.amountwithDiscount - c.vaTamount).toFixed(3).slice(0, -1)) || 0)
                    }
                    else {
                        return a + Number((c.amountwithDiscount.toFixed(3).slice(0, -1) < 0 ? c.amountwithDiscount.toFixed(3).slice(0, -1) * -1 : c.amountwithDiscount.toFixed(3).slice(0, -1)) || 0)
                    }
                }, 0)
            }

            },
            vaTamount: function () {
                if (this.invoice == 'Both' || this.invoice == 'كلاهما') {
                    return this.resultQuery.reduce(function (a, c) {

                        if (c.saleInvoiceId != null || c.purchaseInvoiceId != null) {
                            return a - Number((c.vaTamount.toFixed(3).slice(0, -1) < 0 ? c.vaTamount.toFixed(3).slice(0, -1) * -1 : c.vaTamount.toFixed(3).slice(0, -1)) || 0)
                        }
                        else {
                            return a + Number((c.vaTamount.toFixed(3).slice(0, -1) < 0 ? c.vaTamount.toFixed(3).slice(0, -1) * -1 : c.vaTamount.toFixed(3).slice(0, -1)) || 0)

                    }

                    }, 0)
                }
                else {
                    return this.resultQuery.reduce(function (a, c) {



                        return a + Number((c.vaTamount.toFixed(3).slice(0, -1) < 0 ? c.vaTamount.toFixed(3).slice(0, -1) * -1 : c.vaTamount.toFixed(3).slice(0, -1)) || 0)


                }, 0)
            }
        },
        totalAmount: function () {
            if (this.invoice == 'Both' || this.invoice == 'كلاهما') {
                return this.resultQuery.reduce(function (a, c) {
                    if (c.saleInvoiceId != null || c.purchaseInvoiceId != null) {
                        return a - Number((c.totalAmount.toFixed(3).slice(0, -1) < 0 ? c.totalAmount.toFixed(3).slice(0, -1) * -1 : c.totalAmount.toFixed(3).slice(0, -1)) || 0)
                    }
                    else {
                        return a + Number((c.totalAmount.toFixed(3).slice(0, -1) < 0 ? c.totalAmount.toFixed(3).slice(0, -1) * -1 : c.totalAmount.toFixed(3).slice(0, -1)) || 0)

                    }
                }, 0)
            }
            else {
                return this.resultQuery.reduce(function (a, c) {


                    return a + Number((c.totalAmount.toFixed(3).slice(0, -1) < 0 ? c.totalAmount.toFixed(3).slice(0, -1) * -1 : c.totalAmount.toFixed(3).slice(0, -1)) || 0)


                    }, 0)
                }
            },
            overAllDiscount: function () {


                return this.resultQuery.reduce(function (a, c) {


                    return a + Number((c.overAllDiscount.toFixed(3).slice(0, -1) < 0 ? c.overAllDiscount.toFixed(3).slice(0, -1) * -1 : c.overAllDiscount.toFixed(3).slice(0, -1)) || 0)


                }, 0)

            },
            overAllVat: function () {


                return this.resultQuery.reduce(function (a, c) {


                    return a + Number((c.overAllUnRegisterVAT.toFixed(3).slice(0, -1) < 0 ? c.overAllUnRegisterVAT.toFixed(3).slice(0, -1) * -1 : c.overAllUnRegisterVAT.toFixed(3).slice(0, -1)) || 0)


                }, 0)

            },

    },
    watch: {
            formName: function () {
                if ((this.$i18n.locale == 'en' || this.isLeftToRight())) {
                    this.optionsForSale = ['Both', 'Sale Invoice', 'Sale Return'];
                    this.optionsForPurchase = ['Both', 'Purchase Invoice', 'Purchase Return'];
                    this.customerId = '';
                }
                else {
                    this.optionsForSale = ['كلاهما', 'فاتورة البيع', 'عودة البيع'];
                    this.optionsForPurchase = ['كلاهما', 'فاتورة الشراء', 'عودة شراء'];

                    }
                    if (this.formName == 'Purchase') {
                        this.invoice = '';
                        this.PrintRdlc(this.fromDate, this.toDate, false, this.invoice,this.customerId);
                        this.customerId = '';

                    }
                    else if (this.formName == 'Sale') {
                        this.invoice = '';
                        this.PrintRdlc(this.fromDate, this.toDate, false, this.invoice,this.customerId);
                        this.customerId = '';
                }

                this.render++
            },
            fromDate: function (fromDate) {

                    this.counter++;
                    if (this.counter != 1) {
                        this.PrintRdlc(fromDate, this.toDate, false, this.invoice,this.customerId);
                    }

            },
            toDate: function (toDate) {

                this.counter++;
                if (this.counter != 2) {
                    //toDate = moment(toDate).add(1, 'days').format("DD MMM YYYY");
                    this.PrintRdlc(this.fromDate, toDate, false, this.invoice,this.customerId);
                }

            },
            invoice: function () {

                this.PrintRdlc(this.fromDate, this.toDate, false, this.invoice,this.customerId);

            },
            branchIds: function () {
                var root = this;
                this.branchId = '';
                this.branchIds.forEach(function (result) {
                    root.branchId = root.branchId == '' ? result.id : root.branchId + ',' + result.id;
                })
                this.PrintRdlc(this.fromDate, this.toDate, false, this.invoice,this.customerId);
            },
            customerId: function()
            {
                this.PrintRdlc(this.fromDate, this.toDate, false, this.invoice, this.customerId);
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

        getDate: function (date) {
            return moment(date).format('l');
        },
       

            IsSave: function () {
                this.showReport = !this.showReport;
            },
            PrintRdlc: function (fromdate, todate, val, invoicetag,customerId) {
                
                var companyId = '';
                companyId = localStorage.getItem('CompanyID');
                

                if (val) {
                    this.reportsrc1 = this.$ReportServer + '/ReportManagementModule/SalePurchase/SalePurchaseReports.aspx?companyId=' + companyId + '&fromDate=' + fromdate + '&toDate=' + todate + '&dateType=' + '&formName=' + this.formName + '&Print=' + val + '&Invoice=' + invoicetag + "&branchId=" + this.branchId + '&customerId=' + customerId;
                    this.changereportt++;
                    this.show = !this.show;
                }
                else {
                    this.reportsrc = this.$ReportServer + '/ReportManagementModule/SalePurchase/SalePurchaseReports.aspx?companyId=' + companyId + '&fromDate=' + fromdate + '&toDate=' + todate + '&dateType=' + '&formName=' + this.formName + '&Print=' + val + '&Invoice=' + invoicetag + "&branchId=" + this.branchId + '&customerId=' + customerId
                    this.changereport++;
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

        this.GetHeaderDetail();
        this.IsPaksitanClient = localStorage.getItem('IsPaksitanClient') == "true" ? true : false;

        this.allowBranches = localStorage.getItem('AllowBranches') == 'true' ? true : false;

        if ((this.$i18n.locale == 'en' || this.isLeftToRight())) {
            this.optionsForSale = ['Both', 'Sale Invoice', 'Sale Return'];
            this.optionsForPurchase = ['Both', 'Purchase Invoice', 'Purchase Return'];

        }
        else {
            this.optionsForSale = ['كلاهما', 'فاتورة البيع', 'عودة البيع'];
            this.optionsForPurchase = ['كلاهما', 'فاتورة الشراء', 'عودة شراء'];

            }
            this.headerFooter.footerEn = localStorage.getItem('TermsInEng');
            this.headerFooter.footerAr = localStorage.getItem('TermsInAr');
            this.language = this.$i18n.locale;
            this.toDate = moment().format("DD MMM YYYY");
            this.fromDate = moment().subtract(15, 'days').format('DD MMM YYYY');
            // this.GetInvoiceRecord(this.fromDate, this.toDate);
            this.PrintRdlc(this.fromDate, this.toDate, this.invoice, this.customerId);
            this.render++;
        }
    }
</script>
