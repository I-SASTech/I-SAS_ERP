<template>
    <div class="row" v-if=" isValid('CanAddPromotionOffer') || isValid('CanEditPromotionOffer') ">
        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">{{ $t('AddPromotion.Promotions') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('AddPromotion.Home') }}</a></li>
                                    <li class="breadcrumb-item active">{{ $t('AddPromotion.Promotions') }}</li>
                                </ol>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="card col-md-9">
                <div class="card-body">
                    <div class="row ">

                        <div class="form-group col-sm-12">
                            <div class="radio radio-info form-check-inline">
                                <input type="radio" id="inlineRadio1" value="BuyNGetNSameGroup" name="radioInline" v-model="promotion.promotionType">
                                <label for="inlineRadio1"> Buy N Get N Same Group </label>
                            </div>
                            <div class="radio radio-info form-check-inline">
                                <input type="radio" id="inlineRadio2" value="BuyNGetNAnother" name="radioInline" v-model="promotion.promotionType">
                                <label for="inlineRadio2"> Buy N Get N Another </label>
                            </div>
                            <div class="radio radio-info form-check-inline">
                                <input type="radio" id="inlineRadio3" value="BuyNGetNSameItem" name="radioInline" v-model="promotion.promotionType">
                                <label for="inlineRadio3"> Buy N Get N Same Item </label>
                            </div>
                            <div class="radio radio-info form-check-inline">
                                <input type="radio" id="inlineRadio4" value="GroupNFixOrPercentageDiscount" name="radioInline" v-model="promotion.promotionType">
                                <label for="inlineRadio4"> Group N Fix Or Percentage Discount </label>
                            </div>
                            <div class="radio radio-info form-check-inline">
                                <input type="radio" id="inlineRadio5" value="ItemNFixOrPercentageDiscount" name="radioInline" v-model="promotion.promotionType">
                                <label for="inlineRadio5"> Item N Fix Or Percentage Discount </label>
                            </div>
                        </div>

                        <div class="form-group col-md-6 col-sm-12">
                            <label>{{ $t('AddPromotion.PromotionName') }} :<span class="text-danger"> *</span></label>
                            <div v-bind:class="{'has-danger' : v$.promotion.offer.$error}">
                                <input class="form-control" v-model="v$.promotion.offer.$model" />
                                <span v-if="v$.promotion.offer.$error" class="error text-danger">
                                    <span v-if="!v$.promotion.offer.required">{{ $t('AddPromotion.Offerequired') }}</span>
                                    <span v-if="!v$.promotion.offer.maxLength">{{ $t('AddPromotion.OfferMaximum') }}</span>
                                </span>
                            </div>
                        </div>

                        <div class="form-group col-md-6 col-sm-12" v-if="promotion.promotionType=='BuyNGetNSameGroup' || promotion.promotionType=='GroupNFixOrPercentageDiscount' ">
                            <label>{{ $t('ProductGroup.ProductGroup') }} :</label>
                            <div>
                                <productgroupdropdown v-model="promotion.productGroupId" :modelValue="promotion.productGroupId" />
                            </div>
                        </div>

                        <div class="form-group col-md-6 col-sm-12" v-if="promotion.id=='00000000-0000-0000-0000-000000000000' && (promotion.promotionType=='BuyNGetNAnother' || promotion.promotionType=='BuyNGetNSameItem' || promotion.promotionType=='ItemNFixOrPercentageDiscount')">
                            <label>{{ $t('AddPromotion.ProductList') }} :<span class="text-danger"> *</span></label>
                            <div>
                                <product-for-promotion-dropdown v-model="promotion.productId" :modelValue="promotion.productId" />
                            </div>
                        </div>

                        <div class="form-group col-md-6 col-sm-12" v-if="promotion.id!='00000000-0000-0000-0000-000000000000' && (promotion.promotionType=='BuyNGetNAnother' || promotion.promotionType=='BuyNGetNSameItem' || promotion.promotionType=='ItemNFixOrPercentageDiscount')">
                            <label>{{ $t('AddPromotion.ProductList') }} :<span class="text-danger"> *</span></label>
                            <div>
                                <input type="text" name="name" class="form-control" v-model="promotion.name" disabled />
                            </div>
                        </div>

                        <div class="form-group col-md-6 col-sm-12" v-if="promotion.promotionType!='GroupNFixOrPercentageDiscount' && promotion.promotionType!='ItemNFixOrPercentageDiscount'">
                            <label class="text  font-weight-bolder ">
                                {{ $t('AddBundles.Buy') }} :
                                <span class="text-danger"> *</span>
                            </label>
                            <input class="form-control" v-model="promotion.buy" type="number" />
                        </div>

                        <div class="form-group col-md-6 col-sm-12" v-if="promotion.promotionType!='GroupNFixOrPercentageDiscount' && promotion.promotionType!='ItemNFixOrPercentageDiscount'">
                            <label class="text  font-weight-bolder ">
                                {{ $t('AddBundles.Get') }} :
                                <span class="text-danger"> *</span>
                            </label>
                            <input class="form-control" v-model="promotion.get" type="number" />
                        </div>

                        <div class="form-group col-md-6 col-sm-12" v-if="promotion.promotionType == 'BuyNGetNAnother'">
                            <label>Get Product :<span class="text-danger"> *</span></label>
                            <div>
                                <product-for-promotion-dropdown v-model="promotion.getProductId" :modelValue="promotion.getProductId" />
                            </div>
                        </div>

                        <div class="form-group col-md-6 col-sm-12" v-if="promotion.promotionType!='BuyNGetNSameGroup' && promotion.promotionType!='BuyNGetNAnother' && promotion.promotionType!='BuyNGetNSameItem'">
                            <label>Discount Type :<span class="text-danger"> *</span></label>
                            <multiselect :options="options" v-model="promotion.discountType" :show-labels="false" placeholder="Select Discount Type" v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                            </multiselect>
                        </div>
                        <div class="form-group col-md-6 col-sm-12" v-if="promotion.promotionType!='BuyNGetNSameGroup' && promotion.promotionType!='BuyNGetNAnother' && promotion.promotionType!='BuyNGetNSameItem'">
                            <label>{{ $t('AddPromotion.Discount') }} :<span class="text-danger"> *</span></label>
                            <decimal-to-fixed v-bind:salePriceCheck="false" v-model="v$.promotion.discount.$model" :textAlignLeft="true" />
                            <span v-if="v$.promotion.discount.$error" class="error text-danger">
                                <span v-if="!v$.promotion.discount.maxLength">{{ $t('AddPromotion.DiscountageMaximum') }}</span>
                            </span>
                        </div>

                        <div class="form-group col-md-6 col-sm-12">
                            <label>{{ $t('AddPromotion.FromDate') }}:<span class="text-danger"> *</span></label>
                            <div v-bind:class="{'has-danger' : v$.promotion.fromDate.$error}">
                                <datepicker v-model="v$.promotion.fromDate.$model" :key="daterander"></datepicker>
                                <span v-if="v$.promotion.fromDate.$error" class="error text-danger">
                                    <span v-if="!v$.promotion.fromDate.required">From Date is Required</span>
                                </span>
                            </div>
                        </div>
                        <div class="form-group col-md-6 col-sm-12">
                            <label>{{ $t('AddPromotion.ToDate') }} :<span class="text-danger"> *</span></label>
                            <div v-bind:class="{'has-danger' : v$.promotion.toDate.$error}">
                                <datepicker v-model="v$.promotion.toDate.$model" :key="daterander"></datepicker>
                                <span v-if="v$.promotion.toDate.$error" class="error text-danger">
                                    <span v-if="!v$.promotion.toDate.required">To Date is Required</span>
                                </span>
                                <span v-if="promotion.toDate<promotion.fromDate" class="text-danger">{{ $t('AddPromotion.ToDateMust') }}</span>
                            </div>
                        </div>

                        <div class="form-group col-md-12 col-sm-12">
                            <div class="checkbox checkbox-success form-check-inline ms-2">
                                <input type="checkbox" id="inlineCheckbox10" v-model="promotion.sunday">
                                <label for="inlineCheckbox10"> Sunday </label>
                            </div>
                            <div class="checkbox checkbox-success form-check-inline ms-2">
                                <input type="checkbox" id="inlineCheckbox11" v-model="promotion.monday">
                                <label for="inlineCheckbox11"> Monday </label>
                            </div>
                            <div class="checkbox checkbox-success form-check-inline ms-2">
                                <input type="checkbox" id="inlineCheckbox12" v-model="promotion.tuesday">
                                <label for="inlineCheckbox12"> Tuesday </label>
                            </div>
                            <div class="checkbox checkbox-success form-check-inline ms-2">
                                <input type="checkbox" id="inlineCheckbox13" v-model="promotion.wednesday">
                                <label for="inlineCheckbox13"> Wednesday </label>
                            </div>
                            <div class="checkbox checkbox-success form-check-inline ms-2">
                                <input type="checkbox" id="inlineCheckbox14" v-model="promotion.thursday">
                                <label for="inlineCheckbox14"> Thursday </label>
                            </div>
                            <div class="checkbox checkbox-success form-check-inline ms-2">
                                <input type="checkbox" id="inlineCheckbox15" v-model="promotion.friday">
                                <label for="inlineCheckbox15"> Friday </label>
                            </div>
                            <div class="checkbox checkbox-success form-check-inline ms-2">
                                <input type="checkbox" id="inlineCheckbox16" v-model="promotion.saturday">
                                <label for="inlineCheckbox16"> Saturday </label>
                            </div>
                        </div>

                        <div class="form-group col-md-6 col-sm-12" v-if="promotion.promotionType!='BuyNGetNSameGroup' && promotion.promotionType!='BuyNGetNAnother' && promotion.promotionType!='BuyNGetNSameItem'">
                            <label>Bundle Quantity :<span class="text-danger"> *</span></label>
                            <input v-model="v$.promotion.baseQuantity.$model" class="form-control" type="number" />
                            <span v-if="v$.promotion.baseQuantity.$error" class="error text-danger">
                                <span v-if="!v$.promotion.baseQuantity.required">{{ $t('AddPromotion.Required') }}</span>
                            </span>
                        </div>
                        <div class="form-group col-md-6 col-sm-12">
                            <label>Invoice Bundle Limit :<span class="text-danger"> *</span></label>
                            <input v-model="promotion.upToQuantity" class="form-control" type="number" />
                        </div>
                        <div class="form-group col-md-6 col-sm-12">
                            <label>{{ $t('AddPromotion.StockLimit') }} :<span class="text-danger"> *</span></label>
                            <div v-bind:class="{'has-danger' : v$.promotion.stockLimit.$error}">
                                <input v-model="v$.promotion.stockLimit.$model" class="form-control" type="number" />
                                <span v-if="v$.promotion.stockLimit.$error" class="error text-danger">
                                    <span v-if="!v$.promotion.stockLimit.required">{{ $t('AddPromotion.Required') }}</span>
                                </span>
                            </div>
                        </div>
                        <div class="form-group col-md-12" v-if="allowBranches">
                            <label class="text  font-weight-bolder"> {{ $t('AddBranchUsers.Branch') }}:</label>
                            <div >
                                <branch-dropdown v-model="promotion.branchesIdList" :modelValue="promotion.branchesIdList" :ismultiple="true" />
                            </div>
                        </div>
                        <div class="form-group col-md-12">
                            <!--<div class="checkbox form-check-inline mx-2">
                                <input type="checkbox" id="inlineCheckbox3" v-model="promotion.includingBaseQuantity">
                                <label for="inlineCheckbox3"> Including Base Quantity  </label>
                            </div>-->

                            <div class="checkbox form-check-inline mx-2">
                                <input type="checkbox" id="inlineCheckbox2" v-model="promotion.isActive">
                                <label for="inlineCheckbox2"> {{ $t('AddPromotion.Active') }}  </label>
                            </div>
                        </div>


                        <div class="col-sm-12 arabicLanguage">
                            <button type="button" class="btn btn-outline-primary me-2" :disabled="v$.promotion.$invalid" v-on:click="SaveOffer" v-if="promotion.id=='00000000-0000-0000-0000-000000000000' && isValid('CanAddPromotionOffer') "><i class="far fa-save"></i> {{ $t('AddPromotion.btnSave') }}</button>
                            <button type="button" class="btn btn-outline-primary me-2" :disabled="v$.promotion.$invalid" v-on:click="SaveOffer" v-if="promotion.id!='00000000-0000-0000-0000-000000000000' && isValid('CanEditPromotionOffer') ">{{ $t('AddPromotion.btnUpdate') }}</button>

                            <button type="button" class="btn btn-outline-danger" v-on:click="Close()">{{ $t('AddPromotion.btnClear') }}</button>
                        </div>
                    </div>
                </div>
                <loading v-model:active="loading" :can-cancel="true" :is-full-page="true"></loading>
            </div>
        </div>
    </div>
    <div v-else> <acessdenied></acessdenied></div>
