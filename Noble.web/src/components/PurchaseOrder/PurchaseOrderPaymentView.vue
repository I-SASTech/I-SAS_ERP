<template>
    <modal :show="show" :modalLarge="true">
        <div class="modal-content" v-if=" isValid('CanPayAdvanceFromView') || isValid('CanViewDetailAdvancePayment') || isValid('CanViewDetailOrderExpense') || isValid('CanServicePayAdvanceFromView')">
            <div class="modal-header">
                <h6 class="modal-title" v-if="formName=='BankReceipt'">{{ $t('PurchaseOrderPaymentView.CustomerPayReceipt') }} -  {{ paymentVoucher.voucherNumber }}</h6>
                <h6 class="modal-title" v-if="formName=='BankPay'"> {{ $t('PurchaseOrderPaymentView.SupplierPaymentReceipt') }} -  {{ paymentVoucher.voucherNumber }}</h6>
                <h6 class="modal-title" v-if="formName=='PettyCash'">{{ $t('PurchaseOrderPaymentView.PettyCash') }} -  {{ paymentVoucher.voucherNumber }}</h6>
                <h6 class="modal-title" v-if="formName=='AdvancePay'">{{ $t('PurchaseOrderPaymentView.AdvancePay') }} -  {{ paymentVoucher.voucherNumber }}</h6>
                <h6 class="modal-title" v-if="formName=='AdvanceReceipt'">{{ $t('PurchaseOrderPaymentView.AdvancePayment') }} -  {{ paymentVoucher.voucherNumber }}</h6>
                <h6 class="modal-title" v-if="formName=='AdvanceExpense'">{{ $t('PurchaseOrderPaymentView.Expense') }} -  {{ paymentVoucher.voucherNumber }}</h6>

                <button type="button" class="btn-close" v-on:click="onCancel"></button>
            </div>
            <div class="modal-body">
                <div class="row">

                    <div class="col-lg-6 col-md-6 col-sm-6">
                        <label>
                            {{ $t('PurchaseOrderPaymentView.PaymentMode') }}:

                        </label>
                    </div>
                    <div class="col-lg-6 col-md-6 col-sm-6">
                        <div class="form-group">
                            {{paymentVoucher.paymentMode}}
                        </div>
                    </div>

                    <div class="col-lg-6 col-md-6 col-sm-6">
                        <label>
                            {{ $t('PurchaseOrderPaymentView.PaymentType') }}:
                        </label>

                    </div>
                    <div class="col-lg-6 col-md-6 col-sm-6">
                        <div class="form-group">
                            {{paymentVoucher.paymentMethod}}
                        </div>
                    </div>

                    <div class="col-lg-6 col-md-6 col-sm-6">
                        <label>
                            {{ $t('PurchaseOrderPaymentView.BankCashAccount') }}:
                        </label>
                    </div>
                    <div class="col-lg-6 col-md-6 col-sm-6">
                        <div class="form-group">
                            {{paymentVoucher.bankCashAccountName}}
                        </div>
                    </div>
                    <div class="col-lg-6 col-md-6 col-sm-6">
                        <label v-if="formName=='CashReceipt' || formName=='BankReceipt' || formName=='PettyCash' || formName=='AdvanceReceipt'">
                            {{ $t('PurchaseOrderPaymentView.CustomerAccount') }}
                        </label>
                        <label v-if="formName=='BankPay' || formName=='CashPay' || formName=='AdvancePay'">
                            {{ $t('PurchaseOrderPaymentView.SupplierAccount') }}
                        </label>
                        <label v-if="formName=='AdvanceExpense'">
                            {{ $t('PurchaseOrderPaymentView.ExpenseAccount') }}
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
                            {{ $t('PurchaseOrderPaymentView.SaleInvoice') }}
                        </label>

                    </div>
                    <div class=" col-sm-6" v-if="formName=='CashReceipt' || formName=='BankReceipt'">

                        <div class="form-group">
                            {{paymentVoucher.saleInvoiceCode}}

                        </div>
                    </div>
                    <div class="col-sm-6" v-if="formName=='BankPay' || formName=='CashPay'">
                        <label>
                            {{ $t('PurchaseOrderPaymentView.PurchaseInvoice') }}
                        </label>

                    </div>
                    <div class="col-sm-6" v-if="formName=='BankPay' || formName=='CashPay'">

                        <div class="form-group">
                            {{paymentVoucher.purchaseInvoiceCode}}

                        </div>
                    </div>
                    <div class=" col-sm-6" v-if="formName =='BankReceipt' || formName =='BankPay'">
                        <label>
                            {{ $t('PurchaseOrderPaymentView.ChequeNumber') }}
                        </label>

                    </div>
                    <div class=" col-sm-6" v-if="formName =='BankReceipt' || formName =='BankPay'">
                        {{paymentVoucher.chequeNumber}}

                    </div>
                    <div class=" col-sm-6" v-if="formName =='PettyCash'">
                        <label>
                            {{ $t('PurchaseOrderPaymentView.PattyCashType') }}

                        </label>

                    </div>
                    <div class=" col-sm-6" v-if="formName =='PettyCash'">
                        {{paymentVoucher.pettyCash}}


                    </div>



                    <div class=" col-sm-6">
                        <label>
                            {{ $t('PurchaseOrderPaymentView.Amount') }}
                        </label>

                    </div>
                    <div class=" col-sm-6">
                        {{parseFloat(paymentVoucher.amount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}

                    </div>

                    <div class="col-sm-6 ">
                        <label>
                            {{ $t('PurchaseOrderPaymentView.Narration') }} / {{$t('PurchaseOrderPaymentView.Remarks')}}
                        </label>
                    </div>
                    <div class="col-sm-6 ">
                        {{paymentVoucher.narration}}
                    </div>

                    <!--<div class="col-sm-6 ">
                        <label>
                            {{ $t('Product.TaxMethod') }}
                        </label>
                    </div>
                    <div class="col-sm-6 ">
                        {{paymentVoucher.taxMethod}}
                    </div>

                    <div class="col-sm-6 ">
                        <label>
                            {{ $t('PurchaseOrder.VAT%') }}
                        </label>
                    </div>
                    <div class="col-sm-6 ">
                        {{paymentVoucher.rate}}
                    </div>

                    <div class="col-sm-6 ">
                        <label>
                            {{ $t('VatAccount') }}
                        </label>
                    </div>

                    <div class="col-sm-6 ">
                        {{paymentVoucher.vatName}}
                    </div>-->
                    <div class="col-sm-6 ">
                        <label>
                            {{ $t('PurchaseOrderPaymentView.Bills') }}
                        </label>
                    </div>
                    <div class="col-sm-6 ">
                        {{paymentVoucher.billsName}}
                    </div>
                </div>

            </div>

            <div class="modal-footer">
                <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="PrintPaymentVoucher(paymentVoucher.id)"><i class="far fa-save"></i> {{ $t('PurchaseOrderPaymentView.Print') }}</button>
                <button type="button" class="btn btn-soft-secondary btn-sm" v-on:click="onCancel">{{ $t('PurchaseOrderPaymentView.Cancel') }}</button>
            </div>

            <bulk-attachment :attachmentList="paymentVoucher.attachmentList" @close="isAttachshow=false" :show="isAttachshow" v-if="isAttachshow" />
        </div>
    </modal>
</template>

<script>
    import clickMixin from '@/Mixins/clickMixin'
    import { required } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core';
    import moment from "moment";
    export default {
        mixins: [clickMixin],
        props: ['data', 'formName', 'show'],
        setup() {
            return { v$: useVuelidate() }
        },
        data: function () {
            return {
                ispayable: true,
                isAttachshow: false,
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
                type: '',
                isBank: true,
                voucherNumberRander: 0,
                language: 'Nothing',
                accountrender: 0,
                headerFooter: {
                    footerEn: '',
                    footerAr: '',
                    company: ''
                },
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
                this.isAttachshow = true;
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
            PrintPaymentVoucher: function (value) {
                this.GetHeaderDetail();
                var id = value;
                this.printId = id;
                this.printRender++;
            },
            UpdateStatusToVoid: function () {

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
                this.$emit('close');
            },
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


            if (this.formName == 'AdvanceReceipt') {
                if (this.data == undefined) {
                    this.GetAutoCodeGenerator(this.formName);
                    this.paymentVoucher.paymentVoucherType = this.formName;
                    this.paymentVoucher.contactAccountId = this.customerAccountId;
                    this.paymentVoucher.amount = this.totalAmount;
                }
                if (this.data != undefined) {

                    this.paymentVoucher = this.data.message;
                    this.isShow = false
                    this.attachment = true;
                    this.saleInvoiceRander++;
                    this.paymentVoucher.paymentVoucherType = 'AdvanceReceipt';
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

            if (this.formName == 'AdvancePay') {
                if (this.data == undefined) {
                    this.GetAutoCodeGenerator(this.formName);
                    this.paymentVoucher.paymentVoucherType = this.formName;
                    this.paymentVoucher.contactAccountId = this.customerAccountId;

                    this.paymentVoucher.amount = this.totalAmount;
                }

                if (this.data != undefined) {
                    this.paymentVoucher = this.data;
                    this.isShow = false
                    this.attachment = true;
                    this.purchaseInvoiceRander++
                    this.paymentVoucher.paymentVoucherType = 'AdvancePay';
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
            if (this.formName == 'AdvanceExpense') {
                if (this.data == undefined) {
                    this.GetAutoCodeGenerator(this.formName);
                    this.paymentVoucher.paymentVoucherType = this.formName;
                }
                if (this.data != undefined) {
                    this.paymentVoucher = this.data;
                    this.isShow = false
                    this.attachment = true;
                    this.purchaseInvoiceRander++
                    console.log(this.paymentVoucher);
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
