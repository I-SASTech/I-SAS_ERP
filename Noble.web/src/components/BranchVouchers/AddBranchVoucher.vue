<template>
    <div class="row">
        <div class="col-lg-12">
            <div class="row">

                <div class="col-lg-12">
                    <div class="row">
                        <div class="col d-flex align-items-baseline">
                            <div class="media">
                                <span class="circle-singleline" style="background-color: #1761FD !important;margin:10px !important">BV</span>
                                <div class="media-body align-self-center ms-3">
                                    <h6 class="m-0 font-20">New Branch Voucher</h6>
                                    <div class="col d-flex ">
                                        <p class="text-muted mb-0" style="font-size:13px !important;">{{ paymentVoucher.voucherNumber }}</p>

                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-auto align-self-center">
                            <a v-on:click="GotoPage('/StartScreen')" href="javascript:void(0);" class="btn btn-sm btn-outline-danger">
                                {{ $t('Sale.Close') }}
                            </a>
                        </div>
                    </div>

                    <div class="card">
                        <div class="card-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="row">
                                        <div class="col-lg-4 col-md-4 col-sm-6">
                                            <label>{{ $t('AddPaymentVoucher.Date') }} :<span class="text-danger"> *</span></label>
                                            <div class="form-group" v-bind:class="{ 'has-danger': v$.paymentVoucher.date.$error}">
                                                <datepicker v-model="v$.paymentVoucher.date.$model"></datepicker>
                                                <span v-if="v$.paymentVoucher.date.$error" class="error">
                                                    <span v-if="!v$.paymentVoucher.date.required">{{formName}} {{ $t('AddPaymentVoucher.DateRequired') }}</span>
                                                </span>
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6">
                                            <label> Branch: <span class="text-danger"> *</span> </label>
                                            <div class="form-group" v-bind:class="{ 'has-danger': v$.paymentVoucher.contactAccountId.$error}">
                                                <accountdropdown @update:modelValue="enableInvoiceDropdown(true)" v-model="v$.paymentVoucher.contactAccountId.$model" :formName="'BranchVoucher'" :isPurchase="paymentVoucher.isSupplier"></accountdropdown>
                                                <span v-if="v$.paymentVoucher.contactAccountId.$error" class="error">
                                                    <span v-if="!v$.paymentVoucher.contactAccountId.required">{{formName}} {{ $t('AddPaymentVoucher.AccountRequired') }}</span>
                                                </span>
                                            </div>
                                        </div>


                                        <div class="col-lg-4 col-md-4 col-sm-6" >
                                            <label>
                                                {{ $t('AddPaymentVoucher.Amount') }} :
                                                <span class="text-danger"> *</span>
                                            </label>
                                            <div class="form-group">
                                                <my-currency-input v-model="paymentVoucher.amount" @update:modelValue="zeroPrice(paymentVoucher.amount)"></my-currency-input>
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6" v-if="formName=='PettyCash'">
                                            <label>
                                                {{ $t('AddPaymentVoucher.PaymentMode') }}:
                                                <span class="text-danger"> *</span>
                                            </label>

                                            <div class="form-group">

                                                <multiselect v-if="($i18n.locale == 'en' ||isLeftToRight()) " disabled v-model="paymentVoucher.paymentMode" @update:modelValue="GetAccount(paymentVoucher.paymentMode)" :options="['Cash', 'Bank']" :show-labels="false" placeholder="Select Mode Of Payment">
                                                </multiselect>
                                                <multiselect v-else disabled v-model="paymentVoucher.paymentMode" @update:modelValue="GetAccount(paymentVoucher.paymentMode)" :options="[ 'السيولة النقدية', 'مصرف']" :show-labels="false" placeholder="Select Mode Of Payment">
                                                </multiselect>

                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6" v-else>
                                            <label>
                                                {{ $t('AddPaymentVoucher.PaymentMode') }}:
                                                <span class="text-danger"> *</span>
                                            </label>

                                            <div class="form-group">

                                                <multiselect v-if="($i18n.locale == 'en' ||isLeftToRight()) " :disabled="isTemporaryCashIssue" v-model="paymentVoucher.paymentMode" @update:modelValue="GetAccount(paymentVoucher.paymentMode)" :options="['Cash', 'Bank','Advance']" :show-labels="false" placeholder="Select Type">
                                                </multiselect>
                                                <multiselect v-else :disabled="isTemporaryCashIssue" v-model="paymentVoucher.paymentMode" @update:modelValue="GetAccount(paymentVoucher.paymentMode)" :options="[ 'السيولة النقدية', 'مصرف','يتقدم']" :show-labels="false" v-bind:placeholder="$t('AddPaymentVoucher.SelectOption')">
                                                </multiselect>

                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6">
                                            <label>
                                                {{ $t('AddPaymentVoucher.PaymentType') }}:
                                                <span class="text-danger" v-if="paymentVoucher.paymentMode=='Cash' || paymentVoucher.paymentMode=='السيولة النقدية' "></span>
                                                <span class="text-danger" v-else> *</span>
                                            </label>

                                            <div class="form-group" v-if="paymentVoucher.paymentMode=='Cash' || paymentVoucher.paymentMode=='السيولة النقدية' ">

                                                <multiselect v-if="($i18n.locale == 'en' ||isLeftToRight()) " disabled v-model="paymentVoucher.paymentMethod" :options="['Cheque', 'Transfer','Deposit','Debit Card','Credit Card']" :show-labels="false" placeholder="Select Type">
                                                </multiselect>
                                                <multiselect v-else v-model="paymentVoucher.paymentMethod" disabled :options="[ 'بطاقة إئتمان','بطاقة ائتمان','التحقق من', 'تحويل','الوديعة']" :show-labels="false" v-bind:placeholder="$t('AddPaymentVoucher.SelectOption')">
                                                </multiselect>

                                            </div>
                                            <div class="form-group" v-else>

                                                <multiselect v-if="($i18n.locale == 'en' ||isLeftToRight()) " v-model="paymentVoucher.paymentMethod" :options="['Cheque', 'Transfer','Deposit','Debit Card','Credit Card']" :show-labels="false" placeholder="Select Type">
                                                </multiselect>
                                                <multiselect v-else v-model="paymentVoucher.paymentMethod" :options="[ 'بطاقة إئتمان','بطاقة ائتمان','التحقق من', 'تحويل','الوديعة']" :show-labels="false" v-bind:placeholder="$t('AddPaymentVoucher.SelectOption')">
                                                </multiselect>

                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6">
                                            <label v-if="paymentVoucher.paymentMode=='Cash' || paymentVoucher.paymentMode=='السيولة النقدية' ">
                                                {{ $t('AddPaymentVoucher.CashAccount') }}:
                                                <span class="text-danger"> *</span>

                                            </label>
                                            <label v-else-if="paymentVoucher.paymentMode=='Bank' || paymentVoucher.paymentMode=='مصرف' ">
                                                {{ $t('AddPaymentVoucher.BankAccount') }}
                                                <span class="text-danger"> *</span>

                                            </label>
                                            <label v-else-if="paymentVoucher.paymentMode=='Advance' || paymentVoucher.paymentMode=='يتقدم' ">
                                                {{ $t('AddPaymentVoucher.AdvanceAccount') }} :
                                                <span class="text-danger"> *</span>

                                            </label>
                                            <label v-else>
                                                {{ $t('AddPaymentVoucher.BankAccount') }}:
                                                <span class="text-danger"> *</span>

                                            </label>
                                            <div class="form-group" v-bind:class="{ 'has-danger': v$.paymentVoucher.bankCashAccountId.$error}" v-if="paymentVoucher.paymentMode=='Cash' || paymentVoucher.paymentMode=='السيولة النقدية' " v-bind:key="randerAccount">
                                                <accountdropdown v-model="v$.paymentVoucher.bankCashAccountId.$model" @update:modelValue="GetBankOpeningBalance(paymentVoucher.bankCashAccountId)" :formName="CashPay" :isPurchase="formName=='BankReceipt'?false:true" :disabled="isTemporaryCashIssue"></accountdropdown>
                                                <span v-if="v$.paymentVoucher.bankCashAccountId.$error" class="error">
                                                    <span v-if="!v$.paymentVoucher.bankCashAccountId.required">{{formName}} {{ $t('AddPaymentVoucher.AccountRequired') }}</span>
                                                </span>
                                            </div>
                                            <div class="form-group" v-bind:class="{ 'has-danger': v$.paymentVoucher.bankCashAccountId.$error}" v-else-if="paymentVoucher.paymentMode=='Bank' || paymentVoucher.paymentMode=='مصرف' " v-bind:key="randerAccount">
                                                <accountdropdown @update:modelValue="GetBankOpeningBalance(paymentVoucher.bankCashAccountId)" v-model="v$.paymentVoucher.bankCashAccountId.$model" :formName="BankPay" :isPurchase="formName=='BankReceipt'?false:true"></accountdropdown>
                                                <span v-if="v$.paymentVoucher.bankCashAccountId.$error" class="error">
                                                    <span v-if="!v$.paymentVoucher.bankCashAccountId.required">{{formName}} {{ $t('AddPaymentVoucher.AccountRequired') }}</span>
                                                </span>
                                            </div>
                                            <div class="form-group" v-bind:class="{ 'has-danger': v$.paymentVoucher.bankCashAccountId.$error}" v-else>
                                                <accountdropdown v-model="v$.paymentVoucher.bankCashAccountId.$model" @update:modelValue="GetBankOpeningBalance(paymentVoucher.bankCashAccountId)" :formName="BankPay" :isPurchase="formName=='BankReceipt'?false:true"></accountdropdown>
                                                <span v-if="v$.paymentVoucher.bankCashAccountId.$error" class="error">
                                                    <span v-if="!v$.paymentVoucher.bankCashAccountId.required">{{formName}} {{ $t('AddPaymentVoucher.AccountRequired') }}</span>
                                                </span>
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6" v-if="paymentVoucher.paymentMode=='Bank' || paymentVoucher.paymentMode=='مصرف' ">
                                            <label>
                                                {{ $t('AddPaymentVoucher.TransactionID') }}:
                                            </label>
                                            <div class="form-group">
                                                <input v-model="paymentVoucher.transactionId" class="form-control" type="text" />
                                            </div>

                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6">
                                            <label v-if="paymentVoucher.paymentMode=='Cash' || paymentVoucher.paymentMode=='السيولة النقدية' ">
                                                {{ $t('AddPaymentVoucher.CashAccountRunningBalance') }}:

                                            </label>
                                            <label v-else-if="paymentVoucher.paymentMode=='Bank' || paymentVoucher.paymentMode=='مصرف' ">
                                                {{ $t('AddPaymentVoucher.BankAccountRunningBalance') }}

                                            </label>
                                            <label v-else-if="paymentVoucher.paymentMode=='Advance' || paymentVoucher.paymentMode=='يتقدم' ">
                                                {{ $t('AddPaymentVoucher.AdvanceAccountRunningBalance') }} :

                                            </label>
                                            <label v-else>
                                                {{ $t('AddPaymentVoucher.BankAccountRunningBalance') }}

                                            </label>
                                            <div class="form-group">
                                                <input disabled v-model="runningBalanceCashBank" class="form-control" type="text" />
                                            </div>
                                        </div>


                                        <div class="col-lg-4 col-md-4 col-sm-6" v-if="paymentVoucher.paymentMethod=='Cheque' || paymentVoucher.paymentMethod=='التحقق من' ">
                                            <label>
                                                {{ $t('AddPaymentVoucher.ChequeNumber') }}
                                            </label>
                                            <div class="form-group">
                                                <input v-model="paymentVoucher.chequeNumber" class="form-control" type="text" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-12 mt-4 mb-5">
                                            <div class="card">
                                                <div class="card-body">
                                                    <div class="row">
                                                        <div class="col-lg-8" style="border-right: 1px solid #eee;">
                                                            <div class="form-group pe-3">
                                                                <label> {{ $t('AddPaymentVoucher.Narration') }} / {{$t('AddPaymentVoucher.Remarks')}}</label>
                                                                <textarea v-model="paymentVoucher.narration" rows="3" class="form-control" />
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-4">
                                                            <div class="form-group ps-3">
                                                                <div class="font-xs mb-1">{{ $t('AddPaymentVoucher.Attachment') }}</div>
                                                                <button v-on:click="Attachment()" type="button" class="btn btn-light btn-square btn-outline-dashed mb-1"><i class="fas fa-cloud-upload-alt"></i>  {{ $t('AddSaleOrder.Attachment') }} </button>
                                                                <div>
                                                                    <small class="text-muted">
                                                                        {{ $t('AddPaymentVoucher.FileSize') }}
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
                            </div>
                        </div>

                        <div class="col-lg-12 invoice-btn-fixed-bottom">
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="button-items" v-if="paymentVoucher.id=='00000000-0000-0000-0000-000000000000'">
                                        <button type="button" class="btn btn-outline-primary" v-bind:disabled="v$.paymentVoucher.$invalid || (isTemporaryCashIssue? (temporaryCashIssue < paymentVoucher.amount):false)" v-on:click="SaveVoucher('Approved')"><i class="far fa-save"></i> {{ $t('AddPaymentVoucher.SaveandPost') }}</button>
                                        <button type="button" class="btn btn-outline-primary" v-bind:disabled="v$.paymentVoucher.$invalid" v-on:click="SaveVoucher('Draft')"><i class="far fa-save"></i>  {{ $t('AddPaymentVoucher.SaveasDraft') }}</button>
                                        <button class="btn btn-danger  " v-on:click="onCancel">  {{ $t('AddPaymentVoucher.Cancel') }}</button>
                                    </div>
                                    <div v-else class="button-items">
                                        <div>
                                            <button type="button" class="btn btn-outline-primary" v-on:click="SaveVoucher('Approved')"><i class="far fa-save"></i> {{ $t('AddPaymentVoucher.UpdateandPost')}} </button>
                                            <button type="button" class="btn btn-outline-primary" v-on:click="SaveVoucher('Draft')"><i class="far fa-save"></i>  {{ $t('AddPaymentVoucher.UpdateasDraft') }}</button>
                                            <button class="btn btn-danger  " v-on:click="onCancel">  {{ $t('AddPaymentVoucher.Cancel') }}</button>

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <bulk-attachment :attachmentList="paymentVoucher.attachmentList" :show="show" v-if="show" @close="attachmentSave" />
    </div>
    <!--<div v-else> <acessdenied></acessdenied></div>-->
