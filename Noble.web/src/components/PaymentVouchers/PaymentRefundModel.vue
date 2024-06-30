<template>
    <modal show="show" :modalLarge="true" v-if=" isValid('CanAddSize') || isValid('CanEditSize') ">
        <div class="modal-content">
            <div class="modal-header">
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel">{{ $t('AddPaymentVoucher.AddCustomerPayReceipt')}}</h6>
                <button type="button" class="btn-close" v-on:click="close()"></button>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="col-lg-4 col-md-4 col-sm-6">
                        <label>{{ $t('AddPaymentVoucher.Date') }} :<span class="text-danger"> *</span></label>
                        <div class="form-group" v-bind:class="{ 'has-danger': v$.paymentVoucher.date.$error}">
                            <datepicker v-model="v$.paymentVoucher.date.$model"></datepicker>
                            <span v-if="v$.paymentVoucher.date.$error" class="error">
                                <span v-if="!v$.paymentVoucher.date.required">{{ $t('AddPaymentVoucher.DateRequired') }}</span>
                            </span>
                        </div>
                    </div>

                    <div class="col-lg-4 col-md-4 col-sm-6">
                        <label>
                            {{ $t('AddPaymentVoucher.Amount') }} :
                            <span class="text-danger"> *</span>
                        </label>
                        <div class="form-group">
                            <my-currency-input v-model="paymentVoucher.amount"></my-currency-input>
                        </div>
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-6">
                        <label>
                            {{ $t('AddPaymentVoucher.PaymentMode') }}:
                            <span class="text-danger"> *</span>
                        </label>

                        <div class="form-group">
                            <multiselect v-if="($i18n.locale == 'en' ||isLeftToRight()) "  v-model="paymentVoucher.paymentMode" @update:modelValue="GetAccount(paymentVoucher.paymentMode)" :options="['Cash', 'Bank','Advance']" :show-labels="false" placeholder="Select Type">
                            </multiselect>
                            <multiselect v-else  v-model="paymentVoucher.paymentMode" @update:modelValue="GetAccount(paymentVoucher.paymentMode)" :options="[ 'السيولة النقدية', 'مصرف','يتقدم']" :show-labels="false" v-bind:placeholder="$t('AddPaymentVoucher.SelectOption')">
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
                            <accountdropdown v-model="v$.paymentVoucher.bankCashAccountId.$model" @update:modelValue="GetBankOpeningBalance(paymentVoucher.bankCashAccountId)" :formName="'CashPay'" ></accountdropdown>
                            <span v-if="v$.paymentVoucher.bankCashAccountId.$error" class="error">
                                <span v-if="!v$.paymentVoucher.bankCashAccountId.required"> {{ $t('AddPaymentVoucher.AccountRequired') }}</span>
                            </span>
                        </div>
                        <div class="form-group" v-bind:class="{ 'has-danger': v$.paymentVoucher.bankCashAccountId.$error}" v-else-if="paymentVoucher.paymentMode=='Bank' || paymentVoucher.paymentMode=='مصرف' " v-bind:key="randerAccount">
                            <accountdropdown @update:modelValue="GetBankOpeningBalance(paymentVoucher.bankCashAccountId)" v-model="v$.paymentVoucher.bankCashAccountId.$model" :formName="'BankPay'" ></accountdropdown>
                            <span v-if="v$.paymentVoucher.bankCashAccountId.$error" class="error">
                                <span v-if="!v$.paymentVoucher.bankCashAccountId.required"> {{ $t('AddPaymentVoucher.AccountRequired') }}</span>
                            </span>
                        </div>
                        <div class="form-group" v-bind:class="{ 'has-danger': v$.paymentVoucher.bankCashAccountId.$error}" v-else>
                            <accountdropdown v-model="v$.paymentVoucher.bankCashAccountId.$model" @update:modelValue="GetBankOpeningBalance(paymentVoucher.bankCashAccountId)" :formName="'BankPay'" ></accountdropdown>
                            <span v-if="v$.paymentVoucher.bankCashAccountId.$error" class="error">
                                <span v-if="!v$.paymentVoucher.bankCashAccountId.required"> {{ $t('AddPaymentVoucher.AccountRequired') }}</span>
                            </span>
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
            <div class="modal-footer">
                <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="SaveVoucher()" v-bind:disabled="v$.paymentVoucher.$invalid || amount<paymentVoucher.amount" >{{ $t('AddSize.btnSave') }}</button>
                <button type="button" class="btn btn-soft-secondary btn-sm" v-on:click="close()">{{ $t('AddSize.btnClear') }}</button>
            </div>
            <loading v-model:active="loading" :can-cancel="false" :is-full-page="true"></loading>
        </div>
    </modal>

    <div v-else>
        <acessdenied></acessdenied>
    </div>
</template>

<script>
    import clickMixin from '@/Mixins/clickMixin'
    import { required, minValue } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'
    import Multiselect from 'vue-multiselect'
    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    
    import moment from "moment";

    export default {
        mixins: [clickMixin],
        props: ['newpaymentvoucher'],
        components: {
            Multiselect,
            Loading
        },
        setup() {
            return { v$: useVuelidate() }
        },
        data: function () {
            return {
                arabic: '',
                english: '',
                language: '',
                loading: false,
                randerAccount: 0,
                amount: 0,
                paymentVoucher: {},
            }
        },

        validations() {
            return {
                paymentVoucher: {
                    //voucherNumber: {
                    //    required
                    //},
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
            close: function () {
                this.$emit('close');
            },

            GetAccount: function () {
                this.randerAccount++;
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
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.paymentVoucher.date = this.paymentVoucher.date + " " + moment().format("hh:mm A");

                this.$https.post('/PaymentVoucher/SavePaymentRefund', this.paymentVoucher, { headers: { "Authorization": `Bearer ${token}`}
                }).then(function (response) {
                    if (response.data.id != '00000000-0000-0000-0000-000000000000') {

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
                                root.$emit('save');
                            }
                        });
                    }
                    else if (response.data.id == '00000000-0000-0000-0000-000000000000') {
                        root.$swal({
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                            text: response.data.isAddUpdate,
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

        },

        created() {
            
            this.paymentVoucher = this.newpaymentvoucher;
            this.amount = this.paymentVoucher.amount;
        },

        mounted: function () {
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
            var getLocale = this.$i18n.locale;
            this.language = getLocale;
        },
    }
</script>
