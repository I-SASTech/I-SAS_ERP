<template>
    <div class="row" v-if="isValid('CanAddAutoTemplate')|| isValid('CanEditAutoTemplate')">
        <div class="col-md-12 ml-auto mr-auto">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 v-if="wareHouseTransfer.id === '00000000-0000-0000-0000-000000000000'" class="page-title">
                                    {{ $t('AddWareHouseTransfer.AddWareHouseTransfer') }}
                                </h4>
                                <h4 v-else class="page-title">{{ $t('AddWareHouseTransfer.UpdateWareHouseTransfer') }}</h4>
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
                            <span id="ember695" class="tooltip-container text-dashed-underline "> {{ $t('AddWareHouseTransfer.RegistrationNo') }}<span class="text-danger">*</span></span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <input v-bind:key="randerInput" v-model="wareHouseTransfer.code" disabled class="form-control" type="text">
                        </div>
                    </div>




                    <div class="row form-group">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline ">{{ $t('AddWareHouseTransfer.FromWareHouse') }}:<span class="text-danger">*</span></span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <warehouse-dropdown v-model="v$.wareHouseTransfer.fromWareHouseId.$model"
                                                @default="setDefaultWareHouseFrom"
                                                @update:modelValue="randerWareHouseFunc" :key="rendered" />
                        </div>
                    </div>

                    <div class="row form-group">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline ">From Branch:</span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <branch-dropdown v-model="wareHouseTransfer.fromBranchId" :modelValue="wareHouseTransfer.fromBranchId" :ismultiple="false" @update:modelValue="RederFucn()" />
                        </div>
                    </div>


                </div>
                <div class="col-lg-6">
                    <div class="row form-group">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline ">  {{ $t('AddWareHouseTransfer.Date') }} <span class="text-danger">*</span></span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <datepicker v-model="wareHouseTransfer.date" />
                        </div>
                    </div>
                    <div class="row form-group">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline ">
                                {{ $t('AddWareHouseTransfer.ToWareHouse') }} :<span class="text-danger"> *</span>
                            </span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <warehouse-dropdown v-model="v$.wareHouseTransfer.toWareHouseId.$model" :fromWareHouse="wareHouseTransfer.fromWareHouseId"
                                                v-bind:key="randerWareHouse" />
                            <span v-if="v$.wareHouseTransfer.toWareHouseId.$error" class="error text-danger"> </span>
                        </div>
                    </div>
                    <div class="row form-group">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline ">To Branch:</span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <branch-dropdown v-model="wareHouseTransfer.toBranchId" :modelValue="wareHouseTransfer.toBranchId" 
                                :ismultiple="false" :fromBranch="wareHouseTransfer.fromBranchId" :key="render" />
                        </div>
                    </div>
                </div>


            </div>

            <warehousetransferitem :wareHouseTransferItem="wareHouseTransfer.wareHouseTransferItems" :wareHouse="wareHouseTransfer.fromWareHouseId" @update:modelValue="SaveWareHouseTransferItems" v-bind:key="randerWareHouse" />


            <div class="col-lg-12 invoice-btn-fixed-bottom">
                <div v-if="!loading && wareHouseTransfer.id == '00000000-0000-0000-0000-000000000000'"
                     class="col-md-12 arabicLanguage">

                    <button class="btn btn-outline-primary me-2"
                            v-if="isValid('CanDraftStockTransfer')"
                            v-on:click="saveWareHouseTransfer('Draft')">
                        <i class="far fa-save"></i>  {{ $t('AddWareHouseTransfer.SaveAsDraft') }}
                    </button>
                    <button class="btn btn-outline-primary me-2"
                            v-if="isValid('CanAddStockTransfer')"
                            v-on:click="saveWareHouseTransfer('Approved')">
                        <i class="far fa-save"></i>  {{ $t('AddWareHouseTransfer.SaveAsPost') }}
                    </button>


                    <button class="btn btn-danger me-2" v-on:click="goToWareHouseTransfer">
                        {{ $t('AddPurchase.Cancel') }}
                    </button>
                </div>
                <div v-if="!loading && wareHouseTransfer.id != '00000000-0000-0000-0000-000000000000'"
                     class="col-md-12 arabicLanguage">

                    <button class="btn btn-outline-primary me-2"
                            v-if="isValid('CanDraftStockTransfer')"
                            v-on:click="saveWareHouseTransfer('Draft')">
                        <i class="far fa-save"></i>  {{ $t('AddWareHouseTransfer.UpdateAsDraft') }}
                    </button>
                    <button class="btn btn-outline-primary me-2"
                            v-on:click="saveWareHouseTransfer('Approved')"
                            v-if="isValid('CanEditStockTransfer')">
                        <i class="far fa-save"></i> {{ $t('AddWareHouseTransfer.UpdateAsPost') }}
                    </button>

                    <button class="btn btn-danger me-2" v-on:click="goToWareHouseTransfer">
                        {{ $t('AddPurchase.Cancel') }}
                    </button>
                </div>
            </div>


            <div class="col-lg-12 mt-4 mb-5">
                <div class="card">
                    <div class="card-body">
                        <div class="row">
                            <div class="col-lg-8" >

                            </div>
                            <div class="col-lg-4">
                                <div class="form-group ps-3" v-if="!loading">
                                    <div class="font-xs mb-1">{{ $t('AddWareHouseTransfer.AttachFile') }}</div>

                                    <button v-on:click="Attachment()" type="button"
                                            class="btn btn-light btn-square btn-outline-dashed mb-1">
                                        <i class="fas fa-cloud-upload-alt"></i> {{ $t('AddPurchase.Attachment') }}
                                    </button>

                                    <div>
                                        <small class="text-muted">
                                            {{ $t('AddWareHouseTransfer.FileSize') }}
                                        </small>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
        <bulk-attachment :attachmentList="wareHouseTransfer.attachmentList" :show="show" v-if="show" @close="attachmentSave" />

        <loading v-model:active="loading" :can-cancel="true" :is-full-page="true"></loading>
    </div>

    <div v-else> <acessdenied></acessdenied></div>
