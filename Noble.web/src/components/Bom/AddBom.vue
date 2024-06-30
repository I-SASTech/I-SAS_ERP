<template>
    <div class="row">
        <div class="col-lg-12 ">
            <div class="row">
                <div class="col-sm-12">
                    <div class="col d-flex align-items-baseline">
                        <div class="media">
                            <span class="circle-singleline" style="background-color: #1761FD !important;">BM</span>
                            <div class="media-body align-self-center ms-3">
                                <h4 class="mb-0" v-if="bom.id== '00000000-0000-0000-0000-000000000000'">{{ $t('Add BOM') }} <small  >{{ getDate(bom.createdDate) }}</small ></h4> 
                                <h4 class="mb-0" v-else>{{ $t('Update BOM') }} <small  >{{ getDate(bom.createdDate) }}</small ></h4>
                                <div class="col d-flex ">
                                    <p class="text-muted mb-0" style="font-size:13px !important;"><b>{{ bom.code }}</b></p>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        
        <hr class="hr-dashed hr-menu my-0" />

        <div class="card border-0 col-md-12 mb-5">
            <div class="card-body border-0">
              <div class="col-md-12">
                <div class="row">
                    <div class="col-md-6">
                        <div class="row form-group">
                            <label class="col-form-label col-lg-4">
                                <span class="tooltip-container text-dashed-underline ">{{ $t('AddRecipeNo.Date') }} :</span>
                            </label>
                            <div class="inline-fields col-lg-8">
                                <input v-model="bom.date" class="form-control" type="text">
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6" v-if="status == 'Details'">
                        <div class="row form-group">
                            <label class="col-form-label col-lg-4">
                                <span class="tooltip-container text-dashed-underline ">{{ $t('SaleOrder') }} :</span>
                            </label>
                            <div class="inline-fields col-lg-8">
                                <input v-model="saleOrder" class="form-control" type="text" disabled>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6" v-if="status != 'Details'">
                        <div class="row form-group">
                            <label class="col-form-label col-lg-4">
                                <span class="tooltip-container text-dashed-underline ">{{ $t('SaleOrder') }} :</span>
                            </label>
                            <div class="inline-fields col-lg-8">
                                <saleorderdropdown v-model="bom.saleOrderId" :modelValue="bom.saleOrderId" @update:modelValue="GetSaleOrderItems" :isForBom="true">
                                </saleorderdropdown>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="row form-group">
                            <label class="col-form-label col-lg-4">
                                <span class="tooltip-container text-dashed-underline ">{{ $t('WareHouse') }} :</span>
                            </label>
                            <div class="inline-fields col-lg-8">
                                <warehouse-dropdown v-model="bom.wareHouseId" @update:modelValue="bom.wareHouseId"  />
                            </div>
                        </div>
                    </div>
                    <hr class="hr-dashed hr-menu my-0" />
                    <div class="col-md-12 mt-3">
                        <div class="table-responsive" v-if="bom.bomSaleOrderItem && bom.bomSaleOrderItem.length > 0">
                            <table class="table mb-0" >
                                <thead class="bg-secondary">
                                    <tr>
                                        <th class="text-left"><b>Product DisplayName</b></th>
                                        <th class="text-center"><b> Unit Per Pack </b></th>
                                        <th class="text-center"><b> Unit Name </b></th>
                                        <th class="text-center"><b> Unit Price </b></th>
                                        <th class="text-center"><b> Current Quantity </b></th>
                                        <th class="text-center"><b> Selected Quantity </b></th>
                                        <th class="text-center"><b> Action </b></th>
                                    </tr>
                                </thead>
                                <tbody class="mt-1">
                                    <template v-for="brand in bom.bomSaleOrderItem" :key="brand.id">
                                        <tr class="bgColor">
                                            <td class="text-left">{{ brand.displayName }}</td>
                                            <td class="text-center">{{ brand.unitPerPack }}</td>
                                            <td class="text-center">{{ brand.unitName }}</td>
                                            <td class="text-center">{{ brand.unitPrice }}</td>
                                            <td class="text-center">{{ brand.currentQuantity }}</td>
                                            <td class="text-center">
                                                <decimal-to-fixed v-model="brand.quantity" @change="calcuateQuantity(brand.id)" />
                                            </td>
                                            <td class="text-center">
                                                <a href="javascript:void(0);" @click="removeFromRow(brand.id)">
                                                    <i class="las la-trash-alt text-dark font-16"></i>
                                                </a>
                                            </td>
                                        </tr>
                                        <tr >
                                            <td :colspan="7"> <!-- Adjust colspan based on the number of columns in your table -->
                                                <recipe-item
                                                    @update:modelValue="SavePurchaseItems"
                                                    @change="testFunc(brand.id)"
                                                    :key="purchaseItemRander"
                                                    :rawProducts="brand.bomRawProducts"
                                                    :wareHouseId="bom.wareHouseId"
                                                    :fromBom="true"
                                                />
                                            </td>
                                        </tr>
                                    </template>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
              </div>
            </div>
        </div>

        <div v-if="!loading" class=" col-lg-12 invoice-btn-fixed-bottom">
            <div class="row">
                <div v-if="!loading" class=" col-md-12">
                    <div class="button-items" v-if="bom.id=='00000000-0000-0000-0000-000000000000' ">
                        <button class="btn btn-outline-primary" v-bind:disabled="v$.bom.$invalid || (bom.bomSaleOrderItem && bom.bomSaleOrderItem.length <= 0 ) || isCurrentQuantityLess" v-if="bom.id=='00000000-0000-0000-0000-000000000000'" v-on:click="savePurchase('Draft')"><i class="far fa-save "></i> {{ $t('Save As Draft') }}</button>
                        <button class="btn btn-outline-primary" v-bind:disabled="v$.bom.$invalid || (bom.bomSaleOrderItem && bom.bomSaleOrderItem.length <= 0 ) || isCurrentQuantityLess" v-if="bom.id=='00000000-0000-0000-0000-000000000000'" v-on:click="savePurchase('Post')"><i class="far fa-save "></i> {{ $t('Save As Post') }}</button>

                        <button class="btn btn-danger" v-on:click="Cancel">{{ $t('AddCustomer.Cancel') }}</button>
                    </div>
                    <div class="button-items" v-else>
                        <button class="btn btn-outline-primary" v-bind:disabled="v$.bom.$invalid || (bom.bomSaleOrderItem && bom.bomSaleOrderItem.length <= 0 ) || isCurrentQuantityLess" v-if="status == 'Edit'" v-on:click="savePurchase('Post')"><i class="far fa-save "></i> {{ $t('Save As Post') }}</button>
                        <button class="btn btn-danger" v-on:click="Cancel">{{ $t('AddCustomer.Cancel') }}</button>
                    </div>
                </div>
            </div>
        </div>
        <loading :name="loading" v-model:active="loading" :can-cancel="false" :is-full-page="true"></loading>
    </div>
