<template>
    <div class="col-md-12 ml-auto mr-auto" v-if="isValid('CanAddProductionRecipe')">

        <div class="col-lg-12">

            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 v-if="purchase.id === '00000000-0000-0000-0000-000000000000'" class="page-title">
                                    {{
                                        $t('AddRecipeNo.AddRecipe')
                                    }}
                                </h4>
                                <h4 v-else class="page-title">{{ $t('AddRecipeNo.UpdateRecipe') }} </h4>
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
                            <span class="tooltip-container text-dashed-underline ">{{ $t('AddRecipeNo.Invoice') }} #</span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <input v-model="purchase.registrationNo" class="form-control" type="text" disabled>
                        </div>
                    </div>

                    <div class="row form-group">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline ">{{ $t('AddRecipeNo.Date') }} :</span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <input v-model="purchase.date" class="form-control" type="text" disabled>
                        </div>
                    </div>

                    <div class="row form-group">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline ">
                                {{ $t('AddRecipeNo.RecipeName') }}: <span class="text-danger"> *</span>
                            </span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <input type="text" v-model="purchase.recipeName" class="form-control" />
                        </div>
                    </div>


                </div>
                <div class="col-lg-6">
                    <div class="row form-group">
                        <label class="col-form-label col-lg-4">
                            {{ $t('AddRecipeNo.FinishingProduct') }} :<span class="text-danger"> *</span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <product-dropdown  v-model="purchase.productId" 
                                              @update:modelValues="test"  :raw="false" :emptyselect="true" />
                        </div>
                    </div>
                    <div class="row form-group">
                        <label class="col-form-label col-lg-4">
                            {{ $t('AddRecipeNo.Quantity') }} :<span class="text-danger"> *</span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <input class="form-control" v-model="purchase.quantity" />
                        </div>
                    </div>

                    <div class="row form-group">
                        <label class="col-form-label col-lg-4">
                            {{ $t('AddRecipeNo.ExpireOn') }} :
                        </label>
                        <div class="inline-fields col-lg-8">
                            <datepicker v-model="purchase.expireDate" :key="daterander">
                            </datepicker>
                        </div>
                    </div>

                    <div class="row form-group">
                        <label class="col-form-label col-lg-4">
                        </label>
                        <div class="inline-fields col-lg-8">
                            <div class="checkbox form-check-inline mx-2">
                                <input type="checkbox" id="inlineCheckbox1" v-model="purchase.isActive">
                                <label for="inlineCheckbox1">{{ $t('AddRecipeNo.IsActive') }}</label>
                            </div>

                        </div>
                    </div>

                </div>

                <recipe-item @update:modelValue="SavePurchaseItems" v-bind:purchase="purchase" :key="purchaseItemRander" />

                <div class="col-lg-12 invoice-btn-fixed-bottom">
                    <div v-if="purchase.id === '00000000-0000-0000-0000-000000000000'">
                        <button class="btn btn-outline-primary  me-2" v-if="isValid('CanAddProductionRecipe')"
                                v-on:click="savePurchase('Draft')"
                                v-bind:disabled="v$.purchase.$invalid || purchase.recipeNoItems.filter(x => x.quantity == '').length > 0">
                            <i class="far fa-save"></i> {{ $t('AddRecipeNo.SaveAsDraft') }}
                        </button>
                        <button class="btn btn-outline-primary  me-2" v-if="isValid('CanAddProductionRecipe')"
                                v-on:click="savePurchase('Approved')"
                                v-bind:disabled="v$.purchase.$invalid || purchase.recipeNoItems.filter(x => x.quantity == '').length > 0">
                            <i class="far fa-save"></i> {{ $t('AddRecipeNo.SaveAsPost') }}
                        </button>
                        <button class="btn btn-danger  me-2" v-on:click="goToPurchase">
                            {{ $t('AddRecipeNo.Cancel') }}
                        </button>
                    </div>
                    <div v-else>
                        <button class="btn btn-outline-primary  me-2" v-on:click="savePurchase('Draft')"
                                v-if="isValid('CanAddProductionRecipe')"
                                v-bind:disabled="v$.purchase.$invalid || purchase.recipeNoItems.filter(x => x.quantity == '').length > 0">
                            <i class="far fa-save"></i> {{ $t('AddRecipeNo.UpdateAsDraft') }}
                        </button>

                        <button class="btn btn-outline-primary  me-2" v-on:click="savePurchase('Approved')"
                                v-if="isValid('CanAddProductionRecipe')"
                                v-bind:disabled="v$.purchase.$invalid || purchase.recipeNoItems.filter(x => x.quantity == '').length > 0">
                            <i class="far fa-save"></i> {{ $t('AddRecipeNo.UpdateAsPost') }}
                        </button>
                        <button class="btn btn-danger  me-2" v-on:click="goToPurchase">
                            {{ $t('AddRecipeNo.Cancel') }}
                        </button>
                    </div>
                </div>
            </div>
            <loading v-model:active="loading" :can-cancel="true" :is-full-page="true"></loading>
        </div>
    </div>
    <div v-else>
        <acessdenied></acessdenied>
    </div>
