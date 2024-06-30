<template>
    <div class="row">
        <div class="col-lg-12">
            <div class="row">
                <div class="col">
                    <h5 class="page_title">{{ $t('DailyExpense.DailyExpense1') }}</h5>
                    <ol class="breadcrumb">
                        <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('DailyExpense.Home') }}</a></li>
                        <li class="breadcrumb-item active">
                            {{ $t('DailyExpense.DailyExpense1') }}
                        </li>
                    </ol>
                </div>

                <div class="col-auto align-self-center">

                    <a v-on:click="BackToList" href="javascript:void(0);"
                       class="btn btn-sm btn-outline-danger mx-1">
                        <i class="fas fa-arrow-circle-left fa-lg"></i>

                    </a>
                    <a v-on:click="GotoPage('/StartScreen')" href="javascript:void(0);"
                       class="btn btn-sm btn-outline-danger">
                        {{ $t('SaleOrder.Close') }}
                    </a>
                </div>
            </div>
        </div>
        <div class="col-xs-12 col-sm-9 col-md-9 col-lg-9">
            <div class="card">
                <div class="card-body">
                   



                    <div class="row" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? '' : 'arabicLanguage'">
                        <div class="col-lg-12">

                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                <label class="font-weight-bold">{{ $t('DailyExpenseView.Description') }}</label>
                                <div class="card" style="border: 1px #dddddd solid;">
                                    <div class="card-body">
                                        <label>{{dailyExpense.description}}</label>
                                    </div>
                                </div>
                            </div>

                        </div>
                        <div class="col-md-12">

                            <adddailyexpenserow :isDisable="'true'" :dailyExpenseRows="dailyExpenseDetails" :formName="formName"
                                                @update:modelValue="getupdatedailyExpenseRows"></adddailyexpenserow>

                        </div>
                        <div class="col-lg-12 mt-4 mb-5">
                            <div class="card">
                                <div class="card-body">
                                    <div class="row">
                                        <div class="col-lg-8" style="border-right: 1px solid #eee;">
                                            
                                        </div>
                                        <div class="col-lg-4">
                                            <div class="form-group ps-3">
                                                <div class="font-xs mb-1">{{ $t('DailyExpenseView.AttachFiles') }}</div>
                                                <button v-on:click="Attachment()" type="button" class="btn btn-light btn-square btn-outline-dashed mb-1"><i class="fas fa-cloud-upload-alt"></i> {{ $t('InvoiceView.Attachment') }} </button>
                                                <div>
                                                    <small class="text-muted">
                                                        {{ $t('DailyExpenseView.FileSize') }} 
                                                    </small>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12 ">
                                <button class="btn btn-sm btn-outline-danger" v-on:click="BackToList">
                                    {{ $t('InvoiceView.Cancel') }}
                                </button>
                            </div>

                        </div>

                    </div>

                </div>
            </div>
        </div>
        <div class="col-xs-12 col-sm-3 col-md-3 col-lg-3" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
            <div class="card">
                <div class="card-body">
                    <div class="row">
                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                            <h5 class="view_page_title">{{ $t('DailyExpenseView.BasicInfo') }}</h5>
                        </div>
                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                            <label class="invoice_lbl">{{ $t('DailyExpenseView.VoucherNo') }}#</label>
                            <hr style="margin-top: 0.3rem; margin-bottom: 0.1rem;" />
                            <label>{{dailyExpense.voucherNo}}</label>
                            <hr style="margin-top: 0.1rem;" />
                        </div>



                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 mt-2">
                            <label class="invoice_lbl">{{ $t('DailyExpenseView.SpentDate') }}</label>
                            <hr style="margin-bottom: 0.1rem; margin-top: 0.3rem;" />
                            <label>{{dailyExpense.expenseDate}}</label>
                            <hr style="margin-top: 0.1rem;" />
                        </div>

                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 mt-2" v-if="IsExpenseAccount">
                            <label class="invoice_lbl">  {{ $t('DailyExpenseView.PaymentMode') }}</label>
                            <hr style="margin-bottom: 0.1rem; margin-top: 0.3rem;" />
                            <label>{{dailyExpense.paymentMode}}</label>
                            <hr style="margin-top: 0.1rem;" />
                        </div>
                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 mt-2" v-if="IsExpenseAccount">
                            <label class="invoice_lbl">{{ $t('DailyExpenseView.Bank/CashAccount') }}</label>
                            <hr style="margin-bottom: 0.1rem; margin-top: 0.3rem;" />
                            <label> {{ ($i18n.locale == 'en' ||isLeftToRight()) ? (dailyExpense.accountName != '' && dailyExpense.accountName != null) ? dailyExpense.accountName : dailyExpense.nameArabic : (dailyExpense.nameArabic != '' && dailyExpense.nameArabic != null) ? dailyExpense.nameArabic : dailyExpense.accountName}}</label>
                            <hr style="margin-top: 0.1rem;" />
                        </div>
                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 mt-2">
                            <label class="invoice_lbl">{{ $t('DailyExpenseView.ReferenceNo') }}</label>
                            <hr style="margin-bottom: 0.1rem; margin-top: 0.3rem;" />
                            <label>{{dailyExpense.referenceNo}}</label>
                            <hr style="margin-top: 0.1rem;" />
                        </div>
                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 mt-2" v-if="IsExpenseAccount">
                            <label class="invoice_lbl">{{ $t('DailyExpenseView.NameEn') }}</label>
                            <hr style="margin-bottom: 0.1rem; margin-top: 0.3rem;" />
                            <label>{{dailyExpense.name}}</label>
                            <hr style="margin-top: 0.1rem;" />
                        </div>
                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 mt-2" v-if="IsExpenseAccount">
                            <label class="invoice_lbl">{{ $t('DailyExpenseView.TaxId') }}</label>
                            <hr style="margin-bottom: 0.1rem; margin-top: 0.3rem;" />
                            <label>{{dailyExpense.taxId}}</label>
                            <hr style="margin-top: 0.1rem;" />
                        </div>


                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-6 mt-2">
                            <button class="btn btn-light btn-block text-left">PDF <i class="fas fa-file-pdf float-right" style="color:#EB5757;"></i></button>
                        </div>
                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-6 mt-2">
                            <button class="btn btn-light btn-block text-left">Sheets <i class="fas fa-file-excel float-right" style="color:#198754;"></i></button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-12">
            <!--<div class="card" style="background-color:#EBF2FF;margin-bottom:0;">
            <div class="card-body">

                <div class="row">
                    <div class="col-xs-12 col-sm-6 col-md-6 col-lg-3 mb-2">
                        <label>{{ $t('Sale.Customer') }} :</label>
                        <div v-if="!sale.isCredit">{{($i18n.locale == 'en' ||isLeftToRight())?sale.cashCustomer:(sale.cashCustomer==''?sale.cashCustomer:sale.cashCustomer)}}</div> <div>{{($i18n.locale == 'en' ||isLeftToRight())?sale.customerNameEn:(sale.customerNameAr==''?sale.customerNameEn:sale.customerNameAr)}}</div>
                    </div>
                </div>
            </div>
        </div>-->

        </div>
        <bulk-attachment :documentid="dailyExpense.id" :show="isShow" v-if="isShow" @close="attachmentSave" />
        <loading v-model:active="loading" :can-cancel="true" :is-full-page="true"></loading>
    </div>

