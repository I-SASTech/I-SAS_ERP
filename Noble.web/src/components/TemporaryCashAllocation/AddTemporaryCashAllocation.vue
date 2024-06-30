<template>
    <div v-if=" isValid('CanAddTCAllocation') || isValid('CanDraftTCAllocation')|| isValid('CanEditTCAllocation') ">
        <div class="row">
            <div class="col-md-12 ">
                <div class="page-title-box">
                    <div class="row">
                        <div class="col">
                            <h4 class="page-title">
                              
                                <span v-if="paymentVoucher.id != '00000000-0000-0000-0000-000000000000'">{{ $t('AddTemporaryCashAllocation.UpdateTemporaryCashAllocation')}} <span style="font-weight:bold">  - {{ paymentVoucher.voucherNumber }}</span></span>
                                <span v-if="paymentVoucher.id == '00000000-0000-0000-0000-000000000000'">{{ $t('AddTemporaryCashAllocation.AddTemporaryCashAllocation')}}  <span style="font-weight:bold">  - {{ paymentVoucher.voucherNumber }}</span></span>

                            </h4>
                        </div>
                    </div>
                </div>
                <hr class="hr-dashed hr-menu mt-0" />
                <div>
                    <div>
                        <div class="row">
                            <div class="col-lg-6">
                                <div class="row form-group" v-bind:class="{ 'has-danger': v$.paymentVoucher.date.$error}">
                                    <label class="col-form-label col-lg-4">
                                        <span class="tooltip-container text-dashed-underline "> {{ $t('AddTemporaryCashAllocation.Date')}} : <span class="text-danger">*</span></span>
                                    </label>
                                    <div class="inline-fields col-lg-8">

                                        <datepicker v-model="v$.paymentVoucher.date.$model"></datepicker>
                                    </div>
                                </div>
                                <div class="row form-group">
                                    <label class="col-form-label col-lg-4">
                                        <span class="tooltip-container text-dashed-underline ">  {{ $t('AddPaymentVoucher.PaymentType') }} : <span class="text-danger">*</span></span>
                                    </label>
                                    <div class="inline-fields col-lg-8">
                                        <multiselect v-if="($i18n.locale == 'en' ||isLeftToRight())" v-model="paymentVoucher.paymentMethod" :options="['Cheque', 'Transfer','Deposit']" :show-labels="false" placeholder="Select Type">
                                        </multiselect>
                                        <multiselect v-else v-model="paymentVoucher.paymentMethod" :options="[ 'التحقق من', 'تحويل','الوديعة']" :show-labels="false" v-bind:placeholder="$t('AddPaymentVoucher.SelectOption')">
                                        </multiselect>
                                    </div>
                                </div>
                                <div class="row form-group">

                                    <label v-if="paymentVoucher.paymentMode=='Cash' || paymentVoucher.paymentMode=='السيولة النقدية' " class="col-form-label col-lg-4">
                                        <span class="tooltip-container text-dashed-underline ">{{ $t('AddPaymentVoucher.CashAccount') }} : <span class="text-danger">*</span></span>
                                    </label>
                                    <label v-else-if="paymentVoucher.paymentMode=='Bank' || paymentVoucher.paymentMode=='مصرف' " class="col-form-label col-lg-4">
                                        <span class="tooltip-container text-dashed-underline "> {{ $t('AddPaymentVoucher.BankAccount') }} : <span class="text-danger">*</span></span>
                                    </label>
                                    <label class="col-form-label col-lg-4" v-else>
                                        <span class="tooltip-container text-dashed-underline "> {{ $t('AddPaymentVoucher.BankAccount') }} : <span class="text-danger">*</span></span>
                                    </label>
                                    <div class="inline-fields col-lg-8" v-bind:class="{ 'has-danger': v$.paymentVoucher.bankCashAccountId.$error}" v-if="paymentVoucher.paymentMode=='Cash' || paymentVoucher.paymentMode=='السيولة النقدية' " v-bind:key="randerAccount">
                                        <accountdropdown v-model="v$.paymentVoucher.bankCashAccountId.$model" @update:modelValue="GetBankOpeningBalance(paymentVoucher.bankCashAccountId)" :formName="'CashReceipt'" />
                                    </div>
                                    <div class="inline-fields col-lg-8" v-bind:class="{ 'has-danger': v$.paymentVoucher.bankCashAccountId.$error}" v-else-if="paymentVoucher.paymentMode=='Bank' || paymentVoucher.paymentMode=='مصرف' " v-bind:key="randerAccount">
                                        <accountdropdown @update:modelValue="GetBankOpeningBalance(paymentVoucher.bankCashAccountId)" v-model="v$.paymentVoucher.bankCashAccountId.$model" :formName="'BankReceipt'" />
                                    </div>
                                    <div class="inline-fields col-lg-8" v-bind:class="{ 'has-danger': v$.paymentVoucher.bankCashAccountId.$error}" v-else v-bind:key="randerAccount">
                                        <accountdropdown v-model="v$.paymentVoucher.bankCashAccountId.$model" @update:modelValue="GetBankOpeningBalance(paymentVoucher.bankCashAccountId)" :formName="'CashReceipt'" />
                                    </div>
                                </div>
                                <div class="row form-group">
                                    <label class="col-form-label col-lg-4">
                                        <span class="tooltip-container text-dashed-underline ">{{ $t('AddTemporaryCashRequest.Employee') }} : <span class="text-danger">*</span></span>
                                    </label>
                                    <div class="inline-fields col-lg-8">
                                        <employeeDropdown v-model="paymentVoucher.userEmployeeId" :modelValue="paymentVoucher.userEmployeeId" :temporaryCashAllocation="true" />
                                    </div>
                                </div>
                                    <div class="row form-group">
                                        <label class="col-form-label col-lg-4">
                                            <span class="tooltip-container text-dashed-underline ">  {{ $t('AddPaymentVoucher.Amount') }} : <span class="text-danger">*</span></span>
                                        </label>
                                        <div class="inline-fields col-lg-8">
                                            <my-currency-input v-model="paymentVoucher.amount" />
                                        </div>
                                    </div>

                                </div>
                                <div class="col-lg-6">
                                    <div class="row form-group">
                                        <label class="col-form-label col-lg-4">
                                            <span class="tooltip-container text-dashed-underline ">   {{ $t('AddPaymentVoucher.PaymentMode') }} : <span class="text-danger">*</span></span>
                                        </label>
                                        <div class="inline-fields col-lg-8">
                                            <multiselect v-if="($i18n.locale == 'en' ||isLeftToRight()) " v-model="paymentVoucher.paymentMode" :allow-empty="false" @update:modelValue="GetAccount(paymentVoucher.paymentMode)" :options="['Cash', 'Bank']" :show-labels="false" placeholder="Select Type">
                                            </multiselect>
                                            <multiselect v-else v-model="paymentVoucher.paymentMode" :allow-empty="false" @update:modelValue="GetAccount(paymentVoucher.paymentMode)" :options="[ 'السيولة النقدية', 'مصرف']" :show-labels="false" v-bind:placeholder="$t('AddPaymentVoucher.SelectOption')">
                                            </multiselect>
                                        </div>
                                    </div>
                                    <div class="row form-group">
                                        <label class="col-form-label col-lg-4">
                                            <span class="tooltip-container text-dashed-underline "> {{ $t('AddPaymentVoucher.ChequeNumber') }} : <span class="text-danger">*</span></span>
                                        </label>
                                        <div class="inline-fields col-lg-8">
                                            <input class="form-control" disabled v-model="paymentVoucher.chequeNumber" />
                                        </div>
                                    </div>
                                    <div class="row form-group">
                                        <label class="col-form-label col-lg-4" v-if="paymentVoucher.paymentMode=='Cash' || paymentVoucher.paymentMode=='السيولة النقدية'">
                                            <span class="tooltip-container text-dashed-underline "> {{ $t('AddTemporaryCashAllocation.CashAccountRunningBalance')}}  <span class="text-danger">*</span></span>
                                        </label>
                                        <label class="col-form-label col-lg-4" v-else-if="paymentVoucher.paymentMode=='Bank' || paymentVoucher.paymentMode=='مصرف' ">
                                            <span class="tooltip-container text-dashed-underline "> {{ $t('AddTemporaryCashAllocation.CashAccountRunningBalance')}} <span class="text-danger">*</span></span>
                                        </label>
                                        <label class="col-form-label col-lg-4" v-else>
                                            <span class="tooltip-container text-dashed-underline "> {{ $t('AddTemporaryCashAllocation.CashAccountRunningBalance')}}  <span class="text-danger">*</span></span>
                                        </label>
                                        <div class="inline-fields col-lg-8">
                                            <input disabled v-model="runningBalanceCashBank" class="form-control" type="text" />

                                        </div>
                                    </div>
                                    <div class="row form-group">
                                        <label class="col-form-label col-lg-4">
                                            <span class="tooltip-container text-dashed-underline "> {{ $t('AddPaymentVoucher.RunningBalance') }} : <span class="text-danger">*</span></span>
                                        </label>
                                        <div class="inline-fields col-lg-8">
                                            <input disabled v-model="runningBalance" class="form-control" type="text" />
                                        </div>
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
                                                    <label> {{$t('AddPaymentVoucher.Narration') }} / {{$t('AddPaymentVoucher.Remarks')}}:</label>
                                                    <div>
                                                        <textarea class="form-control " rows="3" autofocus="autofocus"  v-model="paymentVoucher.narration" />
                                                    </div>

                                                </div>
                                            </div>
                                            <div class="col-lg-4" v-if="paymentVoucher.id=='00000000-0000-0000-0000-000000000000'">
                                                <div class="form-group ps-3">
                                                    <div class="font-xs mb-1">{{ $t('AddTemporaryCashAllocation.AttachFile')}} </div>

                                                    <button v-on:click="Attachment()" type="button" class="btn btn-light btn-square btn-outline-dashed mb-1"><i class="fas fa-cloud-upload-alt"></i>   {{ $t('AddSaleOrder.Attachment') }} </button>

                                                    <div>
                                                        <small class="text-muted">
                                                            {{ $t('AddTemporaryCashAllocation.FileSize')}}
                                                        </small>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-4" v-else>
                                                <div class="form-group ps-3">
                                                    <div class="font-xs mb-1">{{ $t('AddTemporaryCashAllocation.AttachFile')}} </div>

                                                    <button v-on:click="Attachment()" type="button" class="btn btn-light btn-square btn-outline-dashed mb-1"><i class="fas fa-cloud-upload-alt"></i> {{ $t('AddSaleOrder.Attachment') }} </button>

                                                    <div>
                                                        <small class="text-muted">
                                                            {{ $t('AddTemporaryCashAllocation.FileSize')}}
                                                        </small>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-12 col-md-12 col-sm-12 invoice-btn-fixed-bottom">
                            <div class="button-items">
                                <div v-if="paymentVoucher.id=='00000000-0000-0000-0000-000000000000'">

                                    <button class="btn btn-outline-primary  mr-2 " v-if="isValid('CanAddTCAllocation')" v-bind:disabled="v$.paymentVoucher.$invalid" v-on:click="SaveVoucher('Approved')"><i class="far fa-save"></i>   {{ $t('AddPaymentVoucher.SaveandPost') }}</button>

                                    <button class="btn btn-outline-primary  mr-2 " v-if="isValid('CanDraftTCAllocation')" v-bind:disabled="v$.paymentVoucher.$invalid" v-on:click="SaveVoucher('Draft')"> <i class="far fa-save"></i>   {{ $t('AddPaymentVoucher.SaveasDraft') }}</button>

                                    <button class="btn btn-danger " v-on:click="goToPurchase"> {{ $t('AddPaymentVoucher.Cancel') }}</button>

                                </div>
                                <div v-else>
                                    <button class="btn btn-outline-primary  mr-2 " v-if="isValid('CanDraftTCAllocation')" v-on:click="SaveVoucher('Rejected')"> <i class="far fa-save"></i>{{ $t('AddPaymentVoucher.SaveasReject') }}</button>
                                    <button class="btn btn-outline-primary  mr-2 " v-if="isValid('CanAddTCAllocation')" v-on:click="SaveVoucher('Approved')"> <i class="far fa-save"></i>{{ $t('AddPaymentVoucher.SaveandPost') }}</button>
                                    <button class="btn btn-outline-primary  mr-2 " v-if="isValid('CanDraftTCAllocation')" v-on:click="SaveVoucher('Draft')"><i class="far fa-save"></i>  {{ $t('AddPaymentVoucher.UpdateasDraft') }}</button>
                                    <button class="btn btn-danger " v-on:click="goToPurchase">{{ $t('AddPaymentVoucher.Cancel') }}</button>

                                </div>
                            </div>
                           

                        </div>
                    </div>

                </div>
            </div>
            <loading v-model:active="loading" :can-cancel="true" :is-full-page="true"></loading>
            <bulk-attachment :attachmentList="paymentVoucher.attachmentList" :show="show" v-if="show" @close="attachmentSave" />
        </div>
    </div>
    <div v-else> <acessdenied></acessdenied></div>

