<template>
    <div class="row">
        <div class="col-xs-12 col-sm-9 col-md-9 col-lg-9">
            <div>
                <div>
                    <div class="row">
                        <div class="col-xs-8 col-sm-8 col-md-8 col-lg-8">
                            <h5 class="view_page_title"> {{ $t('PurchaseBillView.Bills') }}</h5>
                        </div>
                        <div class="col-xs-4 col-sm-4 col-md-4 col-lg-4 text-end mt-3">

                            <a href="javascript:void(0)" class="btn btn-sm btn-outline-danger" v-on:click="GotoPage('/StartScreen')">  close </a>
                           

                        </div>
                    </div>

                    <div class="row">
                        <div class="col-lg-12">
                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                <label class="font-weight-bold">{{ $t('PurchaseBillView.Reference') }}</label>
                                <p>{{purchaseBill.reference}}</p>
                            </div>


                        </div>
                        <div class="col-md-12">

                            <PurchaseBillItem :isDisable="'true'" v-on:updatedailyExpenseRows="getupdatedailyExpenseRows"></PurchaseBillItem>

                        </div>
                        <!--narration part-->
                        <div class="row">
                            <div class="col-lg-12 mt-4 mb-5">
                                <div class="card">
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-lg-8" style="border-right: 1px solid #eee;">
                                                <div class="form-group pe-3">
                                                    <label>{{ $t('PurchaseBillView.Narration') }}:</label>
                                                    <textarea class="form-control " readonly rows="3" v-model="purchaseBill.narration" />
                                                </div>
                                            </div>
                                            <div class="col-lg-4">
                                                <div class="form-group ps-3">
                                                    <div class="font-xs mb-1">{{ $t('PurchaseBillView.AttachFile') }}</div>
                                                    <button v-on:click="Attachment()" v-if="isValid('CanUploadExpenseBillAttachment')" type="button" class="btn btn-light btn-square btn-outline-dashed mb-1"><i class="fas fa-cloud-upload-alt"></i> {{ $t('PurchaseView.Attachment') }} </button>
                                                    <div>
                                                        <small class="text-muted">
                                                            {{ $t('PurchaseBillView.FileSize') }}
                                                        </small>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                           
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <button class="btn btn-danger "
                                        v-on:click="BackToList">
                                    {{ $t('PurchaseOrderView.Cancel') }}
                                </button>
                            </div>
                        </div>
                                        
                                        

                                     
                                    </div>

                                </div>
                            </div>
                        </div>
                        <div class="col-xs-12 col-sm-3 col-md-3 col-lg-3">
                            <div class="card">
                                <div class="card-body">
                                    <div class="row">
                                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                            <h5 class="view_page_title">{{ $t('PurchaseBillView.BasicInfo') }}</h5>
                                        </div>
                                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                            <label class="invoice_lbl">{{ $t('PurchaseBillView.BillNo') }}#</label>
                                            <hr style="margin-top: 0.3rem; margin-bottom: 0.1rem;" />
                                            <label>{{purchaseBill.registrationNo}}</label>
                                            <hr style="margin-top: 0.1rem;" />
                                        </div>



                                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 mt-2">
                                            <label class="invoice_lbl">{{ $t('Bill Date') }}</label>
                                            <hr style="margin-bottom: 0.1rem; margin-top: 0.3rem;" />
                                            <label>{{purchaseBill.billDate}}</label>
                                            <hr style="margin-top: 0.1rem;" />
                                        </div>
                                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 mt-2">
                                            <label class="invoice_lbl">{{ $t('PurchaseBillView.DueDate') }}</label>
                                            <hr style="margin-bottom: 0.1rem; margin-top: 0.3rem;" />
                                            <label>{{purchaseBill.dueDate}}</label>
                                            <hr style="margin-top: 0.1rem;" />
                                        </div>
                                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 mt-2">
                                            <label class="invoice_lbl">{{ $t('PurchaseBillView.Account') }}</label>
                                            <hr style="margin-bottom: 0.1rem; margin-top: 0.3rem;" />
                                            <label>{{purchaseBill.billerAccount}}</label>
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
                            <bulk-attachment :documentid="purchaseBill.id" :show="isAttachshow" v-if="isAttachshow" @close="attachmentSaved" />
                            <loading v-model:active="loading" :can-cancel="true" :is-full-page="true"></loading>

                        </div>

                    </div>

</template>

