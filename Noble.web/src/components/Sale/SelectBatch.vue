<template>
    <div v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">
        <modal :show="show">
            <div class="modal-header">

                <div class="row">
                    <div class="col-9 text-left">
                        <h5 class="modal-title DayHeading" id="myModalLabel"> Select Batch</h5>
                    </div>
                    <div class="col-3">
                        <button class="btn btn-outline-danger btn-sm mt-0" v-on:click="close">
                            Close
                        </button>
                    </div>
                </div>

            </div>
            <div class="modal-body">
                <div class="row">

                    <div class="col-lg-12">

                        <div class="mt-2">
                            <div class=" table-responsive">
                                <table class="table ">
                                    <thead class="m-0">
                                        <tr>
                                            <th>#</th>
                                            <th>
                                                Batch Number
                                            </th>
                                            <th>
                                                Expiry
                                            </th>
                                            <th>
                                                Price
                                            </th>

                                            <th>
                                                Quantity
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr v-for="(inv, index) in product.inventoryBatch" v-bind:key="index">
                                            <td>
                                                {{index+1}}
                                            </td>

                                            <td>
                                                <strong>
                                                    <a href="javascript:void(0)" v-on:click="SelectedBatch(inv)">{{inv.batchNumber}}</a>

                                                </strong>
                                            </td>
                                            <td>
                                                {{getDate(inv.expiryDate)}}
                                            </td>
                                            <td>
                                                {{inv.price}}
                                            </td>
                                            <td>
                                                {{inv.remainingQuantity}}
                                            </td>

                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </modal>
    </div>
</template>
<script>
    import moment from 'moment'
    export default {
        name: 'SelectBatch',
        props: ["selectedproduct", "show"],
        data: function () {
            return {

                type: '',
                message: '',
                username: '',
                product: {}
            }
        },
        methods: {
            getDate: function (x) {
                return moment(x).format("l");
            },
            SelectedBatch: function (batch) {
                this.product.batchNo = batch.batchNumber;
                this.product.batchExpiry = batch.expiryDate;
                this.product.currentQuantity = batch.remainingQuantity;
                this.product.inventory.currentQuantity = batch.remainingQuantity;

                this.$emit('update:modelValue', this.product, batch);
            },
            close: function () {
                this.$emit('close');
            },
        },
        created: function () {
            this.product = this.selectedproduct;
        },
    }
</script>