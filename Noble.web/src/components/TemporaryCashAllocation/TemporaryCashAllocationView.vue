<template>
    <div class="row" v-if=" (isValid('CanViewDetailPettyCash') && formName=='PettyCash') || (isValid('CanViewDetailCPR') && formName=='BankReceipt') || (isValid('CanViewDetailSPR') && formName=='BankPay')">
        <div class="col-lg-8 col-md-8  ml-auto mr-auto" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
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
                    <div class="invoice-area">
                        <div class="invoice-head">
                            <div class="row">
                                <div class=" col-lg-8  ">
                                    <h4 class="card-title " v-if="formName=='BankReceipt'">{{ $t('PaymentVoucherView.CustomerPayReceipt') }} -  {{ paymentVoucher.voucherNumber }}</h4>
                                    <h4 class="card-title " v-if="formName=='BankPay'"> {{ $t('PaymentVoucherView.SupplierPaymentReceipt') }} -  {{ paymentVoucher.voucherNumber }}</h4>
                                    <h4 class="card-title " v-if="formName=='PettyCash'">{{ $t('PaymentVoucherView.PettyCash') }} -  {{ paymentVoucher.voucherNumber }}</h4>
                                    <br />
                                </div>
                                <div class="col-lg-4" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'arabicLanguage' : 'text-left'">
                                    <span>
                                        {{getDate(paymentVoucher.date)}}
                                    </span>
                                </div>
                            </div>
                        </div>
                        <div class="row" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                            <div class="col-lg-12">

                                <div class="row">

                                    <div class="col-lg-6 col-md-6 col-sm-6">
                                        <label>
                                            {{ $t('PaymentVoucherView.PaymentMode') }}:

                                        </label>

                                    </div>
                                    <div class="col-lg-6 col-md-6 col-sm-6">
                                        <div class="form-group">
                                            {{paymentVoucher.paymentMode}}
                                        </div>
                                    </div>
                                    <div class="col-lg-6 col-md-6 col-sm-6">
                                        <label>
                                            {{ $t('PaymentVoucherView.PaymentType') }}:

                                        </label>

                                    </div>
                                    <div class="col-lg-6 col-md-6 col-sm-6">
                                        <div class="form-group">
                                            {{paymentVoucher.paymentMethod}}
                                        </div>
                                    </div>

                                    <div class="col-lg-6 col-md-6 col-sm-6">
                                        <label>
                                            {{ $t('PaymentVoucherView.BankCashAccount') }}:

                                        </label>

                                    </div>
                                    <div class="col-lg-6 col-md-6 col-sm-6">
                                        <div class="form-group">
                                            {{paymentVoucher.bankCashAccountName}}
                                        </div>
                                    </div>
                                    <div class="col-lg-6 col-md-6 col-sm-6">
                                        <label v-if="formName=='CashReceipt' || formName=='BankReceipt' || formName=='PettyCash'">
                                            {{ $t('PaymentVoucherView.CustomerAccount') }}
                                        </label>
                                        <label v-if="formName=='BankPay' || formName=='CashPay'">
                                            {{ $t('PaymentVoucherView.SupplierAccount') }}
                                        </label>

                                    </div>
                                    <div class="col-lg-6 col-md-6 col-sm-6">
                                        <div class="form-group">
                                            <div v-if="($i18n.locale == 'en' ||isLeftToRight())">
                                                {{paymentVoucher.contactAccountName}}
                                            </div>
                                            <div v-else-if="$i18n.locale == 'ar' & paymentVoucher.contactAccountNameAr!=''">
                                                {{paymentVoucher.contactAccountNameAr}}
                                            </div>
                                            <div v-else>
                                                {{paymentVoucher.contactAccountName}}
                                            </div>
                                        </div>
                                    </div>


                                    <div class=" col-sm-6" v-if="formName=='CashReceipt' || formName=='BankReceipt'">
                                        <label>
                                            {{ $t('PaymentVoucherView.SaleInvoice') }}
                                        </label>

                                    </div>
                                    <div class=" col-sm-6" v-if="formName=='CashReceipt' || formName=='BankReceipt'">

                                        <div class="form-group">
                                            {{paymentVoucher.saleInvoiceCode}}

                                        </div>
                                    </div>
                                    <div class="col-sm-6" v-if="formName=='BankPay' || formName=='CashPay'">
                                        <label>
                                            {{ $t('PaymentVoucherView.PurchaseInvoice') }}
                                        </label>

                                    </div>
                                    <div class="col-sm-6" v-if="formName=='BankPay' || formName=='CashPay'">

                                        <div class="form-group">
                                            {{paymentVoucher.purchaseInvoiceCode}}

                                        </div>
                                    </div>
                                    <div class=" col-sm-6" v-if="formName =='BankReceipt' || formName =='BankPay'">
                                        <label>
                                            {{ $t('PaymentVoucherView.ChequeNumber') }}
                                        </label>

                                    </div>
                                    <div class=" col-sm-6" v-if="formName =='BankReceipt' || formName =='BankPay'">
                                        {{paymentVoucher.chequeNumber}}

                                    </div>
                                    <div class=" col-sm-6" v-if="formName =='PettyCash'">
                                        <label>
                                            {{ $t('PaymentVoucherView.PattyCashType') }}

                                        </label>

                                    </div>
                                    <div class=" col-sm-6" v-if="formName =='PettyCash'">
                                        {{paymentVoucher.pettyCash}}


                                    </div>



                                    <div class=" col-sm-6">
                                        <label>
                                            {{ $t('PaymentVoucherView.Amount') }}
                                        </label>

                                    </div>

                                    <div class=" col-sm-6">
                                        {{parseFloat(paymentVoucher.amount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}

                                    </div>

                                </div>
                                <div class="row">
                                    <div class="col-lg-6 ">
                                        <label>
                                            {{ $t('PaymentVoucherView.Narration') }} / {{$t('PaymentVoucherView.Remarks')}}
                                        </label>

                                    </div>
                                    <div class="col-lg-6 ">

                                        <div class="form-group">
                                            {{paymentVoucher.narration}}
                                        </div>
                                    </div>
                                </div>
                            </div>


                        </div>
                        <br />
                    </div>

                    <div class="">
                        <button class="btn btn-primary float-left mr-2" v-on:click="Attachment()">
                            {{ $t('QuotationView.Attachment') }}
                        </button>
                        <button class="btn btn-danger  float-right" v-on:click="onCancel">  {{ $t('PaymentVoucherView.Cancel') }}</button>
                        <button class="btn btn-primary  float-right" v-on:click="PrintPaymentVoucher(paymentVoucher.id)" v-if=" ((isValid('CanPrintPettyCashTemplate1') || isValid('CanPrintPettyCashTemplate2')) && formName=='PettyCash') || (isValid('CanPrintCPR') && formName=='BankReceipt')|| (isValid('CanPrintSPR') && formName=='BankPay')">  {{ $t('PaymentVoucherView.Print') }}</button>


                    </div>
                </div>
            </div>
        </div>
        <bulk-attachment :documentid="paymentVoucher.id" :show="show" v-if="show" @close="attachmentSave" />
        <loading v-model:active="loading" :can-cancel="true" :is-full-page="true"></loading>
        
    </div>
    <div v-else> <acessdenied></acessdenied></div>
</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'
    import { required } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core';
    import moment from "moment";
    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    export default {
        mixins: [clickMixin],
        props: ['formName'],
        setup() {
            return { v$: useVuelidate() }
        },
        data: function () {
            return {
                ispayable: true,
                render: 0,
                paymentVoucher: {
                    id: '00000000-0000-0000-0000-000000000000',
                    date: '',
                    voucherNumber: '',
                    chequeNumber: '',
                    narration: '',
                    paymentVoucherType: '',
                    amount: 0,
                    approvalStatus: 'Draft',
                    purchaseInvoice: '',
                    saleInvoice: '',
                    bankCashAccountId: '',
                    contactAccountId: ''
                },
                printDetails: [],
                printId: '00000000-0000-0000-0000-000000000000',
                printRender: 0,
                printed: false,
                loading: false,
                show: false,
                type: '',
                isBank: true,
                voucherNumberRander: 0,
                language: 'Nothing',
                accountrender: 0,

            }
        },
        created() {
            if (this.formName == 'CashReceipt' || this.formName == 'BankReceipt') {
                this.paymentVoucher.purchaseInvoice = '00000000-0000-0000-0000-000000000000';
                this.paymentVoucher.saleInvoice = '';
            }
            else if (this.formName == 'BankPay' || this.formName == 'CashPay') {
                this.paymentVoucher.purchaseInvoice = '';
                this.paymentVoucher.saleInvoice = '00000000-0000-0000-0000-000000000000';
            }
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
                    saleInvoice: {
                        required
                    },
                    purchaseInvoice: {
                        required
                    }
                }
                }
        },
        methods: {
            Attachment: function () {
                this.show = true;
            },

            attachmentSave: function () {
                this.show = false;
            },

            PrintPaymentVoucher: function (value) {
                
                var id = value;
                this.printId = id;
                this.printRender++;
            },
            UpdateStatusToVoid: function () {
                this.loading = true;
                var root = this;
                var token = '';
                this.paymentVoucher.approvalStatus = "Void";
                this.paymentVoucher.date = moment().format("llll");
                this.paymentVoucher.paymentVoucherType = this.formName;
                this.paymentVoucher.userName = localStorage.getItem('LoginUserName');
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.$https.post('/PaymentVoucher/AddPaymentVoucher', this.paymentVoucher, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
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
                        }).then(function (result) {
                            if (result) {

                                if (root.ispayable) {
                                    window.location.href = "/addPaymentVoucher?formName=" + root.formName;
                                }
                            }
                        });

                    }
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

                                window.location.href = "/paymentVoucherList?formName=" + root.formName;
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
            getDate: function (x) {
                return moment(x).format('DD MMM YYYY');
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
            getPaymentVoucherDetailss: function (paymentVoucherDetails) {
                this.paymentVoucher.paymentVoucherDetails = paymentVoucherDetails;
            },
            SaveVoucher: function (x) {
                this.loading = true;
                var root = this;
                var token = '';
                this.paymentVoucher.isDraft = x;
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.$https.post('/PaymentVoucher/AddPaymentVoucher', this.paymentVoucher, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data.message.id != '00000000-0000-0000-0000-000000000000' && response.data.type == 'Add') {
                        root.$swal({
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved!' : '!تم الحفظ',
                            text: response.data.message.isAddUpdate,
                            type: 'success',
                            icon: 'success',
                            showConfirmButton: false,
                            timer: 800,
                            timerProgressBar: true,
                            confirmButtonClass: "btn btn-success",
                            buttonsStyling: false
                        }).then(function (result) {
                            if (result) {

                                if (root.ispayable) {
                                    window.location.href = "/addPaymentVoucher?formName=" + root.formName;
                                }
                            }
                        });

                    }
                    if (response.data.message.id != '00000000-0000-0000-0000-000000000000' && response.data.type == 'Edit') {
                        root.$swal({
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Update!' : 'تم التحديث!',
                            text: response.data.message.isAddUpdate,
                            type: 'success',
                            icon: 'success',
                            showConfirmButton: false,
                            timer: 800,
                            timerProgressBar: true,
                            confirmButtonClass: "btn btn-success",
                            buttonsStyling: false
                        }).then(function (result) {
                            if (result) {

                                if (root.ispayable) {
                                    window.location.href = "/addPaymentVoucher?formName=" + root.formName;
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
                );
            },
            getpaymentVoucherDetails: function (paymentVoucherItem) {

                this.paymentVoucher = paymentVoucherItem;
            },
            onCancel: function () {
                this.$router.push({
                    path: '/paymentVoucherList',
                    query: {
                        data: 'PaymentVoucherLists' + this.formName,
                        formName: this.formName
                    }
                })
            },
        },
        watch: {
            formName: function () {
                if (this.formName == 'BankPay') {
                    if (this.$route.query.data == undefined) {
                        this.GetAutoCodeGenerator(this.formName);
                        this.paymentVoucher.paymentVoucherType = this.formName;
                    }
                    if (this.$route.query.data != undefined) {
                        this.attachment = true;
                        this.paymentVoucher = this.$route.query.data.message;
                        this.paymentVoucher.paymentVoucherType = 'BankPay';
                        this.paymentVoucherDetails = this.$route.query.data.message.paymentVoucherDetails;
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
                            if (this.paymentVoucher.paymentMode == 5) {
                                this.paymentVoucher.paymentMode = 'يتقدم';
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
                            if (this.paymentVoucher.paymentMode == 5) {
                                this.paymentVoucher.paymentMode = 'Advance';
                            }

                        }
                    }
                }

                if (this.formName == 'BankReceipt') {
                    if (this.$route.query.data == undefined) {
                        this.GetAutoCodeGenerator(this.formName);
                        this.paymentVoucher.paymentVoucherType = this.formName;
                    }
                    if (this.$route.query.data != undefined) {
                        this.attachment = true;
                        this.paymentVoucher = this.$route.query.data.message;
                        this.paymentVoucher.paymentVoucherType = 'BankReceipt';
                        this.paymentVoucherDetails = this.$route.query.data.message.paymentVoucherDetails;
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
                            if (this.paymentVoucher.paymentMode == 5) {
                                this.paymentVoucher.paymentMode = 'يتقدم';
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
                            if (this.paymentVoucher.paymentMode == 5) {
                                this.paymentVoucher.paymentMode = 'Advance';
                            }

                        }
                    }
                }
                if (this.formName == 'PettyCash') {
                    if (this.$route.query.data == undefined) {
                        this.GetAutoCodeGenerator(this.formName);
                        this.paymentVoucher.paymentVoucherType = this.formName;
                    }
                    if (this.$route.query.data != undefined) {
                        this.paymentVoucher = this.$route.query.data.message;
                        this.paymentVoucher.paymentVoucherType = 'PettyCash';
                        this.paymentVoucherDetails = this.$route.query.data.message.paymentVoucherDetails;
                        if (this.$i18n.locale == 'ar') {
                            if (this.paymentVoucher.pettyCash == 1) {
                                this.paymentVoucher.pettyCash = 'مؤقت';
                            }
                            if (this.paymentVoucher.pettyCash == 2) {
                                this.paymentVoucher.pettyCash = 'عام';
                            }
                            if (this.paymentVoucher.pettyCash == 3) {
                                this.paymentVoucher.pettyCash = 'تقدم';
                            }

                        }
                        if ((this.$i18n.locale == 'en' || this.isLeftToRight())) {
                            if (this.paymentVoucher.pettyCash == 1) {
                                this.paymentVoucher.pettyCash = 'Temporary';
                            }
                            if (this.paymentVoucher.pettyCash == 2) {
                                this.paymentVoucher.pettyCash = 'General';
                            }
                            if (this.paymentVoucher.pettyCash == 3) {
                                this.paymentVoucher.pettyCash = 'Advance';
                            }

                        }
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
                            if (this.paymentVoucher.paymentMode == 5) {
                                this.paymentVoucher.paymentMode = 'يتقدم';
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
                            if (this.paymentVoucher.paymentMode == 5) {
                                this.paymentVoucher.paymentMode = 'Advance';
                            }

                        }

                    }

                }
            }
        },
        mounted: function () {

            this.language = this.$i18n.locale;
            this.paymentVoucher.date = moment().format("DD MMM YYYY");
            if (this.formName == 'BankPay') {
                if (this.$route.query.data == undefined) {
                    this.GetAutoCodeGenerator(this.formName);
                    this.paymentVoucher.paymentVoucherType = this.formName;

                }
                if (this.$route.query.data != undefined) {
                    this.paymentVoucher = this.$route.query.data.message;
                    this.isShow = false
                    this.attachment = true;
                    this.purchaseInvoiceRander++
                    this.paymentVoucher.paymentVoucherType = 'BankPay';
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
                        if (this.paymentVoucher.paymentMode == 5) {
                            this.paymentVoucher.paymentMode = 'يتقدم';
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
                        if (this.paymentVoucher.paymentMode == 5) {
                            this.paymentVoucher.paymentMode = 'Advance';
                        }

                    }

                }
            }
            if (this.formName == 'PettyCash') {
                if (this.$route.query.data == undefined) {
                    this.GetAutoCodeGenerator(this.formName);
                    this.paymentVoucher.paymentVoucherType = this.formName;
                    if (this.$i18n.locale == 'ar') {
                        this.paymentVoucher.paymentMode = 'السيولة النقدية';

                    }
                    if ((this.$i18n.locale == 'en' || this.isLeftToRight())) {
                        this.paymentVoucher.paymentMode = 'Cash';

                    }
                }
                if (this.$route.query.data != undefined) {
                    this.paymentVoucher = this.$route.query.data.message;
                    this.isShow = false
                    this.attachment = true;
                    this.saleInvoiceRander++
                    this.paymentVoucher.paymentVoucherType = 'PettyCash';
                    if (this.$i18n.locale == 'ar') {
                        if (this.paymentVoucher.pettyCash == 1) {
                            this.paymentVoucher.pettyCash = 'مؤقت';
                        }
                        if (this.paymentVoucher.pettyCash == 2) {
                            this.paymentVoucher.pettyCash = 'عام';
                        }
                        if (this.paymentVoucher.pettyCash == 3) {
                            this.paymentVoucher.pettyCash = 'تقدم';
                        }

                    }
                    if ((this.$i18n.locale == 'en' || this.isLeftToRight())) {
                        if (this.paymentVoucher.pettyCash == 1) {
                            this.paymentVoucher.pettyCash = 'Temporary';
                        }
                        if (this.paymentVoucher.pettyCash == 2) {
                            this.paymentVoucher.pettyCash = 'General';
                        }
                        if (this.paymentVoucher.pettyCash == 3) {
                            this.paymentVoucher.pettyCash = 'Advance';
                        }

                    }
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
                        if (this.paymentVoucher.paymentMode == 5) {
                            this.paymentVoucher.paymentMode = 'Advance';
                        }

                    }

                }
            }
            if (this.formName == 'BankReceipt') {
                if (this.$route.query.data == undefined) {
                    this.GetAutoCodeGenerator(this.formName);
                    this.paymentVoucher.paymentVoucherType = this.formName;
                }
                if (this.$route.query.data != undefined) {

                    this.paymentVoucher = this.$route.query.data.message;
                    this.isShow = false
                    this.attachment = true;
                    this.saleInvoiceRander++;
                    this.paymentVoucher.paymentVoucherType = 'BankReceipt';
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
                        if (this.paymentVoucher.paymentMode == 5) {
                            this.paymentVoucher.paymentMode = 'يتقدم';
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
                        if (this.paymentVoucher.paymentMode == 5) {
                            this.paymentVoucher.paymentMode = 'Advance';
                        }

                    }

                }
            }
            this.render++;
        }
    }
</script>