</template>

<script>
    import clickMixin from '@/Mixins/clickMixin'
   import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    import moment from 'moment'
    import { required} from '@vuelidate/validators'; 
    import useVuelidate from '@vuelidate/core'
    

    export default ({
        mixins: [clickMixin],
        components: {
            Loading,
        },
        setup() {
            return { v$: useVuelidate() }
        },
        data: function () {
            return {
                render:0,
                loading: false,
                language: 'Nothing',
                bom:{
                    id: '00000000-0000-0000-0000-000000000000',
                    code:'',
                    date:'',
                    createdDate:'',
                    saleOrderId:'',
                    wareHouseId:'',
                    approvalStatus:'',
                    bomSaleOrderItem:[],
                },
                isSelectedSaleOrder: false,
                testId:'',

                isCurrentQuantityLess: false,
                saleOrder:'',
                status:''
            }
        },
        validations() {
            return{
                bom: {
                    code: {
                        required
                    },
                    date: {
                        required
                    },
                    saleOrderId: {
                        required
                    },
                    wareHouseId: {
                        required
                    },
                }
            }
        },

        methods: {
            getDate: function (date) {
                return moment(date).format('LLL');
            },
            calcuateQuantity(id)
            {   
                
               var details =  this.bom.bomSaleOrderItem.find(x => x.id == id);
                if(details.currentQuantity < details.quantity)
                {

                    this.$swal({
                        title: 'Error',
                        text: 'you cannot select more than current quantity',
                        type: 'error',
                        icon: 'error',
                        timer: 1500,
                        timerProgressBar: true,
                    })
                    details.quantity = details.currentQuantity
                }
            },
            testFunc(id)
            {
               
                this.testId = id;

                var root = this;
                var count = 0;
                
                root.bom.bomSaleOrderItem.forEach(function (x) {
                   var d = x.bomRawProducts.find(y => y.currentQuantity < y.quantity);
                   if(d != null)
                   {
                        count = count + 1;
                   }
                });

                if(count > 0)
                {
                    root.isCurrentQuantityLess = true;
                }
                else
                {
                    root.isCurrentQuantityLess = false;
                }
            },
            removeFromRow: function (id) {

                let indexOfObjectToRemove = this.findIndexByPropertyValue(this.bom.bomSaleOrderItem, 'id', id);

                if (indexOfObjectToRemove !== -1) {
                    this.bom.bomSaleOrderItem.splice(indexOfObjectToRemove, 1);
                }
            },
            findIndexByPropertyValue(array, property, value) {
                for (let i = 0; i < array.length; i++) {
                    if (array[i][property] === value) {
                    return i;
                    }
                }
                return -1; // Return -1 if not found
            },
            SavePurchaseItems: function (recipeNoItems) {
              
                
                var array = this.bom.bomSaleOrderItem.find(x => x.id == this.testId)
                array.bomRawProducts = Array.from(recipeNoItems);

                
            },
            GetAutoCodeGenerator: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                root.$https
                    .get('/Product/GetBomAutoCode', {
                        headers: {
                            "Authorization": `Bearer ${token}`
                        }
                    }).then(function (response) {
                        if (response.data != null) {
                            root.bom.code = response.data;
                        }
                    });
            },
            GetSaleOrderItems: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                root.$https
                    .get('/Product/GetSaleOrderItems?id=' + root.bom.saleOrderId, {
                        headers: {
                            "Authorization": `Bearer ${token}`
                        }
                    }).then(function (response) {
                        if (response.data != null) {
                            root.bom.bomSaleOrderItem = response.data.saleOrderItems;
                            root.isSelectedSaleOrder = true;
                        }
                    });
            },
            Cancel: function () {
                this.$router.push({
                    path: '/Bom',

                })
            },
            savePurchase: function (status) {

                this.bom.approvalStatus = status;

                this.loading = true;
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                var wareHouseId = localStorage.getItem('WareHouseId');
                var isPos = false;
                if(this.isValid('PosWithTransactionlevel'))
                {
                    isPos = true;
                }
                this.$https
                    .post('/Product/SaveBomInformation?wareHouseId?' + wareHouseId+'&isPos=' + isPos, root.bom, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(response => {
                        if(response.data.isSuccess)
                        {
                            root.loading = false;
                            root.$swal({
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved!' : '!تم الحفظ',
                                text: response.data.isAddUpdate,
                                type: 'success',
                                icon: 'success',
                                timer: 1500,
                                timerProgressBar: true,
                            }).then(function () {
                                    root.$router.push({
                                        path: '/Bom',
                                        query:{
                                            token:'Manufacturing And Production_token'
                                        }
                                    });   
                            });
                        }
                        else
                        {
                            root.loading = false;
                            root.$swal({
                                title: 'Error',
                                text: response.data.isAddUpdate,
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

        },
        created: function () {
            var root =  this;

            if(root.$route.query.Add == 'false')
            {
                root.$route.query.data = this.$store.isGetEdit;
                root.saleOrder = root.$route.query.data.saleOrder;
                root.status = root.$route.query.status;
            }

            this.bom.date = moment().format("DD MMM YYYY hh:mm A");

            this.$emit('update:modelValue', this.$route.name)
            
            if(root.$route.query.data == undefined)
            {
                this.GetAutoCodeGenerator();
                this.bom.createdDate = moment().format("DD MMM YYYY hh:mm A");
            }
            else
            {
                root.bom = root.$route.query.data;                
            }
        },
        mounted: function () {
            this.language = this.$i18n.locale;
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');

        }
    })
</script>
<style scoped>
    .circle-singleline 
    {
        margin: 20px;
        width: 60px;
        height: 60px;
        border-radius: 50%;
        font-size: 30px;
        text-align: center;
        background: blue;
        color: #fff;
        vertical-align: middle;
        line-height: 60px;
    }
    .non-bold-text {
        font-weight: normal;
        font-size: 14px !important;
    }
    .bgColor{
        background-color: #e1eaf8;
    }
</style>
