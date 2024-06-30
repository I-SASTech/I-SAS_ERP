<template>
    <div class="row" v-if="isValid('CanAddSaleReturn')">
        <div class="col-lg-12">

            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">
                                    {{ $t('AddSaleReturn.SaleReturn') }}
                                </h4>

                            </div>
                            <div class="col-auto align-self-center">
                                <a v-on:click="GotoPage('/StartScreen')" href="javascript:void(0);"
                                   class="btn btn-sm btn-outline-danger">
                                    {{ $t('Sale.Close') }}
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <hr class="hr-dashed hr-menu mt-0" />

            <div class="row" v-if="isDayAlreadyStart">
                <div class="col-lg-6">

                    <div class="row form-group">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline ">
                                {{ $t('AddSaleReturn.Invoice') }} :
                            </span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <sale-invoice-dropdown v-model="sale.saleInvoiceId" @update:modelValue="getSaleDetail" />
                        </div>
                    </div>

                    <div class="row form-group">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline ">
                                {{ $t('AddSaleReturn.SaleReturn') }}
                            </span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <input type="text" class="form-control" v-model="sale.registrationNo" disabled />
                        </div>
                    </div>

                    <div class="row form-group" v-if="!sale.isCredit">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline ">
                                {{ $t('AddSaleReturn.Date') }} :
                            </span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <input type="text" class="form-control" v-model="sale.date" disabled />
                        </div>
                    </div>

                    <div class="row form-group">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline ">
                                {{ $t('AddSaleReturn.Customer') }} :<span class="text-danger"> *</span>
                            </span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <input type="text" class="form-control"
                                   disabled
                                   @focus="$event.target.select()"
                                   v-model="v$.sale.cashCustomer.$model" />
                        </div>
                    </div>

                    <div class="row form-group" v-if="!sale.isCredit">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline ">
                                {{ $t('AddSaleReturn.Mobile') }} :<span class="text-danger"> *</span>
                            </span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <input type="text" class="form-control" v-model="v$.sale.mobile.$model" disabled />
                        </div>
                    </div>



                </div>

                <div class="col-lg-6">

                    <div class="row form-group" v-if="!sale.isCredit">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline ">
                                {{ $t('AddSaleReturn.Customer') }} :<span class="text-danger"> *</span>
                            </span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <customerdropdown v-model="v$.sale.customerId.$model" :disable="true" :modelValue="sale.customerId" v-bind:key="rendered" />
                        </div>
                    </div>

                    <div class="row form-group" v-if="!sale.isCredit">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline ">
                                {{ $t('AddSaleReturn.DueDate') }} :<span class="text-danger"> *</span>
                            </span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <datepicker v-model="v$.sale.dueDate.$model" disabled />
                        </div>
                    </div>

                    <div class="row form-group">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline" :class="{'':sale.isCredit, '': !sale.isCredit}">
                                {{ $t('AddSaleReturn.WareHouse') }} :<span class="text-danger"> *</span>
                            </span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <warehouse-dropdown v-model="v$.sale.wareHouseId.$model" :disable="true" />
                        </div>
                    </div>

                    <div class="row form-group" v-if="saleDefaultVat=='DefaultVatHead' || saleDefaultVat=='DefaultVatHeadItem'">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline" :class="{'':sale.isCredit, '': !sale.isCredit}">
                                {{ $t('AddPurchase.TaxMethod') }} :<span class="text-danger"> *</span>
                            </span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <multiselect v-if="this.$i18n.locale == 'en'" :options="['Inclusive', 'Exclusive']" v-bind:disabled="sale.saleItems.length>0" v-model="sale.taxMethod" :show-labels="false" v-bind:placeholder="$t('AddStockValue.SelectMethod')">
                            </multiselect>
                            <multiselect v-else :options="['شامل', 'غير شامل']" v-bind:disabled="sale.saleItems.length>0" v-model="sale.taxMethod" :show-labels="false" v-bind:placeholder="$t('AddStockValue.SelectMethod')">
                            </multiselect>
                        </div>
                    </div>

                    <div class="row form-group" v-if="saleDefaultVat=='DefaultVatHead' || saleDefaultVat=='DefaultVatHeadItem'">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline" :class="{'':sale.isCredit, '': !sale.isCredit}">
                                {{ $t('AddPurchase.TaxMethod') }} :<span class="text-danger"> *</span>
                            </span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <taxratedropdown v-model="sale.taxRateId" v-bind:modelValue="sale.taxRateId" :isDisable="sale.saleItems.length>0? true :false" v-bind:key="rendered" />
                        </div>
                    </div>

                </div>

                <!-- <sale-return-item :saleItems="sale.saleItems" @update:modelValue="SaveSaleItems" @summary="updateSummary" :key="rendered" /> -->

                <sale-item :saleItems="sale.saleItems" :formName="'SaleReturn'" :key="rendered" @update:modelValue="SaveSaleItems" @summary="updateSummary" :taxMethod="sale.taxMethod" :taxRateId="sale.taxRateId" @discountChanging="updateDiscountChanging" :adjustmentProp="sale.discount" :adjustmentSignProp="adjustmentSignProp" :isDiscountOnTransaction="sale.isDiscountOnTransaction" :transactionLevelDiscountProp="sale.transactionLevelDiscount" :isFixed="sale.isFixed" :isBeforeTax="sale.isBeforeTax" />

                <div class="col-lg-12 mt-4 mb-5">
                    <div class="card">
                        <div class="card-body">
                            <div class="row">
                                <div class="col-lg-8" style="border-right: 1px solid #eee;">

                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group ps-3" v-if="!loading">
                                        <div class="font-xs mb-1">{{ $t('AddSaleReturn.Attachment') }}</div>

                                        <button v-on:click="Attachment()" type="button"
                                                class="btn btn-light btn-square btn-outline-dashed mb-1">
                                            <i class="fas fa-cloud-upload-alt"></i> {{ $t('AddSaleReturn.Attachment') }}
                                        </button>

                                        <div>
                                            <small class="text-muted">
                                                {{ $t('AddSaleReturn.FileSize') }}
                                            </small>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-lg-12 invoice-btn-fixed-bottom">
                    <div v-if="sale.id === '00000000-0000-0000-0000-000000000000'">

                        <button class="btn btn-outline-primary  me-2"
                                v-on:click="saveSaleReturn('SaleReturn',true,false)"
                                v-if="isValid('CanAddSaleReturn')"
                                v-bind:disabled="v$.$invalid || sale.saleItems.filter(x => x.outOfStock).length > 0 || sale.saleItems.filter(x => x.totalPiece=='').length > 0 || sale.saleItems.filter(x => x.unitPrice=='').length > 0">
                            <i class="far fa-save"></i>  {{ $t('AddSaleReturn.SaveAsPost') }}
                        </button>
                        <button class="btn btn-outline-primary  me-2"
                                v-on:click="saveSaleReturn('SaleReturn',true,true)"
                                v-if="isValid('CanAddSaleReturn') && isValid('CanPrintInvoice')"
                                v-bind:disabled="v$.$invalid || sale.saleItems.filter(x => x.outOfStock).length > 0 || sale.saleItems.filter(x => x.totalPiece=='').length > 0 || sale.saleItems.filter(x => x.unitPrice=='').length > 0">
                            <i class="far fa-save"></i>  {{ $t('AddSaleReturn.SaveasPostandPrint') }}
                        </button>
                        <button class="btn btn-danger  me-2"
                                v-on:click="goToSale">
                            {{ $t('AddSaleReturn.Cancel') }}
                        </button>
                    </div>
                    <div v-else>
                        <button class="btn btn-outline-primary  me-2"
                                v-on:click="saveSaleReturn('SaleReturn',true,false)"
                                v-if="isValid('CanAddSaleReturn')"
                                v-bind:disabled="v$.$invalid || sale.saleItems.filter(x => x.outOfStock).length > 0 || sale.saleItems.filter(x => x.totalPiece=='').length > 0 || sale.saleItems.filter(x => x.unitPrice=='').length > 0">
                            <i class="far fa-save"></i> {{ $t('AddSaleReturn.UpdateAsPost') }}
                        </button>
                        <button class="btn btn-outline-primary  me-2"
                                v-on:click="saveSaleReturn('SaleReturn',true,true)"
                                v-if="isValid('CanAddSaleReturn') && isValid('CanPrintInvoice') "
                                v-bind:disabled="v$.$invalid || sale.saleItems.filter(x => x.outOfStock).length > 0 || sale.saleItems.filter(x => x.totalPiece=='').length > 0 || sale.saleItems.filter(x => x.unitPrice=='').length > 0">
                            <i class="far fa-save"></i> {{ $t('AddSaleReturn.UpdateAsPostandPrint') }}
                        </button>
                        <button class="btn btn-danger  me-2"
                                v-on:click="goToSale">
                            {{ $t('AddSaleReturn.Cancel') }}
                        </button>
                    </div>



                </div>




            </div>

            <div class="col-lg-6 col-sm-6 ml-auto mr-auto" v-else>
                <div class="card p-3 text-center " v-if="bankDetail">
                    <h4 class="">{{ $t('AddSaleReturn.FirstStartInvoice') }}</h4>
                    <router-link :to="{path: '/WholeSaleDay', query: { token_name:'DayStart_token', fromDashboard:'true' } }"><a href="javascript:void(0)" class="btn btn-outline-danger ">{{ $t('AddSaleReturn.DayStart') }}</a></router-link>
                </div>
                <div class="card p-3 text-center " v-else>
                    <h4 class="">{{ $t('AddSaleReturn.FirstStartInvoice') }}</h4>
                    <router-link :to="{path: '/dayStart', query: { token_name:'DayStart_token', fromDashboard:'true' } }"><a href="javascript:void(0)" class="btn btn-outline-danger ">{{ $t('AddSaleReturn.DayStart') }}</a></router-link>
                </div>
            </div>

            <bulk-attachment :attachmentList="sale.attachmentList" :show="show" v-if="show" @close="attachmentSave" />

            <saleReturnReport :printDetails="printDetails" :headerFooter="headerFooter" v-if="printDetails.length != 0" :isTouchScreen="saleReturn" v-bind:key="printRender"></saleReturnReport>
            <loading :name="loading" v-model:active="loading" :can-cancel="false" :is-full-page="true"></loading>
        </div>

    </div>



    <div v-else> <acessdenied></acessdenied></div>
