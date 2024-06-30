 <template>
    <div v-if="isValid('CanDraftExpenseBill') || isValid('CanAddExpenseBill') || isValid('CanEditExpenseBill')">
        <div class="row">
            
            <div class="col-md-12 ">
                <div class="col d-flex align-items-baseline">
                    <div class="media">
                        <span class="circle-singleline" style="background-color: #1761FD !important;margin:10px !important">BI</span>
                        <div class="media-body align-self-center ms-3">
                            <h6 class="m-0 font-20" v-if="purchaseBill.id != '00000000-0000-0000-0000-000000000000'"> {{ $t('Update Expense Bill/ Expense Document')}}</h6>
                            <h6 class="m-0 font-20" v-else> {{ $t('AddPurchaseBill.Bills')}}</h6>
                            <div class="col d-flex ">
                                <p class="text-muted mb-0" style="font-size:13px !important;"><b>{{ purchaseBill.registrationNo }}</b> &nbsp;&nbsp; <span>{{ purchaseBill.date }}</span></p>
                            </div>
                        </div>
                    </div>
                </div>
                <hr class="hr-dashed hr-menu mt-0" />
                <div>
                    <div>
                        <div class="row">
                            <div class="col-lg-6">
                               
                                <div class="row form-group">
                                    <label class="col-form-label col-lg-4">
                                        <span class="tooltip-container text-dashed-underline "> {{
                                            $t('AddPurchaseBill.DueDate') }}: <span class="text-danger">*</span></span>
                                    </label>
                                    <div v-bind:class="{ 'has-danger': v$.purchaseBill.dueDate.$error }"
                                        class="inline-fields col-lg-8">
                                        <datepicker v-model="v$.purchaseBill.dueDate.$model"></datepicker>
                                    </div>
                                </div>
                                <div class="row form-group">
                                    <label class="col-form-label col-lg-4">
                                        <span class="tooltip-container text-dashed-underline "> {{
                                            $t('AddPurchaseBill.Reference') }}: </span>
                                    </label>
                                    <div class="inline-fields col-lg-8 ">
                                        <input class="form-control" v-model="purchaseBill.reference" />
                                    </div>
                                </div>

                            </div>
                            <div class="col-lg-6">
                                <div class="row form-group">
                                    <label class="col-form-label col-lg-4">
                                        <span class="tooltip-container text-dashed-underline "> {{
                                            $t('AddPurchaseBill.Date') }} : <span class="text-danger">*</span></span>
                                    </label>
                                    <div class="inline-fields col-lg-8">
                                        <datepicker :key="render" v-model="purchaseBill.billDate"></datepicker>
                                    </div>
                                </div>
                                <div class="row form-group">
                                    <label class="col-form-label col-lg-4">
                                        <span class="tooltip-container text-dashed-underline "> {{
                                            $t('AddPurchaseBill.Account') }} :</span>
                                    </label>
                                    <div class="inline-fields col-lg-8">
                                        <accountdropdown v-model="purchaseBill.billerId"></accountdropdown>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-lg-12 col-md-12 col-sm-12">
                                <PurchaseBillItem v-on:updatedailyExpenseRows="getupdatedailyExpenseRows">
                                </PurchaseBillItem>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-lg-12 mt-4 mb-5">
                                <div class="card">
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-lg-8" style="border-right: 1px solid #eee;">
                                                <div class="form-group pe-3">
                                                    <label>{{ $t('AddPurchaseBill.Narration') }}:</label>
                                                    <div v-bind:class="{ 'has-danger': v$.purchaseBill.narration.$error }">
                                                        <textarea class="form-control " rows="3" autofocus="autofocus"
                                                            v-model="purchaseBill.narration" />
                                                    </div>
                                                    <span v-if="v$.purchaseBill.narration.$error" class="error text-danger">
                                                        <span v-if="!v$.purchaseBill.narration.maxLength">{{
                                                            $t('AddPurchaseBill.DescriptionMaximumlength') }}</span>
                                                    </span>
                                                </div>
                                            </div>
                                            <div class="col-lg-4"
                                                v-if="purchaseBill.id == null || purchaseBill.id == '00000000-0000-0000-0000-000000000000'">
                                                <div class="form-group ps-3">
                                                    <div class="font-xs mb-1">{{ $t('AddPurchaseBill.AttachFile') }} </div>

                                                    <button v-on:click="Attachment()"
                                                        v-if="isValid('CanUploadExpenseBillAttachment')" type="button"
                                                        class="btn btn-light btn-square btn-outline-dashed mb-1"><i
                                                            class="fas fa-cloud-upload-alt"></i> {{
                                                                $t('AddPurchaseBill.Attachment') }} </button>

                                                    <div>
                                                        <small class="text-muted">
                                                            {{ $t('AddPurchaseBill.FileSize') }}
                                                        </small>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-4" v-else>
                                                <div class="form-group ps-3">
                                                    <div class="font-xs mb-1">Attach File(s) </div>

                                                    <button v-on:click="Attachment()"
                                                        v-if="isValid('CanUploadExpenseBillAttachment')" type="button"
                                                        class="btn btn-light btn-square btn-outline-dashed mb-1"><i
                                                            class="fas fa-cloud-upload-alt"></i> {{
                                                                $t('AddPurchaseBill.Attachment') }} </button>

                                                    <div>
                                                        <small class="text-muted">
                                                            You can upload a maximum of 10 files, 5MB each
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
                                <div
                                    v-if="purchaseBill.id == null || purchaseBill.id == '00000000-0000-0000-0000-000000000000'">

                                    <button class="btn btn-outline-primary  mr-2 "
                                        v-on:click="SaveDailyExpenseInformation('Draft')"
                                        v-if="isValid('CanDraftExpenseBill')"
                                        :disabled="v$.purchaseBill.$invalid || purchaseBill.purchaseBillItems.filter(x => x.description == '' && x.amount == 0 && x.accountId == '').length > 0"><i
                                            class="far fa-save"></i> {{ $t('AddPurchaseBill.Save') }}</button>

                                    <button class="btn btn-outline-primary  mr-2 "
                                        v-on:click="SaveDailyExpenseInformation('Approved')"
                                        v-if="isValid('CanAddExpenseBill')"
                                        :disabled="v$.purchaseBill.$invalid || purchaseBill.purchaseBillItems.filter(x => x.description == '' && x.amount == 0 && x.accountId == '').length > 0"><i
                                            class="far fa-save"></i> {{ $t('AddPurchaseBill.SaveAndpost') }}</button>

                                    <button class="btn btn-danger " v-on:click="BackToList()">{{
                                        $t('AddPurchaseBill.Cancel') }}</button>

                                </div>
                                <div v-else>

                                    <button class="btn btn-outline-primary  mr-2 " v-if="purchaseBill.approvalStatus != 3"
                                        v-on:click="SaveDailyExpenseInformation('Draft')" :disabled="v$.purchaseBill.$invalid"><i
                                            class="far fa-save"></i> {{ $t('AddPurchaseBill.Update') }}</button>
                                    <button class="btn btn-outline-primary  mr-2 "
                                        v-on:click="SaveDailyExpenseInformation('Approved')" :disabled="v$.purchaseBill.$invalid"><i
                                            class="far fa-save"></i> {{ $t('AddPurchaseBill.Updateandpost') }}</button>

                                    <button class="btn btn-danger " v-on:click="BackToList()">{{
                                        $t('AddPurchaseBill.Cancel') }}</button>

                                </div>
                            </div>
                        </div>

                    </div>
                </div>

            </div>
        </div>
        <bulk-attachment :attachmentList="purchaseBill.attachmentList" :show="show" v-if="show" @close="attachmentSave" />
        <loading v-model:active="loading" :can-cancel="true" :is-full-page="true"></loading>
    </div>
    <div v-else>
        <acessdenied></acessdenied>
    </div>
