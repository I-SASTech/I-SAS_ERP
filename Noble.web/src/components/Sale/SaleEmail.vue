<template>
    <div class="container-fluid">
        <div class="row" v-bind:style="$i18n.locale == 'ar' ? languageChange('en') : languageChange('ar')">
            <div class="col-xs-12 col-sm-9 col-md-9 col-lg-9">
                <div class="card">
                    <div class="card-body">
                        <div class="row" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                            <div class="col-xs-8 col-sm-8 col-md-8 col-lg-8" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                                <h5 class="view_page_title">{{ $t('InvoiceView.SaleInvoice') }}</h5>
                            </div>
                            <div class="col-xs-4 col-sm-4 col-md-4 col-lg-4" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-right' : 'text-left'">
                                <!--<button class="btn btn-outline-danger  mr-2"
                                    v-on:click="goToSale">
                                <i class="fas fa-arrow-circle-left fa-lg"></i>
                            </button>-->

                            </div>

                            <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                                <h5>{{ $t('InvoiceView.From') }}</h5>
                                <div class="card" style="border: 1px #dddddd solid;">
                                    <div class="card-body">
                                        <label>{{($i18n.locale == 'en' ||isLeftToRight())?headerFooter.company.nameEnglish:headerFooter.company.companyNameArabic}}</label>
                                        <br />
                                        <label>{{($i18n.locale == 'en' ||isLeftToRight())?headerFooter.company.addressEnglish:headerFooter.company.addressArabic}}</label>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                                <h5>{{ $t('InvoiceView.InvoiceTo') }}</h5>
                                <div class="card" style="border: 1px #dddddd solid;">
                                    <div class="card-body">
                                        <label v-if="!sale.isCredit">{{($i18n.locale == 'en' ||isLeftToRight())?sale.cashCustomer:(sale.cashCustomer==''?sale.cashCustomer:sale.cashCustomer)}}</label> <label>{{($i18n.locale == 'en' ||isLeftToRight())? (sale.customerNameEn==''?sale.customerNameAr:sale.customerNameEn):(sale.customerNameAr==''?sale.customerNameEn:sale.customerNameAr)}}</label>
                                        <br />
                                        <label>{{sale.customerAddress}}</label>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                            <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                                <!--<div class="row">
                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                    <label class="font-weight-bold">{{ $t('Sale.Mobile') }}</label>
                                    <p>{{ sale.mobile }}</p>
                                </div>
                            </div>-->
                                <!--<div class="row">
                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                    <label class="font-weight-bold">{{ $t('InvoiceView.Area') }}</label>
                                    <p>{{sale.areaName}}</p>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                    <label class="font-weight-bold">{{ $t('InvoiceView.DispatchNote') }}</label>
                                    <p>{{sale.dispatchNote}}</p>
                                </div>
                            </div>-->
                            </div>
                            <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">

                            </div>
                        </div>

                        <div class="row" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? '' : 'arabicLanguage'">
                            <div class="col-md-12">
                                <sale-email-item :saleItems="sale.saleItems" ref="childComponentRef" :key="randerItem" :wareHouseId="sale.wareHouseId" @update:modelValue="SaveSaleItems" @summary="updateSummary" />

                                <div class="row ">
                                    <div class="col-sm-12" v-if="!sale.isCredit">
                                        <table class="table report-tbl">
                                            <tr style="font-size:19px;font-weight:bold;">
                                                <td style=" color: black;" colspan="5" v-bind:style="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-align:left' : 'text-align:right'">Payment Mode</td>
                                            </tr>
                                            <tr style="font-size:17px;padding-bottom:10px;" v-for="payment in sale.paymentTypes" v-bind:key="payment.id">
                                                <td style="color: black;border-top: 0; padding-bottom:4px;padding-top:4px;" colspan="3" v-bind:style="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-align:left' : 'text-align:right'">{{payment.name}} ({{getType(payment.paymentType)}}) <span v-if="payment.paymentType==2">(Rate={{payment.amountCurrency}}* Amount={{payment.rate}})</span>:</td>
                                                <td style="color: black;border-top: 0;padding-bottom:4px;padding-top:4px;" colspan="2" v-bind:style="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-align:right' : 'text-align:left'">{{parseFloat(payment.amount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                            </tr>
                                            <tr style="font-size:17px;">
                                                <th style="border-top: 0; padding-top: 0; color: black;" colspan="3" v-bind:style="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-align:left' : 'text-align:right'">Change Due:</th>
                                                <th colspan="2" style=" border-top: 0; padding-top: 0; color: black;" v-bind:style="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-align:right' : 'text-align:left'"> {{ sale.change }}</th>
                                            </tr>
                                        </table>
                                    </div>
                                    <div class="col-sm-12" v-else>
                                        <div class=" table-responsive">
                                            <table class="table table_list_bg">
                                                <thead>
                                                    <tr>
                                                        <th>#</th>
                                                        <th style="width:20%;">{{ $t('InvoiceView.VoucherNumber') }} </th>
                                                        <th style="width:20%;">{{ $t('InvoiceView.Date') }} </th>
                                                        <th class="text-right">{{ $t('InvoiceView.Amount') }} </th>
                                                        <th class="text-center">{{ $t('InvoiceView.PaymentMode') }} </th>
                                                        <th>{{ $t('InvoiceView.Description') }} </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr v-for="(payment,index) in sale.paymentVoucher" v-bind:key="index">
                                                        <td>
                                                            {{index+1}}
                                                        </td>
                                                        <th>{{payment.voucherNumber}}</th>
                                                        <th>{{getDate(payment.date)}}</th>
                                                        <th class="text-right">{{currency}} {{parseFloat(payment.amount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</th>
                                                        <th class="text-center"><span v-if="payment.paymentMode==0">Cash</span><span v-if="payment.paymentMode==1">Bank</span></th>
                                                        <th>{{payment.narration}}</th>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                </div>

                                <!--<div class="row">
                                <div class="col-md-6 ">
                                    <button class="btn btn-primary mr-2" v-on:click="Attachment()">
                                        {{ $t('InvoiceView.Attachment') }}
                                    </button>
                                </div>
                                <div class="col-md-6" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-right' : 'text-left'">
                                    <button class="btn btn-danger mr-2" v-on:click="goToSale">
                                        {{ $t('InvoiceView.Cancel') }}
                                    </button>
                                </div>
                            </div>-->

                            </div>
                        </div>

                    </div>
                </div>
            </div>
            <div class="col-xs-12 col-sm-3 col-md-3 col-lg-3" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                <div class="card">
                    <div class="card-body">
                        <div class="row">
                            <div class="col-xs-6 col-sm-6 col-md-6 col-lg-6">
                                <h5 class="view_page_title">{{ $t('InvoiceView.BasicInfo') }}</h5>
                            </div>
                            <div class="col-xs-6 col-sm-6 col-md-6 col-lg-6">
                                <vue-qrcode v-bind:value="qrValue" style="width:140px;" />
                            </div>
                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                <label class="invoice_lbl">{{ $t('InvoiceView.SaleInvoice') }}#</label>
                                <hr style="margin-top: 0.3rem; margin-bottom: 0.1rem;" />
                                <label>{{sale.registrationNo}}-<span v-if='sale.isCredit'> Credit</span><span v-else> Cash</span></label>
                                <hr style="margin-top: 0.1rem;" />
                            </div>

                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 mt-2">
                                <label class="invoice_lbl">{{ $t('InvoiceView.WareHouse') }}</label>
                                <hr style="margin-bottom: 0.1rem; margin-top: 0.3rem;" />
                                <label> {{($i18n.locale == 'en' ||isLeftToRight())?sale.wareHouseName:(sale.wareHouseNameAr==''?sale.wareHouseName:sale.wareHouseNameAr)}}</label>
                                <hr style="margin-top: 0.1rem;" />
                            </div>
                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 mt-2">
                                <label class="invoice_lbl">{{ $t('InvoiceView.Mobile') }}</label>
                                <hr style="margin-bottom: 0.1rem; margin-top: 0.3rem;" />
                                <label> {{ sale.mobile }}</label>
                                <hr style="margin-top: 0.1rem;" />
                            </div>
                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 mt-2">
                                <label class="invoice_lbl">{{ $t('InvoiceView.Date') }}</label>
                                <hr style="margin-bottom: 0.1rem; margin-top: 0.3rem;" />
                                <label>{{sale.date}}</label>
                                <hr style="margin-top: 0.1rem;" />
                            </div>
                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 mt-2">
                                <label class="invoice_lbl">{{ $t('InvoiceView.DueDate') }}</label>
                                <hr style="margin-bottom: 0.1rem; margin-top: 0.3rem;" />
                                <label>{{sale.dueDate}}</label>
                                <hr style="margin-top: 0.1rem;" />
                            </div>
                            <!--<div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 mt-2">
                            <label class="invoice_lbl">Send Copy To</label>
                            <hr style="margin-bottom: 0.1rem; margin-top: 0.3rem;" />
                            <label>Email</label>
                            <hr style="margin-top: 0.1rem;" />
                        </div>
                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 mt-2">
                            <button class="btn btn-primary btn-block">Send Invoice</button>
                        </div>-->
                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-6 mt-2">
                                <button class="btn btn-light btn-block text-left" v-on:click="DownloadPdf">PDF <i class="fas fa-file-pdf float-right" style="color:#EB5757;"></i></button>
                            </div>
                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-6 mt-2">
                                <button class="btn btn-light btn-block text-left">Sheets <i class="fas fa-file-excel float-right" style="color:#198754;"></i></button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-12">

                <!--<div class="card" style="background-color:#EBF2FF;margin-bottom:0;">
        <div class="card-body">

            <div class="row">
                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-3 mb-2">
                    <label>{{ $t('Sale.Customer') }} :</label>
                    <div v-if="!sale.isCredit">{{($i18n.locale == 'en' ||isLeftToRight())?sale.cashCustomer:(sale.cashCustomer==''?sale.cashCustomer:sale.cashCustomer)}}</div> <div>{{($i18n.locale == 'en' ||isLeftToRight())?sale.customerNameEn:(sale.customerNameAr==''?sale.customerNameEn:sale.customerNameAr)}}</div>
                </div>
            </div>
        </div>
    </div>-->

                <bulk-attachment :documentid="sale.id" :show="show" v-if="show" @close="attachmentSave" />
                <!--<saleInvoice :printDetails="printDetails" v-if="printDetails.length != 0" v-bind:key="printRender"></saleInvoice>-->
            </div>

        </div>
        <div class="card-footer col-md-3">
            <loading :name="loading" v-model:active="loading"
                     :can-cancel="false"
                     :is-full-page="true"></loading>
        </div>
    </div>

</template>
<script>
    import moment from "moment";
    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    
    import clickMixin from '@/Mixins/clickMixin'
    export default {
        components: {
            Loading
        },
        name: "AddSale",

        mixins: [clickMixin],
        data: function () {
            return {
                isDownloadPdf:false,
                printRenderEmail:0,
                randerItem:0,
                isRaw: '',
                isMultiUnit: '',
                isArea: '',
                show: false,
                removeCheck: false,
                options: [],
                value: "",
                bankList: [],
                currencyList: [],
                autoNumber: '',
                autoNumberCredit: '',
                toggleCash: false,
                toggleCredit: true,
                currency: '',
                qrValue: '',
                togglePayCash: 'Cash',
                language: 'Nothing',

                toggleBank: 'Bank',
                toggleOtherCurrency: 'OtherCurrency',

                rendered: 0,
                sale: {
                    id: "00000000-0000-0000-0000-000000000000",
                    dispatchNote: [],
                    date: "",
                    registrationNo: "",
                    customerId: null,
                    dueDate: "",
                    wareHouseId: "",
                    saleItems: [],
                    isCredit: false,
                    cashCustomer: "",
                    mobile: "",
                    code: "",
                    invoiceType: "",
                    employeeId: "",
                    areaId: "",
                    change: 0,
                    salePayment: {
                        dueAmount: 0,
                        received: 0,
                        balance: 0,
                        change: 0,
                        paymentTypes: [],
                        paymentType: 'Cash',
                        paymentOptionId: null,
                    },
                    otherCurrency: {
                        currencyId: null,
                        rate: 0,
                        amount: 0
                    }
                },
                printId: '00000000-0000-0000-0000-000000000000',
                printDetails: [],
                printRender: 0,
                loading: false,
                summary: Object,
                companyId: '00000000-0000-0000-0000-000000000000',
                CompanyID: '',
                UserID: '',
                employeeId: '',
                isDayAlreadyStart: false,
                IsProduction: false,
                headerFooter: {
                    footerEn: '',
                    footerAr: '',
                    company: ''
                },
            };
        },
        computed: {
            isPaymentValid: function () {
                var paymentTypesAmount;
                if (this.sale.salePayment.paymentTypes.length > 0) {
                    paymentTypesAmount = this.sale.salePayment.paymentTypes.reduce((total, payment) =>
                        total + payment.amount, 0);
                } else {
                    paymentTypesAmount = 0;
                }

                if (this.sale.salePayment.paymentType == "Bank") {
                    if (this.sale.salePayment.paymentOptionId == "00000000-0000-0000-0000-000000000000" ||
                        this.sale.salePayment.paymentOptionId == null) {
                        return false;
                    }
                }

                if (this.sale.salePayment.paymentType == "OtherCurrency") {
                    if (this.sale.otherCurrency.currencyId == "00000000-0000-0000-0000-000000000000" ||
                        this.sale.otherCurrency.currencyId == null) {
                        return false;
                    }
                }

                if ((parseFloat(this.sale.salePayment.received) <= parseFloat(this.sale.salePayment.dueAmount))) {
                    if (((parseFloat(this.sale.salePayment.received) + paymentTypesAmount)) >= this.sale.salePayment.dueAmount) {
                        return true
                    }
                } else {
                    if (((parseFloat(this.sale.salePayment.received) + paymentTypesAmount)) >= this.sale.salePayment.dueAmount) {
                        return true
                    }
                }
                return false;
            },
            calulateTotalQty: function () {
                return this.sale.saleItems.reduce(function (a, c) { return a + (Number((c.quantity) || 0) > 0 ? Number((c.quantity) || 0) : 0) }, 0)
            },
            calulateNetTotal: function () {
                return this.sale.saleItems.reduce(function (a, c) { return a + Number((c.total + c.includingVat) || 0) }, 0)
            },
            calulateTotalExclVAT: function () {
                return this.sale.saleItems.reduce(function (a, c) { return a + Number((c.total) || 0) }, 0)
            },
            calulateTotalVAT: function () {
                return this.sale.saleItems.reduce(function (a, c) { return a + Number((c.includingVat + c.inclusiveVat) || 0) }, 0)
            },
            calulateTotalInclusiveVAT: function () {
                return this.sale.saleItems.reduce(function (a, c) { return a + Number((c.inclusiveVat) || 0) }, 0)
            },
            calulateDiscountAmount: function () {
                return this.sale.saleItems.reduce(function (a, c) { return a + Number(c.discountAmount || 0) }, 0)
            },
            calulateBundleAmount: function () {
                return this.sale.saleItems.reduce(function (a, c) { return a + Number(c.bundleAmount || 0) }, 0)
            }

        },
        methods: {
            DownloadPdf: function () {
                this.printRenderEmail++
                this.isDownloadPdf =true
            },
            calulateDiscountAmount1: function () {
                return this.sale.saleItems.reduce(function (a, c) { return a + Number(c.discountAmount || 0) }, 0)
            },
            calulateBundleAmount1: function () {
                return this.sale.saleItems.reduce(function (a, c) { return a + Number(c.bundleAmount || 0) }, 0)
            },
            calulateNetTotalWithVAT: function () {
                var total = this.sale.saleItems.reduce(function (a, c) { return a + Number((c.total + c.includingVat) || 0) }, 0);
                var grandTotal = parseFloat(total) - (this.calulateDiscountAmount1() + this.calulateBundleAmount1())
                return (parseFloat(grandTotal).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,"));
            },
            calulateTotalVATofInvoice: function () {
                var total = this.sale.saleItems.reduce(function (a, c) { return a + Number((c.includingVat + c.inclusiveVat) || 0) }, 0);
                return (parseFloat(total).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,"));
            },
            Attachment: function () {
                this.show = true;
            },

            attachmentSave: function () {
                this.show = false;
            },

            languageChange: function (lan) {
                if (this.language == lan) {
                    if (this.sale.id == '00000000-0000-0000-0000-000000000000') {

                        var getLocale = this.$i18n.locale;
                        this.language = getLocale;

                        this.$router.go('/addSale');
                    }
                    else {
                        this.$swal({
                            title: this.$t('InvoiceView.Error'),
                            text: this.$t('InvoiceView.ChangeLanguageError'),
                            confirmButtonClass: "btn btn-danger",
                            icon: 'error',
                            timer: 4000,
                            timerProgressBar: true,
                        });
                    }
                }


            },

            GetDispatchNoteDetail: function (id, index) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                if (id != undefined) {
                    root.$https.get('/Sale/DispatchNoteDetail?Id=' + id[index - 1].id, { headers: { "Authorization": `Bearer ${token}` } })
                        .then(function (response) {
                            if (response.data != null) {
                                response.data.dispatchNoteItems.forEach(function (dn) {
                                    root.$refs.childComponentRef.addProduct(dn.productId, dn.product, dn.quantity);
                                });
                            }
                        },
                            function (error) {
                                this.loading = false;
                                console.log(error);
                            });
                }
            },
            getBank: function () {
                var root = this;
                var token = '';

                if (token == '') {
                    token = localStorage.getItem('token');
                }

                this.$https.get('/Product/PaymentOptionsList', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {
                        root.bankList = response.data.paymentOptions;
                    }
                })
            },
            getType: function (type) {
                if (type == '1') {
                    return 'Bank'
                }
                else if (type == '0') {
                    return 'Cash'
                }
                else {
                    return 'Other Currency';
                }
            }
            , selectBank: function (id) {
                this.isActive = true;
                this.sale.salePayment.paymentOptionId = id;
            },
            getCurrency: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                this.$https.get('/Product/CurrencyList', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {
                        root.currencyList = response.data.currencies;
                    }
                })
            },
            selectCurrency: function (id) {

                this.isActive = true;
                this.sale.otherCurrency.currencyId = id;
            },
            isCredit: function (credit) {

                this.sale.isCredit = credit;
                if (!this.sale.isCredit) {
                    this.sale.customerId = null;
                }
            },
            isPayment: function (credit) {
                this.sale.salePayment.paymentType = credit;
            },
            emptyCashCustomer: function (customerId) {
                this.sale.customerId = customerId;

                if (this.sale.customerId != '00000000-0000-0000-0000-000000000000' && this.sale.customerId != null) {
                    this.sale.cashCustomer = "Walk-In";
                    this.sale.mobile = "";
                }

                var root = this;
                var token = "";
                if (token == '') {
                    token = localStorage.getItem("token");
                }
                root.options = [];
                this.$https
                    .get('/Sale/DispatchNoteList?isDropdown=' + true + '&customerId=' + customerId, {
                        headers: { Authorization: `Bearer ${token}` },
                    })
                    .then(function (response) {

                        if (response.data != null) {
                            response.data.results.forEach(function (dn) {
                                root.options.push({
                                    id: dn.id,
                                    name: dn.registrationNumber,
                                });
                            });
                        }
                    })
                    .then(function () {
                        root.value = root.options.find(function (x) {
                            return x.id == root.values;
                        });
                    });
            },

            setCustomer: function (value) {
                console.log(value);
            },
            OnEditInvoice: function () {


                var total = 0;
                if (this.sale.salePayment.paymentTypes != '' && this.sale.salePayment.paymentTypes != undefined) {
                    this.sale.salePayment.paymentTypes.forEach(x => {

                        total += x.amount;
                    });
                    total = total + parseFloat(this.sale.salePayment.received);
                }
                if (total != 0) {
                    if (this.sale.salePayment.balance <= total) {
                        this.sale.salePayment.balance = 0;
                    }
                    this.sale.salePayment.change = total - this.sale.salePayment.dueAmount;
                }
            },
            calculatBalance: function (received) {

                if (received == '') {
                    received = 0;
                }
                var paymentTypesAmount;
                if (this.sale.salePayment.paymentTypes.length > 0) {
                    paymentTypesAmount = this.sale.salePayment.paymentTypes.reduce((total, payment) =>
                        total + payment.amount, 0);
                } else {
                    paymentTypesAmount = 0;
                }

                this.sale.salePayment.balance = (parseFloat(received) + paymentTypesAmount) - this.sale.salePayment.dueAmount < 0 ? (parseFloat(received) + paymentTypesAmount) - this.sale.salePayment.dueAmount : 0;
                this.sale.salePayment.change = (parseFloat(received) + paymentTypesAmount) - this.sale.salePayment.dueAmount > 0 ? (parseFloat(received) + paymentTypesAmount) - this.sale.salePayment.dueAmount : 0;
                this.sale.change = this.sale.salePayment.change;
            },

            addPayment: function (amount, paymentType) {
                this.sale.salePayment.received = 0;
                var payment = this.sale.salePayment.paymentTypes.find((x) => x.paymentType == paymentType)
                if (payment != undefined) {
                    payment.amount += parseFloat(amount);
                } else {
                    this.sale.salePayment.paymentTypes.push({ paymentType: paymentType, amount: parseFloat(amount) });
                }

                this.calculatBalance(0);
            },

            removeFromPaymentList: function (paymentType) {
                this.sale.salePayment.paymentTypes = this.sale.salePayment.paymentTypes.filter((x) =>
                    x.paymentType != paymentType);

                this.calculatBalance(0);
            },

            updateSummary: function (summary) {
                this.summary = summary;
                this.sale.salePayment.dueAmount = parseFloat(this.summary.withVat)
            },
            SaveSaleItems: function (saleItems) {
                this.sale.saleItems = saleItems;
            },

            goToSale: function () {

                this.$router.push({
                    path: '/sale',
                    query: {
                        data: 'AddSales'
                    }
                });
            },
            getDate: function (date) {
                if (date == null || date == undefined) {
                    return "";
                }
                else {
                    return moment(date).format('LLL');
                }
            },
            GetHeaderDetail: function () {
                
                var root = this;
                var token = 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoibm9ibGVAZ21haWwuY29tIiwic3ViIjoibm9ibGVAZ21haWwuY29tIiwianRpIjoiOGU2MTI1NzYtMDNhNy00MDk0LTg2ZWEtNjAwODViY2E5OTk5IiwiUm9sZSI6Ik5vYmxlIEFkbWluIiwiQ29tcGFueUlkIjoiNWY4ZDU2MTQtMmM3ZS00ZWMwLTg2OGMtZDI1NGU2NTE2YjRkIiwiVXNlcklkIjoiNWY4ZDU2MTQtMmM3ZS00ZWMwLTg2OGMtZDI1NGU2NTE2YjRkIiwiRW1haWwiOiJub2JsZUBnbWFpbC5jb20iLCJOb2JsZUNvbXBhbnlJZCI6IjAwMDAwMDAwLTAwMDAtMDAwMC0wMDAwLTAwMDAwMDAwMDAwMCIsIkJ1c2luZXNzSWQiOiIiLCJDbGllbnRQYXJlbnRJZCI6IiIsIkVtcGxveWVlSWQiOiIiLCJDb3VudGVySWQiOiIwMDAwMDAwMC0wMDAwLTAwMDAtMDAwMC0wMDAwMDAwMDAwMDAiLCJEYXlJZCI6IjAwMDAwMDAwLTAwMDAtMDAwMC0wMDAwLTAwMDAwMDAwMDAwMCIsIklzUHJvY2VlZCI6ZmFsc2UsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6Ik5vYmxlIEFkbWluIiwiZXhwIjoxNjU3MDk3NTE2LCJpc3MiOiJodHRwOi8veW91cmRvbWFpbi5jb20iLCJhdWQiOiJodHRwOi8veW91cmRvbWFpbi5jb20ifQ.IjtMbckhrVhVabG1D-DhjTtidoDRPXxw-qCtL0yKhLY'

                root.$https.get("/Sale/CompanyDetailForEmail?id=" + this.$route.query.companyId, { headers: { Authorization: `Bearer ${token}` }, })
                    .then(function (response) {
                        if (response.data != null) {
                            

                            root.headerFooter.company = response.data;
                            var sellerNameBuff = root.GetTLVForValue('1', root.headerFooter.company.nameEnglish)
                            var vatRegistrationNoBuff = root.GetTLVForValue('2', root.headerFooter.company.vatRegistrationNo)
                            var timeStampBuff = root.GetTLVForValue('3', root.sale.date)
                            var totalWithVat = root.GetTLVForValue('4', root.calulateNetTotalWithVAT())
                            var totalVat = root.GetTLVForValue('5', root.calulateTotalVATofInvoice())
                            var tagArray = [sellerNameBuff, vatRegistrationNoBuff, timeStampBuff, totalWithVat, totalVat]
                            var qrCodeBuff = Buffer.concat(tagArray)
                            root.qrValue = qrCodeBuff.toString('base64')
                        }
                    });
            },
            GetTLVForValue: function (tagNumber, tagValue) {
                var tagBuf = Buffer.from([tagNumber], 'utf-8')
                var tagValueLenBuf = Buffer.from([tagValue.length], 'utf-8')
                var tagValueBuf = Buffer.from(tagValue, 'utf-8')
                var bufsArray = [tagBuf, tagValueLenBuf, tagValueBuf]
                return Buffer.concat(bufsArray)
            },
            GetInvoiceDataForEmail: function (value) {
                console.log(value)
                var root = this;
                root.loading = true
                var token = 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoibm9ibGVAZ21haWwuY29tIiwic3ViIjoibm9ibGVAZ21haWwuY29tIiwianRpIjoiOGU2MTI1NzYtMDNhNy00MDk0LTg2ZWEtNjAwODViY2E5OTk5IiwiUm9sZSI6Ik5vYmxlIEFkbWluIiwiQ29tcGFueUlkIjoiNWY4ZDU2MTQtMmM3ZS00ZWMwLTg2OGMtZDI1NGU2NTE2YjRkIiwiVXNlcklkIjoiNWY4ZDU2MTQtMmM3ZS00ZWMwLTg2OGMtZDI1NGU2NTE2YjRkIiwiRW1haWwiOiJub2JsZUBnbWFpbC5jb20iLCJOb2JsZUNvbXBhbnlJZCI6IjAwMDAwMDAwLTAwMDAtMDAwMC0wMDAwLTAwMDAwMDAwMDAwMCIsIkJ1c2luZXNzSWQiOiIiLCJDbGllbnRQYXJlbnRJZCI6IiIsIkVtcGxveWVlSWQiOiIiLCJDb3VudGVySWQiOiIwMDAwMDAwMC0wMDAwLTAwMDAtMDAwMC0wMDAwMDAwMDAwMDAiLCJEYXlJZCI6IjAwMDAwMDAwLTAwMDAtMDAwMC0wMDAwLTAwMDAwMDAwMDAwMCIsIklzUHJvY2VlZCI6ZmFsc2UsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6Ik5vYmxlIEFkbWluIiwiZXhwIjoxNjU3MDk3NTE2LCJpc3MiOiJodHRwOi8veW91cmRvbWFpbi5jb20iLCJhdWQiOiJodHRwOi8veW91cmRvbWFpbi5jb20ifQ.IjtMbckhrVhVabG1D-DhjTtidoDRPXxw-qCtL0yKhLY'
                root.printDetailsPos = [];
                root.$https.get("/Sale/SaleDetailForEmail?id=" + value + '&isFifo=' + root.$route.query.fifo + '&openBatch=' + root.$route.query.openBatch + '&companyId=' + this.$route.query.companyId, {
                    headers: { Authorization: `Bearer ${token}` },
                })
                    .then(function (response) {
                        if (response.data != null) {
                            

                            root.printDetails = response.data;
                            var data = response.data;
                            root.sale.id = data.id;
                            root.sale.invoiceType = data.invoiceType;
                            root.sale.cashCustomer = data.cashCustomer;
                            root.sale.customerNameEn = data.customerNameEn;
                            root.sale.paymentTypes = data.paymentTypes;
                            root.sale.change = data.change;
                            root.sale.customerNameAr = data.customerNameAr;
                            root.sale.wareHouseName = data.wareHouseName;
                            root.sale.wareHouseNameAr = data.wareHouseNameAr;
                            root.sale.creditCustomer = data.creditCustomer;
                            root.sale.date = moment(data.date).format("LLL");

                            if (data.dueDate != null) {
                                root.sale.dueDate = moment(data.dueDate).format("LLL");

                            }
                            else {
                                root.sale.dueDate = '';
                            }
                            root.sale.registrationNo = data.registrationNo;
                            root.sale.mobile = data.mobile;
                            root.sale.code = data.code;
                            root.sale.isCredit = data.isCredit;
                            root.sale.areaId = data.areaId;
                            root.sale.employeeId = data.employeeId;
                            root.sale.customerId = data.customerId;
                            root.sale.dispatchNote = data.dispatchNote;
                            root.sale.saleItems = data.saleItems;
                            root.sale.paymentVoucher = data.paymentVoucher;


                            if (root.$route.query.multiUnit == 'true' && root.$route.query.decimal == 'true') {
                                root.sale.saleItems.forEach(function (x) {

                                    x.highQty = parseInt(parseFloat(x.quantity) / parseFloat(x.product.unitPerPack));
                                    x.quantity = parseFloat(parseFloat(x.quantity) % parseFloat(x.product.unitPerPack)).toFixed(3).slice(0, -1);
                                    x.unitPerPack = x.product.unitPerPack;
                                });
                                root.printDetails.saleItems = root.sale.saleItems
                                root.randerItem++
                            }
                            if (root.$route.query.multiUnit == 'true' && root.$route.query.decimal != 'true') {
                                root.sale.saleItems.forEach(function (x) {

                                    x.highQty = parseInt(parseInt(x.quantity) / parseInt(x.product.unitPerPack));
                                    x.quantity = parseInt(parseInt(x.quantity) % parseInt(x.product.unitPerPack));
                                    x.unitPerPack = x.product.unitPerPack;
                                });
                                root.printDetails.saleItems = root.sale.saleItems
                                root.randerItem++
                            }
                            
                            root.randerItem++
                            root.loading = false
                        }
                    }).catch(error => {
                        console.log(error)
                        root.$swal.fire(
                            {
                                icon: 'error',
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Something Went Wrong!' : 'هل هناك خطب ما!',
                                text: error.response.data,
                                showConfirmButton: false,
                                timer: 5000,
                                timerProgressBar: true,
                            });

                        root.loading = false
                    })
                    .finally(() => root.loading = false)
            },
        },
        created: function () {
            
            this.GetHeaderDetail();
            this.GetInvoiceDataForEmail(this.$route.query.id);
        },

        mounted: function () {
            var getLocale = this.$route.query.lang;
            if (getLocale == 'en-US' || getLocale == null) {
                getLocale = 'en';
            }
            this.language = getLocale;
            //var IsDayStart = localStorage.getItem('DayStart');
            //var IsExpenseDay = localStorage.getItem('IsExpenseDay');
            //var IsDayStartOn = localStorage.getItem('IsDayStart');
            //if (IsDayStart != 'true') {
            //    this.isDayAlreadyStart = true;
            //    this.language = this.$i18n.locale;
            //    this.currency = localStorage.getItem('currency');
            //    this.isMultiUnit = localStorage.getItem('IsMultiUnit');
            //    this.isRaw = localStorage.getItem('IsProduction');

            //}
            //else {
            //    if (IsExpenseDay == 'true') {
            //        this.isDayAlreadyStart = false;
            //    }
            //    else {
            //        this.CompanyID = localStorage.getItem('CompanyID');
            //        this.UserID = localStorage.getItem('UserID');
            //        this.employeeId = localStorage.getItem('EmployeeId');
            //        if (IsDayStartOn == 'true') {
            //            var root = this;
            //            this.isDayAlreadyStart = true;
            //            root.isArea = localStorage.getItem('IsArea');
            //            root.currency = localStorage.getItem('currency');
            //            root.isMultiUnit = localStorage.getItem('IsMultiUnit');
            //            root.isRaw = localStorage.getItem('IsProduction');

            //        }

            //    }

            //}

        },
    };
</script>

<style scoped>
    .bg_color {
        background-color: #e5e5e5;
    }

    .vue-radio-button {
        height: 100%;
        width: 100%;
        display: flex;
        flex-direction: row;
        justify-content: space-around;
        align-items: center;
    }

    .icon {
        object-fit: contain;
    }

    .v-radio-button-active {
        background-color: #b6d7ff73 !important;
    }

    .title {
        font-size: 11px;
        margin-left: -10px;
    }
</style>

