<template>
    <modal :show="show">

        <div style="margin-bottom:0px"  >
            <div>
                <div class="col-lg-12">
                    <div class="tab-content" id="nav-tabContent">

                        <div class="modal-header">
                            <div>
                                <h5 class="modal-title DayHeading" id="myModalLabel">{{ $t('PaymentModel.Bills') }} </h5>
                                <span class="text-white"><span class="me-3"><b>{{ dailyExpense.voucherNo }}</b></span> <span>{{ dailyExpense.date }}</span></span>
                            </div>
                            <button type="button" class="btn-close" v-on:click="close()"></button>
                        </div>
                        <div>
                            <div class="card-body ">
                                <div class="row ">
                                    <div class="col-lg-12" v-if="isTemporaryCashIssue">
                                        <label>
                                            <label>  {{ $t('PurchaseOrderExpenseBill.Bills') }} : <span class="text-danger"> *</span></label>
                                        </label>
                                        <BillsDropdown v-model="expenseBillId" @update:modelValue="GetPurchaseBillDetail(expenseBillId)" />
                                    </div>
                                    <div class="col-lg-12">
                                        <label>
                                            {{ $t('PaymentModel.PaymentMode') }}:
                                            <span class="text-danger"> *</span>
                                        </label>
                                        <div class="form-group">
                                            <multiselect v-if="($i18n.locale == 'en' ||isLeftToRight()) " :disabled="isTemporaryCashIssue" v-model="dailyExpense.paymentMode" @update:modelValue="GetAccount(dailyExpense.paymentMode)" :options="['Cash', 'Bank']" :show-labels="false" placeholder="Select Type">
                                            </multiselect>
                                            <multiselect v-else :disabled="isTemporaryCashIssue" v-model="dailyExpense.paymentMode" @update:modelValue="GetAccount(dailyExpense.paymentMode)" :options="[ 'السيولة النقدية', 'مصرف']" :show-labels="false" v-bind:placeholder="$t('PaymentModel.SelectOption')" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                                            </multiselect>
                                        </div>
                                    </div>
                                    <div class="col-sm-12">
                                        <label>{{ $t('Expense Date') }}: <span class="text-danger"> *</span></label>
                                        <div >
                                            <datepicker v-model="dailyExpense.expenseDate"></datepicker>
                                        </div>
                                    </div>
                                    <div class="col-sm-12">
                                        <div class="row">
                                            <div class="col-sm-12" v-if="dailyExpense.paymentMode=='Bank' || dailyExpense.paymentMode=='السيولة النقدية' ">
                                                <label>{{ $t('PaymentModel.BankAccount') }}: <span class="text-danger"> *</span></label>
                                                <accountdropdown v-model="dailyExpense.accountId" :formName="'BankPay'" v-bind:key="randerAccount"></accountdropdown>
                                            </div>
                                            <div class="col-sm-12" v-else>
                                                <label>{{ $t('PaymentModel.CashAccount') }}: <span class="text-danger"> *</span></label>
                                                <accountdropdown v-model="dailyExpense.accountId" :formName="'CashPay'" v-bind:key="randerAccount" :disabled="isTemporaryCashIssue"></accountdropdown>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-12">
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <label>{{ $t('PaymentModel.ExpenseAccount') }}: <span class="text-danger"> *</span></label>
                                                <accountdropdown v-model="dailyExpense.expenseId" :disabled="disabled" v-bind:key="randerAccount1" :value="dailyExpense.expenseId"></accountdropdown>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-12" v-if="amount!=0">
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <label>Remaining Amount: </label>
                                                <decimal-to-fixed :textAlignLeft="true" :disable="true" v-model="amount" />
                                            </div>
                                        </div>
                                    </div> 
                                    <div class="col-sm-12">
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <label>{{ $t('PaymentModel.Amount') }}: <span class="text-danger"> *</span></label>
                                                <decimal-to-fixed :textAlignLeft="true" v-model="dailyExpense.amount" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-12" v-if="isValid('ExpenseWithAccount')">
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <label>{{ $t('AddLineItem.ExpenseAccount') }}: <span class="text-danger"> *</span></label>
                                                <div>
                                                    <multiselect :options="options1" v-model="taxMethod" :show-labels="false" v-bind:placeholder="$t('AddLineItem.SelectMethod')">
                                                    </multiselect>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div v-if="!loading">

                            <div class="modal-footer justify-content-right">
                                <button type="button" class="btn btn-soft-primary btn-sm  " v-on:click="SaveDailyExpenseInformation('Approved')" v-bind:disabled="v$.dailyExpense.$invalid"> {{ $t('PaymentModel.btnSave') }}</button>
                                <button type="button" class="btn btn-soft-secondary btn-sm  mr-3 " v-on:click="close">{{ $t('PaymentModel.btnClear') }}</button>
                            </div>
                        </div>
                        <div v-else>
                            <loading v-model:active="loading" :can-cancel="false" :is-full-page="true"></loading>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </modal>
