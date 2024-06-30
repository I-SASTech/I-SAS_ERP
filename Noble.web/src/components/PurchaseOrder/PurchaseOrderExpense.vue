<template>
    <modal :show="show" :modalLarge="true">
        <div class="row" v-if=" isValid('CanAddOrderExpense')" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
            <div class="col-md-12  ml-auto mr-auto" v-bind:style="$i18n.locale == 'ar' ? languageChange('en') : languageChange('ar')">
                <div class="card">
                    <div class="card-body" :key="render">
                        <div class="overlay">
                            <div class="row align-items-center h-100 justify-content-sm-center">
                                <div class="loadingio-spinner-dual-ball-44dlc48bacw">
                                    <div class="ldio-m86dw9oanea">
                                        <div> </div> <div> </div> <div> </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="modal-header">
                            <h5 class="modal-title" v-if="paymentVoucher.id != '00000000-0000-0000-0000-000000000000' && formName=='AdvanceExpense'">{{ $t('PurchaseOrderExpense.AddAdvanceExpensePaymentPay')}} -  {{ paymentVoucher.voucherNumber }}</h5>
                            <h5 class="modal-title" v-if="paymentVoucher.id == '00000000-0000-0000-0000-000000000000'  && formName=='AdvanceExpense'">{{ $t('PurchaseOrderExpense.AddAdvanceExpensePaymentPay')}} -  {{ paymentVoucher.voucherNumber }}</h5>
                        </div>
                        <div>
                            <div class="card-body">
                                <div class="row">
                                    <!--<div class="col-lg-4 col-md-4 col-sm-6">
        <label>
            {{ $t('CustomerAccount') }}:
            <span class="text-danger"> *</span>
        </label>
        <div class="form-group">
            <expense-type-dropdown v-model="expenseTypeId" />
        </div>
    </div>-->
                                    <div class="col-lg-4 col-md-4 col-sm-6">
                                        <label>{{ $t('PurchaseOrderExpense.Date') }} :<span class="text-danger"> *</span></label>
                                        <div class="form-group" v-bind:class="{ 'has-danger': v$.paymentVoucher.date.$error}">
                                            <datepicker v-model="v$.paymentVoucher.date.$model"></datepicker>
                                            <span v-if="v$.paymentVoucher.date.$error" class="error">
                                                <span v-if="!v$.paymentVoucher.date.required">{{formName}}  {{ $t('PurchaseOrderExpense.DateRequired') }}</span>
                                            </span>
                                        </div>
                                    </div>
                                    <div class="col-lg-4 col-md-4 col-sm-6" v-if="formName=='PettyCash'">
                                        <label>
                                            {{ $t('PurchaseOrderExpense.PaymentMode') }}:
                                            <span class="text-danger"> *</span>
                                        </label>

                                        <div class="form-group">

                                            <multiselect v-if="($i18n.locale == 'en' ||isLeftToRight()) " disabled v-model="paymentVoucher.paymentMode" @update:modelValue="GetAccount(paymentVoucher.paymentMode)" :options="['Cash', 'Bank']" :show-labels="false" placeholder="Select Type">
                                            </multiselect>
                                            <multiselect v-else disabled v-model="paymentVoucher.paymentMode" @update:modelValue="GetAccount(paymentVoucher.paymentMode)" :options="[ 'السيولة النقدية', 'مصرف']" :show-labels="false" v-bind:placeholder="$t('SelectOption')" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                                            </multiselect>

                                        </div>
                                    </div>
                                    <div class="col-lg-4 col-md-4 col-sm-6" v-else>
                                        <label>
                                            {{ $t('PurchaseOrderExpense.PaymentMode') }}:
                                            <span class="text-danger"> *</span>
                                        </label>

                                        <div class="form-group">

                                            <multiselect v-if="($i18n.locale == 'en' ||isLeftToRight()) " v-model="paymentVoucher.paymentMode" @update:modelValue="GetAccount(paymentVoucher.paymentMode)" :options="['Cash', 'Bank','Advance']" :show-labels="false" placeholder="Select Type">
                                            </multiselect>
                                            <multiselect v-else v-model="paymentVoucher.paymentMode" @update:modelValue="GetAccount(paymentVoucher.paymentMode)" :options="[ 'السيولة النقدية', 'مصرف','يتقدم']" :show-labels="false" v-bind:placeholder="$t('SelectOption')" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                                            </multiselect>

                                        </div>
                                    </div>
                                    <div class="col-lg-4 col-md-4 col-sm-6">
                                        <label>
                                            {{ $t('PurchaseOrderExpense.PaymentType') }}:
                                            <span class="text-danger" v-if="paymentVoucher.paymentMode=='Cash' || paymentVoucher.paymentMode=='السيولة النقدية' "></span>
                                            <span class="text-danger" v-else> *</span>
                                        </label>



                                        <div class="form-group" v-if="paymentVoucher.paymentMode=='Cash' || paymentVoucher.paymentMode=='السيولة النقدية' ">

                                            <multiselect v-if="($i18n.locale == 'en' ||isLeftToRight()) " disabled v-model="paymentVoucher.paymentMethod" :options="['Cheque', 'Transfer','Deposit']" :show-labels="false" placeholder="Select Type">
                                            </multiselect>
                                            <multiselect v-else v-model="paymentVoucher.paymentMethod" disabled :options="[ 'التحقق من', 'تحويل','الوديعة']" :show-labels="false" v-bind:placeholder="$t('SelectOption')" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                                            </multiselect>

                                        </div>
                                        <div class="form-group" v-else>

                                            <multiselect v-if="($i18n.locale == 'en' ||isLeftToRight()) " v-model="paymentVoucher.paymentMethod" :options="['Cheque', 'Transfer','Deposit']" :show-labels="false" placeholder="Select Type">
                                            </multiselect>
                                            <multiselect v-else v-model="paymentVoucher.paymentMethod" :options="[ 'التحقق من', 'تحويل','الوديعة']" :show-labels="false" v-bind:placeholder="$t('SelectOption')" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                                            </multiselect>

                                        </div>
                                    </div>
                                   
        

                                    <div class="col-lg-4 col-md-4 col-sm-6">
                                        <label v-if="paymentVoucher.paymentMode=='Cash' || paymentVoucher.paymentMode=='السيولة النقدية' ">
                                            {{ $t('PurchaseOrderExpense.CashAccount') }}:
                                            <span class="text-danger"> *</span>

                                        </label>
                                        <label v-else-if="paymentVoucher.paymentMode=='Bank' || paymentVoucher.paymentMode=='مصرف' ">
                                            {{ $t('PurchaseOrderExpense.BankAccount') }}
                                            <span class="text-danger"> *</span>

                                        </label>
                                        <label v-else-if="paymentVoucher.paymentMode=='Advance' || paymentVoucher.paymentMode=='يتقدم' ">
                                            Advance Account :
                                            <span class="text-danger"> *</span>

                                        </label>
                                        <label v-else>
                                            {{ $t('PurchaseOrderExpense.BankAccount') }}:
                                            <span class="text-danger"> *</span>

                                        </label>
                                        <div class="form-group" v-bind:class="{ 'has-danger': v$.paymentVoucher.bankCashAccountId.$error}" v-if="paymentVoucher.paymentMode=='Cash' || paymentVoucher.paymentMode=='السيولة النقدية' " v-bind:key="randerAccount">
                                            <accountdropdown v-model="v$.paymentVoucher.bankCashAccountId.$model" :formName="CashPay" :advance="true"></accountdropdown>
                                            <span v-if="v$.paymentVoucher.bankCashAccountId.$error" class="error">
                                                <span v-if="!v$.paymentVoucher.bankCashAccountId.required">{{formName}}  {{ $t('PurchaseOrderExpense.AccountRequired') }}</span>
                                            </span>
                                        </div>
                                        <div class="form-group" v-bind:class="{ 'has-danger': v$.paymentVoucher.bankCashAccountId.$error}" v-else-if="paymentVoucher.paymentMode=='Bank' || paymentVoucher.paymentMode=='مصرف' " v-bind:key="randerAccount">
                                            <accountdropdown v-model="v$.paymentVoucher.bankCashAccountId.$model" :formName="BankPay" :advance="true"></accountdropdown>
                                            <span v-if="v$.paymentVoucher.bankCashAccountId.$error" class="error">
                                                <span v-if="!v$.paymentVoucher.bankCashAccountId.required">{{formName}}  {{ $t('PurchaseOrderExpense.AccountRequired') }}</span>
                                            </span>
                                        </div>
                                        <div class="form-group" v-bind:class="{ 'has-danger': v$.paymentVoucher.bankCashAccountId.$error}" v-else>
                                            <accountdropdown v-model="v$.paymentVoucher.bankCashAccountId.$model" :formName="'Advance'" :advance="true"></accountdropdown>
                                            <span v-if="v$.paymentVoucher.bankCashAccountId.$error" class="error">
                                                <span v-if="!v$.paymentVoucher.bankCashAccountId.required">{{formName}}  {{ $t('PurchaseOrderExpense.AccountRequired') }}</span>
                                            </span>
                                        </div>
                                    </div>
                                    <div class="col-lg-4 col-md-4 col-sm-6">
                                        <label v-if="formName=='CashReceipt' || formName=='BankReceipt' ">
                                            {{ $t('PurchaseOrderExpense.CustomerAccount') }}:
                                            <span class="text-danger"> *</span>

                                        </label>
                                        <label v-if="formName=='PettyCash' ">
                                            {{ $t('PurchaseOrderExpense.Account') }}:
                                            <span class="text-danger"> *</span>

                                        </label>
                                        <label v-if="formName=='BankPay' || formName=='CashPay'">
                                            {{ $t('PurchaseOrderExpense.SupplierAccount') }}:
                                            <span class="text-danger"> *</span>
                                        </label>
                                        <label v-if="formName=='AdvanceExpense'">
                                            {{ $t('PurchaseOrderExpense.ExpenseAccount') }}:
                                            <span class="text-danger"> *</span>
                                        </label>
                                        <div class="form-group" v-bind:class="{ 'has-danger': v$.paymentVoucher.contactAccountId.$error}">
                                            <accountdropdown @update:modelValue="enableInvoiceDropdown" :formNames="formName" v-model="v$.paymentVoucher.contactAccountId.$model"></accountdropdown>
                                            <span v-if="v$.paymentVoucher.contactAccountId.$error" class="error">
                                                <span v-if="!v$.paymentVoucher.contactAccountId.required">{{formName}}  {{ $t('PurchaseOrderExpense.AccountRequired') }}</span>
                                            </span>
                                        </div>
                                    </div>

                                    <div class="col-lg-4 col-md-4 col-sm-6" v-if="formName=='CashReceipt' || formName=='BankReceipt'">
                                        <label>
                                            {{ $t('PurchaseOrderExpense.SaleInvoice') }}
                                        </label>
                                        <div class="form-group">
                                            <sale-invoice-dropdown ref="saleInvoiceDropdown" v-model="paymentVoucher.saleInvoice" v-bind:isExpense="true" @update:modelValue="getSaleNetAmount" :key="saleInvoiceRander" v-bind:isCredit="true" :contactId="paymentVoucher.contactAccountId" :isDisabled="isShow" />

                                        </div>
                                    </div>
                                    <div hidden class="col-lg-4 col-md-4 col-sm-6" v-if="formName=='BankPay' || formName=='CashPay'">
                                        <label>
                                            {{ $t('PurchaseOrderExpense.PurchaseInvoice') }}
                                        </label>
                                        <div class="form-group">
                                            <purchaseinvoicedropdown @update:modelValue="getPurchaseNetAmount" ref="purchaseInvoiceDropdown" :modelValue="paymentVoucher.purchaseInvoice" v-model="paymentVoucher.purchaseInvoice" v-bind:isExpense="true" :key="purchaseInvoiceRander" :supplierAccountId="paymentVoucher.contactAccountId" :isDisabled="isShow" />

                                        </div>
                                    </div>
                                    <div class="col-lg-4 col-md-4 col-sm-6" v-if="paymentVoucher.paymentMethod=='Cheque' || paymentVoucher.paymentMethod=='التحقق من' ">
                                        <label>
                                            {{ $t('PurchaseOrderExpense.ChequeNumber') }}
                                        </label>
                                        <div class="form-group">
                                            <input v-model="paymentVoucher.chequeNumber" class="form-control" type="text" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'" />
                                        </div>
                                    </div>
                                    <div class="col-lg-4 col-md-4 col-sm-6">

                                    </div>
                                    <div class="col-lg-4 col-md-4 col-sm-6">
                                        <label>
                                            {{ $t('AddLineItem.TaxMethod') }}:
                                            <span class="text-danger"> </span>
                                        </label>
                                        <div class="form-group">
                                            <multiselect :options="options1" @update:modelValue="updateLineTotal(paymentVoucher.taxMethod, 'taxMehtod')" v-model="paymentVoucher.taxMethod" :show-labels="false" v-bind:placeholder="$t('AddLineItem.SelectMethod')" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                                            </multiselect>
                                        </div>
                                    </div>
                                    <div class="col-lg-4 col-md-4 col-sm-6">
                                        <label>
                                            {{ $t('AddLineItem.VAT%') }} :
                                            <span class="text-danger"> </span>
                                        </label>
                                        <div class="form-group">
                                            <taxratedropdown @update:modelValue="updateLineTotal(paymentVoucher.taxRateId,  'vat')" v-model="paymentVoucher.taxRateId" />
                                        </div>
                                    </div>

                                    <div class="col-lg-4 col-md-4 col-sm-6">
                                        <label>
                                            {{ $t('PurchaseOrderExpense.Amount') }} :
                                            <span class="text-danger"> *</span>
                                        </label>
                                        <div class="form-group">
                                            <my-currency-input v-model="paymentVoucher.amount" @update:modelValue="updateLineTotal(paymentVoucher.amount,'amount')"></my-currency-input>
                                            <!--<input v-model="paymentVoucher.amount" class="form-control" type="number" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'" />-->
                                        </div>
                                    </div>
                                    <div class="col-lg-4 col-md-4 col-sm-6">
                                        <label>
                                            {{ $t('AddLineItem.TotalVAT') }}:
                                        </label>
                                        <div class="form-group">
                                            <input class="form-control" disabled v-model="paymentVoucher.vatAmount" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" />
                                        </div>
                                    </div>
                                    <div class="col-lg-4 col-md-4 col-sm-6">
                                        <label>
                                            {{ $t('AddLineItem.Total') }} :
                                        </label>
                                        <div class="form-group">
                                            <input class="form-control" disabled v-model="paymentVoucher.amountAfterVAT" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" />

                                        </div>
                                    </div>
                                   
                                    <div class="col-lg-4 col-md-4 col-sm-6" v-if=" formName=='PettyCash' ">
                                        <label>
                                            {{ $t('PurchaseOrderExpense.PattyCashType') }}
                                        </label>
                                        <div class="form-group">
                                            <multiselect v-if="($i18n.locale == 'en' ||isLeftToRight()) " v-model="paymentVoucher.pettyCash" :options="['Temporary', 'General', 'Advance']" :show-labels="false" placeholder="Select Type">
                                            </multiselect>
                                            <multiselect v-else v-model="paymentVoucher.pettyCash" :options="['مؤقت', 'عام', 'تقدم']" :show-labels="false" v-bind:placeholder="$t('SelectOption')" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                                            </multiselect>
                                        </div>
                                    </div>

                                </div>
                                <div class="row">
                                    <div class="col-lg-8 col-md-8 col-sm-12">
                                        <label>
                                            {{ $t('PurchaseOrderExpense.Narration') }} / {{$t('PurchaseOrderExpense.Remarks')}}
                                        </label>
                                        <div class="form-group">
                                            <textarea v-model="paymentVoucher.narration" class="form-control" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" />
                                        </div>
                                    </div>
                                </div>


                            </div>
                        </div>
                        <div class="row">
                            <div class="col-6 ">
                                <span v-if="paymentVoucher.id=='00000000-0000-0000-0000-000000000000'">
                                    <button type="button" class="btn btn-primary float-right" v-bind:disabled="v$.paymentVoucher.$invalid" v-if=" isValid('CanAddOrderExpense')" v-on:click="SaveVoucher('Approved')"><i class="far fa-save"></i> {{ $t('PurchaseOrderExpense.SaveAndPost') }}</button>
                                </span>
                                <span v-else>
                                    <button type="button" class="btn btn-primary " v-if="  isValid('CanAddOrderExpense')" v-on:click="SaveVoucher('Rejected')"><i class="far fa-save"></i> {{ $t('PaymentVoucher.SaveasReject') }}</button>
                                    <button type="button" class="btn btn-primary  ml-2" v-if=" isValid('CanAddOrderExpense')" v-on:click="SaveVoucher('Approved')"><i class="far fa-save"></i> {{ $t('PurchaseOrderExpense.SaveAndPost') }}</button>
                                </span>
                                <button class="btn mx-2 btn-danger " v-on:click="onCancel">  {{ $t('PurchaseOrderExpense.Cancel') }}</button>


                            </div>
                            <div class="col-6">
                                <button class="btn btn-primary mr-2 float-left" v-on:click="Attachment()">
                                    {{ $t('AddPurchase.Attachment') }}
                                </button>
                            </div>
                           
                        </div>
                    </div>
                </div>
            </div>
            <loading v-model:active="loading" :can-cancel="true" :is-full-page="true"></loading>
            <bulk-attachment :attachmentList=" paymentVoucher.attachmentList" :show="isAttachshow" v-if="isAttachshow" @close="attachmentSaved" />
        </div>
        <div v-else> <acessdenied></acessdenied></div>
    </modal>