<script>
    import clickMixin from '@/Mixins/clickMixin'
    import moment from 'moment';
    //import Multiselect from 'vue-multiselect'
    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    import { required, maxLength } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'
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
                isAttachshow: false,
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
                    purchaseBillItems: [],
                    billAttachments: [],
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
                    date:
                    {
                        required,
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
            GotoPage: function (link) {
                this.$router.push({ path: link });
            },
            Attachment: function () {
                this.isAttachshow = true;
            },

            attachmentSaved: function () {
                this.isAttachshow = false;
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


                return moment(date).format('LLL');

            },

            billAttachments: function (x) {
                this.loading = true;
                var root = this;
                if (x != undefined && x != null && x != '') {
                    this.purchaseBill.billAttachments.push({
                        path: x.path,
                        date: x.date,
                        description: x.description
                    })
                }
                this.attachments = false;
                var url = '/Purchase/SavePurchaseBillInformation';
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }


                root.$https
                    .post(url, root.purchaseBill, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(response => {
                        root.info = response.data.bpi
                        this.$swal.fire({
                            title: root.$t('PurchaseBillView.SavedSuccessfully'),
                            text: root.$t('PurchaseBillView.Saved'),
                            type: 'success',
                            confirmButtonClass: "btn btn-success",
                            buttonStyling: false,
                            icon: 'success',
                            timer: 1500,
                            timerProgressBar: true,

                        });
                    })
                    .catch(error => {
                        console.log(error)
                        this.$swal.fire(
                            {
                                icon: 'error',
                                title: this.$t('PurchaseBillView.Error'),
                                text: this.$t('PurchaseBillView.Error'),
                            });
                        root.errored = true
                    })
                    .finally(() => root.loading = false)
            },
            languageChange: function (lan) {
                if (this.language == lan) {
                    if (this.purchaseBill.id == '00000000-0000-0000-0000-000000000000') {

                        var getLocale = this.$i18n.locale;
                        this.language = getLocale;

                        this.$router.go('/adddailyexpense');
                    }
                    else {

                        this.$swal({
                            title: this.$t('PurchaseBillView.Error'),
                            text: this.$t('PurchaseBillView.ChangeLanguageError'),
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
                    .get('/Purchase/PurchaseBillAutoGenerateNo', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                        if (response.data != null) {
                            root.purchaseBill.registrationNo = response.data;
                        }
                    });
            },
            BackToList: function () {
                this.$router.push({
                    path: '/PurchaseBill',
                    query: {
                        data: 'PurchaseBills'
                    }
                })
            },
            SaveDailyExpenseInformation: function (value) {
                this.loading = true;
                var root = this;
                this.purchaseBill.approvalStatus = value
                var url = '/Purchase/SavePurchaseBillInformation';
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                var prd = root.purchaseBill.purchaseBillItems.findIndex(x => x.amount == 0);
                if (prd >= 0) {
                    root.purchaseBill.purchaseBillItems.splice(prd, 1)
                }
                root.purchaseBill.date = root.purchaseBill.date + " " + moment().format("hh:mm A");
                root.purchaseBill.dueDate = root.purchaseBill.dueDate + " " + moment().format("hh:mm A");

                root.$https
                    .post(url, root.purchaseBill, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(response => {
                        root.info = response.data.bpi
                        this.$swal.fire({
                            title: root.$t('PurchaseBillView.SavedSuccessfully'),
                            text: root.$t('PurchaseBillView.Saved'),
                            type: 'success',
                            confirmButtonClass: "btn btn-success",
                            buttonStyling: false,
                            icon: 'success',
                            timer: 1500,
                            timerProgressBar: true,

                        });
                        this.$router.push('/PurchaseBill')
                    })
                    .catch(error => {
                        console.log(error)
                        this.$swal.fire(
                            {
                                icon: 'error',
                                title: this.$t('PurchaseBillView.Error'),
                                text: this.$t('PurchaseBillView.Error'),
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
                this.purchaseBill.billDate = moment(this.purchaseBill.billDate).format('llll');
                this.purchaseBill.dueDate = moment(this.purchaseBill.dueDate).format('llll');

            }


            this.$emit('update:modelValue', this.$route.name);
        },

        mounted: function () {


            if (this.$route.query.data == undefined) {
                this.AutoIncrementVoucherNo();
                this.purchaseBill.date = moment().format('llll');
                this.render++;
            }



        }
    })

</script>