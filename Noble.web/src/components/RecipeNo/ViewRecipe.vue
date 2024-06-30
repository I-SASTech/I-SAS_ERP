<template>
    <div class="col-md-12 ml-auto mr-auto" v-if="isValid('CanViewProductionRecipe') " v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
        <div class="card" v-bind:style="$i18n.locale == 'ar' ? languageChange('en') : languageChange('ar')">
            <div class="card-body">
                <div class="col-lg-12">
                    <div class="tab-content" id="nav-tabContent">
                        <div class="card-header">
                            <div class="row" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                                <div class="col-lg-6" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                                    <h5>{{ $t('ViewRecipe.RecipeNumber') }} - {{purchase.registrationNo}}</h5>
                                </div>
                                <div class="col-lg-6" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'arabicLanguage' : 'text-left'">
                                    <span>
                                        {{purchase.date}}
                                    </span>
                                </div>
                            </div>
                        </div>
                        <div class="card-body">
                            <div class="row">
                                <div class="col-lg-6">
                                    {{ $t('ViewRecipe.RecipeName') }}
                                </div>
                                <div class="col-lg-6" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-right' : 'text-left'">
                                    {{ purchase.recipeName }}
                                </div>

                                <div class="col-lg-6">
                                    {{ $t('ViewRecipe.FinishingProduct') }}
                                </div>
                                <div class="col-lg-6" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-right' : 'text-left'">
                                    {{ purchase.englishName }}
                                </div>

                                <div class="col-lg-6">
                                    {{ $t('ViewRecipe.Quantity') }}
                                </div>
                                <div class="col-lg-6" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-right' : 'text-left'">
                                    {{ purchase.quantity }}
                                </div>

                                <div class="col-lg-6" hidden>
                                    {{ $t('ViewRecipe.ExpireOn') }}
                                </div>
                                <div class="col-lg-6" hidden v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-right' : 'text-left'">
                                    {{ purchase.expireDate }}
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-lg-12">
                                    <view-recipe-item @update:modelValue="SavePurchaseItems" />
                                </div>
                            </div>
                        </div> 
                    </div>
                    <div class="col-md-12 text-right">
                        <div>                            
                            <button class="btn btn-danger  mr-2"
                                    v-on:click="goToPurchase">
                                {{ $t('ViewRecipe.Cancel') }}
                            </button>
                        </div>
                    </div>
                    <!--<div class="card-footer col-md-3" v-else>
                        <loading v-model:active="loading"
                                 :can-cancel="true"
                                 :on-cancel="onCancel"
                                 :is-full-page="true"></loading>
                    </div>-->
                </div>
            </div>
        </div>
    </div>
    <div v-else> <acessdenied></acessdenied></div>
</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'
    import moment from "moment";
    
    import { required } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core';

    export default {
        mixins: [clickMixin],
        setup() {
            return { v$: useVuelidate() }
        },
        data: function () {
            return {
                language: 'Nothing',
                daterander: 0,
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
                },
                loading: false,
            };
        },
        validations() {
            return {
                purchase: {
                    recipeName: { required },
                    date: { required },
                    expireDate: {},
                    registrationNo: { required },
                    quantity: { required },
                    productId: { required },
                    recipeNoItems: { required },
                },
                }
        },
        methods: {
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
            SavePurchaseItems: function (recipeNoItems) {

                this.purchase.recipeNoItems = recipeNoItems;
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
            
            if (this.$route.query.data != undefined) {
                this.purchase = this.$route.query.data;
                this.purchase.date = moment(this.purchase.date).format('LLL');
                this.attachment = true;
                this.rander++;
            }
        },
        mounted: function () {
            var getLocale = this.$i18n.locale;
            this.language = getLocale;
           
        },
    };
</script>