</template>
<script>
import clickMixin from '@/Mixins/clickMixin'
import moment from 'moment';
//import Multiselect from 'vue-multiselect'
    import { required, maxLength } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'
    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
export default ({
    mixins: [clickMixin],
    components: {
            Loading
        },
        setup() {
            return { v$: useVuelidate() }
        },
    data: function () {
        return {
            loading: false,
            purchaseBillItems: [],
            render: 0,
            show: false,
            attachment: false,
            attachments: false,
            language: 'Nothing',
            options: [],
            purchaseBill: {
                id: '00000000-0000-0000-0000-000000000000',
                registrationNo: '',
                date: '',
                dueDate: '',
                taxMethod: '',
                reference: '',
                billerId: '',
                billDate:'',
                purchaseBillItems: [],
                billAttachments: [],
                attachmentList: [],
                branchId: '',
            },

        }
    },
        validations() {
            return {
                purchaseBill:
                {
                    registrationNo:
                    {
                        required,
                        maxLength: maxLength(30)
                    },
                    dueDate:
                    {
                        required,
                    },
                    narration:
                    {
                    },
                    purchaseBillItems:
                    {
                        required,
                    },


                }
                }
    },
    methods: {
        Attachment: function () {
            this.show = true;
        },

        attachmentSave: function (attachment) {
            this.purchaseBill.attachmentList = attachment;
            this.show = false;
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
        getDate: function (date) {
            return moment(date).format('DD MMM YYYY hh:mm A');
        },

        billAttachments: function (x) {

            var root = this;
            if (x != undefined && x != null && x != '') {
                this.purchaseBill.billAttachments.push({
                    path: x.path,
                    date: x.date,
                    description: x.description
                })
            }
            this.attachments = false;
            this.$swal.fire({
                title: root.$t('AddPurchaseBill.SavedSuccessfully'),
                text: root.$t('AddPurchaseBill.Saved'),
                type: 'success',
                confirmButtonClass: "btn btn-success",
                buttonStyling: false,
                icon: 'success',
                timer: 1500,
                timerProgressBar: true,

            });
        },
        languageChange: function (lan) {
            if (this.language == lan) {
                if (this.purchaseBill.id == '00000000-0000-0000-0000-000000000000') {

                    var getLocale = this.$i18n.locale;
                    this.language = getLocale;

                    this.$router.go('/');
                }
                else {

                    this.$swal({
                        title: this.$t('AddPurchaseBill.Error'),
                        text: this.$t('AddPurchaseBill.ChangeLanguageError'),
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
            this.purchaseBillItems = items;
            this.purchaseBill.purchaseBillItems = items;
        },
        AutoIncrementVoucherNo: function () {
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            root.$https
                .get('/Purchase/PurchaseBillAutoGenerateNo?branchId=' + localStorage.getItem('BranchId'), { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {
                        root.purchaseBill.registrationNo = response.data;
                    }
                });
        },
        BackToList: function () {
            if (this.isValid('CanViewExpenseBill') || this.isValid('CanDraftExpenseBill')) {
                this.$router.push({
                    path: '/PurchaseBill',
                    query: {
                        data: 'PurchaseBills'
                    }
                })
            }
            else {
                this.$router.go();
            }


        },
        SaveDailyExpenseInformation: function (value) {
            this.loading = true;
            var root = this;
            this.purchaseBill.approvalStatus = value
            localStorage.setItem('active', value);

            var url = '/Purchase/SavePurchaseBillInformation';
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            var prd = root.purchaseBill.purchaseBillItems.findIndex(x => x.amount == 0);
            if (prd >= 0) {
                root.purchaseBill.purchaseBillItems.splice(prd, 1)
            }
            
            root.purchaseBill.billDate = root.purchaseBill.billDate + " " + moment().format("hh:mm A");
            root.purchaseBill.dueDate = root.purchaseBill.dueDate + " " + moment().format("hh:mm A");
            root.purchaseBill.branchId = localStorage.getItem('BranchId');

            root.$https
                .post(url, root.purchaseBill, { headers: { "Authorization": `Bearer ${token}` } })
                .then(response => {
                    root.info = response.data.bpi
                    this.$swal.fire({
                        title: root.$t('SavedSuccessfully'),
                        text: root.$t('Saved'),
                        type: 'success',
                        confirmButtonClass: "btn btn-success",
                        buttonStyling: false,
                        icon: 'success',
                        timer: 1500,
                        timerProgressBar: true,

                    });
                    if (this.isValid('CanViewExpenseBill') || this.isValid('CanDraftExpenseBill')) {
                        this.$router.push({
                            path: '/PurchaseBill',
                            query: {
                                data: 'PurchaseBills'
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
                            title: this.$t('AddPurchaseBill.Error'),
                            text: this.$t('AddPurchaseBill.Error'),
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

        if (this.$route.query.data != undefined) {

            this.purchaseBill = this.$route.query.data;

            this.purchaseBill.date = this.getDate(this.purchaseBill.date);
        }


        this.$emit('update:modelValue', this.$route.name);
    },

    mounted: function () {


        if (this.$route.query.data == undefined) {
            this.AutoIncrementVoucherNo();
            this.purchaseBill.date = moment().format('DD MMM YYYY hh:mm A');
            this.purchaseBill.billDate = moment().format('LLL');
            this.render++;
        }



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