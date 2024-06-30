<template>
    <div class="row" v-if="isValid('CanViewDetailSaleReturn')">
        <div class="col-lg-12">
            <div class="row">
                <div class="col">
                    <h5 class="page_title">{{ $t('ViewSaleReturn.SaleReturn') }}</h5>
                    <ol class="breadcrumb">
                        <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('SaleReturn.Home') }}</a></li>
                        <li class="breadcrumb-item active">
                            {{ $t('ViewSaleReturn.SaleReturn') }}
                        </li>
                    </ol>
                </div>

                <div class="col-auto align-self-center">

                    <a v-on:click="goToSale" href="javascript:void(0);"
                       class="btn btn-sm btn-outline-danger mx-1">
                        <i class="fas fa-arrow-circle-left fa-lg"></i>

                    </a>
                    <a v-on:click="GotoPage('/StartScreen')" href="javascript:void(0);"
                       class="btn btn-sm btn-outline-danger">
                        {{ $t('SaleOrder.Close') }}
                    </a>
                </div>
            </div>
        </div>
        <div class="col-xs-12 col-sm-9 col-md-9 col-lg-9">
            <div class="card" >
                <div class="card-header ">
                    <div class="row" >
                      

                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6" >
                            <h5>{{ $t('ViewSaleReturn.From') }}</h5>
                            <div class="card" style="border: 1px #dddddd solid;">
                                <div class="card-body">
                                    <label>{{($i18n.locale == 'en' ||isLeftToRight())?headerFooter.company.nameEnglish:headerFooter.company.companyNameArabic}}</label>
                                    <br />
                                    <label>{{($i18n.locale == 'en' ||isLeftToRight())?headerFooter.company.addressEnglish:headerFooter.company.addressArabic}}</label>
                                </div>
                            </div>
                        </div>
                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6" >
                            <h5>{{ $t('ViewSaleReturn.InvoiceTo') }}</h5>
                            <div class="card" style="border: 1px #dddddd solid;">
                                <div class="card-body">
                                    <label v-if="!sale.isCredit">{{($i18n.locale == 'en' ||isLeftToRight())?sale.cashCustomer:(sale.cashCustomer==''?sale.cashCustomer:sale.cashCustomer)}}</label> <label>{{($i18n.locale == 'en' ||isLeftToRight())?sale.customerNameEn:(sale.customerNameAr==''?sale.customerNameEn:sale.customerNameAr)}}</label>
                                    <br />
                                    <label>{{sale.customerAddress}}</label>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="card-body  mt-0 pt-0">
                    <div class="tab-content" id="nav-tabContent">

                        <!-- <view-sale-return-item :saleItems="sale.saleItems" @update:modelValue="SaveSaleItems" @summary="updateSummary" :key="rendered" /> -->
                        <invoice-view-item :saleItems="sale.saleItems" ref="childComponentRef" :key="rendered"   @update:modelValue="SaveSaleItems" @summary="updateSummary" :taxMethod="sale.taxMethod" :taxRateId="sale.taxRateId"  :adjustmentProp="sale.discount" :adjustmentSignProp="adjustmentSignProp" :isDiscountOnTransaction="sale.isDiscountOnTransaction" :transactionLevelDiscountProp="sale.transactionLevelDiscount" :isFixed="sale.isFixed" :isBeforeTax="sale.isBeforeTax" />
                            
                        <div class="row">
                            <div class="col-md-6 ">
                                <button class="btn btn-sm btn-primary mx-1" v-on:click="Attachment()">
                                    {{ $t('InvoiceView.Attachment') }}
                                </button>
                                <button class="btn btn-sm btn-outline-danger" v-on:click="goToSale">
                                    {{ $t('InvoiceView.Cancel') }}
                                </button>
                            </div>

                        </div>
                       

                    </div>
                </div>
            </div>

        </div>
        <div class="col-xs-12 col-sm-3 col-md-3 col-lg-3" >
            <div class="card">
                <div class="card-body">
                    <div class="row">
                        <div class=" col-lg-12">
                            <h5 class="view_page_title">{{ $t('ViewSaleReturn.BasicInfo') }}</h5>
                        </div>
                        <div class=" col-lg-12">
                            <vue-qrcode v-bind:value="qrValue" style="width:120px;" />
                        </div>
                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                            <label class="invoice_lbl">{{ $t('ViewSaleReturn.SaleReturn') }}#</label>
                            <hr style="margin-top: 0.3rem; margin-bottom: 0.1rem;" />
                            <label>{{sale.registrationNo}}-<span v-if='sale.isCredit'> {{ $t('ViewSaleReturn.Credit') }}</span><span v-else> {{ $t('ViewSaleReturn.Cash') }}</span></label>
                            <hr style="margin-top: 0.1rem;" />
                        </div>
                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                            <label class="invoice_lbl">{{ $t('ViewSaleReturn.InvoiceNo') }}#</label>
                            <hr style="margin-top: 0.3rem; margin-bottom: 0.1rem;" />
                            <label>{{ sale.invoiceNo }}</label>
                            <hr style="margin-top: 0.1rem;" />
                        </div>
                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 mt-2">
                            <label class="invoice_lbl">{{ $t('ViewSaleReturn.WareHouse') }}</label>
                            <hr style="margin-bottom: 0.1rem; margin-top: 0.3rem;" />
                            <label> {{($i18n.locale == 'en' ||isLeftToRight())?sale.wareHouseName:(sale.wareHouseNameAr==''?sale.wareHouseName:sale.wareHouseNameAr)}}</label>
                            <hr style="margin-top: 0.1rem;" />
                        </div>
                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 mt-2">
                            <label class="invoice_lbl">{{ $t('ViewSaleReturn.Mobile') }}</label>
                            <hr style="margin-bottom: 0.1rem; margin-top: 0.3rem;" />
                            <label> {{ sale.mobile }}</label>
                            <hr style="margin-top: 0.1rem;" />
                        </div>
                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 mt-2">
                            <label class="invoice_lbl">{{ $t('ViewSaleReturn.Date') }}</label>
                            <hr style="margin-bottom: 0.1rem; margin-top: 0.3rem;" />
                            <label>{{sale.date}}</label>
                            <hr style="margin-top: 0.1rem;" />
                        </div>
                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 mt-2">
                            <label class="invoice_lbl">{{ $t('ViewSaleReturn.DueDate') }}</label>
                            <hr style="margin-bottom: 0.1rem; margin-top: 0.3rem;" />
                            <label>{{sale.dueDate}}</label>
                            <hr style="margin-top: 0.1rem;" />
                        </div>
                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 mt-2">
                            <label class="invoice_lbl">{{ $t('ViewSaleReturn.SendCopyTo') }}</label>
                            <hr style="margin-bottom: 0.1rem; margin-top: 0.3rem;" />
                            <label>{{ $t('ViewSaleReturn.Email') }}</label>
                            <hr style="margin-top: 0.1rem;" />
                        </div>
                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 mt-2">
                            <button class="btn btn-primary btn-block">{{ $t('ViewSaleReturn.SendInvoice') }}</button>
                        </div>
                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-6 mt-2">
                            <button class="btn btn-light btn-block text-left">PDF <i class="fas fa-file-pdf float-right" style="color:#EB5757;"></i></button>
                        </div>
                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-6 mt-2">
                            <button class="btn btn-light btn-block text-left">Sheets <i class="fas fa-file-excel float-right" style="color:#198754;"></i></button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <bulk-attachment :documentid="sale.id" :show="show" v-if="show" @close="attachmentSave" />

    </div>
    <div v-else> <acessdenied></acessdenied></div>