</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'
    import { required, minValue } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core';
    import Multiselect from 'vue-multiselect'
    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    import moment from "moment";
    export default {
        name: "PurchaseOrderPayment",
        mixins: [clickMixin],
        components: {
            Multiselect,
            Loading
        },
        props: ['formName', 'show', 'purchaseOrderId', 'newisPurchase', 'isPurchasePostExpense'],
        setup() {
            return { v$: useVuelidate() }
        },
        data: function () {
            return {
                isPurchase:this.newisPurchase,
                isAttachshow: false,
                taxMethodList: [],
                options1: [],
                options: [],

                ispayable: true,
                render: 0,
                totalVAT: 0,
                total: 0,
                saleInvoiceRander: 0,
                purchaseInvoiceRander: 0,
                isShow: true,
                attachment: false,
                paymentVoucher: {
                    id: '00000000-0000-0000-0000-000000000000',
                    date: '',
                    voucherNumber: '',
                    chequeNumber: '',
                    narration: '',
                    billsId: '',
                    paymentVoucherType: '',
                    amount: 0,
                    attachmentList: [],
                    approvalStatus: 'Draft',
                    purchaseInvoice: '',
                    saleInvoice: '',
                    bankCashAccountId: '',
                    contactAccountId: '',
                    paymentMode: '',
                    paymentMethod: '',
                    userName: '',
                    taxRateId: '',
                    vatAccountId: '',
                    taxMethod: '',
                    amountAfterVAT: 0,
                    vatAmount: 0,
                    isPurchasePostExpense: false
                },
                loading: false,
                type: '',
                isBank: true,
                voucherNumberRander: 0,
                language: 'Nothing',
                CashPay: 'CashPay',
                BankPay: 'BankPay',
                randerAccount: 0,
                disable: false
            }
        },
        created() {

        },

        validations() {
            return {
                paymentVoucher: {
                    voucherNumber: {
                        required
                    },
                    date: {
                        required
                    },
                    bankCashAccountId: {
                        required
                    },
                    contactAccountId: {
                        required
                    },
                    billsId: {
                    },
                    amount: { minValue: minValue(1) }
                }
                }
        },
        methods: {
            updateLineTotal: function (e, prop) {
                

                if (prop == "vat") {
                    if (e == '' || e == undefined) {
                        e = '';
                    }
                    else {
                        this.paymentVoucher.taxRateId = e;
                    }
                }

                if (prop == "taxMehtod") {
                    if (e == '' || e == undefined) {
                        e = '';
                    }
                    else if (e == 'Exempted' || e == 'معفى') {
                        this.paymentVoucher.taxRateId = null;
                    }
                    else {
                        this.paymentVoucher.taxMehtod = e;
                    }
                }
                if (prop == "amount") {
                    if (e < 0 || e == '' || e == undefined) {
                        e = '';
                    }
                    else {
                        this.paymentVoucher.amount = e;
                    }
                }

                if (this.paymentVoucher.taxRateId != null && this.paymentVoucher.taxRateId != '' && this.paymentVoucher.taxRateId != undefined) {


                    var tax = this.options.find((value) => value.id == this.paymentVoucher.taxRateId);

                    if (this.paymentVoucher.taxMethod == 'Inclusive' || this.paymentVoucher.taxMethod == 'شامل') {
                        this.paymentVoucher.vatAmount = parseFloat(((this.paymentVoucher.amount * tax.rate) / (100 + tax.rate)).toFixed(3).slice(0, -1));
                        this.paymentVoucher.amountAfterVAT = parseFloat(this.paymentVoucher.amount)
                    }
                    else if (this.paymentVoucher.taxMethod == 'Exclusive' || this.paymentVoucher.taxMethod == 'غير شامل') {
                        this.paymentVoucher.vatAmount = ((this.paymentVoucher.amount * tax.rate) / (100)).toFixed(3).slice(0, -1);
                        this.paymentVoucher.amountAfterVAT = parseFloat(this.paymentVoucher.amount) + parseFloat(this.paymentVoucher.vatAmount)

                    }
                    else if (this.paymentVoucher.taxMethod == 'Exempted' || this.paymentVoucher.taxMethod == 'معفى') {
                        this.paymentVoucher.vatAmount = 0;
                        this.paymentVoucher.amountAfterVAT = parseFloat(this.paymentVoucher.amount).toFixed(3).slice(0, -1);
                        this.randerTaxRate++
                    }
                }
                else if (this.paymentVoucher.taxMethod == 'Exempted' || this.paymentVoucher.taxMethod == 'معفى') {
                    this.paymentVoucher.vatAmount = 0;
                    this.paymentVoucher.amountAfterVAT = parseFloat(this.paymentVoucher.amount).toFixed(3).slice(0, -1);
                    this.randerTaxRate++
                }



                

            },
            VatData: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                
                this.$https.get('/Product/TaxRateList?isActive=true', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                    if (response.data != null) {


                        response.data.taxRates.forEach(function (result) {

                          

                            root.options.push({
                                id: result.id,
                                rate: result.rate,
                                name: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? ((result.name != '' && result.name != null) ? result.code + ' ' + result.name : result.code + ' ' + result.nameArabic) + "(" + result.rate + "%)" : ((result.nameArabic != '' && result.nameArabic != null) ? result.code + ' ' + result.nameArabic : result.code + ' ' + result.name) + "(" + result.rate + "%)"
                            })
                        })
                    }
                });

            },


            Attachment: function () {
                this.isAttachshow = true;
            },

            attachmentSaved: function (attachment) {
                this.paymentVoucher.attachmentList = attachment;
                this.isAttachshow = false;
            },
            GetAccount: function (x) {


                if (x == 'السيولة النقدية' || x == 'Bank') {
                    this.randerAccount++;

                }
                else if (x == 'مصرف' || x == 'Cash') {
                    this.randerAccount++;
                }

            },
            DownloadAttachment(path) {

                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                var ext = path.split('.')[1];
                root.$https.get('/Contact/DownloadFile?filePath=' + path, { headers: { "Authorization": `Bearer ${token}` }, responseType: 'blob' })
                    .then(function (response) {
                        const url = window.URL.createObjectURL(new Blob([response.data]));
                        const link = document.createElement('a');
                        link.href = url;
                        link.setAttribute('download', 'file.' + ext);
                        document.body.appendChild(link);
                        link.click();
                    });
            },

            uploadImage() {

                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                var file = null;

                file = this.$refs.imgupload1.files;

                var fileData = new FormData();
                for (var k = 0; k < file.length; k++) {
                    fileData.append("files", file[k]);
                }
                root.$https.post('/Company/UploadFilesAsync', fileData, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {


                        if (response.data != null) {

                            root.paymentVoucher.path = response.data;

                        }
                    },
                        function () {
                            this.loading = false;
                            root.$swal({
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                                text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Something Went Wrong!' : 'هل هناك خطب ما!',
                                type: 'error',
                                confirmButtonClass: "btn btn-danger",
                                buttonsStyling: false
                            });
                        });
            },

          
            UpdateStatus: function (status) {
                this.loading = true;
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                this.$https.post('/PaymentVoucher/UpdateStatusPaymentVoucher?id=' + this.paymentVoucher.id + '&approvalStatus=' + status, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                    if (response.data.message.id != '00000000-0000-0000-0000-000000000000' && response.data.type == 'Edit') {
                        root.$swal({
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Update!' : 'تم التحديث!',
                            text: response.data.message.isAddUpdate,
                            type: 'success',
                            icon: 'success',
                            showConfirmButton: false,
                            timer: 2000,
                            timerProgressBar: true,
                            confirmButtonClass: "btn btn-success",
                            buttonsStyling: false
                        }).then(function (result) {
                            if (result) {

                                if (root.ispayable) {
                                    window.location.href = "/paymentVoucherList?formName=" + root.formName;
                                }
                            }
                        });

                    }
                    else if (response.data.message.id == '00000000-0000-0000-0000-000000000000') {
                        root.$swal({
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                            text: response.data.message.isAddUpdate,
                            type: 'error',
                            confirmButtonClass: "btn btn-info",
                            buttonsStyling: false
                        });
                    }

                }, function (value) {
                    root.$swal({
                        title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                        text: value,
                        type: 'error',
                        confirmButtonClass: "btn btn-info",
                        buttonsStyling: false
                    });
                }
                ).catch(error => {

                    var customError = JSON.stringify(error.response.data.error);
                    root.$swal({
                        title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                        text: customError,
                        type: 'error',
                        confirmButtonClass: "btn btn-info",
                        buttonsStyling: false
                    });
                    root.loading = false;
                });
            },
            getSaleNetAmount: function () {

                this.paymentVoucher.amount = this.$refs.saleInvoiceDropdown.GetAmountOfSelected()
            },
            getPurchaseNetAmount: function () {
                this.paymentVoucher.amount = this.$refs.purchaseInvoiceDropdown.GetAmountOfSelected()
            },
            enableInvoiceDropdown: function () {

                this.paymentVoucher.amount = 0;
                this.paymentVoucher.chequeNumber = ''
                this.paymentVoucher.saleInvoice = '00000000-0000-0000-0000-000000000000';
                this.paymentVoucher.purchaseInvoice = '00000000-0000-0000-0000-000000000000';
                if (this.formName == 'CashReceipt' || this.formName == 'BankReceipt') {
                    this.isShow = false
                    this.saleInvoiceRander++;
                }
                else if (this.formName == 'BankPay' || this.formName == 'CashPay') {
                    this.isShow = false
                    this.purchaseInvoiceRander++;
                }

            },
            languageChange: function (lan) {
                if (this.language == lan) {

                    if (this.paymentVoucher.id == '00000000-0000-0000-0000-000000000000') {

                        var getLocale = this.$i18n.locale;
                        this.language = getLocale;

                        this.$router.go('/addPaymentVoucherformName?formName=' + this.formName);
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

            GetAutoCodeGenerator: function (value) {

                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.$https.get('/PaymentVoucher/AutoGenerateCode?paymentVoucherType=' + value, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {
                        root.paymentVoucher.voucherNumber = response.data;
                        root.voucherNumberRander++;
                    }
                });
            },

            SaveVoucher: function (x) {

                if (this.$i18n.locale == 'ar') {
                    if (this.paymentVoucher.pettyCash == 'مؤقت') {
                        this.paymentVoucher.pettyCash = 1;
                    }
                    if (this.paymentVoucher.pettyCash == 'عام') {
                        this.paymentVoucher.pettyCash = 2;
                    }
                    if (this.paymentVoucher.pettyCash == 'تقدم') {
                        this.paymentVoucher.pettyCash = 3;
                    }
                    if (this.paymentVoucher.paymentMethod == 'التحقق من') {
                        this.paymentVoucher.paymentMethod = 1;
                    }
                    else if (this.paymentVoucher.paymentMethod == 'تحويل') {
                        this.paymentVoucher.paymentMethod = 2;
                    }
                    else if (this.paymentVoucher.paymentMethod == 'الوديعة') {
                        this.paymentVoucher.paymentMethod = 3;
                    }
                    else {
                        this.paymentVoucher.paymentMethod = 0;
                    }

                    if (this.paymentVoucher.paymentMode == 'السيولة النقدية') {
                        this.paymentVoucher.paymentMode = 0;
                    }
                    if (this.paymentVoucher.paymentMode == 'مصرف') {
                        this.paymentVoucher.paymentMode = 1;
                    }
                }
                if ((this.$i18n.locale == 'en' || this.isLeftToRight())) {
                    if (this.paymentVoucher.paymentMethod == 'Cheque') {
                        this.paymentVoucher.paymentMethod = 1;
                    }
                    else if (this.paymentVoucher.paymentMethod == 'Transfer') {
                        this.paymentVoucher.paymentMethod = 2;
                    }
                    else if (this.paymentVoucher.paymentMethod == 'Deposit') {
                        this.paymentVoucher.paymentMethod = 3;
                    }
                    else {
                        this.paymentVoucher.paymentMethod = 0;
                    }
                    if (this.paymentVoucher.paymentMode == 'Cash') {
                        this.paymentVoucher.paymentMode = 0;
                    }
                    if (this.paymentVoucher.paymentMode == 'Bank') {
                        this.paymentVoucher.paymentMode = 1;
                    }
                }
                if (this.paymentVoucher.paymentMethod != 1) {
                    this.paymentVoucher.chequeNumber = '';
                }
                this.loading = true;
                var root = this;
                var token = '';
                this.paymentVoucher.approvalStatus = x;
                this.paymentVoucher.userName = localStorage.getItem('LoginUserName');
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                if (!this.isPurchasePostExpense) {
                    this.paymentVoucher.isPurchasePost = false
                }
                else {
                    this.isPurchase = false
                    this.paymentVoucher.isPurchasePostExpense = true
                }

                this.$https.post('/PaymentVoucher/AddPaymentVoucher?purchaseOrderId=' + this.purchaseOrderId + '&isPurchaseOrderExpense=' + this.isPurchase, this.paymentVoucher, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data.message.id != '00000000-0000-0000-0000-000000000000' && response.data.type == 'Add') {

                            root.$swal({
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved!' : '!تم الحفظ',
                                text: response.data.message.isAddUpdate,
                                type: 'success',
                                icon: 'success',
                                showConfirmButton: false,
                                timer: 2000,
                                timerProgressBar: true,
                                confirmButtonClass: "btn btn-success",
                                buttonsStyling: false
                            });
                            root.onCancel();
                        }
                        else if (response.data.message.id != '00000000-0000-0000-0000-000000000000' && response.data.type == 'Edit') {
                            root.$swal({
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Update!' : 'تم التحديث!',
                                text: response.data.message.isAddUpdate,
                                type: 'success',
                                icon: 'success',
                                showConfirmButton: false,
                                timer: 2000,
                                timerProgressBar: true,
                                confirmButtonClass: "btn btn-success",
                                buttonsStyling: false
                            });
                            root.onCancel();
                        }

                    }).catch(error => {
                        console.log(error)
                        root.$swal.fire(
                            {
                                type: 'error',
                                icon: 'error',
                                title: root.$t('Error'),
                                text: error.response.data,
                                confirmButtonClass: "btn btn-danger",
                                showConfirmButton: true,
                                timer: 5000,
                                timerProgressBar: true,
                            });

                        root.loading = false
                    })
                    .finally(() => root.loading = false)
            },
            getpaymentVoucherDetails: function (paymentVoucherItem) {

                this.paymentVoucher = paymentVoucherItem;
            },
            onCancel: function () {
                this.$emit('close');
            },
        },
        mounted: function () {

            if ((this.$i18n.locale == 'en' || this.isLeftToRight())) {
                this.options1 = ['Inclusive', 'Exclusive', 'Exempted'];
            }
            else {
                this.options1 = ['شامل', 'غير شامل', 'معفى'];
            }

            this.language = this.$i18n.locale;
            if ((this.$i18n.locale == 'en' || this.isLeftToRight())) {
                this.taxMethodList = ['Inclusive', 'Exclusive'];
            }
            else {
                this.taxMethodList = ['شامل', 'غير شامل'];
            }
            this.paymentVoucher.date = moment().format("DD MMM YYYY");
            if (this.formName == 'AdvanceExpense') {
                
                if (this.$route.query.details == undefined) {
                    this.GetAutoCodeGenerator(this.formName);
                    this.VatData();
                    this.paymentVoucher.paymentVoucherType = this.formName;
                }
                if (this.$route.query.details != undefined) {
                    this.paymentVoucher = this.$route.query.details.message;
                    this.isShow = false
                    this.attachment = true;
                    this.purchaseInvoiceRander++
                    this.paymentVoucher.paymentVoucherType = 'AdvanceExpense';
                    if (this.$i18n.locale == 'ar') {
                        if (this.paymentVoucher.paymentMethod == 1) {
                            this.paymentVoucher.paymentMethod = 'التحقق من';
                        }
                        else if (this.paymentVoucher.paymentMethod == 2) {
                            this.paymentVoucher.paymentMethod = 'تحويل';
                        }
                        else if (this.paymentVoucher.paymentMethod == 3) {
                            this.paymentVoucher.paymentMethod = 'الوديعة';
                        }
                        else {
                            this.paymentVoucher.paymentMethod = '';
                        }

                        if (this.paymentVoucher.paymentMode == 0) {
                            this.paymentVoucher.paymentMode = 'السيولة النقدية';
                        }
                        if (this.paymentVoucher.paymentMode == 1) {
                            this.paymentVoucher.paymentMode = 'مصرف';
                        }
                    }
                    if ((this.$i18n.locale == 'en' || this.isLeftToRight())) {
                        if (this.paymentVoucher.paymentMethod == 1) {
                            this.paymentVoucher.paymentMethod = 'Cheque';
                        }
                        else if (this.paymentVoucher.paymentMethod == 2) {
                            this.paymentVoucher.paymentMethod = 'Transfer';
                        }
                        else if (this.paymentVoucher.paymentMethod == 3) {
                            this.paymentVoucher.paymentMethod = 'Deposit';
                        }
                        else {
                            this.paymentVoucher.paymentMethod = '';
                        }
                        if (this.paymentVoucher.paymentMode == 0) {
                            this.paymentVoucher.paymentMode = 'Cash';
                        }
                        if (this.paymentVoucher.paymentMode == 1) {
                            this.paymentVoucher.paymentMode = 'Bank';
                        }
                    }
                }
            }
            this.render++;
        }
    }
</script>