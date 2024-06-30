<template>
    <div class="row" v-if="(isValid('CanViewDetailStockIn')  && formName=='StockIn') || (isValid('CanViewDetailStockOut')  && formName=='StockOut')  ">
        <div class="col-lg-12">
            <div class="row">
                <div class="col">
                    <h5 v-if="formName=='StockIn'" class="page_title">   {{ $t('ViewStock.StockIn') }}</h5>
                    <h5 v-else-if="formName=='StockProduction'" class="page_title">{{ $t('ViewStock.ProductionStock') }}</h5>
                    <h5 v-else class="page_title"> {{ $t('ViewStock.StockOut') }}</h5>
                    <ol class="breadcrumb">
                        <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('ViewStock.Home') }}</a></li>
                        <li class="breadcrumb-item active" v-if="formName=='StockIn'">
                            {{ $t('ViewStock.StockIn') }}
                        </li>
                        <li class="breadcrumb-item active" v-else-if="formName=='StockProduction'">
                            {{ $t('ViewStock.ProductionStock') }}
                        </li>
                        <li class="breadcrumb-item active" v-else>
                            {{ $t('ViewStock.StockOut') }}
                        </li>
                    </ol>
                </div>

                <div class="col-auto align-self-center">

                    <a v-on:click="onCancel" href="javascript:void(0);"
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
                <div class="card-body" :key="render">
                    <div class="row">
                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12" v-if="formName=='StockOut'">
                            <label class="fw-bold">{{ $t('ViewStock.Reason') }}</label>
                            <p> {{productDetail.reason}}</p>

                        </div>
                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                            <label class="fw-bold">{{ $t('ViewStock.Narration') }}:</label>
                            <p>   {{productDetail.narration}}</p>

                        </div>
                    </div>



                    <div class="row">
                        <div class="col-md-12">


                            <div class="row ">
                                <div class="col-lg-12 mt-5" v-bind:class="{'has-danger' : v$.productDetail.$error}">
                                    <viewstockitem @update:modelValue="getstockAdjustmentDetails" :formName="formName" :stockAdjustmentDetailss="stockAdjustmentDetails" :taxMethod="productDetail.taxMethod" :taxRateId="productDetail.taxRateId" />
                                    <span v-if="v$.productDetail.$error" class="error"></span>
                                </div>

                                <div class="col-lg-12 mt-4 mb-5">
                                    <div class="card">
                                        <div class="card-body">
                                            <div class="row">
                                                <div class="col-lg-8" style="border-right: 1px solid #eee;">
                                                    <div class="form-group pe-3">
                                                        
                                                    </div>
                                                </div>
                                                <div class="col-lg-4">
                                                    <div class="form-group ps-3">
                                                        <div class="font-xs mb-1">{{ $t('ViewStock.AttachFile') }}</div>
                                                        <button v-on:click="Attachment()" type="button" class="btn btn-light btn-square btn-outline-dashed mb-1"><i class="fas fa-cloud-upload-alt"></i> {{ $t('ViewStock.Attachment') }} </button>
                                                        <div>
                                                            <small class="text-muted">
                                                                {{ $t('ViewStock.FileSize') }}
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
                                <div class="col-md-6 ">
                                    <button type="button" class="btn btn-sm btn-outline-primary mx-1 " v-if="productDetail.stockAdjustmentDetails.length > 0 && ((isValid('CanPrintStockIn')  && formName=='StockIn') || (isValid('CanPrintStockOut')  && formName=='StockOut'))" v-on:click="PrintStock"><i class="fas fa-print"></i>  {{ $t('ViewStock.Print') }}</button>
                                    <button class="btn btn-danger btn-sm " v-on:click="onCancel">{{ $t('ViewStock.Cancel') }}</button>
                                </div>

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
                        <div class="col-lg-12">
                            <h5 class="view_page_title">{{ $t('ViewStock.BasicInfo') }}</h5>
                        </div>
                        <div class="col-lg-12 ">
                            <label class="invoice_lbl" v-if="formName=='StockIn'">{{ $t('ViewStock.StockIn') }}#</label>
                            <label class="invoice_lbl" v-else-if="formName=='StockProduction'">{{ $t('ViewStock.ProductionStock') }}#</label>
                            <label class="invoice_lbl" v-else>{{ $t('ViewStock.StockOut') }}#</label>
                            <hr style="margin-top: 0.3rem; margin-bottom: 0.1rem;" />
                            <label>{{productDetail.code}}</label>
                            <hr style="margin-top: 0.1rem;" />
                        </div>
                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                            <label class="invoice_lbl"> {{ $t('ViewStock.Warehouse') }}</label>
                            <hr style="margin-bottom: 0.1rem; margin-top: 0.3rem;" />
                            <label> {{productDetail.warehouseName}}</label>
                            <hr style="margin-top: 0.1rem;" />
                        </div>

                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 mt-2">
                            <label class="invoice_lbl">  {{ $t('ViewStock.TaxMethod') }}</label>
                            <hr style="margin-bottom: 0.1rem; margin-top: 0.3rem;" />
                            <label> {{ productDetail.taxMethod }}</label>
                            <hr style="margin-top: 0.1rem;" />
                        </div>
                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 mt-2">
                            <label class="invoice_lbl">  {{ $t('ViewStock.VAT%') }}</label>
                            <hr style="margin-bottom: 0.1rem; margin-top: 0.3rem;" />
                            <label> {{productDetail.taxRateName}}</label>
                            <hr style="margin-top: 0.1rem;" />
                        </div>
                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 mt-2">
                            <label class="invoice_lbl">{{ $t('ViewStock.SendCopyTo') }}</label>
                            <hr style="margin-bottom: 0.1rem; margin-top: 0.3rem;" />
                            <label>{{ $t('ViewStock.Email') }}</label>
                            <hr style="margin-top: 0.1rem;" />
                        </div>
                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 mt-2">
                            <button class="btn btn-light btn-block text-left">{{ $t('ViewStock.SendStockReport') }}</button>
                        </div>
                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-6 mt-2">
                            <button class="btn btn-light btn-block text-left">{{ $t('ViewStock.PDF') }} <i class="fas fa-file-pdf float-right" style="color:#EB5757;"></i></button>
                        </div>
                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-6 mt-2">
                            <button class="btn btn-light btn-block text-left">{{ $t('ViewStock.Sheets') }} <i class="fas fa-file-excel float-right" style="color:#198754;"></i></button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-12">


            <bulk-attachment :documentid="productDetail.id" :show="show" v-if="show" @close="attachmentSave" />
        </div>

    </div>
    <div v-else> <acessdenied></acessdenied></div>