</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'
    

    /* END EXTERNAL SOURCE */
    /* BEGIN EXTERNAL SOURCE */

    import moment from "moment";
    
    import { required, requiredIf } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core';
    //import VueBarcode from 'vue-barcode';
    export default {
        components: {
            
        },
        mixins: [clickMixin],
        name: "AddSaleReturn",
        setup() {
            return { v$: useVuelidate() }
        },
        data: function () {
            return {
                discountTypeOption: 'At Line Item Level',
                adjustmentSignProp: '+',

                toggleValue: '',
                buttons: [
                    {
                        id: false,
                        icon: "money-bill-alt-regular.svg",
                        title: "Cash",
                    },
                    {
                        id: true,
                        icon: "credit-card-solid.svg",
                        title: "Credit",
                    },
                ],
                rendered: 0,
                warehouseRendered: 0,
                qrValue: '',

                sale: {
                    id: "00000000-0000-0000-0000-000000000000",
                    date: "",
                    saleInvoiceId: "",
                    registrationNo: "",
                    customerId: "00000000-0000-0000-0000-000000000000",
                    dueDate: "",
                    wareHouseId: "",
                    saleItems: [],
                    isCredit: false,
                    cashCustomer: "",
                    mobile: "",
                    code: "",
                    invoiceType: "",
                    customerName: "",
                    wareHouseName: "",
                    invoiceNo: "",
                    isSaleReturnPost: false,
                    discount: 0,
                    unRegisteredRate: 0,
                    isButtonDisabled: false,
                    isDiscountOnTransaction: false,
                    isFixed: false,
                    isBeforeTax: true,
                    transactionLevelDiscount: 0
                },
                loading: false,
                show: false,
                summary: Object,
                autoNumber: '',
                language: 'Nothing',
                headerFooter: {
                    footerEn: '',
                    footerAr: '',
                    company: ''
                },
            };
        },
        validations() {
            return {
                sale: {
                    date: { required },
                    registrationNo: { required },
                    customerId: {
                        required: requiredIf((x) => x.isCredit)
                    },
                    dueDate: {},
                    wareHouseId: {},
                    saleItems: {
                        required
                    },
                    cashCustomer: {
                        required: requiredIf((x) => !x.isCredit && (x.customerId == '00000000-0000-0000-0000-000000000000' || x.customerId == null))

                    },
                    mobile: {},
                    code: {},
                }
                }
        },
        computed: {
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
            GotoPage: function (link) {
                this.$router.push({ path: link });
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

            isCredit: function (credit) {
                this.sale.isCredit = credit.id;
                if (!this.sale.isCredit) {
                    this.sale.customerId = null;
                }

            },

            getSaleDetail: function (id) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                root.$https.get('/Sale/SaleDetail?Id=' + id, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data != null) {

                            root.sale.isCredit = response.data.isCredit;
                            root.sale.invoiceType = response.data.invoiceType;
                            root.sale.cashCustomer = response.data.cashCustomer;
                            root.sale.date = moment().format("LLL");
                            root.sale.dueDate = moment().format("LLL");
                            root.sale.mobile = response.data.mobile;
                            root.sale.code = response.data.code;
                            root.sale.customerId = response.data.customerId;
                            root.sale.saleItems = response.data.saleItems;

                            root.rendered++;
                        }
                    },
                        function (error) {
                            console.log(error);
                        });
            },

            updateSummary: function (summary) {
                this.summary = summary;
            },
            AutoIncrementCode: function () {
                var root = this;
                var token = "";
                if (token == '') {
                    token = localStorage.getItem("token");
                }
                root.$https
                    .get("/Sale/SaleAutoGenerateNo?invoiceType=" + "SaleReturn", {
                        headers: { Authorization: `Bearer ${token}` },
                    })
                    .then(function (response) {
                        if (response.data != null) {
                            root.sale.registrationNo = response.data.saleReturn;
                        }
                    });
            },
            SaveSaleItems: function (saleItems, discount, adjustmentSignProp, transactionLevelDiscount) {
                this.sale.saleItems = saleItems;
                this.sale.discount = (discount == '' || discount == null) ? 0 : (adjustmentSignProp == '+' ? parseFloat(discount) : (-1) * parseFloat(discount))

                this.sale.transactionLevelDiscount = (transactionLevelDiscount == '' || transactionLevelDiscount == null) ? 0 : parseFloat(transactionLevelDiscount)
            },
           
            goToSale: function () {
                this.$router.push({
                    path: '/SaleReturn',
                    query: {
                        data: 'AddSaleReturns'
                    }
                });

            },

            setDefaultWareHouse: function (id) {
                this.sale.wareHouseId = id;
                this.warehouseRendered++;
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
            }
        },
        created: function () {
            var root =  this;

            if(root.$route.query.Add == 'false')
            {
                root.$route.query.data = this.$store.isGetEdit;
            }
            this.$emit('update:modelValue', this.$route.name);

            if (this.$route.query.data != undefined) {
                var data = this.$route.query.data;
                this.sale.id = data.id;
                this.sale.wareHouseId = data.wareHouseId;
                this.sale.invoiceType = data.invoiceType;
                this.sale.cashCustomer = data.cashCustomer;
                this.sale.date = moment(data.date).format("LLL");
                this.sale.dueDate = moment(data.dueDate).format("LLL");
                this.sale.registrationNo = data.registrationNo;
                this.sale.mobile = data.mobile;
                this.sale.code = data.code;
                this.sale.customerId = data.customerId;
                this.sale.isCredit = data.isCredit;
                this.sale.saleItems = data.saleItems;
                this.sale.paymentVoucher = data.paymentVoucher;
                this.sale.taxMethod = data.taxMethod;
                this.sale.discount = data.discount;
                this.sale.taxRateId = data.taxRateId;
                this.sale.unRegisteredVatId = data.unRegisteredVatId;
                this.sale.unRegisteredRate = data.unRegisteredRate;

                
                this.sale.isDiscountOnTransaction = data.isDiscountOnTransaction;
                this.sale.isFixed = data.isFixed;
                this.sale.isBeforeTax = data.isBeforeTax;
                this.sale.transactionLevelDiscount = data.transactionLevelDiscount;

                this.discountTypeOption = data.isDiscountOnTransaction ? 'At Transaction Level' : 'At Line Item Level'
                this.sale.taxRateId = data.taxRateId;
                this.adjustmentSignProp = data.discount >= 0 ? '+' : '-'


                if (localStorage.getItem('IsMultiUnit') == 'true' && localStorage.getItem('decimalQuantity') == 'true') {
                    this.sale.saleItems.forEach(function (x) {
                        x.highQty = parseInt(parseFloat(x.remainingQuantity) / parseFloat(x.product.unitPerPack));
                        x.quantity = parseFloat(parseFloat(x.remainingQuantity) % parseFloat(x.product.unitPerPack)).toFixed(3).slice(0, -1);
                        x.unitPerPack = x.product.unitPerPack;
                    });
                }
                else if (localStorage.getItem('IsMultiUnit') == 'true' && localStorage.getItem('decimalQuantity') != 'true') {
                    this.sale.saleItems.forEach(function (x) {
                        x.highQty = parseInt(parseInt(x.remainingQuantity) / parseInt(x.product.unitPerPack));
                        x.quantity = parseInt(parseInt(x.remainingQuantity) % parseInt(x.product.unitPerPack));
                        x.unitPerPack = x.product.unitPerPack;
                    });
                }
                else {
                    this.sale.saleItems.forEach(function (x) {
                        x.highQty = 0;
                        x.quantity = parseInt(x.remainingQuantity);
                    });
                }
            }
            else {
                this.sale.wareHouseId = localStorage.getItem('WareHouseId');
            }
        },

        mounted: function () {

            this.language = this.$i18n.locale;
            this.GetHeaderDetail();
            if (this.$route.query.data == undefined) {
                this.AutoIncrementCode();
                this.sale.date = moment().format("LLL");
                this.sale.dueDate = moment().format("LLL");
            }
        },
    };
</script>