</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'
    import moment from 'moment';
     import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    // import { required, maxLength, requiredIf } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'
     import { required, maxLength, requiredIf } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'
    export default ({
        mixins: [clickMixin],
        props: ['formName'],
          setup() {
            return { v$: useVuelidate() }
        },
          components: {
            Loading
        },
        data: function () {
            return {
                loading: false,
                attachment: false,
                attachments: false,
                dailyExpenseDetails: [],
                BillerRecord: [],
                render: 0,
                language: 'Nothing',
                dailyExpense: {
                    id: '00000000-0000-0000-0000-000000000000',
                    voucherNo: '',
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
                    paymentMode: ''
                },
                CompanyID: '',
                UserID: '',
                employeeId: '',
                isDayAlreadyStart: false,
                IsExpenseAccount: false,
                show: false,
                isShow: false,
                lengthCount: 0,
                randerAccount: 0

            }
        },
        validations() {
           return{
             dailyExpense:
            {
                voucherNo:
                {
                    required,
                    maxLength: maxLength(30)
                },
                date:
                {
                    required,
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
            GotoPage: function (link) {
                this.$router.push({ path: link });
            },
            Attachment: function () {
                this.isShow = true;
            },

            attachmentSave: function () {
                this.isShow = false;
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
            getDate: function (date) {


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
                    title: root.$t('DailyExpenseView.SavedSuccessfully'),
                    text: root.$t('DailyExpenseView.Saved'),
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
            languageChange: function (lan) {
                if (this.language == lan) {
                    if (this.dailyExpense.id == '00000000-0000-0000-0000-000000000000') {

                        var getLocale = this.$i18n.locale;
                        this.language = getLocale;

                        this.$router.go('/adddailyexpense');
                    }
                    else {

                        this.$swal({
                            title: this.$t('DailyExpenseView.Error'),
                            text: this.$t('DailyExpenseView.ChangeLanguageError'),
                            type: 'error',
                            confirmButtonClass: "btn btn-danger",
                            icon: 'error',
                            timer: 4000,
                            timerProgressBar: true,
                        });
                    }
                }


            },
            getupdatedailyExpenseRows: function (items) {
                this.dailyExpenseDetails = items;
                this.dailyExpense.dailyExpenseDetails = items;
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
            BackToList: function () {
                this.$router.push({
                    path: '/dailyexpense',
                    query: {
                        data: 'AddDailyExpense',
                        formName: this.formName
                    }
                })
              
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
                if (this.dailyExpense.paymentMode == "السيولة النقدية") {
                    this.dailyExpense.paymentMode = 'Cash';
                }
                if (this.dailyExpense.paymentMode == "مصرف") {
                    this.dailyExpense.paymentMode = 'Bank';
                }
                //root.dailyExpense.date = root.dailyExpense.date + " " + moment().format("hh:mm A");
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
                            title: root.$t('DailyExpenseView.SavedSuccessfully'),
                            text: root.$t('DailyExpenseView.Saved'),
                            type: 'success',
                            confirmButtonClass: "btn btn-success",
                            buttonStyling: false,
                            icon: 'success',
                            timer: 1500,
                            timerProgressBar: true,

                        });
                        root.$router.push({
                            path: '/dailyexpense',
                            query: { data: root.dailyExpense.approvalStatus }
                        });
                    })
                    .catch(error => {
                        console.log(error)
                        this.$swal.fire(
                            {
                                icon: 'error',
                                title: this.$t('DailyExpenseView.Error'),
                                text: this.$t('DailyExpenseView.Error'),
                            });
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
                root.dailyExpense.date = root.dailyExpense.date + " " + moment().format("hh:mm A");
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
                            title: root.$t('DailyExpenseView.UpdateSuccessfully'),
                            text: root.$t('DailyExpenseView.Updated'),
                            type: 'success',
                            confirmButtonClass: "btn btn-success",
                            buttonStyling: false,
                            icon: 'success',
                            timer: 1500,
                            timerProgressBar: true,

                        });
                        this.$router.push('/dailyexpense')
                    })
                    .catch(error => {
                        console.log(error)
                        this.$swal.fire(
                            {
                                icon: 'error',
                                title: root.$t('DailyExpenseView.Error'),
                                text: root.$t('DailyExpenseView.SomethingWrong'),
                                showConfirmButton: false,
                                timer: 1000,
                                timerProgressBar: true,

                            });
                        root.errored = true
                    })
                    .finally(() => root.loading = false)
            },

        },
        created: function () {

            var root =  this;

            if(root.$route.query.Add == 'false')
            {
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


            //if (this.$route.query.data != undefined) {
            //    this.lengthCount = this.$route.query.data.dailyExpenseDetails.length;
            //}

            if (IsDayStart != 'true') {
                this.isDayAlreadyStart = true;
                if (this.$route.query.data == undefined) {
                    this.AutoIncrementVoucherNo();
                    this.dailyExpense.date = moment().format('llll');
                }
                if (this.$route.query.data != undefined) {

                    this.dailyExpense = this.$route.query.data;
                    if (this.formName == 'dailyexpense') {

                        this.dailyExpense.IsExpenseAccount = false;
                    }
                    else {
                        this.dailyExpense.IsExpenseAccount = localStorage.getItem('IsExpenseAccount') == 'true' ? true : false;
                    }
                    if (this.$i18n.locale == 'ar') {
                        if (this.dailyExpense.paymentMode == 0) 
                        {
                            this.dailyExpense.paymentMode = 'السيولة النقدية';
                        }
                        if (this.dailyExpense.paymentMode == 1) {
                            this.dailyExpense.paymentMode = 'مصرف';
                        }
                        else
                        {
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
                        else
                        {
                            this.dailyExpense.paymentMode = '';
                        }
                    }
                    this.dailyExpense.expenseDate = this.getDate(this.dailyExpense.expenseDate);
                    //this.dailyExpense.dailyExpenseDetails = this.$route.query.data.dailyExpenseDetails;

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
                        root.dailyExpense.date = moment().format('llll');
                        if (this.formName == 'dailyexpense') {

                            this.dailyExpense.IsExpenseAccount = false;
                        }
                        else {
                            this.dailyExpense.IsExpenseAccount = localStorage.getItem('IsExpenseAccount') == 'true' ? true : false;
                        }

                    }
                    if (root.$route.query.data != undefined) {

                        root.dailyExpense = root.$route.query.data;
                        root.dailyExpense.date = moment(root.dailyExpense.date).format('llll');
                        if (this.formName == 'dailyexpense') {

                            this.dailyExpense.IsExpenseAccount = false;
                        }
                        else {
                            this.dailyExpense.IsExpenseAccount = localStorage.getItem('IsExpenseAccount') == 'true' ? true : false;
                        }


                        if (this.$i18n.locale == 'ar') {


                            if (this.dailyExpense.paymentMode == 0) {
                                this.dailyExpense.paymentMode = 'السيولة النقدية';
                            }
                            if (this.dailyExpense.paymentMode == 1) {
                                this.dailyExpense.paymentMode = 'مصرف';
                            }
                            else
                            {
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
                            else
                            {
                                this.dailyExpense.paymentMode = '';
                            }

                        }

                        root.dailyExpense.dailyExpenseDetails = root.$route.query.data.dailyExpenseDetails;
                    }
                    root.render++;
                }
                else {
                    if (root.$route.query.data == undefined) {
                        root.AutoIncrementVoucherNo();
                        root.dailyExpense.date = moment().format('llll');
                        if (this.formName == 'dailyexpense') {

                            this.dailyExpense.IsExpenseAccount = false;
                        }
                        else {
                            this.dailyExpense.IsExpenseAccount = localStorage.getItem('IsExpenseAccount') == 'true' ? true : false;
                        }


                    }
                    if (root.$route.query.data != undefined) {

                        root.dailyExpense = root.$route.query.data;
                        root.dailyExpense.date = moment(root.dailyExpense.date).format('llll');

                        if (this.formName == 'dailyexpense') {

                            this.dailyExpense.IsExpenseAccount = false;
                        }
                        else {
                            this.dailyExpense.IsExpenseAccount = localStorage.getItem('IsExpenseAccount') == 'true' ? true : false;
                        }


                        if (this.$i18n.locale == 'ar') {


                            if (this.dailyExpense.paymentMode == 0) {
                                this.dailyExpense.paymentMode = 'السيولة النقدية';
                            }
                            if (this.dailyExpense.paymentMode == 1) {
                                this.dailyExpense.paymentMode = 'مصرف';
                            }
                            else
                            {
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
                            else
                            {
                                this.dailyExpense.paymentMode = '';
                            }
                        }

                        root.dailyExpense.dailyExpenseDetails = root.$route.query.data.dailyExpenseDetails;
                    }
                    root.render++;
                }
            }


            this.$emit('update:modelValue', this.$route.name);
        },

        mounted: function () {




        }
    })

</script>