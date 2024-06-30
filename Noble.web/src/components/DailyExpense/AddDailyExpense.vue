<template>
    <div class="row" v-if="isValid('CanDraftExpense') || isValid('CanAddExpense') || isValid('CanEditExpense')">
        <div class="col-lg-12" v-if="isDayAlreadyStart">
            <div class="row">
                <div class="col d-flex align-items-baseline">
                    <div class="media">
                        <span class="circle-singleline" style="background-color: #1761FD !important;margin:10px !important">EX</span>
                        <div class="media-body align-self-center ms-3">
                            <h6 class="m-0 font-20" v-if="dailyExpense.id != '00000000-0000-0000-0000-000000000000'"> {{ $t('AddDailyExpense.UpdateExpense')}}</h6>
                            <h6 class="m-0 font-20" v-else> {{ $t('AddDailyExpense.ExpenseRecording')}}</h6>
                            <div class="col d-flex ">
                                <p class="text-muted mb-0" style="font-size:13px !important;"><b>{{ dailyExpense.voucherNo }}</b> &nbsp;&nbsp; <span>{{ dailyExpense.date }}</span></p>
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
            <hr class="hr-dashed hr-menu mt-0" />
            <div class="card border-0">
                <div class="card-body border-0">
                    <div class="row">
                        <div class="col-lg-12 ">
                            <div class="mt-2">
                                <div class="row">
                                    <div class="col-lg-6" v-if="isValid('ExpenseWithAccount')">
                                        <div class="row form-group">
                                            <label class="col-form-label col-lg-4">
                                                <span id="ember695" class="tooltip-container text-dashed-underline ">  {{ $t('AddDailyExpense.SpentDate') }}:  <span class="text-danger"> *</span></span>
                                            </label>
                                            <div class="inline-fields col-lg-8">
                                                <datepicker :key="render" v-model="dailyExpense.expenseDate"></datepicker>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-6" v-if="isValid('CanViewExpenseBill')">
                                        <div class="row form-group">
                                            <label class="col-form-label col-lg-4">
                                                <span id="ember695" class="tooltip-container text-dashed-underline ">  {{ $t('AddDailyExpense.SelectBills') }}:</span>
                                            </label>
                                            <div class="inline-fields col-lg-8">
                                                <BillsDropdown v-model="dailyExpense.billerAccountId" @update:modelValue="GetBilllerValue" ref="BillerAccount" :modelValue="dailyExpense.billerAccountId" :disable="isExpenseBill" :key="expenseBillRender"></BillsDropdown>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-6" v-if="IsExpenseAccount">
                                        <div class="row form-group">
                                            <label class="col-form-label col-lg-4">
                                                <span id="ember695" class="tooltip-container text-dashed-underline ">  {{ $t('AddDailyExpense.PaymentMode') }}:<span class="text-danger"> *</span></span>
                                            </label>
                                            <div class="inline-fields col-lg-8">
                                                <multiselect v-if="($i18n.locale == 'en' ||isLeftToRight()) " :disabled="isTemporaryCashIssue" v-model="dailyExpense.paymentMode" @update:modelValue="GetAccount(dailyExpense.paymentMode)" :options="['Cash', 'Bank']" :show-labels="false" placeholder="Select Type">
                                                </multiselect>
                                                <multiselect v-else v-model="dailyExpense.paymentMode" :disabled="isTemporaryCashIssue" @update:modelValue="GetAccount(dailyExpense.paymentMode)" :options="[ 'السيولة النقدية', 'مصرف']" :show-labels="false" v-bind:placeholder="$t('AddDailyExpense.SelectOption')">
                                                </multiselect>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-6" v-if="IsExpenseAccount">
                                        <div class="row form-group" v-if="dailyExpense.paymentMode=='Cash' || dailyExpense.paymentMode=='السيولة النقدية' " v-bind:key="randerAccount">
                                            <label class="col-form-label col-lg-4">
                                                <span id="ember695" class="tooltip-container text-dashed-underline ">{{ $t('AddDailyExpense.Bank/CashAccount') }}:<span class="text-danger">*</span><span v-if="IsExpenseAmount">{{runningBalance}}</span></span>
                                            </label>
                                            <div class="inline-fields col-lg-8">
                                                <accountdropdown v-model="dailyExpense.accountId" :formName="'CashReceipt'" @update:modelValue="GetBankOpeningBalance(dailyExpense.accountId)" :disabled="isTemporaryCashIssue" />

                                            </div>
                                        </div>
                                        <div class="row form-group" v-else v-bind:key="randerAccount">
                                            <label class="col-form-label col-lg-4">
                                                <span id="ember695" class="tooltip-container text-dashed-underline ">{{ $t('AddDailyExpense.Bank/CashAccount') }}:<span class="text-danger">*</span><span v-if="IsExpenseAmount">{{runningBalance}}</span></span>
                                            </label>
                                            <div class="inline-fields col-lg-8">
                                                <accountdropdown v-model="dailyExpense.accountId" :formName="'BankReceipt'" v-bind:key="randerAccount" />

                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-6">
                                        <div class="row form-group">
                                            <label class="col-form-label col-lg-4">
                                                <span id="ember695" class="tooltip-container text-dashed-underline "> {{ $t('AddDailyExpense.ReferenceNo')}} :</span>
                                            </label>
                                            <div class="inline-fields col-lg-8">
                                                <input class="form-control" v-model="dailyExpense.referenceNo" type="text" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-6" v-if="IsExpenseAccount">
                                        <div class="row form-group">
                                            <label class="col-form-label col-lg-4">
                                                <span id="ember695" class="tooltip-container text-dashed-underline "> {{ $t('AddDailyExpense.ExNameEn')}}:</span>
                                            </label>
                                            <div class="inline-fields col-lg-8">
                                                <input class="form-control" v-model="dailyExpense.name" type="text" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-6" v-if="IsExpenseAccount">
                                        <div class="row form-group">
                                            <label class="col-form-label col-lg-4">
                                                <span id="ember695" class="tooltip-container text-dashed-underline "> {{ $t('AddDailyExpense.TaxId')}}:</span>
                                            </label>
                                            <div class="inline-fields col-lg-8">
                                                <input class="form-control" v-model="dailyExpense.taxId" type="text" />
                                            </div>
                                        </div>
                                    </div>


                                    <div class="col-lg-12 col-md-12 col-sm-12">
                                        <adddailyexpenserow :balance="balance" :formName="formName" :dailyExpense="dailyExpense" :BillerRecord="BillerRecord" :dailyExpenseRows="dailyExpenseDetails" ref="dailyExpenseRef"
                                                            @update:modelValue="getupdatedailyExpenseRows"></adddailyexpenserow>
                                    </div>


                                    <div class="col-lg-12 mt-4 mb-2">
                                        <div class="card">
                                            <div class="card-body">
                                                <div class="row">
                                                    <div class="col-lg-8" style="border-right: 1px solid #eee;">
                                                        <div class="form-group pe-3">
                                                            <label>{{ $t('AddDailyExpense.ExpenseDescription') }}:</label>
                                                            <textarea class="form-control " rows="3" v-model="dailyExpense.description" />
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-4">
                                                        <div class="form-group ps-3">
                                                            <div class="font-xs mb-1">{{ $t('AddDailyExpense.Attachment') }}</div>

                                                            <button v-on:click="Attachment()" type="button" class="btn btn-light btn-square btn-outline-dashed mb-1"><i class="fas fa-cloud-upload-alt"></i> {{ $t('AddSaleOrder.Attachment') }} </button>

                                                            <div>
                                                                <small class="text-muted">
                                                                    {{ $t('AddDailyExpense.FileSize') }}
                                                                </small>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-12 invoice-btn-fixed-bottom">

                                        <div class="col-lg-12 col-md-12 col-sm-12">
                                            <div v-if="dailyExpense.id == null || dailyExpense.id == '00000000-0000-0000-0000-000000000000'" class="button-items">

                                                <button class="btn btn-outline-primary form-group me-2" v-on:click="SaveDailyExpenseInformation('Draft')" v-bind:disabled="v$.dailyExpense.$invalid" v-if="isValid('CanDraftExpense') && !isTemporaryCashIssue"><i class="far fa-save"></i>  {{ $t('AddDailyExpense.Save') }}</button>
                                                <button class="btn btn-outline-primary form-group me-2" v-on:click="SaveDailyExpenseInformation('Approved')" v-bind:disabled="v$.dailyExpense.$invalid || (isTemporaryCashIssue? (temporaryCashIssue < expenseAmount):false)" v-if="isValid('CanAddExpense')"><i class="far fa-save"></i>  {{ $t('AddDailyExpense.SaveAndpost') }}</button>
                                                <button class="btn btn-danger form-group" v-on:click="BackToList()">{{ $t('AddDailyExpense.Cancel') }}</button>

                                            </div>
                                            <div v-else class="button-items">

                                                <button class="btn btn-outline-primary form-group me-2" v-on:click="SaveDailyExpenseInformation('Draft')" v-if="(isValid('CanDraftExpense') || isValid('CanEditExpenseasDraft')) && !dailyExpense.voucherNo.match('DE')" v-bind:disabled="v$.dailyExpense.$invalid"><i class="far fa-save"></i>  {{ $t('AddDailyExpense.Update') }}</button>
                                                <button class="btn btn-outline-primary form-group me-2" v-on:click="SaveDailyExpenseInformation('Approved')" v-if="isValid('CanEditExpense')" v-bind:disabled="v$.dailyExpense.$invalid"><i class="far fa-save"></i>  {{ $t('AddDailyExpense.Updateandpost') }}</button>
                                                <button class="btn btn-primary btn-round float-right me-2" v-on:click="RejectionModel" v-if="isValid('CanSaveExpenseasReject') || isValid('Can Edit Expense as Reject')" v-bind:disabled="v$.dailyExpense.$invalid"><i class="far fa-save"></i> {{ $t('DailyExpense.Reject') }}</button>
                                                <button class="btn btn-danger form-group" v-on:click="BackToList()">{{ $t('AddDailyExpense.Cancel') }}</button>


                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <rejectionmodel :newdailyExpense="dailyExpense"
                                :show="show"
                                v-if="show"
                                @close="show = false" />
                <bulk-attachment :attachmentList="dailyExpense.attachmentList" :show="isShow" v-if="isShow" @close="attachmentSave" />


            </div>
        </div>
        <div class="row d-flex justify-content-center align-items-center" style="height: 70vh;" v-else>
            <div class="col-lg-6 col-sm-6 ml-auto mr-auto">
                <div class="card p-3 text-center " v-if="bankDetail">
                    <h4 class="">{{ $t('FirstStartInvoice') }}</h4>
                    <router-link :to="{path: '/WholeSaleDay', query: { token_name:'DayStart_token', fromDashboard:'true' } }"><a href="javascript:void(0)" class="btn btn-outline-danger ">{{ $t('Dashboard.DayStart') }}</a></router-link>
                </div>
                <div class="card p-5 text-center" v-else>
                    <h4 class="">{{ $t('FirstStartInvoice') }}</h4>
                    <router-link :to="{path: '/dayStart', query: { token_name:'DayStart_token', fromDashboard:'true' } }"><a href="javascript:void(0)" class="btn btn-outline-danger ">{{ $t('Dashboard.DayStart') }}</a></router-link>
                </div>
            </div>
        </div>
         <loading v-model:active="loading" :can-cancel="true" :is-full-page="true"></loading>
    </div>
    <div v-else> <acessdenied></acessdenied></div>

