<template>
    <div class="row " v-if="isValid('CanEditBundleOffer') || isValid('CanAddBundleOffer')">
        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">{{ $t('AddBundles.Bundle') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                    <li class="breadcrumb-item active">{{ $t('AddBundles.Bundle') }}</li>
                                </ol>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="card col-md-8">
                <div class="card-body">
                    <div class="row ">
                        <div class="form-group col-md-6 col-sm-12" v-bind:class="{ 'has-danger': v$.bundleCategory.offer.$error }">
                            <label class="text  font-weight-bolder ">
                                {{ $t('AddBundles.NameEn') }} :
                                <span class="text-danger"> *</span>
                            </label>
                            <input class="form-control" v-model="v$.bundleCategory.offer.$model" type="text" />
                            <span v-if="v$.bundleCategory.offer.$error"  class="error text-danger">
                                <span v-if="!v$.bundleCategory.offer.required">
                                    {{ $t('AddBundles.Offer') }}
                                </span>
                                <span v-if="!v$.bundleCategory.offer.maxLength">
                                    {{ $t('AddBundles.OfferLength') }}
                                </span>
                            </span>
                        </div>

                        <div class="form-group col-md-6 col-sm-12"  v-if="bundleCategory.id == '00000000-0000-0000-0000-000000000000'">
                            <label>
                                {{ $t('AddBundles.ProductList') }}:
                                <span class="text-danger"> *</span>
                            </label>
                            <div>
                                <product-dropdown v-model="bundleCategory.productId" :modelValue="bundleCategory.productId" :status="'isBundle'" :emptyselect="true" :fromBundles="true"> </product-dropdown>
                            </div>
                        </div>
                        <div class="form-group col-md-6 col-sm-12"
                            v-if="bundleCategory.id != '00000000-0000-0000-0000-000000000000'">
                            <label>
                                {{ $t('AddBundles.ProductList') }}:
                                <span class="text-danger"> *</span>
                            </label>
                            <div>
                                <input type="text" v-model='bundleCategory.name' class="form-control" disabled
                                    name="name" />
                            </div>
                        </div>
                        <div class="form-group col-md-6 col-sm-12" v-bind:class="{ 'has-danger': v$.bundleCategory.buy.$error }">
                            <label class="text  font-weight-bolder ">
                                {{ $t('AddBundles.Buy') }} :
                                <span  class="text-danger"> *</span>
                            </label>
                            <input class="form-control" v-model="v$.bundleCategory.buy.$model" type="number" />
                            <span v-if="v$.bundleCategory.buy.$error" class="error text-danger">
                                <span v-if="!v$.bundleCategory.buy.required">{{ $t('AddBundles.Buy2') }}</span>
                                <span v-if="!v$.bundleCategory.buy.maxLength">{{ $t('AddBundles.BuyLength') }}</span>
                            </span>
                        </div>


                        <div class="form-group col-md-6 col-sm-12" v-bind:class="{ 'has-danger': v$.bundleCategory.get.$error }">
                            <label class="text  font-weight-bolder ">
                                {{ $t('AddBundles.Get') }} :
                                <span class="text-danger"> *</span>
                            </label>
                            <input class="form-control" v-model="v$.bundleCategory.get.$model" type="number" />
                            <span v-if="v$.bundleCategory.get.$error" class="error text-danger">
                                <span v-if="!v$.bundleCategory.get.required">{{ $t('AddBundles.Get2') }}</span>
                                <span v-if="!v$.bundleCategory.get.maxLength">{{ $t('AddBundles.GetLength') }}</span>
                            </span>
                        </div>
                        <div class="form-group col-md-6 col-sm-12"  v-bind:class="{ 'has-danger': v$.bundleCategory.quantityLimit.$error }">
                            <label class="text  font-weight-bolder ">
                                {{ $t('AddBundles.Limit') }} :
                                <span class="text-danger"> *</span>
                            </label>
                            <input class="form-control" v-model="v$.bundleCategory.quantityLimit.$model"  type="number" />
                            <span v-if="v$.bundleCategory.quantityLimit.$error" class="error text-danger">
                                <span v-if="!v$.bundleCategory.quantityLimit.required">
                                    {{ $t('AddBundles.Limit2') }}
                                </span>
                                <span v-if="!v$.bundleCategory.quantityLimit.maxLength">
                                    {{ $t('AddBundles.LimitLength') }}
                                </span>
                            </span>
                        </div>
                        <div class="form-group col-md-6 col-sm-12" v-bind:class="{ 'has-danger': v$.bundleCategory.stockLimit.$error }">
                            <label class="text  font-weight-bolder ">
                                {{ $t('AddBundles.StockLimit') }} :
                                <span class="text-danger"> *</span>
                            </label>
                            <input class="form-control" v-model="v$.bundleCategory.stockLimit.$model" type="number" />
                            <span v-if="v$.bundleCategory.stockLimit.$error" class="error text-danger">
                                <span v-if="!v$.bundleCategory.stockLimit.required">{{ $t('AddBundles.Limit2') }}</span>
                                <span v-if="!v$.bundleCategory.stockLimit.maxLength">
                                    {{ $t('AddBundles.LimitLength') }}
                                </span>
                            </span>
                        </div>

                        <div :key="dateRender + 'fromDate'" class="form-group col-md-6 col-sm-12">
                            <label>
                                {{ $t('AddBundles.FromDate') }}:
                                <span class="text-danger"> *</span>
                            </label>
                            <div v-bind:class="{ 'has-danger': v$.bundleCategory.fromDate.$error }">
                                <datepicker v-model="v$.bundleCategory.fromDate.$model" :key="daterander"></datepicker>
                                <span v-if="v$.bundleCategory.fromDate.$error" class="error text-danger">
                                    <span v-if="!v$.bundleCategory.fromDate.required">From Date is Required</span>
                                </span>
                            </div>
                        </div>
                        <div :key="dateRender + 'toDate'" class="form-group col-md-6 col-sm-12">
                            <label>{{ $t('AddBundles.ToDate') }} :<span class="text-danger"> *</span></label>
                            <div v-bind:class="{ 'has-danger': v$.bundleCategory.toDate.$error }">
                                <datepicker v-model="v$.bundleCategory.toDate.$model" :key="daterander"></datepicker>
                                <span v-if="v$.bundleCategory.toDate.$error" class="error text-danger">
                                    <span v-if="!v$.bundleCategory.toDate.required">To Date is Required</span>
                                </span>
                            </div>
                        </div>
                        <div class="form-group col-md-12" v-if="allowBranches">
                            <label class="text  font-weight-bolder"> {{ $t('AddBranchUsers.Branch') }}:</label>
                            <div >
                                <branch-dropdown v-model="bundleCategory.branchesIdList" :modelValue="bundleCategory.branchesIdList" :ismultiple="true" />
                            </div>
                        </div>
                        <div class="form-group col-md-6">
                            <div class="checkbox form-check-inline mx-2">
                                <input type="checkbox" id="inlineCheckbox2" v-model="bundleCategory.isActive">
                                <label for="inlineCheckbox2"> {{ $t('AddBundles.Active')}}  </label>
                            </div>
                        </div>

                        <div class="col-sm-12 arabicLanguage">
                            <button type="button" class="btn btn-outline-primary me-2 " v-bind:disabled="v$.bundleCategory.$invalid" 
                                    v-on:click="SaveOffer" v-if="bundleCategory.id == '00000000-0000-0000-0000-000000000000' && isValid('CanAddBundleOffer')">
                                    {{  $t('AddBundles.btnSave') }}
                            </button>
                            <button type="button" class="btn btn-outline-primary me-2 " v-bind:disabled="v$.bundleCategory.$invalid"
                                v-on:click="SaveOffer"
                                v-if="bundleCategory.id != '00000000-0000-0000-0000-000000000000' && isValid('CanEditBundleOffer')">
                                {{ $t('AddBundles.btnUpdate') }}
                            </button>
                            <button type="button" class="btn btn-outline-danger  mr-3 " v-on:click="Close()">{{
                                    $t('AddBundles.btnClear')
                            }}</button>
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </div>
    <div v-else>
        <acessdenied></acessdenied>
    </div>