</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'
    
    import moment from "moment";
    
    import { required } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core';
    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';

    //import Vue3Barcode from 'vue3-barcode'
    export default {
        mixins: [clickMixin],
        name: "AddWareHouseTransfer",
        components: {
            Loading
        },
        setup() {
            return { v$: useVuelidate() }
        },
        data: function () {
            return {

                wareHouseTransfer: {
                    id: "00000000-0000-0000-0000-000000000000",
                    date: "",
                    code: "",
                    fromWareHouseId: "",
                    toWareHouseId: "",
                    wareHouseTransferItems: [],
                    attachmentList: [],
                    branchId: '',
                    toBranchId:'',
                    fromBranchId:'',

                },
                loading: false,
                show: false,
                rendered: 0,
                render: 0,
                randerInput: 0,
                language: 'Nothing',
                randerWareHouse: 0
            };
        },
        validations() {
            return {
                wareHouseTransfer: {
                    date: { required },
                    code: { required },
                    fromWareHouseId: { required },
                    toWareHouseId: { required },
                    wareHouseTransferItems: {
                        required,
                        isOutOfStock: function (saleItems) {

                            if (saleItems.filter(x => x.outOfStock).length > 0) {
                                return false
                            }

                            return true;
                        }
                    },
                },
                }
        },
        methods: {
            RederFucn: function()
            {
                this.render++;
            },
            Attachment: function () {
                this.show = true;
            },

            attachmentSave: function (attachment) {
                
                this.show = false;
                this.wareHouseTransfer.attachmentList = attachment;
            },

            setDefaultWareHouseFrom: function (id) {

                this.wareHouseTransfer.fromWareHouseId = id;
                this.rendered++;
                this.randerWareHouse++;
            },
            //setDefaultWareHouseTo: function (id) {
            //    this.wareHouseTransfer.toWareHouseId = id;
            //    this.rendered++;
            //},
            randerWareHouseFunc: function () {


                this.randerWareHouse++;
                this.wareHouseTransfer.toWareHouseId = '00000000-0000-0000-0000-000000000000';

            },
            languageChange: function (lan) {
                var root = this;
                if (this.language == lan) {
                    if (this.wareHouseTransfer.id == '00000000-0000-0000-0000-000000000000') {

                        var getLocale = this.language;
                        this.language = getLocale;

                        this.$router.go('/addSale');
                    }
                    else {
                        this.$swal({
                            title: root.language == 'ar' ? 'خطأ' : 'Error!',
                            text: root.language == 'en'  ? 'You cannot Change language during update, otherwise your current page data will be lose!' : 'لا يمكنك تغيير اللغة أثناء التحديث ، وإلا ستفقد بيانات صفحتك الحالية!',
                            type: 'error',
                            confirmButtonClass: "btn btn-danger",
                            icon: 'error',
                            timer: 4000,
                            timerProgressBar: true,
                        });
                    }
                }


            },

            AutoIncrementCode: function () {
                var root = this;
                var token = "";
                if (token == '') {
                    token = localStorage.getItem("token");
                }
                root.$https
                    .get("/Purchase/WareHouseTranferAutoGenerateNo?branchId=" + localStorage.getItem('BranchId'), {
                        headers: { Authorization: `Bearer ${token}` },
                    })
                    .then(function (response) {
                        if (response.data != null) {
                            root.wareHouseTransfer.code = response.data;
                            root.randerInput++;
                        }
                    });
            },
            SaveWareHouseTransferItems: function (wareHouseTransferItems) {
                this.wareHouseTransfer.wareHouseTransferItems = wareHouseTransferItems;
            },
            saveWareHouseTransfer: function (status) {
                
                this.wareHouseTransfer.approvalStatus = status
                localStorage.setItem('active', status);

                this.loading = true;
                var root = this;
                var token = "";
                if (token == '') {
                    token = localStorage.getItem("token");
                }

                var isMultiUnit = localStorage.getItem('IsMultiUnit');
                if (isMultiUnit == 'true') {
                    root.wareHouseTransfer.wareHouseTransferItems.forEach(function (x) {
                        x.quantity = parseInt(parseInt(x.quantity) + (parseInt(x.highQty) * parseInt(x.product.unitPerPack)));
                    });
                }
                root.wareHouseTransfer.branchId = localStorage.getItem('BranchId');

                console.log(root.wareHouseTransfer);
                this.$https
                    .post("/Purchase/SaveWareHouseTransferInformation", root.wareHouseTransfer, {
                        headers: { Authorization: `Bearer ${token}` },
                    })
                    .then((response) => {
                        root.loading = false;
                        root.info = response.data.bpi;

                        root.$swal.fire({
                            icon: "success",
                            title: "Saved Successfully",
                            showConfirmButton: false,

                            timer: 800,
                            timerProgressBar: true,
                        });
                        if (root.isValid('CanViewStockTransfer') || root.isValid('CanDraftStockTransfer')) {
                            root.$router.push({
                                path: '/wareHouseTransfer',
                                query: {
                                    data: 'WareHouseTransfers'
                                }
                            })
                        }
                        else {
                            root.$router.go();
                        }
                    })
                    .catch((error) => {

                        console.log(error);
                        root.$swal.fire({
                            icon: 'error',
                            title: root.language == 'ar' ? 'خطأ' : 'Error!',
                            text: error.response.data,
                            showConfirmButton: false,
                            timer: 5000,
                            timerProgressBar: true,
                        });

                        root.loading = false;
                    })
                    .finally(() => (root.loading = false));
            },
            GotoPage: function (link) {
                this.$router.push({ path: link });
            },
            goToWareHouseTransfer: function () {
                var root = this;
                if (root.isValid('CanViewStockTransfer') || root.isValid('CanDraftStockTransfer')) {
                    root.$router.push({
                        path: '/wareHouseTransfer',
                        query: {
                            data: 'WareHouseTransfers'
                        }
                    })
                }
                else {
                    root.$router.go();
                }

            },


        },
       
        created: function () {
            var root =  this;

            if(root.$route.query.Add == 'false')
            {
                root.$route.query.data = this.$store.isGetEdit;
            }
            if (this.$route.query.data == undefined) {
                this.AutoIncrementCode();
                this.wareHouseTransfer.date = moment().format("LLL");
                this.rander++;
            }
            if (this.$route.query.data != undefined) {


                this.wareHouseTransfer = this.$route.query.data;

                //  this.warehouse = this.$route.query.data;
            }
            this.$emit('update:modelValue', this.$route.name);
        },
        mounted: function () {
            this.language = localStorage.getItem('locales');
        }
    };
</script>
