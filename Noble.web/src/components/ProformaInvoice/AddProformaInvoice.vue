<template>
    <div class="row" v-if="isValid('CanAddProforma') || isValid('CanEditProforma')">
        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 v-if="sale.id == '00000000-0000-0000-0000-000000000000'" class="page-title">{{ $t('AddProformaInvoices.AddProformaInvoices') }}</h4>
                                <h4 v-else class="page-title">{{ $t('AddProformaInvoices.UpdateProformaInvoices') }}</h4>
                            </div>
                            <div class="col-auto align-self-center">
                                <!-- <a v-on:click="GotoPage('/StartScreen')" href="javascript:void(0);"
                                   class="btn btn-sm btn-outline-danger">
                                    {{ $t('Sale.Close') }}
                                </a> -->
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <hr class="hr-dashed hr-menu mt-0" />

            <div class="row">
                <div class="col-lg-6">
                    <div class="row form-group" v-if="isValid('CashInvoicesForCustomer' ) || isValid('CreditInvoices')">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline "> {{ $t('AddSale.Customer') }} : <span class="text-danger">*</span></span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <customerdropdown v-model="v$.sale.customerId.$model" :disable="sale.saleOrderId==null || sale.saleOrderId==''?false:true" :paymentTerm="paymentMethod" ref="CustomerDropdown" @update:modelValue="emptyCashCustomer" :modelValue="sale.customerId" :key="customerRender" />
                            <a href="javascript:void(0);" data-bs-toggle="offcanvas" data-bs-target="#offcanvasRight" aria-controls="offcanvasRight" class="text-primary">{{ $t('AddProformaInvoices.ViewCustomerDetails') }}</a>
                        </div>
                    </div>



                    <div class="row form-group">
                        <label class="col-form-label col-lg-4">
                            <span id="ember695" class="tooltip-container text-dashed-underline "> {{ $t('AddSale.InvoiceNo') }}# <span class="text-danger">*</span></span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <input v-if="!sale.isCredit" v-model="sale.registrationNo" disabled class="form-control" type="text">
                            <input v-if="sale.isCredit" v-model="autoNumberCredit" disabled class="form-control" type="text">
                        </div>
                    </div>

                    <div class="row form-group">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline ">{{ $t('AddProformaInvoices.InvoiceDate') }}<span class="text-danger">*</span></span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <datepicker v-model="sale.date" />
                        </div>
                    </div>


                    <div class="row form-group" v-if="isValid('CanSelectWarehouse')">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline "> {{ $t('AddSale.WareHouse') }} :<span class="text-danger"> *</span></span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <warehouse-dropdown v-model="v$.sale.wareHouseId.$model" />
                        </div>
                    </div>
                    <div class="row form-group" v-if="isValid('CanSelectTransporter')">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline "> {{ $t('AddSale.Transporter/Cargo') }}</span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <logisticDropdown v-model="sale.logisticId" :modelValue="sale.logisticId" v-bind:key="logisticRender" />
                        </div>
                    </div>
                </div>

                <div class="col-lg-6">




                    <div class="row form-group" v-if="isArea=='true' && isValid('CanSelectArea')">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline "> {{ $t('AddSale.Area') }}</span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <areadropdown v-model="sale.areaId" :modelValue="sale.areaId" />
                        </div>
                    </div>



                    <div class="row form-group" v-if="isForMedical=='true'">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline "> {{ $t('AddSale.DoctorName') }}</span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <input v-model="sale.doctorName" class="form-control" />
                        </div>
                    </div>

                    <div class="row form-group" v-if="isForMedical=='true'">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline "> {{ $t('AddSale.HospitalName') }}</span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <input v-model="sale.hospitalName" class="form-control" />
                        </div>
                    </div>

                    <div class="row form-group">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline ">{{ $t('AddProformaInvoices.PONumber') }}</span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <input v-model="sale.poNumber" class="form-control" />
                        </div>
                    </div>

                    <div class="row form-group">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline ">{{ $t('AddProformaInvoices.PODate') }}</span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <datepicker v-model="sale.poDate" />
                        </div>
                    </div>

                    <div class="row form-group" v-if="saleDefaultVat=='DefaultVatHead' || saleDefaultVat=='DefaultVatHeadItem'">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline "> {{ $t('AddPurchase.TaxMethod') }} :<span class="text-danger"> *</span></span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <multiselect v-if="($i18n.locale == 'en' ||isLeftToRight())" :options="['Inclusive', 'Exclusive']" v-bind:disabled="sale.saleItems.length>0" @click="sale.isFixed = false" v-model="sale.taxMethod" :show-labels="false" v-bind:placeholder="$t('AddStockValue.SelectMethod')" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                            </multiselect>
                            <multiselect v-else :options="['شامل', 'غير شامل']" v-bind:disabled="sale.saleItems.length>0" v-model="sale.taxMethod" @select="sale.isFixed = false" :show-labels="false" v-bind:placeholder="$t('AddStockValue.SelectMethod')" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                            </multiselect>
                        </div>
                    </div>

                    <div class="row form-group" v-if="saleDefaultVat=='DefaultVatHead' || saleDefaultVat=='DefaultVatHeadItem'">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline "> {{ $t('AddPurchase.VAT%') }} :<span class="text-danger"> *</span></span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <taxratedropdown v-model="sale.taxRateId" v-bind:modelValue="sale.taxRateId" :isDisable="sale.saleItems.length>0? true :false" :key="customerRender" />
                        </div>
                    </div>

                    <div class="row form-group">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline ">{{ $t('AddProformaInvoices.DiscountType') }}</span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <multiselect :options="['At Transaction Level', 'At Line Item Level']" v-bind:disabled="sale.saleItems.length>0" v-model="discountTypeOption" @select="sale.isDiscountOnTransaction = (discountTypeOption === 'At Transaction Level' ? false : true)" :show-labels="false" v-bind:placeholder="$t('AddStockValue.SelectMethod')" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                            </multiselect>
                        </div>
                    </div>


                </div>

                <sale-item :saleItems="sale.saleItems" ref="childComponentRef" :wareHouseId="sale.wareHouseId" :wholesale="priceType" :saleOrderId="sale.saleOrderId" @update:modelValue="SaveSaleItems" @summary="updateSummary" :taxMethod="sale.taxMethod" :taxRateId="sale.taxRateId" @discountChanging="updateDiscountChanging" :adjustmentProp="sale.discount" :adjustmentSignProp="adjustmentSignProp" :isDiscountOnTransaction="sale.isDiscountOnTransaction" :transactionLevelDiscountProp="sale.transactionLevelDiscount" :isFixed="sale.isFixed" :isBeforeTax="sale.isBeforeTax" />

                <div class="col-lg-12 invoice-btn-fixed-bottom">



                    <div class="button-items">
                        <button class="btn btn-primary mr-2"
                                v-if="isValid('CanAddProforma') || isValid('CanEditProforma')"
                                v-on:click="saveSale('ProformaInvoice',false)"
                                v-bind:disabled="v$.$invalid || !isColorVariantsValid || sale.saleItems.filter(x => x.totalPiece=='').length > 0 || sale.saleItems.filter(x => x.unitPrice=='').length > 0">
                            <span v-if="sale.id == '00000000-0000-0000-0000-000000000000'">
                                {{ $t('AddRegion.btnSave') }}
                            </span>

                            <span v-if="sale.id != '00000000-0000-0000-0000-000000000000'">
                                {{ $t('AddRegion.btnUpdate') }}
                            </span>
                        </button>

                        <button class="btn btn-danger mr-2" v-on:click="goToSale">
                            {{ $t('AddSale.Cancel') }}
                        </button>
                    </div>

                </div>



                <div class="col-lg-12 mt-4 mb-5">
                    <div class="card">
                        <div class="card-body">
                            <div class="row">
                                <div class="col-lg-8" style="border-right: 1px solid #eee;">
                                    <div class="form-group pe-3">
                                        <label>{{ $t('AddSaleOrder.TermandCondition') }}:</label>
                                        <textarea class="form-control " rows="3" v-model="sale.note" />
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group ps-3">
                                        <div class="font-xs mb-1">{{ $t('AddProformaInvoices.Attachment') }}</div>

                                        <button v-on:click="Attachment()" type="button" class="btn btn-light btn-square btn-outline-dashed mb-1"><i class="fas fa-cloud-upload-alt"></i> {{ $t('AddSale.Attachment') }} </button>

                                        <div>
                                            <small class="text-muted">
                                                {{ $t('AddProformaInvoices.FileSize') }}
                                            </small>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <bulk-attachment :attachmentList="sale.attachmentList" :show="show" v-if="show" @close="attachmentSave" />

        <usc-report-print :printDetails="printDetails" :isTouchScreen="sale" :headerFooter="headerFooter" v-if="printDetails.length != 0 && printSize=='A4' && printTemplate=='Template4'" v-bind:key="printRender" />

        <cash-voucher-report :printDetail="printDetailsCashVoucher" v-bind:isTouchScreen="addSale" :headerFooter="headerFooter" v-if="printDetailsCashVoucher.length != 0" v-bind:key="printRender" />
        <SalesThermalSaudiReports3 :printDetail="printDetailsPos" v-bind:isTouchScreen="addSale" :headerFooter="headerFooter" v-if="printDetailsPos.length != 0 && printSize=='Thermal' && printTemplate=='RetailSaTemplate1'" v-bind:key="printRender" />

        <saleInvoice :color="true" :printDetails="printDetails" v-bind:isTouchScreen="addSale" :headerFooter="headerFooter" v-if="printDetails.length != 0 && printTemplate=='Default'" v-bind:key="printRender" />
        <ObaagestSaleInvoice :color="true" :printDetails="printDetails" v-bind:isTouchScreen="addSale" :headerFooter="headerFooter" v-if="printDetails.length != 0 && printTemplate=='Template8'" v-bind:key="printRender" />
        <saleInvoice-template-one :printDetails="printDetails" :isTouchScreen="addSale" :headerFooter="headerFooter" v-if="printDetails.length != 0 && printTemplate=='Template1'" v-bind:key="printRender" />

        <loading v-model:active="loading"
                 :can-cancel="true"
                 :is-full-page="true"></loading>
    </div>
    <div v-else> <acessdenied></acessdenied></div>