</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'
    
    
    import moment from 'moment';
    import { required } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'
    import Multiselect from 'vue-multiselect'
    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    export default {
        props: ['show', 'billsId', 'totalAmount', 'remainingAmount', 'expenseId', 'temporaryCash'],
        mixins: [clickMixin],
        components: {
            Loading,
            Multiselect,
        },
        setup() {
            return { v$: useVuelidate() }
        },
        data: function () {
            return {
                options1:[],

                arabic: '',
                english: '',
                expenseBillId: '',
                temporaryCashIssue: 0,
                isTemporaryCashIssue: false,
                loading: false,
                disabled: true,
                render: 0,
                language: 'Nothing',
                dailyExpense: {
                    id: '00000000-0000-0000-0000-000000000000',
                    voucherNo: '',
                    accountId: '',
                    expenseId: '',
                    date: '',
                    description: 'Payment of Bills from Expense',
                    amount: 0,
                    isDraft: false,
                    IsExpenseAccount: false,
                    reason: '',
                    expenseDate: '',
                    counterId: '00000000-0000-0000-0000-000000000000',
                    isDayStart: false,
                    dailyExpenseDetails: [],
                    paymentMode: '',
                    temporaryCashIssueId: '',

                    grossAmount: 0,
                    totalAmount: 0,
                    totalVat: 0,
                },
                CompanyID: '',
                UserID: '',
                taxMethod:'',
                employeeId: '',
                isDayAlreadyStart: false,
                IsExpenseAccount: false,
                lengthCount: 0,
                randerAccount: 0,
                randerAccount1: 0,
                amount: 0
            }
        },
        validations() {
            return {
                dailyExpense:
                {
                    paymentMode: {
                        required,
                    },
                    accountId: {
                        required,
                    },
                    expenseId: {
                        required,
                    },
                }
                }
        },
        methods: {
            GetPurchaseBillDetail: function (id) {

                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('/Purchase/PurchaseBillDetail?Id=' + id, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data != null) {
                            
                            if (response.data.remainingAmount != 0) {
                                root.dailyExpense.amount = response.data.totalAmount - response.data.remainingAmount;

                                root.amount = response.data.totalAmount - response.data.remainingAmount;

                            }
                            else {
                                root.dailyExpense.amount = response.data.totalAmount;

                            }
                            root.dailyExpense.billerAccountId = response.data.id;
                            root.dailyExpense.expenseId = response.data.billerId;
                            root.randerAccount1++;
                        }
                    },
                        function (error) {
                            this.loading = false;
                            console.log(error);
                        });
            },
            createUUID: function () {
                var dt = new Date().getTime();
                var uuid = 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
                    var r = (dt + Math.random() * 16) % 16 | 0;
                    dt = Math.floor(dt / 16);
                    return (c == 'x' ? r : (r & 0x3 | 0x8)).toString(16);
                });
                return uuid;
            },
            GetAccount: function (x) {


                if (x == 'السيولة النقدية' || x == 'Bank') {
                    this.randerAccount++;

                }
                else if (x == 'مصرف' || x == 'Cash') {
                    this.randerAccount++;
                }

            },
            SaveDailyExpenseInformation: function (value) {
                this.loading = true;
                var root = this;
                this.dailyExpense.approvalStatus = value
                var url = '/Company/SaveDailyExpense';
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                if (this.dailyExpense.paymentMode == "") {
                    this.dailyExpense.paymentMode = 'Default';
                }
                var lineItemId = this.createUUID();

                root.dailyExpense.totalAmount = root.dailyExpense.amount;
                root.dailyExpense.expenseDate = root.dailyExpense.expenseDate + " " + moment().format("hh:mm A");
                var counterId = localStorage.getItem('CounterId');
                var dayStart = localStorage.getItem('IsDayStart');
                root.dailyExpense.counterId = counterId == null ? '00000000-0000-0000-0000-000000000000' : counterId;
                root.dailyExpense.isDayStart = dayStart == "true" ? true : false;
                this.dailyExpense.dailyExpenseDetails.push({
                    id: lineItemId,
                    amount: this.dailyExpense.amount,
                    expenseAccountId: this.dailyExpense.expenseId,
                    description: this.dailyExpense.description,
                });
                root.$https
                    .post(url, root.dailyExpense, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(response => {
                        root.info = response.data.bpi
                        this.$swal.fire({
                            title: root.$t('PaymentModel.SavedSuccessfully'),
                            text: root.$t('PaymentModel.Saved'),
                            type: 'success',
                            confirmButtonClass: "btn btn-success",
                            buttonStyling: false,
                            icon: 'success',
                            timer: 1500,
                            timerProgressBar: true,

                        });
                        root.$emit('RefreshList');
                    })
                    .catch(error => {
                        console.log(error)
                        this.$swal.fire(
                            {
                                icon: 'error',
                                title: this.$t('PaymentModel.Error'),
                                text: this.$t('PaymentModel.Error'),
                            });
                        root.errored = true
                    })
                    .finally(() => root.loading = false)
            },
            AutoIncrementVoucherNo: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https
                    .get('/Company/AutoGenerateCode', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                        if (response.data != null) {
                            root.dailyExpense.voucherNo = response.data;
                        }
                    });
            },
            close: function () {
                this.$emit('close');
            },
        },
        created: function () {
            
            var root = this;
            this.AutoIncrementVoucherNo();
            root.dailyExpense.date = moment().format('llll');
            if (this.temporaryCash != undefined) {
                this.isTemporaryCashIssue = this.temporaryCash.isTemporaryCashIssue;
                this.dailyExpense.temporaryCashIssueId = this.temporaryCash.id;
                this.temporaryCashIssue = this.temporaryCash.amount - (this.temporaryCash.returnAmount + this.temporaryCash.VoucherAmount);

                this.dailyExpense.accountId = this.temporaryCash.temporaryCashAccountId;
                if (this.$i18n.locale == 'ar') {
                    this.dailyExpense.paymentMode = 'السيولة النقدية';
                }
                if ((this.$i18n.locale == 'en' || this.isLeftToRight())) {
                    this.dailyExpense.paymentMode = 'Cash';
                }
                root.amount = 0;
                root.dailyExpense.IsExpenseAccount = root.IsExpenseAccount;
                
            }
            else {
                root.dailyExpense.IsExpenseAccount = root.IsExpenseAccount;
                if (root.remainingAmount != 0) {
                    root.dailyExpense.amount = root.totalAmount - root.remainingAmount;

                    root.amount = root.totalAmount - root.remainingAmount;

                }
                else {
                    root.dailyExpense.amount = root.totalAmount;

                }
                root.dailyExpense.billerAccountId = root.billsId;
                this.dailyExpense.expenseId = this.expenseId;
                this.randerAccount1++;
            }

        },
        mounted: function () {
            if ((this.$i18n.locale == 'en')) {
                this.options1 = ['Inclusive', 'Exclusive', 'Exempted'];
            }
            else if ((this.$i18n.locale == 'ar')) {
                this.options1 = ['شامل', 'غير شامل', 'معفى'];
            }
            else  {
                this.options1 = ['Inclusive', 'Exclusive', 'Exempted'];
            }

            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
        }
    }
</script>
