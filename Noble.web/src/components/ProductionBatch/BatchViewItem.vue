<template>
    <div v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">
        <div class=" table-responsive tableHoverOn">
            <table class="table " v-if="purchaseProducts.length > 0" style="table-layout:fixed;" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                <thead>
                    <tr>
                        <th style="width: 25%;">
                            {{ $t('BatchViewItem.RawProduct') }}
                        </th>
                        <th style="width: 20%;">
                            {{ $t('BatchViewItem.WareHouse') }}
                        </th>
                        <th style="width: 10%;" class="text-center" v-if="isMultiUnit=='true'">
                            {{ $t('BatchViewItem.HighQty') }}
                        </th>
                        <th style="width: 10%;" class="text-center">
                            {{ $t('BatchViewItem.Qty') }}
                        </th>
                        <!--<th style="width: 15%;" class="text-center">
                            {{ $t('PurchaseOrder.TOTALWITHOUTWASTE') }}
                        </th>-->
                        <!--<th style="width: 10%;" class="text-center">
                            {{ $t('PurchaseOrder.WASTE') }}%
                        </th>-->

                    <th style="width: 15%;" class="text-right">
                        {{ $t('BatchViewItem.LineTotal') }}
                    </th>
                    </tr>
                </thead>
                <tbody id="purchase-item">
                    

                        <tr v-for="(prod , index) in purchaseProducts.slice().reverse()" :key="prod.productId + index" style="border-bottom: 1px solid #d6d6d6; ">
                            <td class="border-top-0">
                                <!--<product-dropdown v-model="prod.productId"
                                @update:modelValues="changeProduct($event, prod.rowId, prod.productId)" />-->
                                <span v-if="($i18n.locale == 'en' ||isLeftToRight())">
                                    {{products.find(x => x.id == prod.productId).englishName}}
                                </span>
                                <span v-else>
                                    {{products.find(x => x.id == prod.productId).arabicName}}
                                </span>
                            </td>
                            <td class="border-top-0">
                                {{prod.warehouseName}}
                            </td>

                            <td class="border-top-0 text-center" v-if="isMultiUnit=='true'">
                                {{prod.highQuantity}}
                                <div class="text-center">{{prod.levelOneUnit}}</div>
                            </td>
                            <td class="border-top-0 text-center">
                                {{prod.quantity}}
                                <div class="text-center" v-if="isMultiUnit=='true'">{{prod.basicUnit}}</div>
                            </td>
                            <!--<td class="border-top-0 text-center">
                                {{parseFloat(prod.withoutWastageTotal).toFixed(3).slice(0,-1) | formatAmount}}
                            </td>-->
                            <!--<td class="border-top-0">
                                <input type="number" v-model="prod.waste"
                                       @focus="$event.target.select()" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'"
                                       class="form-control input-border text-center"
                                       @keyup="updateLineTotal($event.target.value, 'waste', prod)" />
                            </td>-->
                            <td class="border-top-0 text-right">
                                {{prod.lineTotal}}
                            </td>
                        </tr>
                    
                </tbody>
            </table>
        </div>

        <!--<hr />-->
        <!--<div class=" table-responsive"
             v-bind:key="rendered + 'g'"
             v-if="purchaseProducts.length > 0">
            <table class="table " style="table-layout:fixed">
                <thead class="m-0">
                    <tr class="text-right">
                        <th class="text-center" style="width:85px;">
                            {{ $t('PurchaseOrder.NoItem') }}
                        </th>
                        <th class="text-center" style="width:100px;">
                            {{ $t('PurchaseOrder.TotalQty') }}
                        </th>
                        <th style="width:100px;">
                            {{ $t('PurchaseOrder.TOTALWITHOUTWASTE') }}
                        </th>
                        <th style="width:100px;">
                            {{ $t('PurchaseOrder.TOTALWITHWASTE') }}
                        </th>

                    </tr>
                </thead>
                <tbody>
                    <tr class="text-right">
                        <td class="text-center">
                            {{ summary.item }}
                        </td>
                        <td class="text-center">
                            {{ summary.qty }}
                        </td>

                        <td>
                             {{ summary.total | formatAmount}}
                        </td>
                        <td>
                               {{ summary.totalWithWaste | formatAmount}}
                        </td>

                    </tr>
                </tbody>
            </table>
        </div>-->
    </div>
</template>

<style scoped>
    #sale-item td {
        padding-bottom: 0px;
        padding-top: 0px;
    }

    .input-border {
        border: transparent;
        background-color: transparent !important;
    }

        .input-border:focus {
            outline: none !important;
            border: none !important;
        }

    .multiselect__tags {
        background-color: transparent !important;
    }

    .multiselect__input, .multiselect__single {
        background-color: transparent !important;
    }

    #purchase-item tr td {
        vertical-align: baseline;
    }

    .tableHoverOn {
        background-color: #F4F6FC !important;
    }
</style>

