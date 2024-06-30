<template>
    <modal :show="show" :modalLarge="true">
        <div class="modal-content">
            <div class="modal-header">
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel"> Invoice Return</h6>
                <button type="button" class="btn-close" v-on:click="close()"></button>
            </div>
            <div class="modal-body">
                <div class="row ">
                    <div class="col-sm-12">
                        <table class="table">
                            <thead class="bg-light-alt">
                                <tr class="tbl_border">
                                    <th style="width: 5%;">#</th>
                                    <th style="width: 30%;">
                                        {{ $t('ReturnItemModel.Product') }}
                                    </th>
                                    <th class="text-center" style="width: 15%;">
                                        {{ $t('ReturnItemModel.UnitPrice') }}
                                    </th>
                                    <th class="text-center" style="width: 10%;">
                                        {{ $t('ReturnItemModel.Quantity') }}
                                    </th>
                                    <th class="text-center" style="width: 20%;">
                                        {{ $t('ReturnItemModel.ReturnQty') }}
                                    </th>
                                    <th class="text-center" style="width: 10%;">
                                        {{ $t('ReturnItemModel.Expire') }}
                                    </th>
                                    <th style="width: 10%;"></th>
                                </tr>
                            </thead>
                            <tbody>

                                <tr v-for="(prod, index) in saleDetail.saleItems" :key="prod.id" class="tbl_border" v-bind:class="{'alert-danger':prod.quantity<prod.returnQuantity || prod.outOfDate}">
                                    <td>{{index+1}}</td>
                                    <td>
                                        {{($i18n.locale == 'en' ||isLeftToRight())?prod.productName==''? prod.arabicName:prod.productName : prod.arabicName==''?prod.productName:prod.arabicName}}
                                    </td>
                                    <td class="text-center">
                                        {{parseFloat(prod.unitPrice).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                    </td>
                                    <td class="text-center" :key="quantityRander">
                                        {{prod.quantity - (prod.returnQuantity != undefined && prod.returnQuantity != '' ? prod.returnQuantity : 0)}}
                                    </td>
                                    <td>
                                        <input v-model="prod.returnQuantity"
                                               type="number" v-shortkey.avoid
                                               @focus="$event.target.select()"
                                               class="form-control text-center"
                                               @keyup="updateLineTotal($event.target.value, 'returnQuantity', prod)" />
                                    </td>
                                    <td class="text-center">
                                        {{prod.outOfDate? 'Expire':''}}
                                    </td>
                                    <td class="text-end">
                                        <a href="javascript:void(0);" @click="removeProduct(prod.id)"><i class="las la-trash-alt text-secondary font-16"></i></a>

                                    </td>
                                </tr>

                            </tbody>
                        </table>

                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="SubmitReturnItem()" v-bind:disabled="saleDetail.saleItems.filter(x => x.quantity< x.returnQuantity).length > 0 || saleDetail.saleItems.filter(x => x.outOfDate).length > 0">{{ $t('ReturnItemModel.Select') }}</button>
                <button type="button" class="btn btn-soft-secondary btn-sm" v-on:click="close()">{{ $t('ReturnItemModel.btnClear') }}</button>
            </div>
        </div>
    </modal>
</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'
    import moment from "moment";
    export default {
        mixins: [clickMixin],
        props: ['show', 'postinvoicedetail'],
        data: function () {
            return {
                saleDetail: '',
                saleList: [],
                save: false,
                quantityRander: 0,
                sale: {},
            }
        },
        methods: {
            SubmitReturnItem: function () {
                var root = this;
                root.saleDetail.isReturnItem = true;
                root.$emit('EditTOuchInvocie', root.saleDetail);
                root.close();
            },
            removeProduct: function (id) {

                this.saleDetail.saleItems = this.saleDetail.saleItems.filter((prod) => {
                    return prod.id != id;
                });

            },
            updateLineTotal: function (e, prop, product) {

                if (e != undefined) {
                    if (prop == "quantity") {
                        product.quantity = e;
                    }
                    if (prop == "returnQuantity") {
                        if (e < 0) {
                            e = 0;
                        }
                        product.returnQuantity = e;
                    }
                    if (product.product.saleReturnDays == 0 || product.product.saleReturnDays == '' || product.product.saleReturnDays == null) {
                        product['outOfDate'] = false
                        
                    }
                    else {
                        var invoiceDate = moment(this.saleDetail.date).add(product.product.saleReturnDays, 'days').format("DD MMM YYYY");
                        var todayDate = moment().format("DD MMM YYYY");
                        var isTrue = moment(invoiceDate).isSameOrAfter(todayDate);
                        if (isTrue) {
                            product['outOfDate'] = false
                        }
                        else {
                            product['outOfDate'] = true
                        }
                    }
                }

            },

            close: function () {
                this.$emit('close', false);
            },

        },
        created: function () {
            this.sale = this.postinvoicedetail;
            this.sale.saleItems = this.sale.saleItems.filter(x => x.remainingQuantity > 0);
            this.saleDetail = this.sale;
        },
        mounted: function () {
            var root = this;
            root.saleDetail.saleItems.forEach(function (item) {
                root.updateLineTotal(item.remainingQuantity, "quantity", item);
            });
        }
    }
</script>