</template>



<script>
    import clickMixin from '@/Mixins/clickMixin'
    import { required, minValue } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core';
    import Multiselect from 'vue-multiselect'
    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    import moment from "moment";
    export default {
        mixins: [clickMixin],
        components: {
            Multiselect,
            Loading
        },
        props: ['formName'],
        setup() {
            return { v$: useVuelidate() }
        },
        data: function () {
            return {
                ispayable: true,
                render: 0,
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
                    paymentVoucherType: 'TemporaryCashAllocation',
                    amount: 0,
                    approvalStatus: 'Draft',
                    bankCashAccountId: '',
                    contactAccountId: '',
                    userEmployeeId: '',
                    paymentMode: '',
                    paymentMethod: '',
                    userName: '',
                    attachmentList: [],
                    branchId:''
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
                    userEmployeeId: {
                        required
                    },
                    amount: { minValue: minValue(1) }
                }
                }
        },
        methods: {
            goToPurchase: function () {
                if (this.isValid('CanViewTCIssue') || this.isValid('CanDraftTCIssue')) {
                    this.$router.push({
                        path: '/TemporaryCashAllocation',
                    })
                }
                else {
                    this.$router.go()
                }
            },
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

            //LimitCheck: function (amount) {

            //    var root = this;
            //    var token = '';
            //    if (token == '') {
            //        token = localStorage.getItem('token');
            //    }
            //    root.$https.get('/EmployeeRegistration/EmployeeDetail?Id=' + this.paymentVoucher.userEmployeeId, { headers: { "Authorization": `Bearer ${token}` } })
            //        .then(function (response) {
            //            if (response.data != null) {
            //
            //                if (amount > response.data.limit) {
            //                    root.$swal.fire(
            //                        {
            //                            icon: 'warning',
            //                            title: 'Amount Exceed The Limit!',
            //                            text: 'Your Limit Is ' + response.data.limit,
            //                            showConfirmButton: false,
            //                            timer: 5000,
            //                            timerProgressBar: true,
            //                        });
            //                    root.paymentVoucher.amount = response.data.limit;
            //                }
            //            }
            //        },
            //            function (error) {
            //                this.loading = false;
            //                console.log(error);
            //            });

            //},
            GetBankOpeningBalance: function (id) {

                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                var root = this
                this.$https.get('/Contact/GetCustomerRunningBalance?id=' + id, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {

                        root.runningBalanceCashBank = parseFloat(response.data) >= 0 ? 'Dr ' + parseFloat(response.data) * +1 : 'Cr ' + parseFloat(response.data) * (-1);
                    }
                });

            },
            languageChange: function (lan) {
                if (this.language == lan) {

                    if (this.paymentVoucher.id == '00000000-0000-0000-0000-000000000000') {

                        var getLocale = this.$i18n.locale;
                        this.language = getLocale;

                        this.$router.go('/AddTemporaryCashAllocation');
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

            GetAutoCodeGenerator: function () {

                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.$https.get('/PaymentVoucher/TemporaryCashAllocationCode', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
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

                var root = this;
                this.paymentVoucher.approvalStatus = x;
                this.paymentVoucher.userName = localStorage.getItem('LoginUserName');
                this.paymentVoucher.date = this.paymentVoucher.date + " " + moment().format("hh:mm A");

                this.$https.post('/PaymentVoucher/AddTemporaryCashAllocation', this.paymentVoucher, { headers: { "Authorization": `Bearer ${localStorage.getItem('token')}` } }).then(function (response) {
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
                                    window.location.href = "/AddTemporaryCashAllocation";
                                }
                            }
                        });

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
                        }).then(function (result) {
                            if (result) {

                                if (root.ispayable) {
                                    root.$router.push({
                                        path: '/TemporaryCashAllocation',
                                        formName: root.formName
                                    })
                                    //    window.location.href = "/paymentVoucherList?=" + ;
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
            getpaymentVoucherDetails: function (paymentVoucherItem) {

                this.paymentVoucher = paymentVoucherItem;
            },
            onCancel: function () {
                if (this.isValid('CanViewTCAllocation') || this.isValid('CanDraftTCAllocation')) {
                    this.$router.push({
                        path: '/TemporaryCashAllocation',
                    })
                }
                else {
                    this.$router.go()
                }
            },
        },

        created: function () {
            var root =  this;

            if(root.$route.query.Add == 'false')
            {
                root.$route.query.data = this.$store.isGetEdit;
            }
            
            this.$emit('update:modelValue', this.$route.name);

            if (this.$route.query.data == undefined) {
                this.GetAutoCodeGenerator();
                this.paymentVoucher.date = moment().format("DD MMM YYYY");
                this.paymentVoucher.paymentMode = this.$i18n.locale == 'ar' ? 'السيولة النقدية' : 'Cash';
            }

            if (this.$route.query.data != undefined) {

                var data = this.$route.query.data.message;
                this.GetBankOpeningBalance(data.bankCashAccountId);

                this.paymentVoucher.id = data.id;
                this.paymentVoucher.paymentVoucherType = 'TemporaryCashAllocation';
                this.paymentVoucher.voucherNumber = data.voucherNumber;
                this.paymentVoucher.date = moment(data.date).format("DD MMM YYYY");
                this.paymentVoucher.bankCashAccountId = data.bankCashAccountId;
                this.paymentVoucher.userEmployeeId = data.userEmployeeId;
                this.paymentVoucher.amount = data.amount;
                this.paymentVoucher.narration = data.narration;
                this.paymentVoucher.chequeNumber = data.chequeNumber;
                this.paymentVoucher.attachmentList = data.attachmentList;

                if (this.$i18n.locale == 'ar') {
                    if (data.paymentMethod == 1) {
                        this.paymentVoucher.paymentMethod = 'التحقق من';
                    }
                    else if (data.paymentMethod == 2) {
                        this.paymentVoucher.paymentMethod = 'تحويل';
                    }
                    else if (data.paymentMethod == 3) {
                        this.paymentVoucher.paymentMethod = 'الوديعة';
                    }
                    else {
                        this.paymentVoucher.paymentMethod = '';
                    }

                    if (data.paymentMode == 0) {
                        this.paymentVoucher.paymentMode = 'السيولة النقدية';
                    }
                    if (data.paymentMode == 1) {
                        this.paymentVoucher.paymentMode = 'مصرف';
                    }
                    if (data.paymentMode == 5) {
                        this.paymentVoucher.paymentMode = 'يتقدم';
                    }



                }
                if ((this.$i18n.locale == 'en' || this.isLeftToRight())) {
                    if (data.paymentMethod == 1) {
                        this.paymentVoucher.paymentMethod = 'Cheque';
                    }
                    else if (data.paymentMethod == 2) {
                        this.paymentVoucher.paymentMethod = 'Transfer';
                    }
                    else if (data.paymentMethod == 3) {
                        this.paymentVoucher.paymentMethod = 'Deposit';
                    }
                    else {
                        this.paymentVoucher.paymentMethod = '';
                    }
                    if (data.paymentMode == 0) {
                        this.paymentVoucher.paymentMode = 'Cash';
                    }
                    if (data.paymentMode == 1) {
                        this.paymentVoucher.paymentMode = 'Bank';
                    }
                    if (data.paymentMode == 5) {
                        this.paymentVoucher.paymentMode = 'Advance';
                    }

                }

            }

            this.render++;
        },

        mounted: function () {
            this.paymentVoucher.branchId = localStorage.getItem('BranchId');
            this.language = this.$i18n.locale;
        }
    }
</script>