</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'
    import moment from "moment";

    import { required } from '@vuelidate/validators'; 
    import useVuelidate from '@vuelidate/core';
    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';

    export default {
        mixins: [clickMixin],
        components: {
            Loading
        },
        setup() {
            return { v$: useVuelidate() }
        },
        data: function () {
            return {
                language: 'Nothing',
                daterander: 0,
                purchaseItemRander: 0,
                rander: 0,
                purchase: {
                    id: "00000000-0000-0000-0000-000000000000",
                    recipeName: "",
                    date: "",
                    registrationNo: "",
                    expireDate: "",
                    productId: "",
                    quantity: "",
                    recipeNoItems: [],
                    isActive: true,
                },
                loading: false,
            };
        },
        validations() {
            return {
                purchase: {
                    recipeName: { required },
                    date: {required},
                    expireDate: {required},
                    registrationNo: { required },
                    quantity: { required },
                    productId: { required },
                },
            }
        },
        methods: {
            GotoPage: function (link) {
                this.$router.push({ path: link });
            },
            test(id){
                this.purchase.productId = id;
            },
            GetSampleRecord: function (Id) {

                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('/Batch/SampleRequestDetail?Id=' + Id, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {

                        if (response.data != null) {
                            root.purchase.recipeNoItems = response.data.sampleRequestItems;
                            root.purchaseItemRander++;
                        }
                        else {
                            console.log("error: something wrong from db.");
                        }
                    },
                        function (error) {
                            root.loading = false;
                            console.log(error);
                        });

            },

            languageChange: function (lan) {
                if (this.language == lan) {
                    if (this.purchase.id == '00000000-0000-0000-0000-000000000000') {

                        var getLocale = this.$i18n.locale;
                        this.language = getLocale;

                        this.$router.go('/AddRecipeNo');
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
            AutoIncrementCode: function () {
                var root = this;
                var token = "";
                if (token == '') {
                    token = localStorage.getItem("token");
                }
                root.$https
                    .get("/Batch/RecipeNoAutoGenerateNo", {
                        headers: { Authorization: `Bearer ${token}` },
                    })
                    .then(function (response) {
                        if (response.data != null) {
                            root.purchase.registrationNo = response.data;
                        }
                    });
            },
            SavePurchaseItems: function (recipeNoItems) {

                this.purchase.recipeNoItems = recipeNoItems;
            },
            savePurchase: function (status) {
                this.purchase.approvalStatus = status
                this.loading = true;
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                this.$https
                    .post('/Batch/SaveRecipeNoInformation', root.purchase, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(response => {
                        root.loading = false
                        root.info = response.data

                        root.$swal({
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved!' : '!تم الحفظ',
                            text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Data Saved Successfully!' : '!حفظ بنجاح',
                            type: 'success',
                            icon: 'success',
                            timer: 1500,
                            timerProgressBar: true,
                        }).then(function (response) {
                            if (response != undefined) {
                                if (root.purchase.id == "00000000-0000-0000-0000-000000000000") {
                                    root.$router.push({
                                        path: '/RecipeNo',
                                        query: { data: status }
                                    });

                                } else {
                                    root.$router.push({
                                        path: '/RecipeNo',
                                        query: { data: status }
                                    });
                                }
                            }
                        });

                    })
                    .catch(error => {
                        console.log(error)
                        root.$swal.fire(
                            {
                                icon: 'error',
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Something Went Wrong!' : 'هل هناك خطب ما!',
                                text: error,
                            });

                        root.loading = false
                    })
                    .finally(() => root.loading = false)

            },

            goToPurchase: function () {
                this.$router.push('/RecipeNo');
            },
        },
        created: function () {
            var root =  this;

            if(root.$route.query.Add == 'false')
            {
                root.$route.query.data = this.$store.isGetEdit;
            }
            this.$emit('update:modelValue', this.$route.name);
            if (this.$route.query.data != undefined) {
                this.purchase = root.$route.query.data;
                this.attachment = true;
                this.rander++;
            }
        },
        mounted: function () {
            var getLocale = this.$i18n.locale;
            this.language = getLocale;
            if (this.$route.query.data == undefined) {
                this.AutoIncrementCode();

                this.purchase.date = moment().format('llll');
                this.daterander++;
            }
        },
    };
</script>