</template>

<script>
    import clickMixin from '@/Mixins/clickMixin'
    import { required, minValue } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core';
    import Multiselect from 'vue-multiselect'

    import moment from "moment";
    export default {
        mixins: [clickMixin],
        components: {
            Multiselect,
        },
        props: ['formName'],
             setup() {
            return { v$: useVuelidate() }
        },
        data: function () {
            return {
                temporaryCashIssue: 0,
                isTemporaryCashIssue: false,
                isAging: false,
                currency: '',
                isService: false,
                ispayable: true,
                render: 0,
                saleInvoiceRander: 0,
                purchaseInvoiceRander: 0,
                isShow: true,
                isMultiPayment: false,
                isPayment: false,
                attachment: false,
                paymentVoucherItems: [],
                paymentVoucher: {
                    id: '00000000-0000-0000-0000-000000000000',
                    date: '',
                    voucherNumber: '',
                    transactionId: '',
                    chequeNumber: '',
                    narration: '',
                    amount: 0,
                    approvalStatus: 'Draft',
                    bankCashAccountId: '',
                    contactAccountId: '',
                    paymentMode: '',
                    natureOfPayment: '',
                    paymentMethod: '',
                    userName: '',
                    attachmentList: [],
                    isSupplier: false,
                    branchId: '',
                },
                loading: false,
                type: '',
                isBank: true,
                language: 'Nothing',
                CashPay: 'CashPay',
                BankPay: 'BankPay',
                randerAccount: 0,
                disable: false,
                show: false,
                runningBalance: 0,
                runningBalanceCashBank: 0,
            }
        },

        validations() {
          return{
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
                amount: {
                    minValue: minValue(0.01)
                }
            }
          }
        },
        methods: {
            Attachment: function () {
                this.show = true;
            },

            attachmentSave: function (attachment) {
                this.paymentVoucher.attachmentList = attachment;
                this.show = false;
            },

            GetAccount: function (x) {
                if (x == 'السيولة النقدية' || x == 'Bank') {
                    this.randerAccount++;
                }
                else if (x == 'مصرف' || x == 'Cash') {
                    this.randerAccount++;
                }
                else if (x == 'يتقدم' || x == 'Advance') {
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
                root.$https.get('/Contact/DownloadFile?filePath=' + path, {
                    headers: {
                        "Authorization": `Bearer ${token}`
                    },
                    responseType: 'blob'
                })
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
                root.$https.post('/Company/UploadFilesAsync', fileData, {
                    headers: {
                        "Authorization": `Bearer ${token}`
                    }
                })
                    .then(function (response) {

                        if (response.data != null) {

                            root.paymentVoucher.path = response.data;

                        }
                    },
                        function () {
                            root.loading = false;
                            root.$swal({
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                                text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Something Went Wrong!' : 'هل هناك خطب ما!',
                                type: 'error',
                                confirmButtonClass: "btn btn-danger",
                                buttonsStyling: false
                            });
                        });
            },

            zeroPrice: function (x) {

                if (x == 0) {
                    this.disable = true;

                } else {
                    this.disable = false;
                }

            },
            GetBankOpeningBalance: function (id) {

                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                var root = this
                this.$https.get('/Contact/GetCustomerRunningBalance?id=' + id, {
                    headers: {
                        "Authorization": `Bearer ${token}`
                    }
                }).then(function (response) {
                    if (response.data != null) {

                        root.runningBalanceCashBank = parseFloat(response.data) >= 0 ? 'Dr ' + parseFloat(response.data) * +1 : 'Cr ' + parseFloat(response.data) * (-1);
                    }
                });

            },
            UpdateStatus: function (status) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                this.$https.post('/PaymentVoucher/UpdateStatusPaymentVoucher?id=' + this.paymentVoucher.id + '&approvalStatus=' + status, {
                    headers: {
                        "Authorization": `Bearer ${token}`
                    }
                }).then(function (response) {

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

                    } else if (response.data.message.id == '00000000-0000-0000-0000-000000000000') {
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
                }).catch(error => {

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

            enableInvoiceDropdown: function () {
                this.isMultiPayment = false;

                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                var root = this
                this.$https.get('/Contact/GetCustomerRunningBalance?id=' + root.paymentVoucher.contactAccountId, {
                    headers: {
                        "Authorization": `Bearer ${token}`
                    }
                }).then(function (response) {
                    if (response.data != null) {

                        root.runningBalance = parseFloat(response.data) >= 0 ? 'Dr ' + parseFloat(response.data) * +1 : 'Cr ' + parseFloat(response.data) * (-1);
                    }
                });
            },

            GetAutoCodeGenerator: function () {

                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.$https.get('/PaymentVoucher/BranchVoucherCode?branchId=' + localStorage.getItem('BranchId'), {
                    headers: {
                        "Authorization": `Bearer ${token}`
                    }
                }).then(function (response) {
                    if (response.data != null) {

                        root.paymentVoucher.voucherNumber = response.data;

                    }
                });
            },

            SaveVoucher: function (x) {

                localStorage.setItem('active', x);

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
                    } else if (this.paymentVoucher.paymentMethod == 'تحويل') {
                        this.paymentVoucher.paymentMethod = 2;
                    } else if (this.paymentVoucher.paymentMethod == 'الوديعة') {
                        this.paymentVoucher.paymentMethod = 3;
                    } else {
                        this.paymentVoucher.paymentMethod = 0;
                    }

                    if (this.paymentVoucher.paymentMode == 'السيولة النقدية') {
                        this.paymentVoucher.paymentMode = 0;
                    }
                    if (this.paymentVoucher.paymentMode == 'مصرف') {
                        this.paymentVoucher.paymentMode = 1;
                    }
                    if (this.paymentVoucher.paymentMode == 'يتقدم') {
                        this.paymentVoucher.paymentMode = 5;
                    }

                }
                if ((this.$i18n.locale == 'en' || this.isLeftToRight())) {
                    if (this.paymentVoucher.paymentMethod == 'Cheque') {
                        this.paymentVoucher.paymentMethod = 1;
                    } else if (this.paymentVoucher.paymentMethod == 'Transfer') {
                        this.paymentVoucher.paymentMethod = 2;
                    } else if (this.paymentVoucher.paymentMethod == 'Deposit') {
                        this.paymentVoucher.paymentMethod = 3;
                    } else if (this.paymentVoucher.paymentMethod == 'Debit Card') {
                        this.paymentVoucher.paymentMethod = 4;
                    } else if (this.paymentVoucher.paymentMethod == 'Credit Card') {
                        this.paymentVoucher.paymentMethod = 5;
                    } else {
                        this.paymentVoucher.paymentMethod = 0;
                    }
                    if (this.paymentVoucher.paymentMode == 'Cash') {
                        this.paymentVoucher.paymentMode = 0;
                    }
                    if (this.paymentVoucher.paymentMode == 'Bank') {
                        this.paymentVoucher.paymentMode = 1;
                    }
                    if (this.paymentVoucher.paymentMode == 'Advance') {
                        this.paymentVoucher.paymentMode = 5;
                    }

                }
                if (this.paymentVoucher.paymentMethod != 1) {
                    this.paymentVoucher.chequeNumber = '';
                }
                var root = this;
                var token = '';
                this.paymentVoucher.approvalStatus = x;
                this.paymentVoucher.userName = localStorage.getItem('LoginUserName');

                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.paymentVoucher.date = this.paymentVoucher.date + " " + moment().format("hh:mm A");
                this.paymentVoucher.branchId = localStorage.getItem('BranchId');


                this.$https.post('/PaymentVoucher/SaveBranchVoucher', this.paymentVoucher, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                    
                    if (root.paymentVoucher.id == '00000000-0000-0000-0000-000000000000') {

                        root.$swal({
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved!' : '!تم الحفظ',
                            text: response.data.isAddUpdate,
                            type: 'success',
                            icon: 'success',
                            showConfirmButton: false,
                            timer: 2000,
                            timerProgressBar: true,
                            confirmButtonClass: "btn btn-success",
                            buttonsStyling: false
                        }).then(function (result) {
                            if (result) {
                                root.$router.push({
                                    path: "/BranchVouchers",
                                    query: {
                                        data: 'BranchVouchers' + root.formName,
                                        formName: root.formName,
                                    }
                                })
                            }
                        });

                    }
                    else if (root.paymentVoucher.id != '00000000-0000-0000-0000-000000000000') {
                        root.$swal({
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Update!' : 'تم التحديث!',
                            text: response.data.isAddUpdate,
                            type: 'success',
                            icon: 'success',
                            showConfirmButton: false,
                            timer: 2000,
                            timerProgressBar: true,
                            confirmButtonClass: "btn btn-success",
                            buttonsStyling: false
                        }).then(function (result) {
                            if (result) {
                                root.$router.push({
                                    path: '/paymentVoucherList',
                                    query: {
                                        data: 'PaymentVoucherLists' + root.formName,
                                        formName: root.formName,
                                    }
                                })
                            }
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
                }).catch(error => {

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


            onCancel: function () {
                this.$router.push({
                    path: '/BranchVouchers',
                    query: {
                        data: 'BranchVouchers' + this.formName,
                        formName: this.formName,
                    }
                })
            },
        },
        watch: {
            formName: function () {
                if (this.formName == 'BankReceipt') {
                    if (this.$route.query.data == undefined) {
                        this.GetAutoCodeGenerator(this.formName);
                    }
                    if (this.$route.query.data != undefined) {
                        this.attachment = true;
                        this.paymentVoucher = this.$route.query.data.message;
                        this.GetBankOpeningBalance(this.paymentVoucher.bankCashAccountId);
                        this.enableInvoiceDropdown(false);
                        this.paymentVoucherDetails = this.$route.query.data.message.paymentVoucherDetails;
                        if (this.$i18n.locale == 'ar') {
                            if (this.paymentVoucher.paymentMethod == 1) {
                                this.paymentVoucher.paymentMethod = 'التحقق من';
                            } else if (this.paymentVoucher.paymentMethod == 2) {
                                this.paymentVoucher.paymentMethod = 'تحويل';
                            } else if (this.paymentVoucher.paymentMethod == 3) {
                                this.paymentVoucher.paymentMethod = 'الوديعة';
                            } else {
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
                            } else if (this.paymentVoucher.paymentMethod == 2) {
                                this.paymentVoucher.paymentMethod = 'Transfer';
                            } else if (this.paymentVoucher.paymentMethod == 3) {
                                this.paymentVoucher.paymentMethod = 'Deposit';
                            } else if (this.paymentVoucher.paymentMethod == 4) {
                                this.paymentVoucher.paymentMethod = 'Debit Card';
                            } else if (this.paymentVoucher.paymentMethod == 5) {
                                this.paymentVoucher.paymentMethod = 'Credit Card';
                            } else {
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

        created: function () {
            this.paymentVoucher.date = moment().format("DD MMM YYYY");
            this.currency = localStorage.getItem('currency');

            this.$emit('update:modelValue', this.$route.name + this.formName);
            this.isService = localStorage.getItem('IsService') == 'true' ? true : false;
            this.paymentVoucher.isSupplier = this.formName == 'IsSupplier' ? true : false;

            if (this.$route.query.data == undefined) {
                this.GetAutoCodeGenerator();
                this.paymentVoucher.paymentMode = (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Cash' : 'السيولة النقدية';
            }

            if (this.$route.query.data != undefined) {

                this.paymentVoucher = this.$route.query.data;
                this.isShow = false

                this.GetBankOpeningBalance(this.paymentVoucher.bankCashAccountId);
                this.enableInvoiceDropdown(false);
                this.attachment = true;
                this.saleInvoiceRander++;
                if (this.$i18n.locale == 'ar') {
                    if (this.paymentVoucher.paymentMethod == 1) {
                        this.paymentVoucher.paymentMethod = 'التحقق من';
                    } else if (this.paymentVoucher.paymentMethod == 2) {
                        this.paymentVoucher.paymentMethod = 'تحويل';
                    } else if (this.paymentVoucher.paymentMethod == 3) {
                        this.paymentVoucher.paymentMethod = 'الوديعة';
                    } else {
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
                    } else if (this.paymentVoucher.paymentMethod == 2) {
                        this.paymentVoucher.paymentMethod = 'Transfer';
                    } else if (this.paymentVoucher.paymentMethod == 3) {
                        this.paymentVoucher.paymentMethod = 'Deposit';
                    } else if (this.paymentVoucher.paymentMethod == 4) {
                        this.paymentVoucher.paymentMethod = 'Debit Card';
                    } else if (this.paymentVoucher.paymentMethod == 5) {
                        this.paymentVoucher.paymentMethod = 'Credit Card';
                    } else {
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
        },

        mounted: function () {
            this.isMultiPayment = false;

            this.language = this.$i18n.locale;

            this.render++;
        }
    }
</script>

<style scoped>
    .badge-icon {
        border-radius: 50%;
        background-color: red;
        color: white;
    }

    .bg-success {
        background-color: #3c873c !important;
    }

    .filter-green {
        filter: invert(17%) sepia(80%) saturate(6562%) hue-rotate(357deg) brightness(98%) contrast(117%);
        opacity: 1 !important;
    }

    .full_size {
        position: absolute;
        top: 0;
        left: 22px;
        width: 100%;
        height: 100%;
        display: block;
        z-index: 9;
        font-size: 0;
    }

    .circle-singleline {
        margin: 20px;
        width: 60px;
        height: 60px;
        border-radius: 50%;
        font-size: 36px;
        text-align: center;
        background: blue;
        color: #fff;
    }

    .custom_code::after {
        background: purple !important;
    }
</style>