</template>
<script>
import clickMixin from '@/Mixins/clickMixin'
import moment from 'moment';

    import { required, maxLength } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'

export default ({
    mixins: [clickMixin],
     setup() {
            return { v$: useVuelidate() }
        },
    data: function () {
        return {
            bundleCategory: {
                id: '00000000-0000-0000-0000-000000000000',
                buy: '',
                offer: '',
                productId: '',
                get: '',
                toDate: '',
                fromDate: '',
                quantityLimit: '',
                stockLimit: '',
                branchId: '',
                branchesIdList: [],
                isActive: true
            },
            productList: '',
            listOfProduct: [],
            loading: false,
            allowBranches: false,
            daterander: 0,
            dateRender: 0,
            language: 'Nothing',
            asasas: '',
        }
    },
    validations() {
       return{
         bundleCategory:
        {
            offer: {
                required,
                maxLength: maxLength(50)
            },

            quantityLimit: {
                required,
                maxLength: maxLength(20)
            },
            stockLimit: {
                required,
                maxLength: maxLength(20)
            },
            buy: {
                required,
                maxLength: maxLength(20)
            },
            get: {
                required,
                maxLength: maxLength(20)
            },
            isActive: {},
            toDate: { required },
            fromDate: { required }
        },

        productList: { required },
       }
    },
    methods: {
        languageChange: function (lan) {

            if (this.language == lan) {
                if (this.bundleCategory.id == '00000000-0000-0000-0000-000000000000') {

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
        Close: function () {
            this.$router.push('/bundles');
        },
        //getProductList: function () {
        //    for (var i = 0; i < this.productList.length; i++) {
        //        this.listOfProduct[i] = this.productList[i].id
        //    }
        //},
        SaveOffer: function () {
            this.loading = true;
            var root = this;

            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            //if (this.bundleCategory.id == '00000000-0000-0000-0000-000000000000') {
            //    this.bundleCategory.productId.push({ id: root.productList });
            //}
            root.bundleCategory.branchId = localStorage.getItem('BranchId');


            root.$https
                .post('/Product/SaveBundleCategoryItems', root.bundleCategory, { headers: { "Authorization": `Bearer ${token}` } })
                .then(response => {

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
                                root.$router.push('/bundles');
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
                                root.$router.push('/bundles');
                            }
                        });
                    }
                    else {
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

                })
                .catch(error => {
                    console.log(error)
                    this.$swal.fire(
                        {
                            icon: 'error',
                            title: (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Something Went Wrong!' : 'هل هناك خطب ما!',
                            text: error,
                            showConfirmButton: false,
                            timer: 1000,
                            timerProgressBar: true,

                        });

                    this.loading = false
                })
                .finally(() => this.loading = false)
        }
    },
    created: function () {
        var root =  this;

        if(root.$route.query.Add == 'false')
        {
        root.$route.query.data = this.$store.isGetEdit;
        }
        this.$emit('update:modelValue', this.$route.name);
        this.allowBranches = localStorage.getItem('AllowBranches') == 'true' ? true : false;

    },
    mounted: function () {
        this.language = this.$i18n.locale;
        if (this.$route.query.data == undefined) {
            this.bundleCategory.fromDate = moment().format('llll');
            this.bundleCategory.toDate = moment().format('llll');
            this.daterander++;
        }
        if (this.$route.query.data != undefined) {
            this.bundleCategory = this.$route.query.data;
            this.dateRender++;
        }
    }
})
</script>