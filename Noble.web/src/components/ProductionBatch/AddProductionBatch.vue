<template>
    <div class="row " v-if="isValid('CanAddProductionBatch')">
        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 v-if="purchase.id === '00000000-0000-0000-0000-000000000000'" class="page-title">
                                    {{ $t('AddProductionBatch.AddProductionBatch') }}
                                </h4>
                                <h4 v-else class="page-title">{{ $t('AddProductionBatch.UpdateProductionBatch') }}</h4>
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
                            <span class="tooltip-container text-dashed-underline ">{{ $t('AddProductionBatch.Invoice') }} #</span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <input v-model="purchase.registrationNo" class="form-control" type="text" disabled>
                        </div>
                    </div>

                    <div class="row form-group">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline ">{{ $t('AddProductionBatch.Date') }} :</span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <input v-model="purchase.date" class="form-control" type="text" disabled>
                        </div>
                    </div>

                    <div class="row form-group">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline ">
                                {{
                                    $t('AddProductionBatch.SaleOrder')
                                }}:
                            </span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <saleorderdropdown v-model="purchase.saleOrderId" :modelValue="purchase.saleOrderId">
                            </saleorderdropdown>
                        </div>
                    </div>


                </div>
                <div class="col-lg-6">
                    <div class="row form-group">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline ">
                                {{ $t('AddProductionBatch.RecipeNumber') }} : <span class="text-danger">*</span>
                            </span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <reciptdropdown v-model="purchase.recipeNoId" :modelValue="purchase.recipeNoId"
                                            @update:modelValue="GetFinishProduct(purchase.recipeNoId)" />
                        </div>
                    </div>

                    <div class="row form-group">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline ">
                                {{ $t('AddProductionBatch.FromDate') }} :
                            </span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <datepicker v-model="purchase.startTime" />
                        </div>
                    </div>

                    <div class="row form-group">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline ">
                                {{ $t('AddProductionBatch.Employee') }} :
                            </span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <employeeDropdown v-model="purchase.employeeRegistrationId"></employeeDropdown>
                        </div>
                    </div>

                </div>

                <div class="card">
                    <div class="card-body ">
                        <div class="row">
                            <div class="col-lg-4 col-sm-12">
                                <h4>{{ $t('AddProductionBatch.Processes') }} </h4>
                                <draggable class="dragArea list-group w-full" v-model="resultQuery" :list="list" @change="log">
                                    <div v-for="process in resultQuery" v-bind:key="process.id"
                                         v-on:click="GetContractor(process.id)"
                                         v-bind:class="process.isActive ? 'opacity50' : ''">
                                        <div class="card">
                                            <div class="card-body d-flex align-items-center">
                                                <div class="col-4">
                                                    <div class="rounded-circle text-center"
                                                         style="width:40px; height: 40px;"
                                                         :style="{ 'background-color': process.color }">
                                                        <i style="padding-top: 14px;"
                                                           class="fas fa-clock text-white"></i>
                                                    </div>
                                                </div>
                                                <div class="col-4">
                                                    <div class="">
                                                        <h5 class="">{{ process.englishName }}</h5>
                                                        <p>{{ process.description }}</p>
                                                    </div>

                                                </div>

                                                <div class="col-4 text-center">
                                                    <div class="">
                                                        <img src="../../../public/images/DragAndDrop.png"
                                                             class="img-fluid" />
                                                    </div>
                                                </div>
                                                <a style="position: absolute; top: 4px; right: 5px;"
                                                   href="javascript:void(0)"
                                                   v-on:click="RemoveProcess(process.id, true)" class="text-danger ">
                                                    <i class="fa fa-window-close" aria-hidden="true"></i>
                                                </a>

                                            </div>
                                        </div>
                                    </div>

                                </draggable>
                                <!--<draggable v-model="resultQuery">
                                    <div v-for="process in resultQuery" v-bind:key="process.id"
                                         v-on:click="GetContractor(process.id)"
                                         v-bind:class="process.isActive ? 'opacity50' : ''">
                                        <div class="card">
                                            <div class="card-body d-flex align-items-center">
                                                <div class="col-4">
                                                    <div class="rounded-circle text-center"
                                                         style="width:40px; height: 40px;"
                                                         :style="{ 'background-color': process.color }">
                                                        <i style="padding-top: 14px;"
                                                           class="fas fa-clock text-white"></i>
                                                    </div>
                                                </div>
                                                <div class="col-4">
                                                    <div class="">
                                                        <h5 class="">{{ process.englishName }}</h5>
                                                        <p>{{ process.description }}</p>
                                                    </div>

                                                </div>

                                                <div class="col-4 text-center">
                                                    <div class="">
                                                        <img src="../../../public/images/DragAndDrop.png"
                                                             class="img-fluid" />
                                                    </div>
                                                </div>
                                                <a style="position: absolute; top: 4px; right: 5px;"
                                                   href="javascript:void(0)"
                                                   v-on:click="RemoveProcess(process.id, true)" class="text-danger ">
                                                    <i class="fa fa-window-close" aria-hidden="true"></i>
                                                </a>

                                            </div>
                                        </div>
                                    </div>

                                </draggable>-->
                            </div>
                            <div class="col-lg-4 col-sm-12">
                                <h4>{{ $t('AddProductionBatch.ContractorsOrSupervisor') }} </h4>

                                <div v-for="contractor in contractorlist" v-bind:key="contractor.contractorId"
                                     v-on:click="SelectContractor(contractor.contractorId, contractor.processId)"
                                     v-bind:class="contractor.isActive ? 'opacity50' : ''">
                                    <div class="card">
                                        <div class="card-body d-flex align-items-center">
                                            <div class="col-4">
                                                <img src="../../../public/images/Frame.png" />
                                            </div>
                                            <div class="col-4">
                                                <div class="">
                                                    <h6 class="">{{ contractor.contractorNameEn }}</h6>
                                                    <p class="description">Subtitle Text</p>
                                                </div>
                                            </div>
                                            <div class="col-4 text-center">
                                                <img src="../../../public/images/DragAndDrop.png"
                                                     style="align-self:center" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-4 col-sm-12">
                                <h4>{{ $t('AddProductionBatch.ListOfItems') }} </h4>
                                <productionbatch-item @update:modelValue="SavePurchaseItems" v-bind:purchase="purchase"
                                                      :key="purchaseItemRander" />
                            </div>
                        </div>

                    </div>

                    <div class="col-lg-12 invoice-btn-fixed-bottom">
                        <div v-if="!loading" class="col-md-12 text-right">
                            <div v-if="purchase.id === '00000000-0000-0000-0000-000000000000'">

                                <div>
                                    <button class="btn btn-outline-primary  me-2" v-if="isValid('CanAddProductionBatch')"
                                            v-on:click="savePurchase('Draft')">
                                        <i class="far fa-save"></i> {{ $t('AddProductionBatch.SaveAsDraft') }}
                                    </button>
                                    <button class="btn btn-danger  me-2" v-on:click="goToPurchase">
                                        {{ $t('AddProductionBatch.Cancel') }}
                                    </button>
                                </div>

                            </div>
                            <div v-else>
                                <button class="btn btn-outline-primary  me-2" v-on:click="savePurchase('Rejected')">
                                    <i class="far fa-save"></i> {{ $t('AddProductionBatch.UpdateAsRejected') }}
                                </button>
                                <button class="btn btn-outline-primary  me-2" v-on:click="savePurchase('Draft')"
                                        v-if="isValid('CanAddProductionBatch')">
                                    <i class="far fa-save"></i> {{ $t('AddProductionBatch.UpdateAsDraft') }}
                                </button>

                                <button class="btn btn-danger  me-2" v-on:click="goToPurchase">
                                    {{ $t('AddProductionBatch.Cancel') }}
                                </button>
                            </div>
                        </div>
                    </div>

                    <loading v-model:active="loading" :can-cancel="true" :is-full-page="true"></loading>
                    <remainingStockmodel :purchase="purchase" :show="show" v-if="show" @close="show = false" />
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

    import moment from "moment";
    //import VueCtkDateTimePicker from 'vue-ctk-date-time-picker';
    //import 'vue-ctk-date-time-picker/dist/vue-ctk-date-time-picker.css';
    //import Vue from 'vue'
    //Vue.component('VueCtkDateTimePicker', VueCtkDateTimePicker);

    import { required } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core';
    import { VueDraggableNext } from 'vue-draggable-next'
    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';

    export default {
        mixins: [clickMixin],
        components: {
            draggable: VueDraggableNext,
            Loading
        },

        setup() {
            return { v$: useVuelidate() }
        },
        data: function () {
            return {
                purchaseItemRander: 0,
                currentPage: 1,
                search: '',
                purchase: {
                    id: "00000000-0000-0000-0000-000000000000",
                    date: "",
                    registrationNo: "",
                    employeeRegistrationId: "",
                    expireDate: "",
                    recipeNoId: "",
                    saleOrderId: "",
                    noOfBatches: 1,
                    productionBatchItems: [],
                    processlist: [],
                    startTime: "",
                    endTime: "",
                    branchId: "",

                },
                ProcessActive: false,
                disable: false,
                loading: false,
                processlist: [],
                contractorlist: [],
                productList: [],
                show: false,
                isRemove: false,
            };
        },
        validations() {
            return {
                purchase: {
                    date: { required },
                    expireDate: {},
                    noOfBatches: {},
                    registrationNo: { required },
                    recipeNoId: {

                    },


                    productionBatchItems: {
                    },
                },
                }
        },
        computed: {
            resultQuery: {
                get() {

                    return this.$store.isProcessList
                },

                set(val) {

                    this.$store.GetProcessList(val);

                    this.processlist = val;
                }
            }
        },
        methods: {
            GotoPage: function (link) {
                this.$router.push({ path: link });
            },
            RemoveProcess: function (id, isRemove) {

                this.isRemove = isRemove;
                this.$store.isProcessList = this.$store.isProcessList.filter((prod) => {
                    return prod.id != id;
                });
            },

            EffectOnItems: function () {
                this.purchaseItemRander++;
            },

            SelectContractor: function (contractorId, processId) {

                var root = this;
                this.contractorlist.find(function (x) {
                    if (x.contractorId == contractorId) {
                        if (x.isActive == false) {
                            x.isActive = true;
                        }
                        else {
                            x.isActive = false;
                        }

                    }

                });
                root.processlist.forEach(function (x) {
                    if (x.id == processId) {

                        x.processContractors.forEach(function (x) {

                            if (x.contractorId == contractorId) {
                                x = root.contractorlist;
                            }

                        });
                    }
                });



            },
            GetContractor: function (id) {

                if (!this.isRemove) {
                    var root = this;
                    root.productList = [];

                    this.processlist.find(function (x) {
                        if (x.id == id) {

                            x.isActive = true;
                        }
                        else {
                            x.isActive = false;
                        }
                    });

                    this.processlist.find(function (x) {
                        if (x.id == id) {
                            root.contractorlist = x.processContractors;
                            root.productList = x.processItems;
                        }
                    });
                }
                this.isRemove = false;

            },
            GetProcessData: function () {

                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('Batch/ProcessList?isDropdown=true', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {


                        response.data.results.forEach(function (result) {
                            root.processlist.push({
                                id: result.id,
                                code: result.code,
                                color: result.color,
                                description: result.description,
                                englishName: result.englishName,
                                processContractors: result.processContractors,
                                processItems: result.processItems,
                                isActive: false,
                            })
                        })

                        root.$store.GetProcessList(root.processlist);
                    }
                    root.loading = false;
                });
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


                            //root.purchase.recipeQuantity = response.data.quantity;
                            //response.data.recipeNoItems.forEach(function (recipe) {
                            //    alert(root.processItems);
                            //    root.processItems.forEach(function (x) {
                            //        if (x.isActive == true) {
                            //            x.forEach(function (result) {
                            //                if (result.productId == recipe.productId) {
                            //                    root.purchase.productionBatchItems.push(recipe);
                            //                }
                            //            })
                            //        }


                            //    });


                            //});

                            root.purchase.recipeQuantity = response.data.quantity;
                            root.purchase.productionBatchItems = response.data.recipeNoItems;
                            //root.productList.forEach(function (result) {
                            //    response.data.recipeNoItems.forEach(function (recipe) {
                            //        if (result.productId == recipe.productId) {
                            //            root.purchase.productionBatchItems.push(recipe);
                            //        }

                            //    });

                            //});
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

                //var root = this;
                //for (var y in productionBatchItems) {
                //    if (productionBatchItems[y].wareHouseId == null) {
                //        root.disable = true;
                //        break;
                //    }
                //    else {
                //        root.disable = false;

                //    }

                //}
                this.purchase.productionBatchItems = productionBatchItems;
            },
            savePurchase: function (status) {

                var root = this;
                this.purchase.approvalStatus = status;

                this.purchase.processlist = [];
                this.$store.isProcessList.forEach(function (item) {
                    var process = root.processlist.find((value) => value.id == item.id);
                    if (process != undefined) {
                        root.purchase.processlist.push(process);
                    }
                });

                this.loading = true;

                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.purchase.branchId = localStorage.getItem('BranchId');

                this.$https.post('/Batch/SaveProductionBatchInformation', root.purchase, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(response => {
                        if (response.data != null && response.data != '00000000-0000-0000-0000-000000000000') {
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
                                    root.loading = false;

                                    if (root.purchase.id == "00000000-0000-0000-0000-000000000000") {
                                        root.$router.push({
                                            path: '/ProductionBatch',
                                            query: { data: status }
                                        });

                                    } else {
                                        root.loading = false;

                                        root.$router.push({
                                            path: '/ProductionBatch',
                                            query: { data: status }
                                        });
                                    }
                                }
                            });
                        }
                        else {
                            root.loading = false
                            root.info = response.data
                            root.$swal({
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                                text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Something Went Wrong!' : 'هل هناك خطب ما!',
                                type: 'error',
                                icon: 'error',
                                timer: 1500,
                                timerProgressBar: true,
                            });
                        }

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
            this.$emit('update:modelValue', this.$route.name);

                 var root =  this;

               if(root.$route.query.Add == 'false')
                {
                root.$route.query.data = root.$store.isGetEdit;
                }
            
            if (this.$route.query.data != undefined) {
                this.purchase = this.$route.query.data;
                this.purchase.date = moment(this.purchase.date).format('LLL');
            }
        },
        mounted: function () {
            if (this.$route.query.data == undefined) {
                this.AutoIncrementCode();
                this.GetProcessData();

                this.purchase.date = moment().format('LLL');
            }
        },
    };
</script>

