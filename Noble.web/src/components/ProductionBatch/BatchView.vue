<template>
    <div class="col-md-12 ml-auto mr-auto" v-if="isValid('CanViewProductionBatch')" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
        <div class="card">
            <div class="card-body">
            
                <div class="col-lg-12">
                    <div class="tab-content" id="nav-tabContent">
                        <div class="card-header p-0">
                            <div class="row DayHeading" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                                <div class="col-lg-6" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                                    <span>{{ $t('BatchView.ProductionBatch') }} - {{purchase.registrationNo}}</span>
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
                                    {{ $t('BatchView.NoOfBatches') }}
                                </div>
                                <div class="col-lg-6" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-right' : 'text-left'">
                                    {{ purchase.noOfBatches }}
                                </div>

                                <div class="col-lg-6">
                                    {{ $t('BatchView.SaleOrder') }}
                                </div>
                                <div class="col-lg-6" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-right' : 'text-left'">
                                    {{ purchase.saleOrderNo }}
                                </div>

                                <div class="col-lg-6">
                                    {{ $t('BatchView.RecipeNumber') }}
                                </div>
                                <div class="col-lg-6" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-right' : 'text-left'">
                                    {{ purchase.recipeName }}
                                </div>

                                <div class="col-lg-6">
                                    {{ $t('BatchView.FromDate') }}
                                </div>
                                <div class="col-lg-6" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-right' : 'text-left'">
                                    {{ purchase.startTime }}
                                </div>

                                <div class="col-lg-6">
                                    {{ $t('BatchView.ToDate') }}
                                </div>
                                <div class="col-lg-6" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-right' : 'text-left'">
                                    {{ purchase.endTime }}
                                </div>
                            </div>
                        </div>
                        <br />
                        <batch-view-item @update:modelValue="SavePurchaseItems" v-bind:purchase="purchase" :key="purchaseItemRander" />

                    </div>
                    <div v-if="!loading" class="col-md-12 text-right">
                       
                        <div >
                            <button class="btn btn-danger  mr-2"
                                    v-on:click="goToPurchase">
                                {{ $t('BatchView.Cancel') }}
                            </button>
                        </div>




                    </div>
                    <div class="card-footer col-md-3" v-else>
                        <loading v-model:active="loading"
                                 :can-cancel="true"
                                 :on-cancel="onCancel"
                                 :is-full-page="true"></loading>
                    </div>
                    <remainingStockmodel :purchase="purchase"
                                         :show="show"
                                         v-if="show"
                                         @close="show=false" />
                </div>
            </div>
        </div>
    </div>
    <div v-else> <acessdenied></acessdenied></div>

</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'
    //
    import moment from "moment";
    //
    import { required } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core';
    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';

    //import VueBarcode from 'vue-barcode';
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
                purchaseItemRander: 0,
                purchase: {
                    id: "00000000-0000-0000-0000-000000000000",
                    date: "",
                    registrationNo: "",
                    expireDate: "",
                    recipeNoId: "",
                    saleOrderId: "",
                    noOfBatches: 1,
                    productionBatchItems: [],
                    startTime: "",
                    endTime: "",

                },
                loading: false,
                show: false,
            };
        },
        validations() {
            return {
                purchase: {
                    date: { required },
                    expireDate: {},
                    registrationNo: { required },
                    noOfBatches: { required },
                    recipeNoId: {
                        required,

                    },


                    productionBatchItems: { required },
                },
                }
        },
        methods: {

            EffectOnItems: function () {
                this.purchaseItemRander++;
            },
            GetFinishProduct: function (id) {
                var root = this;
                var token = "";
                if (token == '') {
                    token = localStorage.getItem("token");
                }

                root.$https.get('/Batch/RecipeNoDetail?Id=' + id, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data != null) {

                            root.purchase.productionBatchItems = response.data.recipeNoItems;
                            root.purchaseItemRander++;

                        }
                    });

            },

            AutoIncrementCode: function () {
                var root = this;
                var token = "";
                if (token == '') {
                    token = localStorage.getItem("token");
                }
                root.$https
                    .get("/Batch/ProductionBatchAutoGenerateNo", {
                        headers: { Authorization: `Bearer ${token}` },
                    })
                    .then(function (response) {
                        if (response.data != null) {
                            root.purchase.registrationNo = response.data;
                        }
                    });
            },
            SavePurchaseItems: function (productionBatchItems) {

                this.purchase.productionBatchItems = productionBatchItems;
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
                    .post('/Batch/SaveProductionBatchInformation', root.purchase, { headers: { "Authorization": `Bearer ${token}` } })
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
                                    root.$router.go('ProductionBatch');

                                } else {
                                    root.$router.push("ProductionBatch");
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
                this.$router.push('/ProductionBatch');
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
            }
        },
        mounted: function () {

        },
    };
</script>
