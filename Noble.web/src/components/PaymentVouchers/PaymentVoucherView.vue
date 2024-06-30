<template>
    <div class="row" v-if="(isValid('CanViewDetailPettyCash') && formName == 'PettyCash') || (isValid('CanViewDetailCPR') && formName == 'BankReceipt') || (isValid('CanViewDetailSPR') && formName == 'BankPay')">
        <div class="col-lg-12">
            <div class="row">

                <div class="col-lg-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">

                                <h4 v-if="formName=='BankReceipt'"> {{ $t('PaymentVoucherView.CustomerPayReceipt') }} -  {{ paymentVoucher.voucherNumber }}</h4>
                                <h4 v-if="formName=='BankPay'"> {{ $t('AddPaymentVoucher.SupplierPaymentReceipt')}} - {{ paymentVoucher.voucherNumber }}</h4>
                                <h4 v-if="formName=='PettyCash'"> {{ $t('AddPaymentVoucher.PettyCash')}} - {{ paymentVoucher.voucherNumber }}</h4>

                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('PaymentVoucherView.Home') }}</a></li>
                                    <li class="breadcrumb-item active" v-if=" formName=='BankReceipt'">{{ $t('PaymentVoucherView.CustomerPayReceipt') }}</li>
                                    <li class="breadcrumb-item active" v-if="formName=='BankPay'">{{$t('PaymentVoucherView.SupplierPaymentReceipt')}}</li>
                                    <li class="breadcrumb-item active" v-if="formName=='PettyCash'">{{ $t('PaymentVoucherView.PettyCash') }}</li>
                                </ol>
                            </div>
                            <div class="col-auto align-self-center ">

                                <h5 class="text-right"> {{paymentVoucher.dates}} </h5>


                            </div>
                        </div>
                    </div>
                    <div class="card col-sm-6 ">
                        <div class="card-body">

                            <div class="row">
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

                                </div>
                            </div>


                        </div>


                        <div class="card-footer">
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="button-items">
                                        <button class="btn btn-primary mr-2 " v-on:click="Attachment()">
                                            {{ $t('QuotationView.Attachment') }}
                                        </button>
                                        <button type="button" class="btn btn-outline-primary   " v-on:click="SaveVoucher('Draft')"><i class="far fa-save"></i>  {{ $t('AddPaymentVoucher.UpdateasDraft') }}</button>

                                        <button class="btn btn-danger  " v-on:click="onCancel">  {{ $t('PaymentVoucherView.Cancel') }}</button>

                                    </div>

                                </div>
                            </div>


                        </div>
                    </div>

                </div>



            </div>
        </div>
        <loading v-model:active="loading" :can-cancel="true" :is-full-page="true"></loading>
        <bulk-attachment :attachmentList="paymentVoucher.attachmentList" :show="show" v-if="show" @close="attachmentSave" />
    </div>
    <div v-else> <acessdenied></acessdenied></div>
</template>
<script>
import clickMixin from '@/Mixins/clickMixin'
import {required, minValue} from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core';
import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';

