<template>
    <div class="row" v-if="((isValid('CanAddStockIn') || isValid('CanEditStockIn') || isValid('CanDraftStockIn')) && formName=='StockIn') || ((isValid('CanAddStockOut') || isValid('CanEditStockOut') || isValid('CanDraftStockOut')) && formName=='StockOut')">
        <div class="col-md-12 ml-auto mr-auto">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 v-if="formName=='StockIn'" class="page-title">
                                    {{ $t('AddStockValue.StockIn') }}
                                </h4>
                                <h4 v-else-if="formName=='StockProduction'" class="page-title">
                                    {{ $t('AddStockValue.ProductionStock') }}
                                </h4>
                                <h4 v-else class="page-title">{{ $t('AddStockValue.StockOut') }}</h4>
                            </div>
                            <div class="col-auto align-self-center">
                                <a v-on:click="GotoPage('/StartScreen')" href="javascript:void(0);"
                                   class="btn btn-sm btn-outline-danger">
                                    {{ $t('Sale.Close') }}
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <hr class="hr-dashed hr-menu mt-0" />
            <div class="row">
                <div class="col-lg-6">
                    <div class="row form-group">
                        <label class="col-form-label col-lg-4">
                            <span id="ember695" class="tooltip-container text-dashed-underline ">
                                <span v-if="formName=='StockIn'">{{ $t('AddStockValue.StockInNo') }}:</span>
                                <span v-else-if="formName=='StockProduction'">{{ $t('AddStockValue.ProductionStockNo') }}:</span>
                                <span v-else>{{ $t('AddStockValue.StockOutNo') }}:</span><span class="text-danger">*</span>
                            </span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <input v-model="productDetail.code" disabled class="form-control" type="text">
                        </div>
                    </div>

                    <div class="row form-group">
                        <label class="col-form-label col-lg-4" v-bind:key="randerInput">
                            <span class="tooltip-container text-dashed-underline "> {{ $t('AddStockValue.Date') }}  <span class="text-danger">*</span></span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <datepicker v-model="productDetail.date" />
                        </div>
                    </div>


                    <div class="row form-group">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline " v-if="formName=='StockOut'">{{ $t('AddStockValue.ExpenseAccount') }}:</span>
                            <span class="tooltip-container text-dashed-underline " v-else>{{ $t('AddStockValue.Account') }}</span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <accountdropdown v-model="productDetail.bankCashAccountId" :key="accountRender" />
                        </div>
                    </div>
                    <div class="row form-group" v-if="formName=='StockOut'">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline ">
                                {{ $t('AddStockValue.Reason') }}
                            </span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <multiselect :options="reasonOptions" v-model="productDetail.reason" :show-labels="false" v-bind:placeholder="$t('SelectType')" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                            </multiselect>
                        </div>
                    </div>



                </div>
                <div class="col-lg-6">


                    <div class="row form-group">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline ">
                                {{ $t('AddPurchase.WareHouse') }} :<span class="text-danger">
                                    *
                                </span>
                            </span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <div class="" v-bind:class="{ 'has-danger': v$.productDetail.warehouseId.$error}">
                                <warehouse-dropdown v-model="productDetail.warehouseId" :modelValue="productDetail.warehouseId" @update:modelValue="GetItemOnWarehouse"></warehouse-dropdown>
                                <span v-if="v$.productDetail.warehouseId.$error" class="error text-danger">
                                    <span v-if="!v$.productDetail.warehouseId.required">{{ $t('AddStockValue.WarehouseRequired') }}</span>
                                </span>
                            </div>
                        </div>
                    </div>
                    <div class="row form-group">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline ">
                                {{ $t('AddPurchase.TaxMethod') }} :<span class="text-danger"> *</span>
                            </span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <multiselect :options="options" v-model="productDetail.taxMethod" v-bind:disabled="productDetail.stockAdjustmentDetails.length>0" :show-labels="false" v-bind:placeholder="$t('AddStockValue.SelectMethod')" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                            </multiselect>
                        </div>
                    </div>
                    <div class="row form-group">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline ">
                                {{ $t('AddAutoPurchaseTemplate.VAT') }} :<span class="text-danger"> *</span>
                            </span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <taxratedropdown v-model="productDetail.taxRateId" :isDisable="productDetail.stockAdjustmentDetails.length>0? true :false" />
                        </div>
                    </div>


                    <!--<div class="row form-group">
                <label class="col-form-label col-lg-4">
                </label>
                <div class="inline-fields col-lg-8">
                    <div class="checkbox form-check-inline mx-2">
                        <input type="checkbox" id="inlineCheckbox1" v-model="purchase.isActive"
                               @change="ChangeSupplier">
                        <label for="inlineCheckbox1"> {{ $t('AddAutoPurchaseTemplate.Status') }} </label>
                    </div>

                </div>
            </div>-->
                </div>

                <stocklinedetail @update:modelValue="getstockAdjustmentDetails" :formName="formName" :stockAdjustmentDetailss="stockAdjustmentDetails" :wareHouseId="productDetail.warehouseId" :taxMethod="productDetail.taxMethod" :taxRateId="productDetail.taxRateId" :key="itemRender" />


                <div class="col-lg-12 invoice-btn-fixed-bottom">
                    <div class="button-items" v-if="productDetail.stockAdjustmentDetails.filter(x => x.outOfStock).length == 0 && productDetail.stockAdjustmentDetails.length > 0 && productDetail.warehouseId !='' && productDetail.stockAdjustmentDetails[productDetail.stockAdjustmentDetails.length - 1].price!=0 && productDetail.stockAdjustmentDetails[productDetail.stockAdjustmentDetails.length - 1].totalPiece!=0">
                        <button type="button" class=" btn btn-outline-primary me-2 " v-on:click="SaveStockValue(true)" v-if="(isValid('CanDraftStockIn') && formName=='StockIn') ||  (isValid('CanDraftStockOut') && formName=='StockOut')  "><i class="far fa-save"></i> {{ $t('AddStockValue.SaveasDraft') }}</button>

                        <button type="button" class=" btn btn-outline-primary me-2   " v-on:click="SaveStockValue(false)" v-if=" ((isValid('CanAddStockIn') || isValid('CanEditStockIn'))  && formName=='StockIn') ||  ((isValid('CanAddStockOut') || isValid('CanEditStockOut'))  && formName=='StockOut')  "><i class="far fa-save"></i> {{ $t('AddStockValue.SaveandPost') }}</button>
                        <button class="btn btn-danger " v-on:click="onCancel">{{ $t('AddStockValue.Cancel') }}</button>
                    </div>
                    <div class="button-items" v-else>
                       <button type="button" class="btn btn-outline-primary  me-2 " disabled v-on:click="SaveStockValue(true)" v-if="(isValid('CanDraftStockIn') && formName=='StockIn') ||  (isValid('CanDraftStockOut') && formName=='StockOut') "><i class="far fa-save"></i>{{ $t('AddStockValue.SaveasDraft') }}</button>

                        <button type="button" class=" btn btn-outline-primary me-2  " disabled v-on:click="SaveStockValue(false)" v-if=" ((isValid('CanAddStockIn') || isValid('CanEditStockIn'))  && formName=='StockIn') ||  ((isValid('CanAddStockOut') || isValid('CanEditStockOut'))  && formName=='StockOut') "><i class="far fa-save"></i> {{ $t('AddStockValue.SaveandPost') }}</button>
                        <button class="btn btn-danger " v-on:click="onCancel">{{ $t('AddStockValue.Cancel') }}</button>
                    </div>


                </div>

                <div class="col-lg-12 mt-4 mb-5">
                    <div class="card">
                        <div class="card-body">
                            <div class="row">
                                <div class="col-lg-8" style="border-right: 1px solid #eee;">
                                    <label>{{ $t('AddPurchaseOrder.TermandCondition') }}:</label>
                                    <textarea class="form-control " rows="3" v-model="productDetail.narration" />
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group ps-3" v-if="!loading">
                                        <div class="font-xs mb-1">{{ $t('AddStockValue.AttachFile') }} </div>

                                        <button v-on:click="Attachment()" type="button"
                                                class="btn btn-light btn-square btn-outline-dashed mb-1">
                                            <i class="fas fa-cloud-upload-alt"></i> {{ $t('AddPurchase.Attachment') }}
                                        </button>

                                        <div>
                                            <small class="text-muted">
                                                {{ $t('AddStockValue.FileSize') }}
                                            </small>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <bulk-attachment :attachmentList="productDetail.attachmentList" :show="show" v-if="show" @close="attachmentSave" />

            <loading v-model:active="loading" :can-cancel="true" :is-full-page="true"></loading>
        </div>
    </div>
    <div v-else> <acessdenied></acessdenied></div>