</template>
<script>
    /* END EXTERNAL SOURCE */
    /* BEGIN EXTERNAL SOURCE */

    import moment from "moment";
    

    import { required, requiredIf } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core';
    import clickMixin from '@/Mixins/clickMixin'
    import Multiselect from "vue-multiselect";
    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    //import VueBarcode from 'vue-barcode';
    export default {
        name: "AddSale",
        mixins: [clickMixin],
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

                poNumber: '',
                UnRegisteredVat: '',
                printSize: '',
                printTemplate: '',
                onPayScreen: false,
                show: false,
                isFifo: false,
                openBatch: 0,
                saleOrderPerm: false,
                bankDetail: false,
                autoPaymentVoucher: false,
                soPaymentTotal: 0,
                customerRender: 0,
                logisticRender: 0,
                saleOrder: false,
                isRaw: '',
                isMultiUnit: '',
                isForMedical: '',
                isArea: '',
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
                togglePayCash: 'Cash',
                language: 'Nothing',
                paymentMethod: '',

                toggleBank: 'Bank',
                toggleAdvance: 'Advance',
                addSale: 'addSale',
                toggleOtherCurrency: 'OtherCurrency',

                rendered: 0,
                sale: {
                    id: "00000000-0000-0000-0000-000000000000",
                    dispatchNote: [],
                    barCode: "",
                    saleOrderId: "",
                    quotationId: "",
                    date: "",
                    poDate: "",
                    poNumber: "",
                    doctorName: "",
                    hospitalName: "",
                    counterId: '',
                    logisticId: '',
                    unRegisteredVatId: '',
                    dayStart: false,
                    isWarranty: true,
                    registrationNo: "",
                    customerId: null,
                    dueDate: "",
                    wareHouseId: "",
                    saleItems: [],
                    attachmentList: [],
                    isCredit: false,
                    cashCustomer: "",
                    mobile: "",
                    email: "",
                    cashCustomerId: "",
                    code: "",
                    invoiceType: "",
                    employeeId: "",
                    areaId: "",
                    customerAddressWalkIn: "",
                    change: 0,
                    unRegisteredVAtAmount: 0,
                    randerVat: 0,
                    soInventoryReserve: '',
                    isSerial: false,
                    autoPaymentVoucher: '',
                    terminalId: '',
                    invoicePrefix: '',
                    salePayment: {
                        dueAmount: 0,
                        received: 0,
                        balance: 0,
                        change: 0,
                        paymentTypes: [],
                        paymentType: 'Cash',
                        paymentOptionId: null,
                        paymentName: 'Cash',
                    },
                    otherCurrency: {
                        currencyId: null,
                        rate: 0,
                        amount: 0
                    },

                    creditPays: [],
                    bankCashAccountId: '',
                    userName: '',
                    isCashInvoicesForCustomer: false,
                    partiallyQuotation: false,
                    partiallySaleOrder: false,
                    colorVariants: false,
                    taxMethod: '',
                    taxRateId: '',
                    discount: 0,
                    unRegisteredRate: 0,
                    isButtonDisabled: false,
                    isDiscountOnTransaction: false,
                    isFixed: false,
                    isBeforeTax: true,
                    transactionLevelDiscount: 0
                },
                printId: '00000000-0000-0000-0000-000000000000',
                printDetailsPos: [],
                printDetailsCashVoucher: [],
                printDetails: [],
                printRender: 0,
                loading: false,
                summary: Object,
                search: '',
                UserID: '',
                employeeId: '',
                customerAdvanceAccountId: '',
                isDayAlreadyStart: false,
                IsProduction: false,
                headerFooter: {
                    returnDays: '',
                    footerEn: '',
                    footerAr: '',
                    printSizeA4: '',
                    company: ''
                },
                accountRender: 0,
                overWrite: false,
                BusinessLogo: '',
                BusinessNameArabic: '',
                BusinessNameEnglish: '',
                BusinessTypeArabic: '',
                BusinessTypeEnglish: '',
                CompanyNameArabic: '',
                CompanyNameEnglish: '',
                priceType: null,
                priceoptions: [{ id: 1, name: 'Retail Price' }, { id: 2, name: 'Wholesale Price' }],
                wholesalePriceActivation: false,
                saleDefaultVat: '',
                creditPayment: 0,
                totalAfterUneRegTax: 0,
            };
        },
        validations() {
            return {
                sale: {
                    date: { required },
                    registrationNo: { required },
                    customerId: {
                        // required: requiredIf((x) => {

                        //     if (x.isCredit) {
                        //         if (x.customerId == '00000000-0000-0000-0000-000000000000' || x.customerId == null)
                        //             return true;
                        //     }
                        //     else if (x.isCashInvoicesForCustomer)
                        //         if (x.customerId == '00000000-0000-0000-0000-000000000000' || x.customerId == null)
                        //             return true;
                        //     return false;
                        // })
                        required: requiredIf(function () {
    if (this.isCredit) {
        return this.customerId === '00000000-0000-0000-0000-000000000000' || this.customerId === null;
    } else if (this.isCashInvoicesForCustomer) {
        return this.customerId === '00000000-0000-0000-0000-000000000000' || this.customerId === null;
    }
    return false;
})
                    },
                    cashCustomer: {
                        // required: requiredIf((x) => {

                        //     if (!x.isCredit)
                        //         if (x.customerId == '00000000-0000-0000-0000-000000000000' || x.customerId == null)
                        //             return true;
                        //     return false;
                        // })
                        required: requiredIf(function () {
        if (!this.isCredit && (this.customerId === '00000000-0000-0000-0000-000000000000' || this.customerId === null)) {
            return true;
        }
        return false;
    })

                    },


                    dueDate: {},
                    wareHouseId: {},
                    saleItems: {
                        required
                    },
                    mobile: {},
                    code: {},
                }
                }
        },
        computed: {
            isColorVariantsValid: function () {

                if (this.sale.saleItems.length > 0 && this.sale.colorVariants) {
                    if (this.sale.saleItems.filter(x => x.colorId == '' || x.colorId == null || x.colorId == undefined).length > 0) {
                        return false;
                    }
                    return true;
                }

                return true;
            },

            isPaymentValid: function () {
                var paymentTypesAmount;
                if (this.sale.salePayment.paymentTypes.length > 0) {
                    paymentTypesAmount = this.sale.salePayment.paymentTypes.reduce((total, payment) =>
                        total + payment.amount, 0);
                } else {
                    paymentTypesAmount = 0;
                }

                if (this.sale.salePayment.paymentType == "Bank" && this.sale.customerId != null && this.sale.customerId != '') {

                    if (this.sale.bankCashAccountId == "00000000-0000-0000-0000-000000000000" ||
                        this.sale.bankCashAccountId == null) {
                        return false;
                    }
                }

                if (this.sale.salePayment.paymentType == "Bank" && (this.sale.customerId == null || this.sale.customerId == '')) {

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

        },
        methods: {
            Attachment: function () {
                this.show = true;
            },

            updateDiscountChanging: function (isFixed, isBeforeTax) {
                this.sale.isFixed = isFixed
                this.sale.isBeforeTax = isBeforeTax
            },

            getCreditAmount: function (data) {
                this.sale.creditPays = data;
                this.creditPayment = 0;
                var root = this
                if (this.sale.creditPays !== null && this.sale.creditPays.length > 0) {
                    this.sale.creditPays.forEach(function (item) {

                        root.creditPayment = root.creditPayment + parseFloat(item.amount);

                    })
                }
            },
            GetUnRegTax: function (totalPayment) {

                var rate;
                if (this.sale.id == "00000000-0000-0000-0000-000000000000") {
                    rate = this.$refs.saleInvoiceDropdown.GetAmountOfSelected();

                }
                else {
                    rate = this.sale.unRegisteredRate;

                }

                if (totalPayment == null || totalPayment == undefined) {
                    totalPayment = 0;
                    this.totalAfterUneRegTax = 0;
                }
                if (this.sale.discount == null || this.sale.discount == undefined || this.sale.discount == '')
                    this.sale.discount = 0

                if (this.sale.discount != 0) {
                    totalPayment = totalPayment - this.sale.discount;

                }
                if (this.UnRegisteredVat.rate == null || this.UnRegisteredVat.rate == undefined || this.UnRegisteredVat.rate == '')
                    this.sale.unRegTax = 0

                if (rate == null || rate == undefined || rate == '') {
                    rate = 0

                }
                this.sale.unRegisteredVAtAmount = ((rate / 100) * totalPayment);
                this.totalAfterUneRegTax = totalPayment + this.sale.unRegisteredVAtAmount;

            },


            attachmentSave: function (attachment) {

                this.sale.attachmentList = attachment;
                this.show = false;
            },

            getAddress: function () {
                this.sale.customerAddressWalkIn = this.$refs.CustomerDropdown.GetCustomerAddress().address;
                this.sale.mobile = this.$refs.CustomerDropdown.GetCustomerAddress().mobile;
            },
            cashCustomerSearch: function (value) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                if (value != '') {
                    this.$https.get('/Sale/SearchCashCustomer?search=' + value, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                        if (response.data != null) {
                            root.sale.cashCustomer = response.data.name;
                            root.sale.mobile = response.data.mobile;
                            root.sale.email = response.data.email;
                            root.sale.cashCustomerId = response.data.customerId;
                            root.sale.customerAddressWalkIn = response.data.address;
                        }
                    })
                }
                else {
                    root.sale.cashCustomer = 'Walk-In';
                    root.sale.mobile = '';
                    root.sale.cashCustomerId = '';
                    root.sale.email = '';
                }

            },
            isDayStart: function () {

                var root = this;

                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.loading = true;
                root.$https.get('/Product/IsDayStart?userId=' + this.UserID + '&employeeId=' + this.employeeId, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data.isDayStart == true) {

                            root.isDayAlreadyStart = true;
                            localStorage.setItem('token', response.data.token);

                            root.getBank();
                            if ((root.$i18n.locale == 'en' || root.isLeftToRight())) {
                                root.sale.cashCustomer = "Walk-In";
                            }
                            else {
                                root.sale.cashCustomer = "ادخل";
                            }
                            root.getCurrency();
                            root.isArea = localStorage.getItem('IsArea');
                            root.currency = localStorage.getItem('currency');
                            root.isMultiUnit = localStorage.getItem('IsMultiUnit');
                            root.isRaw = localStorage.getItem('IsProduction');
                            if (root.$route.query.data == undefined) {

                                root.sale.date = moment().format("LLL");
                                root.sale.dueDate = moment().format("LLL");
                            }
                            root.AutoIncrementCode();
                            if (root.$route.query.data != undefined) {
                                root.warehouse = root.$route.query.data;
                            }

                            if (root.$route.query.mobiledata != undefined) {
                                root.sale.date = root.$route.query.mobiledata.orderDate;
                            }
                        } else {
                            root.isDayAlreadyStart = false;
                        }
                        root.loading = false;

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
                            title: this.$t('AddSale.Error'),
                            text: this.$t('AddSale.ChangeLanguageError'),
                            confirmButtonClass: "btn btn-danger",
                            icon: 'error',
                            timer: 4000,
                            timerProgressBar: true,
                        });
                    }
                }
            },

            GetSaleOrderDetail: function (id) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                if (id != undefined) {

                    root.$https.get('/Purchase/SaleOrderDetail?Id=' + id + '&isFifo=' + this.isFifo + '&openBatch=' + this.openBatch, { headers: { "Authorization": `Bearer ${token}` } })
                        .then(function (response) {
                            if (response.data != null) {
                                root.paymentMethod = response.data.paymentMethod;
                                root.sale.isCredit = response.data.paymentMethod == "Credit" ? true : false;
                                root.sale.customerId = response.data.customerId;
                                root.sale.logisticId = response.data.logisticId;
                                root.sale.mobile = response.data.mobile;
                                root.sale.customerAddressWalkIn = response.data.customerAddress;
                                root.sale.taxMethod = response.data.taxMethod;
                                root.sale.taxRateId = response.data.taxRateId;

                                root.customerAdvanceAccountId = response.data.advanceAccountId;
                                root.$refs.childComponentRef.EmtySaleProductList();
                                response.data.saleOrderItems.forEach(function (so) {

                                    if (so.quantityOut > 0) {
                                        if (localStorage.getItem('IsMultiUnit') == 'true') {
                                            so.highQty = parseInt(parseInt(so.quantityOut) / parseInt(so.product.unitPerPack));
                                            so.quantityOut = parseInt(parseInt(so.quantityOut) % parseInt(so.product.unitPerPack));
                                        }
                                        root.$refs.childComponentRef.addProduct(so.productId, so.product, so.quantityOut, so.unitPrice, true, so.quantityOut, so);
                                    }
                                });
                                root.GetSaleOrderUsedPaymentDetail(response.data.id);

                                root.soPaymentTotal = response.data.paymentVoucher.reduce((total, item) => total + item.amount, 0);
                                root.customerRender++;
                                root.logisticRender++;
                            }
                        },
                            function (error) {
                                root.loading = false;
                                console.log(error);
                            });
                }
            },
            GetSaleOrderUsedPaymentDetail: function (id) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                root.$https.get('/Purchase/SaleInvoicePaymentList?Id=' + id, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data != null) {

                            var remaining = root.soPaymentTotal - response.data;
                            if (remaining > 0) {
                                root.soPaymentTotal = remaining;
                            }
                            else {
                                root.soPaymentTotal = 0;
                            }
                            console.log(root.soPaymentTotal);
                        }
                    },
                        function (error) {
                            root.loading = false;
                            console.log(error);
                        });

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
                                root.loading = false;
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

                this.$https.get('/Product/PaymentOptionsList?isActive=true', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {
                        root.bankList = response.data.paymentOptions;
                    }
                })
            },
            selectBank: function (id, name) {
                this.otherCurrencyId = null;
                this.bankId = id;
                this.isActive = true;
                this.sale.salePayment.paymentOptionId = id;
                this.sale.salePayment.paymentName = name;
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
                        root.currencyList.splice(0, 1);
                    }
                })
            },
            selectCurrency: function (id, name) {
                this.bankId = null;
                this.otherCurrencyId = id;
                this.isActive = true;
                this.sale.otherCurrency.currencyId = id;
                this.sale.salePayment.paymentName = name;
            },
            isCredit: function (credit) {

                console.log(credit);
                this.sale.isCredit = credit;
                if (!this.sale.isCredit) {
                    this.sale.customerId = null;
                    this.sale.customerAddressWalkIn = null;
                    this.sale.cashCustomer = "Walk-In";
                    this.paymentMethod = 'Cash';
                }
                else {
                    this.paymentMethod = 'Credit';
                }
            },
            isPayment: function (credit) {

                if (credit == 'Advance') {
                    this.sale.bankCashAccountId = this.customerAdvanceAccountId;
                    this.accountRender++;
                }
                this.sale.salePayment.paymentType = credit;
                this.accountRender++;
            },
            emptyCashCustomer: function (customerId, advanceAccountId) {

                this.sale.customerId = customerId;
                this.customerAdvanceAccountId = advanceAccountId;

                if (this.sale.customerId != '00000000-0000-0000-0000-000000000000' && this.sale.customerId != null) {
                    if (this.sale.isCredit) {
                        this.paymentMethod = 'Credit';
                    }
                    this.sale.cashCustomer = "";
                    this.sale.mobile = "";
                    this.sale.cashCustomerId = "";
                    this.sale.email = "";
                    this.search = "";
                }
                else {
                    this.paymentMethod = 'Cash';
                    this.sale.cashCustomer = "Walk-In";
                }
                if (this.isRaw == 'true') {
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
                                return x.id == root.modelValue;
                            });
                        });
                }

            },

            setCustomer: function (value) {
                console.log(value);
            },
            OnEditInvoice: function () {


                this.updateSummary(this.summary);
                this.sale.salePayment.dueAmount = (this.sale.salePayment.dueAmount) <= 0 ? '0' : (this.sale.salePayment.dueAmount);
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
                        this.sale.salePayment.change = total - this.sale.salePayment.dueAmount;
                    }
                }

                this.onPayScreen = true;
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
            OtherCurrencyCalculate: function () {
                this.sale.salePayment.received = parseFloat(this.sale.otherCurrency.amount == '' ? 0 : this.sale.otherCurrency.amount).toFixed(3).slice(0, -1) * parseFloat(this.sale.otherCurrency.rate == '' ? 0 : this.sale.otherCurrency.rate).toFixed(3).slice(0, -1);
            },
            addPayment: function (amount, paymentType, name) {
                var root = this;

                this.sale.salePayment.received = 0;
                if (paymentType == 'Cash') {
                    var payment = this.sale.salePayment.paymentTypes.find((x) => x.paymentType == paymentType)
                    if (payment != undefined) {
                        payment.amount += parseFloat(amount);
                        payment.name = name;
                    }
                    else {
                        this.sale.salePayment.paymentTypes.push({ paymentType: paymentType, amount: parseFloat(amount), name: name, rate: 0, otherAmount: 0, bankCashAccountId: root.sale.bankCashAccountId });
                    }
                }
                else if (paymentType == 'Bank') {

                    this.sale.salePayment.paymentTypes.push({ paymentType: paymentType, amount: parseFloat(amount), name: name, rate: 0, otherAmount: 0, bankCashAccountId: root.sale.bankCashAccountId });
                }
                else if (paymentType == 'CashVoucher') {
                    this.sale.salePayment.paymentTypes.push({ paymentType: paymentType, amount: parseFloat(amount), name: name, rate: 0, otherAmount: 0, bankCashAccountId: root.sale.bankCashAccountId });
                }
                else {
                    this.sale.salePayment.paymentTypes.push({ paymentType: paymentType, amount: parseFloat(amount), name: name, rate: root.sale.otherCurrency.rate, otherAmount: root.sale.otherCurrency.amount, bankCashAccountId: root.sale.bankCashAccountId });
                }
                //var payment = this.sale.salePayment.paymentTypes.find((x) => x.paymentType == paymentType)
                //if (payment != undefined) {
                //    payment.amount += parseFloat(amount);
                //} else {
                //    this.sale.salePayment.paymentTypes.push({ paymentType: paymentType, amount: parseFloat(amount) });
                //}

                this.calculatBalance(0);
                this.sale.otherCurrency.amount = 0;
                this.sale.otherCurrency.rate = 0;
            },

            removeFromPaymentList: function (paymentType) {
                this.sale.salePayment.paymentTypes.splice(paymentType, 1);
                //this.sale.salePayment.paymentTypes = this.sale.salePayment.paymentTypes.filter((x) =>
                //    x.paymentType != paymentType);

                this.calculatBalance(0);
            },

            updateSummary: function (summary) {
                this.summary = summary;

                this.sale.salePayment.dueAmount = parseFloat(this.summary.withVat)
                this.GetUnRegTax(this.sale.salePayment.dueAmount);

            },
            AutoIncrementCode: function () {

                var root = this;
                var token = "";
                if (token == '') {
                    token = localStorage.getItem("token");
                }

                root.$https.get("/Sale/SaleAutoGenerateNo?userId=" + localStorage.getItem('UserID') + '&terminalId=' + localStorage.getItem('TerminalId') + '&invoicePrefix=' + localStorage.getItem('InvoicePrefix'), { headers: { Authorization: `Bearer ${token}` }, })
                    .then(function (response) {
                        if (response.data != null) {
                            if (root.sale.id == '00000000-0000-0000-0000-000000000000') {
                                root.sale.registrationNo = response.data.proformaInvoice;
                            }
                        }
                    })
                    .catch(error => {
                        console.log(error)
                        root.$swal.fire(
                            {
                                icon: 'error',
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Cannot Generate Auto Inoice Number!' : 'استوردلا يمكن إنشاء رقم فاتورة تلقائي!',
                                text: error.response.data,
                                showConfirmButton: false,
                                timer: 5000,
                                timerProgressBar: true,
                            });
                    });
            },
            SaveSaleItems: function (saleItems, discount, adjustmentSignProp, transactionLevelDiscount) {
                this.sale.saleItems = saleItems;
                this.sale.discount = (discount == '' || discount == null) ? 0 : (adjustmentSignProp == '+' ? parseFloat(discount) : (-1) * parseFloat(discount))

                this.sale.transactionLevelDiscount = (transactionLevelDiscount == '' || transactionLevelDiscount == null) ? 0 : parseFloat(transactionLevelDiscount)
            },
            PrintInvoice: function (value) {

                var id = value;
                var root = this;
                this.isTouchScreen = true;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                root.printDetails = [];
                root.printDetailsCashVoucher = [];
                root.$https.get("/Sale/SaleDetail?id=" + id, { headers: { Authorization: `Bearer ${token}` }, })
                    .then(function (response) {
                        if (response.data != null) {

                            root.printDetailsPos = response.data;

                            if (localStorage.getItem('IsMultiUnit') == 'true') {

                                root.printDetails.saleItems.forEach(function (x) {

                                    x.highQty = parseInt(parseInt(x.quantity) / parseInt(x.product.unitPerPack));
                                    x.newQuantity = parseInt(parseInt(x.quantity) % parseInt(x.product.unitPerPack));
                                    x.unitPerPack = x.product.unitPerPack;
                                });
                            }
                            root.printRender++;
                            root.loading = false
                        }
                    })
                    .catch(error => {
                        console.log(error)
                        root.loading = false
                        root.$router.go({
                            path: '/addSale'
                        });
                    });
            },
            PrintInvoiceA4: function (value) {

                var id = value;
                var root = this;
                var token = '';

                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.printDetailsPos = [];
                root.printDetailsCashVoucher = [];
                root.$https.get("/Sale/SaleDetail?id=" + id, {
                    headers: { Authorization: `Bearer ${token}` },
                })
                    .then(function (response) {
                        if (response.data != null) {
                            if (localStorage.getItem('IsMultiUnit') == 'true') {

                                response.data.saleItems.forEach(function (x) {

                                    x.highQty = parseInt(parseInt(x.quantity) / parseInt(x.product.unitPerPack));
                                    x.newQuantity = parseInt(parseInt(x.quantity) % parseInt(x.product.unitPerPack));
                                    x.unitPerPack = x.product.unitPerPack;
                                });
                            }
                            root.printDetails = response.data;
                            root.printRender++;
                            root.loading = false
                        }
                    })
                    .catch(error => {
                        console.log(error)
                        root.loading = false
                        root.$router.go({
                            path: '/addSale'
                        });
                    });
            },
            GetHeaderDetail: function () {
                var root = this;

                root.$https.get("/Company/GetCompanyDetail?id=" + localStorage.getItem('CompanyID'), { headers: { Authorization: `Bearer ${localStorage.getItem('token')}` }, })
                    .then(function (response) {
                        if (response.data != null) {
                            root.headerFooter.company = response.data;
                            if (root.overWrite) {

                                root.headerFooter.company.nameArabic = root.BusinessNameArabic;
                                root.headerFooter.company.nameEnglish = root.BusinessNameEnglish;
                                root.headerFooter.company.categoryArabic = root.BusinessTypeArabic;
                                root.headerFooter.company.categoryEnglish = root.BusinessTypeEnglish;

                                root.headerFooter.company.companyNameArabic = root.CompanyNameArabic;
                                root.headerFooter.company.companyNameEnglish = root.CompanyNameEnglish;


                                root.headerFooter.company.logoPath = root.BusinessLogo;
                            }
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
                            root.headerFooter.exchangeDays = response.data.exchangeDays;
                            root.headerFooter.printOptions = response.data.printOptions;
                            root.headerFooter.invoicePrint = response.data.invoicePrint;
                            root.headerFooter.welcomeLineEn = response.data.welcomeLineEn;
                            root.headerFooter.welcomeLineAr = response.data.welcomeLineAr;
                            root.headerFooter.closingLineEn = response.data.closingLineEn;
                            root.headerFooter.closingLineAr = response.data.closingLineAr;
                            root.headerFooter.contactNo = response.data.contactNo;
                            root.headerFooter.managementNo = response.data.managementNo;

                        }
                    },
                        function (error) {
                            root.loading = false;
                            console.log(error);
                        });
            },
            getBase64Image: function (path) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.headerFooter.company.logoPath = 'data:image/png;base64,' + 'iVBORw0KGgoAAAANSUhEUgAAATYAAACjCAMAAAA3vsLfAAAAhFBMVEX///8AcuEAZ98Ab+AAa+AAbeAAad9Tk+cAbuC+1fUFeOLS4/no8/0AZd8AaODv9v1to+vk7/z1+v7d6vqBru3F2vemxfKHsu5Zl+iYvfDU5PnM3/g9iea20PWNte5Rk+gwhOVIjuepx/NjnOl2p+wlfuS30fUAX96cvvAde+M1huWoxfIDNGHbAAAHKElEQVR4nO2d65aiOhBGJQlpVAKId6VVtJ3ROe//fgfvqSQocS3EjrV/NsEJ38qlqlKVabUQBEEQBEEQBEEQBEEQBEEQBEEQBEEQBEEQBEEQBEGQ30E6nG6ybcfrbPNkN2+6N68kDWVimzdHOYlYEFDP8yj1GWGT79q6+W7kQoZWfi/8IfwgmEzA/UVaY1/fiCX49k7V1wbC90wwMq2zt2/D1zOyDSkzinaAb/v19vgteEa2hSgV7TBVxQcscU/IlpB7qhUI9yeqvWxJ9EC1QrdR7f1uGGvZpndn6EW3Yf09bxRb2WZVVCsWOMcNEVvZtqqxZsafvKDvDWIp245XUq2Ypm77WpayaWONRYIxwTXjl2av6H1j2Mm2VgYb88bzwo8Nhz9MFU50X9L/hrCTbQVHmxhcn8QbxS7xk3o73ixWsvXgNgrdgYViBbM6u900VrLBORrt4NMf6KgSlzcFK9k2gdRWX/SV0eayi2Ul215uTLQ1/xsMxmBTV5/fACvZ5KWNGtoC2eiqlg6/BzayxbJs/o/eIAc/Vj1U/PuwkQ1spGynNxgA443X1OV3wEa2viwbNwQjFwxl0wGjja/1BlA2lw23p9c2k33xAz2smrr8DljtpLL/ZLIvgO8VuOzMW8kGZNE3yhi4V047pVaygUnI/6iPp3BHcPlAwUo24AZo9m4Ko0rC5fNSK9nAnuAxZRZmAZDN5R3BMt4G3QAykJ9NYACEjWvsdeMosg3bJZxO8Nowpsaz60Scb5X8BtFr7JteAJTN42X8PeVwdWBzKrLdcDZrL1ZCOWVwOv6hyVYKP8n2Rz0mpYyTiDPtV9webLaytfLgcdtDc6dXNnvZwvIULQm6bfarasdWtla7SjYDc3uKPiFba/dYN8eP5FvPyNYaPdCNEtfzjZ6SrTWM7u0LzHPZqzrzjGytOFfNtNtQEy4HPq48JVvh1Hci04tUrJxf1o48KVurtV4JxcgNmMjbjXzE61lWlC3SC2Rm41UUMRYU+IWvwLKp62bHjaUgMtERyRdlB/wCY1ZpPByNk8lkkizWXcfTTiHxrKB/otfrnUuvIGlB0/1EnKQ3/16P1t/DiutUf3hsPv+cZU2nu8gOYaADEWHL0QMt5uMVuTbn+XT2ml6+F+HCIwwksBUGRbmbFI6L5uAAkJHO7tPWwDiJDCEhKnKzpxQmxNScs/FHCTfVU+TPNiwxZBaVN/cY/YD6vjNxdqf8jGinAnebe8LtU4QbM/9upJsvYfN+cD8wzrY2Jfe/lpnRK5d1+LJq7gXeB+hW4XQgWsjNH3uxgetHCa1qNXtiZtXcY86vb4MqNXvByqq5WiDjHn218D1gJGJcMeLo37MM/WpVuMUbbttvE7grUpKNZnEaz8e+L/1xewk/fmmbKPUZ8/WJyxZ3/9lfjjJ62PYa004nZ+OMRvvrjNNKvhnPB9Ppzz/dxWAuD7cBzO0Gpdrj4/yNOtIylUDngPKLExqO1UMZp5MpgQx+Dh8mzOMeyKOHY8pfSfZZ31OSkVYv6H5DdOGGoD72oGitYXRHmFiZpSKstetNApKUIy2HWY1+jGFOs+ILdOHCZyqTcQRQIHrNQO13z8xmXYkZzEHV6zngyucP1OfOIJv8t8/cwNOsM6IDV0KubZXQBqS5+twZ5EnHr0fDiTGYRv/BetKl/nMgRZXuX/klrwQkzJPrSlYi2xLWXBns2QQYw86WqoWyDtF15zPLFmxCuwo/ZwsjgQ7kgWz+IHxUTzr9DNlSINs1OGSWje3AJOX/6b8HDRT/lZ/yUkDp2XX4mGWLhmBLYAb7Al544W6sUq50vBUylsjWAxd9mJwn0D5w9y4ysPVdJ5VZNk9JhiPamf0c+F4OF12N5Fgtv5j9RtkOgwcs+fqNFrCQjbubIgjDbZe9dPP3dOczkWU4GBzQ81evGlvDuIBwOOAG7frO6UvjS5ZbJj09mnVwAHIQ45hDT97kRTgDtLRoRxYizXxVBWhhUCZl1ihjzRBPcYgYpiVQPr1MrXQHzupPJRqhog2ZnILoaVvLb6h8PfmvRLl0zeP+ZtQeDkdJYLznKVGaB1GwWi5XvnZSHxl8L4eItbOTgPGIc+WE6nL/sN7cowXaH50/lx89uiH8wO0aXXUJK0E4n1mZl6WqSbBb/PurSkEpcfqU9EjqPczqAIWO28f1y9xdv+pGaFiboGpgeU/3j4anmhDnKOH+7sQTyqaYZvfTZz6jxu/ApjwjJmB6tvigtC7ykOzr8nG8wndgHnBU5KbMyPm2JKWyaP9RhR3pmGv/AZNHyb7MRVp3iL41+GTlbtijhHS3F1I5R+BzsrwnQvuLcKk9ZRFPnL5+vZT+buKJw9myECwbtB9FftL2IGPn9nS5cP9igTukvUOJZPVY2bF96HBsDUEQBEEQBEEQBEEQBEEQBEEQBEEQBEEQBEEQBEEQBEEQmf8BckJbXFsgizoAAAAASUVORK5CYII=';
                if (path != null && path != '' && path != undefined) {
                    root.$https.get('/Contact/GetBaseImage?filePath=' + path, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                        if (response.data != null) {
                            root.filePath = response.data;
                            root.headerFooter.company.logoPath = 'data:image/png;base64,' + root.filePath;
                        }

                    });
                }

            },





            saveSale: function (invoiceType, print) {


                this.loading = true;
                var root = this;
                localStorage.setItem('active', invoiceType);
                root.sale.invoiceType = invoiceType;



                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                if (this.sale.salePayment.received != 0) {
                    this.addPayment(this.sale.salePayment.received, this.sale.salePayment.paymentType, this.sale.customerId == null ? this.sale.salePayment.paymentName : this.sale.salePayment.paymentType);
                }

                if (this.sale.discount == '' || this.sale.discount == undefined || this.sale.discount == null) {
                    this.sale.discount = 0;
                }
                this.sale.isSerial = localStorage.getItem('IsSerial') == 'true' ? true : false;
                this.sale.isFifo = this.isFifo;
                this.sale.soInventoryReserve = localStorage.getItem('SoInventoryReserve');
                this.sale.autoPaymentVoucher = localStorage.getItem('AutoPaymentVoucher');
                this.sale.userName = localStorage.getItem('LoginUserName');
                this.sale.terminalId = localStorage.getItem('TerminalId');
                this.sale.invoicePrefix = localStorage.getItem('InvoicePrefix');

                this.sale.partiallyQuotation = localStorage.getItem('partiallyQuotation') == 'true' ? true : false;
                this.sale.partiallySaleOrder = localStorage.getItem('partiallySaleOrder') == 'true' ? true : false;
                this.sale.colorVariants = localStorage.getItem('ColorVariants') == 'true' ? true : false,

                    this.sale.saleItems.forEach(function (x) {
                        x.quantity = x.totalPiece;
                    });
                this.$https
                    .post('/Sale/SaveSaleInformation', root.sale, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(response => {


                        if (response.data.message.id != '00000000-0000-0000-0000-000000000000' && response.data.action == "Add") {

                            if (print) {
                                if (root.$route.query.isDuplicate == 'true') {
                                    root.addSale = 'sale';
                                }
                                if (root.headerFooter.printSizeA4 == 'Thermal') {
                                    root.PrintInvoice(response.data.message.id);
                                }

                                else {
                                    root.PrintInvoiceA4(response.data.message.id);
                                }
                            }
                            else {
                                root.loading = false
                                root.$swal({
                                    title: root.$t('AddSale.SavedSuccessfully'),
                                    text: root.$t('AddSale.Saved'),
                                    type: 'success',
                                    confirmButtonClass: "btn btn-success",
                                    buttonStyling: false,
                                    icon: 'success',
                                    timer: 1500,
                                    timerProgressBar: true,

                                }).then(function (ok) {
                                    if (ok != null) {
                                        if (root.sale.id == '00000000-0000-0000-0000-000000000000' && !root.sale.isDuplicate) {
                                            root.$router.go({
                                                path: '/AddProformaInvoice'
                                            });
                                        }
                                        else {
                                            root.$router.push({
                                                path: '/ProformaInvoices',
                                                query: {
                                                    data: 'AddProformaInvoice'
                                                }
                                            });
                                        }

                                    }
                                });
                            }

                        }
                        else if (response.data.message.id != '00000000-0000-0000-0000-000000000000' && response.data.action == "Update") {
                            if (print) {
                                if (root.$route.query.data.isDuplicate == 'true') {
                                    root.addSale = 'duplicate';
                                }
                                else {
                                    root.addSale = 'sale';
                                }

                                if (root.headerFooter.printSizeA4 == 'Thermal') {
                                    root.PrintInvoice(response.data.message.id);
                                }

                                else {
                                    root.PrintInvoiceA4(response.data.message.id);
                                }
                            }
                            else {
                                root.loading = false
                                root.$swal({
                                    title: root.$t('AddSale.UpdateSuccessfully'),
                                    text: root.$t('AddSale.Updated'),
                                    type: 'success',
                                    confirmButtonClass: "btn btn-success",
                                    buttonStyling: false,
                                    icon: 'success',
                                    timer: 1500,
                                    timerProgressBar: true,

                                }).then(function (ok) {
                                    if (ok != null) {
                                        if (root.sale.invoiceType == 'Hold') {
                                            root.$router.push({
                                                path: '/ProformaInvoices',
                                                query: {
                                                    data: 'AddProformaInvoice'
                                                }
                                            });
                                        }
                                        else {
                                            root.$router.push({
                                                path: '/ProformaInvoices',
                                                query: {
                                                    data: 'AddProformaInvoice'
                                                }
                                            });
                                        }
                                    }
                                });
                            }

                        }
                        else {
                            root.loading = false
                            if (localStorage.getItem('IsMultiUnit') == 'true') {
                                root.sale.saleItems.forEach(function (x) {

                                    x.highQty = parseInt(parseInt(x.quantity) / parseInt(x.unitPerPack));
                                    x.quantity = parseInt(parseInt(x.quantity) % parseInt(x.unitPerPack));

                                });
                            }
                            root.$swal({
                                title: root.$t('AddSale.Error'),
                                text: root.$t('AddSale.SomethingWrong'),
                                type: 'error',
                                confirmButtonClass: "btn btn-danger",
                                icon: 'error',
                                timer: 1500,
                                timerProgressBar: true,
                            });
                        }
                    })
                    .catch(error => {
                        console.log(error)
                        if (localStorage.getItem('IsMultiUnit') == 'true') {
                            root.sale.saleItems.forEach(function (x) {

                                x.highQty = parseInt(parseInt(x.quantity) / parseInt(x.unitPerPack));
                                x.quantity = parseInt(parseInt(x.quantity) % parseInt(x.unitPerPack));

                            });
                        }
                        root.$swal.fire(
                            {
                                type: 'error',
                                icon: 'error',
                                title: error.response.data,
                                text: error.response.data,
                                confirmButtonClass: "btn btn-danger",
                                showConfirmButton: true,
                                timer: 5000,
                                timerProgressBar: true,
                            });

                        root.loading = false
                    });

            },

            goToSale: function () {
                if (this.isValid('CanViewProforma')) {
                    this.$router.push('/ProformaInvoices');

                }
                else {
                    this.$router.go();
                }

            },

            GetUserDefineFlow: function () {
                var root = this;

                if (localStorage.getItem('quotationToSaleOrder') == undefined) {
                    root.$https.get('/Company/UserDefineFlowEdit?companyId=' + localStorage.getItem('CompanyID'), { headers: { "Authorization": `Bearer ${localStorage.getItem('token')}` } })
                        .then(function (response) {
                            if (response.data != null) {
                                localStorage.setItem('quotationToSaleOrder', response.data.quotationToSaleOrder);
                                localStorage.setItem('quotationToSaleInvoice', response.data.quotationToSaleInvoice);
                                localStorage.setItem('partiallyQuotation', response.data.partiallyQuotation);
                                localStorage.setItem('partiallySaleOrder', response.data.partiallySaleOrder);

                                root.saleOrder = localStorage.getItem('quotationToSaleInvoice') == 'true' ? true : false;
                            }
                            else {
                                console.log("error: something wrong from db.");
                            }
                        },
                            function (error) {
                                console.log(error);
                            });
                }
                else {
                    this.saleOrder = localStorage.getItem('quotationToSaleInvoice') == 'true' ? true : false;

                }


            }


        },
        created: function () {

            var root =  this;

            if(root.$route.query.Add == 'false')
            {
                root.$route.query.data = this.$store.isGetEdit;
            }
            this.GetHeaderDetail();

            this.$emit('update:modelValue', this.$route.name);

            this.saleDefaultVat = localStorage.getItem('SaleDefaultVat');
            //this.$route.query.data.isDuplicate
            if (this.$route.query.data != undefined) {
                var data = this.$route.query.data;
                if (!data.isDuplicate) {
                    this.sale.id = data.id;
                    this.sale.saleOrderId = data.saleOrderId;
                    this.sale.quotationId = data.quotationId;
                    this.sale.registrationNo = data.registrationNo;
                }

                this.sale.date = moment().format("LLL");
                this.sale.isDuplicate = data.isDuplicate;
                this.sale.invoiceType = data.invoiceType;
                this.sale.cashCustomer = data.cashCustomer;
                this.sale.dueDate = moment().format("LLL");
                this.sale.mobile = data.mobile;
                this.sale.email = data.email;
                this.sale.isWarranty = data.isWarranty;
                this.sale.cashCustomerId = data.cashCustomerId;
                this.sale.customerAddressWalkIn = data.customerAddressWalkIn;
                this.sale.code = data.code;
                this.sale.areaId = data.areaId;
                this.sale.employeeId = data.employeeId;
                this.sale.isCredit = data.isCredit;
                this.paymentMethod = data.isCredit ? 'Credit' : 'Cash';
                this.sale.customerId = data.customerId;
                this.sale.wareHouseId = data.wareHouseId;
                this.sale.saleItems = data.saleItems;
                this.sale.taxMethod = data.taxMethod;
                this.sale.discount = data.discount;
                this.sale.taxRateId = data.taxRateId;
                this.sale.unRegisteredVatId = data.unRegisteredVatId;
                this.sale.unRegisteredRate = data.unRegisteredRate;
                this.sale.attachmentList = data.attachmentList;
                this.sale.paymentTypes = data.paymentTypes;

                if (localStorage.getItem('IsMultiUnit') == 'true' && localStorage.getItem('decimalQuantity') == 'true') {
                    this.sale.saleItems.forEach(function (x) {

                        x.highQty = parseInt(parseFloat(x.quantity) / parseFloat(x.product.unitPerPack));
                        x.quantity = parseFloat(parseFloat(x.quantity) % parseFloat(x.product.unitPerPack)).toFixed(3).slice(0, -1);
                        x.unitPerPack = x.product.unitPerPack;
                    });
                }
                if (localStorage.getItem('IsMultiUnit') == 'true' && localStorage.getItem('decimalQuantity') != 'true') {
                    this.sale.saleItems.forEach(function (x) {

                        x.highQty = parseInt(parseInt(x.quantity) / parseInt(x.product.unitPerPack));
                        x.quantity = parseInt(parseInt(x.quantity) % parseInt(x.product.unitPerPack));
                        x.unitPerPack = x.product.unitPerPack;
                    });
                }

                this.sale.isDiscountOnTransaction = data.isDiscountOnTransaction;
                this.sale.isFixed = data.isFixed;
                this.sale.isBeforeTax = data.isBeforeTax;
                this.sale.transactionLevelDiscount = data.transactionLevelDiscount;

                this.discountTypeOption = data.isDiscountOnTransaction ? 'At Transaction Level' : 'At Line Item Level'
                this.sale.taxRateId = data.taxRateId;
                this.adjustmentSignProp = data.discount >= 0 ? '+' : '-'

                this.sale.salePayment = {
                    dueAmount: 0,
                    received: 0,
                    balance: 0,
                    change: 0,
                    paymentTypes: [],
                    paymentType: 'Cash',
                    paymentName: 'Cash',
                    paymentOptionId: '',
                };
            }


            else {
                this.sale.wareHouseId = localStorage.getItem('WareHouseId');
                this.sale.taxRateId = localStorage.getItem('TaxRateId');
                this.sale.taxMethod = localStorage.getItem('taxMethod');
                this.sale.date = moment().format('LLL');
            }

            if (this.isValid('CashInvoicesForCustomer') && !this.isValid('CashInvoicesForWalkIn')) {
                this.sale.isCashInvoicesForCustomer = true;
            }

            this.GetUserDefineFlow();
        },

        mounted: function () {
            this.priceType = this.priceoptions.find(option => option.id === 1);
            this.overWrite = localStorage.getItem('overWrite') == 'true' ? true : false;
            this.sale.colorVariants = localStorage.getItem('ColorVariants') == 'true' ? true : false,

                this.BusinessLogo = localStorage.getItem('BusinessLogo');
            this.BusinessNameArabic = localStorage.getItem('BusinessNameArabic');
            this.BusinessNameEnglish = localStorage.getItem('BusinessNameEnglish');
            this.BusinessTypeArabic = localStorage.getItem('BusinessTypeArabic');
            this.BusinessTypeEnglish = localStorage.getItem('BusinessTypeEnglish');
            this.CompanyNameArabic = localStorage.getItem('CompanyNameArabic');
            this.CompanyNameEnglish = localStorage.getItem('CompanyNameEnglish');
            this.isFifo = localStorage.getItem('fIFO') == 'true' ? true : false;
            var batch = localStorage.getItem('openBatch')
            if (batch != undefined && batch != null && batch != "") {
                this.openBatch = batch
            }
            else {
                this.openBatch = 1
            }
            this.saleOrderPerm = localStorage.getItem('saleOrderPerm') == 'true' ? true : false;

            this.wholesalePriceActivation = localStorage.getItem('WholeSalePriceActivation') == 'true' ? true : false;
            this.autoPaymentVoucher = localStorage.getItem('AutoPaymentVoucher') == 'true' ? true : false;
            this.bankDetail = localStorage.getItem('BankDetail') == 'true' ? true : false;

            this.language = this.$i18n.locale;
            var IsDayStart = localStorage.getItem('DayStart');
            this.sale.dayStart = IsDayStart == 'false' ? false : true;
            var IsExpenseDay = localStorage.getItem('IsExpenseDay');
            var IsDayStartOn = localStorage.getItem('IsDayStart');
            this.sale.counterId = localStorage.getItem('CounterId');
            this.headerFooter.returnDays = localStorage.getItem('ReturnDays');
            this.isForMedical = localStorage.getItem('isForMedical');
            this.headerFooter.footerEn = localStorage.getItem('TermsInEng');
            this.headerFooter.footerAr = localStorage.getItem('TermsInAr');
            this.headerFooter.printSizeA4 = localStorage.getItem('PrintSizeA4');
            this.printTemplate = localStorage.getItem('PrintTemplate');
            this.printSize = localStorage.getItem('PrintSizeA4');


            if (IsDayStart != 'true') {
                this.isDayAlreadyStart = true;
                this.language = this.$i18n.locale;
                this.getBank();

                this.getCurrency();
                this.isArea = localStorage.getItem('IsArea');
                this.currency = localStorage.getItem('currency');
                this.isMultiUnit = localStorage.getItem('IsMultiUnit');
                this.isRaw = localStorage.getItem('IsProduction');
                if (this.$route.query.data == undefined) {
                    if ((this.$i18n.locale == 'en' || this.isLeftToRight())) {
                        this.sale.cashCustomer = "Walk-In";
                    }
                    else {
                        this.sale.cashCustomer = "ادخل";
                    }

                    this.sale.dueDate = moment().format("LLL");
                }


                this.AutoIncrementCode();
                if (this.$route.query.data != undefined) {
                    this.warehouse = this.$route.query.data;
                    this.randerVat++;

                }

                if (this.$route.query.mobiledata != undefined) {
                    this.sale.date = this.$route.query.mobiledata.orderDate;
                }
            }
            else {

                if (IsExpenseDay == 'true') {
                    this.isDayAlreadyStart = false;
                }
                else {
                    this.UserID = localStorage.getItem('UserID');
                    this.employeeId = localStorage.getItem('EmployeeId');
                    if (IsDayStartOn == 'true') {
                        var root = this;
                        this.isDayAlreadyStart = true;
                        root.getBank();

                        root.getCurrency();
                        root.isArea = localStorage.getItem('IsArea');
                        root.currency = localStorage.getItem('currency');
                        root.isMultiUnit = localStorage.getItem('IsMultiUnit');
                        root.isRaw = localStorage.getItem('IsProduction');
                        if (root.$route.query.data == undefined) {
                            if ((root.$i18n.locale == 'en' || root.isLeftToRight())) {
                                if (root.isValid('CashInvoices')) {
                                    root.isCredit(root.toggleCash);
                                    root.sale.cashCustomer = "Walk-In";
                                }
                                else if (root.isValid('CreditInvoices')) {
                                    root.isCredit(root.toggleCredit);
                                    //root.paymentMethod = 'Credit';
                                }
                            }
                            else {
                                if (root.isValid('CashInvoices')) {
                                    //root.paymentMethod = 'Cash';
                                    root.isCredit(root.toggleCash);
                                    root.sale.cashCustomer = "ادخل";
                                }
                                else if (root.isValid('CreditInvoices')) {
                                    root.isCredit(root.toggleCredit);
                                    //root.paymentMethod = 'Credit';
                                }
                                //root.sale.cashCustomer = "";
                            }
                            root.sale.dueDate = moment().format("LLL");
                        }
                        if (this.$route.query.data == undefined) {
                            this.sale.date = moment().format('LLL');
                        }
                        root.AutoIncrementCode();
                        if (root.$route.query.data != undefined) {
                            root.warehouse = root.$route.query.data;
                        }

                        if (root.$route.query.mobiledata != undefined) {
                            root.sale.date = root.$route.query.mobiledata.orderDate;
                        }
                    }
                    else {
                        this.isDayAlreadyStart = false;
                    }
                }

            }
            this.sale.barCode = Math.floor(Math.random() * 100000000000);
        },
    };
</script>