import moment from "moment";
export default {
    mixins: [clickMixin],
    
        props: ['formName'],
        components: {
            Loading
        },
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
                paymentVoucherType: '',
                amount: 0,
                approvalStatus: 'Draft',
                purchaseInvoice: '',
                saleInvoice: '',
                bankCashAccountId: '',
                contactAccountId: '',
                paymentMode: '',
                natureOfPayment: '',
                paymentMethod: '',
                userName: '',
                temporaryCashIssueId: '',
                attachmentList: [],
                paymentVoucherItems: [],
            },
            loading: false,
            type: '',
            isBank: true,
            voucherNumberRander: 0,
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
                    amount: {
                        minValue: minValue(0.01)
                    }
                }
                }
    },
    methods: {
        updatedailyExpenseRows: function (detail, amount) {
            this.paymentVoucher.paymentVoucherItems = [];
            this.paymentVoucher.paymentVoucherItems = detail;
            this.paymentVoucher.amount = amount;
            if (!this.isAging) {
                this.paymentVoucherItems = detail;

            }

            this.isPayment = false;

        },
        Attachment: function () {
            this.show = true;
        },

        attachmentSave: function (attachment) {
            this.paymentVoucher.attachmentList = attachment;
            this.show = false;
        },
        AddMultiPayment: function () {

            this.isPayment = true;
        },
        PaymentSave: function () {
            this.isPayment = false;
        },

        GetAccount: function (x) {

            if (x == 'السيولة النقدية' || x == 'Bank') {
                this.randerAccount++;

            } else if (x == 'مصرف' || x == 'Cash') {
                this.randerAccount++;
            } else if (x == 'يتقدم' || x == 'Advance') {
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
        getSaleNetAmount: function () {
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            var root = this
            var totalAmount = this.$refs.saleInvoiceDropdown.GetAmountOfSelected();
            this.$https.get('/Sale/GetRemainingInvoiceAmount?id=' + root.paymentVoucher.saleInvoice, {
                headers: {
                    "Authorization": `Bearer ${token}`
                }
            }).then(function (response) {
                if (response.data != null) {

                    root.paymentVoucher.amount = (parseFloat(totalAmount) - parseFloat(response.data)).toFixed(2);
                }
            });

        },
        getPurchaseNetAmount: function () {
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            var root = this
            var totalAmount = this.$refs.purchaseInvoiceDropdown.GetAmountOfSelected();
            this.$https.get('/Sale/GetRemainingInvoiceAmount?id=' + root.paymentVoucher.purchaseInvoice, {
                headers: {
                    "Authorization": `Bearer ${token}`
                }
            }).then(function (response) {
                if (response.data != null) {

                    root.paymentVoucher.amount = parseFloat(totalAmount) - parseFloat(response.data);
                }
            });
        },
        getData: function (x) {

            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            this.paymentVoucherItems = [];
            if (x == true) {
                if (this.formName == 'BankPay' || this.formName == 'BankPay') {

                    this.$https.get('/PurchasePost/PurchasePostList?isDropDown=' + true + '&supplierid=' + '00000000-0000-0000-0000-000000000000' + '&isExpense=' + true + '&supplierAccountId=' + this.paymentVoucher.contactAccountId + '&isService=' + root.isService, {
                        headers: {
                            "Authorization": `Bearer ${token}`
                        }
                    }).then(function (response) {

                        if (response.data != null) {
                            response.data.results.forEach(function (result) {

                                if (result != undefined) {
                                    root.paymentVoucherItems.push({
                                        purchaseInvoiceId: result.id,
                                        saleInvoiceId: null,
                                        amount: result.netAmount,
                                        payAmount: 0.00,
                                        isAging: root.isAging,
                                        isActive: root.isAging ? true : false,
                                        total: 0.00,
                                        extraAmount: 0.00,
                                        description: result.registrationNumber,
                                        dueAmount: result.netAmount - result.receivedAmount,
                                        receivedAmount: 0,
                                        partiallyInvoices: result.partiallyInvoices,
                                        date: moment(result.date).format('LLL')
                                    });
                                    root.isMultiPayment = true;

                                }

                            })

                            // root.product = response.data;

                            // response.data.results.forEach(function (results) {
                            //     root.options.push({
                            //         purchaseInvoiceId: results.id,
                            //         registrationNumber: results.registrationNumber + '/ ' + results.invoiceNo + ' ( ' + results.date + ')',
                            //         netAmount: results.netAmount

                            //     });

                            // })

                        }
                    })

                } else {
                    var url = '';

                    {
                        url = '/Sale/SaleList?status=' + 'Credit' + '&isDropdown=' + true + '&isService=' + root.isService + '&isExpense=' + true + '&contactId=' + this.paymentVoucher.contactAccountId
                    }

                    this.$https.get(url, {
                        headers: {
                            "Authorization": `Bearer ${token}`
                        }
                    }).then(function (response) {

                        if (response.data != null) {

                            response.data.results.sales.forEach(function (result) {

                                if (result != undefined) {
                                    root.paymentVoucherItems.push({
                                        saleInvoiceId: result.id,
                                        purchaseInvoiceId: null,
                                        amount: result.netAmount,
                                        payAmount: 0.00,
                                        isAging: root.isAging,
                                        isActive: root.isAging ? true : false,
                                        total: 0.00,
                                        extraAmount: 0.00,
                                        description: result.registrationNumber,
                                        dueAmount: result.netAmount - result.receivedAmount,
                                        receivedAmount: result.receivedAmount,
                                        partiallyInvoices: result.partiallyInvoices,
                                        date: moment(result.date).format('LLL')
                                    });
                                    root.isMultiPayment = true;

                                }

                            })

                        }

                    });

                }

            } else {
                this.paymentVoucherItems = this.paymentVoucher.paymentVoucherItems;

            }

        },
        enableInvoiceDropdown: function (x) {
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
            if (this.isValid('MultiplePayment')) {
                this.getData(x);

                // if (this.isValid('UnFollowAging')) {
                //     this.isAging = false;
                // }

            } else {
                if (this.paymentVoucher.saleInvoice === undefined || this.paymentVoucher.saleInvoice === null) {

                    this.paymentVoucher.saleInvoice = '00000000-0000-0000-0000-000000000000';
                }

                if (this.paymentVoucher.purchaseInvoice === undefined || this.paymentVoucher.purchaseInvoice === null) {

                    this.paymentVoucher.purchaseInvoice = '00000000-0000-0000-0000-000000000000';
                }

                if (this.formName == 'CashReceipt' || this.formName == 'BankReceipt') {
                    this.isShow = false
                    this.isMultiPayment = false
                    this.saleInvoiceRander++;
                } else if (this.formName == 'BankPay' || this.formName == 'CashPay') {
                    this.isShow = false
                    this.isMultiPayment = false
                    this.purchaseInvoiceRander++;
                }

            }

        },

        GetAutoCodeGenerator: function (value) {

            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            this.$https.get('/PaymentVoucher/AutoGenerateCode?paymentVoucherType=' + value, {
                headers: {
                    "Authorization": `Bearer ${token}`
                }
            }).then(function (response) {
                if (response.data != null) {

                    root.paymentVoucher.voucherNumber = response.data;
                    root.voucherNumberRander++;

                }
            });
        },

        SaveVoucher: function (x) {
            this.loading = true;
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
            this.paymentVoucher.isView =true;
            this.paymentVoucher.date = this.paymentVoucher.date + " " + moment().format("hh:mm A");

            this.$https.post('/PaymentVoucher/AddPaymentVoucher', this.paymentVoucher, {
                headers: {
                    "Authorization": `Bearer ${token}`
                }
            }).then(function (response) {
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
                            if (root.isTemporaryCashIssue) {
                                root.$router.push({
                                    path: '/TemporaryCashIssue',
                                })
                            } else {
                                if (root.ispayable) {
                                    root.$router.push({
                                        path: "/paymentVoucherList",
                                        query: {
                                            data: 'PaymentVoucherLists' + root.formName,
                                            formName: root.formName,
                                        }
                                    })
                                    // window.location.href = "/addPaymentVoucher?formName=" + root.formName;
                                }
                            }
                        }
                    });

                } else if (response.data.message.id != '00000000-0000-0000-0000-000000000000' && response.data.type == 'Edit') {
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
                                root.$router.push({
                                    path: '/paymentVoucherList',
                                    query: {
                                        data: 'PaymentVoucherLists' + root.formName,
                                        formName: root.formName,
                                    }
                                })
                                //    window.location.href = "/paymentVoucherList?formName=" + root.formName;
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
        getpaymentVoucherDetails: function (paymentVoucherItem) {

            this.paymentVoucher = paymentVoucherItem;
        },
        onCancel: function () {
            if (((this.isValid('CanViewPettyCash') || this.isValid('CanDraftPettyCash')) && this.formName == 'PettyCash') || ((this.isValid('CanViewCPR') || this.isValid('CanDraftCPR')) && this.formName == 'BankReceipt') || ((this.isValid('CanViewSPR') || this.isValid('CanDraftSPR')) && this.formName == 'BankPay')) {
                this.$router.push({
                    path: '/paymentVoucherList',
                    query: {
                        data: 'PaymentVoucherLists' + this.formName,
                        formName: this.formName,
                    }
                })
            }
            // else {
            //     this.$router.go();
            // }

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
                    this.paymentVoucher = this.$route.query.data;
                    this.paymentVoucher.paymentVoucherType = 'BankPay';
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

            if (this.formName == 'BankReceipt') {
                if (this.$route.query.data == undefined) {
                    this.GetAutoCodeGenerator(this.formName);
                    this.paymentVoucher.paymentVoucherType = this.formName;
                }
                if (this.$route.query.data != undefined) {
                    this.attachment = true;
                    this.paymentVoucher = this.$route.query.data.message;
                    this.GetBankOpeningBalance(this.paymentVoucher.bankCashAccountId);
                    this.enableInvoiceDropdown(false);
                    this.paymentVoucher.paymentVoucherType = 'BankReceipt';
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
        if (this.formName == 'BankPay') {
            if (this.$route.query.data == undefined) {
                this.GetAutoCodeGenerator(this.formName);
                this.paymentVoucher.paymentVoucherType = this.formName;
                if (localStorage.getItem('IsSupplierPayCredit') != 'true') {
                    this.paymentVoucher.paymentMode = (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Cash' : 'السيولة النقدية'
                } else {
                    this.paymentVoucher.paymentMode = (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Bank' : 'مصرف'
                }
            }
            if (this.$route.query.data != undefined) {

                if (this.$route.query.data.isTemporaryCashIssue) {
                    this.GetAutoCodeGenerator(this.formName);
                    this.isTemporaryCashIssue = this.$route.query.data.isTemporaryCashIssue;
                    this.paymentVoucher.temporaryCashIssueId = this.$route.query.data.id;
                    this.temporaryCashIssue = this.$route.query.data.amount - (this.$route.query.data.returnAmount + this.$route.query.data.voucherAmount);

                    //this.paymentVoucher = this.$route.query.data.message;
                    this.isShow = false
                    this.attachment = true;
                    this.paymentVoucher.bankCashAccountId = this.$route.query.data.temporaryCashAccountId;
                    this.GetBankOpeningBalance(this.paymentVoucher.bankCashAccountId);
                    this.enableInvoiceDropdown(false);
                    this.purchaseInvoiceRander++
                    this.paymentVoucher.paymentVoucherType = 'BankPay';
                    if (this.$i18n.locale == 'ar') {
                        this.paymentVoucher.paymentMode = 'السيولة النقدية';
                    }
                    if ((this.$i18n.locale == 'en' || this.isLeftToRight())) {
                        this.paymentVoucher.paymentMode = 'Cash';
                    }
                } else {
                    this.paymentVoucher = this.$route.query.data;
                    this.isShow = false
                    this.attachment = true;
                    this.GetBankOpeningBalance(this.paymentVoucher.bankCashAccountId);
                    this.enableInvoiceDropdown(false);
                    this.purchaseInvoiceRander++
                    this.paymentVoucher.paymentVoucherType = 'BankPay';
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
                this.paymentVoucher = this.$route.query.data;
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
        if (this.formName == 'BankReceipt') {
            if (this.$route.query.data == undefined) {
                this.GetAutoCodeGenerator(this.formName);
                this.paymentVoucher.paymentVoucherType = this.formName;

                if (localStorage.getItem('IsCustomerPayCredit') != 'true') {
                    this.paymentVoucher.paymentMode = (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Cash' : 'السيولة النقدية'
                } else {
                    this.paymentVoucher.paymentMode = (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Bank' : 'مصرف'
                }
            }
            if (this.$route.query.data != undefined) {

                this.paymentVoucher = this.$route.query.data;
                this.isShow = false
                this.GetBankOpeningBalance(this.paymentVoucher.bankCashAccountId);
                this.enableInvoiceDropdown(false);
                this.attachment = true;
                this.saleInvoiceRander++;
                this.paymentVoucher.paymentVoucherType = 'BankReceipt';
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
    },

    mounted: function () {
        this.isMultiPayment = false;

        this.language = this.$i18n.locale;

        this.render++;
    }
}
</script>
