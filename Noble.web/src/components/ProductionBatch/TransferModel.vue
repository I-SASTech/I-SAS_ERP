<template>
    <modal :show="show" :modalLarge="true" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">

        <div style="margin-bottom:0px" class="card">
            <div class="card-body">
                <div class="col-lg-12">
                    <div class="tab-content" id="nav-tabContent">

                        <div class="modal-header">

                            <h5 class="modal-title DayHeading" id="myModalLabel"> {{ $t('TransferModel.TransferBatch') }}</h5>

                        </div>

                        <div class="card-body">
                            <div class="row">
                                <div class="table-responsive">
                                    <table class="table table-striped table-hover table_list_bg">
                                        <thead class="">
                                            <tr>
                                                <th>
                                                    {{ $t('TransferModel.ProductionBatchNo') }}
                                                </th>

                                                <th>
                                                    {{ $t('TransferModel.TotalQuantity') }}
                                                </th>
                                                <th>
                                                    {{ $t('TransferModel.RemainingQuantity') }}

                                                </th>
                                                <th>
                                                    {{ $t('TransferModel.DamageQuantity') }}

                                                </th>


                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>

                                                <td>
                                                    {{purchase.registrationNumber}}
                                                </td>
                                                <td>
                                                    {{purchase.netAmount}}
                                                </td>
                                                <td>
                                                    {{purchase.remainingStock}}
                                                </td>
                                                <td>
                                                    {{purchase.damageStock}}
                                                </td>


                                            </tr>
                                        </tbody>
                                    </table>

                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group has-label col-sm-6 ">
                                    <label class="text  font-weight-bolder">   {{ $t('TransferModel.UnitPrice') }} : <span class="text-danger"> *</span></label>
                                    <input class="form-control" v-model="purchase.unitprice" type="number" />

                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group has-label col-sm-6 ">
                                    <label class="text  font-weight-bolder"> {{ $t('TransferModel.RemainingStockWarehouse') }}: <span class="text-danger"> *</span></label>
                                    <warehouse-dropdown v-model="purchase.remainingWareHouse" />

                                </div>
                                <div class="form-group has-label col-sm-6 ">
                                    <label class="text  font-weight-bolder"> {{ $t('TransferModel.DamgeStockWarehouse') }} : <span class="text-danger"> *</span></label>
                                    <warehouse-dropdown v-model="purchase.damageWareHouse" />

                                </div>
                            </div>

                        </div>


                        <div class="modal-footer justify-content-right">

                            <button type="button" class="btn btn-primary  " v-on:click="SaveOrigin" v-bind:disabled="v$.purchase.$invalid">  {{ $t('TransferModel.btnSave') }}</button>
                            <button type="button" class="btn btn-danger  mr-3 " v-on:click="close()"> {{ $t('TransferModel.btnClear') }}</button>

                        </div>
                        <div class="card-footer col-md-3" v-if="loading">
                            <loading v-model:active="loading" :can-cancel="true" :is-full-page="true"></loading>
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </modal>

</template>
<script>
    

    import clickMixin from '@/Mixins/clickMixin'
    
    import { required } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'
    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    export default {
        components: {
            Loading
        },
        mixins: [clickMixin],
        props: ['show', 'newPurchase'],
        setup() {
            return { v$: useVuelidate() }
        },
        data: function () {
            return {
                purchase: this.newPurchase,
                reason: false,
                loading: false,

                UserName: ''

            }
        },
        validations() {
            return {
                purchase: {
                    remainingStock: {
                        required
                    },
                    damageWareHouse: {
                        required
                    },
                    unitprice: {
                        required
                    },

                }
                }
        },
        methods: {
           
            close: function () {



                this.$emit('close', false);


            },

            SaveOrigin: function () {

                this.purchase.approvalStatus = 'Transfer';
                var root = this;
                var token = '';
                this.loading = true;
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.$https.post('/Batch/BatchStatus', this.purchase, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    
                    if (response.data) {
                        
                        root.loading = false;
                        root.$swal({
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved!' : '!تم الحفظ',
                            text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved Successfully!' : '!حفظ بنجاح',
                            type: 'success',
                            icon: 'success',
                            showConfirmButton: false,
                            timer: 1500,
                            timerProgressBar: true,
                        });
                        root.close();
                        root.$emit('RefreshList', 'Transfer');
                    }

                    else {
                        root.$swal({
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                            text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'There is some Error On Status Change!' : 'هناك بعض الخطأ في تغيير الحالة!',
                            type: 'error',
                            icon: 'error',
                            showConfirmButton: false,
                            timer: 1500,
                            timerProgressBar: true,
                        });
                    }
                });
            }
        },
        mounted: function () {
            
            this.UserName = localStorage.getItem('UserName');
            this.purchase.transferBy = this.UserName;
        }
    }
</script>
