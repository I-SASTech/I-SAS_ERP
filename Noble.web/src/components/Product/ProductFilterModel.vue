<template>
    <modal :show="show">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title" id="myModalLabel">{{ $t('ProductFilterModel.InventoryInformation') }}</h5>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="form-group has-label col-sm-12 ">
                        <label class="text"> {{ $t('ProductFilterModel.SelectItem') }} : </label>
                        <product-dropdown :emptyselect="true" v-model="productId" @update:modelValues="ProductDetail" />
                    </div>
                    <div class="col-sm-12" v-for="(inv) in inventory" v-bind:key="inv.id">
                        <div class="row">
                            <div class="form-group has-label col-sm-8 "
                                 v-if="($i18n.locale == 'en' || isLeftToRight())">
                                <label class="text"> {{ $t('ProductFilterModel.WareHouseName') }} : </label>
                                <input class="form-control" readonly type="text" :placeholder="inv.warehouseName">
                            </div>
                            <div class="form-group has-label col-sm-8 "
                                 v-else-if="$i18n.locale == 'ar' & inv.warehouseNameArabic != ''">
                                <label class="text"> {{ $t('ProductFilterModel.WareHouseName') }} : </label>
                                <input class="form-control" readonly type="text" :placeholder="inv.warehouseNameArabic">
                            </div>
                            <div class="form-group has-label col-sm-8 " v-else>
                                <label class="text"> {{ $t('ProductFilterModel.WareHouseName') }} : </label>
                                <input class="form-control" readonly type="text" :placeholder="inv.warehouseName">
                            </div>
                            <div class="form-group has-label col-sm-4 ">
                                <label class="text"> {{ $t('ProductFilterModel.Quantity') }} : </label>
                                <input class="form-control" readonly type="text" :placeholder="inv.currentQuantity">
                            </div>
                        </div>

                    </div>
                    <div class="form-group has-label col-sm-12 ">
                        <label class="text"> {{ $t('ProductFilterModel.TotalQty') }} : </label>
                        <input class="form-control" readonly type="text" :placeholder="totalQuantity">
                    </div>
                </div>
            </div>
            <div class="modal-footer justify-content-right">
                <button type="button" class="btn btn-outline-danger" v-on:click="close()">
                    {{
                        $t('ProductFilterModel.btnClear')
                    }}
                </button>

            </div>

            <loading v-model:active="loading" :can-cancel="true" :is-full-page="true"></loading>

        </div>

    </modal>

</template>
<script>
    
    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';

    import clickMixin from '@/Mixins/clickMixin'

    export default {
        components: {
            Loading
        },
        mixins: [clickMixin],
        props: ['show'],
        data: function () {
            return {

                loading: false,
                productId: '',
                inventory: [],
                totalQuantity: 0,


            }
        },
        methods: {

            close: function () {



                this.$emit('close', false);


            },
            ProductDetail: function (prod) {


                
                var root = this;

                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.totalQuantity = 0;
                root.$https.get('/Product/ProductInventoryDetailQuery?id=' + prod, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {

                        if (response.data) {

                            root.inventory = response.data;

                            root.inventory.forEach(function (x) {
                                root.totalQuantity += x.currentQuantity
                            });


                        } else {
                            console.log("error: something wrong from db.");
                        }
                    },
                        function (error) {
                            root.loading = false;
                            console.log(error);
                        });

            },
        },
        mounted: function () {


        }
    }
</script>