</template>

<script>
    
    
    import clickMixin from '@/Mixins/clickMixin'
    import { required } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core';
    import Multiselect from 'vue-multiselect'
    import moment from "moment";
    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    export default {
        mixins: [clickMixin],
        name: "AddCheckOut",
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
                    attachmentList: [],
                    taxMethod: '',
                    taxRateId: "",
                    reason: '',
                    bankCashAccountId: '',
                    branchId: '',
                    isSerial: false
                },
                isFifo: false,
                loading: false,
                show: false,
                isAttachshow: false,
                language: 'Nothing',
                options: [],
                reasonOptions: [],
                itemRender: 0,
                accountRender: 0,
                randerInput: 0,
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
            GotoPage: function (link) {
                this.$router.push({ path: link });
            },
            attachmentSave: function (attachment) {
                
                this.productDetail.attachmentList = attachment;
                this.show = false;
            },

            GetItemOnWarehouse: function () {
                this.itemRender++;
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

            GetAutoCodeGenerator: function (value) {

                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.$https.get('/Product/StockAdjustmentCode?stockAdjustmentType=' + value + '&branchId=' + localStorage.getItem('BranchId'), { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {
                        root.productDetail.code = response.data;
                        root.randerInput++;

                    }
                });
            },
            onCancel: function () {
                var root = this;
                if (((root.isValid('CanViewStockIn') || root.isValid('CanDraftStockIn')) && root.formName == 'StockIn') || ((root.isValid('CanViewStockOut')  || root.isValid('CanDraftStockOut')) && root.formName == 'StockOut')) {
                    root.$router.push({
                        path: '/stockValue',
                        query: {
                            data: 'StockValues' + root.formName,
                            formName: root.formName,
                        }
                    })
                }
                else {
                    root.$router.go();
                }
               
                
            },
            getstockAdjustmentDetails: function (stockAdjustmentDetails) {

                this.productDetail.stockAdjustmentDetails = stockAdjustmentDetails;
            },
            SaveStockValue: function (x) {
                var root = this;
                var token = '';
                
                if (x == false) {
                    if ((this.isValid('CanDraftStockIn') && this.formName == 'StockIn') || (this.isValid('CanDraftStockOut') && this.formName == 'StockOut')) {
                        localStorage.setItem('active', 'Draft');
                    }
                    else if ((this.isValid('CanViewStockIn') && this.formName == 'StockIn') || (this.isValid('CanViewStockOut') && this.formName == 'StockOut')) {
                        localStorage.setItem('active', 'Approved');
                    }
                    

                }
                if (x == true) {
                    if ((this.isValid('CanDraftStockIn') && this.formName == 'StockIn') || (this.isValid('CanDraftStockOut') && this.formName == 'StockOut')) {
                        localStorage.setItem('active', 'Draft');
                    }
                    else if ((this.isValid('CanViewStockIn') && this.formName == 'StockIn') || (this.isValid('CanViewStockOut') && this.formName == 'StockOut')) {
                        localStorage.setItem('active', 'Approved');
                    }


                }
                this.productDetail.isDraft = x;
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                if (root.formName === 'StockIn')
                    root.loading = true;

                root.productDetail.stockAdjustmentDetails.forEach(function (x) {
                    x.quantity = x.totalPiece;
                });
                this.productDetail.isSerial = localStorage.getItem('IsSerial') == 'true' ? true : false;
                this.productDetail.branchId = localStorage.getItem('BranchId');


                root.productDetail.date = root.productDetail.date + " " + moment().format("hh:mm A");
                this.$https.post('/Product/AddStockAdjustment', this.productDetail, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data.message.id != '00000000-0000-0000-0000-000000000000' && response.data.action == "Add") {

                        root.loading = false
                        root.info = response.data.bpi

                        root.$swal({
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved!' : '!تم الحفظ',
                            text: response.data.message.isAddUpdate,
                            type: 'success',
                            confirmButtonClass: "btn btn-success",
                            buttonStyling: false,
                            icon: 'success',
                            timer: 1500,
                            timerProgressBar: true,

                        }).then(function (ok) {
                            if (ok != null) {
                                if (((root.isValid('CanViewStockIn') || root.isValid('CanDraftStockIn')) && root.formName == 'StockIn') || ((root.isValid('CanViewStockOut') || root.isValid('CanDraftStockOut')) && root.formName == 'StockOut')) {
                                    root.$router.push({
                                        path: '/stockValue',
                                        query: {
                                            data: 'StockValues' + root.formName,
                                            formName: root.formName,
                                        }
                                    })
                                }
                                else {
                                    root.$router.go();
                                }
                            }
                        });
                    }
                    else if (response.data.message.id != '00000000-0000-0000-0000-000000000000' && response.data.action == "Update") {
                        root.loading = false
                        root.info = response.data.bpi

                        root.$swal({
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved!' : '!تم الحفظ',
                            text: response.data.message.isAddUpdate,
                            type: 'success',
                            confirmButtonClass: "btn btn-success",
                            buttonStyling: false,
                            icon: 'success',
                            timer: 1500,
                            timerProgressBar: true,

                        }).then(function (ok) {
                            if (ok != null) {
                                if (((root.isValid('CanViewStockIn') || root.isValid('CanDraftStockIn')) && root.formName == 'StockIn') || ((root.isValid('CanViewStockOut') || root.isValid('CanDraftStockOut')) && root.formName == 'StockOut')) {
                                    root.$router.push({
                                        path: '/stockValue',
                                        query: {
                                            data: 'StockValues' + root.formName,
                                            formName: root.formName,
                                        }
                                    })
                                }
                                else {
                                    root.$router.go();
                                }
                            }
                        });
                    }
                    else {
                        root.loading = false
                        root.$swal({
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                            text: response.data.message.isAddUpdate,
                            type: 'error',
                            confirmButtonClass: "btn btn-danger",
                            icon: 'error',
                            timer: 1500,
                            timerProgressBar: true,
                        });
                    }

                }, function (value) {
                    root.loading = false
                    root.$swal({
                        title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                        text: value,
                        type: 'error',
                        confirmButtonClass: "btn btn-info",
                        buttonsStyling: false
                    });
                }
                );
            },
        },
        watch: {
            formName: function () {
                if (this.formName == 'StockIn') {

                    if (this.$route.query.data == undefined) {
                        this.render++;
                        this.GetAutoCodeGenerator(this.formName);
                        this.productDetail.stockAdjustmentType = this.formName;
                        this.productDetail.date = moment().format("DD MMM YYYY");
                    }
                    if (this.$route.query.data != undefined) {
                        this.render++;
                        this.productDetail = this.$route.query.data.message;

                        this.productDetail.stockAdjustmentType = this.formName;
                        
                    }
                }
                if (this.formName == 'StockProduction') {
                    if (this.$route.query.data == undefined) {
                        this.render++;
                        this.GetAutoCodeGenerator(this.formName);
                        this.productDetail.stockAdjustmentType = this.formName;
                    }
                    if (this.$route.query.data != undefined) {
                        this.render++;
                        this.productDetail = this.$route.query.data.message;

                        this.productDetail.stockAdjustmentType = this.formName;
                    }
                }
                if (this.formName == 'StockOut') {
                    if (this.$route.query.data == undefined) {
                        this.render++;
                        this.GetAutoCodeGenerator(this.formName);
                        this.productDetail.stockAdjustmentType = this.formName;
                        this.productDetail.date = moment().format("DD MMM YYYY");
                        this.productDetail.taxRateId = localStorage.getItem('TaxRateId');
                        this.productDetail.taxMethod = localStorage.getItem('taxMethod');
                    }
                    if (this.$route.query.data != undefined) {
                        this.render++;
                        this.productDetail = this.$route.query.data.message;

                        this.productDetail.stockAdjustmentType = this.formName;

                    }
                }
            }
        },
        created: function () {
            

            this.$emit('update:modelValue', this.$route.name + this.formName);
            this.productDetail.warehouseId = localStorage.getItem('WareHouseId');
            this.isFifo = localStorage.getItem('fIFO') == 'true' ? true : false;
            this.productDetail.date = moment().format("DD MMM YYYY");
        },

        mounted: function () {
            var root =  this;

            if(root.$route.query.Add == 'false')
            {
                root.$route.query.data = this.$store.isGetEdit;
            }


            this.language = this.$i18n.locale;
            if ((this.$i18n.locale == 'en' || this.isLeftToRight())) {
                this.options = ['Inclusive', 'Exclusive'];
                this.reasonOptions = ['Damaged', 'Expire', 'Lost'];
            }
            else {
                this.options = ['شامل', 'غير شامل'];
                this.reasonOptions = ['تالف', 'تنقضي', 'ضائع'];
            }
            this.productDetail.date = moment().format("DD MMM YYYY");
            if (this.formName == 'StockIn') {

                if (this.$route.query.data == undefined) {
                    this.render++;
                    this.GetAutoCodeGenerator(this.formName);

                    this.productDetail.stockAdjustmentType = this.formName;
                }
                if (this.$route.query.data != undefined) {

                    this.render++;
                    this.productDetail = this.$route.query.data;
                    var isMultiUnit2 = localStorage.getItem('IsMultiUnit');
                    if (isMultiUnit2 == 'true' && localStorage.getItem('decimalQuantity') == 'true') {
                        this.productDetail.stockAdjustmentDetails.forEach(function (x) {

                            x.highQty = parseInt(parseFloat(x.quantity) / (parseFloat(x.product.unitPerPack) == 0 ? 1 : parseFloat(x.product.unitPerPack)));
                            x.quantity = parseFloat(parseFloat(x.quantity) % (parseFloat(x.product.unitPerPack) == 0 ? 1 : parseFloat(x.product.unitPerPack))).toFixed(3).slice(0, -1);

                        });
                    }
                    if (isMultiUnit2 == 'true' && localStorage.getItem('decimalQuantity') != 'true') {
                        this.productDetail.stockAdjustmentDetails.forEach(function (x) {

                            x.highQty = parseInt(parseInt(x.quantity) / (parseInt(x.product.unitPerPack) == 0 ? 1 : parseInt(x.product.unitPerPack)));
                            x.quantity = parseInt(parseInt(x.quantity) % (parseInt(x.product.unitPerPack) == 0 ? 1 : parseInt(x.product.unitPerPack)));

                        });
                    }
                    this.productDetail.stockAdjustmentType = this.formName;
                    this.accountRender++;
                }
            }
            
            if (this.formName == 'StockOut') {
                if (this.$route.query.data == undefined) {
                    this.render++;
                    this.GetAutoCodeGenerator(this.formName);
                    this.productDetail.stockAdjustmentType = this.formName;

                    this.productDetail.taxRateId = localStorage.getItem('TaxRateId');
                    this.productDetail.taxMethod = localStorage.getItem('taxMethod');
                }
                if (this.$route.query.data != undefined) {

                    this.productDetail = this.$route.query.data;
                    var isMultiUnit = localStorage.getItem('IsMultiUnit');
                    if (isMultiUnit == 'true' && localStorage.getItem('decimalQuantity') == 'true') {
                        this.productDetail.stockAdjustmentDetails.forEach(function (x) {

                            x.highQty = parseInt(parseFloat(x.quantity) / parseFloat(x.product.unitPerPack));
                            x.quantity = parseFloat(parseFloat(x.quantity) % parseFloat(x.product.unitPerPack)).toFixed(3).slice(0, -1);

                        });
                    }
                    if (isMultiUnit == 'true' && localStorage.getItem('decimalQuantity') != 'true') {
                        this.productDetail.stockAdjustmentDetails.forEach(function (x) {

                            x.highQty = parseInt(parseInt(x.quantity) / parseInt(x.product.unitPerPack));
                            x.quantity = parseInt(parseInt(x.quantity) % parseInt(x.product.unitPerPack));

                        });
                    }
                    this.productDetail.stockAdjustmentType = this.formName;
                    this.render++;
                    this.accountRender++;
                }
            }
            if (this.formName == 'StockProduction') {
                if (this.$route.query.data == undefined) {
                    this.render++;
                    this.GetAutoCodeGenerator(this.formName);
                    this.productDetail.stockAdjustmentType = this.formName;
                }
                if (this.$route.query.data != undefined) {

                    this.render++;
                    this.productDetail = this.$route.query.data;
                    var isMultiUnit1 = localStorage.getItem('IsMultiUnit');
                    if (isMultiUnit1 == 'true' && localStorage.getItem('decimalQuantity') == 'true') {
                        this.productDetail.stockAdjustmentDetails.forEach(function (x) {
                            x.highQty = parseInt(parseFloat(x.quantity) / parseFloat(x.product.unitPerPack));
                            x.quantity = parseFloat(parseFloat(x.quantity) % parseFloat(x.product.unitPerPack)).toFixed(3).slice(0, -1);
                        });
                    }
                    if (isMultiUnit1 == 'true' && localStorage.getItem('decimalQuantity') != 'true') {
                        this.productDetail.stockAdjustmentDetails.forEach(function (x) {
                            x.highQty = parseInt(parseInt(x.quantity) / parseInt(x.product.unitPerPack));
                            x.quantity = parseInt(parseInt(x.quantity) % parseInt(x.product.unitPerPack));
                        });
                    }
                    this.productDetail.stockAdjustmentType = this.formName;
                }
            }
        }
    }
</script>