</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'
    import moment from 'moment';
    import { required, maxLength, requiredIf } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'
    import Multiselect from 'vue-multiselect'
    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    export default ({
        mixins: [clickMixin],
        props: ['formName'],

        components: {
            Multiselect,
            Loading
        },
        setup() {
            return { v$: useVuelidate() }
        },

        data: function () {
            return {
                 loading: false,
                bankDetail: false,
                expenseAmount: 0,
                temporaryCashIssue: 0,
                isTemporaryCashIssue: false,
                attachment: false,
                date: false,
                attachments: false,
                dailyExpenseDetails: [],
                BillerRecord: [],
                render: 0,
                language: 'Nothing',
                dailyExpense: {
                    id: '00000000-0000-0000-0000-000000000000',
                    voucherNo: '',
                    grossAmount: 0,
                    totalAmount: 0,
                    totalVat: 0,
                    accountId: '',
                    date: '',
                    referenceNo: '',
                    name: '',
                    taxId: '',
                    description: '',
                    billerAccountId: '',
                    isDraft: false,
                    IsExpenseAccount: false,
                    reason: '',
                    counterId: '00000000-0000-0000-0000-000000000000',
                    isDayStart: false,
                    dailyExpenseDetails: [],
                    expenseAttachment: [],
                    attachmentList: [],
                    paymentMode: '',
                    temporaryCashIssueId: '',
                    branchId: localStorage.getItem('BranchId'),
                    expenseDate: '',
                },
                CompanyID: '',
                UserID: '',
                employeeId: '',
                isDayAlreadyStart: false,
                IsExpenseAccount: false,
                IsExpenseAmount: false,
                show: false,
                isShow: false,
                lengthCount: 0,
                runningBalance: 0,
                balance: 0,
                randerAccount: 0,
                summary: 0,

                isExpenseBill: false,
                expenseBillRender: 0,
            }
        },
        validations() {
            return {
                dailyExpense:
                {
                    voucherNo:
                    {
                        required,
                        maxLength: maxLength(30)
                    },
                    description:
                    {
                        maxLength: maxLength(200)
                    },
                    dailyExpenseDetails:
                    {
                        required,
                    },
                    paymentMode: {
                         required: requiredIf(function () {
                            return this.dailyExpense.IsExpenseAccount == true ? true : false;
                        }),
                        // required: requiredIf((x) => {
                        //     if (x.IsExpenseAccount == true)
                        //         return true;
                        //     return false;
                        // }),
                    },
                    accountId: {
                         required: requiredIf(function () {
                            return this.dailyExpense.IsExpenseAccount == true ? true : false;
                        }),
                        // required: requiredIf((x) => {
                        //     if (x.IsExpenseAccount == true)
                        //         return true;
                        //     return false;
                        // }),
                    },


                }
            }
        },

        methods: {
            Attachment: function () {
                this.isShow = true;
            },

            attachmentSave: function (attachment) {
                this.dailyExpense.attachmentList = attachment;
                this.isShow = false;
            },

            GetBankOpeningBalance: function (id) {
                if (this.IsExpenseAmount) {
                    if (this.dailyExpense.paymentMode == "السيولة النقدية" || this.dailyExpense.paymentMode == "Cash") {
                        var token = '';
                        if (token == '') {
                            token = localStorage.getItem('token');
                        }
                        var root = this
                        this.$https.get('/Contact/GetCustomerRunningBalance?id=' + id, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                            if (response.data != null) {
                                root.balance = response.data;
                                root.runningBalance = parseFloat(response.data) > 0 ? 'Dr ' + parseFloat(response.data) * +1 : 'Cr ' + parseFloat(response.data) * (-1);
                            }
                        });
                    }

                }


            },
            GetBilllerValue: function () {

                this.BillerRecord = this.$refs.BillerAccount.GetAmountOfSelected();
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
            GetBillerAccount: function (date) {


                return moment(date).format('LLL');

            },
            billAttachments: function (x) {

                var root = this;
                this.attachments = false;
                this.attachment = false;

                if (x != undefined && x != null && x != '') {
                    this.dailyExpense.expenseAttachment.push({
                        path: x.path,
                        date: x.date,
                        description: x.description
                    })
                }
                this.$swal.fire({
                    title: root.$t('AddDailyExpense.SavedSuccessfully'),
                    text: root.$t('AddDailyExpense.Saved'),
                    type: 'success',
                    confirmButtonClass: "btn btn-success",
                    buttonStyling: false,
                    icon: 'success',
                    timer: 1500,
                    timerProgressBar: true,

                });
            },
            GetAccount: function (x) {


                if (x == 'السيولة النقدية' || x == 'Bank') {
                    this.randerAccount++;

                }
                else if (x == 'مصرف' || x == 'Cash') {
                    this.randerAccount++;
                }

            },
            RejectionModel: function () {

                this.dailyExpense.approvalStatus = 'Rejected';
                this.show = !this.show;

            },
            ConvertToDigits: function (x) {

                if (/^[0-9\u0660-\u0669]+$/.test(x) == true) {
                    x = x.replace(/[٠-٩]/g, d => "٠١٢٣٤٥٦٧٨٩".indexOf(d)).replace(/[۰-۹]/g, d => "۰۱۲۳۴۵۶۷۸۹".indexOf(d));

                }
                else {
                    this.$swal({
                        title: this.$t('AddDailyExpense.Error'),
                        text: "Please Enter valid number in arabic or numeric",
                        type: 'error',
                        confirmButtonClass: "btn btn-danger",
                        icon: 'error',
                        timer: 1500,
                        timerProgressBar: true,
                    });
                }




            },
            languageChange: function (lan) {
                if (this.language == lan) {
                    if (this.dailyExpense.id == '00000000-0000-0000-0000-000000000000') {

                        var getLocale = this.$i18n.locale;
                        this.language = getLocale;

                        this.$router.go('/adddailyexpense');
                    }
                    else {

                        this.$swal({
                            title: this.$t('AddDailyExpense.Error'),
                            text: this.$t('AddDailyExpense.ChangeLanguageError'),
                            type: 'error',
                            confirmButtonClass: "btn btn-danger",
                            icon: 'error',
                            timer: 4000,
                            timerProgressBar: true,
                        });
                    }
                }


            },
            getupdatedailyExpenseRows: function (items, amount) {
                this.dailyExpenseDetails = items;
                this.dailyExpense.dailyExpenseDetails = items;
                this.expenseAmount = amount;
            },
            AutoIncrementVoucherNo: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                root.$https
                    .get('/Company/AutoGenerateCode?branchId=' + localStorage.getItem('BranchId'), { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                        if (response.data != null) {
                            root.dailyExpense.voucherNo = response.data;
                        }
                    });
            },
            BackToList: function () {
                if (this.isValid('CanViewExpense') || this.isValid('CanDraftExpense')) {
                    if (this.formName == "ExpenseBills") {
                        this.$router.push({
                            path: '/PurchaseBill',
                        })
                    }
                    else {
                        this.$router.push({
                            path: '/dailyexpense',
                            query: {
                                data: 'AddDailyExpense',
                                formName: this.formName
                            }
                        })
                    }
                }
                else {
                    this.$router.go();
                }
            },
            SaveDailyExpenseInformation: function (value) {
                 this.loading = true;
                var root = this;
                this.dailyExpense.approvalStatus = value;
                if (this.IsExpenseAmount) {
                    if (this.dailyExpense.paymentMode == "السيولة النقدية" || this.dailyExpense.paymentMode == "Cash") {
                        var totalAmount = this.$refs.dailyExpenseRef.GetAmountOfSelected();
                        if (totalAmount > this.balance) {
                            this.$swal.fire(
                                {
                                    icon: 'error',
                                    title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Expense Exceed' : 'تجاوز المصاريف',
                                    text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Your Expense Exceed Your Cash in Hand' : 'نفقتك تتجاوز أموالك في متناول اليد',
                                    showConfirmButton: false,
                                    timer: 3000,
                                    timerProgressBar: true,

                                });
                            return;
                        }

                    }
                }

                this.dailyExpense.grossAmount = this.dailyExpense.dailyExpenseDetails
                    .reduce((total, prod) => total + parseFloat(prod.amount), 0)
                    .toFixed(3).slice(0, -1);
                if (this.IsExpenseAccount) {
                    this.dailyExpense.totalVat = this.dailyExpense.dailyExpenseDetails
                        .reduce((total, prod) => total + parseFloat(prod.vatAmount == undefined ? 0 : prod.vatAmount), 0)
                        .toFixed(3).slice(0, -1);
                    this.dailyExpense.totalAmount = this.dailyExpense.dailyExpenseDetails
                        .reduce((total, prod) => total + parseFloat(prod.amountAfterVAT == undefined ? 0 : prod.amountAfterVAT), 0)
                        .toFixed(3).slice(0, -1);

                }
                else {
                    this.dailyExpense.totalAmount = this.dailyExpense.grossAmount;
                }

                var url = '/Company/SaveDailyExpense';
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                if (this.dailyExpense.paymentMode == "") {
                    this.dailyExpense.paymentMode = 'Default';
                }
                if (this.dailyExpense.paymentMode == "السيولة النقدية") {
                    this.dailyExpense.paymentMode = 'Cash';
                }
                if (this.dailyExpense.paymentMode == "مصرف") {
                    this.dailyExpense.paymentMode = 'Bank';
                }


                localStorage.setItem('active', value);


                root.dailyExpense.branchId = localStorage.getItem('BranchId');
                var counterId = localStorage.getItem('CounterId');
                var dayStart = localStorage.getItem('IsDayStart');
                root.dailyExpense.counterId = counterId == null ? '00000000-0000-0000-0000-000000000000' : counterId;
                root.dailyExpense.isDayStart = dayStart == "true" ? true : false;

                var prd = root.dailyExpense.dailyExpenseDetails.findIndex(x => x.amount == 0);
                if (prd >= 0) {
                    root.dailyExpense.dailyExpenseDetails.splice(prd, 1)
                }

                root.$https
                    .post(url, root.dailyExpense, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(response => {
                        root.info = response.data.bpi
                        root.$swal.fire({
                            title: root.$t('AddDailyExpense.SavedSuccessfully'),
                            text: root.$t('AddDailyExpense.Saved'),
                            type: 'success',
                            confirmButtonClass: "btn btn-success",
                            buttonStyling: false,
                            icon: 'success',
                            timer: 1500,
                            timerProgressBar: true,

                        });
                        root.$store.GetExpenseBillItemsList(null);
                        if (root.isValid('CanViewExpense') || root.isValid('CanDraftExpense')) {
                            if (root.isTemporaryCashIssue) {
                                root.$router.push({
                                    path: '/TemporaryCashIssue',
                                })
                            }
                            else {
                                root.$router.push({
                                    path: '/dailyexpense',
                                    query: {
                                        data: 'AddDailyExpense',
                                        formName: root.formName
                                    }
                                })
                            }
                        }
                        else {
                            root.$router.go();
                        }

                    })
                    .catch(error => {
                        console.log(error)
                        this.$swal.fire(
                            {

                                icon: 'error',
                                title: error.response.data,
                                text: error.response.data,
                            });
                        root.date = true;
                        root.errored = true
                    })
                    .finally(() => root.loading = false)
            },

            UpdateDailyExpenseInformation: function (value) {

                var root = this;
                //if (this.lengthCount != root.dailyExpense.dailyExpenseDetails.length) {
                //    root.dailyExpense.dailyExpenseDetails.pop();
                //}
                root.dailyExpense.approvalStatus = value;
                var url = '/Company/SaveDailyExpense';
                var token = '';

                if (token == '') {
                    token = localStorage.getItem('token');
                }
                if (this.dailyExpense.paymentMode == "") {
                    this.dailyExpense.paymentMode = 'Default';
                }

                var counterId = localStorage.getItem('CounterId');
                var dayStart = localStorage.getItem('IsDayStart');
                root.dailyExpense.counterId = counterId == null ? '00000000-0000-0000-0000-000000000000' : counterId;
                root.dailyExpense.isDayStart = dayStart == "true" ? true : false;
                var prd = root.dailyExpense.dailyExpenseDetails.findIndex(x => x.amount == 0);
                if (prd >= 0) {
                    root.dailyExpense.dailyExpenseDetails.splice(prd, 1)
                }
                root.$https
                    .post(url, root.dailyExpense, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(response => {
                        root.info = response.data.bpi
                        this.$swal.fire({
                            title: root.$t('AddDailyExpense.UpdateSuccessfully'),
                            text: root.$t('AddDailyExpense.Updated'),
                            type: 'success',
                            confirmButtonClass: "btn btn-success",
                            buttonStyling: false,
                            icon: 'success',
                            timer: 1500,
                            timerProgressBar: true,

                        });
                        if (this.isValid('CanViewExpense') || this.isValid('CanDraftExpense')) {
                            this.$router.push({
                                path: '/dailyexpense',
                                query: {
                                    data: 'AddDailyExpense',
                                    formName: root.formName
                                }
                            })
                        }
                        else {
                            this.$router.go();
                        }
                    })
                    .catch(error => {
                        console.log(error)
                        this.$swal.fire(
                            {
                                icon: 'error',
                                title: root.$t('AddDailyExpense.Error'),
                                text: root.$t('AddDailyExpense.SomethingWrong'),
                                showConfirmButton: false,
                                timer: 1000,
                                timerProgressBar: true,

                            });
                        root.errored = true
                    })
                    .finally(() => root.loading = false)
            },
            getDate: function (date) {
                if (date == null || date == undefined) {
                    return "";
                }
                else {
                    return moment(date).format('DD MMM YYYY hh:mm A');
                }
            },
        },
        created: function () {
            var root = this;

            if (root.$route.query.Add == 'false') {
                root.$route.query.data = this.$store.isGetEdit;
            }

            this.language = this.$i18n.locale;
            var IsDayStart = localStorage.getItem('DayStart');
            var IsDayStartOn = localStorage.getItem('IsDayStart');

            if (this.formName == 'dailyexpense') {

                this.IsExpenseAccount = false;
            }
            else {
                this.IsExpenseAccount = localStorage.getItem('IsExpenseAccount') == 'true' ? true : false;
            }

            this.isExpenseBill = this.$route.query.formName == "ExpenseBills" ? true : false;


            if (this.$route.query.formName == "ExpenseBills") {
                this.isDayAlreadyStart = true;
                this.AutoIncrementVoucherNo();
                this.dailyExpense.date = moment().format("DD MMM YYYY hh:mm A");
                this.dailyExpense.expenseDate = moment().format("DD MMM YYYY");

                this.dailyExpense.billerAccountId = this.$route.query.data.id;
                this.expenseBillRender++;
                this.$store.GetExpenseBillItemsList(this.$route.query.data);
            }
            else if (IsDayStart != 'true') {
                this.isDayAlreadyStart = true;
                if (this.$route.query.data == undefined) {
                    this.AutoIncrementVoucherNo();
                    this.dailyExpense.date = moment().format("DD MMM YYYY hh:mm A");
                    this.dailyExpense.expenseDate = moment().format("DD MMM YYYY");
                }
                if (this.$route.query.data != undefined) {

                    this.dailyExpense = this.$route.query.data;
                    if (this.formName == 'dailyexpense') {

                        this.dailyExpense.IsExpenseAccount = false;
                    }
                    else {
                        this.dailyExpense.IsExpenseAccount = this.IsExpenseAccount;
                    }
                    this.dailyExpense.date = this.getDate(this.dailyExpense.date);
                    //this.dailyExpense.dailyExpenseDetails = this.$route.query.data.dailyExpenseDetails;
                    if (this.$i18n.locale == 'ar') {
                        if (this.dailyExpense.paymentMode == 0) {
                            this.dailyExpense.paymentMode = 'السيولة النقدية';
                        }
                        if (this.dailyExpense.paymentMode == 1) {
                            this.dailyExpense.paymentMode = 'مصرف';
                        }
                        else {
                            this.dailyExpense.paymentMode = '';
                        }
                    }
                    if ((this.$i18n.locale == 'en')) {
                        if (this.dailyExpense.paymentMode == 0) {
                            this.dailyExpense.paymentMode = 'Cash';
                        }
                        if (this.dailyExpense.paymentMode == 1) {
                            this.dailyExpense.paymentMode = 'Bank';
                        }
                        else {
                            this.dailyExpense.paymentMode = '';
                        }
                    }
                }
                root.render++;
            }
            else {
                this.CompanyID = localStorage.getItem('CompanyID');
                this.UserID = localStorage.getItem('UserID');
                this.employeeId = localStorage.getItem('EmployeeId');
                if (IsDayStartOn == 'true') {

                    this.isDayAlreadyStart = true;
                    if (root.$route.query.data == undefined) {
                        root.AutoIncrementVoucherNo();
                        root.dailyExpense.date = moment().format("DD MMM YYYY hh:mm A");
                        root.dailyExpense.expenseDate = moment().format("DD MMM YYYY");
                        if (this.formName == 'dailyexpense') {

                            this.dailyExpense.IsExpenseAccount = false;
                        }
                        else {
                            this.dailyExpense.IsExpenseAccount = this.IsExpenseAccount;
                        }
                    }

                    if (root.$route.query.data != undefined) {
                        if (this.formName == 'dailyexpense') {

                            this.dailyExpense.IsExpenseAccount = false;
                        }
                        else {
                            this.dailyExpense.IsExpenseAccount = this.IsExpenseAccount;
                        }
                        /*Temporary Cash Issue*/
                        if (root.$route.query.data.isTemporaryCashIssue) {
                            root.AutoIncrementVoucherNo();
                            root.dailyExpense.date = moment().format("DD MMM YYYY hh:mm A");
                            root.dailyExpense.expenseDate = moment().format("DD MMM YYYY");
                            root.isTemporaryCashIssue = root.$route.query.data.isTemporaryCashIssue;
                            root.dailyExpense.temporaryCashIssueId = root.$route.query.data.id;
                            root.temporaryCashIssue = root.$route.query.data.amount - (root.$route.query.data.returnAmount + root.$route.query.data.voucherAmount);

                            if (this.$i18n.locale == 'ar') {
                                this.dailyExpense.paymentMode = 'السيولة النقدية';
                            }
                            else {
                                this.dailyExpense.paymentMode = 'Cash';
                            }

                            root.dailyExpense.accountId = root.$route.query.data.temporaryCashAccountId;
                            root.GetBankOpeningBalance(root.$route.query.data.temporaryCashAccountId);
                        }
                        else {
                            root.dailyExpense = root.$route.query.data;
                            this.dailyExpense.date = root.getDate(this.dailyExpense.date);
                            if (this.$i18n.locale == 'ar') {
                                if (this.dailyExpense.paymentMode == 0) {
                                    this.dailyExpense.paymentMode = 'السيولة النقدية';
                                }
                                if (this.dailyExpense.paymentMode == 1) {
                                    this.dailyExpense.paymentMode = 'مصرف';
                                }
                                else {
                                    this.dailyExpense.paymentMode = '';
                                }
                            }
                            if ((this.$i18n.locale == 'en')) {

                                if (this.dailyExpense.paymentMode == 0) {
                                    this.dailyExpense.paymentMode = 'Cash';
                                }
                                if (this.dailyExpense.paymentMode == 1) {
                                    this.dailyExpense.paymentMode = 'Bank';
                                }
                                else {
                                    this.dailyExpense.paymentMode = '';
                                }
                            }
                            root.GetBankOpeningBalance(root.dailyExpense.accountId);
                            root.dailyExpense.dailyExpenseDetails = root.$route.query.data.dailyExpenseDetails;

                        }
                    }
                    root.render++;
                }
                else {
                    if (root.$route.query.data == undefined) {
                        root.AutoIncrementVoucherNo();
                        root.dailyExpense.date = moment().format("DD MMM YYYY hh:mm A");
                        root.dailyExpense.expenseDate = moment().format("DD MMM YYYY");
                        if (this.formName == 'dailyexpense') {

                            this.dailyExpense.IsExpenseAccount = false;
                        }
                        else {
                            this.dailyExpense.IsExpenseAccount = this.IsExpenseAccount;
                        }
                    }
                    if (root.$route.query.data != undefined) {

                        root.dailyExpense = root.$route.query.data;
                        if (this.formName == 'dailyexpense') {

                            this.dailyExpense.IsExpenseAccount = false;
                        }
                        else {
                            this.dailyExpense.IsExpenseAccount = this.IsExpenseAccount;
                        }
                        if (this.$i18n.locale == 'ar') {


                            if (this.dailyExpense.paymentMode == 0) {
                                this.dailyExpense.paymentMode = 'السيولة النقدية';
                            }
                            if (this.dailyExpense.paymentMode == 1) {
                                this.dailyExpense.paymentMode = 'مصرف';
                            }
                            else {
                                this.dailyExpense.paymentMode = '';
                            }
                        }
                        if ((this.$i18n.locale == 'en' || this.isLeftToRight())) {

                            if (this.dailyExpense.paymentMode == 0) {
                                this.dailyExpense.paymentMode = 'Cash';
                            }
                            if (this.dailyExpense.paymentMode == 1) {
                                this.dailyExpense.paymentMode = 'Bank';
                            }
                            else {
                                this.dailyExpense.paymentMode = '';
                            }

                        }
                        root.GetBankOpeningBalance(root.dailyExpense.accountId);

                        root.dailyExpense.dailyExpenseDetails = root.$route.query.data.dailyExpenseDetails;
                    }
                    root.render++;
                }
            }


            this.$emit('update:modelValue', this.$route.name);
        },
        
        mounted: function () {
            this.bankDetail = localStorage.getItem('BankDetail') == 'true' ? true : false;


        }
    })

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
<style>
    .test > .multiselect__content-wrapper {
        min-width: 350px !important;
    }
</style>