</template>


<script>
    import clickMixin from '@/Mixins/clickMixin'
    import { required } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core';
    import moment from "moment";
    export default {
        mixins: [clickMixin],
        name: "AddCheckOut",
        props: ['formName'],
        setup() {
            return { v$: useVuelidate() }
        },
        data: function () {
            return {
                stockAdjustmentDetails: [],
                render: 0,
                productDetail: {
                    id: '00000000-0000-0000-0000-000000000000',
                    date: '',
                    narration: '',
                    code: '',
                    warehouseId: '',
                    isDraft: true,
                    stockAdjustmentType: '',
                    stockAdjustmentDetails: [],
                    taxMethod: '',
                    taxRateId: "",
                    reason: '',
                },
                loading: false,
                language: 'Nothing',
                options: [],
                reasonOptions: [],
                headerFooter: {
                    company: '',
                    isStockIn: true

                },
                showPrint: false,
                show: false,
                printRender: 0
            }
        },
        validations() {
            return {
                productDetail: {
                    date: {
                        required
                    },
                    code: {
                        required
                    },
                    warehouseId: {
                        required
                    }
                }
                }
        },
        methods: {
            Attachment: function () {
                this.show = true;
            },

            attachmentSave: function () {
                this.show = false;
            },
            GotoPage: function (link) {
                this.$router.push({ path: link });
            },

            languageChange: function (lan) {
                if (this.language == lan) {
                    if (this.productDetail.id == '00000000-0000-0000-0000-000000000000') {

                        var getLocale = this.$i18n.locale;
                        this.language = getLocale;

                        this.$router.go('/addSale');
                    }
                    else {
                        this.$swal({
                            title: (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Error!' : 'خطأ',
                            text:(this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'You cannot Change language during update, otherwise your current page data will be lose!' : 'لا يمكنك تغيير اللغة أثناء التحديث ، وإلا ستفقد بيانات صفحتك الحالية!',
                            type: 'error',
                            confirmButtonClass: "btn btn-danger",
                            icon: 'error',
                            timer: 4000,
                            timerProgressBar: true,
                        });
                    }
                }


            },
            PrintStock: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                root.$https.get('/Product/StockAdjustmentDetails?id=' + this.productDetail.id, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {

                        root.productDetail = response.data
                        root.showPrint = true;
                        root.printRender++

                    }
                });

            },

            onCancel: function () {
                var root = this;
                root.$router.push({
                    path: '/stockValue', 
                    query: {
                        data: 'StockValues' + root.formName,
                        formName : root.formName,
                    }
                })
            },
            getstockAdjustmentDetails: function (stockAdjustmentDetails) {

                this.productDetail.stockAdjustmentDetails = stockAdjustmentDetails;
            },
            GetHeaderDetail: function () {
                var root = this;
                if (this.formName == 'StockIn')
                    this.headerFooter.isStockIn = true;
                else
                    this.headerFooter.isStockIn = false;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get("/Company/GetCompanyDetail?id=" + localStorage.getItem('CompanyID'), { headers: { Authorization: `Bearer ${token}` }, })
                    .then(function (response) {
                        if (response.data != null) {
                            root.headerFooter.company = response.data;
                        }
                    });
            },
        },
        watch: {
            formName: function () {
                if (this.formName == 'StockIn') {
                    if (this.$route.query.data != undefined) {
                        this.render++;
                        this.productDetail = this.$route.query.data.message;
                        this.productDetail.date = moment(this.productDetail.date).format("LLL");
                        this.productDetail.stockAdjustmentType = this.formName;
                    }
                }
                if (this.formName == 'StockProduction') {
                    if (this.$route.query.data != undefined) {
                        this.render++;
                        this.productDetail = this.$route.query.data.message;
                        this.productDetail.date = moment(this.productDetail.date).format("LLL");
                        this.productDetail.stockAdjustmentType = this.formName;
                    }
                }
                if (this.formName == 'StockOut') {
                    if (this.$route.query.data != undefined) {
                        this.render++;
                        this.productDetail = this.$route.query.data.message;
                        this.productDetail.date = moment(this.productDetail.date).format("LLL");
                        this.productDetail.stockAdjustmentType = this.formName;
                    }
                }
            },

        },
        mounted: function () {
            var root = this;
            if(root.$route.query.Add == 'false')
            {
                root.$route.query.data = this.$store.isGetEdit;
            }
            this.GetHeaderDetail()
            this.showPrint = false;
            this.language = this.$i18n.locale;
            if ((this.$i18n.locale == 'en' || this.isLeftToRight())) {
                this.options = ['Inclusive', 'Exclusive'];
                this.reasonOptions = ['Damaged', 'Expire', 'Lose'];
            }
            else {
                this.options = ['شامل', 'غير شامل'];
                this.reasonOptions = ['تالف', 'تنقضي', 'تخسر'];
            }
            this.productDetail.date = moment().format("DD MMM YYYY");
            if (this.formName == 'StockIn') {
                if (this.$route.query.data != undefined) {
                    this.render++;
                    this.productDetail = this.$route.query.data;

                    this.productDetail.date = moment(this.productDetail.date).format("LLL");
                    this.productDetail.stockAdjustmentType = this.formName;
                }
            }
            if (this.formName == 'StockOut') {
                if (this.$route.query.data != undefined) {

                    this.productDetail = this.$route.query.data;
                    this.productDetail.date = moment(this.productDetail.date).format("LLL");
                    this.productDetail.stockAdjustmentType = this.formName;
                    this.render++;
                }
            }
            if (this.formName == 'StockProduction') {
                if (this.$route.query.data != undefined) {
                    this.render++;
                    this.productDetail = this.$route.query.data;
                    this.productDetail.date = moment(this.productDetail.date).format("LLL");

                    this.productDetail.stockAdjustmentType = this.formName;
                }
            }
        }
    }
</script>