<script>
    //
    
    //import moment from "moment";
    //import Vue3Barcode from 'vue3-barcode'
    import clickMixin from '@/Mixins/clickMixin'
    export default {
        mixins: [clickMixin],
        name: "ProductionBatchItem",
        props: ['purchase'],

        data: function () {
            return {
                rendered: 0,
                product: {
                    id: "",
                },
                products: [],
                purchaseProducts: [],
                loading: false,

                summary: {
                    item: 0,
                    qty: 0,
                    total: 0,
                    totalWithWaste: 0,
                },

                searchTerm: '',
                isMultiUnit: ''
            };
        },
        validations() {},
        filters: {

        },
        methods: {

            changeProduct: function (NewProdId, rowId) {
                this.purchaseProducts = this.purchaseProducts.filter(x => x.rowId != rowId);
                this.addProduct(NewProdId);

            },
            calcuateSummary: function () {

                this.summary.item = this.purchaseProducts.length;
                this.summary.qty = this.purchaseProducts.reduce(
                    (qty, prod) => qty + parseInt(prod.quantity), 0);
                this.summary.total = this.purchaseProducts
                    .reduce((total, prod) => total + (prod.highQuantity * prod.unitPerPack + parseFloat(prod.quantity) * this.purchase.noOfBatches), 0)
                    .toFixed(3).slice(0,-1);
                this.summary.totalWithWaste = this.purchaseProducts
                    .reduce((total, prod) => total + (((prod.highQuantity * prod.unitPerPack) + parseFloat(prod.quantity)) * this.purchase.noOfBatches) - parseFloat(((((prod.highQuantity * prod.unitPerPack) + parseFloat(prod.quantity)) * prod.waste) * this.purchase.noOfBatches) / 100), 0)
                    .toFixed(3).slice(0,-1);
                this.$emit("update:modelValue", this.purchaseProducts);
            },

            updateLineTotal: function (e, prop, product) {


                var prod = this.products.find((x) => x.id == product.productId);
                if (prop == "highQuantity") {
                    if (e < 0) {
                        e = 0;
                    }
                    product.highQuantity = e;
                }
                if (prop == "quantity") {
                    if (e < 0) {
                        e = 0;
                    }
                    product.quantity = e;
                }

                if (prop == "waste") {
                    if (e < 0) {
                        e = 0;
                    }
                    product.waste = e;
                }
                
                if (prod.inventory != null) {
                    if (parseFloat(product.quantity) > prod.inventory.currentQuantity) {
                        product['outOfStock'] = true;
                    } else {
                        product['outOfStock'] = false;
                    }
                } else {
                    product['outOfStock'] = false;
                }
                product.withoutWastageTotal = (product.highQuantity * product.unitPerPack + parseFloat(product.quantity)) * this.purchase.noOfBatches;


                if (product.waste != 0) {
                    product.lineTotal = ((product.highQuantity * product.unitPerPack) + parseFloat(product.quantity)) * this.purchase.noOfBatches - parseFloat(((((product.highQuantity * product.unitPerPack) + parseFloat(product.quantity)) * product.waste) * this.purchase.noOfBatches) / 100);
                }
                else {
                    product.lineTotal = product.highQuantity * product.unitPerPack + parseFloat(product.quantity) * this.purchase.noOfBatches;
                }
                this.purchaseProducts['product'] = product;

                this.calcuateSummary();

                this.$emit("update:modelValue", this.purchaseProducts);

            },

            addProduct: function (productId, newProduct) {

                if (this.products.find(x => x.id == newProduct.id) == undefined || this.products.length <= 0) {
                    this.products.push(newProduct);
                }

                var prod = this.products.find((x) => x.id == productId);

                this.purchaseProducts.push({
                    rowId: this.createUUID(),
                    productId: prod.id,
                    unitPrice: 0,
                    levelOneUnit: prod.levelOneUnit,
                    highQuantity: 0,
                    unitPerPack: prod.unitPerPack,
                    basicUnit: prod.unit == null ? '' : prod.unit.name,
                    quantity: 0,
                    waste: 0,
                    lineTotal: 0,
                    withoutWastageTotal: 0,
                    wareHouseId: '',
                });
                this.product.id = "";
                this.rendered++;
            },
            createUUID: function () {
                var dt = new Date().getTime();
                var uuid = 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
                    var r = (dt + Math.random() * 16) % 16 | 0;
                    dt = Math.floor(dt / 16);
                    return (c == 'x' ? r : (r & 0x3 | 0x8)).toString(16);
                });
                return uuid;
            },


            removeProduct: function (id) {

                this.purchaseProducts = this.purchaseProducts.filter((prod) => {
                    return prod.rowId != id;
                });

                this.calcuateSummary();
            },

            getData: function () {

                var root = this;
                if (root.purchase != undefined) {
                    root.purchase.productionBatchItems.forEach(function (item) {
                        root.purchaseProducts.push({
                            rowId: item.id,
                            id: item.id,
                            product: item.product,
                            productId: item.productId,
                            quantity: item.quantity,
                            unitPerPack: item.unitPerPack,
                            basicUnit: item.unit == null ? '' : item.unit.name,
                            levelOneUnit: item.levelOneUnit,
                            highQuantity: item.highQuantity,
                            waste: item.waste,
                            wareHouseId: item.wareHouseId,
                            warehouseName: item.warehouseName,
                        });
                    });

                    for (var k = 0; k < root.purchaseProducts.length; k++) {
                        
                        root.products.push(root.purchaseProducts[k].product);

                        root.updateLineTotal(root.purchaseProducts[k].quantity, "quantity", root.purchaseProducts[k]);
                        root.updateLineTotal(root.purchaseProducts[k].unitPerPack, "unitPerPack", root.purchaseProducts[k]);
                        root.updateLineTotal(root.purchaseProducts[k].basicUnit, "basicUnit", root.purchaseProducts[k]);
                        root.updateLineTotal(root.purchaseProducts[k].levelOneUnit, "levelOneUnit", root.purchaseProducts[k]);
                        root.updateLineTotal(root.purchaseProducts[k].highQuantity, "highQuantity", root.purchaseProducts[k]);
                        root.updateLineTotal(root.purchaseProducts[k].waste, "waste", root.purchaseProducts[k]);
                    }
                    root.calcuateSummary()
                }

            },
        },
        created: function () {

            this.getData();
        },
        mounted: function () {
            this.isMultiUnit = localStorage.getItem('IsMultiUnit');
        },
    };
</script>
<style scoped>
    .tableHoverOn {
        background-color: #F4F6FC !important;
    }
</style>