</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'
    import { required, maxLength } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'

    import moment from 'moment';    
    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    import Multiselect from "vue-multiselect";
    export default ({
        mixins: [clickMixin],
        components: {
            Multiselect,
            Loading
        },
        setup() {
            return { v$: useVuelidate() }
        },
        data: function () {
            return {
                promotion: {
                    id: '00000000-0000-0000-0000-000000000000',
                    offer: '',
                    productId: '',
                    discountType: '',
                    discount: 0,
                    toDate: '',
                    fromDate: '',
                    baseQuantity: 0,
                    upToQuantity: 0,
                    stockLimit: 0,
                    includingBaseQuantity: false,
                    isActive: true,

                    promotionType: 'BuyNGetNSameGroup',
                    getProductId: '',
                    productGroupId: '',
                    sunday: false,
                    monday: false,
                    tuesday: false,
                    wednesday: false,
                    thursday: false,
                    friday: false,
                    saturday: false,
                    buy: 0,
                    get: 0,
                    branchId: '',
                    branchesIdList:[]
                },
                productList: '',
                language: 'Nothing',
                listOfProduct: [],
                options: [],
                loading: false,
                allowBranches: false,
                daterander: 0
            }
        },
        validations() {
            return {
                promotion:
                {
                    offer: { required, maxLength: maxLength(50) },
                    promotionType: { required },
                    //productId: { required },
                    //discountType: {
                    //    required
                    //},
                    discount: {
                        required
                    },
                    toDate: { required },
                    fromDate: { required },
                    baseQuantity: { required },
                    stockLimit: { required },
                }
                }
        },
        methods: {
            languageChange: function (lan) {

                if (this.language == lan) {
                    if (this.promotion.id == '00000000-0000-0000-0000-000000000000') {

                        var getLocale = this.$i18n.locale;
                        this.language = getLocale;

                        this.$router.go('/addSale');
                    }
                    else {
                        this.$swal({
                            title: (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Error!' : 'خطأ',
                            text: (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'You cannot Change language during update, otherwise your current page data will be lose!' : 'لا يمكنك تغيير اللغة أثناء التحديث ، وإلا ستفقد بيانات صفحتك الحالية!',
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
                this.$router.push('/promotion');
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
                //if (this.promotion.id == '00000000-0000-0000-0000-000000000000') {
                //    this.promotion.productId.push({ id: root.productList });
                //}
                root.promotion.toDate = root.promotion.toDate + " " + moment().format("hh:mm A");
                root.promotion.fromDate = root.promotion.fromDate + " " + moment().format("hh:mm A");
                root.promotion.branchId = localStorage.getItem('BranchId');


                root.$https
                    .post('/Product/SavePromotionOffer', root.promotion, { headers: { "Authorization": `Bearer ${token}` } })
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
                                    root.$router.push('/promotion');
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
                                    root.$router.push('/promotion');
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
                        root.$swal.fire(
                            {
                                icon: 'error',
                                title: (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Something Went Wrong!' : 'هل هناك خطب ما!',
                                text: error,
                                showConfirmButton: false,
                                timer: 1000,
                                timerProgressBar: true,

                            });

                        root.loading = false
                    })
                    .finally(() => root.loading = false)
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
            this.options = ['%', localStorage.getItem('currency')]

            if (this.$route.query.data == undefined) {
                this.promotion.fromDate = moment().format('llll');
                this.promotion.toDate = moment().format('llll');
                this.daterander++;
            }
            if (this.$route.query.data != undefined) {
                this.promotion = this.$route.query.data;
                this.promotion.id = this.$route.query.data.id;
                this.daterander++;
            }
        }
    })
</script>