</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'
    import Multiselect from "vue-multiselect";
    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    /* END EXTERNAL SOURCE */
    /* BEGIN EXTERNAL SOURCE */

    import moment from "moment";


    import { required, requiredIf } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core';
    //import Vue3Barcode from 'vue3-barcode'
    export default {
        mixins: [clickMixin],
        name: "AddSaleReturn",
        components: {

            Multiselect,
            Loading
        },
        setup() {
            return { v$: useVuelidate() }
        },
        data: function () {
            return {
                discountTypeOption: 'At Line Item Level',
                adjustmentSignProp: '+',

                bankDetail: false,
                saleReturnRegNo: '',
                saleReturn: 'saleReturn',
                printDetails: [],
                printDetailsPos: [],
                printRender: 0,
                printRenderPos: 0,
                openBatch: 1,
                isFifo: false,
                headerFooter: {
                    footerEn: '',
                    footerAr: '',
                    company: ''
                },
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
                sale: {
                    id: "00000000-0000-0000-0000-000000000000",
                    date: "",
                    documentType: "SaleReturn",
                    taxMethod: "",
                    taxRateId: "",
                    saleInvoiceId: "",
                    registrationNo: "",
                    customerId: "00000000-0000-0000-0000-000000000000",
                    dueDate: "",
                    wareHouseId: "",
                    saleItems: [],
                    attachmentList: [],
                    isCredit: false,
                    cashCustomer: "",
                    mobile: "",
                    code: "",
                    invoiceType: "",
                    isSaleReturnPost: false,
                    isSerial: false,
                    dayStart: false,
                    discount: 0,
                    isDiscountOnTransaction: false,
                    isFixed: false,
                    isBeforeTax: true,
                    transactionLevelDiscount: 0,
                    grossAmount: 0,
                    vatAmount: 0,
                    discountAmount: 0,
                    totalAmount: 0,
                    branchId: '',
                },
                loading: false,
                summary: Object,
                autoNumber: '',
                language: 'Nothing',
                CompanyID: '',
                UserID: '',
                employeeId: '',
                isDayAlreadyStart: false,
                show: false,
                saleDefaultVat: '',
            };
        },
        validations() {
            return {
                sale: {
                    date: { required },
                    registrationNo: { required },
                    customerId: {
                        required: requiredIf(function () {
                            return this.isCredit;
                        }),
                    },
                    dueDate: {},
                    wareHouseId: {},
                    saleItems: {
                        required
                    },
                    cashCustomer: {
                        required: requiredIf(function () {
                            return !this.isCredit && (this.customerId === '00000000-0000-0000-0000-000000000000' || this.customerId === null);
                        }),
                    },
                    mobile: {},
                    code: {},
                }
            }
        },

        methods: {
            GotoPage: function (link) {
                this.$router.push({ path: link });
            },
            Attachment: function () {
                this.show = true;
            },

            attachmentSave: function (attachment) {

                this.sale.attachmentList = attachment;
                this.show = false;
            },

            PrintInvoicePos: function (value) {
                this.GetHeaderDetail();
                var root = this;
                var token = '';
                root.saleReturnRegNo = value.invoiceNo;
                this.isDisabled = true;
                this.PrinterInterval();
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get("/Sale/SaleDetail?id=" + value.id, {
                    headers: { Authorization: `Bearer ${token}` },
                })
                    .then(function (response) {
                        if (response.data != null) {
                            root.printDetailsPos = response.data;
                            root.printRenderPos++;
                        }
                    });
            },

            PrinterInterval: function () {
                var root = this;

                this.printInterval = setInterval(() => {
                    root.isDisabled = false;
                }, 3000);
            },
            GetInvoicePrintSetting: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                var branchId = localStorage.getItem('BranchId');

                root.$https.get('/Sale/printSettingDetail?branchId=' + branchId, { headers: { "Authorization": `Bearer ${token}` } })
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
                            root.headerFooter.customerNameEn = response.data.customerNameEn;
                            root.headerFooter.customerNameAr = response.data.customerNameAr;

                        }
                    },
                        function (error) {
                            this.loading = false;
                            console.log(error);
                        });
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
            PrintInvoice: function (value) {

                this.GetHeaderDetail();
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.printDetails = [];


                root.$https.get("/Sale/SaleDetail?id=" + value, {
                    headers: { Authorization: `Bearer ${token}` },
                })
                    .then(function (response) {
                        if (response.data != null) {
                            root.saleReturnDesign = true;

                            root.printDetails = response.data;
                            if (localStorage.getItem('IsMultiUnit') == 'true' && localStorage.getItem('decimalQuantity') == 'true') {
                                root.printDetails.saleItems.forEach(function (x) {
                                    x.highQty = parseInt(parseFloat(x.quantity) / parseFloat(x.product.unitPerPack));
                                    x.newQuantity = parseFloat(parseFloat(x.quantity) % parseFloat(x.product.unitPerPack)).toFixed(3).slice(0, -1);
                                    x.unitPerPack = x.product.unitPerPack;
                                });
                            }
                            if (localStorage.getItem('IsMultiUnit') == 'true' && localStorage.getItem('decimalQuantity') != 'true') {
                                root.printDetails.saleItems.forEach(function (x) {
                                    x.highQty = parseInt(parseInt(x.quantity) / parseInt(x.product.unitPerPack));
                                    x.newQuantity = parseInt(parseInt(x.quantity) % parseInt(x.product.unitPerPack));
                                    x.unitPerPack = x.product.unitPerPack;
                                });
                            }
                            root.printRender++;
                        }
                    });
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
                            title: (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Error!' : 'خطأ',
                            text: (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'You cannot Change language during update, otherwise your current page data will be lose!' : 'لا يمكنك تغيير اللغة أثناء التحديث ، وإلا ستفقد بيانات صفحتك الحالية!',
                            type: 'error',
                            confirmButtonClass: "btn btn-danger",
                            icon: 'error',
                            timer: 4000,
                            timerProgressBar: true,
                        });
                    }
                }


            },
            isCredit: function (credit) {
                this.sale.isCredit = credit.id;
                if (!this.sale.isCredit) {
                    this.sale.customerId = null;
                }

            },
            updateDiscountChanging: function (isFixed, isBeforeTax) {
                this.sale.isFixed = isFixed
                this.sale.isBeforeTax = isBeforeTax
            },
            getSaleDetail: function (id) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                root.$https.get('/Sale/SaleDetail?Id=' + id + '&isFifo=' + this.isFifo + '&openBatch=' + this.openBatch, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data != null) {

                            root.sale.isCredit = response.data.isCredit;
                            root.sale.taxRateId = response.data.taxRateId;
                            root.sale.taxMethod = response.data.taxMethod;
                            root.sale.invoiceType = response.data.invoiceType;
                            root.sale.cashCustomer = response.data.cashCustomer;
                            root.sale.cashCustomerId = response.data.cashCustomerId;
                            root.sale.date = moment().format("LLL");
                            root.sale.dueDate = moment().format("LLL");
                            root.sale.mobile = response.data.mobile;
                            root.sale.code = response.data.code;
                            root.sale.customerId = response.data.customerId;
                            root.sale.cashCustomerId = response.data.cashCustomerIdForeign;
                            root.sale.saleItems = response.data.saleItems;
                            root.sale.saleOrderId = response.data.saleOrderId;
                            root.sale.discount = response.data.discount;
                            root.sale.salePayments = response.data.salePayments;

                            if (localStorage.getItem('IsMultiUnit') == 'true' && localStorage.getItem('decimalQuantity') == 'true') {
                                root.sale.saleItems.forEach(function (x) {
                                    x.highQty = parseInt(parseFloat(x.remainingQuantity) / parseFloat(x.product.unitPerPack));
                                    x.quantity = parseFloat(parseFloat(x.remainingQuantity) % parseFloat(x.product.unitPerPack)).toFixed(3).slice(0, -1);
                                    x.unitPerPack = x.product.unitPerPack;
                                });
                            }
                            else if (localStorage.getItem('IsMultiUnit') == 'true' && localStorage.getItem('decimalQuantity') != 'true') {
                                root.sale.saleItems.forEach(function (x) {
                                    x.highQty = parseInt(parseInt(x.remainingQuantity) / parseInt(x.product.unitPerPack));
                                    x.quantity = parseInt(parseInt(x.remainingQuantity) % parseInt(x.product.unitPerPack));
                                    x.unitPerPack = x.product.unitPerPack;
                                });
                            }
                            else {
                                root.sale.saleItems.forEach(function (x) {
                                    x.highQty = 0;
                                    x.quantity = parseInt(x.remainingQuantity);
                                });
                            }
                            root.sale.isDiscountOnTransaction = response.data.isDiscountOnTransaction;
                            root.sale.isFixed = response.data.isFixed;
                            root.sale.isBeforeTax = response.data.isBeforeTax;
                            root.sale.transactionLevelDiscount = response.data.transactionLevelDiscount;

                            root.discountTypeOption = response.data.isDiscountOnTransaction ? 'At Transaction Level' : 'At Line Item Level'
                            root.sale.taxRateId = response.data.taxRateId;
                            root.adjustmentSignProp = response.data.discount >= 0 ? '+' : '-'
                            root.rendered++;

                        }
                    },
                        function (error) {
                            console.log(error);
                        });
            },

            updateSummary: function (summary) {
                this.sale.grossAmount = summary.total;
                this.sale.vatAmount = summary.vat;
                this.sale.discountAmount = summary.discount;
                this.sale.totalAmount = summary.withVat;

                this.summary = summary;
            },
            AutoIncrementCode: function () {
                var root = this;
                var token = "";
                if (token == '') {
                    token = localStorage.getItem("token");
                }

                root.$https.get("/Sale/SaleAutoGenerateNo?userId=" + localStorage.getItem('UserID') + '&terminalId=' + localStorage.getItem('TerminalId') + '&invoicePrefix=' + localStorage.getItem('InvoicePrefix') + '&branchId=' + localStorage.getItem('BranchId'), { headers: { Authorization: `Bearer ${token}` }, }).then(function (response) {
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
            saveSaleReturn: function (invoiceType, status, print) {
                this.loading = true;
                var root = this;
                root.sale.invoiceType = invoiceType;
                root.sale.isSaleReturnPost = status;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                if (this.sale.discount == '' || this.sale.discount == undefined || this.sale.discount == null) {
                    this.sale.discount = 0;
                }
                if (!this.sale.isCredit) {
                    this.sale.dueDate = moment().format("DD MMM YYYY");
                }
                this.sale.saleItems.forEach(function (x) {
                    x.highUnitPrice ? x.quantity = (x.highQty * x.unitPerPack) + x.quantity : x.quantity = x.totalPiece;
                });

                this.sale.branchId = localStorage.getItem('BranchId');
                this.sale.isSerial = localStorage.getItem('IsSerial') == 'true' ? true : false;
                this.sale.isFifo = this.isFifo;
                this.$https
                    .post('/Sale/SaveSaleReturn', root.sale, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(response => {
                        if (response.data.message.id != '00000000-0000-0000-0000-000000000000' && response.data.action == "Add") {
                            root.info = response.data.bpi

                            if (print) {
                                root.PrintInvoice(response.data.message.id);

                            }
                            else {
                                root.$swal({
                                    title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved!' : '!تم الحفظ',
                                    text: response.data.message.isAddUpdate,
                                    type: 'success',
                                    confirmButtonClass: "btn btn-success",
                                    buttonStyling: false,
                                    icon: 'success',
                                    timer: 1500,
                                    timerProgressBar: true,

                                }).then(function (ok) {
                                    if (ok != null) {
                                        if (root.isValid('CanViewSaleReturn')) {
                                            root.$router.push({
                                                path: '/SaleReturn',
                                                query: {
                                                    data: 'AddSaleReturns'
                                                }
                                            });
                                        }
                                        else {
                                            root.$router.go();
                                        }

                                    }
                                });
                            }

                        }
                        else if (response.data.message.id != '00000000-0000-0000-0000-000000000000' && response.data.action == "Update") {
                            root.info = response.data.bpi

                            root.$swal({
                                title: (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Saved!' : '!تم الحفظ',
                                text: response.data.message.isAddUpdate,
                                type: 'success',
                                confirmButtonClass: "btn btn-success",
                                buttonStyling: false,
                                icon: 'success',
                                timer: 1500,
                                timerProgressBar: true,

                            }).then(function (ok) {
                                if (ok != null) {
                                    if (root.sale.invoiceType == 'Draft') {
                                        if (root.isValid('CanViewSaleReturn')) {
                                            root.$router.push({
                                                path: '/SaleReturn',
                                                query: {
                                                    data: 'AddSaleReturns'
                                                }
                                            });
                                        }
                                        else {
                                            root.$router.go();
                                        }

                                    }
                                    else {
                                        if (root.isValid('CanViewSaleReturn')) {
                                            root.$router.push({
                                                path: '/SaleReturn',
                                                query: {
                                                    data: 'AddSaleReturns'
                                                }
                                            });
                                        }
                                        else {
                                            root.$router.go();
                                        }

                                    }
                                }
                            });
                        }
                        else {
                            if (localStorage.getItem('IsMultiUnit') == 'true' && localStorage.getItem('decimalQuantity') == 'true') {
                                root.sale.saleItems.forEach(function (x) {
                                    x.highQty = parseInt(parseFloat(x.quantity) / parseFloat(x.unitPerPack));
                                    x.quantity = parseFloat(parseFloat(x.quantity) % parseFloat(x.unitPerPack)).toFixed(3).slice(0, -1);
                                });
                            }
                            if (localStorage.getItem('IsMultiUnit') == 'true' && localStorage.getItem('decimalQuantity') != 'true') {
                                root.sale.saleItems.forEach(function (x) {
                                    x.highQty = parseInt(parseInt(x.quantity) / parseInt(x.unitPerPack));
                                    x.quantity = parseInt(parseInt(x.quantity) % parseInt(x.unitPerPack));
                                });
                            }
                            root.$swal({
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                                text: response.data.message.isAddUpdate,
                                type: 'error',
                                confirmButtonClass: "btn btn-danger",
                                icon: 'error',
                                timer: 1500,
                                timerProgressBar: true,
                            });
                        }

                    })
                    .catch(error => {
                        if (localStorage.getItem('IsMultiUnit') == 'true' && localStorage.getItem('decimalQuantity') == 'true') {
                            root.sale.saleItems.forEach(function (x) {
                                x.highQty = parseInt(parseFloat(x.quantity) / parseFloat(x.unitPerPack));
                                x.quantity = parseFloat(parseFloat(x.quantity) % parseFloat(x.unitPerPack)).toFixed(3).slice(0, -1);
                            });
                        }
                        if (localStorage.getItem('IsMultiUnit') == 'true' && localStorage.getItem('decimalQuantity') != 'true') {
                            root.sale.saleItems.forEach(function (x) {
                                x.highQty = parseInt(parseInt(x.quantity) / parseInt(x.unitPerPack));
                                x.quantity = parseInt(parseInt(x.quantity) % parseInt(x.unitPerPack));
                            });
                        }
                        console.log(error)
                        root.$swal.fire(
                            {
                                icon: 'error',
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Something Went Wrong!' : 'هل هناك خطب ما!',
                                text: error,
                            });

                        root.loading = false
                    })
                    .finally(() => root.loading = false)

            },

            goToSale: function () {

                if (this.isValid('CanViewSaleReturn')) {
                    this.$router.push({
                        path: '/SaleReturn',
                        query: {
                            data: 'AddSaleReturns'
                        }
                    });
                }
                else {
                    this.$router.go();
                }

            },

        },
        created: function () {
            var root = this;

            if (root.$route.query.Add == 'false') {
                root.$route.query.data = this.$store.isGetEdit;
            }
            this.$emit('update:modelValue', this.$route.name);
            this.saleDefaultVat = localStorage.getItem('SaleDefaultVat');

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

                this.sale.isDiscountOnTransaction = data.isDiscountOnTransaction;
                this.sale.isFixed = data.isFixed;
                this.sale.isBeforeTax = data.isBeforeTax;
                this.sale.transactionLevelDiscount = data.transactionLevelDiscount;

                this.discountTypeOption = data.isDiscountOnTransaction ? 'At Transaction Level' : 'At Line Item Level'
                this.sale.taxRateId = data.taxRateId;
                this.adjustmentSignProp = data.discount >= 0 ? '+' : '-'
            }
            else {
                this.sale.wareHouseId = localStorage.getItem('WareHouseId');
                //this.sale.taxRateId = localStorage.getItem('TaxRateId');
                //this.sale.taxMethod = localStorage.getItem('taxMethod');
            }
        },

        mounted: function () {
            this.language = this.$i18n.locale;
            var IsDayStart = localStorage.getItem('DayStart');
            var IsExpenseDay = localStorage.getItem('IsExpenseDay');
            var IsDayStartOn = localStorage.getItem('IsDayStart');
            this.bankDetail = localStorage.getItem('BankDetail') == 'true' ? true : false;

            this.isFifo = localStorage.getItem('fIFO') == 'true' ? true : false;
            var batch = localStorage.getItem('openBatch')
            if (batch != undefined && batch != null && batch != "") {
                this.openBatch = batch
            }
            else {
                this.openBatch = 1
            }
            if (IsDayStart != 'true') {
                this.isDayAlreadyStart = true;
                if (this.$route.query.data == undefined) {
                    this.AutoIncrementCode();
                    this.sale.date = moment().format("LLL");
                    this.sale.dueDate = moment().format("LLL");
                }
                this.sale.dayStart = false
            }
            else {
                this.sale.dayStart = true
                if (IsExpenseDay == 'true') {
                    this.isDayAlreadyStart = false;
                }
                else {
                    this.CompanyID = localStorage.getItem('CompanyID');
                    this.UserID = localStorage.getItem('UserID');
                    this.employeeId = localStorage.getItem('EmployeeId');
                    if (IsDayStartOn == 'true') {
                        var root = this;
                        this.isDayAlreadyStart = true;
                        if (root.$route.query.data == undefined) {
                            root.AutoIncrementCode();
                            root.sale.date = moment().format("LLL");
                            root.sale.dueDate = moment().format("LLL");
                        }
                    }
                    else {
                        this.isDayAlreadyStart = false;
                    }
                }

            }

        },
    };
</script>

