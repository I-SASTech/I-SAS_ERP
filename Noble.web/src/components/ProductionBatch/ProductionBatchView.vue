<template>
    <div class="col-md-12 ml-auto mr-auto" v-if="isValid('CanAddProductionBatch')" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
        <div class="card">
            <div class="card-body">
                <div class="col-lg-12">
                    <div class="tab-content" id="nav-tabContent">
                        <div class="card-header p-0">
                            <div class="row DayHeading" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                                <div class="col-lg-6" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                                    <span>{{ $t('ProductionBatchView.ProductionBatch') }} - {{purchase.registrationNo}}</span>
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
                                    <div class="row">
                                        <div class="col-lg-6">
                                            <div>
                                                {{ $t('ProductionBatchView.NoOfBatches') }} :
                                            </div>
                                            <div>
                                                {{ $t('ProductionBatchView.SaleOrder') }} :
                                            </div>
                                            <div>
                                                {{ $t('ProductionBatchView.RecipeName') }}   :
                                            </div>
                                            <div>
                                                {{ $t('ProductionBatchView.FromDate') }}  :
                                            </div>
                                            <div>
                                                {{ $t('ProductionBatchView.ToDate') }} :

                                            </div>
                                            <div>
                                                {{ $t('ProductionBatchView.AssumptionStock') }}  :


                                            </div>
                                            <div>
                                                {{ $t('ProductionBatchView.ActualStock') }} :


                                            </div>
                                            <div>
                                                {{ $t('ProductionBatchView.DamageStock') }} :

                                            </div>


                                        </div>
                                        <div class="col-lg-6">
                                            <div>
                                                {{ purchase.noOfBatches }}
                                            </div>
                                            <div>
                                                {{ purchase.saleOrderNo==null?'--':purchase.saleOrderNo }}
                                            </div>
                                            <div>
                                                {{ purchase.recipeName }}
                                            </div>
                                            <div>
                                                {{ purchase.startTime==null?'--':purchase.startTime }}
                                            </div>
                                            <div>
                                                {{ purchase.endTime==null?'--':purchase.endTime }}
                                            </div>
                                            <div>
                                                {{purchase.recipeQuantity*purchase.noOfBatches}}
                                            </div>
                                            <div>
                                                {{purchase.remainingStock}}
                                            </div>
                                            <div>
                                                {{purchase.damageStock==null?'0':purchase.damageStock}}
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-6" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-right' : 'text-left'">
                                    <div class="row">
                                        <div class="col-lg-6">
                                            <div>{{ $t('ProductionBatchView.ProcessBy') }}   :  </div>
                                            <div>
                                                {{ $t('ProductionBatchView.ProcessDate') }}  :
                                            </div>
                                            <div>
                                                {{ $t('ProductionBatchView.CompleteBy') }}   :
                                            </div>
                                            <div>
                                                {{ $t('ProductionBatchView.CompletionDate') }} :
                                            </div>
                                            <div>
                                                {{ $t('ProductionBatchView.TransferBy') }} :
                                            </div>
                                            <div>
                                                {{ $t('ProductionBatchView.TransferDate') }} :
                                            </div>
                                        </div>
                                        <div class="col-lg-6">
                                            <div>{{ purchase.processBy }} </div>
                                            <div>
                                                {{ purchase.processDate }}
                                            </div>
                                            <div>
                                                {{ purchase.completeBy }}
                                            </div>
                                            <div>
                                                {{ purchase.completeDate }}
                                            </div>
                                            <div>
                                                {{ purchase.transferBy }}
                                            </div>
                                            <div>
                                                {{ purchase.transferDate }}
                                            </div>
                                        </div>
                                    </div>

                                </div>


                            </div>
                            <div class="row pt-3">
                                <div class="form-group has-label col-sm-6 ">
                                    <label class="text  font-weight-bolder">  {{ $t('ProductionBatchView.ReasonforLateProcess') }}  :</label>
                                    {{ purchase.lateReason }}
                                    <!--<textarea class="form-control" style="background-color:white !important" disabled v-model="purchase.lateReason" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'" type="text" />-->

                                </div>
                                <div class="form-group has-label col-sm-6 ">
                                    <label class="text  font-weight-bolder"> {{ $t('ProductionBatchView.ReasonforLateCompletion') }} :</label>
                                    {{ purchase.lateReasonCompletion }}
                                    <!--<textarea class="form-control" style="background-color:white !important" disabled  v-model="purchase.lateReasonCompletion" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'" type="text" />-->

                                </div>
                                <!--<div class="col-lg-6"> <span class="pr-3">Reason for Late Process :</span> <span class="lead"> {{ purchase.lateReason }}</span></div>
                                <div class="col-lg-6"><span class="pr-3">Reason for Late Completion :</span> <span class="lead">{{ purchase.lateReasonCompletion }}</span> </div>-->
                            </div>

                        </div>
                        <br />
                        <batch-view-item @update:modelValue="SavePurchaseItems" v-bind:purchase="purchase" :key="purchaseItemRander" />
                        <div class="row p-3">
                            <div class="table-responsive tablecolor ">
                                <table class="table table-striped table-hover table_list_bg">
                                    <thead class="">
                                        <tr>
                                            <th></th>
                                            <th>
                                                {{ $t('ProductionBatchView.ProductName') }}
                                            </th>
                                            <th>
                                                {{ $t('ProductionBatchView.WareHouseName') }}

                                            </th>
                                            <th>
                                                {{ $t('ProductionBatchView.AssumptionStock') }}

                                            </th>
                                            <th>
                                                {{ $t('ProductionBatchView.ActualStock/DamageStock') }}


                                            </th>



                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td></td>
                                            <td>
                                                {{stockDetail.productName}}
                                            </td>
                                            <td>
                                                {{stockDetail.remainingWareHouseName}}
                                            </td>
                                            <td>
                                                <span class="badge badge-success">   {{purchase.recipeQuantity*purchase.noOfBatches}}</span>


                                            </td>
                                            <td>
                                                {{ $t('ProductionBatchView.ActualStock') }}  :  <span class="badge badge-primary">{{purchase.remainingStock}}</span>
                                            </td>


                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td>
                                                {{stockDetail.productName}}
                                            </td>
                                            <td>
                                                {{stockDetail.damageWareHouseName}}
                                            </td>
                                            <td>
                                                <!--<span class="badge badge-success">   {{purchase.recipeQuantity*purchase.noOfBatches}}</span>-->
                                            </td>
                                            <td>
                                                {{ $t('ProductionBatchView.DamageStock') }} : <span class="badge badge-danger"> {{purchase.damageStock==null?'0':purchase.damageStock}}</span>

                                                :p

                                            </td>


                                        </tr>
                                    </tbody>
                                </table>

                            </div>
                        </div>

                    </div>
                    <div v-if="!loading" class="col-md-12 text-right">

                        <div>
                            <button class="btn btn-danger  mr-2"
                                    v-on:click="goToPurchase">
                                {{ $t('ProductionBatchView.Cancel') }}
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

    //import Vue3Barcode from 'vue3-barcode'
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
                stockDetail: [],
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



            GetActualAndDamge: function (id) {
                var root = this;
                var token = "";
                if (token == '') {
                    token = localStorage.getItem("token");
                }
                root.$https.get('/Batch/ProductionStockTransferBatchDetail?Id=' + id, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data != null) {
                            
                            root.stockDetail = response.data;
                        }
                    },
                        function (error) {
                            this.loading = false;
                            console.log(error);
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
            if (this.$route.query.data == undefined) {
                this.AutoIncrementCode();

                this.purchase.date = moment().format('llll');
            }

            this.GetActualAndDamge(this.purchase.id);

        },
    };
</script>
<style scoped>
    .tableHoverOn {
        background-color: #f9fbfe !important;
    }

    .tablecolor {
        background-color: #f9f9f9 !important;
    }
</style>
